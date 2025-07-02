Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmCRefluxCopy

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub PopulateCNMarked()
        cmbComponent.Items.Clear()
        Dim cn1 As New SqlConnection(connString)
        cn1.Open()
        Dim cmd1 As New SqlCommand("Select ID, Name " &
        "from Tests Union Select ID, Name from Groups Union " _
        & "Select ID, Name from Profiles", cn1)
        cmd1.CommandType = CommandType.Text
        Dim dr1 As SqlDataReader = cmd1.ExecuteReader
        If dr1.HasRows Then
            While dr1.Read
                cmbComponent.Items.Add(New MyList(dr1("Name"), dr1("ID")))
            End While
        End If
        cn1.Close()
        cn1 = Nothing
    End Sub

    Private Sub PopulateMarkSources()
        cmbComponent.Items.Clear()
        Dim cn2 As New SqlConnection(connString)
        cn2.Open()
        Dim cmd2 As New SqlCommand("Select " &
        "ID, Name from Tests where Automarker <> 0", cn2)
        cmd2.CommandType = CommandType.Text
        Dim dr2 As SqlDataReader = cmd2.ExecuteReader
        If dr2.HasRows Then
            While dr2.Read
                cmbComponent.Items.Add(New _
                MyList(dr2("Name"), dr2("ID")))
            End While
        End If
        cn2.Close()
        cn2 = Nothing
    End Sub

    Private Sub frmCRefluxCopy_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PopulateCNMarked()
        cmbDelimiter.SelectedIndex = 0
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If chkOutIn.Checked = False Then
            If txtFile.Text <> "" And cmbDelimiter.SelectedIndex <> -1 And
            cmbComponent.SelectedIndex <> -1 And lstFields.SelectedItems.Count = 1 Then
                Dim ItemX As MyList = cmbComponent.SelectedItem
                Me.Tag = txtFile.Text & "|" & cmbDelimiter.SelectedIndex.ToString &
                "|" & lstFields.SelectedIndex.ToString & "|" & ItemX.ItemData.ToString
                Me.Close()
            End If
        Else
            If cmbComponent.SelectedIndex <> -1 Then
                Dim ItemX As MyList = cmbComponent.SelectedItem
                Me.Tag = ItemX.ItemData.ToString
                Me.Close()
            End If
        End If
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
            lstFields.SelectedItems.Count = 1 And cmbComponent.SelectedIndex <> -1 Then
                btnOK.Enabled = True
            Else
                btnOK.Enabled = False
            End If
        Else
            If cmbComponent.SelectedIndex <> -1 Then
                btnOK.Enabled = True
            Else
                btnOK.Enabled = False
            End If
        End If
    End Sub

    Private Sub cmbDelimiter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDelimiter.SelectedIndexChanged
        If cmbDelimiter.SelectedIndex <> -1 Then Update_Progress()
    End Sub

    Private Sub cmbComponent_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbComponent.SelectedIndexChanged
        If cmbComponent.SelectedIndex <> -1 Then Update_Progress()
    End Sub

    Private Sub lstFields_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstFields.SelectedIndexChanged
        If lstFields.SelectedIndex <> -1 Then Update_Progress()
    End Sub

    Private Sub chkOutIn_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOutIn.CheckedChanged
        If chkOutIn.Checked = False Then
            chkOutIn.Text = "Delimited Text File"
            lblComponent.Text = "Default Refluxed Component"
            btnBrowse.Enabled = True
            cmbDelimiter.Enabled = True
            btnLoad.Enabled = True
            lstFields.Enabled = True
            PopulateCNMarked()
        Else
            chkOutIn.Text = "Prolis Analyte"
            lblComponent.Text = "Prolis Source Component"
            btnBrowse.Enabled = False
            cmbDelimiter.Enabled = False
            btnLoad.Enabled = False
            lstFields.Enabled = False
            PopulateMarkSources()
        End If
    End Sub

End Class
