Option Strict On
Option Explicit On

Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms

' ============================================================
'  TransactionHistoryControl.vb
'  The "Transaction history" page.
'    Admin   :  New TransactionHistoryControl(True)   - can Refund / Void (inside the detail form)
'    Cashier :  New TransactionHistoryControl(False)  - view only
'  Data comes from DataStore.QueryTransactions and refreshes on
'  DataStore.TransactionsChanged. Clicking a row opens your
'  TransactionDetailForm.
' ============================================================
Public Class TransactionHistoryControl
    Inherits UserControl

    Private Const PageSize As Integer = 8
    Private Const RowHeight As Integer = 50

    ' column widths in % : id, date, items, qty, total, status
    Private ReadOnly _colWidths As Single() = {15.0F, 17.0F, 29.0F, 8.0F, 14.0F, 17.0F}

    Private ReadOnly _isAdmin As Boolean

    ' layout
    Private root As TableLayoutPanel
    Private pnlHeader As BorderedPanel
    Private btnExport As RoundedButton
    Private btnBell As RoundedButton
    Private divider As Panel
    Private avatar As AvatarCircle
    Private lblUserName As Label
    Private lblUserRole As Label

    ' toolbar
    Private pnlToolbar As Panel
    Private segPeriod As SegmentedControl
    Private btnDate As RoundedButton
    Private btnCashier As RoundedButton
    Private btnStatus As RoundedButton

    ' KPI
    Private cardGross As StatCard
    Private cardNet As StatCard
    Private cardRefunds As StatCard
    Private cardAvg As StatCard

    ' table card
    Private pnlCardTop As Panel
    Private pnlSearch As CardPanel
    Private txtSearch As TextBox
    Private lblSearchHint As Label
    Private lblCount As Label
    Private pnlRows As Panel
    Private lblShowing As Label
    Private flowPager As FlowLayoutPanel

    ' state
    Private _page As Integer = 0
    Private _totalPages As Integer = 1
    Private _anchor As DateTime = DateTime.Today
    Private _cashier As String = ""
    Private _status As String = ""

    Public Sub New(Optional isAdmin As Boolean = True)
        _isAdmin = isAdmin
        DoubleBuffered = True
        BackColor = Theme.PageBg
        Font = Theme.UiFont(9.0F)
        BuildUi()
        RefreshUser()

        If Not DesignMode AndAlso System.ComponentModel.LicenseManager.UsageMode <> System.ComponentModel.LicenseUsageMode.Designtime Then
            AddHandler DataStore.TransactionsChanged, AddressOf OnTransactionsChanged
            RefreshList()
        End If
    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            RemoveHandler DataStore.TransactionsChanged, AddressOf OnTransactionsChanged
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private Sub OnTransactionsChanged(sender As Object, e As EventArgs)
        If IsDisposed Then Return
        If InvokeRequired Then
            BeginInvoke(New MethodInvoker(AddressOf RefreshList))
        Else
            RefreshList()
        End If
    End Sub

    ' ------------------------------------------------------------
    '  Public API
    ' ------------------------------------------------------------
    ''' <summary>Hide the title bar + profile when the page is placed inside a form that already has one.</summary>
    Public Property ShowHeader As Boolean
        Get
            Return pnlHeader.Visible
        End Get
        Set(value As Boolean)
            pnlHeader.Visible = value
            root.RowStyles(0).Height = If(value, 74.0F, 0.0F)
        End Set
    End Property

    Public Sub RefreshUser()
        lblUserName.Text = SessionInfo.DisplayName()
        lblUserRole.Text = SessionInfo.DisplayRole()

        Dim parts As String() = lblUserName.Text.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
        Dim ini As String = ""
        For i As Integer = 0 To Math.Min(1, parts.Length - 1)
            ini &= parts(i).Substring(0, 1).ToUpper()
        Next
        avatar.Initials = ini
    End Sub

    ''' <summary>Re-reads the transactions from DataStore and rebuilds the page.</summary>
    Public Sub RefreshList()
        Dim period As String = PeriodName()

        ' list used for the KPI cards: period + cashier (all statuses, no search)
        Dim baseList As List(Of POS_Transaction) = ByCashier(DataStore.QueryTransactions(period, _anchor, "All statuses", ""))

        Dim gross As Decimal = 0D
        Dim refunds As Decimal = 0D
        Dim completedTotal As Decimal = 0D
        Dim completedCount As Integer = 0
        For Each t As POS_Transaction In baseList
            If IsStatus(t, TransactionStatus.Completed) Then
                gross += t.Total
                completedTotal += t.Total
                completedCount += 1
            ElseIf IsStatus(t, TransactionStatus.Refunded) Then
                gross += t.Total
                refunds += t.Total
            End If
        Next
        Dim net As Decimal = gross - refunds
        cardGross.SetValue(Peso(gross), Theme.TextDark)
        cardNet.SetValue(Peso(net), Theme.TextDark)
        cardRefunds.SetValue(Peso(refunds), If(refunds > 0D, Theme.Red, Theme.TextDark))
        cardAvg.SetValue(Peso(If(completedCount > 0, completedTotal / completedCount, 0D)), Theme.TextDark)

        ' list shown in the table
        Dim items As List(Of POS_Transaction) = ByCashier(DataStore.QueryTransactions(period, _anchor, If(_status = "", "All statuses", _status), txtSearch.Text.Trim()))

        Dim shownTotal As Decimal = 0D
        For Each t As POS_Transaction In items
            If IsStatus(t, TransactionStatus.Completed) Then shownTotal += t.Total
        Next
        lblCount.Text = items.Count.ToString() & " transactions " & ChrW(&HB7).ToString() & " " & Peso(shownTotal) & " total"
        lblCount.Left = pnlCardTop.Width - lblCount.Width - 20

        _totalPages = Math.Max(1, CInt(Math.Ceiling(items.Count / CDbl(PageSize))))
        If _page > _totalPages - 1 Then _page = _totalPages - 1
        If _page < 0 Then _page = 0

        Dim pageItems As List(Of POS_Transaction) = items.Skip(_page * PageSize).Take(PageSize).ToList()

        pnlRows.SuspendLayout()
        For i As Integer = pnlRows.Controls.Count - 1 To 0 Step -1
            Dim c As Control = pnlRows.Controls(i)
            pnlRows.Controls.RemoveAt(i)
            c.Dispose()
        Next

        For i As Integer = 0 To pageItems.Count - 1
            Dim row As Panel = BuildRow(pageItems(i))
            row.Location = New Point(0, i * RowHeight)
            row.Width = pnlRows.ClientSize.Width
            pnlRows.Controls.Add(row)
        Next

        If pageItems.Count = 0 Then
            Dim empty As New Label()
            empty.Text = "No transactions found for this period / filter."
            empty.ForeColor = Theme.TextMuted
            empty.TextAlign = ContentAlignment.MiddleCenter
            empty.SetBounds(0, 40, pnlRows.ClientSize.Width, 40)
            pnlRows.Controls.Add(empty)
        End If
        pnlRows.ResumeLayout()

        ' footer
        Dim firstNo As Integer = If(items.Count = 0, 0, _page * PageSize + 1)
        Dim lastNo As Integer = _page * PageSize + pageItems.Count
        lblShowing.Text = "Showing " & firstNo.ToString() & ChrW(&H2013).ToString() & lastNo.ToString() & " of " & items.Count.ToString()
        BuildPager()
    End Sub

    Private Function PeriodName() As String
        Select Case segPeriod.SelectedIndex
            Case 1 : Return "Weekly"
            Case 2 : Return "Monthly"
            Case Else : Return "Daily"
        End Select
    End Function

    Private Shared Function IsStatus(t As POS_Transaction, status As String) As Boolean
        Return String.Equals(t.Status, status, StringComparison.OrdinalIgnoreCase)
    End Function

    Private Function ByCashier(source As List(Of POS_Transaction)) As List(Of POS_Transaction)
        If _cashier = "" Then Return source
        Dim result As New List(Of POS_Transaction)()
        For Each t As POS_Transaction In source
            If String.Equals(t.Cashier, _cashier, StringComparison.OrdinalIgnoreCase) Then result.Add(t)
        Next
        Return result
    End Function

    ' ------------------------------------------------------------
    '  Build the UI
    ' ------------------------------------------------------------
    Private Sub BuildUi()
        SuspendLayout()

        root = New TableLayoutPanel()
        root.Dock = DockStyle.Fill
        root.ColumnCount = 1
        root.RowCount = 2
        root.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        root.RowStyles.Add(New RowStyle(SizeType.Absolute, 74.0F))
        root.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        root.BackColor = Theme.PageBg
        root.Margin = New Padding(0)

        BuildHeader()
        pnlHeader.Dock = DockStyle.Fill
        pnlHeader.Margin = New Padding(0)
        root.Controls.Add(pnlHeader, 0, 0)

        Dim body As New Panel()
        body.Dock = DockStyle.Fill
        body.Margin = New Padding(0)
        body.Padding = New Padding(24, 20, 24, 24)
        body.BackColor = Theme.PageBg

        Dim t As New TableLayoutPanel()
        t.Dock = DockStyle.Fill
        t.ColumnCount = 1
        t.RowCount = 3
        t.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        t.RowStyles.Add(New RowStyle(SizeType.Absolute, 52.0F))
        t.RowStyles.Add(New RowStyle(SizeType.Absolute, 92.0F))
        t.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        t.BackColor = Theme.PageBg
        t.Margin = New Padding(0)

        BuildToolbar()
        pnlToolbar.Dock = DockStyle.Fill
        pnlToolbar.Margin = New Padding(0)
        t.Controls.Add(pnlToolbar, 0, 0)

        t.Controls.Add(BuildKpiRow(), 0, 1)
        t.Controls.Add(BuildTableCard(), 0, 2)

        body.Controls.Add(t)
        root.Controls.Add(body, 0, 1)

        Controls.Add(root)
        ResumeLayout()
    End Sub

    ' ---------------- header ----------------
    Private Sub BuildHeader()
        pnlHeader = New BorderedPanel()

        Dim lblTitle As New Label()
        lblTitle.Text = "Transaction history"
        lblTitle.Font = Theme.UiFont(15.0F)
        lblTitle.ForeColor = Theme.TextDark
        lblTitle.AutoSize = True
        lblTitle.Location = New Point(24, 12)
        pnlHeader.Controls.Add(lblTitle)

        Dim lblSub As New Label()
        lblSub.Text = "Review sales, refunds, and register activity"
        lblSub.Font = Theme.UiFont(7.5F)
        lblSub.ForeColor = Theme.TextMuted
        lblSub.AutoSize = True
        lblSub.Location = New Point(25, 44)
        pnlHeader.Controls.Add(lblSub)

        btnExport = New RoundedButton()
        btnExport.Text = "Export CSV"
        btnExport.Glyph = Theme.GlyphDownload
        btnExport.GlyphFont = Theme.IconFont(9.0F)
        btnExport.GlyphColor = Theme.TextDark
        btnExport.TextColor = Theme.TextDark
        btnExport.FillColor = Color.White
        btnExport.HoverColor = Theme.HoverGray
        btnExport.BorderColor = Theme.Border
        btnExport.Font = Theme.UiFont(8.5F, FontStyle.Bold)
        btnExport.Size = New Size(112, 36)
        btnExport.Top = 19
        AddHandler btnExport.Click, Sub(s, e) ExportCsv()
        pnlHeader.Controls.Add(btnExport)

        btnBell = New RoundedButton()
        btnBell.Size = New Size(36, 36)
        btnBell.Top = 19
        btnBell.FillColor = Color.White
        btnBell.HoverColor = Theme.HoverGray
        btnBell.BorderColor = Theme.Border
        btnBell.Glyph = Theme.GlyphBell
        btnBell.GlyphColor = Theme.TextMuted
        btnBell.GlyphFont = Theme.IconFont(10.0F)
        AddHandler btnBell.Click, Sub(s, e) ShowRefundsToday()
        pnlHeader.Controls.Add(btnBell)

        divider = New Panel()
        divider.BackColor = Theme.Border
        divider.Size = New Size(1, 36)
        divider.Top = 19
        pnlHeader.Controls.Add(divider)

        avatar = New AvatarCircle()
        avatar.Top = 19
        pnlHeader.Controls.Add(avatar)

        lblUserName = New Label()
        lblUserName.Font = Theme.UiFont(9.0F, FontStyle.Bold)
        lblUserName.ForeColor = Theme.TextDark
        lblUserName.AutoEllipsis = True
        lblUserName.Size = New Size(170, 18)
        lblUserName.Top = 21
        pnlHeader.Controls.Add(lblUserName)

        lblUserRole = New Label()
        lblUserRole.Font = Theme.UiFont(7.5F)
        lblUserRole.ForeColor = Theme.TextMuted
        lblUserRole.AutoEllipsis = True
        lblUserRole.Size = New Size(170, 16)
        lblUserRole.Top = 39
        pnlHeader.Controls.Add(lblUserRole)

        AddHandler pnlHeader.Resize, Sub(s, e) LayoutHeader()
        LayoutHeader()
    End Sub

    Private Sub LayoutHeader()
        Dim x As Integer = pnlHeader.Width - 24
        x -= lblUserName.Width : lblUserName.Left = x : lblUserRole.Left = x
        x -= 10 + avatar.Width : avatar.Left = x
        x -= 16 + divider.Width : divider.Left = x
        x -= 16 + btnBell.Width : btnBell.Left = x
        x -= 12 + btnExport.Width : btnExport.Left = x
    End Sub

    Private Sub ShowRefundsToday()
        Dim refunded As Integer = 0
        Dim amount As Decimal = 0D
        For Each t As POS_Transaction In DataStore.GetDailyTransactions(DateTime.Today)
            If IsStatus(t, TransactionStatus.Refunded) Then
                refunded += 1
                amount += t.Total
            End If
        Next
        MessageBox.Show(FindForm(), "Refunds today: " & refunded.ToString() & " (" & Peso(amount) & ")",
                        "Refunds", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' ---------------- toolbar ----------------
    Private Sub BuildToolbar()
        pnlToolbar = New Panel()
        pnlToolbar.BackColor = Theme.PageBg

        segPeriod = New SegmentedControl()
        segPeriod.Top = 2
        segPeriod.SetItems(New String() {"Daily", "Weekly", "Monthly"}, 0)
        AddHandler segPeriod.SelectedIndexChanged, Sub(s, e)
                                                       _page = 0
                                                       RefreshList()
                                                   End Sub
        pnlToolbar.Controls.Add(segPeriod)

        btnDate = MakeFilterButton(_anchor.ToString("MMM d, yyyy"), Theme.GlyphCalendar)
        AddHandler btnDate.Click, Sub(s, e) ShowCalendar()
        pnlToolbar.Controls.Add(btnDate)

        btnCashier = MakeFilterButton("All cashiers", Theme.GlyphChevron)
        AddHandler btnCashier.Click, Sub(s, e) ShowCashierMenu()
        pnlToolbar.Controls.Add(btnCashier)

        btnStatus = MakeFilterButton("All statuses", Theme.GlyphFilter)
        AddHandler btnStatus.Click, Sub(s, e) ShowStatusMenu()
        pnlToolbar.Controls.Add(btnStatus)

        AddHandler pnlToolbar.Resize, Sub(s, e) LayoutToolbar()
        LayoutToolbar()
    End Sub

    Private Function MakeFilterButton(caption As String, glyph As String) As RoundedButton
        Dim b As New RoundedButton()
        b.Text = caption
        b.Glyph = glyph
        b.GlyphFont = Theme.IconFont(8.0F)
        b.GlyphColor = Theme.TextDark
        b.TextColor = Theme.TextDark
        b.FillColor = Color.White
        b.HoverColor = Theme.HoverGray
        b.BorderColor = Theme.Border
        b.Font = Theme.UiFont(8.5F, FontStyle.Bold)
        b.Top = 2
        b.Height = 34
        FitButton(b)
        Return b
    End Function

    Private Sub FitButton(b As RoundedButton)
        Dim w As Integer = TextRenderer.MeasureText(b.Text, b.Font, New Size(1000, 100), TextFormatFlags.NoPadding).Width
        b.Width = Math.Max(110, w + 58)
    End Sub

    Private Sub LayoutToolbar()
        btnStatus.Left = pnlToolbar.Width - btnStatus.Width
        btnCashier.Left = btnStatus.Left - 10 - btnCashier.Width
        btnDate.Left = btnCashier.Left - 10 - btnDate.Width
    End Sub

    Private Sub ShowCalendar()
        Dim cal As New MonthCalendar()
        cal.MaxSelectionCount = 1
        cal.SetDate(_anchor)

        Dim host As New ToolStripControlHost(cal)
        host.Margin = New Padding(0)
        host.Padding = New Padding(0)

        Dim dd As New ToolStripDropDown()
        dd.Items.Add(host)

        AddHandler cal.DateSelected, Sub(s, e)
                                         _anchor = e.Start.Date
                                         btnDate.Text = _anchor.ToString("MMM d, yyyy")
                                         FitButton(btnDate)
                                         LayoutToolbar()
                                         dd.Close()
                                         _page = 0
                                         RefreshList()
                                     End Sub
        AddHandler dd.Closed, Sub(s, e) BeginInvoke(New MethodInvoker(Sub() dd.Dispose()))
        dd.Show(btnDate, New Point(0, btnDate.Height + 2))
    End Sub

    Private Sub ShowMenu(anchorBtn As Control, options As List(Of String), current As String, onPick As Action(Of String))
        Dim m As New ContextMenuStrip()
        m.Font = Theme.UiFont(9.0F)
        For Each opt As String In options
            Dim value As String = opt
            Dim item As New ToolStripMenuItem(value)
            item.Checked = String.Equals(value, current, StringComparison.OrdinalIgnoreCase)
            AddHandler item.Click, Sub(s, e) onPick(value)
            m.Items.Add(item)
        Next
        AddHandler m.Closed, Sub(s, e) BeginInvoke(New MethodInvoker(Sub() m.Dispose()))
        m.Show(anchorBtn, New Point(0, anchorBtn.Height + 2))
    End Sub

    Private Sub ShowCashierMenu()
        Dim opts As New List(Of String)()
        opts.Add("All cashiers")
        Dim seen As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        For Each t As POS_Transaction In DataStore.Transactions
            If Not String.IsNullOrWhiteSpace(t.Cashier) AndAlso seen.Add(t.Cashier.Trim()) Then opts.Add(t.Cashier.Trim())
        Next
        ShowMenu(btnCashier, opts, If(_cashier = "", "All cashiers", _cashier),
                 Sub(v As String)
                     _cashier = If(v = "All cashiers", "", v)
                     btnCashier.Text = v
                     FitButton(btnCashier)
                     LayoutToolbar()
                     _page = 0
                     RefreshList()
                 End Sub)
    End Sub

    Private Sub ShowStatusMenu()
        Dim opts As New List(Of String) From {"All statuses", TransactionStatus.Completed, TransactionStatus.Refunded, TransactionStatus.Voided}
        ShowMenu(btnStatus, opts, If(_status = "", "All statuses", _status),
                 Sub(v As String)
                     _status = If(v = "All statuses", "", v)
                     btnStatus.Text = v
                     FitButton(btnStatus)
                     LayoutToolbar()
                     _page = 0
                     RefreshList()
                 End Sub)
    End Sub

    ' ---------------- KPI cards ----------------
    Private Function BuildKpiRow() As TableLayoutPanel
        Dim kpi As New TableLayoutPanel()
        kpi.Dock = DockStyle.Fill
        kpi.Margin = New Padding(0)
        kpi.ColumnCount = 4
        kpi.RowCount = 1
        For i As Integer = 1 To 4
            kpi.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
        Next
        kpi.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        kpi.BackColor = Theme.PageBg

        cardGross = MakeCard("Gross sales")
        cardNet = MakeCard("Net sales")
        cardRefunds = MakeCard("Refunds")
        cardAvg = MakeCard("Avg. order")
        kpi.Controls.Add(cardGross, 0, 0)
        kpi.Controls.Add(cardNet, 1, 0)
        kpi.Controls.Add(cardRefunds, 2, 0)
        kpi.Controls.Add(cardAvg, 3, 0)
        cardAvg.Margin = New Padding(0)
        Return kpi
    End Function

    Private Function MakeCard(caption As String) As StatCard
        Dim c As New StatCard()
        c.Setup(caption, "", False, Theme.TealSoft, Theme.Teal)     ' no icon tile on this page
        c.SetValue(Peso(0D), Theme.TextDark)
        c.Dock = DockStyle.Top
        c.Height = 76
        c.Margin = New Padding(0, 0, 14, 0)
        Return c
    End Function

    ' ---------------- table card ----------------
    Private Function BuildTableCard() As CardPanel
        Dim card As New CardPanel()
        card.Radius = 14
        card.Padding = New Padding(1)
        card.Dock = DockStyle.Fill
        card.Margin = New Padding(0)

        Dim t As New TableLayoutPanel()
        t.Dock = DockStyle.Fill
        t.ColumnCount = 1
        t.RowCount = 4
        t.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        t.RowStyles.Add(New RowStyle(SizeType.Absolute, 66.0F))
        t.RowStyles.Add(New RowStyle(SizeType.Absolute, 34.0F))
        t.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        t.RowStyles.Add(New RowStyle(SizeType.Absolute, 54.0F))
        t.BackColor = Color.Transparent
        t.Margin = New Padding(0)

        ' -- top bar: search + "186 transactions . total" --
        pnlCardTop = New Panel()
        pnlCardTop.Dock = DockStyle.Fill
        pnlCardTop.Margin = New Padding(0)
        pnlCardTop.BackColor = Color.Transparent

        pnlSearch = New CardPanel()
        pnlSearch.Radius = 8
        pnlSearch.Size = New Size(330, 36)
        pnlSearch.Location = New Point(16, 15)
        pnlCardTop.Controls.Add(pnlSearch)

        Dim icon As New Label()
        icon.Text = Theme.GlyphSearch
        icon.Font = Theme.IconFont(9.0F)
        icon.ForeColor = Theme.TextMuted
        icon.AutoSize = False
        icon.Size = New Size(20, 36)
        icon.TextAlign = ContentAlignment.MiddleCenter
        icon.Location = New Point(10, 0)
        pnlSearch.Controls.Add(icon)

        txtSearch = New TextBox()
        txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        txtSearch.Font = Theme.UiFont(9.0F)
        txtSearch.BackColor = Color.White
        txtSearch.Location = New Point(36, 9)
        txtSearch.Width = 280
        pnlSearch.Controls.Add(txtSearch)

        lblSearchHint = New HintLabel()
        lblSearchHint.Text = "Search transaction ID, cashier, or item"
        lblSearchHint.Font = Theme.UiFont(8.5F)
        lblSearchHint.ForeColor = Theme.TextMuted
        lblSearchHint.BackColor = Color.White
        lblSearchHint.AutoSize = False
        lblSearchHint.Size = New Size(280, 18)
        lblSearchHint.Location = New Point(36, 9)
        AddHandler lblSearchHint.Click, Sub(s, e) txtSearch.Focus()
        AddHandler pnlSearch.Click, Sub(s, e) txtSearch.Focus()
        AddHandler icon.Click, Sub(s, e) txtSearch.Focus()
        pnlSearch.Controls.Add(lblSearchHint)
        lblSearchHint.BringToFront()

        AddHandler txtSearch.TextChanged, Sub(s, e)
                                              lblSearchHint.Visible = (txtSearch.Text.Length = 0)
                                              _page = 0
                                              RefreshList()
                                          End Sub

        lblCount = New Label()
        lblCount.AutoSize = True
        lblCount.Font = Theme.UiFont(7.5F)
        lblCount.ForeColor = Theme.TextMuted
        lblCount.Top = 27
        lblCount.Text = "0 transactions"
        pnlCardTop.Controls.Add(lblCount)
        AddHandler pnlCardTop.Resize, Sub(s, e) lblCount.Left = pnlCardTop.Width - lblCount.Width - 20
        t.Controls.Add(pnlCardTop, 0, 0)

        ' -- column header --
        Dim colHeader As New Panel()
        colHeader.Dock = DockStyle.Fill
        colHeader.Margin = New Padding(0)
        colHeader.BackColor = Theme.HeaderRowBg

        Dim hdr As TableLayoutPanel = NewColumnsTable()
        Dim titles As String() = {"Transaction ID", "Date & time", "Items", "Qty", "Total", "Status"}
        For i As Integer = 0 To titles.Length - 1
            Dim l As New Label()
            l.Text = titles(i)
            l.Font = Theme.UiFont(7.5F, FontStyle.Bold)
            l.ForeColor = Theme.TextMuted
            l.Dock = DockStyle.Fill
            l.Margin = New Padding(0)
            l.TextAlign = ContentAlignment.MiddleLeft
            hdr.Controls.Add(l, i, 0)
        Next
        colHeader.Controls.Add(hdr)
        t.Controls.Add(colHeader, 0, 1)

        ' -- rows --
        pnlRows = New Panel()
        pnlRows.Dock = DockStyle.Fill
        pnlRows.Margin = New Padding(0)
        pnlRows.BackColor = Color.White
        pnlRows.AutoScroll = True
        AddHandler pnlRows.Resize, Sub(s, e)
                                       For Each c As Control In pnlRows.Controls
                                           If TypeOf c Is BorderedPanel Then c.Width = pnlRows.ClientSize.Width
                                       Next
                                   End Sub
        t.Controls.Add(pnlRows, 0, 2)

        ' -- footer: Showing x of y | Previous 1 2 3 Next --
        Dim footer As New TableLayoutPanel()
        footer.Dock = DockStyle.Fill
        footer.Margin = New Padding(0)
        footer.ColumnCount = 2
        footer.RowCount = 1
        footer.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        footer.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
        footer.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        footer.BackColor = Color.Transparent

        lblShowing = New Label()
        lblShowing.Dock = DockStyle.Fill
        lblShowing.Margin = New Padding(0)
        lblShowing.Padding = New Padding(16, 0, 0, 0)
        lblShowing.TextAlign = ContentAlignment.MiddleLeft
        lblShowing.Font = Theme.UiFont(7.5F)
        lblShowing.ForeColor = Theme.TextMuted
        footer.Controls.Add(lblShowing, 0, 0)

        flowPager = New FlowLayoutPanel()
        flowPager.AutoSize = True
        flowPager.WrapContents = False
        flowPager.FlowDirection = FlowDirection.LeftToRight
        flowPager.Dock = DockStyle.Fill
        flowPager.Margin = New Padding(0, 0, 14, 0)
        flowPager.BackColor = Color.Transparent
        footer.Controls.Add(flowPager, 1, 0)
        t.Controls.Add(footer, 0, 3)

        card.Controls.Add(t)
        Return card
    End Function

    Private Sub BuildPager()
        For i As Integer = flowPager.Controls.Count - 1 To 0 Step -1
            Dim c As Control = flowPager.Controls(i)
            flowPager.Controls.RemoveAt(i)
            c.Dispose()
        Next

        Dim prev As Label = PagerLabel("Previous", _page > 0, False)
        AddHandler prev.Click, Sub(s, e)
                                   If _page > 0 Then
                                       _page -= 1
                                       RefreshList()
                                   End If
                               End Sub
        flowPager.Controls.Add(prev)

        Dim startPage As Integer = Math.Max(0, Math.Min(_page - 2, _totalPages - 5))
        Dim endPage As Integer = Math.Min(_totalPages - 1, startPage + 4)
        For p As Integer = startPage To endPage
            Dim target As Integer = p
            Dim num As Label = PagerLabel((p + 1).ToString(), True, p = _page)
            AddHandler num.Click, Sub(s, e)
                                      _page = target
                                      RefreshList()
                                  End Sub
            flowPager.Controls.Add(num)
        Next

        Dim nxt As Label = PagerLabel("Next", _page < _totalPages - 1, False)
        AddHandler nxt.Click, Sub(s, e)
                                  If _page < _totalPages - 1 Then
                                      _page += 1
                                      RefreshList()
                                  End If
                              End Sub
        flowPager.Controls.Add(nxt)
    End Sub

    Private Function PagerLabel(caption As String, enabled As Boolean, current As Boolean) As Label
        Dim l As New Label()
        l.Text = caption
        l.AutoSize = True
        l.Font = Theme.UiFont(7.5F, If(current, FontStyle.Bold, FontStyle.Regular))
        l.ForeColor = If(current, Theme.Teal, If(enabled, Theme.TextDark, Theme.TextMuted))
        l.Margin = New Padding(8, 19, 0, 0)
        l.Cursor = If(enabled, Cursors.Hand, Cursors.Default)
        Return l
    End Function

    ' ------------------------------------------------------------
    '  Table rows
    ' ------------------------------------------------------------
    Private Function NewColumnsTable() As TableLayoutPanel
        Dim t As New TableLayoutPanel()
        t.Dock = DockStyle.Fill
        t.Margin = New Padding(0)
        t.Padding = New Padding(16, 0, 8, 0)
        t.ColumnCount = _colWidths.Length
        t.RowCount = 1
        t.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        For Each w As Single In _colWidths
            t.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, w))
        Next
        t.BackColor = Color.Transparent
        Return t
    End Function

    Private Function NewCell() As Panel
        Dim p As New Panel()
        p.Dock = DockStyle.Fill
        p.Margin = New Padding(0)
        p.BackColor = Color.Transparent
        Return p
    End Function

    Private Function CellLabel(text As String, f As Font, fore As Color) As Label
        Dim l As New Label()
        l.Text = text
        l.Font = f
        l.ForeColor = fore
        l.Dock = DockStyle.Fill
        l.Margin = New Padding(0)
        l.TextAlign = ContentAlignment.MiddleLeft
        l.AutoEllipsis = True
        l.BackColor = Color.Transparent
        Return l
    End Function

    Private Function BuildRow(tr As POS_Transaction) As Panel
        Dim innerH As Integer = RowHeight - 1

        Dim row As New BorderedPanel()
        row.Height = RowHeight
        row.Padding = New Padding(0, 0, 0, 1)

        Dim t As TableLayoutPanel = NewColumnsTable()
        row.Controls.Add(t)

        Dim qty As Integer = 0
        For Each it As TransactionItem In tr.Items
            qty += it.Quantity
        Next

        t.Controls.Add(CellLabel(tr.TransactionID, Theme.UiFont(8.5F, FontStyle.Bold), Theme.Teal), 0, 0)
        t.Controls.Add(CellLabel(tr.TransactionDate.ToString("MMM d, h:mm tt"), Theme.UiFont(8.5F), Theme.TextDark), 1, 0)
        t.Controls.Add(CellLabel(DataStore.BuildItemsText(tr), Theme.UiFont(8.0F), Theme.TextMuted), 2, 0)
        t.Controls.Add(CellLabel(qty.ToString(), Theme.UiFont(8.5F), Theme.TextDark), 3, 0)
        t.Controls.Add(CellLabel(Peso(tr.Total), Theme.UiFont(8.5F, FontStyle.Bold), Theme.TextDark), 4, 0)

        Dim cellBadge As Panel = NewCell()
        Dim pill As New Badge()
        If IsStatus(tr, TransactionStatus.Completed) Then
            pill.Setup(tr.Status, Theme.GreenSoft, Theme.Green)
        ElseIf IsStatus(tr, TransactionStatus.Refunded) Then
            pill.Setup(tr.Status, Theme.RedSoft, Theme.Red)
        Else
            pill.Setup(tr.Status, Theme.BadgeFixedBg, Theme.BadgeFixedText)
        End If
        pill.Location = New Point(0, (innerH - pill.Height) \ 2)
        cellBadge.Controls.Add(pill)
        t.Controls.Add(cellBadge, 5, 0)

        ' the whole row opens the detail form
        Dim target As POS_Transaction = tr
        HookClick(row, Sub(s, e) OpenDetail(target))
        Return row
    End Function

    Private Sub HookClick(c As Control, handler As EventHandler)
        AddHandler c.Click, handler
        c.Cursor = Cursors.Hand
        For Each child As Control In c.Controls
            HookClick(child, handler)
        Next
    End Sub

    Private Sub OpenDetail(tr As POS_Transaction)
        Using f As New TransactionDetailForm(tr, _isAdmin)
            f.ShowDialog(FindForm())
        End Using
    End Sub

    ' ------------------------------------------------------------
    '  Export
    ' ------------------------------------------------------------
    Private Shared Function Csv(value As String) As String
        Return """" & If(value, "").Replace("""", """""") & """"
    End Function

    Private Sub ExportCsv()
        Dim items As List(Of POS_Transaction) = ByCashier(DataStore.QueryTransactions(PeriodName(), _anchor,
                                                  If(_status = "", "All statuses", _status), txtSearch.Text.Trim()))
        Using dlg As New SaveFileDialog()
            dlg.Title = "Export transactions"
            dlg.Filter = "CSV file (Excel)|*.csv"
            dlg.FileName = "transactions_" & _anchor.ToString("yyyyMMdd") & ".csv"
            If dlg.ShowDialog(FindForm()) <> System.Windows.Forms.DialogResult.OK Then Return

            Dim lines As New List(Of String)()
            lines.Add("Transaction ID,Date,Cashier,Items,Qty,Subtotal,Tax,Total,Payment,Status")
            For Each tr As POS_Transaction In items
                Dim qty As Integer = 0
                For Each it As TransactionItem In tr.Items
                    qty += it.Quantity
                Next
                lines.Add(String.Join(",",
                    Csv(tr.TransactionID),
                    Csv(tr.TransactionDate.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)),
                    Csv(tr.Cashier), Csv(DataStore.BuildItemsText(tr)), qty.ToString(),
                    tr.Subtotal.ToString("0.00", CultureInfo.InvariantCulture),
                    tr.Tax.ToString("0.00", CultureInfo.InvariantCulture),
                    tr.Total.ToString("0.00", CultureInfo.InvariantCulture),
                    Csv(tr.PaymentMethod), Csv(tr.Status)))
            Next

            Try
                File.WriteAllLines(dlg.FileName, lines, New UTF8Encoding(True))
                MessageBox.Show(FindForm(), items.Count.ToString() & " transactions exported.",
                                "Export CSV", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show(FindForm(), "The file could not be saved:" & vbCrLf & ex.Message,
                                "Export CSV", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End Using
    End Sub

End Class