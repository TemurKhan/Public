Imports System.Windows.Forms

Imports Microsoft.Data.SqlClient

Public Class frmRunSetup
    Private SearchMode As Boolean = True

    Private Sub btnRemTst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemTst.Click
        If dgvEquips.SelectedRows(0).Index <> -1 Then
            dgvEquips.Rows.RemoveAt(dgvEquips.SelectedRows(0).Index)
            btnRemTst.Enabled = False
            If dgvEquips.RowCount = 0 Then btnRemTstAll.Enabled = False
        End If
        SyncRanges()
        Update_Progress()
    End Sub

    Private Sub frmRunSetup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CN.Open("DSN=ProlisQC;UID=sa;PWD=''")
        Populate_Depts()
        Populate_Equips()
        cmbDepts.SelectedIndex = 0
        cmbEquipments.SelectedIndex = 0
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub Populate_Depts()
        cmbDepts.Items.Clear()
        Dim sSQL As String = "Select * from Departments"
        If connString <> "" Then
            Dim cnd As New SqlConnection(connString)
            cnd.Open()
            Dim cmdd As New SqlCommand(sSQL, cnd)
            cmdd.CommandType = Data.CommandType.Text
            Dim drd As SqlDataReader = cmdd.ExecuteReader
            If drd.HasRows Then
                While drd.Read
                    cmbDepts.Items.Add(New MyList(drd("Dept_Name"), drd("ID")))
                End While
            End If
            cnd.Close()
            cnd = Nothing
            'Else
            '    Dim cnd As New Data.Odbc.OdbcConnection(connstring)
            '    cnd.Open()
            '    Dim cmdd As New Data.Odbc.OdbcCommand(sSQL, cnd)
            '    cmdd.CommandType = Data.CommandType.Text
            '    Dim drd As Data.Odbc.OdbcDataReader = cmdd.ExecuteReader
            '    If drd.HasRows Then
            '        While drd.Read
            '            cmbDepts.Items.Add(New MyList(drd("Dept_Name"), drd("ID")))
            '        End While
            '    End If
            '    cnd.Close()
            '    cnd = Nothing
        End If
    End Sub

    Private Sub Populate_Equips()
        cmbEquipments.Items.Clear()
        Dim sSQL As String = "Select * from Equipments"
        If connString <> "" Then
            Dim cne As New SqlConnection(connString)
            cne.Open()
            Dim cmde As New SqlCommand(sSQL, cne)
            cmde.CommandType = Data.CommandType.Text
            Dim dre As SqlDataReader = cmde.ExecuteReader
            If dre.HasRows Then
                While dre.Read
                    cmbEquipments.Items.Add(New MyList(dre("Name"), dre("ID")))
                End While
            End If
            cne.Close()
            cne = Nothing
            'Else
            '    Dim cne As New Data.Odbc.OdbcConnection(connstring)
            '    cne.Open()
            '    Dim cmde As New Data.Odbc.OdbcCommand(sSQL, cne)
            '    cmde.CommandType = Data.CommandType.Text
            '    Dim dre As Data.Odbc.OdbcDataReader = cmde.ExecuteReader
            '    If dre.HasRows Then
            '        While dre.Read
            '            cmbEquipments.Items.Add(New MyList(dre("Dept_Name"), dre("ID")))
            '        End While
            '    End If
            '    cne.Close()
            '    cne = Nothing
        End If
    End Sub

    'Private Sub cmbEquipments_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbEquipments.SelectedIndexChanged
    '    If cmbEquipments.SelectedIndex > 0 Then
    '        dgvEquips.Columns(1).ReadOnly = False
    '    Else
    '        dgvEquips.Columns(1).ReadOnly = True
    '    End If
    'End Sub

    Private Sub chkEditNew_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkEditNew.CheckedChanged
        ClearForm()
        If chkEditNew.Checked = False Then
            chkEditNew.Text = "Edit"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Edit.ico")
            SearchMode = True
            btnAnaLookup.Enabled = True
        Else
            chkEditNew.Text = "New"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\New.ico")
            SearchMode = False
            btnAnaLookup.Enabled = False
            txtAnaID.Text = GetNextAnaID()
        End If
    End Sub

    Private Function GetNextAnaID() As Integer
        Dim AnaID As Integer = 1
        Dim sSQL As String = "Select max(ID) as LastID from Anas"
        Dim cnl As New SqlConnection(connString)
        cnl.Open()
        Dim cmdl As New SqlCommand(sSQL, cnl)
        cmdl.CommandType = Data.CommandType.Text
        Dim drl As SqlDataReader = cmdl.ExecuteReader
        If drl.HasRows Then
            While drl.Read
                If drl("LastID") Is DBNull.Value Then
                    AnaID = 1
                Else
                    AnaID = drl("LastID") + 1
                End If
            End While
        End If
        cnl.Close()
        cnl = Nothing
        Return AnaID
    End Function

    Private Sub ClearForm()
        txtAnaID.Text = ""
        txtanaName.Text = ""
        cmbDepts.SelectedIndex = 0
        cmbEquipments.SelectedIndex = 0
        txtControls.Text = "3"
        txtValidaters.Text = "3"
        cmbInRange.SelectedIndex = -1
        cmbInRange.Items.Clear()
        dgvControls.RowCount = 3
        dgvControls.Rows(0).Cells(0).Value = 1
        dgvControls.Rows(0).Cells(1).Value = "Control - 1"
        dgvControls.Rows(0).Cells(2).Value = ""
        dgvControls.Rows(0).Cells(3).Value = ""
        dgvControls.Rows(0).Cells(4).Value = ""
        dgvControls.Rows(1).Cells(0).Value = 2
        dgvControls.Rows(1).Cells(1).Value = "Control - 2"
        dgvControls.Rows(1).Cells(2).Value = ""
        dgvControls.Rows(1).Cells(3).Value = ""
        dgvControls.Rows(1).Cells(4).Value = ""
        dgvControls.Rows(2).Cells(0).Value = 3
        dgvControls.Rows(2).Cells(1).Value = "Control - 3"
        dgvControls.Rows(2).Cells(2).Value = ""
        dgvControls.Rows(2).Cells(3).Value = ""
        dgvControls.Rows(2).Cells(4).Value = ""
        dgvEquips.Rows.Clear()
        dgvRanges.Rows.Clear()
        btnDelete.Enabled = False
        btnSave.Enabled = False
        Update_Progress()
    End Sub

    Private Sub txtTestID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTestID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtTestID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTestID.Validated
        If txtTestID.Text <> "" Then
            Dim sSQL As String = "Select Name from Tests where ID = " & Val(txtTestID.Text) &
            " Union Select Name from Groups where ID = " & Val(txtTestID.Text) & " Union " &
            "Select Name from Profiles where ID = " & Val(txtTestID.Text)
            If connString <> "" Then
                Dim cntgp As New SqlConnection(connString)
                cntgp.Open()
                Dim cmdtgp As New SqlCommand(sSQL, cntgp)
                cmdtgp.CommandType = Data.CommandType.Text
                Dim drtgp As SqlDataReader = cmdtgp.ExecuteReader
                If drtgp.HasRows Then
                    While drtgp.Read
                        txtTestName.Text = drtgp("Name")
                        txtOrdID.Text = txtTestID.Text
                    End While
                Else
                    MsgBox("Invalid ID, use Look up instead")
                    txtTestID.Text = ""
                    txtTestID.Focus()
                End If
                cntgp.Close()
                cntgp = Nothing
                'Else
                '    Dim cntgp As New Data.Odbc.OdbcConnection(connstring)
                '    cntgp.Open()
                '    Dim cmdtgp As New Data.Odbc.OdbcCommand(sSQL, cntgp)
                '    cmdtgp.CommandType = Data.CommandType.Text
                '    Dim drtgp As Data.Odbc.OdbcDataReader = cmdtgp.ExecuteReader
                '    If drtgp.HasRows Then
                '        While drtgp.Read
                '            txtTestName.Text = drtgp("Name")
                '            txtOrdID.Text = txtTestID.Text
                '        End While
                '    Else
                '        MsgBox("Invalid ID, use Look up instead")
                '        txtTestID.Text = ""
                '        txtTestID.Focus()
                '    End If
                '    cntgp.Close()
                '    cntgp = Nothing
            End If
        Else
            txtTestName.Text = ""
        End If
        Update_ListEntry()
    End Sub

    Private Sub Update_ListEntry()
        If txtTestID.Text <> "" And txtTestName.Text <> "" And txtOrdID.Text <> "" Then
            btnAddTst.Enabled = True
        Else
            btnAddTst.Enabled = False
        End If
    End Sub

    Private Sub Update_Progress()
        If txtAnaID.Text <> "" And txtanaName.Text <> "" And txtControls.Text <> "" _
        And dgvEquips.RowCount > 0 Then
            btnSave.Enabled = True
        Else
            btnSave.Enabled = False
        End If
    End Sub

    Private Function ValidateControls() As Boolean
        Dim Controls As Byte = Val(txtControls.Text)
        Dim i As Integer
        Dim Valid As Boolean = True
        For i = 0 To dgvControls.RowCount - 1
            If dgvControls.Rows(i).Cells(0).Value.ToString = "" Or
            dgvControls.Rows(i).Cells(1).Value.ToString = "" Or
            dgvControls.Rows(i).Cells(2).Value Is Nothing Or
            Not IsDate(dgvControls.Rows(i).Cells(3).Value) Then
                Valid = False
                Exit For
            End If
        Next
        ValidateControls = Valid
    End Function

    Private Sub btnAddTst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddTst.Click
        If txtTestID.Text <> "" And txtTestName.Text <> "" _
        And (txtOrdID.Text <> "" Or txtResID.Text <> "") Then
            If Not TestInTheList(txtTestID.Text) Then _
            dgvEquips.Rows.Add(Trim(txtTestID.Text), Trim(txtOrdID.Text), Trim(txtResID.Text), Trim(txtTestName.Text))
            txtTestID.Text = ""
            txtOrdID.Text = ""
            txtResID.Text = ""
            txtTestName.Text = ""
            btnAddTst.Enabled = False
            btnRemTstAll.Enabled = True
            txtTestID.Focus()
        End If
        UpdateInRangePercent(dgvEquips.RowCount)
        SyncRanges()
        Update_Progress()
    End Sub

    Private Sub UpdateInRangePercent(ByVal TestCount As Integer)
        If TestCount > 0 Then
            cmbInRange.Items.Clear()
            Dim i As Integer
            If TestCount = 1 Then
                cmbInRange.Items.Add("100")
                cmbInRange.SelectedIndex = 0
            Else
                For i = 0 To TestCount
                    cmbInRange.Items.Add(CInt(i / TestCount * 100))
                Next
                cmbInRange.SelectedIndex = cmbInRange.Items.Count - 1
            End If
            cmbInRange.Enabled = True
        End If
    End Sub

    Private Function TestInTheList(ByVal Test_ID As String) As Boolean
        Dim i As Integer
        Dim InList As Boolean = False
        For i = 0 To dgvEquips.RowCount - 1
            If dgvEquips.Rows(i).Cells(0).Value = Test_ID Then
                InList = True
                Exit For
            End If
        Next
        TestInTheList = InList
    End Function

    Private Sub btnTestLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestLook.Click
        Dim TestID As String = frmTestLookup.ShowDialog()
        If TestID <> "" Then
            txtTestID.Text = TestID
            txtTestName.Text = GetTGPName(Val(TestID))
            txtOrdID.Text = TestID
            txtOrdID.Focus()
            btnAddTst.Enabled = True
        End If
    End Sub

    Private Sub txtAnaID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAnaID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAnaID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAnaID.Validated
        If txtAnaID.Text <> "" Then
            If chkEditNew.Checked = False Then  'Edit mode
                If IsAnaIDUnique(Val(txtAnaID.Text)) Then
                    MsgBox("There is no Analysis with the ID you typed. You may use the Look UP")
                    txtAnaID.Text = ""
                    txtAnaID.Focus()
                Else
                    Display_Analysis(Val(txtAnaID.Text))
                    btnDelete.Enabled = True
                End If
            Else
                If Not IsAnaIDUnique(Val(txtAnaID.Text)) Then
                    Dim Retval As Integer
                    Retval = MsgBox("There ID you typed has already been used. Either type an unused ID " _
                    & "or simply accept the system assigned ID by pressing No button. " _
                    & "Do you want to type a unique ID ?", MsgBoxStyle.Critical + MsgBoxStyle.YesNo)
                    If Retval = vbYes Then
                        txtAnaID.Text = ""
                        txtAnaID.Focus()
                    Else
                        txtAnaID.Text = GetNextAnaID()
                    End If
                End If
            End If
        End If
        Update_Progress()
    End Sub

    Private Function IsAnaIDUnique(ByVal Ana_ID As Integer) As Boolean
        Dim Unique As Boolean = True
        Dim sSQL As String = "Select * from Anas where ID = " & Ana_ID
        If connString <> "" Then
            Dim cnu As New SqlConnection(connString)
            cnu.Open()
            Dim cmdu As New SqlCommand(sSQL, cnu)
            cmdu.CommandType = Data.CommandType.Text
            Dim dru As SqlDataReader = cmdu.ExecuteReader
            If dru.HasRows Then Unique = False
            cnu.Close()
            cnu = Nothing
            'Else
            '    Dim cnu As New Data.Odbc.OdbcConnection(connstring)
            '    cnu.Open()
            '    Dim cmdu As New Data.Odbc.OdbcCommand(sSQL, cnu)
            '    cmdu.CommandType = Data.CommandType.Text
            '    Dim dru As Data.Odbc.OdbcDataReader = cmdu.ExecuteReader
            '    If dru.HasRows Then Unique = False
            '    cnu.Close()
            '    cnu = Nothing
        End If
        IsAnaIDUnique = Unique
    End Function

    Private Sub Display_Analysis(ByVal Ana_ID As Integer)
        Dim i As Integer
        Dim OrderID As String = ""
        Dim ResultID As String = ""
        Dim TGPName As String = ""
        Dim POS As String = ""
        Dim Lot(1) As String
        Dim ItemX As MyList
        Dim sSQL As String = "Select * from Anas where ID = " & Ana_ID
        Dim cna As New SqlConnection(connString)
        cna.Open()
        Dim cmda As New SqlCommand(sSQL, cna)
        cmda.CommandType = Data.CommandType.Text
        Dim dra As SqlDataReader = cmda.ExecuteReader
        If dra.HasRows Then
            While dra.Read
                txtAnaID.Text = dra("ID").ToString
                txtanaName.Text = dra("Name")
                For i = 0 To cmbDepts.Items.Count - 1
                    ItemX = cmbDepts.Items(i)
                    If dra("Department_ID") = ItemX.ItemData Then
                        cmbDepts.SelectedIndex = i
                        Exit For
                    End If
                Next
                For i = 0 To cmbEquipments.Items.Count - 1
                    ItemX = cmbEquipments.Items(i)
                    If dra("Equipment_ID") = ItemX.ItemData Then
                        cmbEquipments.SelectedIndex = i
                        Exit For
                    End If
                Next
                txtControls.Text = dra("Controls")
                txtValidaters.Text = dra("Validaters")
                For i = 0 To cmbInRange.Items.Count - 1
                    If Val(cmbInRange.Items(i).ToString) = dra("InrangePercent") Then
                        cmbInRange.SelectedIndex = i
                        Exit For
                    End If
                Next
            End While
        End If
        cna.Close()
        cna = Nothing
        '
        dgvEquips.Rows.Clear()
        sSQL = "Select a.*, (Select Name from Tests where ID = a.Test_ID Union Select Name from Groups where ID = a.Test_ID " &
        "Union Select Name from Profiles where ID = a.Test_ID) as Name from Ana_Test a where a.Ana_ID = " & Ana_ID
        Dim cnt As New SqlConnection(connString)
        cnt.Open()
        Dim cmdt As New SqlCommand(sSQL, cnt)
        cmdt.CommandType = Data.CommandType.Text
        Dim drt As SqlDataReader = cmdt.ExecuteReader
        If drt.HasRows Then
            While drt.Read
                If drt("Name") IsNot DBNull.Value Then
                    TGPName = drt("Name")
                Else
                    TGPName = ""
                End If
                If drt("EqTest_ID") Is DBNull.Value Then
                    OrderID = ""
                Else
                    OrderID = Trim(drt("EqTest_ID"))
                End If
                If drt("EqResult_ID") Is DBNull.Value Then
                    ResultID = ""
                Else
                    ResultID = Trim(drt("EqResult_ID"))
                End If
                dgvEquips.Rows.Add(drt("Test_ID"), OrderID, ResultID, TGPName)
            End While
        End If
        cnt.Close()
        cnt = Nothing
        '
        dgvControls.Rows.Clear()
        sSQL = "Select * from Ana_Control where Ana_ID = " & Ana_ID
        Dim cnc As New SqlConnection(connString)
        cnc.Open()
        Dim cmdc As New SqlCommand(sSQL, cnc)
        cmdc.CommandType = Data.CommandType.Text
        Dim drc As SqlDataReader = cmdc.ExecuteReader
        If drc.HasRows Then
            While drc.Read
                If drc("Lot") IsNot DBNull.Value Then
                    Lot(0) = drc("Lot")
                Else
                    Lot(0) = ""
                End If
                If drc("ExpireDate") IsNot DBNull.Value Then
                    Lot(1) = Format(drc("ExpireDate"), SystemConfig.DateFormat)
                Else
                    Lot(1) = ""
                End If
                If drc("Position") IsNot DBNull.Value Then
                    POS = Trim(drc("Position"))
                Else
                    POS = ""
                End If
                dgvControls.Rows.Add(drc("Control_ID"), drc("ControlName"), Lot(0), Lot(1), POS)
            End While
        End If
        cnc.Close()
        cnc = Nothing
        '
        dgvRanges.Rows.Clear()
        SyncRanges()
        sSQL = "Select * from Ana_Ranges where Ana_ID = " & Ana_ID
        If connString <> "" Then
            Dim cnr As New SqlConnection(connString)
            cnr.Open()
            Dim cmdr As New SqlCommand(sSQL, cnr)
            cmdr.CommandType = Data.CommandType.Text
            Dim drr As SqlDataReader = cmdr.ExecuteReader
            If drr.HasRows Then
                While drr.Read
                    For i = 0 To dgvRanges.RowCount - 1
                        If dgvRanges.Rows(i).Cells(0).Value = drr("Control_ID") And
                        dgvRanges.Rows(i).Cells(2).Value = drr("Test_ID") Then
                            dgvRanges.Rows(i).Cells(4).Value = drr("MeanNormal")
                            dgvRanges.Rows(i).Cells(5).Value = drr("FactorAbnormal")
                            dgvRanges.Rows(i).Cells(6).Value = drr("Low")
                            dgvRanges.Rows(i).Cells(7).Value = drr("High")
                        End If
                    Next
                End While
            End If
            cnr.Close()
            cnr = Nothing
            'Else
            '    Dim cnr As New Data.Odbc.OdbcConnection(connstring)
            '    cnr.Open()
            '    Dim cmdr As New Data.Odbc.OdbcCommand(sSQL, cnr)
            '    cmdr.CommandType = Data.CommandType.Text
            '    Dim drr As Data.Odbc.OdbcDataReader = cmdr.ExecuteReader
            '    If drr.HasRows Then
            '        While drr.Read
            '            For i = 0 To dgvRanges.RowCount - 1
            '                If dgvRanges.Rows(i).Cells(0).Value = drr("Control_ID") And
            '                dgvRanges.Rows(i).Cells(2).Value = drr("Test_ID") Then
            '                    dgvRanges.Rows(i).Cells(4).Value = drr("MeanNormal")
            '                    dgvRanges.Rows(i).Cells(5).Value = drr("FactorAbnormal")
            '                    dgvRanges.Rows(i).Cells(6).Value = drr("Low")
            '                    dgvRanges.Rows(i).Cells(7).Value = drr("High")
            '                End If
            '            Next
            '        End While
            'End If
            'cnr.Close()
            'cnr = Nothing
        End If
    End Sub

    Private Function GetTheLot(ByVal LotID As Long) As String()
        Dim Lot() As String = {"", ""}
        Dim sSQL As String = "Select * from Lots where ID = " & LotID
        If connString <> "" Then
            Dim cnl As New SqlConnection(connString)
            cnl.Open()
            Dim cmdl As New SqlCommand(sSQL, cnl)
            cmdl.CommandType = Data.CommandType.Text
            Dim drl As SqlDataReader = cmdl.ExecuteReader
            If drl.HasRows Then
                While drl.Read
                    Lot(0) = drl("LotName")
                    Lot(1) = Format(drl("ExpireDate"), SystemConfig.DateFormat)
                End While
            End If
            cnl.Close()
            cnl = Nothing
            'Else
            '    Dim cnl As New Data.Odbc.OdbcConnection(connstring)
            '    cnl.Open()
            '    Dim cmdl As New Data.Odbc.OdbcCommand(sSQL, cnl)
            '    cmdl.CommandType = Data.CommandType.Text
            '    Dim drl As Data.Odbc.OdbcDataReader = cmdl.ExecuteReader
            '    If drl.HasRows Then
            '        While drl.Read
            '            Lot(0) = drl("LotName")
            '            Lot(1) = Format(drl("ExpireDate"), SystemConfig.DateFormat)
            '        End While
            '    End If
            '    cnl.Close()
            '    cnl = Nothing
        End If
        Return Lot
    End Function

    Private Sub SelectInRangePercent(ByVal Ana_ID As Integer)
        Dim sSQL As String = "Select InRangePercent from Anas where ID = " & Ana_ID
        If connString <> "" Then
            Dim cnr As New SqlConnection(connString)
            cnr.Open()
            Dim cmdr As New SqlCommand(sSQL, cnr)
            cmdr.CommandType = Data.CommandType.Text
            Dim drr As SqlDataReader = cmdr.ExecuteReader
            If drr.HasRows Then
                While drr.Read
                    For i As Integer = 0 To cmbInRange.Items.Count - 1
                        If cmbInRange.Items(i).ToString = drr("InRangePercent") Then
                            cmbInRange.SelectedIndex = i
                            Exit For
                        End If
                    Next
                End While
            End If
            cnr.Close()
            cnr = Nothing
            'Else
            '    Dim cnr As New Data.Odbc.OdbcConnection(connstring)
            '    cnr.Open()
            '    Dim cmdr As New Data.Odbc.OdbcCommand(sSQL, cnr)
            '    cmdr.CommandType = Data.CommandType.Text
            '    Dim drr As Data.Odbc.OdbcDataReader = cmdr.ExecuteReader
            '    If drr.HasRows Then
            '        While drr.Read
            '            For i As Integer = 0 To cmbInRange.Items.Count - 1
            '                If cmbInRange.Items(i).ToString = drr("InRangePercent") Then
            '                    cmbInRange.SelectedIndex = i
            '                    Exit For
            '                End If
            '            Next
            '        End While
            '    End If
            '    cnr.Close()
            '    cnr = Nothing
        End If
    End Sub

    Private Function GetControlName(ByVal Ana_ID As Integer, ByVal Control_ID As Byte) As String
        Dim CName As String = ""
        Dim sSQL As String = "Select ControlName from Ana_Control " &
        "where Ana_ID = " & Ana_ID & " and Control_ID = " & Control_ID
        If connString <> "" Then
            Dim cnc As New SqlConnection(connString)
            cnc.Open()
            Dim cmdc As New SqlCommand(sSQL, cnc)
            cmdc.CommandType = Data.CommandType.Text
            Dim drc As SqlDataReader = cmdc.ExecuteReader
            If drc.HasRows Then
                While drc.Read
                    CName = drc("ControlName")
                End While
            End If
            cnc.Close()
            cnc = Nothing
            'Else
            '    Dim cnc As New Data.Odbc.OdbcConnection(connstring)
            '    cnc.Open()
            '    Dim cmdc As New Data.Odbc.OdbcCommand(sSQL, cnc)
            '    cmdc.CommandType = Data.CommandType.Text
            '    Dim drc As Data.Odbc.OdbcDataReader = cmdc.ExecuteReader
            '    If drc.HasRows Then
            '        While drc.Read
            '            CName = drc("ControlName")
            '        End While
            '    End If
            '    cnc.Close()
            '    cnc = Nothing
        End If
        Return CName
    End Function

    Private Sub txtanaName_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtanaName.Validated
        If txtanaName.Text <> "" Then
            If SearchMode = False Then
                If Not IsAnaUnique(txtanaName.Text) Then
                    MsgBox("Same named Analysis exist in the system. Either update the Analysis" _
                    & " or create the new Analysis with a different name.")
                    txtanaName.Text = ""
                    txtanaName.Focus()
                End If
            End If
        End If
        Update_Progress()
    End Sub

    Private Function IsAnaUnique(ByVal AnaName As String) As Boolean
        Dim Unique As Boolean = True
        Dim sSQL As String = "Select * from Anas where Name = '" & AnaName & "'"
        If connString <> "" Then
            Dim cnu As New SqlConnection(connString)
            cnu.Open()
            Dim cmdu As New SqlCommand(sSQL, cnu)
            cmdu.CommandType = Data.CommandType.Text
            Dim dru As SqlDataReader = cmdu.ExecuteReader
            If dru.HasRows Then Unique = False
            cnu.Close()
            cnu = Nothing
            'Else
            '    Dim cnu As New Data.Odbc.OdbcConnection(connstring)
            '    cnu.Open()
            '    Dim cmdu As New Data.Odbc.OdbcCommand(sSQL, cnu)
            '    cmdu.CommandType = Data.CommandType.Text
            '    Dim dru As Data.Odbc.OdbcDataReader = cmdu.ExecuteReader
            '    If dru.HasRows Then Unique = False
            '    cnu.Close()
            '    cnu = Nothing
        End If
        IsAnaUnique = Unique
    End Function

    Private Sub dgvEquips_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvEquips.CellClick
        If e.RowIndex <> -1 Then btnRemTst.Enabled = True
    End Sub

    Private Sub btnRemTstAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemTstAll.Click
        dgvEquips.Rows.Clear()
        btnRemTstAll.Enabled = False
        btnRemTst.Enabled = False
        SyncRanges()
        Update_Progress()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtAnaID.Text <> "" And txtanaName.Text <> "" And txtControls.Text <> "" _
        And ValidateControls() = True And dgvEquips.RowCount > 0 And cmbEquipments.SelectedIndex <> -1 Then
            SaveAna()
            ClearForm()
            If chkEditNew.Checked = True Then
                SearchMode = False
                txtAnaID.Text = GetNextAnaID()
            Else
                SearchMode = True
            End If
        Else
            MsgBox("All required elements to save the Analysis, are not filled, especially " _
            & "the Control Labels")
        End If
    End Sub

    Private Sub SaveAna()
        Dim ItemX As MyList
        Dim conn As New SqlConnection(connString)
        conn.Open()
        Dim cmdupsert As New SqlCommand("Anas_SP", conn)
        cmdupsert.CommandType = Data.CommandType.StoredProcedure
        cmdupsert.Parameters.AddWithValue("@act", "Upsert")
        cmdupsert.Parameters.AddWithValue("@ID", Val(txtAnaID.Text))
        cmdupsert.Parameters.AddWithValue("@Name", Trim(txtanaName.Text))
        ItemX = cmbDepts.SelectedItem
        cmdupsert.Parameters.AddWithValue("@Department_ID", ItemX.ItemData)
        ItemX = cmbEquipments.SelectedItem
        cmdupsert.Parameters.AddWithValue("@Equipment_ID", ItemX.ItemData)
        cmdupsert.Parameters.AddWithValue("@Controls", Val(txtControls.Text))
        cmdupsert.Parameters.AddWithValue("@Validaters", Val(txtValidaters.Text))
        If cmbInRange.SelectedIndex = -1 Then
            cmdupsert.Parameters.AddWithValue("@InrangePercent", 100)
        Else
            cmdupsert.Parameters.AddWithValue("@InrangePercent", Val(cmbInRange.SelectedItem.ToString))
        End If
        cmdupsert.Parameters.AddWithValue("@LastEditedOn", Date.Now)
        cmdupsert.Parameters.AddWithValue("@EditedBY", ThisUser.ID)
        Try
            cmdupsert.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
            conn = Nothing
        End Try
        'Following code saves the controls
        If Val(txtControls.Text) > 0 Then
            Dim sqlClean As String = "Delete from Ana_Control where Ana_ID = " & Val(txtAnaID.Text) & " and Not Control_ID in ("
            For i As Integer = 0 To dgvControls.RowCount - 1
                Dim cnc As New SqlConnection(connString)
                cnc.Open()
                Dim cmdc As New SqlCommand("Ana_Control_SP", cnc)
                cmdc.CommandType = Data.CommandType.StoredProcedure
                cmdc.Parameters.AddWithValue("@act", "Upsert")
                cmdc.Parameters.AddWithValue("@Ana_ID", Val(txtAnaID.Text))
                cmdc.Parameters.AddWithValue("@Control_ID", Val(dgvControls.Rows(i).Cells(0).Value))
                cmdc.Parameters.AddWithValue("@Ordinal", i)
                cmdc.Parameters.AddWithValue("@ControlName", dgvControls.Rows(i).Cells(1).Value)
                cmdc.Parameters.AddWithValue("@Lot", dgvControls.Rows(i).Cells(2).Value)
                cmdc.Parameters.AddWithValue("@ExpireDate", CDate(dgvControls.Rows(i).Cells(3).Value))
                cmdc.Parameters.AddWithValue("@Position", Trim(dgvControls.Rows(i).Cells(4).Value))
                Try
                    cmdc.ExecuteNonQuery()
                Catch ex As Exception
                Finally
                    cnc.Close()
                    cnc = Nothing
                End Try
                sqlClean = sqlClean & dgvControls.Rows(i).Cells(0).Value & ", "
            Next
            sqlClean = sqlClean.Substring(0, Len(sqlClean) - 2) & ")"
            ExecuteSqlProcedure(sqlClean)
        Else
            ExecuteSqlProcedure("Delete from Ana_Control where Ana_ID = " & Val(txtAnaID.Text))
        End If
        'Following code saves tests
        If dgvEquips.RowCount > 0 Then
            ExecuteSqlProcedure("Delete from Ana_Test where Ana_ID = " & Val(txtAnaID.Text))
            For i As Integer = 0 To dgvEquips.RowCount - 1
                If (dgvEquips.Rows(i).Cells(0).Value IsNot Nothing AndAlso Trim(dgvEquips.Rows(i).Cells(0).Value.ToString) <> "") And
                ((dgvEquips.Rows(i).Cells(1).Value IsNot Nothing AndAlso Trim(dgvEquips.Rows(i).Cells(1).Value.ToString) <> "") Or
                (dgvEquips.Rows(i).Cells(2).Value IsNot Nothing AndAlso Trim(dgvEquips.Rows(i).Cells(2).Value.ToString) <> "")) Then
                    Dim cnt As New SqlConnection(connString)
                    cnt.Open()
                    Dim cmdt As New SqlCommand("Ana_Test_SP", cnt)
                    cmdt.CommandType = Data.CommandType.StoredProcedure
                    cmdt.Parameters.AddWithValue("@act", "Upsert")
                    cmdt.Parameters.AddWithValue("@Ana_ID", Val(txtAnaID.Text))
                    cmdt.Parameters.AddWithValue("@Test_ID", Val(dgvEquips.Rows(i).Cells(0).Value))
                    cmdt.Parameters.AddWithValue("@Ordinal", i)
                    If dgvEquips.Rows(i).Cells(1).Value IsNot Nothing _
                    AndAlso Trim(dgvEquips.Rows(i).Cells(1).Value) <> "" Then
                        cmdt.Parameters.AddWithValue("@EqTest_ID", Trim(dgvEquips.Rows(i).Cells(1).Value))
                    Else
                        cmdt.Parameters.AddWithValue("@EqTest_ID", "")
                    End If
                    If dgvEquips.Rows(i).Cells(2).Value IsNot Nothing _
                     AndAlso Trim(dgvEquips.Rows(i).Cells(2).Value) <> "" Then
                        cmdt.Parameters.AddWithValue("@EqResult_ID", Trim(dgvEquips.Rows(i).Cells(2).Value))
                    Else
                        cmdt.Parameters.AddWithValue("@EqResult_ID", "")
                    End If
                    Try
                        cmdt.ExecuteNonQuery()
                    Catch ex As Exception
                    Finally
                        cnt.Close()
                        cnt = Nothing
                    End Try
                End If
            Next
        Else
            ExecuteSqlProcedure("Delete from Ana_Test where Ana_ID = " & Val(txtAnaID.Text))
        End If
        'Following code saves the Ranges
        If Val(txtControls.Text) > 0 Then
            ExecuteSqlProcedure("Delete from Ana_Ranges where Ana_ID = " & Val(txtAnaID.Text))
            Dim Tests As String = ""
            Dim Ctls As String = ""
            Dim LotInfo() As String
            For i As Integer = 0 To dgvRanges.RowCount - 1
                Dim exist As Boolean = False
                For ii As Integer = 0 To dgvEquips.RowCount - 1
                    If dgvEquips.Rows(ii).Cells(0).Value = dgvRanges.Rows(i).Cells(2).Value Then
                        exist = True
                        Exit For
                    End If
                Next
                If exist = True Then
                    LotInfo = GetLotInfo(Val(txtAnaID.Text), Val(dgvRanges.Rows(i).Cells(0).Value))
                    If LotInfo(0) <> "" And IsDate(LotInfo(1)) Then
                        Dim cnr As New SqlConnection(connString)
                        cnr.Open()
                        Dim cmdr As New SqlCommand("Ana_Ranges_SP", cnr)
                        cmdr.CommandType = Data.CommandType.StoredProcedure
                        cmdr.Parameters.AddWithValue("@act", "Upsert")
                        cmdr.Parameters.AddWithValue("@Ana_ID", Val(txtAnaID.Text))

                        cmdr.Parameters.AddWithValue("@Control_ID", Val(dgvRanges.Rows(i).Cells(0).Value))
                        cmdr.Parameters.AddWithValue("@Test_ID", Val(dgvRanges.Rows(i).Cells(2).Value))
                        cmdr.Parameters.AddWithValue("@Ordinal", i)
                        cmdr.Parameters.AddWithValue("@Lot", LotInfo(0))
                        cmdr.Parameters.AddWithValue("@ExpireDate", CDate(LotInfo(1)))
                        cmdr.Parameters.AddWithValue("@Quantitative", Quantitative(dgvRanges.Rows(i).Cells(2).Value))
                        cmdr.Parameters.AddWithValue("@MeanNormal", dgvRanges.Rows(i).Cells(4).Value)
                        cmdr.Parameters.AddWithValue("@FactorAbnormal", dgvRanges.Rows(i).Cells(5).Value)
                        cmdr.Parameters.AddWithValue("@Low", Val(dgvRanges.Rows(i).Cells(6).Value))
                        cmdr.Parameters.AddWithValue("@High", Val(dgvRanges.Rows(i).Cells(7).Value))
                        Try
                            cmdr.ExecuteNonQuery()
                        Catch ex As Exception
                        Finally
                            cnr.Close()
                            cnr = Nothing
                        End Try
                    End If
                End If

            Next
        Else
            ExecuteSqlProcedure("Delete from Ana_Ranges where Ana_ID = " & Val(txtAnaID.Text))
        End If
    End Sub

    Private Function GetLotInfo(ByVal AnaID As Integer, ByVal ControlID As Long) As String()
        Dim LotInfo() As String = {"", ""}
        Dim sSQL As String = "Select * from Ana_Control where Ana_ID = " & AnaID & " and Control_ID = " & ControlID
        If connString <> "" Then
            Dim cnt As New SqlConnection(connString)
            cnt.Open()
            Dim cmdt As New SqlCommand(sSQL, cnt)
            cmdt.CommandType = Data.CommandType.Text
            Dim drt As SqlDataReader = cmdt.ExecuteReader
            If drt.HasRows Then
                While drt.Read
                    LotInfo(0) = drt("Lot")
                    If IsDate(drt("ExpireDate")) Then LotInfo(1) =
                    Format(drt("ExpireDate"), SystemConfig.DateFormat)
                End While
            End If
            cnt.Close()
            cnt = Nothing
            'Else
            '    Dim cnt As New Data.Odbc.OdbcConnection(connstring)
            '    cnt.Open()
            '    Dim cmdt As New Data.Odbc.OdbcCommand(sSQL, cnt)
            '    cmdt.CommandType = Data.CommandType.Text
            '    Dim drt As Data.Odbc.OdbcDataReader = cmdt.ExecuteReader
            '    If drt.HasRows Then
            '        While drt.Read
            '            LotInfo(0) = drt("Lot")
            '            If IsDate(drt("ExpireDate")) Then LotInfo(1) =
            '            Format(drt("ExpireDate"), SystemConfig.DateFormat)
            '        End While
            '    End If
            '    cnt.Close()
            '    cnt = Nothing
        End If
        Return LotInfo
    End Function

    Private Function UpdateLotID(ByVal AnaID As Integer, ByVal ControlID As Long,
    ByVal Lot As String, ByVal ExpDate As Date) As Long
        Dim LotID As Long = NextLotID()
        Dim sSQL As String = "Select ID from Lots where Ana_ID = " & AnaID &
        " and Control_ID = " & ControlID & " and LotName = '" & Lot & "'"
        If connString <> "" Then
            Dim cni As New SqlConnection(connString)
            cni.Open()
            Dim cmdi As New SqlCommand(sSQL, cni)
            cmdi.CommandType = Data.CommandType.Text
            Dim dri As SqlDataReader = cmdi.ExecuteReader
            If dri.HasRows Then
                While dri.Read
                    LotID = dri("ID")
                End While
            End If
            cni.Close()
            cni = Nothing
            '
            Dim cnl As New SqlConnection(connString)
            cnl.Open()
            Dim cmdupsert As New SqlCommand("Lots_SP", cnl)
            cmdupsert.CommandType = Data.CommandType.StoredProcedure
            cmdupsert.Parameters.AddWithValue("@act", "Upsert")
            cmdupsert.Parameters.AddWithValue("@ID", LotID)
            cmdupsert.Parameters.AddWithValue("@Ana_ID", AnaID)
            cmdupsert.Parameters.AddWithValue("@Control_ID", ControlID)
            cmdupsert.Parameters.AddWithValue("@LotName", Lot)
            cmdupsert.Parameters.AddWithValue("@StartDate", Date.Now)
            cmdupsert.Parameters.AddWithValue("@ExpireDate", ExpDate)
            Try
                cmdupsert.ExecuteNonQuery()
            Catch ex As Exception
            Finally
                cnl.Close()
                cnl = Nothing
            End Try
            'Else
            '    Dim cni As New Data.Odbc.OdbcConnection(connstring)
            '    cni.Open()
            '    Dim cmdi As New Data.Odbc.OdbcCommand(sSQL, cni)
            '    cmdi.CommandType = Data.CommandType.Text
            '    Dim dri As Data.Odbc.OdbcDataReader = cmdi.ExecuteReader
            '    If dri.HasRows Then
            '        While dri.Read
            '            LotID = dri("ID")
            '        End While
            '    End If
            '    cni.Close()
            '    cni = Nothing
            '    '
            '    Dim cnl As New Data.Odbc.OdbcConnection(connstring)
            '    cnl.Open()
            '    Dim cmdupsert As New Data.Odbc.OdbcCommand("Lots_SP", cnl)
            '    cmdupsert.CommandType = Data.CommandType.StoredProcedure
            '    cmdupsert.Parameters.AddWithValue("@act", "Upsert")
            '    cmdupsert.Parameters.AddWithValue("@ID", LotID)
            '    cmdupsert.Parameters.AddWithValue("@Ana_ID", AnaID)
            '    cmdupsert.Parameters.AddWithValue("@Control_ID", ControlID)
            '    cmdupsert.Parameters.AddWithValue("@LotName", Lot)
            '    cmdupsert.Parameters.AddWithValue("@StartDate", Date.Now)
            '    cmdupsert.Parameters.AddWithValue("@ExpireDate", ExpDate)
            '    Try
            '        cmdupsert.ExecuteNonQuery()
            '    Catch ex As Exception
            '    Finally
            '        cnl.Close()
            '        cnl = Nothing
            '    End Try
        End If
        Return LotID
    End Function

    Private Function NextLotID() As Long
        Dim NextID As Long = 1
        Dim sSQL As String = "Select max(ID) as LastID from Lots"
        Dim cno As New SqlConnection(connString)
        cno.Open()
        Dim cmdo As New SqlCommand(sSQL, cno)
        cmdo.CommandType = Data.CommandType.Text
        Dim dro As SqlDataReader = cmdo.ExecuteReader
        If dro.HasRows Then
            While dro.Read
                If dro("LastID") IsNot DBNull.Value _
                Then NextID = dro("LastID") + 1
            End While
        End If
        cno.Close()
        cno = Nothing
        Return NextID
    End Function

    Private Function AddComboChoices(ByVal cmbCell As DataGridViewComboBoxCell, ByVal TestID As Integer) As DataGridViewComboBoxCell
        cmbCell.Items.Clear()
        Dim sSQL As String = "Select * from C_Ranges where Test_ID = " & TestID
        If connString <> "" Then
            Dim cnc As New SqlConnection(connString)
            cnc.Open()
            Dim cmdc As New SqlCommand(sSQL, cnc)
            cmdc.CommandType = Data.CommandType.Text
            Dim drc As SqlDataReader = cmdc.ExecuteReader
            If drc.HasRows Then
                While drc.Read
                    cmbCell.Items.Add(drc("Choice"))
                End While
            End If
            cnc.Close()
            cnc = Nothing
            'Else
            '    Dim cnc As New Data.Odbc.OdbcConnection(connstring)
            '    cnc.Open()
            '    Dim cmdc As New Data.Odbc.OdbcCommand(sSQL, cnc)
            '    cmdc.CommandType = Data.CommandType.Text
            '    Dim drc As Data.Odbc.OdbcDataReader = cmdc.ExecuteReader
            '    If drc.HasRows Then
            '        While drc.Read
            '            cmbCell.Items.Add(drc("Choice"))
            '        End While
            '    End If
            '    cnc.Close()
            '    cnc = Nothing
        End If
        Return cmbCell
    End Function

    Private Sub SyncRanges()
        Dim c As Integer
        Dim t As Integer
        If dgvControls.RowCount > 0 And dgvEquips.RowCount > 0 Then
            'dgvRanges.RowCount = dgvControls.RowCount * dgvEquips.RowCount
            For c = 0 To dgvControls.RowCount - 1
                For t = 0 To dgvEquips.RowCount - 1
                    If GetTGPType(dgvEquips.Rows(t).Cells(0).Value) = "T" AndAlso Not _
                    CntInTheList(dgvControls.Rows(c).Cells(0).Value, dgvEquips.Rows(t).Cells(0).Value) Then
                        dgvRanges.Rows.Add(dgvControls.Rows(c).Cells(0).Value,
                        dgvControls.Rows(c).Cells(1).Value,
                        dgvEquips.Rows(t).Cells(0).Value,
                        dgvEquips.Rows(t).Cells(3).Value, "", "", "", "")
                        If Quantitative(dgvEquips.Rows(t).Cells(0).Value) = False Then
                            Dim cmbCell4 As New DataGridViewComboBoxCell
                            cmbCell4 = AddComboChoices(cmbCell4, dgvEquips.Rows(t).Cells(0).Value)
                            Dim cmbCell5 As New DataGridViewComboBoxCell
                            cmbCell5 = AddComboChoices(cmbCell5, dgvEquips.Rows(t).Cells(0).Value)
                            dgvRanges.Rows(dgvRanges.RowCount - 1).Cells(4) = cmbCell4
                            dgvRanges.Rows(dgvRanges.RowCount - 1).Cells(5) = cmbCell5
                            dgvRanges.Rows(dgvRanges.RowCount - 1).Cells(6).ReadOnly = True
                            dgvRanges.Rows(dgvRanges.RowCount - 1).Cells(7).ReadOnly = True
                        Else
                            dgvRanges.Rows(dgvRanges.RowCount - 1).Cells(6).ReadOnly = False
                            dgvRanges.Rows(dgvRanges.RowCount - 1).Cells(7).ReadOnly = False
                        End If
                    Else
                        SyncControls()
                    End If
                Next
            Next
        Else
            dgvRanges.Rows.Clear()
        End If
    End Sub

    Private Sub SyncControls()
        Dim c As Integer
        Dim r As Integer
        For c = 0 To dgvControls.RowCount - 1
            For r = 0 To dgvRanges.RowCount - 1
                If dgvControls.Rows(c).Cells(0).Value = dgvRanges.Rows(r).Cells(0).Value Then
                    dgvRanges.Rows(r).Cells(1).Value = dgvControls.Rows(c).Cells(1).Value
                End If
            Next
        Next
    End Sub

    Private Function CntInTheList(ByVal CntID As String, ByVal TestID As Integer) As Boolean
        Dim InList As Boolean = False
        Dim i As Integer
        For i = 0 To dgvRanges.RowCount - 1
            If dgvRanges.Rows(i).Cells(0).Value = CntID And
            dgvRanges.Rows(i).Cells(2).Value = TestID Then
                InList = True
                Exit For
            End If
        Next
        CntInTheList = InList
    End Function

    Private Function Quantitative(ByVal TestID As Integer) As Boolean
        Dim Quant As Boolean = False
        Dim sSQL As String = "Select * from Tests where ID = " & TestID
        If connString <> "" Then
            Dim cnq As New SqlConnection(connString)
            cnq.Open()
            Dim cmdq As New SqlCommand(sSQL, cnq)
            cmdq.CommandType = Data.CommandType.Text
            Dim drq As SqlDataReader = cmdq.ExecuteReader
            If drq.HasRows Then
                While drq.Read
                    If drq("Qualitative") IsNot DBNull.Value _
                    Then Quant = Not drq("Qualitative")
                End While
            End If
            cnq.Close()
            cnq = Nothing
            'Else
            '    Dim cnq As New Data.Odbc.OdbcConnection(connstring)
            '    cnq.Open()
            '    Dim cmdq As New Data.Odbc.OdbcCommand(sSQL, cnq)
            '    cmdq.CommandType = Data.CommandType.Text
            '    Dim drq As Data.Odbc.OdbcDataReader = cmdq.ExecuteReader
            '    If drq.HasRows Then
            '        While drq.Read
            '            If drq("Qualitative") IsNot DBNull.Value _
            '            Then Quant = Not drq("Qualitative")
            '        End While
            '    End If
            '    cnq.Close()
            '    cnq = Nothing
        End If
        Return Quant
    End Function

    Private Sub dgvRanges_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvRanges.CellValidated
        If Quantitative(dgvRanges.Rows(e.RowIndex).Cells(2).Value) Then
            If Not dgvRanges.Rows(e.RowIndex).Cells(e.ColumnIndex).Value Is Nothing _
            AndAlso dgvRanges.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString <> "" Then
                If e.ColumnIndex > 3 Then
                    If Not IsNumeric(dgvRanges.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
                        MsgBox("You must enter numeric values only")
                        dgvRanges.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ""
                    End If
                End If
                If e.ColumnIndex = 4 Or e.ColumnIndex = 5 Then
                    If dgvRanges.Rows(e.RowIndex).Cells(5).Value Is Nothing OrElse
                    dgvRanges.Rows(e.RowIndex).Cells(5).Value Is System.DBNull.Value _
                    Then dgvRanges.Rows(e.RowIndex).Cells(5).Value = "0"
                    dgvRanges.Rows(e.RowIndex).Cells(6).Value =
                    CStr(Val(dgvRanges.Rows(e.RowIndex).Cells(4).Value) -
                    Val(dgvRanges.Rows(e.RowIndex).Cells(5).Value))
                    dgvRanges.Rows(e.RowIndex).Cells(7).Value =
                    CStr(Val(dgvRanges.Rows(e.RowIndex).Cells(4).Value) +
                    Val(dgvRanges.Rows(e.RowIndex).Cells(5).Value))
                End If
            End If
        End If
    End Sub

    Private Sub txtControls_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtControls.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtControls_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtControls.Validated
        If txtControls.Text <> "" Then
            If Val(txtControls.Text) > 24 Or Val(txtControls.Text) < 0 Then
                MsgBox("Prolis supports maximum 24 controls for a given Analysis")
                txtControls.Text = ""
                txtControls.Focus()
            Else
                dgvControls.RowCount = Val(txtControls.Text)
                cmbInRange.Items.Clear()
                Dim i As Integer
                For i = 0 To dgvControls.RowCount - 1
                    dgvControls.Rows(i).Cells(0).Value = i + 1
                    dgvControls.Rows(i).Cells(1).Value = "Control - " & CStr(i + 1)
                    'cmbInRange.Items.Add(CStr(CInt(i / Val(txtControls.Text) * 100)) & " %")
                Next
                UpdateInRangePercent(dgvEquips.RowCount)
                cmbInRange.SelectedIndex = cmbInRange.Items.Count - 1
                cmbInRange.Enabled = True
                txtValidaters.Text = txtControls.Text
            End If
        Else
            txtValidaters.Text = ""
            dgvControls.RowCount = 0
        End If
        dtpExpire.Visible = False
        SyncRanges()
    End Sub

    Private Sub dgvControls_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvControls.CellClick
        If e.RowIndex <> -1 Then
            If e.ColumnIndex = 3 Then
                dtpExpire.Left = dgvControls.Left + 290
                dtpExpire.Top = dgvControls.Top + (dgvControls.Rows(e.RowIndex).Height * (e.RowIndex + 1))
                dtpExpire.Visible = True
                dgvControls.Rows(e.RowIndex).Cells(3).Value = Format(dtpExpire.Value, SystemConfig.DateFormat)
            End If
        End If
    End Sub

    Private Sub dtpExpire_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpExpire.Validated
        dgvControls.SelectedCells(3).Value = Format(dtpExpire.Value, SystemConfig.DateFormat)
        dtpExpire.Visible = False
    End Sub

    Private Sub dtpExpire_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpExpire.ValueChanged
        dgvControls.SelectedCells(3).Value = Format(dtpExpire.Value, SystemConfig.DateFormat)
        dtpExpire.Visible = False
    End Sub

    Private Sub dgvControls_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvControls.CellEnter
        If e.ColumnIndex = 3 Then
            dtpExpire.Left = dgvControls.Left + 290
            dtpExpire.Top = dgvControls.Top + (dgvControls.Rows(e.RowIndex).Height * (e.RowIndex + 1))
            dtpExpire.Visible = True
        Else
            dtpExpire.Visible = False
        End If
    End Sub

    Private Sub dgvControls_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvControls.CellEndEdit
        If e.ColumnIndex = 1 Then
            SyncRanges()
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If txtAnaID.Text <> "" And txtanaName.Text <> "" Then
            Dim RetVal As Integer
            RetVal = MsgBox("Prolis designer does not recommend to hard delete any Analysis " _
            & "record once it is being used in the system. Do you really want to delete it?",
            MsgBoxStyle.Critical + MsgBoxStyle.YesNo)
            If RetVal = vbYes Then
                If ThisUser.Hard_Deletion = True Then
                    ExecuteSqlProcedure("Delete from Anas where ID = " & Val(txtAnaID.Text))
                    ExecuteSqlProcedure("Delete from Ana_Control where Ana_ID = " & Val(txtAnaID.Text))
                    ExecuteSqlProcedure("Delete from Ana_Test where Ana_ID = " & Val(txtAnaID.Text))
                    ExecuteSqlProcedure("Delete from Ana_Ranges where Ana_ID = " & Val(txtAnaID.Text))
                    ClearForm()
                Else
                    MsgBox("Oops! You are not allowed to delete the Analysis record.")
                End If
            End If
        Else
            MsgBox("Invalid command while the record is not selected")
        End If
    End Sub

    Private Sub btnDeptEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeptEdit.Click
        frmDepts.ShowDialog()
        Populate_Depts()
    End Sub

    Private Sub btnEquipEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEquipEdit.Click
        Try
            frmEquipments.ShowDialog()
            Populate_Equips()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub txtValidaters_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtValidaters.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtValidaters_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtValidaters.Validated
        If txtValidaters.Text <> "" Then
            If Val(txtControls.Text) > 0 Then   'Analysis explicit controls
                If Val(txtValidaters.Text) > Val(txtControls.Text) Then
                    MsgBox("Analysis validating Controls can not be more than the Analysis " &
                    "control count. Either increase the count or Lower the number of valid" &
                    "ating controls", MsgBoxStyle.Critical, "Prolis")
                    txtValidaters.Text = txtControls.Text
                End If
            Else
                txtValidaters.Text = txtControls.Text
            End If
        Else
            txtValidaters.Text = "0"
        End If
    End Sub

    Private Sub btnAnaLookup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnaLookup.Click
        Dim AnaID As String = frmAnaLookup.ShowDialog()
        If AnaID <> "" Then
            Me.Cursor = Cursors.WaitCursor
            Display_Analysis(Val(AnaID))
            btnSave.Enabled = True
            btnDelete.Enabled = True
            txtanaName.Focus()

            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub dgvRanges_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvRanges.DataError
        On Error Resume Next
    End Sub

End Class
