Imports System.Windows.Forms
Imports System.Data
Imports System.Data.SqlClient

Public Class frmSendOuts
    'TODO: Dymo Code
    'Public DymoAddIn As Dymo.DymoAddIn
    'Public DymoLabel As Dymo.DymoLabels
    '==================
    Private Sub frmSendOuts_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'If sqlCN.State = ConnectionState.Open Then sqlCN.Close()
    End Sub

    Private Sub frmSendOuts_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtID.Text = GetNewSendoutID()
        txtReqQTY.Text = "1"
        dtpDate.Format = DateTimePickerFormat.Custom
        dtpDate.CustomFormat = SystemConfig.DateFormat
        Dim stopWatch As New Stopwatch()
        stopWatch.Start()
        '
        cmbBillType.Items.Clear()
        cmbBillType.Items.Add(New MyList("Me", 0))
        cmbBillType.Items.Add(New MyList("Patient", 2))
        cmbBillType.Items.Add(New MyList("Gratis", 3))
        '
        PopulateLabs()
        populateCandidates()
        '
        stopWatch.Stop()
        lblStatus.Text = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
        stopWatch.Elapsed.Hours, stopWatch.Elapsed.Minutes,
        stopWatch.Elapsed.Seconds, stopWatch.Elapsed.Milliseconds / 10)
        '
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Function GetCandidateAccs() As String()
        Dim Accs() As String = {""}
        Dim Span As Integer = SystemConfig.SendoutSpan
        Dim MinDate As Date = DateAdd(DateInterval.Day, -Span, CDate(Format(dtpDate.Value, SystemConfig.DateFormat)))
        Dim ThisDate As Date = CDate(Format(dtpDate.Value, SystemConfig.DateFormat & " 23:59:00"))
        Dim cnn As New SqlClient.SqlConnection(connString)
        cnn.Open()
        Dim CMD As New SqlCommand("GetOSCandidates_SP", cnn)
        CMD.CommandTimeout = 120
        CMD.CommandType = CommandType.StoredProcedure
        CMD.Parameters.AddWithValue("@FDate", MinDate)
        CMD.Parameters.AddWithValue("@TDate", ThisDate)
        Dim DR As SqlClient.SqlDataReader = CMD.ExecuteReader
        If DR.HasRows Then
            While DR.Read
                If Accs(UBound(Accs)) <> "" Then ReDim Preserve Accs(UBound(Accs) + 1)
                Accs(UBound(Accs)) = DR("AccID").ToString
            End While
        End If
        DR.Close()
        CMD.Dispose()
        cnn.Close()
        Return Accs
    End Function

    Private Sub populateCandidates()
        Dim Span As Integer = SystemConfig.SendoutSpan
        Dim MinDate As Date = DateAdd(DateInterval.Day, -Span, CDate(Format(dtpDate.Value, SystemConfig.DateFormat)))
        Dim ThisDate As Date = CDate(Format(dtpDate.Value, SystemConfig.DateFormat & " 23:59:00"))
        dgvCandidates.Rows.Clear()
        Dim cnpc As New SqlClient.SqlConnection(connString)
        cnpc.Open()
        Dim cmdpc As New SqlCommand("GetOSCandidates_SP", cnpc)
        cmdpc.CommandTimeout = 120
        cmdpc.CommandType = CommandType.StoredProcedure
        cmdpc.Parameters.AddWithValue("@FDate", MinDate)
        cmdpc.Parameters.AddWithValue("@TDate", ThisDate)
        Dim drpc As SqlClient.SqlDataReader = cmdpc.ExecuteReader
        If drpc.HasRows Then
            While drpc.Read
                dgvCandidates.Rows.Add(drpc("Accid"), drpc("Dated"),
                drpc("Provider"), drpc("Patient"), drpc("Billee"))
            End While
        End If
        cnpc.Close()
        cnpc = Nothing
        '
        lblCandidates.Text = "Candidates [ " & dgvCandidates.RowCount & " ]"
    End Sub

    Private Function GetTimeSensitives() As String
        Dim TGPS As String = ""
        Dim sSQL As String = "Select * from TimeSensitiveTests where Company_ID = " & MyLab.ID
        Dim cnt As New SqlClient.SqlConnection(connString)
        cnt.Open()
        Dim cmdt As New SqlCommand(sSQL, cnt)
        cmdt.CommandType = CommandType.Text
        Dim DRt As SqlDataReader = cmdt.ExecuteReader
        If DRt.HasRows Then
            While DRt.Read
                TGPS += DRt("TGP_ID").ToString & ", "
            End While
            If TGPS.EndsWith(", ") Then TGPS =
            Microsoft.VisualBasic.Mid(TGPS, 1, Len(TGPS) - 2)
        End If
        cnt.Close()
        cnt = Nothing
        Return TGPS
    End Function

    Private Function GetAccDate(ByVal ACCID As Long) As Date
        Dim AccDate As Date = Date.Now
        Dim sSQL As String = "Select AccessionDate from Requisitions where ID = " & ACCID

        Dim cnn As New SqlClient.SqlConnection(connString)
        cnn.Open()
        Dim CMD As New SqlCommand(sSQL, cnn)
        CMD.CommandType = CommandType.Text
        Dim DR As SqlDataReader
        DR = CMD.ExecuteReader
        If DR.HasRows Then
            While DR.Read
                AccDate = DR("AccessionDate")
            End While
        End If
        CMD.Dispose()
        DR.Close()
        cnn.Close()
        Return AccDate
    End Function

    Private Function GetProvider(ByVal AccID As Long) As String
        Dim Provider As String = ""
        Dim sSQL As String = "Select * from Providers where ID in (Select " &
        "OrderingProvider_ID from Requisitions where ID = " & AccID & ")"
        Dim cnn As New SqlClient.SqlConnection(connString)
        cnn.Open()
        Dim selCMD As New SqlCommand(sSQL, cnn)
        selCMD.CommandType = CommandType.Text
        Dim selDR As SqlDataReader = selCMD.ExecuteReader
        If selDR.HasRows Then
            While selDR.Read
                If selDR("IsIndividual") IsNot DBNull.Value AndAlso selDR("IsIndividual") = 0 Then
                    Provider = selDR("LastName_BSN")
                Else
                    Provider = selDR("LastName_BSN") & ", " & selDR("FirstName")
                    If selDR("Degree") IsNot DBNull.Value _
                    AndAlso selDR("Degree") <> "" Then _
                    Provider += " " & selDR("Degree")
                End If
            End While
        End If
        selCMD.Dispose()
        selDR.Close()
        cnn.Close()
        Return Provider
    End Function

    Private Function GetPatient(ByVal AccID As Long) As String
        Dim Patient As String = ""
        Dim sSQL As String = "Select * from Patients where ID in (Select " &
        "Patient_ID from Requisitions where ID = " & AccID & ")"
        Dim cnn As New SqlClient.SqlConnection(connString)
        cnn.Open()
        Dim CMD As New SqlCommand(sSQL, cnn)
        CMD.CommandType = CommandType.Text
        Dim DR As SqlDataReader
        DR = CMD.ExecuteReader
        If DR.HasRows Then
            While DR.Read
                Patient = DR("LastName") & ", " & DR("FirstName") & " ; DOB: " &
                Format(DR("DOB"), SystemConfig.DateFormat) & " : Gender: " & DR("Sex")
            End While
        End If
        CMD.Dispose()
        DR.Close()
        cnn.Close()
        Return Patient
    End Function

    Private Function GetBillee(ByVal AccID As Long) As String
        Dim Billee As String = ""
        Dim sSQL As String = "Select * from Requisitions where ID = " & AccID
        '
        Dim cnn As New SqlClient.SqlConnection(connString)
        cnn.Open()
        Dim CMD As New SqlCommand(sSQL, cnn)
        CMD.CommandType = CommandType.Text
        Dim DS As New DataSet
        Dim DA As New SqlDataAdapter(CMD)
        DA.Fill(DS, "Billees")
        CMD.Dispose()
        cnn.Close()
        For i As Integer = 0 To DS.Tables(0).Rows.Count - 1
            If DS.Tables(0).Rows(i).Item("ID") IsNot System.DBNull.Value _
            AndAlso DS.Tables(0).Rows(i).Item("ID") > 0 Then
                If DS.Tables(0).Rows(i).Item("IsGratis") <> 0 Then
                    Billee = "Gratis"
                Else
                    If DS.Tables(0).Rows(i).Item("BillingType_ID") = 0 Then
                        Billee = "Provider"
                    ElseIf DS.Tables(0).Rows(i).Item("BillingType_ID") = 2 Then
                        Billee = "Patient"
                    ElseIf DS.Tables(0).Rows(i).Item("BillingType_ID") = 1 Then
                        If DS.Tables(0).Rows(i).Item("PrimePayer_ID") IsNot System.DBNull.Value _
                        AndAlso DS.Tables(0).Rows(i).Item("PrimePayer_ID") > 0 Then _
                        Billee = GetInsurance(DS.Tables(0).Rows(i).Item("PrimePayer_ID"))
                    Else
                        Billee = "Error"
                    End If
                End If
            End If
        Next
        Return Billee
    End Function

    Private Function GetInsurance(ByVal PayerID As Long) As String
        Dim Payer As String = ""
        Dim sSQL As String = "Select * from Payers where ID = " & PayerID
        '
        Dim cnn As New SqlClient.SqlConnection(connString)
        cnn.Open()
        Dim CMD As New SqlCommand(sSQL, cnn)
        CMD.CommandType = CommandType.Text
        Dim DR As SqlDataReader = CMD.ExecuteReader
        If DR.HasRows Then
            While DR.Read
                Payer = DR("PayerName")
            End While
        End If
        cnn.Close()
        cnn = Nothing
        Return Payer
    End Function

    Private Sub PopulateLabs()
        cmbLab.Items.Clear()
        Dim sSQL As String = "Select * from Labs where Active <> 0"
        '
        Dim cnn As New SqlClient.SqlConnection(connString)
        cnn.Open()
        Dim CMD As New SqlCommand(sSQL, cnn)
        CMD.CommandType = CommandType.Text
        Dim DR As SqlDataReader = CMD.ExecuteReader
        If DR.HasRows Then
            While DR.Read
                cmbLab.Items.Add(New MyList(DR("LabName") & IIf(DR("IsPrimary") _
                = 0, "", " *PRIMARY*"), DR("ID")))
            End While
        End If
        cnn.Close()
        cnn = Nothing
    End Sub

    Private Sub txtLabelQTY_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLabelQTY.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtReqQTY_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtReqQTY.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAccID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub dgvCandidates_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvCandidates.CellDoubleClick
        If e.RowIndex <> -1 Then
            'If cmbLab.SelectedIndex <> -1 Then
            Dim stopWatch As New Stopwatch()
            stopWatch.Start()
            cmbBillType.Items.Clear()
            If dgvCandidates.Rows(e.RowIndex).Cells(4).Value = "Insurance" Then
                cmbBillType.Items.Add(New MyList("Me", 0))
                cmbBillType.Items.Add(New MyList("Insurance", 1))
                cmbBillType.Items.Add(New MyList("Patient", 2))
                cmbBillType.Items.Add(New MyList("Gratis", 3))
            Else
                cmbBillType.Items.Add(New MyList("Me", 0))
                cmbBillType.Items.Add(New MyList("Patient", 2))
                cmbBillType.Items.Add(New MyList("Gratis", 3))
            End If
            DisplayCandidate(e.RowIndex)
            Update_Progress()
            stopWatch.Stop()
            lblStatus.Text = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            stopWatch.Elapsed.Hours, stopWatch.Elapsed.Minutes,
            stopWatch.Elapsed.Seconds, stopWatch.Elapsed.Milliseconds / 10)
            'Else
            '    MsgBox("Select the destination Reference Lab, first and try again", _
            '    MsgBoxStyle.Information, "Prolis")
            'End If
        End If
    End Sub

    Private Function AccOutsourced(ByVal AccID As Long) As Boolean
        Dim AOS As Boolean = False
        Dim sSQL As String = "Select InHouse from Requisitions where InHouse = 0 and ID = " & AccID
        '
        Dim cno As New Data.SqlClient.SqlConnection(connString)
        cno.Open()
        Dim cmdo As New Data.SqlClient.SqlCommand(sSQL, cno)
        cmdo.CommandType = CommandType.Text
        Dim DRo As Data.SqlClient.SqlDataReader = cmdo.ExecuteReader
        If DRo.HasRows Then AOS = True
        cno.Close()
        cno = Nothing
        Return AOS
    End Function

    Private Function SendoutExists(ByVal AccID As Long) As Boolean
        Dim Exist As Boolean = False
        Dim sSQL As String = "Select * from Sendouts where Accession_ID = " & AccID
        '
        Dim cnn As New Data.SqlClient.SqlConnection(connString)
        cnn.Open()
        Dim cmdsel As New Data.SqlClient.SqlCommand(sSQL, cnn)
        cmdsel.CommandType = CommandType.Text
        Dim DRsel As Data.SqlClient.SqlDataReader = cmdsel.ExecuteReader
        If DRsel.HasRows Then Exist = True
        cnn.Close()
        Return Exist
    End Function

    Private Sub ForceCandidate(ByVal AccID As Long)
        Dim ItemX As MyList = cmbLab.SelectedItem
        txtAccID.Text = AccID.ToString
        Dim AOS As Boolean = AccOutsourced(Val(txtAccID.Text))
        'Dim LabCompID() As String
        Dim TGPType As String = ""
        Dim TSTGPS As String = GetTimeSensitives()
        Dim TGPIH As Boolean = True
        Dim sSQL As String = ""
        Dim Billee = GetBillee(AccID)
        If (Billee <> "Patient" And Billee <> "Gratis" And
        Billee <> "Provider") And (AOS = True Or Billee = "Medicaid") Then
            cmbBillType.SelectedIndex = 1
        Else
            cmbBillType.SelectedIndex = 0
        End If
        txtNote.Text = GetAccessionComment(Val(txtAccID.Text))
        dgvOrders.Rows.Clear()
        dgvOrders.Rows.Add()
        'If cmbLab.SelectedIndex <> -1 Then
        Dim LabComponentID As String = ""
        Dim InC As String = ""
        '
        If TSTGPS <> "" Then
            sSQL = "Select r.Accession_ID as AccID, r.TGP_ID as TGPID from Req_TGP r where Not r.TGP_ID in (" & TSTGPS &
            ") and r.Accession_ID in (Select ID from Requisitions where InHouse = 0) and r.Accession_ID = " & AccID
        Else
            sSQL = "Select r.Accession_ID as AccID, r.TGP_ID as TGPID from Req_TGP r where r.Accession_ID " &
            "in (Select ID from Requisitions where InHouse = 0) and r.Accession_ID = " & AccID
        End If
        sSQL += " Union Select a.Accession_ID as AccID, a.TGP_ID as TGPID from Req_TGP a where a.TGP_ID in ((Select ID from Tests " &
        "where InHouse = 0) Union (Select ID from Groups where InHouse = 0) Union (Select ID from Profiles where InHouse = 0)) and " &
        "not a.TGP_ID in (Select TGP_ID from Sendout_TGP where Sendout_Id in (Select ID from SendOuts where Accession_ID = a.Accession_ID)) " &
        "and a.Accession_ID = " & AccID & " union Select b.Accession_ID as AccID, b.TGP_ID as TGPID from " &
        "Req_TGP b where b.TGP_ID in ((Select Profile_ID from Prof_GrpTst where Profile_ID in (Select ID from Profiles where InHouse <> 0) " &
        "and GrpTst_ID in (Select ID from Tests where InHouse = 0 union Select ID from Groups where InHouse = 0))) and not b.TGP_ID in (Select " &
        "TGP_ID from Sendout_TGP where Sendout_Id in (Select ID from SendOuts where Accession_ID = b.Accession_ID)) and b.Accession_ID = " &
        AccID & " Union Select e.Accession_ID as AccID, e.TGP_ID as TGPID from Req_TGP e where e.TGP_ID in " &
        "((Select Profile_ID from Prof_GrpTst where Profile_ID in (Select ID from Profiles where InHouse <> 0) and GrpTst_ID in (Select Group_ID " &
        "from Group_Test where Group_ID in (Select ID from Groups where InHouse <> 0) and Test_ID in (Select ID from Tests where InHouse = 0)))) " &
        "and not e.TGP_ID in (Select TGP_ID from Sendout_TGP where Sendout_Id in (Select ID from SendOuts where Accession_ID = e.Accession_ID)) " &
        "and e.Accession_ID = " & AccID & " Union Select d.Accession_ID as AccID, d.TGP_ID as TGPID from " &
        "Req_TGP d where d.TGP_ID in ((Select Group_ID from Group_Test where Group_ID in (Select ID from Groups where InHouse <> 0) and Test_ID " &
        "in (Select ID from Tests where InHouse = 0))) and not d.TGP_ID in (Select TGP_ID from Sendout_TGP where Sendout_Id in (Select ID from " &
        "SendOuts where Accession_ID = d.Accession_ID)) and d.Accession_ID = " & AccID & " Union Select " &
        "c.Accession_ID as AccID, c.Reflexed_ID as TGPID from Ref_Results c where c.Reflexed_ID in ((Select ID from Tests where InHouse = 0) " &
        "Union (Select ID from Groups where InHouse = 0) Union (Select ID from Profiles where InHouse = 0)) and not c.Reflexed_ID in (Select " &
        "TGP_ID from Sendout_TGP where Sendout_Id in (Select ID from SendOuts where Accession_ID = c.Accession_ID)) and c.Accession_ID = " &
        AccID & " Union Select c.Accession_ID as AccID, c.Test_ID as TGPID from Ref_Results c where " &
        "c.Test_ID in (Select ID from Tests where InHouse = 0) and not c.Test_ID in (Select TGP_ID from Sendout_TGP where Sendout_Id in (Select " &
        "ID from SendOuts where Accession_ID = c.Accession_ID)) and c.Accession_ID = " & AccID & " Union " &
        "Select c.Accession_ID as AccID, c.Info_ID as TGPID from Acc_Info_Results c where c.Info_ID in (Select ID from Tests where InHouse = 0) " &
        "and not c.Info_ID in (Select TGP_ID from Sendout_TGP where Sendout_Id in (Select ID from SendOuts where Accession_ID = " &
        "c.Accession_ID)) and c.Accession_ID = " & AccID
        '
        Dim cns1 As New SqlClient.SqlConnection(connString)
        cns1.Open()
        Dim cmds1 As New SqlClient.SqlCommand(sSQL, cns1)
        cmds1.CommandType = CommandType.Text
        Dim drs1 As SqlClient.SqlDataReader = cmds1.ExecuteReader
        If drs1.HasRows Then
            While drs1.Read
                If AOS = True Then  'TGP must go
                    TGPType = GetTGPType(drs1("TGPID"))
                    If TGPType = "P" Then   'Profile
                        If GetLabComponent(ItemX.ItemData, drs1("TGPID")) = "" Then
                            Dim cns2 As New SqlClient.SqlConnection(connString)
                            cns2.Open()
                            Dim cmds2 As New SqlClient.SqlCommand("Select GrpTst_ID from " &
                            "Prof_GrpTst where Profile_ID = " & drs1("TGPID"), cns2)
                            cmds2.CommandType = CommandType.Text
                            Dim drs2 As SqlClient.SqlDataReader = cmds2.ExecuteReader
                            If drs2.HasRows Then
                                While drs2.Read
                                    If GetTGPType(drs2("GrpTst_ID")) = "G" AndAlso
                                    GetLabComponent(ItemX.ItemData, drs2("GrpTst_ID")) = "" Then
                                        Dim cns3 As New SqlClient.SqlConnection(connString)
                                        cns3.Open()
                                        Dim cmds3 As New SqlClient.SqlCommand("Select Test_ID from " &
                                        "Group_Test where Group_ID = " & drs2("TGPID"), cns3)
                                        cmds3.CommandType = CommandType.Text
                                        Dim drs3 As SqlClient.SqlDataReader = cmds3.ExecuteReader
                                        If drs3.HasRows Then
                                            While drs3.Read
                                                If Not ComponentListed(drs3("Test_ID")) Then
                                                    dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(0).Value = drs3("Test_ID")
                                                    dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(2).Value = GetTGPName(drs3("Test_ID"))
                                                    dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(3).Value =
                                                    GetLabComponent(ItemX.ItemData, drs3("Test_ID"))
                                                    dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(4).Value = 0
                                                    dgvOrders.Rows.Add()
                                                End If
                                            End While
                                        End If
                                        cns3.Close()
                                        cns3 = Nothing
                                    Else    'Test within profile
                                        If Not ComponentListed(drs2("GrpTst_ID")) Then
                                            dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(0).Value = drs2("GrpTst_ID")
                                            dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(2).Value = GetTGPName(drs2("GrpTst_ID"))
                                            dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(3).Value =
                                            GetLabComponent(ItemX.ItemData, drs2("GrpTst_ID"))
                                            dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(4).Value = 0
                                            dgvOrders.Rows.Add()
                                        End If
                                    End If
                                End While
                            End If
                            cns2.Close()
                            cns2 = Nothing
                        Else
                            If Not ComponentListed(drs1("TGPID")) Then
                                dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(0).Value = drs1("TGPID")
                                dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(2).Value = GetTGPName(drs1("TGPID"))
                                dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(3).Value = GetLabComponent(ItemX.ItemData, drs1("TGPID"))
                                dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(4).Value = 0
                                dgvOrders.Rows.Add()
                            End If
                        End If
                    ElseIf TGPType = "G" Then   'group
                        If GetLabComponent(ItemX.ItemData, drs1("TGPID")) = "" Then
                            Dim cns4 As New SqlClient.SqlConnection(connString)
                            cns4.Open()
                            Dim cmds4 As New SqlClient.SqlCommand("Select Test_ID from " &
                            "Group_Test where Group_ID = " & drs1("TGPID"), cns4)
                            cmds4.CommandType = CommandType.Text
                            Dim drs4 As SqlClient.SqlDataReader = cmds4.ExecuteReader
                            If drs4.HasRows Then
                                While drs4.Read
                                    If Not ComponentListed(drs4("Test_ID")) Then
                                        dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(0).Value = drs4("Test_ID")
                                        dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(2).Value = GetTGPName(drs4("Test_ID"))
                                        dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(3).Value =
                                        GetLabComponent(ItemX.ItemData, drs4("Test_ID"))
                                        dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(4).Value = 0
                                        dgvOrders.Rows.Add()
                                    End If
                                End While
                            End If
                            cns4.Close()
                            cns4 = Nothing
                        Else    'Listed
                            If Not ComponentListed(drs1("TGPID")) Then
                                dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(0).Value = drs1("TGPID")
                                dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(2).Value = GetTGPName(drs1("TGPID"))
                                dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(3).Value =
                                GetLabComponent(ItemX.ItemData, drs1("TGPID"))
                                dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(4).Value = 0
                                dgvOrders.Rows.Add()
                            End If
                        End If
                    Else    'Test
                        If Not ComponentListed(drs1("TGPID")) Then
                            dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(0).Value = drs1("TGPID")
                            dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(2).Value = GetTGPName(drs1("TGPID"))
                            dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(3).Value =
                            GetLabComponent(ItemX.ItemData, drs1("TGPID"))
                            dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(4).Value = 0
                            dgvOrders.Rows.Add()
                        End If
                    End If
                Else    'Acc not outsourced
                    If IsTGPInHouse(drs1("TGPID")) = False Then  'not inhouse
                        If Not ComponentListed(drs1("TGPID")) Then
                            dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(0).Value = drs1("TGPID")
                            dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(2).Value = GetTGPName(drs1("TGPID"))
                            dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(3).Value = GetLabComponent(ItemX.ItemData, drs1("TGPID"))
                            dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(4).Value = 0
                            dgvOrders.Rows.Add()
                        End If
                    Else    'not outsourcable
                        TGPType = GetTGPType(drs1("TGPID"))
                        If TGPType = "G" Then   'Check group
                            Dim cns5 As New SqlClient.SqlConnection(connString)
                            cns5.Open()
                            Dim cmds5 As New SqlClient.SqlCommand("Select * from " &
                            "Group_Test where Group_ID = " & drs1("TGPID") &
                            " and Test_ID in (Select ID from Tests where Inhouse = 0)", cns5)
                            cmds5.CommandType = CommandType.Text
                            Dim drs5 As SqlClient.SqlDataReader = cmds5.ExecuteReader
                            If drs5.HasRows Then
                                While drs5.Read
                                    If Not ComponentListed(drs5("Test_ID")) Then
                                        dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(0).Value = drs5("Test_ID")
                                        dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(2).Value = GetTGPName(drs5("Test_ID"))
                                        dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(3).Value =
                                        GetLabComponent(ItemX.ItemData, drs5("Test_ID"))
                                        dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(4).Value = 0
                                        dgvOrders.Rows.Add()
                                    End If
                                End While
                            End If
                            cns5.Close()
                            cns5 = Nothing
                        Else    'Profile
                            Dim cns6 As New SqlClient.SqlConnection(connString)
                            cns6.Open()
                            Dim cmds6 As New SqlClient.SqlCommand("Select * from " &
                            "Prof_GrpTst where Profile_ID = " & drs1("TGPID"), cns6)
                            cmds6.CommandType = CommandType.Text
                            Dim drs6 As SqlClient.SqlDataReader = cmds6.ExecuteReader
                            If drs6.HasRows Then
                                While drs6.Read
                                    If IsTGPInHouse(drs6("GrpTst_ID")) = False Then
                                        If Not ComponentListed(drs6("GrpTst_ID")) Then
                                            dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(0).Value = drs6("GrpTst_ID")
                                            dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(2).Value = GetTGPName(drs6("GrpTst_ID"))
                                            dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(3).Value =
                                            GetLabComponent(ItemX.ItemData, drs6("GrpTst_ID"))
                                            dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(4).Value = 0
                                            dgvOrders.Rows.Add()
                                        End If
                                    Else    'Possible Group
                                        Dim cns7 As New SqlClient.SqlConnection(connString)
                                        cns7.Open()
                                        Dim cmds7 As New SqlClient.SqlCommand("Select * from Group_Test " &
                                        "where Group_ID = " & drs6("GrpTst_ID") &
                                        " and Test_ID in (Select ID from Tests where Inhouse = 0)", cns7)
                                        cmds7.CommandType = CommandType.Text
                                        Dim drs7 As SqlClient.SqlDataReader = cmds7.ExecuteReader
                                        If drs7.HasRows Then
                                            While drs7.Read
                                                If Not ComponentListed(drs7("Test_ID")) Then
                                                    dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(0).Value = drs7("Test_ID")
                                                    dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(2).Value = GetTGPName(drs7("Test_ID"))
                                                    dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(3).Value =
                                                    GetLabComponent(ItemX.ItemData, drs7("Test_ID"))
                                                    dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(4).Value = 0
                                                    dgvOrders.Rows.Add()
                                                End If
                                            End While
                                        End If
                                        cns7.Close()
                                        cns7 = Nothing
                                    End If
                                End While
                            End If
                            cns6.Close()
                            cns6 = Nothing
                        End If
                    End If
                End If
            End While
        End If
        cns1.Close()
        cns1 = Nothing
    End Sub

    Private Sub DisplayCandidate(ByVal RowID As Integer)
        If cmbLab.SelectedIndex = -1 Then   'no lab selected
            MsgBox("No Reference Lab selected. Select the lab " &
            "first and try again.", MsgBoxStyle.Critical, "Prolis")
            Exit Sub
        Else    'lab selected
            If SendoutExists(dgvCandidates.Rows(RowID).Cells(0).Value) Then
                MsgBox("The Sendout record for the accession " & txtAccID.Text & " has been created.")
                populateCandidates()
            Else
                Dim ItemX As MyList = cmbLab.SelectedItem
                txtAccID.Text = dgvCandidates.Rows(RowID).Cells(0).Value.ToString
                Dim AOS As Boolean = AccOutsourced(Val(txtAccID.Text))
                'Dim LabCompID() As String
                Dim TGPType As String = ""
                Dim TSTGPS As String = GetTimeSensitives()
                Dim TGPIH As Boolean = True
                Dim sSQL As String = ""
                If (dgvCandidates.Rows(RowID).Cells(4).Value.ToString <> "Patient" And
                dgvCandidates.Rows(RowID).Cells(4).Value.ToString <> "Gratis" And
                dgvCandidates.Rows(RowID).Cells(4).Value.ToString <> "Provider") And
                (AOS = True Or InStr(dgvCandidates.Rows(RowID).Cells(4).Value.ToString,
                "Medicaid") > 0) Then
                    cmbBillType.SelectedIndex = 1
                Else
                    cmbBillType.SelectedIndex = 0
                End If
                txtNote.Text = GetAccessionComment(Val(txtAccID.Text))
                dgvOrders.Rows.Clear()
                dgvOrders.Rows.Add()
                'If cmbLab.SelectedIndex <> -1 Then
                Dim LabComponentID As String = ""
                Dim InC As String = ""
                '
                If TSTGPS <> "" Then
                    sSQL = "Select r.Accession_ID as AccID, r.TGP_ID as TGPID from Req_TGP r where Not r.TGP_ID in (" & TSTGPS & ") and r.Accession_ID " &
                    "in (Select ID from Requisitions where InHouse = 0) and r.Accession_ID = " & dgvCandidates.Rows(RowID).Cells(0).Value
                Else
                    sSQL = "Select r.Accession_ID as AccID, r.TGP_ID as TGPID from Req_TGP r where r.Accession_ID in (Select ID from Requisitions " &
                    "where InHouse = 0) and r.Accession_ID = " & dgvCandidates.Rows(RowID).Cells(0).Value
                End If
                sSQL += " Union Select a.Accession_ID as AccID, a.TGP_ID as TGPID from Req_TGP a where a.TGP_ID in ((Select ID from Tests " &
                "where InHouse = 0) Union (Select ID from Groups where InHouse = 0) Union (Select ID from Profiles where InHouse = 0)) and " &
                "not a.TGP_ID in (Select TGP_ID from Sendout_TGP where Sendout_Id in (Select ID from SendOuts where Accession_ID = a.Accession_ID)) " &
                "and a.Accession_ID = " & dgvCandidates.Rows(RowID).Cells(0).Value & " union Select b.Accession_ID as AccID, b.TGP_ID as TGPID from " &
                "Req_TGP b where b.TGP_ID in ((Select Profile_ID from Prof_GrpTst where Profile_ID in (Select ID from Profiles where InHouse <> 0) " &
                "and GrpTst_ID in (Select ID from Tests where InHouse = 0 union Select ID from Groups where InHouse = 0))) and not b.TGP_ID in (Select " &
                "TGP_ID from Sendout_TGP where Sendout_Id in (Select ID from SendOuts where Accession_ID = b.Accession_ID)) and b.Accession_ID = " &
                dgvCandidates.Rows(RowID).Cells(0).Value & " Union Select e.Accession_ID as AccID, e.TGP_ID as TGPID from Req_TGP e where e.TGP_ID in " &
                "((Select Profile_ID from Prof_GrpTst where Profile_ID in (Select ID from Profiles where InHouse <> 0) and GrpTst_ID in (Select Group_ID " &
                "from Group_Test where Group_ID in (Select ID from Groups where InHouse <> 0) and Test_ID in (Select ID from Tests where InHouse = 0)))) " &
                "and not e.TGP_ID in (Select TGP_ID from Sendout_TGP where Sendout_Id in (Select ID from SendOuts where Accession_ID = e.Accession_ID)) " &
                "and e.Accession_ID = " & dgvCandidates.Rows(RowID).Cells(0).Value & " Union Select d.Accession_ID as AccID, d.TGP_ID as TGPID from " &
                "Req_TGP d where d.TGP_ID in ((Select Group_ID from Group_Test where Group_ID in (Select ID from Groups where InHouse <> 0) and Test_ID " &
                "in (Select ID from Tests where InHouse = 0))) and not d.TGP_ID in (Select TGP_ID from Sendout_TGP where Sendout_Id in (Select ID from " &
                "SendOuts where Accession_ID = d.Accession_ID)) and d.Accession_ID = " & dgvCandidates.Rows(RowID).Cells(0).Value & " Union Select " &
                "c.Accession_ID as AccID, c.Reflexed_ID as TGPID from Ref_Results c where c.Reflexed_ID in ((Select ID from Tests where InHouse = 0) " &
                "Union (Select ID from Groups where InHouse = 0) Union (Select ID from Profiles where InHouse = 0)) and not c.Reflexed_ID in (Select " &
                "TGP_ID from Sendout_TGP where Sendout_Id in (Select ID from SendOuts where Accession_ID = c.Accession_ID)) and c.Accession_ID = " &
                dgvCandidates.Rows(RowID).Cells(0).Value & " Union Select c.Accession_ID as AccID, c.Test_ID as TGPID from Ref_Results c where " &
                "c.Test_ID in (Select ID from Tests where InHouse = 0) and not c.Test_ID in (Select TGP_ID from Sendout_TGP where Sendout_Id in (Select " &
                "ID from SendOuts where Accession_ID = c.Accession_ID)) and c.Accession_ID = " & dgvCandidates.Rows(RowID).Cells(0).Value & " Union " &
                "Select c.Accession_ID as AccID, c.Info_ID as TGPID from Acc_Info_Results c where c.Info_ID in (Select ID from Tests where InHouse = 0) " &
                "and not c.Info_ID in (Select TGP_ID from Sendout_TGP where Sendout_Id in (Select ID from SendOuts where Accession_ID = " &
                "c.Accession_ID)) and c.Accession_ID = " & dgvCandidates.Rows(RowID).Cells(0).Value
                '
                Dim cns1 As New SqlClient.SqlConnection(connString)
                cns1.Open()
                Dim cmds1 As New SqlClient.SqlCommand(sSQL, cns1)
                cmds1.CommandType = CommandType.Text
                Dim drs1 As SqlClient.SqlDataReader = cmds1.ExecuteReader
                If drs1.HasRows Then
                    While drs1.Read
                        If AOS = True Then  'TGP must go
                            TGPType = GetTGPType(drs1("TGPID"))
                            If TGPType = "P" Then   'Profile
                                If GetLabComponent(ItemX.ItemData, drs1("TGPID")) = "" Then
                                    Dim cns2 As New SqlClient.SqlConnection(connString)
                                    cns2.Open()
                                    Dim cmds2 As New SqlClient.SqlCommand("Select GrpTst_ID from " &
                                    "Prof_GrpTst where Profile_ID = " & drs1("TGPID"), cns2)
                                    cmds2.CommandType = CommandType.Text
                                    Dim drs2 As SqlClient.SqlDataReader = cmds2.ExecuteReader
                                    If drs2.HasRows Then
                                        While drs2.Read
                                            If GetTGPType(drs2("GrpTst_ID")) = "G" AndAlso
                                            GetLabComponent(ItemX.ItemData, drs2("GrpTst_ID")) = "" Then
                                                Dim cns3 As New SqlClient.SqlConnection(connString)
                                                cns3.Open()
                                                Dim cmds3 As New SqlClient.SqlCommand("Select Test_ID from " &
                                                "Group_Test where Group_ID = " & drs2("GrpTst_ID"), cns3)
                                                cmds3.CommandType = CommandType.Text
                                                Dim drs3 As SqlClient.SqlDataReader = cmds3.ExecuteReader
                                                If drs3.HasRows Then
                                                    While drs3.Read
                                                        If Not ComponentListed(drs3("Test_ID")) Then
                                                            dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(0).Value = drs3("Test_ID")
                                                            dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(2).Value = GetTGPName(drs3("Test_ID"))
                                                            dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(3).Value =
                                                            GetLabComponent(ItemX.ItemData, drs3("Test_ID"))
                                                            dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(4).Value = 0
                                                            dgvOrders.Rows.Add()
                                                        End If
                                                    End While
                                                End If
                                                cns3.Close()
                                                cns3 = Nothing
                                            Else    'Test within profile
                                                If Not ComponentListed(drs2("GrpTst_ID")) Then
                                                    dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(0).Value = drs2("GrpTst_ID")
                                                    dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(2).Value = GetTGPName(drs2("GrpTst_ID"))
                                                    dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(3).Value =
                                                    GetLabComponent(ItemX.ItemData, drs2("GrpTst_ID"))
                                                    dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(4).Value = 0
                                                    dgvOrders.Rows.Add()
                                                End If
                                            End If
                                        End While
                                    End If
                                    cns2.Close()
                                    cns2 = Nothing
                                Else
                                    If Not ComponentListed(drs1("TGPID")) Then
                                        dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(0).Value = drs1("TGPID")
                                        dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(2).Value = GetTGPName(drs1("TGPID"))
                                        dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(3).Value = GetLabComponent(ItemX.ItemData, drs1("TGPID"))
                                        dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(4).Value = 0
                                        dgvOrders.Rows.Add()
                                    End If
                                End If
                            ElseIf TGPType = "G" Then   'group
                                If GetLabComponent(ItemX.ItemData, drs1("TGPID")) = "" Then
                                    Dim cns4 As New SqlClient.SqlConnection(connString)
                                    cns4.Open()
                                    Dim cmds4 As New SqlClient.SqlCommand("Select Test_ID from " &
                                    "Group_Test where Group_ID = " & drs1("TGPID"), cns4)
                                    cmds4.CommandType = CommandType.Text
                                    Dim drs4 As SqlClient.SqlDataReader = cmds4.ExecuteReader
                                    If drs4.HasRows Then
                                        While drs4.Read
                                            If Not ComponentListed(drs4("Test_ID")) Then
                                                dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(0).Value = drs4("Test_ID")
                                                dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(2).Value = GetTGPName(drs4("Test_ID"))
                                                dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(3).Value =
                                                GetLabComponent(ItemX.ItemData, drs4("Test_ID"))
                                                dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(4).Value = 0
                                                dgvOrders.Rows.Add()
                                            End If
                                        End While
                                    End If
                                    cns4.Close()
                                    cns4 = Nothing
                                Else    'Listed
                                    If Not ComponentListed(drs1("TGPID")) Then
                                        dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(0).Value = drs1("TGPID")
                                        dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(2).Value = GetTGPName(drs1("TGPID"))
                                        dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(3).Value =
                                        GetLabComponent(ItemX.ItemData, drs1("TGPID"))
                                        dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(4).Value = 0
                                        dgvOrders.Rows.Add()
                                    End If
                                End If
                            Else    'Test
                                If Not ComponentListed(drs1("TGPID")) Then
                                    dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(0).Value = drs1("TGPID")
                                    dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(2).Value = GetTGPName(drs1("TGPID"))
                                    dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(3).Value =
                                    GetLabComponent(ItemX.ItemData, drs1("TGPID"))
                                    dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(4).Value = 0
                                    dgvOrders.Rows.Add()
                                End If
                            End If
                        Else    'Acc not outsourced
                            If IsTGPInHouse(drs1("TGPID")) = False Then  'not inhouse
                                If Not ComponentListed(drs1("TGPID")) Then
                                    dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(0).Value = drs1("TGPID")
                                    dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(2).Value = GetTGPName(drs1("TGPID"))
                                    dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(3).Value = GetLabComponent(ItemX.ItemData, drs1("TGPID"))
                                    dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(4).Value = 0
                                    dgvOrders.Rows.Add()
                                End If
                            Else    'not outsourcable
                                TGPType = GetTGPType(drs1("TGPID"))
                                If TGPType = "G" Then   'Check group
                                    Dim cns5 As New SqlClient.SqlConnection(connString)
                                    cns5.Open()
                                    Dim cmds5 As New SqlClient.SqlCommand("Select * from " &
                                    "Group_Test where Group_ID = " & drs1("TGPID") &
                                    " and Test_ID in (Select ID from Tests where Inhouse = 0)", cns5)
                                    cmds5.CommandType = CommandType.Text
                                    Dim drs5 As SqlClient.SqlDataReader = cmds5.ExecuteReader
                                    If drs5.HasRows Then
                                        While drs5.Read
                                            If Not ComponentListed(drs5("Test_ID")) Then
                                                dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(0).Value = drs5("Test_ID")
                                                dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(2).Value = GetTGPName(drs5("Test_ID"))
                                                dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(3).Value =
                                                GetLabComponent(ItemX.ItemData, drs5("Test_ID"))
                                                dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(4).Value = 0
                                                dgvOrders.Rows.Add()
                                            End If
                                        End While
                                    End If
                                    cns5.Close()
                                    cns5 = Nothing
                                Else    'Profile
                                    Dim cns6 As New SqlClient.SqlConnection(connString)
                                    cns6.Open()
                                    Dim cmds6 As New SqlClient.SqlCommand("Select * from " &
                                    "Prof_GrpTst where Profile_ID = " & drs1("TGPID"), cns6)
                                    cmds6.CommandType = CommandType.Text
                                    Dim drs6 As SqlClient.SqlDataReader = cmds6.ExecuteReader
                                    If drs6.HasRows Then
                                        While drs6.Read
                                            If IsTGPInHouse(drs6("GrpTst_ID")) = False Then
                                                If Not ComponentListed(drs6("GrpTst_ID")) Then
                                                    dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(0).Value = drs6("GrpTst_ID")
                                                    dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(2).Value = GetTGPName(drs6("GrpTst_ID"))
                                                    dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(3).Value =
                                                    GetLabComponent(ItemX.ItemData, drs6("GrpTst_ID"))
                                                    dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(4).Value = 0
                                                    dgvOrders.Rows.Add()
                                                End If
                                            Else    'Possible Group
                                                Dim cns7 As New SqlClient.SqlConnection(connString)
                                                cns7.Open()
                                                Dim cmds7 As New SqlClient.SqlCommand("Select * from Group_Test " &
                                                "where Group_ID = " & drs6("GrpTst_ID") &
                                                " and Test_ID in (Select ID from Tests where Inhouse = 0)", cns7)
                                                cmds7.CommandType = CommandType.Text
                                                Dim drs7 As SqlClient.SqlDataReader = cmds7.ExecuteReader
                                                If drs7.HasRows Then
                                                    While drs7.Read
                                                        If Not ComponentListed(drs7("Test_ID")) Then
                                                            dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(0).Value = drs7("Test_ID")
                                                            dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(2).Value = GetTGPName(drs7("Test_ID"))
                                                            dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(3).Value =
                                                            GetLabComponent(ItemX.ItemData, drs7("Test_ID"))
                                                            dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(4).Value = 0
                                                            dgvOrders.Rows.Add()
                                                        End If
                                                    End While
                                                End If
                                                cns7.Close()
                                                cns7 = Nothing
                                            End If
                                        End While
                                    End If
                                    cns6.Close()
                                    cns6 = Nothing
                                End If
                            End If
                        End If
                    End While
                End If
                cns1.Close()
                cns1 = Nothing
            End If
        End If
    End Sub

    Private Function GetLabIDbyTGPID(ByVal TGPID As Integer) As String
        Dim LabID As String = ""
        Dim sSQL As String = "Select Lab_ID from Lab_TGP where TGP_ID = " & TGPID
        '
        Dim cnl As New Data.SqlClient.SqlConnection(connString)
        cnl.Open()
        Dim cmdl As New Data.SqlClient.SqlCommand(sSQL, cnl)
        cmdl.CommandType = CommandType.Text
        Dim DRl As Data.SqlClient.SqlDataReader = cmdl.ExecuteReader
        If DRl.HasRows Then
            While DRl.Read
                LabID = DRl("Lab_ID").ToString
            End While
        End If
        cnl.Close()
        cnl = Nothing
        Return LabID
    End Function

    Private Function GetAccessionComment(ByVal AccID As Long) As String
        Dim Note As String = ""
        Dim sSQL As String = "Select Comment from Requisitions where ID = " & AccID
        '
        Dim cnac As New SqlClient.SqlConnection(connString)
        cnac.Open()
        Dim cmdac As New SqlCommand(sSQL, cnac)
        cmdac.CommandType = CommandType.Text
        Dim DRac As SqlDataReader = cmdac.ExecuteReader
        If DRac.HasRows Then
            While DRac.Read
                If DRac("Comment") IsNot DBNull.Value _
                Then Note = Trim(DRac("Comment"))
            End While
        End If
        cnac.Close()
        cnac = Nothing
        Return Note
    End Function

    Private Function FindLabComponent(ByVal LabID As Integer, ByVal TGPID As Integer) As String()
        Dim LabCompID() As String = {"", ""}    '0=LabID, 1=CompID
        Dim sSQL As String = "Select * from Lab_TGP where Lab_ID = " & LabID & " and not " &
        "(LabComponentID is Null or LabComponentID = '') and TGP_ID = " & TGPID
        '
        Dim cnflc As New SqlClient.SqlConnection(connString)
        cnflc.Open()
        Dim cmdflc As New SqlCommand(sSQL, cnflc)
        cmdflc.CommandType = CommandType.Text
        Dim drflc As SqlDataReader = cmdflc.ExecuteReader
        If drflc.HasRows Then
            While drflc.Read
                LabCompID(0) = drflc("Lab_ID").ToString
                LabCompID(1) = drflc("LabComponentID").ToString
            End While
        End If
        cnflc.Close()
        cnflc = Nothing
        Return LabCompID
    End Function

    Private Function GetLabComponent(ByVal LabID As Integer, ByVal TGPID As Integer) As String
        Dim CompID As String = ""
        Dim sSQL As String = "Select * from Lab_TGP where Lab_ID = " & LabID & " and TGP_ID = " & TGPID
        '
        Dim cnlc As New SqlClient.SqlConnection(connString)
        cnlc.Open()
        Dim cmdlc As New SqlCommand(sSQL, cnlc)
        cmdlc.CommandType = CommandType.Text
        Dim drlc As SqlDataReader = cmdlc.ExecuteReader
        If drlc.HasRows Then
            While drlc.Read
                If drlc("LabComponentID") IsNot DBNull.Value _
                AndAlso Trim(drlc("LabComponentID")) <> "" Then
                    CompID = Trim(drlc("LabComponentID"))
                Else
                    CompID = drlc("TGP_ID").ToString
                End If
            End While
        End If
        cnlc.Close()
        cnlc = Nothing
        Return CompID
    End Function

    Private Function ComponentListed(ByVal TGPID As Integer) As Boolean
        Dim Listed As Boolean = False
        Dim i As Integer
        For i = 0 To dgvOrders.RowCount - 1
            If dgvOrders.Rows(i).Cells(0).Value = TGPID.ToString Then
                Listed = True
                Exit For
            End If
        Next
        If Listed = False Then
            For i = 0 To dgvOrders.RowCount - 1
                Dim TGPType As String = GetTGPType(Val(dgvOrders.Rows(i).Cells(0).Value))
                If TGPType = "G" Then
                    Dim cncl As New SqlClient.SqlConnection(connString)
                    cncl.Open()
                    Dim cmdcl As New SqlClient.SqlCommand("Select Test_ID from " &
                    "Group_Test where Group_ID = " & Val(dgvOrders.Rows(i).Cells(0).Value), cncl)
                    cmdcl.CommandType = CommandType.Text
                    Dim drcl As SqlClient.SqlDataReader = cmdcl.ExecuteReader
                    If drcl.HasRows Then
                        While drcl.Read
                            If drcl("Test_ID") = TGPID Then
                                Listed = True
                                Exit While
                            End If
                        End While
                    End If
                    cncl.Close()
                    cncl = Nothing
                End If
                If Listed = True Then Exit For
            Next
        End If
        Return Listed
    End Function

    Private Sub DisplaySendout(ByVal SendID As Long)
        Dim ItemX As MyList = Nothing
        Dim cnd1 As New SqlClient.SqlConnection(connString)
        cnd1.Open()
        Dim cmdd1 As New SqlClient.SqlCommand("Select a.*, b.Labels " &
        "from Sendouts a inner join Labs b on a.Lab_ID = b.ID where " &
        "a.ID = " & SendID, cnd1)
        cmdd1.CommandType = CommandType.Text
        Dim drd1 As SqlClient.SqlDataReader = cmdd1.ExecuteReader
        If drd1.HasRows Then
            While drd1.Read
                txtID.Text = drd1("ID")
                txtAccID.Text = drd1("Accession_ID")
                dtpDate.Value = drd1("SentDate")
                'cmbBillType.Items.Clear()
                GetbillType(drd1("Accession_ID"))
                'If GetbillType(drd1("Accession_ID")) = 1 Then
                '    cmbBillType.Items.Add(New MyList("Me", 0))
                '    cmbBillType.Items.Add(New MyList("Insurance", 1))
                '    cmbBillType.Items.Add(New MyList("Patient", 2))
                '    cmbBillType.Items.Add(New MyList("Gratis", 3))
                'Else
                '    cmbBillType.Items.Add(New MyList("Me", 0))
                '    cmbBillType.Items.Add(New MyList("Patient", 2))
                '    cmbBillType.Items.Add(New MyList("Gratis", 3))
                'End If
                For i As Integer = 0 To cmbBillType.Items.Count - 1
                    ItemX = cmbBillType.Items(i)
                    If drd1("BillingType_ID") = ItemX.ItemData _
                    AndAlso cmbBillType.Items(i).ToString = ItemX.Name Then
                        cmbBillType.SelectedIndex = i
                        Exit For
                    End If
                Next
                'If cmbBillType.SelectedIndex = -1 Then _
                'cmbBillType.SelectedIndex = 0
                txtLabelQTY.Text = drd1("Labels").ToString
                txtNote.Text = drd1("Note")
                '
                For i As Integer = 0 To cmbLab.Items.Count - 1
                    ItemX = cmbLab.Items(i)
                    If drd1("Lab_ID") = ItemX.ItemData Then
                        cmbLab.SelectedIndex = i
                        Exit For
                    End If
                Next
            End While
        End If
        cnd1.Close()
        cnd1 = Nothing
        '
        dgvOrders.Rows.Clear()
        dgvOrders.RowCount = 1
        Dim cnd2 As New SqlClient.SqlConnection(connString)
        cnd2.Open()
        Dim cmdd2 As New SqlClient.SqlCommand("Select * from " &
        "Sendout_TGP where Sendout_ID = " & SendID, cnd2)
        cmdd2.CommandType = CommandType.Text
        Dim drd2 As SqlClient.SqlDataReader = cmdd2.ExecuteReader
        If drd2.HasRows Then
            While drd2.Read
                dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(0).Value = drd2("TGP_ID")
                dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(2).Value = GetTGPName(drd2("TGP_ID"))
                dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(3).Value = GetLabComponent(ItemX.ItemData, drd2("TGP_ID"))
                dgvOrders.Rows(dgvOrders.RowCount - 1).Cells(4).Value = drd2("Stat")
                dgvOrders.Rows.Add()
            End While
        End If
        cnd2.Close()
        cnd2 = Nothing
        '
        Update_Progress()
    End Sub

    Private Sub Update_Progress()
        If dgvOrders.RowCount > 0 Then
            Dim Has As Boolean = False
            If txtID.Text <> "" And cmbLab.SelectedIndex <> -1 And cmbBillType.SelectedIndex <> -1 Then
                For i As Integer = 0 To dgvOrders.RowCount - 1
                    If dgvOrders.Rows(i).Cells(0).Value IsNot Nothing _
                    AndAlso dgvOrders.Rows(i).Cells(0).Value.ToString <> "" Then
                        Has = True
                        Exit For
                    End If
                Next
                If Has Then
                    btnSave.Enabled = True
                    btnDelete.Enabled = True
                Else
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                End If
            Else
                btnSave.Enabled = False
                btnDelete.Enabled = False
            End If
        End If
    End Sub

    Private Sub chkNewEdit_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkNewEdit.CheckedChanged
        ClearSendout()
        If chkNewEdit.Checked = False Then  'New
            chkNewEdit.Text = "New"
            chkNewEdit.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\new.ico")
            txtID.Text = GetNewSendoutID()
            dgvCandidates.Enabled = True
            btnSendoutLook.Enabled = False
        Else    'Edit
            chkNewEdit.Text = "Edit"
            chkNewEdit.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Edit.ico")
            txtID.Text = ""
            dgvCandidates.Enabled = False
            btnSendoutLook.Enabled = True
        End If
    End Sub

    Private Function GetNewSendoutID() As Long
        Dim NID As Long = 1
        Dim cnnx As New SqlClient.SqlConnection(connString)
        cnnx.Open()
        Dim cmdnx As New SqlClient.SqlCommand("Select Max(ID) as LastID from Sendouts", cnnx)
        cmdnx.CommandType = CommandType.Text
        Dim drnx As SqlClient.SqlDataReader = cmdnx.ExecuteReader
        If drnx.HasRows Then
            While drnx.Read
                If drnx("LastID") IsNot DBNull.Value _
                Then NID = drnx("LastID") + 1
            End While
        End If
        cnnx.Close()
        cnnx = Nothing
        Return NID
    End Function

    Private Sub ClearSendout()
        txtAccID.Text = "" : dtpDate.Value = Date.Now
        cmbBillType.SelectedIndex = 0 : txtNote.Text = "" : txtID.Text = ""
        cmbSendID.Text = ""
        cmbLab.SelectedIndex = -1
        dgvOrders.Rows.Clear() : dgvOrders.Rows.Add()
        chkPrintLabels.Checked = True : txtLabelQTY.Text = "1"
        chkPrintReq.Checked = True : txtReqQTY.Text = "1"
    End Sub

    Private Sub txtAccID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccID.Validated
        If txtAccID.Text <> "" Then
            If chkNewEdit.Checked = False Then  'New
                If Not IsAccValid(Val(txtAccID.Text)) Then  'Invalid accession
                    MsgBox("Your entry is not a valid accession", MsgBoxStyle.Critical, "Prolis")
                    cmbSendID.Items.Clear()
                    ClearSendout()
                Else
                    Dim Sendout(,) As String = GetSendout(Val(txtAccID.Text))
                    If Sendout(0, 0) <> "" Then 'Sendout exists
                        Dim RetVal As Integer = MsgBox("Sendout record " & Sendout(0, 0) & " exists that was routed to the lab " & Sendout(1, 0) &
                        " for the accession you entered. You may create another Sendout to a different lab (other than Lab# " & Sendout(1, 0) &
                        ") by clicking 'Yes' button. However, to edit the record of the lab# " & Sendout(1, 0) & ", change the routine's " &
                        "mode to 'Edit' and try again.", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Prolis")
                        If RetVal = vbYes Then
                            cmbBillType.SelectedIndex = GetbillType(Val(txtAccID.Text))
                            txtNote.Text = "" : dgvOrders.Rows.Clear() : dgvOrders.Rows.Add()
                        Else
                            txtAccID.Text = ""
                            txtAccID.Focus()
                        End If
                    Else    'Create New Sendout
                        cmbBillType.SelectedIndex = GetbillType(Val(txtAccID.Text))
                        txtNote.Text = "" : dgvOrders.Rows.Clear() : dgvOrders.Rows.Add()
                        Dim RetVal As Integer = MsgBox("There is no send out created for this accession. Either the accession " &
                        "does not possess any test configured to be outsourced or the accession has been resulted. Do you " &
                        "want to force the system to create the sendout?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
                        If RetVal = vbYes Then
                            ForceCandidate(Val(txtAccID.Text))
                        End If
                    End If
                End If
            Else
                Dim Sendout(,) As String = GetSendout(Val(txtAccID.Text))
                cmbSendID.Items.Clear()
                For i As Integer = 0 To UBound(Sendout, 2)
                    If Sendout(0, i) <> "" Then
                        cmbSendID.Items.Add(Sendout(0, i))
                    End If
                Next
            End If
        End If
        Update_Progress()
    End Sub

    Private Function GetbillType(ByVal AccID As Long) As Integer
        Dim BillType As Integer = 0
        Dim cnbt As New SqlClient.SqlConnection(connString)
        cnbt.Open()
        Dim cmdbt As New SqlClient.SqlCommand("Select * " &
        "from Requisitions where ID = " & AccID, cnbt)
        cmdbt.CommandType = CommandType.Text
        Dim drbt As SqlClient.SqlDataReader = cmdbt.ExecuteReader
        If drbt.HasRows Then
            While drbt.Read
                BillType = drbt("BillingType_ID")
            End While
        End If
        cnbt.Close()
        cnbt = Nothing
        'Update Bill Options
        'Me, Insurance, Patient, Gratis
        cmbBillType.Items.Clear()
        If BillType = 1 Then
            cmbBillType.Items.Add(New MyList("Me", 0))
            cmbBillType.Items.Add(New MyList("Insurance", 1))
            cmbBillType.Items.Add(New MyList("Patient", 2))
            cmbBillType.Items.Add(New MyList("Gratis", 3))
        Else
            cmbBillType.Items.Add(New MyList("Me", 0))
            cmbBillType.Items.Add(New MyList("Patient", 2))
            cmbBillType.Items.Add(New MyList("Gratis", 3))
        End If
        Return BillType
    End Function

    Private Function GetSendout(ByVal AccID As Long) As String(,)
        Dim Sendout(1, 0) As String
        Dim cngs As New SqlClient.SqlConnection(connString)
        cngs.Open()
        Dim cmdgs As New SqlClient.SqlCommand("Select * " &
        "from Sendouts where Accession_ID = " & AccID, cngs)
        cmdgs.CommandType = CommandType.Text
        Dim drgs As SqlClient.SqlDataReader = cmdgs.ExecuteReader
        If drgs.HasRows Then
            While drgs.Read
                If Sendout(1, UBound(Sendout, 2)) <> "" Then _
                ReDim Preserve Sendout(1, UBound(Sendout, 2) + 1)
                Sendout(0, UBound(Sendout, 2)) = drgs("ID")
                Sendout(1, UBound(Sendout, 2)) = drgs("Lab_ID")
            End While
        End If
        cngs.Close()
        cngs = Nothing
        Return Sendout
    End Function

    Private Function IsAccValid(ByVal AccID As Long) As Boolean
        Dim Valid As Boolean = False
        Dim cnv As New SqlClient.SqlConnection(connString)
        cnv.Open()
        Dim cmdv As New SqlClient.SqlCommand("Select * " &
        "from Requisitions where ID = " & AccID, cnv)
        cmdv.CommandType = CommandType.Text
        Dim drv As SqlClient.SqlDataReader = cmdv.ExecuteReader
        If drv.HasRows Then Valid = True
        cnv.Close()
        cnv = Nothing
        Return Valid
    End Function

    Private Sub btnSendoutLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendoutLook.Click
        Dim SendoutID As String = frmSendoutLookUp.ShowDialog
        If SendoutID <> "" Then
            DisplaySendout(Val(SendoutID))
            btnDelete.Enabled = True
        End If
        Update_Progress()
    End Sub

    Private Sub dgvOrders_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvOrders.CellClick
        If e.RowIndex <> -1 Then
            If e.ColumnIndex = 1 Then
                Dim TGPID As String = ""
                If System.Windows.Forms.DialogResult.OK = frmAccCompsLookUp.ShowDialog Then _
                    TGPID = frmAccCompsLookUp.Tag
                If TGPID <> "" Then
                    If Not ComponentListed(Val(TGPID)) Then
                        dgvOrders.Rows(e.RowIndex).Cells(0).Value = TGPID
                        dgvOrders.Rows(e.RowIndex).Cells(2).Value = GetTGPName(Val(TGPID))
                        dgvOrders.Rows(e.RowIndex).Cells(3).Value = ""
                        dgvOrders.Rows(e.RowIndex).Cells(4).Value = False
                        dgvOrders.Rows.Add()
                    End If
                End If
                Update_Progress()
            End If
        End If
    End Sub

    Private Sub dgvOrders_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvOrders.CellEndEdit
        On Error Resume Next
        If e.RowIndex <> -1 Then
            If e.ColumnIndex = 0 Then
                If dgvOrders.Rows(e.RowIndex).Cells(0).Value Is Nothing Then
                    dgvOrders.Rows(e.RowIndex).Cells(2).Value = ""
                    dgvOrders.Rows(e.RowIndex).Cells(3).Value = ""
                    dgvOrders.Rows(e.RowIndex).Cells(4).Value = False
                    If e.RowIndex < dgvOrders.RowCount - 1 Then dgvOrders.Rows.Remove(dgvOrders.Rows(e.RowIndex))
                Else
                    If AccComponentValid(Val(txtAccID.Text), Val(dgvOrders.Rows(e.RowIndex).Cells(0).Value)) Then
                        If Not ComponentDuplicate(e.RowIndex) Then
                            dgvOrders.Rows(e.RowIndex).Cells(2).Value =
                            GetTGPName(Val(dgvOrders.Rows(e.RowIndex).Cells(0).Value))
                            dgvOrders.Rows(e.RowIndex).Cells(3).Value = ""
                            dgvOrders.Rows(e.RowIndex).Cells(4).Value = False
                            dgvOrders.Rows.Add()
                        Else
                            MsgBox("Duplicate Component ID", MsgBoxStyle.Critical, "Prolis")
                            dgvOrders.Rows(e.RowIndex).Cells(0).Value = ""
                            dgvOrders.Rows(e.RowIndex).Cells(2).Value = ""
                            dgvOrders.Rows(e.RowIndex).Cells(3).Value = ""
                            dgvOrders.Rows(e.RowIndex).Cells(4).Value = False
                            If e.RowIndex < dgvOrders.RowCount - 1 And e.RowIndex > 0 Then dgvOrders.Rows.Remove(dgvOrders.Rows(e.RowIndex))
                        End If
                    Else
                        MsgBox("Invalid Component ID", MsgBoxStyle.Critical, "Prolis")
                        dgvOrders.Rows(e.RowIndex).Cells(0).Value = ""
                        dgvOrders.Rows(e.RowIndex).Cells(2).Value = ""
                        dgvOrders.Rows(e.RowIndex).Cells(3).Value = ""
                        dgvOrders.Rows(e.RowIndex).Cells(4).Value = False
                        If e.RowIndex < dgvOrders.RowCount - 1 And e.RowIndex > 0 Then dgvOrders.Rows.Remove(dgvOrders.Rows(e.RowIndex))
                    End If
                End If
            End If
            Update_Progress()
        End If
    End Sub

    Private Function AccComponentValid(ByVal AccID As Long, ByVal TGPID As Integer) As Boolean
        Dim Valid As Boolean = False
        Dim cnv As New SqlClient.SqlConnection(connString)
        cnv.Open()
        Dim cmdv As New SqlClient.SqlCommand("Select * from Req_TGP " &
        "where Accession_ID = " & AccID & " and TGP_ID = " & TGPID, cnv)
        cmdv.CommandType = CommandType.Text
        Dim drv As SqlClient.SqlDataReader = cmdv.ExecuteReader
        If drv.HasRows Then Valid = True
        cnv.Close()
        cnv = Nothing
        Return Valid
    End Function

    Private Function ComponentDuplicate(ByVal RowID As Integer) As Boolean
        Dim dup As Boolean = False
        Dim i As Integer
        Dim TGP As Integer = dgvOrders.Rows(RowID).Cells(0).Value
        For i = 0 To dgvOrders.RowCount - 1
            If i <> RowID And TGP.ToString = dgvOrders.Rows(i).Cells(0).Value Then
                dup = True
                Exit For
            End If
        Next
        Return dup
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtID.Text <> "" And txtAccID.Text <> "" And cmbLab.SelectedIndex <> -1 _
        And dgvOrders.RowCount > 0 AndAlso dgvOrders.Rows(0).Cells(0).Value.ToString <> "" Then
            SaveSendout()
            Dim ItemX As MyList = cmbLab.SelectedItem
            PerformInterfaceDisposition(Val(txtID.Text), ItemX.ItemData)
            ClearSendout()
            populateCandidates()
            If chkNewEdit.Checked = False Then txtID.Text = GetNewSendoutID()
        End If
    End Sub

    Private Sub SaveSendout()
        Dim ItemX As MyList = cmbLab.SelectedItem
        Dim ItemB As MyList = cmbBillType.SelectedItem
        Dim LabID As String = Format(ItemX.ItemData, "000")
        If chkNewEdit.Checked = False Then txtID.Text = GetNewSendoutID()
        Dim OSTGPS As String = ""
        'Dim sqlClean As String = "Delete from Sendout_TGP where Sendout_ID = " & Val(txtID.Text) & " and Not TGP_ID in ("
        If connString <> "" Then
            Dim cns As New SqlConnection(connString)
            cns.Open()
            Dim cmds As New SqlCommand("Sendouts_SP", cns)
            cmds.CommandType = CommandType.StoredProcedure
            cmds.Parameters.AddWithValue("@act", "Upsert")
            cmds.Parameters.AddWithValue("@ID", Val(txtID.Text))
            cmds.Parameters.AddWithValue("@Accession_ID", Val(txtAccID.Text))
            cmds.Parameters.AddWithValue("@SentDate", dtpDate.Value)
            cmds.Parameters.AddWithValue("@Lab_ID", ItemX.ItemData)
            'cmds.Parameters.AddWithValue("@Resulted", "")
            'cmds.Parameters.AddWithValue("@ResultDate", "")
            cmds.Parameters.AddWithValue("@Tech_ID", ThisUser.ID)
            cmds.Parameters.AddWithValue("@BillingType_ID", ItemB.ItemData)
            cmds.Parameters.AddWithValue("@Note", txtNote.Text)
            cmds.Parameters.AddWithValue("@RefLabNote", "")
            Try
                cmds.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            cns.Close()
            cns = Nothing
            '
            For i As Integer = 0 To dgvOrders.RowCount - 1
                If dgvOrders.Rows(i).Cells(0).Value IsNot Nothing AndAlso
                Trim(dgvOrders.Rows(i).Cells(0).Value.ToString) <> "" Then
                    Dim cnt As New SqlConnection(connString)
                    cnt.Open()
                    Dim cmdt As New SqlCommand("Sendout_TGP_SP", cnt)
                    cmdt.CommandType = CommandType.StoredProcedure
                    cmdt.Parameters.AddWithValue("@act", "Upsert")
                    cmdt.Parameters.AddWithValue("@Sendout_ID", Val(txtID.Text))
                    cmdt.Parameters.AddWithValue("@TGP_ID", Trim(dgvOrders.Rows(i).Cells(0).Value.ToString))
                    cmdt.Parameters.AddWithValue("@Ordinal", i)
                    'cmdt.Parameters.AddWithValue("@Resulted", "")
                    'cmdt.Parameters.AddWithValue("@ResultDate", "")
                    cmdt.Parameters.AddWithValue("@Stat", False)
                    cmdt.ExecuteNonQuery()
                    OSTGPS += dgvOrders.Rows(i).Cells(0).Value.ToString & ", "
                    cnt.Close()
                    cnt = Nothing
                End If
            Next
            If OSTGPS.EndsWith(", ") Then OSTGPS = Microsoft.VisualBasic.Mid(OSTGPS, 1, Len(OSTGPS) - 2)
            ExecuteSqlProcedure("Delete from Sendout_TGP where Sendout_ID = " & Val(txtID.Text) & " and Not TGP_ID in (" & OSTGPS & ")")
            '
            UpdateOSLabID(Val(txtAccID.Text), LabID)
            'Else    'odbc
            '    Dim cns As New Odbc.OdbcConnection(connstring)
            '    cns.Open()
            '    Dim cmds As New Odbc.OdbcCommand("Sendouts_SP", cns)
            '    cmds.CommandType = CommandType.StoredProcedure
            '    cmds.Parameters.AddWithValue("@act", "Upsert")
            '    cmds.Parameters.AddWithValue("@ID", Val(txtID.Text))
            '    cmds.Parameters.AddWithValue("@Accession_ID", Val(txtAccID.Text))
            '    cmds.Parameters.AddWithValue("@SentDate", dtpDate.Value)
            '    cmds.Parameters.AddWithValue("@Lab_ID", ItemX.ItemData)
            '    'cmds.Parameters.AddWithValue("@Resulted", "")
            '    'cmds.Parameters.AddWithValue("@ResultDate", "")
            '    cmds.Parameters.AddWithValue("@Tech_ID", ThisUser.ID)
            '    cmds.Parameters.AddWithValue("@BillingType_ID", cmbBillType.SelectedIndex)
            '    cmds.Parameters.AddWithValue("@Note", txtNote.Text)
            '    cmds.Parameters.AddWithValue("@RefLabNote", "")
            '    cmds.ExecuteNonQuery()
            '    cns.Close()
            '    cns = Nothing
            '    '
            '    For i As Integer = 0 To dgvOrders.RowCount - 1
            '        If dgvOrders.Rows(i).Cells(0).Value IsNot Nothing AndAlso
            '        Trim(dgvOrders.Rows(i).Cells(0).Value.ToString) <> "" Then
            '            Dim cnt As New Odbc.OdbcConnection(connstring)
            '            cnt.Open()
            '            Dim cmdt As New Odbc.OdbcCommand("Sendout_TGP_SP", cnt)
            '            cmdt.CommandType = CommandType.StoredProcedure
            '            cmdt.Parameters.AddWithValue("@act", "Upsert")
            '            cmdt.Parameters.AddWithValue("@Sendout_ID", Val(txtID.Text))
            '            cmdt.Parameters.AddWithValue("@TGP_ID", Trim(dgvOrders.Rows(i).Cells(0).Value.ToString))
            '            cmdt.Parameters.AddWithValue("@Ordinal", i)
            '            'cmdt.Parameters.AddWithValue("@Resulted", "")
            '            'cmdt.Parameters.AddWithValue("@ResultDate", "")
            '            cmdt.Parameters.AddWithValue("@Stat", False)
            '            cmdt.ExecuteNonQuery()
            '            OSTGPS += dgvOrders.Rows(i).Cells(0).Value.ToString & ", "
            '            cnt.Close()
            '            cnt = Nothing
            '        End If
            '    Next
            '    If OSTGPS.EndsWith(", ") Then OSTGPS = Microsoft.VisualBasic.Mid(OSTGPS, 1, Len(OSTGPS) - 2)
            '    ExecuteSqlProcedure("Delete from Sendout_TGP where Sendout_ID = " & Val(txtID.Text) & " and Not TGP_ID in (" & OSTGPS & ")")
            '    '
            '    UpdateOSLabID(Val(txtAccID.Text), LabID)
        End If

        Dim LabDocs() As String = GetLabDocuments(ItemX.ItemData)
        If chkPrintLabels.Checked = True Then
            If Val(txtLabelQTY.Text) > 0 Then
                If LabDocs(1) <> "" Then
                    PrintLabels(Val(txtID.Text), LabDocs(1), Val(txtLabelQTY.Text))
                Else
                    MsgBox("No Sendout Label defined in Ref Lab Record.", MsgBoxStyle.Critical, "Prolis")
                End If
            End If
        End If
        '
        If chkPrintReq.Checked = True Then
            If Val(txtReqQTY.Text) > 0 Then
                If LabDocs(0) <> "" Then
                    PrintLabDocument(Val(txtID.Text), LabDocs(0), Val(txtReqQTY.Text))
                Else
                    MsgBox("No Sendout Requisition defined in Ref Lab Record.", MsgBoxStyle.Critical, "Prolis")
                End If
            End If
        End If
    End Sub

    Private Function GetOSTestIDs(ByVal AccID As Long) As String()
        Dim TestIDs() As String = {""}
        Dim TGPType As String = ""
        Dim sSQL As String = "Select TGP_ID from Sendout_TGP where Sendout_ID in (Select ID from Sendouts where Accession_ID = " & AccID & ")"
        '
        Dim cns As New SqlClient.SqlConnection(connString)
        cns.Open()
        Dim cmds As New SqlClient.SqlCommand(sSQL, cns)
        cmds.CommandType = CommandType.Text
        Dim drs As SqlClient.SqlDataReader = cmds.ExecuteReader
        If drs.HasRows Then
            While drs.Read
                TGPType = GetTGPType(drs("TGP_ID"))
                If TGPType = "T" Then
                    If TestIDs(UBound(TestIDs)) <> "" Then ReDim Preserve TestIDs(UBound(TestIDs) + 1)
                    TestIDs(UBound(TestIDs)) = drs("TGP_ID").ToString
                ElseIf TGPType = "G" Then
                    Dim gSQL = "Select Test_ID from Group_Test where Group_ID = " & drs("TGP_ID")
                    Dim cngt As New SqlClient.SqlConnection(connString)
                    cngt.Open()
                    Dim cmdgt As New SqlClient.SqlCommand(gSQL, cngt)
                    cmdgt.CommandType = CommandType.Text
                    Dim drgt As SqlClient.SqlDataReader = cmdgt.ExecuteReader
                    If drgt.HasRows Then
                        While drgt.Read
                            If TestIDs(UBound(TestIDs)) <> "" Then ReDim Preserve TestIDs(UBound(TestIDs) + 1)
                            TestIDs(UBound(TestIDs)) = drgt("Test_ID").ToString
                        End While
                    End If
                    cngt.Close()
                    cngt = Nothing
                ElseIf TGPType = "P" Then
                    Dim pSQL As String = "Select GrpTst_ID from Prof_GrpTst where Profile_ID = " & drs("TGP_ID")
                    Dim cnp As New SqlClient.SqlConnection(connString)
                    cnp.Open()
                    Dim cmdp As New SqlClient.SqlCommand(pSQL, cnp)
                    cmdp.CommandType = CommandType.Text
                    Dim drp As SqlClient.SqlDataReader = cmdp.ExecuteReader
                    If drp.HasRows Then
                        While drp.Read
                            If GetTGPType(drp("GrpTst_ID")) = "T" Then
                                If TestIDs(UBound(TestIDs)) <> "" Then ReDim Preserve TestIDs(UBound(TestIDs) + 1)
                                TestIDs(UBound(TestIDs)) = drp("GrpTst_ID").ToString
                            Else    'Group
                                Dim tSQL As String = "Select Test_ID from Group_Test where Group_ID = " & drp("GrpTst_ID")
                                Dim cnt As New SqlClient.SqlConnection(connString)
                                cnt.Open()
                                Dim cmdt As New SqlClient.SqlCommand(tSQL, cnt)
                                cmdt.CommandType = CommandType.Text
                                Dim drt As SqlClient.SqlDataReader = cmdt.ExecuteReader
                                If drt.HasRows Then
                                    While drt.Read
                                        If TestIDs(UBound(TestIDs)) <> "" Then ReDim Preserve TestIDs(UBound(TestIDs) + 1)
                                        TestIDs(UBound(TestIDs)) = drt("Test_ID").ToString
                                    End While
                                End If
                                cnt.Close()
                                cnt = Nothing
                            End If
                        End While
                    End If
                    cnp.Close()
                    cnp = Nothing
                End If
            End While
        End If
        cns.Close()
        cns = Nothing
        Return TestIDs
    End Function

    Private Sub UpdateOSLabID(ByVal AccID As Long, ByVal LabID As String)
        Dim TestIDs() As String = GetOSTestIDs(AccID)
        For i As Integer = 0 To TestIDs.Length - 1
            If TestIDs(i) <> "" Then
                ExecuteSqlProcedure("Update Acc_Results set LabID = '" & LabID & "' where Test_ID = " & TestIDs(i) & " and Accession_ID = " & AccID)
                ExecuteSqlProcedure("Update Acc_Info_Results set LabID = '" & LabID & "' where Info_ID = " & TestIDs(i) & " and Accession_ID = " & AccID)
                ExecuteSqlProcedure("Update Ref_Results set LabID = '" & LabID & "' where Test_ID = " & TestIDs(i) & " and Accession_ID = " & AccID)
            End If
        Next
    End Sub

    Private Sub UpdateLabTGPs(ByVal LabID As Integer, ByVal TGPID As Integer, ByVal OrdID As String)
        ExecuteSqlProcedure("If Exists (Select * from Lab_TGP where TGP_ID = " & TGPID &
        " and Lab_ID = " & LabID & ") Update Lab_TGP set LabComponentID = '" & Trim(OrdID) &
        "' where TGP_ID = " & TGPID & " and Lab_ID = " & LabID & " Else Insert into " &
        "Lab_TGP (Lab_ID, TGP_ID, LabComponentID) values (" & LabID & ", " & TGPID &
        ", '" & Trim(OrdID) & "')")
    End Sub

    Private Sub PerformInterfaceDisposition(ByVal SendoutID As Long, ByVal LabID As Integer)
        Dim InterfaceID As String = GetInterfaceID(4, LabID)
        If InterfaceID <> "" Then
            Dim sSQL As String = "If Exists (Select * from Sendout_Disbursement where Sendout_ID = " &
            SendoutID & " and Interface_ID = " & InterfaceID & ") Update Sendout_Disbursement set " &
            "DisburseDate = '" & Date.Now & "', Routed = 0 where Sendout_ID = " & SendoutID & " and " &
            "Interface_ID = " & InterfaceID & " Else Insert into Sendout_Disbursement (Interface_ID, " &
            "Sendout_ID, DisburseDate, Routed) values (" & InterfaceID & ", " & SendoutID & ", '" & Date.Now & "', 0)"
            ExecuteSqlProcedure(sSQL)
        End If
    End Sub

    Private Sub PrintLabDocument(ByVal SendoutID As Long,
    ByVal DocFile As String, ByVal Qty As Integer)
        '=========================
        'TODO : Crystal Reports Code
        'Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        'Try
        '    oRpt.Load(DocFile)
        '    ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database, My.Settings.UID, My.Settings.PWD)
        '    oRpt.RecordSelectionFormula = "{Sendouts.ID} = " & SendoutID.ToString
        '    'orpt.PrintOptions
        '    oRpt.PrintOptions.PrinterName = GetDefaultPrinter()
        '    oRpt.PrintToPrinter(Qty, False, 0, 0)
        '    My.Application.DoEvents()
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.Critical, "Prolis")
        'Finally
        '    oRpt.Close()
        '    oRpt = Nothing
        'End Try
        '=========================

    End Sub

    Private Function GetLabDocuments(ByVal LabID As Integer) As String()
        Dim LabDocs() As String = {"", ""}
        Dim sSQL As String = "Select * from Labs where ID = " & LabID
        '
        Dim cnld As New SqlClient.SqlConnection(connString)
        cnld.Open()
        Dim cmdld As New SqlClient.SqlCommand(sSQL, cnld)
        cmdld.CommandType = CommandType.Text
        Dim drld As SqlClient.SqlDataReader = cmdld.ExecuteReader
        If drld.HasRows Then
            While drld.Read
                If drld("DocFile") IsNot DBNull.Value _
                AndAlso Trim(drld("DocFile")) <> "" Then
                    LabDocs(0) = ValidateLabelFile(drld("DocFile"))
                Else
                    If IO.File.Exists(My.Application.Info.DirectoryPath &
                    "\Reports\LabOrder.RPT") Then _
                    LabDocs(0) = My.Application.Info.DirectoryPath &
                    "\Reports\LabOrder.RPT"
                End If
                If drld("LabelFile") IsNot DBNull.Value _
                AndAlso Trim(drld("LabelFile")) <> "" Then
                    LabDocs(1) = ValidateLabelFile(drld("LabelFile"))
                Else
                    If IO.File.Exists(My.Application.Info.DirectoryPath &
                    "\Reports\DymoSnd1x2-18.Label") Then _
                    LabDocs(1) = My.Application.Info.DirectoryPath &
                    "\Reports\DymoSnd1x2-18.Label"
                End If
            End While
        End If
        Return LabDocs
    End Function

    Private Sub PrintLabels(ByVal SendoutID As Long, ByVal LabelFile As String, ByVal Labels As Integer)
        Dim Printer As String = GetLabelPrinterName()
        If Printer <> "" Then
            Dim LabelInfo() As String = GetLabelInfo(SendoutID)

            '======================================
            'TODO: Dymo Code
            'DymoAddIn = New DYMO.DymoAddIn
            'DymoLabel = New DYMO.DymoLabels
            'If DymoAddIn.Open(LabelFile) Then
            '    DymoLabel.SetField("ClientLab", LabelInfo(0))
            '    DymoLabel.SetField("Patient", LabelInfo(1))
            '    DymoLabel.SetField("Tests", LabelInfo(2))
            '    DymoLabel.SetField("SendoutID", LabelInfo(3))
            '    DymoLabel.SetField("AccDate", LabelInfo(4))
            '    DymoAddIn.SelectPrinter(Printer)
            '    DymoAddIn.Print(Labels, False)
            'Else
            '    MsgBox("Dymo Label file can not be opened", MsgBoxStyle.Critical, "Prolis")
            'End If
            'DymoAddIn = Nothing
            'DymoLabel = Nothing
            '======================================
        Else
            MsgBox("No Label printer configured", MsgBoxStyle.Critical, "Prolis")
        End If
    End Sub

    Private Function GetLabelInfo(ByVal SendoutID As Long) As String()
        Dim labelInfo() As String = {"", "", "", "", ""}
        Dim Acct As String = ""
        Dim TGPName As String = ""
        Dim sSQL As String = "Select * from Labs where ID in(Select Lab_ID from Sendouts where ID = " & SendoutID & ")"
        '
        Dim cnln1 As New SqlClient.SqlConnection(connString)
        cnln1.Open()
        Dim cmdln1 As New SqlClient.SqlCommand(sSQL, cnln1)
        cmdln1.CommandType = CommandType.Text
        Dim drln1 As SqlClient.SqlDataReader = cmdln1.ExecuteReader
        If drln1.HasRows Then
            While drln1.Read
                If drln1("Account") IsNot DBNull.Value _
                Then Acct = Trim(drln1("Account"))
            End While
        End If
        cnln1.Close()
        cnln1 = Nothing
        '
        sSQL = "Select * from Company where ID = 1"
        Dim cnln2 As New SqlClient.SqlConnection(connString)
        cnln2.Open()
        Dim cmdln2 As New SqlClient.SqlCommand(sSQL, cnln2)
        cmdln2.CommandType = CommandType.Text
        Dim drln2 As SqlClient.SqlDataReader = cmdln2.ExecuteReader
        If drln2.HasRows Then
            While drln2.Read
                If drln2("IsIndividual") = False Then
                    If Acct <> "" Then
                        labelInfo(0) = Acct & "-" & Trim(drln2("LastName_BSN"))
                    Else
                        labelInfo(0) = Trim(drln2("LastName_BSN"))
                    End If
                Else    'Individual
                    If Acct <> "" Then
                        If drln2("Degree") IsNot DBNull.Value _
                        AndAlso Trim(drln2("Degree")) <> "" Then
                            labelInfo(0) = Acct & "-" & Trim(drln2("LastName_BSN")) &
                            ", " & Trim(drln2("FirstName")) & " " & Trim(drln2("Degree"))
                        Else
                            labelInfo(0) = Acct & "-" & Trim(drln2("LastName_BSN")) &
                            ", " & Trim(drln2("FirstName"))
                        End If
                    Else
                        If drln2("Degree") IsNot DBNull.Value _
                        AndAlso Trim(drln2("Degree")) <> "" Then
                            labelInfo(0) = Trim(drln2("LastName_BSN")) & ", " &
                            Trim(drln2("FirstName")) & " " & Trim(drln2("Degree"))
                        Else
                            labelInfo(0) = Trim(drln2("LastName_BSN")) & ", " &
                            Trim(drln2("FirstName"))
                        End If
                    End If
                End If
            End While
        End If
        cnln2.Close()
        cnln2 = Nothing
        '
        sSQL = "Select * from Patients where ID in (Select Patient_ID from Requisitions " &
        "where ID in (Select Accession_ID from Sendouts where ID = " & SendoutID & "))"
        Dim cnln3 As New SqlClient.SqlConnection(connString)
        cnln3.Open()
        Dim cmdln3 As New SqlClient.SqlCommand(sSQL, cnln3)
        cmdln3.CommandType = CommandType.Text
        Dim drln3 As SqlClient.SqlDataReader = cmdln3.ExecuteReader
        If drln3.HasRows Then
            While drln3.Read
                labelInfo(1) = drln3("LastName") & ", " & drln3("FirstName") & " - " &
                Format(drln3("DOB"), SystemConfig.DateFormat) & " - " & drln3("Sex")
            End While
        End If
        cnln3.Close()
        cnln3 = Nothing
        '
        sSQL = "Select * from Lab_TGP where TGP_ID in (Select TGP_ID from Sendout_TGP where Sendout_ID = " & SendoutID & ")"
        Dim cnln4 As New SqlClient.SqlConnection(connString)
        cnln4.Open()
        Dim cmdln4 As New SqlClient.SqlCommand(sSQL, cnln4)
        cmdln4.CommandType = CommandType.Text
        Dim drln4 As SqlClient.SqlDataReader = cmdln4.ExecuteReader
        If drln4.HasRows Then
            While drln4.Read
                TGPName = Trim(GetTGPShortName(drln4("TGP_ID")))
                If drln4("LabComponentID") IsNot DBNull.Value _
                AndAlso Trim(drln4("LabComponentID")) <> "" Then
                    If InStr(labelInfo(2), Trim(drln4("LabComponentID")) & "-" & TGPName) = 0 _
                    Then labelInfo(2) += Trim(drln4("LabComponentID")) & "-" & TGPName & ", "
                Else
                    If InStr(labelInfo(2), TGPName) = 0 _
                    Then labelInfo(2) += TGPName & ", "
                End If
                TGPName = ""
            End While
            If labelInfo(2).EndsWith(", ") Then labelInfo(2) = Microsoft.VisualBasic.Mid(labelInfo(2), 1, Len(labelInfo(2)) - 2)
        End If
        cnln4.Close()
        cnln4 = Nothing
        '
        sSQL = "Select ID as AccID, AccessionDate as AccDate from Requisitions " &
        "where ID in (Select Accession_ID from Sendouts where ID = " & SendoutID & ")"
        Dim cnln5 As New SqlClient.SqlConnection(connString)
        cnln5.Open()
        Dim cmdln5 As New SqlClient.SqlCommand(sSQL, cnln5)
        cmdln5.CommandType = CommandType.Text
        Dim drln5 As SqlClient.SqlDataReader = cmdln5.ExecuteReader
        If drln5.HasRows Then
            While drln5.Read
                labelInfo(3) = drln5("AccID").ToString
                labelInfo(4) = Format(drln5("AccDate"), SystemConfig.DateFormat)
            End While
        End If
        cnln5.Close()
        cnln5 = Nothing
        Return labelInfo
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If txtID.Text <> "" And dgvOrders.RowCount > 0 Then
            If dgvOrders.Rows(0).Cells(0).Value IsNot Nothing AndAlso
            dgvOrders.Rows(0).Cells(0).Value.ToString <> "" Then
                Dim RetVal As Integer = MsgBox("Are you sure to delete the displayed Sendout?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
                If RetVal = vbYes Then
                    UpdateOSLabID(Val(txtAccID.Text), "")
                    ExecuteSqlProcedure("Delete from Sendout_TGP where Sendout_ID = " & Val(txtID.Text))
                    ExecuteSqlProcedure("Delete from Sendouts where ID = " & Val(txtID.Text))
                    cmbSendID.Items.Remove(txtID.Text)
                    ClearSendout()
                    populateCandidates()
                End If
            End If
        End If
    End Sub

    Private Sub cmbSendID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSendID.SelectedIndexChanged
        DisplaySendout(cmbSendID.SelectedItem.ToString)
        'btnDelete.Enabled = True
        'btnSave.Enabled = True
    End Sub

    Private Sub cmbBillType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBillType.SelectedIndexChanged
        Update_Progress()
    End Sub
End Class
