Imports System.Windows.Forms
Imports System.data

Public Class frmPickupLookup

    Private Sub frmPickupLookup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Tag = ""
        cmbTerm.SelectedIndex = 0
        dgvPickups.Rows.Clear()
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)

        txtTerm.Focus()
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub cmbTerm_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTerm.SelectedIndexChanged
        txtTerm.Text = ""
        If cmbTerm.SelectedIndex = 0 Then   'Pickup Date
            Label1.Text = "Pick Up Date"
            txtTerm.Mask = "00/00/0000"
        ElseIf cmbTerm.SelectedIndex = 1 Then   'Client ID
            Label1.Text = "Client ID"
            txtTerm.Mask = "0000000000"
        ElseIf cmbTerm.SelectedIndex = 2 Then   'Client Name
            Label1.Text = "Client Name (even partial)"
            txtTerm.Mask = ""
        Else
            Label1.Text = "Courier Name (even partial)"
            txtTerm.Mask = ""
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click, btn_Cancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        Me.txtTerm.Focus()
        Me.Close()
    End Sub

    Private Sub dgvPickups_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPickups.CellClick
        If e.RowIndex <> -1 Then
            Me.Tag = dgvPickups.Rows(e.RowIndex).Cells(0).Value.ToString
            btnAccept.Enabled = True
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Me.Cursor = Cursors.WaitCursor

        dgvPickups.Rows.Clear()
        If txtTerm.Text <> "" Then
            Dim sSQL As String = ""
            Dim MaxDate As Date = GetMaxDate()
            If cmbTerm.SelectedIndex = 0 Then   'Date
                If IsDate(txtTerm.Text) Then
                    sSQL = "Select * from Pickups where PickupDate between '" & _
                    Format(DateAdd(DateInterval.Day, -3, MaxDate), SystemConfig.DateFormat) & "' and '" & _
                    Format(CDate(txtTerm.Text), SystemConfig.DateFormat) & " 23:59:00'"
                Else
                    MsgBox("Invalid date Criteria provided", MsgBoxStyle.Critical, "PROLIS")
                    txtTerm.Text = ""
                End If
            ElseIf cmbTerm.SelectedIndex = 1 Then   'Client ID
                sSQL = "Select * from Pickups where PickupDate >= '" & DateAdd(DateInterval.Day, -3, MaxDate) _
                & "' and Provider_ID = " & Val(txtTerm.Text)
            ElseIf cmbTerm.SelectedIndex = 2 Then   'Client Name
                Dim LastName As String = ""
                Dim FirstName As String = ""
                Dim Names() As String
                If InStr(txtTerm.Text, ",") > 0 Then    'both names
                    Names = Split(txtTerm.Text, ",")
                    LastName = Trim(Names(0))
                    FirstName = Trim(Names(1))
                    sSQL = "Select * from Pickups where PickupDate >= '" & DateAdd(DateInterval.Day, -3, MaxDate) _
                    & "' and Provider_ID in (Select ID from Providers where LastName_BSN like '" & LastName & _
                    "%' and FirstName like '" & FirstName & "%')"
                Else
                    LastName = Trim(txtTerm.Text)
                    FirstName = ""
                    sSQL = "Select * from Pickups where PickupDate >= '" & DateAdd(DateInterval.Day, -3, MaxDate) _
                    & "' and Provider_ID in (Select ID from Providers where LastName_BSN like '" & LastName & "%')"
                End If
            Else    'Courier Name            
                sSQL = "Select * from Pickups where PickupDate >= '" & DateAdd(DateInterval.Day, -3, MaxDate) _
                & "' and Route_ID in (Select ID from Routes where Courier like '" & txtTerm.Text & "%')"
            End If
            If sSQL <> "" Then
                Dim cnpl As New SqlClient.SqlConnection(connString)
                cnpl.Open()
                Dim cmdpl As New SqlClient.SqlCommand(sSQL, cnpl)
                cmdpl.CommandType = CommandType.Text
                Dim drpl As SqlClient.SqlDataReader = cmdpl.ExecuteReader
                If drpl.HasRows Then
                    While drpl.Read
                        dgvPickups.Rows.Add(drpl("ID"), Format(drpl("PickupDate"),
                        SystemConfig.DateFormat), Format(drpl("PickupDate"), "hh:mm"),
                        GetCourier(drpl("Route_ID")), GetClient(drpl("Provider_ID")))
                    End While
                End If
                cnpl.Close()
                cnpl = Nothing
            End If
            txtTerm.Text = ""
        End If
        Me.Cursor = Cursors.Default
    End Sub
    Private Function GetMaxDate() As Date
        Dim MaxDate As Date = Date.Now
        Dim cnmd As New SqlClient.SqlConnection(connString)
        cnmd.Open()
        Dim cmdmd As New SqlClient.SqlCommand("Select " &
        "max(PickupDate) as LastDate from Pickups", cnmd)
        cmdmd.CommandType = CommandType.Text
        Dim drmd As SqlClient.SqlDataReader = cmdmd.ExecuteReader
        If drmd.HasRows Then
            While drmd.Read
                MaxDate = drmd("LastDate")
            End While
        End If
        cnmd.Close()
        cnmd = Nothing
        Return MaxDate
    End Function
    Private Function GetCourier(ByVal RouteID As Integer) As String
        Dim Courier As String = ""
        Dim cngc As New SqlClient.SqlConnection(connString)
        cngc.Open()
        Dim cmdgc As New SqlClient.SqlCommand(
        "Select * from Routes where ID = " & RouteID, cngc)
        cmdgc.CommandType = CommandType.Text
        Dim drgc As SqlClient.SqlDataReader = cmdgc.ExecuteReader
        If drgc.HasRows Then
            While drgc.Read
                If drgc("Courier") IsNot DBNull.Value Then _
                 Courier = drgc("Courier")
            End While
        End If
        cngc.Close()
        cngc = Nothing
        Return Courier
    End Function
    Private Function GetClient(ByVal ClientID As Long) As String
        Dim Provider As String = ""
        Dim cngp As New SqlClient.SqlConnection(connString)
        cngp.Open()
        Dim cmdgp As New SqlClient.SqlCommand( _
        "Select * from Providers where ID = " & ClientID, cngp)
        cmdgp.CommandType = CommandType.Text
        Dim drgp As SqlClient.SqlDataReader = cmdgp.ExecuteReader
        If drgp.HasRows Then
            While drgp.Read
                If drgp("IsIndividual") IsNot DBNull.Value AndAlso drgp("IsIndividual") = 0 Then    'Entity
                    Provider = drgp("LastName_BSN") & ", " & GetAddress(drgp("Address_ID"))
                Else
                    Provider = drgp("LastName_BSN") & ", " & drgp("FirstName") & " " & _
                    GetAddress(drgp("Address_ID"))
                End If
            End While
        End If
        cngp.Close()
        cngp = Nothing
        Return Provider
    End Function
    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTerm.KeyDown
        If e.KeyCode = Keys.Down AndAlso dgvPickups.Rows.Count > 0 Then
            dgvPickups.Focus()
        End If
    End Sub

    Private Sub dgvPickups_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPickups.CellDoubleClick
        Call btnAccept_Click(sender, e)
    End Sub

    Private Sub dgvTests_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvPickups.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            If dgvPickups.CurrentRow IsNot Nothing Then

                Dim rowIndex As Integer = dgvPickups.CurrentRow.Index

                Me.Tag = dgvPickups.Rows(rowIndex).Cells(0).Value.ToString
                Call btnAccept_Click(sender, e)
                Me.txtTerm.Focus()
            End If
        End If
    End Sub
End Class
