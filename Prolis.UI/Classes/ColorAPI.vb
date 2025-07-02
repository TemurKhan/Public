Imports System.Reflection
Imports Newtonsoft.Json

Public Class CycleThreshold
    Public Property gene As String
    Public Property value As Double
End Class

Public Class ResultPayLoad
    Public Property batch_id As String
    Public Property significance As String
    Public Property cycle_thresholds As List(Of CycleThreshold)
    Public Property assay_type As String
    Public Property time_completed As DateTime
End Class
Public Class ColorRes
    Public Property accession_number As String
    Public Property result_type As String
    Public Property sample_type As String
    Public Property is_approved_for_processing As Boolean
    Public Property rejection_reason As Object
    Public Property collected_at As DateTime
    Public Property organization_name As String
    Public Property shipping_batch_id As Object
    Public Property customer_account_name As String
End Class

Public Class ColorMessage
    Public Property code As String
    Public Property message As String
     
End Class
Public Class ColorAPI
    Shared Property Active As Boolean
    Public Shared Function Sample(BarCode As String, token As String) As String

        Dim dll = Environment.CurrentDirectory() & "/" & "Prolis_Color" & ".dll"

        Dim res As ColorRes = New ColorRes()
        If Not System.IO.File.Exists(dll) Then
            Return "Color interface does not exist in prolis directory"
        End If
        Dim objAssembly As Assembly = Assembly.LoadFrom(dll)
        Dim t As Type = objAssembly.GetType("Prolis_Color.Color")
        Dim obj As Object = Activator.CreateInstance(t)
        Debug.Assert(obj IsNot Nothing)

        Dim m As MethodInfo = t.GetMethod("Sample")
        Dim prm = New Object() {BarCode, token}
        Dim rs = m.Invoke(obj, prm)

        'res = JsonConvert.DeserializeObject(Of ColorRes)(rs)
        Return rs
    End Function
    Public Shared Function Result(BarCode As String, token As String) As String
        Dim dll = Environment.CurrentDirectory() & "/" & "Prolis_Color" & ".dll"

        Dim res As ColorRes = New ColorRes()
        If Not System.IO.File.Exists(dll) Then
            Return "DLL NOT FOUND"
        End If
        Dim objAssembly As Assembly = Assembly.LoadFrom(dll)
        Dim t As Type = objAssembly.GetType("Prolis_Color.Color")
        Dim obj As Object = Activator.CreateInstance(t)
        Debug.Assert(obj IsNot Nothing)
        Dim m As MethodInfo = t.GetMethod("Result") 'Result (json),barcode,token
        Dim prm = New Object() {BarCode, token}
        Dim rs = m.Invoke(obj, prm)
        'res = JsonConvert.DeserializeObject(Of ColorRes)(rs)
        Return rs
    End Function
    Public Shared Function Destroy(BarCode As String, token As String) As String
        Dim dll = Environment.CurrentDirectory() & "/" & "Prolis_Color" & ".dll"

        Dim res As ColorRes = New ColorRes()
        If Not System.IO.File.Exists(dll) Then
            Return "DLL NOT FOUND"
        End If
        Dim objAssembly As Assembly = Assembly.LoadFrom(dll)
        Dim t As Type = objAssembly.GetType("Prolis_Color.Color")
        Dim obj As Object = Activator.CreateInstance(t)
        Debug.Assert(obj IsNot Nothing)
        Dim m As MethodInfo = t.GetMethod("Destroy")
        Dim prm = New Object() {BarCode, token}
        Dim rs = m.Invoke(obj, prm)
        'res = JsonConvert.DeserializeObject(Of ColorRes)(rs)
        Return rs
    End Function
End Class
