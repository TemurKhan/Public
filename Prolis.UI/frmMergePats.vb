Imports System.Windows.Forms
Imports System.data

Public Class frmMergePats
    Private IsCancelled As Boolean = True
    Private Dups(,) As String    '0=LName, 1=FName, 2=DOB, 3=Sex, 4=CNT

    Private Sub frmMergePats_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadDuplicatePatient()
        cmbKeep.SelectedIndex = 0
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If IsCancelled = True Then Me.Close()
        IsCancelled = True
    End Sub

    Private Sub LoadDuplicatePatient()
        dgvDupPat.Rows.Clear()
        Dim i As Long = 0
        Dim cndup As New SqlClient.SqlConnection(connString)
        cndup.Open()
        Dim cmddup As New SqlClient.SqlCommand("Select LastName, FirstName, " &
        "DOB, Sex, Count(LastName + FirstName) as PatCnt from Patients group " &
        "by LastName, FirstName, DOB, Sex having Count(LastName + FirstName) > 1", cndup)
        cmddup.CommandType = CommandType.Text
        Dim drdup As SqlClient.SqlDataReader = cmddup.ExecuteReader
        If drdup.HasRows Then
            ReDim Dups(4, i)
            While drdup.Read
                If i > UBound(Dups, 2) Then ReDim Preserve Dups(UBound(Dups, 1), i)
                Dups(0, i) = Trim(drdup("LastName"))
                Dups(1, i) = Trim(drdup("FirstName"))
                Dups(2, i) = Format(drdup("DOB"), SystemConfig.DateFormat)
                Dups(3, i) = Trim(drdup("Sex"))
                Dups(4, i) = Trim(drdup("PatCnt"))
                i += 1
            End While
        Else
            ReDim Dups(4, 0)
        End If
        cndup.Close()
        cndup = Nothing
        '
        If Dups(0, 0) <> "" Then
            txtCount.Text = UBound(Dups, 2) + 1
        Else
            txtCount.Text = "0"
        End If
        For i = 0 To UBound(Dups, 2)
            dgvDupPat.Rows.Clear()
            If Dups(0, i) <> "" Then
                LoadNextDuplicate(Dups(0, i), Dups(1, i), Dups(2, i), Dups(3, i))
                Dups(0, i) = "" : Dups(1, i) = "" : Dups(2, i) = ""
                Dups(3, i) = "" : Dups(4, i) = ""
                Exit For
            End If
        Next
        If dgvDupPat.RowCount = 0 Then MsgBox("Zero duplicate patients.", MsgBoxStyle.Exclamation, "Prolis")
    End Sub

    Private Sub LoadNextDuplicate(ByVal LName As String, ByVal FName As String, ByVal DOB As Date, ByVal Sex As String)
        Dim Address As String = ""
        Dim MName As String = ""
        Dim Coverage() As String
        Dim cnnd As New SqlClient.SqlConnection(connString)
        cnnd.Open()
        Dim cmdnd As New SqlClient.SqlCommand("Select * from Patients " &
        "where LastName = '" & LName & "' and FirstName = '" & FName &
        "' and DOB = '" & DOB & "' and Sex = '" & Sex & "'", cnnd)
        cmdnd.CommandType = CommandType.Text
        Dim drnd As SqlClient.SqlDataReader = cmdnd.ExecuteReader
        If drnd.HasRows Then
            While drnd.Read
                Coverage = GetPatientCoverage(drnd("ID"))
                If drnd("MiddleName") IsNot DBNull.Value _
                AndAlso Trim(drnd("MiddleName")) <> "" Then _
                MName = Microsoft.VisualBasic.Left(drnd("MiddleName"), 1)
                If drnd("Address_ID") IsNot DBNull.Value Then _
                Address = GetAddress(drnd("Address_ID"))
                dgvDupPat.Rows.Add(False, drnd("ID"), drnd("LastName"),
                drnd("FirstName"), MName, Format(drnd("DOB"), SystemConfig.DateFormat),
                Microsoft.VisualBasic.Left(drnd("Sex"), 1), Address, Coverage(0), Coverage(1))
            End While
        End If
        cnnd.Close()
        cnnd = Nothing
    End Sub

    Private Sub AddPatientToGrid(ByVal PatID As Long)
        Dim Address As String = ""
        Dim MName As String = ""
        Dim Coverage() As String = GetPatientCoverage(PatID)
        Dim cnptg As New SqlClient.SqlConnection(connString)
        cnptg.Open()
        Dim cmdptg As New SqlClient.SqlCommand("Select * from Patients where ID = " & PatID, cnptg)
        cmdptg.CommandType = CommandType.Text
        Dim drptg As SqlClient.SqlDataReader = cmdptg.ExecuteReader
        If drptg.HasRows Then
            While drptg.Read
                If drptg("MiddleName") IsNot DBNull.Value AndAlso
                drptg("MiddleName") <> "" Then _
                MName = Microsoft.VisualBasic.Left(drptg("MiddleName"), 1)
                If drptg("Address_ID") IsNot DBNull.Value Then _
                Address = GetAddress(drptg("Address_ID"))
                dgvDupPat.Rows.Add(False, drptg("ID"), drptg("LastName"),
                drptg("FirstName"), MName, Format(drptg("DOB"),
                SystemConfig.DateFormat), Microsoft.VisualBasic.Left(drptg("Sex"), 1),
                Address, Coverage(0), Coverage(1))
            End While
        End If
        cnptg.Close()
        cnptg = Nothing
    End Sub

    Private Function GetPatientCoverage(ByVal PatID As Long) As String()
        Dim COV(1) As String
        Dim cnptg As New SqlClient.SqlConnection(connString)
        cnptg.Open()
        Dim cmdptg As New SqlClient.SqlCommand("Select a.PolicyNo as " &
        "Policy, b.PayerName as Payer from Coverages a inner join " &
        "Payers b on a.Insurance_ID = b.ID where a.Patient_ID = " & PatID, cnptg)
        cmdptg.CommandType = CommandType.Text
        Dim drptg As SqlClient.SqlDataReader = cmdptg.ExecuteReader
        If drptg.HasRows Then
            While drptg.Read
                If drptg("Payer") IsNot DBNull.Value Then COV(0) = drptg("Payer")
                If drptg("Policy") IsNot DBNull.Value Then COV(1) = drptg("Policy")
            End While
        End If
        cnptg.Close()
        cnptg = Nothing
        Return COV
    End Function

    Private Sub btnLoadNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadNext.Click
        dgvDupPat.Rows.Clear()
        For i As Integer = 0 To UBound(Dups, 2)
            If Dups(0, i) <> "" Then
                LoadNextDuplicate(Dups(0, i), Dups(1, i), Dups(2, i), Dups(3, i))
                Dups(0, i) = "" : Dups(1, i) = "" : Dups(2, i) = ""
                Dups(3, i) = "" : Dups(4, i) = ""
                Exit For
            End If
        Next
        If dgvDupPat.RowCount = 0 Then MsgBox("Zero duplicate patients.", MsgBoxStyle.Exclamation, "Prolis")
    End Sub

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        Dim Kept As Long : Dim Remed As Long
        Dim Processed As Long = 0
        IsCancelled = False
        If chkAuto.Checked = False Then 'manual
            If dgvDupPat.RowCount = 0 Then  'no duplicate
                MsgBox("No record displayed", MsgBoxStyle.Critical, "Prolis")
            ElseIf dgvDupPat.Rows(0).Cells(0).Value = True And
            dgvDupPat.Rows(1).Cells(0).Value = True Then
                MsgBox("Improper 'Keep' selection", MsgBoxStyle.Critical, "Prolis")
            ElseIf dgvDupPat.Rows(0).Cells(0).Value = False And
            dgvDupPat.Rows(1).Cells(0).Value = False Then
                MsgBox("One 'Keep' selection required", MsgBoxStyle.Critical, "Prolis")
            ElseIf (dgvDupPat.Rows(0).Cells(0).Value = True And
            dgvDupPat.Rows(1).Cells(0).Value = False) Or
            (dgvDupPat.Rows(0).Cells(0).Value = False And
            dgvDupPat.Rows(1).Cells(0).Value = True) Then
                '
                If dgvDupPat.Rows(0).Cells(0).Value = True Then
                    Kept = Val(dgvDupPat.Rows(0).Cells(1).Value)
                    Remed = Val(dgvDupPat.Rows(1).Cells(1).Value)
                Else
                    Kept = Val(dgvDupPat.Rows(1).Cells(1).Value)
                    Remed = Val(dgvDupPat.Rows(0).Cells(1).Value)
                End If
                If chkTransAddress.Checked = True Then TransferAddress(Remed, Kept)
                ExecuteSqlProcedure("Update Requisitions set Patient_ID = " &
                Kept & " where Patient_ID = " & Remed)
                ExecuteSqlProcedure("Update Requisitions set PrimePayer_ID = " & Kept &
                " where Patient_ID = " & Remed & " and BillingType_ID = 2")
                ExecuteSqlProcedure("Update Req_Coverage set Insured_ID = " & Kept &
                " where Insured_ID = " & Remed & " and Relation = 0")
                If chkTransInsurance.Checked = True Then TransferInsurance(Remed, Kept)
                ExecuteSqlProcedure("Update Coverages set Insured_ID = " & Kept &
                " where Insured_ID = " & Remed)
                '
                ExecuteSqlProcedure("Update Payments set Ar_ID = " & Kept & " where ArType = 2 and Ar_ID = " & Remed)
                ExecuteSqlProcedure("Update Charges set Ar_ID = " & Kept & " where ArType = 2 and Ar_ID = " & Remed)
                ClientOrderPatTransfer(Remed, Kept)
                ExecuteSqlProcedure("Delete from Client_Order where Patient_ID = " & Remed)
                ClientOrderDxPatTransfer(Remed, Kept)
                ExecuteSqlProcedure("Delete from Client_Order_Dx where Patient_ID = " & Remed)
                ClientPatientTransfer(Remed, Kept)
                ExecuteSqlProcedure("Delete from Client_Patient where Patient_ID = " & Remed)
                ExecuteSqlProcedure("Delete from Patients where ID = " & Remed)
                dgvDupPat.Rows.Clear() : btnProcess.Enabled = False
                chkTransAddress.Checked = False : chkTransInsurance.Checked = False
                chkTransAddress.Enabled = True : chkTransInsurance.Enabled = True
                Processed += 1
                If Val(txtCount.Text) > 0 Then _
                txtCount.Text = (Val(txtCount.Text) - 1).ToString
            End If
        Else    'Auto
            btnProcess.Enabled = False
            btnLoadNext.Enabled = False
            Do Until dgvDupPat.RowCount = 0 Or IsCancelled = True
                If cmbKeep.SelectedIndex = 0 Then   'Keep earlier / smaller
                    Kept = Val(dgvDupPat.Rows(0).Cells(1).Value)
                    Remed = Val(dgvDupPat.Rows(1).Cells(1).Value)
                ElseIf cmbKeep.SelectedIndex = 1 Then   'Keep later / higher
                    Kept = Val(dgvDupPat.Rows(1).Cells(1).Value)
                    Remed = Val(dgvDupPat.Rows(0).Cells(1).Value)
                End If
                If chkTransAddress.Checked = True Then TransferAddress(Remed, Kept)
                ExecuteSqlProcedure("Update Requisitions set Patient_ID = " & Kept &
                " where Patient_ID = " & Remed)
                ExecuteSqlProcedure("Update Requisitions set PrimePayer_ID = " & Kept &
                " where Patient_ID = " & Remed & " and BillingType_ID = 2")
                ExecuteSqlProcedure("Update Req_Coverage set Insured_ID = " & Kept &
                " where Insured_ID = " & Remed & " and Relation = 0")
                If chkTransInsurance.Checked = True Then TransferInsurance(Remed, Kept)
                ExecuteSqlProcedure("Update Coverages set Insured_ID = " & Kept &
                " where Insured_ID = " & Remed)
                '
                ExecuteSqlProcedure("Update Payments set Ar_ID = " & Kept & " where ArType = 2 and Ar_ID = " & Remed)
                ExecuteSqlProcedure("Update Charges set Ar_ID = " & Kept & " where ArType = 2 and Ar_ID = " & Remed)
                ClientOrderPatTransfer(Remed, Kept)
                ExecuteSqlProcedure("Delete from Client_Order where Patient_ID = " & Remed)
                ClientOrderDxPatTransfer(Remed, Kept)
                ExecuteSqlProcedure("Delete from Client_Order_Dx where Patient_ID = " & Remed)
                ClientPatientTransfer(Remed, Kept)
                ExecuteSqlProcedure("Delete from Client_Patient where Patient_ID = " & Remed)
                ExecuteSqlProcedure("Delete from Patients where ID = " & Remed)
                '
                dgvDupPat.Rows.Clear()
                '
                LoadDuplicatePatient()
                My.Application.DoEvents()
                Threading.Thread.Sleep(1000)
                My.Application.DoEvents()
                Processed += 1
            Loop
            chkTransAddress.Checked = False : chkTransInsurance.Checked = False
            chkTransAddress.Enabled = True : chkTransInsurance.Enabled = True
            chkAuto.Checked = False : cmbKeep.SelectedIndex = 0
            btnLoadNext.Enabled = True
        End If
        IsCancelled = True
        MsgBox("System removed " & Processed & " duplicate patient(s)",
        MsgBoxStyle.Information, "Prolis")
        Processed = 0
    End Sub

    Protected Sub ClientPatientTransfer(ByVal FromPatID As Long, ByVal ToPatID As Long)
        Dim cnptg As New SqlClient.SqlConnection(connString)
        cnptg.Open()
        Dim cmdptg As New SqlClient.SqlCommand("Select * from " &
        "Client_Patient where Patient_ID = " & FromPatID, cnptg)
        cmdptg.CommandType = CommandType.Text
        Dim drptg As SqlClient.SqlDataReader = cmdptg.ExecuteReader
        If drptg.HasRows Then
            While drptg.Read
                Dim cnt As New SqlClient.SqlConnection(connString)
                cnt.Open()
                Dim cmdt As New SqlClient.SqlCommand("Client_Patient_SP", cnt)
                cmdt.CommandType = CommandType.StoredProcedure
                cmdt.Parameters.AddWithValue("@act", "Upsert")
                cmdt.Parameters.AddWithValue("@Provider_ID", drptg("Provider_ID"))
                cmdt.Parameters.AddWithValue("@Patient_ID", ToPatID)
                cmdt.Parameters.AddWithValue("@EMRNo", drptg("EMRNo"))
                cmdt.Parameters.AddWithValue("@Room", drptg("Room"))
                cmdt.Parameters.AddWithValue("@Shift", drptg("Shift"))
                cmdt.Parameters.AddWithValue("@ClientUser_ID", drptg("ClientUser_ID"))
                cmdt.Parameters.AddWithValue("@AdmitDate", drptg("AdmitDate"))
                cmdt.Parameters.AddWithValue("@DischargeDate", drptg("DischargeDate"))
                cmdt.Parameters.AddWithValue("@TestDays", drptg("TestDays"))
                cmdt.Parameters.AddWithValue("@AttendingProvider_ID", drptg("AttendingProvider_ID"))
                cmdt.Parameters.AddWithValue("@BillingType_ID", drptg("BillingType_ID"))
                cmdt.Parameters.AddWithValue("@Phleb_Loc", drptg("Phleb_Loc"))
                Try
                    cmdt.ExecuteNonQuery()
                Catch ex As Exception
                    MsgBox(ex.Message)
                Finally
                    cnt.Close()
                    cnt = Nothing
                End Try
            End While
        End If
        cnptg.Close()
        cnptg = Nothing
    End Sub

    Protected Sub ClientOrderDxPatTransfer(ByVal FromPatID As Long, ByVal ToPatID As Long)
        Dim cnptg As New SqlClient.SqlConnection(connString)
        cnptg.Open()
        Dim cmdptg As New SqlClient.SqlCommand("Select * from " &
        "Client_Order_Dx where Patient_ID = " & FromPatID, cnptg)
        cmdptg.CommandType = CommandType.Text
        Dim drptg As SqlClient.SqlDataReader = cmdptg.ExecuteReader
        If drptg.HasRows Then
            While drptg.Read
                ExecuteSqlProcedure("If Exists (Select * from Client_Order_Dx where Patient_ID = " &
                ToPatID & ") Update Client_Order_Dx set Provider_ID = " & drptg("Provider_ID") &
                ", Dx_Code = '" & drptg("Dx_Code") & "' where Patient_ID = " & ToPatID & " Else " &
                "Insert into Client_Order_Dx (Provider_ID, Patient_ID, Dx_Code) values (" &
                drptg("Provider_ID") & ", " & ToPatID & ", '" & drptg("Dx_Code") & "')")
            End While
        End If
        cnptg.Close()
        cnptg = Nothing
    End Sub

    Protected Sub ClientOrderPatTransfer(ByVal FromPatID As Long, ByVal ToPatID As Long)
        Dim cnptg As New SqlClient.SqlConnection(connString)
        cnptg.Open()
        Dim cmdptg As New SqlClient.SqlCommand("Select * from " &
        "Client_Order where Patient_ID = " & FromPatID, cnptg)
        cmdptg.CommandType = CommandType.Text
        Dim drptg As SqlClient.SqlDataReader = cmdptg.ExecuteReader
        If drptg.HasRows Then
            While drptg.Read
                Dim cnco As New SqlClient.SqlConnection(connString)
                cnco.Open()
                Dim cmdco As New SqlClient.SqlCommand("Client_Order_SP", cnco)
                cmdco.CommandType = CommandType.StoredProcedure
                cmdco.Parameters.AddWithValue("@act", "Upsert")
                cmdco.Parameters.AddWithValue("@Provider_ID", drptg("Provider_ID"))
                cmdco.Parameters.AddWithValue("@Patient_ID", ToPatID)
                cmdco.Parameters.AddWithValue("@TGP_ID", drptg("TGP_ID"))
                cmdco.Parameters.AddWithValue("@Order_Interval", drptg("Order_Interval"))
                cmdco.Parameters.AddWithValue("@IntervalQTY", drptg("IntervalQTY"))
                cmdco.Parameters.AddWithValue("@Active", drptg("Active"))
                cmdco.Parameters.AddWithValue("@StartDate", drptg("StartDate"))
                cmdco.Parameters.AddWithValue("@EndDate", drptg("EndDate"))
                cmdco.Parameters.AddWithValue("@Dx_Code", drptg("Dx_Code"))
                cmdco.Parameters.AddWithValue("@IsESRD", drptg("IsESRD"))
                Try
                    cmdco.ExecuteNonQuery()
                Catch ex As Exception
                    MsgBox(ex.Message)
                Finally
                    cnco.Close()
                    cnco = Nothing
                End Try
            End While
        End If
        cnptg.Close()
        cnptg = Nothing
    End Sub

    Private Sub TransferAddress(ByVal Remed As Long, ByVal Kept As Long)
        Dim AddID As Long = -1
        Dim cnptg As New SqlClient.SqlConnection(connString)
        cnptg.Open()
        Dim cmdptg As New SqlClient.SqlCommand("Select " & _
        "Address_ID from Patients where ID = " & Remed, cnptg)
        cmdptg.CommandType = CommandType.Text
        Dim drptg As SqlClient.SqlDataReader = cmdptg.ExecuteReader
        If drptg.HasRows Then
            While drptg.Read
                If drptg("Address_ID") IsNot DBNull.Value _
                Then AddID = drptg("Address_ID")
            End While
        End If
        cnptg.Close()
        cnptg = Nothing
        If AddID <> -1 Then
            ExecuteSqlProcedure("Update Patients set Address_ID = " & _
            AddID & " where ID = " & Kept)
        End If
    End Sub

    Private Sub TransferInsurance(ByVal Remed As Long, ByVal Kept As Long)
        ExecuteSqlProcedure("Update Coverages set Patient_ID = " & -99 & _
        " where Patient_ID = " & Kept)
        ExecuteSqlProcedure("Update Coverages set Patient_ID = " & Kept & _
        " where Patient_ID = " & Remed)
        ExecuteSqlProcedure("Delete from Coverages where Patient_ID = " & -99)
    End Sub

    Private Sub dgvDupPat_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDupPat.CellEnter
        If e.ColumnIndex = 0 Then btnProcess.Enabled = False
    End Sub

    Private Sub dgvDupPat_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDupPat.CellValidated
        If e.RowIndex <> -1 Then 'valid row
            If e.ColumnIndex = 0 Then   'keep
                Dim i As Integer
                Dim chkd As Integer = 0
                For i = 0 To dgvDupPat.RowCount - 1
                    If dgvDupPat.Rows(i).Cells(0).Value = True Then chkd += 1
                Next
                If chkd = 1 Or chkAuto.Checked Then
                    btnProcess.Enabled = True
                Else
                    btnProcess.Enabled = False
                End If
            End If
        End If
    End Sub

    Private Sub chkAuto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAuto.CheckedChanged
        If chkAuto.Checked = False Then 'Unlock Setting
            cmbKeep.Enabled = True
            chkTransAddress.Enabled = True
            chkTransInsurance.Enabled = True
        Else
            cmbKeep.Enabled = False
            chkTransAddress.Enabled = False
            chkTransInsurance.Enabled = False
        End If
        btnProcess.Enabled = True
    End Sub
End Class
