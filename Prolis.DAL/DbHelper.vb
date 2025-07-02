Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Data.SqlClient
Imports Microsoft.Identity.Client
Imports Prolis.Utils

Public Class DbHelper

    'Private Shared ReadOnly _connectionString As String

    'Shared Sub New()
    '    ' Get encrypted conn string from appsettings.json (UI project)
    '    Dim encrypted = AppConfig.Configuration("ConnectionStrings:EncryptedDefault")
    '    ' Decrypt using internal key
    '    Dim decrypted = CryptoHelper.Decrypt(encrypted)
    '    _connectionString = decrypted
    'End Sub

    Private Shared _connectionString As String

    ' Load only once unless explicitly refreshed
    Shared Sub New()
        LoadConnection()
    End Sub

    ' Public method to reload after settings change
    Public Shared Sub ReloadConnection()
        LoadConnection(forceReload:=True)
    End Sub

    ' Internal loader
    Private Shared Sub LoadConnection(Optional forceReload As Boolean = False)
        If _connectionString Is Nothing OrElse forceReload Then
            Dim encrypted = AppConfig.Configuration("ConnectionStrings:EncryptedDefault")
            Dim decrypted = CryptoHelper.Decrypt(encrypted)
            _connectionString = decrypted
        End If

    End Sub
    ' Execute SELECT and return DataTable
    Public Shared Function ExecuteQuery(query As String, Optional parameters As SqlParameter() = Nothing) As DataTable
        Dim table As New DataTable()

        Using conn As New SqlConnection(_connectionString)
            Using cmd As New SqlCommand(query, conn)
                If parameters IsNot Nothing Then
                    cmd.Parameters.AddRange(parameters)
                End If

                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(table)
                End Using
            End Using
        End Using

        Return table
    End Function

    ' Execute INSERT/UPDATE/DELETE
    Public Shared Function ExecuteNonQuery(query As String, Optional parameters As SqlParameter() = Nothing) As Integer
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Using cmd As New SqlCommand(query, conn)
                If parameters IsNot Nothing Then
                    cmd.Parameters.AddRange(parameters)
                End If
                Return cmd.ExecuteNonQuery()
            End Using
        End Using
    End Function

    ' Execute and return a single value (e.g., count, sum, scalar)
    Public Shared Function ExecuteScalar(query As String, Optional parameters As SqlParameter() = Nothing) As Object
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Using cmd As New SqlCommand(query, conn)
                If parameters IsNot Nothing Then
                    cmd.Parameters.AddRange(parameters)
                End If
                Return cmd.ExecuteScalar()
            End Using
        End Using
    End Function

End Class
