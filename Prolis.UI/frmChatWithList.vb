Imports System.ComponentModel
Imports System.Data.SqlClient

Public Class frmChatWithList

    Dim listofUser As New User

    Private conn As SqlConnection
    Private cmd As SqlCommand

    Private isFormLoaded As Boolean = False
    'Private isChatLoaded As Boolean = False
    Private checkForPrevMsg As Boolean = False

    Dim Sender_Font As New Font("Arial", 9, FontStyle.Bold)
    Dim ID_Font As New Font("Courier New", 6)
    Dim Msg_Font As New Font("Times New Roman", 12)

    Dim SelectedUser As User

    Private selectedRowIndex As Integer = -1

    Dim usersList As New List(Of User)



    Public Sub New()
        InitializeComponent()
        'InitializeBrowser()
        'InitializeBrowser(url).GetAwaiter().GetResult()

    End Sub

    'Private Async Sub InitializeBrowser(Optional url As String = Nothing)
    '    Try
    '        Dim userDataFolder As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName)
    '        Dim env As CoreWebView2Environment = Await CoreWebView2Environment.CreateAsync(Nothing, userDataFolder)
    '        Await WebView.EnsureCoreWebView2Async(env)
    '        WebView.Source = New UriBuilder(If(url, url)).Uri
    '    Catch ex As Exception
    '        MessageBox.Show("Error initializing browser: " & ex.Message)
    '    End Try
    'End Sub


    'Private Async Function EnsureWebView2InitializedAsync() As Task
    '    ' Ensure CoreWebView2 is initialized
    '    If WebView.CoreWebView2 Is Nothing Then
    '        Await WebView.EnsureCoreWebView2Async(Nothing)
    '    End If
    'End Function

    Private Sub frmChatWithList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize WebView2
        '===================\\
        ' Initialize WebView2
        'Await EnsureWebView2InitializedAsync()

        '' Create and build the HTML content using StringBuilder
        'Dim chatHtml As New StringBuilder()
        'chatHtml.Append("<html><body>")
        'chatHtml.Append("<div id='chatBox' style='height:400px; overflow:auto; border:1px solid #ccc;'></div>")
        'chatHtml.Append("<input type='text' id='userInput' style='width:100%;' />")
        'chatHtml.Append("<button onclick='sendMessage()'>Send</button>")
        'chatHtml.Append("<script>")
        'chatHtml.Append("function sendMessage() {")
        'chatHtml.Append("    var chatBox = document.getElementById('chatBox');")
        'chatHtml.Append("    var userInput = document.getElementById('userInput');")
        'chatHtml.Append("    var message = userInput.value;")
        'chatHtml.Append("    chatBox.innerHTML += '<div>' + message + '</div>';")
        'chatHtml.Append("    userInput.value = '';")
        'chatHtml.Append("    chatBox.scrollTop = chatBox.scrollHeight;")
        'chatHtml.Append("}")
        'chatHtml.Append("</script>")
        'chatHtml.Append("</body></html>")

        '' Convert StringBuilder to String
        'Dim htmlString As String = chatHtml.ToString()

        '' Load the HTML string into WebView2
        'WebView.NavigateToString(htmlString)

        '===================

        Try
            dgv.RowHeadersVisible = False
            dgvClients.RowHeadersVisible = False

            txtUserID.Text = ThisUser.ID
            txtUserName.Text = ThisUser.UserName

            Using con As New SqlConnection(connString)
                'Dim con As New SqlConnection(connString)
                con.Open()
                Using cmd As New SqlCommand($"select FullName from users where id = {ThisUser.ID} ", con)
                    txtUserName.Text = cmd.ExecuteScalar

                End Using
                'Dim sendcommand As New SqlCommand("INSERT INTO tbl_chat (SenderID, ReceiverID,  messageText, isRead ,messageType) VALUES (' " & txtUserID.Text & "', ' " & lblUserID.Text & "',  ' " & txtMsg.Text & "',  " & IIf(txtUserID.Text = lblUserID.Text, 1, 0) & ",'" & megType.Text & "') ; select SCOPE_IDENTITY(); ", con)

            End Using

            usersList.Clear()
            UpdateUsersList(usersList)
            LoadClientDGV()
            LoadDGV()
            Dim selectedIndex As Integer = TabControl1.SelectedIndex
            isFormLoaded = True
            TabControl1.SelectedIndex = 1
            If dgvClients.Rows.Count > 0 Then
                ' Select the first row
                dgvClients.Rows(0).Selected = True
                LoadChatByClientUserID()

            End If
            If dgv.Rows.Count > 0 Then
                ' Select the first row
                dgv.Rows(0).Selected = True

                Dim row As DataGridViewRow = dgv.Rows(0)
                lblUserID.Text = row.Cells("SenderID").Value.ToString()
                lblUserName.Text = row.Cells("SenderName").Value.ToString()

                LoadChatByUserID()

                ' Load HTML into WebView2


            End If

        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub DisplayMessages(list As List(Of Messages))
        If SelectedUser Is Nothing Then Return

        If SelectedUser.listOfMessages.Count = 0 Then
            rtbBgChat.Clear()
            rtbChat.Clear()
            Return
        End If

        'Dim chatHtml As New StringBuilder

        For Each msg In list

            Dim SenderName, msgTxt As String

            SenderName = msg.SenderName & " (" & msg.Dated & ")"
            msgTxt = msg.messageText
            '=================================
            'chatHtml.AppendLine("<div>")
            'chatHtml.AppendLine($"<br><strong>{SenderName}</strong>")
            'chatHtml.AppendLine($"<br>{msgTxt}")

            '=================================

            If msg.SenderName <> txtUserName.Text Then
                rtbBgChat.SelectionAlignment = HorizontalAlignment.Left
            Else
                rtbBgChat.SelectionAlignment = HorizontalAlignment.Right
            End If

            rtbBgChat.SelectionFont = Sender_Font
            rtbBgChat.SelectionColor = Color.SteelBlue

            AppendTextWithFontAndColor(rtbBgChat, SenderName, Sender_Font, Color.SteelBlue)

            If msg.SenderName <> txtUserName.Text AndAlso msg.isRead = False Then

                AppendTextWithFontAndColor(rtbBgChat, "|ID:" & msg.messageID & "|", ID_Font, rtbBgChat.BackColor)

                'chatHtml.AppendLine($"<br>{"|ID:" & msg.messageID & "|"}")

            End If

            'chatHtml.AppendLine("</div>")

            rtbBgChat.SelectionFont = Msg_Font
            rtbBgChat.SelectionColor = Color.Black

            AppendTextWithFontAndColor(rtbBgChat, Environment.NewLine & msgTxt & Environment.NewLine, Msg_Font, Color.Black)

            msg.isAdded = True
        Next



        If rtbBgChat.TextLength <> rtbChat.TextLength Then

            'WebView.NavigateToString(chatHtml.ToString)

            'WebView.NavigateToString(chatHtml.ToString)

            With rtbChat

                rtbChat.Clear()
                rtbChat.Rtf = rtbBgChat.Rtf
                rtbChat.SelectionStart = rtbBgChat.TextLength
                rtbChat.ScrollToCaret()
            End With
        End If
    End Sub
    Private Sub AppendTextWithFontAndColor(rtb As RichTextBox, text As String, font As Font, color As Color)

        ' Save the current selection font and color
        Dim currentFont As Font = rtb.SelectionFont
        Dim currentColor As Color = rtb.SelectionColor
        Dim currentAlignment As HorizontalAlignment = rtb.SelectionAlignment

        ' Move the caret to the end of the RichTextBox content
        rtb.SelectionStart = rtb.TextLength
        rtb.SelectionLength = 0

        ' Set the font and color for the new text
        rtb.SelectionFont = font
        rtb.SelectionColor = color

        ' Append the text with the specified font and color
        'rtb.AppendText(text)
        'Insert the text with the specified font And color
        rtb.SelectedText = text

        ' Restore the original alignment, font, and color
        rtb.SelectionAlignment = currentAlignment
        rtb.SelectionFont = currentFont
        rtb.SelectionColor = currentColor

    End Sub

    Private Sub LoadDGV()
        ' Clear existing rows and columns in DataGridView
        With dgv
            .Rows.Clear()
            .Columns.Clear()

            ' Add columns to DataGridView
            .Columns.Add("SenderID", "Sender ID")
            .Columns.Add("SenderName", "Sender Name")
            .Columns.Add("unReadMessageCount", "Unread Message Count")

            .Columns(0).Visible = False
            .Columns(2).Width = 30
            .Columns(2).MinimumWidth = 30
            .Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        End With

        ' Iterate through the list and add rows to DataGridView
        usersList.RemoveAll(Function(u) u.UserID = ThisUser.ID)
        For Each user In usersList
            If user.UserName.Contains(". ") Then

                Continue For
            End If
            dgv.Rows.Add(user.UserID, user.UserName, user.unReadMessageCount)
        Next

    End Sub
    Private Sub LoadClientDGV()
        With dgvClients
            .Rows.Clear()
            .Columns.Clear()

            ' Add columns to DataGridView
            .Columns.Add("SenderID", "Sender ID")
            .Columns.Add("SenderName", "Sender Name")
            .Columns.Add("unReadMessageCount", "Unread Message Count")

            .Columns(0).Visible = False
            .Columns(2).Width = 30
            .Columns(2).MinimumWidth = 30
            .Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        End With
        ' Iterate through the list and add rows to DataGridView
        usersList.RemoveAll(Function(u) u.UserID = ThisUser.ID)
        For Each user In usersList
            If user.UserName.Contains(". ") Then
                dgvClients.Rows.Add(user.UserID, user.UserID.ToString() + user.UserName, user.unReadMessageCount)
                Continue For
            End If

        Next

    End Sub
    Public Function GetUsersWithMessages() As List(Of User)
        usersList.Clear()

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT  id as SenderID , FullName as SenderName , (SELECT COUNT(isread) FROM tbl_Chat WHERE SenderID = u.id AND isRead = 0) AS 'unReadMessageCount' FROM users u ORDER BY (SELECT MAX(dated) FROM tbl_Chat WHERE (SenderID = u.id AND ReceiverID =  " & txtUserID.Text & ") OR (SenderID =  " & txtUserID.Text & " AND ReceiverID = u.id)) DESC;"

            Using command As New SqlCommand(query, connection)
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Dim user As New User With {
                            .UserID = reader.GetInt32(reader.GetOrdinal("SenderID")),
                            .UserName = reader.GetString(reader.GetOrdinal("SenderName")),
                            .unReadMessageCount = IIf(reader.GetInt32(reader.GetOrdinal("unReadMessageCount")) = 0, "", reader.GetInt32(reader.GetOrdinal("unReadMessageCount")))
                        }

                        ' Populate the list of messages for each user
                        user.listOfMessages = GetMessagesByUserID(user.UserID, txtUserID.Text)

                        ' Add the user to the list
                        usersList.Add(user)
                    End While
                End Using
            End Using
            connection.Open()
            Dim query1 As String = "SELECT  Provider_ID as SenderID ,  '. '+u.LastName +' '+ u.FirstName as SenderName , (SELECT COUNT(isread) FROM tbl_Chat WHERE SenderID = u.Provider_ID AND isRead = 0) AS 'unReadMessageCount' FROM Client_Users u ORDER BY (SELECT MAX(dated) FROM tbl_Chat WHERE (SenderID = u.Provider_ID AND ReceiverID =  " & txtUserID.Text & ") OR (SenderID =  " & txtUserID.Text & " AND ReceiverID = u.Provider_ID)) DESC;"
            'Dim query1 As String = "SELECT  Provider_ID as SenderID ,  '. '+u.UserName   as SenderName , (SELECT COUNT(isread) FROM tbl_Chat WHERE SenderID = u.Provider_ID AND isRead = 0) AS 'unReadMessageCount' FROM Client_Users u ORDER BY (SELECT MAX(dated) FROM tbl_Chat WHERE (SenderID = u.Provider_ID AND ReceiverID =  " & txtUserID.Text & ") OR (SenderID =  " & txtUserID.Text & " AND ReceiverID = u.Provider_ID)) DESC;"
            Using command As New SqlCommand(query1, connection)
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Dim user As New User With {
                            .UserID = reader.GetInt32(reader.GetOrdinal("SenderID")),
                            .UserName = reader.GetString(reader.GetOrdinal("SenderName")),
                            .unReadMessageCount = IIf(reader.GetInt32(reader.GetOrdinal("unReadMessageCount")) = 0, "", reader.GetInt32(reader.GetOrdinal("unReadMessageCount")))
                        }

                        ' Populate the list of messages for each user
                        user.listOfMessages = GetMessagesByUserID(user.UserID, txtUserID.Text)

                        ' Add the user to the list
                        usersList.Add(user)
                    End While
                End Using
            End Using

        End Using

        Return usersList
    End Function

    Private Function GetMessagesByUserID(SenderID As Integer, ReceiverID As Integer) As List(Of Messages)
        Dim messagesList As New List(Of Messages)

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT dbo.tbl_Chat.messageType, dbo.tbl_Chat.MessageID, dbo.tbl_Chat.SenderID, dbo.users.FullName as SenderName, dbo.tbl_Chat.ReceiverID, users_1.Fullname AS ReceiverName, dbo.tbl_Chat.messageText, dbo.tbl_Chat.isread, dbo.tbl_Chat.Dated FROM dbo.tbl_Chat INNER JOIN dbo.users ON dbo.tbl_Chat.SenderID = dbo.users.id INNER JOIN dbo.users AS users_1 ON dbo.tbl_Chat.ReceiverID = users_1.id where (ReceiverID =  @ReceiverID and SenderID = @SenderID ) or ( ReceiverID = @SenderID and SenderID = @ReceiverID ) order by Dated"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@SenderID", SenderID)
                command.Parameters.AddWithValue("@ReceiverID", ReceiverID)

                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Dim message As New Messages With {
                            .messageID = Convert.ToInt32(reader("messageID")),
                            .SenderID = Convert.ToInt32(reader("SenderID")),
                            .SenderName = reader("SenderName").ToString,
                            .ReceiverID = Convert.ToInt32(reader("ReceiverID")),
                            .ReceiverName = reader("ReceiverName").ToString,
                            .messageText = reader("messageText").ToString,
                            .isRead = Convert.ToBoolean(reader("isRead")),
                            .Dated = Convert.ToDateTime(reader("Dated")),
                            .isAdded = True,
                            .megType = reader("messageType").ToString
                        }

                        ' Add the message to the list
                        messagesList.Add(message)
                    End While
                End Using
            End Using
        End Using

        Return messagesList
    End Function

    Private Sub dgv_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellClick

        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgv.Rows(e.RowIndex)
            lblUserID.Text = row.Cells("SenderID").Value.ToString()
            lblUserName.Text = row.Cells("SenderName").Value.ToString()
            If lblUserName.Text.Contains(". ") Then
                megType.Text = "internal-external"
            End If

            If Not IsNothing(SelectedUser) AndAlso lblUserID.Text = SelectedUser.UserID Then

                Return '' do nothing if same user
            End If

        End If

        LoadChatByUserID()
    End Sub

    Private Sub LoadChatByUserID()
        rtbChat.Clear()
        rtbBgChat.Clear()

        SelectedUser = usersList.FirstOrDefault(Function(user) user.UserID = lblUserID.Text)

        Dim maxMessageID As Integer = GetMaxMessageID()
        Dim con As New SqlConnection(connString)
        con.Open()

        Dim query As String = "SELECT dbo.tbl_Chat.messageType,dbo.tbl_Chat.MessageID, dbo.tbl_Chat.SenderID, dbo.users.FullName as SenderName, dbo.tbl_Chat.ReceiverID, users_1.Fullname AS ReceiverName, dbo.tbl_Chat.messageText, dbo.tbl_Chat.isread, dbo.tbl_Chat.Dated FROM dbo.tbl_Chat INNER JOIN dbo.users ON dbo.tbl_Chat.SenderID = dbo.users.id INNER JOIN dbo.users AS users_1 ON dbo.tbl_Chat.ReceiverID = users_1.id where (ReceiverID = '" & txtUserID.Text & "' and SenderID = '" & lblUserID.Text & "' or ReceiverID = '" & lblUserID.Text & "' and SenderID = '" & txtUserID.Text & "') and MessageID > " & maxMessageID & " order by Dated"

        Using command As New SqlCommand(query, con)

            Using reader As SqlDataReader = command.ExecuteReader()
                While reader.Read()

                    Dim message As New Messages With {
    .messageID = If(reader("messageID") IsNot DBNull.Value, Convert.ToInt32(reader("messageID")), 0),
    .SenderID = If(reader("SenderID") IsNot DBNull.Value, Convert.ToInt32(reader("SenderID")), 0),
    .SenderName = If(reader("SenderName") IsNot DBNull.Value, reader("SenderName").ToString(), ""),
    .ReceiverID = If(reader("ReceiverID") IsNot DBNull.Value, Convert.ToInt32(reader("ReceiverID")), 0),
    .ReceiverName = If(reader("ReceiverName") IsNot DBNull.Value, reader("ReceiverName").ToString(), ""),
    .messageText = If(reader("messageText") IsNot DBNull.Value, reader("messageText").ToString(), ""),
    .isRead = If(reader("isRead") IsNot DBNull.Value, Convert.ToBoolean(reader("isRead")), False),
    .Dated = If(reader("Dated") IsNot DBNull.Value, Convert.ToDateTime(reader("Dated")), DateTime.MinValue),
    .megType = If(reader("messageType") IsNot DBNull.Value, reader("messageType").ToString(), "internal")
}

                    ' Add the message to the list
                    SelectedUser.listOfMessages.Add(message)
                End While
            End Using
        End Using
        If lblUserName.Text.Contains(". ") Then
            megType.Text = "internal-external"
        End If
        DisplayMessages(SelectedUser.listOfMessages)
    End Sub
    Private Sub LoadChatByClientUserID()
        If lblUserID.Text = "Name" Then
            Exit Sub
        End If
        rtbChat.Clear()
        rtbBgChat.Clear()
        If SelectedUser Is Nothing Then
            Return

        End If
        SelectedUser.listOfMessages.Clear()
        SelectedUser = usersList.FirstOrDefault(Function(user) user.UserID = lblUserID.Text)

        Dim maxMessageID As Integer = GetMaxMessageID()
        Dim con As New SqlConnection(connString)
        con.Open()

        Dim query As String = "SELECT ReceiverID,dbo.tbl_Chat.messageType,dbo.tbl_Chat.MessageID, dbo.tbl_Chat.SenderID,    dbo.tbl_Chat.messageText, dbo.tbl_Chat.isread, dbo.tbl_Chat.Dated FROM dbo.tbl_Chat   where (ReceiverID = '" & txtUserID.Text & "' and SenderID = '" & lblUserID.Text & "' )or (ReceiverID = '" & lblUserID.Text & "' and SenderID = '" & txtUserID.Text & "')    order by Dated"
        'MessageID > " & maxMessageID & "

        Using command As New SqlCommand(query, con)

            Using reader As SqlDataReader = command.ExecuteReader()
                While reader.Read()
                    Dim typ = reader("messageType").ToString()

                    'Dim message As New Messages With {
                    '                .messageID = If(reader("messageID") IsNot DBNull.Value, Convert.ToInt32(reader("messageID")), 0),
                    '                .SenderID = If(reader("SenderID") IsNot DBNull.Value, Convert.ToInt32(reader("SenderID")), 0),
                    '                .ReceiverName = If(typ = "external-internal", ThisUser.Name, CommonData.RetrieveColumnValue("Client_Users", "LastName", "Provider_ID", lblUserID.Text, connString, "").ToString()),
                    '                .SenderName = If(typ = "internal-external", ThisUser.Name, CommonData.RetrieveColumnValue("Client_Users", "LastName", "Provider_ID", lblUserID.Text, connString, "").ToString()),
                    '                .ReceiverID = If(reader("ReceiverID") IsNot DBNull.Value, Convert.ToInt32(reader("ReceiverID")), 0),
                    '                .messageText = If(reader("messageText") IsNot DBNull.Value, reader("messageText").ToString(), ""),
                    '                .isRead = If(reader("isRead") IsNot DBNull.Value, Convert.ToBoolean(reader("isRead")), False),
                    '                .Dated = If(reader("Dated") IsNot DBNull.Value, Convert.ToDateTime(reader("Dated")), DateTime.MinValue),
                    '                                .megType = If(reader("messageType") IsNot DBNull.Value, reader("messageType").ToString(), "internal")
                    '            }


                    Dim message As New Messages With {
                                    .messageID = If(reader("messageID") IsNot DBNull.Value, Convert.ToInt32(reader("messageID")), 0),
                                    .SenderID = If(reader("SenderID") IsNot DBNull.Value, Convert.ToInt32(reader("SenderID")), 0),
                                    .ReceiverName = If(typ = "external-internal", ThisUser.Name, CommonData.RetrieveColumnValue("Client_Users", "UserName", "Provider_ID", lblUserID.Text, "").ToString()),
                                    .SenderName = If(typ = "internal-external", ThisUser.Name, CommonData.RetrieveColumnValue("Client_Users", "UserName", "Provider_ID", lblUserID.Text, "").ToString()),
                                    .ReceiverID = If(reader("ReceiverID") IsNot DBNull.Value, Convert.ToInt32(reader("ReceiverID")), 0),
                                    .messageText = If(reader("messageText") IsNot DBNull.Value, reader("messageText").ToString(), ""),
                                    .isRead = If(reader("isRead") IsNot DBNull.Value, Convert.ToBoolean(reader("isRead")), False),
                                    .Dated = If(reader("Dated") IsNot DBNull.Value, Convert.ToDateTime(reader("Dated")), DateTime.MinValue),
                                                    .megType = If(reader("messageType") IsNot DBNull.Value, reader("messageType").ToString(), "internal")
                                }


                    '.ReceiverName = If(reader("ReceiverName") IsNot DBNull.Value, reader("ReceiverName").ToString(), ""),

                    ' Add the message to the list
                    SelectedUser.listOfMessages.Add(message)
                End While
            End Using
        End Using
        If lblUserName.Text.Contains(". ") Then
            megType.Text = "internal-external"
        End If
        If SelectedUser Is Nothing Then
            Return
        End If
        DisplayMessages(SelectedUser.listOfMessages)
    End Sub

    Private Function GetMaxMessageID() As Integer

        Dim maxMessageID As Integer = 0

        If SelectedUser IsNot Nothing AndAlso SelectedUser.listOfMessages.Count > 0 Then

            maxMessageID = SelectedUser.listOfMessages.Max(Function(msg) msg.messageID)

        End If

        Return maxMessageID
    End Function
    Private Async Sub SearchVisibleTextInWebView2(searchText As String)
        Dim script As String = "
    function searchVisibleText(searchText) {
        const visibleElements = [];
        const elements = document.querySelectorAll('body *');

        elements.forEach(element => {
            const rect = element.getBoundingClientRect();
            const isVisible = (
                rect.top >= 0 &&
                rect.left >= 0 &&
                rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) &&
                rect.right <= (window.innerWidth || document.documentElement.clientWidth)
            );

            if (isVisible && element.textContent.includes(searchText)) {
                visibleElements.push(element);
            }
        });

        return visibleElements.length > 0;
    }

    searchVisibleText('" & searchText & "');"

        'Dim result As String = Await WebView.CoreWebView2.ExecuteScriptAsync(script)

        'If Boolean.TryParse(result, False) AndAlso Boolean.Parse(result) Then
        '    MessageBox.Show("Text found in the visible area!")
        'Else
        '    MessageBox.Show("Text not found in the visible area.")
        'End If
    End Sub


    Private WithEvents backgroundWorker As New BackgroundWorker()
    Dim first10Messages As List(Of Messages)
    Dim otherMessages As List(Of Messages)

    Private Sub btnSendMsg_Click(sender As Object, e As EventArgs) Handles btnSendMsg.Click

        If String.IsNullOrWhiteSpace(txtMsg.Text) Then

            txtMsg.Focus()
            Exit Sub
        End If

        Dim con As New SqlConnection(connString)
        con.Open()

        Dim sendcommand As New SqlCommand("INSERT INTO tbl_chat (SenderID, ReceiverID,  messageText, isRead ,messageType) VALUES (' " & txtUserID.Text & "', ' " & lblUserID.Text & "',  ' " & txtMsg.Text & "',  " & IIf(txtUserID.Text = lblUserID.Text, 1, 0) & ",'" & megType.Text & "') ; select SCOPE_IDENTITY(); ", con)

        Try

            Dim insertedID As Integer = sendcommand.ExecuteScalar()

            If insertedID > 0 Then

                Dim message As New Messages With {
    .messageID = insertedID,
    .SenderID = Convert.ToInt32(txtUserID.Text),
    .SenderName = txtUserName.Text,
    .ReceiverID = Convert.ToInt32(lblUserID.Text),
    .ReceiverName = lblUserName.Text,
    .messageText = txtMsg.Text,
    .isRead = False,
    .Dated = Date.Now
}
                '' Add the message to the list
                SelectedUser.listOfMessages.Add(message)

                rtbBgChat.HideSelection = False ' Keeps selection when losing focus

                rtbBgChat.SelectionAlignment = HorizontalAlignment.Right

                Dim SenderName As String = txtUserName.Text & " (" & Now.ToString & ")"

                AppendTextWithFontAndColor(rtbBgChat, SenderName, Sender_Font, Color.SteelBlue)

                AppendTextWithFontAndColor(rtbBgChat, Environment.NewLine & txtMsg.Text & Environment.NewLine, Msg_Font, Color.Black)

                txtMsg.Clear()

                If rtbBgChat.TextLength <> rtbChat.TextLength Then

                    With rtbChat

                        .Clear()
                        .Rtf = rtbBgChat.Rtf
                        .SelectionStart = rtbBgChat.TextLength
                        .ScrollToCaret()
                    End With
                End If

            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Database Error")
        End Try
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If dgv.InvokeRequired Then
            ' Invoke the method on the UI thread
            dgv.Invoke(Sub()
                           UpdateControls()
                       End Sub)
        Else
            ' You are already on the UI thread, directly call the method
            UpdateControls()
        End If
    End Sub

    Private Sub UpdateControls()
        'SaveSelectedUser()

        Dim newUserList As New List(Of User)

        UpdateUsersList(newUserList)
        CompareLists(newUserList)

        LoadDGV()

        RestoreSelectedUser()
        Dim selectedIndex As Integer = TabControl1.SelectedIndex
        If dgv.SelectedRows.Count > 0 AndAlso selectedIndex = 1 Then
            ' Get the current selected row
            Dim currentRow As DataGridViewRow = dgv.SelectedRows(0)

            ' Assuming the column you want to retrieve has the name "ColumnName"
            Dim columnIndex As Integer = dgv.Columns("UnreadMessageCount").Index

            ' Get the value from the specific column in the current row
            Dim cellValue As Object = currentRow.Cells(columnIndex).Value

            ' Check if the cell value is not null
            If cellValue IsNot Nothing Then

                GetUnreadMessages()

            End If
        ElseIf dgvClients.SelectedRows.Count > 0 AndAlso selectedIndex = 0 Then
            ' Get the current selected row
            Dim currentRow As DataGridViewRow = dgvClients.SelectedRows(0)

            ' Assuming the column you want to retrieve has the name "ColumnName"
            Dim columnIndex As Integer = dgvClients.Columns("UnreadMessageCount").Index

            ' Get the value from the specific column in the current row
            Dim cellValue As Object = currentRow.Cells(columnIndex).Value

            ' Check if the cell value is not null
            If cellValue IsNot Nothing Then

                GetUnreadMessages()

            End If

        End If
    End Sub
    Public Sub CompareLists(newUserList As List(Of User))

        For Each user In newUserList
            Dim matchingUser = usersList.Find(Function(existingUser) existingUser.Equals(user))

            If matchingUser IsNot Nothing Then
                ' Update the members of the new user with the corresponding members from the existing user
                user.listOfMessages = matchingUser.listOfMessages
                ' Update other members as needed
            End If
        Next

        usersList.Clear()
        usersList = newUserList

    End Sub
    Private Sub UpdateUsersList(newUserList As List(Of User))

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT id as SenderID , FullName as SenderName , (SELECT COUNT(isread) FROM tbl_Chat WHERE SenderID = u.id AND ReceiverID =  " & txtUserID.Text & " AND isRead = 0) AS 'unReadMessageCount' FROM users u ORDER BY (SELECT MAX(dated) FROM tbl_Chat WHERE (SenderID = u.id AND ReceiverID = " & txtUserID.Text & ") OR (SenderID =  " & txtUserID.Text & " AND ReceiverID = u.id)) DESC;"

            Using command As New SqlCommand(query, connection)
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Dim user As New User With {
                            .UserID = reader.GetInt64(reader.GetOrdinal("SenderID")),
                            .UserName = reader.GetString(reader.GetOrdinal("SenderName")),
                        .unReadMessageCount = IIf(reader.GetInt32(reader.GetOrdinal("unReadMessageCount")) = 0, "", reader.GetInt32(reader.GetOrdinal("unReadMessageCount")))
                                                   }
                        ' Add the user to the list
                        newUserList.Add(user)
                    End While
                End Using
            End Using
            'Dim query1 As String = "SELECT  Provider_ID as SenderID ,  '. '+u.LastName +' '+ u.FirstName as SenderName , (SELECT COUNT(isread) FROM tbl_Chat WHERE SenderID = u.Provider_ID AND isRead = 0) AS 'unReadMessageCount' FROM Client_Users u ORDER BY (SELECT MAX(dated) FROM tbl_Chat WHERE (SenderID = u.Provider_ID AND ReceiverID =  " & txtUserID.Text & ") OR (SenderID =  " & txtUserID.Text & " AND ReceiverID = u.Provider_ID)) DESC;"
            Dim query1 As String = $"SELECT  Provider_ID as SenderID ,  '. '+u.LastName +' '+ u.FirstName as SenderName , (SELECT COUNT(isread) FROM tbl_Chat WHERE SenderID = u.Provider_ID AND isRead = 0) AS 'unReadMessageCount' FROM Client_Users u ORDER BY (SELECT MAX(dated) FROM tbl_Chat WHERE (SenderID = u.Provider_ID AND ReceiverID =  " & txtUserID.Text & ") OR (SenderID =  " & txtUserID.Text & " AND ReceiverID = u.Provider_ID)) DESC;"
            '        query1 = "SELECT " & _
            '"u.Provider_ID AS SenderID," & _
            '"CONCAT('. ', u.LastName, ' ', u.FirstName) AS SenderName, " & _
            '"COUNT(CASE WHEN c.isRead = 0 THEN 1 END) AS unReadMessageCount " & _
            '        "FROM " & _
            '        "Client_Users u " & _
            '        "Left Join  " & _
            '" tbl_Chat c ON (c.SenderID = u.Provider_ID AND c.ReceiverID = " & txtUserID.Text & ")" & _
            '            " OR (c.SenderID = " & txtUserID.Text & " AND c.ReceiverID = u.Provider_ID)" & _
            '        " where Provider_ID = 45100 GROUP BY" & _
            '        " u.Provider_ID, u.LastName, u.FirstName" & _
            '        " ORDER BY" & _
            '" MAX(c.dated) DESC;"
            Using command As New SqlCommand(query1, connection)
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Dim user As New User With {
                            .UserID = Convert.ToInt32(reader("SenderID")),
                            .UserName = reader("SenderName").ToString(),
                            .unReadMessageCount = IIf(reader("unReadMessageCount").ToString() = "0", "", reader("unReadMessageCount").ToString())
                        }

                        If newUserList.Any(Function(v) v.UserID = user.UserID) Then
                            Continue While
                        End If
                        ' Populate the list of messages for each user
                        user.listOfMessages = GetMessagesByUserID(user.UserID, txtUserID.Text)

                        ' Add the user to the list
                        newUserList.Add(user)
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub GetUnreadMessages()
        If lblUserID.Text = "Name" Then
            Return
        End If
        Dim query As String = "SELECT  dbo.tbl_Chat.MessageID          , dbo.tbl_Chat.SenderID           , dbo.users.FullName as SenderName, dbo.tbl_Chat.ReceiverID         , users_1.Fullname AS ReceiverName, dbo.tbl_Chat.messageText        , dbo.tbl_Chat.isread             , dbo.tbl_Chat.Dated FROM dbo.tbl_Chat  INNER Join dbo.users  ON dbo.tbl_Chat.SenderID = dbo.users.id INNER Join dbo.users AS users_1 ON dbo.tbl_Chat.ReceiverID = users_1.id where (ReceiverID = '" & txtUserID.Text & "' and SenderID = '" & lblUserID.Text & "' ) and MessageID > " & GetMaxMessageID() & " and     isread = 0 order by Dated"

        Try
            Dim tempList As New List(Of Messages)
            Using connection As New SqlConnection(connString)
                connection.Open()

                Using command As New SqlCommand(query, connection)

                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()

                            Dim message As New Messages With {
     .messageID = If(reader("messageID") IsNot DBNull.Value, Convert.ToInt32(reader("messageID")), 0),
     .SenderID = If(reader("SenderID") IsNot DBNull.Value, Convert.ToInt32(reader("SenderID")), 0),
     .SenderName = If(reader("SenderName") IsNot DBNull.Value, reader("SenderName").ToString(), ""),
     .ReceiverID = If(reader("ReceiverID") IsNot DBNull.Value, Convert.ToInt32(reader("ReceiverID")), 0),
     .ReceiverName = If(reader("ReceiverName") IsNot DBNull.Value, reader("ReceiverName").ToString(), ""),
     .messageText = If(reader("messageText") IsNot DBNull.Value, reader("messageText").ToString(), ""),
     .isRead = If(reader("isRead") IsNot DBNull.Value, Convert.ToBoolean(reader("isRead")), False),
     .Dated = If(reader("Dated") IsNot DBNull.Value, Convert.ToDateTime(reader("Dated")), DateTime.MinValue)
 }


                            ' Check if the message with the same ID already exists in the list
                            If Not SelectedUser.listOfMessages.Any(Function(m) m.messageID = message.messageID) Then
                                tempList.Add(message)
                                ' Add the message to the list
                                SelectedUser.listOfMessages.Add(message)
                            End If

                        End While
                    End Using
                End Using

                'query = "SELECT  dbo.tbl_Chat.MessageID         , dbo.tbl_Chat.SenderID           ,  '. '+user1.LastName +' '+ user1.FirstName as SenderName, dbo.tbl_Chat.ReceiverID         , '' AS ReceiverName, dbo.tbl_Chat.messageText        , dbo.tbl_Chat.isread             , dbo.tbl_Chat.Dated FROM dbo.tbl_Chat  left Join dbo.Client_Users user1 ON dbo.tbl_Chat.SenderID = user1.Provider_ID   where (ReceiverID = '" & txtUserID.Text & "' and SenderID = '" & lblUserID.Text & "' ) or (ReceiverID = '" & lblUserID.Text & "' and SenderID = '" & txtUserID.Text & "' ) and MessageID > " & GetMaxMessageID() & " and     isread = 0 order by Dated"
                query = "SELECT  dbo.tbl_Chat.MessageID          , dbo.tbl_Chat.SenderID           ,  '. '+user1.LastName +' '+ user1.FirstName as SenderName, dbo.tbl_Chat.ReceiverID         , '' AS ReceiverName, dbo.tbl_Chat.messageText        , dbo.tbl_Chat.isread             , dbo.tbl_Chat.Dated FROM dbo.tbl_Chat  left Join dbo.Client_Users user1 ON dbo.tbl_Chat.SenderID = user1.Provider_ID   where (ReceiverID = '" & txtUserID.Text & "' and SenderID = '" & lblUserID.Text & "' ) or (ReceiverID = '" & lblUserID.Text & "' and SenderID = '" & txtUserID.Text & "' ) and MessageID > " & GetMaxMessageID() & " and     isread = 0 order by Dated"
                Using command As New SqlCommand(query, connection)

                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()

                            Dim message As New Messages With {
     .messageID = If(reader("messageID") IsNot DBNull.Value, Convert.ToInt32(reader("messageID")), 0),
     .SenderID = If(reader("SenderID") IsNot DBNull.Value, Convert.ToInt32(reader("SenderID")), 0),
     .SenderName = If(reader("SenderName") IsNot DBNull.Value, reader("SenderName").ToString(), ""),
     .ReceiverID = If(reader("ReceiverID") IsNot DBNull.Value, Convert.ToInt32(reader("ReceiverID")), 0),
     .ReceiverName = If(reader("ReceiverName") IsNot DBNull.Value, reader("ReceiverName").ToString(), ""),
     .messageText = If(reader("messageText") IsNot DBNull.Value, reader("messageText").ToString(), ""),
     .isRead = If(reader("isRead") IsNot DBNull.Value, Convert.ToBoolean(reader("isRead")), False),
     .Dated = If(reader("Dated") IsNot DBNull.Value, Convert.ToDateTime(reader("Dated")), DateTime.MinValue)
 }


                            ' Check if the message with the same ID already exists in the list
                            If Not SelectedUser.listOfMessages.Any(Function(m) m.messageID = message.messageID) Then
                                tempList.Add(message)
                                ' Add the message to the list
                                SelectedUser.listOfMessages.Add(message)
                            End If

                        End While
                    End Using
                End Using

            End Using

            DisplayMessages(tempList)

        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SaveSelectedUser()
        If dgv.SelectedRows.Count > 0 Then
            selectedRowIndex = dgv.SelectedRows(0).Index

            lblUserID.Text = dgv.Rows(selectedRowIndex).Cells("SenderID").Value.ToString()

        End If
    End Sub

    ' Method to restore the selected row index
    Private Sub RestoreSelectedUser()
        If lblUserID.Text = "Name" Then
            Exit Sub
        End If
        ' Iterate through the rows to find the one with the provided UserID
        For Each row As DataGridViewRow In dgv.Rows
            ' Assuming the column containing UserID is of integer type
            Try
                If row.Cells("SenderID").Value IsNot Nothing AndAlso
                         Integer.Parse(row.Cells("SenderID").Value.ToString()) = lblUserID.Text Then
                    ' Clear existing selection
                    dgv.ClearSelection()

                    ' Select the row with the matching UserID
                    row.Selected = True

                    ' Exit the loop since we found the row
                    Exit For
                End If
            Catch ex As Exception

            End Try

        Next
        For Each row As DataGridViewRow In dgvClients.Rows
            ' Assuming the column containing UserID is of integer type
            Try
                If row.Cells("SenderID").Value IsNot Nothing AndAlso
                              Integer.Parse(row.Cells("SenderID").Value.ToString()) = lblUserID.Text Then
                    ' Clear existing selection
                    dgvClients.ClearSelection()

                    ' Select the row with the matching UserID
                    row.Selected = True

                    ' Exit the loop since we found the row
                    Exit For
                End If
            Catch ex As Exception

            End Try

        Next
    End Sub

    Private Sub rtbChat_VScroll(sender As Object, e As EventArgs) Handles rtbChat.VScroll

        FindVisibleMessages()

        'rtbBgChat.Text = rtbChat.Text ''update both
    End Sub

    Private Sub FindVisibleMessages()
        ' Get the index of the first visible character
        Dim firstCharIndex As Integer = rtbChat.GetCharIndexFromPosition(New Point(0, 0))

        ' Get the index of the last visible character
        Dim lastCharIndex As Integer = rtbChat.GetCharIndexFromPosition(New Point(rtbChat.Width - 1, rtbChat.Height - 1))

        ' Get the line numbers for the first and last visible characters
        Dim firstLine As Integer = rtbChat.GetLineFromCharIndex(firstCharIndex)
        Dim lastLine As Integer = rtbChat.GetLineFromCharIndex(lastCharIndex)

        ' Iterate through the lines in the visible area
        For lineIndex As Integer = firstLine To lastLine
            ' Get the start and end indices of each line
            Dim lineStartIndex As Integer = rtbChat.GetFirstCharIndexFromLine(lineIndex)
            Dim lineEndIndex As Integer

            ' Check if it's the last line to avoid going beyond the actual text
            If lineIndex < rtbChat.Lines.Length - 1 Then
                lineEndIndex = rtbChat.GetFirstCharIndexFromLine(lineIndex + 1) - 1
            Else
                lineEndIndex = rtbChat.TextLength - 1
            End If

            ' Extract the text of the current line
            Dim lineText As String = rtbChat.Text.Substring(lineStartIndex, lineEndIndex - lineStartIndex + 1)

            If lineText.Contains("|ID:") Then
                Dim id As String = ExtractID(lineText)

                If Not String.IsNullOrEmpty(id) Then

                    Dim flag As Boolean = UpdateReadStatusForVisibleMessage(id)

                    If flag = True Then
                        Dim searchString As String = "|ID:" & id & "|"

                        rtbChat.SelectionStart = lineStartIndex

                        Dim newText As String = ReplaceTextPreservingLineBreaks(lineText, searchString, "")

                        ' Update the line in the rtbChat
                        rtbChat.Select(lineStartIndex, lineText.Length)
                        rtbChat.SelectedText = newText

                        rtbBgChat.Rtf = rtbChat.Rtf
                        'rtbBgChat.Text = rtbChat.Text ''update both
                    End If
                End If

            End If
        Next
    End Sub

    Private Sub rtbChat_Click(sender As Object, e As EventArgs) Handles rtbChat.Click

        'SearchVisibleTextInWebView2(rtbChat.Text)
        'GetVisibleTextFromWebView2()
        FindVisibleMessages()

    End Sub

    Private Async Sub GetVisibleTextFromWebView2()
        '    Dim script As String = "
        'function getVisibleText() {
        '    const visibleText = [];
        '    const divs = document.querySelectorAll('div');

        '    divs.forEach(div => {
        '        const rect = div.getBoundingClientRect();
        '        const isVisible = (
        '            rect.top >= 0 &&
        '            rect.left >= 0 &&
        '            rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) &&
        '            rect.right <= (window.innerWidth || document.documentElement.clientWidth)
        '        );
        '        if (isVisible) {
        '            visibleText.push(div.innerText.trim());
        '        }
        '    });

        '    return visibleText.join('\n');
        '}

        'getVisibleText();"

        '    Dim result As String = Await WebView.CoreWebView2.ExecuteScriptAsync(script)

        '    ' The result will be a JSON-encoded string, so you may need to decode it
        '    Dim visibleText As String = result.Trim(""""c).Replace("\n", Environment.NewLine)

        '    Dim idPattern As String = "\|ID:\d+\|"
        '    Dim matches As MatchCollection = Regex.Matches(visibleText, idPattern)

        '    ' Collect all the matched IDs
        '    Dim visibleIDs As New List(Of String)
        '    For Each match As Match In matches
        '        visibleIDs.Add(match.Value)

        '        Dim id As String = ExtractID(match.Value)

        '        If Not String.IsNullOrEmpty(id) Then

        '            Dim flag As Boolean = UpdateReadStatusForVisibleMessage(id)

        '            'If flag = True Then
        '            '    Dim searchString As String = "|ID:" & id & "|"

        '            '    rtbChat.SelectionStart = lineStartIndex

        '            '    Dim newText As String = ReplaceTextPreservingLineBreaks(lineText, searchString, "")

        '            '    ' Update the line in the rtbChat
        '            '    rtbChat.Select(lineStartIndex, lineText.Length)
        '            '    rtbChat.SelectedText = newText

        '            '    rtbBgChat.Text = rtbChat.Text ''update both
        '            'End If
        '        End If
        '    Next

        '    ' Join all matched IDs into a single string (or handle as needed)
        '    Dim allVisibleIDs As String = String.Join(Environment.NewLine, visibleIDs)

        '    ' Display the matched IDs
        '    'MessageBox.Show(allVisibleIDs)

        '    ' Display the visible text or use it as needed
        '    'MessageBox.Show(visibleText)
    End Sub


    Private Function ExtractID(lineText As String) As String
        ' Check if the lineText contains the pattern "|ID:"
        Dim startIndex As Integer = lineText.IndexOf("|ID:")

        ' If the pattern is found, find the index of the next occurrence of "|"
        If startIndex >= 0 Then
            Dim endIndex As Integer = lineText.IndexOf("|", startIndex + 4)

            ' If "|" is found, extract the substring excluding "|ID:" and "|"
            If endIndex > 0 Then
                ' Adjust the indices to exclude "|ID:" and "|"
                Return lineText.Substring(startIndex + 4, endIndex - startIndex - 4)
            End If
        End If

        ' No matching pattern found in the line
        Return String.Empty
    End Function
    Private Function UpdateReadStatusForVisibleMessage(id As Integer)

        Dim con As New SqlConnection(connString)
        con.Open()
        Dim sendcommand As New SqlCommand("Update tbl_chat set isRead = 1 where  messageID = " & id & " ", con)

        Try

            Dim flag As Boolean = sendcommand.ExecuteNonQuery()

            Return flag

        Catch ex As Exception

            MsgBox(ex.Message, MsgBoxStyle.Critical, "Database Error")
            Return False
        End Try
    End Function


    Private Function ReplaceTextPreservingLineBreaks(originalText As String, searchString As String, newValue As String) As String
        ' Split the original text by line breaks
        Dim lines As String() = originalText.Split({vbLf}, StringSplitOptions.None)

        ' Iterate through each line and replace the text
        For i As Integer = 0 To lines.Length - 1
            If lines(i).Contains(searchString) Then
                ' Replace the text in the line
                lines(i) = lines(i).Replace(searchString, newValue)
            End If
        Next

        ' Join the lines back together with line breaks
        Dim resultText As String = String.Join(vbLf, lines)

        Return resultText
    End Function

    Private Sub searchuser_TextChanged(sender As Object, e As EventArgs) Handles searchuser.TextChanged
        Dim selectedIndex As Integer = TabControl1.SelectedIndex
        If selectedIndex = 1 Then
            Dim searchText As String = searchuser.Text.Trim()
            Dim columnIndex As Integer = 1 ' Index of the column you want to search within
            If searchText.Length < 3 Then
                Return
            End If
            If String.IsNullOrEmpty(searchText) Then
                ' If search text is empty, display all rows
                For Each row As DataGridViewRow In dgv.Rows
                    row.Visible = True
                Next
            Else
                ' Lists to store matching and non-matching rows
                Dim matchingRows As New List(Of DataGridViewRow)
                Dim nonMatchingRows As New List(Of DataGridViewRow)

                ' Iterate through each row and separate matching and non-matching rows
                For Each row As DataGridViewRow In dgv.Rows
                    If row.Cells(columnIndex).Value IsNot Nothing AndAlso row.Cells(columnIndex).Value.ToString().IndexOf(searchText, StringComparison.CurrentCultureIgnoreCase) >= 0 Then
                        matchingRows.Add(row)
                    Else
                        nonMatchingRows.Add(row)
                    End If
                Next

                ' Clear DataGridView rows
                dgv.Rows.Clear()

                ' Add matching rows first
                For Each matchingRow As DataGridViewRow In matchingRows
                    dgv.Rows.Add(matchingRow)
                Next

                ' Add non-matching rows after matching rows
                For Each nonMatchingRow As DataGridViewRow In nonMatchingRows
                    dgv.Rows.Add(nonMatchingRow)
                Next
            End If
        Else
            Dim searchText As String = searchuser.Text.Trim()
            Dim columnIndex As Integer = 1 ' Index of the column you want to search within
            If searchText.Length < 3 Then
                Return
            End If
            If String.IsNullOrEmpty(searchText) Then
                ' If search text is empty, display all rows
                For Each row As DataGridViewRow In dgvClients.Rows
                    row.Visible = True
                Next
            Else
                ' Lists to store matching and non-matching rows
                Dim matchingRows As New List(Of DataGridViewRow)
                Dim nonMatchingRows As New List(Of DataGridViewRow)

                ' Iterate through each row and separate matching and non-matching rows
                For Each row As DataGridViewRow In dgvClients.Rows
                    If row.Cells(columnIndex).Value IsNot Nothing AndAlso row.Cells(columnIndex).Value.ToString().IndexOf(searchText, StringComparison.CurrentCultureIgnoreCase) >= 0 Then
                        matchingRows.Add(row)
                    Else
                        nonMatchingRows.Add(row)
                    End If
                Next

                ' Clear DataGridView rows
                dgvClients.Rows.Clear()

                ' Add matching rows first
                For Each matchingRow As DataGridViewRow In matchingRows
                    dgvClients.Rows.Add(matchingRow)
                Next

                ' Add non-matching rows after matching rows
                For Each nonMatchingRow As DataGridViewRow In nonMatchingRows
                    dgvClients.Rows.Add(nonMatchingRow)
                Next
            End If
        End If

    End Sub

    Private Sub searchuser_Leave(sender As Object, e As EventArgs) Handles searchuser.Leave
        If searchuser.Text = "" Then
            searchuser.Text = "Search here..."
        End If
    End Sub

    Private Sub searchuser_Enter(sender As Object, e As EventArgs) Handles searchuser.Enter
        If searchuser.Text.Contains("Search") Then
            searchuser.Text = ""
        End If
    End Sub

    Private Sub dgvClients_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvClients.CellClick

        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvClients.Rows(e.RowIndex)
            lblUserID.Text = row.Cells("SenderID").Value.ToString()
            lblUserName.Text = lblUserID.Text + " - " + row.Cells("SenderName").Value.ToString()
            If lblUserName.Text.Contains(". ") Then
                megType.Text = "internal-external"
            End If

            If Not IsNothing(SelectedUser) AndAlso lblUserID.Text = SelectedUser.UserID Then

                Return '' do nothing if same user
            End If

        End If

        LoadChatByClientUserID()
    End Sub


    'Private Sub WebView_NavigationCompleted(sender As Object, e As CoreWebView2NavigationCompletedEventArgs) Handles WebView.NavigationCompleted
    '    ' Inject JavaScript to detect click and resize events
    '    Dim script As String = "
    'document.addEventListener('click', function(event) {
    '    try {
    '        chrome.webview.postMessage(JSON.stringify({ type: 'click', x: event.clientX, y: event.clientY }));
    '    } catch (err) {
    '        console.error('Error sending click message:', err);
    '    }
    '});

    'window.addEventListener('resize', function() {
    '    try {
    '        chrome.webview.postMessage(JSON.stringify({ type: 'resize', width: window.innerWidth, height: window.innerHeight }));
    '    } catch (err) {
    '        console.error('Error sending resize message:', err);
    '    }
    '});
    '"
    '    WebView.CoreWebView2.ExecuteScriptAsync(script)
    'End Sub

    'Private Sub WebView21_WebMessageReceived(sender As Object, e As CoreWebView2WebMessageReceivedEventArgs) Handles WebView.WebMessageReceived
    '    Try
    '        Dim jsonMessage As String = e.TryGetWebMessageAsJson()
    '        If Not String.IsNullOrEmpty(jsonMessage) Then
    '            ' Process the JSON message here
    '            MessageBox.Show("Received JSON message: " & jsonMessage)
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show("Error processing WebView2 message: " & ex.Message)
    '    End Try
    'End Sub

End Class