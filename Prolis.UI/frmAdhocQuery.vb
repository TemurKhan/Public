Imports System.Windows.Forms
Imports System.data
Imports System.data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Text
Imports System.IO
Imports Brushes = System.Drawing.Brushes

Public Class frmAdhocQuery
    Public Shared MouseX As Integer
    Public Shared MouseY As Integer
    Dim mRow As Integer = 0
    Dim newPage As Boolean = True

    Private Sub txtSQL_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSQL.TextChanged
        If txtSQL.Text <> "" Then
            btnExecute.Enabled = True
            txtScriptID.ReadOnly = False
        Else
            btnExecute.Enabled = False
            txtScriptID.Text = ""
            txtScriptID.ReadOnly = True
            'dgvResult.Rows.Clear()
            txtResult.Text = ""
        End If
        RunSaveProgress()
    End Sub

    Private Sub QueryProgress()
        If chkGridText.Checked = False Then 'default - grid
            If dgvResult.RowCount = 0 Then
                btnPrint.Enabled = False
            Else
                btnPrint.Enabled = True
            End If
        Else    'text
            If txtResult.Text = "" Then
                btnPrint.Enabled = False
            Else
                btnPrint.Enabled = True
            End If
        End If
    End Sub

    Private Sub chkGridText_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkGridText.Click
        If chkGridText.Checked = False Then
            chkGridText.Checked = True
        Else
            chkGridText.Checked = False
        End If
        If chkGridText.Checked = False Then 'default
            chkGridText.Text = "Grid"
            'chkGridText.Image = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath _
            '& "\Images\Worksheet.ico")

            dgvResult.Visible = True
            txtResult.Visible = False
        Else
            chkGridText.Text = "Text"
            'chkGridText.Image = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath _
            '& "\Images\Ansi835.ico")
            dgvResult.Visible = False
            txtResult.Visible = True
        End If
    End Sub

    Private Sub btnExecute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExecute.Click
        Dim sSQL As String = ""
        If txtSQL.SelectedText.Length > 0 Then
            sSQL = txtSQL.SelectedText
        Else
            sSQL = txtSQL.Text.Trim
        End If
        'sql = sql.Replace("'", "''")
        Try
            If InStr(sSQL, "update") > 0 Or _
            InStr(sSQL, "delete") > 0 Or _
            InStr(sSQL, "insert") > 0 Then
                MsgBox("A statement other than 'select' is not allowed", MsgBoxStyle.Critical, "Prolis")
                txtSQL.Text = ""
            Else
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
                writeData(dt)
                If txtResult.Text <> "" Then
                    btnPrint.Enabled = True
                    btnExport.Enabled = True
                End If
            End If
        Catch Ex As Exception
            MsgBox("An error '" & Ex.Message & "' occured.", MsgBoxStyle.Critical, "Prolis")
        End Try
    End Sub

    Private Sub frmAdhocQuery_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim cs As String = "Dsn=ProlisQC; Database=Labase; UID=prolis;PWD=gujrati"
        'CN = New ADODB.Connection
        'CN.Open(cs)
        txtResult.Visible = False
        dgvResult.Visible = True
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If chkGridText.Checked = False Then
            PrintDocument1.Print()
        Else
            If txtResult.Text <> "" Then
                Printer.Print(txtResult.Text)
            End If
        End If
    End Sub

    Public Shared Function PrintTable(ByVal dt As DataTable) As String
        Dim dtReader As DataTableReader = dt.CreateDataReader()
        Dim result As New StringBuilder()
        While dtReader.Read()
            For i As Integer = 0 To dtReader.FieldCount - 1
                result.AppendFormat("{0} = {1}", dtReader.GetName(i).Trim(), dtReader.GetValue(i).ToString().Trim())
            Next
            result.AppendLine()
        End While
        dtReader.Close()
        Return result.ToString()
    End Function

    Sub writeData(ByVal dt As DataTable)
        Dim data As New StringBuilder
        Dim hdr As String = ""
        For Each column As DataColumn In dt.Columns
            hdr += column.ColumnName.ToString & Chr(9)
        Next
        If hdr.EndsWith(Chr(9)) Then hdr = Microsoft.VisualBasic.Mid(hdr, 1, Len(hdr) - 1)
        data.AppendLine(hdr)
        For i As Integer = 0 To dt.Rows.Count - 1
            Dim rowstr As String = ""
            For j As Integer = 0 To dt.Columns.Count - 1
                rowstr += dt.Rows(i)(j).ToString & Chr(9)
            Next
            If rowstr.EndsWith(Chr(9)) Then rowstr = Microsoft.VisualBasic.Mid(rowstr, 1, Len(rowstr) - 1)
            data.AppendLine(rowstr)
        Next
        txtResult.Text = data.ToString
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        newPage = True
        With dgvResult
            Dim fmt As StringFormat = New StringFormat(StringFormatFlags.LineLimit)
            fmt.LineAlignment = StringAlignment.Center
            fmt.Trimming = StringTrimming.EllipsisCharacter
            Dim y As Single = e.MarginBounds.Top
            Do While mRow < .RowCount
                Dim row As DataGridViewRow = .Rows(mRow)
                Dim x As Single = e.MarginBounds.Left
                Dim h As Single = 0
                For Each cell As DataGridViewCell In row.Cells
                    Dim rc As RectangleF = New RectangleF(x, y, cell.Size.Width, cell.Size.Height)
                    e.Graphics.DrawRectangle(Pens.Black, rc.Left, rc.Top, rc.Width, rc.Height)
                    If (newPage) Then
                        e.Graphics.DrawString(dgvResult.Columns(cell.ColumnIndex).HeaderText, .Font, Brushes.Black, rc, fmt)
                    Else
                        e.Graphics.DrawString(dgvResult.Rows(cell.RowIndex).Cells(cell.ColumnIndex).FormattedValue.ToString(), .Font, Brushes.Black, rc, fmt)
                    End If
                    x += rc.Width
                    h = Math.Max(h, rc.Height)
                Next
                If newPage = False Then mRow += 1
                newPage = False
                y += h
                If y + h > e.MarginBounds.Bottom Then
                    e.HasMorePages = True
                    mRow -= 1
                    newPage = True
                    Exit Sub
                End If
            Loop
            mRow = 0
        End With
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim FileName As String
        If System.Windows.Forms.DialogResult.OK = SaveFileDialog1.ShowDialog() Then
            FileName = SaveFileDialog1.FileName
            Dim SW As New IO.StreamWriter(FileName)
            SW.Write(txtResult.Text)
            SW.Close()
            SW = Nothing
        End If
    End Sub

    Private Sub txtScriptID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtScriptID.TextChanged
        RunSaveProgress()
    End Sub

    Private Sub RunSaveProgress()
        If txtSQL.Text <> "" And txtScriptID.Text <> "" Then
            btnSave.Enabled = True
        Else
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sSQL As String = ""
        'Dim Script As String = CommentFreeSQL(Trim(txtSQL.Text))
        If Trim(txtScriptID.Text) <> "" And Trim(txtSQL.Text) <> "" Then
            sSQL = "If Exists (Select * from Scripts where Identifier = '" & Replace(Trim(txtScriptID.Text), "'", "''") & "') " & _
            "Update Scripts Set Script = '" & Replace(Trim(txtSQL.Text), "'", "''") & "' where Identifier = '" & Replace(Trim(txtScriptID.Text), _
            "'", "''") & "' Else Insert into Scripts (Identifier, Script) Values ('" & Replace(Trim(txtScriptID.Text), "'", "''") & _
            "', '" & Replace(Trim(txtSQL.Text), "'", "''") & "')"
            If sSQL <> "" Then
                Dim RetStr As String = ExecuteSqlProcedure(sSQL)
                If RetStr = "" Then
                    txtScriptID.Text = ""
                    RunSaveProgress()
                Else
                    MsgBox(RetStr)
                End If
            End If
        End If
    End Sub

    Private Function CommentFreeSQL(ByVal SQL As String) As String
        Dim PS As Integer
        Dim PE As Integer
        Dim BadStr As String
        Do
            PS = InStr(SQL, "/*")
            PE = InStr(SQL, "*/")
            If PS > 0 And PE > 0 Then
                BadStr = SQL.Substring(PS - 1, PE + 1)
                SQL = Replace(SQL, BadStr, "")
                PS = 0 : PE = 0
            End If
        Loop Until PS = 0 And PE = 0
        Return SQL
    End Function

    Private Sub btnScriptLookUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnScriptLookUp.Click
        Dim IdScript As String = frmScriptLookUp.ShowDialog
        If IdScript <> "" Then
            Dim DATA() As String = Split(IdScript, "|")
            txtScriptID.Text = DATA(0)
            txtSQL.Text = DATA(1)
        End If
    End Sub

    Private Sub dgvResult_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvResult.CellMouseClick
        Dim rowClicked As DataGridView.HitTestInfo = dgvResult.HitTest(e.X, e.Y)

        'Select Right Clicked Row if its not the header row
        If e.Button = MouseButtons.Right AndAlso e.RowIndex > -1 Then
            'Clear any currently sellected rows
            'DgvDelays.ClearSelection()
            'Me.DgvDelays.Rows(e.RowIndex).Selected = True
            'CMS.Show(dgvResult, New System.Drawing.Point(MouseX, MouseY))
        End If
    End Sub

    Private Sub dgvResult_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvResult.CellMouseDown
        If e.Button = MouseButtons.Right AndAlso e.RowIndex > -1 Then
            Dim rowClicked As DataGridView.HitTestInfo = dgvResult.HitTest(e.X, e.Y)
            'CMS.Show(rowClicked.ColumnX, rowClicked.RowY)
            CMS.Show(dgvResult, New System.Drawing.Point(e.X, e.Y))
        End If
    End Sub

    Private Sub CMS_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles CMS.ItemClicked
        If e.ClickedItem.ToString.Contains("Copy") Then    'copy to clipboard
            ' Add the selection to the clipboard.
            Clipboard.SetDataObject(dgvResult.GetClipboardContent())
        ElseIf e.ClickedItem.ToString.Contains("CSV") Then    'Export to CSV
            Clipboard.SetDataObject(dgvResult.GetClipboardContent())
            If Clipboard.ContainsText Then
                Dim STR As String = Clipboard.GetText
                Dim Recs() As String = Split(STR, vbCrLf)
                Dim Comps() As String
                For i As Integer = 0 To Recs.Length - 1
                    If Recs(i) <> "" Then
                        Comps = Split(Recs(i), Chr(9))
                        For c As Integer = 0 To Comps.Length - 1
                            If Comps(c).Contains(",") Then
                                Comps(c) = Chr(34) & Comps(c) & Chr(34)
                            End If
                        Next
                        Recs(i) = Join(Comps, ",")
                    End If
                Next
                STR = Join(Recs, vbCrLf)
                SaveFileDialog1.DefaultExt = "csv"
                SaveFileDialog1.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*"
                If DialogResult.OK = SaveFileDialog1.ShowDialog Then
                    Dim SW As New StreamWriter(SaveFileDialog1.FileName)
                    SW.Write(STR)
                    SW.Close()
                    SW = Nothing
                    MsgBox("Your selected data exported, successfully.")
                End If
            Else
                MsgBox("There is nothing to export")
            End If
        Else    ''Export to TAB
            Clipboard.SetDataObject(dgvResult.GetClipboardContent())
            SaveFileDialog1.DefaultExt = "txt"
            SaveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            If Clipboard.ContainsText Then
                Dim STR As String = Clipboard.GetText
                If DialogResult.OK = SaveFileDialog1.ShowDialog Then
                    SaveFileDialog1.DefaultExt = "txt"
                    Dim SW As New StreamWriter(SaveFileDialog1.FileName)
                    SW.Write(STR)
                    SW.Close()
                    SW = Nothing
                    MsgBox("Your selected data exported, successfully.")
                End If
            Else
                MsgBox("There is nothing to export")
            End If
        End If
    End Sub
End Class
