Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmViewBARHistory

    Private Sub frmViewBARHistory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If frmBillingEdit.txtAccessionID.Text <> "" Then
            LoadBARHistory(Val(frmBillingEdit.txtAccessionID.Text))
        End If
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub LoadBARHistory(ByVal AccID As Long)
        dgvHistory.Rows.Clear()
        Dim AccVALS() As String = GetAccVALS(AccID) 'Date, ProvID, doctor, PatID, Patient
        txtAccID.Text = AccID.ToString
        txtAccDate.Text = AccVALS(0)
        txtPatientID.Text = AccVALS(3)
        txtPatient.Text = AccVALS(4)
        txtProviderID.Text = AccVALS(1)
        txtProvider.Text = AccVALS(2)
        '
        Dim BAREvent As String = ""
        Dim ArParty As String = ""
        Dim ArType As String = ""
        Dim cnbar As New SqlConnection(connString)
        cnbar.Open()
        Dim cmdbar As New SqlCommand("Select * from " &
        "REQ_BAR_HISTORY where Accession_ID = " & AccID, cnbar)
        cmdbar.CommandType = CommandType.Text
        Dim drbar As SqlDataReader = cmdbar.ExecuteReader
        If drbar.HasRows Then
            While drbar.Read
                If drbar("BAR_Event_Type_ID") = 1 Then
                    BAREvent = "Bill"
                ElseIf drbar("BAR_Event_Type_ID") = 2 Then
                    BAREvent = "Reverse"
                ElseIf drbar("BAR_Event_Type_ID") = 3 Then
                    BAREvent = "Payment"
                ElseIf drbar("BAR_Event_Type_ID") = 4 Then
                    BAREvent = "Adjustment"
                ElseIf drbar("BAR_Event_Type_ID") = 5 Then
                    BAREvent = "837 Output"
                ElseIf drbar("BAR_Event_Type_ID") = 6 Then
                    BAREvent = "Paper Output"
                Else
                    BAREvent = "Undefined"
                End If
                '
                If drbar("ArType_ID") = 0 Then
                    ArType = "Client"
                ElseIf drbar("ArType_ID") = 1 Then
                    ArType = "Insurance"
                ElseIf drbar("ArType_ID") = 2 Then
                    ArType = "Patient"
                Else
                    ArType = "Clearing House"
                End If
                '
                ArParty = GetArParty(drbar("ArType_ID"), drbar("Ar_ID"))
                '
                dgvHistory.Rows.Add(BAREvent, Format(drbar("BAR_Event_Date"), SystemConfig.DateFormat),
                ArParty, ArType, Trim(drbar("Doc_No")), Format(drbar("Amount"),
                "0.00"), GetUserName(drbar("Performed_By")))
            End While
        End If
        cnbar.Close()
        cnbar = Nothing
    End Sub

    Private Function GetArParty(ByVal ArTypeID As Int16, ByVal ArID As Long) As String
        Dim Party As String = ""
        Dim sSQL As String = ""
        If ArTypeID = 0 Then    'Client
            sSQL = "Select * from Providers where ID = " & ArID
        ElseIf ArTypeID = 1 Then    'Insurance
            sSQL = "Select * from Payers where ID = " & ArID
        Else
            sSQL = "Select * from Patients where ID = " & ArID
        End If
        Dim cnar As New SqlConnection(connString)
        cnar.Open()
        Dim cmdar As New SqlCommand(sSQL, cnar)
        cmdar.CommandType = CommandType.Text
        Dim drar As SqlDataReader = cmdar.ExecuteReader
        If drar.HasRows Then
            While drar.Read
                If ArTypeID = 0 Then    'Client
                    If drar("IsIndividual") IsNot DBNull.Value AndAlso drar("IsIndividual") = 0 Then 'Entity
                        Party = drar("LastName_BSN") & " [" & ArID.ToString & "]"
                    Else
                        If drar("Degree") IsNot DBNull.Value _
                        AndAlso drar("Degree") <> "" Then
                            Party = drar("LastName_BSN") & ", " & drar("FirstName") _
                            & IIf(drar("MiddleName") Is DBNull.Value, "", " " &
                            drar("MiddleName")) & " " & drar("Degree") & " [" & ArID.ToString & "]"
                        Else
                            Party = drar("LastName_BSN") & ", " & drar("FirstName") _
                            & IIf(drar("MiddleName") Is DBNull.Value, "", " " &
                            drar("MiddleName")) & " [" & ArID.ToString & "]"
                        End If
                    End If
                ElseIf ArTypeID = 1 Then    'Insurance
                    Party = drar("PayerName") & " [" & ArID.ToString & "]"
                Else
                    If drar("Sex") = "M" Then
                        Party = drar("LastName") & ", " & drar("FirstName") & " [" & ArID.ToString &
                        "], DOB: " & Format(drar("DOB"), SystemConfig.DateFormat) & ", GENDER: Male"
                    ElseIf drar("Sex") = "F" Then
                        Party = drar("LastName") & ", " & drar("FirstName") & " [" & ArID.ToString &
                        "], DOB: " & Format(drar("DOB"), SystemConfig.DateFormat) & ", GENDER: Female"
                    Else
                        Party = drar("LastName") & ", " & drar("FirstName") & " [" & ArID.ToString &
                        "], DOB: " & Format(drar("DOB"), SystemConfig.DateFormat) & ", GENDER: Unknown"
                    End If
                End If
            End While
        End If
        cnar.Close()
        cnar = Nothing
        Return Party
    End Function

    Private Function GetAccVALS(ByVal AccID As Long) As String()
        'Date, ProvID, doctor, PatID, Patient
        Dim VALS() As String = {"", "", "", "", ""}
        Dim cngv As New SqlConnection(connString)
        cngv.Open()
        Dim cmdgv As New SqlCommand("Select a.Patient_ID, b.ID, a.AccessionDate as AccDate, b.LastName_BSN as PrLName, " &
        "b.FirstName as PrFName, b.MiddleName as PrMName, b.IsIndividual, b.Degree, c.LastName, c.FirstName, " &
        "c.MiddleName, c.Sex, c.DOB from Providers b inner join (Requisitions a inner join Patients c on " &
        "c.ID = a.Patient_ID) on a.OrderingProvider_ID = b.ID where a.ID = " & AccID, cngv)
        cmdgv.CommandType = CommandType.Text
        Dim drgv As SqlDataReader = cmdgv.ExecuteReader
        If drgv.HasRows Then
            While drgv.Read
                VALS(0) = Format(drgv("AccDate"), SystemConfig.DateFormat)
                VALS(1) = drgv("ID").ToString
                If drgv("IsIndividual") IsNot DBNull.Value AndAlso drgv("IsIndividual") = 0 Then 'Entity
                    VALS(2) = drgv("PrLName")
                Else
                    If drgv("Degree") IsNot DBNull.Value _
                    AndAlso drgv("Degree") <> "" Then
                        VALS(2) = drgv("PrLName") & ", " & drgv("PrFName") _
                        & IIf(drgv("PrMName") Is DBNull.Value, "", " " & _
                        drgv("PrMName")) & " " & drgv("Degree")
                    Else
                        VALS(2) = drgv("PrLName") & ", " & drgv("PrFName") _
                        & IIf(drgv("PrMName") Is DBNull.Value, "", " " & _
                        drgv("PrMName"))
                    End If
                End If
                '
                VALS(3) = drgv("Patient_ID")
                If drgv("Sex") = "M" Then
                    VALS(4) = drgv("LastName") & ", " & drgv("FirstName") & _
                    IIf(drgv("MiddleName") Is DBNull.Value, "", " " & _
                    drgv("MiddleName")) & ", DOB: " & Format(drgv("DOB"), _
                    SystemConfig.DateFormat) & ", GENDER: Male"
                ElseIf drgv("Sex") = "F" Then
                    VALS(4) = drgv("LastName") & ", " & drgv("FirstName") & _
                    IIf(drgv("MiddleName") Is DBNull.Value, "", " " & _
                    drgv("MiddleName")) & ", DOB: " & Format(drgv("DOB"), _
                    SystemConfig.DateFormat) & ", GENDER: Female"
                Else
                    VALS(4) = drgv("LastName") & ", " & drgv("FirstName") & _
                    IIf(drgv("MiddleName") Is DBNull.Value, "", " " & _
                    drgv("MiddleName")) & ", DOB: " & Format(drgv("DOB"), _
                    SystemConfig.DateFormat) & ", GENDER: Unknown"
                End If
            End While
        End If
        cngv.Close()
        cngv = Nothing
        Return VALS
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class
