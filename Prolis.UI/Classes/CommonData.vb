
Imports Microsoft.Data.SqlClient

Public Class CommonData
    'Public Shared Property connString As String

    'Public Shared Function GetAccPatIDFromInvoice(ByVal ChargeID As Long, connstring As String) As String()
    '    Dim AccPatID() As String = {"", "", ""} '0=AccID, 1=PatID, 2=SvcDate
    '    Dim CNP As New ADODB.Connection
    '    CNP.Open(connstring)
    '    Dim Rs As New ADODB.Recordset
    '    Rs.Open("Select a.*, b.Patient_ID from Charges a inner join " &
    '    "Requisitions b on a.Accession_ID = b.ID where a.ID = " & ChargeID,
    '    CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '    If Not Rs.BOF Then
    '        AccPatID(0) = Rs.Fields("Accession_ID").Value.ToString
    '        AccPatID(1) = Rs.Fields("Patient_ID").Value.ToString
    '        AccPatID(2) = Rs.Fields("Svc_Date").Value
    '    End If
    '    Rs.Close()
    '    Rs = Nothing
    '    CNP.Close()
    '    CNP = Nothing
    '    Return AccPatID
    'End Function

    Public Shared Function GetAccPatIDFromInvoice(ByVal ChargeID As Long) As String()
        Dim AccPatID() As String = {"", "", ""} ' 0=AccID, 1=PatID, 2=SvcDate

        Try
            Using conn As New SqlConnection(connString)
                conn.Open()

                Dim query As String = "SELECT a.Accession_ID, b.Patient_ID, a.Svc_Date " &
                                  "FROM Charges a INNER JOIN Requisitions b " &
                                  "ON a.Accession_ID = b.ID WHERE a.ID = @ChargeID"

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.Add(New SqlParameter("@ChargeID", ChargeID))

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            AccPatID(0) = reader("Accession_ID").ToString()
                            AccPatID(1) = reader("Patient_ID").ToString()
                            AccPatID(2) = reader("Svc_Date").ToString()
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' Handle or log the exception appropriately
            Console.WriteLine("Error: " & ex.Message)
        End Try

        Return AccPatID
    End Function

    Public Shared Function DoesColumnExist(ByVal sc As String, ByVal tableName As String, ByVal columnName As String) As Boolean
        Dim columnExists As Boolean = False
        Dim connection As New SqlConnection(sc)
        Try


            connection.Open()

            ' Check if the column exists in the table
            Using command As New SqlCommand("SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" & tableName & "' AND COLUMN_NAME = '" & columnName & "'", connection)
                columnExists = CInt(command.ExecuteScalar()) > 0
            End Using
        Catch ex As Exception
            ' Handle any exceptions here
            ' You can log the exception or take appropriate actions
            ' For simplicity, this example doesn't include error handling.
        Finally
            connection.Close()
        End Try

        Return columnExists
    End Function
    Public Shared Function GetPOS(AccID As String) As String
        Dim sSQL = "select  POS_Code from providers where ID in ( select OrderingProvider_ID from Requisitions r where ID =" & AccID & ")"
        Dim cnpc1 As New SqlConnection(connString)
        Dim pos = "81"
        cnpc1.Open()
        Dim cmdpc1 As New SqlCommand(sSQL, cnpc1)
        cmdpc1.CommandType = CommandType.Text
        Dim drpc1 As SqlDataReader = cmdpc1.ExecuteReader
        If drpc1.HasRows Then
            While drpc1.Read
                If drpc1("POS_Code") IsNot DBNull.Value Then pos = Trim(drpc1("POS_Code"))
            End While
        End If
        cnpc1.Close()
        cnpc1 = Nothing
        Return pos
    End Function
    Public Shared Function ExecuteQuery(query As String) As List(Of Dictionary(Of String, Object))


        'Dim values = CommonData.ExecuteQuery(sSQL, connString)

        'For Each row In values
        '    For Each kvp In row
        '                    Console.WriteLine($"Column: {kvp.Key}, Value: {kvp.Value}")
        '    Next
        '    Console.WriteLine("------------")
        'Next

        Dim result As New List(Of Dictionary(Of String, Object))
        If query = "" Then
            Return result
        End If
        Try
            Using connection As New SqlConnection(connString)
                connection.Open()
                Using command As New SqlCommand(query, connection)
                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            Dim row As New Dictionary(Of String, Object)
                            For i As Integer = 0 To reader.FieldCount - 1
                                Dim columnName As String = reader.GetName(i)
                                Dim columnValue As Object = reader.GetValue(i)
                                row.Add(columnName, columnValue)
                            Next
                            result.Add(row)
                        End While
                    End Using
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("An exception occured while processing a query: " & ex.Message & " Query: " & query)
        End Try

        Return result
    End Function

    Public Shared Function RetrieveColumnValue(ByVal tableName As String, ByVal Getcolumn As String, ByVal wherecolumn As String, ByVal columnNameValue As String, Optional AllcolumnName As String = "ALL") As Object
        Dim result As Object = Nothing

        Using connection As SqlConnection = New SqlConnection(connString)
            connection.Open()

            Dim query As String

            If AllcolumnName = "ALL" Then
                ' If no column is provided, return all columns
                query = "SELECT * FROM " & tableName & " where " & wherecolumn & " = " & columnNameValue
            Else
                ' If a column is provided, return that column
                query = "SELECT " & Getcolumn & "  FROM " & tableName & " where " & wherecolumn & " = " & columnNameValue
            End If

            Using command As SqlCommand = New SqlCommand(query, connection)
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()

                        result = If(String.IsNullOrEmpty(Getcolumn), reader(0), reader(Getcolumn))



                    End While

                End Using
            End Using
        End Using

        Return result
    End Function


    Public Shared Function RetrieveColumnValueList(ByVal tableName As String, ByVal Getcolumn As String, ByVal wherecolumn As String, ByVal columnNameValue As String, Optional AllcolumnName As String = "ALL") As Object
        Dim result As Object = Nothing
        Dim results As List(Of Object) = New List(Of Object)
        Dim query = ""
        If AllcolumnName = "ALL" Then
            ' If no column is provided, return all columns
            query = "SELECT * FROM " & tableName & " where " & wherecolumn & " = " & columnNameValue
            Return ExecuteQuery(query)
        Else
            ' If a column is provided, return that column
            query = "SELECT " & Getcolumn & "  FROM " & tableName & " where " & wherecolumn & " = " & columnNameValue
        End If

        Using connection As SqlConnection = New SqlConnection(connString)
            connection.Open()



            Using command As SqlCommand = New SqlCommand(query, connection)
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()

                        result = If(String.IsNullOrEmpty(Getcolumn), reader(0), reader(Getcolumn))
                        results.Add(result)


                    End While

                End Using
            End Using
        End Using

        Return results
    End Function
    Shared Sub SaveReqSCoverage(connString As String, p2 As Double, p3 As Double)
        Throw New NotImplementedException
    End Sub


    Public Shared Function IsExistsInTable(ByVal tableName As String, ByVal column As String, ByVal value As String) As Boolean
        Dim result = False

        Using connection As SqlConnection = New SqlConnection(connString)
            connection.Open()

            Dim query As String


            ' If a column is provided, return that column
            query = "SELECT *  FROM " & tableName & " where " & column & " = " & value


            Using command As SqlCommand = New SqlCommand(query, connection)
                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.HasRows() Then
                        result = True
                    End If
                End Using
            End Using
        End Using

        Return result
    End Function


End Class
