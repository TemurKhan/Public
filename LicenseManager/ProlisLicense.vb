Option Compare Text
Option Strict Off
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports Microsoft.VisualBasic
Imports System.Management
Imports System.Globalization
Imports ADODB
Imports System.Data.Odbc
Imports System.Data





Public Class License
    Protected key As String = "<!&/?@*\>#>"

    Public Sub ConvertTextToLicense(ByVal TxtFile As String)
        Dim FNAME As String = Microsoft.VisualBasic.Mid(TxtFile, 1, InStr(TxtFile, "."))
        Dim Data As String = ""
        Dim SR As StreamReader = New StreamReader(TxtFile)
        Data = SR.ReadToEnd
        Dim SW As New StreamWriter(FNAME & "LIC")
        SW.Write(encryptString(Data))
        SW.Close()
        SR.Close()
        SW = Nothing : SR = Nothing
    End Sub

    Public Sub New()
        'constructor
    End Sub

    Public Function encryptString(ByVal strtext As String) As String
        Return Encrypt(strtext, key)
    End Function

    Public Function decryptString(ByVal strtext As String) As String
        Return Decrypt(strtext, key)
    End Function

    'The function used to encrypt the text
    Private Function Encrypt(ByVal strText As String, ByVal strEncrKey _
             As String) As String
        Dim byKey() As Byte = {}
        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}

        Try
            byKey = System.Text.Encoding.UTF8.GetBytes(Left(strEncrKey, 8))

            Dim des As New DESCryptoServiceProvider()
            Dim inputByteArray() As Byte = Encoding.UTF8.GetBytes(strText)
            Dim ms As New MemoryStream()
            Dim cs As New CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Return Convert.ToBase64String(ms.ToArray())

        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    'The function used to decrypt the text
    Private Function Decrypt(ByVal strText As String, ByVal sDecrKey _
               As String) As String
        Dim byKey() As Byte = {}
        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
        Dim inputByteArray(strText.Length) As Byte

        Try
            byKey = System.Text.Encoding.UTF8.GetBytes(Left(sDecrKey, 8))
            Dim des As New DESCryptoServiceProvider()
            inputByteArray = Convert.FromBase64String(strText)
            Dim ms As New MemoryStream()
            Dim cs As New CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write)

            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8

            Return encoding.GetString(ms.ToArray())

        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

End Class






Module LicProcs
    Public Function GetDateFormat(ByVal odbCS As String) As String
        Dim DateFormat As String = "MM/dd/yyyy"
        Dim cndf As New OdbcConnection(odbCS)
        cndf.Open()
        Dim cmddf As New OdbcCommand("Select DateFormat from System_Config where Company_ID = 1", cndf)
        cmddf.CommandType = CommandType.Text
        Dim drdf As OdbcDataReader = cmddf.ExecuteReader
        If drdf.HasRows Then
            While drdf.Read
                DateFormat = drdf("DateFormat")
            End While
        End If
        cndf.Close()
        cndf = Nothing
        Return DateFormat
    End Function
End Module


Public Class ProlisLicense
    Protected key As String = "<!&/?@*\>#>"
    Public Licensee As String       'Entity
    Public License As String = ""   'key
    Public SEATS As Integer = 0
    Public LicVer As String         'Prolis Version - Component Version
    Public DiagTarget As String = "H"   'Human (Default) or Vetrinary (V)
    Public AutoAccession As Boolean = False  'Auto accessioning
    Public AppRun As Boolean = False         'Permission
    Public CS As Boolean = False             'Permission
    Public AER As Boolean = False            'AutoEqpRes Permission
    Public ALR As Boolean = False            'AutoLabRes Permission
    Public MyRpts As Boolean = False         'MyReports permission
    Public AuditTrail As Boolean             'Audit Trail feature
    Public Bill As Boolean = False           'Permission
    Public BillExport As Boolean = False
    Public BillDashboard As Boolean = False
    Public PreAuth As Boolean = False
    Public BillFUDash As Boolean = False
    Public PmtDash As Boolean = False
    Public EECC_UID As String = ""
    Public EECC_PWD As String = ""
    Public EECC_ClientID As String = ""
    Public EECC As Boolean = False           'Eligibility & Claim, depend on credentials
    Public Fax As Boolean = False            'Prolis Fax permission
    Public PDF As Boolean = False            'Create PDF permission
    Public ORResults As Boolean = False
    Public ORAccession As Boolean = False
    Public ORPatient As Boolean = False
    Public ORSchedule As Boolean = False
    Public ORReports As Boolean = False
    Public ORMyRpts As Boolean = False

    Public Function ValidateSite(ByVal odbCS As String, ByVal SiteID As String) As Boolean
        Dim SiteValid As Boolean = False
        Try
            Dim AppID As String = ""
            Dim SEGS() As String
            Dim Fields() As String
            Dim Comps() As String
            Dim DOA As Date
            Dim DateFormat As String = GetDateFormat(odbCS)
            Dim cnvs As New OdbcConnection(odbCS)
            cnvs.Open()
            Dim cmdvs As New OdbcCommand("Select System_Params from Company", cnvs)
            cmdvs.CommandType = CommandType.Text
            Dim drvs As OdbcDataReader = cmdvs.ExecuteReader
            If drvs.HasRows Then
                While drvs.Read
                    If drvs("System_Params") IsNot DBNull.Value _
                    AndAlso Trim(drvs("System_Params")) <> "" Then
                        Dim LIC As String = decryptString(Trim(drvs("System_Params")))
                        If LIC <> "" Then
                            LIC = Replace(LIC, Chr(2), "") : LIC = Replace(LIC, Chr(3), "")
                            SEGS = Split(LIC, vbCrLf)
                            For i As Integer = 0 To SEGS.Length - 1
                                SEGS(i) = Replace(SEGS(i), vbLf, "")
                                SEGS(i) = Replace(SEGS(i), vbCr, "")
                                SEGS(i) = Replace(SEGS(i), vbCrLf, "")
                                If SEGS(i).StartsWith("ENTITY") Then
                                    'ENTITY|6001601^ID~GLOBAL LAB INC^NAME~273985956^FID~1972800118^NPI~14D2019002^CLIA|20120606^DOS
                                    Fields = Split(SEGS(i), "|")
                                    Comps = Split(Fields(1), "~")
                                    Licensee = Microsoft.VisualBasic.Mid(Comps(0), 1,
                                    InStr(Comps(0), "^") - 1) & " - " &
                                    Microsoft.VisualBasic.Mid(Comps(1), 1,
                                    InStr(Comps(1), "^") - 1)
                                ElseIf SEGS(i).StartsWith("COMPONENT") Then
                                    'COMPONENT|1|600^ID~PROLIS^NAME|4.1.0.6^VER|10^TYPE|6001601^OWN|20110806^DOD|20120606^DOS
                                    'COMPONENT|2|602^ID~OUTREACH^NAME|4.1.0.6^VER|11^TYPE|6001601^OWN|20110806^DOD|20120606^DOS
                                    'COMPONENT|15|618^ID~REMOTE SITE LICENSE^NAME|1.0.0.0^VER|25^TYPE|6001601^OWN|20121001^DOD|20121001^DOS
                                    AppID = ""
                                    Fields = Split(SEGS(i), "|")
                                    Comps = Split(Fields(2), "~")
                                    If Microsoft.VisualBasic.Mid(Comps(1), 1,
                                        InStr(Comps(1), "^") - 1) = "REMOTE SITE LICENSE" Then
                                        AppID = Microsoft.VisualBasic.Mid(Comps(0), 1,
                                        InStr(Comps(0), "^") - 1)
                                        LicVer = Microsoft.VisualBasic.Mid(Fields(3), 1,
                                        InStr(Fields(3), "^") - 1)
                                    End If
                                ElseIf SEGS(i).StartsWith("FEATURE") Then
                                    'FEATURE|1|0^ID~AppRun^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                    'FEATURE|1|528941^ID~Site^NAME|618^OWN|1^ACT|20121001^DOA|20121001^DOS
                                    If AppID <> "" Then
                                        If InStr(SEGS(i), AppID & "^OWN") > 0 Then
                                            Fields = Split(SEGS(i), "|")
                                            If InStr(Fields(2), SiteID & "^ID~Site") > 0 Then
                                                DOA = DateTime.ParseExact(Microsoft.VisualBasic.Mid(Fields(5),
                                                1, InStr(Fields(5), "^") - 1), "yyyyMMdd", CultureInfo.InvariantCulture)
                                                If Date.Now >= DOA Then 'Current or past
                                                    SiteValid = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                                Else
                                                    SiteValid = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                                End If
                                                Exit For
                                            End If
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    End If
                End While
            End If
            cnvs.Close()
            cnvs = Nothing
        Catch ex As Exception
            SiteValid = False
        End Try
        Return SiteValid
    End Function
    Public Sub WriteTextToFile(ByVal fileName As String, ByVal textToWrite As String)
        ' Get the current directory
        Dim currentDirectory As String = Directory.GetCurrentDirectory()

        ' Combine the directory path with the file name
        Dim filePath As String = Path.Combine(currentDirectory, fileName)

        ' Write the text to the file, creating the file if it does not exist
        Try
            ' If the file does not exist, it will be created
            File.AppendAllText(filePath, textToWrite & Environment.NewLine)
            Console.WriteLine("Text written to file successfully.")
        Catch ex As Exception
            Console.WriteLine("An error occurred: " & ex.Message)
        End Try
    End Sub
    Public Sub New(ByVal odbCS As String, ByVal AppName As String)
        GetCentralLicense(odbCS, AppName)
    End Sub
    Public Sub New(ByVal odbCS1 As ADODB.Connection, ByVal AppName As String)
        GetCentralLicense(odbCS1, AppName)
    End Sub
    Private Sub GetCentralLicense(ByVal odbCS1 As ADODB.Connection, ByVal AppName As String)
        Dim i As Integer
        Dim odbCS As String = "DSN=ProlisQC; UID=prolis; PWD=gujrati"

        Dim k As Integer
        Dim TmpDate As String = ""
        Dim AppID As String = ""
        Dim SEGS() As String
        Dim DOA As Date
        Dim cngcl As New Odbc.OdbcConnection(odbCS)
        cngcl.Open()
        Dim cmdgcl As New Odbc.OdbcCommand("Select System_Params from Company", cngcl)
        cmdgcl.CommandType = CommandType.Text
        Dim drgcl As Odbc.OdbcDataReader = cmdgcl.ExecuteReader
        If drgcl.HasRows Then
            While drgcl.Read
                If drgcl("System_Params") IsNot DBNull.Value _
                AndAlso Trim(drgcl("System_Params")) <> "" Then
                    Dim LIC As String = decryptString(Trim(drgcl("System_Params")))
                    If LIC <> "" Then
                        LIC = Replace(LIC, Chr(2), "") : LIC = Replace(LIC, Chr(3), "")
                        SEGS = Split(LIC, vbCrLf)
                        For i = 0 To SEGS.Length - 1
                            If SEGS(i).StartsWith("ENTITY") Then
                                'ENTITY|6001601^ID~GLOBAL LAB INC^NAME~357167666^KEY~273985956^FID~1972800118^NPI~14D2019002^CLIA|20120606^DOS
                                Dim Fields() As String = Split(SEGS(i), "|")
                                Dim Comps() As String = Split(Fields(1), "~")
                                Licensee = Microsoft.VisualBasic.Mid(Comps(0), 1,
                                InStr(Comps(0), "^") - 1) & " - " &
                                Microsoft.VisualBasic.Mid(Comps(1), 1,
                                InStr(Comps(1), "^") - 1)
                                If Comps.Length > 3 Then
                                    For k = 2 To Comps.Length - 1
                                        If Comps(k) = "KEY" Then
                                            If Comps(k - 1) = GetClientKey() Then
                                                License = encryptString(Comps(k - 1))
                                            End If
                                        End If
                                    Next
                                End If
                            ElseIf SEGS(i).StartsWith("COMPONENT") Then
                                'COMPONENT|1|600^ID~PROLIS^NAME|4.1.0.6^VER|10^TYPE|6001601^OWN|20110806^DOD|20120606^DOS
                                'COMPONENT|2|602^ID~OUTREACH^NAME|4.1.0.6^VER|11^TYPE|6001601^OWN|20110806^DOD|20120606^DOS
                                AppID = ""
                                Dim Fields() As String = Split(SEGS(i), "|")
                                Dim Comps() As String = Split(Fields(2), "~")
                                If AppName = Microsoft.VisualBasic.Mid(Comps(1), 1,
                                    InStr(Comps(1), "^") - 1) Then
                                    AppID = Microsoft.VisualBasic.Mid(Comps(0), 1,
                                    InStr(Comps(0), "^") - 1)
                                    LicVer = Microsoft.VisualBasic.Mid(Fields(3), 1,
                                    InStr(Fields(3), "^") - 1)
                                End If
                            ElseIf SEGS(i).StartsWith("FEATURE") Then
                                'FEATURE|1|0^ID~AppRun^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                If AppID <> "" Then
                                    Dim Fields() As String = Split(SEGS(i), "|")
                                    If InStr(SEGS(i), AppID & "^OWN") > 0 Then
                                        If InStr(Fields(2), "AppRun") > 0 Then
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Today >= DOA Then 'Current or past
                                                AppRun = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                AppRun = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "AutoAccession") > 0 Then
                                            'FEATURE|2|1^ID~ClientService^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                AutoAccession = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                AutoAccession = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "ClientService") > 0 Then
                                            'FEATURE|2|1^ID~ClientService^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                CS = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                CS = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "AutoEqpRes") > 0 Then
                                            'FEATURE|3|2^ID~AutoEqpRes^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                AER = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                AER = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "AutoLabRes") > 0 Then
                                            'FEATURE|4|3^ID~AutoLabRes^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                ALR = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                ALR = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "MyReports") > 0 Then
                                            'FEATURE|5|4^ID~MyReports^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                MyRpts = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                MyRpts = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "Billing") > 0 Then
                                            'FEATURE|6|5^ID~Billing^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                Bill = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                Bill = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "ProlisFax") > 0 Then
                                            'FEATURE|7|6^ID~ProlisFax^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                Fax = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                Fax = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "ProlisPDF") > 0 Then
                                            'FEATURE|7|6^ID~ProlisFax^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                PDF = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                PDF = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "AuditTrail") > 0 Then
                                            'FEATURE|7|6^ID~ProlisFax^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                AuditTrail = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                AuditTrail = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "Seats") > 0 Then
                                            'FEATURE|10|10^ID~Seats^NAME|600^OWN|1^COUNT|20121121^DOA|20121121^DOS
                                            Dim tmpdata() As String = Split(Fields(4), "^")
                                            If tmpdata(1) = "COUNT" Then SEATS = Val(tmpdata(0))
                                        ElseIf InStr(Fields(2), "DiagTarget") > 0 Then
                                            'FEATURE|10|10^ID~DiagTarget^NAME|600^OWN|H|20121121^DOA|20121121^DOS
                                            'FEATURE|10|10^ID~DiagTarget^NAME|600^OWN|V|20121121^DOA|20121121^DOS
                                            DiagTarget = Fields(4)
                                        ElseIf InStr(Fields(2), "BillExport") > 0 Then
                                            'FEATURE|7|6^ID~ProlisFax^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                BillExport = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                BillExport = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "EECC") > 0 Then
                                            'FEATURE|10|10^ID~EECC^NAME|600^OWN|1^ACT|20121121^DOA|123456^UID^GHJF^PWD^45100^ClientID|20121121^DOS
                                            Dim Comps() As String = Split(Fields(6), "^")
                                            If Comps.Length > 5 AndAlso Comps(0) <> "" AndAlso
                                            Comps(2) <> "" AndAlso Comps(4) <> "" Then
                                                EECC_UID = Comps(0)
                                                EECC_PWD = Comps(2)
                                                EECC_ClientID = Comps(4)
                                            End If
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If EECC_UID <> "" And EECC_PWD <> "" And EECC_ClientID <> "" Then
                                                If Date.Now >= DOA Then
                                                    EECC = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                                Else
                                                    EECC = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                                End If
                                            Else
                                                EECC = False
                                            End If
                                        ElseIf InStr(Fields(2), "Results") > 0 Then
                                            'FEATURE|7|6^ID~ProlisFax^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                ORResults = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                ORResults = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "Accession") > 0 Then
                                            'FEATURE|7|6^ID~ProlisFax^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                ORAccession = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                ORAccession = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "Schedule") > 0 Then
                                            'FEATURE|7|6^ID~ProlisFax^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                ORSchedule = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                ORSchedule = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "Reports") > 0 Then
                                            'FEATURE|7|6^ID~ProlisFax^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                ORReports = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                ORReports = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "ORMyReports") > 0 Then
                                            'FEATURE|7|6^ID~ProlisFax^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                ORMyRpts = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                ORMyRpts = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "ORPatient") > 0 Then
                                            'FEATURE|7|6^ID~ProlisFax^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                ORPatient = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                ORPatient = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "BillDashboard") > 0 Then
                                            'FEATURE|7|6^ID~ProlisFax^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                BillDashboard = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                BillDashboard = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "PreAuth") > 0 Then
                                            'FEATURE|7|6^ID~ProlisFax^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                PreAuth = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                PreAuth = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "FollowUp") > 0 Then
                                            'FEATURE|7|6^ID~ProlisFax^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                BillFUDash = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                BillFUDash = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "PaymentDash") > 0 Then
                                            'FEATURE|7|6^ID~ProlisFax^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                PmtDash = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                PmtDash = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        Next
                    End If
                End If
            End While
        End If
        cngcl.Close()
        cngcl = Nothing
    End Sub

    Private Sub GetCentralLicense(ByVal odbCS As String, ByVal AppName As String)
        Dim i As Integer
        Dim k As Integer
        Dim TmpDate As String = ""
        Dim AppID As String = ""
        Dim SEGS() As String
        Dim DOA As Date
        Dim cngcl As New Odbc.OdbcConnection(odbCS)
        cngcl.Open()
        AppName = AppName.Replace(".UI", "")
        Dim cmdgcl As New Odbc.OdbcCommand("Select System_Params from Company", cngcl)
        cmdgcl.CommandType = CommandType.Text
        Dim drgcl As Odbc.OdbcDataReader = cmdgcl.ExecuteReader
        If drgcl.HasRows Then
            While drgcl.Read
                If drgcl("System_Params") IsNot DBNull.Value _
                AndAlso Trim(drgcl("System_Params")) <> "" Then
                    Dim LIC As String = decryptString(Trim(drgcl("System_Params")))
                    If LIC <> "" Then
                        LIC = LIC.Replace(Chr(2), "") : LIC = LIC.Replace(Chr(3), "")
                        SEGS = Split(LIC, vbCrLf)
                        For i = 0 To SEGS.Length - 1
                            If SEGS(i).StartsWith("ENTITY") Then
                                'ENTITY|6001601^ID~GLOBAL LAB INC^NAME~357167666^KEY~273985956^FID~1972800118^NPI~14D2019002^CLIA|20120606^DOS
                                Dim Fields() As String = Split(SEGS(i), "|")
                                Dim Comps() As String = Split(Fields(1), "~")
                                Licensee = Microsoft.VisualBasic.Mid(Comps(0), 1,
                                InStr(Comps(0), "^") - 1) & " - " &
                                Microsoft.VisualBasic.Mid(Comps(1), 1,
                                InStr(Comps(1), "^") - 1)
                                If Comps.Length > 3 Then
                                    For k = 2 To Comps.Length - 1
                                        If Comps(k) = "KEY" Then
                                            If Comps(k - 1) = GetClientKey() Then
                                                License = encryptString(Comps(k - 1))
                                            End If
                                        End If
                                    Next
                                End If
                            ElseIf SEGS(i).StartsWith("COMPONENT") Then
                                'COMPONENT|1|600^ID~PROLIS^NAME|4.1.0.6^VER|10^TYPE|6001601^OWN|20110806^DOD|20120606^DOS
                                'COMPONENT|2|602^ID~OUTREACH^NAME|4.1.0.6^VER|11^TYPE|6001601^OWN|20110806^DOD|20120606^DOS
                                AppID = ""
                                Dim Fields() As String = Split(SEGS(i), "|")
                                Dim Comps() As String = Split(Fields(2), "~")
                                If AppName = Microsoft.VisualBasic.Mid(Comps(1), 1,
                                    InStr(Comps(1), "^") - 1) Then
                                    AppID = Microsoft.VisualBasic.Mid(Comps(0), 1,
                                    InStr(Comps(0), "^") - 1)
                                    LicVer = Microsoft.VisualBasic.Mid(Fields(3), 1,
                                    InStr(Fields(3), "^") - 1)
                                End If
                            ElseIf SEGS(i).StartsWith("FEATURE") Then
                                'FEATURE|1|0^ID~AppRun^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                If AppID <> "" Then
                                    Dim Fields() As String = Split(SEGS(i), "|")
                                    If InStr(SEGS(i), AppID & "^OWN") > 0 Then
                                        If InStr(Fields(2), "AppRun") > 0 Then
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Today >= DOA Then 'Current or past
                                                AppRun = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                AppRun = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "AutoAccession") > 0 Then
                                            'FEATURE|2|1^ID~ClientService^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                AutoAccession = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                AutoAccession = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "ClientService") > 0 Then
                                            'FEATURE|2|1^ID~ClientService^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                CS = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                CS = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "AutoEqpRes") > 0 Then
                                            'FEATURE|3|2^ID~AutoEqpRes^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                AER = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                AER = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "AutoLabRes") > 0 Then
                                            'FEATURE|4|3^ID~AutoLabRes^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                ALR = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                ALR = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "MyReports") > 0 Then
                                            'FEATURE|5|4^ID~MyReports^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                MyRpts = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                MyRpts = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "Billing") > 0 Then
                                            'FEATURE|6|5^ID~Billing^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                Bill = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                Bill = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "ProlisFax") > 0 Then
                                            'FEATURE|7|6^ID~ProlisFax^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                Fax = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                Fax = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "ProlisPDF") > 0 Then
                                            'FEATURE|7|6^ID~ProlisFax^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                PDF = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                PDF = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "AuditTrail") > 0 Then
                                            'FEATURE|7|6^ID~ProlisFax^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                AuditTrail = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                AuditTrail = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "Seats") > 0 Then
                                            'FEATURE|10|10^ID~Seats^NAME|600^OWN|1^COUNT|20121121^DOA|20121121^DOS
                                            Dim tmpdata() As String = Split(Fields(4), "^")
                                            If tmpdata(1) = "COUNT" Then SEATS = Val(tmpdata(0))
                                        ElseIf InStr(Fields(2), "DiagTarget") > 0 Then
                                            'FEATURE|10|10^ID~DiagTarget^NAME|600^OWN|H|20121121^DOA|20121121^DOS
                                            'FEATURE|10|10^ID~DiagTarget^NAME|600^OWN|V|20121121^DOA|20121121^DOS
                                            DiagTarget = Fields(4)
                                        ElseIf InStr(Fields(2), "BillExport") > 0 Then
                                            'FEATURE|7|6^ID~ProlisFax^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                BillExport = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                BillExport = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "EECC") > 0 Then
                                            'FEATURE|10|10^ID~EECC^NAME|600^OWN|1^ACT|20121121^DOA|123456^UID^GHJF^PWD^45100^ClientID|20121121^DOS
                                            Dim Comps() As String = Split(Fields(6), "^")
                                            If Comps.Length > 5 AndAlso Comps(0) <> "" AndAlso
                                            Comps(2) <> "" AndAlso Comps(4) <> "" Then
                                                EECC_UID = Comps(0)
                                                EECC_PWD = Comps(2)
                                                EECC_ClientID = Comps(4)
                                            End If
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If EECC_UID <> "" And EECC_PWD <> "" And EECC_ClientID <> "" Then
                                                If Date.Now >= DOA Then
                                                    EECC = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                                Else
                                                    EECC = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                                End If
                                            Else
                                                EECC = False
                                            End If
                                        ElseIf InStr(Fields(2), "Results") > 0 Then
                                            'FEATURE|7|6^ID~ProlisFax^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                ORResults = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                ORResults = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "Accession") > 0 Then
                                            'FEATURE|7|6^ID~ProlisFax^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                ORAccession = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                ORAccession = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "Schedule") > 0 Then
                                            'FEATURE|7|6^ID~ProlisFax^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                ORSchedule = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                ORSchedule = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "Reports") > 0 Then
                                            'FEATURE|7|6^ID~ProlisFax^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                ORReports = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                ORReports = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "ORMyReports") > 0 Then
                                            'FEATURE|7|6^ID~ProlisFax^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                ORMyRpts = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                ORMyRpts = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "ORPatient") > 0 Then
                                            'FEATURE|7|6^ID~ProlisFax^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                ORPatient = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                ORPatient = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "BillDashboard") > 0 Then
                                            'FEATURE|7|6^ID~ProlisFax^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                BillDashboard = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                BillDashboard = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "PreAuth") > 0 Then
                                            'FEATURE|7|6^ID~ProlisFax^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                PreAuth = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                PreAuth = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "FollowUp") > 0 Then
                                            'FEATURE|7|6^ID~ProlisFax^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                BillFUDash = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                BillFUDash = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        ElseIf InStr(Fields(2), "PaymentDash") > 0 Then
                                            'FEATURE|7|6^ID~ProlisFax^NAME|600^OWN|1^ACT|20120606^DOA|20120606^DOS
                                            TmpDate = Microsoft.VisualBasic.Mid(Fields(5), 1, InStr(Fields(5), "^") - 1)
                                            DOA = DateTime.ParseExact(TmpDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                                            If Date.Now >= DOA Then
                                                PmtDash = Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            Else
                                                PmtDash = Not Convert.ToBoolean(Val(Microsoft.VisualBasic.Left(Fields(4), 1)))
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        Next
                    End If
                End If
            End While
        End If
        cngcl.Close()
        cngcl = Nothing
    End Sub

    Private Function GetClientKey() As String
        Dim tmpStr2 As String = ""
        Dim myScop As New Management.ManagementScope("\\" & Environment.MachineName & "\root\cimv2")
        Dim oQuer As New Management.SelectQuery("SELECT * FROM WIN32_DiskDrive")
        Dim oResult As New Management.ManagementObjectSearcher(myScop, oQuer)
        Dim oIte As Management.ManagementObject
        Dim oPropert As Management.PropertyData

        For Each oIte In oResult.Get()
            For Each oPropert In oIte.Properties
                If Not oPropert.Value Is Nothing AndAlso oPropert.Name = "Signature" Then
                    tmpStr2 = oPropert.Value.ToString
                    Exit For
                End If
            Next
        Next
        Return tmpStr2
    End Function

    Public Function encryptString(ByVal strtext As String) As String
        Return Encrypt(strtext, key)
    End Function

    Public Function decryptString(ByVal strtext As String) As String
        Return Decrypt(strtext, key)
    End Function

    'The function used to encrypt the text
    Private Function Encrypt(ByVal strText As String, ByVal strEncrKey _
             As String) As String
        Dim byKey() As Byte = {}
        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}

        Try
            byKey = System.Text.Encoding.UTF8.GetBytes(Left(strEncrKey, 8))

            Dim des As New DESCryptoServiceProvider()
            Dim inputByteArray() As Byte = Encoding.UTF8.GetBytes(strText)
            Dim ms As New MemoryStream()
            Dim cs As New CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Return Convert.ToBase64String(ms.ToArray())

        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    'The function used to decrypt the text
    Private Function Decrypt(ByVal strText As String, ByVal sDecrKey _
               As String) As String
        Dim byKey() As Byte = {}
        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
        Dim inputByteArray(strText.Length) As Byte

        Try
            byKey = System.Text.Encoding.UTF8.GetBytes(Left(sDecrKey, 8))
            Dim des As New DESCryptoServiceProvider()
            inputByteArray = Convert.FromBase64String(strText)
            Dim ms As New MemoryStream()
            Dim cs As New CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write)

            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8
            Dim ddd = encoding.GetString(ms.ToArray())
            Return encoding.GetString(ms.ToArray())

        Catch ex As Exception
            Return ex.Message
        End Try
    End Function
End Class
