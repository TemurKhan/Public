Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Windows.Forms
Imports System.data

Public Class frmPrintPickUp

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        '((Not(IsNull({PickUps.ID}) and {Providers.PickUP} = 0)) or
        '{PickUps.PickupDate} in {?Report Date}) and {Routes.Courier} = 'LEO KRAMMER'
        '
        Dim Formula As String = "((Not(IsNull({PickUps.ID}) and {Providers.PickUP} = 0)) or " & _
        "{PickUps.PickupDate} in {?Report Date}) and {Routes.Courier} in ['"
        If lstCouriers.CheckedItems.Count > 0 Then
            Dim i As Integer
            For i = 0 To lstCouriers.CheckedItems.Count - 1
                Formula += lstCouriers.CheckedItems(i).ToString & "','"
            Next
            Formula = Microsoft.VisualBasic.Mid(Formula, 1, Len(Formula) - 2) & "];"
            Dim UID As String = My.Settings.UID.ToString
            Dim PWD As String = My.Settings.PWD.ToString

            '========================================
            'TODO: Crystal Reports Code
            'Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            'oRpt.Load(Application.StartupPath & "\Reports\PickUp.RPT")
            'oRpt.SetDatabaseLogon(UID, PWD)
            ''
            'Dim crParameterDiscreteValue As ParameterDiscreteValue
            'Dim crParameterFieldDefinitions As ParameterFieldDefinitions
            'Dim crParameterFieldLocation As ParameterFieldDefinition
            'Dim crParameterValues As ParameterValues
            'crParameterFieldDefinitions = oRpt.DataDefinition.ParameterFields
            'crParameterFieldLocation = crParameterFieldDefinitions.Item("Report Date")
            'crParameterValues = crParameterFieldLocation.CurrentValues
            'crParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
            'crParameterDiscreteValue.Value = dtpDate.Value.ToString
            'crParameterValues.Add(crParameterDiscreteValue)
            'crParameterFieldLocation.ApplyCurrentValues(crParameterValues)
            ''
            'oRpt.RecordSelectionFormula = Formula
            'If cmbDestination.SelectedIndex = 0 Then
            '    oRpt.PrintToPrinter(1, False, 0, 0)
            'Else
            '    frmRV.CRRV.ReportSource = oRpt
            '    frmRV.CRRV.ParameterFieldInfo.Item(0).HasCurrentValue = True
            '    frmRV.Show()
            '    frmRV.MdiParent = ProlisQC
            'End If
            '========================================

        End If
    End Sub

    Private Sub btnSelAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelAll.Click
        Dim i As Integer
        For i = 0 To lstCouriers.Items.Count - 1
            lstCouriers.SetItemChecked(i, True)
        Next
    End Sub

    Private Sub btnDeSelAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeSelAll.Click
        Dim i As Integer
        For i = 0 To lstCouriers.Items.Count - 1
            lstCouriers.SetItemChecked(i, False)
        Next
    End Sub

    Private Sub frmPrintPickUp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbDestination.SelectedIndex = 0
        PopulateCouriers()
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub PopulateCouriers()
        lstCouriers.Items.Clear()
        Dim cnsp As New SqlClient.SqlConnection(connString)
        cnsp.Open()
        Dim cmdsp As New SqlClient.SqlCommand("Select Distinct Courier from Routes", cnsp)
        cmdsp.CommandType = CommandType.Text
        Dim drsp As SqlClient.SqlDataReader = cmdsp.ExecuteReader
        If drsp.HasRows Then
            While drsp.Read
                lstCouriers.Items.Add(drsp("Courier"))
            End While
        End If
        cnsp.Close()
        cnsp = Nothing
        btnSelAll_Click(Nothing, Nothing)
    End Sub
End Class
