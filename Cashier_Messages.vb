Option Strict On
Option Explicit On

Imports System.Drawing
Imports System.Windows.Forms
Imports Guna.UI2.WinForms

'=====================================================================
' CASHIER MESSAGES  -  NEW FILE (another part of the Cashier class)
' Same look as the Admin messages screen (ForestUi colours / bubbles), but the
' cashier can only talk to the ADMIN of his own shop: there is exactly one
' conversation, and the data comes from DataStore.ThreadMessages(MyChatName()),
' so another cashier's chat is never loaded.
' Cashier_Designer.vb is NOT changed: the existing controls (FlowLayoutPanel3, flpMessages,
' txtChat, btnSend, CashierName, Guna2HtmlLabel60) are re-used and re-parented.
'=====================================================================
Partial Public Class Cashier

    Private cmHeader As Panel
    Private cmChatHeader As Panel
    Private cmAvatarHost As Panel
    Private cmComposer As Panel
    Private cmEditBar As Panel
    Private cmRoleBadge As Guna2Panel
    Private cmBuilt As Boolean = False
    Private cmRendering As Boolean = False
    Private cmLastWidth As Integer = -1
    Private chatEditing As ChatMessage = Nothing

    Private ReadOnly cmBubbleFont As New Font("Segoe UI", 10.0F)
    Private ReadOnly cmMetaFont As New Font("Segoe UI", 8.0F)

    '=================================================================
    ' LAYOUT
    '=================================================================
    Private Sub BuildForestMessaging()

        Dim pg As Panel = pnl_CashierMessages
        pg.SuspendLayout()
        pg.AutoSize = False
        pg.BackColor = ForestUi.Page
        pg.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right

        ' old pieces out of the page (some of their children are re-used below)
        pg.Controls.Remove(FlowLayoutPanel3)
        pg.Controls.Remove(pnl_MainChat)
        pnl_MainChat.Visible = False
        flpMessagesdsds.Visible = False
        Guna2HtmlLabel60.Visible = False

        ' ---------- body: one big rounded card ----------
        Dim body As New Panel()
        body.Dock = DockStyle.Fill
        body.BackColor = ForestUi.Page
        body.Padding = New Padding(24)

        Dim shell As Guna2Panel = ForestUi.Card(100, 100, 12, ForestUi.CardFill, ForestUi.Border)
        shell.Dock = DockStyle.Fill
        shell.Padding = New Padding(2)
        body.Controls.Add(shell)

        Dim listPane As New Panel()
        listPane.Dock = DockStyle.Left
        listPane.Width = 300
        listPane.BackColor = ForestUi.CardFill

        Dim sep As New Panel()
        sep.Dock = DockStyle.Left
        sep.Width = 1
        sep.BackColor = ForestUi.Border

        Dim chatPane As New Panel()
        chatPane.Dock = DockStyle.Fill
        chatPane.BackColor = ForestUi.ChatBg

        shell.Controls.Add(chatPane)      ' Fill first, then the docked edges
        shell.Controls.Add(sep)
        shell.Controls.Add(listPane)

        ' ---------- left: the ONE conversation (Admin) ----------
        FlowLayoutPanel3.Dock = DockStyle.Fill
        FlowLayoutPanel3.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        FlowLayoutPanel3.FlowDirection = FlowDirection.TopDown
        FlowLayoutPanel3.WrapContents = False
        FlowLayoutPanel3.AutoScroll = False
        FlowLayoutPanel3.Padding = Padding.Empty
        FlowLayoutPanel3.Margin = Padding.Empty
        FlowLayoutPanel3.BorderStyle = BorderStyle.None
        FlowLayoutPanel3.BackColor = ForestUi.CardFill
        listPane.Controls.Add(FlowLayoutPanel3)

        Dim listTitleRow As New Panel()
        listTitleRow.Dock = DockStyle.Top
        listTitleRow.Height = 62
        listTitleRow.BackColor = ForestUi.CardFill
        Dim listTitle As Label = ForestUi.Lbl("Admin conversation", 10.5F, FontStyle.Bold, ForestUi.Ink, 22, 22)
        listTitleRow.Controls.Add(listTitle)
        listPane.Controls.Add(listTitleRow)

        ' ---------- right: chat ----------
        flpMessages.Dock = DockStyle.Fill
        flpMessages.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        flpMessages.BackColor = ForestUi.ChatBg
        flpMessages.BorderStyle = BorderStyle.None
        flpMessages.FlowDirection = FlowDirection.TopDown
        flpMessages.WrapContents = False
        flpMessages.AutoScroll = True
        flpMessages.Padding = New Padding(24, 12, 24, 12)
        chatPane.Controls.Add(flpMessages)

        ' composer (re-uses txtChat + btnSend)
        cmComposer = New Panel()
        cmComposer.Dock = DockStyle.Bottom
        cmComposer.Height = 76
        cmComposer.BackColor = ForestUi.CardFill
        Dim composerLine As New Panel()
        composerLine.Dock = DockStyle.Top
        composerLine.Height = 1
        composerLine.BackColor = ForestUi.Border
        cmComposer.Controls.Add(composerLine)

        ' "Editing message" bar (only while the cashier corrects one of his own messages)
        cmEditBar = New Panel()
        cmEditBar.Dock = DockStyle.Top
        cmEditBar.Height = 30
        cmEditBar.BackColor = ForestUi.AccentSoft
        cmEditBar.Visible = False
        cmEditBar.Controls.Add(ForestUi.Lbl("Editing message", 8.5F, FontStyle.Bold, ForestUi.Accent, 24, 7))
        Dim editCancel As Label = ForestUi.Lbl("Cancel", 8.5F, FontStyle.Underline, ForestUi.Muted, 140, 7)
        editCancel.Cursor = Cursors.Hand
        AddHandler editCancel.Click, Sub(o As Object, ev As EventArgs) CancelChatEdit()
        cmEditBar.Controls.Add(editCancel)
        cmComposer.Controls.Add(cmEditBar)

        txtChat.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        txtChat.BorderRadius = 20
        txtChat.BorderColor = ForestUi.Border
        txtChat.FillColor = ForestUi.CardFill
        txtChat.ForeColor = ForestUi.Ink
        txtChat.Font = New Font("Segoe UI", 10.0F)
        txtChat.PlaceholderText = "Write a message to the admin..."
        txtChat.PlaceholderForeColor = ForestUi.Muted
        txtChat.FocusedState.BorderColor = ForestUi.Accent
        txtChat.HoverState.BorderColor = ForestUi.Accent
        cmComposer.Controls.Add(txtChat)
        AddHandler txtChat.KeyDown, AddressOf TxtChat_KeyDown

        btnSend.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        btnSend.Animated = False
        btnSend.BorderRadius = 20
        btnSend.FillColor = ForestUi.Accent
        btnSend.ForeColor = Color.White
        btnSend.Font = New Font("Segoe MDL2 Assets", 12.0F)
        btnSend.HoverState.FillColor = ForestUi.AccentDark
        btnSend.Text = ChrW(&HE725)
        btnSend.Cursor = Cursors.Hand
        btnSend.Size = New Size(40, 40)
        cmComposer.Controls.Add(btnSend)
        AddHandler cmComposer.Resize, AddressOf CmComposer_Resize
        chatPane.Controls.Add(cmComposer)

        ' chat header (re-uses CashierName)
        cmChatHeader = New Panel()
        cmChatHeader.Dock = DockStyle.Top
        cmChatHeader.Height = 76
        cmChatHeader.BackColor = ForestUi.CardFill
        Dim headerLine As New Panel()
        headerLine.Dock = DockStyle.Bottom
        headerLine.Height = 1
        headerLine.BackColor = ForestUi.Border
        cmChatHeader.Controls.Add(headerLine)

        cmAvatarHost = New Panel()
        cmAvatarHost.Size = New Size(44, 44)
        cmAvatarHost.Location = New Point(24, 16)
        cmAvatarHost.BackColor = ForestUi.CardFill
        cmChatHeader.Controls.Add(cmAvatarHost)

        CashierName.AutoSize = True
        CashierName.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        CashierName.ForeColor = ForestUi.Ink
        CashierName.BackColor = Color.Transparent
        CashierName.Location = New Point(80, 14)
        cmChatHeader.Controls.Add(CashierName)

        cmRoleBadge = MakeAdminBadge("Admin")
        cmRoleBadge.Location = New Point(82, 46)
        cmChatHeader.Controls.Add(cmRoleBadge)
        chatPane.Controls.Add(cmChatHeader)

        pg.Controls.Add(body)

        ' ---------- page header band (the user chip + bell are placed on it by AttachTopBar) ----------
        cmHeader = New Panel()
        cmHeader.Dock = DockStyle.Top
        cmHeader.Height = 80
        cmHeader.BackColor = ForestUi.CardFill
        Dim hdrLine As New Panel()
        hdrLine.Dock = DockStyle.Bottom
        hdrLine.Height = 1
        hdrLine.BackColor = ForestUi.Border
        cmHeader.Controls.Add(hdrLine)
        pg.Controls.Add(cmHeader)

        pg.ResumeLayout(True)
        CmComposer_Resize(Nothing, EventArgs.Empty)

        cmBuilt = True
        cmLastWidth = flpMessages.ClientSize.Width
        AddHandler flpMessages.Resize, AddressOf CmChatArea_Resize
        RefreshCashierConvoList()
    End Sub

    Private Sub CmComposer_Resize(sender As Object, e As EventArgs)
        If cmComposer Is Nothing Then Return
        Dim w As Integer = cmComposer.ClientSize.Width
        Dim y0 As Integer = 18 + If(cmEditBar IsNot Nothing AndAlso cmEditBar.Visible, cmEditBar.Height, 0)
        btnSend.Location = New Point(w - 20 - btnSend.Width, y0)
        txtChat.Location = New Point(20, y0)
        txtChat.Size = New Size(Math.Max(100, btnSend.Left - 12 - txtChat.Left), 40)
    End Sub

    ' small dark pill, e.g. "Admin"
    Private Function MakeAdminBadge(text As String) As Guna2Panel
        Dim f As New Font("Segoe UI", 7.0F, FontStyle.Bold)
        Dim w As Integer = TextRenderer.MeasureText(text, f).Width + 14
        Dim b As Guna2Panel = ForestUi.Card(w, 17, 8, ForestUi.Ink)
        Dim l As New Label()
        l.Dock = DockStyle.Fill
        l.BackColor = Color.Transparent
        l.ForeColor = Color.White
        l.Font = f
        l.TextAlign = ContentAlignment.MiddleCenter
        l.UseMnemonic = False
        l.Text = text
        b.Controls.Add(l)
        Return b
    End Function

    '=================================================================
    ' THE ONE CONVERSATION
    '=================================================================
    ' the manager of this cashier's shop (old messages only say "Admin")
    Private Function CmAdminName() As String
        Dim m As AdminAccount = DataStore.GetShopManager(ShopContext.CurrentShopId)
        If m IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(m.FullName) Then Return m.FullName.Trim()
        Return "Admin"
    End Function

    Private Sub RefreshCashierConvoList()
        If Not cmBuilt Then Return
        Dim adminName As String = CmAdminName()
        Dim last As ChatMessage = DataStore.ThreadLast(MyChatName())

        FlowLayoutPanel3.SuspendLayout()
        For i As Integer = FlowLayoutPanel3.Controls.Count - 1 To 0 Step -1
            Dim c As Control = FlowLayoutPanel3.Controls(i)
            FlowLayoutPanel3.Controls.RemoveAt(i)
            c.Dispose()
        Next

        Dim w As Integer = FlowLayoutPanel3.ClientSize.Width
        If w < 100 Then w = 299
        Dim h As Integer = 76

        Dim sectionLbl As Label = ForestUi.Lbl("ADMIN", 7.5F, FontStyle.Bold, ForestUi.Muted, 0, 0)
        sectionLbl.AutoSize = False
        sectionLbl.Size = New Size(w, 30)
        sectionLbl.Padding = New Padding(22, 0, 0, 6)
        sectionLbl.TextAlign = ContentAlignment.BottomLeft
        sectionLbl.Margin = Padding.Empty
        FlowLayoutPanel3.Controls.Add(sectionLbl)

        Dim p As New Panel()
        p.Size = New Size(w, h)
        p.Margin = Padding.Empty
        p.BackColor = ForestUi.AccentSoft          ' always the open conversation

        Dim av As Control = ForestUi.Avatar(ForestUi.Initials(adminName), 40, ForestUi.AvatarGray, ForestUi.Ink)
        av.Location = New Point(16, 18)
        p.Controls.Add(av)

        Dim timeW As Integer = 0
        If last IsNot Nothing Then
            Dim tm As Label = ForestUi.Lbl(ForestUi.TimeAgo(last.TimeSent), 7.5F, FontStyle.Regular, ForestUi.Muted)
            tm.Location = New Point(w - tm.Width - 16, 15)
            timeW = tm.Width + 6
            p.Controls.Add(tm)
        End If

        Dim nameLbl As Label = ForestUi.Lbl(adminName, 9.5F, FontStyle.Bold, ForestUi.Ink, 68, 14)
        nameLbl.AutoSize = False
        nameLbl.AutoEllipsis = True
        nameLbl.Size = New Size(Math.Max(40, w - 68 - 16 - timeW), 20)
        p.Controls.Add(nameLbl)

        Dim badge As Guna2Panel = MakeAdminBadge("Admin")
        badge.Location = New Point(68, 42)
        p.Controls.Add(badge)

        Dim previewX As Integer = badge.Right + 6
        Dim preview As Label = ForestUi.Lbl(If(last IsNot Nothing, last.Message, "No messages yet"), 8.5F, FontStyle.Regular, ForestUi.Muted, previewX, 41)
        preview.AutoSize = False
        preview.AutoEllipsis = True
        preview.Size = New Size(Math.Max(30, w - previewX - 16), 20)
        p.Controls.Add(preview)

        FlowLayoutPanel3.Controls.Add(p)
        FlowLayoutPanel3.ResumeLayout(True)

        ' chat header follows the admin
        CashierName.Text = adminName
        cmAvatarHost.Controls.Clear()
        cmAvatarHost.Controls.Add(ForestUi.Avatar(ForestUi.Initials(adminName), 44, ForestUi.AccentSoft, ForestUi.Accent))
        Dim firstName As String = If(adminName.Contains(" "), adminName.Substring(0, adminName.IndexOf(" "c)), adminName)
        If chatEditing Is Nothing Then txtChat.PlaceholderText = "Write a message to " & firstName & "..."
    End Sub

    '=================================================================
    ' CHAT BUBBLES  (replaces the old LoadCashierChat body)
    '=================================================================
    Private Sub CmChatArea_Resize(sender As Object, e As EventArgs)
        Dim w As Integer = flpMessages.ClientSize.Width
        If w = cmLastWidth OrElse w <= 0 Then Return
        cmLastWidth = w
        RenderCashierChat()
    End Sub

    Private Sub RenderCashierChat()
        If Not cmBuilt OrElse cmRendering Then Return
        cmRendering = True
        Try
            flpMessages.SuspendLayout()
            For i As Integer = flpMessages.Controls.Count - 1 To 0 Step -1
                Dim c As Control = flpMessages.Controls(i)
                flpMessages.Controls.RemoveAt(i)
                c.Dispose()
            Next

            Dim areaW As Integer = flpMessages.ClientSize.Width - flpMessages.Padding.Horizontal - SystemInformation.VerticalScrollBarWidth
            If areaW < 240 Then areaW = 240
            Dim maxBubble As Integer = Math.Min(560, CInt(areaW * 0.7))

            ' only THIS cashier's conversation with the admin
            Dim thread As List(Of ChatMessage) = DataStore.ThreadMessages(MyChatName())

            If thread.Count = 0 Then
                Dim hint As Label = ForestUi.Lbl("No messages yet. Say hello to the admin!", 9.5F, FontStyle.Regular, ForestUi.Muted)
                hint.AutoSize = False
                hint.Size = New Size(areaW, 60)
                hint.TextAlign = ContentAlignment.MiddleCenter
                flpMessages.Controls.Add(hint)
            End If

            Dim lastDate As DateTime = DateTime.MinValue
            For Each chat As ChatMessage In thread
                If chat.TimeSent.Date <> lastDate Then
                    lastDate = chat.TimeSent.Date
                    Dim dateText As String = If(lastDate = DateTime.Today, "TODAY", lastDate.ToString("dddd").ToUpper()) &
                                             " " & ChrW(&HB7) & " " & lastDate.ToString("MMMM d").ToUpper()
                    Dim divider As Label = ForestUi.Lbl(dateText, 8.0F, FontStyle.Regular, ForestUi.Muted)
                    divider.AutoSize = False
                    divider.Size = New Size(areaW, 34)
                    divider.TextAlign = ContentAlignment.MiddleCenter
                    divider.Margin = New Padding(0, 4, 0, 2)
                    flpMessages.Controls.Add(divider)
                End If
                flpMessages.Controls.Add(CmBuildBubble(chat, areaW, maxBubble))
            Next

            flpMessages.ResumeLayout(True)
            If flpMessages.Controls.Count > 0 Then
                flpMessages.ScrollControlIntoView(flpMessages.Controls(flpMessages.Controls.Count - 1))
            End If

            ' looking at the conversation = reading it (the admin then sees "Read")
            If pnl_CashierMessages.Visible Then DataStore.MarkThreadRead(False, MyChatName())
            RefreshCashierConvoList()
        Finally
            cmRendering = False
        End Try
    End Sub

    Private Function CmBuildBubble(chat As ChatMessage, areaW As Integer, maxBubble As Integer) As Guna2Panel

        Dim mine As Boolean = DataStore.IsFromCashier(chat)        ' written by this cashier

        Const padX As Integer = 16
        Const padY As Integer = 12

        Dim flags As TextFormatFlags = TextFormatFlags.WordBreak Or TextFormatFlags.NoPadding
        Dim txt As String = If(chat.Message, "")
        Dim txtSize As Size = TextRenderer.MeasureText(txt, cmBubbleFont, New Size(maxBubble - 2 * padX, 0), flags)

        Dim metaText As String = chat.TimeSent.ToString("hh:mm tt")
        If mine Then
            Dim readAt As DateTime = DataStore.AdminReadAt(MyChatName())
            metaText &= " " & ChrW(&HB7) & " " & If(readAt >= chat.TimeSent, "Read", "Sent")
        End If
        If DataStore.MessageEditedAt(chat) <> DateTime.MinValue Then metaText &= " " & ChrW(&HB7) & " Edited"
        Dim metaSize As Size = TextRenderer.MeasureText(metaText, cmMetaFont)

        Dim canEdit As Boolean = mine AndAlso DataStore.CanEditMessage(chat, False, MyChatName())
        Dim editW As Integer = If(canEdit, TextRenderer.MeasureText("Edit", cmMetaFont).Width + 14, 0)

        Dim contentW As Integer = Math.Max(txtSize.Width, metaSize.Width + editW) + 6
        Dim bubbleW As Integer = contentW + 2 * padX
        Dim bubbleH As Integer = padY + txtSize.Height + 8 + metaSize.Height + padY

        Dim bubble As Guna2Panel = If(mine,
                                      ForestUi.Card(bubbleW, bubbleH, 12, ForestUi.Bubble),
                                      ForestUi.Card(bubbleW, bubbleH, 12, ForestUi.CardFill, Color.FromArgb(238, 242, 246)))

        Dim msgLbl As New Label()
        msgLbl.AutoSize = False
        msgLbl.UseMnemonic = False
        msgLbl.BackColor = Color.Transparent
        msgLbl.ForeColor = If(mine, Color.White, ForestUi.Ink)
        msgLbl.Font = cmBubbleFont
        msgLbl.Text = txt
        msgLbl.Location = New Point(padX, padY)
        msgLbl.Size = New Size(contentW, txtSize.Height + 2)
        bubble.Controls.Add(msgLbl)

        Dim metaLbl As New Label()
        metaLbl.AutoSize = True
        metaLbl.BackColor = Color.Transparent
        metaLbl.ForeColor = If(mine, Color.FromArgb(190, 165, 145), Color.FromArgb(150, 160, 172))
        metaLbl.Font = cmMetaFont
        metaLbl.Text = metaText
        metaLbl.Location = New Point(padX, padY + txtSize.Height + 8)
        bubble.Controls.Add(metaLbl)

        If canEdit Then
            Dim editLbl As New Label()
            editLbl.AutoSize = True
            editLbl.BackColor = Color.Transparent
            editLbl.ForeColor = Color.FromArgb(240, 200, 165)
            editLbl.Font = New Font("Segoe UI", 8.0F, FontStyle.Underline)
            editLbl.Cursor = Cursors.Hand
            editLbl.UseMnemonic = False
            editLbl.Text = "Edit"
            editLbl.Location = New Point(bubbleW - padX - TextRenderer.MeasureText("Edit", cmMetaFont).Width, padY + txtSize.Height + 8)
            AddHandler editLbl.Click, Sub(o As Object, ev As EventArgs) BeginChatEdit(chat)
            bubble.Controls.Add(editLbl)
        End If

        ' sent = right side, received = left side
        bubble.Margin = New Padding(If(mine, Math.Max(0, areaW - bubbleW), 0), 8, 0, 8)
        Return bubble
    End Function

    '=================================================================
    ' CORRECTING A MESSAGE THE CASHIER ALREADY SENT
    '=================================================================
    Private Sub BeginChatEdit(chat As ChatMessage)
        chatEditing = chat
        txtChat.Text = chat.Message
        txtChat.SelectionStart = txtChat.Text.Length
        cmEditBar.Visible = True
        cmComposer.Height = 76 + cmEditBar.Height
        CmComposer_Resize(Nothing, EventArgs.Empty)
        txtChat.Focus()
    End Sub

    Private Sub CancelChatEdit()
        chatEditing = Nothing
        txtChat.Clear()
        If cmEditBar IsNot Nothing Then cmEditBar.Visible = False
        If cmComposer IsNot Nothing Then
            cmComposer.Height = 76
            CmComposer_Resize(Nothing, EventArgs.Empty)
        End If
    End Sub

    Private Sub TxtChat_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Escape AndAlso chatEditing IsNot Nothing Then
            e.SuppressKeyPress = True
            CancelChatEdit()
        ElseIf e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            btnSend.PerformClick()
        End If
    End Sub

End Class
