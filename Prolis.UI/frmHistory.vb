Imports System.Windows.Forms
Imports System.Data

Public Class frmHistory

    Public TestID As String = "0"  'bcz this can be empty or null
    Public AccessionID As String = "0" 'bcz this can be empty or null

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function


    Private Sub frmHistory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim TestID As Integer = frmResults.TestID
        Dim PatID As Long = 0
        txtName.Text = "" : txtDOB.Text = "" : txtSex.Text = ""
        pctSex.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Blank.ico")
        Dim cn1 As New SqlClient.SqlConnection(connString)
        cn1.Open()
        Dim cmd1 As New SqlClient.SqlCommand($"Select * from  
                                                Patients where ID in (Select Patient_ID from Requisitions  
                                                where ID = {AccessionID})", cn1)
        cmd1.CommandType = CommandType.Text
        Dim dr1 As SqlClient.SqlDataReader = cmd1.ExecuteReader
        If dr1.HasRows Then
            While dr1.Read
                PatID = dr1("ID")
                txtName.Text = dr1("LastName") & ", " & dr1("FirstName")
                txtDOB.Text = Format(dr1("DOB"), SystemConfig.DateFormat)
                If dr1("Sex") = "M" Then
                    pctSex.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Male.ico")
                    txtSex.Text = "Male"
                ElseIf dr1("Sex") = "F" Then
                    pctSex.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Female.ico")
                    txtSex.Text = "Female"
                Else
                    pctSex.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Blank.ico")
                    txtSex.Text = "Unknown"
                End If
            End While
        End If
        cn1.Close()
        cn1 = Nothing
        '
        txtTest.Text = ""
        Dim cn2 As New SqlClient.SqlConnection(connString)
        cn2.Open()
        Dim cmd2 As New SqlClient.SqlCommand("Select * from Tests where ID = " & TestID, cn2)
        cmd2.CommandType = CommandType.Text
        Dim dr2 As SqlClient.SqlDataReader = cmd2.ExecuteReader
        If dr2.HasRows Then
            While dr2.Read
                txtTest.Text = dr2("Name")
                If dr2("UOM") Is DBNull.Value Then
                    txtUnit.Text = ""
                Else
                    txtUnit.Text = dr2("UOM")
                End If
            End While
        End If
        cn2.Close()
        cn2 = Nothing
        dgvResults.Rows.Clear()
        Dim cn3 As New SqlClient.SqlConnection(connString)
        cn3.Open()
        Dim cmd3 As New SqlClient.SqlCommand("Select a.ID as AccID, a.AccessionDate as " &
        "AccDate, b.Result as Result from Requisitions a inner join Acc_Results b on a.ID " &
        "= b.Accession_ID where a.Patient_ID = " & PatID & " and b.Test_ID = " & TestID &
        " order by a.AccessionDate DESC", cn3)
        cmd3.CommandType = CommandType.Text
        Dim dr3 As SqlClient.SqlDataReader = cmd3.ExecuteReader
        If dr3.HasRows Then
            While dr3.Read
                dgvResults.Rows.Add(dr3("AccID"), Format(dr3("AccDate"),
                SystemConfig.DateFormat), dr3("Result"))
            End While
        End If
        cn3.Close()
        cn3 = Nothing
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class
