Imports System.Runtime.CompilerServices
Imports System.Globalization

Public Module mdlExtensions
    <Extension()>
    Public Function IfNullMakeEmpty(ByRef obj As String) As String

        If obj Is DBNull.Value Then
            obj = ""
        End If
        Return obj
    End Function
    <Extension()>
    Public Function GetPaymentType(ByRef obj As Object) As String
        Dim paymenT As String = ""
        If obj = "1" Then

        ElseIf obj = "2" Then
        ElseIf obj = "3" Then
        ElseIf obj = "0" Then

        End If
        Return paymenT
    End Function
    <Extension()>
    Public Function ToDateString(ByRef obj As Object) As String
        Dim format As String = "yyyyMMdd"
        Dim parsedDate As Date = DateTime.ParseExact(obj, format, CultureInfo.InvariantCulture)

        Return parsedDate.ToString("MM/dd/yyyy")

    End Function
    <Extension()>
    Public Function ToARTypeString(ByRef obj As Object) As String
        'Client()
        'Third Party
        'Patient()
        Dim artype As String = ""
        If obj = "0" Then
            artype = "Client"
        ElseIf obj = "1" Then
            artype = "Third Party"
        ElseIf obj = "2" Then
            artype = "Patient"
        End If
        Return artype
    End Function

End Module
