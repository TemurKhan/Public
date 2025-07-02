Imports System.ComponentModel
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Xml
Imports Microsoft.Data.SqlClient
Imports Org.BouncyCastle.Asn1
Imports DataRow = System.Data.DataRow
Imports DataTable = System.Data.DataTable

'Imports DataRow = System.Data.DataRow
'Imports DataTable = System.Data.DataTable
'Imports SqlCommand = Microsoft.Data.SqlClient.SqlCommand
'Imports SqlConnection = Microsoft.Data.SqlClient.SqlConnection
'Imports SqlDataAdapter = Microsoft.Data.SqlClient.SqlDataAdapter
'Imports SqlDataReader = Microsoft.Data.SqlClient.SqlDataReader

Public Class CheckEligibility

    Dim payerrbid As String
    Dim patientid As String
    Dim UserInfo() As String
    Shared ACCIDs As String
    'Private Function GetMyUserInfo() As String()
    '    Dim LIC As New LicenseManager.ProlisLicense(connstring, "Prolis")
    '    Dim UserInfo() As String = {"", ""}
    '    Dim CNP As New ADODB.Connection
    '    CNP.Open(connstring)
    '    Dim Rs As New ADODB.Recordset
    '    Rs.Open("select * from partners where Name like 'Ability%'",
    '    CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '    If Not Rs.BOF Then
    '        If Rs.Fields("UserName").Value IsNot DBNull.Value _
    '        AndAlso Trim(Rs.Fields("UserName").Value) <> "" Then _
    '        UserInfo(0) = Trim(Rs.Fields("UserName").Value)
    '        '
    '        If Rs.Fields("Password").Value IsNot DBNull.Value _
    '        AndAlso Trim(Rs.Fields("Password").Value) <> "" Then _
    '        UserInfo(1) = LIC.decryptString(Trim(Rs.Fields("Password").Value))
    '    End If
    '    Rs.Close()
    '    Rs = Nothing
    '    Return UserInfo
    'End Function

    Private Function GetMyUserInfo() As String()
        Dim LIC As New LicenseManager.ProlisLicense(connString, "Prolis") ' Old: Dim LIC As New LicenseManager.ProlisLicense(connstring, "Prolis")
        Dim UserInfo() As String = {"", ""} ' Old: Dim UserInfo() As String = {"", ""}
        Dim DtPartners As New DataTable() ' Used to replace ADODB.Recordset

        Using connection As New Microsoft.Data.SqlClient.SqlConnection(connString) ' Old: Dim CNP As New ADODB.Connection
            connection.Open() ' Old: CNP.Open(connstring)

            Dim sSQL As String = "SELECT * FROM partners WHERE Name LIKE 'Ability%'" ' Old: Rs.Open("select * from partners where Name like 'Ability%'"
            Using cmd As New Microsoft.Data.SqlClient.SqlCommand(sSQL, connection)
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(DtPartners) ' Populate the DataTable
                End Using
            End Using
        End Using

        If DtPartners.Rows.Count > 0 Then ' Old: If Not Rs.BOF Then
            Dim row As DataRow = DtPartners.Rows(0)

            If row("UserName") IsNot DBNull.Value AndAlso Trim(row("UserName").ToString()) <> "" Then ' Old: If Rs.Fields("UserName").Value IsNot DBNull.Value AndAlso Trim(Rs.Fields("UserName").Value) <> ""
                UserInfo(0) = Trim(row("UserName").ToString()) ' Old: UserInfo(0) = Trim(Rs.Fields("UserName").Value)
            End If

            If row("Password") IsNot DBNull.Value AndAlso Trim(row("Password").ToString()) <> "" Then ' Old: If Rs.Fields("Password").Value IsNot DBNull.Value AndAlso Trim(Rs.Fields("Password").Value) <> ""
                UserInfo(1) = LIC.decryptString(Trim(row("Password").ToString())) ' Old: UserInfo(1) = LIC.decryptString(Trim(Rs.Fields("Password").Value))
            End If
        End If

        Return UserInfo ' Old: Return UserInfo
    End Function
    Public Function Eligibility(accid As String) As String
        'Public Function Eligibility(accid As String, cnu As ADODB.Connection, connstring As String) As String
        ACCIDs = accid

        UserInfo = GetMyUserInfo()

        Dim msgstr As String = "" 'CheckEligibilityRecord(accid)
        If msgstr = "" Then
            msgstr = Execute(accid)
        Else

        End If
        Return msgstr
    End Function




    Function Execute(ByVal accid As String) As String
        'check for existing record in db , if not process eligibility check

        Dim ts As String = ""
        Dim dt As Date = Now()
        ts = dt.ToString("yyyy-MM-dd HH:mm:ss")
        Dim PldID As String = Date.Now.ToString("0ddHHmmss")
        Dim soapstring As String = SoapEnvelop(PldID, accid).ToString
        Dim soapResult As String = ""
        Try
            If soapstring <> "" Then
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim request As HttpWebRequest = CreateWebRequest()

                Dim soapEnvelopeXml As New XmlDocument()
                soapEnvelopeXml.LoadXml(soapstring)
                Using stream As Stream = request.GetRequestStream()
                    soapEnvelopeXml.Save(stream)
                End Using

                Using response As WebResponse = request.GetResponse()
                    Using rd As New StreamReader(response.GetResponseStream())
                        soapResult = rd.ReadToEnd()

                    End Using
                End Using
                ProcessResponse(soapResult)
            Else
                soapResult = "Payload string is empty , Request not processed"

            End If

        Catch ex As Exception
            soapResult = ex.Message

        End Try
        ProcessResponse(soapResult)
        Return soapResult

    End Function

    ' Create a soap webrequest to [Url]
    Public Shared Function CreateWebRequest() As HttpWebRequest
        Dim Request As HttpWebRequest = DirectCast(WebRequest.Create("https://eligibilityone.abilitynetwork.com/EligibilityOneCORE.asmx?op=COREEnvelopeRealTimeRequest"), HttpWebRequest)
        Request.Headers.Add("SOAP:Action")
        Request.ContentType = "text/xml;charset=""utf-8"""
        Request.Accept = "text/xml"
        Request.Method = "POST"
        Return Request
    End Function

    Function Payload(ByVal PldID As String, ByVal accid As String) As StringBuilder
        Dim DT2Y As String = Date.Now.ToString("yyMMdd")
        Dim DT4Y As String = Date.Now.ToString("yyyyMMdd")
        Dim TM As String = Date.Now.ToString("HHmm")
        Dim ts As String = ""

        Dim dt As Date = Now()

        Dim tms As String = dt.ToString("HHmmss")
        ts = dt.ToString("yyMMdd")
        TM = dt.ToString("HHMM")
        'Dim pid As String = dt.ToString("yyMMddHHMMSS")
        Dim ppid As String = "20" & ts
        'Dim db As String = dob.ToString("yyyyMMdd")
        ' Dim sdate As String = fromdt.ToString("yyyyMMdd") & "-" & todate.ToString("yyyyMMdd")
        Dim payloadxi As New StringBuilder
        Dim payerinfo As String = GetPayerInfo(accid).ToString
        Dim providerinfo As String = ""
        Dim subscriberinfo As String = ""
        If payerinfo <> "" Then
            providerinfo = GetProviderInfo().ToString
        Else
            payloadxi.Append("")
        End If
        If payerinfo <> "" AndAlso providerinfo <> "" Then
            subscriberinfo = GetSubscriberinfo(accid).ToString
        Else
            payloadxi.Append("")
        End If
        Dim cptcodes As String = GetCPTCodes(accid).ToString
        cptcodes = Replace(cptcodes, vbCrLf, "")
        'check if record is already available
        If payerinfo <> "" AndAlso providerinfo <> "" AndAlso subscriberinfo <> "" Then
            Dim segstr As String = payerinfo & providerinfo & subscriberinfo & cptcodes
            Dim ar() As String = segstr.Split("~")
            Dim segments As Integer = ar.Length + 7
            payloadxi.Append("<![CDATA[" & BuildISA(PldID, UserInfo) & BuildGS(UserInfo) &
            "ST*270*" & PldID & "*005010X279A1~BHT*0022*13*" & PldID & "*" & DT4Y & "*" & TM & "~" &
            segstr & "SE*" & segments.ToString & "*" & PldID & "~GE*1*" & PldID & "~IEA*1*" & PldID & "~]]>")
        End If
        Return payloadxi
    End Function

    Function SoapEnvelop(ByVal PldID As String, ByVal AccID As String) As StringBuilder
        Dim ts As String = ""
        Dim dt As Date = Now()
        ts = dt.ToString("yyyy-MM-dd HH:mm:ss")
        ts = ts & "Z"
        ts = ts.Replace(" ", "T")
        Dim plstr As String = Payload(PldID, AccID).ToString

        Dim envelop As New StringBuilder
        If plstr <> "" Then

            envelop.Append("<soap:Envelope xmlns:soap=""http://www.w3.org/2003/05/soap-envelope"" xmlns:wsse=""wsse"" xmlns:cor =""http://www.caqh.org/SOAP/WSDL/CORERule2.2.0.xsd"">")
            envelop.Append("<soap:Header>")
            envelop.Append("<wsse:Security>")
            envelop.Append("<wsse:UsernameToken>")
            envelop.Append("<wsse:Username>" & UserInfo(0) & "</wsse:Username>")
            envelop.Append("<wsse:Password>" & UserInfo(1) & "</wsse:Password>")
            envelop.Append("</wsse:UsernameToken>")
            envelop.Append("</wsse:Security>")
            envelop.Append("</soap:Header>")
            envelop.Append("<soap:Body>")
            envelop.Append("<cor:COREEnvelopeRealTimeRequest>")
            envelop.Append("<cor:PayloadType>X12_270_Request_005010X279A1004010X092A1</cor:PayloadType>")
            envelop.Append("<cor:ProcessingMode>RealTimeMode</cor:ProcessingMode>")
            envelop.Append("<cor:PayloadID>" & PldID & "</cor:PayloadID>")
            envelop.Append("<cor:TimeStamp>" & ts & "</cor:TimeStamp>")
            envelop.Append("<cor:SenderID>" & SenderID & "</cor:SenderID>")
            envelop.Append("<cor:ReceiverID>ABILITY</cor:ReceiverID>")
            envelop.Append("<cor:CORERuleVersion>2.2.0</cor:CORERuleVersion>")
            envelop.Append("<cor:Payload>" & plstr)
            envelop.Append("</cor:Payload>")
            envelop.Append("</cor:COREEnvelopeRealTimeRequest>")
            envelop.Append("</soap:Body>")
            envelop.Append("</soap:Envelope>")
        Else
            envelop.Append("")
        End If
        Return envelop
    End Function

    Function GetProviderInfo() As StringBuilder
        Dim provinfo As New StringBuilder ' Old: Dim provinfo As New StringBuilder
        Dim providername As String = "" ' Old: Dim providername As String = ""
        Dim npi As String = "" ' Old: Dim npi As String = ""
        Dim Degree As String = "" ' Old: Dim Degree As String = ""
        Dim provfirstname As String = "" ' Old: Dim provfirstname As String = ""
        Dim provlastname As String = "" ' Old: Dim provlastname As String = ""

        Using connection As New SqlConnection(connString) ' Old: Dim Rs As New ADODB.Recordset
            connection.Open() ' Old: Rs.Open()

            Dim query As String = "SELECT TOP(1) * FROM company" ' Old: Rs.Open("Select top(1) * from company")
            Using cmd As New SqlCommand(query, connection)
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then ' Old: If Not Rs.BOF Then
                        If Not Convert.ToBoolean(reader("IsIndividual")) Then ' Old: Rs.Fields("IsIndividual").Value = False
                            If reader("LastName_BSN") IsNot DBNull.Value Then providername = reader("LastName_BSN").ToString().Trim()
                            If reader("NPI") IsNot DBNull.Value Then npi = reader("NPI").ToString().Trim()
                            If providername <> "" AndAlso npi <> "" Then
                                provinfo.Append("HL*2*1*21*1~NM1*1P*2*" & providername & "*****XX*" & npi & "~") ' Old: NM1 segment for organization
                            Else
                                ProcessResponse("Provider is an organization. Name of organization and npi are required data for eligibility enquiry")
                            End If
                        Else
                            If reader("Degree") IsNot DBNull.Value Then Degree = reader("Degree").ToString()
                            If reader("LastName_BSN") IsNot DBNull.Value Then provlastname = reader("LastName_BSN").ToString()
                            If reader("FirstName") IsNot DBNull.Value Then provfirstname = reader("FirstName").ToString()
                            If reader("NPI") IsNot DBNull.Value Then npi = reader("NPI").ToString()
                            If provlastname <> "" AndAlso provfirstname <> "" AndAlso npi <> "" Then
                                provinfo.Append("HL*2*1*21*1~NM1*1P*2*" & provlastname & "*****XX*" & npi & "~") ' Old: NM1 segment for individual
                            Else
                                ProcessResponse("Provider is an individual. First Name, Last Name and npi are required data for eligibility enquiry")
                            End If
                        End If
                    End If
                End Using
            End Using
        End Using ' Old: Rs.Close(), Rs = Nothing

        Return provinfo ' Old: Return provinfo
    End Function

    Function GetPayerInfo(ByVal accid As String) As StringBuilder
        Dim payerinfo As New StringBuilder ' Old: Dim payerinfo As New StringBuilder
        Dim PayerName As String = "" ' Old: Dim PayerName As String = ""
        Dim PayerIdentity As String = "" ' Old: Dim PayerIdentity As String = ""

        Using connection As New SqlConnection(connString) ' Old: Dim cnpin As New Odbc.OdbcConnection(connstring)
            connection.Open() ' Old: cnpin.Open()

            Dim sSQL As String = "
            SELECT a.PayerName, a.PayerCode, a.EligibilityCode 
            FROM payers a 
            INNER JOIN Req_Coverage b ON b.payer_ID = a.ID 
            WHERE b.Preference = 'P' AND b.Accession_ID = @AccessionID" ' Old: Replace hardcoded query

            Using cmd As New SqlCommand(sSQL, connection) ' Old: Dim cmdpin As New Odbc.OdbcCommand(...)
                cmd.Parameters.AddWithValue("@AccessionID", Val(accid)) ' Old: Embed accid into query directly (now parameterized)

                Using reader As SqlDataReader = cmd.ExecuteReader() ' Old: Dim drpin As Odbc.OdbcDataReader = cmdpin.ExecuteReader
                    If reader.HasRows Then ' Old: If drpin.HasRows Then
                        While reader.Read() ' Old: While drpin.Read
                            PayerName = Trim(reader("PayerName").ToString()) ' Old: PayerName = Trim(drpin("PayerName"))

                            If reader("EligibilityCode") IsNot DBNull.Value Then ' Old: If drpin("EligibilityCode") IsNot DBNull.Value
                                PayerIdentity = Trim(reader("EligibilityCode").ToString()) ' Old: PayerIdentity = Trim(drpin("EligibilityCode"))
                            ElseIf reader("PayerCode") IsNot DBNull.Value Then ' Old: ElseIf drpin("PayerCode") IsNot DBNull.Value
                                PayerIdentity = Trim(reader("PayerCode").ToString()) ' Old: PayerIdentity = Trim(drpin("PayerCode"))
                            End If
                        End While
                    End If
                End Using
            End Using
        End Using ' Old: cnpin.Close()

        If PayerName <> "" AndAlso PayerIdentity <> "" Then ' Old: If PayerName <> "" AndAlso PayerIdentity <> ""
            payerinfo.Append("HL*1**20*1~NM1*PR*2*" & PayerName.Trim & "*****PI*" & PayerIdentity.Trim & "~") ' Old: Append HL segment
        Else
            ProcessResponse("Payer Name and the Eligibility Code are required data for eligibility enquiry") ' Old: ProcessResponse(...)
        End If

        Return payerinfo ' Old: Return payerinfo
    End Function


    Function GetSubscriberinfo(ByVal accid As String) As StringBuilder
        Dim subscriberinfo As New StringBuilder ' Old: Dim subscriberinfo As New StringBuilder
        Dim policy As String = "" ' Old: Dim policy As String = ""
        Dim group As String = "" ' Old: Dim group As String = ""
        Dim relation As String = "" ' Old: Dim relation As String = ""
        Dim patient As Integer = 0 ' Old: Dim patient As Integer = 0
        Dim subfirstname As String = "" ' Old: Dim subfirstname As String = ""
        Dim sublastname As String = "" ' Old: Dim sublastname As String = ""
        Dim submiddlename As String = "" ' Old: Dim submiddlename As String = ""
        Dim subgender As String = "" ' Old: Dim subgender As String = ""
        Dim subdob As Date ' Old: Dim subdob As Date
        Dim pfirstname As String = "" ' Old: Dim pfirstname As String = ""
        Dim plastname As String = "" ' Old: Dim plastname As String = ""
        Dim pmiddlename As String = "" ' Old: Dim pmiddlename As String = ""
        Dim pgender As String = "" ' Old: Dim pgender As String = ""
        Dim pdob As Date ' Old: Dim pdob As Date
        Dim sdate As Date ' Old: Dim sdate As Date
        Dim servicerange As String = "" ' Old: Dim servicerange As String = ""

        Using connection As New SqlConnection(connString) ' Old: Dim cnu As New ADODB.Connection
            connection.Open() ' Old: cnu.Open()

            ' Query for subscriber coverage
            Dim coverageQuery As String = "
            SELECT PolicyNo, GroupNo, Relation, Insured_ID 
            FROM req_coverage 
            WHERE Accession_ID = @AccessionID AND Preference = 'P'"
            Using cmdCoverage As New SqlCommand(coverageQuery, connection) ' Old: Rs.Open()
                cmdCoverage.Parameters.AddWithValue("@AccessionID", accid)

                Using readerCoverage As SqlDataReader = cmdCoverage.ExecuteReader() ' Old: ADODB.Recordset
                    If readerCoverage.Read() Then ' Old: Not Rs.BOF
                        If readerCoverage("PolicyNo") IsNot DBNull.Value Then policy = Trim(readerCoverage("PolicyNo").ToString())
                        If readerCoverage("GroupNo") IsNot DBNull.Value Then group = Trim(readerCoverage("GroupNo").ToString())
                        If readerCoverage("Relation") IsNot DBNull.Value Then relation = readerCoverage("Relation").ToString()
                        If readerCoverage("Insured_ID") IsNot DBNull.Value Then patient = Convert.ToInt32(readerCoverage("Insured_ID"))

                        ' If the subscriber is the patient (relation = "0")
                        If relation = "0" Then
                            Dim patientQuery As String = "SELECT LastName, FirstName, MiddleName, Sex, DOB FROM Patients WHERE ID = @PatientID"
                            Using cmdPatient As New SqlCommand(patientQuery, connection)
                                cmdPatient.Parameters.AddWithValue("@PatientID", patient)

                                Using readerPatient As SqlDataReader = cmdPatient.ExecuteReader()
                                    If readerPatient.Read() Then
                                        If readerPatient("LastName") IsNot DBNull.Value Then sublastname = Trim(readerPatient("LastName").ToString())
                                        If readerPatient("FirstName") IsNot DBNull.Value Then subfirstname = Trim(readerPatient("FirstName").ToString())
                                        If readerPatient("MiddleName") IsNot DBNull.Value Then submiddlename = Trim(readerPatient("MiddleName").ToString())
                                        If readerPatient("Sex") IsNot DBNull.Value Then subgender = readerPatient("Sex").ToString()
                                        If readerPatient("DOB") IsNot DBNull.Value Then subdob = Convert.ToDateTime(readerPatient("DOB"))
                                    End If
                                End Using
                            End Using

                            Dim sdateQuery As String = "SELECT AccessionDate FROM requisitions WHERE ID = @AccessionID"
                            Using cmdSdate As New SqlCommand(sdateQuery, connection)
                                cmdSdate.Parameters.AddWithValue("@AccessionID", accid)

                                Using readerSdate As SqlDataReader = cmdSdate.ExecuteReader()
                                    If readerSdate.Read() AndAlso readerSdate("AccessionDate") IsNot DBNull.Value Then
                                        sdate = Convert.ToDateTime(readerSdate("AccessionDate"))
                                        servicerange = sdate.ToString("yyyyMMdd") & "-" & sdate.ToString("yyyyMMdd")
                                    End If
                                End Using
                            End Using

                            If sublastname <> "" AndAlso subfirstname <> "" AndAlso policy <> "" AndAlso servicerange <> "" Then
                                subscriberinfo.Append("HL*3*2*22*0~NM1*IL*1*" & sublastname & "*" &
                            subfirstname & "****MI*" & policy & "~DMG*D8*" & subdob.ToString("yyyyMMdd") & "*~DTP*291*RD8*" & servicerange & "~")
                            Else
                                ProcessResponse("Subscriber's details are missing")
                            End If
                        Else
                            ' Query for patient info when subscriber is not the patient
                            Dim patientDetailsQuery As String = "
                            SELECT Patients.LastName, Patients.FirstName, Patients.MiddleName, Patients.Sex, Patients.DOB, requisitions.AccessionDate 
                            FROM Patients 
                            INNER JOIN requisitions ON requisitions.Patient_ID = Patients.ID 
                            WHERE requisitions.ID = @AccessionID"
                            Using cmdPatientDetails As New SqlCommand(patientDetailsQuery, connection)
                                cmdPatientDetails.Parameters.AddWithValue("@AccessionID", accid)

                                Using readerPatientDetails As SqlDataReader = cmdPatientDetails.ExecuteReader()
                                    If readerPatientDetails.Read() Then
                                        If readerPatientDetails("LastName") IsNot DBNull.Value Then plastname = readerPatientDetails("LastName").ToString()
                                        If readerPatientDetails("FirstName") IsNot DBNull.Value Then pfirstname = readerPatientDetails("FirstName").ToString()
                                        If readerPatientDetails("MiddleName") IsNot DBNull.Value Then pmiddlename = readerPatientDetails("MiddleName").ToString()
                                        If readerPatientDetails("Sex") IsNot DBNull.Value Then pgender = readerPatientDetails("Sex").ToString()
                                        If readerPatientDetails("DOB") IsNot DBNull.Value Then pdob = Convert.ToDateTime(readerPatientDetails("DOB"))
                                        If readerPatientDetails("AccessionDate") IsNot DBNull.Value Then
                                            sdate = Convert.ToDateTime(readerPatientDetails("AccessionDate"))
                                            servicerange = sdate.ToString("yyyyMMdd") & "-" & sdate.ToString("yyyyMMdd")
                                        End If
                                    End If
                                End Using
                            End Using

                            If plastname <> "" AndAlso pfirstname <> "" AndAlso policy <> "" AndAlso servicerange <> "" Then
                                subscriberinfo.Append("HL*3*2*22*1~NM1*IL*1*" & sublastname & "*" & subfirstname &
                            "****MI*" & policy & "~HL*4*3*23*0~NM1*03*1*" & plastname & "*" & pfirstname &
                            "****MI*" & policy & "~DMG*D8*" & pdob.ToString("yyyyMMdd") & "*~DTP*291*RD8*" & servicerange & "~")
                            Else
                                ProcessResponse("Subscriber's and patient's details are missing")
                            End If
                        End If
                    End If
                End Using
            End Using
        End Using

        Return subscriberinfo
    End Function

    Function GetAndParseX12_271(ByVal resp As String) As String

        Dim x12Data As String = ExtractX12Payload(resp)
        Dim data As New Dictionary(Of String, String)

        Dim EligData As EligData = New EligData()
        EligData.CoverageInfo = New List(Of CoverageInfo)()
        EligData.CoPayment = New List(Of CoPayment)()
        EligData.coverageDetails = New List(Of CoverageDetail)()
        ' Extract ISA and GS segments
        Dim isaSegment As Match = Regex.Match(x12Data, "ISA.*?~")
        Dim gsSegment As Match = Regex.Match(x12Data, "GS.*?~")
        ' Extract ST and BHT segments
        Dim stSegment As Match = Regex.Match(x12Data, "ST.*?~")
        Dim bhtSegment As Match = Regex.Match(x12Data, "BHT.*?~")
        ' Extract HL segments
        Dim hlSegments As MatchCollection = Regex.Matches(x12Data, "HL.*?~")
        ' Extract NM1 segments
        Dim nm1Segments As MatchCollection = Regex.Matches(x12Data, "NM1.*?~")
        ' Extract EB segments
        Dim ebSegments As MatchCollection = Regex.Matches(x12Data, "EB.*?~")
        ' Extract DEP segments
        Dim depSegments As MatchCollection = Regex.Matches(x12Data, "DEP.*?~")
        ' Extract REF segments
        Dim refSegments As MatchCollection = Regex.Matches(x12Data, "REF.*?~")
        ' Extract STS segment
        Dim stsSegment As Match = Regex.Match(x12Data, "STS.*?~")
        ' Extract STC segments
        Dim stcSegments As MatchCollection = Regex.Matches(x12Data, "STC.*?~")
        ' Extract relevant information
        For Each nm1Segment As Match In nm1Segments
            Dim nm1Elements() As String = nm1Segment.Value.Split("*")
            If nm1Elements(1) = "PR" Then
                data("healthPlanName") = nm1Elements(3)
            ElseIf nm1Elements(1) = "IL" Then
                data("primaryCareProvider") = nm1Elements(3)
            ElseIf nm1Elements(1) = "82" Then
                data("providerName") = nm1Elements(3)
                data("providerType") = nm1Elements(4)
                data("providerNpi") = nm1Elements(5)
            End If
        Next
        Dim ebs() As String = resp.Split("~")
        If resp <> "" Then
            Dim messageServiceText = ""
            Dim dtpDate = ""
            For i As Integer = 0 To ebs.Length - 1
                If ebs(i).Substring(0, 2) = "EB" Or ebs(i).Substring(0, 3) = "MSG" Or ebs(i).Substring(0, 3) = "AAA" Or ebs(i).Substring(0, 3) = "DTP" Then
                    Dim seg As String = ebs(i).Substring(0, 2)
                    Dim v = translate(ebs(i), seg)
                    Dim ebElements() As String = ebs(i).Split("*")
                    If seg.ToLower = "eb" Then
                        seg = "EB"
                    ElseIf seg.ToLower = "ms" Then
                        seg = "MSG"
                        messageServiceText = v
                        Continue For
                    ElseIf seg.ToLower = "aaa" Then
                        seg = "AAA"
                    ElseIf seg.ToLower = "dt" Then
                        seg = "DT"
                        Dim dates = FormatDateRange(ebElements(3))
                        dtpDate = dates
                        Continue For

                    End If
                    '
                    ' EligData.

                    If ebElements.Length > 4 Then

                    End If

                    If ebElements.Length > 4 AndAlso (ebElements(4) = "HN" Or ebElements(4) = "PR" Or ebElements(1) = "G" Or ebElements(2) = "IND" Or ebElements(2) = "FAM") Then
                        Dim CoverageInfo As CoverageInfo = New CoverageInfo()
                        If ebElements.Length > 4 And ebElements(1) = "1" Then

                            If ebElements(4) = "HN" Then
                                CoverageInfo.HealthPlanName = ebElements(5)
                                CoverageInfo.PCPStatus = IIf(v.Contains("Active"), "Active", "-")
                            ElseIf ebElements(4) = "PR" Then
                                CoverageInfo.HealthPlanName = ebElements(5)
                                CoverageInfo.PCPStatus = IIf(v.Contains("Active"), "Active", "-")
                            End If

                        ElseIf ebElements(1) = "G" Then


                            If ebElements(2) = "IND" And ebElements(4) = "HN" Then
                                CoverageInfo.IndividualOutOfPocketMax = ebElements(7)
                            ElseIf ebElements(2) = "IND" And ebElements(4) = "PR" Then
                                CoverageInfo.IndividualOutOfPocketMax = ebElements(7)
                            End If
                            If ebElements(2) = "FAM" And ebElements(4) = "HN" Then
                                CoverageInfo.FamilyOutOfPocketMax = ebElements(7)
                            ElseIf ebElements(2) = "FAM" And ebElements(4) = "PR" Then
                                CoverageInfo.FamilyOutOfPocketMax = ebElements(7)
                            End If
                        ElseIf ebElements(1) = "C" Then
                            If ebElements(2) = "IND" Then
                                CoverageInfo.IndividualDeductible = ebElements(7)
                                If CoverageInfo.ReferralRequired = "" Then CoverageInfo.ReferralRequired = messageServiceText
                            End If
                            If ebElements(2) = "FAM" Then
                                CoverageInfo.FamilyDeductible = ebElements(7)
                            End If
                        End If
                        EligData.CoverageInfo.Add(CoverageInfo)
                    End If


                    Dim translatee = ""
                    Dim amount = ""

                    If v.Contains("Co-") And ebElements(1) = "B" Then
                        For it As Integer = 1 To ebElements.Length - 1
                            If it = 1 Then
                                Continue For
                            End If
                            If ebElements(it) <> "" Then
                                Dim fldnum As String = ""
                                If it < 10 Then
                                    fldnum = "EB0" & it
                                Else
                                    fldnum = "EB" & it
                                End If
                                Dim vlue = GetFieldValue(fldnum, ebElements(it))
                                If vlue <> "Day" Then
                                    If it = 7 Then
                                        amount = vlue
                                    Else
                                        translatee = translatee & " " & vlue

                                    End If

                                End If

                            End If
                            If it = 7 Then
                                Exit For
                            End If
                        Next
                        Dim coPaysments = New CoPayment With {.ServiceType = messageServiceText, .Service = translatee, .Amount = amount, .CoverageDate = dtpDate}
                        EligData.CoPayment.Add(coPaysments)
                    ElseIf ebElements(1) = "1" Then

                        For it As Integer = 1 To ebElements.Length - 1
                            If it = 1 Then
                                Continue For
                            End If
                            If ebElements(it) <> "" Then
                                Dim fldnum As String = ""
                                If it < 10 Then
                                    fldnum = "EB0" & it
                                Else
                                    fldnum = "EB" & it
                                End If
                                Dim vlue = GetFieldValue(fldnum, ebElements(it))
                                amount = vlue
                                If vlue <> "Day" Then
                                    If it = 7 Then
                                        amount = vlue
                                    Else
                                        If vlue <> "Y" Then
                                            translatee = translatee & " " & vlue
                                        End If


                                    End If

                                End If


                            End If
                            If it = 7 Then


                            End If
                        Next
                        If messageServiceText <> "" Then
                            Dim CoverageDetail = New CoverageDetail With {.ServiceType = messageServiceText, .Service = translatee, .Status = IIf(amount = "Y", "Active", ""), .CoverageDate = dtpDate}
                            EligData.coverageDetails.Add(CoverageDetail)
                        End If

                    End If

                End If
            Next
        End If
        Dim htmlTxt1 = ""
        Dim coverages = EligData.CoverageInfo
        Dim coPayments = EligData.CoPayment.OrderBy(Function(v) v.ServiceType).ToList()
        Dim coverageDetails = EligData.coverageDetails.OrderBy(Function(v) v.ServiceType).ToList()


        For Each coverage In coverages
            Dim htmlTxt2 = ""



            If Not coverage.IndividualDeductible Is Nothing Then
                htmlTxt2 = "<li><strong>Individual Deductible:</strong> ${{IndividualDeductible}}</li>"
                htmlTxt2 = htmlTxt2.Replace("{{IndividualDeductible}}", coverage.IndividualDeductible)
                htmlTxt1 += Environment.NewLine + htmlTxt2

            End If
            If Not coverage.FamilyDeductible Is Nothing Then
                htmlTxt2 = "<li><strong>Family Deductible</strong> ${{FamilyDeductible}}</li>"
                htmlTxt2 = htmlTxt2.Replace("{{FamilyDeductible}}", coverage.FamilyDeductible)
                htmlTxt1 += Environment.NewLine + htmlTxt2

            End If
            If Not coverage.IndividualOutOfPocketMax Is Nothing Then
                htmlTxt2 = " <li><strong>Individual Out-Of-Pocket Maximum</strong> ${{IndividualOutOfPocketMax}}</li>"
                htmlTxt2 = htmlTxt2.Replace("{{IndividualOutOfPocketMax}}", coverage.IndividualOutOfPocketMax)
                htmlTxt1 += Environment.NewLine + htmlTxt2

            End If
            If Not coverage.FamilyOutOfPocketMax Is Nothing Then
                htmlTxt2 = "<li><strong>Family Out-Of-Pocket Maximum:</strong> ${{FamilyOutOfPocketMax}}</li>"
                htmlTxt2 = htmlTxt2.Replace("{{FamilyOutOfPocketMax}}", coverage.FamilyOutOfPocketMax)
                htmlTxt1 += Environment.NewLine + htmlTxt2

            End If
            If Not coverage.ReferralRequired Is Nothing Then
                htmlTxt2 = "<li><strong>:</strong> ${{ReferralRequired}}</li>"
                htmlTxt2 = htmlTxt2.Replace("{{ReferralRequired}}", coverage.ReferralRequired)
                htmlTxt1 += Environment.NewLine + htmlTxt2
            End If
        Next


        Dim htmlTxt = GetEligibilityHtml()
        htmlTxt = htmlTxt.Replace("{{FinancialInfo}}", htmlTxt1)

        ' Replace placeholders in the HTML template with the CoverageInfo data
        If coverages.Count = 0 Then
            coverages = New List(Of CoverageInfo)()
            Dim CoverageInfo = New CoverageInfo()
            CoverageInfo.HealthPlanName = ""
            CoverageInfo.PCPStatus = ""
            coverages.Add(CoverageInfo)
        End If
        htmlTxt = htmlTxt.Replace("{{HealthPlanName}}", coverages.FirstOrDefault().HealthPlanName)
        htmlTxt = htmlTxt.Replace("{{PCPStatus}}", coverages.FirstOrDefault().PCPStatus)
        'htmlTxt = htmlTxt.Replace("{{ReferralRequired}}", coverage.ReferralRequired)
        'htmlTxt = htmlTxt.Replace("{{IndividualDeductible}}", coverage.IndividualDeductible)
        'htmlTxt = htmlTxt.Replace("{{FamilyDeductible}}", coverage.FamilyDeductible)
        'htmlTxt = htmlTxt.Replace("{{IndividualOutOfPocketMax}}", coverage.IndividualOutOfPocketMax)
        'htmlTxt = htmlTxt.Replace("{{FamilyOutOfPocketMax}}", coverage.FamilyOutOfPocketMax)
        If coverages.FirstOrDefault().PCPStatus = "Active" Then
            htmlTxt = htmlTxt.Replace("<span Class=""status"">Active Coverage</span>", "<span Class=""status"">" & coverages.FirstOrDefault().PCPStatus & "</span>")
        Else
            htmlTxt = htmlTxt.Replace("<span Class=""status"">Active Coverage</span>", "<span Class=""status"" style='color:Red'>Not Found</span")

        End If

        ' Dynamically build the Co-Payments table rows
        Dim rows As String = ""
        For Each cp As CoPayment In coPayments
            rows &= "<tr>"
            rows &= "<td>" & cp.ServiceType & "</td>"
            rows &= "<td>" & cp.Service & "</td>"
            rows &= "<td>$" & cp.Amount & "</td>"
            rows &= "<td>" & cp.CoverageDate & "</td>"
            rows &= "</tr>"
        Next

        ' Inject the dynamically generated rows into the HTML template
        htmlTxt = htmlTxt.Replace("<!-- Co-Payment Rows will be dynamically inserted here -->", rows)

        ' Dynamically build the Coverage Details table rows
        Dim coverageDetailRows As String = ""
        For Each cd As CoverageDetail In coverageDetails
            coverageDetailRows &= "<tr>"
            coverageDetailRows &= "<td>" & cd.ServiceType & "</td>"
            coverageDetailRows &= "<td>" & cd.Service & "</td>"
            coverageDetailRows &= "<td>" & cd.Status & "</td>"
            coverageDetailRows &= "<td>" & cd.CoverageDate & "</td>"
            coverageDetailRows &= "</tr>"
        Next

        htmlTxt = htmlTxt.Replace("<!-- Coverage Details Rows will be dynamically inserted here -->", coverageDetailRows)

        Return htmlTxt
    End Function
    Public Function FormatDateRange(ByVal dateRange As String) As String
        Dim dates As String() = dateRange.Split("-"c) ' Split the date range into two parts
        If dates.Length = 2 Then
            Dim startDate As DateTime = DateTime.ParseExact(dates(0), "yyyyMMdd", Nothing)
            Dim endDate As DateTime = DateTime.ParseExact(dates(1), "yyyyMMdd", Nothing)
            ' Return the formatted date range

            Return Format(startDate, SystemConfig.DateFormat) & " - " & Format(endDate, SystemConfig.DateFormat)
        End If
        Return dateRange ' Return the original string if formatting fails
    End Function
    Private Function GetEligibilityHtml() As String
        Dim htmlTxt As String = "
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Eligibility Status</title>
    <style>
        /* CSS Styling */
        body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background-color: #f0f4f8; padding: 20px; }
        .container {  background-color: #fff; padding: 30px; margin: 0 auto; border-radius: 12px; box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1); }
        h1 { font-size: 28px; text-align: center; color: #00796b; margin-bottom: 20px; }
        h2 { font-size: 22px; margin-bottom: 15px; color: #004d40; border-left: 5px solid #00796b; padding-left: 10px; }
        table { width: 100%; border-collapse: collapse; margin-top: 15px; font-size: 18px; }
        th, td { padding: 12px; border: 1px solid #ddd; }
        th { background-color: #00796b; color: #fff; text-align: left; }
        tr:nth-child(even) { background-color: #f9f9f9; }
        tr:hover { background-color: #e0f7fa; }
        hr { margin: 30px 0; border: none; border-top: 1px solid #ddd; }
    </style>
</head>
<body>
    <div class=""container"">
        <h1>Eligibility Status: <span class=""status"">Active Coverage</span></h1>
        <hr>
        <section class=""coverage-overview"">
            <h2>Coverage Overview</h2>
            <ul>
                <li><strong>Health Plan Name:</strong> {{HealthPlanName}}</li>
                <li><strong>Primary Care Provider (PCP):</strong> {{PCPStatus}}</li>
                <li><strong>Referral Required for Specialist:</strong> {{ReferralRequired}}</li>
            </ul>
        </section>
        <hr>
        <section class=""financial-info"">
            <h2>Key Financial Information</h2>
            <ul>
                {{FinancialInfo}}
            </ul>
        </section>
        <hr>
        <section class=""co-payments"">
            <h2>Co-Payments</h2>
            <table>
                <thead>
                    <tr>
                        <th>Service Type</th>
                        <th>Details</th>
                        <th>Co-Payment</th>
                    
                        <th>Coverage Date</th>
                    </tr>
                </thead>
                <tbody id=""dynamicTable"">
                    <!-- Co-Payment Rows will be dynamically inserted here -->
                </tbody>
            </table>
        </section>
        <hr>
        <section class=""coverage-details"">
            <h2>Coverage Details</h2>
            <table>
                <thead>
                    <tr>
                        <th>Service Type</th>
                        <th>Service</th>
                        <th>Status</th>
                        <th>Coverage Date</th>
                    </tr>
                </thead>
                <tbody id=""dynamicCoverageDetailsTable"">
                    <!-- Coverage Details Rows will be dynamically inserted here -->
                </tbody>
            </table>
        </section>
    </div>
</body>
</html>
"

        Return htmlTxt
    End Function
    Function ProcessResponse(ByVal resp As String) As StringBuilder
        Dim sb As New StringBuilder
        Dim ebs() As String = resp.Split("~")
        For i As Integer = 0 To ebs.Length - 1
            If ebs(i).Substring(0, 2) = "EB" Or ebs(i).Substring(0, 3) = "MSG" Or ebs(i).Substring(0, 3) = "AAA" Then
                Dim seg As String = ebs(i).Substring(0, 2)
                If seg.ToLower = "eb" Then
                    seg = "EB"
                ElseIf seg.ToLower = "ms" Then
                    seg = "MSG"
                Else
                    seg = "AAA"
                End If
                'transalate values from respective segments
                sb.AppendLine(translate(ebs(i), seg))
            End If
        Next
        If sb.ToString = "" Or sb.ToString = "{}" Then
            SaveEligibilityResponse(ACCIDs, resp)
        Else
            SaveEligibilityResponse(ACCIDs, sb.ToString, resp)

        End If
        Return sb
    End Function
    Public Function ExtractX12Payload(responseXml As String) As String
        Try
            Dim xmlDoc As New XmlDocument()
            xmlDoc.LoadXml(responseXml)
            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
            nsmgr.AddNamespace("ns", "http://www.caqh.org/SOAP/WSDL/CORERule2.2.0.xsd")

            ' Now select the Payload node
            Dim payloadNode As XmlNode = xmlDoc.SelectSingleNode("//ns:Payload", nsmgr)

            ' Navigate to the Payload node

            If payloadNode IsNot Nothing Then
                Dim payloadText As String = payloadNode.InnerText
                ' Decode CDATA if necessary
                If payloadText.StartsWith("<![CDATA[") AndAlso payloadText.EndsWith("]]>") Then
                    payloadText = payloadText.Substring(9, payloadText.Length - 12) ' Strip CDATA
                End If
                Return payloadText
            End If
        Catch ex As Exception

        End Try


        Return String.Empty ' Return empty if Payload not found
    End Function
    Function translate(ByVal segstr As String, ByVal seg As String) As String
        translate = ""
        Dim flds() As String = segstr.Split("*")
        If seg.ToLower = "msg" Then
            translate = translate & " " & flds(1)
        ElseIf seg.ToLower = "aaa" Then
            For i As Integer = 1 To flds.Length - 1
                If flds(i) <> "" Then
                    Dim fldnum As String = ""
                    If i < 10 Then
                        fldnum = "aaa0" & i
                    Else
                        fldnum = "aaa" & i
                    End If
                    translate = translate & " " & GetFieldValue(fldnum, flds(i))
                End If
            Next
        Else
            For i As Integer = 1 To flds.Length - 1
                If flds(i) <> "" Then
                    Dim fldnum As String = ""
                    If i < 10 Then
                        fldnum = "EB0" & i
                    Else
                        fldnum = "EB" & i
                    End If
                    translate = translate & " " & GetFieldValue(fldnum, flds(i))
                End If
            Next

        End If
        Return translate
    End Function

    Function GetFieldValue(ByVal fldno As String, ByVal fldtxt As String) As String
        Dim Meaning As String = "" ' Old: Dim Meaning As String = ""
        Dim found As Boolean = False ' Old: Dim found = False
        Dim cods = fldtxt.Split("^") ' Old: Dim cods = fldtxt.Split("^")

        Using cnval As New SqlConnection(connString) ' Old: Dim cnval As New Odbc.OdbcConnection(connstring)
            cnval.Open() ' Old: cnval.Open()

            For Each v As String In cods
                Dim query As String = "
                SELECT EBfldmeans 
                FROM EBMsgLookup 
                WHERE EBFldNo = @FldNo AND EBfldtxt = @FldTxt" ' Old: Embedded query with parameters
                Using cmdval As New SqlCommand(query, cnval) ' Old: Dim cmdval As New Odbc.OdbcCommand(...)
                    ' Add parameters for SQL query
                    cmdval.Parameters.AddWithValue("@FldNo", fldno)
                    cmdval.Parameters.AddWithValue("@FldTxt", v)

                    Using drval As SqlDataReader = cmdval.ExecuteReader() ' Old: Dim drval As Odbc.OdbcDataReader = cmdval.ExecuteReader
                        If drval.HasRows Then ' Old: If drval.HasRows Then
                            While drval.Read() ' Old: While drval.Read
                                If drval("EBfldmeans") IsNot DBNull.Value Then ' Old: If drval("EBfldmeans") IsNot DBNull.Value Then
                                    Dim currentValue As String = drval("EBfldmeans").ToString()

                                    If Meaning <> "" Then
                                        If currentValue.Contains(Meaning) Then
                                            Meaning += currentValue.Replace(Meaning, " ") ' Old: Meaning += ...
                                        ElseIf Meaning.Contains(currentValue) Then
                                            Meaning = Meaning.Replace(currentValue, " ") ' Old: Meaning = ...
                                        End If
                                    Else
                                        Meaning += currentValue
                                        found = True ' Old: found = True
                                    End If
                                End If
                            End While
                        End If
                    End Using
                End Using
            Next
        End Using ' Old: cnval.Close()

        ' Return the appropriate value
        If found Then
            Return Meaning ' Old: Return Meaning
        Else
            Return fldtxt ' Old: Return fldtxt
        End If
    End Function
    Public Sub SaveEligibilityResponse(ByVal AccID As String, ByVal Msg As String, Optional ByVal xmlstring As String = "")
        Dim dt As Date = Date.Now
        Dim cnu As New SqlConnection(connString)
        cnu.Open()

        ' Define the SQL query to insert a new record
        Dim insertQuery As String = "INSERT INTO EBMessages (Accession_ID, MsgDate, Msg, xmlResponse) " &
                                    "VALUES (@AccID, @MsgDate, @Msg, @xmlResponse)"

        ' Open the SQL connection if it's not already open
        If cnu.State = ConnectionState.Closed Then
            cnu.Open()
        End If

        ' Create a SQL command object to execute the insert query
        Using insertCmd As New SqlCommand(insertQuery, cnu)
            ' Add parameters to prevent SQL injection
            insertCmd.Parameters.AddWithValue("@AccID", AccID)
            insertCmd.Parameters.AddWithValue("@MsgDate", dt.ToShortDateString)
            insertCmd.Parameters.AddWithValue("@Msg", Msg)
            insertCmd.Parameters.AddWithValue("@xmlResponse", xmlstring)

            ' Execute the insert query
            insertCmd.ExecuteNonQuery()
        End Using

        ' Close the connection if it's open
        If cnu.State = ConnectionState.Open Then
            cnu.Close()
        End If
    End Sub


    Function GetCPTCodes(ByVal accid As String) As StringBuilder
        Dim CPT As New StringBuilder ' Old: Dim CPT As New StringBuilder

        Using connection As New SqlConnection(connString) ' Old: Dim cnu As New ADODB.Connection
            connection.Open() ' Old: cnu.Open()

            Dim query As String = "SELECT * FROM vReqTBillable WHERE AccID = @AccID" ' Old: Rs.Open("Select * from vReqTBillable where AccID = '" & accid & "'"
            Using cmd As New SqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@AccID", accid) ' Parameterized query for AccID

                Using reader As SqlDataReader = cmd.ExecuteReader() ' Old: Dim Rs As New ADODB.Recordset
                    If reader.HasRows Then ' Old: If Not Rs.BOF Then
                        While reader.Read() ' Old: Do Until Rs.EOF
                            If reader("CPT") IsNot DBNull.Value Then ' Old: If Rs.Fields("CPT").Value IsNot System.DBNull.Value Then
                                CPT.AppendLine("EQ**HC:" & reader("CPT").ToString().Trim() & "~") ' Old: CPT.AppendLine("EQ**HC:" & Rs.Fields("CPT").Value.ToString.Trim & "~")
                            End If
                        End While
                    End If
                End Using
            End Using
        End Using

        If CPT.ToString() = "" Then ' Old: If CPT.ToString = "" Then
            CPT.AppendLine("EQ*30~") ' Old: CPT.AppendLine("EQ*30~")
        End If

        Return CPT ' Old: Return CPT
    End Function

    Function CheckEligibilityRecord(ByVal accid As Long, ByVal connString As String) As String
        Dim Status As String = "" ' Old: Dim Status As String = ""

        Using cncer As New SqlConnection(connString) ' Old: Dim cncer As New Odbc.OdbcConnection(connstring)
            cncer.Open() ' Old: cncer.Open()

            Dim query As String = "SELECT Status FROM Eligibility WHERE Accession_ID = @AccessionID" ' Old: Embedded accid in query
            Using cmdcer As New SqlCommand(query, cncer) ' Old: Dim cmdcer As New Odbc.OdbcCommand(...)
                cmdcer.Parameters.AddWithValue("@AccessionID", accid) ' Use parameterized query

                Using drcer As SqlDataReader = cmdcer.ExecuteReader() ' Old: Dim drcer As Odbc.OdbcDataReader = cmdcer.ExecuteReader
                    If drcer.HasRows Then ' Old: If drcer.HasRows Then
                        While drcer.Read() ' Old: While drcer.Read
                            If drcer("Status") IsNot DBNull.Value Then ' Old: If drcer("Status") IsNot DBNull.Value Then
                                Status = drcer("Status").ToString() ' Old: Status = drcer("Status")
                            End If
                        End While
                    End If
                End Using
            End Using
        End Using ' Old: cncer.Close(), cncer = Nothing

        Return Status ' Old: Return Status
    End Function



End Class
' Class to hold Coverage and Financial Information

Public Class EligData
    Public CoverageInfo As List(Of CoverageInfo)
    Public CoPayment As List(Of CoPayment)
    Public coverageDetails As List(Of CoverageDetail)
End Class
Public Class CoverageInfo
    Public Property HealthPlanName As String
    Public Property PCPStatus As String
    Public Property ReferralRequired As String
    Public Property IndividualDeductible As String
    Public Property FamilyDeductible As String
    Public Property IndividualOutOfPocketMax As String
    Public Property FamilyOutOfPocketMax As String
End Class

' Class to hold Co-Payment Information
Public Class CoPayment
    Public Property ServiceType As String
    Public Property Service As String
    Public Property Amount As String = "$" + Amount
    Public Property CoverageDate As String
End Class

' Class to hold Coverage Details Information
Public Class CoverageDetail
    Public Property ServiceType As String
    Public Property Service As String
    Public Property Status As String
    Public Property CoverageDate As String
End Class
