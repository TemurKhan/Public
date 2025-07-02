Imports System.Windows.Forms
Imports System.Data.OleDb
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmDxCopy

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub frmDxCopy_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PopulateDBTables()
        cmbDelimiter.SelectedIndex = 0
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub PopulateDBTables()
        cmbTables.Items.Clear()

        Using connection As New SqlConnection(connString)
            connection.Open()

            ' Retrieve schema information about the tables
            Dim tablesSchema As DataTable = connection.GetSchema("Tables")

            For Each row As DataRow In tablesSchema.Rows
                Dim tableName As String = row("TABLE_NAME").ToString()
                cmbTables.Items.Add(tableName)
            Next
        End Using
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If txtFile.Text <> "" And cmbDelimiter.SelectedIndex <> -1 Then
            Me.Tag = txtFile.Text & "|" & cmbDelimiter.SelectedIndex.ToString
            Dim i As Integer
            For i = 0 To dgvFields.RowCount - 1
                If dgvFields.Rows(i).Cells(0).Value <> "" And dgvFields.Rows(i).Cells(1).Value <> "" Then
                    Me.Tag += "|" & dgvFields.Rows(i).Cells(0).Value & "^" & Microsoft.VisualBasic.Left(dgvFields.Rows(i).Cells(1).Value, 1)
                End If
            Next
            Me.Tag += "|" & cmbTables.SelectedItem.ToString
            Me.Close()
        End If
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        If txtFile.Text <> "" And cmbDelimiter.SelectedIndex <> -1 _
        AndAlso cmbTables.SelectedIndex <> -1 Then
            Dim Delim As String
            Dim cntb As New SqlConnection(connString)
            cntb.Open()
            Dim cmdtb As New SqlCommand("Select * from " & cmbTables.SelectedItem.ToString, cntb)
            cmdtb.CommandType = CommandType.Text
            Dim drtb As SqlDataReader = cmdtb.ExecuteReader
            If cmbDelimiter.SelectedIndex = 0 Then
                Delim = ","
            ElseIf cmbDelimiter.SelectedIndex = 1 Then
                Delim = Chr(9)
            ElseIf cmbDelimiter.SelectedIndex = 2 Then
                Delim = "|"
            Else
                Delim = vbCrLf
            End If
            Dim SR As New System.IO.StreamReader(txtFile.Text)
            Dim Data As String = SR.ReadLine
            Dim Fields() As String = Data.Split(Delim)
            dgvFields.Rows.Clear()
            For i As Integer = 0 To drtb.VisibleFieldCount - 1
                Dim fldSource As New DataGridViewComboBoxCell
                'fldSource.Items.Clear()
                For n As Integer = 0 To Fields.Length - 1
                    fldSource.Items.Add(n.ToString & " = " & Fields(n).ToString)
                Next
                dgvFields.Rows.Add(drtb.GetName(i), "")
                dgvFields.Rows(dgvFields.RowCount - 1).Cells(1) = fldSource
                fldSource = Nothing
            Next
            SR.Close()
            SR = Nothing
            cntb.Close()
            cntb = Nothing
        End If
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Dim OFD As New OpenFileDialog
        OFD.Title = "Delimited Text file"
        If OFD.ShowDialog = DialogResult.OK Then
            txtFile.Text = OFD.FileName
            cmbTables.SelectedIndex = -1
        Else
            txtFile.Text = ""
        End If
        Update_Progress()
    End Sub

    Private Sub Update_Progress()
        If txtFile.Text <> "" And cmbDelimiter.SelectedIndex <> -1 Then
            Dim i As Integer
            Dim Selected As Boolean = True
            For i = 0 To dgvFields.RowCount - 1
                If dgvFields.Rows(i).Cells(0).Value = "" Or dgvFields.Rows(i).Cells(1).Value = "" Then Selected = False
            Next
            btnOK.Enabled = Selected
        End If
    End Sub

    Private Sub cmbDelimiter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDelimiter.SelectedIndexChanged
        If cmbDelimiter.SelectedIndex <> -1 Then Update_Progress()
    End Sub

    Private Sub dgvFields_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvFields.CellValueChanged
        Update_Progress()
    End Sub

    Private Sub dgvFields_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvFields.DataError
        On Error Resume Next
    End Sub

End Class
