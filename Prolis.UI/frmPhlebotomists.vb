Imports Microsoft.Data.SqlClient

Public Class frmPhlebotomists

    Public Shared RouteId = 0
    Private Sub frmPhlebotomists_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PopulatePhlebotomists()
        'txtCell.TextMaskFormat = SystemConfig.PhoneMask
        'txtHPhone.TextMaskFormat = SystemConfig.PhoneMask
        'txtStartDate.TextMaskFormat = SystemConfig.DateFormat
        'txtEndDate.TextMaskFormat = SystemConfig.DateFormat
    End Sub

    Private Sub PopulatePhlebotomists()
        dgvPhlebotomists.Rows.Clear()
        Dim MName As String = ""
        Dim Cell As String = ""
        Dim HPhone As String = ""
        Dim AddressID As String = ""
        Dim cnph As New SqlConnection(connString)
        cnph.Open()
        Dim cmdph As New SqlCommand("Select * from Phlebotomists", cnph)
        cmdph.CommandType = CommandType.Text
        Dim drph As SqlDataReader = cmdph.ExecuteReader
        If drph.HasRows Then
            While drph.Read
                If drph("MiddleName") Is DBNull.Value Then
                    MName = ""
                Else
                    MName = Trim(drph("MiddleName"))
                End If
                If drph("Cell") Is DBNull.Value Then
                    Cell = ""
                Else
                    Cell = Trim(drph("cell"))
                End If
                Try
                    If drph("HomePhone") Is DBNull.Value Then
                        HPhone = ""
                    Else
                        HPhone = Trim(drph("HomePhone"))
                    End If
                Catch ex As Exception

                End Try
                Try
                    If drph("HPhone") Is DBNull.Value Then
                        HPhone = ""
                    Else
                        HPhone = Trim(drph("HPhone"))
                    End If
                Catch ex As Exception

                End Try

                If drph("Address_ID") Is DBNull.Value Then
                    AddressID = "0"
                Else
                    AddressID = drph("Address_ID").ToString
                End If
                dgvPhlebotomists.Rows.Add(drph("ID"), drph("LastName"), drph("FirstName"), MName,
                drph("FullName"), drph("Email"), drph("Password"), drph("IsActive"), drph("Cell"),
                Cell, HPhone, AddressID, IIf(AddressID <> "", GetAddress(AddressID), ""))
            End While
        End If
        cnph.Close()
        cnph = Nothing
    End Sub

    Private Sub chkActive_CheckedChanged(sender As Object, e As EventArgs) Handles chkActive.CheckedChanged
        If chkActive.Checked = False Then
            chkActive.Text = "No"
        Else
            chkActive.Text = "Yes"
        End If
    End Sub

    Private Sub chkEditNew_Click(sender As Object, e As EventArgs) Handles chkEditNew.Click
        If chkEditNew.Checked = False Then
            chkEditNew.Text = "Edit"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Edit.ico")
            ClearForm()
            dgvPhlebotomists.Enabled = True
            btnDelete.Enabled = True
        Else
            chkEditNew.Text = "New"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\New.ico")
            ClearForm()
            txtID.Text = NextPhlebotomistID()
            dgvPhlebotomists.Enabled = False
            btnDelete.Enabled = False
        End If
        btnAccept.Enabled = False
    End Sub

    Private Sub ClearForm()
        txtID.Text = ""
        txtLName.Text = ""
        txtFName.Text = ""
        txtMName.Text = ""
        chkActive.Checked = True
        txtEmail.Text = ""
        txtPassword.Text = ""
        txtCell.Text = ""
        txtHPhone.Text = ""
        txtAddress.Text = ""
        txtCity.Text = ""
        txtState.Text = ""
        txtZip.Text = ""
        txtCountry.Text = ""
        txtStartDate.Text = ""
        txtEndDate.Text = ""
        btnAccept.Enabled = False
    End Sub

    Private Function NextPhlebotomistID() As Integer
        Dim PID As Integer = 1
        Dim cnnid As New SqlConnection(connString)
        cnnid.Open()
        Dim cmdnid As New SqlCommand(
        "Select max(ID) as LastID from Phlebotomists", cnnid)
        cmdnid.CommandType = CommandType.Text
        Dim drnid As SqlDataReader = cmdnid.ExecuteReader
        If drnid.HasRows Then
            While drnid.Read
                If drnid("LastID") IsNot DBNull.Value _
                Then PID = drnid("LastID") + 1
            End While
        End If
        cnnid.Close()
        cnnid = Nothing
        Return PID
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtID.Text <> "" And Trim(txtLName.Text) <> "" And
        Trim(txtFName.Text) <> "" And Trim(txtEmail.Text) <> "" Then
            SavePhlebotomist(Val(txtID.Text))
            PopulatePhlebotomists()
            ClearForm()
            If chkEditNew.Checked = True Then
                txtID.Text = NextPhlebotomistID()
                txtLName.Focus()
            End If
        End If
    End Sub

    Private Sub SavePhlebotomist(ByVal PhlebID As Long)
        If Trim(txtLName.Text) <> "" And Trim(txtFName.Text) <> "" And
        Trim(txtEmail.Text) <> "" Then
            If String.IsNullOrEmpty(txtMName.Text) Then
                txtMName.Text = ""
            End If
            Dim AddressID As String = ""
            If Trim(txtAddress.Text) <> "" And Trim(txtCity.Text) <> "" And
            Trim(txtState.Text) <> "" And Trim(txtZip.Text) <> "" Then
                AddressID = GetAddressID(Trim(txtAddress.Text), "", Trim(txtCity.Text), Trim(txtState.Text), Trim(txtZip.Text), Trim(txtCountry.Text))
            Else
                AddressID = ""
            End If
            Dim mm = ""
            If String.IsNullOrEmpty((txtMName.Text)) Then
                mm = ""
            Else
                mm = " " & Trim(txtMName.Text).Substring(0, 1).ToUpper
            End If

            Dim q = "If Exists (Select * from Phlebotomists where ID = " & PhlebID & ") Update " &
            "Phlebotomists set LastName = '" & Trim(txtLName.Text) & "', FirstName = '" & Trim(txtFName.Text) &
            "', MiddleName = '" & Trim(txtMName.Text) & "', FullName = '" & Trim(txtLName.Text) & ", " &
            Trim(txtFName.Text) & mm & "', IsActive = " & Convert.ToInt16(chkActive.Checked) & ", Email = '" & Trim(txtEmail.Text) &
            "', Password = '" & IIf(Trim(txtPassword.Text) <> "", LIC.encryptString(Trim(txtPassword.Text)), "") &
            "', Cell = '" & Trim(txtCell.Text) & "', HomePhone = '" & Trim(txtHPhone.Text) & "', Address_ID = " &
            IIf(AddressID <> "", AddressID, "Null") & ", StartDate = '" & txtStartDate.Text &
            "', EndDate = " & IIf(IsDate(txtEndDate.Text), "'" & txtEndDate.Text & "'", "NULL") & " where ID = " &
            PhlebID & " Else Insert into Phlebotomists (ID, LastName, FirstName, MiddleName, FullName, IsActive, " &
            "Email, User_ID, Password, SSN, Cell, HomePhone, Address_ID, StartDate, EndDate) values (" & PhlebID &
            ", '" & Trim(txtLName.Text) & "', '" & Trim(txtFName.Text) & "', '" & Trim(txtMName.Text) & "', '" &
            Trim(txtLName.Text) & ", " & Trim(txtFName.Text) & mm & "', " & Convert.ToInt16(chkActive.Checked) & ", '" &
            Trim(txtEmail.Text) & "', '" & Trim(txtEmail.Text) & "', '" & IIf(Trim(txtPassword.Text) <> "",
            LIC.encryptString(Trim(txtPassword.Text)), "") & "', '', '" & Trim(txtCell.Text) & "', '" &
            Trim(txtHPhone.Text) & "', " & IIf(AddressID <> "", AddressID, "Null") & ", '" & txtStartDate.Text &
            "', " & IIf(IsDate(txtEndDate.Text), "'" & txtEndDate.Text & "'", "NULL") & ")"
            ExecuteSqlProcedure(q)
            '
        End If
    End Sub

    Private Sub dgvPhlebotomists_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPhlebotomists.CellContentDoubleClick
        DisplayPhlebotomist(dgvPhlebotomists.Rows(e.RowIndex).Cells(0).Value)
    End Sub
    Private Sub DisplayPhlebotomist(ByVal pbId As Long)

        Dim cndr As New SqlConnection(connString)
        cndr.Open()
        Dim cmddr As New SqlCommand("Select " &
        "* from Phlebotomists p left outer join Addresses a on p.Address_ID=a.ID where p.ID = " & pbId, cndr)
        cmddr.CommandType = CommandType.Text
        Dim drdr As SqlDataReader = cmddr.ExecuteReader
        If drdr.HasRows Then
            While drdr.Read
                txtID.Text = drdr("ID")
                txtLName.Text = drdr("LastName")
                txtFName.Text = drdr("FirstName")
                txtMName.Text = drdr("MiddleName")

                txtEmail.Text = drdr("Email")
                '' drdr("User_ID")
                txtPassword.Text = drdr("Password")
                '  drdr("SSN")
                txtHPhone.Text = drdr("HomePhone")
                txtCell.Text = drdr("Cell")
                ' drdr("IsActive")
                txtAddress.Text = IIf(drdr("Address1") Is DBNull.Value, "", drdr("Address1").ToString())
                txtStartDate.Text = IIf(drdr("StartDate") Is DBNull.Value Or drdr("StartDate").ToString().Contains("1900"), "", drdr("StartDate").ToString())
                txtEndDate.Text = IIf(drdr("EndDate") Is DBNull.Value, "", drdr("EndDate").ToString())
                txtCity.Text = IIf(drdr("City") Is DBNull.Value, "", drdr("City").ToString())
                txtState.Text = IIf(drdr("State") Is DBNull.Value, "", drdr("State").ToString())
                txtCountry.Text = IIf(drdr("Country") Is DBNull.Value, "", drdr("Country").ToString())
                txtZip.Text = IIf(drdr("Zip") Is DBNull.Value, "", drdr("Zip").ToString())
                If drdr("IsActive") = False Then
                    chkActive.Text = "No"
                Else
                    chkActive.Text = "Yes"
                End If
            End While
        End If
        cndr.Close()
        cndr = Nothing
    End Sub

    Private Sub dgvPhlebotomists_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPhlebotomists.CellContentClick
        If e.RowIndex <> -1 Then
            btnAccept.Enabled = True
            DisplayPhlebotomist(dgvPhlebotomists.Rows(e.RowIndex).Cells(0).Value)
        Else
            btnAccept.Enabled = False
        End If
    End Sub

    Private Sub btnAccept_Click(sender As Object, e As EventArgs) Handles btnAccept.Click
        Dim cndr As New SqlConnection(connString)
        cndr.Open()
        Dim cmddr As New SqlCommand("update " &
        " Routes set Phlebotomist_ID =" & txtID.Text & " where ID = " & RouteId, cndr)
        cmddr.CommandType = CommandType.Text
        Dim drdr As SqlDataReader = cmddr.ExecuteReader
        cndr.Close()
        cndr = Nothing
        Me.Close()
    End Sub
End Class