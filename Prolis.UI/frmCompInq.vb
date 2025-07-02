Imports System.Windows.Forms
Imports Microsoft.Data.SqlClient

Public Class frmCompInq

    Private Sub frmCompInq_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbPosition.SelectedIndex = 0
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub DisplayTGP(ByVal TGPID As Integer)
        ClearForm()
        Dim TGPType As String = GetTGPType(TGPID)
        If TGPType = "T" Then
            dgvConstituents.Visible = False : dgvRanges.Visible = True
            DisplayAnalyte(TGPID)
            DisplayExtMarking(TGPID)
            Label26.Text = "Ranges"
        ElseIf TGPType = "G" Then
            dgvConstituents.Visible = True : dgvRanges.Visible = False
            DisplayGroup(TGPID)
            DisplayExtMarking(TGPID)
            Label26.Text = "Constituents"
        Else
            dgvConstituents.Visible = True : dgvRanges.Visible = False
            DisplayProfile(TGPID)
            DisplayExtMarking(TGPID)
            Label26.Text = "Constituents"
        End If
    End Sub

    Private Sub DisplayExtMarking(ByVal TGPID As Integer)
        dgvMarking.Rows.Clear()

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT Sex, AgeFrom, AgeTo FROM MarkingModifiers WHERE Test_ID = @TestID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@TestID", TGPID)

                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.HasRows Then
                        While reader.Read()
                            dgvMarking.Rows.Add(
                            reader("Sex").ToString().Trim(),
                            reader("AgeFrom").ToString(),
                            reader("AgeTo").ToString()
                        )
                        End While
                    End If
                End Using
            End Using
        End Using
    End Sub
    Private Sub ClearForm()
        cmbPosition.SelectedIndex = 0
        'dgvTGs.Rows.Clear()
        txtResultNote.Text = ""
        txtDescription.Text = ""
        dgvProperties.Rows.Clear()
        dgvMarking.Rows.Clear()
        dgvConstituents.Rows.Clear()
        dgvRanges.Rows.Clear()
    End Sub

    Private Sub DisplayProfile(ByVal ProfileID As Integer)
        dgvProperties.Rows.Clear()
        Dim CompType As String = "P"

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT * FROM Profiles WHERE ID = @ProfileID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@ProfileID", ProfileID)

                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.HasRows Then
                        reader.Read()

                        dgvProperties.Rows.Add("ID", reader("ID").ToString())
                        dgvProperties.Rows.Add("Name, Full", reader("Name").ToString().Trim())
                        dgvProperties.Rows(dgvProperties.RowCount - 1).Height = 20
                        dgvProperties.Rows.Add("Name, Abbreviated", reader("Abbr").ToString().Trim())

                        CompType = If(reader("ComponentType").ToString() = "T", "Analyte",
                              If(reader("ComponentType").ToString() = "G", "Group", "Profile"))

                        dgvProperties.Rows.Add("Type", CompType)
                        dgvProperties.Rows.Add("Specimen Used", GetSource(ProfileID))
                        dgvProperties.Rows.Add("Active", If(reader("IsActive").ToString() = "0", "No", "Yes"))
                        dgvProperties.Rows.Add("Orderable", If(reader("IsMarkable").ToString() = "0", "No", "Yes"))
                        dgvProperties.Rows.Add("In House", If(reader("InHouse").ToString() = "0", "No", "Yes"))
                        dgvProperties.Rows.Add("Billable, Client", If(reader("CBillable").ToString() = "0", "No", "Yes"))
                        dgvProperties.Rows.Add("Billable, Patient", If(reader("PBillable").ToString() = "0", "No", "Yes"))

                        If reader("TBillable").ToString() = "0" Then
                            dgvProperties.Rows.Add("Billable, Insurance", "No")
                        Else
                            dgvProperties.Rows.Add("Billable, Insurance", "Yes")
                            dgvProperties.Rows.Add("CPT Code", If(reader("CPT_Code") IsNot DBNull.Value,
                                                        reader("CPT_Code").ToString().Trim(), ""))
                        End If

                        dgvProperties.Rows.Add("Price, List", If(reader("ListPrice") IsNot DBNull.Value,
                                                          Format(reader("ListPrice"), "##,##0.00"), ""))
                        For i As Integer = 1 To 9
                            dgvProperties.Rows.Add($"Price Level {i}", If(reader($"Price{i}") IsNot DBNull.Value,
                                                                Format(reader($"Price{i}"), "##,##0.00"), ""))
                        Next

                        txtDescription.Text = reader("Description").ToString()
                        DisplayConstituents(ProfileID)
                    End If
                End Using
            End Using
        End Using
    End Sub
    Private Sub DisplayGroup(ByVal GroupID As Integer)
        dgvProperties.Rows.Clear()
        Dim CompType As String = "G"

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT * FROM Groups WHERE ID = @GroupID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@GroupID", GroupID)

                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.HasRows Then
                        reader.Read()

                        dgvProperties.Rows.Add("ID", reader("ID").ToString())
                        dgvProperties.Rows.Add("Name, Full", reader("Name").ToString().Trim())
                        dgvProperties.Rows.Add("Name, Abbreviated", reader("Abbr").ToString().Trim())

                        CompType = If(reader("ComponentType").ToString() = "T", "Analyte",
                              If(reader("ComponentType").ToString() = "G", "Group", "Profile"))
                        dgvProperties.Rows.Add("Type", CompType)

                        dgvProperties.Rows.Add("Specimen Used", GetSource(GroupID))
                        dgvProperties.Rows.Add("Active", If(reader("IsActive").ToString() = "0", "No", "Yes"))
                        dgvProperties.Rows.Add("Orderable", If(reader("IsMarkable").ToString() = "0", "No", "Yes"))
                        dgvProperties.Rows.Add("In House", If(reader("InHouse").ToString() = "0", "No", "Yes"))
                        dgvProperties.Rows.Add("Billable, Client", If(reader("CBillable").ToString() = "0", "No", "Yes"))
                        dgvProperties.Rows.Add("Billable, Patient", If(reader("PBillable").ToString() = "0", "No", "Yes"))

                        If reader("TBillable").ToString() = "0" Then
                            dgvProperties.Rows.Add("Billable, Insurance", "No")
                        Else
                            dgvProperties.Rows.Add("Billable, Insurance", "Yes")
                            dgvProperties.Rows.Add("CPT Code", If(reader("CPT_Code") IsNot DBNull.Value,
                                                        reader("CPT_Code").ToString().Trim(), ""))
                        End If

                        dgvProperties.Rows.Add("Price, List", If(reader("ListPrice") IsNot DBNull.Value,
                                                          Format(reader("ListPrice"), "##,##0.00"), ""))
                        For i As Integer = 1 To 9
                            dgvProperties.Rows.Add($"Price Level {i}", If(reader($"Price{i}") IsNot DBNull.Value,
                                                                Format(reader($"Price{i}"), "##,##0.00"), ""))
                        Next

                        txtDescription.Text = reader("Description").ToString()
                        DisplayConstituents(GroupID)
                    End If
                End Using
            End Using
        End Using
    End Sub

    Private Sub DisplayAnalyte(ByVal TestID As Integer)
        dgvProperties.Rows.Clear()
        Dim CompType As String = "T"

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT * FROM Tests WHERE ID = @TestID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@TestID", TestID)

                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.HasRows Then
                        reader.Read()

                        dgvProperties.Rows.Add("ID", reader("ID").ToString())
                        dgvProperties.Rows.Add("Name, Full", reader("Name").ToString().Trim())
                        dgvProperties.Rows.Add("Name, Abbreviated", reader("Abbr").ToString().Trim())

                        CompType = If(reader("ComponentType").ToString() = "T", "Analyte",
                              If(reader("ComponentType").ToString() = "G", "Group", "Profile"))
                        dgvProperties.Rows.Add("Type", CompType)

                        If reader("IsCalculated").ToString() = "0" Then
                            dgvProperties.Rows.Add("Specimen Used", GetSource(TestID))
                        Else
                            dgvProperties.Rows.Add("Calculated", "Yes")
                            dgvProperties.Rows.Add("Formula", If(reader("Formula") IsNot DBNull.Value,
                                                            reader("Formula").ToString(), ""))
                        End If

                        dgvProperties.Rows.Add("Active", If(reader("IsActive").ToString() = "0", "No", "Yes"))
                        dgvProperties.Rows.Add("Orderable", If(reader("IsMarkable").ToString() = "0", "No", "Yes"))
                        dgvProperties.Rows.Add("In House", If(reader("InHouse").ToString() = "0", "No", "Yes"))
                        dgvProperties.Rows.Add("Billable, Client", If(reader("CBillable").ToString() = "0", "No", "Yes"))
                        dgvProperties.Rows.Add("Billable, Patient", If(reader("PBillable").ToString() = "0", "No", "Yes"))

                        If reader("TBillable").ToString() = "0" Then
                            dgvProperties.Rows.Add("Billable, Insurance", "No")
                        Else
                            dgvProperties.Rows.Add("Billable, Insurance", "Yes")
                            dgvProperties.Rows.Add("CPT Code", If(reader("CPT_Code") IsNot DBNull.Value,
                                                            reader("CPT_Code").ToString().Trim(), ""))
                        End If

                        dgvProperties.Rows.Add("Resultable", If(reader("HasResult").ToString() = "0", "No", "Yes"))
                        dgvProperties.Rows.Add("Reportable", If(reader("IsReportable").ToString() = "0", "No", "Yes"))
                        dgvProperties.Rows.Add("Result Type", If(reader("Qualitative").ToString() = "0", "Quantitative", "Qualitative"))
                        dgvProperties.Rows.Add("Result UOM", If(reader("UOM") IsNot DBNull.Value,
                                                            reader("UOM").ToString().Trim(), ""))

                        For i As Integer = 1 To 9
                            dgvProperties.Rows.Add($"Price Level {i}", If(reader($"Price{i}") IsNot DBNull.Value,
                                                                Format(reader($"Price{i}"), "##,##0.00"), ""))
                        Next

                        txtDescription.Text = reader("Description").ToString()
                        txtResultNote.Text = reader("ResultNote").ToString()
                        DisplayRanges(TestID)
                    End If
                End Using
            End Using
        End Using
    End Sub
    Private Sub DisplayRanges(ByVal TestID As Integer)
        dgvRanges.Rows.Clear()
        Dim dStyle As DataGridViewCellStyle = dgvRanges.DefaultCellStyle
        Dim HStyle As New DataGridViewCellStyle With {
        .ForeColor = Color.White,
        .BackColor = Color.Brown,
        .Font = New Font("Verdana", 8, FontStyle.Bold)
    }

        Using connection As New SqlConnection(connString)
            connection.Open()

            ' Choice Ranges
            Dim queryChoice As String = "SELECT Choice, Flag FROM C_Ranges WHERE Test_ID = @TestID"
            Using commandChoice As New SqlCommand(queryChoice, connection)
                commandChoice.Parameters.AddWithValue("@TestID", TestID)

                Using readerChoice As SqlDataReader = commandChoice.ExecuteReader()
                    If readerChoice.HasRows Then
                        dgvRanges.Rows.Add("Choice Ranges", "", "", "", "")
                        dgvRanges.Rows(dgvRanges.RowCount - 1).Cells(0).Style = HStyle

                        While readerChoice.Read()
                            dgvRanges.Rows.Add("", readerChoice("Choice").ToString().Trim(),
                                           readerChoice("Flag").ToString().Substring(0, 2), "", "")
                            dgvRanges.Rows(dgvRanges.RowCount - 1).Cells(0).Style = dStyle
                        End While

                        dgvRanges.Rows.Add("", "", "", "", "")
                        dgvRanges.Rows(dgvRanges.RowCount - 1).Cells(0).Style = dStyle
                    End If
                End Using
            End Using

            ' Numeric Ranges
            Dim queryNumeric As String = "SELECT ValueFrom, ValueTo, Flag FROM N_Ranges WHERE Test_ID = @TestID"
            Using commandNumeric As New SqlCommand(queryNumeric, connection)
                commandNumeric.Parameters.AddWithValue("@TestID", TestID)

                Using readerNumeric As SqlDataReader = commandNumeric.ExecuteReader()
                    If readerNumeric.HasRows Then
                        dgvRanges.Rows.Add("Numeric Ranges", "", "", "", "")
                        dgvRanges.Rows(dgvRanges.RowCount - 1).Cells(0).Style = HStyle

                        While readerNumeric.Read()
                            dgvRanges.Rows.Add("", $"{readerNumeric("ValueFrom")} - {readerNumeric("ValueTo")}",
                                           readerNumeric("Flag").ToString().Substring(0, 2), "", "")
                            dgvRanges.Rows(dgvRanges.RowCount - 1).Cells(0).Style = dStyle
                        End While

                        dgvRanges.Rows.Add("", "", "", "", "")
                        dgvRanges.Rows(dgvRanges.RowCount - 1).Cells(0).Style = dStyle
                    End If
                End Using
            End Using

            ' Age Gender Ranges
            Dim queryAgeGender As String = "SELECT ValueFrom, ValueTo, Flag, Sex, AgeFrom, AgeTo FROM AG_Ranges WHERE Test_ID = @TestID"
            Using commandAgeGender As New SqlCommand(queryAgeGender, connection)
                commandAgeGender.Parameters.AddWithValue("@TestID", TestID)

                Using readerAgeGender As SqlDataReader = commandAgeGender.ExecuteReader()
                    If readerAgeGender.HasRows Then
                        dgvRanges.Rows.Add("Age Gender Ranges", "", "", "", "")
                        dgvRanges.Rows(dgvRanges.RowCount - 1).Cells(0).Style = HStyle

                        While readerAgeGender.Read()
                            dgvRanges.Rows.Add("", $"{readerAgeGender("ValueFrom")} - {readerAgeGender("ValueTo")}",
                                           readerAgeGender("Flag").ToString().Substring(0, 2),
                                           readerAgeGender("Sex").ToString().Trim(),
                                           $"{readerAgeGender("AgeFrom")} - {readerAgeGender("AgeTo")}")
                            dgvRanges.Rows(dgvRanges.RowCount - 1).Cells(0).Style = dStyle
                        End While

                        dgvRanges.Rows.Add("", "", "", "", "")
                        dgvRanges.Rows(dgvRanges.RowCount - 1).Cells(0).Style = dStyle
                    End If
                End Using
            End Using
        End Using
    End Sub
    Private Function GetMaterial(ByVal TGPID As Integer) As String
        Dim Material As String = ""
        Dim TGPType As String = GetTGPType(TGPID)
        Dim TIN As String = ""

        Using connection As New SqlConnection(connString)
            connection.Open()

            If TGPType = "T" Then
                Dim queryTest As String = "SELECT Name FROM Materials WHERE ID IN (SELECT Material_ID FROM Tests WHERE ID = @TGPID)"
                Using commandTest As New SqlCommand(queryTest, connection)
                    commandTest.Parameters.AddWithValue("@TGPID", TGPID)
                    Using reader As SqlDataReader = commandTest.ExecuteReader()
                        While reader.Read()
                            Dim materialName As String = reader("Name").ToString().Trim()
                            If Not Material.Contains(materialName) Then
                                Material &= materialName & ", "
                            End If
                        End While
                    End Using
                End Using
            ElseIf TGPType = "G" Then
                Dim queryGroup As String = "SELECT Test_ID FROM Group_Test WHERE Group_ID = @TGPID"
                Using commandGroup As New SqlCommand(queryGroup, connection)
                    commandGroup.Parameters.AddWithValue("@TGPID", TGPID)
                    Using readerGroup As SqlDataReader = commandGroup.ExecuteReader()
                        While readerGroup.Read()
                            Dim testID As Integer = readerGroup("Test_ID")
                            Dim queryMaterial As String = "SELECT Name FROM Materials WHERE ID IN (SELECT Material_ID FROM Tests WHERE ID = @TestID)"
                            Using commandMaterial As New SqlCommand(queryMaterial, connection)
                                commandMaterial.Parameters.AddWithValue("@TestID", testID)
                                Using readerMaterial As SqlDataReader = commandMaterial.ExecuteReader()
                                    While readerMaterial.Read()
                                        Dim materialName As String = readerMaterial("Name").ToString().Trim()
                                        If Not Material.Contains(materialName) Then
                                            Material &= materialName & ", "
                                        End If
                                    End While
                                End Using
                            End Using
                        End While
                    End Using
                End Using
            Else
                Dim queryProfile As String = "SELECT GrpTst_ID FROM Prof_GrpTst WHERE Profile_ID = @TGPID"
                Using commandProfile As New SqlCommand(queryProfile, connection)
                    commandProfile.Parameters.AddWithValue("@TGPID", TGPID)
                    Using readerProfile As SqlDataReader = commandProfile.ExecuteReader()
                        While readerProfile.Read()
                            Dim grpTstID As Integer = readerProfile("GrpTst_ID")
                            TIN = GetTGPType(grpTstID)

                            If TIN = "T" Then
                                Dim queryMaterial As String = "SELECT Name FROM Materials WHERE ID IN (SELECT Material_ID FROM Tests WHERE ID = @TestID)"
                                Using commandMaterial As New SqlCommand(queryMaterial, connection)
                                    commandMaterial.Parameters.AddWithValue("@TestID", grpTstID)
                                    Using readerMaterial As SqlDataReader = commandMaterial.ExecuteReader()
                                        While readerMaterial.Read()
                                            Dim materialName As String = readerMaterial("Name").ToString().Trim()
                                            If Not Material.Contains(materialName) Then
                                                Material &= materialName & ", "
                                            End If
                                        End While
                                    End Using
                                End Using
                            Else
                                Dim queryGroup As String = "SELECT Test_ID FROM Group_Test WHERE Group_ID = @GrpTstID"
                                Using commandGroup As New SqlCommand(queryGroup, connection)
                                    commandGroup.Parameters.AddWithValue("@GrpTstID", grpTstID)
                                    Using readerGroup As SqlDataReader = commandGroup.ExecuteReader()
                                        While readerGroup.Read()
                                            Dim testID As Integer = readerGroup("Test_ID")
                                            Dim queryMaterial As String = "SELECT Name FROM Materials WHERE ID IN (SELECT Material_ID FROM Tests WHERE ID = @TestID)"
                                            Using commandMaterial As New SqlCommand(queryMaterial, connection)
                                                commandMaterial.Parameters.AddWithValue("@TestID", testID)
                                                Using readerMaterial As SqlDataReader = commandMaterial.ExecuteReader()
                                                    While readerMaterial.Read()
                                                        Dim materialName As String = readerMaterial("Name").ToString().Trim()
                                                        If Not Material.Contains(materialName) Then
                                                            Material &= materialName & ", "
                                                        End If
                                                    End While
                                                End Using
                                            End Using
                                        End While
                                    End Using
                                End Using
                            End If
                        End While
                    End Using
                End Using
            End If
        End Using

        If Material.Length >= 2 Then Material = Material.Substring(0, Material.Length - 2)
        Return Material
    End Function
    Private Sub DisplayConstituents(ByVal GPID As Integer)
        dgvConstituents.Rows.Clear()
        Dim GPType As String = GetTGPType(GPID)
        Dim TIN As String = ""
        Dim Comp As Image = System.Drawing.Image.FromFile(IO.Path.Combine(Application.StartupPath, "Images\Test.Ico"))

        Using connection As New SqlConnection(connString)
            connection.Open()

            If GPType = "G" Then
                Dim queryGroup As String = "SELECT ID, Name FROM Tests WHERE ID IN (SELECT Test_ID FROM Group_Test WHERE Group_ID = @GPID)"
                Using commandGroup As New SqlCommand(queryGroup, connection)
                    commandGroup.Parameters.AddWithValue("@GPID", GPID)
                    Using readerGroup As SqlDataReader = commandGroup.ExecuteReader()
                        If readerGroup.HasRows Then
                            Comp = System.Drawing.Image.FromFile(IO.Path.Combine(Application.StartupPath, "Images\Test.Ico"))
                            While readerGroup.Read()
                                dgvConstituents.Rows.Add(readerGroup("ID"), Comp, readerGroup("Name").ToString())
                            End While
                        End If
                    End Using
                End Using
            Else
                Dim queryProfile As String = "SELECT GrpTst_ID FROM Prof_GrpTst WHERE Profile_ID = @GPID"
                Using commandProfile As New SqlCommand(queryProfile, connection)
                    commandProfile.Parameters.AddWithValue("@GPID", GPID)
                    Using readerProfile As SqlDataReader = commandProfile.ExecuteReader()
                        If readerProfile.HasRows Then
                            While readerProfile.Read()
                                TIN = GetTGPType(readerProfile("GrpTst_ID"))

                                Comp = If(TIN = "T",
                                      System.Drawing.Image.FromFile(IO.Path.Combine(Application.StartupPath, "Images\Test.Ico")),
                                      System.Drawing.Image.FromFile(IO.Path.Combine(Application.StartupPath, "Images\Group.Ico")))

                                dgvConstituents.Rows.Add(readerProfile("GrpTst_ID"), Comp, GetTGPName(readerProfile("GrpTst_ID")))
                            End While
                        End If
                    End Using
                End Using
            End If
        End Using
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLook.Click
        Dim sSQL As String = ""

        If Not String.IsNullOrEmpty(txtTerm.Text) Then
            If IsNumeric(txtTerm.Text) Then
                sSQL = "SELECT ID, Name, Abbr, Description, ComponentType FROM Tests WHERE IsMarkable <> 0 AND ID = @Term " &
                   "UNION SELECT ID, Name, Abbr, Description, ComponentType FROM Groups WHERE IsMarkable <> 0 AND ID = @Term " &
                   "UNION SELECT ID, Name, Abbr, Description, ComponentType FROM Profiles WHERE IsMarkable <> 0 AND ID = @Term " &
                   "ORDER BY Name"
            Else
                Dim searchCondition As String = If(cmbPosition.SelectedIndex = 0,
                "LIKE '%' + @Term + '%'", "LIKE @Term + '%'")

                sSQL = "SELECT ID, Name, Abbr, Description, ComponentType FROM Tests WHERE IsMarkable <> 0 AND (Name " & searchCondition &
                   " OR Abbr " & searchCondition & " OR Description " & searchCondition & ") " &
                   "UNION SELECT ID, Name, Abbr, Description, ComponentType FROM Groups WHERE IsMarkable <> 0 AND (Name " & searchCondition &
                   " OR Abbr " & searchCondition & " OR Description " & searchCondition & ") " &
                   "UNION SELECT ID, Name, Abbr, Description, ComponentType FROM Profiles WHERE IsMarkable <> 0 AND (Name " & searchCondition &
                   " OR Abbr " & searchCondition & " OR Description " & searchCondition & ") ORDER BY Name"
            End If
            txtTerm.Text = ""
        Else
            sSQL = "SELECT ID, Name, Abbr, Description, ComponentType FROM Tests WHERE IsMarkable <> 0 AND IsActive <> 0 " &
               "UNION SELECT ID, Name, Abbr, Description, ComponentType FROM Groups WHERE IsMarkable <> 0 AND IsActive <> 0 " &
               "UNION SELECT ID, Name, Abbr, Description, ComponentType FROM Profiles WHERE IsMarkable <> 0 AND IsActive <> 0"
        End If

        dgvTGs.Rows.Clear()

        Using connection As New SqlConnection(connString)
            connection.Open()
            Using command As New SqlCommand(sSQL, connection)
                If Not String.IsNullOrEmpty(txtTerm.Text) Then
                    command.Parameters.AddWithValue("@Term", txtTerm.Text)
                End If

                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Dim imagePath As String = If(reader("ComponentType").ToString() = "T", "Images\Test.Ico",
                                          If(reader("ComponentType").ToString() = "G", "Images\Group.Ico", "Images\Profile.Ico"))
                        Dim image As Image = System.Drawing.Image.FromFile(IO.Path.Combine(Application.StartupPath, imagePath))

                        dgvTGs.Rows.Add(reader("ID"), image, reader("Name"), reader("Abbr"), reader("Description"))
                    End While
                End Using
            End Using
        End Using
    End Sub
    Private Sub dgvTGs_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTGs.CellDoubleClick
        DisplayTGP(dgvTGs.Rows(e.RowIndex).Cells(0).Value)
    End Sub
    Private Function GetSource(ByVal TGPID As Integer) As String
        Dim Source As String = ""
        Dim TGPType As String = GetTGPType(TGPID)
        Dim TIN As String = ""

        Using connection As New SqlConnection(connString)
            connection.Open()

            If TGPType = "T" Then
                Dim queryTest As String = "SELECT Name FROM Sources WHERE Material_ID IN (SELECT Material_ID FROM Test_Material WHERE Test_ID = @TGPID)"
                Using commandTest As New SqlCommand(queryTest, connection)
                    commandTest.Parameters.AddWithValue("@TGPID", TGPID)
                    Using reader As SqlDataReader = commandTest.ExecuteReader()
                        While reader.Read()
                            Dim sourceName As String = reader("Name").ToString().Trim()
                            If Not Source.Contains(sourceName) Then
                                Source &= sourceName & ", "
                            End If
                        End While
                    End Using
                End Using
            ElseIf TGPType = "G" Then
                Dim queryGroup As String = "SELECT Test_ID FROM Group_Test WHERE Group_ID = @TGPID"
                Using commandGroup As New SqlCommand(queryGroup, connection)
                    commandGroup.Parameters.AddWithValue("@TGPID", TGPID)
                    Using readerGroup As SqlDataReader = commandGroup.ExecuteReader()
                        While readerGroup.Read()
                            Dim testID As Integer = readerGroup("Test_ID")
                            Dim querySource As String = "SELECT Name FROM Sources WHERE Material_ID IN (SELECT Material_ID FROM Test_Material WHERE Test_ID = @TestID)"
                            Using commandSource As New SqlCommand(querySource, connection)
                                commandSource.Parameters.AddWithValue("@TestID", testID)
                                Using readerSource As SqlDataReader = commandSource.ExecuteReader()
                                    While readerSource.Read()
                                        Dim sourceName As String = readerSource("Name").ToString().Trim()
                                        If Not Source.Contains(sourceName) Then
                                            Source &= sourceName & ", "
                                        End If
                                    End While
                                End Using
                            End Using
                        End While
                    End Using
                End Using
            Else
                Dim queryProfile As String = "SELECT GrpTst_ID FROM Prof_GrpTst WHERE Profile_ID = @TGPID"
                Using commandProfile As New SqlCommand(queryProfile, connection)
                    commandProfile.Parameters.AddWithValue("@TGPID", TGPID)
                    Using readerProfile As SqlDataReader = commandProfile.ExecuteReader()
                        While readerProfile.Read()
                            Dim grpTstID As Integer = readerProfile("GrpTst_ID")
                            TIN = GetTGPType(grpTstID)

                            Dim querySource As String = "SELECT Name FROM Sources WHERE Material_ID IN (SELECT Material_ID FROM Test_Material WHERE Test_ID = @GrpTstID)"
                            Using commandSource As New SqlCommand(querySource, connection)
                                commandSource.Parameters.AddWithValue("@GrpTstID", grpTstID)
                                Using readerSource As SqlDataReader = commandSource.ExecuteReader()
                                    While readerSource.Read()
                                        Dim sourceName As String = readerSource("Name").ToString().Trim()
                                        If Not Source.Contains(sourceName) Then
                                            Source &= sourceName & ", "
                                        End If
                                    End While
                                End Using
                            End Using
                        End While
                    End Using
                End Using
            End If
        End Using

        If Source.Length >= 2 Then Source = Source.Substring(0, Source.Length - 2)
        Return Source
    End Function
End Class
