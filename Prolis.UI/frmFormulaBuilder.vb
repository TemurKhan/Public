Imports System.Windows.Forms
Imports Microsoft.Data.SqlClient

Public Class frmFormulaBuilder

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub LoadTests()
        tvTests.Nodes.Clear()

        Using connection As New SqlConnection(connString)
            connection.Open()

            ' First Query: For Choice Analytes
            Dim choiceQuery As String = "SELECT * FROM Tests WHERE HasResult <> 0 AND Qualitative <> 0 ORDER BY Name"
            Using choiceCommand As New SqlCommand(choiceQuery, connection)
                Using reader As SqlDataReader = choiceCommand.ExecuteReader()
                    If reader.HasRows Then
                        Dim choiceNode As New TreeNode("Choice Analytes")
                        tvTests.Nodes.Add(choiceNode)
                        While reader.Read()
                            Dim childNode As New TreeNode(reader("Name").ToString())
                            childNode.Tag = reader("ID")
                            choiceNode.Nodes.Add(childNode)
                        End While
                    End If
                End Using
            End Using

            ' Second Query: For Numeric Analytes
            Dim numericQuery As String = "SELECT * FROM Tests WHERE HasResult <> 0 AND Qualitative = 0 ORDER BY Name"
            Using numericCommand As New SqlCommand(numericQuery, connection)
                Using reader As SqlDataReader = numericCommand.ExecuteReader()
                    If reader.HasRows Then
                        Dim numericNode As New TreeNode("Numeric Analytes")
                        tvTests.Nodes.Add(numericNode)
                        While reader.Read()
                            Dim childNode As New TreeNode(reader("Name").ToString())
                            childNode.Tag = reader("ID")
                            numericNode.Nodes.Add(childNode)
                        End While
                    End If
                End Using
            End Using
        End Using
    End Sub
    Private Sub frmFormulaBuilder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CN.Open("DSN=ProlisQC;UID=sa;PWD=''")
        LoadTests()
        txtFormula.Text = frmTests.txtFormula.Text
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub tvTests_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvTests.NodeMouseDoubleClick
        If e.Node.Level <> 0 Then
            txtFormula.SelectedText = "{" & e.Node.Tag & "} "
            txtFormula.Focus()
        End If
    End Sub

    Private Sub tvOperators_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvOperators.NodeMouseDoubleClick
        Dim SelPoint As Integer
        If e.Node.Level <> 0 Then
            If e.Node.Parent.Name = "Operators" Then
                If e.Node.Name.Substring(0, 3) = "( )" Then
                    txtFormula.SelectedText = "( " & txtFormula.SelectedText & " )"
                Else
                    txtFormula.SelectedText = e.Node.Name.Substring(0, 2)
                End If
                txtFormula.Focus()
            Else
                SelPoint = txtFormula.SelectionStart
                txtFormula.SelectedText = e.Node.Name & " "
                txtFormula.SelectionStart = SelPoint + InStr(e.Node.Name, "(") + 1
                txtFormula.Focus()
            End If
        End If
    End Sub

    Private Sub btnSyntax_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSyntax.Click
        If txtFormula.Text = "" Then
            MsgBox("No error found")
        Else
            Dim RetVal = ValidateFormula(txtFormula.Text)
            If RetVal = 0 Then
                MsgBox("No error found")
            ElseIf RetVal = 1 Then
                MsgBox("Opening bracket '(' is always used with a closing bracket ')' and you are missing one")
            ElseIf RetVal = 2 Then
                MsgBox("Opening ResultOf '{' is always used with a closing ResultOf '}' and you are missing one")
            ElseIf RetVal = 3 Then
                MsgBox("Number of Operands must be greater in quantity than Operators. An Operand is expected.")
                txtFormula.SelectionStart = Len(txtFormula.Text)
            ElseIf RetVal = 4 Then
                MsgBox("Number of Operators are less than expected. An Operator is expected.")
            End If
        End If
    End Sub
    Private Function ValidateFormula(ByVal Formula As String) As Integer
        Dim i As Integer
        Dim str() As Char = Formula.ToCharArray
        Dim OBS As Integer
        Dim CBS As Integer
        Dim ROOBS As Integer
        Dim ROCBS As Integer
        Dim Operators As Integer
        Dim Operands As Integer
        'Dim FACS As Integer
        '
        For i = LBound(str) To UBound(str)
            If str(i) = "(" Then OBS = OBS + 1
            If str(i) = ")" Then CBS = CBS + 1
            If str(i) = "{" Then ROOBS = ROOBS + 1
            '
            If str(i) = "}" Then ROCBS = ROCBS + 1

            If (str(i) = "-" Or str(i) = "+" Or str(i) = "*" Or str(i) = "^" Or str(i) = "/" Or str(i) = "\") Then Operators = Operators + 1
        Next
        'If ROOBS = ROCBS Then ROS = ROOBS
        Operands = GETOPERANDS(Formula)
        If ((OBS > 0 Or CBS > 0) And OBS <> CBS) Then
            ValidateFormula = 1
        ElseIf ((ROOBS > 0 Or ROCBS > 0) And ROOBS <> ROCBS) Then
            ValidateFormula = 2
        ElseIf Operands = Operators Then
            ValidateFormula = 3
        ElseIf Operands <> Operators + 1 Then
            ValidateFormula = 4
        Else
            ValidateFormula = 0
        End If
    End Function
    Private Function GETOPERANDS(ByVal Formula As String) As Integer
        Dim i As Integer
        Dim str As String = Replace(Formula, " ", "")
        Dim strArray() As String
        Dim FACS As Integer = 0
        str = Replace(str, "-", " ")
        str = Replace(str, "+", " ")
        str = Replace(str, "*", " ")
        str = Replace(str, "/", " ")
        str = Replace(str, "\", " ")
        str = Replace(str, "^", " ")
        strArray = Split(str, " ")
        For i = LBound(strArray) To UBound(strArray)
            If strArray(i) <> "" Then FACS = FACS + 1
        Next
        GETOPERANDS = FACS
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        frmTests.txtFormula.Text = txtFormula.Text
        txtFormula.Text = ""
        Me.Close()
    End Sub
End Class
