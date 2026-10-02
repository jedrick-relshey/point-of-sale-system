Option Strict On
Option Explicit On

Imports System.Drawing
Imports Guna.UI2.WinForms

Public Module CafeUi

    Public ReadOnly Property Coffee As Color
        Get
            Return Color.FromArgb(62, 39, 35)
        End Get
    End Property

    Public ReadOnly Property Cream As Color
        Get
            Return Color.FromArgb(241, 236, 233)
        End Get
    End Property

    Public ReadOnly Property Sand As Color
        Get
            Return Color.FromArgb(216, 203, 199)
        End Get
    End Property

    Public Sub StyleMessaging(root As Panel,
                              sidebar As FlowLayoutPanel,
                              chatPanel As Panel,
                              messageList As FlowLayoutPanel,
                              titleLabel As Guna2HtmlLabel,
                              detailLabel As Guna2HtmlLabel,
                              messageInput As Guna2TextBox,
                              sendButton As Guna2Button,
                              conversationTitle As String,
                              pageTitle As String,
                              pageSubtitle As String)

        root.BackColor = Cream
        root.AutoScroll = False

        Dim pageHeading As New Label With {
            .Name = "lblMessagePageHeading", .Text = pageTitle, .AutoSize = True,
            .Font = New Font("Segoe UI", 18.0F, FontStyle.Bold), .ForeColor = Coffee,
            .Location = New Point(28, 17), .BackColor = Color.Transparent
        }
        Dim pageDescription As New Label With {
            .Name = "lblMessagePageDescription", .Text = pageSubtitle, .AutoSize = True,
            .Font = New Font("Segoe UI", 9.5F), .ForeColor = Color.FromArgb(105, 79, 70),
            .Location = New Point(30, 51), .BackColor = Color.Transparent
        }
        For Each old As Control In root.Controls.OfType(Of Control)().Where(Function(c) c.Name = pageHeading.Name OrElse c.Name = pageDescription.Name).ToArray()
            root.Controls.Remove(old)
        Next
        root.Controls.Add(pageHeading)
        root.Controls.Add(pageDescription)
        pageHeading.BringToFront()
        pageDescription.BringToFront()

        sidebar.Location = New Point(28, 88)
        sidebar.Size = New Size(348, Math.Max(420, root.ClientSize.Height - 104))
        sidebar.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        sidebar.BackColor = Color.White
        sidebar.BorderStyle = BorderStyle.FixedSingle
        sidebar.FlowDirection = FlowDirection.TopDown
        sidebar.WrapContents = False
        sidebar.Padding = Padding.Empty
        sidebar.Controls.Clear()

        Dim activeHeader As New Label With {
            .Text = "Active Chats", .Font = New Font("Segoe UI", 10.0F, FontStyle.Bold),
            .ForeColor = Coffee, .BackColor = Sand, .TextAlign = ContentAlignment.MiddleLeft,
            .Padding = New Padding(14, 0, 0, 0), .Size = New Size(346, 43), .Margin = Padding.Empty
        }
        Dim conversation As New Panel With {
            .BackColor = Sand, .Size = New Size(346, 104), .Margin = Padding.Empty
        }
        Dim person As New Label With {
            .Text = conversationTitle, .Font = New Font("Segoe UI", 10.0F, FontStyle.Bold),
            .ForeColor = Coffee, .AutoSize = True, .Location = New Point(14, 13)
        }
        Dim preview As New Label With {
            .Text = "Store team conversation", .Font = New Font("Segoe UI", 9.0F),
            .ForeColor = Color.FromArgb(105, 79, 70), .AutoSize = True, .Location = New Point(14, 42)
        }
        Dim topic As New Label With {
            .Text = "TEAM CHAT", .Font = New Font("Segoe UI", 8.0F, FontStyle.Bold),
            .ForeColor = Color.White, .BackColor = Color.FromArgb(181, 111, 0),
            .AutoSize = True, .Padding = New Padding(7, 4, 7, 4), .Location = New Point(14, 67)
        }
        conversation.Controls.Add(person)
        conversation.Controls.Add(preview)
        conversation.Controls.Add(topic)
        sidebar.Controls.Add(activeHeader)
        sidebar.Controls.Add(conversation)

        chatPanel.Location = New Point(396, 88)
        chatPanel.Size = New Size(Math.Max(500, root.ClientSize.Width - 419), Math.Max(420, root.ClientSize.Height - 104))
        chatPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        chatPanel.BackColor = Color.White
        chatPanel.BorderStyle = BorderStyle.FixedSingle

        titleLabel.Text = conversationTitle
        titleLabel.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
        titleLabel.ForeColor = Coffee
        titleLabel.Location = New Point(14, 10)
        titleLabel.Size = New Size(chatPanel.Width - 180, 25)

        detailLabel.Text = "Cashier Status: Online  •  Terminal #01"
        detailLabel.Font = New Font("Segoe UI", 9.0F)
        detailLabel.ForeColor = Color.FromArgb(105, 79, 70)
        detailLabel.Location = New Point(15, 37)
        detailLabel.Size = New Size(chatPanel.Width - 30, 18)

        messageList.Location = New Point(1, 64)
        messageList.Size = New Size(chatPanel.ClientSize.Width - 2, chatPanel.ClientSize.Height - 137)
        messageList.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        messageList.AutoScroll = True
        messageList.BackColor = Color.White
        messageList.FlowDirection = FlowDirection.TopDown
        messageList.WrapContents = False
        messageList.Padding = New Padding(10, 10, 10, 4)

        messageInput.Location = New Point(14, chatPanel.ClientSize.Height - 56)
        messageInput.Size = New Size(chatPanel.ClientSize.Width - 125, 40)
        messageInput.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        messageInput.BorderRadius = 5
        messageInput.BorderColor = Color.FromArgb(190, 164, 154)
        messageInput.FillColor = Color.White
        messageInput.ForeColor = Coffee
        messageInput.PlaceholderForeColor = Color.FromArgb(125, 100, 90)
        messageInput.PlaceholderText = "Type a message..."

        sendButton.Location = New Point(chatPanel.ClientSize.Width - 110, chatPanel.ClientSize.Height - 56)
        sendButton.Size = New Size(96, 40)
        sendButton.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        sendButton.FillColor = Coffee
        sendButton.ForeColor = Color.White
        sendButton.BorderRadius = 5
        sendButton.Text = "Send"
        sendButton.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        sendButton.BringToFront()
    End Sub

    Public Sub StyleHistoryGrid(grid As DataGridView)
        grid.AllowUserToAddRows = False
        grid.AllowUserToDeleteRows = False
        grid.AllowUserToResizeRows = False
        grid.ReadOnly = True
        grid.RowHeadersVisible = False
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        grid.MultiSelect = False
        grid.BackgroundColor = Color.White
        grid.BorderStyle = BorderStyle.None
        grid.GridColor = Color.FromArgb(225, 215, 211)
        grid.EnableHeadersVisualStyles = False
        grid.ColumnHeadersHeight = 42
        grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        grid.RowTemplate.Height = 40
        grid.ColumnHeadersDefaultCellStyle.BackColor = Sand
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Coffee
        grid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Sand
        grid.DefaultCellStyle.BackColor = Color.White
        grid.DefaultCellStyle.ForeColor = Color.FromArgb(83, 60, 53)
        grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(246, 240, 237)
        grid.DefaultCellStyle.SelectionForeColor = Coffee
        grid.DefaultCellStyle.Font = New Font("Segoe UI", 9.5F)
        grid.AlternatingRowsDefaultCellStyle.BackColor = Color.White
    End Sub

End Module
