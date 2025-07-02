Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmSources

    Private Sub chkEditNew_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkEditNew.CheckedChanged
        If chkEditNew.Checked = False Then
            chkEditNew.Text = "Edit"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Edit.ico")
            ClearForm()
            dgvSources.Enabled = True
            btnDelete.Enabled = True
        Else
            chkEditNew.Text = "New"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\New.ico")
            ClearForm()
            txtSourceID.Text = NextSourceID()
            dgvSources.Enabled = False
            btnDelete.Enabled = False
        End If
    End Sub
    Private Sub ClearForm()
        txtSourceID.Text = ""
        txtSource.Text = ""
        txtUoM.Text = ""
        cmbMaterials.SelectedIndex = -1
    End Sub

    Private Function NextSourceID() As Integer
        Dim NID As Integer = 1
        Dim cni As New SqlConnection(connString)
        cni.Open()
        Dim cmdi As New SqlCommand("Select max(ID) as LastID from Sources", cni)
        cmdi.CommandType = CommandType.Text
        Dim dri As SqlDataReader = cmdi.ExecuteReader
        If dri.HasRows Then
            While dri.Read
                If dri("LastID") IsNot DBNull.Value _
                Then NID = dri("LastID") + 1
            End While
        End If
        cni.Close()
        cni = Nothing
        Return NID
    End Function

    Private Sub frmsources_formclosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'TODO : uncomment the following line
        ' If Me.MdiParent Is frmrequisitions Then frmrequisitions.populatesources()
    End Sub

    Private Sub frmSources_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CN.Open("DSN=ProlisQC;UID=sa;PWD=''")
        PopulateSources()
        PopulateMaterials()
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Friend Sub PopulateMaterials()
        cmbMaterials.Items.Clear()
        Dim cnpm As New SqlConnection(connString)
        cnpm.Open()
        Dim cmdpm As New SqlCommand("Select * from Materials", cnpm)
        cmdpm.CommandType = CommandType.Text
        Dim drpm As SqlDataReader = cmdpm.ExecuteReader
        If drpm.HasRows Then
            While drpm.Read
                cmbMaterials.Items.Add(New MyList(drpm("Name"), drpm("ID")))
            End While
        End If
        cnpm.Close()
        cnpm = Nothing
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtSourceID.Text <> "" And txtSource.Text <> "" And txtUoM.Text <> "" And
        cmbMaterials.SelectedIndex <> -1 Then
            SaveSource(Val(txtSourceID.Text))
            PopulateSources()
            ClearForm()
            If chkEditNew.Checked = True Then
                txtSourceID.Text = NextSourceID()
                txtSource.Focus()
            End If

        End If
    End Sub

    Private Sub SaveSource(ByVal SourceID As Integer)
        Dim ItemX As MyList = cmbMaterials.SelectedItem
        ExecuteSqlProcedure("If Exists (Select * from Sources where ID = " & SourceID &
        ") Update Sources Set Name = '" & Trim(txtSource.Text) & "', UoM = '" &
        Trim(txtUoM.Text) & "', Material_ID = " & ItemX.ItemData & " where ID = " &
        SourceID & " Else Insert into Sources (ID, Name, UoM, Material_ID) values " &
        "(" & SourceID & ", '" & Trim(txtSource.Text) & "', '" & Trim(txtUoM.Text) &
        "', " & ItemX.ItemData & ")")
    End Sub

    Private Sub PopulateSources()
        dgvSources.Rows.Clear()
        Dim cnps As New SqlConnection(connString)
        cnps.Open()
        Dim cmdps As New SqlCommand("Select a.*, b.Name as Material " &
        "from Sources a inner join Materials b on b.ID = a.Material_ID", cnps)
        cmdps.CommandType = CommandType.Text
        Dim drps As SqlDataReader = cmdps.ExecuteReader
        If drps.HasRows Then
            While drps.Read
                dgvSources.Rows.Add(drps("ID"), drps("Name"), _
                drps("UoM"), drps("Material_ID"), drps("Material"))
            End While
        End If
        cnps.Close()
        cnps = Nothing
    End Sub

    Private Sub btnMatLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMatLook.Click
        frmMaterials.ShowDialog()
        PopulateMaterials()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub dgvSources_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSources.CellDoubleClick
        ClearForm()
        txtSourceID.Text = dgvSources.Rows(e.RowIndex).Cells(0).Value
        txtSource.Text = dgvSources.Rows(e.RowIndex).Cells(1).Value
        txtUoM.Text = dgvSources.Rows(e.RowIndex).Cells(2).Value
        Dim i As Integer
        Dim ItemX As MyList
        For i = 0 To cmbMaterials.Items.Count - 1
            ItemX = cmbMaterials.Items(i)
            If ItemX.ItemData = dgvSources.Rows(e.RowIndex).Cells(3).Value Then
                cmbMaterials.SelectedIndex = i
                Exit For
            End If
        Next
    End Sub

    Private Function IsDuplicate(ByVal Source As String) As Boolean
        Dim i As Integer
        Dim Duplicate As Boolean = False
        For i = 0 To dgvSources.RowCount - 1
            If Source = dgvSources.Rows(i).Cells(1).Value Then
                Duplicate = True
                Exit For
            End If
        Next
        IsDuplicate = Duplicate
    End Function

    Private Sub txtSource_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSource.LostFocus
        If chkEditNew.Checked = True Then
            If IsDuplicate(txtSource.Text) Then
                MsgBox("A source with the same name already exists. Duplicate record can not be created.")
                txtSource.Text = ""
                txtSource.Focus()
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If txtSourceID.Text <> "" And txtSource.Text <> "" And cmbMaterials.SelectedIndex <> -1 Then
            Dim RetVal As Integer = MsgBox("Are you sure to delete this record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
            If RetVal = vbYes Then
                ExecuteSqlProcedure("Delete from Sources where ID = " & Val(txtSourceID.Text))
                txtSourceID.Text = "" : txtSource.Text = ""
                txtUoM.Text = "" : cmbMaterials.SelectedIndex = -1
                PopulateSources()
            End If
        End If
    End Sub
End Class
