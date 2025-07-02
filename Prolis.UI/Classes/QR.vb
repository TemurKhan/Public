Imports System.Reflection
Imports System.Data.SqlClient

Public Class QR

    Shared Property Qualify As Boolean

    Public Shared Function GenerateQrForAccession(AccID As Long, token As String, DNS As String, companyID As String) As Boolean
        Dim success = False
        Dim MyConnection1 As New SqlConnection(connString)
        If MyConnection1.State = ConnectionState.Closed Then
            MyConnection1.Open()
        End If
        Dim found = False
        Dim selcm As New SqlCommand("select * from QR where AccID=" & AccID, MyConnection1)
        selcm.CommandType = CommandType.Text
        Try
            Dim dr As SqlDataReader = selcm.ExecuteReader()
            If dr.HasRows Then
                While dr.Read
                    found = True
                End While
            End If

        Catch ex As Exception
            ' SendMail("ShowPDFReport", "PrintReport", ex.Message)
        Finally
            If MyConnection1.State = ConnectionState.Open Then
                MyConnection1.Close()
            End If
        End Try
        If found Then
            Return success
        End If
        If String.IsNullOrEmpty(token) Or String.IsNullOrEmpty(DNS) Then
            MessageBox.Show("QR Code couldn't generated because QR credentials are empty", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If
        Dim dll = Environment.CurrentDirectory() & "/" & companyID & ".dll"
        Dim file = ""
        Dim Odbc = "DSN=ProlisQC; MARS_Connection=yes; UID=prolis;PWD=gujrati"
        If Not System.IO.File.Exists(dll) Then
            Return False
        End If
        Dim objAssembly As Assembly = Assembly.LoadFrom(dll)

        Dim t As Type = objAssembly.GetType("QR.QRProcess")
        Dim obj As Object = Activator.CreateInstance(t)
        Debug.Assert(obj IsNot Nothing)
        Dim m As MethodInfo = t.GetMethod("GenerateAsync")
        Debug.Assert(m IsNot Nothing)
        '--------------------------------------------------------

        Dim pname = "test"
        Dim pId = "1"
        Dim dob = "1"
        Dim Collection_Date As String = ""


        If MyConnection1.State = ConnectionState.Closed Then
            MyConnection1.Open()
        End If
        Dim selcmd1 As New SqlCommand("select P.ID as pid, Concat(P.LastName,' ', P.FirstName) as PatientName ,p.DOB,p.HomePhone as Phone,p.Sex,R.ID as AccID, Sp.SourceDate as [Collection_Date] from Requisitions R join Patients P on R.Patient_ID=P.ID join Specimens Sp on sp.Accession_ID=R.ID    where R.ID=" & AccID, MyConnection1)
        selcmd1.CommandType = CommandType.Text
        Try
            Dim dr As SqlDataReader = selcmd1.ExecuteReader()
            If dr.HasRows Then
                While dr.Read
                    pname = dr("PatientName")
                    pId = dr("pid")
                    dob = dr("DOB")
                    Collection_Date = dr("Collection_Date")
                End While
            End If

        Catch ex As Exception
            ' SendMail("ShowPDFReport", "PrintReport", ex.Message)
        Finally
            If MyConnection1.State = ConnectionState.Open Then
                MyConnection1.Close()
            End If
        End Try
        If Not String.IsNullOrEmpty(Collection_Date) Then
            ' Collection_Date = Collection_Date.Replace(":", "-").Replace("/", "-")
            'dob = dob.Replace(":", "-").Replace("/", "-")
            Dim qrdetail As String = "Collection_Date=" & Collection_Date & "|Accession_ID=" & AccID.ToString() & "|Patient_Name=" & pname & "|DOB=" & dob & "|Patient_ID=" & pId



            Dim par = {qrdetail, AccID.ToString(), decryptString(token), file, DNS, Odbc, True}
            Dim parameters As Object() = New Object(0) {par}

            'invoke method
            m.Invoke(obj, par)
            Return success
        End If
        '--------------------------------------------------------|
        If MyConnection1.State = ConnectionState.Open Then
            MyConnection1.Close()
        End If


        Return success

    End Function
End Class
