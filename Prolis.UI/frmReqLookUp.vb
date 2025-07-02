Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmReqLookUp

    Private Sub frmReqLookUp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CN.Open("DSN=ProlisQC;UID=sa;PWD=''")
        Me.Tag = ""
        PopulateProviders()
        txtPatient.Text = ""
        cmbProvider.SelectedIndex = -1
        chkNonPatient.Checked = False
        txtDate.Text = ""
        dgv.Rows.Clear()
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub PopulateProviders()
        cmbProvider.Items.Clear()
        Dim cnpp As New SqlConnection(connString)
        cnpp.Open()
        Dim cmdpp As New SqlCommand("Select * from Providers", cnpp)
        cmdpp.CommandType = CommandType.Text
        Dim drpp As SqlDataReader = cmdpp.ExecuteReader
        If drpp.HasRows Then
            While drpp.Read
                If drpp("IsIndividual") IsNot DBNull.Value AndAlso drpp("IsIndividual") = 0 Then
                    cmbProvider.Items.Add(New MyList(drpp("LastName_BSN"), drpp("ID")))
                Else
                    If drpp("Degree") Is DBNull.Value Then
                        cmbProvider.Items.Add(New MyList(drpp("LastName_BSN") _
                        & ", " & drpp("FirstName"), drpp("ID")))
                    Else
                        cmbProvider.Items.Add(New MyList(drpp("LastName_BSN") &
                        ", " & drpp("FirstName") & " " & drpp("Degree"), drpp("ID")))
                    End If
                End If
            End While
        End If
        cnpp.Close()
        cnpp = Nothing
    End Sub

    Private Sub UpdateSelection()
        If txtPatient.Text <> "" Or cmbProvider.SelectedIndex <> -1 Or
        chkNonPatient.Checked = True Or IsDate(txtDate.Text) Then
            btnAccSearch.Enabled = True
        Else
            btnAccSearch.Enabled = False
        End If
    End Sub
    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function
    Private Sub txtPatient_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatient.Validated
        UpdateSelection()
    End Sub

    Private Sub cmbProvider_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProvider.SelectedIndexChanged
        UpdateSelection()
    End Sub

    Private Sub chkNonPatient_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNonPatient.CheckedChanged
        If chkNonPatient.Checked = True Then
            txtPatient.Text = ""
            txtPatient.Enabled = False
        Else
            txtPatient.Enabled = True
        End If
        UpdateSelection()
    End Sub

    Private Sub chkNonPatient_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkNonPatient.Validated
        UpdateSelection()
    End Sub

    Private Sub txtDate_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDate.Validated
        If txtDate.Text <> "__/__/____" Then
            If Not IsDate(txtDate.Text) Then
                MsgBox("Invalid date", MsgBoxStyle.Critical, "Prolis")
                txtDate.Text = ""
            End If
        End If
        UpdateSelection()
    End Sub

    Private Sub btnAccSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccSearch.Click
        Me.Cursor = Cursors.WaitCursor

        If txtPatient.Text <> "" Or cmbProvider.SelectedIndex <> -1 Or
        chkNonPatient.Checked = True Or IsDate(txtDate.Text) = True Then
            Dim sSQL As String = ""
            sSQL = "Select a.ID as AccID, a.AccessionDate as AccDate, " &
            "a.BillingType_ID as BillType, b.LastName as PatLName, b.FirstName " &
            "as PatFName, b.DOB as DOB, b.Sex as Gender, c.IsIndividual as " &
            "Individual, c.LastName_BSN as ProvLName, c.FirstName as ProvFName, " &
            "c.Degree as Degree from Providers c inner join (Requisitions a left " &
            "outer join Patients b on a.Patient_ID = b.ID) on " _
            & "a.OrderingProvider_ID = c.ID where a.Received <> 0"
            '
            If chkNonPatient.Checked = True Then    'Non Patient
                sSQL = sSQL & " and b.ID = Null"
            Else    'Patient

                If txtPatient.Text <> "" Then
                    Dim LastName As String = ""
                    Dim FirstName As String = ""
                    Dim Name() As String
                    If InStr(txtPatient.Text, ",") <> 0 Then
                        Name = Split(txtPatient.Text, ",")
                        LastName = Name(0)
                        LastName = Replace(LastName, "*", "%")
                        FirstName = Name(1)
                        FirstName = Replace(FirstName, "*", "%")
                    Else
                        LastName = txtPatient.Text
                        'LastName = Replace(LastName, "*", "%")
                    End If
                    If FirstName = "" Then
                        sSQL = sSQL & " and a.Patient_ID in (Select ID from Patients " _
                        & "where LastName like '" & LastName & "%')"
                    Else
                        sSQL = sSQL & " and a.Patient_ID in (Select ID from Patients " _
                        & "where LastName like '" & LastName & "%' and FirstName like '" _
                        & FirstName & "%')"
                    End If
                End If      'End of patient name process
            End If
            If cmbProvider.SelectedIndex <> -1 Then
                Dim ItemX As MyList = cmbProvider.SelectedItem
                sSQL = sSQL & " and a.OrderingProvider_ID = " & ItemX.ItemData
            End If
            If IsDate(txtDate.Text) Then
                Dim FromDate As DateTime = CDate(txtDate.Text & " 00:00:00")
                Dim ToDate As DateTime = CDate(txtDate.Text & " 23:59:00")
                sSQL = sSQL & " and a.AccessionDate between '" & Format(FromDate, SystemConfig.DateFormat) _
                & "' and '" & Format(ToDate, SystemConfig.DateFormat & " HH:mm:00") & "'"
            End If
            '
            Dim ProvName As String = ""
            Dim PatName As String = ""
            Dim Payer As String = ""
            dgv.Rows.Clear()
            Dim cnpp As New SqlConnection(connString)
            cnpp.Open()
            Dim cmdpp As New SqlCommand(sSQL, cnpp)
            cmdpp.CommandType = CommandType.Text
            Dim drpp As SqlDataReader = cmdpp.ExecuteReader
            If drpp.HasRows Then
                While drpp.Read
                    If drpp("Individual") = 0 Then
                        ProvName = drpp("ProvLName")
                    Else
                        If drpp("Degree") Is DBNull.Value Then
                            ProvName = drpp("ProvLName") & ", " & drpp("ProvFName")
                        Else
                            ProvName = drpp("ProvLName") & ", " &
                            drpp("ProvFName") & " " & drpp("Degree")
                        End If
                    End If
                    '
                    If drpp("BillType") = 0 Then
                        Payer = "Client"
                    ElseIf drpp("BillType") = 2 Then
                        Payer = "Patient"
                    Else
                        Payer = GetPayerName(drpp("AccID"))
                    End If
                    If drpp("PatLName") IsNot DBNull.Value Then
                        dgv.Rows.Add(drpp("AccID"), Format(drpp("AccDate"),
                        SystemConfig.DateFormat), ProvName, drpp("PatLName") & ", " &
                        drpp("PatFName"), drpp("Gender"), Format(drpp("DOB"), SystemConfig.DateFormat), Payer)
                    Else
                        dgv.Rows.Add(drpp("AccID"), Format(drpp("AccDate"), SystemConfig.DateFormat),
                        ProvName, "*** Non Patient ***", "", "", Payer)
                    End If
                End While
            End If
            cnpp.Close()
            cnpp = Nothing
        End If

        Me.Cursor = Cursors.Default
    End Sub

    Private Function GetPayerName(ByVal AccID As Long) As String
        Dim Payer As String = ""
        Dim cnpn As New SqlConnection(connString)
        cnpn.Open()
        Dim cmdpn As New SqlCommand("Select PayerName from " &
        "Payers where ID in (Select Payer_ID from Req_Coverage where " &
        "Preference = 'P' and Accession_ID = " & AccID & ")", cnpn)
        cmdpn.CommandType = CommandType.Text
        Dim drpn As SqlDataReader = cmdpn.ExecuteReader
        If drpn.HasRows Then
            While drpn.Read
                Payer = drpn("PayerName")
            End While
        End If
        cnpn.Close()
        cnpn = Nothing
        Return Payer
    End Function

    Private Sub dgvAccessions_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellClick
        If e.RowIndex <> -1 Then
            Me.Tag = CStr(dgv.Rows(e.RowIndex).Cells(0).Value)
            btnAccept.Enabled = True
        End If
    End Sub

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        If dgv.SelectedRows.Count > 0 Then

            Me.Tag = dgv.SelectedRows(0).Cells(0).Value
        End If
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click, btn_Cancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub txtPatient_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPatient.KeyDown
        If e.KeyCode = Keys.Down AndAlso dgv.Rows.Count > 0 Then
            dgv.Focus()
        End If
    End Sub

    Private Sub dgvTests_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellDoubleClick
        Call btnAccept_Click(sender, e)
    End Sub

    Private Sub dgv_KeyDown(sender As Object, e As KeyEventArgs) Handles dgv.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Call btnAccept_Click(sender, e)
            Me.txtPatient.Focus()
        End If
    End Sub
End Class
