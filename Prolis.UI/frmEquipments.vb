Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmEquipments
    Private Sub frmEquipments_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CN.Open("DSN=ProlisQC;UID=sa;PWD=''")
        Populate_Equips()
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub
    Private Sub Populate_Equips()
        dgvEquips.Rows.Clear()
        Dim IFaceType As String = ""
        Dim cnpe As New SqlConnection(connString)
        cnpe.Open()
        Dim cmdpe As New SqlCommand("Select " &
        "* from Equipments order by Name", cnpe)
        cmdpe.CommandType = CommandType.Text
        Dim drpe As SqlDataReader = cmdpe.ExecuteReader
        If drpe.HasRows Then
            While drpe.Read
                If drpe("IPBased") = 0 Then
                    IFaceType = "Serial"
                Else
                    IFaceType = "Network"
                End If
                dgvEquips.Rows.Add(drpe("ID"), drpe("Name"),
                drpe("Active"), IFaceType, drpe("Comm_DLL"),
                drpe("BaudRate"), drpe("Parity"),
                drpe("DataBits"), drpe("StopBit"))
            End While
        End If
        cnpe.Close()
        cnpe = Nothing
    End Sub

    Private Sub chkEditNew_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkEditNew.CheckedChanged
        If chkEditNew.Checked = False Then
            chkEditNew.Text = "Edit"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Edit.ico")
            dgvEquips.Enabled = True
            txtEquipID.Text = ""
        Else
            chkEditNew.Text = "New"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\New.ico")
            dgvEquips.Enabled = False
            txtEquipID.Text = NextEquipID()
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim RetVal As Integer
        If dgvEquips.SelectedRows(0).Index <> -1 Then
            If Equip_Used(dgvEquips.SelectedRows(0).Cells(0).Value) Then
                MsgBox("The equipment you are trying to delete, has been " _
                & "used in the analysis set up. To avoid the record left " _
                & "orphan, PROLIS won't let you delete this record")
            Else
                RetVal = MsgBox("Generally you should not delete any equipment. " _
                & "Are you sure to delete this record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo)
                If RetVal = vbYes Then
                    ExecuteSqlProcedure("Delete from Equipments where ID = " & dgvEquips.SelectedRows(0).Cells(0).Value)
                    FormClear()
                    Populate_Equips()
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                End If
            End If
        End If
    End Sub

    Private Function Equip_Used(ByVal Equipment_ID As Integer) As Boolean
        Dim Used As Boolean = False
        Dim cnu As New SqlConnection(connString)
        cnu.Open()
        Dim cmdu As New SqlCommand("Select * from Anas where " &
        "Equipment_ID = " & Val(dgvEquips.SelectedRows(0).Cells(0).Value), cnu)
        cmdu.CommandType = CommandType.Text
        Dim dru As SqlDataReader = cmdu.ExecuteReader
        If dru.HasRows Then Used = True
        cnu.Close()
        cnu = Nothing
        Return Used
    End Function

    Private Sub FormClear()
        txtEquipID.Text = ""
        txtEquipName.Text = ""
        txtCommDLL.Text = ""
        cmbBaud.SelectedIndex = -1
        cmbParity.SelectedIndex = -1
        cmbData.SelectedIndex = -1
        cmbStop.SelectedIndex = -1
        chkSerialIP.Checked = False
    End Sub

    Private Function NextEquipID() As Byte
        Dim EID As Integer = 1
        Dim cne As New SqlConnection(connString)
        cne.Open()
        Dim cmde As New SqlCommand("Select " &
        "max(ID) as LastID from Equipments", cne)
        cmde.CommandType = CommandType.Text
        Dim dre As SqlDataReader = cmde.ExecuteReader
        If dre.HasRows Then
            While dre.Read
                If dre("LastID") IsNot DBNull.Value _
                Then EID = dre("LastID") + 1
            End While
        End If
        cne.Close()
        cne = Nothing
        Return EID
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtEquipID.Text <> "" And txtEquipName.Text <> "" And txtCommDLL.Text <> "" _
        And ((chkSerialIP.Checked = False And cmbBaud.SelectedIndex <> -1 And
        cmbParity.SelectedIndex <> -1 And cmbData.SelectedIndex <> -1 And
        cmbStop.SelectedIndex <> -1) Or (chkSerialIP.Checked = True)) Then
            Dim cneq As New SqlConnection(connString)
            cneq.Open()
            Dim cmdupsert As New SqlCommand("Equipments_SP", cneq)
            cmdupsert.CommandType = CommandType.StoredProcedure
            cmdupsert.Parameters.AddWithValue("@act", "Upsert")
            cmdupsert.Parameters.AddWithValue("@ID", Val(txtEquipID.Text))
            cmdupsert.Parameters.AddWithValue("@Name", Trim(txtEquipName.Text))
            cmdupsert.Parameters.AddWithValue("@Active", chkActive.Checked)
            cmdupsert.Parameters.AddWithValue("@IPBased", chkSerialIP.Checked)
            cmdupsert.Parameters.AddWithValue("@Port", "NONE")
            cmdupsert.Parameters.AddWithValue("@Comm_DLL", Trim(txtCommDLL.Text))
            If chkSerialIP.Checked = False Then
                cmdupsert.Parameters.AddWithValue("@BaudRate", Trim(cmbBaud.SelectedItem.ToString))
                cmdupsert.Parameters.AddWithValue("@Parity", Trim(cmbParity.SelectedItem.ToString))
                cmdupsert.Parameters.AddWithValue("@DataBits", Trim(cmbData.SelectedItem.ToString))
                cmdupsert.Parameters.AddWithValue("@StopBit", Trim(cmbStop.SelectedItem.ToString))
            Else
                cmdupsert.Parameters.AddWithValue("@BaudRate", "")
                cmdupsert.Parameters.AddWithValue("@Parity", "")
                cmdupsert.Parameters.AddWithValue("@DataBits", "")
                cmdupsert.Parameters.AddWithValue("@StopBit", "")
            End If
            cmdupsert.Parameters.AddWithValue("@IsOnLine", 0)
            Try
                cmdupsert.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                cneq.Close()
                cneq = Nothing
            End Try
            Populate_Equips()
            FormClear()
            If chkEditNew.Checked = True Then _
                txtEquipID.Text = NextEquipID()
        Else
            MsgBox("You need to enter all the Equipment information, to save it.")
            txtEquipName.Focus()
        End If
    End Sub

    Private Sub dgvEquips_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvEquips.CellDoubleClick
        Dim i As Integer
        If e.RowIndex <> -1 Then
            txtEquipID.Text = ""
            txtEquipName.Text = ""
            txtCommDLL.Text = ""
            txtEquipID.Text = dgvEquips.Rows(e.RowIndex).Cells(0).Value
            txtEquipName.Text = dgvEquips.Rows(e.RowIndex).Cells(1).Value
            chkActive.Checked = dgvEquips.Rows(e.RowIndex).Cells(2).Value
            If dgvEquips.Rows(e.RowIndex).Cells(3).Value = "Serial" Then
                chkSerialIP.Checked = False
            Else
                chkSerialIP.Checked = True
            End If
            txtCommDLL.Text = IIf(dgvEquips.Rows(e.RowIndex).Cells(4).Value Is _
            System.DBNull.Value, "", dgvEquips.Rows(e.RowIndex).Cells(4).Value)
            For i = 0 To cmbBaud.Items.Count - 1
                If dgvEquips.Rows(e.RowIndex).Cells(5).Value Is System.DBNull.Value Then
                    cmbBaud.SelectedIndex = -1
                    Exit For
                Else
                    If Trim(cmbBaud.Items(i).ToString) = Trim(dgvEquips.Rows(e.RowIndex).Cells(5).Value) Then
                        cmbBaud.SelectedIndex = i
                        Exit For
                    End If
                End If
            Next
            For i = 0 To cmbParity.Items.Count - 1
                If dgvEquips.Rows(e.RowIndex).Cells(6).Value Is System.DBNull.Value Then
                    cmbParity.SelectedIndex = -1
                    Exit For
                Else
                    If Trim(cmbParity.Items(i).ToString) = Trim(dgvEquips.Rows(e.RowIndex).Cells(6).Value) Then
                        cmbParity.SelectedIndex = i
                        Exit For
                    End If
                End If
            Next
            For i = 0 To cmbData.Items.Count - 1
                If dgvEquips.Rows(e.RowIndex).Cells(7).Value Is System.DBNull.Value Then
                    cmbData.SelectedIndex = -1
                    Exit For
                Else
                    If Trim(cmbData.Items(i).ToString) = Trim(dgvEquips.Rows(e.RowIndex).Cells(7).Value) Then
                        cmbData.SelectedIndex = i
                        Exit For
                    End If
                End If
            Next
            For i = 0 To cmbStop.Items.Count - 1
                If dgvEquips.Rows(e.RowIndex).Cells(8).Value Is System.DBNull.Value Then
                    cmbStop.SelectedIndex = -1
                    Exit For
                Else
                    If Trim(cmbStop.Items(i).ToString) = Trim(dgvEquips.Rows(e.RowIndex).Cells(8).Value) Then
                        cmbStop.SelectedIndex = i
                        Exit For
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub txtEquipName_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEquipName.Validated
        If txtEquipID.Text <> "" And txtEquipName.Text <> "" And txtCommDLL.Text <> "" _
        And cmbBaud.SelectedIndex <> -1 And cmbParity.SelectedIndex <> -1 And _
        cmbData.SelectedIndex <> -1 And cmbStop.SelectedIndex <> -1 Then btnSave.Enabled = True
    End Sub

    Private Sub txtCommDLL_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCommDLL.Validated
        If txtEquipID.Text <> "" And txtEquipName.Text <> "" And txtCommDLL.Text <> "" _
         And cmbBaud.SelectedIndex <> -1 And cmbParity.SelectedIndex <> -1 And _
         cmbData.SelectedIndex <> -1 And cmbStop.SelectedIndex <> -1 Then btnSave.Enabled = True
    End Sub

    Private Sub cmbBaud_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBaud.SelectedIndexChanged
        If txtEquipID.Text <> "" And txtEquipName.Text <> "" And txtCommDLL.Text <> "" _
        And cmbBaud.SelectedIndex <> -1 And cmbParity.SelectedIndex <> -1 And _
        cmbData.SelectedIndex <> -1 And cmbStop.SelectedIndex <> -1 Then btnSave.Enabled = True
    End Sub

    Private Sub cmbParity_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbParity.SelectedIndexChanged
        If txtEquipID.Text <> "" And txtEquipName.Text <> "" And txtCommDLL.Text <> "" _
 And cmbBaud.SelectedIndex <> -1 And cmbParity.SelectedIndex <> -1 And _
 cmbData.SelectedIndex <> -1 And cmbStop.SelectedIndex <> -1 Then btnSave.Enabled = True
    End Sub

    Private Sub cmbData_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbData.SelectedIndexChanged
        If txtEquipID.Text <> "" And txtEquipName.Text <> "" And txtCommDLL.Text <> "" _
 And cmbBaud.SelectedIndex <> -1 And cmbParity.SelectedIndex <> -1 And _
 cmbData.SelectedIndex <> -1 And cmbStop.SelectedIndex <> -1 Then btnSave.Enabled = True
    End Sub

    Private Sub cmbStop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbStop.SelectedIndexChanged
        If txtEquipID.Text <> "" And txtEquipName.Text <> "" And txtCommDLL.Text <> "" _
 And cmbBaud.SelectedIndex <> -1 And cmbParity.SelectedIndex <> -1 And _
 cmbData.SelectedIndex <> -1 And cmbStop.SelectedIndex <> -1 Then btnSave.Enabled = True
    End Sub

    Private Sub chkSerialIP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSerialIP.CheckedChanged
        If chkSerialIP.Checked = False Then 'Serial
            chkSerialIP.Text = "Serial"
            cmbBaud.Enabled = True
            cmbParity.Enabled = True
            cmbData.Enabled = True
            cmbStop.Enabled = True
        Else
            chkSerialIP.Text = "Network"
            cmbBaud.SelectedIndex = -1
            cmbParity.SelectedIndex = -1
            cmbData.SelectedIndex = -1
            cmbStop.SelectedIndex = -1
            cmbBaud.Enabled = False
            cmbParity.Enabled = False
            cmbData.Enabled = False
            cmbStop.Enabled = False
        End If
    End Sub
End Class
