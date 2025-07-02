Public Class frmReasonLookUp

    Private Sub btnLook_Click(sender As Object, e As EventArgs) Handles btnLook.Click
        Me.Cursor = Cursors.WaitCursor

        dgv.Rows.Clear()
        Dim sSQL As String = ""
        Dim Code As String = ""
        Dim PC As String = ""
        Dim CodeType As String = ""
        If Trim(txtTerm.Text) <> "" Then
            If Trim(txtTerm.Text).Contains("-") Then
                Dim Parts() As String = Split(Trim(txtTerm.Text), "-")
                PC = Trim(Parts(0))
                If Parts(1).Contains(",") Then
                    Code = Trim(Parts(1).Substring(0, InStr(Parts(1), ",") - 1))
                Else
                    Code = Trim(Parts(1))
                End If
            Else
                PC = ""
                Code = Trim(txtTerm.Text)
            End If
        End If
        If PC <> "" Then
            sSQL = "Select * from ReasonRemarks where Code = '" & PC & "'"
            Dim cnrc As New SqlClient.SqlConnection(connString)
            cnrc.Open()
            Dim cmdrc As New SqlClient.SqlCommand(sSQL, cnrc)
            cmdrc.CommandType = CommandType.Text
            Dim drrc As SqlClient.SqlDataReader = cmdrc.ExecuteReader
            If drrc.HasRows Then
                While drrc.Read
                    CodeType = drrc("Description")
                End While
            End If
            cnrc.Close()
            cnrc = Nothing
        Else
            CodeType = ""
        End If
        If Code <> "" Then
            sSQL = "Select * from ReasonRemarks where Code = '" & Code & "'"
        Else
            sSQL = "Select * from ReasonRemarks"
        End If
        Dim cnrc1 As New SqlClient.SqlConnection(connString)
        cnrc1.Open()
        Dim cmdrc1 As New SqlClient.SqlCommand(sSQL, cnrc1)
        cmdrc1.CommandType = CommandType.Text
        Dim drrc1 As SqlClient.SqlDataReader = cmdrc1.ExecuteReader
        If drrc1.HasRows Then
            While drrc1.Read
                If CodeType <> "" Then
                    dgv.Rows.Add(PC, CodeType)
                    dgv.Rows.Add(drrc1("Code"), drrc1("Description"))
                Else
                    dgv.Rows.Add(drrc1("Code"), drrc1("Description"))
                End If
            End While
        End If
        cnrc1.Close()
        cnrc1 = Nothing

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub frmReasonLookUp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btnLook_Click(Nothing, Nothing)
    End Sub

    Private Sub btn_Cancel_Click(sender As Object, e As EventArgs) Handles btn_Cancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub


End Class