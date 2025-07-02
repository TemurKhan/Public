Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmPatLookUp

    Public patientName As String


    Private Sub cmbSearch_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSearch.SelectedIndexChanged
        If cmbSearch.SelectedIndex = 0 Then
            Label1.Text = "Name (Last, First) even partial"
            txtSearch.Mask = ""
        Else
            Label1.Text = "Social Security #(No Space or dash)"
            txtSearch.Mask = "###-##-####"
        End If
        txtSearch.Text = ""
    End Sub

    Private Sub frmPatLookUp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbSearch.SelectedIndex = 0
        Me.Tag = ""
        btnAccept.Enabled = False
        dgv.Rows.Clear()
        lbl_TotRec.Text = ""

        'If Me.Owner.Name = "frmRequisitions" Then
        If Not String.IsNullOrWhiteSpace(patientName) Then

            FindPatientByName(patientName)
            If dgv.RowCount = 0 Then Me.Close()
        End If

        'End If

        'If frmRequisitions.IsHandleCreated() Then
        '    If Trim(frmRequisitions.txtLName.Text) <> "" And _
        '    Trim(frmRequisitions.txtFName.Text) <> "" Then
        '        Dim PatName As String = ""
        '        PatName = Trim(frmRequisitions.txtLName.Text) _
        '        & ", " & Trim(frmRequisitions.txtFName.Text)
        '        FindPatientByName(PatName)
        '        If dgv.RowCount = 0 Then Me.Close()
        '    End If
        'ElseIf frmPatient.IsHandleCreated() Then
        '    If Trim(frmPatient.txtLName.Text) <> "" And _
        '    Trim(frmPatient.txtFName.Text) <> "" Then
        '        Dim PatName As String = ""
        '        PatName = Trim(frmPatient.txtLName.Text) & _
        '        ", " & Trim(frmPatient.txtFName.Text)
        '        FindPatientByName(PatName)
        '        If dgv.RowCount = 0 Then Me.Close()
        '    End If
        'End If

        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)

        Me.txtSearch.Focus()
    End Sub

    Private Function IsFormOpened(ByVal frm As System.Windows.Forms.Form) As Boolean
        Dim IsOpened As Boolean = False
        Dim TheForm As System.Windows.Forms.Form
        For Each TheForm In Application.OpenForms
            If TheForm Is frm Then
                IsOpened = True
                Exit For
            End If
        Next
        Return IsOpened
    End Function

    Friend Sub FindPatientByName(ByVal PatName As String)
        Dim sSQL As String = ""
        Dim LastName As String = ""
        Dim FirstName As String = ""
        Dim Name() As String
        'Dim Rs As New ADODB.Recordset
        If InStr(PatName, ",") <> 0 Then
            Name = Split(PatName, ",")
            LastName = Replace(Name(0), "'", "''")
            LastName = Trim(LastName) & "%"
            FirstName = Replace(Name(1), "'", "''")
            FirstName = Trim(FirstName) & "%"
        Else
            LastName = Replace(PatName, "'", "''")
            LastName = Trim(LastName) & "%"
        End If
        'If FirstName = "" Then
        '    sSQL = "Select * from Patients where LastName like '" & LastName & "'"
        'Else
        '    sSQL = "Select * from Patients where LastName like '" & LastName & _
        '    "' and FirstName like '" & FirstName & "'"
        'End If
        If FirstName = "" Then
            sSQL = "Select * from vw_Patients where LastName like '" & LastName & "'"
        Else
            sSQL = "Select * from vw_Patients where LastName like '" & LastName & "' and FirstName like '" & FirstName & "'"
        End If
        dgv.Rows.Clear()
        Dim cnfp As New SqlConnection(connString)
        cnfp.Open()
        Dim cmdfp As New SqlCommand(sSQL, cnfp)
        cmdfp.CommandType = CommandType.Text
        Dim drfp As SqlDataReader = cmdfp.ExecuteReader
        If drfp.HasRows Then
            While drfp.Read

                Dim ad = drfp("ID")
                Dim lname = drfp("LastName")
                Dim dod = drfp("DOB")
                Dim addr = drfp("Address")
                Dim gg = GetFullGender(IIf(drfp("Sex") Is DBNull.Value, "", drfp("Sex")))
                If IsDate(dod.ToString()) Then
                    dgv.Rows.Add(drfp("ID"), drfp("LastName"), drfp("FirstName"),
                Format(drfp("DOB"), SystemConfig.DateFormat), GetFullGender(drfp("Sex")),
                drfp("SSN"), drfp("Address"))
                Else
                    dgv.Rows.Add(drfp("ID"), drfp("LastName"), drfp("FirstName"),
                dod, gg,
               drfp("SSN"), drfp("Address"))
                End If

            End While
        End If
        cnfp.Close()
        cnfp = Nothing
    End Sub

    Private Sub btnPatSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatSearch.Click
        Me.Cursor = Cursors.WaitCursor

        dgv.Rows.Clear()
        If txtSearch.Text <> "" Then
            If cmbSearch.SelectedIndex = 0 Then
                FindPatientByName(txtSearch.Text)
            Else
                FindPatientBySSN(SSNNeat(txtSearch.Text))
            End If
            txtSearch.Text = ""
        End If

        Me.Cursor = Cursors.Default

        lbl_TotRec.Text = dgv.Rows.Count.ToString & " Record Found"

    End Sub

    Private Sub FindPatientBySSN(ByVal SSN As String)
        dgv.Rows.Clear()
        If Trim(SSN) <> "" Then
            Dim cnssn As New SqlConnection(connString)
            cnssn.Open()
            Dim cmdssn As New SqlCommand("Select " &
            "* from vw_Patients where SSN = '" & SSN & "'", cnssn)
            cmdssn.CommandType = CommandType.Text
            Dim drssn As SqlDataReader = cmdssn.ExecuteReader
            If drssn.HasRows Then
                While drssn.Read
                    dgv.Rows.Add(drssn("ID"), drssn("LastName"),
                    drssn("FirstName"), Format(drssn("DOB"), SystemConfig.DateFormat),
                    GetFullGender(drssn("Sex")), SSN, drssn("Address"))      'GetAddress(drssn("Address_ID"))
                End While
            End If
            cnssn.Close()
            cnssn = Nothing
        End If
    End Sub

    Private Sub btnPatient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatient.Click
        frmPatient.Show()
        frmPatient.MdiParent = frmDashboard
        Me.Close()
    End Sub

    Private Sub dgvPatients_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellClick
        If e.RowIndex <> -1 Then
            Me.Tag = dgv.Rows(e.RowIndex).Cells(0).Value.ToString
            btnAccept.Enabled = True
        Else
            Me.Tag = ""
        End If
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        patientName = ""
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        'Me.Tag = dgvPatients.Rows(e.RowIndex).Cells(0).Value
        Me.txtSearch.Focus()
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click, btn_Cancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        If txtSearch.Text = "" Then
            btnPatSearch.Enabled = False
        Else
            btnPatSearch.Enabled = True
        End If
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
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
            If dgv.CurrentRow IsNot Nothing Then

                Dim rowIndex As Integer = dgv.CurrentRow.Index

                Me.Tag = dgv.Rows(rowIndex).Cells(0).Value.ToString
                Call btnAccept_Click(Nothing, Nothing)
                Me.txtSearch.Focus()
            End If
        End If
    End Sub

End Class
