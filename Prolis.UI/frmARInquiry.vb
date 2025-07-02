Imports System.Data
Imports System.IO

Public Class frmARInquiry

    Private origWidth As Integer
    Private origHeight As Integer
    Private Shared AccList As List(Of String) = New List(Of String)

    Private Sub frmARInquiry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MaximumSize = MaxSize
        origWidth = Me.Width
        origHeight = Me.Height
        cmbARType.SelectedIndex = 3
        cmbDateType.SelectedIndex = 2
        cmbSearch.SelectedIndex = -1
        ConfigureDateTimePicker(dtpDateFrom)
        ConfigureDateTimePicker(dtpDateTo)
        If ThisUser.UserName.ToLower.Contains("techteam") Then
            'dgvDetail.Size = New Size(1187, 521)
            '  Panel1.Show()
        Else

            Panel1.Hide()
        End If
    End Sub

    Private Function GetPaymentVals(ByVal DocNo As String) As String()
        Dim Vals() As String = {"", "", ""}
        If cmbARType.SelectedIndex < 3 Then 'except all
            Dim sSQL As String = "Select a.PayerName, b.Amount, b.Amount, b.UnApplied " & _
            "from Payers a inner join Payments b on b.Ar_ID = a.ID where b.ArType = " & _
            cmbARType.SelectedIndex & " and b.DocNo = '" & DocNo & "'"
            '
            Dim MyCon As New Data.SqlClient.SqlConnection(connString)
            MyCon.Open()
            Dim selcmd As New Data.SqlClient.SqlCommand(sSQL, MyCon)
            selcmd.CommandType = Data.CommandType.Text
            Dim selDR As Data.SqlClient.SqlDataReader = selcmd.ExecuteReader
            If selDR.HasRows Then
                While selDR.Read
                    Vals(0) = selDR("PayerName")
                    Vals(1) = Math.Round(selDR("Amount"), 2)
                    Vals(2) = Math.Round(selDR("UnApplied"), 2)
                End While
            End If
            selDR.Close()
            selcmd.Dispose()
            MyCon.Close()
        End If
        Return Vals
    End Function

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        On Error Resume Next
        'Dim idd = Date.Parsee(dtpDateFrom.Text, "YYYY-MM-DD")
        Dim idd As Date = DateTime.ParseExact(dtpDateFrom.Text, "yyyy-MM-dd", Globalization.CultureInfo.InvariantCulture)


        If IsDate(dtpDateFrom.Text) Or IsDate(dtpDateTo.Text) Or
        (cmbSearch.SelectedIndex > -1 And Trim(txtSearch.Text) <> "") Then
            txtSpeed.Text = ""

            My.Application.DoEvents()
            Dim stopWatch As New Stopwatch()
            stopWatch.Start()
            '
            dgvDetail.Rows.Clear()
            My.Application.DoEvents()
            Dim sSQL As String = ""
            'Date Type Checks
            'cmbDateType.SelectedIndex = 0 BillDate
            'cmbDateType.SelectedIndex = 1 Received Date
            'cmbDateType.SelectedIndex  = 2 ServiceDate

            'cmbARType
            ' cmbARType.SelectedIndex  = 0 Client
            ' cmbARType.SelectedIndex  = 1 Insurance
            ' cmbARType.SelectedIndex  = 2 Patient
            ' cmbARType.SelectedIndex  = 3 ALL

            ' cmbSearch.SelectedText
            ' cmbSearch.SelectedText = 0 Provider/Entity ID
            ' cmbSearch.SelectedText = 1 Provider/Entity Name
            ' cmbSearch.SelectedText = 2 Insurance ID
            ' cmbSearch.SelectedText = 3 Insurance Name
            ' cmbSearch.SelectedText = 4 Patient(ID)
            ' cmbSearch.SelectedText = 5 Patient(Name(Last, First))
            ' cmbSearch.SelectedText = 6 Accession(Range(First, Last))
            ' cmbSearch.SelectedText = 7 Document ID
            ' cmbSearch.SelectedText = 8 Invoice ID
            ' cmbSearch.SelectedText = 9 CPT()


            ' Insurance 1   bill date, cmbSearch 2, 
            If cmbARType.SelectedIndex = 1 AndAlso cmbDateType.SelectedIndex = 0 AndAlso cmbSearch.SelectedIndex = 2 AndAlso IsNumeric(txtSearch.Text) Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    'sSQL = GetBillingQuery()

                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)
                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If
            ElseIf cmbARType.SelectedIndex = 3 AndAlso cmbDateType.SelectedIndex = 2 AndAlso cmbSearch.SelectedIndex = 7 Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    'sSQL = GetBillingQuery()

                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex, txtSearch.Text)
                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                Else
                    'sSQL = GetBillingQuery()
                    If Not IsDate(dtpDateFrom.Text) OrElse Not IsDate(dtpDateTo.Text) Then
                        MessageBox.Show("Please Select Both Dates")
                        Return
                    End If

                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex, txtSearch.Text)
                    Dim fdate = Convert.ToDateTime(DateAdd(DateInterval.Year, -1, dtpDateFrom.Value)).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If
                ' Insurance 1   Receive Date,, cmbSearch 2, 
            ElseIf cmbARType.SelectedIndex = 1 AndAlso cmbDateType.SelectedIndex = 1 AndAlso cmbSearch.SelectedIndex = 2 AndAlso IsNumeric(txtSearch.Text) Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)

                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If
                ' Insurance 1   service Date, , cmbSearch 2, 
            ElseIf cmbARType.SelectedIndex = 1 AndAlso cmbDateType.SelectedIndex = 2 AndAlso cmbSearch.SelectedIndex = 2 AndAlso IsNumeric(txtSearch.Text) Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)

                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If



                ' Insurance 0   bill Date,, cmbSearch 2, 
            ElseIf cmbARType.SelectedIndex = 0 AndAlso cmbDateType.SelectedIndex = 0 AndAlso cmbSearch.SelectedIndex = 2 AndAlso IsNumeric(txtSearch.Text) Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)

                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If

                ' Insurance 0   receive Date,, cmbSearch 2,
            ElseIf cmbARType.SelectedIndex = 0 AndAlso cmbDateType.SelectedIndex = 1 AndAlso cmbSearch.SelectedIndex = 2 AndAlso IsNumeric(txtSearch.Text) Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    'sSQL = GetBillingQuery()

                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)
                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If
                ' Insurance 0   service Date,, cmbSearch 2, 
            ElseIf cmbARType.SelectedIndex = 0 AndAlso cmbDateType.SelectedIndex = 2 AndAlso cmbSearch.SelectedIndex = 2 AndAlso IsNumeric(txtSearch.Text) Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)

                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If





                ' Insurance 2   bill Date,, cmbSearch 2, 
            ElseIf cmbARType.SelectedIndex = 2 AndAlso cmbDateType.SelectedIndex = 0 AndAlso cmbSearch.SelectedIndex = 2 AndAlso IsNumeric(txtSearch.Text) Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)

                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If

                ' Insurance 2  receive Date,, cmbSearch 2,
            ElseIf cmbARType.SelectedIndex = 2 AndAlso cmbDateType.SelectedIndex = 1 AndAlso cmbSearch.SelectedIndex = 2 AndAlso IsNumeric(txtSearch.Text) Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    'sSQL = GetBillingQuery()

                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)
                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If
                ' Insurance 2  service Date,, cmbSearch 2, 
            ElseIf cmbARType.SelectedIndex = 2 AndAlso cmbDateType.SelectedIndex = 2 AndAlso cmbSearch.SelectedIndex = 2 AndAlso IsNumeric(txtSearch.Text) Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)

                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If


                '    Search 0 Provider ID start
                ' Insurance 1   bill Date,, cmbSearch 0, 
            ElseIf cmbARType.SelectedIndex = 1 AndAlso cmbDateType.SelectedIndex = 0 AndAlso cmbSearch.SelectedIndex = 0 AndAlso IsNumeric(txtSearch.Text) Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    'sSQL = GetBillingQuery()

                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)
                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If
                ' Insurance 1   Receive Date,, cmbSearch 0, 
            ElseIf cmbARType.SelectedIndex = 1 AndAlso cmbDateType.SelectedIndex = 1 AndAlso cmbSearch.SelectedIndex = 0 AndAlso IsNumeric(txtSearch.Text) Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)

                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If
                ' Insurance 1   service Date, , cmbSearch 0, 
            ElseIf cmbARType.SelectedIndex = 1 AndAlso cmbDateType.SelectedIndex = 2 AndAlso cmbSearch.SelectedIndex = 0 AndAlso IsNumeric(txtSearch.Text) Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)

                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If




                ' Insurance 0   bill Date,, cmbSearch 0, 
            ElseIf cmbARType.SelectedIndex = 0 AndAlso cmbDateType.SelectedIndex = 0 AndAlso cmbSearch.SelectedIndex = 0 AndAlso IsNumeric(txtSearch.Text) Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)

                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If

                ' Insurance 0   receive Date,, cmbSearch 0,
            ElseIf cmbARType.SelectedIndex = 0 AndAlso cmbDateType.SelectedIndex = 1 AndAlso cmbSearch.SelectedIndex = 0 AndAlso IsNumeric(txtSearch.Text) Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    'sSQL = GetBillingQuery()

                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)
                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If
                ' Insurance 0   service Date,, cmbSearch 0, 
            ElseIf cmbARType.SelectedIndex = 0 AndAlso cmbDateType.SelectedIndex = 2 AndAlso cmbSearch.SelectedIndex = 0 AndAlso IsNumeric(txtSearch.Text) Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)

                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If





                ' Insurance 2   bill Date,, cmbSearch 0, 
            ElseIf cmbARType.SelectedIndex = 2 AndAlso cmbDateType.SelectedIndex = 0 AndAlso cmbSearch.SelectedIndex = 0 AndAlso IsNumeric(txtSearch.Text) Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)

                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If

                ' Insurance 2  receive Date,, cmbSearch 0,
            ElseIf cmbARType.SelectedIndex = 2 AndAlso cmbDateType.SelectedIndex = 1 AndAlso cmbSearch.SelectedIndex = 0 AndAlso IsNumeric(txtSearch.Text) Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    'sSQL = GetBillingQuery()

                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)
                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If
                ' Insurance 2  service Date,, cmbSearch 0, 
            ElseIf cmbARType.SelectedIndex = 2 AndAlso cmbDateType.SelectedIndex = 2 AndAlso cmbSearch.SelectedIndex = 0 AndAlso IsNumeric(txtSearch.Text) Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)

                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If


                ' Billing Type, Date Type, DatesFrom, DateTo
            ElseIf cmbARType.SelectedIndex = 1 AndAlso cmbDateType.SelectedIndex = 0 AndAlso cmbSearch.SelectedIndex = -1 Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    'sSQL = GetBillingQuery()

                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)
                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If
            ElseIf cmbARType.SelectedIndex = 3 AndAlso cmbDateType.SelectedIndex = 0 AndAlso cmbSearch.SelectedIndex = -1 Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    'sSQL = GetBillingQuery()

                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)
                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If
                ' Insurance 1   Receive Date,, cmbSearch 2, 
            ElseIf cmbARType.SelectedIndex = 1 AndAlso cmbDateType.SelectedIndex = 1 AndAlso cmbSearch.SelectedIndex = -1 Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)

                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If

            ElseIf cmbARType.SelectedIndex = 3 AndAlso cmbDateType.SelectedIndex = 1 AndAlso cmbSearch.SelectedIndex = -1 Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)

                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If
                ' Insurance 1   service Date, , cmbSearch 2, 
            ElseIf cmbARType.SelectedIndex = 1 AndAlso cmbDateType.SelectedIndex = 2 AndAlso cmbSearch.SelectedIndex = -1 Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)

                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If

            ElseIf cmbARType.SelectedIndex = 3 AndAlso cmbDateType.SelectedIndex = 2 AndAlso cmbSearch.SelectedIndex = -1 Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)

                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If

                ' Insurance 0   bill Date,, cmbSearch 2, 
            ElseIf cmbARType.SelectedIndex = 0 AndAlso cmbDateType.SelectedIndex = 0 AndAlso cmbSearch.SelectedIndex = -1 Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)

                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If

                ' Insurance 0   receive Date,, cmbSearch 2,
            ElseIf cmbARType.SelectedIndex = 0 AndAlso cmbDateType.SelectedIndex = 1 AndAlso cmbSearch.SelectedIndex = -1 Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    'sSQL = GetBillingQuery()

                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)
                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If
                ' Insurance 0   service Date,, cmbSearch 2, 
            ElseIf cmbARType.SelectedIndex = 0 AndAlso cmbDateType.SelectedIndex = 2 AndAlso cmbSearch.SelectedIndex = -1 Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)

                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If





                ' Insurance 2   bill Date,, cmbSearch 2, 
            ElseIf cmbARType.SelectedIndex = 2 AndAlso cmbDateType.SelectedIndex = 0 AndAlso cmbSearch.SelectedIndex = -1 Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)

                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If

                ' Insurance 2  receive Date,, cmbSearch 2,
            ElseIf cmbARType.SelectedIndex = 2 AndAlso cmbDateType.SelectedIndex = 1 AndAlso cmbSearch.SelectedIndex = -1 Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    'sSQL = GetBillingQuery()

                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)
                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If
                ' Insurance 2  service Date,, cmbSearch 2, 
            ElseIf cmbARType.SelectedIndex = 2 AndAlso cmbDateType.SelectedIndex = 2 AndAlso cmbSearch.SelectedIndex = -1 Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)

                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If


                '    Search 0 Provider ID start
                ' Insurance 1   bill Date,, cmbSearch 0, 
            ElseIf cmbARType.SelectedIndex = 1 AndAlso cmbDateType.SelectedIndex = 0 AndAlso cmbSearch.SelectedIndex = -1 Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    'sSQL = GetBillingQuery()

                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)
                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If
                ' Insurance 1   Receive Date,, cmbSearch 0, 
            ElseIf cmbARType.SelectedIndex = 1 AndAlso cmbDateType.SelectedIndex = 1 AndAlso cmbSearch.SelectedIndex = -1 Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)

                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If
                ' Insurance 1   service Date, , cmbSearch 0, 
            ElseIf cmbARType.SelectedIndex = 1 AndAlso cmbDateType.SelectedIndex = 2 AndAlso cmbSearch.SelectedIndex = -1 Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)

                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If




                ' Insurance 0   bill Date,, cmbSearch 0, 
            ElseIf cmbARType.SelectedIndex = 0 AndAlso cmbDateType.SelectedIndex = 0 AndAlso cmbSearch.SelectedIndex = -1 Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)

                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If

                ' Insurance 0   receive Date,, cmbSearch 0,
            ElseIf cmbARType.SelectedIndex = 0 AndAlso cmbDateType.SelectedIndex = 1 AndAlso cmbSearch.SelectedIndex = -1 Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    'sSQL = GetBillingQuery()

                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)
                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If
                ' Insurance 0   service Date,, cmbSearch 0, 
            ElseIf cmbARType.SelectedIndex = 0 AndAlso cmbDateType.SelectedIndex = 2 AndAlso cmbSearch.SelectedIndex = -1 Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)

                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If





                ' Insurance 2   bill Date,, cmbSearch 0, 
            ElseIf cmbARType.SelectedIndex = 2 AndAlso cmbDateType.SelectedIndex = 0 AndAlso cmbSearch.SelectedIndex = -1 Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)

                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If

                ' Insurance 2  receive Date,, cmbSearch 0,
            ElseIf cmbARType.SelectedIndex = 2 AndAlso cmbDateType.SelectedIndex = 1 AndAlso cmbSearch.SelectedIndex = -1 Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    'sSQL = GetBillingQuery()

                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)
                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If
                ' Insurance 2  service Date,, cmbSearch 0, 
            ElseIf cmbARType.SelectedIndex = 2 AndAlso cmbDateType.SelectedIndex = 2 AndAlso cmbSearch.SelectedIndex = -1 Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)

                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If

            ElseIf cmbARType.SelectedIndex = 3 AndAlso cmbDateType.SelectedIndex = 2 AndAlso cmbSearch.SelectedIndex = 2 Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)

                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If
            ElseIf cmbARType.SelectedIndex = 3 AndAlso cmbDateType.SelectedIndex = 2 AndAlso cmbSearch.SelectedIndex = 4 Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)

                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If
            ElseIf cmbARType.SelectedIndex = 3 AndAlso cmbDateType.SelectedIndex = 2 AndAlso cmbSearch.SelectedIndex = 9 Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex, txtSearch.Text)

                    Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@PrimePayer_ID", txtSearch.Text)
                End If

            ElseIf cmbARType.SelectedIndex = 3 AndAlso cmbDateType.SelectedIndex = 2 AndAlso cmbSearch.SelectedIndex = 6 Then
                sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex, txtSearch.Text)

                Dim fdate = Convert.ToDateTime(dtpDateFrom.Text).ToString("yyyy-MM-dd") & " 00:00:00.000"
                Dim tdate = Convert.ToDateTime(dtpDateTo.Text).ToString("yyyy-MM-dd") & " 23:59:00.000"
                sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@ID", txtSearch.Text)

            ElseIf cmbARType.SelectedIndex = 33 AndAlso cmbSearch.SelectedIndex = 6 Then
                If IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                    sSQL = QueryProvider.AR_By_Date_Insurance(cmbDateType.SelectedIndex, cmbARType.SelectedIndex, cmbSearch.SelectedIndex)
                    Dim sfdate = CommonData.RetrieveColumnValue("Charges", "Svc_Date", "Accession_ID", "'" & txtSearch.Text & "'", "")
                    Dim fdate = Convert.ToDateTime(sfdate).ToString("yyyy-MM-dd") & " 00:00:00.000"
                    Dim tdate = Convert.ToDateTime(sfdate).ToString("yyyy-MM-dd") & " 23:59:00.000"
                    sSQL = sSQL.Replace("@FromDate", "'" + fdate + "'") _
                    .Replace("@ToDate", "'" + tdate + "'") _
                    .Replace("@ID", txtSearch.Text)
                End If
                'End Search 0 Provider IS


            Else
                If cmbARType.SelectedIndex > 2 Then 'all
                    sSQL = "Select a.ArType as Artype, a.Accession_ID as Accession, a.ID as Invoice, IsNull(convert(nvarchar, a.Svc_Date, 101), '') as [Svc Date], " &
                    "(Case when a.ArType = 0 then d.LastName_BSN + ', ' + d.FirstName When a.ArType = 1 Then e.PayerName Else c.LastName + ', ' " &
                    "+ c.FirstName End) as [Bill To], c.LastName + ', ' + c.FirstName as Patient, IsNull(a.GrossAmount, 0) as [Bill Amt], " &
                    "IsNull((Select top 1 f.DocNo from Payments f inner join (Payment_Detail g inner join Charge_Detail h on h.Charge_ID = " &
                    "g.Charge_ID) on g.Payment_ID = f.ID where g.Charge_ID = a.ID), '') as [Doc No], IsNull(Convert(nvarchar, (Select top 1 " &
                    "f.PaymentDate from Payments f inner join (Payment_Detail g inner join Charge_Detail h on h.Charge_ID = g.Charge_ID) on " &
                    "g.Payment_ID = f.ID where g.Charge_ID = a.ID), 101), '') as [Trn Date], IsNull((Select Sum(AppliedAmount) from Payment_Detail " &
                    "where Charge_ID = a.ID), 0) as Payment, IsNull((Select Sum(WrittenOff) from Payment_Detail where Charge_ID = a.ID), 0) as " &
                    "[W.O.], IsNull(a.GrossAmount - IsNull((Select Sum(AppliedAmount) from Payment_Detail where Charge_ID = a.ID), 0) - " &
                    "IsNull((Select Sum(WrittenOff) from Payment_Detail where Charge_ID = a.ID), 0), 0) as Balance, (Case when IsNull((Select " &
                    "top 1 f.DocNo from Payments f inner join (Payment_Detail g inner join Charge_Detail h on h.Charge_ID = g.Charge_ID) on " &
                    "g.Payment_ID = f.ID where g.Charge_ID = a.ID), '') <> '' Then DateDiff(day, a.Bill_Date, (Select top 1 f.PaymentDate " &
                    "from Payments f inner join (Payment_Detail g inner join Charge_Detail h on h.Charge_ID = g.Charge_ID) on g.Payment_ID = " &
                    "f.ID where g.Charge_ID = a.ID)) Else DateDiff(day, a.Bill_Date, GETDATE()) end) as Age from Charges a inner join " &
                    "(Patients c inner join (Providers d inner join (Requisitions b left outer join Payers e on e.ID = b.PrimePayer_ID) on " &
                    "b.OrderingProvider_ID = d.ID) on c.ID = b.Patient_ID) on b.ID = a.Accession_ID"
                Else
                    sSQL = "Select a.ArType as Artype, a.Accession_ID as Accession, a.ID as Invoice, IsNull(convert(nvarchar, a.Svc_Date, 101), '') as [Svc Date], " &
                    "(Case when a.ArType = 0 then d.LastName_BSN + ', ' + d.FirstName When a.ArType = 1 Then e.PayerName Else c.LastName + ', ' " &
                    "+ c.FirstName End) as [Bill To], c.LastName + ', ' + c.FirstName as Patient, IsNull(a.GrossAmount, 0) as [Bill Amt], " &
                    "IsNull((Select top 1 f.DocNo from Payments f inner join (Payment_Detail g inner join Charge_Detail h on h.Charge_ID = " &
                    "g.Charge_ID) on g.Payment_ID = f.ID where g.Charge_ID = a.ID), '') as [Doc No], IsNull(Convert(nvarchar, (Select top 1 " &
                    "f.PaymentDate from Payments f inner join (Payment_Detail g inner join Charge_Detail h on h.Charge_ID = g.Charge_ID) on " &
                    "g.Payment_ID = f.ID where g.Charge_ID = a.ID), 101), '') as [Trn Date], IsNull((Select Sum(AppliedAmount) from Payment_Detail " &
                    "where Charge_ID = a.ID), 0) as Payment, IsNull((Select Sum(WrittenOff) from Payment_Detail where Charge_ID = a.ID), 0) as " &
                    "[W.O.], IsNull(a.GrossAmount - IsNull((Select Sum(AppliedAmount) from Payment_Detail where Charge_ID = a.ID), 0) - " &
                    "IsNull((Select Sum(WrittenOff) from Payment_Detail where Charge_ID = a.ID), 0), 0) as Balance, (Case when IsNull((Select " &
                    "top 1 f.DocNo from Payments f inner join (Payment_Detail g inner join Charge_Detail h on h.Charge_ID = g.Charge_ID) on " &
                    "g.Payment_ID = f.ID where g.Charge_ID = a.ID), '') <> '' Then DateDiff(day, a.Bill_Date, (Select top 1 f.PaymentDate " &
                    "from Payments f inner join (Payment_Detail g inner join Charge_Detail h on h.Charge_ID = g.Charge_ID) on g.Payment_ID = " &
                    "f.ID where g.Charge_ID = a.ID)) Else DateDiff(day, a.Bill_Date, GETDATE()) end) as Age from Charges a inner join (Patients " &
                    "c inner join (Providers d inner join (Requisitions b left outer join Payers e on e.ID = b.PrimePayer_ID) on " &
                    "b.OrderingProvider_ID = d.ID) on c.ID = b.Patient_ID) on b.ID = a.Accession_ID where a.ArType  = " & cmbARType.SelectedIndex
                End If

                'If cmbARType.SelectedIndex > 2 Then 'all
                '    sSQL = "Select a.Accession_ID as AccID, a.ID as InvID, convert(nvarchar, a.Svc_Date, 101) as " & _
                '    "SvcDate, IsNull(c.LastName + ', ' + c.FirstName, '') as Patient, IsNull(a.GrossAmount, 0) as " & _
                '    "Billed, Round(IsNull((Select Sum(AppliedAmount) from Payment_Detail where Charge_ID = a.ID), " & _
                '    "0), 2) as Paid, Round(IsNull((Select Sum(WrittenOff) from Payment_Detail where Charge_ID = " & _
                '    "a.ID), 0), 2) as WO from Charges a inner join (Requisitions b Left outer join Patients c on " & _
                '    "c.ID = b.Patient_ID) on b.ID = a.Accession_ID where b.Received <> 0"
                'Else
                '    sSQL = "Select a.Accession_ID as AccID, a.ID as InvID, convert(nvarchar, a.Svc_Date, 101) as " & _
                '    "SvcDate, IsNull(c.LastName + ', ' + c.FirstName, '') as Patient, IsNull(a.GrossAmount, 0) as " & _
                '    "Billed, Round(IsNull((Select Sum(AppliedAmount) from Payment_Detail where Charge_ID = a.ID), " & _
                '    "0), 2) as Paid, Round(IsNull((Select Sum(WrittenOff) from Payment_Detail where Charge_ID = " & _
                '    "a.ID), 0), 2) as WO from Charges a inner join (Requisitions b Left outer join Patients c on " & _
                '    "c.ID = b.Patient_ID) on b.ID = a.Accession_ID where b.Received <> 0 And b.BillingType_ID = " & _
                '    cmbARType.SelectedIndex
                'End If
                '
                Dim dtpDateFrom1 = dtpDateFrom.Text
                Dim dtpDateTo1 = dtpDateTo.Text
                If cmbSearch.SelectedIndex = 6 AndAlso txtSearch.Text <> "" Then
                    Dim sfdate = CommonData.RetrieveColumnValue("Charges", "Svc_Date", "Accession_ID", "'" & txtSearch.Text & "'", "")
                    dtpDateFrom1 = Convert.ToDateTime(sfdate).ToString("yyyy-MM-dd")
                    dtpDateTo1 = Convert.ToDateTime(sfdate).ToString("yyyy-MM-dd")
                End If
                If cmbDateType.SelectedIndex = 1 Then  'Receive date
                    If IsDate(dtpDateFrom1) And Not IsDate(dtpDateTo1) Then
                        sSQL += " and b.ReceivedTime between '" & dtpDateFrom1 &
                        "' and '" & dtpDateFrom1 & " 23:59:00'"
                    ElseIf Not IsDate(dtpDateFrom1) And IsDate(dtpDateTo1) Then
                        sSQL += " and b.ReceivedTime between '" & dtpDateTo1 &
                        "' and '" & dtpDateTo1 & " 23:59:00'"
                    ElseIf IsDate(dtpDateFrom1) And IsDate(dtpDateTo1) Then
                        sSQL += " and b.ReceivedTime between '" & dtpDateFrom1 &
                        "' and '" & dtpDateTo1 & " 23:59:00'"
                    End If
                ElseIf cmbDateType.SelectedIndex = 2 Then  'service date

                    If IsDate(dtpDateFrom1) And Not IsDate(dtpDateTo1) Then
                        sSQL += " and a.Svc_Date between '" & dtpDateFrom1 &
                        "' and '" & dtpDateFrom1 & " 23:59:00'"
                    ElseIf Not IsDate(dtpDateFrom1) And IsDate(dtpDateTo1) Then
                        sSQL += " and a.Svc_Date between '" & dtpDateTo1 &
                        "' and '" & dtpDateTo1 & " 23:59:00'"
                    ElseIf IsDate(dtpDateFrom1) And IsDate(dtpDateTo1) Then
                        sSQL += " and a.Svc_Date between '" & dtpDateFrom1 &
                        "' and '" & dtpDateTo1 & " 23:59:00'"
                    End If
                Else    'Bill Date
                    If IsDate(dtpDateFrom1) And Not IsDate(dtpDateTo1) Then
                        sSQL += " and a.Bill_Date between '" & dtpDateFrom1 &
                        "' and '" & dtpDateFrom1 & " 23:59:00'"
                    ElseIf Not IsDate(dtpDateFrom1) And IsDate(dtpDateTo1) Then
                        sSQL += " and a.Bill_Date between '" & dtpDateTo1 &
                        "' and '" & dtpDateTo1 & " 23:59:00'"
                    ElseIf IsDate(dtpDateFrom1) And IsDate(dtpDateTo1) Then
                        sSQL += " and a.Bill_Date between '" & dtpDateFrom1 &
                        "' and '" & dtpDateTo1 & " 23:59:00'"
                    End If
                End If
                '
                If cmbSearch.SelectedIndex = 0 Then     'Provider ID
                    If Trim(txtSearch.Text) <> "" AndAlso IsNumeric(Trim(txtSearch.Text)) Then _
                    sSQL += " and b.OrderingProvider_ID = " & CLng(Trim(txtSearch.Text))
                ElseIf cmbSearch.SelectedIndex = 1 Then     'Provider Name
                    If Trim(txtSearch.Text) <> "" Then
                        Dim LName As String = ""
                        Dim FName As String = ""
                        Dim Names() As String
                        If InStr(Trim(txtSearch.Text), ",") > 0 Then
                            Names = Split(Trim(txtSearch.Text), ",")
                            LName = Trim(Names(0))
                            FName = Trim(Names(1))
                        Else
                            LName = Trim(txtSearch.Text)
                        End If
                        If FName <> "" And LName <> "" Then
                            sSQL += " and b.OrderingProvider_ID in (Select Top 1 ID from Providers where IsIndividual " &
                            "<> 0 and LastName_BSN like '" & LName & "%' and FirstName like '" & FName & "%')"
                        ElseIf FName = "" And LName <> "" Then
                            sSQL += " and b.OrderingProvider_ID in (Select top 1 ID from Providers where IsIndividual " &
                            "= 0 and LastName_BSN like '" & LName & "%')"
                        End If
                    End If
                ElseIf cmbSearch.SelectedIndex = 2 Then     'Insurance ID
                    If Trim(txtSearch.Text) <> "" AndAlso IsNumeric(Trim(txtSearch.Text)) Then _
                    sSQL += " and b.PrimePayer_ID = " & CLng(Trim(txtSearch.Text))
                ElseIf cmbSearch.SelectedIndex = 3 Then     'Insurance Name
                    If searchID.Text <> "" Then
                        sSQL += " and b.PrimePayer_ID = " & CLng(Trim(searchID.Text))
                    ElseIf Trim(txtSearch.Text) <> "" Then
                        sSQL += " and b.PrimePayer_ID in (Select ID from Payers where " &
                            "PayerName like '" & Trim(txtSearch.Text) & "%')"

                    End If

                ElseIf cmbSearch.SelectedIndex = 4 Then     'Patient ID
                    If Trim(txtSearch.Text) <> "" AndAlso IsNumeric(Trim(txtSearch.Text)) Then _
                    sSQL += " and b.Patient_ID = " & CLng(Trim(txtSearch.Text))
                ElseIf cmbSearch.SelectedIndex = 5 Then     'Patient Name
                    If searchID.Text <> "" Then
                        sSQL += " and b.Patient_ID = " & CLng(Trim(searchID.Text))
                    ElseIf Trim(txtSearch.Text) <> "" Then
                        Dim LName As String = ""
                        Dim FName As String = ""
                        Dim Names() As String
                        If InStr(Trim(txtSearch.Text), ",") > 0 Then
                            Names = Split(Trim(txtSearch.Text), ",")
                            LName = Trim(Names(0))
                            FName = Trim(Names(1))
                        Else
                            LName = Trim(txtSearch.Text)
                        End If
                        If FName <> "" And LName <> "" Then
                            sSQL += " and b.Patient_ID in (Select ID from Patients where " &
                                "LastName like '" & LName & "%' and FirstName like '" & FName & "%')"
                        ElseIf FName = "" And LName <> "" Then
                            sSQL += " and b.Patient_ID in (Select ID from Patients " &
                                "where LastName like '" & LName & "%')"
                        End If
                    End If


                ElseIf cmbSearch.SelectedIndex = 6 Then     'Accessions
                    If Trim(txtSearch.Text) <> "" Then
                        Dim AccFrom As String = ""
                        Dim AccTo As String = ""
                        Dim Accs() As String
                        If InStr(Trim(txtSearch.Text), ",") > 0 Then
                            Accs = Split(Trim(txtSearch.Text), ",")
                            AccFrom = Trim(Accs(0))
                            AccTo = Trim(Accs(1))
                        Else
                            AccFrom = Trim(txtSearch.Text)
                        End If
                        If AccFrom <> "" And AccTo <> "" Then
                            If CLng(AccFrom) < CLng(AccTo) Then
                                sSQL += " and b.ID between " & CLng(AccFrom) & " and " & CLng(AccTo)
                            ElseIf CLng(AccFrom) > CLng(AccTo) Then
                                sSQL += " and b.ID between " & CLng(AccTo) & " and " & CLng(AccFrom)
                            Else
                                sSQL += " and b.ID = " & CLng(AccTo)
                            End If
                        ElseIf AccFrom <> "" And AccTo = "" Then
                            sSQL += " and b.ID = " & CLng(AccFrom)
                        ElseIf AccFrom = "" And AccTo <> "" Then
                            sSQL += " and b.ID = " & CLng(AccTo)
                        End If
                    End If
                ElseIf cmbSearch.SelectedIndex = 7 Then     'Document
                    If Trim(txtSearch.Text) <> "" Then
                        sSQL += " and a.ID in (Select distinct Charge_ID from Payment_Detail where Payment_ID in " &
                        "(Select ID from Payments where DocNo = '" & Trim(txtSearch.Text) & "'))"
                        Dim PayVars() As String = GetPaymentVals(Trim(txtSearch.Text))
                        txtPayer.Text = PayVars(0)
                        txtDocAmt.Text = PayVars(1)
                        txtUnAppliedAmt.Text = PayVars(2)
                        My.Application.DoEvents()
                    End If
                ElseIf cmbSearch.SelectedIndex = 8 Then     'Invoice ID
                    If Trim(txtSearch.Text) <> "" Then
                        sSQL += " and a.ID = " & Trim(txtSearch.Text)
                    End If
                ElseIf cmbSearch.SelectedIndex = 9 Then     'CPT
                    If Trim(txtSearch.Text) <> "" Then
                        sSQL += " and a.ID in (Select distinct Charge_ID from Charge_Detail where " &
                        "CPT_Code = '" & Trim(txtSearch.Text) & "')"
                    End If
                End If
            End If

            '
            Dim AccCnt As Integer = 0
            Dim Pats As Integer = 0
            Dim CNT As Integer = 0
            Dim Age As Integer = 0
            Dim APD As Integer = 0
            Dim A0_30 As Integer = 0
            Dim A31_60 As Integer = 0
            Dim A61_90 As Integer = 0
            Dim A91Up As Integer = 0
            Dim Billed() As String
            Dim TotalBilled As Double = 0
            Dim TotalPaid As Double = 0
            Dim TotalWO As Double = 0
            Dim Billee As String = ""
            Dim Provider As String = ""
            Dim SvcDate As String = ""
            Dim Payments(3, 0) As String '0=Ck, 1 = Date, 2 = Amt, 3 = WO
            Dim InvBal As Double = 0
            '
            If sSQL = "" Then
                Return
            End If
            Dim tbl As New DataTable
            Dim cnsrch As New Data.SqlClient.SqlConnection(connString)
            cnsrch.Open()
            Dim cmdsrch As New Data.SqlClient.SqlCommand(sSQL, cnsrch)
            cmdsrch.CommandType = Data.CommandType.Text
            'Dim dasrch As New SqlClient.SqlDataAdapter(cmdsrch)
            'dasrch.Fill(tbl)
            'dgvDetail.DataSource = tbl
            Dim drsrch As Data.SqlClient.SqlDataReader = cmdsrch.ExecuteReader
            If drsrch IsNot Nothing AndAlso drsrch.HasRows Then
                While drsrch.Read
                    If drsrch("Age") >= 0 And drsrch("Age") <= 30 Then
                        A0_30 += drsrch("Bill Amt") - drsrch("Payment") - drsrch("W.O.")
                    ElseIf drsrch("Age") >= 31 And drsrch("Age") <= 60 Then
                        A31_60 += drsrch("Bill Amt") - drsrch("Payment") - drsrch("W.O.")
                    ElseIf drsrch("Age") >= 61 And drsrch("Age") <= 90 Then
                        A61_90 += drsrch("Bill Amt") - drsrch("Payment") - drsrch("W.O.")
                    Else
                        A91Up += drsrch("Bill Amt") - drsrch("Payment") - drsrch("W.O.")
                    End If
                    TotalBilled += drsrch("Bill Amt")
                    TotalPaid += drsrch("Payment")
                    TotalWO += drsrch("W.O.")
                    '
                    AccCnt += 1
                    Dim sr = drsrch("Bill To") & " _" & drsrch("Artype")
                    dgvDetail.Rows.Add(drsrch("Accession"), drsrch("Invoice"), drsrch("Svc Date"),
                    sr, drsrch("Patient"), drsrch("Bill Amt"), drsrch("Doc No"),
                    drsrch("Trn Date"), drsrch("Payment"), drsrch("W.O."), drsrch("Balance"), drsrch("Age"))
                End While
            End If
            cnsrch.Close()
            cnsrch = Nothing

            '
            txtAccs.Text = AccCnt.ToString
            txtPats.Text = AccCnt.ToString
            txtBilled.Text = Format(TotalBilled, "##,##0.00")
            txtPaid.Text = Format(TotalPaid, "##,##0.00")
            If cmbSearch.SelectedIndex = 7 Then
                'txtUnAppliedAmt.Text = Format(Val(txtDocAmt.Text) - Val(txtPaid.Text), "0.00")
                txtInvCount.Text = AccCnt.ToString
            Else
                txtUnAppliedAmt.Text = ""
                txtInvCount.Text = ""
            End If
            txtWO.Text = Format(TotalWO, "####0.00")
            txtBal.Text = Format(TotalBilled - (TotalPaid + TotalWO), "####00.00")
            If CNT <> 0 Then
                txtAPD.Text = CInt(APD / CNT)
            Else
                txtAPD.Text = CInt(APD / 1)
            End If
            txt0_30.Text = Format(A0_30, "####0.00")
            txt31_60.Text = Format(A31_60, "####0.00")
            txt61_90.Text = Format(A61_90, "####0.00")
            txt91Up.Text = Format(A91Up, "####0.00")
            '
            stopWatch.Stop()
            txtSpeed.Text = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            stopWatch.Elapsed.Hours, stopWatch.Elapsed.Minutes,
            stopWatch.Elapsed.Seconds, stopWatch.Elapsed.Milliseconds / 10)
            My.Application.DoEvents()
        Else
            MsgBox("Make at least one selection, 'Date' or ('Criteria' and 'Term')", MsgBoxStyle.Critical, "Prolis")
        End If
    End Sub

    Private Function GetBilled(ByVal InvID As Long) As String()
        Dim billed() As String = {"", "", ""}   'Payer, Date, Amount
        Dim sSQL As String = "Select (Select PayerName from Payers where ID in (Select Ar_ID from Charges where " &
        "ArType = 1 and ID = " & InvID & ") Union Select LastName + ', ' + FirstName from Patients where ID in " &
        "(Select Ar_ID from Charges where ArType = 2 and ID = " & InvID & ") Union Select LastName_BSN from " &
        "Providers where IsIndividual = 0 and ID in (Select Ar_ID from Charges where ArType = 0 and ID = " &
        InvID & ") Union Select LastName_BSN + ', ' + FirstName from Providers where IsIndividual <> 0 and ID " &
        "in (Select Ar_ID from Charges where ArType = 0 and ID = " & InvID & ")) as Payer, a.Bill_Date, " &
        "(Select round(Sum(Extend), 2) from Charge_Detail where Charge_ID = " & InvID & ") as Amount from " &
        "Charges a where a.ID = " & InvID
        Dim cnb As New SqlClient.SqlConnection(connString)
        cnb.Open()
        Dim cmdb As New SqlClient.SqlCommand(sSQL, cnb)
        cmdb.CommandType = CommandType.Text
        Dim drb As SqlClient.SqlDataReader = cmdb.ExecuteReader
        If drb.HasRows Then
            While drb.Read
                If drb("Payer") IsNot DBNull.Value Then billed(0) = drb("Payer")
                If drb("Bill_Date") IsNot DBNull.Value Then billed(1) =
                Format(drb("Bill_Date"), SystemConfig.DateFormat)
                If drb("Amount") IsNot DBNull.Value Then _
                billed(2) = Math.Round(drb("Amount"), 2)
            End While
        End If
        cnb.Close()
        cnb = Nothing
        '
        Return billed
    End Function

    Private Function GetPayments(ByVal InvID As Long) As String(,)
        Dim PMNTS(3, 0) As String    '0=Ck, 1 = Date, 2 = Amt, 3 = WO
        Dim Amt As Double = 0
        Dim WO As Double = 0
        Dim Rebill As String = 0
        Dim sSQL As String = "Select a.DocNo, round((Select Sum(AppliedAmount) from Payment_Detail where " &
        "Payment_ID = a.ID and Charge_ID = " & InvID & "), 2) as PMNT, Round((Select Sum(WrittenOff) from " &
        "Payment_Detail where Payment_ID = a.ID and Charge_ID = " & InvID & "), 2) as WO, IsNull(convert(nvarchar, " &
        "a.PaymentDate, 101), '') as Dated from Payments a where a.ID in (Select distinct Payment_ID from " &
        "Payment_Detail where Charge_ID = " & InvID & ")"
        Dim cnpmt As New SqlClient.SqlConnection(connString)
        cnpmt.Open()
        Dim cmdpmt As New SqlClient.SqlCommand(sSQL, cnpmt)
        cmdpmt.CommandType = CommandType.Text
        Dim drpmt As SqlClient.SqlDataReader = cmdpmt.ExecuteReader
        If drpmt.HasRows Then
            While drpmt.Read
                If PMNTS(0, UBound(PMNTS, 2)) <> "" Then _
                ReDim Preserve PMNTS(3, UBound(PMNTS, 2) + 1)
                PMNTS(0, UBound(PMNTS, 2)) = Trim(drpmt("DocNo"))
                PMNTS(1, UBound(PMNTS, 2)) = drpmt("Dated")
                PMNTS(2, UBound(PMNTS, 2)) = drpmt("PMNT").ToString
                PMNTS(3, UBound(PMNTS, 2)) = drpmt("WO").ToString
            End While
        End If
        cnpmt.Close()
        cnpmt = Nothing
        Return PMNTS
    End Function

    Private Function GetBillee(ByVal InvID As Long) As String
        Dim Billee As String = ""
        Dim sSQL As String = ""
        Dim cn1 As New SqlClient.SqlConnection(connString)
        cn1.Open()
        Dim cmd1 As New SqlClient.SqlCommand("Select * from Providers where ID in " &
        "(Select Ar_ID from Charges where ArType = 0 and ID = " & InvID & ")", cn1)
        cmd1.CommandType = CommandType.Text
        Dim dr1 As SqlClient.SqlDataReader = cmd1.ExecuteReader
        If dr1.HasRows Then
            While dr1.Read
                If dr1("IsIndividual") = True Then
                    If dr1("Degree") IsNot DBNull.Value _
                    AndAlso Trim(dr1("Degree")) <> "" Then
                        Billee = dr1("LastName_BSN") & ", " &
                        dr1("FirstName") & " " & dr1("Degree")
                    Else
                        Billee = dr1("LastName_BSN") & ", " &
                        dr1("FirstName")
                    End If
                Else
                    Billee = dr1("LastName_BSN")
                End If
            End While
        Else    'not a client
            Dim cn2 As New SqlClient.SqlConnection(connString)
            cn2.Open()
            Dim cmd2 As New SqlClient.SqlCommand("Select * from Payers where ID in " &
            "(Select Ar_ID from Charges where ArType = 1 and ID = " & InvID & ")", cn2)
            cmd2.CommandType = CommandType.Text
            Dim dr2 As SqlClient.SqlDataReader = cmd2.ExecuteReader
            If dr2.HasRows Then
                While dr2.Read
                    Billee = dr2("PayerName")
                End While
            Else
                Billee = "Patient"
            End If
            cn2.Close()
            cn2 = Nothing
        End If
        cn1.Close()
        cn1 = Nothing
        Return Billee
    End Function

    Private Sub frmARInquiry_ResizeBegin(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeBegin
        origWidth = Me.Width
        origHeight = Me.Height
    End Sub

    Private Sub frmARInquiry_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        MeResize(Me, origWidth, origHeight)
    End Sub

    Private Sub btnDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetail.Click
        If IsDate(dtpDateFrom.Text) Or IsDate(dtpDateTo.Text) Or
        (cmbSearch.SelectedIndex > -1 And Trim(txtSearch.Text) <> "") Then
            Dim Formula As String = "{Charges.ArType} = " & cmbARType.SelectedIndex
            'TODO: Crystal Report
            'Dim oRPT As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            'oRPT.Load(GetReportPath("ARDetail.rpt"))
            'ApplyNewServer(oRPT, My.Settings.DSN, My.Settings.Database, My.Settings.UID, My.Settings.PWD)
            '======================================
            If cmbDateType.SelectedIndex = 2 Then  'Service Date
                If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
                    '{Charges.Svc_Date} in DateTime (2012, 03, 01, 16, 00, 00) to DateTime (2012, 03, 29, 19, 10, 00)
                    Formula += " and {Charges.Svc_Date} in DateTime(" &
                    Year(CDate(dtpDateFrom.Text)) & ", " &
                    Month(CDate(dtpDateFrom.Text)) & ", " &
                    CDate(dtpDateFrom.Text).Day & ", 00, 00, 00) To " &
                    "DateTime(" & Year(CDate(dtpDateFrom.Text)) & ", " &
                    Month(CDate(dtpDateFrom.Text)) & ", " &
                    CDate(dtpDateFrom.Text).Day & ", 23, 59, 00)"
                ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                    Formula += " and {Charges.Svc_Date} in DateTime(" &
                    Year(CDate(dtpDateTo.Text)) & ", " &
                    Month(CDate(dtpDateTo.Text)) & ", " &
                    CDate(dtpDateTo.Text).Day & ", 00, 00, 00) To " &
                    "DateTime(" & Year(CDate(dtpDateTo.Text)) & ", " &
                    Month(CDate(dtpDateTo.Text)) & ", " &
                    CDate(dtpDateTo.Text).Day & ", 23, 59, 00)"
                ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                    Formula += " and {Charges.Svc_Date} in DateTime(" &
                    Year(CDate(dtpDateFrom.Text)) & ", " &
                    Month(CDate(dtpDateFrom.Text)) & ", " &
                    CDate(dtpDateFrom.Text).Day & ", 00, 00, 00) To " &
                    "DateTime(" & Year(CDate(dtpDateTo.Text)) & ", " &
                    Month(CDate(dtpDateTo.Text)) & ", " &
                    CDate(dtpDateTo.Text).Day & ", 23, 59, 00)"
                End If
            ElseIf cmbDateType.SelectedIndex = 1 Then  'receive Date
                If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
                    '{Charges.Svc_Date} in DateTime (2012, 03, 01, 16, 00, 00) to DateTime (2012, 03, 29, 19, 10, 00)
                    Formula += " and {Requisitions.ReceivedTime} in DateTime(" &
                    Year(CDate(dtpDateFrom.Text)) & ", " &
                    Month(CDate(dtpDateFrom.Text)) & ", " &
                    CDate(dtpDateFrom.Text).Day & ", 00, 00, 00) To " &
                    "DateTime(" & Year(CDate(dtpDateFrom.Text)) & ", " &
                    Month(CDate(dtpDateFrom.Text)) & ", " &
                    CDate(dtpDateFrom.Text).Day & ", 23, 59, 00)"
                ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                    Formula += " and {Requisitions.ReceivedTime} in DateTime(" &
                    Year(CDate(dtpDateTo.Text)) & ", " &
                    Month(CDate(dtpDateTo.Text)) & ", " &
                    CDate(dtpDateTo.Text).Day & ", 00, 00, 00) To " &
                    "DateTime(" & Year(CDate(dtpDateTo.Text)) & ", " &
                    Month(CDate(dtpDateTo.Text)) & ", " &
                    CDate(dtpDateTo.Text).Day & ", 23, 59, 00)"
                ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                    Formula += " and {Requisitions.ReceivedTime} in DateTime(" &
                    Year(CDate(dtpDateFrom.Text)) & ", " &
                    Month(CDate(dtpDateFrom.Text)) & ", " &
                    CDate(dtpDateFrom.Text).Day & ", 00, 00, 00) To " &
                    "DateTime(" & Year(CDate(dtpDateTo.Text)) & ", " &
                    Month(CDate(dtpDateTo.Text)) & ", " &
                    CDate(dtpDateTo.Text).Day & ", 23, 59, 00)"
                End If
            Else    'Bill Date
                If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
                    Formula += " and {Charges.Bill_Date} in DateTime(" &
                    Year(CDate(dtpDateFrom.Text)) & ", " &
                    Month(CDate(dtpDateFrom.Text)) & ", " &
                    CDate(dtpDateFrom.Text).Day & ", 00, 00, 00) To " &
                    "DateTime(" & Year(CDate(dtpDateFrom.Text)) & ", " &
                    Month(CDate(dtpDateFrom.Text)) & ", " &
                    CDate(dtpDateFrom.Text).Day & ", 23, 59, 00)"
                ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                    Formula += " and {Charges.Bill_Date} in DateTime(" &
                    Year(CDate(dtpDateTo.Text)) & ", " &
                    Month(CDate(dtpDateTo.Text)) & ", " &
                    CDate(dtpDateTo.Text).Day & ", 00, 00, 00) To " &
                    "DateTime(" & Year(CDate(dtpDateTo.Text)) & ", " &
                    Month(CDate(dtpDateTo.Text)) & ", " &
                    CDate(dtpDateTo.Text).Day & ", 23, 59, 00)"
                ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                    Formula += " and {Charges.Bill_Date} in DateTime(" &
                    Year(CDate(dtpDateFrom.Text)) & ", " &
                    Month(CDate(dtpDateFrom.Text)) & ", " &
                    CDate(dtpDateFrom.Text).Day & ", 00, 00, 00) To " &
                    "DateTime(" & Year(CDate(dtpDateTo.Text)) & ", " &
                    Month(CDate(dtpDateTo.Text)) & ", " &
                    CDate(dtpDateTo.Text).Day & ", 23, 59, 00)"
                End If
            End If
            '
            If cmbSearch.SelectedIndex = 0 Then     'Provider ID
                If Trim(txtSearch.Text) <> "" AndAlso IsNumeric(Trim(txtSearch.Text)) Then _
                Formula += " and {Requisitions.OrderingProvider_ID} = " &
                CLng(Trim(txtSearch.Text))
            ElseIf cmbSearch.SelectedIndex = 1 Then     'Provider Name
                If Trim(txtSearch.Text) <> "" Then
                    Dim LName As String = ""
                    Dim FName As String = ""
                    Dim Names() As String
                    If InStr(Trim(txtSearch.Text), ",") > 0 Then
                        Names = Split(Trim(txtSearch.Text), ",")
                        LName = Trim(Names(0))
                        FName = Trim(Names(1))
                    Else
                        LName = Trim(txtSearch.Text)
                    End If
                    If FName <> "" And LName <> "" Then
                        Formula += " and {Providers_AR.LastName_BSN} like '" & LName _
                        & "*' and {Providers_AR.FirstName} like '" & FName & "*'"
                    ElseIf FName = "" And LName <> "" Then
                        Formula += " and {Providers_AR.LastName_BSN} like '" &
                        LName & "*'"
                    End If
                End If
            ElseIf cmbSearch.SelectedIndex = 2 Then     'Insurance ID
                If Trim(txtSearch.Text) <> "" AndAlso IsNumeric(Trim(txtSearch.Text)) Then _
                Formula += " and {Requisitions.PrimePayer_ID} = " &
                CLng(Trim(txtSearch.Text))
            ElseIf cmbSearch.SelectedIndex = 3 Then     'Insurance Name
                If Trim(txtSearch.Text) <> "" Then
                    Formula += " and {Payers.PayerName} like '" &
                    Trim(txtSearch.Text) & "*'"
                End If
            ElseIf cmbSearch.SelectedIndex = 4 Then     'Patient ID
                If Trim(txtSearch.Text) <> "" AndAlso IsNumeric(Trim(txtSearch.Text)) Then _
                Formula += " and {Requisitions.Patient_ID} = " & CLng(Trim(txtSearch.Text))
            ElseIf cmbSearch.SelectedIndex = 5 Then     'Patient Name
                If Trim(txtSearch.Text) <> "" Then
                    Dim LName As String = ""
                    Dim FName As String = ""
                    Dim Names() As String
                    If InStr(Trim(txtSearch.Text), ",") > 0 Then
                        Names = Split(Trim(txtSearch.Text), ",")
                        LName = Trim(Names(0))
                        FName = Trim(Names(1))
                    Else
                        LName = Trim(txtSearch.Text)
                    End If
                    If FName <> "" And LName <> "" Then
                        Formula += " and {Patients_AR.LastName} like '" & LName &
                        "*' and {Patients_AR.FirstName} like '" & FName & "*'"
                    ElseIf FName = "" And LName <> "" Then
                        Formula += " and {Patients_AR.LastName} like '" & LName & "*'"
                    End If
                End If
            ElseIf cmbSearch.SelectedIndex = 6 Then     'Accessions
                If Trim(txtSearch.Text) <> "" Then
                    Dim AccFrom As String = ""
                    Dim AccTo As String = ""
                    Dim Accs() As String
                    If InStr(Trim(txtSearch.Text), ",") > 0 Then
                        Accs = Split(Trim(txtSearch.Text), ",")
                        AccFrom = Trim(Accs(0))
                        AccTo = Trim(Accs(1))
                    Else
                        AccFrom = Trim(txtSearch.Text)
                    End If
                    If AccFrom <> "" And AccTo <> "" Then
                        If CLng(AccFrom) < CLng(AccTo) Then
                            Formula += " and {Requisitions.ID} in [" & CLng(AccFrom) _
                            & " To " & CLng(AccTo) & "]"
                        ElseIf CLng(AccFrom) > CLng(AccTo) Then
                            Formula += " and {Requisitions.ID} in [" & CLng(AccTo) _
                            & " To " & CLng(AccFrom) & "]"
                        Else
                            Formula += " and {Requisitions.ID} = " & CLng(AccTo)
                        End If
                    ElseIf AccFrom <> "" And AccTo = "" Then
                        Formula += " and {Requisitions.ID} = " & CLng(AccFrom)
                    ElseIf AccFrom = "" And AccTo <> "" Then
                        Formula += " and {Requisitions.ID} = " & CLng(AccTo)
                    End If
                End If
            End If
            'TODO: Crystal Report
            'oRPT.RecordSelectionFormula = Formula
            'frmRV.CRRV.ReportSource = oRPT
            '===========================================
            frmRV.Show()
            frmRV.MdiParent = frmDashboard
        Else
            MsgBox("Make at least one selection, 'Date' or ('Criteria' and 'Term')", MsgBoxStyle.Critical, "Prolis")
        End If
    End Sub

    Private Sub btnSummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSummary.Click
        If IsDate(dtpDateFrom.Text) Or IsDate(dtpDateTo.Text) Or
        (cmbSearch.SelectedIndex > -1 And Trim(txtSearch.Text) <> "") Then
            Dim Formula As String = ""
            If cmbARType.SelectedIndex < 3 Then
                Formula += "{Charges.ArType} = " & cmbARType.SelectedIndex
            End If

            'TODO: Crystal Report
            '=======================
            'Dim oRPT As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            'oRPT.Load(GetReportPath("ARSummary.rpt"))
            'ApplyNewServer(oRPT, My.Settings.DSN, My.Settings.Database, My.Settings.UID, My.Settings.PWD)
            '
            '======================================
            If cmbDateType.SelectedIndex = 2 Then  'Service Date
                If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
                    '{Charges.Svc_Date} in DateTime (2012, 03, 01, 16, 00, 00) to DateTime (2012, 03, 29, 19, 10, 00)
                    If Formula = "" Then
                        Formula += "{Charges.Svc_Date} in Date(" &
                        Year(CDate(dtpDateFrom.Text)) & ", " &
                        Month(CDate(dtpDateFrom.Text)) & ", " &
                        CDate(dtpDateFrom.Text).Day & ") To " &
                        "Date(" & Year(CDate(dtpDateFrom.Text)) & ", " &
                        Month(CDate(dtpDateFrom.Text)) & ", " &
                        CDate(dtpDateFrom.Text).Day & ")"
                    Else
                        Formula += " and {Charges.Svc_Date} in Date(" &
                        Year(CDate(dtpDateFrom.Text)) & ", " &
                        Month(CDate(dtpDateFrom.Text)) & ", " &
                        CDate(dtpDateFrom.Text).Day & ") To " &
                        "Date(" & Year(CDate(dtpDateFrom.Text)) & ", " &
                        Month(CDate(dtpDateFrom.Text)) & ", " &
                        CDate(dtpDateFrom.Text).Day & ")"
                    End If
                ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                    If Formula = "" Then
                        Formula += "{Charges.Svc_Date} in Date(" &
                        Year(CDate(dtpDateTo.Text)) & ", " &
                        Month(CDate(dtpDateTo.Text)) & ", " &
                        CDate(dtpDateTo.Text).Day & ") To " &
                        "Date(" & Year(CDate(dtpDateTo.Text)) & ", " &
                        Month(CDate(dtpDateTo.Text)) & ", " &
                        CDate(dtpDateTo.Text).Day & ")"
                    Else
                        Formula += " and {Charges.Svc_Date} in Date(" &
                        Year(CDate(dtpDateTo.Text)) & ", " &
                        Month(CDate(dtpDateTo.Text)) & ", " &
                        CDate(dtpDateTo.Text).Day & ") To " &
                        "Date(" & Year(CDate(dtpDateTo.Text)) & ", " &
                        Month(CDate(dtpDateTo.Text)) & ", " &
                        CDate(dtpDateTo.Text).Day & ")"
                    End If
                ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                    If Formula = "" Then
                        Formula += "{Charges.Svc_Date} in Date(" &
                        Year(CDate(dtpDateFrom.Text)) & ", " &
                        Month(CDate(dtpDateFrom.Text)) & ", " &
                        CDate(dtpDateFrom.Text).Day & ") To " &
                        "Date(" & Year(CDate(dtpDateTo.Text)) & ", " &
                        Month(CDate(dtpDateTo.Text)) & ", " &
                        CDate(dtpDateTo.Text).Day & ")"
                    Else
                        Formula += " and {Charges.Svc_Date} in Date(" &
                        Year(CDate(dtpDateFrom.Text)) & ", " &
                        Month(CDate(dtpDateFrom.Text)) & ", " &
                        CDate(dtpDateFrom.Text).Day & ") To " &
                        "Date(" & Year(CDate(dtpDateTo.Text)) & ", " &
                        Month(CDate(dtpDateTo.Text)) & ", " &
                        CDate(dtpDateTo.Text).Day & ")"
                    End If
                End If
            ElseIf cmbDateType.SelectedIndex = 1 Then  'Receive Date
                If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
                    '{Charges.Svc_Date} in DateTime (2012, 03, 01, 16, 00, 00) to DateTime (2012, 03, 29, 19, 10, 00)
                    If Formula = "" Then
                        Formula += "{Requisitions.ReceivedTime} in Date(" &
                        Year(CDate(dtpDateFrom.Text)) & ", " &
                        Month(CDate(dtpDateFrom.Text)) & ", " &
                        CDate(dtpDateFrom.Text).Day & ") To " &
                        "Date(" & Year(CDate(dtpDateFrom.Text)) & ", " &
                        Month(CDate(dtpDateFrom.Text)) & ", " &
                        CDate(dtpDateFrom.Text).Day & ")"
                    Else
                        Formula += " and {Requisitions.ReceivedTime} in Date(" &
                        Year(CDate(dtpDateFrom.Text)) & ", " &
                        Month(CDate(dtpDateFrom.Text)) & ", " &
                        CDate(dtpDateFrom.Text).Day & ") To " &
                        "Date(" & Year(CDate(dtpDateFrom.Text)) & ", " &
                        Month(CDate(dtpDateFrom.Text)) & ", " &
                        CDate(dtpDateFrom.Text).Day & ")"
                    End If
                ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                    If Formula = "" Then
                        Formula += "{Requisitions.ReceivedTime} in Date(" &
                        Year(CDate(dtpDateTo.Text)) & ", " &
                        Month(CDate(dtpDateTo.Text)) & ", " &
                        CDate(dtpDateTo.Text).Day & ") To " &
                        "Date(" & Year(CDate(dtpDateTo.Text)) & ", " &
                        Month(CDate(dtpDateTo.Text)) & ", " &
                        CDate(dtpDateTo.Text).Day & ")"
                    Else
                        Formula += " and {Requisitions.ReceivedTime} in Date(" &
                        Year(CDate(dtpDateTo.Text)) & ", " &
                        Month(CDate(dtpDateTo.Text)) & ", " &
                        CDate(dtpDateTo.Text).Day & ") To " &
                        "Date(" & Year(CDate(dtpDateTo.Text)) & ", " &
                        Month(CDate(dtpDateTo.Text)) & ", " &
                        CDate(dtpDateTo.Text).Day & ")"
                    End If
                ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                    If Formula = "" Then
                        Formula += "{Requisitions.ReceivedTime} in Date(" &
                        Year(CDate(dtpDateFrom.Text)) & ", " &
                        Month(CDate(dtpDateFrom.Text)) & ", " &
                        CDate(dtpDateFrom.Text).Day & ") To " &
                        "Date(" & Year(CDate(dtpDateTo.Text)) & ", " &
                        Month(CDate(dtpDateTo.Text)) & ", " &
                        CDate(dtpDateTo.Text).Day & ")"
                    Else
                        Formula += " and {Requisitions.ReceivedTime} in Date(" &
                        Year(CDate(dtpDateFrom.Text)) & ", " &
                        Month(CDate(dtpDateFrom.Text)) & ", " &
                        CDate(dtpDateFrom.Text).Day & ") To " &
                        "Date(" & Year(CDate(dtpDateTo.Text)) & ", " &
                        Month(CDate(dtpDateTo.Text)) & ", " &
                        CDate(dtpDateTo.Text).Day & ")"
                    End If
                End If
            Else    'Bill Date
                If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
                    If Formula = "" Then
                        Formula += "{Charges.Bill_Date} in Date(" &
                        Year(CDate(dtpDateFrom.Text)) & ", " &
                        Month(CDate(dtpDateFrom.Text)) & ", " &
                        CDate(dtpDateFrom.Text).Day & ") To " &
                        "Date(" & Year(CDate(dtpDateFrom.Text)) & ", " &
                        Month(CDate(dtpDateFrom.Text)) & ", " &
                        CDate(dtpDateFrom.Text).Day & ")"
                    Else
                        Formula += " and {Charges.Bill_Date} in Date(" &
                        Year(CDate(dtpDateFrom.Text)) & ", " &
                        Month(CDate(dtpDateFrom.Text)) & ", " &
                        CDate(dtpDateFrom.Text).Day & ") To " &
                        "Date(" & Year(CDate(dtpDateFrom.Text)) & ", " &
                        Month(CDate(dtpDateFrom.Text)) & ", " &
                        CDate(dtpDateFrom.Text).Day & ")"
                    End If
                ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                    If Formula = "" Then
                        Formula += "{Charges.Bill_Date} in Date(" &
                        Year(CDate(dtpDateTo.Text)) & ", " &
                        Month(CDate(dtpDateTo.Text)) & ", " &
                        CDate(dtpDateTo.Text).Day & ") To " &
                        "Date(" & Year(CDate(dtpDateTo.Text)) & ", " &
                        Month(CDate(dtpDateTo.Text)) & ", " &
                        CDate(dtpDateTo.Text).Day & ")"
                    Else
                        Formula += " and {Charges.Bill_Date} in Date(" &
                        Year(CDate(dtpDateTo.Text)) & ", " &
                        Month(CDate(dtpDateTo.Text)) & ", " &
                        CDate(dtpDateTo.Text).Day & ") To " &
                        "Date(" & Year(CDate(dtpDateTo.Text)) & ", " &
                        Month(CDate(dtpDateTo.Text)) & ", " &
                        CDate(dtpDateTo.Text).Day & ")"
                    End If
                ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                    If Formula = "" Then
                        Formula += "{Charges.Bill_Date} in Date(" &
                        Year(CDate(dtpDateFrom.Text)) & ", " &
                        Month(CDate(dtpDateFrom.Text)) & ", " &
                        CDate(dtpDateFrom.Text).Day & ") To " &
                        "Date(" & Year(CDate(dtpDateTo.Text)) & ", " &
                        Month(CDate(dtpDateTo.Text)) & ", " &
                        CDate(dtpDateTo.Text).Day & ")"
                    Else
                        Formula += " and {Charges.Bill_Date} in Date(" &
                        Year(CDate(dtpDateFrom.Text)) & ", " &
                        Month(CDate(dtpDateFrom.Text)) & ", " &
                        CDate(dtpDateFrom.Text).Day & ") To " &
                        "Date(" & Year(CDate(dtpDateTo.Text)) & ", " &
                        Month(CDate(dtpDateTo.Text)) & ", " &
                        CDate(dtpDateTo.Text).Day & ")"
                    End If
                End If
            End If
            '
            If cmbSearch.SelectedIndex = 0 Then     'Provider ID
                If Trim(txtSearch.Text) <> "" AndAlso IsNumeric(Trim(txtSearch.Text)) Then
                    If Formula = "" Then
                        Formula += "{Requisitions.OrderingProvider_ID} = " &
                        CLng(Trim(txtSearch.Text))
                    Else
                        Formula += " and {Requisitions.OrderingProvider_ID} = " &
                        CLng(Trim(txtSearch.Text))
                    End If
                End If
            ElseIf cmbSearch.SelectedIndex = 1 Then     'Provider Name
                If Trim(txtSearch.Text) <> "" Then
                    Dim LName As String = ""
                    Dim FName As String = ""
                    Dim Names() As String
                    If InStr(Trim(txtSearch.Text), ",") > 0 Then
                        Names = Split(Trim(txtSearch.Text), ",")
                        LName = Trim(Names(0))
                        FName = Trim(Names(1))
                    Else
                        LName = Trim(txtSearch.Text)
                    End If
                    If FName <> "" And LName <> "" Then
                        If Formula = "" Then
                            Formula += "{Providers_AR.LastName_BSN} like '" & LName _
                            & "*' and {Providers_AR.FirstName} like '" & FName & "*'"
                        Else
                            Formula += " and {Providers_AR.LastName_BSN} like '" & LName _
                            & "*' and {Providers_AR.FirstName} like '" & FName & "*'"
                        End If
                    ElseIf FName = "" And LName <> "" Then
                        If Formula = "" Then
                            Formula += "{Providers_AR.LastName_BSN} like '" & LName & "*'"
                        Else
                            Formula += " and {Providers_AR.LastName_BSN} like '" & LName & "*'"
                        End If
                    End If
                End If
            ElseIf cmbSearch.SelectedIndex = 2 Then     'Insurance ID
                If Trim(txtSearch.Text) <> "" AndAlso IsNumeric(Trim(txtSearch.Text)) Then
                    If Formula = "" Then
                        Formula += "{Requisitions.PrimePayer_ID} = " & CLng(Trim(txtSearch.Text))
                    Else
                        Formula += " and {Requisitions.PrimePayer_ID} = " & CLng(Trim(txtSearch.Text))
                    End If
                End If
            ElseIf cmbSearch.SelectedIndex = 3 Then     'Insurance Name
                If Trim(txtSearch.Text) <> "" Then
                    If Formula = "" Then
                        Formula += "{Payers.PayerName} like '" & Trim(txtSearch.Text) & "*'"
                    Else
                        Formula += " and {Payers.PayerName} like '" & Trim(txtSearch.Text) & "*'"
                    End If
                End If
            ElseIf cmbSearch.SelectedIndex = 4 Then     'Patient ID
                If Trim(txtSearch.Text) <> "" AndAlso IsNumeric(Trim(txtSearch.Text)) Then
                    If Formula = "" Then
                        Formula += "{Requisitions.Patient_ID} = " & CLng(Trim(txtSearch.Text))
                    Else
                        Formula += " and {Requisitions.Patient_ID} = " & CLng(Trim(txtSearch.Text))
                    End If
                End If
            ElseIf cmbSearch.SelectedIndex = 5 Then     'Patient Name
                If Trim(txtSearch.Text) <> "" Then
                    Dim LName As String = ""
                    Dim FName As String = ""
                    Dim Names() As String
                    If InStr(Trim(txtSearch.Text), ",") > 0 Then
                        Names = Split(Trim(txtSearch.Text), ",")
                        LName = Trim(Names(0))
                        FName = Trim(Names(1))
                    Else
                        LName = Trim(txtSearch.Text)
                    End If
                    If FName <> "" And LName <> "" Then
                        If Formula = "" Then
                            Formula += "{Patients.LastName} like '" & LName &
                            "*' and {Patients.FirstName} like '" & FName & "*'"
                        Else
                            Formula += " and {Patients.LastName} like '" & LName &
                            "*' and {Patients.FirstName} like '" & FName & "*'"
                        End If
                    ElseIf FName = "" And LName <> "" Then
                        If Formula = "" Then
                            Formula += "{Patients.LastName} like '" & LName & "*'"
                        Else
                            Formula += " and {Patients.LastName} like '" & LName & "*'"
                        End If
                    End If
                End If
            ElseIf cmbSearch.SelectedIndex = 6 Then     'Accessions
                If Trim(txtSearch.Text) <> "" Then
                    Dim AccFrom As String = ""
                    Dim AccTo As String = ""
                    Dim Accs() As String
                    If InStr(Trim(txtSearch.Text), ",") > 0 Then
                        Accs = Split(Trim(txtSearch.Text), ",")
                        AccFrom = Trim(Accs(0))
                        AccTo = Trim(Accs(1))
                    Else
                        AccFrom = Trim(txtSearch.Text)
                    End If
                    If AccFrom <> "" And AccTo <> "" Then
                        If Formula = "" Then
                            Formula += "{Requisitions.ID} in [" & CLng(AccFrom) _
                            & " To " & CLng(AccTo) & "]"
                        Else
                            Formula += " and {Requisitions.ID} in [" & CLng(AccFrom) _
                            & " To " & CLng(AccTo) & "]"
                        End If
                    ElseIf AccFrom <> "" And AccTo = "" Then
                        If Formula = "" Then
                            Formula += "{Requisitions.ID} = " & CLng(AccFrom)
                        Else
                            Formula += " and {Requisitions.ID} = " & CLng(AccFrom)
                        End If
                    ElseIf AccFrom = "" And AccTo <> "" Then
                        If Formula = "" Then
                            Formula += "{Requisitions.ID} = " & CLng(AccTo)
                        Else
                            Formula += " and {Requisitions.ID} = " & CLng(AccTo)
                        End If
                    End If
                End If
            ElseIf cmbSearch.SelectedIndex = 7 Then     'Document ID
                If Formula = "" Then
                    Formula += "{Payments.DocNo} = '" & Trim(txtSearch.Text) & "'"
                Else
                    Formula += " and {Payments.DocNo} = '" & Trim(txtSearch.Text) & "'"
                End If
            End If
            'TODO: Crystal Report
            '=================================
            'oRPT.RecordSelectionFormula = Formula
            'frmRV.CRRV.ReportSource = oRPT
            '=================================
            frmRV.Show()
            frmRV.MdiParent = frmDashboard
        Else
            MsgBox("Make at least one selection, 'Date' or ('Criteria' and 'Term')", MsgBoxStyle.Critical, "Prolis")
        End If
    End Sub

    Private Sub dgvDetail_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDetail.CellClick
        Dim c = e.ColumnIndex
        If e.RowIndex = -1 Then
            Return
        End If
        dgvDetail.Rows(e.RowIndex).Cells(e.ColumnIndex).ToString()
        If e.RowIndex <> -1 Then

            Try
                Clipboard.SetText(dgvDetail.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                clipboardMsg.ForeColor = Color.Red
                clipboardMsg.Text = dgvDetail.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & ""
            Catch ex As Exception

            End Try
        End If

    End Sub

    Private Sub Label24_Click(sender As Object, e As EventArgs) Handles Label24.Click
        txtSearch.Text = Clipboard.GetText()
    End Sub


    Function GetBillingQuery() As String
        Dim query As String = "DROP TABLE IF EXISTS #Charges;" & vbCrLf &
                      "DROP TABLE IF EXISTS #Charge_Detail;" & vbCrLf &
                      "DROP TABLE IF EXISTS #Payment_Detail;" & vbCrLf &
                      "DROP TABLE IF EXISTS #Payments;" & vbCrLf &
                      "DROP TABLE IF EXISTS #Requisitions;" & vbCrLf &
                      "SELECT * INTO #Charges FROM Charges c WHERE c.Svc_Date BETWEEN @FromDate AND @ToDate;" & vbCrLf &
                      "SELECT * INTO #Charge_Detail FROM Charge_detail WHERE charge_id IN (" & vbCrLf &
                      "    SELECT id FROM #Charges c WHERE c.Svc_Date BETWEEN @FromDate AND @ToDate" & vbCrLf &
                      ");" & vbCrLf &
                      "SELECT * INTO #Payment_Detail FROM Payment_Detail pd WHERE pd.Charge_ID IN (" & vbCrLf &
                      "    SELECT id FROM #Charges c WHERE c.Svc_Date BETWEEN @FromDate AND @ToDate" & vbCrLf &
                      ");" & vbCrLf &
                      "SELECT * INTO #Payments FROM Payments WHERE ID IN (" & vbCrLf &
                      "    SELECT pd.Payment_ID FROM #Payment_Detail pd WHERE pd.Charge_ID IN" & vbCrLf &
                      "    (SELECT id FROM #Charges c WHERE c.Svc_Date BETWEEN @FromDate AND @ToDate)" & vbCrLf &
                      ");" & vbCrLf &
                      "SELECT" & vbCrLf &
                      "    b.PrimePayer_ID," & vbCrLf &
                      "    a.Accession_ID AS Accession," & vbCrLf &
                      "    a.ID AS Invoice," & vbCrLf &
                      "    ISNULL(CONVERT(NVARCHAR, a.Svc_Date, 101), '') AS [Svc Date]," & vbCrLf &
                      "    (CASE" & vbCrLf &
                      "        WHEN a.ArType = 0 THEN d.LastName_BSN + '. ' + d.FirstName" & vbCrLf &
                      "        WHEN a.ArType = 1 THEN e.PayerName" & vbCrLf &
                      "        ELSE c.LastName + '. ' + c.FirstName" & vbCrLf &
                      "    END) AS [Bill To]," & vbCrLf &
                      "    (c.LastName + '. ' + c.FirstName) AS Patient," & vbCrLf &
                      "    ISNULL(a.GrossAmount, 0) AS [Bill Amt]," & vbCrLf &
                      "    ISNULL((SELECT TOP 1 f.DocNo FROM #Payments f" & vbCrLf &
                      "            INNER JOIN (#Payment_Detail g" & vbCrLf &
                      "                INNER JOIN #Charge_Detail h ON h.Charge_ID = g.Charge_ID)" & vbCrLf &
                      "            ON g.Payment_ID = f.ID" & vbCrLf &
                      "            WHERE g.Charge_ID = a.ID), '') AS [Doc No]," & vbCrLf &
                      "    ISNULL(CONVERT(NVARCHAR, (SELECT TOP 1 f.PaymentDate FROM #Payments f" & vbCrLf &
                      "            INNER JOIN (#Payment_Detail g" & vbCrLf &
                      "                INNER JOIN #Charge_Detail h ON h.Charge_ID = g.Charge_ID)" & vbCrLf &
                      "            ON g.Payment_ID = f.ID" & vbCrLf &
                      "            WHERE g.Charge_ID = a.ID), 101), '') AS [Trn Date]," & vbCrLf &
                      "    ISNULL((SELECT SUM(AppliedAmount) FROM #Payment_Detail WHERE Charge_ID = a.ID), 0) AS Payment," & vbCrLf &
                      "    ISNULL((SELECT SUM(WrittenOff) FROM #Payment_Detail WHERE Charge_ID = a.ID), 0) AS [W.O.]," & vbCrLf &
                      "    ISNULL(a.GrossAmount - ISNULL((SELECT SUM(AppliedAmount) FROM #Payment_Detail WHERE Charge_ID = a.ID), 0)" & vbCrLf &
                      "            - ISNULL((SELECT SUM(WrittenOff) FROM #Payment_Detail WHERE Charge_ID = a.ID), 0), 0) AS Balance," & vbCrLf &
                      "    (CASE" & vbCrLf &
                      "        WHEN ISNULL((SELECT TOP 1 f.DocNo FROM #Payments f" & vbCrLf &
                      "                    INNER JOIN (#Payment_Detail g" & vbCrLf &
                      "                        INNER JOIN #Charge_Detail h ON h.Charge_ID = g.Charge_ID)" & vbCrLf &
                      "                    ON g.Payment_ID = f.ID" & vbCrLf &
                      "                    WHERE g.Charge_ID = a.ID), '') <> '' THEN DATEDIFF(DAY, a.Bill_Date," & vbCrLf &
                      "                    (SELECT TOP 1 f.PaymentDate FROM #Payments f" & vbCrLf &
                      "                    INNER JOIN (#Payment_Detail g" & vbCrLf &
                      "                        INNER JOIN #Charge_Detail h ON h.Charge_ID = g.Charge_ID)" & vbCrLf &
                      "                    ON g.Payment_ID = f.ID" & vbCrLf &
                      "                    WHERE g.Charge_ID = a.ID))" & vbCrLf &
                      "        ELSE DATEDIFF(DAY, a.Bill_Date, GETDATE())" & vbCrLf &
                      "    END) AS Age" & vbCrLf &
                      "FROM #Charges a" & vbCrLf &
                      "INNER JOIN (Patients c" & vbCrLf &
                      "    INNER JOIN (Providers d" & vbCrLf &
                      "        INNER JOIN (Requisitions b" & vbCrLf &
                      "            JOIN Payers e ON e.ID = b.PrimePayer_ID)" & vbCrLf &
                      "        ON b.OrderingProvider_ID = d.ID)" & vbCrLf &
                      "    ON c.ID = b.Patient_ID)" & vbCrLf &
                      "ON b.ID = a.Accession_ID" & vbCrLf &
                      "WHERE   b.PrimePayer_ID = @PrimePayer_ID" & vbCrLf &
                      "AND a.Svc_Date BETWEEN @FromDate AND @ToDate;"
        Return query
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ExportDataGridViewToCSV(dgvDetail)
    End Sub
    Private Sub ExportDataGridViewToCSV(ByVal dataGridView As DataGridView)
        ' Show the save file dialog to let the user choose the location and file name
        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*"
        saveFileDialog.FilterIndex = 1
        saveFileDialog.RestoreDirectory = True

        If saveFileDialog.ShowDialog() = DialogResult.OK Then
            ' Open the file for writing
            Using writer As New StreamWriter(saveFileDialog.FileName)

                ' Write the column headers to the CSV file
                Dim headers As String = String.Join(",", dataGridView.Columns.Cast(Of DataGridViewColumn).Select(Function(column) column.HeaderText))
                writer.WriteLine(headers)

                ' Write each row of data to the CSV file
                For Each row As DataGridViewRow In dataGridView.Rows
                    Dim cells As String() = row.Cells.Cast(Of DataGridViewCell).Select(Function(cell) If(cell.Value IsNot Nothing, cell.Value.ToString(), "")).ToArray()
                    writer.WriteLine(String.Join(",", cells))
                Next

            End Using

            ' Notify the user that the export is complete
            MessageBox.Show("Export to CSV file complete.", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        If cmbSearch.SelectedIndex <> 6 Then
            Return

        End If
        Dim sqlQuery As String = "DECLARE @accid AS bigint = " & txtSearch.Text &
    " DECLARE @targetTGP TABLE (TGP_ID int)" &
    " INSERT INTO @targetTGP (TGP_ID)" &
    " SELECT DISTINCT TGP_ID" &
    " FROM Req_TGP" &
    " WHERE Accession_ID = @accid" &
    " DECLARE @CurrentTGP int" &
    " DECLARE cur CURSOR FOR" &
    " SELECT TGP_ID FROM @targetTGP" &
    " OPEN cur" &
    " FETCH NEXT FROM cur INTO @CurrentTGP" &
    " WHILE @@FETCH_STATUS = 0" &
    " BEGIN" &
    " DECLARE @targetChargeID AS int = (" &
    " SELECT DISTINCT Charge_ID" &
    " FROM Payment_Detail" &
    " WHERE Charge_ID IN (" &
    " SELECT ID" &
    " FROM Charges" &
    " WHERE Accession_ID = @accid" &
    " )" &
    " AND Charge_ID NOT IN (" &
    " SELECT DISTINCT Charge_ID" &
    " FROM Charge_Detail" &
    " WHERE Charge_ID IN (" &
    " SELECT ID" &
    " FROM Charges" &
    " WHERE Accession_ID = @accid" &
    " )" &
    " AND POS_Code = 81" &
    " ))" &
    " DECLARE @targetChargeID81 AS int = (" &
    " SELECT DISTINCT Charge_ID" &
    " FROM Charge_Detail" &
    " WHERE Charge_ID IN (" &
    " SELECT ID" &
    " FROM Charges" &
    " WHERE Accession_ID = @accid" &
    " )" &
    " AND POS_Code = 81" &
    " )" &
    " UPDATE Payment_Detail" &
    " SET WrittenOff = ChargeAmount - (" &
    " (SELECT LinePrice" &
    " FROM Charge_Detail" &
    " WHERE Charge_ID = @targetChargeID81" &
    " AND tgp_id = @CurrentTGP) + AppliedAmount" &
    " )" &
    " WHERE TGP_ID = @CurrentTGP" &
    " AND Charge_ID = @targetChargeID" &
    " UPDATE Payment_Detail" &
    " SET Balance = ChargeAmount - WrittenOff" &
    " WHERE TGP_ID = @CurrentTGP" &
    " AND Charge_ID = @targetChargeID" &
    " FETCH NEXT FROM cur INTO @CurrentTGP" &
    " END" &
    " CLOSE cur" &
    " DEALLOCATE cur"


        ExecuteSqlProcedure(sqlQuery)
        btnGo.PerformClick()
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        Dim f = DirectCast(sender, System.Windows.Forms.OpenFileDialog).FileName
        Dim txt = ""
        Using s = New StreamReader(f)
            txt = s.ReadToEnd()
        End Using
        AccList.AddRange(txt.Split(Environment.NewLine))
    End Sub



    Private Sub Button3_Click(sender As Object, e As EventArgs)
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub nxt_Click(sender As Object, e As EventArgs)

        Dim accid = AccList(Convert.ToInt32(cnt.Text))
        If Not IsNumeric(accid) Then
            If AccList.Count = Convert.ToInt32(cnt.Text) + 1 Then
                Return
            End If
            cnt.Text = Convert.ToInt32(cnt.Text) + 1
            Return
        End If
        txtSearch.Text = accid
        btnGo.PerformClick()
        If AccList.Count = Convert.ToInt32(cnt.Text) + 1 Then
            Return
        End If
        cnt.Text = Convert.ToInt32(cnt.Text) + 1
    End Sub

    Private Sub prev_Click(sender As Object, e As EventArgs)
        Dim accid = AccList(Convert.ToInt32(cnt.Text))
        If Not IsNumeric(accid) Then
            If (Convert.ToInt32(cnt.Text) - 1) < 0 Then
                Return
            End If
            cnt.Text = Convert.ToInt32(cnt.Text) - 1
            Return
        End If
        txtSearch.Text = accid
        btnGo.PerformClick()
        If (Convert.ToInt32(cnt.Text) - 1) < 0 Then
            Return
        End If
        cnt.Text = Convert.ToInt32(cnt.Text) - 1
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)
        If dgvDetail.SelectedCells()(0).ColumnIndex = 1 Then
            Dim s = dgvDetail.SelectedCells()(0).Value
            Dim r = MessageBox.Show("You want to delete Invoice " & s, "", MessageBoxButtons.YesNo)
            If r = DialogResult.OK Then
                ExecuteSqlProcedure("delete from Charge_Detail where Charge_ID =   " & s)
                ExecuteSqlProcedure("delete from Payment_Detail  where Charge_ID =  " & s)
                ExecuteSqlProcedure("delete from Charges where Charge_ID = " & s)
                btnGo.PerformClick()
            End If

        End If

    End Sub


    Private Sub dgvDetail_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDetail.CellDoubleClick
        Dim c = e.ColumnIndex
        If e.RowIndex = -1 Then
            Return
        End If
        dgvDetail.Rows(e.RowIndex).Cells(e.ColumnIndex).ToString()
        If e.RowIndex <> -1 Then
            If e.ColumnIndex = 0 Then
                Try
                    Clipboard.SetText(dgvDetail.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                    clipboardMsg.ForeColor = Color.Red
                    clipboardMsg.Text = dgvDetail.Rows(e.RowIndex).Cells(e.ColumnIndex).Value

                    frmBillingEdit.Close()

                    frmBillingEdit.Show()
                    frmBillingEdit.MdiParent = frmDashboard
                    frmBillingEdit.dgvDiscrete.Rows.Clear()
                    frmBillingEdit.dgvDiscrete.Rows.Add(clipboardMsg.Text)
                    ' frmBillingEdit.cmbABU.SelectedIndex = cmbARType.SelectedIndex\ _
                    Dim vl = dgvDetail.Rows(e.RowIndex).Cells(e.ColumnIndex + 3).Value.ToString().Split("_")(1).Trim(" _")
                    frmBillingEdit.cmbBillType.SelectedIndex = vl
                    frmBillingEdit.cmbABU.SelectedIndex = 0
                    frmBillingEdit.lblClearDates_Click1(sender, e)
                    frmBillingEdit.btnTarget.PerformClick()
                    frmBillingEdit.btnLoad.PerformClick()

                Catch ex As Exception

                End Try
            ElseIf e.ColumnIndex = 4 Then
                Try
                    Clipboard.SetText(dgvDetail.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                    clipboardMsg.ForeColor = Color.Red
                    clipboardMsg.Text = dgvDetail.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
                    Dim accID = dgvDetail.Rows(e.RowIndex).Cells(0).Value
                    Dim contents = CommonData.RetrieveColumnValue("Requisitions", "Patient_ID", "ID", "'" & accID & "'", "")

                    frmPatient.Close()

                    frmPatient.Show()
                    frmPatient.Activate()
                    frmPatient.chkNewEdit.PerformClick()
                    frmPatient.txtPatientID.Text = contents
                    frmPatient.txtPatientID.Focus()
                    SendKeys.Send("{TAB}")

                Catch ex As Exception

                End Try
            ElseIf e.ColumnIndex = 6 Then
                Try
                    Clipboard.SetText(dgvDetail.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                    clipboardMsg.ForeColor = Color.Red
                    clipboardMsg.Text = dgvDetail.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
                    frmPayments.Close()
                    Dim value1 As String = dgvDetail.Rows(e.RowIndex).Cells(0).Value
                    Dim value2 As String = dgvDetail.Rows(e.RowIndex).Cells(1).Value 

                    frmPayments.txtID.Text = ""

                    frmPayments.Show()
                    frmPayments.chkEditNew.PerformClick()
                    frmPayments.txtDoc.Text = dgvDetail.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
                    frmPayments.MdiParent = frmDashboard
                    frmPayments.txtDoc.Focus()
                    Dim shownHandler As EventHandler = Sub(s, ea)
                                                           OnFrmPaymentsShown(s, ea, value1, value2)
                                                           SelectInvoiceRow(value1, value2)
                                                           RemoveHandler frmPayments.Shown, shownHandler
                                                       End Sub
                    frmPayments.SelectNextControl(frmPayments.txtDoc, True, True, True, True)
                    SelectInvoiceRow(value1, value2)
                Catch ex As Exception

                End Try
            End If

        End If
    End Sub
    Private Sub OnFrmPaymentsShown(sender As Object, e As EventArgs, value1 As String, value2 As String)

        ' Simulate Tab key press to move focus to the next control
        frmPayments.SelectNextControl(frmPayments.txtDoc, True, True, True, True)
        SelectInvoiceRow(value1, value2)

    End Sub
    Private Sub SelectInvoiceRow(value1 As String, value2 As String)
        For Each row As DataGridViewRow In frmPayments.dgvInvoices.Rows
            If row.Cells(0).Value IsNot Nothing AndAlso row.Cells(1).Value IsNot Nothing Then
                If row.Cells(0).Value.ToString() = value2 AndAlso row.Cells(1).Value.ToString() = value1 Then
                    ' Select the row
                    row.Selected = True

                    ' Optionally, scroll to the row to make it visible
                    frmPayments.dgvInvoices.FirstDisplayedScrollingRowIndex = row.Index

                    ' Optionally, trigger the cell click event
                    frmPayments.dgvInvoices.CurrentCell = row.Cells(0)
                    frmPayments.dgvInvoices.BeginEdit(True)

                    Exit For
                End If
            End If
        Next
    End Sub

    Private Sub cmbSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSearch.SelectedIndexChanged
        txtSearch.Text = ""
        searchID.Text = ""
        If cmbSearch.SelectedIndex = 3 Then
            Dim PayerInfo As String = frmActivePayersLookUp.ShowDialog()
            If PayerInfo <> "" Then
                Dim PRS() As String = Split(PayerInfo, "|")
                txtSearch.Text = PRS(1)
                searchID.Text = PRS(0)

            End If
        ElseIf cmbSearch.SelectedIndex = 5 Then
            Dim PatientID As String = frmPatLookUp_NameID.ShowDialog()
            If PatientID <> "" Then
                Dim Pr() = PatientID.Split("|")
                txtSearch.Text = Pr(1) + " ," + Pr(2)
                searchID.Text = Pr(0)
            End If

        End If

    End Sub

    Private Sub dtpDateFrom_CloseUp(sender As Object, e As EventArgs) Handles dtpDateFrom.CloseUp
        ' After selecting a valid date, revert to the standard date format
        CloseUpDateTimePicker(dtpDateFrom)
    End Sub
    Private Sub dtpDateTo_CloseUp(sender As Object, e As EventArgs) Handles dtpDateTo.CloseUp
        CloseUpDateTimePicker(dtpDateTo)
    End Sub

    Private Sub lblClearDates_Click(sender As Object, e As EventArgs) Handles lblClearDates.Click
        ClearDateTimePicker(dtpDateFrom)
        ClearDateTimePicker(dtpDateTo)
    End Sub
End Class