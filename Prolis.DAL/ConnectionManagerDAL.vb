Imports Microsoft.Extensions.Configuration
Imports Newtonsoft.Json.Linq
Imports Microsoft.Data.SqlClient
Imports System.IO
Imports Prolis.Utils

Imports System.Data ' For CryptoHelper

Public Class ConnectionManagerDAL

    ' Path to appsettings.json
    Private Shared ReadOnly appSettingsPath As String = Path.Combine(AppContext.BaseDirectory, "appsettings.json")

    ' Function to test the SQL connection using a plain connection string
    Private Function TestConnection(connStr As String) As Boolean
        Try
            Using conn As New SqlConnection(connStr)
                conn.Open()
                Return True
            End Using
        Catch
            Return False
        End Try
    End Function

    ' Function to save the encrypted connection string to appsettings.json
    Public Function SaveConnectionString(plainConnStr As String) As Boolean
        ' Test connection before saving
        If Not TestConnection(plainConnStr) Then
            Return False ' Invalid connection string
        End If

        ' Encrypt connection string
        Dim encryptedConnStr As String = CryptoHelper.Encrypt(plainConnStr)

        ' Load existing JSON
        Dim json As String = File.ReadAllText(appSettingsPath)
        Dim jObject As JObject = JObject.Parse(json)

        ' Set encrypted value
        jObject("ConnectionStrings")("EncryptedDefault") = encryptedConnStr

        ' Save it back to file
        File.WriteAllText(appSettingsPath, jObject.ToString())

        DbHelper.ReloadConnection()

        Return True ' Saved successfully
    End Function

    Public Function LoadConnectionString() As String
        Try
            ' Load appsettings.json
            Dim json As String = File.ReadAllText(appSettingsPath)
            Dim jObject As JObject = JObject.Parse(json)

            ' Get encrypted connection string
            Dim encryptedConnStr As String = jObject("ConnectionStrings")("EncryptedDefault")?.ToString()

            If String.IsNullOrWhiteSpace(encryptedConnStr) Then
                Return Nothing ' or throw exception if needed
            End If

            ' Decrypt and return it
            Return CryptoHelper.Decrypt(encryptedConnStr)

        Catch ex As Exception
            ' Log or handle as needed
            Return Nothing
        End Try
    End Function




End Class
