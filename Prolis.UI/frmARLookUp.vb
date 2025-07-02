Imports System.Windows.Forms

Public Class frmARLookUp
    Public Shared ArType As Integer = 1
    Private Sub frmARLookUp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbARType.SelectedIndex = ArType

        txtSearch.Text = "" : txtFrom.Text = "" : txtTo.Text = "" : txtAccID.Text = ""
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub txtFrom_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFrom.Validated
        If UserEnteredText(txtFrom) <> "" Then
            If Not IsDate(txtFrom.Text) Then
                MsgBox("Not a valid date")
                txtFrom.Text = ""
                txtFrom.Focus()
            Else
                txtAccID.Text = ""
            End If
        End If
    End Sub

    Private Sub txtTo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTo.Validated
        If UserEnteredText(txtTo) <> "" Then
            If Not IsDate(txtTo.Text) Then
                MsgBox("Not a valid date")
                txtTo.Text = ""
                txtTo.Focus()
            Else
                txtAccID.Text = ""
            End If
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click, btn_Cancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        If dgvARs.SelectedRows.Count > 0 Then

            Me.Tag = dgvARs.SelectedRows(0).Cells(0).Value
        End If
        Me.Close()
    End Sub

    Private Sub btnPatSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatSearch.Click
        Me.Cursor = Cursors.WaitCursor

        dgvARs.Rows.Clear()
        Dim sSQL As String = ""
        Dim LName As String = "" : Dim FName As String = ""
        If InStr(txtSearch.Text, ",") > 0 Then
            LName = Trim(Microsoft.VisualBasic.Mid(txtSearch.Text, 1, InStr(txtSearch.Text, ",") - 2))
            FName = Trim(Microsoft.VisualBasic.Mid(txtSearch.Text, InStr(txtSearch.Text, ",") + 1))
        Else
            LName = Trim(txtSearch.Text)
        End If
        If cmbARType.SelectedIndex = 0 Then     'Client
            If txtControl.Text = "" Then
                If txtSearch.Text <> "" And Not (IsDate(txtFrom.Text) And IsDate(txtTo.Text)) And txtAccID.Text = "" Then      'Just Search Term
                    sSQL = "Select * from Providers where ID in (Select distinct Ar_ID from Charges where ArType = 0) and " & _
                    "LastName_BSN like '" & LName & "%'" & IIf(FName <> "", " and FirstName like '" & FName & "%'", "")
                ElseIf txtSearch.Text <> "" And IsDate(txtFrom.Text) And IsDate(txtTo.Text) And txtAccID.Text = "" Then      'Search Term and dates
                    sSQL = "Select * from Providers where ID in (Select distinct Ar_ID from Charges where ArType = 0 " & _
                    "and Svc_Date >= '" & CDate(txtFrom.Text & " 00:00:01 AM") & "' and Svc_Date <= '" & _
                    CDate(txtTo.Text & " 11:59:59 PM") & "') and LastName_BSN like '" & LName & "%'" & _
                    IIf(FName <> "", " and FirstName like '" & FName & "%'", "")
                ElseIf txtSearch.Text = "" And IsDate(txtFrom.Text) And IsDate(txtTo.Text) And txtAccID.Text = "" Then      'Just dates
                    sSQL = "Select * from Providers where ID in (Select distinct Ar_ID from Charges where ArType = 0 " & _
                    "and Svc_Date >= '" & CDate(txtFrom.Text & " 00:00:01 AM") & "' and Svc_Date <= '" & _
                    CDate(txtTo.Text & " 11:59:59 PM") & "')"
                ElseIf txtAccID.Text <> "" Then
                    sSQL = "Select * from Providers where ID in (Select distinct Ar_ID from Charges where ArType = 0 " & _
                    "and Accession_ID = " & Val(txtAccID.Text) & ")"
                End If
            Else
                sSQL = "Select * from Providers where ID in (Select distinct Ar_ID from Charges where " & _
                "ArType = 0 and ID = " & Val(txtControl.Text) & ")"
            End If
        ElseIf cmbARType.SelectedIndex = 2 Then     'Patient 2
            If txtControl.Text = "" Then
                If txtSearch.Text <> "" And Not (IsDate(txtFrom.Text) And IsDate(txtTo.Text)) And txtAccID.Text = "" Then      'Just Search Term
                    sSQL = "Select * from Patients where ID in (Select distinct Ar_ID from Charges where ArType = 2) and " & _
                    "LastName like '" & LName & "%'" & IIf(FName <> "", " and FirstName like '" & FName & "%'", "")
                ElseIf txtSearch.Text <> "" And IsDate(txtFrom.Text) And IsDate(txtTo.Text) And txtAccID.Text = "" Then      'Search Term and dates
                    sSQL = "Select * from Patients where ID in (Select distinct Ar_ID from Charges where ArType = 2 " & _
                    "and Svc_Date >= '" & CDate(txtFrom.Text & " 00:00:01 AM") & "' and Svc_Date <= '" & _
                    CDate(txtTo.Text & " 11:59:59 PM") & "') and LastName like '" & LName & "%'" _
                     & IIf(FName <> "", " and FirstName like '" & FName & "%'", "")
                ElseIf txtSearch.Text = "" And IsDate(txtFrom.Text) And IsDate(txtTo.Text) And txtAccID.Text = "" Then      'Just dates
                    sSQL = "Select * from Patients where ID in (Select distinct Ar_ID from Charges where ArType = 2 " & _
                    "and Svc_Date >= '" & CDate(txtFrom.Text & " 00:00:01 AM") & "' and Svc_Date <= '" & _
                    CDate(txtTo.Text & " 11:59:59 PM") & "')"
                ElseIf txtAccID.Text <> "" Then
                    sSQL = "Select * from Patients where ID in (Select distinct Ar_ID from Charges where ArType = 2 " & _
                    "and Accession_ID = " & Val(txtAccID.Text) & ")"
                End If
            Else
                sSQL = "Select * from Patients where ID in (Select distinct Ar_ID from Charges where " & _
                "ArType = 2 and ID = " & Val(txtControl.Text) & ")"
            End If
        Else    'Third Party 1
            If txtControl.Text = "" Then
                If txtSearch.Text <> "" And Not (IsDate(txtFrom.Text) And IsDate(txtTo.Text)) And txtAccID.Text = "" Then      'Just Search Term
                    sSQL = "Select * from Payers where ID in (Select distinct Ar_ID from Charges where ArType = 1) and " & _
                    "PayerName like '%" & LName & "%'"
                ElseIf txtSearch.Text <> "" And IsDate(txtFrom.Text) And IsDate(txtTo.Text) And txtAccID.Text = "" Then      'Search Term and dates
                    sSQL = "Select * from Payers where ID in (Select distinct Ar_ID from Charges where ArType = 1 " & _
                    "and Svc_Date >= '" & CDate(txtFrom.Text & " 00:00:01 AM") & "' and Svc_Date <= '" & _
                    CDate(txtTo.Text & " 11:59:59 PM") & "') and PayerName like '" & LName & "%'"
                ElseIf txtSearch.Text = "" And IsDate(txtFrom.Text) And IsDate(txtTo.Text) And txtAccID.Text = "" Then      'Just dates
                    sSQL = "Select * from Payers where ID in (Select distinct Ar_ID from Charges where ArType = 1 " & _
                    "and Svc_Date >= '" & CDate(txtFrom.Text & " 00:00:01 AM") & "' and Svc_Date <= '" & _
                    CDate(txtTo.Text & " 11:59:59 PM") & "')"
                ElseIf txtAccID.Text <> "" Then
                    sSQL = "Select * from Payers where ID in (Select distinct Ar_ID from Charges where ArType = 1 " & _
                    "and Accession_ID = " & Val(txtAccID.Text) & ")"
                End If
            Else
                sSQL = "Select * from Payers where ID in (Select distinct Ar_ID from Charges where " & _
                "ArType = 1 and ID = " & Val(txtControl.Text) & ")"
            End If
        End If
        If sSQL <> "" Then
            Dim cnarl As New SqlClient.SqlConnection(connString)
            cnarl.Open()
            Dim cmdarl As New SqlClient.SqlCommand(sSQL, cnarl)
            cmdarl.CommandType = CommandType.Text
            Dim drarl As SqlClient.SqlDataReader = cmdarl.ExecuteReader
            If drarl.HasRows Then
                While drarl.Read
                    If cmbARType.SelectedIndex = 0 Then     'Client
                        Dim Provider As String = ""
                        If drarl("IsIndividual") IsNot DBNull.Value AndAlso drarl("IsIndividual") = 0 Then
                            Provider = drarl("LastName_BSN") & ", " & GetAddress(drarl("Address_ID"))
                        Else
                            If drarl("Degree") Is DBNull.Value Then
                                Provider = drarl("LastName_BSN") & ", " & drarl("FirstName")
                            Else
                                Provider = drarl("LastName_BSN") & ", " & drarl("FirstName") & " " & Trim(drarl("Degree"))
                            End If
                            Provider += ", " & GetAddress(drarl("Address_ID"))
                        End If
                        dgvARs.Rows.Add(drarl("ID"), Provider, Format(GetBalance(0, drarl("ID")), "#0.00"))
                    ElseIf cmbARType.SelectedIndex = 2 Then     'Patient
                        Dim Address As String = ""
                        If drarl("Address_ID") IsNot DBNull.Value AndAlso drarl("Address_ID") > 0 Then _
                        Address = " Address: " & GetAddress(drarl("Address_ID"))
                        Dim Bal As Single = GetBalance(2, drarl("ID"))
                        '
                        dgvARs.Rows.Add(drarl("ID"), drarl("LastName") & ", " & _
                        drarl("FirstName") & " DOB: " & Format(drarl("DOB"), _
                        SystemConfig.DateFormat) & " Gender: " & drarl("Sex") & Address _
                        , Format(Bal, "#0.00"))
                    ElseIf cmbARType.SelectedIndex = 1 Then     'Insurance
                        dgvARs.Rows.Add(drarl("ID"), drarl("PayerName") & ", " & _
                        GetAddress(drarl("Address_ID")), Format(GetBalance(1, drarl("ID")), "#0.00"))
                    End If
                End While
            End If
            cnarl.Close()
            cnarl = Nothing
        End If

        Me.Cursor = Cursors.Default
    End Sub

    Private Function GetBalance(ByVal ARType As Integer, ByVal ArID As Long) As Single
        Dim Billed As Single = 0 : Dim Paid As Single = 0
        Dim cngb As New SqlClient.SqlConnection(connString)
        cngb.Open()
        Dim cmdgb As New SqlClient.SqlCommand("Select SUM(GrossAmount) as Billed " & _
        "from Charges where ArType = " & ARType & " and Ar_ID = " & ArID, cngb)
        cmdgb.CommandType = CommandType.Text
        Dim drgb As SqlClient.SqlDataReader = cmdgb.ExecuteReader
        If drgb.HasRows Then
            While drgb.Read
                If drgb("Billed") IsNot DBNull.Value _
                Then Billed = drgb("Billed")
            End While
        End If
        cngb.Close()
        cngb = Nothing
        '
        Dim cngp As New SqlClient.SqlConnection(connString)
        cngp.Open()
        Dim cmdgp As New SqlClient.SqlCommand("Select SUM(Amount) as Paid " & _
        "from Payments where ArType = " & ARType & " and Ar_ID = " & ArID, cngp)
        cmdgp.CommandType = CommandType.Text
        Dim drgp As SqlClient.SqlDataReader = cmdgp.ExecuteReader
        If drgp.HasRows Then
            While drgp.Read
                If drgp("Paid") IsNot DBNull.Value Then Paid = drgp("Paid")
            End While
        End If
        cngp.Close()
        cngp = Nothing
        Return Billed - Paid
    End Function

    Private Sub txtAccID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAccID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccID.Validated
        If txtAccID.Text <> "" Then
            txtFrom.Text = "" : txtTo.Text = "" : txtSearch.Text = ""
        End If
    End Sub

    Private Sub txtSearch_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.Validated
        If txtSearch.Text <> "" Then txtAccID.Text = ""
    End Sub

    Private Sub dgvARs_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvARs.CellClick
        If e.RowIndex <> -1 Then
            Me.Tag = dgvARs.Rows(e.RowIndex).Cells(0).Value.ToString
            btnAccept.Enabled = True
        End If
    End Sub

    Private Sub txtControl_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtControl.KeyPress
        Numerals(sender, e)
    End Sub


    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown, txtAccID.KeyDown, txtControl.KeyDown, txtTo.KeyDown
        If e.KeyCode = Keys.Down AndAlso dgvARs.Rows.Count > 0 Then
            dgvARs.Focus()
        End If
    End Sub

    Private Sub dgvARs_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvARs.CellDoubleClick
        Call btnAccept_Click(Nothing, Nothing)
    End Sub

    Private Sub dgvARs_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvARs.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Call btnAccept_Click(Nothing, Nothing)
            Me.txtSearch.Focus()
        End If
    End Sub

    Private Sub Label19_Click(sender As Object, e As EventArgs) Handles Label19.Click
        txtAccID.Text = Clipboard.GetText()
        btnPatSearch.PerformClick()
    End Sub
End Class
