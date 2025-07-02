Imports System.Data.SqlClient
Imports System.Text
Imports DataTable = System.Data.DataTable

Public Class frmBARAdhoc
    Private sSQL As String = ""
    Private TF As String = ""
    Private UF As String = ""

    Private Sub frmBARAdhoc_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PopulateFields()
    End Sub

    Private Sub PopulateFields()
        dgvFields.Rows.Clear()
        Dim sSQL As String = "Select 0 as Sel, COLUMN_NAME as Field, COLUMN_NAME as [FIELD NAME], TABLE_NAME as [Table], 'ac' " & _
        "as Tbl from INFORMATION_SCHEMA.COLUMNS where COLUMN_NAME in ('ID', 'EMRNO', 'AccessionDate', 'Received', 'ReceivedTime', " & _
        "'Rejected', 'RejectReason', 'Reported_Final', 'RPT_Status', 'BillingType_ID', 'PrimePayer_ID', 'Comment') and " & _
        "TABLE_NAME = 'Requisitions' order by Ordinal_Position"
        Dim cn1 As New SqlClient.SqlConnection(connString)
        cn1.Open()
        Dim cmd1 As New SqlClient.SqlCommand(sSQL, cn1)
        cmd1.CommandType = CommandType.Text
        Dim dr1 As SqlClient.SqlDataReader = cmd1.ExecuteReader
        If dr1.HasRows Then
            While dr1.Read
                If dr1("field") = "ID" Then
                    dgvFields.Rows.Add(dr1("Sel"), dr1("Tbl") & "." & dr1("field"), "Accession ID", dr1("Table"), dr1("Tbl"))
                Else
                    dgvFields.Rows.Add(dr1("Sel"), dr1("Tbl") & "." & dr1("field"), dr1("FIELD NAME"), dr1("Table"), dr1("Tbl"))
                End If
            End While
        End If
        cn1.Close()
        cn1 = Nothing
        '
        sSQL = "Select 0 as Sel, COLUMN_NAME as Field, COLUMN_NAME as [FIELD NAME], TABLE_NAME as [Table], 'pt' as Tbl " &
        "from INFORMATION_SCHEMA.COLUMNS where COLUMN_NAME in ('ID', 'LastName', 'FirstName', 'MiddleName', 'Sex',  'DOB', " &
        "'HomePhone', 'Email', 'Cell', 'Note') and TABLE_NAME = 'Patients' order by Ordinal_Position "
        Dim cn2 As New SqlClient.SqlConnection(connString)
        cn2.Open()
        Dim cmd2 As New SqlClient.SqlCommand(sSQL, cn2)
        cmd2.CommandType = CommandType.Text
        Dim dr2 As SqlClient.SqlDataReader = cmd2.ExecuteReader
        If dr2.HasRows Then
            While dr2.Read
                If dr2("field") = "ID" Then
                    dgvFields.Rows.Add(dr2("Sel"), dr2("Tbl") & "." & dr2("field"), "Patient ID", dr2("Table"), dr2("Tbl"))
                Else
                    dgvFields.Rows.Add(dr2("Sel"), dr2("Tbl") & "." & dr2("field"), dr2("FIELD NAME"), dr2("Table"), dr2("Tbl"))
                End If
            End While
        End If
        cn2.Close()
        cn2 = Nothing
        '
        sSQL = "Select 0 as Sel, COLUMN_NAME as Field, COLUMN_NAME as [FIELD NAME], TABLE_NAME as [Table], 'ch' as Tbl " &
        "from INFORMATION_SCHEMA.COLUMNS where COLUMN_NAME in ('ID', 'ArType', 'Ar_ID', 'BillReason', 'GrossAmount', " &
        "'Bill_Date', 'Svc_Date') and TABLE_NAME = 'Charges' order by Ordinal_Position"
        Dim cn3 As New SqlClient.SqlConnection(connString)
        cn3.Open()
        Dim cmd3 As New SqlClient.SqlCommand(sSQL, cn3)
        cmd3.CommandType = CommandType.Text
        Dim dr3 As SqlClient.SqlDataReader = cmd3.ExecuteReader
        If dr3.HasRows Then
            While dr3.Read
                If dr3("field") = "ID" Then
                    dgvFields.Rows.Add(dr3("Sel"), dr3("Tbl") & "." & dr3("field"), "Invoice ID", dr3("Table"), dr3("Tbl"))
                Else
                    dgvFields.Rows.Add(dr3("Sel"), dr3("Tbl") & "." & dr3("field"), dr3("FIELD NAME"), dr3("Table"), dr3("Tbl"))
                End If
            End While
        End If
        cn3.Close()
        cn3 = Nothing
        '
        sSQL = "Select 0 as Sel, COLUMN_NAME as Field, COLUMN_NAME as [FIELD NAME], TABLE_NAME as [Table], 'pd' as Tbl " &
        "from INFORMATION_SCHEMA.COLUMNS where COLUMN_NAME in ('PayerClaimID', 'ChargeAmount', 'AppliedAmount', " &
        "'WrittenOff', 'Rem_Code') and TABLE_NAME = 'payment_detail' order by Ordinal_Position"
        Dim cn4 As New SqlClient.SqlConnection(connString)
        cn4.Open()
        Dim cmd4 As New SqlClient.SqlCommand(sSQL, cn4)
        cmd4.CommandType = CommandType.Text
        Dim dr4 As SqlClient.SqlDataReader = cmd4.ExecuteReader
        If dr4.HasRows Then
            While dr4.Read
                dgvFields.Rows.Add(dr4("Sel"), dr4("field"), dr4("FIELD NAME"), dr4("Table"), dr4("Tbl"))
            End While
        End If
        cn4.Close()
        cn4 = Nothing
        '
        sSQL = "Select 0 as Sel, COLUMN_NAME as Field, COLUMN_NAME as [FIELD NAME], TABLE_NAME as [Table], 'pm' as Tbl " &
        "from INFORMATION_SCHEMA.COLUMNS where COLUMN_NAME in ('ID', 'DocNo', 'PaymentDate', 'Amount', 'UnApplied') and " &
        "TABLE_NAME = 'Payments' order by Ordinal_Position"
        Dim cn5 As New SqlClient.SqlConnection(connString)
        cn5.Open()
        Dim cmd5 As New SqlClient.SqlCommand(sSQL, cn5)
        cmd5.CommandType = CommandType.Text
        Dim dr5 As SqlClient.SqlDataReader = cmd5.ExecuteReader
        If dr5.HasRows Then
            While dr5.Read
                If dr5("field") = "ID" Then
                    dgvFields.Rows.Add(dr5("Sel"), dr5("Tbl") & "." & dr5("field"), "Payment ID", dr5("Table"), dr5("Tbl"))
                Else
                    dgvFields.Rows.Add(dr5("Sel"), dr5("Tbl") & "." & dr5("field"), dr5("FIELD NAME"), dr5("Table"), dr5("Tbl"))
                End If
            End While
        End If
        cn5.Close()
        cn5 = Nothing
    End Sub

    Private Sub dgvFields_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvFields.CellContentClick
        If e.ColumnIndex = 0 Then   'Sel
            If dgvFields.Rows(e.RowIndex).Cells(0).Value = False Then
                dgvFields.Rows(e.RowIndex).Cells(0).Value = True
            Else
                dgvFields.Rows(e.RowIndex).Cells(0).Value = False
            End If
            ComposeSQLFields()
        End If
    End Sub

    'Private Sub ComposeSQLFields()
    '    Dim sqlTables As String = ""
    '    sSQL = "Select"
    '    For i As Integer = 0 To dgvFields.RowCount - 1
    '        If dgvFields.Rows(i).Cells(0).Value = True Then
    '            If dgvFields.Rows(i).Cells(4).Value <> "pd" Then
    '                sSQL += " " & dgvFields.Rows(i).Cells(1).Value & " as [" & dgvFields.Rows(i).Cells(2).Value & "], "
    '                If Not sqlTables.Contains(dgvFields.Rows(i).Cells(3).Value & " " & dgvFields.Rows(i).Cells(4).Value &
    '                ", ") Then sqlTables += dgvFields.Rows(i).Cells(3).Value & " " & dgvFields.Rows(i).Cells(4).Value & ", "
    '            Else
    '                If dgvFields.Rows(i).Cells(1).Value = "ChargeAmount" _
    '                OrElse dgvFields.Rows(i).Cells(1).Value = "AppliedAmount" _
    '                OrElse dgvFields.Rows(i).Cells(1).Value = "WrittenOff" Then
    '                    sSQL += " (Select sum(" & dgvFields.Rows(i).Cells(1).Value & ") from " & dgvFields.Rows(i).Cells(3).Value &
    '                    " where Charge_ID = ch.ID) as [" & dgvFields.Rows(i).Cells(2).Value & "], "
    '                    If Not sqlTables.Contains(dgvFields.Rows(i).Cells(3).Value & " " & dgvFields.Rows(i).Cells(4).Value &
    '                    ", ") Then sqlTables += dgvFields.Rows(i).Cells(3).Value & " " & dgvFields.Rows(i).Cells(4).Value & ", "
    '                Else
    '                    sSQL += " " & dgvFields.Rows(i).Cells(1).Value & " as [" & dgvFields.Rows(i).Cells(2).Value & "], "
    '                    If Not sqlTables.Contains(dgvFields.Rows(i).Cells(3).Value & " " & dgvFields.Rows(i).Cells(4).Value &
    '                    ", ") Then sqlTables += dgvFields.Rows(i).Cells(3).Value & " " & dgvFields.Rows(i).Cells(4).Value & ", "
    '                End If
    '            End If
    '        End If
    '    Next
    '    If sSQL.EndsWith(", ") Then sSQL = sSQL.Substring(0, Len(sSQL) - 2)
    '    If sqlTables.EndsWith(", ") Then sqlTables = sqlTables.Substring(0, Len(sqlTables) - 2)
    '    If sqlTables <> "" Then
    '        PopulateDateTypes(sqlTables)
    '        PopulateIdenTypes(sqlTables)
    '        sSQL += " from " & sqlTables
    '        If sqlTables.Contains(", ") Then    'multiple tables
    '            TF = ""
    '            If sqlTables.Contains(" ac") And sqlTables.Contains(" pt") _
    '            And sqlTables.Contains(" ch") And sqlTables.Contains(" pd") _
    '            And sqlTables.Contains(" pm") Then  'all 5 tables, ac, pt, ch, pd and pm
    '                If TF = "" Then TF = " where "
    '                TF += "ac.Patient_ID = pt.ID and ac.ID = ch.Accession_ID " &
    '                " and ch.ID = pd.Charge_ID and pd.Payment_ID = pm.ID"
    '            ElseIf sqlTables.Contains(" ac") And sqlTables.Contains(" pt") _
    '            And sqlTables.Contains(" ch") And sqlTables.Contains(" pd") _
    '            And Not sqlTables.Contains(" pm") Then  'ac, pt, ch, pd
    '                If TF = "" Then TF = " where "
    '                TF += "ac.Patient_ID = pt.ID and ac.ID = " &
    '                "ch.Accession_ID and ch.ID = pd.Charge_ID"
    '            ElseIf sqlTables.Contains(" ac") And sqlTables.Contains(" pt") _
    '            And sqlTables.Contains(" ch") And Not sqlTables.Contains(" pd") _
    '            And Not sqlTables.Contains(" pm") Then  'ac, pt, ch
    '                If TF = "" Then TF = " where "
    '                TF += "ac.Patient_ID = pt.ID and ac.ID = ch.Accession_ID"
    '            ElseIf sqlTables.Contains(" ac") And sqlTables.Contains(" pt") _
    '            And Not sqlTables.Contains(" ch") And Not sqlTables.Contains(" pd") _
    '            And Not sqlTables.Contains(" pm") Then  'ac, pt
    '                If TF = "" Then TF = " where "
    '                TF += "ac.Patient_ID = pt.ID"
    '            ElseIf sqlTables.Contains(" ac") And Not sqlTables.Contains(" pt") _
    '            And sqlTables.Contains(" ch") And sqlTables.Contains(" pd") _
    '            And Not sqlTables.Contains(" pm") Then  'ac, ch, pd
    '                If TF = "" Then TF = " where "
    '                TF += "ac.ID = ch.Accession_ID"
    '            ElseIf Not sqlTables.Contains(" ac") And Not sqlTables.Contains(" pt") _
    '            And sqlTables.Contains(" ch") And sqlTables.Contains(" pd") _
    '            And Not sqlTables.Contains(" pm") Then  'ch, pd
    '                If TF = "" Then TF = " where "
    '                TF += "ch.ID = pd.Charge_ID"
    '            ElseIf Not sqlTables.Contains(" ac") And Not sqlTables.Contains(" pt") _
    '            And sqlTables.Contains(" ch") And sqlTables.Contains(" pd") _
    '            And sqlTables.Contains(" pm") Then  'ch, pd, pm
    '                If TF = "" Then TF = " where "
    '                TF += "ch.ID = pd.Charge_ID and pd.Payment_ID = pm.ID"
    '            ElseIf sqlTables.Contains(" ac") And Not sqlTables.Contains(" pt") _
    '            And sqlTables.Contains(" ch") And Not sqlTables.Contains(" pd") _
    '            And Not sqlTables.Contains(" pm") Then  'ac, ch
    '                If TF = "" Then TF = " where "
    '                TF += "ac.ID = ch.Accession_ID"
    '            ElseIf sqlTables.Contains(" ac") And Not sqlTables.Contains(" pt") _
    '            And sqlTables.Contains(" ch") And sqlTables.Contains(" pd") _
    '            And sqlTables.Contains(" pm") Then  'ac, ch, pd, pm
    '                If TF = "" Then TF = " where "
    '                TF += "ac.ID = ch.Accession_ID and ch.ID = pd.Charge_ID " &
    '                "and pd.Payment_ID = pm.ID"
    '            End If
    '            '
    '            If TF.EndsWith(", ") Then TF = TF.Substring(0, Len(TF) - 2)
    '            If TF <> "" Then sSQL += TF
    '        End If
    '    Else
    '        sSQL = ""
    '    End If
    '    UpdateUF()
    '    If sSQL <> "" AndAlso UF <> "" Then sSQL += UF
    '    txtSQL.Text = sSQL
    '    If sSQL <> "" Then btnReset.Enabled = True
    'End Sub
    Private Sub ComposeSQLFields()
        Dim sqlTables As New HashSet(Of String)
        Dim sbSQL As New StringBuilder("SELECT ")
        Dim conditions As New List(Of String)

        For Each row As DataGridViewRow In dgvFields.Rows
            If Convert.ToBoolean(row.Cells(0).Value) Then
                Dim fieldName As String = row.Cells(1).Value.ToString()
                Dim aliasName As String = row.Cells(2).Value.ToString()
                Dim tableName As String = row.Cells(3).Value.ToString()
                Dim tableAlias As String = row.Cells(4).Value.ToString()

                If tableAlias <> "pd" Then
                    sbSQL.Append($" {fieldName} AS [{aliasName}], ")
                    sqlTables.Add($"{tableName} {tableAlias}")
                Else
                    If {"ChargeAmount", "AppliedAmount", "WrittenOff"}.Contains(fieldName) Then
                        sbSQL.Append($" (SELECT SUM({fieldName}) FROM {tableName} WHERE Charge_ID = ch.ID) AS [{aliasName}], ")
                    Else
                        sbSQL.Append($" {fieldName} AS [{aliasName}], ")
                    End If
                    sqlTables.Add($"{tableName} {tableAlias}")
                End If
            End If
        Next

        ' Remove trailing comma
        If sbSQL.ToString().EndsWith(", ") Then sbSQL.Length -= 2

        ' Construct table joins
        If sqlTables.Count > 0 Then
            sbSQL.Append(" FROM " & String.Join(", ", sqlTables))

            ' Generate dynamic table conditions
            If sqlTables.Contains("ac") And sqlTables.Contains("pt") Then conditions.Add("ac.Patient_ID = pt.ID")
            If sqlTables.Contains("ac") And sqlTables.Contains("ch") Then conditions.Add("ac.ID = ch.Accession_ID")
            If sqlTables.Contains("ch") And sqlTables.Contains("pd") Then conditions.Add("ch.ID = pd.Charge_ID")
            If sqlTables.Contains("pd") And sqlTables.Contains("pm") Then conditions.Add("pd.Payment_ID = pm.ID")

            If conditions.Count > 0 Then sbSQL.Append(" WHERE " & String.Join(" AND ", conditions))
        Else
            sbSQL.Clear()
        End If

        ' Add any additional filters from UpdateUF()
        UpdateUF()
        If sbSQL.Length > 0 AndAlso Not String.IsNullOrEmpty(UF) Then sbSQL.Append(UF)

        txtSQL.Text = sbSQL.ToString()
        btnReset.Enabled = sbSQL.Length > 0
    End Sub

    Private Sub UpdateUF()
        UF = ""
        If IsDate(txtDateFrom.Text) AndAlso IsDate(txtDateTo.Text) Then
            If cmbDateType.SelectedItem.ToString.StartsWith("Accession") Then
                If TF <> "" Then
                    UF += " and ac.AccessionDate between '" & txtDateFrom.Text & "' and '" & txtDateTo.Text & " 23:59:00'"
                Else
                    UF += " where ac.AccessionDate between '" & txtDateFrom.Text & "' and '" & txtDateTo.Text & " 23:59:00'"
                End If
            ElseIf cmbDateType.SelectedItem.ToString.StartsWith("Receive") Then
                If TF <> "" Then
                    UF += " and ac.ReceivedTime between '" & txtDateFrom.Text & "' and '" & txtDateTo.Text & " 23:59:00'"
                Else
                    UF += " where ac.ReceivedTime between '" & txtDateFrom.Text & "' and '" & txtDateTo.Text & " 23:59:00'"
                End If
            ElseIf cmbDateType.SelectedItem.ToString.StartsWith("Reported") Then
                If TF <> "" Then
                    UF += " and ac.Reported_Final between '" & txtDateFrom.Text & "' and '" & txtDateTo.Text & " 23:59:00'"
                Else
                    UF += " where ac.Reported_Final between '" & txtDateFrom.Text & "' and '" & txtDateTo.Text & " 23:59:00'"
                End If
            ElseIf cmbDateType.SelectedItem.ToString.StartsWith("Bill") Then
                If TF <> "" Then
                    UF += " and ch.Bill_Date between '" & txtDateFrom.Text & "' and '" & txtDateTo.Text & " 23:59:00'"
                Else
                    UF += " where ch.Bill_Date between '" & txtDateFrom.Text & "' and '" & txtDateTo.Text & " 23:59:00'"
                End If
            ElseIf cmbDateType.SelectedItem.ToString.StartsWith("Service") Then
                If TF <> "" Then
                    UF += " and ch.Svc_Date between '" & txtDateFrom.Text & "' and '" & txtDateTo.Text & " 23:59:00'"
                Else
                    UF += " where ch.Svc_Date between '" & txtDateFrom.Text & "' and '" & txtDateTo.Text & " 23:59:00'"
                End If
            ElseIf cmbDateType.SelectedItem.ToString.StartsWith("Payment") Then
                If TF <> "" Then
                    UF += " and pm.PaymentDate between '" & txtDateFrom.Text & "' and '" & txtDateTo.Text & " 23:59:00'"
                Else
                    UF += " where pm.PaymentDate between '" & txtDateFrom.Text & "' and '" & txtDateTo.Text & " 23:59:00'"
                End If
            End If
        ElseIf txtIDFrom.Text <> "" And txtIDTo.Text = "" Then
            If cmbIdentype.SelectedItem.ToString.StartsWith("Accession") Then
                If TF <> "" Then
                    UF += " and ac.ID = " & txtIDFrom.Text
                Else
                    UF += " where ac.ID = " & txtIDFrom.Text
                End If
            ElseIf cmbIdentype.SelectedItem.ToString.StartsWith("Invoice") Then
                If TF <> "" Then
                    UF += " and ch.ID = " & txtIDFrom.Text
                Else
                    UF += " where ch.ID = " & txtIDFrom.Text
                End If
            ElseIf cmbIdentype.SelectedItem.ToString.StartsWith("Document") Then
                If TF <> "" Then
                    UF += " and pm.DocNo = '" & txtIDFrom.Text & "'"
                Else
                    UF += " where pm.DocNo = '" & txtIDFrom.Text & "'"
                End If
            End If
        ElseIf txtIDFrom.Text = "" And txtIDTo.Text <> "" Then
            If cmbIdentype.SelectedItem.ToString.StartsWith("Accession") Then
                If TF <> "" Then
                    UF += " and ac.ID = " & txtIDTo.Text
                Else
                    UF += " where ac.ID = " & txtIDTo.Text
                End If
            ElseIf cmbIdentype.SelectedItem.ToString.StartsWith("Invoice") Then
                If TF <> "" Then
                    UF += " and ch.ID = " & txtIDTo.Text
                Else
                    UF += " where ch.ID = " & txtIDTo.Text
                End If
            ElseIf cmbIdentype.SelectedItem.ToString.StartsWith("Document") Then
                If TF <> "" Then
                    UF += " and pm.DocNo = '" & txtIDTo.Text & "'"
                Else
                    UF += " where pm.DocNo = '" & txtIDTo.Text & "'"
                End If
            End If
        ElseIf txtIDFrom.Text <> "" And txtIDTo.Text <> "" Then
            If cmbIdentype.SelectedItem.ToString.StartsWith("Accession") Then
                If TF <> "" Then
                    UF += " and ac.ID between " & txtIDFrom.Text & " and " & txtIDTo.Text
                Else
                    UF += " where ac.ID between " & txtIDFrom.Text & " and " & txtIDTo.Text
                End If
            ElseIf cmbIdentype.SelectedItem.ToString.StartsWith("Invoice") Then
                If TF <> "" Then
                    UF += " and ch.ID between " & txtIDFrom.Text & " and " & txtIDTo.Text
                Else
                    UF += " where ch.ID between " & txtIDFrom.Text & " and " & txtIDTo.Text
                End If
            End If
        End If
        If sSQL <> "" AndAlso UF <> "" Then sSQL += UF
        txtSQL.Text = sSQL
    End Sub

    Private Sub PopulateDateTypes(ByVal sqlTables As String)
        cmbDateType.Items.Clear()
        If sqlTables.Contains(" ac") And sqlTables.Contains(" ch") And sqlTables.Contains(" pm") Then
            cmbDateType.Items.Add("Accession")
            cmbDateType.Items.Add("Bill")
            cmbDateType.Items.Add("Payment")
            cmbDateType.Items.Add("Receive")
            cmbDateType.Items.Add("Reported Final")
            cmbDateType.Items.Add("Service")
            cmbDateType.SelectedIndex = 3
        ElseIf sqlTables.Contains(" ac") And Not sqlTables.Contains(" ch") And Not sqlTables.Contains(" pm") Then
            cmbDateType.Items.Add("Accession")
            cmbDateType.Items.Add("Receive")
            cmbDateType.Items.Add("Reported Final")
            cmbDateType.SelectedIndex = 1
        ElseIf sqlTables.Contains(" ac") And sqlTables.Contains(" ch") And Not sqlTables.Contains(" pm") Then
            cmbDateType.Items.Add("Accession")
            cmbDateType.Items.Add("Bill")
            cmbDateType.Items.Add("Receive")
            cmbDateType.Items.Add("Reported Final")
            cmbDateType.Items.Add("Service")
            cmbDateType.SelectedIndex = 2
        ElseIf Not sqlTables.Contains(" ac") And sqlTables.Contains(" ch") And sqlTables.Contains(" pm") Then
            cmbDateType.Items.Add("Bill")
            cmbDateType.Items.Add("Payment")
            cmbDateType.Items.Add("Service")
            cmbDateType.SelectedIndex = 2
        ElseIf Not sqlTables.Contains(" ac") And sqlTables.Contains(" ch") And Not sqlTables.Contains(" pm") Then
            cmbDateType.Items.Add("Bill")
            cmbDateType.Items.Add("Service")
            cmbDateType.SelectedIndex = 1
        ElseIf Not sqlTables.Contains(" ac") And Not sqlTables.Contains(" ch") And sqlTables.Contains(" pm") Then
            cmbDateType.Items.Add("Payment")
            cmbDateType.SelectedIndex = 0
        End If
    End Sub

    Private Sub PopulateIdenTypes(ByVal sqlTables As String)
        cmbIdentype.Items.Clear()
        If sqlTables.Contains(" ac") And sqlTables.Contains(" ch") And sqlTables.Contains(" pm") Then
            cmbIdentype.Items.Add("Accession")
            cmbIdentype.Items.Add("Invoice")
            cmbIdentype.Items.Add("Document")
            cmbIdentype.SelectedIndex = 0
        ElseIf sqlTables.Contains(" ac") And Not sqlTables.Contains(" ch") And Not sqlTables.Contains(" pm") Then
            cmbIdentype.Items.Add("Accession")
            cmbIdentype.SelectedIndex = 0
        ElseIf sqlTables.Contains(" ac") And sqlTables.Contains(" ch") And Not sqlTables.Contains(" pm") Then
            cmbIdentype.Items.Add("Accession")
            cmbIdentype.Items.Add("Invoice")
            cmbIdentype.SelectedIndex = 0
        ElseIf Not sqlTables.Contains(" ac") And sqlTables.Contains(" ch") And sqlTables.Contains(" pm") Then
            cmbIdentype.Items.Add("Invoice")
            cmbIdentype.Items.Add("Document")
            cmbIdentype.SelectedIndex = 0
        ElseIf Not sqlTables.Contains(" ac") And sqlTables.Contains(" ch") And Not sqlTables.Contains(" pm") Then
            cmbIdentype.Items.Add("Invoice")
            cmbIdentype.SelectedIndex = 0
        ElseIf Not sqlTables.Contains(" ac") And Not sqlTables.Contains(" ch") And sqlTables.Contains(" pm") Then
            cmbIdentype.Items.Add("Document")
            cmbIdentype.SelectedIndex = 0
        End If
    End Sub

    Private Sub txtDateTo_Validated(sender As Object, e As EventArgs) Handles txtDateTo.Validated
        If IsDate(txtDateTo.Text) Then
            txtIDFrom.Text = ""
            txtIDTo.Text = ""
            If IsDate(txtDateFrom.Text) AndAlso IsDate(txtDateTo.Text) Then UpdateUF()
        End If
    End Sub

    Private Sub txtIDFrom_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIDFrom.KeyPress
        If Not cmbIdentype.SelectedText.StartsWith("Doc") Then
            Numerals(txtIDFrom, e)
        End If
    End Sub

    Private Sub txtIDFrom_Validated(sender As Object, e As EventArgs) Handles txtIDFrom.Validated
        If txtIDFrom.Text <> "" Then
            txtDateFrom.Text = ""
            txtDateTo.Text = ""
            UpdateUF()
        End If
    End Sub

    Private Sub txtSQL_TextChanged(sender As Object, e As EventArgs) Handles txtSQL.TextChanged
        If txtSQL.Text <> "" Then
            btnExecute.Enabled = True
        Else
            btnExecute.Enabled = False
        End If
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        For i As Integer = 0 To dgvFields.RowCount - 1
            dgvFields.Rows(i).Cells(0).Value = False
        Next
        sSQL = ""
        UF = ""
        TF = ""
        txtDateFrom.Text = ""
        txtDateTo.Text = ""
        txtIDFrom.Text = ""
        txtIDTo.Text = ""
        cmbDateType.Items.Clear()
        cmbDateType.SelectedIndex = -1
        cmbIdentype.Items.Clear()
        cmbIdentype.SelectedIndex = -1
        Dim dt As New DataTable
        dgvResult.DataSource = dt
        lblStatus.Text = ""
        btnPrint.Enabled = False
        txtSQL.Text = ""
        btnReset.Enabled = False
    End Sub

    Private Sub txtDateFrom_Validated(sender As Object, e As EventArgs) Handles txtDateFrom.Validated
        If IsDate(txtDateFrom.Text) Then
            txtIDFrom.Text = ""
            txtIDTo.Text = ""
        End If
    End Sub

    Private Sub txtIDTo_Validated(sender As Object, e As EventArgs) Handles txtIDTo.Validated
        If txtIDTo.Text <> "" Then
            txtDateFrom.Text = ""
            txtDateTo.Text = ""
            UpdateUF()
        End If
    End Sub

    Private Sub cmbIdentype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbIdentype.SelectedIndexChanged
        If cmbIdentype.SelectedText.StartsWith("Doc") Then
            txtIDTo.Text = ""
            txtIDTo.Enabled = False
        Else
            txtIDTo.Enabled = True
        End If
    End Sub

    Private Sub btnExecute_Click(sender As Object, e As EventArgs) Handles btnExecute.Click
        Dim dt As New DataTable
        Using cnn As New SqlConnection(connString)
            Dim cmd As New SqlCommand(sSQL, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            Using dad As New SqlDataAdapter(cmd)
                dad.Fill(dt)
            End Using
            cnn.Close()
        End Using
        dgvResult.DataSource = dt
        lblStatus.Text = "Rows: " & dt.Rows.Count
        'writeData(dt)
        If dt.Rows.Count > 0 Then
            btnPrint.Enabled = True
            'btnExport.Enabled = True
        Else
            btnPrint.Enabled = False
        End If
    End Sub
End Class