Imports System.Windows.Forms
Imports Microsoft.VisualBasic
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Microsoft.Data.SqlClient
Imports DataTable = System.Data.DataTable

Public Class frmTests
    Private SearchMode As Boolean = True
    Private OldAtribs As String
    Private NewAtribs As String
    Public Shared rsNotes As String = ""

    Dim Panel2Location As Point

    '=============================================

    Private _testID As String

    ' Property to get and set TestID
    Public Property TestID As String
        Get
            Return _testID
        End Get
        Set(value As String)
            _testID = value
        End Set
    End Property

    '=============================================

    Private Sub cmbSex_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSex.GotFocus
        cmbSex.BackColor = FCOLOR
    End Sub

    Private Sub cmbSex_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbSex.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmbSex_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSex.LostFocus
        cmbSex.BackColor = NFCOLOR
    End Sub
    Private Sub cmbSex_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSex.SelectedIndexChanged
        If txtAgeFrom.Text <> "" And txtAgeTo.Text <> "" Then btnAddModifier.Enabled = True
    End Sub

    Private Sub dgvModifiers_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvModifiers.CellContentClick
        Try
            If dgvModifiers.SelectedRows(0).Index <> -1 Then btnRemoveMod.Enabled = True
        Catch
        End Try
    End Sub

    Private Sub txtAgeFrom_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAgeFrom.GotFocus
        txtAgeFrom.BackColor = FCOLOR
    End Sub

    Private Sub txtAgeFrom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAgeFrom.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtAgeFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAgeFrom.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAgeFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAgeFrom.LostFocus
        txtAgeFrom.BackColor = NFCOLOR
        If txtAgeFrom.Text <> "" Then
            If Val(txtAgeFrom.Text) > 0 And Val(txtAgeFrom.Text) < 130 Then
                If cmbSex.SelectedIndex <> -1 And txtAgeTo.Text <> "" Then btnAddModifier.Enabled = True
            Else
                MsgBox("Prolis supports the Age range from 1 to 130 years only")
                txtAgeFrom.Text = ""
                txtAgeFrom.Focus()
            End If
        End If
    End Sub

    Private Sub txtAgeTo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAgeTo.GotFocus
        txtAgeTo.BackColor = FCOLOR
    End Sub

    Private Sub txtAgeTo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAgeTo.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtAgeTo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAgeTo.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAgeTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAgeTo.LostFocus
        txtAgeTo.BackColor = NFCOLOR
        If txtAgeTo.Text <> "" Then
            If Val(txtAgeTo.Text) > 0 And Val(txtAgeTo.Text) < 130 Then
                If cmbSex.SelectedIndex <> -1 And txtAgeFrom.Text <> "" Then btnAddModifier.Enabled = True
            Else
                MsgBox("Prolis supports the Age range from 1 to 130 years only")
                txtAgeTo.Text = ""
                txtAgeTo.Focus()
            End If
        End If
    End Sub

    Private Sub btnAddModifier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddModifier.Click
        If txtTestID.Text <> "" Then
            dgvModifiers.Rows.Add(dgvModifiers.RowCount + 1, txtTestID.Text, cmbSex.SelectedItem.ToString, txtAgeFrom.Text, txtAgeTo.Text)
            cmbSex.SelectedIndex = -1
            txtAgeFrom.Text = ""
            txtAgeTo.Text = ""
            btnAddModifier.Enabled = False
        End If
    End Sub

    Private Sub btnRemoveAllMod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveAllMod.Click
        dgvModifiers.Rows.Clear()
        btnRemoveAllMod.Enabled = False
    End Sub

    Private Sub btnRemoveMod_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemoveMod.Click
        dgvModifiers.Rows.Remove(dgvModifiers.SelectedRows(0))
        btnRemoveMod.Enabled = False
    End Sub

    Private Sub txtAgeTo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAgeTo.TextChanged
        If txtAgeTo.Text <> "" Then
            If Val(txtAgeTo.Text) > 0 And Val(txtAgeTo.Text) < 130 Then
                If cmbSex.SelectedIndex <> -1 And txtAgeFrom.Text <> "" Then btnAddModifier.Enabled = True
            End If
        End If
    End Sub

    Private Sub ChkActive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkActive.CheckedChanged
        If ChkActive.Checked = False Then
            ChkActive.Text = "NO"
        Else
            ChkActive.Text = "YES"
        End If
    End Sub

    Private Sub ChkMarkable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkMarkable.CheckedChanged
        Dim ctl As Control
        If ChkMarkable.Checked = False Then
            ChkMarkable.Text = "NO"
            dgvModifiers.Rows.Clear()
            For Each ctl In TabControl1.TabPages("tpMarking").Controls
                ctl.Enabled = False
            Next
        Else
            ChkMarkable.Text = "YES"
            For Each ctl In TabControl1.TabPages("tpMarking").Controls
                ctl.Enabled = True
            Next
        End If
    End Sub

    Private Sub ChkResult_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkResult.CheckedChanged
        If ChkResult.Checked = False Then
            ChkResult.Text = "NO"
            DisableResults()
        Else
            ChkResult.Text = "YES"
            EnableResults()
        End If
        Update_Progress()
    End Sub
    Private Sub DisableResults()
        txtUOM.Text = ""
        txtUOM.Enabled = False
        chkResultType.Enabled = False
        ChkCalculated.Checked = False
        ChkCalculated.Enabled = False
        chkAGRange.Checked = False
        chkAGRange.Enabled = False
        chkAutomarker.Checked = False
        chkAutomarker.Enabled = False
        dgvNRanges.Rows.Clear()
        TabControl2.Enabled = False
    End Sub
    Private Sub EnableResults()
        txtUOM.Enabled = True
        chkResultType.Enabled = True
        ChkCalculated.Enabled = True
        chkAGRange.Enabled = True
        chkAutomarker.Enabled = True
        TabControl2.Enabled = True
    End Sub

    Private Sub chkResultType_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkResultType.CheckedChanged
        If chkResultType.Checked = False Then
            chkResultType.Text = "Numeric"
            cmbDecimals.Enabled = True
            chkAGRange.Enabled = True
            EnableNRanges()
            DisableCRanges()
            TabControl2.SelectTab("tpNRanges")
            chkDeltaCheck.Checked = False
            chkDeltaCheck.Enabled = False
        Else
            chkResultType.Text = "Choice"
            cmbDecimals.Enabled = False
            chkAGRange.Checked = False
            chkAGRange.Enabled = False
            chkDeltaCheck.Enabled = True
            DisableNRanges()
            EnableCRanges()
            TabControl2.SelectTab("tpCRanges")
        End If
        chkAutomarker.Checked = False
    End Sub
    Private Sub DisableCRanges()
        dgvCRanges.Rows.Clear()
        dgvCRanges.Enabled = False
        cmbChoice.Enabled = False
        cmbCFlag.Enabled = False
    End Sub
    Private Sub EnableCRanges()
        dgvCRanges.Enabled = True
        cmbChoice.Enabled = True
        cmbCFlag.Enabled = True
    End Sub
    Private Sub DisableNRanges()
        dgvNRanges.Rows.Clear()
        dgvNRanges.Enabled = False
        txtNRFrom.Enabled = False
        txtNRTo.Enabled = False
        cmbNRFlag.Enabled = False
    End Sub
    Private Sub EnableNRanges()
        dgvNRanges.Enabled = True
        txtNRFrom.Enabled = True
        txtNRTo.Enabled = True
        cmbNRFlag.Enabled = True
    End Sub

    Private Sub chkEditNew_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkEditNew.CheckedChanged
        ClearForm()
        If chkEditNew.Checked = False Then
            chkEditNew.Text = "Edit"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Edit.ico")
            btnTestLook.Enabled = True
            btnImport.Enabled = True
        Else
            chkEditNew.Text = "New"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\New.ico")
            btnTestLook.Enabled = False
            txtSearchID.Text = GetNextTestID()
            txtTestID.Text = txtSearchID.Text
            btnImport.Enabled = False
        End If
    End Sub

    Private Sub ClearReportableRange()
        txtRptFrom.Text = ""
        txtRptTo.Text = ""
        chkRptRange.Checked = False
    End Sub

    Private Sub ClearForm()
        txtSearchID.Text = ""
        rsNotes = ""
        txtSearchID.ReadOnly = False
        txtTestID.Text = ""
        txtName.Text = ""
        txtAbbr.Text = ""
        txtAltNames.Text = ""
        chkEvaluate.Checked = False
        txtDescription.Text = ""
        txtLoinc.Text = ""
        txtSlideID.Text = ""
        cmbDept.SelectedIndex = 6
        txtResultNote.Text = ""
        btnNote.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\NoteBlank.ico")
        ChkActive.Checked = True
        ChkMarkable.Checked = True
        ChkResult.Checked = True
        chkTBillable.Checked = True
        chkPBillable.Checked = True
        chkCBillable.Checked = True
        chkNorC.Checked = False
        chkForI.Checked = False
        chkAGRange.Checked = False
        cmbCBehavior.SelectedIndex = 1
        cmbNBehavior.SelectedIndex = 1
        cmbAGBehavior.SelectedIndex = 1
        chkReportable.Checked = True
        dgvSRanges.Rows.Clear()
        btnSave.Enabled = False
        btnDelete.Enabled = False
        btnReplicate.Enabled = False
        dgvModifiers.Rows.Clear()
        cmbRptResSex.SelectedIndex = 0
        cmbRPTDescSex.SelectedIndex = 0
        cmbRptNoteSex.SelectedIndex = 0
        chkDotSuppress.Checked = True
        chkDispRange.Checked = True
        chkDispFlag.Checked = True
        chkPreAnalytical.Checked = False
        chkInHouse.Checked = True
        chkESig.Checked = False
        dgvInfo.Rows.Clear()
        dgvInfo.Rows.Add()
        ClearResult()
        ClearBilling()
        ClearMaterials()
        ClearReportableRange()
        dgv_AOE_Questions.Rows.Clear()
        dgv_AOE_Ans.Rows.Clear()
        'txt_AOE_Question.Clear()
        chk_AOE_Required.Checked = False
        cbo_AOE_Q_Type.SelectedIndex = 0
    End Sub

    Private Sub ClearMaterials()
        Dim i As Integer
        For i = 0 To lstMaterials.Items.Count - 1
            lstMaterials.SetItemChecked(i, False)
        Next
    End Sub

    Private Sub ClearResult()
        chkResultType.Checked = False
        dgvNRanges.Rows.Clear()
        txtUOM.Text = ""
        cmbDecimals.SelectedIndex = 5
        ChkCalculated.Checked = False
        chkAGRange.Checked = False
        chkAutomarker.Checked = False
        cmbChoice.Text = ""
        cmbCFlag.Text = ""
        txtRptFrom.Text = ""
        txtRptTo.Text = ""
        chkRptRange.Checked = False
    End Sub

    Private Sub ClearBilling()
        txtCPTCode.Text = "" : txtCPTMCR.Text = "" : txtCPTMCD.Text = ""
        txtCPTSPC.Text = "" : txtBillUnit.Text = "1.0" : txtPOS.Text = "81"
        txtMod1.Text = "" : txtMod2.Text = "" : txtMod3.Text = "" : txtMod4.Text = ""
        txtListPrice.Text = "" : txtPrice1.Text = "" : txtPrice2.Text = ""
        txtPrice3.Text = "" : txtPrice4.Text = "" : txtPrice5.Text = ""
        txtPrice6.Text = "" : txtPrice7.Text = "" : txtPrice8.Text = ""
        txtPrice9.Text = "" : dgvNecessity.Rows.Clear()
    End Sub
    Private Function GetNextTestID() As Long
        Dim LastID As Integer = 0
        Dim DefaultID As Integer = 1

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "
            SELECT MAX(ID) AS LastID FROM Tests
            UNION
            SELECT MAX(ID) AS LastID FROM Groups
            UNION
            SELECT MAX(ID) AS LastID FROM Profiles
            UNION
            SELECT MAX(ID) AS LastID FROM Departments"

            Using command As New SqlCommand(query, connection)
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        If Not IsDBNull(reader("LastID")) Then
                            Dim currentID As Integer = Convert.ToInt32(reader("LastID"))
                            If currentID > LastID Then
                                LastID = currentID
                            End If
                        End If
                    End While
                End Using
            End Using
        End Using

        If LastID > DefaultID Then
            Return LastID + 1
        Else
            Return DefaultID
        End If
    End Function

    Private Sub ChkCalculated_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkCalculated.CheckedChanged
        If ChkCalculated.Checked = False Then
            ChkCalculated.Text = "NO"
            txtFormula.Text = ""
            txtFormula.Enabled = False
            btnFormula.Enabled = False
        Else
            ChkCalculated.Text = "YES"
            txtFormula.Enabled = True
            btnFormula.Enabled = True
        End If
    End Sub

    Private Sub EnableAGRanges()
        dgvAGRanges.Enabled = True
        txtAGRAgeFrom.Enabled = True
        txtAGRAgeTo.Enabled = True
        cmbAGSex.Enabled = True
        txtAGVFrom.Enabled = True
        txtAGVTo.Enabled = True
        cmbAGFlag.Enabled = True
    End Sub

    Private Sub DisableAGRanges()
        dgvAGRanges.Rows.Clear()
        dgvAGRanges.Enabled = False
        txtAGRAgeFrom.Enabled = False
        txtAGRAgeTo.Enabled = False
        cmbAGSex.Enabled = False
        txtAGVFrom.Enabled = False
        txtAGVTo.Enabled = False
        cmbAGFlag.Enabled = False
    End Sub

    Private Sub chkAGRange_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAGRange.CheckedChanged
        If chkResultType.Checked = False Then   'Numeric
            EnableAGRanges()
            EnableNRanges()
            DisableCRanges()
            If chkAGRange.Checked = True Then
                TabControl2.SelectTab("tpAGRanges")
            Else
                TabControl2.SelectTab("tpNRanges")
                dgvAGRanges.Rows.Clear()
            End If
        Else
            DisableAGRanges()
            DisableNRanges()
            EnableCRanges()
            TabControl2.SelectTab("tpCRanges")
        End If
    End Sub

    Private Sub chkAutomarker_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAutomarker.CheckedChanged
        If chkAutomarker.Checked = True Then
            If chkResultType.Checked = False Then
                EnableNAutos()
                DisableCAutos()
                TabControl2.SelectTab("tpNAutomarks")
            Else
                DisableNAutos()
                EnableCAutos()
                TabControl2.SelectTab("tpCAutomarks")
            End If
            EnableCondAutos()
        Else
            DisableCondAutos()
            DisableNAutos()
            DisableCAutos()
        End If
    End Sub

    Private Sub EnableCondAutos()
        dgvCondAutomarks.Enabled = True
        cmbAutoCond.Enabled = True
        txtCondTrigID.ReadOnly = False
        txtCondTrigName.ReadOnly = False
        btnCondTrig.Enabled = True
    End Sub

    Private Sub DisableCondAutos()
        dgvCondAutomarks.Rows.Clear()
        dgvCondAutomarks.Enabled = False
        cmbAutoCond.Enabled = False
        txtCondTrigID.ReadOnly = True
        txtCondTrigName.ReadOnly = True
        btnCondTrig.Enabled = False
    End Sub

    Private Sub DisableCAutos()
        dgvCAutomarks.Rows.Clear()
        dgvCAutomarks.Enabled = False
        cmbCAutoChoice.Enabled = False
        txtCTrigID.Enabled = False
        txtCTrigName.Enabled = False
        btnCTrig.Enabled = False
    End Sub

    Private Sub EnableCAutos()
        dgvCAutomarks.Enabled = True
        cmbCAutoChoice.Enabled = True
        txtCTrigID.Enabled = True
        txtCTrigName.Enabled = True
        btnCTrig.Enabled = True
    End Sub

    Private Sub DisableNAutos()
        dgvNAutomarks.Rows.Clear()
        dgvNAutomarks.Enabled = False
        txtNRangeFrom.Enabled = False
        txtNRangeTo.Enabled = False
        txtNTrigID.Enabled = False
        txtNTrigName.Enabled = False
        btnNTrig.Enabled = False
    End Sub

    Private Sub EnableNAutos()
        dgvNAutomarks.Enabled = True
        txtNRangeFrom.Enabled = True
        txtNRangeTo.Enabled = True
        txtNTrigID.Enabled = True
        txtNTrigName.Enabled = True
        btnNTrig.Enabled = True
    End Sub

    Private Sub txtNRFrom_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNRFrom.LostFocus
        txtNRFrom.BackColor = FCOLOR
    End Sub

    Private Sub txtNRFrom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNRFrom.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtNRFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNRFrom.KeyPress
        NRanges(txtNRFrom, e)
    End Sub

    Private Sub txtNRFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNRFrom.LostFocus
        txtNRFrom.BackColor = NFCOLOR
    End Sub

    Private Sub txtNRTo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNRTo.GotFocus
        txtNRTo.BackColor = FCOLOR
    End Sub

    Private Sub txtNRTo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNRTo.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtNRTo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNRTo.KeyPress
        Prices(txtNRTo, e)
    End Sub

    Private Sub txtNRTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNRTo.LostFocus
        txtNRTo.BackColor = NFCOLOR
    End Sub

    Private Sub cmbNRFlag_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbNRFlag.GotFocus
        cmbNRFlag.BackColor = FCOLOR
    End Sub

    Private Sub cmbNRFlag_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbNRFlag.TextChanged
        If cmbNRFlag.Text <> "" Then
            If txtTestID.Text <> "" And txtNRTo.Text <> "" And txtNRFrom.Text <> "" And cmbNBehavior.SelectedIndex <> -1 Then btnAddNR.Enabled = True
        End If
    End Sub

    Private Sub btnAddNR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNR.Click
        If txtTestID.Text <> "" And txtNRFrom.Text <> "" And txtNRTo.Text <> "" And cmbNBehavior.SelectedIndex <> -1 Then
            dgvNRanges.Rows.Add(txtTestID.Text, dgvNRanges.RowCount + 1, txtNRFrom.Text, txtNRTo.Text, cmbNRFlag.Text, cmbNBehavior.SelectedItem.ToString)
            txtNRFrom.Text = ""
            txtNRTo.Text = ""
            cmbNRFlag.Text = ""
            btnRemAllNR.Enabled = True
            txtNRFrom.Focus()
        End If
    End Sub

    Private Sub SaveTest(ByVal TestID As Integer)
        Dim ItemX As MyList
        Dim i As Integer
        Dim sSQL As String = ""
        Dim cnt As New SqlConnection(connString)
        cnt.Open()
        Dim cmdupsert As New SqlCommand("Tests_SP", cnt)
        cmdupsert.CommandType = CommandType.StoredProcedure
        '(@ID int = NULL ,@Name nvarchar(60) = NULL ,@Abbr nchar(10) = NULL ,@AlternateNames nvarchar(4000) = NULL ,@Description nvarchar(250) = NULL ,@Loinc nchar(20) = NULL ,@SlideID nvarchar(10) = NULL ,@ComponentType nchar(1) = NULL ,@IsActive bit = NULL ,@Material_ID tinyint = NULL ,@HasResult bit = NULL, @PreAnalytical bit = NULL ,@PreAnaFormat tinyint = NULL ,@PreAnaRequired bit = NULL, @DeltaCheck bit = NULL, @UOM nvarchar(25) = NULL ,@IsIndustrial bit = NULL, @Qualitative bit = NULL ,@ESig bit = NULL, @DecimalPlaces smallint = NULL, @ResultNote nvarchar(4000) = NULL ,@In_House bit = NULL ,@InHouse bit = NULL, @IsCalculated bit = NULL, @AGRanges bit = NULL, @ReportableRange bit = NULL, @ForI bit = NULL, @NormCut bit = NULL, @Formula nvarchar(250) = NULL ,@Automarker bit = NULL, @CPT_Code nchar(10) = NULL ,@CPT_MCR nchar(10) = NULL ,@CPT_MCD nchar(10) = NULL ,@CPT_SPC nchar(10) = NULL ,@Mod1 nchar(10) = NULL ,@Mod2 nchar(10) = NULL ,@Mod3 nchar(10) = NULL ,@Mod4 nchar(10) = NULL ,@Bill_Unit real = NULL, @POS_Code nchar(12) = NULL ,@PBillable bit = NULL, @CBillable bit = NULL ,@TBillable bit = NULL, @IsReportable bit = NULL ,@Report_Result bit = NULL ,@RptResSex tinyint = NULL ,@Report_Description bit = NULL, @RptDescSex tinyint = NULL, @Report_Note bit = NULL, @RptNoteSex tinyint = NULL, @Report_Source bit = NULL, @Report_Tech bit = NULL, @Report_Info bit = NULL, @Report_Dept bit = NULL, @DotSuppress bit = NULL ,@Report_Var1 bit = NULL, @ReportRange bit = NULL, @ReportFlag bit = NULL, @IsMarkable bit = NULL ,@Department_ID tinyint = NULL, @ListPrice real = NULL, @Price1 real = NULL, @Price2 real = NULL, @Price3 real = NULL, @Price4 real = NULL ,@Price5 real = NULL, @Price6 real = NULL ,@Price7 real = NULL, @Price8 real = NULL, @Price9 real = NULL, @LastEditedOn smalldatetime = NULL ,@EditedBy smallint = NULL  , @Command nvarchar(12)
        cmdupsert.Parameters.AddWithValue("@Command", "Upsert")
        cmdupsert.Parameters.AddWithValue("@ID", TestID)
        cmdupsert.Parameters.AddWithValue("@Name", Trim(txtName.Text))
        cmdupsert.Parameters.AddWithValue("@Abbr", Trim(txtAbbr.Text))
        cmdupsert.Parameters.AddWithValue("@AlternateNames", Trim(txtAltNames.Text))
        cmdupsert.Parameters.AddWithValue("@MedEval", chkEvaluate.Checked)
        cmdupsert.Parameters.AddWithValue("@Description", Trim(txtDescription.Text))
        cmdupsert.Parameters.AddWithValue("@Loinc", Trim(txtLoinc.Text))
        cmdupsert.Parameters.AddWithValue("@SlideID", Trim(txtSlideID.Text))
        cmdupsert.Parameters.AddWithValue("@ComponentType", "T")
        cmdupsert.Parameters.AddWithValue("@IsActive", ChkActive.Checked)
        cmdupsert.Parameters.AddWithValue("@Material_ID", 0)
        cmdupsert.Parameters.AddWithValue("@HasResult", ChkResult.Checked)
        cmdupsert.Parameters.AddWithValue("@PreAnalytical", chkPreAnalytical.Checked)
        If chkPreAnalytical.Checked = True Then
            If cmbInfoFormat.SelectedIndex <> -1 Then _
            cmdupsert.Parameters.AddWithValue("@PreAnaFormat", cmbInfoFormat.SelectedIndex)
            cmdupsert.Parameters.AddWithValue("@PreAnaRequired", chkClinicalRequired.Checked)
        Else
            cmdupsert.Parameters.AddWithValue("@PreAnaFormat", 2)
            cmdupsert.Parameters.AddWithValue("@PreAnaRequired", False)
        End If
        cmdupsert.Parameters.AddWithValue("@DeltaCheck", chkDeltaCheck.Checked)
        cmdupsert.Parameters.AddWithValue("@UOM", Trim(txtUOM.Text))
        cmdupsert.Parameters.AddWithValue("@IsIndustrial", chkAGRange.Checked)
        cmdupsert.Parameters.AddWithValue("@Qualitative", chkResultType.Checked)
        cmdupsert.Parameters.AddWithValue("@ESig", chkESig.Checked)
        If chkResultType.Checked = False Then   'Quantitative
            If cmbDecimals.SelectedIndex = 5 Then   'Auto
                cmdupsert.Parameters.AddWithValue("@DecimalPlaces", GetMaxDecPlaces())
            Else
                cmdupsert.Parameters.AddWithValue("@DecimalPlaces", cmbDecimals.SelectedIndex)
            End If
        Else
            cmdupsert.Parameters.AddWithValue("@DecimalPlaces", 2)
        End If
        cmdupsert.Parameters.AddWithValue("@ResultNote", RTF_To_Text(Trim(txtResultNote.Text)))
        cmdupsert.Parameters.AddWithValue("@ResultNote_RTF", Trim(txtResultNote.Text))
        cmdupsert.Parameters.AddWithValue("@In_House", chkInHouse.Checked)
        cmdupsert.Parameters.AddWithValue("@InHouse", chkInHouse.Checked)
        If ChkCalculated.Checked = True And Trim(txtFormula.Text) = "" Then ChkCalculated.Checked = False
        cmdupsert.Parameters.AddWithValue("@IsCalculated", ChkCalculated.Checked)
        cmdupsert.Parameters.AddWithValue("@AGRanges", chkAGRange.Checked)
        cmdupsert.Parameters.AddWithValue("@ReportableRange", chkRptRange.Checked)
        cmdupsert.Parameters.AddWithValue("@ForI", chkForI.Checked)
        cmdupsert.Parameters.AddWithValue("@NormCut", chkNorC.Checked)
        cmdupsert.Parameters.AddWithValue("@Formula", Trim(txtFormula.Text))
        cmdupsert.Parameters.AddWithValue("@Automarker", chkAutomarker.Checked)
        cmdupsert.Parameters.AddWithValue("@CPT_Code", Trim(txtCPTCode.Text))
        cmdupsert.Parameters.AddWithValue("@CPT_MCR", Trim(txtCPTMCR.Text))
        cmdupsert.Parameters.AddWithValue("@CPT_MCD", Trim(txtCPTMCD.Text))
        cmdupsert.Parameters.AddWithValue("@CPT_SPC", Trim(txtCPTSPC.Text))
        cmdupsert.Parameters.AddWithValue("@Mod1", Trim(txtMod1.Text))
        cmdupsert.Parameters.AddWithValue("@Mod2", Trim(txtMod2.Text))
        cmdupsert.Parameters.AddWithValue("@Mod3", Trim(txtMod3.Text))
        cmdupsert.Parameters.AddWithValue("@Mod4", Trim(txtMod4.Text))
        cmdupsert.Parameters.AddWithValue("@Bill_Unit", Val(txtBillUnit.Text))
        cmdupsert.Parameters.AddWithValue("@POS_Code", Trim(txtPOS.Text))
        cmdupsert.Parameters.AddWithValue("@PBillable", chkPBillable.Checked)
        cmdupsert.Parameters.AddWithValue("@CBillable", chkCBillable.Checked)
        cmdupsert.Parameters.AddWithValue("@TBillable", chkTBillable.Checked)
        cmdupsert.Parameters.AddWithValue("@IsReportable", chkReportable.Checked)
        cmdupsert.Parameters.AddWithValue("@Report_Result", chkReportResult.Checked)
        cmdupsert.Parameters.AddWithValue("@RptResSex", cmbRptResSex.SelectedIndex)
        cmdupsert.Parameters.AddWithValue("@Report_Description", chkReportDescription.Checked)
        cmdupsert.Parameters.AddWithValue("@RptDescSex", cmbRPTDescSex.SelectedIndex)
        cmdupsert.Parameters.AddWithValue("@Report_Note", chkReportNote.Checked)
        cmdupsert.Parameters.AddWithValue("@RptNoteSex", cmbRptNoteSex.SelectedIndex)
        cmdupsert.Parameters.AddWithValue("@Report_Source", chkReportSource.Checked)
        cmdupsert.Parameters.AddWithValue("@Report_Tech", chkReportTech.Checked)
        cmdupsert.Parameters.AddWithValue("@Report_Info", chkReportInfo.Checked)
        cmdupsert.Parameters.AddWithValue("@Report_Dept", chkReportDept.Checked)
        cmdupsert.Parameters.AddWithValue("@DotSuppress", chkDotSuppress.Checked)
        cmdupsert.Parameters.AddWithValue("@Report_Var1", chkReportVar1.Checked)
        cmdupsert.Parameters.AddWithValue("@ReportRange", chkDispRange.Checked)
        cmdupsert.Parameters.AddWithValue("@ReportFlag", chkDispFlag.Checked)
        cmdupsert.Parameters.AddWithValue("@IsMarkable", ChkMarkable.Checked)
        ItemX = cmbDept.SelectedItem
        cmdupsert.Parameters.AddWithValue("@Department_ID", ItemX.ItemData)
        cmdupsert.Parameters.AddWithValue("@ListPrice", Val(txtListPrice.Text))
        cmdupsert.Parameters.AddWithValue("@Price1", Val(txtPrice1.Text))
        cmdupsert.Parameters.AddWithValue("@Price2", Val(txtPrice2.Text))
        cmdupsert.Parameters.AddWithValue("@Price3", Val(txtPrice3.Text))
        cmdupsert.Parameters.AddWithValue("@Price4", Val(txtPrice4.Text))
        cmdupsert.Parameters.AddWithValue("@Price5", Val(txtPrice5.Text))
        cmdupsert.Parameters.AddWithValue("@Price6", Val(txtPrice6.Text))
        cmdupsert.Parameters.AddWithValue("@Price7", Val(txtPrice7.Text))
        cmdupsert.Parameters.AddWithValue("@Price8", Val(txtPrice8.Text))
        cmdupsert.Parameters.AddWithValue("@Price9", Val(txtPrice9.Text))
        cmdupsert.Parameters.AddWithValue("@LastEditedOn", Date.Now)
        cmdupsert.Parameters.AddWithValue("@EditedBy", ThisUser.ID)
        Try
            cmdupsert.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
            Exit Sub
        Finally
            cnt.Close()
            cnt = Nothing
        End Try
        ExecuteSqlProcedure("Delete from Test_Material where Test_ID = " & TestID)
        If ChkCalculated.Checked = False Then
            If lstMaterials.CheckedItems.Count > 0 Then
                For i = 0 To lstMaterials.CheckedItems.Count - 1
                    ItemX = lstMaterials.CheckedItems(i)
                    sSQL = "Insert into Test_Material (Test_ID, Material_ID, " &
                    "Ordinal) values (" & TestID & ", " & ItemX.ItemData & ", " & i & ")"
                    ExecuteSqlProcedure(sSQL)
                Next
            Else
                MsgBox("No material selected for this non-Calculated test. Open this test in the Edit mode and select the material.")
            End If
        End If
        '
        Dim AppLevel As Integer
        ExecuteSqlProcedure("Delete from TGP_Info where TGP_ID = " & TestID)
        For i = 0 To dgvInfo.RowCount - 1
            If dgvInfo.Rows(i).Cells(1).Value IsNot Nothing AndAlso
            dgvInfo.Rows(i).Cells(1).Value.ToString <> "" Then
                If Trim(dgvInfo.Rows(i).Cells(5).Value) = "Accession" Then
                    AppLevel = 0
                ElseIf Trim(dgvInfo.Rows(i).Cells(5).Value) = "Component" Then
                    AppLevel = 1
                Else
                    AppLevel = 2
                End If
                sSQL = "Insert into TGP_Info (TGP_ID, Info_ID, Required, AppLevel, Ordinal, LastEdited_On, LastEdited_By) " &
                "values (" & TestID & ", " & Val(dgvInfo.Rows(i).Cells(1).Value) & ", " & Val(dgvInfo.Rows(i).Cells(4).Value) &
                ", " & AppLevel & ", " & i & ", '" & Date.Now & "', " & ThisUser.ID & ")"
                ExecuteSqlProcedure(sSQL)
            End If
        Next
    End Sub

    Private Function GetMaxDecPlaces() As Integer
        Dim MaxDec As Integer = 0
        Dim DP As Integer = 0
        Dim i As Integer
        If dgvNRanges.RowCount > 0 Then
            For i = 0 To dgvNRanges.RowCount - 1
                If InStr(dgvNRanges.Rows(i).Cells(2).Value.ToString, ".") = 0 Then
                    DP = 0
                Else
                    DP = Len(Microsoft.VisualBasic.Mid(dgvNRanges.Rows(i).Cells(2).Value.ToString,
                    InStr(dgvNRanges.Rows(i).Cells(2).Value.ToString, ".") + 1))
                End If
                If DP > MaxDec Then MaxDec = DP
                If InStr(dgvNRanges.Rows(i).Cells(3).Value.ToString, ".") = 0 Then
                    DP = 0
                Else
                    DP = Len(Microsoft.VisualBasic.Mid(dgvNRanges.Rows(i).Cells(3).Value.ToString,
                    InStr(dgvNRanges.Rows(i).Cells(3).Value.ToString, ".") + 1))
                End If
                If DP > MaxDec Then MaxDec = DP
            Next
        End If
        '
        If dgvAGRanges.RowCount > 0 Then
            For i = 0 To dgvAGRanges.RowCount - 1
                If InStr(dgvAGRanges.Rows(i).Cells(5).Value.ToString, ".") = 0 Then
                    DP = 0
                Else
                    DP = Len(Microsoft.VisualBasic.Mid(dgvAGRanges.Rows(i).Cells(5).Value.ToString,
                    InStr(dgvAGRanges.Rows(i).Cells(5).Value.ToString, ".") + 1))
                End If
                If DP > MaxDec Then MaxDec = DP
                If InStr(dgvAGRanges.Rows(i).Cells(6).Value.ToString, ".") = 0 Then
                    DP = 0
                Else
                    DP = Len(Microsoft.VisualBasic.Mid(dgvAGRanges.Rows(i).Cells(6).Value.ToString,
                    InStr(dgvAGRanges.Rows(i).Cells(6).Value.ToString, ".") + 1))
                End If
                If DP > MaxDec Then MaxDec = DP
            Next
        End If
        Return MaxDec
    End Function

    Private Sub dgvNRanges_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvNRanges.CellClick
        btnRemNR.Enabled = True
    End Sub

    Private Sub btnRemNR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemNR.Click
        dgvNRanges.Rows.Remove(dgvNRanges.SelectedRows(0))
        btnRemNR.Enabled = False
        If dgvNRanges.RowCount = 0 Then btnRemAllNR.Enabled = False
    End Sub

    Private Sub btnRemAllNR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemAllNR.Click
        dgvNRanges.Rows.Clear()
        btnRemAllNR.Enabled = False
        btnRemNR.Enabled = False
    End Sub

    Private Sub SaveModifiers(ByVal TestID As Integer)
        ExecuteSqlProcedure("Delete from MarkingModifiers where Test_ID = " & TestID)
        For i As Integer = 0 To dgvModifiers.RowCount - 1
            ExecuteSqlProcedure("Insert into MarkingModifiers (Test_ID, Sex, AgeFrom, " &
            "AgeTo, LastEditedOn, EditedBy) values (" & TestID & ", '" &
            dgvModifiers.Rows(i).Cells(2).Value.ToString.Substring(0, 1) & "', " &
            dgvModifiers.Rows(i).Cells(3).Value & ", " & dgvModifiers.Rows(i).Cells(4).Value &
            ", '" & Date.Now & "', " & ThisUser.ID & ")")
        Next
    End Sub

    Private Sub SaveAGRanges(ByVal TestID As Integer)
        ExecuteSqlProcedure("Delete from AG_Ranges where Test_ID = " & TestID)
        For i As Integer = 0 To dgvAGRanges.RowCount - 1
            ExecuteSqlProcedure("Insert into AG_Ranges (Test_ID, AgeFrom, AgeTo, Sex, ValueFrom, " &
            "ValueTo, Flag, Behavior, LastEditedOn, EditedBy) values (" & TestID & ", " &
            dgvAGRanges.Rows(i).Cells(2).Value & ", " & dgvAGRanges.Rows(i).Cells(3).Value &
            ", '" & dgvAGRanges.Rows(i).Cells(4).Value.ToString.Substring(0, 1) & "', " &
            Replace(dgvAGRanges.Rows(i).Cells(5).Value, " ", "") & ", " &
            dgvAGRanges.Rows(i).Cells(6).Value & ", '" & dgvAGRanges.Rows(i).Cells(7).Value &
            "', '" & dgvAGRanges.Rows(i).Cells(8).Value & "', '" & Date.Now & "', " & ThisUser.ID & ")")
        Next
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If txtSearchID.Text = txtTestID.Text Then
            If txtTestID.Text <> "" And txtName.Text <> "" And txtAbbr.Text <> "" And ((lstMaterials.CheckedItems.Count > 0 And ChkCalculated.Checked = False) Or (ChkCalculated.Checked = True)) Then

                SaveTest(Val(txtTestID.Text))

                '**************************************************
                Dim DeptX As MyList
                DeptX = cmbDept.SelectedItem

                Dim sql As String = $"
                                    IF EXISTS (SELECT *
                                    FROM Department_Test
                                    WHERE Test_ID = {txtTestID.Text})
                                      UPDATE Department_Test
                                        SET Department_ID = {DeptX.ItemData}
                                      WHERE Test_ID = {txtTestID.Text}
                                    ELSE
                                      INSERT INTO Department_Test (Department_ID, Test_ID, Ordinal)
                                      VALUES({DeptX.ItemData}, {txtTestID.Text}, (SELECT ISNULL(MAX(Ordinal) + 1, 1) FROM Department_Test 
                                      WHERE Department_ID = {DeptX.ItemData}))
                                    "

                ExecuteSqlProcedure(sql)
                '**************************************************

                ExecuteSqlProcedure("Delete from MarkingModifiers where Test_ID = " & Val(txtTestID.Text))
                If ChkMarkable.Checked = True Then
                    If dgvModifiers.RowCount > 0 Then SaveModifiers(Val(txtTestID.Text))
                End If
                If ChkResult.Checked = True Then    'Has result
                    If chkResultType.Checked = False Then   'It is a Numeric Test
                        If SystemConfig.DiagTarget = "V" Then   'Veterinary
                            If dgvSRanges.RowCount > 0 Then
                                SaveSRanges(Val(txtTestID.Text))    'Species ranges entered
                            Else
                                ExecuteSqlProcedure("Delete from S_Ranges where Test_ID = " & Val(txtTestID.Text))
                            End If
                        Else    'Human
                            If dgvNRanges.RowCount > 0 Then
                                SaveNRanges(Val(txtTestID.Text)) 'Numeric Ranges entered
                            Else
                                ExecuteSqlProcedure("Delete from N_Ranges where Test_ID = " & Val(txtTestID.Text))
                            End If
                            '
                            If chkAGRange.Checked = True AndAlso dgvAGRanges.RowCount > 0 Then
                                SaveAGRanges(Val(txtTestID.Text)) 'AG Ranges in place 
                            Else
                                ExecuteSqlProcedure("Delete from AG_Ranges where Test_ID = " & Val(txtTestID.Text))
                            End If
                            '
                            If dgvNAutomarks.RowCount > 0 Then
                                SaveNAutos(Val(txtTestID.Text))
                            Else
                                ExecuteSqlProcedure("Delete from N_Triggers where Test_ID = " & Val(txtTestID.Text))
                            End If
                            '
                            If txtRptFrom.Text <> "" And txtRptTo.Text <> "" Then
                                If CSng(txtRptFrom.Text) <> CSng(txtRptTo.Text) Then
                                    SaveReportable(Val(txtTestID.Text))
                                End If
                            Else
                                ExecuteSqlProcedure("Delete from Reportable_Ranges where Test_ID = " & Val(txtTestID.Text))
                            End If
                        End If
                    Else    'Qualitative
                        If dgvCRanges.RowCount > 0 Then
                            SaveCRanges(Val(txtTestID.Text))
                        Else
                            ExecuteSqlProcedure("Delete from C_Ranges where Test_ID = " & Val(txtTestID.Text))
                        End If
                        '
                        If dgvCAutomarks.RowCount > 0 Then
                            SaveCAutos(Val(txtTestID.Text))
                        Else
                            ExecuteSqlProcedure("Delete from C_Triggers where Test_ID = " & Val(txtTestID.Text))
                        End If
                    End If
                    If dgvCondAutomarks.RowCount > 0 Then
                        SaveConditionalTriggers(Val(txtTestID.Text))
                    Else
                        ExecuteSqlProcedure("Delete from Conditional_Triggers where Test_ID = " & Val(txtTestID.Text))
                    End If
                End If
                '
                UpdateNecessity(Val(txtTestID.Text))
                'PopulateCNMarked()
                '

                '=================================================
                'CRUD of AOE_TGP_Questions Table
                ExecuteSqlProcedure("Delete from AOE_TGP_Questions where TGP_Id = " & Val(txtTestID.Text))

                If dgv_AOE_Questions.RowCount > 0 Then

                    Dim query As String = "Insert into AOE_TGP_Questions(TGP_Id, Q_Id, Answers, AnsType, Required) Values "

                    For x = 0 To dgv_AOE_Questions.RowCount - 1
                        query += $"({txtTestID.Text}, {dgv_AOE_Questions.Rows(x).Cells(0).Value} ,'{dgv_AOE_Questions.Rows(x).Cells(2).Value}','{dgv_AOE_Questions.Rows(x).Cells(4).Value}','{dgv_AOE_Questions.Rows(x).Cells(3).Value}') ,"

                    Next

                    If query.EndsWith(",") Then
                        query = query.Substring(0, query.Length - 1) & ";"

                    End If

                    ExecuteSqlProcedure(query)
                End If

                '=================================================
                If SystemConfig.AuditTrail = True Then
                    NewAtribs = GetCurrentAttribs()
                    Dim ChangeAtribs() As String = {""}
                    If OldAtribs <> NewAtribs Then ChangeAtribs = GetChangedAtribs(OldAtribs, NewAtribs)
                    If chkEditNew.Checked = False Then  'Edit
                        If ChangeAtribs(0) <> "" Then
                            If ChangeAtribs(0) <> ChangeAtribs(1) Then _
                            LogUserEvent(ThisUser.ID, 602, Date.Now.ToString,
                            "Analyte", Val(txtTestID.Text), ChangeAtribs(0), ChangeAtribs(1))
                        End If
                    Else
                        LogUserEvent(ThisUser.ID, 601, Date.Now.ToString,
                        "Analyte", Val(txtTestID.Text), "Added", ChangeAtribs(1))
                    End If
                    NewAtribs = ""
                    OldAtribs = ""
                    ChangeAtribs(0) = ""
                    If ChangeAtribs.Length > 1 Then ChangeAtribs(1) = ""
                End If
                '

                If Replication = True Then Return

                ClearForm()
                If chkEditNew.Checked = True Then
                    txtTestID.Text = GetNextTestID()
                    SearchMode = False
                Else
                    SearchMode = True
                End If
            Else
                MsgBox("Analyte name, Abbr and the Material are required to save it.")
            End If
        Else
            MsgBox("Analyte ID is different than that of displayed record. Correct it and try again.", MsgBoxStyle.Critical, "Prolis")
            txtSearchID.Text = txtTestID.Text
            txtSearchID.Focus()
        End If
    End Sub

    Private Sub SaveSRanges(ByVal TestID As Integer)
        ExecuteSqlProcedure("Delete from S_Ranges where Test_ID = " & TestID)
        For i As Integer = 0 To dgvSRanges.RowCount - 1
            Dim SpID As String = Microsoft.VisualBasic.Mid(dgvSRanges.Rows(i).Cells(1).Value,
            InStr(dgvSRanges.Rows(i).Cells(1).Value, "(") + 1)
            SpID = Microsoft.VisualBasic.Mid(SpID, 1, InStr(SpID, ")") - 1)
            ExecuteSqlProcedure("Insert into S_Ranges (Test_ID, Species_ID, ValueFrom, ValueTo, " &
            "Flag, Behavior, LastEditedOn, EditedBy, Ordinal) values (" & TestID & ", " & SpID &
            ", '" & dgvSRanges.Rows(i).Cells(2).Value & "', '" & dgvSRanges.Rows(i).Cells(3).Value &
            "', '" & dgvSRanges.Rows(i).Cells(4).Value & "', '" & dgvSRanges.Rows(i).Cells(5).Value &
            "', '" & Date.Now & "', " & ThisUser.ID & ", " & i & ")")
        Next
    End Sub

    Private Function GetChangedAtribs(ByVal OldAtribs, ByVal NewAtribs) As String()
        Dim CATS() As String = {"", ""}
        Dim i As Integer
        Dim OFields() As String
        Dim NFields() As String
        If OldAtribs = "" And NewAtribs <> "" Then  'New
            CATS(1) = NewAtribs
        ElseIf OldAtribs <> "" And OldAtribs <> NewAtribs Then  'change
            OFields = Split(OldAtribs, "|")
            NFields = Split(NewAtribs, "|")
            For i = 0 To NFields.Length - 1
                If OFields(i) <> NFields(i) Then
                    CATS(0) += OFields(i) & "|"
                    CATS(1) += NFields(i) & "|"
                End If
            Next
            If CATS(0).Length > 1 And Microsoft.VisualBasic.Right(CATS(0), 1) = "|" _
            Then CATS(0) = Microsoft.VisualBasic.Mid(CATS(0), 1, Len(CATS(0)) - 1)
            If CATS(1).Length > 1 And Microsoft.VisualBasic.Right(CATS(1), 1) = "|" _
            Then CATS(1) = Microsoft.VisualBasic.Mid(CATS(1), 1, Len(CATS(1)) - 1)
        End If
        Return CATS
    End Function

    Private Sub SaveReportable(ByVal TestID As Integer)
        ExecuteSqlProcedure("If Exists (Select * from Reportable_Ranges where Test_ID = " & TestID &
        ") Update Reportable_Ranges set ValueFrom = " & CSng(txtRptFrom.Text) & ", ValueTo = " &
        CSng(txtRptTo.Text) & ", EditedBy = " & ThisUser.ID & ", LastEditedOn = '" & Date.Now &
        "' where Test_ID = " & TestID & " Else Insert into Reportable_Ranges (Test_ID, ValueFrom, " &
        "ValueTo, EditedBy, LastEditedOn) values (" & TestID & ", " & CSng(txtRptFrom.Text) & ", " &
        CSng(txtRptTo.Text) & ", " & ThisUser.ID & ", '" & Date.Now & "')")
    End Sub

    Private Sub UpdateNecessity(ByVal TestID As Integer)
        If chkTBillable.Checked = True Then
            If dgvNecessity.RowCount > 0 Then
                Dim i As Integer
                Dim sSQL As String = ""
                For i = 0 To dgvNecessity.RowCount - 1
                    If dgvNecessity.Rows(i).Cells(2).Value IsNot Nothing _
                    AndAlso Trim(dgvNecessity.Rows(i).Cells(2).Value) <> "" Then
                        sSQL = "If Exists (Select * from Necessity where TGP_ID = " & TestID &
                        " and Dx_Code = '" & Trim(dgvNecessity.Rows(i).Cells(2).Value) & "') " &
                        "Update Necessity Set Ordinal = 0, ICD9 = null where TGP_ID = " & TestID &
                        " and Dx_Code = '" & Trim(dgvNecessity.Rows(i).Cells(2).Value) & "' Else " &
                        "Insert into Necessity (TGP_ID, Dx_Code, ICD9, Ordinal) values (" & TestID &
                        ", '" & Trim(dgvNecessity.Rows(i).Cells(2).Value) & "', Null, 0)"
                        ExecuteSqlProcedure(sSQL)
                    End If
                Next
            End If
        End If
    End Sub

    Private Function InNecessity(ByVal Dx As String, ByVal TGPID As Integer) As Boolean
        Dim DxIn As Boolean = False
        Dim cndx As New SqlConnection(connString)
        cndx.Open()
        Dim cmddx As New SqlCommand("Select * from Necessity " &
        "where TGP_ID = " & TGPID & " and Dx_Code = '" & Dx & "'", cndx)
        cmddx.CommandType = CommandType.Text
        Dim drdx As SqlDataReader = cmddx.ExecuteReader
        If drdx.HasRows Then DxIn = True
        cndx.Close()
        cndx = Nothing
        Return DxIn
    End Function

    Private Sub SaveCRanges(ByVal TestID As Integer)
        ExecuteSqlProcedure("Delete from C_Ranges where Test_ID = " & TestID)
        For i As Integer = 0 To dgvCRanges.RowCount - 1
            ExecuteSqlProcedure("Insert into C_Ranges (Test_ID, Choice, Flag, Behavior, Ordinal, EditedBy, LastEditedOn) " &
            "values (" & TestID & ", '" & Trim(dgvCRanges.Rows(i).Cells(2).Value) & "', '" &
            Trim(dgvCRanges.Rows(i).Cells(3).Value) & "', '" & Trim(dgvCRanges.Rows(i).Cells(4).Value) _
            & "', " & i & ", " & ThisUser.ID & ", '" & Date.Now & "')")
        Next
    End Sub

    Private Sub SaveConditionalTriggers(ByVal TestID As Integer)
        ExecuteSqlProcedure("Delete from Conditional_Triggers where Test_ID = " & TestID)
        For i As Integer = 0 To dgvCondAutomarks.RowCount - 1
            ExecuteSqlProcedure("Insert into Conditional_Triggers (Test_ID, Condition, Marked_ID) " &
            "values (" & TestID & ", '" & Trim(dgvCondAutomarks.Rows(i).Cells(2).Value).Substring(0, 100) &
            "', " & dgvCondAutomarks.Rows(i).Cells(3).Value & ")")
        Next
    End Sub

    Private Sub SaveCAutos(ByVal TestID As Integer)
        ExecuteSqlProcedure("Delete from C_Triggers where Test_ID = " & TestID)
        For i As Integer = 0 To dgvCAutomarks.RowCount - 1
            ExecuteSqlProcedure("Insert into C_Triggers (Test_ID, Choice, Marked_ID, ReflexMulti, Ordinal) " &
            "values (" & TestID & ", '" & Trim(dgvCAutomarks.Rows(i).Cells(2).Value) & "', " &
            dgvCAutomarks.Rows(i).Cells(3).Value & ", " & Convert.ToInt16(dgvCAutomarks.Rows(i).Cells(5).Value) &
            ", " & i & ")")
        Next
    End Sub

    Private Sub SaveNAutos(ByVal TestID As Integer)
        ExecuteSqlProcedure("Delete from N_Triggers where Test_ID = " & TestID)
        For i As Integer = 0 To dgvNAutomarks.RowCount - 1
            ExecuteSqlProcedure("Insert into N_Triggers (Test_ID, ValueFrom, ValueTo, Marked_ID, ReflexMulti, Ordinal, " &
            "LastEditedOn, EditedBy) values (" & TestID & ", " & dgvNAutomarks.Rows(i).Cells(2).Value & ", " &
            dgvNAutomarks.Rows(i).Cells(3).Value & ", " & dgvNAutomarks.Rows(i).Cells(4).Value & ", " &
            Convert.ToInt16(dgvNAutomarks.Rows(i).Cells(6).Value) & ", " & i & ", '" & Date.Now & "', " & ThisUser.ID & ")")
        Next
    End Sub

    Private Sub SaveNRanges(ByVal TestID As Integer)
        ExecuteSqlProcedure("Delete from N_Ranges where Test_ID = " & TestID)
        Dim sSQL As String = ""
        For i As Integer = 0 To dgvNRanges.RowCount - 1
            If dgvNRanges.Rows(i).Cells(5).Value Is DBNull.Value _
            OrElse dgvNRanges.Rows(i).Cells(5).Value Is Nothing Then
                If dgvNRanges.Rows(i).Cells(4).Value = "Low" Or
                dgvNRanges.Rows(i).Cells(4).Value = "High" Or
                dgvNRanges.Rows(i).Cells(4).Value = "Positive" Or
                dgvNRanges.Rows(i).Cells(4).Value = "Reactive" Or
                dgvNRanges.Rows(i).Cells(4).Value = "Detected" Then
                    dgvNRanges.Rows(i).Cells(5).Value = "Caution"
                ElseIf InStr(dgvNRanges.Rows(i).Cells(4).Value, "Panic") > 0 Then
                    dgvNRanges.Rows(i).Cells(5).Value = "Panic"
                Else
                    dgvNRanges.Rows(i).Cells(5).Value = "Ignore"
                End If
            End If
            If dgvNRanges.Rows(i).Cells(2).Value <> "" And
            dgvNRanges.Rows(i).Cells(3).Value <> "" And
            dgvNRanges.Rows(i).Cells(4).Value <> "" And
            dgvNRanges.Rows(i).Cells(5).Value <> "" Then
                sSQL = "Insert into N_Ranges (Test_ID, ValueFrom, ValueTo, Flag, Behavior, Ordinal, EditedBy, " &
                "LastEditedOn) values (" & TestID & ", " & dgvNRanges.Rows(i).Cells(2).Value & ", " &
                dgvNRanges.Rows(i).Cells(3).Value & ", '" & Trim(dgvNRanges.Rows(i).Cells(4).Value) & "', '" &
                Trim(dgvNRanges.Rows(i).Cells(5).Value) & "', " & i & ", " & ThisUser.ID & ", '" & Date.Now & "')"
                ExecuteSqlProcedure(sSQL)
            End If
        Next
    End Sub

    Private Sub btnRemoveAGR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveAGR.Click
        dgvAGRanges.Rows.Remove(dgvAGRanges.SelectedRows(0))
        btnRemoveAGR.Enabled = False
        If dgvAGRanges.RowCount = 0 Then btnRemAllAGR.Enabled = False
    End Sub

    Private Sub btnRemAllAGR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemAllAGR.Click
        dgvAGRanges.Rows.Clear()
        btnRemAllAGR.Enabled = False
        btnRemoveAGR.Enabled = False
    End Sub

    Private Sub btnAddAGR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddAGR.Click
        If txtTestID.Text <> "" And txtAGRAgeFrom.Text <> "" And txtAGRAgeTo.Text <> "" And cmbAGSex.SelectedIndex _
        <> -1 And txtAGVFrom.Text <> "" And txtAGVTo.Text <> "" And cmbAGBehavior.SelectedIndex <> -1 Then
            dgvAGRanges.Rows.Add(Val(txtTestID.Text), dgvAGRanges.RowCount + 1,
            txtAGRAgeFrom.Text, txtAGRAgeTo.Text, cmbAGSex.SelectedItem,
            txtAGVFrom.Text, txtAGVTo.Text, cmbAGFlag.Text, cmbAGBehavior.SelectedItem.ToString)
            txtAGRAgeFrom.Text = ""
            txtAGRAgeTo.Text = ""
            cmbAGSex.SelectedIndex = -1
            txtAGVFrom.Text = ""
            txtAGVTo.Text = ""
            cmbAGFlag.SelectedIndex = -1
            btnAddAGR.Enabled = False
            btnRemAllAGR.Enabled = True
            txtAGRAgeFrom.Focus()
        End If
    End Sub

    Private Sub txtAGRAgeFrom_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAGRAgeFrom.GotFocus
        txtAGRAgeFrom.BackColor = FCOLOR
    End Sub

    Private Sub txtAGRAgeFrom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAGRAgeFrom.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtAGRAgeFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAGRAgeFrom.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAGRAgeFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAGRAgeFrom.LostFocus
        txtAGRAgeFrom.BackColor = NFCOLOR
        If txtAGRAgeFrom.Text <> "" Then
            If Not (Val(txtAGRAgeFrom.Text) >= 0 And Val(txtAGRAgeFrom.Text) <= 130) Then
                MsgBox("Prolis supports the Age range from 0 - 130 only")
                txtAGRAgeFrom.Text = ""
                txtAGRAgeFrom.Focus()
            End If
            If txtAGRAgeTo.Text <> "" And cmbAGSex.SelectedIndex <> -1 And txtAGVFrom.Text <> "" _
            And txtAGVTo.Text <> "" Then btnAddAGR.Enabled = True
        End If
    End Sub

    Private Sub txtAGRAgeTo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAGRAgeTo.GotFocus
        txtAGRAgeTo.BackColor = FCOLOR
    End Sub

    Private Sub txtAGRAgeTo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAGRAgeTo.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtAGRAgeTo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAGRAgeTo.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAGRAgeTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAGRAgeTo.LostFocus
        txtAGRAgeTo.BackColor = NFCOLOR
        If txtAGRAgeTo.Text <> "" Then
            If Not (Val(txtAGRAgeTo.Text) >= 0 And Val(txtAGRAgeTo.Text) <= 130) Then
                MsgBox("Prolis supports the Age range from 0 - 130 only", MsgBoxStyle.Critical, "Prolis")
                txtAGRAgeTo.Text = ""
                txtAGRAgeTo.Focus()
            End If
            If txtAGRAgeFrom.Text <> "" And cmbAGSex.SelectedIndex <> -1 And txtAGVFrom.Text <> "" _
            And txtAGVTo.Text <> "" Then btnAddAGR.Enabled = True
            If txtAGRAgeFrom.Text <> "" And txtAGRAgeTo.Text <> "" Then
                If Val(txtAGRAgeFrom.Text) > Val(txtAGRAgeTo.Text) Then
                    MsgBox("Higher side of the range must be greater than the lower side.", MsgBoxStyle.Critical, "Prolis")
                    txtAGRAgeTo.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub cmbAGSex_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAGSex.GotFocus
        cmbAGSex.BackColor = FCOLOR
    End Sub

    Private Sub cmbAGSex_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbAGSex.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmbAGSex_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAGSex.LostFocus
        cmbAGSex.BackColor = NFCOLOR
    End Sub

    Private Sub cmbAGSex_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAGSex.SelectedIndexChanged
        If txtAGRAgeTo.Text <> "" And txtAGRAgeFrom.Text <> "" And txtAGVFrom.Text <> "" _
        And txtAGVTo.Text <> "" Then btnAddAGR.Enabled = True
    End Sub

    Private Sub txtAGVFrom_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAGVFrom.GotFocus
        txtAGVFrom.BackColor = FCOLOR
    End Sub

    Private Sub txtAGVFrom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAGVFrom.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtAGVFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAGVFrom.KeyPress
        NRanges(txtAGVFrom, e)
    End Sub

    Private Sub txtAGVFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAGVFrom.LostFocus
        txtAGVFrom.BackColor = NFCOLOR
        If txtAGVFrom.Text <> "" Then
            If Not IsNumeric(txtAGVFrom.Text) Then
                MsgBox("This field only accepts numeric characters like '0,1,2,3,4,5,6,7,8,9,. and -")
                txtAGVFrom.Text = ""
                txtAGVFrom.Focus()
            End If
            If txtAGRAgeTo.Text <> "" And cmbAGSex.SelectedIndex <> -1 And txtAGRAgeFrom.Text <> "" _
            And txtAGVTo.Text <> "" Then btnAddAGR.Enabled = True
        End If
    End Sub

    Private Sub txtAGVTo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAGVTo.GotFocus
        txtAGVTo.BackColor = FCOLOR
    End Sub

    Private Sub txtAGVTo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAGVTo.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtAGVTo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAGVTo.KeyPress
        Prices(txtAGVTo, e)
    End Sub

    Private Sub txtAGVTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAGVTo.LostFocus
        txtAGVTo.BackColor = NFCOLOR
        If txtAGVTo.Text <> "" Then
            If Not IsNumeric(txtAGVTo.Text) Then
                MsgBox("This field only accepts numeric characters like '0,1,2,3,4,5,6,7,8,9,. and -")
                txtAGVTo.Text = ""
                txtAGVTo.Focus()
            End If
            If txtAGRAgeTo.Text <> "" And cmbAGSex.SelectedIndex <> -1 And txtAGRAgeFrom.Text <> "" _
            And txtAGVFrom.Text <> "" And cmbAGBehavior.SelectedIndex <> -1 Then btnAddAGR.Enabled = True
        End If
    End Sub

    Private Sub cmbAGFlag_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAGFlag.GotFocus
        cmbAGFlag.BackColor = FCOLOR
    End Sub

    Private Sub cmbAGFlag_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbAGFlag.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmbAGFlag_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAGFlag.LostFocus
        cmbAGFlag.BackColor = NFCOLOR
    End Sub

    Private Sub cmbAGFlag_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAGFlag.TextChanged
        If txtAGRAgeTo.Text <> "" And cmbAGSex.SelectedIndex <> -1 And txtAGRAgeFrom.Text <> "" _
        And txtAGVTo.Text <> "" And txtAGVFrom.Text <> "" Then btnAddAGR.Enabled = True
    End Sub

    Private Sub cmbChoice_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbChoice.GotFocus
        cmbChoice.BackColor = FCOLOR
    End Sub

    Private Sub cmbChoice_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbChoice.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmbChoice_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbChoice.LostFocus
        cmbChoice.BackColor = NFCOLOR
    End Sub

    Private Sub cmbChoice_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbChoice.TextChanged
        If cmbChoice.Text <> "" And cmbCFlag.Text <> "" Then btnAddChoice.Enabled = True
    End Sub

    Private Sub btnAddChoice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddChoice.Click
        If txtTestID.Text <> "" And cmbCFlag.Text <> "" And cmbChoice.Text <> "" And cmbCBehavior.SelectedIndex <> -1 Then
            If IsChoiceUnique(Val(txtTestID.Text), cmbChoice.Text) Then
                dgvCRanges.Rows.Add(txtTestID.Text, dgvCRanges.RowCount + 1, cmbChoice.Text, cmbCFlag.Text, cmbCBehavior.SelectedItem.ToString)
                cmbChoice.Text = ""
                cmbChoice.SelectedIndex = -1
                cmbCFlag.Text = ""
                cmbCFlag.SelectedIndex = -1
                btnAddChoice.Enabled = False
                btnRemAllChoice.Enabled = True
                cmbChoice.Focus()
            Else
                MsgBox("This record has been added to the list already")
            End If
        End If
    End Sub

    Private Function IsChoiceUnique(ByVal TstID As Integer, ByVal Choice As String) As Boolean
        'On Error Resume Next
        Dim i As Integer
        Dim IsUniq As Boolean = True
        For i = 0 To dgvCRanges.RowCount - 1
            If dgvCRanges.Rows(i).Cells(2).Value.ToString = Choice Then
                IsUniq = False
                Exit For
            End If
        Next
        IsChoiceUnique = IsUniq
    End Function

    Private Sub dgvCRanges_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvCRanges.CellClick
        If dgvCRanges.RowCount > 0 Then
            If dgvCRanges.Rows(e.RowIndex).Index <> -1 Then btnRemChoice.Enabled = True
        End If
    End Sub

    Private Sub btnRemChoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemChoice.Click
        dgvCRanges.Rows.Remove(dgvCRanges.SelectedRows(0))
        btnRemChoice.Enabled = False
        If dgvCRanges.RowCount = 0 Then btnRemAllChoice.Enabled = False
    End Sub

    Private Sub btnRemAllChoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemAllChoice.Click
        dgvCRanges.Rows.Clear()
        btnRemAllChoice.Enabled = False
        btnRemChoice.Enabled = False
    End Sub

    Private Sub dgvAGRanges_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvAGRanges.CellClick
        If dgvAGRanges.Rows(e.RowIndex).Index <> -1 Then btnRemoveAGR.Enabled = True
    End Sub

    Private Function IsCAutoUnique(ByVal Test_ID As Integer, ByVal Choice As String, ByVal Marked_ID As Integer) As Boolean
        Dim i As Integer
        Dim IsUniq As Boolean = True
        For i = 0 To dgvCAutomarks.RowCount - 1
            If dgvCAutomarks.Rows(i).Cells(0).Value = Test_ID And dgvCAutomarks.Rows(i).Cells(2).Value = Choice _
            And dgvCAutomarks.Rows(i).Cells(3).Value = Marked_ID Then IsUniq = False
            Exit For
        Next
        IsCAutoUnique = IsUniq
    End Function

    Private Sub btnRemCAuto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemAllCAuto.Click
        dgvCAutomarks.Rows.Remove(dgvCAutomarks.SelectedRows(0))
        btnRemCAuto.Enabled = False
        If dgvCAutomarks.RowCount = 0 Then btnRemAllCAuto.Enabled = False
    End Sub

    Private Sub btnRemAllCAuto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemAllCAuto.Click
        dgvCAutomarks.Rows.Clear()
        btnRemCAuto.Enabled = False
        btnRemAllCAuto.Enabled = False
    End Sub

    Private Sub cmbCAutoChoice_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCAutoChoice.GotFocus
        cmbCAutoChoice.BackColor = FCOLOR
    End Sub

    Private Sub cmbCAutoChoice_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbCAutoChoice.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmbCAutoChoice_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCAutoChoice.LostFocus
        cmbCAutoChoice.BackColor = NFCOLOR
    End Sub

    Private Sub cmbCAutoChoice_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCAutoChoice.TextChanged
        If cmbCAutoChoice.Text <> "" And txtCTrigID.Text <> "" Then btnAddCAuto.Enabled = True
    End Sub

    Private Sub btnAddNAuto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNAuto.Click
        If txtTestID.Text <> "" And txtNRangeFrom.Text <> "" And txtNRangeTo.Text <> "" _
        And txtNTrigID.Text <> "" Then
            If IsNAutoUnique(Val(txtTestID.Text), Val(txtNRangeFrom.Text),
            Val(txtNRangeTo.Text), Val(txtNTrigID.Text)) Then
                dgvNAutomarks.Rows.Add(txtTestID.Text, dgvNAutomarks.RowCount + 1, txtNRangeFrom.Text,
                txtNRangeTo.Text, Val(txtNTrigID.Text), txtNTrigName.Text, chkNMultiReflex.Checked)
                txtNRangeFrom.Text = ""
                txtNRangeTo.Text = ""
                txtNTrigID.Text = ""
                txtNTrigName.Text = ""
                chkNMultiReflex.Checked = False
                btnAddNAuto.Enabled = False
                btnRemAllNAuto.Enabled = True
                txtNRangeFrom.Focus()
            Else
                MsgBox("This entry has already been made.")
            End If
        End If
    End Sub

    Private Sub frmTests_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CN.Open("DSN=ProlisQC;UID=sa;PWD=''")
        'PopulateCNMarked()
        PopulateMaterials()
        PopulateDepartments()
        cmbRptResSex.SelectedIndex = 0
        cmbRPTDescSex.SelectedIndex = 0
        cmbRptNoteSex.SelectedIndex = 0
        cmbDecimals.SelectedIndex = 5
        If SystemConfig.DiagTarget = "V" Then   'Veterinary
            chkAGRange.Enabled = False
            chkDeltaCheck.Enabled = False
            PopulateSpecies()
            cmbSBehavior.SelectedIndex = 1
            gbNRanges.Visible = False
            gbCRanges.Enabled = False
            gbSRanges.Visible = True
        Else    'Human
            cmbCBehavior.SelectedIndex = 1
            cmbNBehavior.SelectedIndex = 1
            cmbAGBehavior.SelectedIndex = 1
            chkAGRange.Enabled = True
            chkDeltaCheck.Enabled = True
            gbCRanges.Enabled = True
            gbNRanges.Visible = True
            gbSRanges.Visible = False
        End If
        dgvInfo.RowCount = 1
        'dgvInfoQS.Rows.Add(GetNextQID(), "", "Choice")
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)

        If Not String.IsNullOrEmpty(_testID) Then

            txtSearchID.Text = _testID
            txtSearchID_Validated(Nothing, Nothing)

            txtName.Focus()

        End If

        LoadQuestionCombo()

        Panel2Location = Panel2.Location  'save the location of Panel2 for further usage
        cbo_AOE_Q_Type.SelectedIndex = 0
    End Sub

    Private Sub PopulateSpecies()
        cmbSpecies.Items.Clear()
        Dim cnsp As New SqlConnection(connString)
        cnsp.Open()
        Dim cmdsp As New SqlCommand("Select * from Species order by Species", cnsp)
        cmdsp.CommandType = CommandType.Text
        Dim drsp As SqlDataReader = cmdsp.ExecuteReader
        If drsp.HasRows Then
            While drsp.Read
                cmbSpecies.Items.Add(New MyList(drsp("Species"), drsp("ID")))
            End While
        End If
        cnsp.Close()
        cnsp = Nothing
    End Sub

    Private Sub PopulateDepartments()
        cmbDept.Items.Clear()
        Dim cnpd As New SqlConnection(connString)
        cnpd.Open()
        Dim cmdpd As New SqlCommand("Select * from Departments order by Dept_Name", cnpd)
        cmdpd.CommandType = CommandType.Text
        Dim drpd As SqlDataReader = cmdpd.ExecuteReader
        If drpd.HasRows Then
            While drpd.Read
                cmbDept.Items.Add(New MyList(drpd("Dept_Name"), drpd("ID")))
            End While
        End If
        cnpd.Close()
        cnpd = Nothing
    End Sub

    Private Sub PopulateMaterials()
        lstMaterials.Items.Clear()
        Dim cnpm As New SqlConnection(connString)
        cnpm.Open()
        Dim cmdpm As New SqlCommand("Select * from Materials", cnpm)
        cmdpm.CommandType = CommandType.Text
        Dim drpm As SqlDataReader = cmdpm.ExecuteReader
        If drpm.HasRows Then
            While drpm.Read
                lstMaterials.Items.Add(New MyList(drpm("Name"), drpm("ID")))
            End While
        End If
        cnpm.Close()
        cnpm = Nothing
    End Sub

    Private Function IsNAutoUnique(ByVal TestID As Integer, ByVal ValueFrom As Single,
    ByVal ValueTo As Single, ByVal MarkedID As Integer) As Boolean
        'Try
        Dim i As Integer
        Dim IsUniq As Boolean = True
        For i = 0 To dgvNAutomarks.RowCount - 1
            If (dgvNAutomarks.Rows(i).Cells(0).Value IsNot Nothing _
            AndAlso dgvNAutomarks.Rows(i).Cells(0).Value.ToString = TestID.ToString) And
            (dgvNAutomarks.Rows(i).Cells(2).Value IsNot Nothing _
            AndAlso dgvNAutomarks.Rows(i).Cells(2).Value = ValueFrom) And
            (dgvNAutomarks.Rows(i).Cells(3).Value IsNot Nothing _
            AndAlso dgvNAutomarks.Rows(i).Cells(3).Value = ValueTo) And
            dgvNAutomarks.Rows(i).Cells(4).Value = MarkedID Then IsUniq = False
            Exit For
        Next
        'Catch Ex As Exception
        '    MsgBox(Ex.Message)
        '    IsNAutoUnique = False
        'End Try
        IsNAutoUnique = IsUniq
    End Function

    Private Sub btnRemAllNAuto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemAllNAuto.Click
        dgvNAutomarks.Rows.Clear()
        btnRemAllNAuto.Enabled = False
        btnRemNAuto.Enabled = False
    End Sub

    Private Sub btnRemNAuto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemNAuto.Click
        dgvNAutomarks.Rows.Remove(dgvNAutomarks.SelectedRows(0))
        btnRemNAuto.Enabled = False
        If dgvNAutomarks.RowCount = 0 Then btnRemAllNAuto.Enabled = False
    End Sub

    Private Sub btnFormula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFormula.Click
        frmFormulaBuilder.ShowDialog()
    End Sub

    Friend Sub DisplayTheTest(ByVal TestID As Integer)
        ClearForm()
        Dim i As Integer
        Dim m As Integer
        Dim MatIDs() As String
        Dim ItemX As MyList
        Dim cndtt As New SqlConnection(connString)
        cndtt.Open()
        Dim cmddtt As New SqlCommand("Select * from Tests where ID = " & TestID, cndtt)
        cmddtt.CommandType = CommandType.Text
        Dim drdtt As SqlDataReader = cmddtt.ExecuteReader
        If drdtt.HasRows Then
            While drdtt.Read
                txtSearchID.Text = drdtt("ID")
                txtTestID.Text = drdtt("ID")
                txtName.Text = drdtt("Name")
                txtAbbr.Text = Trim(drdtt("Abbr"))
                If drdtt("AlternateNames") IsNot DBNull.Value Then txtAltNames.Text = drdtt("AlternateNames")
                If drdtt("MedEval") Is DBNull.Value Then
                    If Trim(txtAltNames.Text) <> "" Then
                        chkEvaluate.Checked = True
                    Else
                        chkEvaluate.Checked = False
                    End If
                Else
                    chkEvaluate.Checked = drdtt("MedEval")
                End If
                If drdtt("Description") IsNot DBNull.Value Then txtDescription.Text = drdtt("Description")
                If drdtt("Loinc") IsNot DBNull.Value Then txtLoinc.Text = drdtt("Loinc")
                txtSlideID.Text = ""
                If drdtt("SlideID") IsNot DBNull.Value Then txtSlideID.Text = drdtt("SlideID")
                If drdtt("Isactive") IsNot DBNull.Value Then
                    ChkActive.Checked = drdtt("Isactive")
                Else
                    ChkActive.Checked = False
                End If
                If drdtt("HasResult") IsNot DBNull.Value Then
                    ChkResult.Checked = drdtt("HasResult")
                Else
                    ChkResult.Checked = True
                End If
                chkDeltaCheck.Checked = drdtt("DeltaCheck")
                If drdtt("IsMarkable") IsNot DBNull.Value Then
                    ChkMarkable.Checked = drdtt("IsMarkable")
                Else
                    ChkMarkable.Checked = True
                End If
                If drdtt("TBillable") IsNot DBNull.Value Then
                    chkTBillable.Checked = drdtt("TBillable")
                Else
                    chkTBillable.Checked = False
                End If
                If drdtt("CBillable") IsNot DBNull.Value Then
                    chkCBillable.Checked = drdtt("CBillable")
                Else
                    chkCBillable.Checked = True
                End If
                If drdtt("PBillable") IsNot DBNull.Value Then
                    chkPBillable.Checked = drdtt("PBillable")
                Else
                    chkPBillable.Checked = True
                End If
                chkForI.Checked = drdtt("ForI")
                chkNorC.Checked = drdtt("NormCut")
                If drdtt("AGRanges") IsNot DBNull.Value Then
                    chkAGRange.Checked = drdtt("AGRanges")
                Else
                    chkAGRange.Checked = False
                End If
                chkRptRange.Checked = drdtt("ReportableRange")
                If drdtt("Qualitative") IsNot DBNull.Value Then
                    chkResultType.Checked = drdtt("Qualitative")
                Else
                    chkResultType.Checked = False
                End If
                If drdtt("DecimalPlaces") IsNot DBNull.Value AndAlso
                drdtt("DecimalPlaces") >= 0 Then
                    cmbDecimals.SelectedIndex = drdtt("DecimalPlaces")
                Else
                    cmbDecimals.SelectedIndex = 2
                End If
                If drdtt("ResultNote_RTF") IsNot DBNull.Value AndAlso
                drdtt("ResultNote_RTF") <> "" Then
                    txtResultNote.Text = drdtt("ResultNote_RTF")
                    If txtResultNote.Text <> "" Then
                        btnNote.Image =
                    System.Drawing.Image.FromFile(Application.StartupPath _
                    & "\Images\Note.ico")
                        rsNotes = txtResultNote.Text
                    End If
                Else
                    txtResultNote.Text = ""
                    btnNote.Image =
                    System.Drawing.Image.FromFile(Application.StartupPath _
                    & "\Images\Noteblank.ico")
                End If
                If drdtt("IsCalculated") IsNot DBNull.Value Then
                    ChkCalculated.Checked = drdtt("IsCalculated")
                Else
                    ChkCalculated.Checked = False
                End If
                If drdtt("Formula") IsNot DBNull.Value Then txtFormula.Text = drdtt("Formula")
                If drdtt("Automarker") IsNot DBNull.Value Then
                    chkAutomarker.Checked = drdtt("Automarker")
                Else
                    chkAutomarker.Checked = False
                End If
                If drdtt("NormCut") IsNot DBNull.Value Then
                    chkNorC.Checked = drdtt("NormCut")
                Else
                    chkNorC.Checked = False
                End If
                For i = 0 To lstMaterials.Items.Count - 1
                    lstMaterials.SetItemChecked(i, False)
                Next
                If drdtt("IsCalculated") IsNot DBNull.Value _
                AndAlso drdtt("IsCalculated") = 0 Then
                    MatIDs = GetMaterialIDs(drdtt("ID"))
                    For m = 0 To MatIDs.Length - 1
                        For i = 0 To lstMaterials.Items.Count - 1
                            ItemX = lstMaterials.Items(i)
                            If CInt(MatIDs(m)) = ItemX.ItemData Then
                                lstMaterials.SetItemChecked(i, True)
                                Exit For
                            End If
                        Next
                    Next
                End If
                If drdtt("IsReportable") IsNot DBNull.Value Then
                    chkReportable.Checked = drdtt("IsReportable")
                Else
                    chkReportable.Checked = True
                End If
                If drdtt("Report_Result") IsNot DBNull.Value Then chkReportResult.Checked = drdtt("Report_Result")
                If drdtt("RptResSex") IsNot DBNull.Value Then cmbRptResSex.SelectedIndex = drdtt("RptResSex")
                If drdtt("Report_Description") IsNot DBNull.Value Then chkReportDescription.Checked = drdtt("Report_Description")
                If drdtt("RptDescSex") IsNot DBNull.Value Then cmbRPTDescSex.SelectedIndex = drdtt("RptDescSex")
                If drdtt("Report_Note") IsNot DBNull.Value Then chkReportNote.Checked = drdtt("Report_Note")
                If drdtt("RptNoteSex") IsNot DBNull.Value Then cmbRptNoteSex.SelectedIndex = drdtt("RptNoteSex")
                If drdtt("Report_Source") IsNot DBNull.Value Then chkReportSource.Checked = drdtt("Report_Source")
                If drdtt("Report_Tech") IsNot DBNull.Value Then chkReportTech.Checked = drdtt("Report_Tech")
                If drdtt("Report_info") IsNot DBNull.Value Then chkReportInfo.Checked = drdtt("Report_Info")
                If drdtt("Report_Dept") IsNot DBNull.Value Then chkReportDept.Checked = drdtt("Report_Dept")
                If drdtt("DotSuppress") IsNot DBNull.Value Then chkDotSuppress.Checked = drdtt("DotSuppress")
                If drdtt("Report_Var1") IsNot DBNull.Value Then chkReportVar1.Checked = drdtt("Report_Var1")
                If drdtt("ReportRange") IsNot DBNull.Value Then chkDispRange.Checked = drdtt("ReportRange")
                If drdtt("ReportFlag") IsNot DBNull.Value Then chkDispFlag.Checked = drdtt("ReportFlag")
                If drdtt("CPT_Code") IsNot DBNull.Value Then txtCPTCode.Text = drdtt("CPT_Code")
                If drdtt("CPT_MCR") IsNot DBNull.Value Then txtCPTMCR.Text = drdtt("CPT_MCR")
                If drdtt("CPT_MCD") IsNot DBNull.Value Then txtCPTMCD.Text = drdtt("CPT_MCD")
                If drdtt("CPT_SPC") IsNot DBNull.Value Then txtCPTSPC.Text = drdtt("CPT_SPC")
                If drdtt("Bill_Unit") IsNot DBNull.Value Then
                    txtBillUnit.Text = drdtt("Bill_Unit")
                Else
                    txtBillUnit.Text = "1.0"
                End If
                If drdtt("Mod1") IsNot DBNull.Value Then txtMod1.Text = Trim(drdtt("Mod1"))
                If drdtt("Mod2") IsNot DBNull.Value Then txtMod2.Text = Trim(drdtt("Mod2"))
                If drdtt("Mod3") IsNot DBNull.Value Then txtMod3.Text = Trim(drdtt("Mod3"))
                If drdtt("Mod4") IsNot DBNull.Value Then txtMod4.Text = Trim(drdtt("Mod4"))
                If drdtt("POS_Code") IsNot DBNull.Value Then txtPOS.Text = Trim(drdtt("POS_Code"))
                If drdtt("UOM") IsNot DBNull.Value Then txtUOM.Text = drdtt("UOM")
                For i = 0 To cmbDept.Items.Count - 1
                    ItemX = cmbDept.Items(i)
                    If ItemX.ItemData = drdtt("Department_ID") Then
                        cmbDept.SelectedIndex = i
                        Exit For
                    End If
                Next
                If drdtt("ListPrice") IsNot DBNull.Value Then txtListPrice.Text = drdtt("ListPrice")
                If drdtt("Price1") IsNot DBNull.Value Then txtPrice1.Text = drdtt("Price1")
                If drdtt("Price2") IsNot DBNull.Value Then txtPrice2.Text = drdtt("Price2")
                If drdtt("Price3") IsNot DBNull.Value Then txtPrice3.Text = drdtt("Price3")
                If drdtt("Price4") IsNot DBNull.Value Then txtPrice4.Text = drdtt("Price4")
                If drdtt("Price5") IsNot DBNull.Value Then txtPrice6.Text = drdtt("Price6")
                If drdtt("Price7") IsNot DBNull.Value Then txtPrice7.Text = drdtt("Price7")
                If drdtt("Price8") IsNot DBNull.Value Then txtPrice8.Text = drdtt("Price8")
                If drdtt("Price9") IsNot DBNull.Value Then txtPrice9.Text = drdtt("Price9")
                chkPreAnalytical.Checked = drdtt("PreAnalytical")
                cmbInfoFormat.SelectedIndex = drdtt("PreAnaFormat")
                chkClinicalRequired.Checked = drdtt("PreAnaRequired")
                chkInHouse.Checked = drdtt("InHouse")
                chkESig.Checked = drdtt("ESig")
                DisplayTestInfos(drdtt("ID"))
                '
                If ChkMarkable.Checked = True Then DisplayModifiers(Val(txtTestID.Text))
                If ChkResult.Checked = True Then
                    If chkResultType.Checked = True Then   'Choice
                        DisplayCRanges(Val(txtTestID.Text))
                        If chkAutomarker.Checked = True Then DisplayCAutos(Val(txtTestID.Text))
                    Else
                        If SystemConfig.DiagTarget = "V" Then
                            DisplaySRanges(Val(txtTestID.Text))
                        Else
                            'If chkAGRange.Checked = True Then
                            DisplayAGRanges(Val(txtTestID.Text))
                            'Else
                            DisplayNRanges(Val(txtTestID.Text))
                            'End If
                            DisplayReportableRange(Val(txtTestID.Text))
                            If chkAutomarker.Checked = True Then DisplayNAutos(Val(txtTestID.Text))
                            DisplayConditionalAutos(Val(txtTestID.Text))
                        End If
                    End If
                End If
                '
                OldAtribs = GetCurrentAttribs()
                '
                If chkTBillable.Checked = True Then
                    If TabControl3.SelectedTab.Name = "tpNecessity" Then
                        DisplayNecessity(TestID)
                    End If
                End If

                DisplayAOE(Val(txtTestID.Text))


            End While
        End If
        cndtt.Close()
        cndtt = Nothing
    End Sub

    Private Sub DisplayAOE(TGP_ID As Integer)
        dgv_AOE_Questions.Rows.Clear()

        Dim sSQL As String = $"select * from AOE_TGP_Questions a inner join AOE_Questions b  on a.Q_Id= b.Q_Id where TGP_Id = {TGP_ID}"
        Dim cnsr As New SqlConnection(connString)
        cnsr.Open()
        Dim cmdsr As New SqlCommand(sSQL, cnsr)
        Dim drsr As SqlDataReader = cmdsr.ExecuteReader
        If drsr.HasRows Then
            While drsr.Read
                dgv_AOE_Questions.Rows.Add(drsr("Q_Id"), drsr("Question"), drsr("Answers"), drsr("Required"), drsr("AnsType"), System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Delete.ico"))
            End While
        End If
        cnsr.Close()
        cnsr = Nothing
    End Sub

    Private Sub DisplaySRanges(ByVal TestID As Integer)
        dgvSRanges.Rows.Clear()
        Dim sSQL As String = "Select a.Species_ID, b.Species, a.ValueFrom, " &
        "a.ValueTo, a.Flag, a.Behavior from S_Ranges a inner join Species b " &
        "On a.Species_ID = b.ID where a.Test_ID = " & TestID & " order by Ordinal"
        Dim cnsr As New SqlConnection(connString)
        cnsr.Open()
        Dim cmdsr As New SqlCommand(sSQL, cnsr)
        Dim drsr As SqlDataReader = cmdsr.ExecuteReader
        If drsr.HasRows Then
            While drsr.Read
                dgvSRanges.Rows.Add(TestID, drsr("Species") & " (" & drsr("Species_ID") &
                ")", drsr("ValueFrom"), drsr("ValueTo"), drsr("Flag"), drsr("Behavior"))
            End While
            btnSRemAll.Enabled = True
        End If
        cnsr.Close()
        cnsr = Nothing
    End Sub

    Private Function GetCurrentAttribs() As String
        Dim Attribs As String = ""
        Dim i As Integer
        Dim ItemX As MyList
        Dim Tmps As String = ""
        Attribs = "Name=" & Trim(txtName.Text) & "|Abbr=" & Trim(txtAbbr.Text) & "|Materials="
        For i = 0 To lstMaterials.CheckedItems.Count - 1
            ItemX = lstMaterials.CheckedItems(i)
            Tmps += ItemX.ItemData.ToString & "^"
        Next
        If Tmps.Length > 1 And Microsoft.VisualBasic.Right(Tmps, 1) = "^" _
        Then Tmps = Microsoft.VisualBasic.Mid(Tmps, 1, Len(Tmps) - 1)
        Attribs += Tmps
        Tmps = ""
        Attribs += "|Active=" & ChkActive.Text & "|Markable=" & ChkMarkable.Text & "|CBillable=" &
        chkCBillable.Text & "|PBillable=" & chkPBillable.Text & "|TBillable=" & chkTBillable.Text &
        "|Result=" & ChkResult.Text & "|Reportable=" & chkReportable.Text & "|ForI=" & chkForI.Text _
        & "|NormCut=" & chkNorC.Text & "|MarkExt="
        If dgvModifiers.RowCount > 0 Then
            For i = 0 To dgvModifiers.RowCount - 1
                Tmps += dgvModifiers.Rows(i).Cells(0).Value.ToString & ":" &
                dgvModifiers.Rows(i).Cells(1).Value.ToString & ":" &
                dgvModifiers.Rows(i).Cells(2).Value.ToString & "^"
            Next
            If Tmps.Length > 1 And Microsoft.VisualBasic.Right(Tmps, 1) = "^" _
            Then Tmps = Microsoft.VisualBasic.Mid(Tmps, 1, Len(Tmps) - 1)
            Attribs += Tmps
            Tmps = ""
        End If
        If ChkResult.Text = "Yes" Then
            Attribs += "|ResultType=" & chkResultType.Text & "|UOM=" & Trim(txtUOM.Text) &
            "|Decimals=" & cmbDecimals.SelectedItem.ToString & "|Calculated=" & ChkCalculated.Text _
            & "|Formula=" & Trim(txtFormula.Text) & "|NRange=NS|AGRange=NS|CRange=NS"
        Else
            Attribs += "|||||||"
        End If
        If chkCBillable.Text = "Yes" Or chkTBillable.Text = "Yes" Or chkPBillable.Text = "Yes" Then
            Attribs += "|CPT=" & Trim(txtCPTCode.Text) & "|ListPrice=" & Trim(txtListPrice.Text) &
            "|Price1=" & Trim(txtPrice1.Text) & "|Price2=" & Trim(txtPrice2.Text) & "|Price3=" &
            Trim(txtPrice3.Text) & "|Price4=" & Trim(txtPrice4.Text) & "|Price5=" &
            Trim(txtPrice5.Text) & "|Price6=" & Trim(txtPrice6.Text) & "|Price7=" &
            Trim(txtPrice7.Text) & "|Price8=" & Trim(txtPrice8.Text) & "|Price9=" & Trim(txtPrice9.Text)
        Else
            Attribs += "|||||||||||"
        End If
        Return Attribs
    End Function

    Private Sub DisplayTestInfos(ByVal TGPID As Integer)
        dgvInfo.Rows.Clear()
        Dim cndti As New SqlConnection(connString)
        cndti.Open()
        Dim cmddti As New SqlCommand("Select a.*, b.Name from TGP_Info a inner join " &
        "Tests b on a.Info_ID = b.ID where a.TGP_ID = " & TGPID & " order by a.Ordinal", cndti)
        cmddti.CommandType = CommandType.Text
        Dim drdti As SqlDataReader = cmddti.ExecuteReader
        If drdti.HasRows Then
            While drdti.Read
                dgvInfo.Rows.Add(System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\Images\Eraser.ico"),
                drdti("Info_ID"), System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\Images\Looks.ico"),
                drdti("Name"), drdti("Required"), Nothing)
                Dim CCol As DataGridViewComboBoxColumn = CType(dgvInfo.Columns(5),
                DataGridViewComboBoxColumn)
                dgvInfo.Rows(dgvInfo.RowCount - 1).Cells(5).Value =
                CCol.Items(drdti("AppLevel"))
            End While
        End If
        cndti.Close()
        cndti = Nothing
        dgvInfo.Rows.Add()
    End Sub

    Private Sub DisplayReportableRange(ByVal TestID As Integer)
        Dim cndrr As New SqlConnection(connString)
        cndrr.Open()
        Dim cmddrr As New SqlCommand("Select * from Reportable_Ranges where Test_ID = " & TestID, cndrr)
        cmddrr.CommandType = CommandType.Text
        Dim drdrr As SqlDataReader = cmddrr.ExecuteReader
        If drdrr.HasRows Then
            While drdrr.Read
                txtRptFrom.Text = drdrr("ValueFrom").ToString
                txtRptTo.Text = drdrr("ValueTo").ToString
            End While
        End If
        cndrr.Close()
        cndrr = Nothing
    End Sub

    Private Function GetMaterialIDs(ByVal TestID As Integer) As String()
        Dim MatIDs(0) As String
        Dim cnmis As New SqlConnection(connString)
        cnmis.Open()
        Dim cmdmis As New SqlCommand("Select Material_ID from Test_Material where Test_ID = " & TestID, cnmis)
        cmdmis.CommandType = CommandType.Text
        Dim drmis As SqlDataReader = cmdmis.ExecuteReader
        If drmis.HasRows Then
            While drmis.Read
                If MatIDs(UBound(MatIDs)) <> "" Then ReDim Preserve MatIDs(UBound(MatIDs) + 1)
                MatIDs(UBound(MatIDs)) = drmis("Material_ID")
            End While
        End If
        cnmis.Close()
        cnmis = Nothing
        Return MatIDs
    End Function

    Private Sub DisplayNecessity(ByVal TestID As String)
        dgvNecessity.Rows.Clear()
        If TestID <> "" Then
            Dim sSQL As String = "Select a.Dx_Code, b.Description from MedicalNecessity a " &
            "inner join DiagCodes b on a.Dx_Code = b.Code where a.CPT_Code in (Select " &
            "CPT_Code from Tests where ID = " & TestID & ")"
            Dim cndn As New SqlConnection(connString)
            cndn.Open()
            Dim cmddn As New SqlCommand(sSQL, cndn)
            Dim drdn As SqlDataReader = cmddn.ExecuteReader
            If drdn.HasRows Then
                While drdn.Read
                    dgvNecessity.Rows.Add(dgvNecessity.RowCount + 1, TestID,
                    drdn("Dx_Code"), drdn("Description"))
                End While
                btnRemAllNec.Enabled = True
            End If
            cndn.Close()
            cndn = Nothing
        End If
    End Sub

    Private Function CodeName(ByVal Code As String) As String
        Dim CdName As String = ""
        Dim sSQL As String = "Select Description from DiagCodes where Code = '" & Code & "'"
        Dim cncn As New SqlConnection(connString)
        cncn.Open()
        Dim cmdcn As New SqlCommand(sSQL, cncn)
        Dim drcn As SqlDataReader = cmdcn.ExecuteReader
        If drcn.HasRows Then
            While drcn.Read
                If drcn("Description") IsNot DBNull.Value _
                AndAlso Trim(drcn("Description")) <> "" Then _
                CdName = Replace(drcn("Description"), Chr(34), "")
            End While
        End If
        cncn.Close()
        cncn = Nothing
        Return CdName
    End Function

    Private Sub DisplayNAutos(ByVal TestID As Integer)
        Dim sSQL As String = "Select * from N_Triggers where Test_ID = " & TestID
        Dim cndna As New SqlConnection(connString)
        cndna.Open()
        Dim cmddna As New SqlCommand(sSQL, cndna)
        Dim drdna As SqlDataReader = cmddna.ExecuteReader
        If drdna.HasRows Then
            While drdna.Read
                dgvNAutomarks.Rows.Add(Test_ID, dgvNAutomarks.RowCount + 1,
                drdna("ValueFrom"), drdna("ValueTo"),
                drdna("Marked_ID"), GetTGPName(drdna("Marked_ID")) _
                & " (" & drdna("Marked_ID").ToString & ")", drdna("ReflexMulti"))
            End While
        End If
        cndna.Close()
        cndna = Nothing
    End Sub

    Private Sub DisplayNRanges(ByVal TestID As Integer)
        dgvNRanges.Rows.Clear()
        Dim MaxDec As String = ""
        Dim DP As Integer = GetMaxDecimals(TestID)
        If DP = 1 Then
            MaxDec = "0.0"
        ElseIf DP = 2 Then
            MaxDec = "0.00"
        ElseIf DP = 3 Then
            MaxDec = "0.000"
        ElseIf DP = 4 Then
            MaxDec = "0.0000"
        Else
            MaxDec = "0"
        End If
        Dim sSQL As String = "Select * from N_Ranges where Test_ID = " & TestID
        Dim cndnr As New SqlConnection(connString)
        cndnr.Open()
        Dim cmddnr As New SqlCommand(sSQL, cndnr)
        Dim drdnr As SqlDataReader = cmddnr.ExecuteReader
        If drdnr.HasRows Then
            While drdnr.Read
                dgvNRanges.Rows.Add(Test_ID, dgvNRanges.RowCount + 1,
                Format(drdnr("ValueFrom"), MaxDec),
                Format(drdnr("ValueTo"), MaxDec),
                drdnr("Flag"), drdnr("Behavior"))
            End While
            btnRemAllNR.Enabled = True
        End If
        cndnr.Close()
        cndnr = Nothing
    End Sub

    Private Sub DisplayAGRanges(ByVal TestID)
        dgvAGRanges.Rows.Clear()
        Dim MaxDec As String = ""
        Dim DP As Integer = GetMaxDecimals(TestID)
        If DP = 1 Then
            MaxDec = "0.0"
        ElseIf DP = 2 Then
            MaxDec = "0.00"
        ElseIf DP = 3 Then
            MaxDec = "0.000"
        ElseIf DP = 4 Then
            MaxDec = "0.0000"
        Else
            MaxDec = "0"
        End If
        Dim sSQL As String = "Select * from AG_Ranges where Test_ID = " & TestID
        Dim cnagr As New SqlConnection(connString)
        cnagr.Open()
        Dim cmdagr As New SqlCommand(sSQL, cnagr)
        Dim dragr As SqlDataReader = cmdagr.ExecuteReader
        If dragr.HasRows Then
            While dragr.Read
                dgvAGRanges.Rows.Add(Test_ID, dgvAGRanges.RowCount + 1,
                dragr("AgeFrom"), dragr("AgeTo"),
                ToGender(dragr("Sex")), Format(dragr("ValueFrom"), MaxDec),
                Format(dragr("ValueTo"), MaxDec), dragr("Flag"),
                IIf(dragr("Behavior") Is DBNull.Value, "Ignore", dragr("Behavior")))
            End While
            btnRemAllAGR.Enabled = True
        Else
            chkAGRange.Checked = False
        End If
        cnagr.Close()
        cnagr = Nothing
    End Sub

    Private Sub DisplayConditionalAutos(ByVal TestID As Integer)
        dgvCondAutomarks.Rows.Clear()
        Dim sSQL As String = "Select * from Conditional_Triggers where Test_ID = " & TestID
        Dim cndca As New SqlConnection(connString)
        cndca.Open()
        Dim cmddca As New SqlCommand(sSQL, cndca)
        cmddca.CommandType = CommandType.Text
        Dim drdca As SqlDataReader = cmddca.ExecuteReader
        If drdca.HasRows Then
            While drdca.Read
                dgvCondAutomarks.Rows.Add(TestID, dgvCAutomarks.RowCount + 1,
                drdca("Condition"), drdca("Marked_ID"),
                GetTGPName(drdca("Marked_ID")) & " (" &
                drdca("Marked_ID").ToString & ")")
            End While
        End If
        cndca.Close()
        cndca = Nothing
    End Sub

    Private Sub DisplayCAutos(ByVal TestID As Integer)
        dgvCAutomarks.Rows.Clear()
        Dim sSQL As String = "Select * from C_Triggers where Test_ID = " & TestID
        Dim cncas As New SqlConnection(connString)
        cncas.Open()
        Dim cmdcas As New SqlCommand(sSQL, cncas)
        cmdcas.CommandType = CommandType.Text
        Dim drcas As SqlDataReader = cmdcas.ExecuteReader
        If drcas.HasRows Then
            While drcas.Read
                dgvCAutomarks.Rows.Add(TestID, dgvCAutomarks.RowCount + 1,
                 drcas("Choice"), drcas("Marked_ID"),
                 GetTGPName(drcas("Marked_ID")) & " (" &
                 drcas("Marked_ID").ToString & ")", drcas("ReflexMulti"))
            End While
        End If
        cncas.Close()
        cncas = Nothing
    End Sub

    Private Sub DisplayCRanges(ByVal TestID As Integer)
        dgvCRanges.Rows.Clear()
        Dim sSQL As String = "Select * from C_Ranges where Test_ID = " & TestID
        Dim cncrs As New SqlConnection(connString)
        cncrs.Open()
        Dim cmdcrs As New SqlCommand(sSQL, cncrs)
        cmdcrs.CommandType = CommandType.Text
        Dim drcrs As SqlDataReader = cmdcrs.ExecuteReader
        If drcrs.HasRows Then
            While drcrs.Read
                dgvCRanges.Rows.Add(Test_ID, dgvCRanges.RowCount + 1, drcrs("Choice"), drcrs("Flag"), drcrs("Behavior"))
            End While
        End If
        cncrs.Close()
        cncrs = Nothing
    End Sub

    Private Sub DisplayModifiers(ByVal TestID As Integer)
        dgvModifiers.Rows.Clear()
        Dim sSQL As String = "Select * from MarkingModifiers where Test_ID = " & TestID
        Dim cndm As New SqlConnection(connString)
        cndm.Open()
        Dim cmddm As New SqlCommand(sSQL, cndm)
        cmddm.CommandType = CommandType.Text
        Dim drdm As SqlDataReader = cmddm.ExecuteReader
        If drdm.HasRows Then
            While drdm.Read
                dgvModifiers.Rows.Add(dgvModifiers.RowCount + 1, Test_ID, ToGender(drdm("Sex")), drdm("AgeFrom"), drdm("AgeTo"))
            End While
        End If
        cndm.Close()
        cndm = Nothing
    End Sub

    Private Function ToGender(ByVal Sex As String) As String
        If Sex = "M" Then
            ToGender = "M - Male"
        ElseIf Sex = "F" Then
            ToGender = "F - Female"
        ElseIf Sex = "I" Then
            ToGender = "I - Inbetween"
        Else
            ToGender = "U - Unreported"
        End If
    End Function

    Private Sub txtName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.GotFocus
        txtName.BackColor = FCOLOR
    End Sub

    Private Sub txtName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.LostFocus
        txtName.BackColor = NFCOLOR
        Update_Progress()
    End Sub

    Private Sub txtAbbr_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAbbr.GotFocus
        txtAbbr.BackColor = FCOLOR
    End Sub

    Private Sub txtAbbr_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAbbr.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtAbbr_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAbbr.LostFocus
        txtAbbr.BackColor = NFCOLOR
        Update_Progress()
    End Sub

    Private Sub txtCPTID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Numerals(sender, e)
    End Sub

    Private Sub txtListPrice_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtListPrice.GotFocus
        txtListPrice.BackColor = FCOLOR
    End Sub

    Private Sub txtListPrice_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtListPrice.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub
    Private Sub txtListPrice_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtListPrice.KeyPress
        Prices(txtListPrice, e)
    End Sub

    Private Sub txtPrice1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice1.GotFocus
        txtPrice1.BackColor = FCOLOR
    End Sub

    Private Sub txtPrice1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPrice1.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub
    Private Sub txtPrice1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrice1.KeyPress
        Prices(txtPrice1, e)
    End Sub

    Private Sub txtPrice2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice2.GotFocus
        txtPrice2.BackColor = FCOLOR
    End Sub

    Private Sub txtPrice2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPrice2.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtPrice2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrice2.KeyPress
        Prices(txtPrice2, e)
    End Sub

    Private Sub txtPrice3_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice3.GotFocus
        txtPrice3.BackColor = FCOLOR
    End Sub

    Private Sub txtPrice3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPrice3.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtPrice3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrice3.KeyPress
        Prices(txtPrice3, e)
    End Sub

    Private Sub txtPrice4_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice4.GotFocus
        txtPrice4.BackColor = FCOLOR
    End Sub

    Private Sub txtPrice4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPrice4.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtPrice4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrice4.KeyPress
        Prices(txtPrice4, e)
    End Sub

    Private Sub txtPrice5_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice5.GotFocus
        txtPrice5.BackColor = FCOLOR
    End Sub

    Private Sub txtPrice5_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPrice5.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtPrice5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrice5.KeyPress
        Prices(txtPrice5, e)
    End Sub

    Private Sub txtPrice6_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice6.GotFocus
        txtPrice6.BackColor = FCOLOR
    End Sub

    Private Sub txtPrice6_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPrice6.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtPrice6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrice6.KeyPress
        Prices(txtPrice6, e)
    End Sub

    Private Sub txtPrice7_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice7.GotFocus
        txtPrice7.BackColor = FCOLOR
    End Sub

    Private Sub txtPrice7_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPrice7.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtPrice7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrice7.KeyPress
        Prices(txtPrice7, e)
    End Sub

    Private Sub txtPrice8_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice8.GotFocus
        txtPrice8.BackColor = FCOLOR
    End Sub

    Private Sub txtPrice8_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPrice8.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtPrice8_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrice8.KeyPress
        Prices(txtPrice8, e)
    End Sub

    Private Sub txtPrice9_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice9.GotFocus
        txtPrice9.BackColor = FCOLOR
    End Sub

    Private Sub txtPrice9_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPrice9.KeyDown
        If e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtPrice9_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrice9.KeyPress
        Prices(txtPrice9, e)
    End Sub

    Private Sub txtICD9Code_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtICD9Code.GotFocus
        txtICD9Code.BackColor = FCOLOR
    End Sub

    Private Sub txtICD9Code_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtICD9Code.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtICD9Code_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtICD9Code.LostFocus
        txtICD9Code.BackColor = NFCOLOR
        If txtICD9Code.Text <> "" Then
            Dim cndx As New SqlConnection(connString)
            cndx.Open()
            Dim cmddx As New SqlCommand("Select * from DiagCodes where Code = '" & Trim(txtICD9Code.Text) & "'", cndx)
            cmddx.CommandType = CommandType.Text
            Dim drdx As SqlDataReader = cmddx.ExecuteReader
            If drdx.HasRows Then
                While drdx.Read
                    txtICD9Description.Text = drdx("Description")
                End While
            Else
                MsgBox("The code does not exist in the master file.", MsgBoxStyle.Critical, "Prolis")
                txtICD9Code.Text = ""
                txtICD9Description.Text = ""
            End If
            cndx.Close()
            cndx = Nothing
        Else
            txtICD9Description.Text = ""
        End If
    End Sub

    Private Sub txtICD9Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtICD9Code.TextChanged
        If txtICD9Code.Text <> "" Then
            btnAddNec.Enabled = True
        Else
            btnAddNec.Enabled = False
        End If
    End Sub

    Private Sub btnRemNec_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemNec.Click
        If dgvNecessity.SelectedRows.Count > 0 Then
            Dim RetVal As Integer = MsgBox("Are you sure you want to delete the selected " &
            "Code from Necessity?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
            If RetVal = vbYes Then
                ExecuteSqlProcedure("Delete from MedicalNecessity where CPT_Code = '" & Trim(txtCPTCode.Text) &
                " and Dx_Code = '" & Trim(dgvNecessity.SelectedRows(0).Cells(2).Value.ToString) & "'")
                dgvNecessity.Rows.Remove(dgvNecessity.SelectedRows(0))
                btnRemNec.Enabled = False
                If dgvNecessity.RowCount = 0 Then btnRemAllNec.Enabled = False
            End If
            dgvNecessity.SelectedRows(0).Selected = False
        End If
    End Sub

    Private Sub btnRemAllNec_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemAllNec.Click
        If dgvNecessity.SelectedRows.Count > 0 Then
            Dim RetVal As Integer = MsgBox("Are you sure you want to delete the entire necessity " &
            "of the displayed Group component?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
            If RetVal = vbYes Then
                ExecuteSqlProcedure("Delete from MedicalNecessity where CPT_Code = '" & Trim(txtCPTCode.Text) & "'")
                dgvNecessity.Rows.Clear()
                btnRemAllNec.Enabled = False
                btnRemNec.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnAddNec_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNec.Click
        If Trim(txtCPTCode.Text) <> "" And Trim(txtICD9Code.Text) <> "" And txtICD9Description.Text <> "" Then
            If NecessityUnique(txtTestID.Text, txtICD9Code.Text) Then
                dgvNecessity.Rows.Add(dgvNecessity.RowCount, txtTestID.Text, txtICD9Code.Text, txtICD9Description.Text)
                SaveNecessity(Trim(txtCPTCode.Text), Trim(txtICD9Code.Text))
                txtICD9Code.Text = ""
                txtICD9Description.Text = ""
                btnAddNec.Enabled = False
                btnRemAllNec.Enabled = True
                txtICD9Code.Focus()
            Else
                MsgBox("Record is already in the list")
                txtICD9Code.Text = ""
                txtICD9Code.Focus()
            End If
            If dgvNecessity.RowCount > 0 Then _
            dgvNecessity.FirstDisplayedScrollingRowIndex = dgvNecessity.RowCount - 1
        Else
            MsgBox("The code does not exist in the master file.", MsgBoxStyle.Critical, "Prolis")
            txtICD9Code.Text = ""
        End If
    End Sub

    Private Function NecessityUnique(ByVal Test_ID As String, ByVal ICD9 As String) As Boolean
        Dim Uniq As Boolean = True
        Dim i As Integer
        For i = 0 To dgvNecessity.RowCount - 1
            If dgvNecessity.Rows(i).Cells(1).Value.ToString = Test_ID.ToString And Trim(dgvNecessity.Rows(i).Cells(2).Value) = ICD9 Then
                Uniq = False
                Exit For
            End If
        Next
        NecessityUnique = Uniq
    End Function

    Private Function PartOfGrouprofiles(ByVal TestID As Integer) As Boolean
        Dim IsPart As Boolean = False
        Dim cnpgp As New SqlConnection(connString)
        cnpgp.Open()
        Dim cmdpgp As New SqlCommand("Select Group_ID as Component from Group_Test where Test_ID = " &
        TestID & " Union Select Profile_ID as component from Prof_GrpTst where GrpTst_ID = " & TestID, cnpgp)
        cmdpgp.CommandType = CommandType.Text
        Dim drpgp As SqlDataReader = cmdpgp.ExecuteReader
        If drpgp.HasRows Then
            While drpgp.Read
                If drpgp("Component") IsNot DBNull.Value Then IsPart = True
            End While
        End If
        cnpgp.Close()
        cnpgp = Nothing
        Return IsPart
    End Function

    Private Function MarkedOnPats(ByVal TestID As Integer) As Boolean
        Dim Marked As Boolean
        Dim cnmop As New SqlConnection(connString)
        cnmop.Open()
        Dim cmdmop As New SqlCommand("Select Accession_ID from Req_TGP where TGP_ID = " & TestID, cnmop)
        cmdmop.CommandType = CommandType.Text
        Dim drmop As SqlDataReader = cmdmop.ExecuteReader
        If drmop.HasRows Then
            Marked = True
        Else
            Marked = False
        End If
        cnmop.Close()
        cnmop = Nothing
        Return Marked
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If txtSearchID.Text = txtTestID.Text Then
            If txtTestID.Text <> "" And txtName.Text <> "" And txtAbbr.Text <> "" Then
                If Not (PartOfGrouprofiles(Val(txtTestID.Text)) = True Or MarkedOnPats(Val(txtTestID.Text)) = True) Then
                    If ThisUser.Hard_Deletion = True Then
                        Dim Retval As Integer
                        Retval = MsgBox("It is recommended to make the Analyte Inactive instead of deleting " _
                        & "it from the system entirely. Are you firm to delete this test anyway?", MsgBoxStyle.Critical + MsgBoxStyle.YesNo)
                        If Retval = vbYes Then
                            Using cnx As New SqlConnection(connString)
                                cnx.Open()
                                Using transaction As SqlTransaction = cnx.BeginTransaction()
                                    Try

                                        ExecuteSqlProcedureWithTransaction("Delete from MarkingModifiers where Test_ID = " & Val(txtTestID.Text), cnx, transaction)
                                        ExecuteSqlProcedureWithTransaction("Delete from C_Triggers where Test_ID = " & Val(txtTestID.Text), cnx, transaction)
                                        ExecuteSqlProcedureWithTransaction("Delete from N_Ranges where Test_ID = " & Val(txtTestID.Text), cnx, transaction)
                                        ExecuteSqlProcedureWithTransaction("Delete from C_Ranges where Test_ID = " & Val(txtTestID.Text), cnx, transaction)
                                        ExecuteSqlProcedureWithTransaction("Delete from AG_Ranges where Test_ID = " & Val(txtTestID.Text), cnx, transaction)
                                        ExecuteSqlProcedureWithTransaction("Delete from N_Triggers where Test_ID = " & Val(txtTestID.Text), cnx, transaction)
                                        ExecuteSqlProcedureWithTransaction("Delete from Necessity where TGP_ID = " & Val(txtTestID.Text), cnx, transaction)
                                        ExecuteSqlProcedureWithTransaction("Delete from Department_Test where Test_ID = " & Val(txtTestID.Text), cnx, transaction)
                                        ExecuteSqlProcedureWithTransaction("Delete from Tests where ID = " & Val(txtTestID.Text), cnx, transaction)
                                        transaction.Commit()
                                        If SystemConfig.AuditTrail = True Then
                                            LogUserEvent(ThisUser.ID, 603, Date.Now.ToString, "Analyte", CInt(txtTestID.Text), txtTestID.Text, "")
                                        End If
                                        ClearForm()
                                        SearchMode = True

                                    Catch ex As Exception
                                        transaction.Rollback()
                                        MsgBox("An error occurred while deleting the test: " & ex.Message, MsgBoxStyle.Critical)
                                    End Try
                                End Using
                            End Using
                        End If
                    Else
                        MsgBox("You don't have permission to delete any component of the system", MsgBoxStyle.Critical)
                    End If
                Else
                    MsgBox("The Analyte you want to delete, is either a part of Group(s) and/or " _
                    & "Profile(s) or it has been used in processing the requisition(s). System " _
                    & "does not allow the deletion of such component. You may mark it as " _
                    & "'Inactive'", MsgBoxStyle.Critical)
                End If
            End If
        Else
            MsgBox("The Test ID is different than that of the displayed record. Correct and try again.", MsgBoxStyle.Critical, "Prolis")
            txtSearchID.Text = txtTestID.Text
            txtSearchID.Focus()
        End If
    End Sub

    Private Sub btnTestLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestLook.Click
        Dim TestID As String = frmTestLookup.ShowDialog()
        If TestID <> "" Then
            DisplayTheTest(Val(TestID))
            Update_Progress()
            TestID = ""
            btnReplicate.Enabled = True
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub txtNRangeFrom_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNRangeFrom.GotFocus
        txtNRangeFrom.BackColor = FCOLOR
    End Sub

    Private Sub txtNRangeFrom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNRangeFrom.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtNRangeFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNRangeFrom.KeyPress
        NRanges(txtNRangeFrom, e)
    End Sub

    Private Sub txtNRangeTo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNRangeTo.GotFocus
        txtNRangeTo.BackColor = FCOLOR
    End Sub

    Private Sub txtNRangeTo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNRangeTo.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtNRangeTo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNRangeTo.KeyPress
        Prices(txtNRangeTo, e)
    End Sub

    Private Sub txtNRangeTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNRangeTo.LostFocus
        txtNRangeTo.BackColor = NFCOLOR
    End Sub

    Private Sub txtNRangeTo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNRangeTo.Validated
        If txtNRangeFrom.Text <> "" And txtNRangeTo.Text <> "" _
        And txtNTrigID.Text <> "" Then btnAddNAuto.Enabled = True
    End Sub

    Private Sub txtNRangeFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNRangeFrom.LostFocus
        txtNRangeFrom.BackColor = NFCOLOR
    End Sub

    Private Sub txtNRangeFrom_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNRangeFrom.Validated
        If txtNRangeFrom.Text <> "" And txtNRangeTo.Text <> "" _
        And txtNTrigID.Text <> "" Then btnAddNAuto.Enabled = True
    End Sub

    Private Sub cmbNRFlag_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbNRFlag.SelectedIndexChanged
        If txtNRFrom.Text <> "" And txtNRTo.Text <> "" And cmbNRFlag.SelectedIndex <> -1 Then btnAddNR.Enabled = True
    End Sub

    Private Sub txtNRFrom_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNRFrom.Validated
        If txtNRFrom.Text <> "" And txtNRTo.Text <> "" And cmbNRFlag.SelectedIndex <> -1 Then btnAddNR.Enabled = True
    End Sub

    Private Sub txtNRTo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNRTo.Validated
        If txtNRFrom.Text <> "" And txtNRTo.Text <> "" And cmbNRFlag.SelectedIndex <> -1 Then btnAddNR.Enabled = True
    End Sub

    Private Sub cmbCFlag_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCFlag.GotFocus
        cmbCFlag.BackColor = FCOLOR
    End Sub

    Private Sub cmbCFlag_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbCFlag.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Left Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub cmbCFlag_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCFlag.LostFocus
        cmbCFlag.BackColor = NFCOLOR
    End Sub

    Private Sub cmbCFlag_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCFlag.TextChanged
        If cmbChoice.Text <> "" And cmbCFlag.Text <> "" Then btnAddChoice.Enabled = True
    End Sub

    Private Sub chkReportable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkReportable.CheckedChanged
        If chkReportable.Checked = False Then
            chkReportable.Text = "NO"
            chkReportResult.Checked = False
            chkReportDescription.Checked = False
            chkReportNote.Checked = False
            chkReportSource.Checked = False
            chkReportTech.Checked = False
            chkReportInfo.Checked = False
            chkReportDept.Checked = False
            chkDotSuppress.Checked = False
            chkReportVar1.Checked = False
            GBReport.Enabled = False
        Else
            chkReportable.Text = "YES"
            chkReportResult.Checked = True
            chkReportDescription.Checked = False
            chkReportNote.Checked = True
            chkReportSource.Checked = False
            chkReportTech.Checked = False
            chkReportInfo.Checked = True
            chkReportDept.Checked = False
            chkDotSuppress.Checked = False
            chkReportVar1.Checked = False
            GBReport.Enabled = True
        End If
    End Sub

    Private Sub btnSource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSource.Click
        frmSources.ShowDialog()
        PopulateMaterials()
    End Sub

    Private Sub dgvNecessity_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvNecessity.CellClick
        If e.RowIndex <> -1 Then btnRemNec.Enabled = True
    End Sub

    Private Sub dgvCAutomarks_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvCAutomarks.CellClick
        btnRemCAuto.Enabled = True
    End Sub

    Private Sub dgvNAutomarks_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvNAutomarks.CellClick
        btnRemNAuto.Enabled = True
    End Sub

    Private Sub btnICD9Lookup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnICD9Lookup.Click
        Dim Dx As String = frmDiagnosis.ShowDialog
        If Dx <> "" Then
            txtICD9Code.Text = Dx
        End If
    End Sub

    Private Sub btnCRefCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCRefCopy.Click
        Dim CMDSTR As String = frmCRefluxCopy.ShowDialog
        If Not CMDSTR Is Nothing AndAlso CMDSTR <> "" Then
            If InStr(CMDSTR, "|") > 0 Then
                dgvCAutomarks.Rows.Clear()
                Dim CMD() As String = CMDSTR.Split("|")
                Dim File As String = CMD(0)
                Dim Delim As String
                Dim Data As String
                Dim Fields() As String
                Dim FLD As Integer = Val(CMD(2))
                Dim TRG As Integer = Val(CMD(3))
                If CMD(1) = "0" Then
                    Delim = ","
                ElseIf CMD(1) = "1" Then
                    Delim = Chr(9)
                ElseIf CMD(1) = "2" Then
                    Delim = "|"
                Else
                    Delim = vbCrLf
                End If
                Dim SR As New System.IO.StreamReader(File)
                Do Until SR.Peek = -1
                    Data = SR.ReadLine
                    If Data.Length > 0 Then
                        Fields = Data.Split(Delim)
                        If IsCAutoUnique(Val(txtTestID.Text), Fields(FLD), TRG) Then
                            dgvCAutomarks.Rows.Add(txtTestID.Text,
                            dgvCAutomarks.RowCount + 1,
                            Fields(FLD), TRG, GetTGPName(TRG))
                        End If
                    End If
                Loop
                SR.Close()
                SR = Nothing
            Else
                dgvCAutomarks.Rows.Clear()
                Dim cnrc As New SqlConnection(connString)
                cnrc.Open()
                Dim cmdrc As New SqlCommand("Select * from C_Triggers where Test_ID = " & Val(CMDSTR), cnrc)
                cmdrc.CommandType = CommandType.Text
                Dim drrc As SqlDataReader = cmdrc.ExecuteReader
                If drrc.HasRows Then
                    While drrc.Read
                        dgvCAutomarks.Rows.Add(txtTestID.Text, dgvCAutomarks.RowCount + 1,
                            Trim(drrc("Choice")), drrc("Marked_ID"), GetTGPName(drrc("Marked_ID")))
                    End While
                End If
                cnrc.Close()
                cnrc = Nothing
            End If
        End If
        If dgvCAutomarks.RowCount > 0 Then btnRemAllCAuto.Enabled = True
    End Sub

    Private Sub btnCRCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCRCopy.Click
        Dim CMDSTR As String = frmCRCopy.ShowDialog
        If Not CMDSTR Is Nothing AndAlso CMDSTR <> "" Then
            If InStr(CMDSTR, "|") > 0 Then
                dgvCRanges.Rows.Clear()
                Dim CMD() As String = CMDSTR.Split("|")
                Dim File As String = CMD(0)
                Dim Delim As String
                Dim i As Integer
                Dim Data As String
                Dim Fields() As String
                Dim FLD As Integer = Val(CMD(2))
                Dim FLG As String = Trim(CMD(3))
                If CMD(1) = "0" Then
                    Delim = ","
                ElseIf CMD(1) = "1" Then
                    Delim = Chr(9)
                ElseIf CMD(1) = "2" Then
                    Delim = "|"
                Else
                    Delim = vbCrLf
                End If
                Dim SR As New System.IO.StreamReader(File)
                Do Until SR.EndOfStream
                    Data = SR.ReadLine
                    If Data.Length > 0 Then
                        Fields = Data.Split(Delim)
                        For i = 0 To Fields.Length - 1
                            If Trim(Fields(i)) <> "" Then
                                If IsChoiceUnique(Val(txtTestID.Text), Trim(Fields(i))) Then
                                    dgvCRanges.Rows.Add(Val(txtTestID.Text),
                                    dgvCRanges.RowCount + 1, Trim(Fields(i)), FLG)
                                End If
                            End If
                        Next
                    End If
                Loop
                SR.Close()
                SR = Nothing
            Else
                dgvCRanges.Rows.Clear()
                Dim cncr As New SqlConnection(connString)
                cncr.Open()
                Dim cmdcr As New SqlCommand("Select * from C_Ranges where Test_ID = " & Val(CMDSTR), cncr)
                cmdcr.CommandType = CommandType.Text
                Dim drcr As SqlDataReader = cmdcr.ExecuteReader
                If drcr.HasRows Then
                    Dim c As Integer = 1
                    While drcr.Read
                        dgvCRanges.Rows.Add(Val(txtTestID.Text), c,
                        Trim(drcr("Choice")), drcr("Flag"), drcr("Behavior"))
                        c += 1
                    End While
                End If
                cncr.Close()
                cncr = Nothing
            End If
        End If
        If dgvCRanges.RowCount > 0 Then btnRemAllChoice.Enabled = True
    End Sub

    Private Sub dgvCRanges_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvCRanges.CellDoubleClick
        If e.RowIndex <> -1 Then
            cmbChoice.Text = dgvCRanges.Rows(e.RowIndex).Cells(2).Value
            cmbCFlag.Text = dgvCRanges.Rows(e.RowIndex).Cells(3).Value
            For i As Integer = 0 To cmbCBehavior.Items.Count - 1
                If dgvCRanges.Rows(e.RowIndex).Cells(4).Value IsNot DBNull.Value AndAlso
                cmbCBehavior.Items(i).ToString = dgvCRanges.Rows(e.RowIndex).Cells(4).Value Then
                    cmbCBehavior.SelectedIndex = i
                    Exit For
                End If
            Next
            dgvCRanges.Rows.RemoveAt(e.RowIndex)
        End If
    End Sub

    Private Sub btnNecCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNecCopy.Click
        Dim TGPID As String = frmNecCopy.ShowDialog()
        If TGPID <> "" Then
            Dim cnnec As New SqlConnection(connString)
            cnnec.Open()
            Dim cmdnec As New SqlCommand("Select ICD9 from Necessity where TGP_ID = " & Val(TGPID), cnnec)
            cmdnec.CommandType = CommandType.Text
            Dim drnec As SqlDataReader = cmdnec.ExecuteReader
            If drnec.HasRows Then
                While drnec.Read
                    dgvNecessity.Rows.Add(dgvNecessity.RowCount, txtTestID.Text, Trim(drnec("ICD9")), "")
                End While
            End If
            cnnec.Close()
            cnnec = Nothing
            If dgvNecessity.RowCount > 0 Then btnRemAllNec.Enabled = True
        End If
    End Sub

    Private Sub btnNote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNote.Click

        Dim f As New frmNotes
        If IsRtf(txtResultNote.Text) = False Then
            txtResultNote.Text = ConvertPlainTextToRtf(txtResultNote.Text)
        End If
        f.ProlisText2.RichTextBox1.Rtf = txtResultNote.Text
        f.ShowDialog()

        If f.DialogResult = DialogResult.OK Then

            If String.IsNullOrWhiteSpace(f.ProlisText2.RichTextBox1.Text) Then
                txtResultNote.Clear()
                btnNote.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\NoteBlank.ico")

            Else
                txtResultNote.Text = f.ProlisText2.RichTextBox1.Rtf
                btnNote.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Note.ico")
            End If

        End If

        'txtResultNote.Text = frmTestNote.ShowDialog()
        'If txtResultNote.Text <> "" Then
        '    btnNote.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Note.ico")
        'Else
        '    btnNote.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\NoteBlank.ico")
        'End If
    End Sub

    Private Sub chkTBillable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTBillable.CheckedChanged
        If chkTBillable.Checked = False Then
            chkTBillable.Text = "NO"
        Else
            chkTBillable.Text = "YES"
        End If
        If chkPBillable.Checked Or chkTBillable.Checked Or chkCBillable.Checked Then
            TabControl3.Enabled = True
        Else
            ClearBilling()
            TabControl3.Enabled = False
        End If
        Update_Progress()
    End Sub

    Private Sub chkPBillable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPBillable.CheckedChanged
        If chkPBillable.Checked = False Then
            chkPBillable.Text = "NO"
        Else
            chkPBillable.Text = "YES"
        End If
        If chkPBillable.Checked Or chkTBillable.Checked Or chkCBillable.Checked Then
            TabControl3.Enabled = True
        Else
            ClearBilling()
            TabControl3.Enabled = False
        End If
        Update_Progress()
    End Sub

    Private Sub chkCBillable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCBillable.CheckedChanged
        If chkCBillable.Checked = False Then
            chkCBillable.Text = "NO"
        Else
            chkCBillable.Text = "YES"
        End If
        If chkPBillable.Checked Or chkTBillable.Checked Or chkCBillable.Checked Then
            TabControl3.Enabled = True
        Else
            ClearBilling()
            TabControl3.Enabled = False
        End If
        Update_Progress()
    End Sub

    Private Sub Update_Progress()
        If txtTestID.Text <> "" And txtName.Text <> "" And txtAbbr.Text <> "" And (((ChkResult.Checked = True) _
                And (lstMaterials.CheckedItems.Count > 0 Or txtFormula.Text <> "")) Or (ChkResult.Checked =
                False)) And ((chkTBillable.Checked = True And Trim(txtCPTCode.Text) <> "" And
                txtListPrice.Text <> "") Or (chkTBillable.Checked = False)) And ((chkCBillable.Checked = True _
                Or chkPBillable.Checked = True And txtListPrice.Text <> "") Or (chkCBillable.Checked =
                False And chkPBillable.Checked = False)) Then
            btnSave.Enabled = True
            If chkEditNew.Checked = False Then btnDelete.Enabled = True
        Else
            btnSave.Enabled = False
            btnDelete.Enabled = False
        End If
    End Sub

    Private Sub lstMaterials_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMaterials.GotFocus
        lstMaterials.BackColor = FCOLOR
    End Sub

    Private Sub lstMaterials_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstMaterials.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub lstMaterials_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMaterials.LostFocus
        lstMaterials.BackColor = NFCOLOR
    End Sub

    Private Sub lstMaterials_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMaterials.SelectedValueChanged
        Update_Progress()
    End Sub

    Private Sub txtFormula_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFormula.GotFocus
        txtFormula.BackColor = FCOLOR
    End Sub

    Private Sub txtFormula_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFormula.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtFormula_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFormula.LostFocus
        txtFormula.BackColor = NFCOLOR
    End Sub

    Private Sub txtFormula_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFormula.Validated
        Update_Progress()
    End Sub

    Private Sub txtICD9Description_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtICD9Description.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.Left Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub chkResultType_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkResultType.GotFocus
        chkResultType.BackColor = FCOLOR
    End Sub

    Private Sub chkResultType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles chkResultType.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub chkResultType_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkResultType.LostFocus
        chkResultType.BackColor = NFCOLOR
    End Sub

    Private Sub txtUOM_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUOM.GotFocus
        txtUOM.BackColor = FCOLOR
    End Sub

    Private Sub txtUOM_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUOM.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtUOM_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUOM.LostFocus
        txtUOM.BackColor = NFCOLOR
    End Sub

    Private Sub cmbDecimals_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDecimals.GotFocus
        cmbDecimals.BackColor = FCOLOR
    End Sub

    Private Sub cmbDecimals_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbDecimals.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.PageUp Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub cmbDecimals_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDecimals.LostFocus
        cmbDecimals.BackColor = NFCOLOR
    End Sub

    Private Sub ChkCalculated_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkCalculated.GotFocus
        ChkCalculated.BackColor = FCOLOR
    End Sub

    Private Sub ChkCalculated_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ChkCalculated.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub ChkCalculated_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkCalculated.LostFocus
        ChkCalculated.BackColor = NFCOLOR
    End Sub

    Private Sub chkAGRange_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAGRange.GotFocus
        chkAGRange.BackColor = FCOLOR
    End Sub

    Private Sub chkAGRange_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles chkAGRange.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub chkAGRange_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAGRange.LostFocus
        chkAGRange.BackColor = NFCOLOR
    End Sub

    Private Sub chkAutomarker_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAutomarker.GotFocus
        chkAutomarker.BackColor = FCOLOR
    End Sub

    Private Sub chkAutomarker_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles chkAutomarker.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub chkAutomarker_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAutomarker.LostFocus
        chkAutomarker.BackColor = NFCOLOR
    End Sub

    Private Sub cmbNRFlag_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbNRFlag.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.PageUp Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub cmbNRFlag_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbNRFlag.LostFocus
        cmbNRFlag.BackColor = NFCOLOR
    End Sub

    Private Sub cmbDept_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDept.GotFocus
        cmbDept.BackColor = FCOLOR
    End Sub

    Private Sub cmbDept_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbDept.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.PageUp Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub cmbDept_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDept.LostFocus
        cmbDept.BackColor = NFCOLOR
    End Sub

    Private Sub txtDescription_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDescription.GotFocus
        txtDescription.BackColor = FCOLOR
    End Sub

    Private Sub txtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDescription.KeyDown
        If e.KeyCode = Keys.PageDown Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.PageUp Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtDescription_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDescription.LostFocus
        txtDescription.BackColor = NFCOLOR
    End Sub

    Private Sub txtLoinc_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLoinc.GotFocus
        txtLoinc.BackColor = FCOLOR
    End Sub

    Private Sub txtLoinc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLoinc.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtLoinc_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLoinc.LostFocus
        txtLoinc.BackColor = NFCOLOR
    End Sub

    Private Sub txtCPTCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCPTCode.GotFocus
        txtCPTCode.BackColor = FCOLOR
    End Sub

    Private Sub txtCPTCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCPTCode.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtCPTCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCPTCode.LostFocus
        txtCPTCode.BackColor = NFCOLOR
    End Sub

    Private Sub txtListPrice_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtListPrice.LostFocus
        txtListPrice.BackColor = NFCOLOR
    End Sub

    Private Sub txtPrice1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice1.LostFocus
        txtPrice1.BackColor = NFCOLOR
    End Sub

    Private Sub txtPrice2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice2.LostFocus
        txtPrice2.BackColor = NFCOLOR
    End Sub

    Private Sub txtPrice3_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice3.LostFocus
        txtPrice3.BackColor = NFCOLOR
    End Sub

    Private Sub txtPrice4_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice4.LostFocus
        txtPrice4.BackColor = NFCOLOR
    End Sub

    Private Sub txtPrice5_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice5.LostFocus
        txtPrice5.BackColor = NFCOLOR
    End Sub

    Private Sub txtPrice6_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice6.LostFocus
        txtPrice6.BackColor = NFCOLOR
    End Sub

    Private Sub txtPrice7_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice7.LostFocus
        txtPrice7.BackColor = NFCOLOR
    End Sub

    Private Sub txtPrice8_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice8.LostFocus
        txtPrice8.BackColor = NFCOLOR
    End Sub

    Private Sub txtPrice9_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice9.LostFocus
        txtPrice9.BackColor = NFCOLOR
    End Sub

    Private Sub txtSearchID_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchID.GotFocus
        txtSearchID.BackColor = FCOLOR
    End Sub

    Private Sub txtSearchID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearchID.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtSearchID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtSearchID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchID.LostFocus
        txtSearchID.BackColor = NFCOLOR
    End Sub

    Private Sub txtBillUnit_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBillUnit.GotFocus
        txtBillUnit.BackColor = FCOLOR
    End Sub

    Private Sub txtBillUnit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBillUnit.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtBillUnit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBillUnit.KeyPress
        Prices(txtBillUnit, e)
    End Sub

    Private Sub txtBillUnit_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBillUnit.LostFocus
        txtBillUnit.BackColor = NFCOLOR
    End Sub

    Private Sub txtBillUnit_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBillUnit.Validated
        If txtBillUnit.Text = "" Then txtBillUnit.Text = "1.0"
    End Sub

    Private Sub txtSearchID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchID.Validated
        Dim RetVal As Integer
        If txtSearchID.Text <> "" Then
            If chkEditNew.Checked = True Then      'New
                If IsTGPUnique(Val(txtSearchID.Text)) = True Then
                    txtTestID.Text = txtSearchID.Text
                    If txtName.Text <> "" And txtAbbr.Text <> "" Then btnSave.Enabled = True
                Else
                    RetVal = MsgBox("You need to provide an unused number or simply accept the system generated one." _
                    & " Do you want to provide the unique number?", MsgBoxStyle.Critical + MsgBoxStyle.YesNo)
                    If RetVal = vbYes Then
                        txtSearchID.Text = ""
                        txtSearchID.Focus()
                    Else
                        txtSearchID.Text = GetNextTestID()
                    End If
                End If
            Else
                DisplayTheTest(Val(txtSearchID.Text))

                If Not String.IsNullOrEmpty(txtSearchID.Text) Then

                    btnReplicate.Enabled = True
                End If
                'MsgBox("The test code you typed, does not exit. You may use " _
                '& "the 'Lookup' function to select the test")
                'txtSearchID.Text = ""
                'txtSearchID.Focus()
            End If
        Else
            ClearForm()
        End If
        Update_Progress()
    End Sub

    Private Sub txtRptFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRptFrom.KeyPress
        Prices(txtRptFrom, e)
    End Sub

    Private Sub txtRptTo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRptTo.KeyPress
        Prices(txtRptTo, e)
    End Sub

    Private Sub chkForI_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkForI.CheckedChanged
        If chkForI.Checked = False Then
            chkForI.Text = "FLAG"
        Else
            chkForI.Text = "INTERP"
        End If
    End Sub

    Private Sub chkNorC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNorC.CheckedChanged
        If chkNorC.Checked = False Then
            chkNorC.Text = "NORMAL"
        Else
            chkNorC.Text = "CUTOFF"
        End If
    End Sub

    Private Function GetNextQID() As Integer
        Dim QID As Integer = 1
        Dim cnqid As New SqlConnection(connString)
        cnqid.Open()
        Dim cmdqid As New SqlCommand("Select Max(ID) as LastID from Clinical_Info", cnqid)
        cmdqid.CommandType = CommandType.Text
        Dim drqid As SqlDataReader = cmdqid.ExecuteReader
        If drqid.HasRows Then
            While drqid.Read
                If drqid("LastID") Is DBNull.Value Then
                    QID = 1
                Else
                    QID = drqid("LastID") + 1
                End If
            End While
        End If
        cnqid.Close()
        cnqid = Nothing
        Return QID
    End Function

    'Private Function ChoiceInTheList(ByVal Choice As String) As Boolean
    '    Dim InList As Boolean = False
    '    Dim i As Integer
    '    For i = 0 To dgvInfoAS.RowCount - 1
    '        If Choice = dgvInfoAS.Rows(i).Cells(1).Value Then
    '            InList = True
    '            Exit For
    '        End If
    '    Next
    '    Return InList
    'End Function

    'Private Sub dgvInfoAS_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
    '    If e.RowIndex <> -1 Then
    '        btnRemAChoice.Enabled = True
    '    End If
    'End Sub

    'Private Sub btnRemAChoice_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    dgvInfoAS.Rows.Remove(dgvInfoAS.SelectedRows(0))
    '    btnRemAChoice.Enabled = False
    '    If dgvInfoAS.RowCount = 0 Then btnRemAllAC.Enabled = False
    'End Sub

    'Private Sub btnRemAllAC_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    dgvInfoAS.Rows.Clear()
    '    btnRemAllAC.Enabled = False
    '    btnRemAChoice.Enabled = False
    'End Sub

    'Private Sub btnRemQ_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If dgvInfoQS.SelectedRows(0).Index <> -1 Then
    '        dgvInfoAS.Rows.Clear()
    '        If dgvInfoQS.SelectedRows(0).Index = dgvInfoQS.RowCount - 1 Then
    '            dgvInfoQS.Rows.Remove(dgvInfoQS.SelectedRows(0))
    '        Else
    '            Dim i As Integer
    '            For i = dgvInfoQS.SelectedRows(0).Index To dgvInfoQS.RowCount - 2
    '                dgvInfoQS.Rows(i).Cells(1).Value = dgvInfoQS.Rows(i + 1).Cells(1).Value
    '                dgvInfoQS.Rows(i).Cells(2).Value = dgvInfoQS.Rows(i + 1).Cells(2).Value
    '            Next
    '            dgvInfoQS.Rows(dgvInfoQS.RowCount - 1).Cells(1).Value = ""
    '            dgvInfoQS.Rows(dgvInfoQS.RowCount - 1).Cells(2).Value = "Choice"
    '        End If
    '        btnRemQ.Enabled = False
    '        If dgvInfoQS.RowCount = 1 Then btnRemAllQ.Enabled = False
    '    End If

    'End Sub

    Private Sub txtMod1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMod1.GotFocus
        txtMod1.BackColor = FCOLOR
    End Sub

    Private Sub txtMod1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMod1.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtMod1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMod1.LostFocus
        txtMod1.BackColor = NFCOLOR
    End Sub

    Private Sub txtMod2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMod2.GotFocus
        txtMod2.BackColor = FCOLOR
    End Sub

    Private Sub txtMod2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMod2.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtMod2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMod2.LostFocus
        txtMod2.BackColor = NFCOLOR
    End Sub

    Private Sub txtMod3_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMod3.GotFocus
        txtMod3.BackColor = FCOLOR
    End Sub

    Private Sub txtMod3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMod3.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtMod3_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMod3.LostFocus
        txtMod3.BackColor = NFCOLOR
    End Sub

    Private Sub txtMod4_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMod4.GotFocus
        txtMod4.BackColor = FCOLOR
    End Sub

    Private Sub txtMod4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMod4.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtMod4_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMod4.LostFocus
        txtMod4.BackColor = NFCOLOR
    End Sub

    Private Sub txtPOS_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPOS.GotFocus
        txtPOS.BackColor = FCOLOR
    End Sub

    Private Sub txtPOS_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPOS.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtPOS_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPOS.LostFocus
        txtPOS.BackColor = NFCOLOR
    End Sub

    Private Sub txtPOS_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPOS.Validated
        If txtPOS.Text = "" Then txtPOS.Text = "81"
    End Sub

    Private Sub txtCPTMCD_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCPTMCD.GotFocus
        txtCPTMCD.BackColor = FCOLOR
    End Sub

    Private Sub txtCPTMCD_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCPTMCD.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtCPTMCD_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCPTMCD.LostFocus
        txtCPTMCD.BackColor = NFCOLOR
    End Sub

    Private Sub txtCPTMCR_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCPTMCR.GotFocus
        txtCPTMCR.BackColor = FCOLOR
    End Sub

    Private Sub txtCPTMCR_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCPTMCR.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtCPTMCR_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCPTMCR.LostFocus
        txtCPTMCR.BackColor = NFCOLOR
    End Sub

    Private Sub txtCPTSPC_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCPTSPC.GotFocus
        txtCPTSPC.BackColor = FCOLOR
    End Sub

    Private Sub txtCPTSPC_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCPTSPC.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtCPTSPC_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCPTSPC.LostFocus
        txtCPTSPC.BackColor = NFCOLOR
    End Sub

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        frmTGPUpdate.ShowDialog()
        frmTGPUpdate.MdiParent = frmDashboard
    End Sub

    Private Sub TabControl3_Selected(ByVal sender As Object, ByVal e As System.Windows.Forms.TabControlEventArgs) Handles TabControl3.Selected
        If e.TabPage.Name = "tpnecessity" Then
            If chkTBillable.Checked = True Then
                If dgvNecessity.RowCount = 0 Then _
                DisplayNecessity(Val(txtTestID.Text))
            End If
        End If
    End Sub

    Private Sub txtNTrigID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNTrigID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtNTrigID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNTrigID.Validated
        If txtNTrigID.Text <> "" Then
            Dim cntid As New SqlConnection(connString)
            cntid.Open()
            Dim cmdtid As New SqlCommand("Select ID, Name from Tests where ID = " & Val(txtNTrigID.Text) _
            & " Union Select ID, Name from Groups where ID = " & Val(txtNTrigID.Text) _
            & " Union Select ID, Name from Profiles where ID = " & Val(txtNTrigID.Text), cntid)
            cmdtid.CommandType = CommandType.Text
            Dim drtid As SqlDataReader = cmdtid.ExecuteReader
            If drtid.HasRows Then
                While drtid.Read
                    txtNTrigID.Text = drtid("ID")
                    txtNTrigName.Text = drtid("Name")
                    If txtNRangeFrom.Text <> "" And txtNRangeTo.Text <> "" _
                    Then btnAddNAuto.Enabled = True
                End While
            Else
                MsgBox("Invalid Component ID", MsgBoxStyle.Critical, "Prolis")
                txtNTrigID.Text = ""
                txtNTrigName.Text = ""
                btnAddNAuto.Enabled = False
                txtNTrigID.Focus()
            End If
            cntid.Close()
            cntid = Nothing
        End If
    End Sub

    Private Sub txtCTrigID_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
        If txtCTrigID.Text <> "" Then
            Dim cncid As New SqlConnection(connString)
            cncid.Open()
            Dim cmdcid As New SqlCommand("Select ID, Name from Tests where ID = " & Val(txtCTrigID.Text) _
            & " Union Select ID, Name from Groups where ID = " & Val(txtCTrigID.Text) _
            & " Union Select ID, Name from Profiles where ID = " & Val(txtCTrigID.Text), cncid)
            cmdcid.CommandType = CommandType.Text
            Dim drcid As SqlDataReader = cmdcid.ExecuteReader
            If drcid.HasRows Then
                While drcid.Read
                    txtCTrigID.Text = drcid("ID")
                    txtCTrigName.Text = drcid("Name") & " (" & drcid("ID") & ")"
                    btnAddCAuto.Enabled = True
                End While
            Else
                MsgBox("Invalid Component ID", MsgBoxStyle.Critical, "Prolis")
                txtCTrigID.Text = ""
                txtCTrigName.Text = ""
                txtNTrigID.Focus()
            End If
            cncid.Close()
            cncid = Nothing
        End If
    End Sub

    Private Sub btnNTrig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNTrig.Click
        Dim CompInfo As String = frmAllCompsLookUp.ShowDialog
        If CompInfo <> "" Then
            Dim CMPS() As String = Split(CompInfo, "|")
            txtNTrigID.Text = CMPS(0)
            If CMPS.Length >= 2 Then txtNTrigName.Text = CMPS(1)
            If txtNRangeFrom.Text <> "" And txtNRangeTo.Text <> "" _
            Then btnAddNAuto.Enabled = True
        End If
    End Sub

    Private Sub dgvNAutomarks_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvNAutomarks.CellDoubleClick
        If e.RowIndex <> -1 Then
            txtNRangeFrom.Text = dgvNAutomarks.Rows(e.RowIndex).Cells(2).Value
            txtNRangeTo.Text = dgvNAutomarks.Rows(e.RowIndex).Cells(3).Value
            txtNTrigID.Text = dgvNAutomarks.Rows(e.RowIndex).Cells(4).Value
            txtNTrigName.Text = dgvNAutomarks.Rows(e.RowIndex).Cells(5).Value
            chkNMultiReflex.Checked = dgvNAutomarks.Rows(e.RowIndex).Cells(6).Value
            dgvNAutomarks.Rows.RemoveAt(e.RowIndex)
            btnAddNAuto.Enabled = True
        End If
    End Sub

    Private Sub chkPreAnalytical_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPreAnalytical.CheckedChanged
        If chkPreAnalytical.Checked = False Then    'regular test
            chkPreAnalytical.Text = "No"
            cmbInfoFormat.SelectedIndex = -1
            chkClinicalRequired.Checked = False
            cmbInfoFormat.Enabled = False
            chkClinicalRequired.Enabled = False
        Else
            chkPreAnalytical.Text = "Yes"
            cmbInfoFormat.SelectedIndex = 2
            chkClinicalRequired.Checked = False
            cmbInfoFormat.Enabled = True
            chkClinicalRequired.Enabled = True
        End If
    End Sub

    Private Sub chkClinicalRequired_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkClinicalRequired.CheckedChanged
        If chkClinicalRequired.Checked = False Then
            chkClinicalRequired.Text = "No"
        Else
            chkClinicalRequired.Text = "Yes"
        End If
    End Sub

    Private Sub chkInHouse_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkInHouse.CheckedChanged
        If chkInHouse.Checked = True Then
            chkInHouse.Text = "Yes"
        Else
            chkInHouse.Text = "No"
        End If
    End Sub

    'Private Sub dgvInfo_CellContentClick1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvInfo.CellContentClick
    '    If e.ColumnIndex = 0 Then   'Delete
    '        If dgvInfo.Rows(e.RowIndex).Cells(3).Value IsNot Nothing _
    '        AndAlso dgvInfo.Rows(e.RowIndex).Cells(3).Value <> "" Then
    '            If dgvInfo.RowCount = 1 Then
    '                dgvInfo.Rows(e.RowIndex).Cells(0).Value = System.Drawing.Image.FromFile(
    '                My.Application.Info.DirectoryPath & "\Images\Blank.ico")
    '                dgvInfo.Rows(e.RowIndex).Cells(1).Value = ""
    '                dgvInfo.Rows(e.RowIndex).Cells(3).Value = ""
    '                DirectCast(dgvInfo.Rows(e.RowIndex).Cells(4).Value, CheckBox).Checked = False
    '                DirectCast(dgvInfo.Rows(e.RowIndex).Cells(5).Value, Windows.Forms.ComboBox).SelectedIndex = -1
    '            ElseIf dgvInfo.RowCount > 1 Then
    '                dgvInfo.Rows.RemoveAt(e.RowIndex)
    '            End If
    '        End If
    '    ElseIf e.ColumnIndex = 2 Then   'Lookup
    '        Dim InfoID As String = frmInfoLookUp.ShowDialog
    '        If InfoID <> "" Then InsertInfo(Val(InfoID), e.RowIndex)
    '        If dgvInfo.Rows(e.RowIndex).Cells(3).Value IsNot Nothing AndAlso
    '        dgvInfo.Rows(e.RowIndex).Cells(3).Value <> "" Then dgvInfo.RowCount += 1
    '    ElseIf e.ColumnIndex = 4 Then   'Required
    '        If dgvInfo.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = False Then
    '            dgvInfo.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = True
    '        Else
    '            dgvInfo.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = False
    '        End If
    '    End If
    'End Sub

    Private Sub dgvInfo_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvInfo.CellContentClick
        If e.ColumnIndex = 0 Then ' Delete
            If dgvInfo.Rows(e.RowIndex).Cells(3).Value IsNot Nothing _
        AndAlso dgvInfo.Rows(e.RowIndex).Cells(3).Value.ToString() <> "" Then
                If dgvInfo.Rows.Count = 1 Then
                    dgvInfo.Rows(e.RowIndex).Cells(0).Value = System.Drawing.Image.FromFile(
                    IO.Path.Combine(Application.StartupPath, "Images\Blank.ico"))
                    dgvInfo.Rows(e.RowIndex).Cells(1).Value = ""
                    dgvInfo.Rows(e.RowIndex).Cells(3).Value = ""
                    dgvInfo.Rows(e.RowIndex).Cells(4).Value = False
                    Dim comboBoxCell = TryCast(dgvInfo.Rows(e.RowIndex).Cells(5), DataGridViewComboBoxCell)
                    If comboBoxCell IsNot Nothing Then comboBoxCell.Value = Nothing
                ElseIf dgvInfo.Rows.Count > 1 Then
                    dgvInfo.Rows.RemoveAt(e.RowIndex)
                End If
            End If
        ElseIf e.ColumnIndex = 2 Then ' Lookup
            Dim InfoID As String = frmInfoLookUp.ShowDialog()
            If Not String.IsNullOrEmpty(InfoID) Then
                InsertInfo(Val(InfoID), e.RowIndex)
            End If

            If dgvInfo.Rows(e.RowIndex).Cells(3).Value IsNot Nothing AndAlso
        dgvInfo.Rows(e.RowIndex).Cells(3).Value.ToString() <> "" Then
                dgvInfo.Rows.Add()
            End If
        ElseIf e.ColumnIndex = 4 Then ' Required
            Dim currentValue = Convert.ToBoolean(dgvInfo.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
            dgvInfo.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Not currentValue
        End If
    End Sub


    Private Sub InsertInfo(ByVal InfoID As Integer, ByVal RowIndex As Integer)
        If Not InfoInGrid(InfoID) Then
            Dim cnii As New SqlConnection(connString)
            cnii.Open()
            Dim cmdii As New SqlCommand("Select * from Tests where ID = " & InfoID, cnii)
            cmdii.CommandType = CommandType.Text
            Dim drii As SqlDataReader = cmdii.ExecuteReader
            If drii.HasRows Then
                While drii.Read
                    dgvInfo.Rows(RowIndex).Cells(0).Value = System.Drawing.Image.FromFile(
                    My.Application.Info.DirectoryPath & "\Images\Eraser.ico")
                    dgvInfo.Rows(RowIndex).Cells(1).Value = drii("ID")
                    dgvInfo.Rows(RowIndex).Cells(3).Value = drii("Name")
                    dgvInfo.Rows(RowIndex).Cells(4).Value = drii("PreAnaRequired")
                    dgvInfo.Rows(RowIndex).Cells(5).Value = 0
                End While
            End If
            cnii.Close()
            cnii = Nothing
        End If
    End Sub

    Private Function InfoInGrid(ByVal InfoID As Integer) As Boolean
        Dim InGrid As Integer = 0
        Dim i As Integer
        For i = 0 To dgvInfo.RowCount - 1
            If dgvInfo.Rows(i).Cells(1).Value IsNot Nothing AndAlso
            dgvInfo.Rows(i).Cells(1).Value.ToString = InfoID.ToString Then
                InGrid += 1
            End If
        Next
        If InGrid > 1 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub dgvInfo_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvInfo.CellEndEdit
        If e.ColumnIndex = 1 Then
            If dgvInfo.Rows(e.RowIndex).Cells(e.ColumnIndex).Value IsNot Nothing _
            AndAlso dgvInfo.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString <> "" _
            Then dgvInfo.Rows.Add()
        End If
    End Sub

    Private Sub dgvInfo_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvInfo.CellValidated
        If e.ColumnIndex = 1 Then   'ID
            If dgvInfo.Rows(e.RowIndex).Cells(e.ColumnIndex).Value IsNot Nothing _
            AndAlso dgvInfo.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString <> "" Then
                InsertInfo(dgvInfo.Rows(e.RowIndex).Cells(e.ColumnIndex).Value, e.RowIndex)
                If dgvInfo.Rows(e.RowIndex).Cells(3).Value Is Nothing Then
                    MsgBox("Invalid Info ID", MsgBoxStyle.Critical, "Prolis")
                    dgvInfo.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ""
                ElseIf dgvInfo.Rows(e.RowIndex).Cells(3).Value.ToString = "" Then
                    MsgBox("Invalid Info ID", MsgBoxStyle.Critical, "Prolis")
                    dgvInfo.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ""
                End If
            End If
        End If
    End Sub

    Private Sub dgvInfo_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvInfo.DataError
        On Error Resume Next
    End Sub

    Private Sub dgvCondAutomarks_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvCondAutomarks.CellContentClick
        If e.RowIndex <> -1 Then btnCondRem.Enabled = True
    End Sub

    Private Sub btnCondRem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCondRem.Click
        If dgvCondAutomarks.RowCount > 0 Then _
        dgvCondAutomarks.Rows.RemoveAt(dgvCondAutomarks.SelectedRows(0).Index)
        If dgvCondAutomarks.RowCount = 0 Then
            btnCondRem.Enabled = False
            btnCondRemAll.Enabled = False
        End If
    End Sub

    Private Sub btnCondRemAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCondRemAll.Click
        dgvCondAutomarks.Rows.Clear()
        btnCondRem.Enabled = False
        btnCondRemAll.Enabled = False
    End Sub

    Private Sub txtCondTrigID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCondTrigID.Validated
        If txtCondTrigID.Text <> "" Then
            txtCondTrigName.Text = GetTGPName(Val(txtCondTrigID.Text))
            If cmbAutoCond.Text <> "" And txtCondTrigName.Text <> "" Then
                btnCondAdd.Enabled = True
            Else
                btnCondAdd.Enabled = False
            End If
        Else
            txtCondTrigName.Text = ""
            btnCondAdd.Enabled = False
        End If
    End Sub

    Private Sub btnCondAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCondAdd.Click
        Dim i As Integer = dgvCondAutomarks.RowCount + 1
        If txtTestID.Text <> "" And cmbAutoCond.Text <> "" And txtCondTrigID.Text <> "" Then
            If Not CondinList(cmbAutoCond.Text, txtCondTrigID.Text) Then
                dgvCondAutomarks.Rows.Add(txtTestID.Text, i, cmbAutoCond.Text,
                txtCondTrigID.Text, txtCondTrigName.Text)
                cmbAutoCond.Text = ""
                txtCondTrigID.Text = ""
                txtCondTrigName.Text = ""
                btnCondAdd.Enabled = False
                btnCondRemAll.Enabled = True
            End If
        Else
            MsgBox("Required fields don't have values", MsgBoxStyle.Critical, "Prolis")
        End If
    End Sub

    Private Function CondinList(ByVal Condition As String, ByVal TGPID As Integer) As Boolean
        Dim InList As Boolean = False
        For i As Integer = 0 To dgvCondAutomarks.RowCount - 1
            If UCase(dgvCondAutomarks.Rows(i).Cells(2).Value) = UCase(Condition) _
            And dgvCondAutomarks.Rows(i).Cells(3).Value = TGPID Then
                InList = True
                Exit For
            End If
        Next
        Return InList
    End Function

    Private Sub btnCondTrig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCondTrig.Click
        Dim CondTrigID As String = frmAllCompsLookUp.ShowDialog
        If CondTrigID <> "" Then
            Dim CMPS() As String = Split(CondTrigID, "|")
            txtCondTrigID.Text = CMPS(0)
            If CMPS.Length >= 2 Then txtCondTrigName.Text = CMPS(1)
            If cmbAutoCond.Text <> "" And txtCondTrigID.Text <> "" And
            txtCondTrigName.Text <> "" Then btnCondAdd.Enabled = True
        End If
    End Sub

    Private Sub btnCTrig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCTrig.Click
        Dim CTrigID As String = frmAllCompsLookUp.ShowDialog
        If CTrigID <> "" Then
            Dim CMPS() As String = Split(CTrigID, "|")
            txtCTrigID.Text = CMPS(0)
            If CMPS.Length >= 2 Then txtCTrigName.Text = CMPS(1)
            If cmbCAutoChoice.Text <> "" And txtCTrigID.Text <> "" And
            txtCTrigName.Text <> "" Then btnAddCAuto.Enabled = True
        End If
    End Sub

    Private Sub dgvNRanges_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvNRanges.CellDoubleClick
        If e.RowIndex <> -1 Then
            txtNRFrom.Text = dgvNRanges.Rows(e.RowIndex).Cells(2).Value
            txtNRTo.Text = dgvNRanges.Rows(e.RowIndex).Cells(3).Value
            cmbNRFlag.Text = dgvNRanges.Rows(e.RowIndex).Cells(4).Value
            For i As Integer = 0 To cmbNBehavior.Items.Count - 1
                If dgvNRanges.Rows(e.RowIndex).Cells(5).Value IsNot DBNull.Value AndAlso
                cmbNBehavior.Items(i).ToString = dgvNRanges.Rows(e.RowIndex).Cells(5).Value Then
                    cmbNBehavior.SelectedIndex = i
                    Exit For
                End If
            Next
            dgvNRanges.Rows.RemoveAt(e.RowIndex)
        End If
    End Sub

    Private Sub dgvAGRanges_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvAGRanges.CellDoubleClick
        If e.RowIndex <> -1 Then
            txtAGRAgeFrom.Text = dgvAGRanges.Rows(e.RowIndex).Cells(2).Value
            txtAGRAgeTo.Text = dgvAGRanges.Rows(e.RowIndex).Cells(3).Value
            For i As Integer = 0 To cmbAGSex.Items.Count - 1
                If cmbAGSex.Items(i).ToString.Substring(0, 1) =
                dgvAGRanges.Rows(e.RowIndex).Cells(4).Value.ToString.Substring(0, 1) Then
                    cmbAGSex.SelectedIndex = i
                    Exit For
                End If
            Next
            txtAGVFrom.Text = dgvAGRanges.Rows(e.RowIndex).Cells(5).Value
            txtAGVTo.Text = dgvAGRanges.Rows(e.RowIndex).Cells(6).Value
            cmbAGFlag.Text = dgvAGRanges.Rows(e.RowIndex).Cells(7).Value
            For i As Integer = 0 To cmbAGBehavior.Items.Count - 1
                If cmbAGBehavior.Items(i).ToString = dgvAGRanges.Rows(e.RowIndex).Cells(8).Value.ToString Then
                    cmbAGBehavior.SelectedIndex = i
                    Exit For
                End If
            Next
            dgvAGRanges.Rows.RemoveAt(e.RowIndex)
        End If
    End Sub

    Private Sub cmbCFlag_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCFlag.SelectedIndexChanged
        If txtTestID.Text <> "" And cmbChoice.Text <> "" And cmbCBehavior.SelectedIndex <> -1 Then btnAddChoice.Enabled = True
    End Sub

    Private Sub cmbAGFlag_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAGFlag.SelectedIndexChanged
        If txtAGRAgeTo.Text <> "" And cmbAGSex.SelectedIndex <> -1 And txtAGRAgeFrom.Text <> "" _
        And txtAGVFrom.Text <> "" And cmbAGBehavior.SelectedIndex <> -1 Then btnAddAGR.Enabled = True
    End Sub

    Private Sub txtAltNames_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAltNames.Validated
        If txtAltNames.Text <> "" Then
            chkEvaluate.Checked = True
        Else
            chkEvaluate.Checked = True
        End If
    End Sub

    Private Sub dgvSRanges_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSRanges.CellClick
        btnSRemove.Enabled = True
    End Sub

    Private Sub dgvSRanges_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSRanges.CellDoubleClick
        'Dim SPID As Integer
        Dim ItemX As MyList
        For i As Integer = 0 To cmbSpecies.Items.Count - 1
            ItemX = cmbSpecies.Items(i)
            If InStr(dgvSRanges.Rows(e.RowIndex).Cells(1).Value, ItemX.Name) > 0 Then
                cmbSpecies.SelectedIndex = i
                Exit For
            End If
        Next
        txtSVFrom.Text = dgvSRanges.Rows(e.RowIndex).Cells(2).Value
        txtSVTo.Text = dgvSRanges.Rows(e.RowIndex).Cells(3).Value
        cmbSFlag.Text = dgvSRanges.Rows(e.RowIndex).Cells(4).Value
        cmbSBehavior.Text = dgvSRanges.Rows(e.RowIndex).Cells(5).Value
        btnSAdd.Enabled = True
        dgvSRanges.Rows.RemoveAt(e.RowIndex)
    End Sub

    Private Sub btnSAdd_Click(sender As Object, e As EventArgs) Handles btnSAdd.Click
        If cmbSpecies.SelectedIndex <> -1 And txtSVFrom.Text <> "" And txtSVTo.Text <> "" _
        And cmbSFlag.Text <> "" And cmbSBehavior.SelectedIndex <> -1 Then
            Dim ItemX As MyList = cmbSpecies.SelectedItem
            dgvSRanges.Rows.Add(txtTestID.Text, ItemX.Name & " (" & ItemX.ItemData _
            & ")", txtSVFrom.Text, txtSVTo.Text, cmbSFlag.Text, cmbSBehavior.Text)
            txtSVFrom.Text = ""
            txtSVTo.Text = ""
            cmbSFlag.Text = ""
            cmbSBehavior.Text = "Ignore"
            btnSAdd.Enabled = False
        End If

    End Sub

    Private Sub btnSRemove_Click(sender As Object, e As EventArgs) Handles btnSRemove.Click
        If dgvSRanges.SelectedRows.Count > 0 Then
            dgvSRanges.Rows.RemoveAt(dgvSRanges.SelectedRows(0).Index)
            btnSRemove.Enabled = False
        End If
    End Sub

    Private Sub btnSRemAll_Click(sender As Object, e As EventArgs) Handles btnSRemAll.Click
        dgvSRanges.Rows.Clear()
    End Sub

    Private Sub UpdateSREntry()
        If cmbSpecies.SelectedIndex <> -1 And txtSVFrom.Text <> "" And txtSVTo.Text <> "" And
        cmbSFlag.Text <> "" And cmbSBehavior.SelectedIndex <> -1 And txtTestID.Text <> "" Then
            btnSAdd.Enabled = True
        Else
            btnSAdd.Enabled = False
        End If
    End Sub

    Private Sub cmbSBehavior_TabIndexChanged(sender As Object, e As EventArgs) Handles cmbSBehavior.TabIndexChanged
        UpdateSREntry()
    End Sub

    Private Sub cmbSFlag_Validated(sender As Object, e As EventArgs) Handles cmbSFlag.Validated
        UpdateSREntry()
    End Sub

    Private Sub cmbSpecies_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSpecies.SelectedIndexChanged
        UpdateSREntry()
    End Sub

    Private Sub txtSVFrom_Validated(sender As Object, e As EventArgs) Handles txtSVFrom.Validated
        UpdateSREntry()
    End Sub

    Private Sub txtSVTo_Validated(sender As Object, e As EventArgs) Handles txtSVTo.Validated
        UpdateSREntry()
    End Sub

    Private Sub dgvCAutomarks_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCAutomarks.CellDoubleClick
        If e.RowIndex <> -1 Then
            cmbCAutoChoice.Text = dgvCAutomarks.Rows(e.RowIndex).Cells(2).Value
            txtCTrigID.Text = dgvCAutomarks.Rows(e.RowIndex).Cells(3).Value
            txtCTrigName.Text = dgvCAutomarks.Rows(e.RowIndex).Cells(4).Value
            chkCMultiReflex.Checked = dgvCAutomarks.Rows(e.RowIndex).Cells(5).Value
            dgvCAutomarks.Rows.RemoveAt(e.RowIndex)
            btnAddCAuto.Enabled = True
        End If
    End Sub

    Private Sub btnAddCAuto_Click(sender As Object, e As EventArgs) Handles btnAddCAuto.Click
        If txtTestID.Text <> "" And cmbCAutoChoice.Text <> "" And txtCTrigID.Text <> "" Then
            If IsCAutoUnique(Val(txtTestID.Text), cmbCAutoChoice.Text, Val(txtCTrigID.Text)) Then
                dgvCAutomarks.Rows.Add(txtTestID.Text, dgvCAutomarks.RowCount + 1,
                cmbCAutoChoice.Text, Val(txtCTrigID.Text), txtCTrigName.Text, chkCMultiReflex.Checked)
                cmbCAutoChoice.Text = ""
                cmbCAutoChoice.SelectedIndex = -1
                txtCTrigID.Text = ""
                txtCTrigName.Text = ""
                chkCMultiReflex.Checked = False
                btnRemAllCAuto.Enabled = True
                btnAddCAuto.Enabled = False
                cmbCAutoChoice.Focus()
            End If
        End If
    End Sub

    Private Sub cmbNBehavior_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNBehavior.SelectedIndexChanged
        If txtTestID.Text <> "" And txtNRTo.Text <> "" And txtNRFrom.Text <> "" And cmbNBehavior.SelectedIndex <> -1 Then btnAddNR.Enabled = True
    End Sub

    Dim Replication As Boolean
    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles btnReplicate.Click

        Dim RetVal As Integer = MsgBox("Are you sure to Replicate the selected Record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
        If RetVal = vbYes Then
            Replication = True
            txtSearchID.Text = GetNextTestID()
            txtTestID.Text = txtSearchID.Text
            btnImport.Enabled = False
            btnSave_Click(Nothing, Nothing)
            Replication = False
            MessageBox.Show($"Record Replicated as New ID = {txtTestID.Text} ")
        End If
    End Sub

    Private Sub txtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged
        Me.cmbNRFlag.Items.Clear()
        Me.cmbNRFlag.Items.AddRange(New Object() {"Abnormal", "Abnormal Panic", "Accepted", "Critical Abnormal", "Critical High", "Critical Low", "Detected", "High", "High Panic", "Insensitive", "Low", "Low Panic", "Negative", "Non Negative", "Normal", "Not Detected", "Positive", "Resistant", "Sensitive", "Susceptible", "Unaccepted", "Undetected"})

    End Sub

    Private Sub cbo_AOE_Q_Type_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_AOE_Q_Type.SelectedIndexChanged
        If cbo_AOE_Q_Type.Text = "Drop Down" Then
            Panel1.Visible = True
            Panel2.Location = Panel2Location
        Else
            Panel1.Visible = False

            Panel2.Location = Panel1.Location
        End If
    End Sub

    Private Sub btn_AddAnsToList_Click(sender As Object, e As EventArgs) Handles btn_AddAnsToList.Click
        If Not String.IsNullOrWhiteSpace(txtAnswer.Text) Then

            dgv_AOE_Ans.Rows.Add(txtAnswer.Text, System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Delete.ico"))
            txtAnswer.Clear()
            txtAnswer.Focus()
        End If

    End Sub
    Private Sub dgv_AOE_Ans_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_AOE_Ans.CellContentClick
        ' Check if the clicked cell is in the "Delete" column
        If e.ColumnIndex = dgv_AOE_Ans.Columns("Col_Delete").Index AndAlso dgv_AOE_Ans.Rows.Count > 0 Then
            ' Confirm delete action (optional)
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this row?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

            If result = DialogResult.Yes Then
                ' Delete the current row
                dgv_AOE_Ans.Rows.RemoveAt(e.RowIndex)
            End If
        End If
    End Sub

    Private Sub btn_Add_Q_to_List_Click(sender As Object, e As EventArgs) Handles btn_Add_Q_to_List.Click
        If Not String.IsNullOrWhiteSpace(cbo_Qsn.Text) Then
            Dim allAnswers As String = String.Empty

            If cbo_AOE_Q_Type.Text = "Drop Down" Then

                ' Loop through all rows in the DataGridView
                For Each row As DataGridViewRow In dgv_AOE_Ans.Rows
                    '' Ensure the row is not a new row (empty row at the bottom)
                    'If Not row.IsNewRow Then
                    ' Get the value of the first cell (answer)
                    Dim answer As String = row.Cells(0).Value.ToString()
                    ' Append the answer to the string, separated by a comma or any other delimiter
                    allAnswers &= answer & "|"
                    'End If
                Next

                ' Remove the trailing comma and space, if any
                If allAnswers.EndsWith("|") Then
                    allAnswers = allAnswers.Substring(0, allAnswers.Length - 1)
                End If

            End If

            If String.IsNullOrWhiteSpace(txtSrNo.Text) Then
                ' Add new row to DataGridView
                dgv_AOE_Questions.Rows.Add(cbo_Qsn.SelectedValue, cbo_Qsn.Text, allAnswers, chk_AOE_Required.Checked, cbo_AOE_Q_Type.Text, System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Delete.ico"))
            Else
                ' Edit an existing row based on SrNo
                Dim found As Boolean = False

                ' Loop through the rows to find the one with matching SrNo
                For Each row As DataGridViewRow In dgv_AOE_Questions.Rows
                    ' Assuming "SrNo" is in the first column (index 0)
                    If row.Cells(0).Value.ToString() = txtSrNo.Text Then
                        ' Update the values in the matching row
                        'row.Cells(1).Value = txt_AOE_Question.Text
                        row.Cells(2).Value = allAnswers
                        row.Cells(3).Value = chk_AOE_Required.Checked
                        row.Cells(4).Value = cbo_AOE_Q_Type.Text
                        found = True
                        Exit For ' Exit the loop once the row is found and updated
                    End If
                Next
            End If


            chk_AOE_Required.Checked = False
            dgv_AOE_Ans.Rows.Clear()

            txtSrNo.Clear()

            'dgv_AOE_Questions.Visible = True
        End If
    End Sub

    Private Sub dgv_AOE_Questions_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_AOE_Questions.CellContentClick

        If e.ColumnIndex = dgv_AOE_Questions.Columns("DataGridViewImageColumn3").Index AndAlso dgv_AOE_Questions.Rows.Count > 0 Then
            ' Confirm delete action (optional)
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this row?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

            If result = DialogResult.Yes Then
                ' Delete the current row
                dgv_AOE_Questions.Rows.RemoveAt(e.RowIndex)

                ' Reset the serial numbers (SrNo) in the first column (index 0)
                ResetSerialNumbers()
            End If
        End If
    End Sub


    Private Sub ResetSerialNumbers()
        ' Loop through all rows to reset the serial number
        For i As Integer = 0 To dgv_AOE_Questions.Rows.Count - 1
            ' Set the value of the first column (index 0) to the row index + 1 (for 1-based numbering)
            dgv_AOE_Questions.Rows(i).Cells(0).Value = (i + 1).ToString()
        Next
    End Sub

    Private Sub dgv_AOE_Questions_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_AOE_Questions.CellDoubleClick
        ' Ensure the double-click is on a valid row
        If e.RowIndex >= 0 Then
            ' Get the selected row
            Dim selectedRow As DataGridViewRow = dgv_AOE_Questions.Rows(e.RowIndex)

            Dim columnNames As String = String.Empty

            ' Loop through all columns in the DataGridView
            For Each column As DataGridViewColumn In dgv_AOE_Questions.Columns
                ' Append the column header text to the string
                columnNames &= column.Name & ", "
            Next

            ' Remove the trailing comma and space
            If columnNames.EndsWith(", ") Then
                columnNames = columnNames.Substring(0, columnNames.Length - 2)
            End If

            ' Display the concatenated column names (optional) 


            'MessageBox.Show(columnNames)
            Dim allAnswers As String = String.Empty

            ' Load the values from the selected row into TextBox controls
            txtSrNo.Text = selectedRow.Cells(0).Value.ToString() ' Assuming Answer is in the first column
            cbo_Qsn.Text = selectedRow.Cells(1).Value.ToString() ' Assuming ID is in the second column
            allAnswers = selectedRow.Cells(2).Value.ToString() ' Assuming ID is in the second column
            chk_AOE_Required.Checked = selectedRow.Cells(3).Value.ToString() ' Assuming ID is in the second column
            cbo_AOE_Q_Type.Text = selectedRow.Cells(4).Value.ToString() ' Assuming ID is in the second column

            If Not String.IsNullOrWhiteSpace(allAnswers) Then
                Dim Ans() As String = allAnswers.Split("|")

                For i = 0 To Ans.Length - 1
                    dgv_AOE_Ans.Rows.Add(Ans(i), System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Delete.ico"))
                Next
            End If
        End If
    End Sub

    Private Sub btn_New_Qsn_Click(sender As Object, e As EventArgs) Handles btn_New_Qsn.Click
        Dim f As New frmQuestions
        f.ShowDialog()

        LoadQuestionCombo()
    End Sub

    Private Sub LoadQuestionCombo()
        Dim query As String = $"Select * from AOE_Questions order by Question"

        Dim dt As New DataTable()

        Using conn As New SqlConnection(connString)
            Using cmd As New SqlCommand(query, conn)
                Using da As New SqlDataAdapter(cmd)
                    conn.Open()

                    da.Fill(dt)
                End Using
            End Using
        End Using

        cbo_Qsn.DataSource = dt
        cbo_Qsn.DisplayMember = "Question"
        cbo_Qsn.ValueMember = "Q_Id"
    End Sub

    Private Sub cmbNRFlag_Click(sender As Object, e As EventArgs) Handles cmbNRFlag.Click
        Me.cmbNRFlag.Items.Clear()
        Me.cmbNRFlag.Items.AddRange(New Object() {"Abnormal", "Abnormal Panic", "Accepted", "Critical Abnormal", "Critical High", "Critical Low", "Detected", "High", "High Panic", "Insensitive", "Low", "Low Panic", "Negative", "Non Negative", "Normal", "Not Detected", "Positive", "Resistant", "Sensitive", "Susceptible", "Unaccepted", "Undetected"})

    End Sub

End Class