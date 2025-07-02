Imports System.Windows.Forms
Imports System.Data

Public Class frmRTF
    Public Shared rtt As String
    Public Shared img As Image
    Private Sub frmRTF_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        frmResults.pctImg.Image = img
        Me.pctImg.Image = frmResults.pctImg.Image
        Me.txtRTF.Rtf = frmResults.txtRTF.Rtf
        frmResults.txtRTF.Rtf = rtt
        Me.txtRTF.Rtf = rtt
        'frmResults.txtRTF.Rtf = ""
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.txtRTF.Rtf  'CType(TextBox1.Text, Integer)
    End Function

    Private Sub txtRTF_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRTF.KeyPress
        Dim DotPoint As Integer
        Dim term As String
        If e.KeyChar = Chr(13) Then
            DotPoint = InStrRev(txtRTF.Text, ".", txtRTF.SelectionStart)
            If DotPoint <> 0 Then
                term = txtRTF.Text.Substring(DotPoint, txtRTF.SelectionStart - 2 -
                DotPoint + 1)
                If InStr(term, " ") <> 0 Then
                    Exit Sub
                Else
                    Dim Phrase As String = GetPhrase(term)
                    If Phrase <> "" Then
                        txtRTF.SelectionStart = txtRTF.SelectionStart - Len(term) - 2
                        txtRTF.SelectionLength = Len(term) + 1
                        txtRTF.SelectedText = GetPhrase(term)
                        e.KeyChar = Chr(0)
                    End If
                End If
            End If
        End If
    End Sub

    Private Function GetPhrase(ByVal PhraseKey As String) As String
        Dim Phr As String = ""
        Dim cnph As New SqlClient.SqlConnection(connString)
        cnph.Open()
        Dim cmdph As New SqlClient.SqlCommand("Select * from " &
        "Phrases where PhraseKey like '" & PhraseKey & "'", cnph)
        cmdph.CommandType = CommandType.Text
        Dim drph As SqlClient.SqlDataReader = cmdph.ExecuteReader
        If drph.HasRows Then
            While drph.Read
                Phr = drph("Phrase")
            End While
        End If
        cnph.Close()
        cnph = Nothing
        Return Phr
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Me.Close()
    End Sub

    Private Sub UpdatePhrase(ByVal PhraseKey As String, ByVal Phrase As String)
        ExecuteSqlProcedure("If Exists (Select * from Phrases where PhraseKey = '" &
        PhraseKey & "') Update Phrases set Phrase = '" & Phrase & "' where " &
        "PhraseKey = '" & PhraseKey & "' Else Insert into Phrases (ID, PhraseKey, " &
        "Phrase) values (" & NextPhraseID() & ", '" & PhraseKey & "', '" & Phrase & "')")
    End Sub

    Private Function NextPhraseID() As Integer
        Dim Nid As Integer = 1
        Dim Phr As String = ""
        Dim cnph As New SqlClient.SqlConnection(connString)
        cnph.Open()
        Dim cmdph As New SqlClient.SqlCommand("Select max(ID) as LastID from Phrases", cnph)
        cmdph.CommandType = CommandType.Text
        Dim drph As SqlClient.SqlDataReader = cmdph.ExecuteReader
        If drph.HasRows Then
            While drph.Read
                If drph("LastID") IsNot DBNull.Value _
                Then Nid = drph("LastID") + 1
            End While
        End If
        cnph.Close()
        cnph = Nothing
        Return Nid
    End Function

    Private Sub chkBold_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBold.Click
        If chkBold.Checked = True Then
            txtRTF.SelectionFont = New Font(txtRTF.SelectionFont, FontStyle.Bold)
        Else
            txtRTF.SelectionFont = New Font(txtRTF.SelectionFont, FontStyle.Regular)
        End If
    End Sub

    Private Sub btnFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFont.Click
        If FontDialog1.ShowDialog = DialogResult.OK Then
            txtRTF.SelectionFont = FontDialog1.Font
        End If
    End Sub

    Private Sub chkItalic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkItalic.Click
        If chkItalic.Checked = True Then _
        txtRTF.SelectionFont = New Font(txtRTF.SelectionFont, FontStyle.Italic)
    End Sub

    Private Sub chkUnderline_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUnderline.Click
        If chkUnderline.Checked = True Then _
        txtRTF.SelectionFont = New Font(txtRTF.SelectionFont, FontStyle.Underline)
    End Sub

    Private Sub chkLAlign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLAlign.Click
        If chkLAlign.Checked = True Then
            chkMAlign.Checked = False
            chkRAlign.Checked = False
            txtRTF.SelectionAlignment = HorizontalAlignment.Left
        End If
    End Sub

    Private Sub chkRAlign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRAlign.Click
        If chkRAlign.Checked = True Then
            chkMAlign.Checked = False
            chkLAlign.Checked = False
            txtRTF.SelectionAlignment = HorizontalAlignment.Right
        End If
    End Sub

    Private Sub chkMAlign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMAlign.Click
        If chkMAlign.Checked = True Then
            chkLAlign.Checked = False
            chkRAlign.Checked = False
            txtRTF.SelectionAlignment = HorizontalAlignment.Center
        End If
    End Sub

    Private Sub chkBList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBList.Click
        If chkBList.Checked = True Then
            txtRTF.SelectionBullet = True
        Else
            txtRTF.SelectionBullet = False
        End If
    End Sub

    Private Sub btnFontColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFontColor.Click
        If ColorDialog1.ShowDialog = DialogResult.OK Then
            txtRTF.SelectionColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub btnImageFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Dim TheFile As String = OpenFileDialog1.FileName
            If TheFile <> "" Then
                Dim img As New System.Drawing.Bitmap(TheFile)
                Clipboard.Clear()
                Clipboard.SetDataObject(img)
                txtRTF.Paste()
            End If
        End If
    End Sub

    Private Sub btnPhraseLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhraseLook.Click
        Dim PhraseID As String = frmPhraseLook.ShowDialog
        If PhraseID <> "" Then
            Dim MyText As String = GetPhraseByID(Val(PhraseID))
            If MyText.StartsWith("{\rtf1\") Then
                txtRTF.Rtf = MyText
            Else
                txtRTF.Text = MyText
            End If
        End If
    End Sub

    Private Function GetPhraseByID(ByVal PhraseID As Integer) As String
        Dim Phr As String = ""
        Dim cnph As New SqlClient.SqlConnection(connString)
        cnph.Open()
        Dim cmdph As New SqlClient.SqlCommand("Select " &
        "* from Phrases where ID = " & PhraseID, cnph)
        cmdph.CommandType = CommandType.Text
        Dim drph As SqlClient.SqlDataReader = cmdph.ExecuteReader
        If drph.HasRows Then
            While drph.Read
                Phr = drph("Phrase")
            End While
        End If
        cnph.Close()
        cnph = Nothing
        Return Phr
    End Function

    Private Sub btnImgLookUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImgAdd.Click
        'OpenFileDialog1.InitialDirectory = "c:\Program Files\Prolis\Images"
        OpenFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG"
        Try
            OpenFileDialog1.ShowDialog()
            pctImg.Image = Image.FromFile(OpenFileDialog1.FileName, True)
            frmResults.pctImg.Image = pctImg.Image
        Catch Ex As Exception
            MessageBox.Show("Selection process aborted by the user.")
        End Try
    End Sub

    Private Sub btnImgDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImgDelete.Click
        Dim RetVal As Integer = MsgBox("Are you sure you want to clean the image " &
        "buffer?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, My.Application.Info.AssemblyName)
        If RetVal = vbYes Then
            pctImg.Image = Nothing
            frmResults.pctImg.Image = Nothing
        End If
    End Sub
End Class
