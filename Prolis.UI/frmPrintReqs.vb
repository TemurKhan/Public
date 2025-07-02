Imports System.Windows.Forms
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmPrintReqs
    Private RptFile As String = ""

    Private Sub chkCusGen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCusGen.Click
        ClearForm()
        If chkCusGen.Checked = False Then   'Generic
            chkCusGen.Text = "Generic"
            chkCusGen.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\ReqGen.ico")
            RptFile = Application.StartupPath & "\Reports\GenReqForm.RPT"
            btnTForm.Enabled = True
            txtReqID.Enabled = True
            txtComment.Enabled = False
        Else
            chkCusGen.Text = "Custom"
            chkCusGen.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\ReqCus.ico")
            RptFile = Application.StartupPath & "\Reports\CusReqForm.RPT"
            btnTForm.Enabled = False
            txtReqID.Text = ""
            txtReqID.Enabled = False
            txtComment.Enabled = False
        End If
    End Sub

    Private Sub ClearForm()
        'txtProvID.Text = ""
        txtProvName.Text = ""
        txtAddress.Text = ""
        txtCSZ.Text = ""
        txtQty.Text = "1"
        txtReqID.Text = ""
        cmbAttend.SelectedIndex = -1
        cmbAttend.Items.Clear()
        txtPrintName.Text = ""
        txtComment.Text = ""
    End Sub

    Private Sub txtProvID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtProvID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtProvID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProvID.Validated
        ClearForm()
        If Trim(txtProvID.Text) <> "" _
        Then DisplayProvider(Trim(txtProvID.Text))
    End Sub

    Private Sub DisplayProvider(ByVal ProvID As Long)
        Dim Provider As String = ""
        Dim cnpp As New SqlConnection(connString)
        cnpp.Open()
        Dim cmdpp As New SqlCommand("Select " &
        "* from Providers where ID = " & ProvID, cnpp)
        cmdpp.CommandType = CommandType.Text
        Dim drpp As SqlDataReader = cmdpp.ExecuteReader
        If drpp.HasRows Then
            While drpp.Read
                If drpp("IsIndividual") IsNot DBNull.Value AndAlso drpp("IsIndividual") = 0 Then
                    Provider = drpp("LastName_BSN")
                Else
                    If drpp("Degree") Is DBNull.Value Then
                        Provider = drpp("LastName_BSN") & ", " &
                        drpp("FirstName")
                    Else
                        Provider = drpp("LastName_BSN") & ", " &
                        drpp("FirstName") & " " & drpp("Degree")
                    End If
                End If
                txtProvID.Text = ProvID
                txtProvName.Text = Provider
                txtAddress.Text = GetAddress1(drpp("Address_ID")) & " " &
                GetAddress2(drpp("Address_ID"))
                txtCSZ.Text = GetAddressCSZ(drpp("Address_ID"))
            End While
        End If
        cnpp.Close()
        cnpp = Nothing
        PopulateAttend(ProvID)
    End Sub

    Private Sub PopulateAttend(ByVal ClinicID As Long)
        cmbAttend.Items.Clear()
        Dim Provider As String = txtProvName.Text
        cmbAttend.Items.Add(New MyList(Provider, Val(txtProvID.Text)))
        Dim cnpa As New SqlConnection(connString)
        cnpa.Open()
        Dim cmdpa As New SqlCommand("Select * from " &
        "Providers where ID in (Select Provider_ID from " &
        "Clinic_Provider where Clinic_ID = " & ClinicID & ")", cnpa)
        cmdpa.CommandType = CommandType.Text
        Dim drpa As SqlDataReader = cmdpa.ExecuteReader
        If drpa.HasRows Then
            While drpa.Read
                If drpa("IsIndividual") IsNot DBNull.Value AndAlso drpa("IsIndividual") = 0 Then
                    Provider = drpa("LastName_BSN")
                Else
                    If drpa("Degree") Is DBNull.Value Then
                        Provider = drpa("LastName_BSN") & ", " &
                        drpa("FirstName")
                    Else
                        Provider = drpa("LastName_BSN") & ", " &
                        drpa("FirstName") & " " & drpa("Degree")
                    End If
                End If
                cmbAttend.Items.Add(New MyList(Provider, drpa("ID")))
            End While
        End If
        cnpa.Close()
        cnpa = Nothing
        cmbAttend.SelectedIndex = 0
    End Sub

    Private Sub cmbAttend_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAttend.SelectedIndexChanged
        If cmbAttend.SelectedIndex <> -1 Then
            Dim ItemX As MyList = cmbAttend.SelectedItem
            Dim ClinicID As Long = Val(txtProvID.Text)
            Dim PrintName As String = ""
            Dim cnpa As New SqlConnection(connString)
            cnpa.Open()
            Dim cmdpa As New SqlCommand("Select * " &
            "from Clinic_Provider where Clinic_ID = " & ClinicID &
            " and Provider_ID = " & ItemX.ItemData, cnpa)
            cmdpa.CommandType = CommandType.Text
            Dim drpa As SqlDataReader = cmdpa.ExecuteReader
            If drpa.HasRows Then
                While drpa.Read
                    If drpa("PrintName") IsNot DBNull.Value Then _
                   txtPrintName.Rtf = drpa("PrintName")
                    If drpa("Tests") IsNot DBNull.Value Then _
                    txtComment.Rtf = drpa("Tests")
                End While
            Else
                If ClinicID = ItemX.ItemData Then   'Individual
                    PrintName = Trim(txtProvName.Text)
                    PrintName += vbCrLf & Trim(txtAddress.Text)
                    PrintName += vbCrLf & Trim(txtCSZ.Text)
                    txtPrintName.Text = PrintName
                Else
                    PrintName = Trim(txtProvName.Text)
                    PrintName += vbCrLf & cmbAttend.Text
                    PrintName += vbCrLf & Trim(txtAddress.Text)
                    PrintName += vbCrLf & Trim(txtCSZ.Text)
                    txtPrintName.Text = PrintName
                End If
            End If
            cnpa.Close()
            cnpa = Nothing
            If txtPrintName.Text = "" Then
                If ClinicID = ItemX.ItemData Then   'Individual
                    PrintName = Trim(txtProvName.Text)
                    PrintName += vbCrLf & Trim(txtAddress.Text)
                    PrintName += vbCrLf & Trim(txtCSZ.Text)
                    txtPrintName.Text = PrintName
                Else
                    PrintName = Trim(txtProvName.Text)
                    PrintName += vbCrLf & cmbAttend.Text
                    PrintName += vbCrLf & Trim(txtAddress.Text)
                    PrintName += vbCrLf & Trim(txtCSZ.Text)
                    txtPrintName.Text = PrintName
                End If
            End If
            If chkCusGen.Checked = False Then
                txtReqID.Text = Format(Date.Now, "mmss")
            Else
                txtReqID.Text = ""
            End If
            btnPrint.Enabled = True
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If txtPrintName.Text <> "" And Val(txtQty.Text) > 0 And _
        txtProvID.Text <> "" And cmbAttend.SelectedIndex <> -1 Then
            Try
                Dim ItemX As MyList = cmbAttend.SelectedItem
                Dim UID As String = My.Settings.UID.ToString
                Dim PWD As String = My.Settings.PWD.ToString
                Dim i As Integer
                Dim RequisitionID As String = ""
                '

                'TODO: Crystal Reports
                '============================================
                'Dim pVal As ParameterDiscreteValue
                'Dim pFields As ParameterFieldDefinitions
                'Dim pField As ParameterFieldDefinition
                'Dim pVals As ParameterValues
                'SavePrintData(Val(txtProvID.Text), ItemX.ItemData)
                'If chkCusGen.Checked = False Then   'Generic
                '    For i = 0 To Val(txtQty.Text) - 1
                '        Dim oRpt As New ReportDocument
                '        oRpt.Load(RptFile)
                '        oRpt.SetDatabaseLogon(UID, PWD)
                '        pFields = oRpt.DataDefinition.ParameterFields
                '        '
                '        If txtReqID.Text = "" Then   'Label
                '            RequisitionID = "Blank"
                '        Else
                '            If Val(txtProvID.Text) = ItemX.ItemData Then
                '                RequisitionID = txtProvID.Text & "C" & (Val(txtReqID.Text) + i).ToString
                '            Else
                '                RequisitionID = txtProvID.Text & "P" & (Val(txtReqID.Text) + i).ToString
                '            End If
                '        End If
                '        pField = pFields.Item("RequisitionID")
                '        pVals = pField.CurrentValues
                '        pVal = New CrystalDecisions.Shared.ParameterDiscreteValue
                '        pVal.Value = RequisitionID
                '        pVals.Add(pVal)
                '        pField.ApplyCurrentValues(pVals)
                '        '
                '        pField = pFields.Item("ClinicID")
                '        pVals = pField.CurrentValues
                '        pVal = New CrystalDecisions.Shared.ParameterDiscreteValue
                '        pVal.Value = Val(txtProvID.Text)
                '        pVals.Add(pVal)
                '        pField.ApplyCurrentValues(pVals)
                '        '
                '        pField = pFields.Item("ProviderID")
                '        pVals = pField.CurrentValues
                '        pVal = New CrystalDecisions.Shared.ParameterDiscreteValue
                '        pVal.Value = ItemX.ItemData
                '        pVals.Add(pVal)
                '        pField.ApplyCurrentValues(pVals)
                '        oRpt.PrintToPrinter(1, False, 0, 0)
                '        My.Application.DoEvents()
                '        'System.Threading.Thread.Sleep(500)
                '        oRpt.Close()
                '        'End If
                '        oRpt = Nothing
                '    Next
                'Else    'Custom
                '    Dim oRpt As New ReportDocument
                '    oRpt.Load(RptFile)
                '    oRpt.SetDatabaseLogon(UID, PWD)
                '    pFields = oRpt.DataDefinition.ParameterFields
                '    '
                '    pField = pFields.Item("ClinicID")
                '    pVals = pField.CurrentValues
                '    pVal = New CrystalDecisions.Shared.ParameterDiscreteValue
                '    pVal.Value = Val(txtProvID.Text)
                '    pVals.Add(pVal)
                '    pField.ApplyCurrentValues(pVals)
                '    '
                '    pField = pFields.Item("ProviderID")
                '    pVals = pField.CurrentValues
                '    pVal = New CrystalDecisions.Shared.ParameterDiscreteValue
                '    pVal.Value = ItemX.ItemData
                '    pVals.Add(pVal)
                '    pField.ApplyCurrentValues(pVals)
                '    oRpt.PrintToPrinter(Val(txtQty.Text), False, 0, 0)
                '    My.Application.DoEvents()
                '    'System.Threading.Thread.Sleep(500)
                '    oRpt.Close()
                '    'End If
                '    oRpt = Nothing
                'End If
                '============================================
                '
            Catch Ex As Exception
                MsgBox(Ex.Message)
            End Try
        End If
    End Sub

    Private Sub SavePrintData(ByVal ClinicID As Long, ByVal ProviderID As Long)
        ExecuteSqlProcedure("If Exists (Select * from Clinic_Provider where Clinic_ID = " & _
        ClinicID & " and Provider_ID = " & ProviderID & ") Update Clinic_Provider set " & _
        "PrintName = '" & txtPrintName.Rtf & "', Tests = '" & txtComment.Rtf & "' where " & _
        "Clinic_ID = " & ClinicID & " and Provider_ID = " & ProviderID & " Else Insert " & _
        "into Clinic_Provider (Clinic_ID, Provider_ID, PrintName, Tests) values (" & ClinicID & _
        ", " & ProviderID & ", '" & txtPrintName.Rtf & "', '" & txtComment.Rtf & "')")
    End Sub

    Private Sub frmPrintReqs_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RptFile = Application.StartupPath & "\Reports\GenReqForm.RPT"
        txtQty.Text = "1"
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub txtQty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub btnNForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim FontDlg As New FontDialog
        If FontDlg.ShowDialog = DialogResult.OK Then
            txtPrintName.Font = FontDlg.Font
        End If
    End Sub

    Private Sub btnTForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTForm.Click
        Dim FontDlg As New FontDialog
        FontDlg.Font = txtComment.SelectionFont
        If FontDlg.ShowDialog = DialogResult.OK Then
            txtComment.SelectionFont = FontDlg.Font
        End If
        FontDlg.Dispose()
    End Sub

    Private Sub txtQty_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.Validated
        If txtQty.Text = "" Then txtQty.Text = "1"
    End Sub

    Private Sub btnProvLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProvLook.Click
        Dim ProvID As String = frmProviderLookup.ShowDialog
        If ProvID <> "" Then
            DisplayProvider(Val(ProvID))
        End If
    End Sub

    Private Sub txtReqID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtReqID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub btnNForm_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNForm.Click
        Dim FontDlg As New FontDialog
        FontDlg.Font = txtPrintName.SelectionFont
        If FontDlg.ShowDialog = DialogResult.OK Then
            txtPrintName.SelectionFont = FontDlg.Font
        End If
        FontDlg.Dispose()
    End Sub
End Class
