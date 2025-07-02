Imports System.Windows.Forms
Imports System.Security.Cryptography

Public Class frmChange_PWD
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If txtExistingPWD.Text = Trim(ThisUser.Password) Then
            If txtNewPWD.Text = txtConfirmPWD.Text And IsPWDValid(txtNewPWD.Text) Then
                ChangePWD(Trim(txtNewPWD.Text))
                ThisUser.Password = Trim(txtNewPWD.Text)
                frmDashboard.LoginUser(ThisUser.ID)
                Me.Close()
            Else
                MsgBox("Your new password has to be a valid password minimum 8 character " & _
                "and maximum 20 characters long with at least one Capital letter (A thru " & _
                "Z), at least one lower case letter (a thru z), at least one number (0 " & _
                "thru 9) and at least one of the special characters !, @, #, $, %, %, ^, " & _
                "&, *, ( or ). Make sure to type the same in confirm box.")
                If Len(txtNewPWD.Text) < 3 Then
                    txtNewPWD.Focus()
                ElseIf txtNewPWD.Text <> txtConfirmPWD.Text Then
                    txtConfirmPWD.Focus()
                End If
            End If
        Else
            MsgBox("You must provide your existing password, to make the change possible")
            txtExistingPWD.Focus()
        End If
    End Sub

    Private Sub ChangePWD(ByVal PWD As String)
        ExecuteSqlProcedure("Update Users set Password = '" & encryptString(PWD) & _
        "', Change_PWD = 0 where ID = " & ThisUser.ID)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub frmChange_PWD_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ALO IsNot Nothing Then _
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub
End Class
