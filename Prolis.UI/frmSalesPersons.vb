Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmSalesPersons

    Private Sub chkEditNew_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkEditNew.CheckedChanged
        If chkEditNew.Checked = False Then
            chkEditNew.Text = "Edit"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Edit.ico")
            ClearForm()
            dgvSales.Enabled = True
            btnDelete.Enabled = True
        Else
            chkEditNew.Text = "New"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\New.ico")
            ClearForm()
            txtID.Text = NextsalesID()
            dgvSales.Enabled = False
            btnDelete.Enabled = False
        End If
    End Sub

    Private Sub ClearForm()
        txtID.Text = ""
        txtName.Text = ""
        txtSSN.Text = ""
        chkActive.Checked = True
        txtEmail.Text = ""
        txtHPhone.Text = ""
        txtCell.Text = ""
        txtStart.Text = ""
        txtEnd.Text = ""
        txtUID.Text = ""
        txtPWD.Text = ""
        txtAdd1.Text = ""
        txtAdd2.Text = ""
        txtCity.Text = ""
        txtState.Text = ""
        txtZip.Text = ""
        txtCountry.Text = ""
        dgvClients.Rows.Clear()
        dgvClients.Rows.Add()
    End Sub

    Private Function NextsalesID() As Integer
        Dim NID As Long = 1
        Dim cnsid As New SqlConnection(connString)
        cnsid.Open()
        Dim cmdsid As New SqlCommand("Select " &
        "max(ID) as LastID from SalesPersons", cnsid)
        cmdsid.CommandType = CommandType.Text
        Dim drsid As SqlDataReader = cmdsid.ExecuteReader
        If drsid.HasRows Then
            While drsid.Read
                If drsid("LastID") IsNot DBNull.Value _
                Then NID = drsid("LastID") + 1
            End While
        End If
        cnsid.Close()
        cnsid = Nothing
        Return NID
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtID.Text <> "" And txtName.Text <> "" Then
            SaveSalesPerson(Val(txtID.Text))
            PopulateSalesPersons()
            ClearForm()
            If chkEditNew.Checked = True Then
                txtID.Text = NextsalesID()
                txtName.Focus()
            End If
        End If
    End Sub

    Private Sub SaveSalesPerson(ByVal SalesID As Integer)
        Dim LName As String = ""
        Dim FName As String = ""
        If Trim(txtName.Text).Contains(",") Then
            Dim Names() As String = Split(Trim(txtName.Text), ",")
            LName = Trim(Names(0))
            FName = Trim(Names(1))
        Else
            LName = Trim(txtName.Text)
            FName = ""
        End If
        Dim cnsp As New SqlConnection(connString)
        cnsp.Open()
        Dim cmdupsert As New SqlCommand("SalesPersons_SP", cnsp)
        cmdupsert.CommandType = CommandType.StoredProcedure
        cmdupsert.Parameters.AddWithValue("@act", "Upsert")
        cmdupsert.Parameters.AddWithValue("@ID", SalesID)
        cmdupsert.Parameters.AddWithValue("@LastName", LName)
        cmdupsert.Parameters.AddWithValue("@FirstName", FName)
        cmdupsert.Parameters.AddWithValue("@MiddleName", "")
        cmdupsert.Parameters.AddWithValue("@FullName", txtName.Text)
        cmdupsert.Parameters.AddWithValue("@Email", Trim(txtEmail.Text))
        If Trim(txtUID.Text) <> "" Then
            cmdupsert.Parameters.AddWithValue("@User_ID", Trim(txtUID.Text))
            cmdupsert.Parameters.AddWithValue("@Password", LIC.encryptString(Trim(txtPWD.Text)))
        End If
        cmdupsert.Parameters.AddWithValue("@SSN", SSNNeat(txtSSN.Text))
        cmdupsert.Parameters.AddWithValue("@HomePhone", PhoneNeat(txtHPhone.Text))
        cmdupsert.Parameters.AddWithValue("@Cell", PhoneNeat(txtCell.Text))
        cmdupsert.Parameters.AddWithValue("@IsActive", chkActive.Checked)
        If IsDate(txtStart.Text) Then _
        cmdupsert.Parameters.AddWithValue("@StartDate", CDate(txtStart.Text))
        If IsDate(txtEnd.Text) Then _
        cmdupsert.Parameters.AddWithValue("@EndDate", CDate(txtEnd.Text))
        If txtAdd1.Text <> "" And txtCity.Text <> "" And
        txtState.Text <> "" And txtZip.Text <> "" Then
            cmdupsert.Parameters.AddWithValue("@Address_ID", GetAddressID(txtAdd1.Text,
            txtAdd2.Text, txtCity.Text, txtState.Text, txtZip.Text, txtCountry.Text))
        End If
        Try
            cmdupsert.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cnsp.Close()
            cnsp = Nothing
        End Try
        Dim GoodProvIDs As String = ""
        For i As Integer = 0 To dgvClients.RowCount - 1
            If dgvClients.Rows(i).Cells(0).Value IsNot Nothing _
            AndAlso Trim(dgvClients.Rows(i).Cells(0).Value) <> "" Then
                ExecuteSqlProcedure("Update Providers set SalesPerson_ID = " &
                SalesID & " where ID = " & dgvClients.Rows(i).Cells(0).Value)
                GoodProvIDs += dgvClients.Rows(i).Cells(0).Value.ToString & ", "
            End If
        Next
        If GoodProvIDs.EndsWith(", ") Then GoodProvIDs = Microsoft.VisualBasic.Mid(GoodProvIDs, 1, Len(GoodProvIDs) - 2)
        ExecuteSqlProcedure("Update Providers set SalesPerson_ID = 0 where " &
        "SalesPerson_ID = " & SalesID & " and not ID in (" & GoodProvIDs & ")")
    End Sub


    Private Sub PopulateSalesPersons()
        Dim SSN As String = ""
        Dim Address As String = ""
        Dim Email As String = ""

        dgvSales.Rows.Clear()

        Using cnps As New SqlConnection(connString)
            cnps.Open()

            Dim query As String = "SELECT * FROM SalesPersons"
            Using cmdps As New SqlCommand(query, cnps)
                Using drps As SqlDataReader = cmdps.ExecuteReader()
                    If drps.HasRows Then
                        While drps.Read()
                            SSN = If(drps("SSN") IsNot DBNull.Value,
                                 $"{drps("SSN").ToString.Substring(0, 3)}-{drps("SSN").ToString.Substring(3, 2)}-{drps("SSN").ToString.Substring(5)}",
                                 "")

                            Address = If(drps("Address_ID") IsNot DBNull.Value, GetAddress(drps("Address_ID")), "")
                            Email = If(drps("Email") IsNot DBNull.Value, drps("Email").ToString(), "")

                            dgvSales.Rows.Add(drps("ID"), drps("FullName"), drps("IsActive"), Email, Address)
                        End While
                    End If
                End Using
            End Using
        End Using
    End Sub
    Private Function IsDuplicate(ByVal Material As String) As Boolean
        Dim i As Integer
        Dim Duplicate As Boolean = False
        For i = 0 To dgvSales.RowCount - 1
            If Material = dgvSales.Rows(i).Cells(1).Value Then
                Duplicate = True
                Exit For
            End If
        Next
        IsDuplicate = Duplicate
    End Function

    Private Sub frmSalesPersons_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dgvClients.RowCount = 1
        'CN.Open("DSN=ProlisQC")
        PopulateSalesPersons()
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub dgvSales_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSales.CellDoubleClick
        If e.RowIndex <> -1 Then _
        DislaySalesRecord(dgvSales.Rows(e.RowIndex).Cells(0).Value)
    End Sub

    Private Sub DislaySalesRecord(ByVal SalesID As Long)
        Dim cnsr As New SqlConnection(connString)
        cnsr.Open()
        Dim cmdsr As New SqlCommand("Select * from SalesPersons where ID = " & SalesID, cnsr)
        cmdsr.CommandType = Data.CommandType.Text
        Dim drsr As SqlDataReader = cmdsr.ExecuteReader
        If drsr.HasRows Then
            While drsr.Read
                txtID.Text = drsr("ID")
                txtName.Text = drsr("FullName")
                chkActive.Checked = drsr("IsActive")
                If drsr("SSN") IsNot DBNull.Value Then txtSSN.Text = drsr("SSN")
                If drsr("HomePhone") IsNot DBNull.Value Then txtHPhone.Text = drsr("HomePhone")
                If drsr("Cell") IsNot DBNull.Value Then txtCell.Text = Trim(drsr("Cell"))
                If drsr("StartDate") IsNot DBNull.Value Then _
                txtStart.Text = Format(drsr("StartDate"), SystemConfig.DateFormat)
                If drsr("EndDate") IsNot DBNull.Value Then _
                txtEnd.Text = Format(drsr("EndDate"), SystemConfig.DateFormat)
                If drsr("User_ID") IsNot DBNull.Value Then txtUID.Text = Trim(drsr("User_ID"))
                If drsr("Password") IsNot DBNull.Value Then txtPWD.Text = drsr("Password")
                If drsr("Address_ID") IsNot DBNull.Value Then
                    txtAdd1.Text = GetAddress1(drsr("Address_ID"))
                    txtAdd2.Text = GetAddress2(drsr("Address_ID"))
                    txtCity.Text = GetAddressCity(drsr("Address_ID"))
                    txtState.Text = GetAddressState(drsr("Address_ID"))
                    txtZip.Text = GetAddressZip(drsr("Address_ID"))
                    txtCountry.Text = GetAddressCountry(drsr("Address_ID"))
                End If
                If drsr("Email") IsNot DBNull.Value Then txtEmail.Text = drsr("Email")
            End While
        End If
        cnsr.Close()
        cnsr = Nothing
        '
        dgvClients.Rows.Clear()
        Dim Client As String = ""
        Dim Phone As String = ""
        Dim ClientAddress As String = ""
        Dim cndc As New SqlConnection(connString)
        cndc.Open()
        Dim cmddc As New SqlCommand("Select * from Providers where SalesPerson_ID = " & SalesID, cndc)
        cmddc.CommandType = CommandType.Text
        Dim drdc As SqlDataReader = cmddc.ExecuteReader
        If drdc.HasRows Then
            While drdc.Read
                If drdc("IsIndividual") = False Then
                    Client = drdc("LastName_BSN")
                Else
                    If drdc("Degree") IsNot DBNull.Value Then
                        Client = drdc("LastName_BSN") & ", " & drdc("FirstName") & drdc("Degree")
                    Else
                        Client = drdc("LastName_BSN") & ", " & drdc("FirstName") &
                        IIf(drdc("MiddleName") IsNot DBNull.Value, " " &
                        drdc("MiddleName").ToString.Substring(0, 1), "")
                    End If
                End If
                If drdc("Phone") IsNot DBNull.Value AndAlso Trim(drdc("Phone")) <> "" Then
                    Phone = "(" & Trim(drdc("Phone")).Substring(0, 3) & ") " &
                    Trim(drdc("Phone")).Substring(3, 3) & "-" & Trim(drdc("Phone")).Substring(6)
                Else : Phone = ""
                End If
                If drdc("Address_ID") IsNot DBNull.Value Then
                    ClientAddress = GetAddress(drdc("Address_ID"))
                Else
                    ClientAddress = ""
                End If
                dgvClients.Rows.Add(drdc("ID"), Nothing, Client, Phone, ClientAddress)
            End While
            lblClients.Text = "Clients [ " & dgvClients.RowCount & " ]"
        End If
        dgvClients.Rows.Add()
        cndc.Close()
        cndc = Nothing
    End Sub

    Private Sub chkActive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkActive.CheckedChanged
        If chkActive.Checked = True Then
            chkActive.Text = "Yes"
        Else
            chkActive.Text = "No"
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If txtID.Text <> "" And txtName.Text <> "" Then
            Dim RetVal As Integer
            RetVal = MsgBox("Prolis archtecht strongly recommends not to delete " _
            & "any record from PROLIS which may result in an unstable system." _
            & " In Prolis, records can be marked inactive if option provided." _
            & " Any record marked inactive, is similar to a deleted record. " _
            & " Are you sure you want to delete this record?", MsgBoxStyle.Question _
            + MsgBoxStyle.YesNo, "Prolis")
            If RetVal = vbYes Then
                ExecuteSqlProcedure("Delete from SalesPersons where ID = " & Val(txtID.Text))
                PopulateSalesPersons()
                ClearForm()
            End If
        End If
    End Sub

    Private Sub dgvClients_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvClients.CellContentClick
        If e.ColumnIndex = 1 Then  'lookup
            Dim ProviderID As String = frmProviderLookup.ShowDialog
            If (ProviderID <> "" AndAlso Not ClientListed(ProviderID)) And txtID.Text <> "" Then
                Dim Client As String = ""
                Dim Phone As String = ""
                Dim CAddress As String = ""
                Dim cndp As New SqlConnection(connString)
                cndp.Open()
                Dim cmddp As New SqlCommand("Select * from Providers where ID = " & ProviderID, cndp)
                cmddp.CommandType = CommandType.Text
                Dim drdp As SqlDataReader = cmddp.ExecuteReader
                If drdp.HasRows Then
                    While drdp.Read
                        If drdp("IsIndividual") = False Then
                            Client = drdp("LastName_BSN")
                        Else
                            If drdp("Degree") IsNot DBNull.Value Then
                                Client = drdp("LastName_BSN") & ", " & drdp("FirstName") & drdp("Degree")
                            Else
                                Client = drdp("LastName_BSN") & ", " & drdp("FirstName") &
                                IIf(drdp("MiddleName") IsNot DBNull.Value, " " &
                                drdp("MiddleName").ToString.Substring(0, 1), "")
                            End If
                        End If
                        If drdp("Phone") IsNot DBNull.Value AndAlso Trim(drdp("Phone")) <> "" Then
                            Phone = "(" & Trim(drdp("Phone")).Substring(0, 3) & ") " &
                            Trim(drdp("Phone")).Substring(3, 3) & "-" & Trim(drdp("Phone")).Substring(6)
                        Else : Phone = ""
                        End If
                        If drdp("Address_ID") IsNot DBNull.Value Then
                            CAddress = GetAddress(drdp("Address_ID"))
                        Else
                            CAddress = ""
                        End If
                        dgvClients.Rows.Insert(0, drdp("ID"), Nothing, Client, Phone, CAddress)
                        If e.RowIndex = dgvClients.RowCount - 1 Then dgvClients.Rows.Add()
                    End While
                    lblClients.Text = "Clients [ " & dgvClients.RowCount & " ]"
                End If
                cndp.Close()
                cndp = Nothing
            End If
        End If
    End Sub

    Private Function ClientListed(ByVal ProviderID As Long) As Boolean
        Dim Has As Boolean = False
        For i As Integer = 0 To dgvClients.RowCount - 1
            If dgvClients.Rows(i).Cells(0).Value = ProviderID.ToString Then
                Has = True
                Exit For
            End If
        Next
        Return Has
    End Function

    Private Function Duplicate(ByVal ProviderID As String, ByVal RowID As Integer) As Boolean
        Dim Dup As Boolean = False
        For i As Integer = 0 To dgvClients.RowCount - 1
            If dgvClients.Rows(i).Cells(0).Value = ProviderID And i <> RowID Then
                Dup = True
                Exit For
            End If
        Next
        Return Dup
    End Function

    Private Sub dgvClients_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvClients.CellEndEdit
        If e.ColumnIndex = 0 Then   'ClientID
            If dgvClients.Rows(e.RowIndex).Cells(0).Value IsNot Nothing _
            AndAlso Trim(dgvClients.Rows(e.RowIndex).Cells(0).Value) <> "" Then
                Dim ProviderID As String = Trim(dgvClients.Rows(e.RowIndex).Cells(0).Value)
                If (ProviderID <> "" AndAlso Not Duplicate(ProviderID, e.RowIndex)) And txtID.Text <> "" Then
                    Dim Client As String = ""
                    Dim Phone As String = ""
                    Dim CAddress As String = ""
                    Dim cndp As New SqlConnection(connString)
                    cndp.Open()
                    Dim cmddp As New SqlCommand("Select * from Providers where ID = " & ProviderID, cndp)
                    cmddp.CommandType = CommandType.Text
                    Dim drdp As SqlDataReader = cmddp.ExecuteReader
                    If drdp.HasRows Then
                        While drdp.Read
                            If drdp("IsIndividual") = False Then
                                Client = drdp("LastName_BSN")
                            Else
                                If drdp("Degree") IsNot DBNull.Value Then
                                    Client = drdp("LastName_BSN") & ", " & drdp("FirstName") & drdp("Degree")
                                Else
                                    Client = drdp("LastName_BSN") & ", " & drdp("FirstName") & _
                                    IIf(drdp("MiddleName") IsNot DBNull.Value, " " & _
                                    drdp("MiddleName").ToString.Substring(0, 1), "")
                                End If
                            End If
                            If drdp("Phone") IsNot DBNull.Value AndAlso Trim(drdp("Phone")) <> "" Then
                                Phone = "(" & Trim(drdp("Phone")).Substring(0, 3) & ") " & _
                                Trim(drdp("Phone")).Substring(3, 3) & "-" & Trim(drdp("Phone")).Substring(6)
                            Else : Phone = ""
                            End If
                            If drdp("Address_ID") IsNot DBNull.Value Then
                                CAddress = GetAddress(drdp("Address_ID"))
                            Else
                                CAddress = ""
                            End If
                            dgvClients.Rows(e.RowIndex).Cells(2).Value = Client
                            dgvClients.Rows(e.RowIndex).Cells(3).Value = Phone
                            dgvClients.Rows(e.RowIndex).Cells(4).Value = CAddress
                            If e.RowIndex = dgvClients.RowCount - 1 Then dgvClients.Rows.Add()
                        End While
                        lblClients.Text = "Clients [ " & dgvClients.RowCount & " ]"
                    Else
                        MsgBox("Not a valid Client ID.")
                        dgvClients.Rows(e.RowIndex).Cells(0).Value = ""
                    End If
                    cndp.Close()
                    cndp = Nothing
                End If
            Else
                dgvClients.Rows(e.RowIndex).Cells(2).Value = ""
                dgvClients.Rows(e.RowIndex).Cells(3).Value = ""
                dgvClients.Rows(e.RowIndex).Cells(4).Value = ""
            End If
        End If
    End Sub
End Class
