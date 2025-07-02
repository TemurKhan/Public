Imports System.Windows.Forms
Imports System.data

Public Class frmPmntLookUp
    Private Sub frmPmntLookUp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbARType.SelectedIndex = frmPayments.ArType
        txtSearch.Text = "" : txtFrom.Text = "" : txtTo.Text = "" : txtDocID.Text = ""
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)

        txtSearch.Focus()
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
            End If
        End If
    End Sub

    Private Sub txtTo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTo.Validated
        If UserEnteredText(txtTo) <> "" Then
            If Not IsDate(txtTo.Text) Then
                MsgBox("Not a valid date")
                txtTo.Text = ""
                txtTo.Focus()
            End If
        End If
    End Sub

    Private Sub btnPmtSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPmtSearch.Click
        Me.Cursor = Cursors.WaitCursor

        dgvPayments.Rows.Clear()
        Dim sSQL As String = "Select * from Payments where ArType = " & cmbARType.SelectedIndex
        If Trim(txtSearch.Text) <> "" Then
            Dim LName As String = "" : Dim FName As String = ""
            If InStr(txtSearch.Text, ",") > 0 Then
                LName = Trim(Microsoft.VisualBasic.Mid(txtSearch.Text, 1, InStr(txtSearch.Text, ",") - 2))
                FName = Trim(Microsoft.VisualBasic.Mid(txtSearch.Text, InStr(txtSearch.Text, ",") + 1))
            Else
                LName = Trim(txtSearch.Text)
            End If
            If cmbARType.SelectedIndex = 0 Then     'Client
                sSQL += " and Ar_ID in (Select distinct ID from Providers where LastName_BSN like '" & LName & "%'"
                If FName <> "" Then
                    sSQL += " and FirstName like '" & FName & "%')"
                Else
                    sSQL += ")"
                End If
            ElseIf cmbARType.SelectedIndex = 1 Then     'TP
                sSQL += " and Ar_ID in (Select distinct ID from Payers where PayerName like '" & LName & "%')"
            Else    'Patient
                sSQL += " and Ar_ID in (Select distinct ID from Patients where LastName like '" & LName & "%'"
                If FName <> "" Then
                    sSQL += " and FirstName like '" & FName & "%')"
                Else
                    sSQL += ")"
                End If
            End If
            txtSearch.Text = ""
        End If
        '
        If IsDate(txtFrom.Text) And Not IsDate(txtTo.Text) Then
            sSQL += " and PaymentDate between '" & txtFrom.Text & "' and '" & txtFrom.Text & " 23:59:00'"
            txtFrom.Text = ""
        ElseIf Not IsDate(txtFrom.Text) And IsDate(txtTo.Text) Then
            sSQL += " and PaymentDate between '" & txtTo.Text & "' and '" & txtTo.Text & " 23:59:00'"
            txtTo.Text = ""
        ElseIf IsDate(txtFrom.Text) And IsDate(txtTo.Text) Then
            sSQL += " and PaymentDate between '" & txtFrom.Text & "' and '" & txtTo.Text & " 23:59:00'"
            txtFrom.Text = "" : txtTo.Text = ""
        End If
        '
        If txtDocID.Text <> "" Then
            sSQL += " and DocNo = '" & Trim(txtDocID.Text) & "'"
            txtDocID.Text = ""
        End If
        '
        If Trim(txtInvID.Text) <> "" Then
            sSQL += " and ID in (Select distinct " & _
            "Payment_ID from Payment_Detail where Charge_ID = " & Trim(txtInvID.Text) & ")"
            txtInvID.Text = ""
        End If
        '
        If IsDate(txtSvcDate.Text) Then
            sSQL += " and ID in (Select distinct Payment_ID from Payment_Detail where Charge_ID " & _
            "in (Select ID from Charges where Svc_Date between '" & CDate(txtSvcDate.Text) & _
            "' and '" & CDate(txtSvcDate.Text & " 23:59:00") & "'))"
            txtSvcDate.Text = ""
        End If
        '
        If sSQL <> "" Then
            Dim ArType As String = ""
            Dim cnpl As New SqlClient.SqlConnection(connString)
            cnpl.Open()
            Dim cmdpl As New SqlClient.SqlCommand(sSQL, cnpl)
            cmdpl.CommandType = CommandType.Text
            Dim drpl As SqlClient.SqlDataReader = cmdpl.ExecuteReader
            If drpl.HasRows Then
                While drpl.Read
                    If drpl("ArType") = 0 Then
                        ArType = "C"
                    ElseIf drpl("ArType") = 1 Then
                        ArType = "T"
                    Else
                        ArType = "P"
                    End If
                    dgvPayments.Rows.Add(drpl("ID"), drpl("DocNo"), _
                    Format(drpl("PaymentDate"), SystemConfig.DateFormat), _
                    Format(drpl("Amount"), "0.00"), ArType, _
                    GetARName(drpl("Ar_ID"), drpl("ArType")))
                End While
            End If
            cnpl.Close()
            cnpl = Nothing
        End If

        Me.Cursor = Cursors.Default

    End Sub

    Private Function GetARName(ByVal ArID As Long, ByVal ArType As Integer) As String
        Dim ArName As String = ""
        Dim sSQL As String = ""
        If ArType = 0 Then  'Client
            sSQL = "Select * from Providers where ID = " & ArID
        ElseIf ArType = 1 Then 'TP
            sSQL = "Select * from Payers where ID = " & ArID
        Else    'Patient
            ssql = "Select * from Patients where ID = " & ArID
        End If
        Dim cnar As New SqlClient.SqlConnection(connString)
        cnar.Open()
        Dim cmdar As New SqlClient.SqlCommand(sSQL, cnar)
        cmdar.CommandType = CommandType.Text
        Dim drar As SqlClient.SqlDataReader = cmdar.ExecuteReader
        If drar.HasRows Then
            While drar.Read
                If ArType = 0 Then
                    If drar("IsIndividual") IsNot DBNull.Value AndAlso drar("IsIndividual") = 0 Then
                        ArName = drar("LastName_BSN")
                    Else
                        ArName = drar("LastName_BSN") & ", " & drar("FirstName") _
                        & IIf(drar("Degree") Is DBNull.Value, "", " " & drar("Degree"))
                    End If
                ElseIf ArType = 1 Then
                    ArName = drar("PayerName")
                Else
                    ArName = drar("LastName") & ", " & drar("FirstName")
                End If
            End While
        End If
        cnar.Close()
        cnar = Nothing
        Return ArName
    End Function

    Private Sub dgvPayments_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPayments.CellClick
        If e.RowIndex <> -1 Then
            Me.Tag = dgvPayments.Rows(e.RowIndex).Cells(0).Value.ToString _
            & "|" & dgvPayments.Rows(e.RowIndex).Cells(1).Value
            btnAccept.Enabled = True
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click, btn_Cancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        Me.txtSearch.Focus()
        Me.Close()
    End Sub


    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Down AndAlso dgvPayments.Rows.Count > 0 Then
            dgvPayments.Focus()
        End If
    End Sub

    Private Sub dgvPayments_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPayments.CellDoubleClick
        Call btnAccept_Click(sender, e)
    End Sub

    Private Sub dgvPayments_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvPayments.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            If dgvPayments.CurrentRow IsNot Nothing Then

                Dim rowIndex As Integer = dgvPayments.CurrentRow.Index

                Me.Tag = dgvPayments.Rows(rowIndex).Cells(0).Value.ToString _
           & "|" & dgvPayments.Rows(rowIndex).Cells(1).Value

                Call btnAccept_Click(sender, e)
                Me.txtSearch.Focus()
            End If
        End If
    End Sub

End Class
