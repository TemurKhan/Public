Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmCRCopy

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub frmCRCopy_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PopulateFlags()
        txtFile.Text = ""
        lstFields.Items.Clear()
        cmbDelimiter.SelectedIndex = 0
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub PopulateFlags()
        cmbFlag.Items.Clear()
        cmbFlag.Items.Add("A - Abnormal")
        cmbFlag.Items.Add("I - Ignore")
        cmbFlag.Items.Add("N - Normal")
        cmbFlag.Items.Add("P - Panic")
        cmbFlag.Items.Add("R - Repeat")
    End Sub

    Private Sub PopulateCComponents()
        cmbFlag.Items.Clear()
        Dim cncc As New SqlConnection(connString)
        cncc.Open()
        Dim cmdcc As New SqlCommand("Select " &
        "ID, Name from Tests where ID in (Select Distinct Test_ID " _
        & "from C_Ranges)", cncc)
        cmdcc.CommandType = CommandType.Text
        Dim drcc As SqlDataReader = cmdcc.ExecuteReader
        If drcc.HasRows Then
            While drcc.Read
                cmbFlag.Items.Add(New MyList(drcc("Name"), drcc("ID")))
            End While
        End If
        cncc.Close()
        cncc = Nothing
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If chkOutIn.Checked = False Then
            If txtFile.Text <> "" And cmbDelimiter.SelectedIndex <> -1 And _
            cmbFlag.Text <> "" And lstFields.SelectedItems.Count = 1 Then
                Me.Tag = txtFile.Text & "|" & cmbDelimiter.SelectedIndex.ToString & _
                "|" & lstFields.SelectedIndex.ToString & "|" & cmbFlag.Text
                Me.Close()
            End If
        Else
            If cmbFlag.SelectedIndex <> -1 Then
                Dim ItemX As MyList = cmbFlag.SelectedItem
                Me.Tag = ItemX.ItemData.ToString
                Me.Close()
            End If
        End If
    End Sub

    Private Sub chkOutIn_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOutIn.CheckedChanged
        txtFile.Text = ""
        cmbFlag.Text = ""
        cmbFlag.SelectedIndex = -1
        lstFields.Items.Clear()
        If chkOutIn.Checked = False Then
            chkOutIn.Text = "Delimited Text File"
            lblComponent.Text = "Default Flag"
            btnBrowse.Enabled = True
            cmbDelimiter.Enabled = True
            btnLoad.Enabled = True
            lstFields.Enabled = True
            PopulateFlags()
        Else
            chkOutIn.Text = "Prolis Analyte"
            lblComponent.Text = "Prolis Source Component"
            btnBrowse.Enabled = False
            cmbDelimiter.Enabled = False
            btnLoad.Enabled = False
            lstFields.Enabled = False
            PopulateCComponents()
        End If
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Dim OFD As New OpenFileDialog
        OFD.Title = "Delimited Text file"
        If OFD.ShowDialog = DialogResult.OK Then
            txtFile.Text = OFD.FileName
        Else
            txtFile.Text = ""
        End If
        Update_Progress()
    End Sub

    Private Sub Update_Progress()
        If chkOutIn.Checked = False Then
            If txtFile.Text <> "" And cmbDelimiter.SelectedIndex <> -1 And _
            lstFields.SelectedItems.Count = 1 And cmbFlag.Text <> "" Then
                btnOK.Enabled = True
            Else
                btnOK.Enabled = False
            End If
        Else
            If cmbFlag.SelectedIndex <> -1 Then
                btnOK.Enabled = True
            Else
                btnOK.Enabled = False
            End If
        End If
    End Sub

    Private Sub cmbFlag_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFlag.SelectedIndexChanged
        If cmbFlag.SelectedIndex <> -1 Then Update_Progress()
    End Sub

    Private Sub cmbDelimiter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDelimiter.SelectedIndexChanged
        If cmbDelimiter.SelectedIndex <> -1 Then Update_Progress()
    End Sub

    Private Sub lstFields_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstFields.SelectedIndexChanged
        If lstFields.SelectedIndex <> -1 Then Update_Progress()
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        If txtFile.Text <> "" And cmbDelimiter.SelectedIndex <> -1 Then
            Dim i As Integer
            Dim Delim As String
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
            lstFields.Items.Clear()
            For i = 0 To Fields.Length - 1
                lstFields.Items.Add(Trim(Fields(i)))
            Next
            SR.Close()
            SR = Nothing
        End If
    End Sub
End Class
