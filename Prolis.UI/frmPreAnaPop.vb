Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmPreAnaPop
    Public Shared sHandleCreated As Boolean = False
    Public Shared btnNewChecked As Boolean = False
    Public Shared xtAccID As String = ""
    Public Shared xtID As String = ""
    Public Shared urTGP As String = ""
    Private Sub frmPreAnaPop_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'If Me.Tag.ToString.EndsWith("|") Then Me.Tag = Me.Tag.ToString.Substring(0, Len(Me.Tag) - 1)
    End Sub

    Private Sub frmPreAnaPop_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Tag = ""
        If sHandleCreated Then
            If btnNewChecked = False Then
                txtAccID.Text = ""
            Else
                txtAccID.Text = xtAccID
            End If

            txtID.Text = urTGP
            btnOK.Enabled = False
            txtTGP.Text = GetTGPName(Val(txtID.Text))
            DisplayTGPChildren(txtAccID.Text, Val(txtID.Text))
        End If
    End Sub

    Friend Sub DisplayTGPChildren(ByVal AccID As String, ByVal TGPID As String)
        dgvResults.Rows.Clear()
        Answers.Text = ""
        Dim ans As String = ""
        Dim sSQL As String = ""
        If AccID <> "" Then
            sSQL = "Select Description, b.ID, b.Name, (Select Response from Req_Info_Response where TGP_ID = a.TGP_ID and Info_ID = a.Info_ID " &
            "and Accession_ID = " & AccID & ") as Result from (TGP_Info a inner join Tests b on a.Info_ID = b.ID) where b.IsActive " &
            "<> 0 and b.HasResult <> 0 and b.Preanalytical <> 0 and a.TGP_ID = " & TGPID & " Union Select Description, b.ID, b.Name, (Select " &
            "Response from Req_Info_Response where TGP_ID = a.TGP_ID and Info_ID = a.Info_ID and Accession_ID = " & AccID & ") as " &
            "Result from (TGP_Info a inner join Tests b on a.Info_ID = b.ID) where b.IsActive <> 0 and b.HasResult <> 0 and " &
            "b.Preanalytical <> 0 and a.TGP_ID in (Select Test_ID from Group_Test where Group_ID = " & TGPID & ") Union Select " &
            "Description, b.ID, b.Name, (Select Response from Req_Info_Response where TGP_ID = a.TGP_ID and Info_ID = a.Info_ID and Accession_ID " &
            "= " & AccID & ") as Result from (TGP_Info a inner join Tests b on a.Info_ID = b.ID) where b.IsActive <> 0 and " &
            "b.HasResult <> 0 and b.Preanalytical <> 0 and a.TGP_ID in ( Select GrpTst_ID from Prof_GrpTst where Profile_ID = " &
            TGPID & ") Union Select Description, b.ID, b.Name, (Select Response from Req_Info_Response where TGP_ID = a.TGP_ID and Info_ID = " &
            "a.Info_ID and Accession_ID = " & AccID & ") as Result from (TGP_Info a inner join Tests b on a.Info_ID = b.ID) where " &
            "b.IsActive <> 0 and b.HasResult <> 0 and b.Preanalytical <> 0 and a.TGP_ID in (Select Test_ID from Group_Test where " &
            "Group_ID in (Select GrpTst_ID from Prof_GrpTst where Profile_ID = " & TGPID & "))"
        Else
            sSQL = "Select Description, b.ID, b.Name, '' as Result from TGP_Info a inner join Tests b on a.Info_ID = b.ID where b.IsActive <> 0 " &
            "and b.HasResult <> 0 and b.Preanalytical <> 0 and a.TGP_ID = " & TGPID & " Union Select Description, b.ID, b.Name, '' as Result " &
            "from TGP_Info a inner join Tests b on a.Info_ID = b.ID where b.IsActive <> 0 and b.HasResult <> 0 and b.Preanalytical " &
            "<> 0 and a.TGP_ID in (Select Test_ID from Group_Test where Group_ID = " & TGPID & ") Union Select Description, b.ID, b.Name, '' as " &
            "Result from TGP_Info a inner join Tests b on a.Info_ID = b.ID where b.IsActive <> 0 and b.HasResult <> 0 and " &
            "b.Preanalytical <> 0 and a.TGP_ID in (Select GrpTst_ID from Prof_GrpTst where Profile_ID = " & TGPID & ") Union Select " &
            "Description, b.ID, b.Name, '' as Result from TGP_Info a inner join Tests b on a.Info_ID = b.ID where b.IsActive <> 0 and b.HasResult " &
            "<> 0 and b.Preanalytical <> 0 and a.TGP_ID in (Select Test_ID from Group_Test where Group_ID in (Select GrpTst_ID from " &
            "Prof_GrpTst where Profile_ID = " & TGPID & "))"
        End If
        '
        Dim cnpa As New SqlConnection(connString)
        cnpa.Open()
        Dim cmdpa As New SqlCommand(sSQL, cnpa)
        cmdpa.CommandType = CommandType.Text
        Dim drpa As SqlDataReader = cmdpa.ExecuteReader
        If drpa.HasRows Then
            While drpa.Read
                dgvResults.Rows.Add(drpa("ID"), drpa("Name"), drpa("Result"))
                ans += drpa("Description")
                dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).Style.BackColor = Color.White
            End While
        End If
        cnpa.Close()
        cnpa = Nothing
        Answers.Text = ans
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'built in the form closing event
    End Function

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim Data As String = ""
        dgvResults.EndEdit()
        For i As Integer = 0 To dgvResults.RowCount - 1
            If dgvResults.Rows(i).Cells(2).Value IsNot Nothing _
            AndAlso Trim(dgvResults.Rows(i).Cells(2).Value) <> "" Then
                Data += Trim(txtID.Text) & "^" & Trim(dgvResults.Rows(i).Cells(0).Value.ToString) _
                & "^" & Trim(dgvResults.Rows(i).Cells(2).Value.ToString) & "|"
            End If
        Next
        If Data.EndsWith("|") Then Data = Data.Substring(0, Len(Data) - 1)
        Me.Tag = Data
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub dgvResults_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvResults.CellEndEdit
        btnOK.Enabled = True
    End Sub
End Class
