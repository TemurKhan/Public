Imports System.Windows.Forms
Imports Microsoft.Data.SqlClient

Public Class frmMaterials

    Private Sub chkEditNew_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkEditNew.CheckedChanged
        If chkEditNew.Checked = False Then
            chkEditNew.Text = "Edit"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Edit.ico")
            ClearForm()
            dgvMaterials.Enabled = True
            btnDelete.Enabled = True
        Else
            chkEditNew.Text = "New"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\New.ico")
            ClearForm()
            txtMaterialID.Text = NextMaterialID()
            dgvMaterials.Enabled = False
            btnDelete.Enabled = False
        End If
    End Sub

    Private Sub ClearForm()
        txtMaterialID.Text = ""
        txtMaterial.Text = ""
        txtUoM.Text = ""
    End Sub

    Private Function NextMaterialID() As Integer
        Dim MID As Integer = 1
        Dim cnmid As New SqlConnection(connString)
        cnmid.Open()
        Dim cmdmid As New SqlCommand("Select max(ID) as LastID from Materials", cnmid)
        cmdmid.CommandType = CommandType.Text
        Dim drmid As SqlDataReader = cmdmid.ExecuteReader
        If drmid.HasRows Then
            While drmid.Read
                If drmid("LastID") IsNot DBNull.Value _
                Then MID = drmid("LastID") + 1
            End While
        End If
        cnmid.Close()
        cnmid = Nothing
        Return MID
    End Function

    Private Sub frmMaterials_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.ParentForm Is frmSources Then frmSources.PopulateMaterials()
    End Sub

    Private Sub frmMaterials_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CN.Open("DSN=ProlisQC;UID=sa;PWD=''")
        PopulateMaterials()
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtMaterialID.Text <> "" And txtMaterial.Text <> "" And txtUoM.Text <> "" Then
            SaveMaterial(Val(txtMaterialID.Text))
            PopulateMaterials()
            ClearForm()
            If chkEditNew.Checked = True Then
                txtMaterialID.Text = NextMaterialID()
                txtMaterial.Focus()
            End If
        End If
    End Sub

    Private Sub SaveMaterial(ByVal Material_ID As Integer)


        Using connection As New SqlConnection(connString)
            connection.Open()

            ' Check if the material exists
            Dim selectQuery As String = "SELECT * FROM Materials WHERE ID = @MaterialID"
            Using selectCommand As New SqlCommand(selectQuery, connection)
                selectCommand.Parameters.AddWithValue("@MaterialID", Material_ID)

                Using reader As SqlDataReader = selectCommand.ExecuteReader()
                    If reader.HasRows Then
                        ' Update existing material
                        reader.Close()
                        Dim updateQuery As String = "
                        UPDATE Materials 
                        SET Name = @Name, UoM = @UoM 
                        WHERE ID = @MaterialID"
                        Using updateCommand As New SqlCommand(updateQuery, connection)
                            updateCommand.Parameters.AddWithValue("@MaterialID", Material_ID)
                            updateCommand.Parameters.AddWithValue("@Name", txtMaterial.Text)
                            updateCommand.Parameters.AddWithValue("@UoM", txtUoM.Text)
                            updateCommand.ExecuteNonQuery()
                        End Using
                    Else
                        reader.Close()
                        ' Insert new material
                        Dim insertQuery As String = "
                        INSERT INTO Materials (ID, Name, UoM) 
                        VALUES (@MaterialID, @Name, @UoM)"
                        Using insertCommand As New SqlCommand(insertQuery, connection)
                            insertCommand.Parameters.AddWithValue("@MaterialID", Val(txtMaterialID.Text))
                            insertCommand.Parameters.AddWithValue("@Name", txtMaterial.Text)
                            insertCommand.Parameters.AddWithValue("@UoM", txtUoM.Text)
                            insertCommand.ExecuteNonQuery()
                        End Using
                    End If
                End Using
            End Using
        End Using
    End Sub

    Private Sub PopulateMaterials()
        dgvMaterials.Rows.Clear()

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT * FROM Materials"
            Using command As New SqlCommand(query, connection)
                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.HasRows Then
                        While reader.Read()
                            dgvMaterials.Rows.Add(
                            reader("ID"),
                            reader("Name"),
                            reader("UoM")
                        )
                        End While
                    End If
                End Using
            End Using
        End Using
    End Sub

    Private Sub dgvMaterials_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMaterials.CellDoubleClick
        ClearForm()
        txtMaterialID.Text = dgvMaterials.Rows(e.RowIndex).Cells(0).Value
        txtMaterial.Text = dgvMaterials.Rows(e.RowIndex).Cells(1).Value
        txtUoM.Text = dgvMaterials.Rows(e.RowIndex).Cells(2).Value
    End Sub

    Private Sub txtMaterial_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMaterial.LostFocus
        If chkEditNew.Checked = True Then
            If IsDuplicate(txtMaterial.Text) Then
                MsgBox("A material with the same name already exists. Duplicate record can not be created.")
                txtMaterial.Text = ""
                txtMaterial.Focus()
            End If
        End If
    End Sub
    Private Function IsDuplicate(ByVal Material As String) As Boolean
        Dim i As Integer
        Dim Duplicate As Boolean = False
        For i = 0 To dgvMaterials.RowCount - 1
            If Material = dgvMaterials.Rows(i).Cells(1).Value Then
                Duplicate = True
                Exit For
            End If
        Next
        IsDuplicate = Duplicate
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim RetVal As Integer
        If txtMaterialID.Text <> "" And txtMaterial.Text <> "" Then
            If MaterialUsed(Val(txtMaterialID.Text)) = False Then
                RetVal = MsgBox("It is suggested to keep the record and modify " & _
                "it according to your requirement. Are you sure you want to " & _
                "delete it instead?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
                If RetVal = vbYes Then
                    ExecuteSqlProcedure("Delete from Materials where ID = " & Val(txtMaterialID.Text))
                    txtMaterialID.Text = ""
                    txtMaterial.Text = ""
                    txtUoM.Text = ""
                    PopulateMaterials()
                    txtMaterialID.Focus()
                End If
            Else
                MsgBox("The material record has been used in analytes. So, it can " & _
                "not be deleted. In order to delete any material, first remove " & _
                "all its references then try it.", MsgBoxStyle.Critical, "Prolis")
            End If
        End If
    End Sub

    Private Function MaterialUsed(ByVal MatID As Integer) As Boolean
        Dim used As Boolean = False

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT * FROM Test_Material WHERE Material_ID = @MaterialID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@MaterialID", MatID)

                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.HasRows Then
                        used = True
                    End If
                End Using
            End Using
        End Using

        Return used
    End Function
End Class
