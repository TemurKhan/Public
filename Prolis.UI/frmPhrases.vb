Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmPhrases

    Private Function NextPhraseID() As Integer
        Dim NID As Integer
        Dim cncid As New SqlConnection(connString)
        cncid.Open()
        Dim cmdcid As New SqlCommand("Select " &
        "max(ID) as LastID from Phrases", cncid)
        cmdcid.CommandType = CommandType.Text
        Dim drcid As SqlDataReader = cmdcid.ExecuteReader
        If drcid.HasRows Then
            While drcid.Read
                If drcid("LastID") IsNot DBNull.Value _
                Then NID = drcid("LastID") + 1
            End While
        End If
        cncid.Close()
        cncid = Nothing
        Return NID
    End Function

    Private Sub txtPhraseID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPhraseID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub FormClear()
        txtPhraseID.Text = ""
        txtPhraseKey.Text = ""
        cmbInputKeys.SelectedIndex = -1
        txtPhrase.Text = ""
    End Sub

    Private Sub txtPhraseID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPhraseID.Validated
        If txtPhraseID.Text <> "" Then
            If chkEditNew.Checked = False Then  'Edit
                If IsPhraseIDUnique(Val(txtPhraseID.Text)) = True Then
                    MsgBox("No record found", MsgBoxStyle.Critical, "Prolis")
                Else
                    DisplayPhrase(Val(txtPhraseID.Text))
                    btnSave.Enabled = True
                    btnDelete.Enabled = True
                End If
            Else    'Add New
                If IsPhraseIDUnique(Val(txtPhraseID.Text)) = False Or
                Val(txtPhraseID.Text) > 65535 Then
                    Dim RetVal As Integer = MsgBox("You have typed in a used ID " &
                    "or the value is more than the acceptable range (1 - 65535). " &
                    "Either type an unused ID or accept the system genrated ID. " &
                    "Press 'Yes' to accept the system generated ID or 'No' to type" &
                    " the ID your self", MsgBoxStyle.Critical + MsgBoxStyle.YesNo, "Prolis")
                    If RetVal = vbYes Then  '
                        txtPhraseID.Text = NextPhraseID()
                    Else
                        txtPhraseID.Text = ""
                        txtPhraseID.Focus()
                    End If
                End If
            End If
        End If
        UpdateProgress()
    End Sub

    Private Sub DisplayPhrase(ByVal PhraseID As Integer)
        Dim ItemX As MyList
        FormClear()
        Dim cndp As New SqlConnection(connString)
        cndp.Open()
        Dim cmddp As New SqlCommand("Select " &
        "* from Phrases where ID = " & PhraseID, cndp)
        cmddp.CommandType = CommandType.Text
        Dim drdp As SqlDataReader = cmddp.ExecuteReader
        If drdp.HasRows Then
            While drdp.Read
                txtPhraseID.Text = drdp("ID")
                txtPhraseKey.Text = drdp("PhraseKey")
                For i As Integer = 0 To cmbInputKeys.Items.Count - 1
                    ItemX = cmbInputKeys.Items(i)
                    If drdp("InputKeys") IsNot DBNull.Value _
                    AndAlso ItemX.ItemData = drdp("InputKeys") Then
                        cmbInputKeys.SelectedIndex = i
                        Exit For
                    End If
                Next
                For i As Integer = 0 To cmbCatagories.Items.Count - 1
                    ItemX = cmbCatagories.Items(i)
                    If ItemX.ItemData = drdp("PhraseType_ID") Then
                        cmbCatagories.SelectedIndex = i
                        Exit For
                    End If
                Next
                If drdp("Phrase").ToString.StartsWith("{\rtf1\") Then
                    txtPhrase.Rtf = drdp("Phrase")
                Else
                    txtPhrase.Text = drdp("Phrase")
                End If
            End While
        End If
        cndp.Close()
        cndp = Nothing
    End Sub

    Private Function IsPhraseIDUnique(ByVal PhraseID As Integer) As Boolean
        Dim IsIt As Boolean = False
        Dim cniu As New SqlConnection(connString)
        cniu.Open()
        Dim cmdiu As New SqlCommand("Select " &
        "* from Phrases where ID = " & PhraseID, cniu)
        cmdiu.CommandType = CommandType.Text
        Dim driu As SqlDataReader = cmdiu.ExecuteReader
        If driu.HasRows Then IsIt = True
        cniu.Close()
        cniu = Nothing
        Return IsIt
    End Function

    Private Sub SavePhrase(ByVal PhraseID As Integer, ByVal PhraseKey As String, ByVal Phrase As String)
        Dim ItemC As New MyList
        If cmbCatagories.SelectedIndex <> -1 Then
            ItemC = New MyList(cmbCatagories.SelectedItem.ToString, cmbCatagories.SelectedIndex)
        End If
        Dim ItemX As New MyList
        If cmbInputKeys.SelectedIndex <> -1 Then
            ItemX = New MyList(cmbInputKeys.SelectedItem.ToString, cmbInputKeys.SelectedIndex)
        End If
        ' Dim ItemX As MyList = IIf(cmbInputKeys.SelectedIndex <> -1, cmbInputKeys.SelectedItem, Nothing)
        ExecuteSqlProcedure("If Exists (Select * from Phrases where ID = " & PhraseID &
        ") Update Phrases set PhraseKey = '" & PhraseKey & "', PhraseType_ID = " &
        ItemC.ItemData & ", InputKeys = '" & IIf(ItemX Is Nothing, "Null", ItemX.ItemData) &
        "', Phrase = '" & Phrase & "' where ID = " & PhraseID & " Else Insert into Phrases " &
        "(ID, PhraseKey, PhraseType_ID, InputKeys, Phrase) values (" & PhraseID & ", '" &
        PhraseKey & "', " & ItemC.ItemData & ", '" & IIf(ItemX Is Nothing, "Null",
        ItemX.ItemData) & "', '" & Phrase & "')")
    End Sub

    Private Sub txtPhraseKey_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPhraseKey.Validated
        If txtPhraseKey.Text <> "" Then
            Dim Keys As Integer = FindPhraseKeys(txtPhraseKey.Text)
            If chkEditNew.Checked = True Then  'Add
                If Keys > 0 Then
                    MsgBox("Deplicate Key. Type a Unique key")
                    txtPhraseKey.Text = ""
                    txtPhraseKey.Focus()
                End If
            End If
        End If
        UpdateProgress()
    End Sub

    Private Sub UpdateProgress()
        If txtPhraseID.Text <> "" And txtPhraseKey.Text <> "" And txtPhrase.Text <> "" Then
            btnSave.Enabled = True
        Else
            btnSave.Enabled = False
        End If
    End Sub

    Private Function FindPhraseKeys(ByVal Key As String) As Integer
        Dim i As Integer = 0
        Dim cnpk As New SqlConnection(connString)
        cnpk.Open()
        Dim cmdpk As New SqlCommand("Select * " &
        "from Phrases where PhraseKey = '" & Key & "'", cnpk)
        cmdpk.CommandType = CommandType.Text
        Dim drpk As SqlDataReader = cmdpk.ExecuteReader
        If drpk.HasRows Then
            While drpk.Read
                i += 1
            End While
        End If
        cnpk.Close()
        cnpk = Nothing
        Return i
    End Function

    Private Sub btnPhraseLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhraseLook.Click
        Dim PhraseID As String = frmPhraseLook.ShowDialog
        If PhraseID <> "" Then
            DisplayPhrase(Val(PhraseID))
            btnSave.Enabled = True
            btnDelete.Enabled = True
        End If
    End Sub

    Private Sub frmPhrases_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        PopulateKeys()
        PopulatePhraseCatagories()
        cmbCatagories.SelectedIndex = 0
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub PopulatePhraseCatagories()
        cmbCatagories.Items.Clear()
        Dim cnpk As New SqlConnection(connString)
        cnpk.Open()
        Dim cmdpk As New SqlCommand(
        "Select * from PhraseTypes", cnpk)
        cmdpk.CommandType = CommandType.Text
        Dim drpk As SqlDataReader = cmdpk.ExecuteReader
        If drpk.HasRows Then
            While drpk.Read
                cmbCatagories.Items.Add(New _
                MyList(drpk("PhraseType"), drpk("ID")))
            End While
        End If
        cnpk.Close()
        cnpk = Nothing
    End Sub

    Private Sub PopulateUnusedKeys()
        cmbInputKeys.Items.Clear()
        If Not KeysUsed(Keys.F2) Then cmbInputKeys.Items.Add(New MyList("F2", Keys.F2))
        If Not KeysUsed(Keys.F3) Then cmbInputKeys.Items.Add(New MyList("F3", Keys.F3))
        If Not KeysUsed(Keys.F4) Then cmbInputKeys.Items.Add(New MyList("F4", Keys.F4))
        If Not KeysUsed(Keys.F5) Then cmbInputKeys.Items.Add(New MyList("F5", Keys.F5))
        If Not KeysUsed(Keys.F6) Then cmbInputKeys.Items.Add(New MyList("F6", Keys.F6))
        If Not KeysUsed(Keys.F7) Then cmbInputKeys.Items.Add(New MyList("F7", Keys.F7))
        If Not KeysUsed(Keys.F8) Then cmbInputKeys.Items.Add(New MyList("F8", Keys.F8))
        If Not KeysUsed(Keys.F9) Then cmbInputKeys.Items.Add(New MyList("F9", Keys.F9))
        If Not KeysUsed(Keys.F10) Then cmbInputKeys.Items.Add(New MyList("F10", Keys.F10))
        If Not KeysUsed(Keys.F11) Then cmbInputKeys.Items.Add(New MyList("F11", Keys.F11))
        If Not KeysUsed(Keys.F12) Then cmbInputKeys.Items.Add(New MyList("F12", Keys.F12))
        If Not KeysUsed(Keys.Shift + Keys.F2) Then cmbInputKeys.Items.Add(New MyList("Shift + F2", Keys.Shift + Keys.F2))
        If Not KeysUsed(Keys.Shift + Keys.F3) Then cmbInputKeys.Items.Add(New MyList("Shift + F3", Keys.Shift + Keys.F3))
        If Not KeysUsed(Keys.Shift + Keys.F4) Then cmbInputKeys.Items.Add(New MyList("Shift + F4", Keys.Shift + Keys.F4))
        If Not KeysUsed(Keys.Shift + Keys.F5) Then cmbInputKeys.Items.Add(New MyList("Shift + F5", Keys.Shift + Keys.F5))
        If Not KeysUsed(Keys.Shift + Keys.F6) Then cmbInputKeys.Items.Add(New MyList("Shift + F6", Keys.Shift + Keys.F6))
        If Not KeysUsed(Keys.Shift + Keys.F7) Then cmbInputKeys.Items.Add(New MyList("Shift + F7", Keys.Shift + Keys.F7))
        If Not KeysUsed(Keys.Shift + Keys.F8) Then cmbInputKeys.Items.Add(New MyList("Shift + F8", Keys.Shift + Keys.F8))
        If Not KeysUsed(Keys.Shift + Keys.F9) Then cmbInputKeys.Items.Add(New MyList("Shift + F9", Keys.Shift + Keys.F9))
        If Not KeysUsed(Keys.Shift + Keys.F10) Then cmbInputKeys.Items.Add(New MyList("Shift + F10", Keys.Shift + Keys.F10))
        If Not KeysUsed(Keys.Shift + Keys.F11) Then cmbInputKeys.Items.Add(New MyList("Shift + F11", Keys.Shift + Keys.F11))
        If Not KeysUsed(Keys.Shift + Keys.F12) Then cmbInputKeys.Items.Add(New MyList("Shift + F12", Keys.Shift + Keys.F12))
        If Not KeysUsed(Keys.Control + Keys.F2) Then cmbInputKeys.Items.Add(New MyList("Control + F2", Keys.Control + Keys.F2))
        If Not KeysUsed(Keys.Control + Keys.F3) Then cmbInputKeys.Items.Add(New MyList("Control + F3", Keys.Control + Keys.F3))
        If Not KeysUsed(Keys.Control + Keys.F4) Then cmbInputKeys.Items.Add(New MyList("Control + F4", Keys.Control + Keys.F4))
        If Not KeysUsed(Keys.Control + Keys.F5) Then cmbInputKeys.Items.Add(New MyList("Control + F5", Keys.Control + Keys.F5))
        If Not KeysUsed(Keys.Control + Keys.F6) Then cmbInputKeys.Items.Add(New MyList("Control + F6", Keys.Control + Keys.F6))
        If Not KeysUsed(Keys.Control + Keys.F7) Then cmbInputKeys.Items.Add(New MyList("Control + F7", Keys.Control + Keys.F7))
        If Not KeysUsed(Keys.Control + Keys.F8) Then cmbInputKeys.Items.Add(New MyList("Control + F8", Keys.Control + Keys.F8))
        If Not KeysUsed(Keys.Control + Keys.F9) Then cmbInputKeys.Items.Add(New MyList("Control + F9", Keys.Control + Keys.F9))
        If Not KeysUsed(Keys.Control + Keys.F10) Then cmbInputKeys.Items.Add(New MyList("Control + F10", Keys.Control + Keys.F10))
        If Not KeysUsed(Keys.Control + Keys.F11) Then cmbInputKeys.Items.Add(New MyList("Control + F11", Keys.Control + Keys.F11))
        If Not KeysUsed(Keys.Control + Keys.F12) Then cmbInputKeys.Items.Add(New MyList("Control + F12", Keys.Control + Keys.F12))
    End Sub

    Private Function KeysUsed(ByVal Val As Keys) As Boolean
        Dim Used As Boolean = False
        Dim cnpk As New SqlConnection(connString)
        cnpk.Open()
        Dim cmdpk As New SqlCommand("Select * " &
        "from Phrases where Inputkeys = " & Val, cnpk)
        cmdpk.CommandType = CommandType.Text
        Dim drpk As SqlDataReader = cmdpk.ExecuteReader
        If drpk.HasRows Then Used = True
        cnpk.Close()
        cnpk = Nothing
        Return Used
    End Function

    Private Sub PopulateKeys()
        cmbInputKeys.Items.Clear()
        cmbInputKeys.Items.Add(New MyList("F2", Keys.F2))
        cmbInputKeys.Items.Add(New MyList("F3", Keys.F3))
        cmbInputKeys.Items.Add(New MyList("F4", Keys.F4))
        cmbInputKeys.Items.Add(New MyList("F5", Keys.F5))
        cmbInputKeys.Items.Add(New MyList("F6", Keys.F6))
        cmbInputKeys.Items.Add(New MyList("F7", Keys.F7))
        cmbInputKeys.Items.Add(New MyList("F8", Keys.F8))
        cmbInputKeys.Items.Add(New MyList("F9", Keys.F9))
        cmbInputKeys.Items.Add(New MyList("F10", Keys.F10))
        cmbInputKeys.Items.Add(New MyList("F11", Keys.F11))
        cmbInputKeys.Items.Add(New MyList("F12", Keys.F12))
        cmbInputKeys.Items.Add(New MyList("Shift + F2", Keys.Shift + Keys.F2))
        cmbInputKeys.Items.Add(New MyList("Shift + F3", Keys.Shift + Keys.F3))
        cmbInputKeys.Items.Add(New MyList("Shift + F4", Keys.Shift + Keys.F4))
        cmbInputKeys.Items.Add(New MyList("Shift + F5", Keys.Shift + Keys.F5))
        cmbInputKeys.Items.Add(New MyList("Shift + F6", Keys.Shift + Keys.F6))
        cmbInputKeys.Items.Add(New MyList("Shift + F7", Keys.Shift + Keys.F7))
        cmbInputKeys.Items.Add(New MyList("Shift + F8", Keys.Shift + Keys.F8))
        cmbInputKeys.Items.Add(New MyList("Shift + F9", Keys.Shift + Keys.F9))
        cmbInputKeys.Items.Add(New MyList("Shift + F10", Keys.Shift + Keys.F10))
        cmbInputKeys.Items.Add(New MyList("Shift + F11", Keys.Shift + Keys.F11))
        cmbInputKeys.Items.Add(New MyList("Shift + F12", Keys.Shift + Keys.F12))
        cmbInputKeys.Items.Add(New MyList("Control + F2", Keys.Control + Keys.F2))
        cmbInputKeys.Items.Add(New MyList("Control + F3", Keys.Control + Keys.F3))
        cmbInputKeys.Items.Add(New MyList("Control + F4", Keys.Control + Keys.F4))
        cmbInputKeys.Items.Add(New MyList("Control + F5", Keys.Control + Keys.F5))
        cmbInputKeys.Items.Add(New MyList("Control + F6", Keys.Control + Keys.F6))
        cmbInputKeys.Items.Add(New MyList("Control + F7", Keys.Control + Keys.F7))
        cmbInputKeys.Items.Add(New MyList("Control + F8", Keys.Control + Keys.F8))
        cmbInputKeys.Items.Add(New MyList("Control + F9", Keys.Control + Keys.F9))
        cmbInputKeys.Items.Add(New MyList("Control + F10", Keys.Control + Keys.F10))
        cmbInputKeys.Items.Add(New MyList("Control + F11", Keys.Control + Keys.F11))
        cmbInputKeys.Items.Add(New MyList("Control + F12", Keys.Control + Keys.F12))
    End Sub

    Private Sub btnFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFont.Click
        If FontDialog1.ShowDialog = DialogResult.OK Then
            txtPhrase.SelectionFont = FontDialog1.Font
        End If
    End Sub

    Private Sub chkBold_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBold.Click
        If chkBold.Checked = True Then
            txtPhrase.SelectionFont = New Font(txtPhrase.SelectionFont, FontStyle.Bold)
        Else
            txtPhrase.SelectionFont = New Font(txtPhrase.SelectionFont, FontStyle.Regular)
        End If
    End Sub

    Private Sub chkItalic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkItalic.Click
        If chkItalic.Checked = True Then _
        txtPhrase.SelectionFont = New Font(txtPhrase.SelectionFont, FontStyle.Italic)
    End Sub

    Private Sub chkUnderline_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUnderline.Click
        If chkUnderline.Checked = True Then _
        txtPhrase.SelectionFont = New Font(txtPhrase.SelectionFont, FontStyle.Underline)
    End Sub

    Private Sub chkLAlign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLAlign.Click
        If chkLAlign.Checked = True Then
            chkMAlign.Checked = False
            chkRAlign.Checked = False
            txtPhrase.SelectionAlignment = HorizontalAlignment.Left
        End If
    End Sub

    Private Sub chkMAlign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMAlign.Click
        If chkMAlign.Checked = True Then
            chkLAlign.Checked = False
            chkRAlign.Checked = False
            txtPhrase.SelectionAlignment = HorizontalAlignment.Center
        End If
    End Sub

    Private Sub chkRAlign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRAlign.Click
        If chkRAlign.Checked = True Then
            chkMAlign.Checked = False
            chkLAlign.Checked = False
            txtPhrase.SelectionAlignment = HorizontalAlignment.Right
        End If
    End Sub

    Private Sub chkBList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBList.Click
        If chkBList.Checked = True Then
            txtPhrase.SelectionBullet = True
        Else
            txtPhrase.SelectionBullet = False
        End If
    End Sub

    Private Sub btnFontColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFontColor.Click
        If ColorDialog1.ShowDialog = DialogResult.OK Then
            txtPhrase.SelectionColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub btnImageFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImageFile.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Dim TheFile As String = OpenFileDialog1.FileName
            If TheFile <> "" Then
                Dim img As New System.Drawing.Bitmap(TheFile)
                Clipboard.Clear()
                Clipboard.SetDataObject(img)
                txtPhrase.Paste()
            End If
        End If
    End Sub

    Private Sub chkEditNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEditNew.Click
        FormClear()
        If chkEditNew.Checked = False Then
            chkEditNew.Text = "Edit"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Edit.ico")
            btnPhraseLook.Enabled = True
            PopulateKeys()
        Else
            chkEditNew.Text = "New"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\New.ico")
            txtPhraseID.Text = NextPhraseID()
            btnPhraseLook.Enabled = False
            PopulateUnusedKeys()
        End If
    End Sub

    Private Sub txtPhrase_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPhrase.Validated
        UpdateProgress()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If txtPhraseID.Text <> "" And txtPhraseKey.Text <> "" _
        And txtPhrase.Text <> "" Then
            Dim RetVal As Integer = MsgBox("Are you sure you want to delete this phrase?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
            If RetVal = vbYes Then
                ExecuteSqlProcedure("Delete from Phrases where ID = " & Val(txtPhraseID.Text))
                FormClear()
                btnDelete.Enabled = False
                btnSave.Enabled = False
                txtPhraseID.Focus()
            End If
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtPhraseID.Text <> "" And txtPhraseKey.Text <> "" _
        And txtPhrase.Text <> "" Then
            SavePhrase(Val(txtPhraseID.Text), txtPhraseKey.Text, txtPhrase.Rtf)
            FormClear()
            PopulateUnusedKeys()
            btnSave.Enabled = False
            btnDelete.Enabled = False
            If chkEditNew.Checked = True Then
                txtPhraseID.Text = NextPhraseID()
                txtPhraseKey.Focus()
            End If
        Else
            MsgBox("In order to save the phrase, you need to provide values in all required fields")
        End If
    End Sub
End Class
