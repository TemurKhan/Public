Imports System.Reflection
Imports System.IO

Public Class Services
    Public Shared Function InvokeMethod(ByVal dllPath As String, ByVal className As String, ByVal methodName As String, ByVal parameters As Object()) As Object

        If Not File.Exists(dllPath) Then
            MessageBox.Show("Brady printer is not set because DLL is missing. Contact Prolis support.")
            Return ""
        End If
        Dim assembly As Assembly = assembly.LoadFrom(dllPath)
     
        ' Get the type of the class
        Dim types = assembly.GetTypes().First(Function(c) c.Name = className)
        Dim type As Type = assembly.GetType(className)

        ' Create an instance of the class
        Dim instance As Object = Activator.CreateInstance(types)

        ' Get the method info
        Dim methodInfo As MethodInfo = types.GetMethod(methodName)

        ' Invoke the method with parameters
        Return methodInfo.Invoke(instance, parameters)
    End Function
End Class
