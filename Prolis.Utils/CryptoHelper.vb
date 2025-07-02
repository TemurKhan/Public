Imports System.Security.Cryptography
Imports System.Text
Imports System.Linq

Public Class CryptoHelper

    ' 🔐 Hardcoded encryption key — hidden from UI/appsettings
    Private Shared ReadOnly key As String = "ProlisByTariqCh."   '16 bytes (128 bits)
    Public Shared Function Encrypt(plainText As String) As String
        Dim aes As Aes = Aes.Create()
        aes.Key = Encoding.UTF8.GetBytes(key)
        aes.GenerateIV()

        Dim encryptor = aes.CreateEncryptor()
        Dim inputBytes = Encoding.UTF8.GetBytes(plainText)
        Dim encryptedBytes = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length)

        Dim result = Convert.ToBase64String(aes.IV.Concat(encryptedBytes).ToArray())
        Return result
    End Function

    Public Shared Function Decrypt(encryptedBase64 As String) As String
        Dim fullBytes = Convert.FromBase64String(encryptedBase64)
        Dim iv = fullBytes.Take(16).ToArray()
        Dim cipherText = fullBytes.Skip(16).ToArray()

        Dim aes As Aes = Aes.Create()
        aes.Key = Encoding.UTF8.GetBytes(key)
        aes.IV = iv

        Dim decryptor = aes.CreateDecryptor()
        Dim decryptedBytes = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length)
        Return Encoding.UTF8.GetString(decryptedBytes)
    End Function
End Class
