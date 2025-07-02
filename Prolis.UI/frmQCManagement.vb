Imports System.Windows.Forms
Imports System.Data

Public Class frmQCManagement

    Private Sub frmQCManagement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtRunID.Text = GetNextQCRunID()
        lbldate.Text += " (" & SystemConfig.DateFormat & ")"
        dtpDate.Format = DateTimePickerFormat.Custom
        dtpDate.CustomFormat = SystemConfig.DateFormat
        dtpDate.Value = Date.Now
        txtTime.Text = Format(dtpDate.Value, "HH:mm")
        cmbShift.SelectedIndex = 0
        LoadEquipments()
        If cmbEquipment.SelectedIndex <> -1 Then
            Dim ItemX As MyList = cmbEquipment.SelectedItem
            LoadAnalyses(ItemX.ItemData)
        End If
        txtRunID.ReadOnly = True
        cmbAnalysis.Enabled = False
    End Sub

    Private Sub LoadEquipments()
        cmbEquipment.Items.Clear()
        Dim cneq As New SqlClient.SqlConnection(connString)
        cneq.Open()
        Dim cmdeq As New SqlClient.SqlCommand("Select * from " &
        "Equipments where Active <> 0 and ID in (Select " &
        "distinct Equipment_ID from Anas) order by Name", cneq)
        cmdeq.CommandType = CommandType.Text
        Dim dreq As SqlClient.SqlDataReader = cmdeq.ExecuteReader
        If dreq.HasRows Then
            While dreq.Read
                cmbEquipment.Items.Add(New MyList(dreq("Name"), dreq("ID")))
            End While
        End If
        If cmbEquipment.Items.Count > 0 _
        Then cmbEquipment.SelectedIndex = 0
        cneq.Close()
        cneq = Nothing
    End Sub

    Private Sub QCProgress()
        If chkAllInd.Checked = False Then   'all
            If cmbEquipment.SelectedIndex <> -1 And cmbAnalysis.Items.Count > 0 Then
                btnSave.Enabled = True
            Else
                btnSave.Enabled = False
                btnDelete.Checked = False
            End If
        Else
            If txtRunID.Text <> "" And IsDate(txtTime.Text) And
            cmbAnalysis.SelectedIndex <> -1 And txtRunName.Text <> "" Then
                btnSave.Enabled = True
                If btnNew.Checked = True Then btnDelete.Checked = True
            Else
                btnSave.Enabled = False
                btnDelete.Checked = False
            End If
        End If
    End Sub

    Private Function GetNextQCRunID() As Long
        Dim NextID As Long = 1
        Dim cnqid As New SqlClient.SqlConnection(connString)
        cnqid.Open()
        Dim cmdqid As New SqlClient.SqlCommand("Select Max(ID) as LastID from Runs", cnqid)
        cmdqid.CommandType = CommandType.Text
        Dim drqid As SqlClient.SqlDataReader = cmdqid.ExecuteReader
        If drqid.HasRows Then
            While drqid.Read
                If drqid("LastID") IsNot DBNull.Value _
                Then NextID = drqid("LastID") + 1
            End While
        End If
        cnqid.Close()
        cnqid = Nothing
        Return NextID
    End Function

    Private Sub LoadAnalyses(ByVal EquipID As Integer)
        cmbAnalysis.Items.Clear()
        Dim cnla As New SqlClient.SqlConnection(connString)
        cnla.Open()
        Dim cmdla As New SqlClient.SqlCommand("Select * from Anas " &
        "where Equipment_ID = " & EquipID & " order by Name", cnla)
        cmdla.CommandType = CommandType.Text
        Dim drla As SqlClient.SqlDataReader = cmdla.ExecuteReader
        If drla.HasRows Then
            While drla.Read
                cmbAnalysis.Items.Add(New MyList(drla("Name"), drla("ID")))
            End While
        End If
        cnla.Close()
        cnla = Nothing
    End Sub

    Private Sub txtRunID_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRunID.GotFocus
        txtRunID.BackColor = FCOLOR
    End Sub

    Private Sub txtRunID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRunID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtRunID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRunID.LostFocus
        txtRunID.BackColor = NFCOLOR
        QCProgress()
    End Sub

    Private Sub txtTime_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTime.GotFocus
        txtTime.BackColor = FCOLOR
    End Sub

    Private Sub txtTime_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTime.LostFocus
        txtTime.BackColor = NFCOLOR
    End Sub

    Private Sub cmbShift_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbShift.GotFocus
        cmbShift.BackColor = FCOLOR
    End Sub

    Private Sub cmbShift_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbShift.LostFocus
        cmbShift.BackColor = NFCOLOR
    End Sub

    Private Sub cmbAnalysis_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAnalysis.GotFocus
        cmbAnalysis.BackColor = FCOLOR
    End Sub

    Private Sub cmbAnalysis_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAnalysis.LostFocus
        cmbAnalysis.BackColor = NFCOLOR
    End Sub

    Private Sub txtRunName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRunName.GotFocus
        txtRunName.BackColor = FCOLOR
    End Sub

    Private Sub txtRunName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRunName.LostFocus
        txtRunName.BackColor = NFCOLOR
    End Sub

    Private Sub cmbAnalysis_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAnalysis.SelectedIndexChanged
        If cmbAnalysis.SelectedIndex <> -1 Then
            txtRunName.Text = cmbAnalysis.SelectedItem.ToString & "-" & Format(dtpDate.Value,
            "yyMMdd") & "-" & cmbShift.SelectedIndex.ToString
            Dim ItemX As MyList = cmbAnalysis.SelectedItem
            txtControls.Text = GetControlCount(ItemX.ItemData)
            txtValidated.Text = "No"
            txtCreatedBy.Text = ThisUser.UserName
            txtEditedBy.Text = ThisUser.UserName
            txtEditedOn.Text = Format(Date.Now, SystemConfig.DateFormat)
        Else
            txtRunName.Text = ""
            txtControls.Text = ""
            txtValidated.Text = ""
            txtCreatedBy.Text = ""
            txtEditedBy.Text = ""
            txtEditedOn.Text = ""
        End If
        QCProgress()
    End Sub

    Private Function GetControlCount(ByVal AnaID As Integer) As Integer
        Dim CC As Integer = 0
        Dim cncc As New SqlClient.SqlConnection(connString)
        cncc.Open()
        Dim cmdcc As New SqlClient.SqlCommand("Select distinct " &
        "Control_ID from Ana_Control where Ana_ID = " & AnaID, cncc)
        cmdcc.CommandType = CommandType.Text
        Dim drcc As SqlClient.SqlDataReader = cmdcc.ExecuteReader
        If drcc.HasRows Then
            While drcc.Read
                CC += 1
            End While
        End If
        cncc.Close()
        cncc = Nothing
        Return CC
    End Function

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        If btnNew.Checked = False Then  'new
            btnNew.Text = "New"
            btnNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\New.ico")
            ClearForm()
            btnQCRunLook.Enabled = False
            txtRunID.Text = GetNextQCRunID()
            chkAllInd.Enabled = True
        Else
            btnNew.Text = "Edit"
            btnNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Edit.ico")
            ClearForm()
            btnQCRunLook.Enabled = True
            chkAllInd.Checked = True
            chkAllInd.Enabled = False
        End If
    End Sub

    Private Sub ClearForm()
        btnSave.Enabled = False
        btnDelete.Enabled = False
        txtRunID.Text = ""
        dtpDate.Value = Date.Now
        txtTime.Text = Format(dtpDate.Value, "HH:mm")
        cmbShift.SelectedIndex = 0
        cmbAnalysis.SelectedIndex = -1
        txtRunName.Text = ""
        txtControls.Text = ""
        txtValidators.Text = ""
        txtValidated.Text = ""
        txtCreatedBy.Text = ""
        txtEditedBy.Text = ""
        txtEditedOn.Text = ""
    End Sub

    Private Sub btnQCRunLook_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQCRunLook.Click
        Dim RunID As String = frmQCRunsLookUp.Showdialog()
        If RunID <> "" Then DisplayRun(Val(RunID))
    End Sub

    Private Sub txtRunID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRunID.Validated
        If txtRunID.Text <> "" Then
            Dim cnrv As New SqlClient.SqlConnection(connString)
            cnrv.Open()
            Dim cmdrv As New SqlClient.SqlCommand("Select * " &
            "from Runs where ID = " & Val(txtRunID.Text), cnrv)
            cmdrv.CommandType = CommandType.Text
            Dim drrv As SqlClient.SqlDataReader = cmdrv.ExecuteReader
            If drrv.HasRows Then
                DisplayRun(Val(txtRunID.Text))
            Else
                If btnNew.Checked = True Then  'old
                    MsgBox("Invalid ID", MsgBoxStyle.Critical, "Prolis")
                    txtRunID.Focus()
                End If
            End If
            cnrv.Close()
            cnrv = Nothing
        End If
    End Sub

    Private Sub DisplayRun(ByVal RunID As Long)
        Dim ItemX As MyList
        Dim cndr As New SqlClient.SqlConnection(connString)
        cndr.Open()
        Dim cmddr As New SqlClient.SqlCommand("Select * from Runs where ID = " & RunID, cndr)
        cmddr.CommandType = CommandType.Text
        Dim drdr As SqlClient.SqlDataReader = cmddr.ExecuteReader
        If drdr.HasRows Then
            While drdr.Read
                txtRunID.Text = drdr("ID").ToString
                dtpDate.Value = drdr("RunDate")
                txtTime.Text = Format(drdr("RunDate"), "HH:mm")
                For i As Integer = 0 To cmbShift.Items.Count - 1
                    If i = drdr("Shift") Then
                        cmbShift.SelectedIndex = i
                        Exit For
                    End If
                Next
                For i As Integer = 0 To cmbAnalysis.Items.Count - 1
                    ItemX = cmbAnalysis.Items(i)
                    If ItemX.ItemData = drdr("Analysis_ID") Then
                        cmbAnalysis.SelectedIndex = i
                        Exit For
                    End If
                Next
                txtRunName.Text = drdr("Name")
                txtControls.Text = drdr("Controls")
                txtValidators.Text = drdr("Validaters")
                If drdr("Validated") = 0 Then
                    txtValidated.Text = "No"
                Else
                    txtValidated.Text = "Yes"
                End If
                txtCreatedBy.Text = GetUserName(drdr("Tech_ID"))
                txtEditedBy.Text = GetUserName(drdr("EditedBy"))
                txtEditedOn.Text = Format(drdr("LastEditedOn"), SystemConfig.DateFormat)
                '
                btnDelete.Enabled = True
            End While
        End If
        cndr.Close()
        cndr = Nothing
    End Sub

    Private Function SaveRun(ByVal RunID As Long, ByVal RunName As String, ByVal AnaID As Integer,
    ByVal Shift As Integer, ByVal Controls As Integer, ByVal Valers As Integer) As Long
        RunID = UpdateRunID(RunID, RunName)
        ExecuteSqlProcedure("If Exists (Select * from Runs where ID = " & RunID &
        ") Update Runs set RunDate = '" & Format(dtpDate.Value, SystemConfig.DateFormat) &
        " " & txtTime.Text & "', Shift = " & Shift & ", Analysis_ID = " & AnaID &
        ", Name = '" & RunName & "', Controls = " & Controls & ", Validaters = " & Valers &
        ", EditedBy = " & ThisUser.ID & ", LastEditedOn = '" & Date.Now & "' where ID = " &
        RunID & " Else Insert into Runs (ID, RunDate, Shift, Analysis_ID, Name, Controls, " &
        "Validaters, Validated, Tech_ID, EditedBy,LastEditedOn) values (" & RunID & ", '" &
        Format(dtpDate.Value, SystemConfig.DateFormat) & " " & txtTime.Text & "', " & Shift &
        ", " & AnaID & ", '" & RunName & "', " & Controls & ", " & Valers & ", 0, " & ThisUser.ID &
        ", " & ThisUser.ID & ", '" & Date.Now & "')")
        Return RunID
    End Function

    Private Function UpdateRunID(ByVal RunID As Long, ByVal RunName As String) As Long
        Dim cnrv As New SqlClient.SqlConnection(connString)
        cnrv.Open()
        Dim cmdrv As New SqlClient.SqlCommand("Select * " &
        "from Runs where Name = '" & RunName & "'", cnrv)
        cmdrv.CommandType = CommandType.Text
        Dim drrv As SqlClient.SqlDataReader = cmdrv.ExecuteReader
        If drrv.HasRows Then
            While drrv.Read
                RunID = drrv("ID")
            End While
        End If
        cnrv.Close()
        cnrv = Nothing
        Return RunID
    End Function

    Private Sub SaveRunControl(ByVal RunID As Long)
        If Val(txtControls.Text) > 0 Then
            Dim i As Integer
            Dim LotInfo() As String
            'ExecuteSqlProcedure("Delete from Run_Control where Run_ID = " & RunID)
            For i = 1 To Val(txtControls.Text)
                LotInfo = GetLotInfo(RunID, i)
                ExecuteSqlProcedure("If Not Exists (Select * from Run_Control where Run_ID = " &
                RunID & " and Control_ID = " & i & ") Insert into Run_Control (Run_ID, Control_ID, " &
                "ControlName, Lot, ExpireDate, Ordinal) values (" & RunID & ", " & i & ", '" &
                LotInfo(0) & "', '" & LotInfo(1) & "', " & IIf(IsDate(LotInfo(2)), "'" &
                CDate(LotInfo(2)) & "'", "Null") & ", " & i & ")")
            Next
        End If
    End Sub

    Private Function GetLotInfo(ByVal RunID As Long, ByVal ControlID As Long) As String()
        Dim LotInfo() As String = {"", "", ""}
        Dim cnli As New SqlClient.SqlConnection(connString)
        cnli.Open()
        Dim cmdli As New SqlClient.SqlCommand("Select * from Ana_Control where Control_ID = " &
        ControlID & " and Ana_ID in (Select Analysis_ID from Runs where ID = " & RunID & ")", cnli)
        cmdli.CommandType = CommandType.Text
        Dim drli As SqlClient.SqlDataReader = cmdli.ExecuteReader
        If drli.HasRows Then
            While drli.Read
                LotInfo(0) = Trim(drli("ControlName"))
                LotInfo(1) = Trim(drli("Lot"))
                LotInfo(2) = Format(drli("ExpireDate"), SystemConfig.DateFormat)
            End While
        End If
        cnli.Close()
        cnli = Nothing
        Return LotInfo
    End Function

    Private Sub SaveRunControlTest(ByVal RunID As Long)
        Dim i As Integer = 0
        Dim sSQL As String = ""
        Dim Decs As String = "0"
        'ExecuteSqlProcedure("Delete from QC_Results where Run_ID = " & RunID)
        Dim cnqr As New SqlClient.SqlConnection(connString)
        cnqr.Open()
        Dim cmdqr As New SqlClient.SqlCommand("Select a.*, b.DecimalPlaces from " & _
        "Ana_Ranges a inner join Tests b on b.ID = a.Test_ID where a.Ana_ID " & _
        "in (Select Analysis_ID from Runs where ID = " & RunID & ")", cnqr)
        cmdqr.CommandType = CommandType.Text
        Dim drqr As SqlClient.SqlDataReader = cmdqr.ExecuteReader
        If drqr.HasRows Then
            While drqr.Read
                If drqr("DecimalPlaces") = 1 Then
                    Decs = "0.0"
                ElseIf drqr("DecimalPlaces") = 2 Then
                    Decs = "0.00"
                ElseIf drqr("DecimalPlaces") = 3 Then
                    Decs = "0.000"
                ElseIf drqr("DecimalPlaces") = 4 Then
                    Decs = "0.0000"
                End If
                ExecuteSqlProcedure("If Exists (Select * from QC_Results where Run_ID = " & RunID & " and " & _
                "Control_ID = " & drqr("Control_ID") & " and Test_ID = " & drqr("Test_ID") & ") Update " & _
                "QC_Results set QCRange = '" & Format(drqr("Low"), Decs) & "-" & Format(drqr("High"), Decs) & _
                "' where Run_ID = " & RunID & " and Control_ID = " & drqr("Control_ID") & " and Test_ID = " & _
                drqr("Test_ID") & " Else Insert into QC_Results (Run_ID, Control_ID, Test_ID, Ordinal, " & _
                "Result, Flag, QCRange, T_Result, I_Result, Comment, UOM, Released, Released_By, Release_Time) " & _
                "values (" & RunID & ", " & drqr("Control_ID") & ", " & drqr("Test_ID") & ", " & i & ", '', '', '" & _
                Format(drqr("Low"), Decs) & "-" & Format(drqr("High"), Decs) & "', '', Null, '', '', 0, null, null)")
                '
                i += 1
            End While
        End If
        cnqr.Close()
        cnqr = Nothing
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim ItemX As MyList
        Dim RunID As Long
        Dim RunName As String = ""
        Dim Controls As Int16 = 0
        If chkAllInd.Checked = False Then   'All
            For i As Integer = 0 To cmbAnalysis.Items.Count - 1
                ItemX = cmbAnalysis.Items(i)
                RunID = GetNextQCRunID()
                Controls = GetControlCount(ItemX.ItemData)
                RunName = ItemX.Name & "-" & Format(dtpDate.Value, _
                "yyMMdd") & "-" & cmbShift.SelectedIndex.ToString
                RunID = SaveRun(RunID, RunName, ItemX.ItemData, _
                cmbShift.SelectedIndex, Val(txtControls.Text), Val(txtValidators.Text))
                SaveRunControl(RunID)
                SaveRunControlTest(RunID)
            Next
            ClearForm()
            If btnNew.Checked = False Then  'new
                txtRunID.Text = GetNextQCRunID()
            End If
        Else
            If txtRunID.Text <> "" And IsDate(txtTime.Text) And _
            cmbAnalysis.SelectedIndex <> -1 And txtRunName.Text <> "" Then
                ItemX = cmbAnalysis.SelectedItem
                Controls = GetControlCount(ItemX.ItemData)
                RunID = SaveRun(txtRunID.Text, txtRunName.Text, ItemX.ItemData, _
                cmbShift.SelectedIndex, Controls, Val(txtValidators.Text))
                SaveRunControl(RunID)
                SaveRunControlTest(RunID)
                ClearForm()
                If btnNew.Checked = False Then  'new
                    txtRunID.Text = GetNextQCRunID()
                End If
            Else
                MsgBox("The input in the Red labeled fields, is required", MsgBoxStyle.Critical, "Prolis")
                If txtRunID.Text = "" Then
                    txtRunID.Focus()
                ElseIf Not IsDate(txtTime.Text) Then
                    txtTime.Focus()
                ElseIf cmbAnalysis.SelectedIndex = -1 Then
                    cmbAnalysis.Focus()
                Else
                    txtRunName.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If txtRunID.Text <> "" And txtRunName.Text <> "" Then
            Dim RetVal As Integer = MsgBox("Deleting the QC record, will delete all of the results " & _
            "associated with this record. Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
            If RetVal = vbYes Then
                ExecuteSqlProcedure("Delete from QC_Results where Run_ID = " & Trim(txtRunID.Text))
                ExecuteSqlProcedure("Delete from Run_Control where Run_ID = " & Trim(txtRunID.Text))
                ExecuteSqlProcedure("Delete from Runs where ID = " & Trim(txtRunID.Text))
                ClearForm()
            End If
        End If
    End Sub

    Private Sub chkAllInd_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAllInd.CheckedChanged
        cmbAnalysis.SelectedIndex = -1
        If chkAllInd.Checked = False Then   'All
            chkAllInd.Text = "All"
            txtRunID.ReadOnly = True
            cmbAnalysis.Enabled = False
            txtRunName.ReadOnly = True
        Else
            chkAllInd.Text = "Individual"
            txtRunID.ReadOnly = False
            txtRunName.ReadOnly = False
            cmbAnalysis.Enabled = True
        End If
        QCProgress()
    End Sub

    Private Sub cmbEquipment_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbEquipment.SelectedIndexChanged
        Dim ItemX As MyList = cmbEquipment.SelectedItem
        LoadAnalyses(ItemX.ItemData)
        QCProgress()
    End Sub
End Class
