Option Strict On
Option Explicit On

Imports System.Drawing
Imports System.Windows.Forms
Imports Guna.UI2.WinForms

' Admin.ForestUi.vb  -  NEW FILE (second half of the Admin class).
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
    Private listSearchWrap As Panel
    Private listSearchBox As Guna2Panel
    Private convoSearch As Guna2TextBox
    Private selectedConvo As String = ""

    Private chatHeader As Panel
    Private chatAvatarHost As Panel
    Private chatStatusDot As Guna2Panel
    Private composer As Panel

    Private ReadOnly bubbleFont As New Font("Segoe UI", 10.0F)
    Private ReadOnly metaFont As New Font("Segoe UI", 8.0F)
    Private lastChatWidth As Integer = -1

    Private editBar As Panel
    Private editingMessage As ChatMessage = Nothing
    Private chatRoleBadge As Guna2Panel
    Private msgRendering As Boolean = False
    Private lastAdminInitials As String = ""

    '=================================================================
    ' SIDEBAR
    '=================================================================
    Private Sub StyleSidebar()

        Navigation.BackColor = ForestUi.Navy

        ' remove the old cafe-style bits
        Guna2HtmlLabel40.Visible = False
        Guna2CustomGradientPanel2.Visible = False

        ' logo tile + app name
        Dim logo As Guna2Panel = ForestUi.Card(40, 40, 10, ForestUi.Accent)
        logo.Location = New Point(14, 16)
        Dim logoText As New Label()
        logoText.Dock = DockStyle.Fill
        logoText.BackColor = Color.Transparent
        logoText.ForeColor = Color.White
        logoText.Font = New Font("Segoe UI", 15.0F, FontStyle.Bold)
        logoText.TextAlign = ContentAlignment.MiddleCenter
        logoText.Text = ForestUi.AppName.Substring(0, 1)
        logo.Controls.Add(logoText)
        Navigation.Controls.Add(logo)

        Guna2HtmlLabel26.Text = ForestUi.AppName
        Guna2HtmlLabel26.Font = New Font("Segoe UI", 14.0F, FontStyle.Regular)
        Guna2HtmlLabel26.ForeColor = Color.White
        Guna2HtmlLabel26.Location = New Point(62, 14)

        Dim sub1 As Label = ForestUi.Lbl(ForestUi.AppSub, 7.5F, FontStyle.Regular, ForestUi.NavMuted, 63, 38)
        Navigation.Controls.Add(sub1)

        Guna2HtmlLabel4.Text = "ADMIN WORKSPACE"
        Guna2HtmlLabel4.Font = New Font("Segoe UI", 7.5F, FontStyle.Bold)
        Guna2HtmlLabel4.ForeColor = ForestUi.NavMuted
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
            b.ForeColor = ForestUi.NavText
            b.Font = navFontOff
            b.HoverState.FillColor = ForestUi.NavHover
            b.HoverState.ForeColor = Color.White
            b.Cursor = Cursors.Hand
            y += 48
        Next

        ' unread badge on the Messages item
        navBadge = ForestUi.Card(22, 22, 11, ForestUi.Accent)
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
        Dim help As Guna2Panel = ForestUi.Card(175, 104, 12, ForestUi.NavCard)
        help.Location = New Point(12, 420)
        Dim helpIcon As New Label()
        helpIcon.AutoSize = True
        helpIcon.BackColor = Color.Transparent
        helpIcon.ForeColor = ForestUi.Accent
        helpIcon.Font = New Font("Segoe MDL2 Assets", 14.0F)
        helpIcon.Text = ForestUi.GlyphHelp
        helpIcon.Location = New Point(12, 10)
        help.Controls.Add(helpIcon)
        help.Controls.Add(ForestUi.Lbl("Need help?", 10.0F, FontStyle.Bold, Color.White, 12, 38))
        Dim helpText As Label = ForestUi.Lbl("Visit the operations guide or contact support.", 8.0F,
                                              FontStyle.Regular, ForestUi.NavMuted, 12, 60)
        helpText.AutoSize = False
        helpText.Size = New Size(152, 36)
        help.Controls.Add(helpText)
        Navigation.Controls.Add(help)

        ' store status
        Dim dot As Guna2Panel = ForestUi.Card(8, 8, 4, ForestUi.Accent)
        dot.Location = New Point(20, 548)
        dot.FillColor = ForestUi.Green
        dot.Anchor = AnchorStyles.Left Or AnchorStyles.Bottom
        dot.Top = Math.Max(530, Navigation.Height - 118)
        Navigation.Controls.Add(dot)
        Dim storeLbl As Label = ForestUi.Lbl(ForestUi.StoreName & " " & ChrW(&HB7) & " Open", 8.5F,
                                             FontStyle.Regular, ForestUi.NavText, 34, dot.Top - 5)
        storeLbl.Anchor = AnchorStyles.Left Or AnchorStyles.Bottom
        Navigation.Controls.Add(storeLbl)

        ' logout (same button, new look)
        btnAdminLogout.Animated = False
        btnAdminLogout.BorderRadius = 10
        btnAdminLogout.Size = New Size(175, 42)
        btnAdminLogout.Location = New Point(12, Math.Max(540, Navigation.Height - 62))
        btnAdminLogout.Anchor = AnchorStyles.Left Or AnchorStyles.Bottom
        btnAdminLogout.Text = "Log out"
        btnAdminLogout.TextAlign = HorizontalAlignment.Left
        btnAdminLogout.TextOffset = New Point(12, 0)
        Dim logoutLine As New Panel()
        logoutLine.Size = New Size(175, 1)
        logoutLine.BackColor = ForestUi.NavCard
        logoutLine.Location = New Point(12, btnAdminLogout.Top - 8)
        logoutLine.Anchor = AnchorStyles.Left Or AnchorStyles.Bottom
        Navigation.Controls.Add(logoutLine)
        btnAdminLogout.FillColor = Color.Transparent
        btnAdminLogout.ForeColor = Color.FromArgb(240, 140, 140)
        btnAdminLogout.Font = navFontOff
        btnAdminLogout.HoverState.FillColor = ForestUi.NavHover
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
            b.FillColor = If(isActive, ForestUi.NavActive, Color.Transparent)
            b.HoverState.FillColor = If(isActive, ForestUi.NavActive, ForestUi.NavHover)
            b.ForeColor = If(isActive, Color.White, ForestUi.NavText)
            b.Font = If(isActive, navFontOn, navFontOff)
        Next
    End Sub

    Private Sub UpdateMessageBadge()
        If navBadge Is Nothing Then Return

        Dim unread As Integer = DataStore.TotalUnreadForAdmin()
        navBadge.Visible = unread > 0
        navBadgeText.Text = If(unread > 9, "9+", unread.ToString())
    End Sub

    '=================================================================
    ' MESSAGES PAGE LAYOUT
    '=================================================================
    Private Sub BuildMessagingLayout()

        Dim pg As Panel = pnl_Messages
        pg.SuspendLayout()
        pg.BackColor = ForestUi.Page
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
        body.BackColor = ForestUi.Page
        body.Padding = New Padding(24)

        Dim shell As Guna2Panel = ForestUi.Card(100, 100, 12, ForestUi.CardFill, ForestUi.Border)
        shell.Dock = DockStyle.Fill
        shell.Padding = New Padding(2)
        body.Controls.Add(shell)

        listPane = New Panel()
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
        FlowLayoutPanel3.BackColor = ForestUi.CardFill
        FlowLayoutPanel3.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        listPane.Controls.Add(FlowLayoutPanel3)

        listSearchWrap = New Panel()
        listSearchWrap.Dock = DockStyle.Top
        listSearchWrap.Height = 66
        listSearchWrap.BackColor = ForestUi.CardFill

        listSearchBox = ForestUi.Card(260, 44, 10, ForestUi.CardFill, ForestUi.Border)
        listSearchBox.Location = New Point(20, 4)
        Dim searchIcon As New Label()
        searchIcon.AutoSize = True
        searchIcon.BackColor = Color.Transparent
        searchIcon.ForeColor = ForestUi.Muted
        searchIcon.Font = New Font("Segoe MDL2 Assets", 11.0F)
        searchIcon.Text = ForestUi.GlyphSearch
        searchIcon.Location = New Point(14, 13)
        listSearchBox.Controls.Add(searchIcon)

        convoSearch = New Guna2TextBox()
        convoSearch.BorderThickness = 0
        convoSearch.BorderRadius = 0
        convoSearch.FillColor = ForestUi.CardFill
        convoSearch.Font = New Font("Segoe UI", 10.0F)
        convoSearch.ForeColor = ForestUi.Ink
        convoSearch.PlaceholderText = "Search Admin or Cashier"
        convoSearch.PlaceholderForeColor = ForestUi.Muted
        convoSearch.Location = New Point(42, 6)
        convoSearch.Size = New Size(205, 32)
        listSearchBox.Controls.Add(convoSearch)
        AddHandler convoSearch.TextChanged, AddressOf ConvoSearch_TextChanged
        listSearchWrap.Controls.Add(listSearchBox)
        listPane.Controls.Add(listSearchWrap)

        ' "ACCESS SCOPE:  [Admin] & [Cashier] only"
        Dim scopeRow As New Panel()
        scopeRow.Dock = DockStyle.Top
        scopeRow.Height = 70
        scopeRow.BackColor = ForestUi.CardFill
        Dim scopeCaption As Label = ForestUi.Lbl("ACCESS SCOPE", 7.5F, FontStyle.Bold, ForestUi.Muted, 22, 6)
        scopeCaption.UseMnemonic = False
        scopeRow.Controls.Add(scopeCaption)
        Dim scopePill As Guna2Panel = ForestUi.Card(260, 32, 16, ForestUi.AccentSoft)
        scopePill.Location = New Point(20, 28)
        Dim scopeAdmin As Guna2Panel = MakeRoleBadge("Admin")
        scopeAdmin.Location = New Point(10, 7)
        scopePill.Controls.Add(scopeAdmin)
        Dim scopeAmp As Label = ForestUi.Lbl("&", 8.0F, FontStyle.Regular, ForestUi.Muted, scopeAdmin.Right + 8, 8)
        scopeAmp.UseMnemonic = False
        scopePill.Controls.Add(scopeAmp)
        Dim scopeCashier As Guna2Panel = MakeRoleBadge("Cashier")
        scopeCashier.Location = New Point(scopeAmp.Right + 6, 7)
        scopePill.Controls.Add(scopeCashier)
        scopePill.Controls.Add(ForestUi.Lbl("only", 8.0F, FontStyle.Regular, ForestUi.Muted, scopeCashier.Right + 8, 8))
        scopeRow.Controls.Add(scopePill)
        listPane.Controls.Add(scopeRow)

        listTitleRow = New Panel()
        listTitleRow.Dock = DockStyle.Top
        listTitleRow.Height = 62
        listTitleRow.BackColor = ForestUi.CardFill
        Dim listTitle As Label = ForestUi.Lbl("Admin & Cashier conversations", 10.5F, FontStyle.Bold, ForestUi.Ink, 22, 22)
        listTitle.UseMnemonic = False
        listTitleRow.Controls.Add(listTitle)
        listPane.Controls.Add(listTitleRow)

        ' ---------- right: chat ----------
        ' messages area (re-used control)
        flpAdminMessages.Dock = DockStyle.Fill
        flpAdminMessages.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        flpAdminMessages.BackColor = ForestUi.ChatBg
        flpAdminMessages.FlowDirection = FlowDirection.TopDown
        flpAdminMessages.WrapContents = False
        flpAdminMessages.AutoScroll = True
        flpAdminMessages.Padding = New Padding(24, 12, 24, 12)
        chatPane.Controls.Add(flpAdminMessages)

        ' composer (re-uses txtAdminChat + btnAdmin)
        composer = New Panel()
        composer.Dock = DockStyle.Bottom
        composer.Height = 76
        composer.BackColor = ForestUi.CardFill
        Dim composerLine As New Panel()
        composerLine.Dock = DockStyle.Top
        composerLine.Height = 1
        composerLine.BackColor = ForestUi.Border
        composer.Controls.Add(composerLine)

        ' "Editing message" bar (only visible while the admin corrects one of his own messages)
        editBar = New Panel()
        editBar.Dock = DockStyle.Top
        editBar.Height = 30
        editBar.BackColor = ForestUi.AccentSoft
        editBar.Visible = False
        editBar.Controls.Add(ForestUi.Lbl("Editing message", 8.5F, FontStyle.Bold, ForestUi.Accent, 24, 7))
        Dim editCancel As Label = ForestUi.Lbl("Cancel", 8.5F, FontStyle.Underline, ForestUi.Muted, 140, 7)
        editCancel.Cursor = Cursors.Hand
        AddHandler editCancel.Click, Sub(o As Object, ev As EventArgs) CancelEdit()
        editBar.Controls.Add(editCancel)
        composer.Controls.Add(editBar)

        txtAdminChat.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        txtAdminChat.BorderRadius = 20
        txtAdminChat.BorderColor = ForestUi.Border
        txtAdminChat.FillColor = ForestUi.CardFill
        txtAdminChat.ForeColor = ForestUi.Ink
        txtAdminChat.Font = New Font("Segoe UI", 10.0F)
        txtAdminChat.PlaceholderText = "Write a message..."
        txtAdminChat.PlaceholderForeColor = ForestUi.Muted
        txtAdminChat.FocusedState.BorderColor = ForestUi.Accent
        txtAdminChat.HoverState.BorderColor = ForestUi.Accent
        composer.Controls.Add(txtAdminChat)
        AddHandler txtAdminChat.KeyDown, AddressOf TxtAdminChat_KeyDown

        btnAdmin.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        btnAdmin.Animated = False
        btnAdmin.BorderRadius = 20
        btnAdmin.FillColor = ForestUi.Accent
        btnAdmin.ForeColor = Color.White
        btnAdmin.Font = New Font("Segoe MDL2 Assets", 12.0F)
        btnAdmin.HoverState.FillColor = ForestUi.AccentDark
        btnAdmin.Text = ChrW(&HE725)
        btnAdmin.Cursor = Cursors.Hand
        btnAdmin.Size = New Size(40, 40)
        composer.Controls.Add(btnAdmin)

        ' the old handler sent every message to "Cashier"; this one sends to the open conversation
        RemoveHandler btnAdmin.Click, AddressOf btnAdminSend_Click
        AddHandler btnAdmin.Click, AddressOf AdminChat_Send
        AddHandler composer.Resize, AddressOf Composer_Resize
        chatPane.Controls.Add(composer)

        ' chat header (re-uses CashierName + Guna2HtmlLabel60)
        chatHeader = New Panel()
        chatHeader.Dock = DockStyle.Top
        chatHeader.Height = 76
        chatHeader.BackColor = ForestUi.CardFill
        Dim headerLine As New Panel()
        headerLine.Dock = DockStyle.Bottom
        headerLine.Height = 1
        headerLine.BackColor = ForestUi.Border
        chatHeader.Controls.Add(headerLine)

        chatAvatarHost = New Panel()
        chatAvatarHost.Size = New Size(44, 44)
        chatAvatarHost.Location = New Point(24, 16)
        chatAvatarHost.BackColor = ForestUi.CardFill
        chatHeader.Controls.Add(chatAvatarHost)

        CashierName.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        CashierName.ForeColor = ForestUi.Ink
        CashierName.BackColor = Color.Transparent
        CashierName.Location = New Point(80, 14)
        chatHeader.Controls.Add(CashierName)

        chatRoleBadge = MakeRoleBadge("Cashier")
        chatRoleBadge.Location = New Point(82, 46)
        chatHeader.Controls.Add(chatRoleBadge)

        chatStatusDot = ForestUi.Card(8, 8, 4, ForestUi.Accent)   ' not used in the new design
        chatStatusDot.Visible = False
        Guna2HtmlLabel60.Visible = False

        chatPane.Controls.Add(chatHeader)

        pg.Controls.Add(body)

        ' ---------- page header (title, bell, user, logout) ----------
        hdrPanel = New Panel()
        hdrPanel.Dock = DockStyle.Top
        hdrPanel.Height = 80
        hdrPanel.BackColor = ForestUi.CardFill
        Dim hdrLine As New Panel()
        hdrLine.Dock = DockStyle.Bottom
        hdrLine.Height = 1
        hdrLine.BackColor = ForestUi.Border
        hdrPanel.Controls.Add(hdrLine)


        hdrBell = ForestUi.GlyphButton(ForestUi.GlyphBell, 40, ForestUi.CardFill, ForestUi.Ink)
        hdrBell.BorderColor = ForestUi.Border
        hdrBell.BorderThickness = 1
        hdrPanel.Controls.Add(hdrBell)

        hdrDivider = New Panel()
        hdrDivider.Size = New Size(1, 40)
        hdrDivider.BackColor = ForestUi.Border
        hdrPanel.Controls.Add(hdrDivider)

        hdrAvatar = ForestUi.Avatar("AD", 40, ForestUi.AccentSoft, ForestUi.Accent)
        hdrPanel.Controls.Add(hdrAvatar)
        hdrUserName = ForestUi.Lbl("Admin", 10.0F, FontStyle.Bold, ForestUi.Ink)
        hdrUserRole = ForestUi.Lbl("Administrator", 8.5F, FontStyle.Regular, ForestUi.Muted)
        hdrPanel.Controls.Add(hdrUserName)
        hdrPanel.Controls.Add(hdrUserRole)

        hdrLogout = ForestUi.GlyphButton(ForestUi.GlyphPower, 36, ForestUi.CardFill, ForestUi.Danger)
        hdrPanel.Controls.Add(hdrLogout)
        AddHandler hdrLogout.Click, AddressOf HdrLogout_Click
        AddHandler hdrPanel.Resize, AddressOf HdrPanel_Resize

        pg.Controls.Add(hdrPanel)
        pg.ResumeLayout(True)

        ' initial positions (Resize may not fire for controls that never change size)
        HdrPanel_Resize(Nothing, EventArgs.Empty)
        Composer_Resize(Nothing, EventArgs.Empty)

        AddHandler DataStore.ReadStatesChanged, AddressOf Admin_ReadStatesChanged
        RefreshAdminIdentity()
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

    Private Sub Composer_Resize(sender As Object, e As EventArgs)
        If composer Is Nothing Then Return
        Dim w As Integer = composer.ClientSize.Width
        Dim y0 As Integer = 18 + If(editBar IsNot Nothing AndAlso editBar.Visible, editBar.Height, 0)
        btnAdmin.Location = New Point(w - 20 - btnAdmin.Width, y0)
        txtAdminChat.Location = New Point(20, y0)
        txtAdminChat.Size = New Size(Math.Max(100, btnAdmin.Left - 12 - txtAdminChat.Left), 40)
    End Sub

    Private Sub TxtAdminChat_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Escape AndAlso editingMessage IsNot Nothing Then
            e.SuppressKeyPress = True
            CancelEdit()
            Return
        End If
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            btnAdmin.PerformClick()
        End If
    End Sub

    Private Sub HdrLogout_Click(sender As Object, e As EventArgs)
        DoLogout()
    End Sub


    Private Sub ConvoSearch_TextChanged(sender As Object, e As EventArgs)
        RefreshConversationList()
    End Sub

    '=================================================================
    ' SIGNED-IN ADMIN  (name comes from the login, nothing is typed in)
    '=================================================================
    Private Function CurrentAdminName() As String
        If Not String.IsNullOrWhiteSpace(CurrentSession.FullName) Then Return CurrentSession.FullName.Trim()
        If Not String.IsNullOrWhiteSpace(CurrentSession.Username) Then Return CurrentSession.Username.Trim()
        Return "Admin"
    End Function

    Private Function InitialsOf(name As String) As String
        Dim ini As String = ""
        For Each part As String In If(name, "").Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
            ini &= Char.ToUpper(part(0))
            If ini.Length = 2 Then Exit For
        Next
        Return If(ini = "", "A", ini)
    End Function

    Private Sub RefreshAdminIdentity()
        If hdrPanel Is Nothing OrElse hdrUserName Is Nothing Then Return

        Dim nm As String = CurrentAdminName()
        hdrUserName.Text = nm
        hdrUserRole.Text = If(String.Equals(CurrentSession.Role, UserRoles.SuperAdmin, StringComparison.OrdinalIgnoreCase),
                              "Super Admin", "Administrator")

        Dim ini As String = InitialsOf(nm)
        If ini <> lastAdminInitials Then
            lastAdminInitials = ini
            Dim old As Guna2Panel = hdrAvatar
            hdrAvatar = ForestUi.Avatar(ini, 40, ForestUi.AccentSoft, ForestUi.Accent)
            hdrPanel.Controls.Add(hdrAvatar)
            If old IsNot Nothing Then
                hdrPanel.Controls.Remove(old)
                old.Dispose()
            End If
        End If
        HdrPanel_Resize(Nothing, EventArgs.Empty)
    End Sub

    ' small dark pill, e.g. "Cashier" / "Admin"
    Private Function MakeRoleBadge(text As String) As Guna2Panel
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

    ' the admin writes to the cashier whose conversation is open
    Private Sub AdminChat_Send(sender As Object, e As EventArgs)
        If String.IsNullOrWhiteSpace(txtAdminChat.Text) Then Return
        If selectedConvo = "" Then Return

        If editingMessage IsNot Nothing Then
            Dim err As String = ""
            If DataStore.EditMessage(editingMessage, txtAdminChat.Text, True, CurrentAdminName(), err) Then
                CancelEdit()                  ' MessagesChanged already redrew the chat
            Else
                MessageBox.Show(err, "Edit message", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
            Return
        End If

        Dim newMessage As New ChatMessage With {
            .Sender = CurrentAdminName(),
            .Receiver = selectedConvo,
            .Message = txtAdminChat.Text.Trim(),
            .TimeSent = DateTime.Now
        }
        txtAdminChat.Clear()
        DataStore.AddMessage(newMessage)      ' saved + MessagesChanged refreshes this screen
    End Sub

    ' ---- correcting a message the admin already sent ----
    Private Sub BeginEdit(chat As ChatMessage)
        editingMessage = chat
        txtAdminChat.Text = chat.Message
        txtAdminChat.SelectionStart = txtAdminChat.Text.Length
        editBar.Visible = True
        composer.Height = 76 + editBar.Height
        Composer_Resize(Nothing, EventArgs.Empty)
        txtAdminChat.Focus()
    End Sub

    Private Sub CancelEdit()
        editingMessage = Nothing
        txtAdminChat.Clear()
        If editBar IsNot Nothing Then editBar.Visible = False
        composer.Height = 76
        Composer_Resize(Nothing, EventArgs.Empty)
    End Sub

    Private Sub Admin_ReadStatesChanged(sender As Object, e As EventArgs)
        If msgRendering Then Return
        LoadChatMessages()
    End Sub

    Private Sub Admin_FormClosed_Messaging(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        RemoveHandler DataStore.ReadStatesChanged, AddressOf Admin_ReadStatesChanged
    End Sub

    '=================================================================
    ' CONVERSATION LIST
    '=================================================================
    Private Function AllCashierNames() As List(Of String)
        Dim result As New List(Of String)
        For Each acc As CashierAccount In DataStore.Cashiers
            If Not String.IsNullOrWhiteSpace(acc.FullName) Then result.Add(acc.FullName)
        Next
        Return result
    End Function

    Private Function LastTimeOf(nm As String) As DateTime
        Dim m As ChatMessage = DataStore.ThreadLast(nm)
        Return If(m Is Nothing, DateTime.MinValue, m.TimeSent)
    End Function

    ' keeps selectedConvo pointing at a real cashier (the one with the newest message if nothing is selected)
    Private Sub EnsureSelectedConvo()
        Dim all As List(Of String) = AllCashierNames()
        For Each n As String In all
            If String.Equals(n, selectedConvo, StringComparison.OrdinalIgnoreCase) Then
                selectedConvo = n
                Return
            End If
        Next

        selectedConvo = ""
        Dim best As DateTime = DateTime.MinValue
        For Each n As String In all
            Dim t As DateTime = LastTimeOf(n)
            If selectedConvo = "" OrElse t > best Then
                selectedConvo = n
                best = t
            End If
        Next
    End Sub

    Private Function MakeSectionLabel(text As String, w As Integer) As Label
        Dim l As Label = ForestUi.Lbl(text, 7.5F, FontStyle.Bold, ForestUi.Muted, 0, 0)
        l.AutoSize = False
        l.UseMnemonic = False
        l.Size = New Size(w, 30)
        l.Padding = New Padding(22, 0, 0, 6)
        l.TextAlign = ContentAlignment.BottomLeft
        l.Margin = Padding.Empty
        Return l
    End Function

    Private Sub RefreshConversationList()
        If FlowLayoutPanel3 Is Nothing OrElse listPane Is Nothing OrElse chatAvatarHost Is Nothing Then Return

        Dim before As String = selectedConvo
        EnsureSelectedConvo()
        If Not msgRendering AndAlso Not String.Equals(before, selectedConvo, StringComparison.Ordinal) Then
            LoadChatMessages()      ' the selection moved to another cashier: reload the bubbles (this redraws the list too)
            Return
        End If

        Dim query As String = If(convoSearch Is Nothing, "", convoSearch.Text.Trim())

        ' cashiers (newest conversation first)
        Dim names As New List(Of String)
        For Each nm As String In AllCashierNames()
            If query.Length > 0 AndAlso nm.IndexOf(query, StringComparison.OrdinalIgnoreCase) < 0 Then Continue For
            names.Add(nm)
        Next
        names = names.OrderByDescending(Function(n) LastTimeOf(n)).ThenBy(Function(n) n, StringComparer.OrdinalIgnoreCase).ToList()

        FlowLayoutPanel3.SuspendLayout()
        For i As Integer = FlowLayoutPanel3.Controls.Count - 1 To 0 Step -1
            Dim c As Control = FlowLayoutPanel3.Controls(i)
            FlowLayoutPanel3.Controls.RemoveAt(i)
            c.Dispose()
        Next

        Dim rowH As Integer = 76
        Dim totalH As Integer = 30 + Math.Max(1, names.Count) * rowH
        Dim needsScroll As Boolean = totalH > FlowLayoutPanel3.ClientSize.Height
        Dim w As Integer = FlowLayoutPanel3.ClientSize.Width - If(needsScroll, SystemInformation.VerticalScrollBarWidth, 0)
        If w < 100 Then w = 299

        FlowLayoutPanel3.Controls.Add(MakeSectionLabel("CASHIERS", w))
        If names.Count = 0 Then
            Dim empty As Label = ForestUi.Lbl("No cashiers found.", 9.5F, FontStyle.Regular, ForestUi.Muted, 0, 0)
            empty.AutoSize = False
            empty.Size = New Size(w, 60)
            empty.TextAlign = ContentAlignment.MiddleCenter
            FlowLayoutPanel3.Controls.Add(empty)
        End If
        For Each nm As String In names
            FlowLayoutPanel3.Controls.Add(BuildConvoItem(nm, w, rowH))
        Next

        FlowLayoutPanel3.ResumeLayout(True)

        ' chat header + composer follow the selection
        Dim hasSel As Boolean = (selectedConvo <> "")
        CashierName.Text = If(hasSel, selectedConvo, "No cashier selected")
        chatRoleBadge.Visible = hasSel
        chatAvatarHost.Controls.Clear()
        If hasSel Then AddAvatarSafe(chatAvatarHost, selectedConvo, 44, ForestUi.AccentSoft, ForestUi.Accent, Point.Empty)

        Dim firstName As String = If(selectedConvo.Contains(" "), selectedConvo.Substring(0, selectedConvo.IndexOf(" "c)), selectedConvo)
        txtAdminChat.PlaceholderText = If(hasSel, "Write a message to " & firstName & "...", "Add a cashier to start chatting")
        txtAdminChat.Enabled = hasSel
        btnAdmin.Enabled = hasSel
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
            Dim fb As Control = ForestUi.Avatar(initials, size, back, fore)
            If loc <> Point.Empty Then fb.Location = loc
            host.Controls.Add(fb)
        End Try
    End Sub

    ' one cashier: avatar, name, time of the last message, "Cashier" badge, preview, unread count
    Private Function BuildConvoItem(nm As String, w As Integer, h As Integer) As Panel

        Dim isSel As Boolean = String.Equals(nm, selectedConvo, StringComparison.OrdinalIgnoreCase)
        Dim last As ChatMessage = DataStore.ThreadLast(nm)

        Dim unread As Integer = DataStore.UnreadForAdmin(nm)
        If isSel AndAlso pnl_Messages.Visible Then unread = 0      ' the open conversation counts as read

        Dim p As New Panel()
        p.Size = New Size(w, h)
        p.Margin = Padding.Empty
        p.BackColor = If(isSel, ForestUi.AccentSoft, ForestUi.CardFill)
        p.Cursor = Cursors.Hand
        p.Tag = nm

        AddAvatarSafe(p, nm, 40,
                      If(isSel, Color.FromArgb(205, 240, 234), ForestUi.AvatarGray),
                      If(isSel, ForestUi.Accent, ForestUi.Ink), New Point(16, 18))

        Dim timeW As Integer = 0
        If last IsNot Nothing Then
            Dim tm As Label = ForestUi.Lbl(ForestUi.TimeAgo(last.TimeSent), 7.5F, FontStyle.Regular, ForestUi.Muted)
            tm.Location = New Point(w - tm.Width - 16, 15)
            timeW = tm.Width + 6
            p.Controls.Add(tm)
        End If

        Dim nameLbl As Label = ForestUi.Lbl(nm, 9.5F, FontStyle.Bold, ForestUi.Ink, 68, 14)
        nameLbl.AutoSize = False
        nameLbl.AutoEllipsis = True
        nameLbl.UseMnemonic = False
        nameLbl.Size = New Size(Math.Max(40, w - 68 - 16 - timeW), 20)
        p.Controls.Add(nameLbl)

        Dim badge As Guna2Panel = MakeRoleBadge("Cashier")
        badge.Location = New Point(68, 42)
        p.Controls.Add(badge)

        Dim previewX As Integer = badge.Right + 6
        Dim reserve As Integer = If(unread > 0, 28, 0)
        Dim preview As Label = ForestUi.Lbl(If(last IsNot Nothing, last.Message, "No messages yet"), 8.5F, FontStyle.Regular, ForestUi.Muted, previewX, 41)
        preview.AutoSize = False
        preview.AutoEllipsis = True
        preview.UseMnemonic = False
        preview.Size = New Size(Math.Max(30, w - previewX - 16 - reserve), 20)
        p.Controls.Add(preview)

        If unread > 0 Then
            Dim dot As Guna2Panel = ForestUi.Card(20, 20, 10, ForestUi.Accent)
            Dim dotText As New Label()
            dotText.Dock = DockStyle.Fill
            dotText.BackColor = Color.Transparent
            dotText.ForeColor = Color.White
            dotText.Font = New Font("Segoe UI", 7.5F, FontStyle.Bold)
            dotText.TextAlign = ContentAlignment.MiddleCenter
            dotText.UseMnemonic = False
            dotText.Text = If(unread > 9, "9+", unread.ToString())
            dot.Controls.Add(dotText)
            dot.Location = New Point(w - 16 - 20, 40)
            p.Controls.Add(dot)
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
        If editingMessage IsNot Nothing Then CancelEdit()
        selectedConvo = CStr(c.Tag)
        LoadChatMessages()          ' shows this cashier's chat and redraws the list
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
        If msgRendering Then Return
        msgRendering = True
        Try
            RefreshAdminIdentity()
            EnsureSelectedConvo()

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

            ' only the open cashier's conversation
            Dim thread As List(Of ChatMessage) = DataStore.ThreadMessages(selectedConvo)

            If thread.Count = 0 Then
                Dim hint As Label = ForestUi.Lbl(If(selectedConvo = "", "No cashier selected.", "No messages yet. Say hello!"),
                                                 9.5F, FontStyle.Regular, ForestUi.Muted)
                hint.AutoSize = False
                hint.Size = New Size(areaW, 60)
                hint.TextAlign = ContentAlignment.MiddleCenter
                flpAdminMessages.Controls.Add(hint)
            End If

            Dim lastDate As DateTime = DateTime.MinValue
            For Each chat As ChatMessage In thread

                If chat.TimeSent.Date <> lastDate Then
                    lastDate = chat.TimeSent.Date
                    Dim dateText As String = If(lastDate = DateTime.Today, "TODAY", lastDate.ToString("dddd").ToUpper()) _
                                          & " " & ChrW(&HB7) & " " & lastDate.ToString("MMMM d").ToUpper()
                    Dim divider As Label = ForestUi.Lbl(dateText, 8.0F, FontStyle.Regular, ForestUi.Muted)
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

            ' looking at the conversation = reading it
            If pnl_Messages.Visible AndAlso selectedConvo <> "" Then DataStore.MarkThreadRead(True, selectedConvo)

            RefreshConversationList()   ' previews, times, unread counts
            UpdateMessageBadge()
        Finally
            msgRendering = False
        End Try
    End Sub

    Private Function BuildBubble(chat As ChatMessage, areaW As Integer, maxBubble As Integer) As Guna2Panel

        Dim fromAdmin As Boolean = Not DataStore.IsFromCashier(chat)

        Const padX As Integer = 16
        Const padY As Integer = 12

        Dim flags As TextFormatFlags = TextFormatFlags.WordBreak Or TextFormatFlags.NoPadding
        Dim txt As String = If(chat.Message, "")
        Dim txtSize As Size = TextRenderer.MeasureText(txt, bubbleFont, New Size(maxBubble - 2 * padX, 0), flags)

        Dim metaText As String = chat.TimeSent.ToString("hh:mm tt")
        If fromAdmin Then
            ' "Read" once the cashier has opened the chat after this message was sent
            Dim readAt As DateTime = DataStore.CashierReadAt(selectedConvo)
            metaText &= " " & ChrW(&HB7) & " " & If(readAt >= chat.TimeSent, "Read", "Sent")
        End If
        If DataStore.MessageEditedAt(chat) <> DateTime.MinValue Then metaText &= " " & ChrW(&HB7) & " Edited"
        Dim metaSize As Size = TextRenderer.MeasureText(metaText, metaFont)

        Dim canEdit As Boolean = fromAdmin AndAlso DataStore.CanEditMessage(chat, True, CurrentAdminName())
        Dim editW As Integer = If(canEdit, TextRenderer.MeasureText("Edit", metaFont).Width + 14, 0)

        Dim contentW As Integer = Math.Max(txtSize.Width, metaSize.Width + editW) + 6
        Dim bubbleW As Integer = contentW + 2 * padX
        Dim bubbleH As Integer = padY + txtSize.Height + 8 + metaSize.Height + padY

        Dim fill As Color = If(fromAdmin, ForestUi.Bubble, ForestUi.CardFill)
        Dim bubble As Guna2Panel = If(fromAdmin,
                                      ForestUi.Card(bubbleW, bubbleH, 12, fill),
                                      ForestUi.Card(bubbleW, bubbleH, 12, fill, Color.FromArgb(238, 242, 246)))

        Dim msgLbl As New Label()
        msgLbl.AutoSize = False
        msgLbl.UseMnemonic = False
        msgLbl.BackColor = Color.Transparent
        msgLbl.ForeColor = If(fromAdmin, Color.White, ForestUi.Ink)
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

        If canEdit Then
            Dim editLbl As New Label()
            editLbl.AutoSize = True
            editLbl.BackColor = Color.Transparent
            editLbl.ForeColor = Color.FromArgb(240, 200, 165)
            editLbl.Font = New Font("Segoe UI", 8.0F, FontStyle.Underline)
            editLbl.Cursor = Cursors.Hand
            editLbl.UseMnemonic = False
            editLbl.Text = "Edit"
            editLbl.Location = New Point(bubbleW - padX - TextRenderer.MeasureText("Edit", metaFont).Width, padY + txtSize.Height + 8)
            AddHandler editLbl.Click, Sub(o As Object, ev As EventArgs) BeginEdit(chat)
            bubble.Controls.Add(editLbl)
        End If

        ' sent = right side, received = left side
        bubble.Margin = New Padding(If(fromAdmin, Math.Max(0, areaW - bubbleW), 0), 8, 0, 8)
        Return bubble
    End Function

End Class