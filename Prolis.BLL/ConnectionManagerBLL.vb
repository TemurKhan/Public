Imports Microsoft.Data.SqlClient
Imports System.Data
Imports Prolis.DAL

Public Class ConnectionManagerBLL

    ' Function to handle the encryption and save to DAL
    Public Sub EncryptAndSaveConnectionString(connString As String)

        Dim conManager As New ConnectionManagerDAL()
        ' Validate the connection
        If conManager.SaveConnectionString(connString) Then

        Else
            Throw New Exception("Unable to connect to the server. Please check your connection details.")
        End If
    End Sub

    Public Function DecryptAndLoadConnectionString() As String
        Dim conManager As New ConnectionManagerDAL()
        ' Validate the connection
        Dim connString As String = conManager.LoadConnectionString()
        If Not String.IsNullOrEmpty(connString) Then

            Return connString

        Else
            Throw New Exception("Unable to connect to the server. Please check your connection details.")
        End If
    End Function


    ' Method to retrieve a DataTable
    Public Shared Function GetDataTable(query As String, Optional parameters As SqlParameter() = Nothing) As DataTable
        Return DbHelper.ExecuteQuery(query, parameters)
    End Function

    ' Method to execute INSERT/UPDATE/DELETE queries
    Public Shared Function ExecuteCommand(query As String, Optional parameters As SqlParameter() = Nothing) As Integer
        Return DbHelper.ExecuteNonQuery(query, parameters)
    End Function

    ' Method to execute scalar queries (e.g., count, sum)
    Public Shared Function GetScalarValue(query As String, Optional parameters As SqlParameter() = Nothing) As Object

        Return DbHelper.ExecuteScalar(query, parameters)
    End Function

End Class
