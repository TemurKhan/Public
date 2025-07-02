Public Class QueryProvider
     
    Public Shared Function AR_By_Date_Insurance(ByVal DateType As Integer, ByVal billingType As Integer, ByVal searchIndex As Integer, Optional searchText As String = "") As String
        Dim dateColName = ""
        Dim Tables = "SELECT * INTO #Charges FROM Charges c WHERE " & dateColName & " BETWEEN @FromDate AND @ToDate;" & vbCrLf &
                           "SELECT * INTO #AccessionsTemp FROM Requisitions WHERE ID IN (SELECT Accession_ID FROM #Charges);" & vbCrLf &
                           "SELECT * INTO #Charge_Detail FROM Charge_detail WHERE charge_id IN (SELECT id FROM #Charges);" & vbCrLf &
                           vbCrLf &
                           "SELECT * INTO #Payment_Detail FROM Payment_Detail pd WHERE pd.Charge_ID IN (SELECT id FROM #Charges);" & vbCrLf &
                           "SELECT * INTO #Payments FROM Payments WHERE ID IN (SELECT Payment_ID FROM #Payment_Detail);"
        '' Search index 2 - - - - - -  Start
        If searchIndex = -1 Then
            If DateType = 0 Then
                dateColName = "Bill_Date"
                Tables = "SELECT * INTO #Charges FROM Charges c WHERE " & dateColName & " BETWEEN @FromDate AND @ToDate;" & vbCrLf &
                               "SELECT * INTO #AccessionsTemp FROM Requisitions WHERE ID IN (SELECT Accession_ID FROM #Charges);" & vbCrLf &
                               "SELECT * INTO #Charge_Detail FROM Charge_detail WHERE charge_id IN (SELECT id FROM #Charges);" & vbCrLf &
                               vbCrLf &
                               "SELECT * INTO #Payment_Detail FROM Payment_Detail pd WHERE pd.Charge_ID IN (SELECT id FROM #Charges);" & vbCrLf &
                               "SELECT * INTO #Payments FROM Payments WHERE ID IN (SELECT Payment_ID FROM #Payment_Detail);"
            ElseIf DateType = 1 Then
                dateColName = "ReceivedTime"
                Tables = "SELECT * INTO #AccessionsTemp FROM Requisitions WHERE ReceivedTime BETWEEN @FromDate AND @ToDate;" & vbCrLf &
                             vbCrLf &
                             "SELECT * INTO #Charges FROM Charges c WHERE c.Accession_ID IN (SELECT ID FROM #AccessionsTemp);" & vbCrLf &
                             "SELECT * INTO #Charge_Detail FROM Charge_detail WHERE charge_id IN (SELECT id FROM #Charges);" & vbCrLf &
                             vbCrLf &
                             "SELECT * INTO #Payment_Detail FROM Payment_Detail pd WHERE pd.Charge_ID IN (SELECT id FROM #Charges);" & vbCrLf &
                             "SELECT * INTO #Payments FROM Payments WHERE ID IN (SELECT Payment_ID FROM #Payment_Detail);"

            ElseIf DateType = 2 Then

                dateColName = "Svc_Date"
                Tables = "SELECT * INTO #Charges FROM Charges c WHERE " & dateColName & " BETWEEN @FromDate AND @ToDate;" & vbCrLf &
                               "SELECT * INTO #AccessionsTemp FROM Requisitions WHERE ID IN (SELECT Accession_ID FROM #Charges);" & vbCrLf &
                               "SELECT * INTO #Charge_Detail FROM Charge_detail WHERE charge_id IN (SELECT id FROM #Charges);" & vbCrLf &
                               vbCrLf &
                               "SELECT * INTO #Payment_Detail FROM Payment_Detail pd WHERE pd.Charge_ID IN (SELECT id FROM #Charges);" & vbCrLf &
                               "SELECT * INTO #Payments FROM Payments WHERE ID IN (SELECT Payment_ID FROM #Payment_Detail);"
            End If
        End If
        If searchIndex = 2 Then
            If DateType = 0 Then
                dateColName = "Bill_Date"
                Tables = "SELECT * INTO #Charges FROM Charges c WHERE " & dateColName & " BETWEEN @FromDate AND @ToDate;" & vbCrLf &
                               "SELECT * INTO #AccessionsTemp FROM Requisitions WHERE ID IN (SELECT Accession_ID FROM #Charges);" & vbCrLf &
                               "SELECT * INTO #Charge_Detail FROM Charge_detail WHERE charge_id IN (SELECT id FROM #Charges);" & vbCrLf &
                               vbCrLf &
                               "SELECT * INTO #Payment_Detail FROM Payment_Detail pd WHERE pd.Charge_ID IN (SELECT id FROM #Charges);" & vbCrLf &
                               "SELECT * INTO #Payments FROM Payments WHERE ID IN (SELECT Payment_ID FROM #Payment_Detail);"
            ElseIf DateType = 1 Then
                dateColName = "ReceivedTime"
                Tables = "SELECT * INTO #AccessionsTemp FROM Requisitions WHERE ReceivedTime BETWEEN @FromDate AND @ToDate;" & vbCrLf &
                             vbCrLf &
                             "SELECT * INTO #Charges FROM Charges c WHERE c.Accession_ID IN (SELECT ID FROM #AccessionsTemp);" & vbCrLf &
                             "SELECT * INTO #Charge_Detail FROM Charge_detail WHERE charge_id IN (SELECT id FROM #Charges);" & vbCrLf &
                             vbCrLf &
                             "SELECT * INTO #Payment_Detail FROM Payment_Detail pd WHERE pd.Charge_ID IN (SELECT id FROM #Charges);" & vbCrLf &
                             "SELECT * INTO #Payments FROM Payments WHERE ID IN (SELECT Payment_ID FROM #Payment_Detail);"

            ElseIf DateType = 2 Then

                dateColName = "Svc_Date"
                Tables = "SELECT * INTO #Charges FROM Charges c WHERE " & dateColName & " BETWEEN @FromDate AND @ToDate;" & vbCrLf &
                               "SELECT * INTO #AccessionsTemp FROM Requisitions WHERE ID IN (SELECT Accession_ID FROM #Charges);" & vbCrLf &
                               "SELECT * INTO #Charge_Detail FROM Charge_detail WHERE charge_id IN (SELECT id FROM #Charges);" & vbCrLf &
                               vbCrLf &
                               "SELECT * INTO #Payment_Detail FROM Payment_Detail pd WHERE pd.Charge_ID IN (SELECT id FROM #Charges);" & vbCrLf &
                               "SELECT * INTO #Payments FROM Payments WHERE ID IN (SELECT Payment_ID FROM #Payment_Detail);"
            End If
        End If
        If searchIndex = 4 Then
            If DateType = 0 Then
                dateColName = "Bill_Date"
                Tables = "SELECT * INTO #Charges FROM Charges c WHERE " & dateColName & " BETWEEN @FromDate AND @ToDate;" & vbCrLf &
                               "SELECT * INTO #AccessionsTemp FROM Requisitions WHERE ID IN (SELECT Accession_ID FROM #Charges);" & vbCrLf &
                               "SELECT * INTO #Charge_Detail FROM Charge_detail WHERE charge_id IN (SELECT id FROM #Charges);" & vbCrLf &
                               vbCrLf &
                               "SELECT * INTO #Payment_Detail FROM Payment_Detail pd WHERE pd.Charge_ID IN (SELECT id FROM #Charges);" & vbCrLf &
                               "SELECT * INTO #Payments FROM Payments WHERE ID IN (SELECT Payment_ID FROM #Payment_Detail);"
            ElseIf DateType = 1 Then
                dateColName = "ReceivedTime"
                Tables = "SELECT * INTO #AccessionsTemp FROM Requisitions WHERE ReceivedTime BETWEEN @FromDate AND @ToDate;" & vbCrLf &
                             vbCrLf &
                             "SELECT * INTO #Charges FROM Charges c WHERE c.Accession_ID IN (SELECT ID FROM #AccessionsTemp);" & vbCrLf &
                             "SELECT * INTO #Charge_Detail FROM Charge_detail WHERE charge_id IN (SELECT id FROM #Charges);" & vbCrLf &
                             vbCrLf &
                             "SELECT * INTO #Payment_Detail FROM Payment_Detail pd WHERE pd.Charge_ID IN (SELECT id FROM #Charges);" & vbCrLf &
                             "SELECT * INTO #Payments FROM Payments WHERE ID IN (SELECT Payment_ID FROM #Payment_Detail);"

            ElseIf DateType = 2 Then

                dateColName = "Svc_Date"
                Tables = "SELECT * INTO #Charges FROM Charges c WHERE " & dateColName & " BETWEEN @FromDate AND @ToDate;" & vbCrLf &
                               "SELECT * INTO #AccessionsTemp FROM Requisitions WHERE ID IN (SELECT Accession_ID FROM #Charges);" & vbCrLf &
                               "SELECT * INTO #Charge_Detail FROM Charge_detail WHERE charge_id IN (SELECT id FROM #Charges);" & vbCrLf &
                               vbCrLf &
                               "SELECT * INTO #Payment_Detail FROM Payment_Detail pd WHERE pd.Charge_ID IN (SELECT id FROM #Charges);" & vbCrLf &
                               "SELECT * INTO #Payments FROM Payments WHERE ID IN (SELECT Payment_ID FROM #Payment_Detail);"
            End If
        End If
        If searchIndex = 6 Then
            If DateType = 0 Then
                dateColName = "Bill_Date"
                Tables = "SELECT distinct * INTO #Charges FROM Charges c WHERE " & dateColName & " BETWEEN @FromDate AND @ToDate;" & vbCrLf &
                               "SELECT * INTO #AccessionsTemp FROM Requisitions WHERE ID IN (SELECT Accession_ID FROM #Charges);" & vbCrLf &
                               "SELECT * INTO #Charge_Detail FROM Charge_detail WHERE charge_id IN (SELECT id FROM #Charges);" & vbCrLf &
                               vbCrLf &
                               "SELECT * INTO #Payment_Detail FROM Payment_Detail pd WHERE pd.Charge_ID IN (SELECT id FROM #Charges);" & vbCrLf &
                               "SELECT * INTO #Payments FROM Payments WHERE ID IN (SELECT Payment_ID FROM #Payment_Detail);"
            ElseIf DateType = 1 Then
                dateColName = "ReceivedTime"
                Tables = "SELECT distinct * INTO #AccessionsTemp FROM Requisitions WHERE ReceivedTime BETWEEN @FromDate AND @ToDate;" & vbCrLf &
                             vbCrLf &
                             "SELECT * INTO #Charges FROM Charges c WHERE c.Accession_ID IN (SELECT ID FROM #AccessionsTemp);" & vbCrLf &
                             "SELECT * INTO #Charge_Detail FROM Charge_detail WHERE charge_id IN (SELECT id FROM #Charges);" & vbCrLf &
                             vbCrLf &
                             "SELECT * INTO #Payment_Detail FROM Payment_Detail pd WHERE pd.Charge_ID IN (SELECT id FROM #Charges);" & vbCrLf &
                             "SELECT * INTO #Payments FROM Payments WHERE ID IN (SELECT Payment_ID FROM #Payment_Detail);"

            ElseIf DateType = 2 Then

                dateColName = "Svc_Date"
                Tables = "SELECT distinct * INTO #Charges FROM Charges c WHERE " & dateColName & " BETWEEN @FromDate AND @ToDate;" & vbCrLf &
                               "SELECT * INTO #AccessionsTemp FROM Requisitions WHERE ID IN (SELECT Accession_ID FROM #Charges);" & vbCrLf &
                               "SELECT * INTO #Charge_Detail FROM Charge_detail WHERE charge_id IN (SELECT id FROM #Charges);" & vbCrLf &
                               vbCrLf &
                               "SELECT * INTO #Payment_Detail FROM Payment_Detail pd WHERE pd.Charge_ID IN (SELECT id FROM #Charges);" & vbCrLf &
                               "SELECT * INTO #Payments FROM Payments WHERE ID IN (SELECT Payment_ID FROM #Payment_Detail);"
            End If
        End If
        '' Search index 2 - - - - - -  END

        '' Search index 0 - - - - - -  Start
        If searchIndex = 0 Then ' search by provider 
            If DateType = 0 Then
                dateColName = "Bill_Date"
                Tables = "SELECT * INTO #Charges FROM Charges c WHERE " & dateColName & " BETWEEN @FromDate AND @ToDate;" & vbCrLf &
                               "SELECT * INTO #AccessionsTemp FROM Requisitions WHERE ID IN (SELECT Accession_ID FROM #Charges);" & vbCrLf &
                               "SELECT * INTO #Charge_Detail FROM Charge_detail WHERE charge_id IN (SELECT id FROM #Charges);" & vbCrLf &
                               vbCrLf &
                               "SELECT * INTO #Payment_Detail FROM Payment_Detail pd WHERE pd.Charge_ID IN (SELECT id FROM #Charges);" & vbCrLf &
                               "SELECT * INTO #Payments FROM Payments WHERE ID IN (SELECT Payment_ID FROM #Payment_Detail);"
            ElseIf DateType = 1 Then
                dateColName = "ReceivedTime"
                Tables = "SELECT * INTO #AccessionsTemp FROM Requisitions WHERE ReceivedTime BETWEEN @FromDate AND @ToDate;" & vbCrLf &
                             vbCrLf &
                             "SELECT * INTO #Charges FROM Charges c WHERE c.Accession_ID IN (SELECT ID FROM #AccessionsTemp);" & vbCrLf &
                             "SELECT * INTO #Charge_Detail FROM Charge_detail WHERE charge_id IN (SELECT id FROM #Charges);" & vbCrLf &
                             vbCrLf &
                             "SELECT * INTO #Payment_Detail FROM Payment_Detail pd WHERE pd.Charge_ID IN (SELECT id FROM #Charges);" & vbCrLf &
                             "SELECT * INTO #Payments FROM Payments WHERE ID IN (SELECT Payment_ID FROM #Payment_Detail);"

            ElseIf DateType = 2 Then

                dateColName = "Svc_Date"
                Tables = "SELECT * INTO #Charges FROM Charges c WHERE " & dateColName & " BETWEEN @FromDate AND @ToDate;" & vbCrLf &
                               "SELECT * INTO #AccessionsTemp FROM Requisitions WHERE ID IN (SELECT Accession_ID FROM #Charges);" & vbCrLf &
                               "SELECT * INTO #Charge_Detail FROM Charge_detail WHERE charge_id IN (SELECT id FROM #Charges);" & vbCrLf &
                               vbCrLf &
                               "SELECT * INTO #Payment_Detail FROM Payment_Detail pd WHERE pd.Charge_ID IN (SELECT id FROM #Charges);" & vbCrLf &
                               "SELECT * INTO #Payments FROM Payments WHERE ID IN (SELECT Payment_ID FROM #Payment_Detail);"
            End If
        End If
        If searchIndex = 7 Then ' search by provider 

            dateColName = "Svc_Date"
            Tables = "SELECT * INTO #Charges FROM Charges c WHERE ID in (Select distinct Charge_ID from Payment_Detail where Payment_ID in " &
                    "(Select ID from Payments where DocNo = '" & Trim(searchText) & "')) ;" & vbCrLf &
                           "SELECT * INTO #AccessionsTemp FROM Requisitions WHERE ID IN (SELECT Accession_ID FROM #Charges);" & vbCrLf &
                           "SELECT * INTO #Charge_Detail FROM Charge_detail WHERE charge_id IN (SELECT id FROM #Charges);" & vbCrLf &
                           vbCrLf &
                           "SELECT * INTO #Payment_Detail FROM Payment_Detail pd WHERE pd.Charge_ID IN (SELECT id FROM #Charges);" & vbCrLf &
                           "SELECT * INTO #Payments FROM Payments WHERE ID IN (SELECT Payment_ID FROM #Payment_Detail);"

        End If
        If searchIndex = 90 Then ' search by cpt 

            Dim condi = " and a.ID in (Select distinct Charge_ID from Charge_Detail where " &
                        "CPT_Code = '" & Trim(searchText) & "')"

            dateColName = "Svc_Date"
            Tables = "SELECT * INTO #Charges FROM Charges c WHERE ID in (Select distinct Charge_ID from Payment_Detail where Payment_ID in " &
                    "(Select ID from Payments where DocNo = '" & Trim(searchText) & "')) ;" & vbCrLf &
                           "SELECT * INTO #AccessionsTemp FROM Requisitions WHERE ID IN (SELECT Accession_ID FROM #Charges);" & vbCrLf &
                           "SELECT * INTO #Charge_Detail FROM Charge_detail WHERE charge_id IN (SELECT id FROM #Charges);" & vbCrLf &
                           vbCrLf &
                           "SELECT * INTO #Payment_Detail FROM Payment_Detail pd WHERE pd.Charge_ID IN (SELECT id FROM #Charges);" & vbCrLf &
                           "SELECT * INTO #Payments FROM Payments WHERE ID IN (SELECT Payment_ID FROM #Payment_Detail);"

        End If
        '' search with cpt-code
        If searchIndex = 9 Then
            If DateType = 0 Then
                dateColName = "Bill_Date"
                Tables = "SELECT distinct * INTO #Charges FROM Charges c WHERE c.ID in (Select distinct Charge_ID from Charge_Detail where " &
                        "CPT_Code = '" & Trim(searchText) & "') and " & dateColName & " BETWEEN @FromDate AND @ToDate ;" & vbCrLf &
                               "SELECT * INTO #AccessionsTemp FROM Requisitions WHERE ID IN (SELECT Accession_ID FROM #Charges);" & vbCrLf &
                               "SELECT * INTO #Charge_Detail FROM Charge_detail WHERE charge_id IN (SELECT id FROM #Charges);" & vbCrLf &
                               vbCrLf &
                               "SELECT * INTO #Payment_Detail FROM Payment_Detail pd WHERE pd.Charge_ID IN (SELECT id FROM #Charges);" & vbCrLf &
                               "SELECT * INTO #Payments FROM Payments WHERE ID IN (SELECT Payment_ID FROM #Payment_Detail);"
            ElseIf DateType = 1 Then
                dateColName = "ReceivedTime"
                Tables = "SELECT distinct * INTO #AccessionsTemp FROM Requisitions WHERE  ReceivedTime BETWEEN @FromDate AND @ToDate;" & vbCrLf &
                             vbCrLf &
                             "SELECT * INTO #Charges FROM Charges c WHERE c.Accession_ID IN (SELECT ID FROM #AccessionsTemp) and c.ID in (Select distinct Charge_ID from Charge_Detail where " &
                        "CPT_Code = '" & Trim(searchText) & "') ;" & vbCrLf &
                             "SELECT * INTO #Charge_Detail FROM Charge_detail WHERE charge_id IN (SELECT id FROM #Charges);" & vbCrLf &
                             vbCrLf &
                             "SELECT * INTO #Payment_Detail FROM Payment_Detail pd WHERE pd.Charge_ID IN (SELECT id FROM #Charges);" & vbCrLf &
                             "SELECT * INTO #Payments FROM Payments WHERE ID IN (SELECT Payment_ID FROM #Payment_Detail);"

            ElseIf DateType = 2 Then

                dateColName = "Svc_Date"
                Tables = "SELECT distinct * INTO #Charges FROM Charges c WHERE ID in (Select distinct Charge_ID from Charge_Detail where " &
                        "CPT_Code = '" & Trim(searchText) & "') and " & dateColName & " BETWEEN @FromDate AND @ToDate;" & vbCrLf &
                               "SELECT * INTO #AccessionsTemp FROM Requisitions WHERE ID IN (SELECT Accession_ID FROM #Charges);" & vbCrLf &
                               "SELECT * INTO #Charge_Detail FROM Charge_detail WHERE charge_id IN (SELECT id FROM #Charges);" & vbCrLf &
                               vbCrLf &
                               "SELECT * INTO #Payment_Detail FROM Payment_Detail pd WHERE pd.Charge_ID IN (SELECT id FROM #Charges);" & vbCrLf &
                               "SELECT * INTO #Payments FROM Payments WHERE ID IN (SELECT Payment_ID FROM #Payment_Detail);"
            End If
        End If
        '__________________________________________

        Dim sqlQuery As String = "DROP TABLE IF EXISTS #Charges;" & vbCrLf &
                           "DROP TABLE IF EXISTS #Charge_Detail;" & vbCrLf &
                           "DROP TABLE IF EXISTS #Payment_Detail;" & vbCrLf &
                           "DROP TABLE IF EXISTS #Payments;" & vbCrLf &
                           "DROP TABLE IF EXISTS #Requisitions;" & vbCrLf &
                           "DROP TABLE IF EXISTS #AccessionsTemp;" & vbCrLf &
                           vbCrLf &
                           "-- Service Date given " & vbCrLf &
                            Tables & vbCrLf &
                           " SELECT  a.ArType," & vbCrLf &
                           "    b.PrimePayer_ID," & vbCrLf &
                           "    a.Accession_ID AS Accession," & vbCrLf &
                           "    a.ID AS Invoice," & vbCrLf &
                           "    ISNULL(CONVERT(NVARCHAR, a.Svc_Date, 101), '') AS [Svc Date]," & vbCrLf &
                           "    (CASE" & vbCrLf &
                           "        WHEN a.ArType = 0 THEN d.LastName_BSN + ', ' + d.FirstName" & vbCrLf &
                           "        WHEN a.ArType = 1 THEN e.PayerName" & vbCrLf &
                           "        ELSE c.LastName + ', ' + c.FirstName" & vbCrLf &
                           "    END) AS [Bill To]," & vbCrLf &
                           "    (c.LastName + ', ' + c.FirstName) AS Patient," & vbCrLf &
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
                           "        INNER JOIN (#AccessionsTemp b" & vbCrLf &
                           "            JOIN Payers e ON e.ID = b.PrimePayer_ID)" & vbCrLf &
                           "        ON b.OrderingProvider_ID = d.ID)" & vbCrLf &
                           "    ON c.ID = b.Patient_ID)" & vbCrLf &
                           "ON b.ID = a.Accession_ID" & IIf(billingType = 3, "", " WHERE ArType = " & billingType & " ")

        If searchIndex = 2 Then
            sqlQuery += " AND b.PrimePayer_ID = @PrimePayer_ID"
        ElseIf searchIndex = 0 Then ' search by provider 
            sqlQuery += " AND b.OrderingProvider_ID = @PrimePayer_ID"
        ElseIf searchIndex = 6 Then ' search by accession 
            sqlQuery += " AND b.ID = @ID"
        ElseIf searchIndex = 4 Then
            sqlQuery += " AND b.Patient_ID = @PrimePayer_ID"

        End If


        Return sqlQuery
    End Function
     
End Class
