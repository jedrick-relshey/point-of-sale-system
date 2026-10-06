Option Strict On
Option Explicit On

Imports System.Drawing
Imports System.Windows.Forms
Imports Guna.UI2.WinForms

' Admin.Counterly.vb  -  NEW FILE (second half of the Admin class).
' Restyles the sidebar and rebuilds the Messages page to match the Counterly design.
' Admin_Designer.vb is NOT changed: the page is laid out in code and re-uses your
' existing controls (flpAdminMessages, txtAdminChat, btnAdmin, CashierName,
' Guna2HtmlLabel60, FlowLayoutPanel3), so all existing event handlers keep working.
Partial Public Class Admin

    ' ---- sidebar ----
    Private navButtons As New List(Of Guna2Button)
    Private navFontOn As New Font("Segoe UI", 10.5F, FontStyle.Bold)
    Private navFontOff As New Font("Segoe UI", 10.5F, FontStyle.Regular)
    Private navBadge As Guna2Panel
    Private navBadgeText As Label
    Private seenMessageCount As Integer = 0

    ' ---- messages page ----
    Private hdrPanel As Panel
    Private hdrLogout As Guna2Button
    Private hdrBell As Guna2Button
    Private hdrAvatar As Guna2Panel
    Private hdrUserName As Label
    Private hdrUserRole As Label
    Private hdrDivider As Panel

    Private listPane As Panel
    Private listTitleRow As Panel
    Private listCompose As Guna2Button
    Private listSearchWrap As Panel
    Private listSearchBox As Guna2Panel
    Private convoSearch As Guna2TextBox
    Private selectedConvo As String = ""

    Private chatHeader As Panel
    Private chatAvatarHost As Panel
    Private chatStatusDot As Guna2Panel
    Private chatCall As Guna2Button
    Private chatMore As Guna2Button
    Private composer As Panel

    Private ReadOnly bubbleFont As New Font("Segoe UI", 10.0F)
    Private ReadOnly metaFont As New Font("Segoe UI", 8.0F)
    Private lastChatWidth As Integer = -1

    '=================================================================
    ' SIDEBAR
    '=================================================================
    Private Sub StyleSidebar()

        Navigation.BackColor = Counterly.Navy

        ' remove the old cafe-style bits
        Guna2HtmlLabel40.Visible = False
        Guna2CustomGradientPanel2.Visible = False

        ' logo tile + app name
        Dim logo As Guna2Panel = Counterly.Card(40, 40, 10, Counterly.Teal)
        logo.Location = New Point(14, 16)
        Dim logoText As New Label()
        logoText.Dock = DockStyle.Fill
        logoText.BackColor = Color.Transparent
        logoText.ForeColor = Color.White
        logoText.Font = New Font("Segoe UI", 15.0F, FontStyle.Bold)
        logoText.TextAlign = ContentAlignment.MiddleCenter
        logoText.Text = Counterly.AppName.Substring(0, 1)
        logo.Controls.Add(logoText)
        Navigation.Controls.Add(logo)

        Guna2HtmlLabel26.Text = Counterly.AppName
        Guna2HtmlLabel26.Font = New Font("Segoe UI", 14.0F, FontStyle.Regular)
        Guna2HtmlLabel26.ForeColor = Color.White
        Guna2HtmlLabel26.Location = New Point(62, 14)

        Dim sub1 As Label = Counterly.Lbl("RETAIL OS", 7.5F, FontStyle.Regular, Counterly.NavMuted, 63, 38)
        Navigation.Controls.Add(sub1)

        Guna2HtmlLabel4.Text = "ADMIN WORKSPACE"
        Guna2HtmlLabel4.Font = New Font("Segoe UI", 7.5F, FontStyle.Bold)
        Guna2HtmlLabel4.ForeColor = Counterly.NavMuted
        Guna2HtmlLabel4.Location = New Point(16, 82)

        ' menu buttons, in the order shown in the design
        btn_Product.Text = "Products"
        btnHistory.Text = "Transactions"

        navButtons.Clear()
        navButtons.AddRange(New Guna2Button() {btn_dashboardAdmin, btnInventory, btn_Product,
                                               btnHistory, btnCashiers, btnMessages})
        Dim y As Integer = 108
        For Each b As Guna2Button In navButtons
            b.Animated = False
            b.BorderRadius = 10
            b.Size = New Size(175, 42)
            b.Location = New Point(12, y)
            b.FillColor = Color.Transparent
            b.ForeColor = Counterly.NavText
            b.Font = navFontOff
            b.HoverState.FillColor = Counterly.NavHover
            b.HoverState.ForeColor = Color.White
            b.Cursor = Cursors.Hand
            y += 48
        Next

        ' unread badge on the Messages item
        navBadge = Counterly.Card(22, 22, 11, Counterly.Teal)
        navBadgeText = New Label()
        navBadgeText.Dock = DockStyle.Fill
        navBadgeText.BackColor = Color.Transparent
        navBadgeText.ForeColor = Color.White
        navBadgeText.Font = New Font("Segoe UI", 8.0F, FontStyle.Bold)
        navBadgeText.TextAlign = ContentAlignment.MiddleCenter
        navBadge.Controls.Add(navBadgeText)
        navBadge.Location = New Point(btnMessages.Right - 36, btnMessages.Top + 10)
        navBadge.Visible = False
        Navigation.Controls.Add(navBadge)
        navBadge.BringToFront()

        ' "Need help?" card
        Dim help As Guna2Panel = Counterly.Card(175, 104, 12, Counterly.NavCard)
        help.Location = New Point(12, 420)
        Dim helpIcon As New Label()
        helpIcon.AutoSize = True
        helpIcon.BackColor = Color.Transparent
        helpIcon.ForeColor = Counterly.Teal
        helpIcon.Font = New Font("Segoe MDL2 Assets", 14.0F)
        helpIcon.Text = Counterly.GlyphHelp
        helpIcon.Location = New Point(12, 10)
        help.Controls.Add(helpIcon)
        help.Controls.Add(Counterly.Lbl("Need help?", 10.0F, FontStyle.Bold, Color.White, 12, 38))
        Dim helpText As Label = Counterly.Lbl("Visit the operations guide or contact support.", 8.0F,
                                              FontStyle.Regular, Counterly.NavMuted, 12, 60)
        helpText.AutoSize = False
        helpText.Size = New Size(152, 36)
        help.Controls.Add(helpText)
        Navigation.Controls.Add(help)

        ' store status
        Dim dot As Guna2Panel = Counterly.Card(8, 8, 4, Counterly.Teal)
        dot.Location = New Point(20, 548)
        Navigation.Controls.Add(dot)
        Navigation.Controls.Add(Counterly.Lbl(Counterly.StoreName & " " & ChrW(&HB7) & " Open", 8.5F,
                                              FontStyle.Regular, Counterly.NavText, 34, 543))

        ' logout (same button, new look)
        btnAdminLogout.Animated = False
        btnAdminLogout.BorderRadius = 10
        btnAdminLogout.Size = New Size(175, 42)
        btnAdminLogout.Location = New Point(12, 587)
        btnAdminLogout.FillColor = Color.Transparent
        btnAdminLogout.ForeColor = Color.FromArgb(240, 140, 140)
        btnAdminLogout.Font = navFontOff
        btnAdminLogout.HoverState.FillColor = Counterly.NavHover
        btnAdminLogout.HoverState.ForeColor = Color.White

        HighlightNav(pnl_dashboard_system)
    End Sub

    ''' <summary>Highlights the sidebar item that belongs to the visible page.</summary>
    Private Sub HighlightNav(page As Panel)
        Dim active As Guna2Button = Nothing
        If page Is pnl_dashboard_system Then
            active = btn_dashboardAdmin
        ElseIf page Is pnl_Inventory Then
            active = btnInventory
        ElseIf page Is pnl_Products Then
            active = btn_Product
        ElseIf page Is pnl_History Then
            active = btnHistory
        ElseIf page Is pnlCashiers Then
            active = btnCashiers
        ElseIf page Is pnl_Messages Then
            active = btnMessages
        End If

        For Each b As Guna2Button In navButtons
            Dim isActive As Boolean = (b Is active)
            b.FillColor = If(isActive, Counterly.NavActive, Color.Transparent)
            b.HoverState.FillColor = If(isActive, Counterly.NavActive, Counterly.NavHover)
            b.ForeColor = If(isActive, Color.White, Counterly.NavText)
            b.Font = If(isActive, navFontOn, navFontOff)
        Next
    End Sub

    Private Sub UpdateMessageBadge()
        If navBadge Is Nothing Then Return

        Dim all As List(Of ChatMessage) = DataStore.ChatMessages.ToList()
        If pnl_Messages.Visible Then seenMessageCount = all.Count

        Dim unread As Integer = 0
        For i As Integer = seenMessageCount To all.Count - 1
            If Not String.Equals(all(i).Sender, "Admin", StringComparison.OrdinalIgnoreCase) Then unread += 1
        Next

        navBadge.Visible = unread > 0
        navBadgeText.Text = unread.ToString()
    End Sub

    '=================================================================
    ' MESSAGES PAGE LAYOUT
    '=================================================================
    Private Sub BuildMessagingLayout()

        Dim pg As Panel = pnl_Messages
        pg.SuspendLayout()
        pg.BackColor = Counterly.Page
        pg.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right

        ' take the old pieces out of the page; some are re-used below
        Label5.Visible = False
        pg.Controls.Remove(Label5)
        pg.Controls.Remove(pnl_MainChat)
        pg.Controls.Remove(FlowLayoutPanel3)
        pnl_MainChat.Visible = False

        ' ---------- body: one big rounded card ----------
        Dim body As New Panel()
        body.Dock = DockStyle.Fill
        body.BackColor = Counterly.Page
        body.Padding = New Padding(24)

        Dim shell As Guna2Panel = Counterly.Card(100, 100, 12, Color.White, Counterly.Border)
        shell.Dock = DockStyle.Fill
        shell.Padding = New Padding(2)
        body.Controls.Add(shell)

        listPane = New Panel()
        listPane.Dock = DockStyle.Left
        listPane.Width = 300
        listPane.BackColor = Color.White

        Dim sep As New Panel()
        sep.Dock = DockStyle.Left
        sep.Width = 1
        sep.BackColor = Counterly.Border

        Dim chatPane As New Panel()
        chatPane.Dock = DockStyle.Fill
        chatPane.BackColor = Counterly.ChatBg

        shell.Controls.Add(chatPane)   ' Fill goes first, then the docked edges
        shell.Controls.Add(sep)
        shell.Controls.Add(listPane)

        ' ---------- left: team conversations ----------
        FlowLayoutPanel3.Dock = DockStyle.Fill
        FlowLayoutPanel3.FlowDirection = FlowDirection.TopDown
        FlowLayoutPanel3.WrapContents = False
        FlowLayoutPanel3.AutoScroll = True
        FlowLayoutPanel3.Padding = Padding.Empty
        FlowLayoutPanel3.Margin = Padding.Empty
        FlowLayoutPanel3.BackColor = Color.White
        FlowLayoutPanel3.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        listPane.Controls.Add(FlowLayoutPanel3)

        listSearchWrap = New Panel()
        listSearchWrap.Dock = DockStyle.Top
        listSearchWrap.Height = 66
        listSearchWrap.BackColor = Color.White

        listSearchBox = Counterly.Card(260, 44, 10, Color.White, Counterly.Border)
        listSearchBox.Location = New Point(20, 4)
        Dim searchIcon As New Label()
        searchIcon.AutoSize = True
        searchIcon.BackColor = Color.Transparent
        searchIcon.ForeColor = Counterly.Muted
        searchIcon.Font = New Font("Segoe MDL2 Assets", 11.0F)
        searchIcon.Text = Counterly.GlyphSearch
        searchIcon.Location = New Point(14, 13)
        listSearchBox.Controls.Add(searchIcon)

        convoSearch = New Guna2TextBox()
        convoSearch.BorderThickness = 0
        convoSearch.BorderRadius = 0
        convoSearch.FillColor = Color.White
        convoSearch.Font = New Font("Segoe UI", 10.0F)
        convoSearch.ForeColor = Counterly.Ink
        convoSearch.PlaceholderText = "Search team members"
        convoSearch.PlaceholderForeColor = Counterly.Muted
        convoSearch.Location = New Point(42, 6)
        convoSearch.Size = New Size(205, 32)
        listSearchBox.Controls.Add(convoSearch)
        AddHandler convoSearch.TextChanged, AddressOf ConvoSearch_TextChanged
        listSearchWrap.Controls.Add(listSearchBox)
        listPane.Controls.Add(listSearchWrap)

        listTitleRow = New Panel()
        listTitleRow.Dock = DockStyle.Top
        listTitleRow.Height = 72
        listTitleRow.BackColor = Color.White
        listTitleRow.Controls.Add(Counterly.Lbl("Team conversations", 12.0F, FontStyle.Regular, Counterly.Ink, 22, 24))
        listCompose = Counterly.GlyphButton(Counterly.GlyphEdit, 36, Counterly.TealSoft, Counterly.Teal)
        listCompose.Location = New Point(244, 18)
        listTitleRow.Controls.Add(listCompose)
        AddHandler listCompose.Click, AddressOf ListCompose_Click
        listPane.Controls.Add(listTitleRow)

        ' ---------- right: chat ----------
        ' messages area (re-used control)
        flpAdminMessages.Dock = DockStyle.Fill
        flpAdminMessages.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        flpAdminMessages.BackColor = Counterly.ChatBg
        flpAdminMessages.FlowDirection = FlowDirection.TopDown
        flpAdminMessages.WrapContents = False
        flpAdminMessages.AutoScroll = True
        flpAdminMessages.Padding = New Padding(24, 12, 24, 12)
        chatPane.Controls.Add(flpAdminMessages)

        ' composer (re-uses txtAdminChat + btnAdmin)
        composer = New Panel()
        composer.Dock = DockStyle.Bottom
        composer.Height = 76
        composer.BackColor = Color.White
        Dim composerLine As New Panel()
        composerLine.Dock = DockStyle.Top
        composerLine.Height = 1
        composerLine.BackColor = Counterly.Border
        composer.Controls.Add(composerLine)

        txtAdminChat.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        txtAdminChat.BorderRadius = 10
        txtAdminChat.BorderColor = Counterly.Border
        txtAdminChat.FillColor = Counterly.Page
        txtAdminChat.ForeColor = Counterly.Ink
        txtAdminChat.Font = New Font("Segoe UI", 10.0F)
        txtAdminChat.PlaceholderText = "Type a message..."
        txtAdminChat.PlaceholderForeColor = Counterly.Muted
        txtAdminChat.FocusedState.BorderColor = Counterly.Teal
        txtAdminChat.HoverState.BorderColor = Counterly.Teal
        composer.Controls.Add(txtAdminChat)
        AddHandler txtAdminChat.KeyDown, AddressOf TxtAdminChat_KeyDown

        btnAdmin.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        btnAdmin.Animated = False
        btnAdmin.BorderRadius = 10
        btnAdmin.FillColor = Counterly.Teal
        btnAdmin.ForeColor = Color.White
        btnAdmin.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnAdmin.HoverState.FillColor = Counterly.TealDark
        btnAdmin.Text = "Send"
        btnAdmin.Cursor = Cursors.Hand
        btnAdmin.Size = New Size(96, 44)
        composer.Controls.Add(btnAdmin)
        AddHandler composer.Resize, AddressOf Composer_Resize
        chatPane.Controls.Add(composer)

        ' chat header (re-uses CashierName + Guna2HtmlLabel60)
        chatHeader = New Panel()
        chatHeader.Dock = DockStyle.Top
        chatHeader.Height = 76
        chatHeader.BackColor = Color.White
        Dim headerLine As New Panel()
        headerLine.Dock = DockStyle.Bottom
        headerLine.Height = 1
        headerLine.BackColor = Counterly.Border
        chatHeader.Controls.Add(headerLine)

        chatAvatarHost = New Panel()
        chatAvatarHost.Size = New Size(44, 44)
        chatAvatarHost.Location = New Point(24, 16)
        chatAvatarHost.BackColor = Color.White
        chatHeader.Controls.Add(chatAvatarHost)

        CashierName.Font = New Font("Segoe UI", 11.0F, FontStyle.Regular)
        CashierName.ForeColor = Counterly.Ink
        CashierName.BackColor = Color.Transparent
        CashierName.Location = New Point(80, 16)
        chatHeader.Controls.Add(CashierName)

        chatStatusDot = Counterly.Card(8, 8, 4, Counterly.Teal)
        chatStatusDot.Location = New Point(82, 46)
        chatHeader.Controls.Add(chatStatusDot)

        Guna2HtmlLabel60.Text = "Cashier terminal"
        Guna2HtmlLabel60.Font = New Font("Segoe UI", 8.5F, FontStyle.Regular)
        Guna2HtmlLabel60.ForeColor = Counterly.Muted
        Guna2HtmlLabel60.BackColor = Color.Transparent
        Guna2HtmlLabel60.Location = New Point(96, 41)
        chatHeader.Controls.Add(Guna2HtmlLabel60)

        chatCall = Counterly.GlyphButton(Counterly.GlyphPhone, 36, Counterly.Page, Counterly.Muted)
        chatMore = Counterly.GlyphButton(Counterly.GlyphMore, 36, Counterly.Page, Counterly.Muted)
        chatHeader.Controls.Add(chatCall)
        chatHeader.Controls.Add(chatMore)
        AddHandler chatHeader.Resize, AddressOf ChatHeader_Resize
        chatPane.Controls.Add(chatHeader)

        pg.Controls.Add(body)

        ' ---------- page header (title, bell, user, logout) ----------
        hdrPanel = New Panel()
        hdrPanel.Dock = DockStyle.Top
        hdrPanel.Height = 80
        hdrPanel.BackColor = Color.White
        Dim hdrLine As New Panel()
        hdrLine.Dock = DockStyle.Bottom
        hdrLine.Height = 1
        hdrLine.BackColor = Counterly.Border
        hdrPanel.Controls.Add(hdrLine)

        hdrPanel.Controls.Add(Counterly.Lbl("Messages", 17.0F, FontStyle.Regular, Counterly.Ink, 28, 12))
        hdrPanel.Controls.Add(Counterly.Lbl("Coordinate with cashiers and keep store operations moving", 9.5F,
                                            FontStyle.Regular, Counterly.Muted, 29, 46))

        hdrBell = Counterly.GlyphButton(Counterly.GlyphBell, 40, Color.White, Counterly.Ink)
        hdrBell.BorderColor = Counterly.Border
        hdrBell.BorderThickness = 1
        hdrPanel.Controls.Add(hdrBell)

        hdrDivider = New Panel()
        hdrDivider.Size = New Size(1, 40)
        hdrDivider.BackColor = Counterly.Border
        hdrPanel.Controls.Add(hdrDivider)

        hdrAvatar = Counterly.Avatar("AD", 40, Counterly.TealSoft, Counterly.Teal)
        hdrPanel.Controls.Add(hdrAvatar)
        hdrUserName = Counterly.Lbl("Admin", 10.0F, FontStyle.Bold, Counterly.Ink)
        hdrUserRole = Counterly.Lbl("Administrator", 8.5F, FontStyle.Regular, Counterly.Muted)
        hdrPanel.Controls.Add(hdrUserName)
        hdrPanel.Controls.Add(hdrUserRole)

        hdrLogout = Counterly.GlyphButton(Counterly.GlyphPower, 36, Color.White, Counterly.Danger)
        hdrPanel.Controls.Add(hdrLogout)
        AddHandler hdrLogout.Click, AddressOf HdrLogout_Click
        AddHandler hdrPanel.Resize, AddressOf HdrPanel_Resize

        pg.Controls.Add(hdrPanel)
        pg.ResumeLayout(True)

        ' initial positions (Resize may not fire for controls that never change size)
        HdrPanel_Resize(Nothing, EventArgs.Empty)
        ChatHeader_Resize(Nothing, EventArgs.Empty)
        Composer_Resize(Nothing, EventArgs.Empty)

        RefreshConversationList()
        lastChatWidth = flpAdminMessages.ClientSize.Width
        AddHandler flpAdminMessages.Resize, AddressOf ChatArea_Resize
    End Sub

    Private Sub HdrPanel_Resize(sender As Object, e As EventArgs)
        If hdrPanel Is Nothing Then Return
        Dim x As Integer = hdrPanel.ClientSize.Width - 24

        hdrLogout.Location = New Point(x - hdrLogout.Width, 22)
        x -= hdrLogout.Width + 14

        Dim tw As Integer = Math.Max(hdrUserName.Width, hdrUserRole.Width)
        hdrUserName.Location = New Point(x - tw, 21)
        hdrUserRole.Location = New Point(x - tw, 42)
        x -= tw + 12

        hdrAvatar.Location = New Point(x - hdrAvatar.Width, 20)
        x -= hdrAvatar.Width + 20

        hdrDivider.Location = New Point(x, 20)
        hdrBell.Location = New Point(x - 20 - hdrBell.Width, 20)
    End Sub

    Private Sub ChatHeader_Resize(sender As Object, e As EventArgs)
        If chatHeader Is Nothing Then Return
        Dim w As Integer = chatHeader.ClientSize.Width
        chatMore.Location = New Point(w - 24 - chatMore.Width, 20)
        chatCall.Location = New Point(chatMore.Left - 8 - chatCall.Width, 20)
    End Sub

    Private Sub Composer_Resize(sender As Object, e As EventArgs)
        If composer Is Nothing Then Return
        Dim w As Integer = composer.ClientSize.Width
        btnAdmin.Location = New Point(w - 20 - btnAdmin.Width, 16)
        txtAdminChat.Location = New Point(20, 16)
        txtAdminChat.Size = New Size(Math.Max(100, btnAdmin.Left - 12 - 20), 44)
    End Sub

    Private Sub TxtAdminChat_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            btnAdmin.PerformClick()
        End If
    End Sub

    Private Sub HdrLogout_Click(sender As Object, e As EventArgs)
        DoLogout()
    End Sub

    Private Sub ListCompose_Click(sender As Object, e As EventArgs)
        txtAdminChat.Focus()
    End Sub

    Private Sub ConvoSearch_TextChanged(sender As Object, e As EventArgs)
        RefreshConversationList()
    End Sub

    '=================================================================
    ' CONVERSATION LIST
    '=================================================================
    Private Sub RefreshConversationList()
        If FlowLayoutPanel3 Is Nothing OrElse listPane Is Nothing OrElse chatAvatarHost Is Nothing Then Return

        Dim query As String = If(convoSearch Is Nothing, "", convoSearch.Text.Trim())

        ' who is in the list?
        Dim names As New List(Of String)
        For Each acc As CashierAccount In DataStore.Cashiers
            If String.IsNullOrWhiteSpace(acc.FullName) Then Continue For
            If query.Length > 0 AndAlso acc.FullName.IndexOf(query, StringComparison.OrdinalIgnoreCase) < 0 Then Continue For
            names.Add(acc.FullName)
        Next

        ' keep a valid selection
        If Not names.Contains(selectedConvo) Then
            selectedConvo = If(names.Count > 0, names(0), "")
        End If

        FlowLayoutPanel3.SuspendLayout()
        For i As Integer = FlowLayoutPanel3.Controls.Count - 1 To 0 Step -1
            Dim c As Control = FlowLayoutPanel3.Controls(i)
            FlowLayoutPanel3.Controls.RemoveAt(i)
            c.Dispose()
        Next

        Dim rowH As Integer = 82
        Dim needsScroll As Boolean = names.Count * rowH > FlowLayoutPanel3.ClientSize.Height
        Dim w As Integer = FlowLayoutPanel3.ClientSize.Width - If(needsScroll, SystemInformation.VerticalScrollBarWidth, 0)
        If w < 100 Then w = 299

        If names.Count = 0 Then
            Dim empty As Label = Counterly.Lbl("No team members found.", 9.5F, FontStyle.Regular, Counterly.Muted, 0, 0)
            empty.AutoSize = False
            empty.Size = New Size(w, 60)
            empty.TextAlign = ContentAlignment.MiddleCenter
            FlowLayoutPanel3.Controls.Add(empty)
        End If

        For Each nm As String In names
            FlowLayoutPanel3.Controls.Add(BuildConvoItem(nm, w, rowH))
        Next
        FlowLayoutPanel3.ResumeLayout(True)

        ' chat header follows the selection
        CashierName.Text = If(selectedConvo = "", "Cashier Team", selectedConvo)
        chatAvatarHost.Controls.Clear()
        AddAvatarSafe(chatAvatarHost, CashierName.Text, 44, Counterly.TealSoft, Counterly.Teal, Point.Empty)
    End Sub

    ' Adds a cashier avatar. If the cashier's photo can't be drawn (e.g. an image that was already disposed
    ' after a logout/login), falls back to the plain initials avatar instead of crashing the screen.
    Private Sub AddAvatarSafe(host As Control, fullName As String, size As Integer, back As Color, fore As Color, loc As Point)
        Dim av As Control = Nothing
        Try
            av = CashierAvatar(fullName, size, back, fore)
            If loc <> Point.Empty Then av.Location = loc
            host.Controls.Add(av)
        Catch ex As ArgumentException
            If av IsNot Nothing Then
                host.Controls.Remove(av)
                av.Dispose()
            End If
            Dim initials As String = ""
            For Each part As String In If(fullName, "").Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
                initials &= Char.ToUpper(part(0))
                If initials.Length = 2 Then Exit For
            Next
            If initials = "" Then initials = "C"
            Dim fb As Control = Counterly.Avatar(initials, size, back, fore)
            If loc <> Point.Empty Then fb.Location = loc
            host.Controls.Add(fb)
        End Try
    End Sub

    Private Function BuildConvoItem(nm As String, w As Integer, h As Integer) As Panel

        Dim isSel As Boolean = String.Equals(nm, selectedConvo, StringComparison.OrdinalIgnoreCase)

        ' latest message sent by this cashier (if any)
        Dim hasLast As Boolean = False
        Dim lastText As String = ""
        Dim lastTime As DateTime = DateTime.Now
        For Each m As ChatMessage In DataStore.ChatMessages
            If String.Equals(m.Sender, nm, StringComparison.OrdinalIgnoreCase) Then
                hasLast = True
                lastText = m.Message
                lastTime = m.TimeSent
            End If
        Next

        Dim p As New Panel()
        p.Size = New Size(w, h)
        p.Margin = Padding.Empty
        p.BackColor = If(isSel, Counterly.TealSoft, Color.White)
        p.Cursor = Cursors.Hand
        p.Tag = nm

        AddAvatarSafe(p, nm, 40,
                      If(isSel, Color.FromArgb(205, 240, 234), Counterly.AvatarGray),
                      If(isSel, Counterly.Teal, Counterly.Ink), New Point(16, 21))

        p.Controls.Add(Counterly.Lbl(nm, 10.0F, FontStyle.Bold, Counterly.Ink, 68, 17))

        Dim preview As Label = Counterly.Lbl(If(hasLast, lastText, "Cashier"), 9.0F, FontStyle.Regular, Counterly.Muted, 68, 41)
        preview.AutoSize = False
        preview.AutoEllipsis = True
        preview.Size = New Size(Math.Max(40, w - 68 - 16), 20)
        p.Controls.Add(preview)

        If hasLast Then
            Dim tm As Label = Counterly.Lbl(Counterly.TimeAgo(lastTime), 8.0F, FontStyle.Regular, Counterly.Muted)
            tm.Location = New Point(w - tm.Width - 16, 19)
            p.Controls.Add(tm)
        End If

        ' every child behaves like the row itself
        For Each c As Control In p.Controls
            c.Tag = nm
            c.Cursor = Cursors.Hand
            AddHandler c.Click, AddressOf ConvoItem_Click
            For Each inner As Control In c.Controls
                inner.Tag = nm
                AddHandler inner.Click, AddressOf ConvoItem_Click
            Next
        Next
        AddHandler p.Click, AddressOf ConvoItem_Click
        Return p
    End Function

    Private Sub ConvoItem_Click(sender As Object, e As EventArgs)
        Dim c As Control = TryCast(sender, Control)
        If c Is Nothing OrElse c.Tag Is Nothing Then Return
        selectedConvo = CStr(c.Tag)
        RefreshConversationList()
        txtAdminChat.Focus()
    End Sub

    '=================================================================
    ' CHAT BUBBLES  (replaces the old LoadChatMessages in Admin.vb)
    '=================================================================
    Private Sub ChatArea_Resize(sender As Object, e As EventArgs)
        Dim w As Integer = flpAdminMessages.ClientSize.Width
        If w = lastChatWidth OrElse w <= 0 Then Return
        lastChatWidth = w
        LoadChatMessages()
    End Sub

    Private Sub LoadChatMessages()

        flpAdminMessages.SuspendLayout()
        For i As Integer = flpAdminMessages.Controls.Count - 1 To 0 Step -1
            Dim c As Control = flpAdminMessages.Controls(i)
            flpAdminMessages.Controls.RemoveAt(i)
            c.Dispose()
        Next

        ' usable width (always reserve the scrollbar so nothing ever scrolls sideways)
        Dim areaW As Integer = flpAdminMessages.ClientSize.Width - flpAdminMessages.Padding.Horizontal _
                               - SystemInformation.VerticalScrollBarWidth
        If areaW < 240 Then areaW = 240
        Dim maxBubble As Integer = Math.Min(560, CInt(areaW * 0.7))

        Dim lastDate As DateTime = DateTime.MinValue
        For Each chat As ChatMessage In DataStore.ChatMessages

            If chat.TimeSent.Date <> lastDate Then
                lastDate = chat.TimeSent.Date
                Dim dateText As String = If(lastDate = DateTime.Today, "TODAY", lastDate.ToString("dddd").ToUpper()) _
                                      & " " & ChrW(&HB7) & " " & lastDate.ToString("MMMM d").ToUpper()
                Dim divider As Label = Counterly.Lbl(dateText, 8.0F, FontStyle.Regular, Counterly.Muted)
                divider.AutoSize = False
                divider.Size = New Size(areaW, 34)
                divider.TextAlign = ContentAlignment.MiddleCenter
                divider.Margin = New Padding(0, 4, 0, 2)
                flpAdminMessages.Controls.Add(divider)
            End If

            flpAdminMessages.Controls.Add(BuildBubble(chat, areaW, maxBubble))
        Next

        flpAdminMessages.ResumeLayout(True)
        If flpAdminMessages.Controls.Count > 0 Then
            flpAdminMessages.ScrollControlIntoView(flpAdminMessages.Controls(flpAdminMessages.Controls.Count - 1))
        End If

        RefreshConversationList()   ' previews + times
        UpdateMessageBadge()
    End Sub

    Private Function BuildBubble(chat As ChatMessage, areaW As Integer, maxBubble As Integer) As Guna2Panel

        Dim fromAdmin As Boolean = String.Equals(chat.Sender, "Admin", StringComparison.OrdinalIgnoreCase)

        Const padX As Integer = 16
        Const padY As Integer = 12

        Dim flags As TextFormatFlags = TextFormatFlags.WordBreak Or TextFormatFlags.NoPadding
        Dim txt As String = If(chat.Message, "")
        Dim txtSize As Size = TextRenderer.MeasureText(txt, bubbleFont, New Size(maxBubble - 2 * padX, 0), flags)

        Dim metaText As String = chat.TimeSent.ToString("hh:mm tt") & If(fromAdmin, " " & ChrW(&HB7) & " Read", "")
        Dim metaSize As Size = TextRenderer.MeasureText(metaText, metaFont)

        Dim contentW As Integer = Math.Max(txtSize.Width, metaSize.Width) + 6
        Dim bubbleW As Integer = contentW + 2 * padX
        Dim bubbleH As Integer = padY + txtSize.Height + 8 + metaSize.Height + padY

        Dim fill As Color = If(fromAdmin, Counterly.Bubble, Color.White)
        Dim bubble As Guna2Panel = If(fromAdmin,
                                      Counterly.Card(bubbleW, bubbleH, 12, fill),
                                      Counterly.Card(bubbleW, bubbleH, 12, fill, Color.FromArgb(238, 242, 246)))

        Dim msgLbl As New Label()
        msgLbl.AutoSize = False
        msgLbl.UseMnemonic = False
        msgLbl.BackColor = Color.Transparent
        msgLbl.ForeColor = If(fromAdmin, Color.White, Counterly.Ink)
        msgLbl.Font = bubbleFont
        msgLbl.Text = txt
        msgLbl.Location = New Point(padX, padY)
        msgLbl.Size = New Size(contentW, txtSize.Height + 2)
        bubble.Controls.Add(msgLbl)

        Dim metaLbl As New Label()
        metaLbl.AutoSize = True
        metaLbl.BackColor = Color.Transparent
        metaLbl.ForeColor = If(fromAdmin, Color.FromArgb(170, 190, 205), Color.FromArgb(150, 160, 172))
        metaLbl.Font = metaFont
        metaLbl.Text = metaText
        metaLbl.Location = New Point(padX, padY + txtSize.Height + 8)
        bubble.Controls.Add(metaLbl)

        ' sent = right side, received = left side
        bubble.Margin = New Padding(If(fromAdmin, Math.Max(0, areaW - bubbleW), 0), 8, 0, 8)
        Return bubble
    End Function

End Class