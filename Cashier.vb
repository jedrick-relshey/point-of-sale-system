Option Strict On
Option Explicit On

Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports Guna.UI2.WinForms

' Cashier.vb  -  REPLACES your old Cashier.vb  (use together with the new Cashier_Designer.vb).
' The visual layout (positions, colours, fonts, cart panel, payment tiles, nav buttons) is defined in
' Cashier_Designer.vb so it shows in the Design view. This file only fills in what has to be code:
' drawn icons, the banner text, product cards, cart rows and the checkout logic.
'   btn_chkout  = "Continue to Payment"  (pays with the selected tile: tileCash / tileCard)
'   Panel1      = Inventory screen (read-only; layout in the designer, rows in code)
'   pnl_History = Order history  (layout in the designer, rows/receipt in code)
Public Class Cashier

    Private cart As New Dictionary(Of Product, Integer)
    Private selectedCategory As String = "All"

    Private isProcessing As Boolean = False
    Private closingForLogout As Boolean = False

    Private ReadOnly originalFore As New Dictionary(Of Guna2Button, Color)
    Private ReadOnly originalFill As New Dictionary(Of Guna2Button, Color)
    Private navButtons As Guna2Button()

    Private ReadOnly brown As Color = Color.FromArgb(62, 39, 35)

    ' ---- Menu-screen palette (same colours as Cashier_Designer.vb) ----
    Private Shared ReadOnly Accent As Color = Color.FromArgb(193, 106, 58)
    Private Shared ReadOnly AccentSoft As Color = Color.FromArgb(246, 226, 212)
    Private Shared ReadOnly Ink As Color = Color.FromArgb(52, 34, 28)
    Private Shared ReadOnly Muted As Color = Color.FromArgb(140, 120, 108)
    Private Shared ReadOnly CardFill As Color = Color.FromArgb(255, 252, 248)
    Private Shared ReadOnly CardBorder As Color = Color.FromArgb(234, 222, 210)
    Private Shared ReadOnly PageBg As Color = Color.FromArgb(248, 243, 235)
    Private Shared ReadOnly PhotoBg As Color = Color.FromArgb(244, 236, 226)

    Private Const BannerTitle As String = "Forest Roast Cafe"
    Private Const BannerSubtitle As String = "Freshly brewed. Crafted for your day."

    Private ReadOnly CategoryKeys As String() = {"All", "Hot Coffee", "Iced Coffee", "Specialty", "Non Coffee"}
    Private catButtons As Guna2Button()
    Private ReadOnly cardImages As New List(Of Image)
    Private ReadOnly cartThumbs As New List(Of Image)
    Private bannerSource As Image
    Private bannerRendered As Bitmap
    Private menuReady As Boolean = False
    Private lastMenuWidth As Integer = 0
    Private selectedPayment As String = PaymentMethods.Cash
    Private selectedCardType As String = ""
    Private profilePhoto As Image

    '=================================================================
    ' LOAD / CLOSE
    '=================================================================
    Private Sub Cashier_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DataStore.Initialize()

        fl_Menu.FlowDirection = FlowDirection.LeftToRight
        fl_Menu.WrapContents = True
        fl_Menu.AutoScroll = True

        fl_MenuProduct.FlowDirection = FlowDirection.TopDown
        fl_MenuProduct.WrapContents = False
        fl_MenuProduct.AutoScroll = True

        ApplyCashierTheme()

        navButtons = {btn_DashBoard, btn_Point_Of_Sale, btn_invtry, btn_hstry, btn_CashierMessages}
        For Each b As Guna2Button In navButtons
            originalFore(b) = b.ForeColor
            originalFill(b) = b.FillColor
        Next

        SetupDashboard()
        SetupInventory()
        SetupHistory()
        BuildMessagingLayout()

        AddHandler DataStore.TransactionsChanged, AddressOf Cashier_TransactionsChanged
        AddHandler DataStore.ProductsChanged, AddressOf Cashier_ProductsChanged
        AddHandler DataStore.MessagesChanged, AddressOf Cashier_MessagesChanged

        LoadProducts()
        fl_MenuProduct.Controls.Clear()
        ResetPaymentDisplay()
        RefreshCashierData()
        RefreshCashierInventory()

        ShowCashierPanel(pnl_PointOfSale, btn_Point_Of_Sale)
    End Sub

    Private Sub Cashier_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        RemoveHandler DataStore.TransactionsChanged, AddressOf Cashier_TransactionsChanged
        RemoveHandler DataStore.ProductsChanged, AddressOf Cashier_ProductsChanged
        RemoveHandler DataStore.MessagesChanged, AddressOf Cashier_MessagesChanged
        If catBarImage IsNot Nothing Then catBarImage.Dispose()
        If trendImage IsNot Nothing Then trendImage.Dispose()
        DisposeCardImages()
        DisposeCartThumbs()
        If profilePhoto IsNot Nothing Then profilePhoto.Dispose()

        If Not closingForLogout Then Application.Exit()
    End Sub

    Private Sub btnCashierLogout_Click(sender As Object, e As EventArgs) Handles btnCashierLogout.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to logout?",
            "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            closingForLogout = True
            AppNavigation.ShowLogin()
            Me.Close()
        End If
    End Sub

    '=================================================================
    ' NAVIGATION
    '=================================================================
    Private Sub ShowCashierPanel(target As Control, activeButton As Guna2Button)

        main_pnl.Visible = True
        ClosePopups(Nothing, EventArgs.Empty)

        For Each c As Control In {pnl_PointOfSale, pnl_CashierMessages, dashbrd_pnl, Panel1, pnl_History}
            c.Visible = (c Is target)
        Next
        target.BringToFront()

        If target Is pnl_PointOfSale Then
            AttachTopBar(pnl_PointOfSale, 20 + Guna2Panel6.Width)
        ElseIf target Is dashbrd_pnl OrElse target Is Panel1 OrElse target Is pnl_History Then
            AttachTopBar(target, 20)
        End If

        For Each b As Guna2Button In navButtons
            b.FillColor = originalFill(b)
            b.ForeColor = originalFore(b)
        Next
        activeButton.FillColor = Accent
        activeButton.ForeColor = Color.White
        For Each b As Guna2Button In navButtons
            b.HoverState.FillColor = If(b Is activeButton, Accent, Color.FromArgb(66, 45, 38))
            b.HoverState.ForeColor = Color.White
        Next
    End Sub

    Private Sub btn_DashBoard_Click(sender As Object, e As EventArgs) Handles btn_DashBoard.Click
        ShowCashierPanel(dashbrd_pnl, btn_DashBoard)
        RefreshCashierData()
    End Sub

    Private Sub btn_Point_Of_Sale_Click(sender As Object, e As EventArgs) Handles btn_Point_Of_Sale.Click
        ShowCashierPanel(pnl_PointOfSale, btn_Point_Of_Sale)
        LoadProducts()
    End Sub

    Private Sub btn_invtry_Click(sender As Object, e As EventArgs) Handles btn_invtry.Click
        ShowCashierPanel(Panel1, btn_invtry)
        RefreshCashierInventory()
    End Sub

    Private Sub btn_hstry_Click(sender As Object, e As EventArgs) Handles btn_hstry.Click
        ShowCashierPanel(pnl_History, btn_hstry)
        RefreshHistory()
    End Sub

    Private Sub btn_CashierMessages_Click(sender As Object, e As EventArgs) Handles btn_CashierMessages.Click
        ShowCashierPanel(pnl_CashierMessages, btn_CashierMessages)
        LoadCashierChat()
    End Sub

    '=================================================================
    ' DATA-CHANGED EVENTS
    '=================================================================
    Private Sub Cashier_TransactionsChanged(sender As Object, e As EventArgs)
        RefreshCashierData()
        RefreshHistory()
    End Sub

    Private Sub Cashier_ProductsChanged(sender As Object, e As EventArgs)
        LoadProducts()
        SyncCartWithCatalog()
        RefreshCashierDashboard()
        RefreshCashierInventory()
    End Sub

    Private Sub Cashier_MessagesChanged(sender As Object, e As EventArgs)
        If pnl_CashierMessages.Visible Then LoadCashierChat()
    End Sub

    '=================================================================
    ' DASHBOARD
    '=================================================================
    '=================================================================
    ' DASHBOARD  (layout is in Cashier_Designer.vb  |  numbers, chart, lists: here)
    '=================================================================
    Private Class DayStats
        Public Sales As Decimal
        Public Orders As Integer
        Public Items As Integer
    End Class

    Private trendImage As Bitmap
    Private lastDashWidth As Integer = 0
    Private Const HourStart As Integer = 7
    Private Const HourEnd As Integer = 21

    Private Sub SetupDashboard()

        StyleGrid(dashGrid, 38)
        dashGrid.Columns.Add(MakeCol("colDOrder", "Order", 84, 0, True))
        dashGrid.Columns.Add(MakeCol("colDTime", "Time", 112, 0, False))
        dashGrid.Columns.Add(MakeCol("colDItems", "Items", 0, 140, False))
        dashGrid.Columns.Add(MakeCol("colDPay", "Payment", 66, 0, False))
        dashGrid.Columns.Add(MakeCol("colDTotal", "Total", 84, 0, True))
        dashGrid.Columns.Add(MakeCol("colDStatus", "Status", 100, 0, False))

        lblDashLegToday.ForeColor = Accent
        lblDashLegYest.ForeColor = Color.FromArgb(222, 190, 165)

        AddHandler dashbrd_pnl.Resize, Sub(s As Object, ev As EventArgs) LayoutDash()
        AddHandler picDashTrend.SizeChanged, Sub(s As Object, ev As EventArgs) RenderTrend()
        LayoutDash()
    End Sub

    Private Sub LayoutDash()
        If pnlDashTrend Is Nothing Then Return
        Const pad As Integer = 20
        Const gap As Integer = 14
        Dim total As Integer = dashbrd_pnl.ClientSize.Width - pad * 2
        If total < 600 Then Return

        LayoutKpiRow(dashbrd_pnl, New Control() {pnlDK1, pnlDK2, pnlDK3, pnlDK4})

        Dim leftW As Integer = CInt((total - gap) * 0.62)
        Dim rightW As Integer = total - gap - leftW
        Dim rx As Integer = pad + leftW + gap
        For Each c As Control In New Control() {pnlDashTrend, pnlDashRecent}
            c.SetBounds(pad, c.Top, leftW, c.Height)
        Next
        For Each c As Control In New Control() {pnlDashCat, pnlDashTop, pnlDashAlert}
            c.SetBounds(rx, c.Top, rightW, c.Height)
        Next

        If dashbrd_pnl.ClientSize.Width <> lastDashWidth Then
            lastDashWidth = dashbrd_pnl.ClientSize.Width
            RefreshCashierDashboard()
        End If
    End Sub

    Private Sub LoadTransactions()
        If dashGrid Is Nothing OrElse dashGrid.Columns.Count = 0 Then Return
        dashGrid.Rows.Clear()

        Dim recent As New List(Of POS_Transaction)(DataStore.Transactions)
        recent.Sort(Function(a, b) b.TransactionDate.CompareTo(a.TransactionDate))

        Dim count As Integer = 0
        For Each t As POS_Transaction In recent
            Dim timeText As String = If(t.TransactionDate.Date = DateTime.Today,
                                        t.TransactionDate.ToString("hh:mm tt"),
                                        t.TransactionDate.ToString("MMM d, hh:mm tt"))
            Dim idx As Integer = dashGrid.Rows.Add(t.TransactionID, timeText, DataStore.BuildItemsText(t),
                                                   t.PaymentMethod, Peso(t.Total), t.Status)
            dashGrid.Rows(idx).Tag = t
            count += 1
            If count >= 10 Then Exit For
        Next
    End Sub

    Private Sub dashGrid_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dashGrid.CellPainting
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
        If dashGrid.Columns(e.ColumnIndex).Name <> "colDStatus" Then Return
        Dim status As String = Convert.ToString(e.Value)
        e.PaintBackground(e.CellBounds, True)
        DrawPill(e.Graphics, e.CellBounds, status, PillBack(status), PillFore(status))
        e.Paint(e.CellBounds, DataGridViewPaintParts.Border)
        e.Handled = True
    End Sub

    Private Sub lnkDashViewAll_Click(sender As Object, e As EventArgs) Handles lnkDashViewAll.Click
        btn_hstry.PerformClick()
    End Sub

    Private Sub btnDashExport_Click(sender As Object, e As EventArgs) Handles btnDashExport.Click
        Dim rows As New List(Of String())
        For Each t As POS_Transaction In DataStore.Transactions
            If t.TransactionDate.Date <> DateTime.Today Then Continue For
            rows.Add(New String() {t.TransactionID, t.TransactionDate.ToString("yyyy-MM-dd HH:mm"), t.Cashier, DataStore.BuildItemsText(t),
                                   t.PaymentMethod, t.Subtotal.ToString("0.00"), t.Tax.ToString("0.00"), t.Total.ToString("0.00"), t.Status})
        Next
        SaveCsv("daily_report", New String() {"Order", "Date", "Cashier", "Items", "Payment", "Subtotal", "Tax", "Total", "Status"}, rows)
    End Sub

    Private Function StatsFor(day As DateTime) As DayStats
        Dim st As New DayStats()
        For Each t As POS_Transaction In DataStore.Transactions
            If t.TransactionDate.Date <> day.Date Then Continue For
            If Not String.Equals(t.Status, TransactionStatus.Completed, StringComparison.OrdinalIgnoreCase) Then Continue For
            st.Sales += t.Total
            st.Orders += 1
            For Each item As TransactionItem In t.Items
                st.Items += item.Quantity
            Next
        Next
        Return st
    End Function

    Private Sub SetTrend(lbl As Label, today As Decimal, yesterday As Decimal)
        If yesterday <= 0D Then
            lbl.Text = If(today > 0D, "New today", "No data yet")
            lbl.ForeColor = Muted
            Return
        End If
        Dim pct As Decimal = (today - yesterday) / yesterday * 100D
        lbl.Text = If(pct >= 0D, ChrW(&H25B2), ChrW(&H25BC)) & " " & Math.Abs(pct).ToString("0.#") & "% vs yesterday"
        lbl.ForeColor = If(pct >= 0D, Color.FromArgb(92, 122, 84), Color.FromArgb(176, 72, 48))
    End Sub

    Private Sub RefreshCashierDashboard()
        If lblDashGreeting Is Nothing Then Return

        ' ---- greeting ----
        Dim hr As Integer = DateTime.Now.Hour
        Dim part As String = If(hr < 12, "Good morning", If(hr < 18, "Good afternoon", "Good evening"))
        Dim names As String() = lblUserName.Text.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
        lblDashGreeting.Text = part & If(names.Length > 0, ", " & names(0), "")
        lblDashSub.Text = "Here's how Forest Roast is doing today  " & ChrW(&HB7) & "  " & DateTime.Now.ToString("MMMM d, yyyy")

        ' ---- KPI cards (today vs yesterday) ----
        Dim today As DayStats = StatsFor(DateTime.Today)
        Dim yest As DayStats = StatsFor(DateTime.Today.AddDays(-1))
        Dim avgToday As Decimal = If(today.Orders = 0, 0D, today.Sales / today.Orders)
        Dim avgYest As Decimal = If(yest.Orders = 0, 0D, yest.Sales / yest.Orders)

        lblDK1V.Text = Peso(today.Sales)
        SetTrend(lblDK1S, today.Sales, yest.Sales)
        lblDK2V.Text = today.Orders.ToString()
        SetTrend(lblDK2S, today.Orders, yest.Orders)
        lblDK3V.Text = today.Items.ToString()
        SetTrend(lblDK3S, today.Items, yest.Items)
        lblDK4V.Text = Peso(avgToday)
        SetTrend(lblDK4S, avgToday, avgYest)

        RenderTrend()
        BuildDashCategories()
        BuildDashTop()
        BuildDashAlerts()
    End Sub

    '---------------- sales trend chart ----------------
    Private Function CumulativeByHour(day As DateTime, upToHour As Integer) As Decimal()
        Dim count As Integer = Math.Min(HourEnd, Math.Max(HourStart, upToHour)) - HourStart + 1
        Dim buckets(HourEnd - HourStart) As Decimal
        For Each t As POS_Transaction In DataStore.Transactions
            If t.TransactionDate.Date <> day.Date Then Continue For
            If Not String.Equals(t.Status, TransactionStatus.Completed, StringComparison.OrdinalIgnoreCase) Then Continue For
            Dim h As Integer = Math.Min(HourEnd, Math.Max(HourStart, t.TransactionDate.Hour))
            buckets(h - HourStart) += t.Total
        Next
        Dim result(count - 1) As Decimal
        Dim running As Decimal = 0D
        For i As Integer = 0 To count - 1
            running += buckets(i)
            result(i) = running
        Next
        Return result
    End Function

    Private Sub RenderTrend()
        If picDashTrend Is Nothing OrElse picDashTrend.Width < 80 OrElse picDashTrend.Height < 50 Then Return

        Dim w As Integer = picDashTrend.Width
        Dim h As Integer = picDashTrend.Height
        Dim bmp As New Bitmap(w, h)
        Dim todaySeries As Decimal() = CumulativeByHour(DateTime.Today, DateTime.Now.Hour)
        Dim yestSeries As Decimal() = CumulativeByHour(DateTime.Today.AddDays(-1), HourEnd)

        Dim maxVal As Decimal = 0D
        For Each v As Decimal In todaySeries
            If v > maxVal Then maxVal = v
        Next
        For Each v As Decimal In yestSeries
            If v > maxVal Then maxVal = v
        Next

        Using g As Graphics = Graphics.FromImage(bmp)
            g.SmoothingMode = SmoothingMode.AntiAlias
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit
            g.Clear(CardFill)

            Dim left As Integer = 8
            Dim right As Integer = w - 8
            Dim top As Integer = 6
            Dim bottom As Integer = h - 20
            Dim chartW As Integer = right - left
            Dim chartH As Integer = bottom - top
            Dim slots As Integer = HourEnd - HourStart

            ' grid + hour labels
            Using gridPen As New Pen(Color.FromArgb(240, 232, 224), 1.0F),
                  f As New Font("Segoe UI", 7.5F), mutedB As New SolidBrush(Muted),
                  sf As New StringFormat With {.Alignment = StringAlignment.Center}
                For i As Integer = 0 To 3
                    Dim y As Single = top + chartH * i / 3.0F
                    g.DrawLine(gridPen, left, y, right, y)
                Next
                For i As Integer = 0 To slots Step 2
                    Dim hour As Integer = HourStart + i
                    Dim label As String = If(hour = 12, "12 PM", If(hour > 12, (hour - 12).ToString() & " PM", hour.ToString() & " AM"))
                    Dim x As Single = left + chartW * i / CSng(slots)
                    g.DrawString(label, f, mutedB, x, bottom + 4, sf)
                Next
            End Using

            If maxVal <= 0D Then
                Using f As New Font("Segoe UI", 9.0F), mutedB As New SolidBrush(Muted),
                      sf As New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
                    g.DrawString("No sales yet today", f, mutedB, New RectangleF(0, top, w, chartH), sf)
                End Using
            Else
                DrawSeries(g, yestSeries, maxVal, left, top, chartW, chartH, slots, Color.FromArgb(222, 190, 165), 2.0F)
                DrawSeries(g, todaySeries, maxVal, left, top, chartW, chartH, slots, Accent, 2.75F)
            End If
        End Using

        Dim old As Bitmap = trendImage
        trendImage = bmp
        picDashTrend.Image = bmp
        If old IsNot Nothing Then old.Dispose()
    End Sub

    Private Shared Sub DrawSeries(g As Graphics, series As Decimal(), maxVal As Decimal, left As Integer, top As Integer,
                                  chartW As Integer, chartH As Integer, slots As Integer, color As Color, width As Single)
        If series.Length = 0 Then Return
        Dim pts(series.Length - 1) As PointF
        For i As Integer = 0 To series.Length - 1
            Dim x As Single = left + chartW * i / CSng(slots)
            Dim y As Single = top + chartH * (1.0F - CSng(series(i) / maxVal))
            pts(i) = New PointF(x, y)
        Next
        Using pen As New Pen(color, width), b As New SolidBrush(color)
            pen.LineJoin = LineJoin.Round
            pen.StartCap = LineCap.Round
            pen.EndCap = LineCap.Round
            If pts.Length >= 3 Then
                g.DrawCurve(pen, pts, 0.4F)
            ElseIf pts.Length = 2 Then
                g.DrawLine(pen, pts(0), pts(1))
            End If
            Dim last As PointF = pts(pts.Length - 1)
            g.FillEllipse(b, last.X - 3.5F, last.Y - 3.5F, 7.0F, 7.0F)
        End Using
    End Sub

    '---------------- list helpers ----------------
    Private Shared Sub ClearFlow(flow As FlowLayoutPanel)
        For i As Integer = flow.Controls.Count - 1 To 0 Step -1
            Dim old As Control = flow.Controls(i)
            flow.Controls.RemoveAt(i)
            old.Dispose()
        Next
    End Sub

    Private Shared Function TextLabel(text As String, x As Integer, y As Integer, w As Integer, h As Integer, f As Font, fore As Color,
                                      Optional align As ContentAlignment = ContentAlignment.MiddleLeft) As Label
        Return New Label With {.Text = text, .AutoSize = False, .AutoEllipsis = True, .Location = New Point(x, y), .Size = New Size(w, h),
                               .Font = f, .ForeColor = fore, .BackColor = CardFill, .TextAlign = align}
    End Function

    Private Sub BuildDashCategories()
        Dim byId As New Dictionary(Of String, Product)
        For Each p As Product In DataStore.Products
            byId(p.Id) = p
        Next

        Dim totals As New Dictionary(Of String, Decimal)
        For Each t As POS_Transaction In DataStore.Transactions
            If t.TransactionDate.Date <> DateTime.Today Then Continue For
            If Not String.Equals(t.Status, TransactionStatus.Completed, StringComparison.OrdinalIgnoreCase) Then Continue For
            For Each item As TransactionItem In t.Items
                Dim cat As String = "Other"
                Dim prod As Product = Nothing
                If byId.TryGetValue(item.ProductId, prod) Then cat = CategoryOf(prod)
                If Not totals.ContainsKey(cat) Then totals(cat) = 0D
                totals(cat) += item.LineTotal
            Next
        Next

        Dim keys As New List(Of String)(totals.Keys)
        keys.Sort(Function(a, b) totals(b).CompareTo(totals(a)))

        flDashCat.SuspendLayout()
        ClearFlow(flDashCat)
        Dim rowW As Integer = Math.Max(150, flDashCat.ClientSize.Width - 4)

        If keys.Count = 0 Then
            flDashCat.Controls.Add(TextLabel("No sales yet today.", 0, 0, rowW, 40, New Font("Segoe UI", 9.0F), Muted, ContentAlignment.MiddleCenter))
        End If

        Dim best As Decimal = If(keys.Count > 0, totals(keys(0)), 0D)
        For i As Integer = 0 To Math.Min(3, keys.Count - 1)
            Dim key As String = keys(i)
            Dim row As New Panel With {.Size = New Size(rowW, 30), .Margin = New Padding(0, 0, 0, 3), .BackColor = CardFill}
            row.Controls.Add(TextLabel(key, 0, 0, rowW - 90, 17, New Font("Segoe UI", 8.5F), Ink))
            row.Controls.Add(TextLabel(Peso(totals(key)), rowW - 90, 0, 90, 17, New Font("Segoe UI", 8.5F, FontStyle.Bold), Ink, ContentAlignment.MiddleRight))
            Dim track As New Guna2Panel With {.Location = New Point(0, 20), .Size = New Size(rowW, 7), .BorderRadius = 3,
                .FillColor = Color.FromArgb(240, 232, 224), .BackColor = CardFill}
            Dim fillW As Integer = If(best <= 0D, 0, Math.Max(6, CInt(rowW * CDbl(totals(key) / best))))
            track.Controls.Add(New Guna2Panel With {.Location = New Point(0, 0), .Size = New Size(Math.Min(fillW, rowW), 7), .BorderRadius = 3,
                .FillColor = CatColors(i Mod CatColors.Length), .BackColor = Color.FromArgb(240, 232, 224)})
            row.Controls.Add(track)
            flDashCat.Controls.Add(row)
        Next
        flDashCat.ResumeLayout()
    End Sub

    Private Sub BuildDashTop()
        Dim qty As New Dictionary(Of String, Integer)
        Dim revenue As New Dictionary(Of String, Decimal)
        For Each t As POS_Transaction In DataStore.Transactions
            If t.TransactionDate.Date <> DateTime.Today Then Continue For
            If Not String.Equals(t.Status, TransactionStatus.Completed, StringComparison.OrdinalIgnoreCase) Then Continue For
            For Each item As TransactionItem In t.Items
                Dim key As String = item.ProductName
                If Not qty.ContainsKey(key) Then
                    qty(key) = 0
                    revenue(key) = 0D
                End If
                qty(key) += item.Quantity
                revenue(key) += item.LineTotal
            Next
        Next

        Dim keys As New List(Of String)(qty.Keys)
        keys.Sort(Function(a, b) qty(b).CompareTo(qty(a)))

        flDashTop.SuspendLayout()
        ClearFlow(flDashTop)
        Dim rowW As Integer = Math.Max(150, flDashTop.ClientSize.Width - 4)
        If keys.Count = 0 Then
            flDashTop.Controls.Add(TextLabel("No items sold yet today.", 0, 0, rowW, 40, New Font("Segoe UI", 9.0F), Muted, ContentAlignment.MiddleCenter))
        End If

        For i As Integer = 0 To Math.Min(2, keys.Count - 1)
            Dim key As String = keys(i)
            Dim row As New Panel With {.Size = New Size(rowW, 29), .Margin = New Padding(0, 0, 0, 1), .BackColor = CardFill}
            row.Controls.Add(New Label With {.Text = (i + 1).ToString(), .AutoSize = False, .Size = New Size(22, 22), .Location = New Point(0, 3),
                .TextAlign = ContentAlignment.MiddleCenter, .BackColor = Color.FromArgb(243, 232, 222), .ForeColor = Accent,
                .Font = New Font("Segoe UI", 8.0F, FontStyle.Bold)})
            row.Controls.Add(TextLabel(key, 30, 0, rowW - 30 - 84, 15, New Font("Segoe UI", 8.5F, FontStyle.Bold), Ink))
            row.Controls.Add(TextLabel(qty(key).ToString() & " sold", 30, 15, rowW - 30 - 84, 13, New Font("Segoe UI", 7.5F), Muted))
            row.Controls.Add(TextLabel(Peso(revenue(key)), rowW - 84, 5, 84, 18, New Font("Segoe UI", 8.5F, FontStyle.Bold), Ink, ContentAlignment.MiddleRight))
            flDashTop.Controls.Add(row)
        Next
        flDashTop.ResumeLayout()
    End Sub

    Private Sub BuildDashAlerts()
        Dim need As New List(Of Product)
        For Each p As Product In DataStore.Products
            If DataStore.GetProductStockStatus(p) <> StockStatus.InStock Then need.Add(p)
        Next
        need.Sort(Function(a, b) a.Stock.CompareTo(b.Stock))
        lblDashAlertNote.Text = need.Count.ToString() & " need attention"

        flDashAlert.SuspendLayout()
        ClearFlow(flDashAlert)
        Dim rowW As Integer = Math.Max(150, flDashAlert.ClientSize.Width - 4)
        If need.Count = 0 Then
            flDashAlert.Controls.Add(TextLabel("All items are well stocked.", 0, 0, rowW, 40, New Font("Segoe UI", 9.0F), Muted, ContentAlignment.MiddleCenter))
        End If

        For i As Integer = 0 To Math.Min(2, need.Count - 1)
            Dim p As Product = need(i)
            Dim isOut As Boolean = (p.Stock <= 0)
            Dim chipBack As Color = If(isOut, Color.FromArgb(250, 226, 222), Color.FromArgb(250, 235, 210))
            Dim fore As Color = If(isOut, Color.FromArgb(176, 72, 48), Color.FromArgb(170, 105, 30))
            Dim row As New Panel With {.Size = New Size(rowW, 26), .Margin = New Padding(0, 0, 0, 2), .BackColor = CardFill}
            row.Controls.Add(New Label With {.Text = "!", .AutoSize = False, .Size = New Size(22, 22), .Location = New Point(0, 2),
                .TextAlign = ContentAlignment.MiddleCenter, .BackColor = chipBack, .ForeColor = fore,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)})
            row.Controls.Add(TextLabel(p.Name, 30, 3, rowW - 30 - 100, 18, New Font("Segoe UI", 8.5F, FontStyle.Bold), Ink))
            row.Controls.Add(TextLabel(If(isOut, "Out of stock", p.Stock.ToString() & " left"), rowW - 100, 3, 100, 18,
                                       New Font("Segoe UI", 8.0F, FontStyle.Bold), fore, ContentAlignment.MiddleRight))
            flDashAlert.Controls.Add(row)
        Next
        flDashAlert.ResumeLayout()
    End Sub

    Private Sub RefreshCashierData()
        LoadTransactions()
        RefreshCashierDashboard()
    End Sub

    Private Sub BuildMessagingLayout()
        CafeUi.StyleMessaging(pnl_CashierMessages, FlowLayoutPanel3, pnl_MainChat,
                              flpMessages, CashierName, Guna2HtmlLabel60,
                              txtChat, btnSend, "Admin Support",
                              "Terminal Communications",
                              "Send a message to the administrator for support or store updates.")
    End Sub

    '=================================================================
    ' HISTORY  (layout is in Cashier_Designer.vb  |  rows, totals, receipt: here)
    '=================================================================
    Private histList As New List(Of POS_Transaction)
    Private selectedTrx As POS_Transaction
    Private suppressHist As Boolean = False

    Private Sub SetupHistory()

        StyleGrid(histGrid, 42)
        histGrid.Columns.Add(MakeCol("colHOrder", "Order", 80, 0, True))
        histGrid.Columns.Add(MakeCol("colHTime", "Date & time", 118, 0, False))
        histGrid.Columns.Add(MakeCol("colHCashier", "Cashier", 0, 60, False))
        histGrid.Columns.Add(MakeCol("colHItems", "Items", 0, 140, False))
        histGrid.Columns.Add(MakeCol("colHPay", "Payment", 62, 0, False))
        histGrid.Columns.Add(MakeCol("colHTotal", "Total", 84, 0, True))
        histGrid.Columns.Add(MakeCol("colHStatus", "Status", 96, 0, False))

        Guna2ComboBox1.Items.AddRange(New Object() {"All statuses", TransactionStatus.Completed, TransactionStatus.Refunded, TransactionStatus.Voided})
        Guna2ComboBox1.SelectedIndex = 0
        Guna2ComboBox2.Items.AddRange(New Object() {"All payments", PaymentMethods.Cash, PaymentMethods.Card})
        Guna2ComboBox2.SelectedIndex = 0
        dtpHistory.Value = DateTime.Today

        AddHandler pnl_History.Resize, Sub(s As Object, ev As EventArgs) LayoutKpiRows()
        LayoutKpiRows()
        RefreshHistory()
    End Sub

    Private Sub HistoryFilter_Changed(sender As Object, e As EventArgs) Handles Guna2TextBox1.TextChanged, Guna2ComboBox1.SelectedIndexChanged, Guna2ComboBox2.SelectedIndexChanged, dtpHistory.ValueChanged
        RefreshHistory()
    End Sub

    Private Sub btnHistNewOrder_Click(sender As Object, e As EventArgs) Handles btnHistNewOrder.Click
        btn_Point_Of_Sale.PerformClick()
    End Sub

    Private Function FilteredTransactions() As List(Of POS_Transaction)
        Dim q As String = Guna2TextBox1.Text.Trim()
        Dim statusFilter As String = If(Guna2ComboBox1.SelectedIndex > 0, Guna2ComboBox1.Text, "")
        Dim payFilter As String = If(Guna2ComboBox2.SelectedIndex > 0, Guna2ComboBox2.Text, "")
        Dim result As New List(Of POS_Transaction)

        For Each t As POS_Transaction In DataStore.Transactions
            If statusFilter <> "" AndAlso Not String.Equals(t.Status, statusFilter, StringComparison.OrdinalIgnoreCase) Then Continue For
            If payFilter <> "" AndAlso Not String.Equals(t.PaymentMethod, payFilter, StringComparison.OrdinalIgnoreCase) Then Continue For
            If dtpHistory.Checked AndAlso t.TransactionDate.Date <> dtpHistory.Value.Date Then Continue For
            If q <> "" Then
                Dim hay As String = t.TransactionID & " " & t.Cashier & " " & t.PaymentMethod & " " & DataStore.BuildItemsText(t)
                If hay.IndexOf(q, StringComparison.OrdinalIgnoreCase) < 0 Then Continue For
            End If
            result.Add(t)
        Next
        result.Sort(Function(a, b) b.TransactionDate.CompareTo(a.TransactionDate))
        Return result
    End Function

    Private Sub RefreshHistory()
        If histGrid Is Nothing OrElse histGrid.Columns.Count = 0 Then Return

        Dim keepId As String = If(selectedTrx IsNot Nothing, selectedTrx.TransactionID, "")
        histList = FilteredTransactions()

        suppressHist = True
        histGrid.Rows.Clear()
        Dim salesTotal As Decimal = 0D, netTotal As Decimal = 0D, taxTotal As Decimal = 0D, refundTotal As Decimal = 0D
        Dim orderCount As Integer = 0, refundCount As Integer = 0

        For Each t As POS_Transaction In histList
            Dim timeText As String = If(t.TransactionDate.Date = DateTime.Today,
                                        "Today, " & t.TransactionDate.ToString("hh:mm tt"),
                                        t.TransactionDate.ToString("MMM d, hh:mm tt"))
            Dim idx As Integer = histGrid.Rows.Add(t.TransactionID, timeText, t.Cashier, DataStore.BuildItemsText(t),
                                                   t.PaymentMethod, Peso(t.Total), t.Status)
            histGrid.Rows(idx).Tag = t

            If String.Equals(t.Status, TransactionStatus.Completed, StringComparison.OrdinalIgnoreCase) Then
                salesTotal += t.Total
                netTotal += t.Subtotal
                taxTotal += t.Tax
                orderCount += 1
            ElseIf String.Equals(t.Status, TransactionStatus.Refunded, StringComparison.OrdinalIgnoreCase) Then
                refundTotal += t.Total
                refundCount += 1
            End If
        Next
        suppressHist = False

        lblHK1T.Text = "Total sales"
        lblHK1V.Text = Peso(salesTotal)
        lblHK1S.Text = orderCount.ToString() & If(orderCount = 1, " order", " orders")
        lblHK2V.Text = Peso(netTotal)
        lblHK2S.Text = "Before tax"
        lblHK3V.Text = Peso(refundTotal)
        lblHK3S.Text = refundCount.ToString() & If(refundCount = 1, " transaction", " transactions")
        lblHK4V.Text = Peso(taxTotal)
        lblHK4S.Text = (DataStore.PosSettings.TaxRate * 100D).ToString("0.##") & "% rate"

        lblHistCount.Text = histList.Count.ToString() & If(histList.Count = 1, " record", " records")
        lblHistShowing.Text = "Showing " & histList.Count.ToString() & " of " & DataStore.Transactions.Count.ToString() & " transactions"

        If histList.Count > 0 Then
            Dim target As Integer = 0
            For i As Integer = 0 To histList.Count - 1
                If histList(i).TransactionID = keepId Then target = i : Exit For
            Next
            histGrid.ClearSelection()
            histGrid.Rows(target).Selected = True
            histGrid.CurrentCell = histGrid.Rows(target).Cells(0)
            RenderDetail(histList(target))
        Else
            RenderDetail(Nothing)
        End If
    End Sub

    Private Sub histGrid_SelectionChanged(sender As Object, e As EventArgs) Handles histGrid.SelectionChanged
        If suppressHist OrElse histGrid.SelectedRows.Count = 0 Then Return
        RenderDetail(TryCast(histGrid.SelectedRows(0).Tag, POS_Transaction))
    End Sub

    Private Sub histGrid_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles histGrid.CellPainting
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
        If histGrid.Columns(e.ColumnIndex).Name <> "colHStatus" Then Return
        Dim status As String = Convert.ToString(e.Value)
        e.PaintBackground(e.CellBounds, True)
        DrawPill(e.Graphics, e.CellBounds, status, PillBack(status), PillFore(status))
        e.Paint(e.CellBounds, DataGridViewPaintParts.Border)
        e.Handled = True
    End Sub

    '---------------- receipt panel ----------------
    Private Sub RenderDetail(t As POS_Transaction)
        selectedTrx = t
        Dim has As Boolean = (t IsNot Nothing)
        lblDetEmpty.Visible = Not has
        For Each c As Control In New Control() {lblDetTitle, btnDetStatus, lblDetDate, btnDetPrint, lblDetSecA, lblDetCashierCap,
                                                lblDetCashier, lblDetPayCap, lblDetPay, lblDetSecB, flDetItems, lblDetSubCap,
                                                lblDetSub, lblDetTaxCap, lblDetTax, lblDetCash, pnlDetTotal, lblDetReceipt}
            c.Visible = has
        Next

        flDetItems.SuspendLayout()
        For i As Integer = flDetItems.Controls.Count - 1 To 0 Step -1
            Dim old As Control = flDetItems.Controls(i)
            flDetItems.Controls.RemoveAt(i)
            old.Dispose()
        Next
        If Not has Then
            flDetItems.ResumeLayout()
            Return
        End If

        lblDetTitle.Text = "Order #" & t.TransactionID
        btnDetStatus.Text = t.Status
        btnDetStatus.FillColor = PillBack(t.Status)
        btnDetStatus.BorderColor = PillBack(t.Status)
        btnDetStatus.ForeColor = PillFore(t.Status)
        btnDetStatus.HoverState.FillColor = PillBack(t.Status)
        btnDetStatus.HoverState.ForeColor = PillFore(t.Status)
        lblDetDate.Text = t.TransactionDate.ToString("MMMM d, yyyy  -  hh:mm tt")
        lblDetCashier.Text = t.Cashier
        lblDetPay.Text = t.PaymentMethod
        lblDetSub.Text = Peso(t.Subtotal)
        lblDetTax.Text = Peso(t.Tax)
        lblDetTotal.Text = Peso(t.Total)
        lblDetCash.Text = If(t.PaymentMethod = PaymentMethods.Cash,
                             "Cash received " & Peso(t.CashReceived) & "   |   Change " & Peso(t.ChangeGiven), "")
        lblDetReceipt.Text = "Receipt ID  " & t.TransactionID

        Dim rowW As Integer = Math.Max(150, flDetItems.ClientSize.Width - 4)
        For Each item As TransactionItem In t.Items
            Dim row As New Panel With {.Width = rowW, .Height = 30, .Margin = New Padding(0, 0, 0, 2), .BackColor = CardFill}
            row.Controls.Add(New Label With {.Text = item.Quantity.ToString(), .AutoSize = False, .Size = New Size(22, 22),
                .Location = New Point(0, 4), .TextAlign = ContentAlignment.MiddleCenter, .BackColor = Color.FromArgb(243, 232, 222),
                .ForeColor = Ink, .Font = New Font("Segoe UI", 8.0F, FontStyle.Bold)})
            row.Controls.Add(New Label With {.Text = item.ProductName, .AutoSize = False, .AutoEllipsis = True,
                .Size = New Size(rowW - 30 - 74, 22), .Location = New Point(30, 4), .TextAlign = ContentAlignment.MiddleLeft,
                .BackColor = CardFill, .ForeColor = Ink, .Font = New Font("Segoe UI", 8.5F)})
            row.Controls.Add(New Label With {.Text = Peso(item.LineTotal), .AutoSize = False, .Size = New Size(74, 22),
                .Location = New Point(rowW - 74, 4), .TextAlign = ContentAlignment.MiddleRight,
                .BackColor = CardFill, .ForeColor = Ink, .Font = New Font("Segoe UI", 8.5F, FontStyle.Bold)})
            flDetItems.Controls.Add(row)
        Next
        flDetItems.ResumeLayout()
    End Sub

    Private Sub btnDetPrint_Click(sender As Object, e As EventArgs) Handles btnDetPrint.Click
        If selectedTrx Is Nothing Then Return
        Dim lines As List(Of String) = BuildReceiptLines(selectedTrx)

        Using doc As New System.Drawing.Printing.PrintDocument()
            AddHandler doc.PrintPage, Sub(s As Object, ev As System.Drawing.Printing.PrintPageEventArgs)
                                          Using f As New Font("Consolas", 9.5F)
                                              Dim y As Single = ev.MarginBounds.Top
                                              For Each ln As String In lines
                                                  ev.Graphics.DrawString(ln, f, Brushes.Black, CSng(ev.MarginBounds.Left), y)
                                                  y += f.GetHeight(ev.Graphics) + 2.0F
                                              Next
                                          End Using
                                          ev.HasMorePages = False
                                      End Sub
            Using dlg As New PrintPreviewDialog()
                dlg.Document = doc
                dlg.Width = 720
                dlg.Height = 820
                dlg.ShowDialog(Me)
            End Using
        End Using
    End Sub

    Private Function BuildReceiptLines(t As POS_Transaction) As List(Of String)
        Const w As Integer = 40
        Dim lines As New List(Of String)
        Dim centered As Func(Of String, String) = Function(s As String) s.PadLeft((w + s.Length) \ 2).PadRight(w)
        Dim pair As Func(Of String, String, String) =
            Function(l As String, r As String)
                Dim room As Integer = Math.Max(1, w - r.Length - 1)
                If l.Length > room Then l = l.Substring(0, room)
                Return l.PadRight(room + 1) & r
            End Function
        Dim rule As String = New String("-"c, w)

        lines.Add(centered("FOREST ROAST CAFE"))
        lines.Add(centered("Official Receipt"))
        lines.Add(rule)
        lines.Add("Receipt: " & t.TransactionID)
        lines.Add("Date:    " & t.TransactionDate.ToString("MMM d, yyyy hh:mm tt"))
        lines.Add("Cashier: " & t.Cashier)
        lines.Add(rule)
        For Each item As TransactionItem In t.Items
            lines.Add(pair(item.Quantity.ToString() & "x " & item.ProductName, Peso(item.LineTotal)))
        Next
        lines.Add(rule)
        lines.Add(pair("Subtotal", Peso(t.Subtotal)))
        lines.Add(pair("Tax", Peso(t.Tax)))
        lines.Add(pair("TOTAL", Peso(t.Total)))
        lines.Add(rule)
        lines.Add(pair("Payment", t.PaymentMethod))
        If t.PaymentMethod = PaymentMethods.Cash Then
            lines.Add(pair("Cash received", Peso(t.CashReceived)))
            lines.Add(pair("Change", Peso(t.ChangeGiven)))
        End If
        lines.Add(pair("Status", t.Status))
        lines.Add(rule)
        lines.Add(centered("Thank you! Please come again."))
        Return lines
    End Function

    Private Sub btnHistExport_Click(sender As Object, e As EventArgs) Handles btnHistExport.Click
        Dim rows As New List(Of String())
        For Each t As POS_Transaction In histList
            rows.Add(New String() {t.TransactionID, t.TransactionDate.ToString("yyyy-MM-dd HH:mm"), t.Cashier, DataStore.BuildItemsText(t),
                                   t.PaymentMethod, t.Subtotal.ToString("0.00"), t.Tax.ToString("0.00"), t.Total.ToString("0.00"), t.Status})
        Next
        SaveCsv("transactions", New String() {"Order", "Date", "Cashier", "Items", "Payment", "Subtotal", "Tax", "Total", "Status"}, rows)
    End Sub

    '=================================================================
    ' INVENTORY  (read-only for cashiers: no add / adjust / purchase order)
    '=================================================================
    Private invBusy As Boolean = False
    Private catBarImage As Bitmap
    Private invShown As New List(Of Product)

    Private Shared ReadOnly CatColors As Color() = {
        Color.FromArgb(193, 106, 58), Color.FromArgb(222, 196, 172), Color.FromArgb(176, 126, 48),
        Color.FromArgb(92, 122, 84), Color.FromArgb(120, 104, 96)}

    Private Sub SetupInventory()

        StyleGrid(invGrid, 54)
        invGrid.Columns.Add(MakeCol("colInvItem", "Item", 0, 150, False))
        invGrid.Columns.Add(MakeCol("colInvSku", "SKU", 70, 0, False))
        invGrid.Columns.Add(MakeCol("colInvCat", "Category", 92, 0, False))
        invGrid.Columns.Add(MakeCol("colInvStock", "Current stock", 112, 0, False))
        invGrid.Columns.Add(MakeCol("colInvMin", "Reorder", 62, 0, False))
        invGrid.Columns.Add(MakeCol("colInvPrice", "Price", 78, 0, False))
        invGrid.Columns.Add(MakeCol("colInvStatus", "Status", 100, 0, False))

        cboInvStatus.Items.AddRange(New Object() {"All statuses", StockStatus.InStock, StockStatus.LowStock, StockStatus.OutOfStock})
        cboInvStatus.SelectedIndex = 0
        cboInvCategory.Items.Add("All categories")
        cboInvCategory.SelectedIndex = 0

        AddHandler Panel1.Resize, Sub(s As Object, ev As EventArgs) LayoutKpiRows()
        LayoutKpiRows()
        RefreshCashierInventory()
    End Sub

    Private Sub InventoryFilter_Changed(sender As Object, e As EventArgs) Handles txtInvSearch.TextChanged, cboInvCategory.SelectedIndexChanged, cboInvStatus.SelectedIndexChanged
        If Not invBusy Then RefreshCashierInventory()
    End Sub

    Private Shared Function CategoryOf(p As Product) As String
        Return If(String.IsNullOrWhiteSpace(p.Category), "Uncategorized", p.Category.Trim())
    End Function

    Private Sub RefreshCashierInventory()
        If invGrid Is Nothing OrElse invGrid.Columns.Count = 0 Then Return

        ' ---- category filter list (kept in sync with the catalog) ----
        Dim cats As New List(Of String)
        For Each p As Product In DataStore.Products
            Dim cn As String = CategoryOf(p)
            If Not cats.Contains(cn) Then cats.Add(cn)
        Next
        cats.Sort()
        Dim same As Boolean = (cboInvCategory.Items.Count = cats.Count + 1)
        If same Then
            For i As Integer = 0 To cats.Count - 1
                If cboInvCategory.Items(i + 1).ToString() <> cats(i) Then same = False : Exit For
            Next
        End If
        If Not same Then
            invBusy = True
            Dim keep As String = cboInvCategory.Text
            cboInvCategory.Items.Clear()
            cboInvCategory.Items.Add("All categories")
            For Each cn As String In cats
                cboInvCategory.Items.Add(cn)
            Next
            Dim keepIdx As Integer = cboInvCategory.Items.IndexOf(keep)
            cboInvCategory.SelectedIndex = If(keepIdx >= 0, keepIdx, 0)
            invBusy = False
        End If

        Dim q As String = txtInvSearch.Text.Trim()
        Dim catFilter As String = If(cboInvCategory.SelectedIndex > 0, cboInvCategory.Text, "")
        Dim statusFilter As String = If(cboInvStatus.SelectedIndex > 0, cboInvStatus.Text, "")

        invGrid.Rows.Clear()
        invShown.Clear()
        Dim totalValue As Decimal = 0D
        Dim lowCount As Integer = 0, outCount As Integer = 0

        For Each p As Product In DataStore.Products
            Dim st As String = DataStore.GetProductStockStatus(p)
            totalValue += p.Price * p.Stock
            If st = StockStatus.LowStock Then lowCount += 1
            If st = StockStatus.OutOfStock Then outCount += 1

            If catFilter <> "" AndAlso CategoryOf(p) <> catFilter Then Continue For
            If statusFilter <> "" AndAlso st <> statusFilter Then Continue For
            If q <> "" Then
                Dim hay As String = p.Name & " " & p.Id & " " & CategoryOf(p)
                If hay.IndexOf(q, StringComparison.OrdinalIgnoreCase) < 0 Then Continue For
            End If

            Dim idx As Integer = invGrid.Rows.Add(p.Name, p.Id, CategoryOf(p), p.Stock, DataStore.GetMinStock(p), Peso(p.Price), st)
            invGrid.Rows(idx).Tag = p
            invShown.Add(p)
        Next
        invGrid.ClearSelection()

        lblIK1V.Text = DataStore.Products.Count.ToString()
        lblIK1S.Text = cats.Count.ToString() & If(cats.Count = 1, " category", " categories")
        lblIK2V.Text = Peso(totalValue)
        lblIK2S.Text = "At selling price"
        lblIK3V.Text = lowCount.ToString()
        lblIK3S.Text = "Needs reorder"
        lblIK4V.Text = outCount.ToString()
        lblIK4S.Text = "Action required"
        lblInvCount.Text = invShown.Count.ToString() & If(invShown.Count = 1, " item", " items")
        lblInvShowing.Text = "Showing " & invShown.Count.ToString() & " of " & DataStore.Products.Count.ToString() & " items"

        BuildCategorySummary(totalValue)
        BuildAttentionList()
    End Sub

    Private Sub BuildCategorySummary(totalValue As Decimal)

        Dim items As New Dictionary(Of String, Integer)
        Dim vals As New Dictionary(Of String, Decimal)
        For Each p As Product In DataStore.Products
            Dim key As String = CategoryOf(p)
            If Not items.ContainsKey(key) Then
                items(key) = 0
                vals(key) = 0D
            End If
            items(key) += 1
            vals(key) += p.Price * p.Stock
        Next

        Dim keys As New List(Of String)(vals.Keys)
        keys.Sort(Function(a, b) vals(b).CompareTo(vals(a)))

        Dim names As New List(Of String), counts As New List(Of Integer), amounts As New List(Of Decimal)
        For i As Integer = 0 To keys.Count - 1
            If keys.Count > 5 AndAlso i >= 4 Then
                If names.Count = 4 Then
                    names.Add("Other") : counts.Add(0) : amounts.Add(0D)
                End If
                counts(4) += items(keys(i))
                amounts(4) += vals(keys(i))
            Else
                names.Add(keys(i)) : counts.Add(items(keys(i))) : amounts.Add(vals(keys(i)))
            End If
        Next

        lblInvCatTotal.Text = Peso(totalValue) & " total"

        ' stacked bar
        Dim bar As New Bitmap(picInvCatBar.Width, picInvCatBar.Height)
        Using g As Graphics = Graphics.FromImage(bar)
            g.SmoothingMode = SmoothingMode.AntiAlias
            g.Clear(CardFill)
            Dim whole As Decimal = 0D
            For Each a As Decimal In amounts
                whole += a
            Next
            Using clip As GraphicsPath = RoundRectPath(New Rectangle(0, 0, bar.Width - 1, bar.Height - 1), 4, False)
                g.SetClip(clip)
                g.Clear(Color.FromArgb(240, 232, 224))
                If whole > 0D Then
                    Dim x As Integer = 0
                    For i As Integer = 0 To amounts.Count - 1
                        Dim w As Integer = If(i = amounts.Count - 1, bar.Width - x, CInt(Math.Round(CDbl(amounts(i) / whole) * bar.Width)))
                        Using b As New SolidBrush(CatColors(i Mod CatColors.Length))
                            g.FillRectangle(b, x, 0, w, bar.Height)
                        End Using
                        x += w
                    Next
                End If
            End Using
        End Using
        Dim oldBar As Bitmap = catBarImage
        catBarImage = bar
        picInvCatBar.Image = bar
        If oldBar IsNot Nothing Then oldBar.Dispose()

        ' legend rows
        flInvCat.SuspendLayout()
        For i As Integer = flInvCat.Controls.Count - 1 To 0 Step -1
            Dim old As Control = flInvCat.Controls(i)
            flInvCat.Controls.RemoveAt(i)
            old.Dispose()
        Next
        For i As Integer = 0 To names.Count - 1
            Dim row As New Panel With {.Size = New Size(flInvCat.ClientSize.Width - 4, 24), .Margin = New Padding(0, 0, 0, 2), .BackColor = CardFill}
            row.Controls.Add(New Label With {.Text = ChrW(&H25CF), .AutoSize = False, .Size = New Size(16, 22), .Location = New Point(0, 1),
                .ForeColor = CatColors(i Mod CatColors.Length), .BackColor = CardFill, .Font = New Font("Segoe UI", 9.0F),
                .TextAlign = ContentAlignment.MiddleLeft})
            row.Controls.Add(New Label With {.Text = names(i), .AutoSize = False, .AutoEllipsis = True, .Size = New Size(112, 22), .Location = New Point(16, 1),
                .ForeColor = Ink, .BackColor = CardFill, .Font = New Font("Segoe UI", 8.5F), .TextAlign = ContentAlignment.MiddleLeft})
            row.Controls.Add(New Label With {.Text = counts(i).ToString() & If(counts(i) = 1, " item", " items"), .AutoSize = False,
                .Size = New Size(56, 22), .Location = New Point(128, 1), .ForeColor = Muted, .BackColor = CardFill,
                .Font = New Font("Segoe UI", 7.5F), .TextAlign = ContentAlignment.MiddleRight})
            row.Controls.Add(New Label With {.Text = Peso(amounts(i)), .AutoSize = False, .Size = New Size(76, 22),
                .Location = New Point(row.Width - 76, 1), .ForeColor = Ink, .BackColor = CardFill,
                .Font = New Font("Segoe UI", 8.5F, FontStyle.Bold), .TextAlign = ContentAlignment.MiddleRight})
            flInvCat.Controls.Add(row)
        Next
        flInvCat.ResumeLayout()
    End Sub

    Private Sub BuildAttentionList()

        Dim need As New List(Of Product)
        For Each p As Product In DataStore.Products
            If DataStore.GetProductStockStatus(p) <> StockStatus.InStock Then need.Add(p)
        Next
        need.Sort(Function(a, b) a.Stock.CompareTo(b.Stock))

        lblInvAttnCount.Text = need.Count.ToString() & If(need.Count = 1, " item", " items")

        flInvAttn.SuspendLayout()
        For i As Integer = flInvAttn.Controls.Count - 1 To 0 Step -1
            Dim old As Control = flInvAttn.Controls(i)
            flInvAttn.Controls.RemoveAt(i)
            old.Dispose()
        Next

        Dim rowW As Integer = Math.Max(150, flInvAttn.ClientSize.Width - 4)
        If need.Count = 0 Then
            flInvAttn.Controls.Add(New Label With {.Text = "All items are well stocked.", .AutoSize = False,
                .Size = New Size(rowW, 40), .ForeColor = Muted, .BackColor = CardFill, .Font = New Font("Segoe UI", 9.0F),
                .TextAlign = ContentAlignment.MiddleCenter})
        End If

        For Each p As Product In need
            Dim isOut As Boolean = (p.Stock <= 0)
            Dim back As Color = If(isOut, Color.FromArgb(250, 226, 222), Color.FromArgb(250, 236, 212))
            Dim fore As Color = If(isOut, Color.FromArgb(176, 72, 48), Color.FromArgb(170, 105, 30))
            Dim card As New Guna2Panel With {.Size = New Size(rowW, 46), .Margin = New Padding(0, 0, 0, 8), .BorderRadius = 10,
                .FillColor = back, .BackColor = CardFill}
            card.Controls.Add(New Label With {.Text = p.Name, .AutoSize = False, .AutoEllipsis = True, .Size = New Size(rowW - 100, 18),
                .Location = New Point(12, 6), .ForeColor = Ink, .BackColor = back, .Font = New Font("Segoe UI", 8.5F, FontStyle.Bold)})
            card.Controls.Add(New Label With {.Text = "Reorder at " & DataStore.GetMinStock(p).ToString(), .AutoSize = False,
                .Size = New Size(rowW - 100, 16), .Location = New Point(12, 25), .ForeColor = Muted, .BackColor = back,
                .Font = New Font("Segoe UI", 7.5F)})
            card.Controls.Add(New Label With {.Text = If(isOut, "Out of stock", p.Stock.ToString() & " left"), .AutoSize = False,
                .Size = New Size(90, 18), .Location = New Point(rowW - 100, 14), .ForeColor = fore, .BackColor = back,
                .TextAlign = ContentAlignment.MiddleRight, .Font = New Font("Segoe UI", 8.0F, FontStyle.Bold)})
            flInvAttn.Controls.Add(card)
        Next
        flInvAttn.ResumeLayout()
    End Sub

    Private Sub invGrid_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles invGrid.CellPainting
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
        Dim p As Product = TryCast(invGrid.Rows(e.RowIndex).Tag, Product)
        If p Is Nothing Then Return
        Dim col As String = invGrid.Columns(e.ColumnIndex).Name
        If col <> "colInvItem" AndAlso col <> "colInvStock" AndAlso col <> "colInvStatus" Then Return

        e.PaintBackground(e.CellBounds, True)
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit
        Dim cb As Rectangle = e.CellBounds
        Dim st As String = DataStore.GetProductStockStatus(p)

        Select Case col
            Case "colInvItem"
                Using thumb As Bitmap = MakeThumb(p.Image, 38)
                    g.DrawImage(thumb, cb.X + 12, cb.Y + (cb.Height - 38) \ 2)
                End Using
                Using sf As New StringFormat With {.Trimming = StringTrimming.EllipsisCharacter, .FormatFlags = StringFormatFlags.NoWrap},
                      nameFont As New Font("Segoe UI", 9.0F, FontStyle.Bold),
                      descFont As New Font("Segoe UI", 7.5F),
                      inkB As New SolidBrush(Ink), mutedB As New SolidBrush(Muted)
                    Dim textW As Single = cb.Width - 62
                    g.DrawString(p.Name, nameFont, inkB, New RectangleF(cb.X + 58, cb.Y + 10, textW, 18), sf)
                    g.DrawString(If(String.IsNullOrWhiteSpace(p.Description), CategoryOf(p), p.Description), descFont, mutedB,
                                 New RectangleF(cb.X + 58, cb.Y + 29, textW, 16), sf)
                End Using

            Case "colInvStock"
                Dim minStock As Integer = DataStore.GetMinStock(p)
                Using f As New Font("Segoe UI", 9.0F, FontStyle.Bold), inkB As New SolidBrush(Ink)
                    g.DrawString(p.Stock.ToString(), f, inkB, cb.X + 10, cb.Y + 11)
                End Using
                Dim track As New Rectangle(cb.X + 10, cb.Y + 33, Math.Min(86, cb.Width - 22), 6)
                Using tp As GraphicsPath = RoundRectPath(track, 3, False), tb As New SolidBrush(Color.FromArgb(238, 230, 222))
                    g.FillPath(tb, tp)
                End Using
                Dim ratio As Double = If(minStock <= 0, 1.0, Math.Min(1.0, p.Stock / (minStock * 3.0)))
                Dim fillW As Integer = CInt(track.Width * ratio)
                If p.Stock > 0 AndAlso fillW < 6 Then fillW = 6
                If fillW > 0 Then
                    Dim barColor As Color = If(st = StockStatus.InStock, Color.FromArgb(92, 122, 84),
                                               If(st = StockStatus.LowStock, Color.FromArgb(205, 130, 40), Color.FromArgb(198, 60, 50)))
                    Using fp As GraphicsPath = RoundRectPath(New Rectangle(track.X, track.Y, fillW, track.Height), 3, False),
                          fb As New SolidBrush(barColor)
                        g.FillPath(fb, fp)
                    End Using
                End If

            Case "colInvStatus"
                DrawPill(g, cb, st, PillBack(st), PillFore(st))
        End Select

        e.Paint(e.CellBounds, DataGridViewPaintParts.Border)
        e.Handled = True
    End Sub

    Private Sub btnInvExport_Click(sender As Object, e As EventArgs) Handles btnInvExport.Click
        Dim rows As New List(Of String())
        For Each p As Product In invShown
            rows.Add(New String() {p.Id, p.Name, CategoryOf(p), p.Stock.ToString(), DataStore.GetMinStock(p).ToString(),
                                   p.Price.ToString("0.00"), DataStore.GetProductStockStatus(p)})
        Next
        SaveCsv("inventory", New String() {"SKU", "Item", "Category", "Stock", "Reorder level", "Price", "Status"}, rows)
    End Sub

    '=================================================================
    ' SHARED PIECES FOR THE HISTORY + INVENTORY PAGES
    '=================================================================
    Private Sub StyleGrid(g As DataGridView, rowHeight As Integer)
        g.EnableHeadersVisualStyles = False
        g.BorderStyle = BorderStyle.None
        g.BackgroundColor = CardFill
        g.GridColor = Color.FromArgb(240, 230, 220)
        g.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        g.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        g.ColumnHeadersHeight = 34
        g.RowTemplate.Height = rowHeight
        g.RowHeadersVisible = False
        g.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        g.MultiSelect = False
        g.AllowUserToResizeColumns = False

        With g.ColumnHeadersDefaultCellStyle
            .BackColor = Color.FromArgb(243, 232, 222)
            .ForeColor = Muted
            .Font = New Font("Segoe UI", 8.5F)
            .SelectionBackColor = Color.FromArgb(243, 232, 222)
            .SelectionForeColor = Muted
            .Alignment = DataGridViewContentAlignment.MiddleLeft
            .Padding = New Padding(10, 0, 0, 0)
        End With
        With g.DefaultCellStyle
            .BackColor = CardFill
            .ForeColor = Ink
            .Font = New Font("Segoe UI", 8.75F)
            .SelectionBackColor = Color.FromArgb(242, 224, 210)
            .SelectionForeColor = Ink
            .Padding = New Padding(10, 0, 0, 0)
        End With
    End Sub

    Private Function MakeCol(name As String, header As String, width As Integer, fillWeight As Integer, bold As Boolean) As DataGridViewTextBoxColumn
        Dim col As New DataGridViewTextBoxColumn With {.Name = name, .HeaderText = header, .SortMode = DataGridViewColumnSortMode.NotSortable}
        If fillWeight > 0 Then
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            col.FillWeight = fillWeight
            col.MinimumWidth = 60
        Else
            col.Width = width
        End If
        If bold Then col.DefaultCellStyle.Font = New Font("Segoe UI", 8.75F, FontStyle.Bold)
        Return col
    End Function

    Private Shared Function PillBack(status As String) As Color
        Select Case status
            Case TransactionStatus.Completed, StockStatus.InStock
                Return Color.FromArgb(225, 236, 225)
            Case TransactionStatus.Refunded, StockStatus.OutOfStock
                Return Color.FromArgb(250, 226, 222)
            Case StockStatus.LowStock
                Return Color.FromArgb(250, 235, 210)
            Case Else
                Return Color.FromArgb(236, 230, 224)
        End Select
    End Function

    Private Shared Function PillFore(status As String) As Color
        Select Case status
            Case TransactionStatus.Completed, StockStatus.InStock
                Return Color.FromArgb(70, 110, 70)
            Case TransactionStatus.Refunded, StockStatus.OutOfStock
                Return Color.FromArgb(176, 72, 48)
            Case StockStatus.LowStock
                Return Color.FromArgb(170, 105, 30)
            Case Else
                Return Color.FromArgb(110, 95, 88)
        End Select
    End Function

    Private Shared Sub DrawPill(g As Graphics, cell As Rectangle, text As String, back As Color, fore As Color)
        g.SmoothingMode = SmoothingMode.AntiAlias
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit
        Using f As New Font("Segoe UI", 8.0F, FontStyle.Bold)
            Dim sz As SizeF = g.MeasureString(text, f)
            Dim w As Integer = CInt(sz.Width) + 18
            Dim h As Integer = 22
            Dim r As New Rectangle(cell.X + 10, cell.Y + (cell.Height - h) \ 2, w, h)
            Using path As GraphicsPath = RoundRectPath(r, 10, False), b As New SolidBrush(back), tb As New SolidBrush(fore)
                g.FillPath(b, path)
                g.DrawString(text, f, tb, r.X + 9, r.Y + (h - sz.Height) / 2.0F + 1.0F)
            End Using
        End Using
    End Sub

    Private Sub LayoutKpiRows()
        LayoutKpiRow(pnl_History, New Control() {pnlHK1, pnlHK2, pnlHK3, pnlHK4})
        LayoutKpiRow(Panel1, New Control() {pnlIK1, pnlIK2, pnlIK3, pnlIK4})
    End Sub

    Private Sub LayoutKpiRow(host As Control, cards As Control())
        Const gap As Integer = 14
        Dim w As Integer = (host.ClientSize.Width - 40 - gap * 3) \ 4
        If w < 120 Then Return
        For i As Integer = 0 To cards.Length - 1
            cards(i).SetBounds(20 + i * (w + gap), cards(i).Top, w, cards(i).Height)
        Next
    End Sub

    ''' <summary>The bell + profile chip + dropdown are shared: move them to whichever page is showing.</summary>
    Private Sub AttachTopBar(host As Control, rightMargin As Integer)
        pnlProfileMenu.Visible = False
        If pnlUserChip.Parent IsNot host Then
            host.Controls.Add(pnlUserChip)
            host.Controls.Add(btnBell)
            host.Controls.Add(pnlProfileMenu)
        End If
        pnlUserChip.Left = host.ClientSize.Width - rightMargin - pnlUserChip.Width
        pnlUserChip.Top = 12
        btnBell.Left = pnlUserChip.Left - 10 - btnBell.Width
        btnBell.Top = 12
        pnlProfileMenu.Left = pnlUserChip.Left
        pnlProfileMenu.Top = 56
        For Each c As Control In New Control() {pnlUserChip, btnBell, pnlProfileMenu}
            c.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Next
        pnlUserChip.BringToFront()
        btnBell.BringToFront()
        pnlProfileMenu.BringToFront()
    End Sub

    Private Shared Function CsvCell(value As String) As String
        If value Is Nothing Then Return ""
        If value.IndexOfAny(New Char() {","c, """"c, ControlChars.Cr, ControlChars.Lf}) >= 0 Then
            Return """" & value.Replace("""", """""") & """"
        End If
        Return value
    End Function

    Private Sub SaveCsv(defaultName As String, headers As String(), rows As List(Of String()))
        Using dlg As New SaveFileDialog()
            dlg.Filter = "CSV file (*.csv)|*.csv"
            dlg.FileName = defaultName & "_" & DateTime.Now.ToString("yyyyMMdd_HHmm") & ".csv"
            If dlg.ShowDialog(Me) <> DialogResult.OK Then Return
            Try
                Dim sb As New System.Text.StringBuilder()
                Dim cells As New List(Of String)
                For Each h As String In headers
                    cells.Add(CsvCell(h))
                Next
                sb.AppendLine(String.Join(",", cells.ToArray()))
                For Each r As String() In rows
                    cells.Clear()
                    For Each v As String In r
                        cells.Add(CsvCell(v))
                    Next
                    sb.AppendLine(String.Join(",", cells.ToArray()))
                Next
                System.IO.File.WriteAllText(dlg.FileName, sb.ToString(), New System.Text.UTF8Encoding(True))
                MessageBox.Show("Saved " & rows.Count.ToString() & " rows.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Export", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    '=================================================================
    ' PRODUCT MENU  (price is ALWAYS read from product.Price)
    '=================================================================
    Private Sub LoadProducts()

        fl_Menu.SuspendLayout()
        ClearMenuCards()

        Dim searchText As String = txt_SearchMenu.Text.Trim().ToLower()
        Dim cardW As Integer = Math.Max(120, (fl_Menu.Width - SystemInformation.VerticalScrollBarWidth - 4 * 10 - 2) \ 4)
        Dim shown As Integer = 0

        For Each product As Product In DataStore.Products

            If searchText <> "" Then
                If Not product.Name.ToLower().Contains(searchText) AndAlso
                   Not product.Category.ToLower().Contains(searchText) AndAlso
                   Not product.Description.ToLower().Contains(searchText) Then
                    Continue For
                End If
            End If

            If selectedCategory <> "All" Then
                If Not CategoryMatches(product.Category, selectedCategory) Then Continue For
            End If

            fl_Menu.Controls.Add(BuildProductCard(product, cardW))
            shown += 1
        Next

        fl_Menu.ResumeLayout()

        If lblMenuTitle IsNot Nothing Then
            lblMenuTitle.Text = If(selectedCategory = "All", "Special Menu All Items", selectedCategory & " Items")
            lblAvailable.Text = ChrW(&H25CF) & "  " & shown.ToString() & " items available"
            lblAvailable.Left = fl_Menu.Right - lblAvailable.Width
        End If
        UpdateCategoryTiles()
        lastMenuWidth = fl_Menu.Width
        menuReady = True
    End Sub

    '=================================================================
    ' MENU SCREEN  (layout is in Cashier_Designer.vb; this is what must be code)
    '=================================================================

    '=================================================================
    ' MENU SCREEN THEME  (header row, banner, category tiles, product cards, cart, side nav)
    '=================================================================
    Private Function BuildProductCard(product As Product, cardW As Integer) As Control

        Dim card As New Guna2Panel With {
            .Size = New Size(cardW, 156), .Margin = New Padding(0, 0, 10, 12),
            .BorderRadius = 12, .FillColor = CardFill, .BorderColor = CardBorder,
            .BorderThickness = 1, .BackColor = PageBg}

        Dim stockLevel As String = DataStore.GetProductStockStatus(product)
        Dim badge As String = If(stockLevel <> StockStatus.InStock AndAlso product.Stock > 0, "Low stock", "")

        Dim pic As New PictureBox With {
            .Location = New Point(1, 1), .Size = New Size(cardW - 2, 80),
            .BackColor = CardFill, .SizeMode = PictureBoxSizeMode.Normal}
        Dim photo As Bitmap = MakeCardPhoto(product.Image, pic.Width, pic.Height, badge)
        cardImages.Add(photo)
        pic.Image = photo

        Dim nameLabel As New Label With {
            .Text = product.Name, .Location = New Point(10, 87), .Size = New Size(cardW - 20, 20),
            .Font = New Font("Segoe UI Semibold", 9.5F), .ForeColor = Ink,
            .BackColor = CardFill, .AutoEllipsis = True}

        Dim infoText As String = product.Description
        Dim infoColor As Color = Muted
        If stockLevel <> StockStatus.InStock Then
            infoText = If(product.Stock <= 0, "Out of stock", "Only " & product.Stock.ToString() & " left")
            infoColor = StatusColor(stockLevel)
        End If
        Dim infoLabel As New Label With {
            .Text = infoText, .Location = New Point(10, 107), .Size = New Size(cardW - 20, 16),
            .Font = New Font("Segoe UI", 8.0F), .ForeColor = infoColor,
            .BackColor = CardFill, .AutoEllipsis = True}

        Dim priceLabel As New Label With {
            .Text = Peso(product.Price), .Location = New Point(10, 127), .Size = New Size(cardW - 78, 22),
            .Font = New Font("Segoe UI", 10.5F, FontStyle.Bold), .ForeColor = Ink,
            .BackColor = CardFill, .TextAlign = ContentAlignment.MiddleLeft}

        Dim addButton As New Guna2Button With {
            .Size = New Size(56, 26), .Location = New Point(cardW - 66, 125),
            .BorderRadius = 7, .Animated = True, .Tag = product,
            .Font = New Font("Segoe UI", 8.5F, FontStyle.Bold), .Cursor = Cursors.Hand}

        If product.Stock > 0 Then
            addButton.Text = "+ Add"
            addButton.FillColor = AccentSoft
            addButton.ForeColor = Ink
            addButton.HoverState.FillColor = Accent
            addButton.HoverState.ForeColor = Color.White
        Else
            addButton.Text = "Sold out"
            addButton.Enabled = False
            addButton.DisabledState.FillColor = Color.FromArgb(232, 226, 220)
            addButton.DisabledState.ForeColor = Muted
            addButton.DisabledState.BorderColor = Color.FromArgb(232, 226, 220)
            addButton.DisabledState.CustomBorderColor = Color.FromArgb(232, 226, 220)
        End If
        AddHandler addButton.Click, AddressOf OrderButton_Click

        card.Controls.Add(pic)
        card.Controls.Add(nameLabel)
        card.Controls.Add(infoLabel)
        card.Controls.Add(priceLabel)
        card.Controls.Add(addButton)
        Return card
    End Function


    Private Sub ClearMenuCards()
        For i As Integer = fl_Menu.Controls.Count - 1 To 0 Step -1
            Dim c As Control = fl_Menu.Controls(i)
            fl_Menu.Controls.RemoveAt(i)
            c.Dispose()
        Next
        DisposeCardImages()
    End Sub


    Private Sub DisposeCardImages()
        For Each img As Image In cardImages
            img.Dispose()
        Next
        cardImages.Clear()
    End Sub


    Private Sub DisposeCartThumbs()
        For Each img As Image In cartThumbs
            img.Dispose()
        Next
        cartThumbs.Clear()
    End Sub

    Private Sub ApplyCashierTheme()

        ' name + photo of whoever is logged in (icons/logos are placed by you in the designer)
        LoadCurrentProfile()

        ' banner: keep the designer image as the source, draw the title/text on top at runtime
        If topimage.Image IsNot Nothing Then bannerSource = New Bitmap(topimage.Image)
        topimage.SizeMode = PictureBoxSizeMode.Normal

        ' category tiles (the 5 existing buttons keep their click handlers)
        catButtons = {btn_All, btn_hotCoffee, btn_IcedCoffee, btn_Specialty, btn_Non_Coffee}
        UpdateCategoryTiles()
        UpdatePaymentTiles()

        AddHandler fl_Menu.SizeChanged, AddressOf FlMenu_SizeChanged

        ' clicking on empty areas closes the profile dropdown / card chooser
        For Each host As Control In New Control() {pnl_PointOfSale, fl_Menu, topimage, fl_MenuProduct}
            AddHandler host.Click, AddressOf ClosePopups
        Next
        AddHandler topimage.SizeChanged, Sub(s As Object, ev As EventArgs) RenderBanner()
        LayoutCategoryTiles()
        RenderBanner()
        UpdateCheckoutTexts()
    End Sub

    '=================================================================
    ' LOGGED-IN CASHIER PROFILE  (name + photo) AND PROFILE DROPDOWN
    '=================================================================
    Private Function FindCurrentAccount() As CashierAccount
        For Each acc As CashierAccount In DataStore.Cashiers
            If String.Equals(acc.Username, CurrentSession.Username, StringComparison.OrdinalIgnoreCase) Then Return acc
        Next
        For Each acc As CashierAccount In DataStore.Cashiers
            If String.Equals(acc.FullName, CurrentSession.FullName, StringComparison.OrdinalIgnoreCase) Then Return acc
        Next
        Return Nothing
    End Function

    ''' <summary>Fills the profile chip and the Register card with the cashier who logged in.</summary>
    Private Sub LoadCurrentProfile()
        Dim displayName As String = CurrentSession.FullName
        Dim photo As Image = Nothing
        Try
            Dim acc As CashierAccount = FindCurrentAccount()
            If acc IsNot Nothing Then
                If Not String.IsNullOrWhiteSpace(acc.FullName) Then displayName = acc.FullName
                photo = CashierPhotos.Load(acc.Id)
            End If
        Catch ex As Exception
            photo = Nothing
        End Try
        If String.IsNullOrWhiteSpace(displayName) Then displayName = "Cashier"

        lblUserName.Text = displayName
        lblRegName.Text = displayName

        Dim oldAvatar As Image = picAvatar.Image
        picAvatar.Image = MakeAvatar(displayName, 30, photo)
        If oldAvatar IsNot Nothing Then oldAvatar.Dispose()

        If profilePhoto IsNot Nothing Then profilePhoto.Dispose()
        profilePhoto = photo
    End Sub

    Private Sub ProfileChip_Click(sender As Object, e As EventArgs) Handles pnlUserChip.Click, picAvatar.Click, lblUserName.Click, lblUserRole.Click, lblChevron.Click
        If pnlProfileMenu.Visible Then
            pnlProfileMenu.Visible = False
        Else
            LoadCurrentProfile()
            pnlCardSelect.Visible = False
            pnlProfileMenu.Visible = True
            pnlProfileMenu.BringToFront()
        End If
    End Sub

    Private Sub ClosePopups(sender As Object, e As EventArgs)
        pnlProfileMenu.Visible = False
        pnlCardSelect.Visible = False
    End Sub

    Private Sub btnMenuSettings_Click(sender As Object, e As EventArgs) Handles btnMenuSettings.Click
        pnlProfileMenu.Visible = False
        MessageBox.Show("There are no cashier settings yet.", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnMenuLogout_Click(sender As Object, e As EventArgs) Handles btnMenuLogout.Click
        pnlProfileMenu.Visible = False
        btnCashierLogout.PerformClick()
    End Sub

    Private Sub btnMenuProfile_Click(sender As Object, e As EventArgs) Handles btnMenuProfile.Click
        pnlProfileMenu.Visible = False
        LoadCurrentProfile()

        Dim acc As CashierAccount = FindCurrentAccount()
        Dim displayName As String = lblUserName.Text
        Dim userName As String = CurrentSession.Username
        Dim statusText As String = If(acc Is Nothing OrElse acc.IsActive, "Active", "Inactive")

        Using dlg As New Form()
            dlg.Text = "My Profile"
            dlg.FormBorderStyle = FormBorderStyle.FixedDialog
            dlg.StartPosition = FormStartPosition.CenterParent
            dlg.MaximizeBox = False
            dlg.MinimizeBox = False
            dlg.ShowInTaskbar = False
            dlg.ClientSize = New Size(320, 300)
            dlg.BackColor = PageBg

            Dim big As Bitmap = MakeAvatar(displayName, 110, profilePhoto)
            Dim pic As New PictureBox With {.Image = big, .Size = New Size(110, 110),
                                            .Location = New Point(105, 24), .BackColor = PageBg}

            Dim nameLbl As New Label With {.Text = displayName, .AutoSize = False, .Size = New Size(300, 28),
                .Location = New Point(10, 148), .TextAlign = ContentAlignment.MiddleCenter,
                .Font = New Font("Segoe UI", 14.0F, FontStyle.Bold), .ForeColor = Ink, .BackColor = PageBg}
            Dim userLbl As New Label With {.Text = "@" & userName, .AutoSize = False, .Size = New Size(300, 20),
                .Location = New Point(10, 178), .TextAlign = ContentAlignment.MiddleCenter,
                .Font = New Font("Segoe UI", 9.5F), .ForeColor = Muted, .BackColor = PageBg}
            Dim roleLbl As New Label With {.Text = "Cashier  " & ChrW(&H2022) & "  " & statusText, .AutoSize = False,
                .Size = New Size(300, 20), .Location = New Point(10, 200), .TextAlign = ContentAlignment.MiddleCenter,
                .Font = New Font("Segoe UI", 9.5F, FontStyle.Bold), .ForeColor = Accent, .BackColor = PageBg}

            Dim closeBtn As New Guna2Button With {.Text = "Close", .Size = New Size(120, 38), .Location = New Point(100, 240),
                .BorderRadius = 10, .FillColor = Accent, .ForeColor = Color.White,
                .Font = New Font("Segoe UI", 9.5F, FontStyle.Bold), .Cursor = Cursors.Hand, .BackColor = PageBg}
            AddHandler closeBtn.Click, Sub(s As Object, ev As EventArgs) dlg.Close()

            dlg.Controls.AddRange(New Control() {pic, nameLbl, userLbl, roleLbl, closeBtn})
            dlg.ShowDialog(Me)
            big.Dispose()
        End Using
    End Sub

    Private Sub FlMenu_SizeChanged(sender As Object, e As EventArgs)
        LayoutCategoryTiles()
        If menuReady AndAlso fl_Menu.Width <> lastMenuWidth Then LoadProducts()
    End Sub

    ''' <summary>Spreads the 5 category tiles across the same width as the product grid.</summary>
    Private Sub LayoutCategoryTiles()
        If catButtons Is Nothing Then Return
        Const gap As Integer = 10
        Dim tileW As Integer = (fl_Menu.Width - gap * (catButtons.Length - 1)) \ catButtons.Length
        If tileW < 60 Then Return
        For i As Integer = 0 To catButtons.Length - 1
            catButtons(i).SetBounds(fl_Menu.Left + i * (tileW + gap), catButtons(i).Top, tileW, catButtons(i).Height)
        Next
    End Sub

    Private Sub lblSeeAll_Click(sender As Object, e As EventArgs) Handles lblSeeAll.Click
        selectedCategory = "All"
        LoadProducts()
    End Sub

    Private Sub btnBell_Click(sender As Object, e As EventArgs) Handles btnBell.Click
        btn_CashierMessages.PerformClick()
    End Sub

    Private Sub tileCash_Click(sender As Object, e As EventArgs) Handles tileCash.Click
        selectedPayment = PaymentMethods.Cash
        pnlCardSelect.Visible = False
        ResetCardSelection()
        UpdatePaymentTiles()
    End Sub

    Private Sub tileCard_Click(sender As Object, e As EventArgs) Handles tileCard.Click
        selectedPayment = PaymentMethods.Card
        UpdatePaymentTiles()
        ShowCardChooser()
    End Sub

    '---------------- card chooser popup ----------------
    Private Sub ShowCardChooser()
        pnlProfileMenu.Visible = False
        HighlightCardChoice()
        pnlCardSelect.Visible = True
        pnlCardSelect.BringToFront()
    End Sub

    Private Sub CardOption_Click(sender As Object, e As EventArgs) Handles btnCardDebit.Click, btnCardCredit.Click, btnCardPrepaid.Click
        Dim b As Guna2Button = TryCast(sender, Guna2Button)
        If b Is Nothing Then Return
        selectedCardType = b.Text
        tileCard.Text = selectedCardType
        HighlightCardChoice()
        pnlCardSelect.Visible = False
    End Sub

    Private Sub btnCardClose_Click(sender As Object, e As EventArgs) Handles btnCardClose.Click
        pnlCardSelect.Visible = False
    End Sub

    Private Sub HighlightCardChoice()
        For Each b As Guna2Button In New Guna2Button() {btnCardDebit, btnCardCredit, btnCardPrepaid}
            Dim chosen As Boolean = (b.Text = selectedCardType)
            b.FillColor = If(chosen, AccentSoft, CardFill)
            b.BorderColor = If(chosen, Accent, CardBorder)
            b.Font = New Font("Segoe UI", 9.5F, If(chosen, FontStyle.Bold, FontStyle.Regular))
        Next
    End Sub

    Private Sub ResetCardSelection()
        selectedCardType = ""
        tileCard.Text = "Card"
    End Sub

    Private Sub UpdatePaymentTiles()
        Dim cash As Boolean = (selectedPayment = PaymentMethods.Cash)
        StylePayTile(tileCash, cash)
        StylePayTile(tileCard, Not cash)
        txt_Cash_Receive.Visible = cash
        Guna2HtmlLabel24.Visible = cash
        lbl_Change.Visible = cash
    End Sub

    Private Sub StylePayTile(b As Guna2Button, active As Boolean)
        b.FillColor = If(active, AccentSoft, CardFill)
        b.BorderColor = If(active, Accent, CardBorder)
        b.ForeColor = If(active, Ink, Muted)
        b.Font = New Font("Segoe UI", 8.5F, If(active, FontStyle.Bold, FontStyle.Regular))
        b.HoverState.FillColor = If(active, AccentSoft, Color.FromArgb(255, 245, 236))
        b.HoverState.ForeColor = Ink
    End Sub

    ''' <summary>Tax caption (uses the tax rate from settings) and the "Continue to Payment" text.</summary>
    Private Sub UpdateCheckoutTexts()
        Guna2HtmlLabel18.Text = "Tax (" & (DataStore.PosSettings.TaxRate * 100D).ToString("0.##") & "%)"
        btn_chkout.Text = "Continue to Payment    " & lbl_Total.Text & "  " & ChrW(&H2192)
    End Sub




    Private Sub UpdateCategoryTiles()
        If catButtons Is Nothing Then Return
        For i As Integer = 0 To catButtons.Length - 1
            Dim b As Guna2Button = catButtons(i)
            Dim active As Boolean = (CategoryKeys(i) = selectedCategory)
            b.FillColor = If(active, Accent, CardFill)
            b.ForeColor = If(active, Color.White, Ink)
            b.BorderColor = If(active, Accent, CardBorder)
            b.HoverState.FillColor = If(active, Accent, Color.FromArgb(255, 245, 236))
            b.HoverState.ForeColor = If(active, Color.White, Ink)
        Next
    End Sub


    Private Sub RenderBanner()
        If topimage Is Nothing OrElse topimage.Width < 80 OrElse topimage.Height < 40 Then Return
        Dim w As Integer = topimage.Width
        Dim h As Integer = topimage.Height
        Dim bmp As New Bitmap(w, h)
        Using g As Graphics = Graphics.FromImage(bmp)
            g.SmoothingMode = SmoothingMode.AntiAlias
            g.InterpolationMode = InterpolationMode.HighQualityBicubic
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit
            If bannerSource IsNot Nothing Then
                DrawCover(g, bannerSource, New Rectangle(0, 0, w, h))
            Else
                g.Clear(Color.FromArgb(70, 45, 36))
            End If
            Using shade As New LinearGradientBrush(New Rectangle(0, 0, w, h),
                                                   Color.FromArgb(225, 28, 18, 14), Color.FromArgb(30, 28, 18, 14), 0.0F)
                g.FillRectangle(shade, 0, 0, w, h)
            End Using
            Using f1 As New Font("Segoe UI", 21.0F, FontStyle.Bold),
                  f2 As New Font("Segoe UI", 10.5F),
                  sub1 As New SolidBrush(Color.FromArgb(240, 230, 220))
                g.DrawString(BannerTitle, f1, Brushes.White, 20, 16)
                g.DrawString(BannerSubtitle, f2, sub1, 22, 56)
            End Using
        End Using
        Dim old As Bitmap = bannerRendered
        bannerRendered = bmp
        topimage.Image = bmp
        If old IsNot Nothing Then old.Dispose()
    End Sub

    '---------------- drawing helpers ----------------
    ''' <summary>Round profile picture: the cashier's photo if there is one, otherwise their initials.</summary>
    Private Shared Function MakeAvatar(fullName As String, size As Integer, photo As Image) As Bitmap
        Dim bmp As New Bitmap(size, size)
        Using g As Graphics = Graphics.FromImage(bmp)
            g.SmoothingMode = SmoothingMode.AntiAlias
            g.InterpolationMode = InterpolationMode.HighQualityBicubic
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit
            Using circle As New GraphicsPath()
                circle.AddEllipse(0, 0, size - 1, size - 1)
                If photo IsNot Nothing Then
                    g.SetClip(circle)
                    DrawCover(g, photo, New Rectangle(0, 0, size, size))
                    g.ResetClip()
                Else
                    Dim initials As String = ""
                    For Each part As String In fullName.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
                        initials &= Char.ToUpper(part(0))
                        If initials.Length = 2 Then Exit For
                    Next
                    If initials = "" Then initials = "C"
                    Using b As New SolidBrush(Accent)
                        g.FillPath(b, circle)
                    End Using
                    Using f As New Font("Segoe UI", size * 0.36F, FontStyle.Bold),
                          sf As New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
                        g.DrawString(initials, f, Brushes.White, New RectangleF(0, 0, size, size), sf)
                    End Using
                End If
            End Using
        End Using
        Return bmp
    End Function

    ''' <summary>Fits the WHOLE picture inside dest (no zoom, no cropping); the leftover space is the background colour.</summary>
    Private Shared Sub DrawContain(g As Graphics, img As Image, dest As Rectangle)
        Dim scale As Single = Math.Min(dest.Width / CSng(img.Width), dest.Height / CSng(img.Height))
        Dim w As Integer = Math.Max(1, CInt(img.Width * scale))
        Dim h As Integer = Math.Max(1, CInt(img.Height * scale))
        g.DrawImage(img, New Rectangle(dest.X + (dest.Width - w) \ 2, dest.Y + (dest.Height - h) \ 2, w, h))
    End Sub

    Private Shared Sub DrawCover(g As Graphics, img As Image, dest As Rectangle)
        Dim scale As Single = Math.Max(dest.Width / CSng(img.Width), dest.Height / CSng(img.Height))
        Dim sw As Single = dest.Width / scale
        Dim sh As Single = dest.Height / scale
        g.DrawImage(img, dest, (img.Width - sw) / 2.0F, (img.Height - sh) / 2.0F, sw, sh, GraphicsUnit.Pixel)
    End Sub


    Private Shared Function RoundRectPath(r As Rectangle, rad As Integer, topOnly As Boolean) As GraphicsPath
        Dim p As New GraphicsPath()
        Dim d As Integer = rad * 2
        p.AddArc(r.X, r.Y, d, d, 180, 90)
        p.AddArc(r.Right - d, r.Y, d, d, 270, 90)
        If topOnly Then
            p.AddLine(r.Right, r.Y + rad, r.Right, r.Bottom)
            p.AddLine(r.Right, r.Bottom, r.X, r.Bottom)
        Else
            p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90)
            p.AddArc(r.X, r.Bottom - d, d, d, 90, 90)
        End If
        p.CloseFigure()
        Return p
    End Function


    Private Shared Function MakeCardPhoto(src As Image, w As Integer, h As Integer, badge As String) As Bitmap
        Dim bmp As New Bitmap(w, h)
        Using g As Graphics = Graphics.FromImage(bmp)
            g.SmoothingMode = SmoothingMode.AntiAlias
            g.InterpolationMode = InterpolationMode.HighQualityBicubic
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit
            g.Clear(CardFill)
            Using path As GraphicsPath = RoundRectPath(New Rectangle(0, 0, w - 1, h), 11, True)
                g.SetClip(path)
                g.Clear(PhotoBg)
                If src IsNot Nothing Then DrawContain(g, src, New Rectangle(0, 0, w, h))
            End Using
            g.ResetClip()
            If badge <> "" Then
                Using f As New Font("Segoe UI", 7.5F, FontStyle.Bold)
                    Dim sz As SizeF = g.MeasureString(badge, f)
                    Dim rc As New Rectangle(8, 8, CInt(sz.Width) + 6, 18)
                    Using bp As GraphicsPath = RoundRectPath(rc, 8, False),
                          fillB As New SolidBrush(Color.FromArgb(240, 255, 250, 244)),
                          txtB As New SolidBrush(Accent)
                        g.FillPath(fillB, bp)
                        g.DrawString(badge, f, txtB, rc.X + 3, rc.Y + 2)
                    End Using
                End Using
            End If
        End Using
        Return bmp
    End Function


    Private Shared Function MakeThumb(src As Image, size As Integer) As Bitmap
        Dim bmp As New Bitmap(size, size)
        Using g As Graphics = Graphics.FromImage(bmp)
            g.SmoothingMode = SmoothingMode.AntiAlias
            g.InterpolationMode = InterpolationMode.HighQualityBicubic
            g.Clear(Color.Transparent)
            Using path As GraphicsPath = RoundRectPath(New Rectangle(0, 0, size - 1, size - 1), 9, False)
                g.SetClip(path)
                g.Clear(PhotoBg)
                If src IsNot Nothing Then DrawContain(g, src, New Rectangle(0, 0, size, size))
            End Using
        End Using
        Return bmp
    End Function



    Private Function CategoryMatches(productCategory As String, selectedCat As String) As Boolean

        Dim category As String = productCategory.Trim().ToLower()
        Dim selected As String = selectedCat.Trim().ToLower()

        If selected = "all" Then Return True
        If selected = "hot coffee" Then Return category.Contains("hot")
        If selected = "iced coffee" Then Return category.Contains("iced")
        If selected = "specialty" Then Return category.Contains("special")
        If selected = "non coffee" Then Return category.Contains("non")

        Return category = selected
    End Function

    '=================================================================
    ' CART
    '=================================================================
    Private Sub OrderButton_Click(sender As Object, e As EventArgs)

        Dim orderButton As Control = TryCast(sender, Control)
        If orderButton Is Nothing Then Exit Sub

        Dim product As Product = TryCast(orderButton.Tag, Product)
        If product Is Nothing Then Exit Sub

        If product.Stock <= 0 Then
            MessageBox.Show("This product is out of stock.", "Order", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            LoadProducts()
            Exit Sub
        End If

        If cart.ContainsKey(product) Then
            If cart(product) >= product.Stock Then
                MessageBox.Show("You cannot order more than the available stock.", "Stock Limit",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            cart(product) += 1
        Else
            cart.Add(product, 1)
        End If

        UpdateCartDisplay()
    End Sub

    ''' <summary>After Admin changes prices / stock / deletes products the cart is re-read from the catalog.</summary>
    Private Sub SyncCartWithCatalog()
        For Each p As Product In New List(Of Product)(cart.Keys)
            If Not DataStore.Products.Contains(p) OrElse p.Stock <= 0 Then
                cart.Remove(p)
            ElseIf cart(p) > p.Stock Then
                cart(p) = p.Stock
            End If
        Next
        UpdateCartDisplay()
    End Sub

    Private Sub UpdateCartDisplay()

        fl_MenuProduct.SuspendLayout()
        fl_MenuProduct.Controls.Clear()
        DisposeCartThumbs()

        Dim itemW As Integer = Math.Max(220, fl_MenuProduct.ClientSize.Width - 16 - SystemInformation.VerticalScrollBarWidth - 6)
        Dim itemCount As Integer = 0

        For Each item As KeyValuePair(Of Product, Integer) In cart

            Dim product As Product = item.Key
            Dim quantity As Integer = item.Value
            itemCount += quantity

            Dim row As New Panel()
            row.Width = itemW
            row.Height = 72
            row.Margin = New Padding(16, 2, 0, 8)
            row.BackColor = CardFill

            Dim thumb As Bitmap = MakeThumb(product.Image, 56)
            cartThumbs.Add(thumb)
            Dim pic As New PictureBox()
            pic.Image = thumb
            pic.Size = New Size(56, 56)
            pic.Location = New Point(0, 4)
            pic.BackColor = CardFill

            Dim nameLabel As New Label()
            nameLabel.Text = product.Name
            nameLabel.AutoEllipsis = True
            nameLabel.Size = New Size(itemW - 66 - 80, 18)
            nameLabel.Location = New Point(66, 4)
            nameLabel.Font = New Font("Segoe UI Semibold", 9.5F)
            nameLabel.ForeColor = Ink
            nameLabel.BackColor = CardFill

            Dim itemTotalLabel As New Label()
            itemTotalLabel.Text = Peso(product.Price * quantity)
            itemTotalLabel.Size = New Size(80, 18)
            itemTotalLabel.Location = New Point(itemW - 80, 4)
            itemTotalLabel.TextAlign = ContentAlignment.MiddleRight
            itemTotalLabel.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
            itemTotalLabel.ForeColor = Ink
            itemTotalLabel.BackColor = CardFill

            Dim priceLabel As New Label()
            priceLabel.Text = Peso(product.Price) & " each"
            priceLabel.Size = New Size(itemW - 66, 15)
            priceLabel.Location = New Point(66, 23)
            priceLabel.Font = New Font("Segoe UI", 8.0F)
            priceLabel.ForeColor = Muted
            priceLabel.BackColor = CardFill

            Dim minusButton As New Guna2Button()
            minusButton.Text = "-"
            minusButton.Size = New Size(28, 28)
            minusButton.Location = New Point(66, 40)
            minusButton.BorderThickness = 1
            minusButton.BorderColor = Color.FromArgb(226, 190, 165)
            minusButton.BorderRadius = 6
            minusButton.FillColor = AccentSoft
            minusButton.ForeColor = Ink
            minusButton.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
            minusButton.HoverState.FillColor = Accent
            minusButton.HoverState.ForeColor = Color.White
            minusButton.Cursor = Cursors.Hand
            minusButton.BackColor = CardFill

            Dim quantityLabel As New Label()
            quantityLabel.Text = quantity.ToString()
            quantityLabel.Size = New Size(28, 28)
            quantityLabel.Location = New Point(95, 40)
            quantityLabel.TextAlign = ContentAlignment.MiddleCenter
            quantityLabel.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
            quantityLabel.ForeColor = Ink
            quantityLabel.BackColor = CardFill

            Dim plusButton As New Guna2Button()
            plusButton.Text = "+"
            plusButton.Size = New Size(28, 28)
            plusButton.Location = New Point(124, 40)
            plusButton.BorderThickness = 1
            plusButton.BorderColor = Color.FromArgb(226, 190, 165)
            plusButton.BorderRadius = 6
            plusButton.FillColor = AccentSoft
            plusButton.ForeColor = Ink
            plusButton.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
            plusButton.HoverState.FillColor = Accent
            plusButton.HoverState.ForeColor = Color.White
            plusButton.Cursor = Cursors.Hand
            plusButton.BackColor = CardFill

            Dim deleteButton As New Label()
            deleteButton.Text = "Remove"
            deleteButton.Size = New Size(64, 18)
            deleteButton.Location = New Point(itemW - 64, 46)
            deleteButton.TextAlign = ContentAlignment.MiddleRight
            deleteButton.Font = New Font("Segoe UI", 8.0F, FontStyle.Bold)
            deleteButton.ForeColor = Color.FromArgb(176, 72, 48)
            deleteButton.BackColor = CardFill
            deleteButton.Cursor = Cursors.Hand

            AddHandler minusButton.Click, Sub(s As Object, ev As EventArgs) DecreaseQuantity(product)
            AddHandler plusButton.Click, Sub(s As Object, ev As EventArgs) IncreaseQuantity(product)
            AddHandler deleteButton.Click, Sub(s As Object, ev As EventArgs) DeleteCartItem(product)

            row.Controls.Add(pic)
            row.Controls.Add(nameLabel)
            row.Controls.Add(itemTotalLabel)
            row.Controls.Add(priceLabel)
            row.Controls.Add(minusButton)
            row.Controls.Add(quantityLabel)
            row.Controls.Add(plusButton)
            row.Controls.Add(deleteButton)

            fl_MenuProduct.Controls.Add(row)
        Next

        fl_MenuProduct.ResumeLayout()

        If lblCartCount IsNot Nothing Then lblCartCount.Text = itemCount.ToString() & If(itemCount = 1, " item", " items")
        If lblEmpty IsNot Nothing Then lblEmpty.Visible = (cart.Count = 0)

        UpdateTotals()
    End Sub

    Private Sub DeleteCartItem(product As Product)
        If Not cart.ContainsKey(product) Then Exit Sub
        cart.Remove(product)
        UpdateCartDisplay()
    End Sub

    Private Sub IncreaseQuantity(product As Product)
        If Not cart.ContainsKey(product) Then Exit Sub

        If cart(product) >= product.Stock Then
            MessageBox.Show("You cannot order more than the available stock.", "Stock Limit",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        cart(product) += 1
        UpdateCartDisplay()
    End Sub

    Private Sub DecreaseQuantity(product As Product)
        If Not cart.ContainsKey(product) Then Exit Sub

        cart(product) -= 1
        If cart(product) <= 0 Then cart.Remove(product)

        UpdateCartDisplay()
    End Sub

    '=================================================================
    ' TOTALS
    '=================================================================
    Private Sub ComputeTotals(ByRef subtotal As Decimal, ByRef tax As Decimal, ByRef total As Decimal)
        subtotal = 0D
        For Each item As KeyValuePair(Of Product, Integer) In cart
            subtotal += item.Key.Price * item.Value
        Next
        tax = Math.Round(subtotal * DataStore.PosSettings.TaxRate, 2, MidpointRounding.AwayFromZero)
        total = subtotal + tax
    End Sub

    Private Sub UpdateTotals()
        Dim subtotal, tax, total As Decimal
        ComputeTotals(subtotal, tax, total)

        lbl_Subtotal.Text = Peso(subtotal)
        lbl_Tax.Text = Peso(tax)
        lbl_Total.Text = Peso(total)

        UpdateChange()
    End Sub

    Private Sub UpdateChange()
        Dim subtotal, tax, total As Decimal
        ComputeTotals(subtotal, tax, total)

        Dim cashReceived As Decimal
        If TryParseMoney(txt_Cash_Receive.Text, cashReceived) AndAlso cashReceived >= total AndAlso total > 0D Then
            lbl_Change.Text = Peso(cashReceived - total)
        Else
            lbl_Change.Text = Peso(0D)
        End If
        UpdateCheckoutTexts()
    End Sub

    Private Sub txt_Cash_Receive_TextChanged(sender As Object, e As EventArgs) Handles txt_Cash_Receive.TextChanged
        UpdateChange()
    End Sub

    Private Sub ResetPaymentDisplay()
        lbl_Subtotal.Text = Peso(0D)
        lbl_Tax.Text = Peso(0D)
        lbl_Total.Text = Peso(0D)
        lbl_Change.Text = Peso(0D)
        UpdateCheckoutTexts()
    End Sub

    Private Sub ClearCart()
        cart.Clear()
        UpdateCartDisplay()
        txt_Cash_Receive.Clear()
        ResetCardSelection()
        ResetPaymentDisplay()
    End Sub

    Private Sub btn_Clear_Click(sender As Object, e As EventArgs) Handles btn_Clear.Click
        ClearCart()
    End Sub

    '=================================================================
    ' CHECKOUT  (btn_chkout pays with the selected tile: Cash or Card)
    '=================================================================
    Private Sub btn_chkout_Click(sender As Object, e As EventArgs) Handles btn_chkout.Click
        If selectedPayment = PaymentMethods.Card AndAlso selectedCardType = "" Then
            ShowCardChooser()
            Return
        End If
        ProcessCheckout(selectedPayment)
    End Sub

    Private Sub ProcessCheckout(method As String)

        If isProcessing Then Return                 ' blocks double-click / duplicate checkout
        isProcessing = True
        btn_chkout.Enabled = False

        Try
            If cart.Count = 0 Then
                MessageBox.Show("Your cart is empty.", "Checkout", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim subtotal, tax, total As Decimal
            ComputeTotals(subtotal, tax, total)

            Dim cashReceived As Decimal = 0D
            If method = PaymentMethods.Cash Then
                If Not TryParseMoney(txt_Cash_Receive.Text, cashReceived) Then
                    MessageBox.Show("Please enter the cash received (numbers only).", "Checkout",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txt_Cash_Receive.Focus()
                    Return
                End If
                If cashReceived < total Then
                    MessageBox.Show("Cash received is not enough.", "Checkout",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txt_Cash_Receive.Focus()
                    Return
                End If
            End If

            Dim lines As New List(Of KeyValuePair(Of Product, Integer))
            For Each item As KeyValuePair(Of Product, Integer) In cart
                lines.Add(New KeyValuePair(Of Product, Integer)(item.Key, item.Value))
            Next

            Dim errorMessage As String = ""
            Dim trx As POS_Transaction = DataStore.CreateSale(
                CurrentSession.FullName, CurrentSession.Username,
                lines, method, cashReceived, errorMessage)

            If trx Is Nothing Then
                MessageBox.Show(errorMessage, "Checkout", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                LoadProducts()
                SyncCartWithCatalog()
                Return
            End If

            Dim cardUsed As String = selectedCardType
            ClearCart()
            LoadProducts()

            Dim info As String = "Checkout successful!" & vbCrLf & vbCrLf &
                                 "Transaction: " & trx.TransactionID & vbCrLf &
                                 "Payment: " & trx.PaymentMethod & vbCrLf &
                                 "Total: " & Peso(trx.Total)
            If trx.PaymentMethod = PaymentMethods.Card AndAlso cardUsed <> "" Then
                info &= vbCrLf & "Card: " & cardUsed
            End If
            If trx.PaymentMethod = PaymentMethods.Cash Then
                info &= vbCrLf & "Cash: " & Peso(trx.CashReceived) & vbCrLf & "Change: " & Peso(trx.ChangeGiven)
            End If
            MessageBox.Show(info, "Checkout", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Checkout failed: " & ex.Message, "Checkout", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            isProcessing = False
            btn_chkout.Enabled = True
        End Try
    End Sub

    '=================================================================
    ' MENU FILTERS
    '=================================================================
    Private Sub txt_SearchMenu_TextChanged(sender As Object, e As EventArgs) Handles txt_SearchMenu.TextChanged
        LoadProducts()
    End Sub

    Private Sub btn_All_Click(sender As Object, e As EventArgs) Handles btn_All.Click
        selectedCategory = "All"
        LoadProducts()
    End Sub

    Private Sub btn_hotCoffee_Click(sender As Object, e As EventArgs) Handles btn_hotCoffee.Click
        selectedCategory = "Hot Coffee"
        LoadProducts()
    End Sub

    Private Sub btn_IcedCoffee_Click(sender As Object, e As EventArgs) Handles btn_IcedCoffee.Click
        selectedCategory = "Iced Coffee"
        LoadProducts()
    End Sub

    Private Sub btn_Specialty_Click(sender As Object, e As EventArgs) Handles btn_Specialty.Click
        selectedCategory = "Specialty"
        LoadProducts()
    End Sub

    Private Sub btn_Non_Coffee_Click(sender As Object, e As EventArgs) Handles btn_Non_Coffee.Click
        selectedCategory = "Non Coffee"
        LoadProducts()
    End Sub

    '=================================================================
    ' CASHIER CHAT (persistent)
    '=================================================================
    Private Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click

        If String.IsNullOrWhiteSpace(txtChat.Text) Then Return

        Dim newMessage As New ChatMessage With {
            .Sender = "Cashier",
            .Receiver = "Admin",
            .Message = txtChat.Text.Trim(),
            .TimeSent = DateTime.Now
        }

        txtChat.Clear()
        DataStore.AddMessage(newMessage)        ' saved; MessagesChanged reloads the chat
    End Sub

    Private Sub LoadCashierChat()

        flpMessages.Controls.Clear()

        For Each chat As ChatMessage In DataStore.ChatMessages

            Dim isMine As Boolean = (chat.Sender = "Cashier")

            Dim messagePanel As New RoundedPanel()
            messagePanel.Width = flpMessages.ClientSize.Width - 120
            messagePanel.Height = 90
            messagePanel.Margin = New Padding(5)
            messagePanel.Padding = New Padding(10)
            messagePanel.BackColor = If(isMine, brown, Color.FromKnownColor(KnownColor.ControlLight))

            Dim fore As Color = If(isMine, Color.White, brown)

            Dim senderLabel As New Label()
            senderLabel.Text = chat.Sender
            senderLabel.AutoSize = True
            senderLabel.Location = New Point(10, 8)
            senderLabel.Font = New Font("Segoe UI", 9, FontStyle.Bold)
            senderLabel.ForeColor = fore

            Dim messageLabel As New Label()
            messageLabel.Text = chat.Message
            messageLabel.AutoSize = False
            messageLabel.Width = messagePanel.Width - 20
            messageLabel.Height = 40
            messageLabel.Location = New Point(10, 28)
            messageLabel.Font = New Font("Segoe UI", 10, FontStyle.Regular)
            messageLabel.ForeColor = fore

            Dim timeLabel As New Label()
            timeLabel.Text = chat.TimeSent.ToString("MMM d, hh:mm tt")
            timeLabel.AutoSize = True
            timeLabel.Location = New Point(10, 68)
            timeLabel.Font = New Font("Segoe UI", 8, FontStyle.Regular)
            timeLabel.ForeColor = If(isMine, Color.LightGray, Color.Gray)

            messagePanel.Controls.Add(senderLabel)
            messagePanel.Controls.Add(messageLabel)
            messagePanel.Controls.Add(timeLabel)

            flpMessages.Controls.Add(messagePanel)
        Next

        If flpMessages.Controls.Count > 0 Then
            flpMessages.ScrollControlIntoView(flpMessages.Controls(flpMessages.Controls.Count - 1))
        End If
    End Sub

End Class