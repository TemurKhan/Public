Imports System.Windows.Forms
Imports System.data

Public Class frmAccCompsLookUp

    Private Sub AccCompsLookUp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadComponents(Val(frmSendOuts.txtAccID.Text))
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub LoadComponents(ByVal AccID As Long)
        Me.Cursor = Cursors.WaitCursor

        dgvTests.Rows.Clear()
        Dim ImgPath As System.Drawing.Image
        'and not TGP_ID in (Select z.TGP_ID from Sendout_TGP " & _
        '"z inner join Sendouts y on y.ID = z.Sendout_ID where y.Accession_ID = " & AccID & ")
        Dim sSQL As String = "Select TGP_Type, TGP_ID, IsStat from Req_TGP where Accession_ID " & _
        "in (Select ID from Requisitions where InHouse = 0) and Accession_ID = " & AccID & _
        "and not TGP_ID in (Select z.TGP_ID from Sendout_TGP z inner join Sendouts y on y.ID = " & _
        "z.Sendout_ID where y.Accession_ID = " & AccID & ") Union Select a.TGP_Type, a.TGP_ID, " & _
        "a.IsStat from Req_TGP a inner join Profiles b on b.ID = a.TGP_ID where b.InHouse = 0 " & _
        "and a.Accession_ID = " & AccID & " and not a.TGP_ID in (Select z.TGP_ID from Sendout_TGP " & _
        "z inner join Sendouts y on y.ID = z.Sendout_ID where y.Accession_ID = " & AccID & ")" & _
        " Union Select a.TGP_Type, a.TGP_ID, a.IsStat from Req_TGP a inner join Groups b on b.ID = " & _
        "a.TGP_ID where b.InHouse = 0 and a.Accession_ID = " & AccID & " and not a.TGP_ID in (Select " & _
        "z.TGP_ID from Sendout_TGP z inner join Sendouts y on y.ID = z.Sendout_ID where y.Accession_ID = " & _
        AccID & ") Union Select a.TGP_Type, a.TGP_ID, a.IsStat from Req_TGP a inner join Tests b on " & _
        "b.ID = a.TGP_ID where b.InHouse = 0 and a.Accession_ID = " & AccID & " and not a.TGP_ID in " & _
        "(Select z.TGP_ID from Sendout_TGP z inner join Sendouts y on y.ID = z.Sendout_ID where " & _
        "y.Accession_ID = " & AccID & ") Union Select c.ComponentType as TGP_Type, c.ID as TGP_ID, " & _
        "a.IsStat as IsStat from Req_TGP a inner join (Group_Test b inner join Tests c on c.ID = " & _
        "b.Test_ID) on b.Group_ID = a.TGP_ID where c.InHouse = 0 and b.Group_ID in (Select ID from " & _
        "Groups where Inhouse <> 0) and a.Accession_ID = " & AccID & " and not a.TGP_ID in (Select " & _
        "z.TGP_ID from Sendout_TGP z inner join Sendouts y on y.ID = z.Sendout_ID where y.Accession_ID = " _
        & AccID & ") Union Select c.ComponentType as TGP_Type, c.ID as TGP_ID, a.IsStat as IsStat from " & _
        "Req_TGP a inner join (Prof_GrpTst b inner join Groups c on c.ID = b.GrpTst_ID) on b.Profile_ID " & _
        "= a.TGP_ID where c.InHouse = 0 and b.Profile_ID in (Select ID from Profiles where Inhouse <> 0) " & _
        "and a.Accession_ID = " & AccID & " and not a.TGP_ID in (Select z.TGP_ID from Sendout_TGP z " & _
        "inner join Sendouts y on y.ID = z.Sendout_ID where y.Accession_ID = " & AccID & ") Union Select " & _
        "c.ComponentType as TGP_Type, c.ID as TGP_ID, a.IsStat as IsStat from Req_TGP a inner join " & _
        "(Prof_GrpTst b inner join Tests c on c.ID = b.GrpTst_ID) on b.Profile_ID = a.TGP_ID where " & _
        "c.InHouse = 0 and b.Profile_ID in (Select ID from Profiles where Inhouse <> 0) and a.Accession_ID = " & _
        AccID & " and not a.TGP_ID in (Select z.TGP_ID from Sendout_TGP z inner join Sendouts y on y.ID = " & _
        "z.Sendout_ID where y.Accession_ID = " & AccID & ") Union Select d.ComponentType as TGP_Type, " & _
        "d.ID as TGP_ID, a.IsStat as IsStat from Req_TGP a inner join (Prof_GrpTst b inner join " & _
        "(Group_Test c inner join Tests d on d.ID = c.Test_ID) on c.Group_ID = b.GrpTst_ID) on b.Profile_ID " & _
        "= a.TGP_ID where d.InHouse = 0 and b.Profile_ID in (Select ID from Profiles where Inhouse <> 0) " & _
        "and a.Accession_ID = " & AccID & " and not a.TGP_ID in (Select z.TGP_ID from Sendout_TGP z inner " & _
        "join Sendouts y on y.ID = z.Sendout_ID where y.Accession_ID = " & AccID & ")"

        'Dim sSQL As String = "Select TGP_Type, TGP_ID, IsStat from Req_TGP where Accession_ID " & _
        '"in (Select ID from Requisitions where InHouse = 0) and Accession_ID = " & AccID & _
        '" Union Select a.TGP_Type, a.TGP_ID, a.IsStat from Req_TGP a inner join Profiles b on " & _
        '"b.ID = a.TGP_ID where b.InHouse = 0 and a.Accession_ID = " & AccID & " Union " & _
        '"Select a.TGP_Type, a.TGP_ID, a.IsStat from Req_TGP a inner join Groups b on b.ID = " & _
        '"a.TGP_ID where b.InHouse = 0 and a.Accession_ID = " & AccID & " Union Select a.TGP_Type, " & _
        '"a.TGP_ID, a.IsStat from Req_TGP a inner join Tests b on b.ID = a.TGP_ID where b.InHouse " & _
        '"= 0 and a.Accession_ID = " & AccID & " Union Select c.ComponentType as TGP_Type, c.ID " & _
        '"as TGP_ID, a.IsStat as IsStat from Req_TGP a inner join (Group_Test b inner join Tests " & _
        '"c on c.ID = b.Test_ID) on b.Group_ID = a.TGP_ID where c.InHouse = 0 and a.Accession_ID " & _
        '"= " & AccID & " Union Select c.ComponentType as TGP_Type, c.ID as TGP_ID, a.IsStat as " & _
        '"IsStat from Req_TGP a inner join (Prof_GrpTst b inner join Groups c on c.ID = " & _
        '"b.GrpTst_ID) on b.Profile_ID = a.TGP_ID where c.InHouse = 0 and a.Accession_ID = " & AccID _
        '& " Union Select c.ComponentType as TGP_Type, c.ID as TGP_ID, a.IsStat as IsStat from " & _
        '"Req_TGP a inner join (Prof_GrpTst b inner join Tests c on c.ID = b.GrpTst_ID) on " & _
        '"b.Profile_ID = a.TGP_ID where c.InHouse = 0 and a.Accession_ID = " & AccID & " Union " & _
        '"Select d.ComponentType as TGP_Type, d.ID as TGP_ID, a.IsStat as IsStat from Req_TGP a " & _
        '"inner join (Prof_GrpTst b inner join (Group_Test c inner join Tests d on d.ID = " & _
        '"c.Test_ID) on c.Group_ID = b.GrpTst_ID) on b.Profile_ID = a.TGP_ID where d.InHouse = 0 " & _
        '"and a.Accession_ID = " & AccID
        Dim cncs As New SqlClient.SqlConnection(connString)
        cncs.Open()
        Dim cmdcs As New SqlClient.SqlCommand(sSQL, cncs)
        cmdcs.CommandType = CommandType.Text
        Dim drcs As SqlClient.SqlDataReader = cmdcs.ExecuteReader
        If drcs.HasRows Then
            While drcs.Read
                If drcs("TGP_Type") = "T" Then
                    ImgPath = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Test.ico")
                ElseIf drcs("TGP_Type") = "G" Then
                    ImgPath = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Group.ico")
                Else
                    ImgPath = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Profile.ico")
                End If
                dgvTests.Rows.Add(drcs("TGP_ID"), GetTGPName(drcs("TGP_ID")), _
                ImgPath, drcs("IsStat"))
            End While
        End If
        cncs.Close()
        cncs = Nothing

        Me.Cursor = Cursors.Default
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.DialogResult  'CType(TextBox1.Text, Integer)
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click, btn_Cancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub dgvTests_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTests.CellClick
        If e.RowIndex <> -1 Then
            Me.Tag = dgvTests.Rows(e.RowIndex).Cells(0).Value.ToString
            btnOK.Enabled = True
        End If
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        btnOK.Enabled = False
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub dgvTests_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTests.CellDoubleClick
        Call btnOK_Click(Nothing, Nothing)
    End Sub

    Private Sub dgvTests_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvTests.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            If dgvTests.CurrentRow IsNot Nothing Then

                Dim rowIndex As Integer = dgvTests.CurrentRow.Index

                Me.Tag = dgvTests.Rows(rowIndex).Cells(0).Value.ToString
                Call btnOK_Click(Nothing, Nothing)
            End If
        End If

    End Sub
     
End Class
