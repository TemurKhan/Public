Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmResPatLookUp
    Private Sub cmbSearch_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If cmbSearch.SelectedIndex = 0 Then
            Label1.Text = "Name (Last, First) even partial"
            txtSearch.Mask = ""
        ElseIf cmbSearch.SelectedIndex = 1 Then
            Label1.Text = "Social Security #(No Space or dash)"
            txtSearch.Mask = "###-##-####"
        Else
            Label1.Text = "EMR# (Complete)"
            txtSearch.Mask = ""
        End If
        txtSearch.Text = ""
    End Sub

    Private Sub frmResPatLookUp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbSearch.SelectedIndex = 0
        Me.Tag = ""
        dgv.Rows.Clear()
        'PopulateProviders()
        If frmResInq.txtName.Text <> "" Then
            txtSearch.Text = frmResInq.txtName.Text
            btnPatSearch_Click(Nothing, Nothing)
        End If
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)

        txtProviderID.Focus()
    End Sub

    Private Sub btnPatSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatSearch.Click
        Me.Cursor = Cursors.WaitCursor

        Dim sSQL As String = ""
        If IsDate(txtFrom.Text) And Not IsDate(txtTo.Text) Then  'From DOS
            sSQL = "Select Distinct a.*, b.ID as Accession, c.SourceDate as DOS, b.EMRNo as MRN, " &
            "b.RequisitionNo as ReqID, b.OrderingProvider_ID as ProviderID from Patients a inner join " &
            "(Requisitions b inner join Specimens c on b.ID = c.Accession_ID) on a.ID = b.Patient_ID " &
            "where b.Received <> 0 and c.SourceDate between '" & Format(CDate(txtFrom.Text),
            SystemConfig.DateFormat) & "' and '" & Format(CDate(txtFrom.Text), SystemConfig.DateFormat) & " 23:59:00'"
        ElseIf IsDate(txtFrom.Text) And IsDate(txtTo.Text) Then 'from + To
            sSQL = "Select Distinct a.*, b.ID as Accession, c.SourceDate as DOS, b.EMRNo as MRN, " &
            "b.RequisitionNo as ReqID, b.OrderingProvider_ID as ProviderID from Patients a inner join " &
            "(Requisitions b inner join Specimens c on b.ID = c.Accession_ID) on a.ID = b.Patient_ID " &
            "where b.Received <> 0 and c.SourceDate between '" & Format(CDate(txtFrom.Text),
            SystemConfig.DateFormat) & "' and '" & Format(CDate(txtTo.Text), SystemConfig.DateFormat) & " 23:59:00'"
        ElseIf Not IsDate(txtFrom.Text) And IsDate(txtTo.Text) Then  'To
            sSQL = "Select Distinct a.*, b.ID as Accession, c.SourceDate as DOS, b.EMRNo as MRN, " &
            "b.RequisitionNo as ReqID, b.OrderingProvider_ID as ProviderID from Patients a inner join " &
            "(Requisitions b inner join Specimens c on b.ID = c.Accession_ID) on a.ID = b.Patient_ID " &
            "where b.Received <> 0 and c.SourceDate between '" & Format(CDate(txtTo.Text),
            SystemConfig.DateFormat) & "' and '" & Format(CDate(txtTo.Text), SystemConfig.DateFormat) & " 23:59:00'"
        End If
        '
        If txtProviderID.Text <> "" Then    'Doctor selected
            If sSQL = "" Then
                sSQL = "Select Distinct a.*, b.ID as Accession, c.SourceDate as DOS, " &
                "b.EMRNo as MRN, b.RequisitionNo as ReqID, b.OrderingProvider_ID as ProviderID " &
                "from Patients a inner join (Requisitions b inner join Specimens c on b.ID = " &
                "c.Accession_ID) on a.ID = b.Patient_ID where b.Received <> 0 and " &
                "b.OrderingProvider_ID = " & Val(txtProviderID.Text)
            Else
                sSQL = sSQL & " and b.OrderingProvider_ID = " & Val(txtProviderID.Text)
            End If
        End If
        '
        If txtSearch.Text <> "" Then
            If cmbSearch.SelectedIndex = 0 Then 'Patient Name
                Dim LastName As String = ""
                Dim FirstName As String = ""
                Dim Name() As String
                If InStr(txtSearch.Text, ",") <> 0 Then
                    Name = Split(txtSearch.Text, ",")
                    LastName = Name(0)
                    LastName = Trim(LastName) & "%"
                    FirstName = Name(1)
                    FirstName = Trim(FirstName) & "%"
                Else
                    LastName = Trim(txtSearch.Text) & "%"
                End If
                '
                If FirstName = "" Then
                    If sSQL = "" Then
                        sSQL = "Select Distinct a.*, b.ID as Accession, c.SourceDate as DOS, " &
                        "b.EMRNo as MRN, b.RequisitionNo as ReqID, b.OrderingProvider_ID as ProviderID " &
                        "from Patients a inner join (Requisitions b inner join Specimens c on b.ID = " &
                        "c.Accession_ID) on a.ID = b.Patient_ID where b.Received <> 0 and " &
                        "a.LastName Like '" & LastName & "%'"
                    Else
                        sSQL = sSQL & " and a.lastname like '" & LastName & "%'"
                    End If
                Else
                    If sSQL = "" Then
                        sSQL = "Select Distinct a.*, b.ID as Accession, c.SourceDate as DOS, " &
                        "b.EMRNo as MRN, b.RequisitionNo as ReqID, b.OrderingProvider_ID as ProviderID " &
                        "from Patients a inner join (Requisitions b inner join Specimens c on b.ID = " &
                        "c.Accession_ID) on a.ID = b.Patient_ID where b.Received <> 0 and " &
                        "a.LastName Like '" & LastName & "%' and a.FirstName Like '" & FirstName & "%'"
                    Else
                        sSQL = sSQL & " and a.lastname like '" & LastName & "%' and a.FirstName Like '" _
                        & FirstName & "%'"
                    End If
                End If
            ElseIf cmbSearch.SelectedIndex = 0 Then 'By SSN
                Dim SSN As String = SSNNeat(txtSearch.Text)
                If sSQL = "" Then
                    sSQL = "Select Distinct a.*, b.ID as Accession, c.SourceDate as DOS, " &
                    "b.EMRNo as MRN, b.RequisitionNo as ReqID, b.OrderingProvider_ID as " &
                    "ProviderID from Patients a inner join (Requisitions b inner join " &
                    "Specimens c on b.ID = c.Accession_ID) on a.ID = b.Patient_ID where " &
                    "b.Received <> 0 and a.SSN = '" & SSN & "'"
                Else
                    sSQL = sSQL & " and a.SSN = '" & SSN & "'"
                End If
            Else    'EMR
                If sSQL = "" Then
                    sSQL = "Select Distinct a.*, b.ID as Accession, c.SourceDate as DOS, " &
                    "b.EMRNo as MRN, b.RequisitionNo as ReqID, b.OrderingProvider_ID as " &
                    "ProviderID from Patients a inner join (Requisitions b inner join " &
                    "Specimens c on b.ID = c.Accession_ID) on a.ID = b.Patient_ID where " &
                    "b.Received <> 0 and b.EMRNo = '" & Trim(txtSearch.Text) & "'"
                Else
                    sSQL = sSQL & " and b.emrno = '" & Trim(txtSearch.Text) & "'"
                End If
            End If
        End If
        '
        If IsDate(txtDOB.Text) Then
            If sSQL = "" Then
                sSQL = "Select Distinct a.*, b.ID as Accession, c.SourceDate as DOS, " &
                "b.EMRNo as MRN, b.RequisitionNo as ReqID, b.OrderingProvider_ID as " &
                "ProviderID from Patients a inner join (Requisitions b inner join " &
                "Specimens c on b.ID = c.Accession_ID) on a.ID = b.Patient_ID where " &
                "b.Received <> 0 and a.DOB = '" & txtDOB.Text & "'"
            Else
                sSQL = sSQL & " and a.DOB = '" & txtDOB.Text & "'"
            End If
        End If
        '
        dgv.Rows.Clear()
        If sSQL <> "" Then
            Dim MRN As String = ""
            Dim ReqID As String = ""
            Dim cnpat As New SqlConnection(connString)
            cnpat.Open()
            Dim cmdpat As New SqlCommand(sSQL, cnpat)
            cmdpat.CommandType = CommandType.Text
            Dim drpat As SqlDataReader = cmdpat.ExecuteReader
            If drpat.HasRows Then
                While drpat.Read
                    If drpat("MRN") IsNot DBNull.Value _
                    AndAlso drpat("MRN") <> "" Then
                        MRN = drpat("MRN")
                    Else
                        MRN = ""
                    End If
                    If drpat("ReqID") IsNot DBNull.Value _
                    AndAlso drpat("ReqID") <> "" Then
                        ReqID = drpat("ReqID")
                    Else
                        ReqID = ""
                    End If
                    dgv.Rows.Add(drpat("ID"),
                        drpat("LastName"), drpat("FirstName"),
                        Format(drpat("DOB"), SystemConfig.DateFormat),
                        drpat("Sex"), MRN, drpat("Accession"),
                        Format(drpat("DOS"), SystemConfig.DateFormat),
                        ReqID, GetProviderName(drpat("ProviderID")))
                End While
            End If
            cnpat.Close()
            cnpat = Nothing
            txtSearch.Text = ""
            txtFrom.Text = ""
            txtTo.Text = ""
            txtDOB.Text = ""
            txtProviderID.Text = ""
            txtProviderName.Text = ""
        End If

        Me.Cursor = Cursors.Default
    End Sub

    Private Function GetProviderName(ByVal ProviderID As Long) As String
        Dim Provider As String = ""
        Dim cnprv As New SqlConnection(connString)
        cnprv.Open()
        Dim cmdprv As New SqlCommand("Select " &
        "* from Providers where ID = " & ProviderID, cnprv)
        cmdprv.CommandType = CommandType.Text
        Dim drprv As SqlDataReader = cmdprv.ExecuteReader
        If drprv.HasRows Then
            While drprv.Read
                If drprv("IsIndividual") = True Then
                    If drprv("Degree") IsNot DBNull.Value AndAlso
                    drprv("Degree") <> "" Then
                        Provider = drprv("LastName_BSN") & ", " &
                        drprv("FirstName") & " " & drprv("Degree")
                    Else
                        Provider = drprv("LastName_BSN") & ", " &
                        drprv("FirstName")
                    End If
                Else
                    Provider = drprv("LastName_BSN")
                End If
            End While
        End If
        cnprv.Close()
        cnprv = Nothing
        Return Provider
    End Function

    Private Sub FindPatientBySSN(ByVal SSN As String)
        SSN = SSNNeat(SSN)
        Dim cnpat As New SqlConnection(connString)
        cnpat.Open()
        Dim cmdpat As New SqlCommand("Select * from Patients " &
        "where ID in (Select distinct Patient_ID from Requisitions where " &
        "Received <> 0 and ID in (Select distinct Accession_ID from " &
        "Acc_Results where Released <> 0)) and SSN = '" & SSN & "'", cnpat)
        cmdpat.CommandType = CommandType.Text
        Dim drpat As SqlDataReader = cmdpat.ExecuteReader
        If drpat.HasRows Then
            While drpat.Read
                dgv.Rows.Add(drpat("ID"),
                drpat("LastName"), drpat("FirstName"),
                Format(drpat("DOB"), SystemConfig.DateFormat),
                GetFullGender(drpat("Sex")), drpat("SSN"),
                GetAddress(drpat("Address_ID")))
            End While
        End If
        cnpat.Close()
        cnpat = Nothing
    End Sub

    Private Sub dgvPatients_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellClick
        If e.RowIndex <> -1 Then
            Me.Tag = dgv.Rows(e.RowIndex).Cells(0).Value.ToString & "|" & dgv.Rows(e.RowIndex).Cells(6).Value.ToString
            btnAccept.Enabled = True
        Else
            Me.Tag = ""
        End If
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        'Me.Tag = dgvPatients.Rows(e.RowIndex).Cells(0).Value

        If dgv.CurrentRow IsNot Nothing Then

            Dim rowIndex As Integer = dgv.CurrentRow.Index

            Me.Tag = dgv.Rows(rowIndex).Cells(0).Value.ToString & "|" & dgv.Rows(rowIndex).Cells(6).Value.ToString

        End If

        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub txtFrom_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtFrom.BackColor = FCOLOR
    End Sub

    Private Sub txtFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtFrom.BackColor = NFCOLOR
    End Sub

    Private Sub txtFrom_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
        If UserEnteredText(txtFrom) <> "" Then
            If Not IsDate(txtFrom.Text) Then
                MsgBox("Not a valid date")
                txtFrom.Text = ""
                txtFrom.Focus()
            End If
        End If
    End Sub

    Private Sub txtTo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtTo.BackColor = FCOLOR
    End Sub

    Private Sub txtTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtTo.BackColor = FCOLOR
    End Sub

    Private Sub txtTo_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
        If UserEnteredText(txtTo) <> "" Then
            If Not IsDate(txtTo.Text) Then
                MsgBox("Not a valid date")
                txtTo.Text = ""
                txtTo.Focus()
            End If
        End If
    End Sub

    Private Sub txtProviderID_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtProviderID.BackColor = FCOLOR
    End Sub

    Private Sub txtProviderID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Numerals(sender, e)
    End Sub

    Private Sub txtProviderID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtProviderID.BackColor = NFCOLOR
        If txtProviderID.Text <> "" Then
            Dim cnprv As New SqlConnection(connString)
            cnprv.Open()
            Dim cmdprv As New SqlCommand("Select * from " &
            "Providers where ID = " & Val(txtProviderID.Text), cnprv)
            cmdprv.CommandType = CommandType.Text
            Dim drprv As SqlDataReader = cmdprv.ExecuteReader
            If drprv.HasRows Then
                While drprv.Read
                    If drprv("IsIndividual") = False Then
                        txtProviderName.Text = drprv("LastName_BSN")
                    Else
                        If drprv("Degree") IsNot DBNull.Value Then
                            txtProviderName.Text = drprv("LastName_BSN") & ", " &
                            drprv("FirstName") & " " & drprv("Degree")
                        Else
                            txtProviderName.Text = drprv("LastName_BSN") & ", " &
                            drprv("FirstName")
                        End If
                    End If
                End While
            End If
            cnprv.Close()
            cnprv = Nothing
        Else
            txtProviderName.Text = ""
        End If
    End Sub

    Private Sub txtProviderName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtProviderName.BackColor = FCOLOR
    End Sub

    Private Sub txtProviderName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtProviderName.BackColor = NFCOLOR
    End Sub

    Private Sub txtDOB_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtDOB.BackColor = FCOLOR
    End Sub

    Private Sub txtDOB_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtDOB.BackColor = NFCOLOR
    End Sub

    Private Sub txtSearch_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtSearch.BackColor = FCOLOR
    End Sub

    Private Sub txtSearch_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtSearch.BackColor = NFCOLOR
    End Sub

    Private Sub btnProvLookUp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ProvID As String = frmProviderLookup.ShowDialog()
        If ProvID <> "" Then
            Dim cnprv As New SqlConnection(connString)
            cnprv.Open()
            Dim cmdprv As New SqlCommand("Select " &
            "* from Providers where ID = " & ProvID, cnprv)
            cmdprv.CommandType = CommandType.Text
            Dim drprv As SqlDataReader = cmdprv.ExecuteReader
            If drprv.HasRows Then
                While drprv.Read
                    If drprv("IsIndividual") = False Then
                        txtProviderName.Text = drprv("LastName_BSN")
                    Else
                        If drprv("Degree") IsNot DBNull.Value Then
                            txtProviderName.Text = drprv("LastName_BSN") & ", " &
                            drprv("FirstName") & " " & drprv("Degree")
                        Else
                            txtProviderName.Text = drprv("LastName_BSN") & ", " &
                            drprv("FirstName")
                        End If
                    End If
                End While
            End If
            cnprv.Close()
            cnprv = Nothing
        Else
            txtProviderName.Text = ""
        End If
    End Sub
    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Down AndAlso dgv.Rows.Count > 0 Then
            dgv.Focus()
        End If
    End Sub

    Private Sub dgv_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellDoubleClick
        Call btnAccept_Click(Nothing, Nothing)
    End Sub

    Private Sub dgvTests_KeyDown(sender As Object, e As KeyEventArgs) Handles dgv.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Call btnAccept_Click(Nothing, Nothing)
            Me.txtSearch.Focus()
        End If
    End Sub

    Private Sub GroupBox3_Enter(sender As Object, e As EventArgs) Handles GroupBox3.Enter

    End Sub
End Class
