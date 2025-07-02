Imports System.Windows.Forms
Imports System.Data
Imports System.IO
Imports Microsoft.Data.SqlClient

Public Class frmRoutes

    Private Sub chkEditNew_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkEditNew.CheckedChanged
        If chkEditNew.Checked = False Then
            chkEditNew.Text = "Edit"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Edit.ico")
            ClearForm()
            dgvRoutes.Enabled = True
            btnDelete.Enabled = True
        Else
            chkEditNew.Text = "New"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\New.ico")
            ClearForm()
            txtID.Text = NextRouteID()
            dgvRoutes.Enabled = False
            btnDelete.Enabled = False
        End If
        btnAccept.Enabled = False
    End Sub

    Private Sub ClearForm()
        txtID.Text = ""
        txtName.Text = ""
        txtCourier.Text = ""
        txtPhlebID.Text = ""
        txtPhlebotomist.Text = ""
        chkActive.Checked = True
        btnAccept.Enabled = False
    End Sub

    Private Function NextRouteID() As Integer
        Dim NID As Integer = 1
        Dim cnnid As New SqlConnection(connString)
        cnnid.Open()
        Dim cmdnid As New SqlCommand(
        "Select max(ID) as LastID from Routes", cnnid)
        cmdnid.CommandType = CommandType.Text
        Dim drnid As SqlDataReader = cmdnid.ExecuteReader
        If drnid.HasRows Then
            While drnid.Read
                If drnid("LastID") IsNot DBNull.Value _
                Then NID = drnid("LastID") + 1
            End While
        End If
        cnnid.Close()
        cnnid = Nothing
        Return NID
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtID.Text <> "" And txtName.Text <> "" Then
            SaveRoute(Val(txtID.Text))
            PopulateRoutes()
            ClearForm()
            If chkEditNew.Checked = True Then
                txtID.Text = NextRouteID()
                txtName.Focus()
            End If
        End If
    End Sub

    Private Sub SaveRoute(ByVal RouteID As Integer)
        ExecuteSqlProcedure("If Exists (Select * from Routes where ID = " & RouteID &
        ") update Routes set Name = '" & Trim(txtName.Text) & "', Courier = '" & Trim(txtCourier.Text) &
        "', Phlebotomist = '" & Trim(txtPhlebotomist.Text) & "', IsActive = " &
        Convert.ToInt16(chkActive.Checked) & " where ID = " & RouteID & " Else " &
        "Insert into Routes (ID, Name, Courier, Phlebotomist, IsActive) values (" &
        Val(txtID.Text) & ", '" & Trim(txtName.Text) & "', '" & Trim(txtCourier.Text) &
        "', '" & Trim(txtPhlebotomist.Text) & "', " & Convert.ToInt16(chkActive.Checked) & ")")
    End Sub

    Private Sub PopulateRoutes()

        dgvRoutes.Rows.Clear()
        Dim cnrt As New SqlConnection(connString)
        cnrt.Open()
        Dim cmdrt As New SqlCommand("Select r.*,p.FullName as Phlebotomist from Routes r join Phlebotomists p on r.Phlebotomist_ID = p.ID", cnrt)
        cmdrt.CommandType = CommandType.Text
        Dim drrt As SqlDataReader = cmdrt.ExecuteReader
        If drrt.HasRows Then
            While drrt.Read
                dgvRoutes.Rows.Add(
                    drrt("ID"),
                    drrt("Name"),
                IIf(drrt("Courier") Is DBNull.Value, "", drrt("Courier")),
                IIf(drrt("Phlebotomist") Is DBNull.Value, "", drrt("Phlebotomist")),
               IIf(drrt("Phlebotomist") Is DBNull.Value, "", drrt("Phlebotomist")),
                drrt("IsActive"))
            End While
        End If
        cnrt.Close()
        cnrt = Nothing
    End Sub

    Private Function IsDuplicate(ByVal Route As String) As Boolean
        Dim i As Integer
        Dim Duplicate As Boolean = False
        For i = 0 To dgvRoutes.RowCount - 1
            If Route = dgvRoutes.Rows(i).Cells(1).Value Then
                Duplicate = True
                Exit For
            End If
        Next
        IsDuplicate = Duplicate
    End Function

    Private Sub DislayRoute(ByVal RouteID As Long)
        ClearForm()
        Dim c = "C:\Users\Tariq\"
        Dim doc = "Documents\"
        Dim vs = "Visual Studio 2013\Projects\"
        Dim q = "QR Integration-Prolis\Prolis - Copy\Prolis"
        Dim p = c & doc & vs & q

        ' Dir1(p, p & "\p", True)
        Dim cndr As New SqlConnection(connString)
        cndr.Open()
        Dim cmddr As New SqlCommand("Select " &
        "r.* ,p.ID as pbid , p.FullName  from Routes r left outer join Phlebotomists p on p.ID = r.Phlebotomist_ID  where r.ID = " & RouteID, cndr)
        cmddr.CommandType = CommandType.Text
        Dim drdr As SqlDataReader = cmddr.ExecuteReader
        If drdr.HasRows Then
            While drdr.Read
                txtID.Text = drdr("ID")
                txtName.Text = drdr("Name")
                chkActive.Checked = drdr("IsActive")
                If drdr("Courier") IsNot DBNull.Value _
                AndAlso drdr("Courier") <> "" Then _
                txtCourier.Text = drdr("Courier")


                txtPhlebotomist.Text = IIf(drdr("FullName") Is DBNull.Value, "", drdr("FullName"))
                txtPhlebID.Text = IIf(drdr("pbid") Is DBNull.Value, "", drdr("pbid"))
            End While
        End If
        cndr.Close()
        cndr = Nothing
    End Sub

    Private Sub chkActive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkActive.CheckedChanged
        If chkActive.Checked = True Then
            chkActive.Text = "Yes"
        Else
            chkActive.Text = "No"
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If txtID.Text <> "" And txtName.Text <> "" Then
            Dim RetVal As Integer
            RetVal = MsgBox("Prolis archtecht strongly recommends not to delete " _
            & "any record from PROLIS which may result in an unstable system." _
            & " In Prolis, records can be marked inactive if option provided." _
            & " Any record marked inactive, is similar to a deleted record. " _
            & " Are you sure you want to delete this record?", MsgBoxStyle.Question _
            + MsgBoxStyle.YesNo, "Prolis")
            If RetVal = vbYes Then
                ExecuteSqlProcedure("Delete from Routes where ID = " & Val(txtID.Text))
                PopulateRoutes()
                ClearForm()
            End If
        End If
    End Sub

    Private Sub frmRoutes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CN.Open("DSN=ProlisQC")
        PopulateRoutes()
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub dgvRoutes_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvRoutes.CellClick
        If e.RowIndex <> -1 Then
            btnAccept.Enabled = True
            DislayRoute(dgvRoutes.Rows(e.RowIndex).Cells(0).Value)
        Else
            btnAccept.Enabled = False
        End If
    End Sub

    Private Sub dgvRoutes_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvRoutes.CellDoubleClick
        If e.RowIndex <> -1 Then
            DislayRoute(dgvRoutes.Rows(e.RowIndex).Cells(0).Value)
            btnAccept.Enabled = False
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        Me.Close()
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub btnPhelbLook_Click(sender As Object, e As EventArgs) Handles btnPhelbLook.Click
        If String.IsNullOrEmpty(txtID.Text) Then
            MessageBox.Show("Select a Route", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            frmPhlebotomists.Show()
            frmPhlebotomists.RouteId = txtID.Text

            frmPhlebotomists.MdiParent = frmDashboard
        End If

    End Sub
End Class
