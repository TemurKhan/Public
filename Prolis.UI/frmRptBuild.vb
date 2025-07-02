Imports System.Windows.Forms
Imports System.data

Public Class frmRptBuild

    Private Sub frmRptBuild_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PopulateUsers()
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub PopulateUsers()
        lstUsers.Items.Clear()
        Dim cnrb As New SqlClient.SqlConnection(connString)
        cnrb.Open()
        Dim cmdrb As New SqlClient.SqlCommand(
        "Select * from Users where ID > 0", cnrb)
        cmdrb.CommandType = CommandType.Text
        Dim drrb As SqlClient.SqlDataReader = cmdrb.ExecuteReader
        If drrb.HasRows Then
            While drrb.Read
                lstUsers.Items.Add(New MyList(drrb("FullName"), drrb("ID")))
            End While
        End If
        cnrb.Close()
        cnrb = Nothing
    End Sub

    Private Sub ClearForm()
        txtID.Text = ""
        txtName.Text = ""
        txtFile.Text = ""
        txtDesc.Text = ""
        btnSave.Enabled = False
        btnDelete.Enabled = False
        btnDeAuthAll_Click(btnDeAuthAll, New System.EventArgs)
    End Sub

    Private Sub txtID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub btnAuthAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAuthAll.Click
        Dim i As Integer
        For i = 0 To lstUsers.Items.Count - 1
            lstUsers.SetItemChecked(i, True)
        Next
        UpdateProgress()
    End Sub

    Private Sub btnDeAuthAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeAuthAll.Click
        Dim i As Integer
        For i = 0 To lstUsers.Items.Count - 1
            lstUsers.SetItemChecked(i, False)
        Next
        UpdateProgress()
    End Sub

    Private Sub chkEditNew_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkEditNew.CheckedChanged
        ClearForm()
        If chkEditNew.Checked = False Then
            chkEditNew.Text = "Edit"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Edit.ico")
            btnRptLook.Enabled = True
        Else
            chkEditNew.Text = "New"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\New.ico")
            btnRptLook.Enabled = False
            txtID.Text = NextRptID()
        End If
        UpdateProgress()
    End Sub

    Private Sub txtID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtID.Validated
        If txtID.Text <> "" Then
            Dim RetVal As Integer
            If chkEditNew.Checked = False Then  'Edit
                If IsRptIDUnique(Val(txtID.Text)) = True Then
                    MsgBox("Invalid ID. Use Look up function.")
                    ClearForm()
                    txtID.Focus()
                Else
                    DisplayReport(Val(txtID.Text))
                    btnDelete.Enabled = True
                    btnSave.Enabled = True
                End If
            Else        'New
                If IsRptIDUnique(Val(txtID.Text)) = False Then
                    RetVal = MsgBox("Duplicate ID. Type in an unused ID or accept " &
                    "the system assigned ID. Click 'Yes' and type in the ID your " &
                    "self or click 'No' and the system will generate the ID for you" _
                    , MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
                    If RetVal = vbYes Then
                        txtID.Text = ""
                        txtID.Focus()
                    Else
                        txtID.Text = NextRptID()
                    End If
                End If
            End If
        Else
            ClearForm()
        End If
        UpdateProgress()
    End Sub

    Private Sub UpdateProgress()
        If txtID.Text <> "" And txtName.Text <> "" And txtFile.Text <> "" Then
            btnSave.Enabled = True
            If chkEditNew.Checked = False Then  'Edit
                btnDelete.Enabled = True
            End If
        Else
            btnSave.Enabled = False
            btnDelete.Enabled = False
        End If
    End Sub

    Private Function NextRptID() As Integer
        Dim NID As Integer = 1
        Dim cnn As New SqlClient.SqlConnection(connString)
        cnn.Open()
        Dim cmdn As New SqlClient.SqlCommand(
        "Select Max(ID) as LastID from Reports", cnn)
        cmdn.CommandType = CommandType.Text
        Dim drn As SqlClient.SqlDataReader = cmdn.ExecuteReader
        If drn.HasRows Then
            While drn.Read
                If drn("LastID") IsNot DBNull.Value _
                Then NID = drn("LastID") + 1
            End While
        End If
        cnn.Close()
        cnn = Nothing
        Return NID
    End Function

    Private Function IsRptIDUnique(ByVal RptID As Integer) As Boolean
        Dim UniqID As Boolean = True
        Dim cnu As New SqlClient.SqlConnection(connString)
        cnu.Open()
        Dim cmdu As New SqlClient.SqlCommand(
        "Select * from Reports where ID = " & RptID, cnu)
        cmdu.CommandType = CommandType.Text
        Dim dru As SqlClient.SqlDataReader = cmdu.ExecuteReader
        If dru.HasRows Then UniqID = False
        cnu.Close()
        cnu = Nothing
        Return UniqID
    End Function

    Private Sub DisplayReport(ByVal RptID As Integer)
        Dim ItemX As MyList
        Dim cnru As New SqlClient.SqlConnection(connString)
        cnru.Open()
        Dim cmdru As New SqlClient.SqlCommand("Select a.*, b.User_ID from " & _
        "Reports a inner join Report_User b on b.Report_ID = a.ID where " & _
        "a.ID = " & RptID, cnru)
        cmdru.CommandType = CommandType.Text
        Dim drru As SqlClient.SqlDataReader = cmdru.ExecuteReader
        If drru.HasRows Then
            While drru.Read
                txtID.Text = drru("ID")
                txtName.Text = drru("Name")
                txtFile.Text = drru("File_Path")
                If drru("Description") IsNot DBNull.Value _
                Then txtDesc.Text = drru("Description")
                '
                For i As Integer = 0 To lstUsers.Items.Count - 1
                    ItemX = lstUsers.Items(i)
                    If ItemX.ItemData = drru("User_ID") Then _
                    lstUsers.SetItemChecked(i, True)
                Next
            End While
        End If
        cnru.Close()
        cnru = Nothing
        UpdateProgress()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtID.Text <> "" And txtName.Text <> "" And txtFile.Text <> "" Then
            SaveReport(Val(txtID.Text))
            ClearForm()
            If chkEditNew.Checked = True Then txtID.Text = NextRptID()
            btnDelete.Enabled = False
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub SaveReport(ByVal RptID As Integer)
        Try
            'Dim Rs As New ADODB.Recordset
            Dim ItemX As MyList = Nothing
            Dim RPTFile As String
            Dim DestPath As String = Application.StartupPath & "\MyReports\"
            RPTFile = Microsoft.VisualBasic.Mid(txtFile.Text, InStrRev(txtFile.Text, "\") + 1)
            If Not System.IO.Directory.Exists(DestPath) Then System.IO.Directory.CreateDirectory(DestPath)
            System.IO.File.Copy(txtFile.Text, DestPath & RPTFile, True)
            ExecuteSqlProcedure("If Exists (Select * from Reports where ID = " & RptID & _
            ") Update Reports Set Name = '" & Trim(txtName.Text) & "', File_Path = '" & _
            DestPath & RPTFile & "', Description = '" & Trim(txtDesc.Text) & "' where " & _
            "ID = " & RptID & " Else Insert into Reports (ID, Name, File_Path, " & _
            "Description) values (" & RptID & ", '" & Trim(txtName.Text) & "', '" & _
            DestPath & RPTFile & "', '" & Trim(txtDesc.Text) & "')")
            For i As Integer = 0 To lstUsers.Items.Count - 1
                If lstUsers.GetItemChecked(i) = True Then
                    ItemX = lstUsers.Items(i)
                    ExecuteSqlProcedure("If not Exists (Select * from Report_User where Report_ID = " & _
                    RptID & " and User_ID = " & ItemX.ItemData & ") Insert into Report_User (" & _
                    "User_ID, Report_ID) values (" & ItemX.ItemData & ", " & RptID & ")")
                Else
                    ExecuteSqlProcedure("Delete from Report_User where " & _
                    "Report_ID = " & RptID & " and User_ID = " & ItemX.ItemData)
                End If
            Next
        Catch Ex As Exception
            MsgBox(Ex.Message.ToString)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If txtID.Text <> "" And txtName.Text <> "" And txtFile.Text <> "" Then
            Dim RetVal As Integer
            RetVal = MsgBox("Are you sure about unregistering this report?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
            If RetVal = vbYes Then
                ExecuteSqlProcedure("Delete from Report_User where Report_ID = " & Val(txtID.Text))
                ExecuteSqlProcedure("Delete from Reports where ID = " & Val(txtID.Text))
                ClearForm()
                btnDelete.Enabled = False
                btnSave.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFile.Click
        Dim FileOpn As New OpenFileDialog
        With FileOpn
            .Filter = "Report Files(*.RPT)|*.RPT"
            .InitialDirectory = Application.StartupPath & "\MyReports\"
        End With
        If MsgBoxResult.Ok = FileOpn.ShowDialog Then
            txtFile.Text = FileOpn.FileName
            UpdateProgress()
        End If
        FileOpn.Dispose()
        FileOpn = Nothing
    End Sub

    Private Sub txtName_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.Validated
        UpdateProgress()
    End Sub

    Private Sub txtDesc_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDesc.Validated
        UpdateProgress()
    End Sub

    Private Sub btnRptLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRptLook.Click
        Dim ReportID As String = frmReportLookUp.showdialog()
        DisplayReport(Val(ReportID))
    End Sub
End Class
