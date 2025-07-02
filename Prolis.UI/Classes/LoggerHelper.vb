Imports NLog
Imports System.Runtime.CompilerServices
Imports System.Diagnostics
Imports Microsoft.Identity.Client.Internal

Public Class LoggerHelper
    Private Shared ReadOnly Logger As Logger = LogManager.GetCurrentClassLogger()

    ' Logs an error message only in debug mode
    Public Shared Sub LogError(ex As Exception,
                               Optional message As String = "",
                               <CallerMemberName> Optional caller As String = Nothing,
                               <CallerLineNumber> Optional lineNumber As Integer = 0)

#If DEBUG Then
        Dim callingClass As String = New StackFrame(1).GetMethod().DeclaringType.Name
        Logger.Error(ex, $"{message} | Class: {callingClass} | Method: {caller} | Line: {lineNumber}")
#End If
    End Sub

    ' Logs an info message only in debug mode
    Public Shared Sub LogInfo(message As String,
                              <CallerMemberName> Optional caller As String = Nothing,
                              <CallerLineNumber> Optional lineNumber As Integer = 0)

#If DEBUG Then
        Dim callingClass As String = New StackFrame(1).GetMethod().DeclaringType.Name
        Logger.Info($"{message} | Class: {callingClass} | Method: {caller} | Line: {lineNumber}")
#End If
    End Sub
End Class
