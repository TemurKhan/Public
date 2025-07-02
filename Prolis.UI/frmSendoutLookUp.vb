Imports System.Windows.Forms
Imports System.data

Public Class frmSendoutLookUp

    Private Sub frmSendoutLookUp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpDate.Value = Date.Today
        PopulateLabs()
        txtAccID.Clear()
        dgv.Rows.Clear()
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
        txtAccID.Focus()

    End Sub

    Private Sub PopulateLabs()
        cmbLab.Items.Clear()
        cmbLab.Items.Add(New MyList("Any Laboratory", 0))
        Dim sSQL As String = "Select * from Labs"
        If connString <> "" Then
            Dim cnpl As New Data.SqlClient.SqlConnection(connString)
            cnpl.Open()
            Dim cmdpl As New Data.SqlClient.SqlCommand(sSQL, cnpl)
            cmdpl.CommandType = Data.CommandType.Text
            Dim drpl As Data.SqlClient.SqlDataReader = cmdpl.ExecuteReader
            If drpl.HasRows Then
                While drpl.Read
                    cmbLab.Items.Add(New MyList(drpl("LabName") & IIf(drpl("IsPrimary") = 0, "", " *PRIMARY*"), drpl("ID")))
                End While
            End If
            cnpl.Close()
            cnpl = Nothing
            'Else
            '    Dim cnpl As New Odbc.OdbcConnection(connstring)
            '    cnpl.Open()
            '    Dim cmdpl As New Odbc.OdbcCommand(sSQL, cnpl)
            '    cmdpl.CommandType = Data.CommandType.Text
            '    Dim drpl As Odbc.OdbcDataReader = cmdpl.ExecuteReader
            '    If drpl.HasRows Then
            '        While drpl.Read
            '            cmbLab.Items.Add(New MyList(drpl("LabName") & IIf(drpl("IsPrimary") = 0, "", " *PRIMARY*"), drpl("ID")))
            '        End While
            '    End If
            '    cnpl.Close()
            '    cnpl = Nothing
        End If
        cmbLab.SelectedIndex = 0
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click, btn_Cancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If dgv.SelectedRows.Count > 0 Then

            Me.Tag = dgv.SelectedRows(0).Cells(0).Value
        End If

        Me.Close()
    End Sub

    Private Sub dgvSendouts_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellClick
        If e.RowIndex <> -1 Then
            Me.Tag = dgv.Rows(e.RowIndex).Cells(0).Value.ToString
            btnOK.Enabled = True
        End If
    End Sub

    Private Sub btnLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLook.Click
        Me.Cursor = Cursors.WaitCursor

        Dim sSQL As String = ""
        Dim ItemX As MyList = cmbLab.SelectedItem
        If txtAccID.Text = "" And cmbLab.SelectedIndex = 0 Then 'Only Date
            sSQL = "Select * from Sendouts where SentDate between '" & Format(DateAdd(DateInterval.Day,
            -SystemConfig.SendoutSpan, dtpDate.Value), SystemConfig.DateFormat) & "' and '" & Format(dtpDate.Value,
            SystemConfig.DateFormat) & " 23:59:00' order by Accession_ID"
        ElseIf txtAccID.Text <> "" And cmbLab.SelectedIndex = 0 Then 'AccID
            sSQL = "Select * from Sendouts where Accession_ID = " & Val(txtAccID.Text)
        ElseIf txtAccID.Text <> "" And cmbLab.SelectedIndex > 0 Then 'AccID and Lab
            sSQL = "Select * from Sendouts where Accession_ID = " & Val(txtAccID.Text) &
            " and Lab_ID = " & ItemX.ItemData
        ElseIf txtAccID.Text = "" And cmbLab.SelectedIndex > 0 Then 'Lab and date
            sSQL = "Select * from Sendouts where Lab_ID = " & ItemX.ItemData & " and SentDate " &
            "between '" & Format(DateAdd(DateInterval.Day, -SystemConfig.SendoutSpan, dtpDate.Value),
            SystemConfig.DateFormat) & "' and '" & Format(dtpDate.Value, SystemConfig.DateFormat) & " 23:59:00' order by Accession_ID"
        End If
        dgv.Rows.Clear()
        If sSQL <> "" Then
            If connString <> "" Then
                Dim cnl As New Data.SqlClient.SqlConnection(connString)
                cnl.Open()
                Dim cmdl As New Data.SqlClient.SqlCommand(sSQL, cnl)
                cmdl.CommandType = Data.CommandType.Text
                Dim drl As Data.SqlClient.SqlDataReader = cmdl.ExecuteReader
                If drl.HasRows Then
                    While drl.Read
                        dgv.Rows.Add(drl("ID"), drl("Accession_ID"),
                        Format(drl("SentDate"), SystemConfig.DateFormat), GetLabName(drl("Lab_ID")),
                        GetTestNames(drl("ID")))
                    End While
                End If
                cnl.Close()
                cnl = Nothing
                'Else
                '    Dim cnl As New Odbc.OdbcConnection(connstring)
                '    cnl.Open()
                '    Dim cmdl As New Odbc.OdbcCommand(sSQL, cnl)
                '    cmdl.CommandType = Data.CommandType.Text
                '    Dim drl As Odbc.OdbcDataReader = cmdl.ExecuteReader
                '    If drl.HasRows Then
                '        While drl.Read
                '            dgv.Rows.Add(drl("ID"), drl("Accession_ID"),
                '            Format(drl("SentDate"), SystemConfig.DateFormat), GetLabName(drl("Lab_ID")),
                '            GetTestNames(drl("ID")))
                '        End While
                '    End If
                '    cnl.Close()
                '    cnl = Nothing
            End If
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Function GetLabName(ByVal LabID As Integer) As String
        Dim Lab As String = ""
        Dim sSQL As String = "Select * from Labs where ID = " & LabID
        If connString <> "" Then
            Dim cnln As New Data.SqlClient.SqlConnection(connString)
            cnln.Open()
            Dim cmdln As New Data.SqlClient.SqlCommand(sSQL, cnln)
            cmdln.CommandType = Data.CommandType.Text
            Dim drln As Data.SqlClient.SqlDataReader = cmdln.ExecuteReader
            If drln.HasRows Then
                While drln.Read
                    Lab = drln("LabName")
                End While
            End If
            cnln.Close()
            cnln = Nothing
            'Else
            '    Dim cnln As New Odbc.OdbcConnection(connstring)
            '    cnln.Open()
            '    Dim cmdln As New Odbc.OdbcCommand(sSQL, cnln)
            '    cmdln.CommandType = Data.CommandType.Text
            '    Dim drln As Odbc.OdbcDataReader = cmdln.ExecuteReader
            '    If drln.HasRows Then
            '        While drln.Read
            '            Lab = drln("LabName")
            '        End While
            '    End If
            '    cnln.Close()
            '    cnln = Nothing
        End If
        Return Lab
    End Function

    Private Function GetTestNames(ByVal SendoutID As Long) As String
        Dim Tests As String = ""
        Dim sSQL As String = "Select * from Sendout_TGP where Sendout_ID = " & SendoutID
        If connString <> "" Then
            Dim cntn As New Data.SqlClient.SqlConnection(connString)
            cntn.Open()
            Dim cmdtn As New Data.SqlClient.SqlCommand(sSQL, cntn)
            cmdtn.CommandType = Data.CommandType.Text
            Dim drtn As Data.SqlClient.SqlDataReader = cmdtn.ExecuteReader
            If drtn.HasRows Then
                While drtn.Read
                    Tests += GetTGPName(drtn("TGP_ID")) & ", "
                End While
                Tests = Microsoft.VisualBasic.Mid(Tests, 1, Len(Tests) - 2)
            End If
            cntn.Close()
            cntn = Nothing
            'Else
            '    Dim cntn As New Odbc.OdbcConnection(connstring)
            '    cntn.Open()
            '    Dim cmdtn As New Odbc.OdbcCommand(sSQL, cntn)
            '    cmdtn.CommandType = Data.CommandType.Text
            '    Dim drtn As Odbc.OdbcDataReader = cmdtn.ExecuteReader
            '    If drtn.HasRows Then
            '        While drtn.Read
            '            Tests += GetTGPName(drtn("TGP_ID")) & ", "
            '        End While
            '        Tests = Microsoft.VisualBasic.Mid(Tests, 1, Len(Tests) - 2)
            '    End If
            '    cntn.Close()
            '    cntn = Nothing
        End If
        Return Tests
    End Function

    Private Sub txtAccID_KeyDown(sender As Object, e As KeyEventArgs) Handles txtAccID.KeyDown
        If e.KeyCode = Keys.Down AndAlso dgv.Rows.Count > 0 Then
            dgv.Focus()
        End If
    End Sub

    Private Sub dgv_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellDoubleClick
        Call btnOK_Click(Nothing, Nothing)
    End Sub

    Private Sub dgvTests_KeyDown(sender As Object, e As KeyEventArgs) Handles dgv.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Call btnOK_Click(Nothing, Nothing)
            Me.txtAccID.Focus()
        End If
    End Sub
End Class
