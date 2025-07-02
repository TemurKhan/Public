Option Compare Text
Imports System.Windows.Forms
Imports System.data

Public Class frmOrderLookUp

    Private Sub frmOrderLookUp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CN.Open("DSN=ProlisQC;UID=sa;PWD=''")
        Me.Tag = ""
        PopulateProviders()
        txtPatient.Text = ""
        'cmbProvider.SelectedIndex = 0
        cmbProvider.SelectedIndex = -1
        cmbProvider.ResetText()
        chkNonProvider.Checked = False
        txtDate.Text = ""
        dgvOrders.Rows.Clear()
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub PopulateProviders()
        cmbProvider.Items.Clear()
        Dim cnpr As New SqlClient.SqlConnection(connString)
        cnpr.Open()
        Dim cmdpr As New SqlClient.SqlCommand("Select * from Providers " &
        "where ID in (Select OrderingProvider_ID from Orders)", cnpr)
        cmdpr.CommandType = CommandType.Text
        Dim drpr As SqlClient.SqlDataReader = cmdpr.ExecuteReader
        If drpr.HasRows Then
            While drpr.Read
                If drpr("IsIndividual") IsNot DBNull.Value AndAlso drpr("IsIndividual") = 0 Then
                    cmbProvider.Items.Add(New MyList(drpr("LastName_BSN") &
                    " [" & drpr("ID").ToString & "]", drpr("ID")))
                Else
                    If drpr("Degree") Is DBNull.Value Then
                        cmbProvider.Items.Add(New MyList(drpr("LastName_BSN") & ", " &
                        drpr("FirstName") & " [" & drpr("ID").ToString & "]", drpr("ID")))
                    Else
                        cmbProvider.Items.Add(New MyList(drpr("LastName_BSN") &
                        ", " & drpr("FirstName") & " " & drpr("Degree") & " [" &
                        drpr("ID").ToString & "]", drpr("ID")))
                    End If
                End If
            End While
        End If
        cnpr.Close()
        cnpr = Nothing
    End Sub

    Private Sub UpdateSelection()
        If txtPatient.Text <> "" Or cmbProvider.SelectedIndex <> -1 Or
        chkNonProvider.Checked = True Or IsDate(txtDate.Text) Then
            btnSearch.Enabled = True
        Else
            btnSearch.Enabled = False
        End If
    End Sub
    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub txtDate_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDate.Validated
        If txtDate.Text <> "__/__/____" Then
            If Not IsDate(txtDate.Text) Then
                MsgBox("Invalid date", MsgBoxStyle.Critical, "Prolis")
                txtDate.Text = ""
            End If
        End If
        UpdateSelection()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Me.Cursor = Cursors.WaitCursor

        If txtPatient.Text <> "" Or cmbProvider.SelectedIndex <> -1 Or
        chkNonProvider.Checked = True Or IsDate(txtDate.Text) = True Then
            Dim sSQL As String = ""
            sSQL = "Select a.ID as OrderID, a.OrderDate as OrderDate, b.LastName " _
            & "as PatLName, b.FirstName as PatFName, b.DOB as DOB, b.Sex as Gender" _
            & ", c.IsIndividual as Individual, c.LastName_BSN as ProvLName, " &
            "c.FirstName as ProvFName, c.Degree as Degree from Providers c inner " &
            "join (Orders a inner join Patients b on a.Patient_ID = b.ID) on " _
            & "a.OrderingProvider_ID = c.ID"
            'Dim Rs As New ADODB.Recordset
            If chkNonProvider.Checked = True Then    'Non Patient
                sSQL = sSQL & " where b.ID = Null"
            Else    'Patient
                If txtPatient.Text <> "" Then
                    Dim LastName As String = ""
                    Dim FirstName As String = ""
                    Dim Name() As String
                    If InStr(txtPatient.Text, ",") <> 0 Then
                        Name = Split(txtPatient.Text, ",")
                        LastName = Trim(Name(0))
                        If InStr(LastName, "%") = 0 Then LastName += "%"
                        FirstName = Trim(Name(1))
                        If InStr(FirstName, "%") = 0 Then FirstName += "%"
                    Else
                        LastName = txtPatient.Text
                        If InStr(LastName, "%") = 0 Then LastName += "%"
                        FirstName = ""
                    End If
                    If FirstName = "" Then
                        If InStr(sSQL, "where") = 0 Then
                            sSQL = sSQL & " where a.Patient_ID in (Select ID from Patients " _
                            & "where LastName like '" & LastName & "')"
                        Else
                            sSQL = sSQL & " and a.Patient_ID in (Select ID from Patients " _
                            & "where LastName like '" & LastName & "')"
                        End If
                    Else
                        If InStr(sSQL, "where") = 0 Then
                            sSQL = sSQL & " where a.Patient_ID in (Select ID from Patients " _
                            & "where LastName like '" & LastName & "' and FirstName like '" _
                            & FirstName & "')"
                        Else
                            sSQL = sSQL & " and a.Patient_ID in (Select ID from Patients " _
                            & "where LastName like '" & LastName & "' and FirstName like '" _
                            & FirstName & "')"
                        End If
                    End If
                End If      'End of patient name process
            End If
            If cmbProvider.SelectedIndex <> -1 Then
                Dim ItemX As MyList = cmbProvider.SelectedItem
                If InStr(sSQL, "where") = 0 Then
                    sSQL = sSQL & " where a.OrderingProvider_ID = " & ItemX.ItemData
                Else
                    sSQL = sSQL & " and a.OrderingProvider_ID = " & ItemX.ItemData
                End If
            End If
            If IsDate(txtDate.Text) Then
                Dim FromDate As DateTime = CDate(txtDate.Text & " 00:00:00")
                Dim ToDate As DateTime = CDate(txtDate.Text & " 23:59:00")
                If InStr(sSQL, "where") = 0 Then
                    sSQL = sSQL & " where a.OrderDate between '" & Format(FromDate, SystemConfig.DateFormat) _
                    & "' and '" & Format(ToDate, "MM/dd/yyyy HH:mm:00") & "'"
                Else
                    sSQL = sSQL & " and a.OrderDate between '" & Format(FromDate, SystemConfig.DateFormat) _
                    & "' and '" & Format(ToDate, "MM/dd/yyyy HH:mm:00") & "'"
                End If
            End If
            '
            Dim ProvName As String = ""
            Dim PatName As String = ""
            Dim Instances() As Integer = {0, 0}
            dgvOrders.Rows.Clear()
            Dim cnpv As New SqlClient.SqlConnection(connString)
            cnpv.Open()
            Dim cmdpv As New SqlClient.SqlCommand(sSQL, cnpv)
            cmdpv.CommandType = CommandType.Text
            Dim drpv As SqlClient.SqlDataReader = cmdpv.ExecuteReader
            If drpv.HasRows Then
                While drpv.Read
                    Instances = UpdateInstances(drpv("OrderID"))
                    If drpv("Individual") = 0 Then
                        ProvName = drpv("ProvLName")
                    Else
                        If drpv("Degree") Is DBNull.Value Then
                            ProvName = drpv("ProvLName") & ", " &
                            drpv("ProvFName")
                        Else
                            ProvName = drpv("ProvLName") & ", " &
                            drpv("ProvFName") & " " & drpv("Degree")
                        End If
                    End If
                    '
                    If drpv("PatLName") IsNot DBNull.Value Then
                        dgvOrders.Rows.Add(drpv("OrderID"), Format(drpv("OrderDate"),
                        SystemConfig.DateFormat), Instances(0), Instances(1), ProvName,
                        drpv("PatLName") & ", " & drpv("PatFName"), drpv("Gender"),
                        Format(drpv("DOB"), SystemConfig.DateFormat))
                    Else
                        dgvOrders.Rows.Add(drpv("OrderID"), Format(drpv("OrderDate"),
                        SystemConfig.DateFormat), Instances(0),
                        Instances(1), ProvName, "*** Non Patient ***", "", "")
                    End If
                End While
            End If
            cnpv.Close()
            cnpv = Nothing
        End If

        Me.Cursor = Cursors.Default
    End Sub

    Private Function UpdateInstances(ByVal OrderID As Long) As Integer()
        Dim INST() As Integer = {0, 0}
        Dim InstCount As Integer = 0
        Dim ExecCount As Integer = 0
        Dim cnins As New SqlClient.SqlConnection(connString)
        cnins.Open()
        Dim cmdins As New SqlClient.SqlCommand("Select * " & _
        "from Order_Instance where Order_ID = " & OrderID, cnins)
        cmdins.CommandType = CommandType.Text
        Dim drins As SqlClient.SqlDataReader = cmdins.ExecuteReader
        If drins.HasRows Then
            While drins.Read
                InstCount += 1
                If drins("Executed") <> 0 Then ExecCount += 1
            End While
        End If
        cnins.Close()
        cnins = Nothing
        INST(0) = InstCount : INST(1) = ExecCount
        Return INST
    End Function

    Private Sub dgvOrders_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvOrders.CellClick
        If e.RowIndex <> -1 Then
            Me.Tag = CStr(dgvOrders.Rows(e.RowIndex).Cells(0).Value)
            btnAccept.Enabled = True
        End If
    End Sub

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click, btn_Cancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub chkNonProvider_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNonProvider.CheckedChanged
        If chkNonProvider.Checked = True Then
            cmbProvider.SelectedIndex = -1
            cmbProvider.Enabled = False
        Else
            cmbProvider.Enabled = True
        End If
        UpdateSelection()
    End Sub

    Private Sub cmbProvider_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProvider.SelectedIndexChanged
        UpdateSelection()
    End Sub

    Private Sub txtPatient_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatient.Validated
        UpdateSelection()
    End Sub

    Private Sub txt_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPatient.KeyDown
        If e.KeyCode = Keys.Down AndAlso dgvOrders.Rows.Count > 0 Then
            dgvOrders.Focus()
        End If
    End Sub

    Private Sub dgvTests_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvOrders.CellDoubleClick
        Call btnAccept_Click(Nothing, Nothing)
    End Sub

    Private Sub dgvTests_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvOrders.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            If dgvOrders.CurrentRow IsNot Nothing Then

                Dim rowIndex As Integer = dgvOrders.CurrentRow.Index

                Me.Tag = dgvOrders.Rows(rowIndex).Cells(0).Value.ToString

                Call btnAccept_Click(Nothing, Nothing)
                Me.txtPatient.Focus()
            End If
        End If
    End Sub

End Class
