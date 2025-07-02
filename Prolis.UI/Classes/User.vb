Imports System.Runtime.Remoting.Messaging

Public Class User
    Property UserID As Long
    Property UserName As String
    Property IsClientUser As Boolean = False
    Property unReadMessageCount As String
    Property listOfMessages As New List(Of Messages)

    ' Override Equals and GetHashCode based on the ID property
    Public Overrides Function Equals(obj As Object) As Boolean
        If obj Is Nothing OrElse Not Me.GetType() Is obj.GetType() Then
            Return False
        End If

        Dim other As User = DirectCast(obj, User)
        Return Me.UserID = other.UserID
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return Me.UserID.GetHashCode()
    End Function

End Class

Public Class Messages
    Property messageID As Integer
    Property SenderID As Integer
    Property SenderName As String
    Property ReceiverID As Integer
    Property ReceiverName As String
    Property messageText As String
    Property isRead As Boolean
    Property Dated As Date
    Property isAdded As Boolean
    Property megType As String



End Class

