Option Compare Text
Imports System.Windows.Forms
Imports Microsoft.Data.SqlClient

Public Class frmESig

    Private Sub frmESig_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtPWD1.Text = ""
        txtPWD2.Text = ""
        PopulateSignatories()
        Dim Data As String = frmResults.txtRTF.Text
        If Data.Length > 10 Then
            Do
                Data = Microsoft.VisualBasic.Mid(Data, InStr(Data, vbLf) + 1)
            Loop Until (Data.StartsWith(vbLf) = False)
            txtSignee.Text = Trim(Microsoft.VisualBasic.Mid(Data, 1, InStr(Data, vbLf) - 1))
            Do
                Data = Microsoft.VisualBasic.Mid(Data, InStr(Data, vbLf) + 1)
            Loop Until (Data.StartsWith(vbLf) = False)
            For i As Integer = 0 To dgvSignatories.RowCount - 1
                If InStr(dgvSignatories.Rows(i).Cells(1).Value, Data) > 0 Then
                    dgvSignatories.Rows(i).Selected = True
                    txtPWD1.ReadOnly = False
                    txtPWD2.ReadOnly = False
                    Exit For
                End If
            Next
        End If
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub PopulateSignatories0()
        'Dim POS As String = ""
        'dgvSignatories.Rows.Clear()
        'Dim CNP As New ADODB.Connection
        'CNP.Open(connstring)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("Select * from Users where (Cytotech <> 0 or Pathologist " & _
        '"<> 0) Union Select * from Users where (Fullname in (Select LastNAme " & _
        '"+ ' ' + FirstNAme as Name from Lab_Directors) or (FullName in (Select " & _
        '"FirstName + ' ' + LastNAme from Lab_Directors)))", CNP, _
        'ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        'If Not Rs.BOF Then
        '    Do Until Rs.EOF
        '        If Rs.Fields("Pathologist").Value <> 0 Then POS += "PATH, "
        '        If Rs.Fields("Director").Value <> 0 Then POS += "DIR, "
        '        If Rs.Fields("Cytotech").Value <> 0 Then POS += "CYTOTECH"
        '        If Microsoft.VisualBasic.Right(POS, 2) = ", " Then _
        '        POS = Microsoft.VisualBasic.Mid(POS, 1, Len(POS) - 2)
        '        dgvSignatories.Rows.Add(Rs.Fields("ID").Value, _
        '        Rs.Fields("FullName").Value, POS)
        '        POS = ""
        '        Rs.MoveNext()
        '    Loop
        'End If
        'Rs.Close()
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
    End Sub

    Private Sub PopulateSignatories()
        dgvSignatories.Rows.Clear()

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "
            SELECT ID, FullName, 
                   CASE WHEN Pathologist <> 0 THEN 'PATH, ' ELSE '' END +
                   CASE WHEN Director <> 0 THEN 'DIR, ' ELSE '' END +
                   CASE WHEN Cytotech <> 0 THEN 'CYTOTECH' ELSE '' END AS Position
            FROM Users 
            WHERE Pathologist <> 0 OR Cytotech <> 0
            UNION 
            SELECT ID, FullName, 'DIR' 
            FROM Users 
            WHERE FullName IN (
                SELECT LastName + ' ' + FirstName FROM Lab_Directors 
                UNION 
                SELECT FirstName + ' ' + LastName FROM Lab_Directors
            )"

            Using command As New SqlCommand(query, connection)
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Dim POS As String = reader("Position").ToString().TrimEnd(", ")
                        dgvSignatories.Rows.Add(reader("ID"), reader("FullName"), POS)
                    End While
                End Using
            End Using
        End Using
    End Sub
    Private Sub dgvSignatories_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSignatories.CellClick
        If e.RowIndex <> -1 Then
            txtPWD1.ReadOnly = False
            txtPWD2.ReadOnly = False
        Else
            txtPWD1.ReadOnly = True
            txtPWD2.ReadOnly = True
        End If
    End Sub

    Private Sub btnValidate_Click0(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValidate.Click
        'If txtPWD1.Text <> "" And txtPWD1.Text = txtPWD2.Text _
        'And dgvSignatories.SelectedRows.Count > 0 Then
        '    Dim CNP As New ADODB.Connection
        '    CNP.Open(connstring)
        '    Dim Rs As New ADODB.Recordset
        '    Rs.Open("Select * from Users where ID = " &
        '    dgvSignatories.SelectedRows(0).Cells(0).Value, CNP,
        '    ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        '    If Not Rs.BOF Then
        '        If decryptString(Rs.Fields("Password").Value) = txtPWD1.Text Then
        '            ' Dim MyLic As New LicenseManager.License

        '            If LIC Is Nothing Then LIC = New LicenseManager.ProlisLicense(connString, My.Application.Info.AssemblyName)

        '            txtSignee.Text = LIC.encryptString(Rs.Fields("ID").Value &
        '            "|" & Rs.Fields("FullName").Value)
        '            Me.Tag = Rs.Fields("ID").Value & "|" &
        '            Rs.Fields("FullName").Value & "|" & txtSignee.Text
        '            btnAccept.Enabled = True
        '            btnDelete.Enabled = True
        '            txtPWD1.Text = ""
        '            txtPWD2.Text = ""
        '            txtPWD1.ReadOnly = True
        '            txtPWD2.ReadOnly = True
        '            dgvSignatories.SelectedRows(0).Selected = False
        '        Else
        '            MsgBox("You need to enter your correct password in both fields", MsgBoxStyle.Critical, "Prolis")
        '            txtSignee.Text = ""
        '            Me.Tag = ""
        '            btnAccept.Enabled = False
        '            btnDelete.Enabled = False
        '        End If
        '    End If
        '    Rs.Close()
        '    Rs = Nothing
        '    CNP.Close()
        '    CNP = Nothing
        'Else
        '    MsgBox("You need to enter your correct password in both fields", MsgBoxStyle.Critical, "Prolis")
        '    txtSignee.Text = ""
        '    Me.Tag = ""
        '    btnAccept.Enabled = False
        'End If
    End Sub

    Private Sub btnValidate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValidate.Click
        If txtPWD1.Text <> "" AndAlso txtPWD1.Text = txtPWD2.Text AndAlso dgvSignatories.SelectedRows.Count > 0 Then
            Using connection As New SqlConnection(connString)
                connection.Open()

                Dim query As String = "SELECT ID, FullName, Password FROM Users WHERE ID = @UserID"

                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@UserID", dgvSignatories.SelectedRows(0).Cells(0).Value)

                    Using reader As SqlDataReader = command.ExecuteReader()
                        If reader.Read() Then
                            If decryptString(reader("Password").ToString()) = txtPWD1.Text Then
                                ' Dim MyLic As New LicenseManager.License
                                If LIC Is Nothing Then LIC = New LicenseManager.ProlisLicense(connString, My.Application.Info.AssemblyName)
                                txtSignee.Text = LIC.encryptString(reader("ID").ToString() & "|" & reader("FullName").ToString())
                                Me.Tag = reader("ID").ToString() & "|" & reader("FullName").ToString() & "|" & txtSignee.Text

                                btnAccept.Enabled = True
                                btnDelete.Enabled = True
                                txtPWD1.Text = ""
                                txtPWD2.Text = ""
                                txtPWD1.ReadOnly = True
                                txtPWD2.ReadOnly = True
                                dgvSignatories.SelectedRows(0).Selected = False
                            Else
                                MsgBox("You need to enter your correct password in both fields", MsgBoxStyle.Critical, "Prolis")
                                txtSignee.Text = ""
                                Me.Tag = ""
                                btnAccept.Enabled = False
                                btnDelete.Enabled = False
                            End If
                        End If
                    End Using
                End Using
            End Using
        Else
            MsgBox("You need to enter your correct password in both fields", MsgBoxStyle.Critical, "Prolis")
            txtSignee.Text = ""
            Me.Tag = ""
            btnAccept.Enabled = False
        End If
    End Sub
    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        txtSignee.Text = ""
        Me.Tag = ""
        txtPWD1.Text = ""
        txtPWD2.Text = ""
        txtPWD1.ReadOnly = True
        txtPWD2.ReadOnly = True
        If dgvSignatories.SelectedRows.Count > 0 Then _
        dgvSignatories.SelectedRows(0).Selected = False
        btnAccept.Enabled = False
        btnDelete.Enabled = False
        Me.Close()
    End Sub
End Class
