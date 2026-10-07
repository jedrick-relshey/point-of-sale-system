Option Strict On
Option Explicit On

Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports Guna.UI2.WinForms

' Admin_Dashboard.vb  -  NEW FILE (part of the Admin class)
' Builds the Dashboard page in code (header, 4 KPI cards, Sales performance chart,
' Recent transactions, Low-stock alerts). The old dashboard controls stay in the designer
' but are hidden, so the existing code that sets them keeps working.
Partial Public Class Admin

    Private dashBuilt As Boolean = False
    Private dashHeader As ForestHeader
    Private dashBody As Panel

    Private dashKpiSales As ForestKpi
    Private dashKpiWeek As ForestKpi
    Private dashKpiMonth As ForestKpi
    Private dashKpiTx As ForestKpi

    Private dashSalesCard As Guna2Panel
    Private dashSalesTitle As Label
    Private dashSalesSub As Label
    Private dashSalesValue As Label
    Private dashSalesDelta As Label
    Private dashSeg As ForestSegmented
    Private dashChart As ForestBarChart

    Private dashRecentCard As Guna2Panel
    Private dashRecentLink As Label
    Private dashRecentTable As ForestTable

    Private dashAlertCard As Guna2Panel
    Private dashAlertPill As Guna2Panel
    Private dashAlertList As Panel
    Private dashAlertBtn As Guna2Button

    '=================================================================
    ' BUILD
    '=================================================================
    Private Sub BuildDashboardUi()
        For Each c As Control In pnl_dashboard_system.Controls
            c.Visible = False
        Next
        pnl_dashboard_system.BackColor = ForestUi.Page

        dashBody = New Panel()
        dashBody.Dock = DockStyle.Fill
        dashBody.BackColor = ForestUi.Page
        dashBody.AutoScroll = True

        ' header
        dashHeader = New ForestHeader("Dashboard", "")
        Dim export As Guna2Button = ForestUi.TextButton("Export report", 130, 38, True)
        AddHandler export.Click, AddressOf DashExport_Click
        dashHeader.AddAction(export)
        dashHeader.SetUser(DashUserName(), "Admin")
        AddHandler dashHeader.LogoutClicked, Sub(s As Object, ev As EventArgs) DoLogout()
        AddHandler dashHeader.Bell.Click, Sub(s As Object, ev As EventArgs) btnMessages.PerformClick()

        ' KPI cards
        dashKpiSales = New ForestKpi("Total Sales Today", ChrW(&H20B1), ForestUi.AccentSoft, ForestUi.Accent)
        dashKpiWeek = New ForestKpi("Weekly Revenue", ChrW(&HE787), ForestUi.AccentSoft, ForestUi.Accent)
        dashKpiMonth = New ForestKpi("Monthly Revenue", ChrW(&H2197), ForestUi.AccentSoft, ForestUi.Accent)
        dashKpiTx = New ForestKpi("Total Transactions", ChrW(&HE7C3), ForestUi.AccentSoft, ForestUi.Accent)
        For Each k As ForestKpi In New ForestKpi() {dashKpiSales, dashKpiWeek, dashKpiMonth, dashKpiTx}
            dashBody.Controls.Add(k)
        Next

        BuildDashSalesCard()
        BuildDashRecentCard()
        BuildDashAlertCard()

        pnl_dashboard_system.Controls.Add(dashBody)
        pnl_dashboard_system.Controls.Add(dashHeader)

        AddHandler dashBody.Resize, Sub(s As Object, ev As EventArgs) LayoutDashboard()
        dashBuilt = True
        LayoutDashboard()
        RefreshDashboardUi()
    End Sub

    Private Sub BuildDashSalesCard()
        dashSalesCard = ForestUi.Card(600, 238, 14, ForestUi.CardFill, ForestUi.Border)
        dashSalesTitle = ForestUi.Lbl("Sales performance", 11.0F, FontStyle.Bold, ForestUi.Ink, 18, 14)
        dashSalesSub = ForestUi.Lbl("Revenue across your selected period", 8.0F, FontStyle.Regular, ForestUi.Muted, 19, 37)
        dashSalesValue = ForestUi.Lbl(Peso(0D), 19.0F, FontStyle.Bold, ForestUi.Ink, 18, 58)
        dashSalesDelta = ForestUi.Lbl("", 8.0F, FontStyle.Bold, ForestUi.OkFore, 150, 68)

        dashSeg = New ForestSegmented(New String() {"Daily", "Weekly", "Monthly"})
        AddHandler dashSeg.SelectedChanged, Sub(s As Object, ev As EventArgs) UpdateSalesChart()

        dashChart = New ForestBarChart()

        dashSalesCard.Controls.Add(dashSalesTitle)
        dashSalesCard.Controls.Add(dashSalesSub)
        dashSalesCard.Controls.Add(dashSalesValue)
        dashSalesCard.Controls.Add(dashSalesDelta)
        dashSalesCard.Controls.Add(dashSeg)
        dashSalesCard.Controls.Add(dashChart)
        dashBody.Controls.Add(dashSalesCard)
    End Sub

    Private Sub BuildDashRecentCard()
        dashRecentCard = ForestUi.Card(600, 260, 14, ForestUi.CardFill, ForestUi.Border)
        dashRecentCard.Controls.Add(ForestUi.Lbl("Recent transactions", 10.5F, FontStyle.Bold, ForestUi.Ink, 18, 14))
        dashRecentCard.Controls.Add(ForestUi.Lbl("Latest register activity", 8.0F, FontStyle.Regular, ForestUi.Muted, 19, 36))

        dashRecentLink = ForestUi.Lbl("View all transactions", 8.5F, FontStyle.Bold, ForestUi.Accent)
        dashRecentLink.Cursor = Cursors.Hand
        AddHandler dashRecentLink.Click, Sub(s As Object, ev As EventArgs) btnHistory.PerformClick()
        dashRecentCard.Controls.Add(dashRecentLink)

        dashRecentTable = New ForestTable()
        dashRecentTable.RowHeight = 40
        dashRecentTable.SetColumns(New TableColumn() {
            New TableColumn("Transaction", 1.25F, 90),
            New TableColumn("Time", 1.0F, 70),
            New TableColumn("Cashier", 1.2F, 90),
            New TableColumn("Items", 0.9F, 60),
            New TableColumn("Total", 1.0F, 70),
            New TableColumn("Status", 1.1F, 90)})
        dashRecentCard.Controls.Add(dashRecentTable)
        dashBody.Controls.Add(dashRecentCard)
    End Sub

    Private Sub BuildDashAlertCard()
        dashAlertCard = ForestUi.Card(300, 260, 14, ForestUi.CardFill, ForestUi.Border)

        Dim warn As New Label()
        warn.AutoSize = True
        warn.BackColor = Color.Transparent
        warn.ForeColor = ForestUi.Accent
        warn.Font = New Font("Segoe MDL2 Assets", 12.0F)
        warn.Text = ForestUi.GlyphWarn
        warn.Location = New Point(18, 17)
        dashAlertCard.Controls.Add(warn)
        dashAlertCard.Controls.Add(ForestUi.Lbl("Low-stock alerts", 10.5F, FontStyle.Bold, ForestUi.Ink, 44, 16))

        dashAlertList = New Panel()
        dashAlertList.BackColor = ForestUi.CardFill
        dashAlertCard.Controls.Add(dashAlertList)

        dashAlertBtn = ForestUi.TextButton(ChrW(&H2192) & "  Review inventory", 156, 34, False)
        AddHandler dashAlertBtn.Click, Sub(s As Object, ev As EventArgs) btnInventory.PerformClick()
        dashAlertCard.Controls.Add(dashAlertBtn)
        dashBody.Controls.Add(dashAlertCard)
    End Sub

    '=================================================================
    ' LAYOUT
    '=================================================================
    Private Sub LayoutDashboard()
        If dashBody Is Nothing OrElse dashSalesCard Is Nothing Then Return
        Dim pad As Integer = 22
        Dim gap As Integer = 14
        Dim w As Integer = Math.Max(640, dashBody.ClientSize.Width - pad * 2)

        ' KPI row
        Dim kw As Integer = (w - gap * 3) \ 4
        Dim kpis As ForestKpi() = New ForestKpi() {dashKpiSales, dashKpiWeek, dashKpiMonth, dashKpiTx}
        For i As Integer = 0 To 3
            kpis(i).SetBounds(pad + i * (kw + gap), 18, kw, 90)
        Next

        ' sales performance
        Dim salesTop As Integer = 18 + 90 + gap
        Dim salesH As Integer = 238
        dashSalesCard.SetBounds(pad, salesTop, w, salesH)
        dashSeg.Location = New Point(w - dashSeg.Width - 18, 16)
        dashChart.SetBounds(18, 104, w - 36, salesH - 104 - 14)
        dashSalesDelta.Location = New Point(dashSalesValue.Right + 10, dashSalesValue.Top + 10)

        ' bottom row
        Dim rowTop As Integer = salesTop + salesH + gap
        Dim rowH As Integer = Math.Max(250, dashBody.ClientSize.Height - rowTop - 18)
        Dim recentW As Integer = CInt(w * 0.64)
        Dim alertW As Integer = w - recentW - gap

        dashRecentCard.SetBounds(pad, rowTop, recentW, rowH)
        dashRecentLink.Location = New Point(recentW - dashRecentLink.Width - 18, 20)
        dashRecentTable.SetBounds(14, 62, recentW - 28, rowH - 62 - 12)

        dashAlertCard.SetBounds(pad + recentW + gap, rowTop, alertW, rowH)
        dashAlertList.SetBounds(14, 56, alertW - 28, rowH - 56 - 58)
        dashAlertBtn.Location = New Point(18, rowH - 50)
        PlaceAlertPill()
    End Sub

    Private Sub PlaceAlertPill()
        If dashAlertPill Is Nothing Then Return
        dashAlertPill.Location = New Point(dashAlertCard.Width - dashAlertPill.Width - 18, 16)
    End Sub

    '=================================================================
    ' DATA
    '=================================================================
    Private Function DashUserName() As String
        Dim n As String = CurrentSession.FullName
        If String.IsNullOrWhiteSpace(n) Then n = "Admin"
        Return n
    End Function

    Private Function DashSales(period As String, anchor As DateTime) As Decimal
        Return DataStore.GetSalesSummary(DataStore.QueryTransactions(period, anchor, "All Statuses", "")).TotalSales
    End Function

    Private Shared Function DashDelta(current As Decimal, previous As Decimal, ByRef positive As Boolean) As String
        If previous <= 0D Then
            positive = True
            Return If(current > 0D, "New", "")
        End If
        Dim pct As Decimal = (current - previous) / previous * 100D
        positive = (pct >= 0D)
        Return If(pct >= 0D, "+", "") & pct.ToString("0.0", CultureInfo.InvariantCulture) & "%"
    End Function

    Public Sub RefreshDashboardUi()
        If Not dashBuilt Then Return

        Dim pos As Boolean = True
        Dim todaySales As Decimal = DashSales("Daily", DateTime.Today)
        Dim yesterday As Decimal = DashSales("Daily", DateTime.Today.AddDays(-1))
        dashKpiSales.SetValue(Peso(todaySales), DashDelta(todaySales, yesterday, pos), pos)

        Dim weekSales As Decimal = DashSales("Weekly", DateTime.Today)
        Dim prevWeek As Decimal = DashSales("Weekly", DateTime.Today.AddDays(-7))
        dashKpiWeek.SetValue(Peso(weekSales), DashDelta(weekSales, prevWeek, pos), pos)

        Dim monthSales As Decimal = DashSales("Monthly", DateTime.Today)
        Dim prevMonth As Decimal = DashSales("Monthly", DateTime.Today.AddMonths(-1))
        dashKpiMonth.SetValue(Peso(monthSales), DashDelta(monthSales, prevMonth, pos), pos)

        Dim completed As Integer = 0
        For Each t As POS_Transaction In DataStore.Transactions
            If t.Status = TransactionStatus.Completed Then completed += 1
        Next
        Dim todayCount As Integer = DataStore.GetTodayOrderCount()
        dashKpiTx.SetValue(completed.ToString("N0"), If(todayCount > 0, "+" & todayCount.ToString() & " today", ""), True)

        Dim hr As Integer = DateTime.Now.Hour
        Dim greet As String = If(hr < 12, "Good morning", If(hr < 18, "Good afternoon", "Good evening"))
        Dim first As String = DashUserName().Split(New Char() {" "c})(0)
        dashHeader.SubtitleLabel.Text = greet & ", " & first & " " & ChrW(&HB7) & " " &
            DateTime.Today.ToString("dddd, MMMM d", CultureInfo.InvariantCulture) & " " & ChrW(&HB7) & " " & ForestUi.StoreName

        UpdateSalesChart()
        RefreshDashRecent()
        RefreshDashAlerts()
    End Sub

    Private Sub UpdateSalesChart()
        If dashChart Is Nothing Then Return
        Dim pos As Boolean = True
        Dim vals As Decimal()
        Dim labs As String()
        Dim hi As Integer = -1
        Dim total As Decimal = 0D
        Dim previous As Decimal = 0D
        Dim versus As String = "yesterday"

        Select Case dashSeg.SelectedIndex
            Case 0   ' daily: one bar per hour
                Dim list As List(Of POS_Transaction) = DataStore.GetDailyTransactions(DateTime.Today)
                Dim firstH As Integer = 8
                Dim lastH As Integer = 19
                For Each t As POS_Transaction In list
                    If t.Status <> TransactionStatus.Completed Then Continue For
                    firstH = Math.Min(firstH, t.TransactionDate.Hour)
                    lastH = Math.Max(lastH, t.TransactionDate.Hour)
                Next
                Dim n As Integer = lastH - firstH + 1
                vals = New Decimal(n - 1) {}
                labs = New String(n - 1) {}
                For i As Integer = 0 To n - 1
                    labs(i) = (firstH + i).ToString() & ":00"
                Next
                For Each t As POS_Transaction In list
                    If t.Status <> TransactionStatus.Completed Then Continue For
                    vals(t.TransactionDate.Hour - firstH) += t.Total
                    total += t.Total
                Next
                hi = Math.Min(n - 1, Math.Max(0, DateTime.Now.Hour - firstH))
                previous = DashSales("Daily", DateTime.Today.AddDays(-1))
                versus = "yesterday"

            Case 1   ' weekly: one bar per day
                Dim start As DateTime = DataStore.GetWeekStart(DateTime.Today)
                vals = New Decimal(6) {}
                labs = New String(6) {}
                For i As Integer = 0 To 6
                    Dim d As DateTime = start.AddDays(i)
                    labs(i) = d.ToString("ddd", CultureInfo.InvariantCulture)
                    vals(i) = DataStore.GetSalesSummary(DataStore.GetDailyTransactions(d)).TotalSales
                    total += vals(i)
                    If d.Date = DateTime.Today Then hi = i
                Next
                previous = DashSales("Weekly", DateTime.Today.AddDays(-7))
                versus = "last week"

            Case Else   ' monthly: one bar per month of this year
                vals = New Decimal(11) {}
                labs = New String(11) {}
                For m As Integer = 1 To 12
                    labs(m - 1) = New DateTime(DateTime.Today.Year, m, 1).ToString("MMM", CultureInfo.InvariantCulture)
                    vals(m - 1) = DataStore.GetSalesSummary(DataStore.GetMonthlyTransactions(DateTime.Today.Year, m)).TotalSales
                Next
                total = vals(DateTime.Today.Month - 1)
                hi = DateTime.Today.Month - 1
                previous = DashSales("Monthly", DateTime.Today.AddMonths(-1))
                versus = "last month"
        End Select

        dashChart.SetData(vals, labs, hi)
        dashSalesValue.Text = Peso(total)
        Dim d1 As String = DashDelta(total, previous, pos)
        dashSalesDelta.Text = If(d1 = "", "", d1 & " vs " & versus)
        dashSalesDelta.ForeColor = If(pos, ForestUi.OkFore, ForestUi.Danger)
        dashSalesDelta.Location = New Point(dashSalesValue.Right + 10, dashSalesValue.Top + 10)
    End Sub

    Private Function DashCell(text As String, bold As Boolean, fore As Color) As Label
        Dim l As New Label()
        l.AutoSize = False
        l.AutoEllipsis = True
        l.Size = New Size(100, 18)
        l.BackColor = Color.Transparent
        l.ForeColor = fore
        l.Font = New Font("Segoe UI", 8.5F, If(bold, FontStyle.Bold, FontStyle.Regular))
        l.TextAlign = ContentAlignment.MiddleLeft
        l.UseMnemonic = False
        l.Text = text
        Return l
    End Function

    Private Function DashStatusPill(status As String) As Guna2Panel
        If status = TransactionStatus.Completed Then Return ForestUi.Pill("Completed", ForestUi.OkBack, ForestUi.OkFore)
        If status = TransactionStatus.Refunded Then Return ForestUi.Pill("Refunded", ForestUi.BadBack, ForestUi.BadFore)
        Return ForestUi.Pill(status, ForestUi.AvatarGray, ForestUi.Muted)
    End Function

    Private Sub RefreshDashRecent()
        Dim recent As New List(Of POS_Transaction)(DataStore.Transactions)
        recent.Sort(Function(a, b) b.TransactionDate.CompareTo(a.TransactionDate))

        dashRecentTable.ClearRows()
        If recent.Count = 0 Then
            dashRecentTable.ShowEmptyMessage("No transactions yet.")
            Return
        End If
        Dim shown As Integer = 0
        For Each t As POS_Transaction In recent
            Dim qty As Integer = 0
            For Each it As TransactionItem In t.Items
                qty += it.Quantity
            Next
            dashRecentTable.AddRow(New Control() {
                DashCell(t.TransactionID, True, ForestUi.Accent),
                DashCell(t.TransactionDate.ToString("hh:mm tt", CultureInfo.InvariantCulture), False, ForestUi.Ink),
                DashCell(t.Cashier, False, ForestUi.Ink),
                DashCell(qty.ToString() & If(qty = 1, " item", " items"), False, ForestUi.Ink),
                DashCell(Peso(t.Total), True, ForestUi.Ink),
                DashStatusPill(t.Status)})
            shown += 1
            If shown >= 6 Then Exit For
        Next
    End Sub

    Private Sub RefreshDashAlerts()
        Dim low As List(Of Product) = DataStore.GetLowStockProducts()
        low.Sort(Function(a, b) a.Stock.CompareTo(b.Stock))

        If dashAlertPill IsNot Nothing Then
            dashAlertCard.Controls.Remove(dashAlertPill)
            dashAlertPill.Dispose()
        End If
        dashAlertPill = ForestUi.Pill(low.Count.ToString() & If(low.Count = 1, " item", " items"), ForestUi.AccentSoft, ForestUi.Muted)
        dashAlertCard.Controls.Add(dashAlertPill)
        PlaceAlertPill()

        For i As Integer = dashAlertList.Controls.Count - 1 To 0 Step -1
            Dim c As Control = dashAlertList.Controls(i)
            dashAlertList.Controls.RemoveAt(i)
            c.Dispose()
        Next

        If low.Count = 0 Then
            dashAlertList.Controls.Add(ForestUi.Lbl("All items are well stocked.", 9.0F, FontStyle.Regular, ForestUi.Muted, 4, 12))
            Return
        End If

        Dim y As Integer = 0
        For i As Integer = 0 To Math.Min(low.Count, 4) - 1
            Dim p As Product = low(i)
            Dim row As Guna2Panel = ForestUi.Card(Math.Max(120, dashAlertList.Width), 52, 10, ForestUi.Page)
            row.Location = New Point(0, y)
            row.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
            row.Controls.Add(ForestUi.Lbl(p.Name, 9.0F, FontStyle.Regular, ForestUi.Ink, 12, 9))
            row.Controls.Add(ForestUi.Lbl(p.Stock.ToString() & " left", 7.75F, FontStyle.Regular, ForestUi.Muted, 13, 29))
            Dim pill As Guna2Panel = If(p.Stock <= 0,
                ForestUi.Pill("Critical", ForestUi.BadBack, ForestUi.BadFore),
                ForestUi.Pill("Low", ForestUi.AccentSoft, ForestUi.Accent))
            pill.Location = New Point(row.Width - pill.Width - 12, 15)
            pill.Anchor = AnchorStyles.Top Or AnchorStyles.Right
            row.Controls.Add(pill)
            dashAlertList.Controls.Add(row)
            y += 60
        Next
    End Sub

    '=================================================================
    ' EXPORT
    '=================================================================
    Private Function DashCsv(value As String) As String
        Return """" & If(value, "").Replace("""", """""") & """"
    End Function

    Private Sub DashExport_Click(sender As Object, e As EventArgs)
        Using dlg As New SaveFileDialog()
            dlg.Filter = "CSV file (*.csv)|*.csv"
            dlg.FileName = "sales-report-" & DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) & ".csv"
            If dlg.ShowDialog(Me) <> DialogResult.OK Then Return

            Dim all As New List(Of POS_Transaction)(DataStore.Transactions)
            all.Sort(Function(a, b) b.TransactionDate.CompareTo(a.TransactionDate))
            Dim sb As New StringBuilder()
            sb.AppendLine("Transaction ID,Date,Cashier,Items,Total,Status")
            For Each t As POS_Transaction In all
                sb.AppendLine(String.Join(",", New String() {
                    DashCsv(t.TransactionID),
                    DashCsv(t.TransactionDate.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)),
                    DashCsv(t.Cashier),
                    DashCsv(DataStore.BuildItemsText(t)),
                    t.Total.ToString("0.00", CultureInfo.InvariantCulture),
                    DashCsv(t.Status)}))
            Next
            Try
                File.WriteAllText(dlg.FileName, sb.ToString(), New UTF8Encoding(True))
                MessageBox.Show("Report saved.", "Export report", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("The report could not be saved: " & ex.Message, "Export report", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End Using
    End Sub

End Class