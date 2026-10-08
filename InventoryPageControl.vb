Option Strict On
Option Explicit On

Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms

' ============================================================
'  InventoryPageControl.vb
'  The "Inventory" page.
'    [Products]    KPI cards, search + filter, stock table with
'                  - / + / check to update the quantity.
'    [Ingredients] your existing IngredientInventoryView (unchanged).
'  Data comes from DataStore (Products, GetProductStockStatus,
'  RestockProduct, AdjustStock) and refreshes on ProductsChanged.
' ============================================================
Public Class InventoryPageControl
    Inherits UserControl

    Private Const PageSize As Integer = 8
    Private Const RowHeight As Integer = 52

    ' column widths in % : product, category, sku, quantity, status, stock update
    Private ReadOnly _colWidths As Single() = {27.0F, 15.0F, 14.0F, 11.0F, 14.0F, 19.0F}

    ' header
    Private pnlHeader As BorderedPanel
    Private segMode As SegmentedControl
    Private lblTitle As Label
    Private lblSub As Label
    Private btnExport As RoundedButton
    Private btnBell As RoundedButton
    Private divider As Panel
    Private avatar As AvatarCircle
    Private lblUserName As Label
    Private lblUserRole As Label

    ' body
    Private pnlProducts As Panel
    Private pnlIngredients As Panel
    Private ingredientView As IngredientInventoryView

    ' products view
    Private cardTotal As StatCard
    Private cardLow As StatCard
    Private cardOut As StatCard
    Private cardValue As StatCard
    Private pnlToolbar As Panel
    Private pnlSearch As CardPanel
    Private txtSearch As TextBox
    Private lblSearchHint As Label
    Private lblMin As Label
    Private nudMin As NumericUpDown
    Private segFilter As SegmentedControl
    Private pnlRows As Panel
    Private lblShowing As Label
    Private lblPage As Label
    Private btnPrev As RoundedButton
    Private btnNext As RoundedButton

    Private _page As Integer = 0
    Private _totalPages As Integer = 1
    Private _syncingMin As Boolean = False

    Public Sub New()
        DoubleBuffered = True
        BackColor = Theme.PageBg
        Font = Theme.UiFont(9.0F)
        BuildUi()
        RefreshUser()

        If Not DesignMode AndAlso System.ComponentModel.LicenseManager.UsageMode <> System.ComponentModel.LicenseUsageMode.Designtime Then
            AddHandler DataStore.ProductsChanged, AddressOf OnProductsChanged
            RefreshList()
        End If
    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            RemoveHandler DataStore.ProductsChanged, AddressOf OnProductsChanged
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private Sub OnProductsChanged(sender As Object, e As EventArgs)
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

    ''' <summary>Rebuilds KPI cards + table from DataStore.Products.</summary>
    Public Sub RefreshList()
        Dim all As List(Of Product) = DataStore.Products.ToList()

        ' ---- KPI cards ----
        Dim lowCount As Integer = 0
        Dim outCount As Integer = 0
        Dim value As Decimal = 0D
        For Each p As Product In all
            Dim st As String = DataStore.GetProductStockStatus(p)
            If st = StockStatus.LowStock Then lowCount += 1
            If st = StockStatus.OutOfStock Then outCount += 1
            value += p.Price * p.Stock
        Next
        cardTotal.SetValue(all.Count.ToString("N0"), Theme.TextDark)
        cardLow.SetValue(lowCount.ToString("N0"), Theme.Orange)
        cardOut.SetValue(outCount.ToString("N0"), Theme.Red)
        cardValue.SetValue(Peso(value), Theme.TextDark)

        ' ---- default minimum stock box ----
        _syncingMin = True
        nudMin.Value = Math.Max(nudMin.Minimum, Math.Min(nudMin.Maximum, DataStore.PosSettings.DefaultMinStock))
        _syncingMin = False

        ' ---- table ----
        Dim items As List(Of Product) = GetFiltered(all)

        _totalPages = Math.Max(1, CInt(Math.Ceiling(items.Count / CDbl(PageSize))))
        If _page > _totalPages - 1 Then _page = _totalPages - 1
        If _page < 0 Then _page = 0

        Dim pageItems As List(Of Product) = items.Skip(_page * PageSize).Take(PageSize).ToList()

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
            empty.Text = If(all.Count = 0, "No products yet.", "No products match your search / filter.")
            empty.ForeColor = Theme.TextMuted
            empty.TextAlign = ContentAlignment.MiddleCenter
            empty.SetBounds(0, 40, pnlRows.ClientSize.Width, 40)
            pnlRows.Controls.Add(empty)
        End If
        pnlRows.ResumeLayout()

        ' ---- footer ----
        Dim firstNo As Integer = If(items.Count = 0, 0, _page * PageSize + 1)
        Dim lastNo As Integer = _page * PageSize + pageItems.Count
        lblShowing.Text = "Showing " & firstNo.ToString() & ChrW(&H2013).ToString() & lastNo.ToString() &
                          " of " & items.Count.ToString() & " products"
        lblPage.Text = "Page " & (_page + 1).ToString() & " of " & _totalPages.ToString()
        btnPrev.TextColor = If(_page > 0, Theme.TextDark, Theme.TextMuted)
        btnNext.TextColor = If(_page < _totalPages - 1, Theme.TextDark, Theme.TextMuted)
        btnPrev.Invalidate()
        btnNext.Invalidate()
    End Sub

    ' ------------------------------------------------------------
    '  Build the UI
    ' ------------------------------------------------------------
    Private Sub BuildUi()
        SuspendLayout()

        Dim root As New TableLayoutPanel()
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

        Dim host As New Panel()
        host.Dock = DockStyle.Fill
        host.Margin = New Padding(0)
        host.Padding = New Padding(24, 20, 24, 24)
        host.BackColor = Theme.PageBg

        pnlProducts = BuildProductsView()
        pnlProducts.Dock = DockStyle.Fill

        pnlIngredients = New Panel()
        pnlIngredients.Dock = DockStyle.Fill
        pnlIngredients.BackColor = Theme.PageBg
        pnlIngredients.Visible = False

        host.Controls.Add(pnlProducts)
        host.Controls.Add(pnlIngredients)
        root.Controls.Add(host, 0, 1)

        Controls.Add(root)
        ResumeLayout()
    End Sub

    ' ---------------- header ----------------
    Private Sub BuildHeader()
        pnlHeader = New BorderedPanel()

        lblTitle = New Label()
        lblTitle.Text = "Inventory"
        lblTitle.Font = Theme.UiFont(15.0F)
        lblTitle.ForeColor = Theme.TextDark
        lblTitle.AutoSize = True
        lblTitle.Location = New Point(24, 12)
        pnlHeader.Controls.Add(lblTitle)

        lblSub = New Label()
        lblSub.Text = "Monitor stock levels and update quantities across your catalog"
        lblSub.Font = Theme.UiFont(7.5F)
        lblSub.ForeColor = Theme.TextMuted
        lblSub.AutoSize = True
        lblSub.Location = New Point(25, 44)
        pnlHeader.Controls.Add(lblSub)

        segMode = New SegmentedControl()
        segMode.Top = 20
        segMode.SetItems(New String() {"Products", "Ingredients"}, 0)
        AddHandler segMode.SelectedIndexChanged, Sub(s, e) SetMode(segMode.SelectedIndex = 1)
        pnlHeader.Controls.Add(segMode)

        btnExport = New RoundedButton()
        btnExport.Text = "Export stock"
        btnExport.Glyph = Theme.GlyphDownload
        btnExport.GlyphFont = Theme.IconFont(9.0F)
        btnExport.GlyphColor = Theme.TextDark
        btnExport.TextColor = Theme.TextDark
        btnExport.FillColor = Color.White
        btnExport.HoverColor = Theme.HoverGray
        btnExport.BorderColor = Theme.Border
        btnExport.Font = Theme.UiFont(8.5F, FontStyle.Bold)
        btnExport.Size = New Size(116, 36)
        btnExport.Top = 19
        AddHandler btnExport.Click, Sub(s, e) ExportStock()
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
        AddHandler btnBell.Click, Sub(s, e) ShowLowStock()
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
        segMode.Left = Math.Max(lblTitle.Right, lblSub.Right) + 32

        Dim x As Integer = pnlHeader.Width - 24
        x -= lblUserName.Width : lblUserName.Left = x : lblUserRole.Left = x
        x -= 10 + avatar.Width : avatar.Left = x
        x -= 16 + divider.Width : divider.Left = x
        x -= 16 + btnBell.Width : btnBell.Left = x
        x -= 12 + btnExport.Width : btnExport.Left = x
    End Sub

    Private Sub SetMode(ingredients As Boolean)
        If ingredients Then
            If ingredientView Is Nothing Then
                ingredientView = New IngredientInventoryView(Theme.PageBg)
                ingredientView.Dock = DockStyle.Fill
                pnlIngredients.Controls.Add(ingredientView)
            End If
            ingredientView.RefreshData()
        Else
            RefreshList()
        End If
        pnlProducts.Visible = Not ingredients
        pnlIngredients.Visible = ingredients
        btnExport.Visible = Not ingredients
        LayoutHeader()
    End Sub

    Private Sub ShowLowStock()
        Dim low As List(Of Product) = DataStore.GetLowStockProducts()
        If low.Count = 0 Then
            MessageBox.Show(FindForm(), "All products are well stocked.", "Stock alerts", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Dim sb As New StringBuilder()
        For Each p As Product In low.Take(15)
            sb.AppendLine(p.Name & "  (" & p.Stock.ToString() & " left, min " & DataStore.GetMinStock(p).ToString() & ")")
        Next
        If low.Count > 15 Then sb.AppendLine("... and " & (low.Count - 15).ToString() & " more")
        MessageBox.Show(FindForm(), sb.ToString(), "Low / out of stock", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    ' ---------------- products view ----------------
    Private Function BuildProductsView() As Panel
        Dim view As New Panel()
        view.BackColor = Theme.PageBg

        Dim t As New TableLayoutPanel()
        t.Dock = DockStyle.Fill
        t.ColumnCount = 1
        t.RowCount = 2
        t.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        t.RowStyles.Add(New RowStyle(SizeType.Absolute, 92.0F))
        t.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        t.BackColor = Theme.PageBg
        t.Margin = New Padding(0)

        ' -- KPI cards --
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

        cardTotal = MakeCard("Total SKUs", Theme.GlyphShop, False, Theme.TealSoft, Theme.Teal)
        cardLow = MakeCard("Low stock", Theme.GlyphWarning, False, Theme.OrangeSoft, Theme.Orange)
        cardOut = MakeCard("Out of stock", Theme.GlyphCancel, False, Theme.RedSoft, Theme.Red)
        cardValue = MakeCard("Inventory value", ChrW(&H20B1).ToString(), True, Theme.GreenSoft, Theme.Green)
        kpi.Controls.Add(cardTotal, 0, 0)
        kpi.Controls.Add(cardLow, 1, 0)
        kpi.Controls.Add(cardOut, 2, 0)
        kpi.Controls.Add(cardValue, 3, 0)
        cardValue.Margin = New Padding(0)
        t.Controls.Add(kpi, 0, 0)

        ' -- table card --
        t.Controls.Add(BuildTableCard(), 0, 1)

        view.Controls.Add(t)
        Return view
    End Function

    Private Function MakeCard(caption As String, icon As String, iconIsText As Boolean, fill As Color, fore As Color) As StatCard
        Dim c As New StatCard()
        c.Setup(caption, icon, iconIsText, fill, fore)
        c.Dock = DockStyle.Top
        c.Height = 76
        c.Margin = New Padding(0, 0, 14, 0)
        Return c
    End Function

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

        ' -- toolbar: search | default min | segmented filter --
        pnlToolbar = New Panel()
        pnlToolbar.Dock = DockStyle.Fill
        pnlToolbar.Margin = New Padding(0)
        pnlToolbar.BackColor = Color.Transparent

        pnlSearch = New CardPanel()
        pnlSearch.Radius = 8
        pnlSearch.Size = New Size(330, 36)
        pnlSearch.Location = New Point(16, 15)
        pnlToolbar.Controls.Add(pnlSearch)

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
        lblSearchHint.Text = "Search product, SKU, or category"
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

        segFilter = New SegmentedControl()
        segFilter.Top = 16
        segFilter.SetItems(New String() {"All", "Low Stock", "Out of Stock"}, 0)
        AddHandler segFilter.SelectedIndexChanged, Sub(s, e)
                                                       _page = 0
                                                       RefreshList()
                                                   End Sub
        pnlToolbar.Controls.Add(segFilter)

        nudMin = New NumericUpDown()
        nudMin.Minimum = 1D
        nudMin.Maximum = 9999D
        nudMin.Width = 60
        nudMin.Top = 20
        nudMin.Value = 5D
        AddHandler nudMin.ValueChanged, Sub(s, e)
                                            If _syncingMin Then Return
                                            DataStore.SetDefaultMinStock(CInt(nudMin.Value))   ' raises ProductsChanged
                                        End Sub
        pnlToolbar.Controls.Add(nudMin)

        lblMin = New Label()
        lblMin.Text = "Default minimum stock"
        lblMin.Font = Theme.UiFont(8.0F)
        lblMin.ForeColor = Theme.TextMuted
        lblMin.AutoSize = True
        lblMin.Top = 24
        pnlToolbar.Controls.Add(lblMin)

        AddHandler pnlToolbar.Resize, Sub(s, e) LayoutToolbar()
        LayoutToolbar()
        t.Controls.Add(pnlToolbar, 0, 0)

        ' -- column header --
        Dim colHeader As New Panel()
        colHeader.Dock = DockStyle.Fill
        colHeader.Margin = New Padding(0)
        colHeader.BackColor = Theme.HeaderRowBg

        Dim hdr As TableLayoutPanel = NewColumnsTable()
        Dim titles As String() = {"Product", "Category", "SKU", "Quantity", "Status", "Stock update"}
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

        ' -- footer --
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

        Dim flow As New FlowLayoutPanel()
        flow.AutoSize = True
        flow.WrapContents = False
        flow.FlowDirection = FlowDirection.LeftToRight
        flow.Dock = DockStyle.Fill
        flow.Margin = New Padding(0, 0, 14, 0)
        flow.BackColor = Color.Transparent

        btnPrev = MakePagerButton("Previous")
        AddHandler btnPrev.Click, Sub(s, e)
                                      If _page > 0 Then
                                          _page -= 1
                                          RefreshList()
                                      End If
                                  End Sub
        lblPage = New Label()
        lblPage.AutoSize = True
        lblPage.Font = Theme.UiFont(7.5F)
        lblPage.ForeColor = Theme.TextDark
        lblPage.Margin = New Padding(10, 19, 10, 0)
        btnNext = MakePagerButton("Next")
        AddHandler btnNext.Click, Sub(s, e)
                                      If _page < _totalPages - 1 Then
                                          _page += 1
                                          RefreshList()
                                      End If
                                  End Sub
        flow.Controls.Add(btnPrev)
        flow.Controls.Add(lblPage)
        flow.Controls.Add(btnNext)
        footer.Controls.Add(flow, 1, 0)
        t.Controls.Add(footer, 0, 3)

        card.Controls.Add(t)
        Return card
    End Function

    Private Function MakePagerButton(caption As String) As RoundedButton
        Dim b As New RoundedButton()
        b.Text = caption
        b.Font = Theme.UiFont(8.0F, FontStyle.Bold)
        b.FillColor = Color.White
        b.HoverColor = Theme.HoverGray
        b.BorderColor = Theme.Border
        b.TextColor = Theme.TextDark
        b.Size = New Size(78, 30)
        b.Margin = New Padding(0, 12, 0, 0)
        Return b
    End Function

    Private Sub LayoutToolbar()
        If segFilter Is Nothing Then Return
        segFilter.Left = pnlToolbar.Width - 16 - segFilter.Width
        nudMin.Left = segFilter.Left - 16 - nudMin.Width
        lblMin.Left = nudMin.Left - 8 - lblMin.Width
    End Sub

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

    Private Function SmallButton(glyph As String, fill As Color, glyphColor As Color, border As Color) As RoundedButton
        Dim b As New RoundedButton()
        b.Size = New Size(28, 28)
        b.Radius = 7
        b.FillColor = fill
        b.HoverColor = Color.FromArgb(225, 212, 205)
        b.BorderColor = border
        b.Glyph = glyph
        b.GlyphColor = glyphColor
        b.GlyphFont = Theme.IconFont(8.5F)
        Return b
    End Function

    Private Function BuildRow(p As Product) As Panel
        Dim innerH As Integer = RowHeight - 1

        Dim row As New BorderedPanel()
        row.Height = RowHeight
        row.Padding = New Padding(0, 0, 0, 1)

        Dim t As TableLayoutPanel = NewColumnsTable()
        row.Controls.Add(t)

        Dim st As String = DataStore.GetProductStockStatus(p)

        ' 0 - thumbnail + name
        Dim cellName As Panel = NewCell()
        Dim lblName As Label = CellLabel(If(p.Name, ""), Theme.UiFont(8.5F), Theme.TextDark)
        lblName.Padding = New Padding(46, 0, 0, 0)
        cellName.Controls.Add(lblName)
        Dim thumb As New ThumbBox()
        thumb.Size = New Size(34, 34)
        thumb.Location = New Point(0, (innerH - 34) \ 2)
        thumb.Picture = p.Image
        cellName.Controls.Add(thumb)
        thumb.BringToFront()
        t.Controls.Add(cellName, 0, 0)

        ' 1 - category, 2 - SKU
        t.Controls.Add(CellLabel(If(p.Category, ""), Theme.UiFont(8.0F), Theme.TextMuted), 1, 0)
        t.Controls.Add(CellLabel(If(p.Id, ""), Theme.UiFont(8.0F), Theme.TextMuted), 2, 0)

        ' 3 - quantity (orange = low, red = out)
        Dim qtyColor As Color = Theme.TextDark
        If st = StockStatus.OutOfStock Then
            qtyColor = Theme.Red
        ElseIf st = StockStatus.LowStock Then
            qtyColor = Theme.Orange
        End If
        t.Controls.Add(CellLabel(p.Stock.ToString(), Theme.UiFont(9.0F, FontStyle.Bold), qtyColor), 3, 0)

        ' 4 - status badge
        Dim cellBadge As Panel = NewCell()
        Dim pill As New Badge()
        If st = StockStatus.OutOfStock Then
            pill.Setup(st, Theme.RedSoft, Theme.Red)
        ElseIf st = StockStatus.LowStock Then
            pill.Setup(st, Theme.OrangeSoft, Theme.Orange)
        Else
            pill.Setup(st, Theme.GreenSoft, Theme.Green)
        End If
        pill.Location = New Point(0, (innerH - pill.Height) \ 2)
        cellBadge.Controls.Add(pill)
        t.Controls.Add(cellBadge, 4, 0)

        ' 5 - stock update:  [-] [ qty ] [+] [check]
        Dim cellUpd As Panel = NewCell()

        Dim box As New TextBox()
        box.Text = p.Stock.ToString()
        box.TextAlign = HorizontalAlignment.Center
        box.Font = Theme.UiFont(9.0F, FontStyle.Bold)
        box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        box.MaxLength = 7
        box.Width = 50
        box.Location = New Point(34, (innerH - box.Height) \ 2)

        Dim btnMinus As RoundedButton = SmallButton(Theme.GlyphMinus, Color.White, Theme.TextDark, Theme.Border)
        btnMinus.Location = New Point(0, (innerH - 28) \ 2)
        Dim btnPlus As RoundedButton = SmallButton(Theme.GlyphAdd, Theme.TealSoft, Theme.Teal, Color.Empty)
        btnPlus.Location = New Point(90, (innerH - 28) \ 2)
        Dim btnOk As RoundedButton = SmallButton(Theme.GlyphCheck, Theme.HoverGray, Theme.TextDark, Color.Empty)
        btnOk.Location = New Point(124, (innerH - 28) \ 2)

        AddHandler box.KeyPress, Sub(s, e)
                                     If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then e.Handled = True
                                 End Sub
        AddHandler box.KeyDown, Sub(s, e)
                                    If e.KeyCode = Keys.Enter Then
                                        e.SuppressKeyPress = True
                                        ApplyStock(p, box)
                                    End If
                                End Sub
        AddHandler btnMinus.Click, Sub(s, e)
                                       Dim n As Integer = BoxValue(box)
                                       If n > 0 Then box.Text = (n - 1).ToString()
                                   End Sub
        AddHandler btnPlus.Click, Sub(s, e)
                                      Dim n As Integer = BoxValue(box)
                                      If n < 9999999 Then box.Text = (n + 1).ToString()
                                  End Sub
        AddHandler btnOk.Click, Sub(s, e) ApplyStock(p, box)

        cellUpd.Controls.Add(btnMinus)
        cellUpd.Controls.Add(box)
        cellUpd.Controls.Add(btnPlus)
        cellUpd.Controls.Add(btnOk)
        t.Controls.Add(cellUpd, 5, 0)

        Return row
    End Function

    Private Function BoxValue(box As TextBox) As Integer
        Dim n As Integer
        If Not Integer.TryParse(box.Text.Trim(), n) OrElse n < 0 Then n = 0
        Return n
    End Function

    ' ------------------------------------------------------------
    '  Update stock  (higher = restock, lower = logged adjustment)
    ' ------------------------------------------------------------
    Private Sub ApplyStock(p As Product, box As TextBox)
        Dim newQty As Integer = BoxValue(box)
        Dim current As Integer = p.Stock
        If newQty = current Then Return

        If newQty > current Then
            If DataStore.RestockProduct(p, newQty - current) Then Return
            box.Text = current.ToString()
            Return
        End If

        ' lowering stock must have a reason (it is saved in the stock adjustment log)
        Dim reason As String = Microsoft.VisualBasic.Interaction.InputBox(
            "Why is the stock of " & p.Name & " lowered from " & current.ToString() & " to " & newQty.ToString() & "?",
            "Lower stock", "Stock count")
        If String.IsNullOrWhiteSpace(reason) Then
            box.Text = current.ToString()
            Return
        End If

        Dim err As String = ""
        If Not DataStore.AdjustStock(p, current - newQty, "Correction", reason, CurrentSession.FullName, err) Then
            MessageBox.Show(FindForm(), err, "Lower stock", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            box.Text = current.ToString()
        End If
    End Sub

    ' ------------------------------------------------------------
    '  Filter / export
    ' ------------------------------------------------------------
    Private Function GetFiltered(source As List(Of Product)) As List(Of Product)
        Dim q As String = txtSearch.Text.Trim()
        Dim result As New List(Of Product)()

        For Each p As Product In source
            If q.Length > 0 Then
                Dim hay As String = If(p.Name, "") & " " & If(p.Category, "") & " " & If(p.Id, "")
                If hay.IndexOf(q, StringComparison.OrdinalIgnoreCase) < 0 Then Continue For
            End If

            Dim st As String = DataStore.GetProductStockStatus(p)
            If segFilter.SelectedIndex = 1 AndAlso st <> StockStatus.LowStock Then Continue For
            If segFilter.SelectedIndex = 2 AndAlso st <> StockStatus.OutOfStock Then Continue For

            result.Add(p)
        Next
        Return result
    End Function

    Private Shared Function Csv(value As String) As String
        Return """" & If(value, "").Replace("""", """""") & """"
    End Function

    Private Sub ExportStock()
        Using dlg As New SaveFileDialog()
            dlg.Title = "Export stock"
            dlg.Filter = "CSV file (Excel)|*.csv"
            dlg.FileName = "stock_" & DateTime.Now.ToString("yyyyMMdd") & ".csv"
            If dlg.ShowDialog(FindForm()) <> System.Windows.Forms.DialogResult.OK Then Return

            Dim lines As New List(Of String)()
            lines.Add("SKU,Product,Category,Quantity,Min Stock,Status,Price,Value")
            For Each p As Product In DataStore.Products
                lines.Add(String.Join(",",
                    Csv(p.Id), Csv(p.Name), Csv(p.Category),
                    p.Stock.ToString(), DataStore.GetMinStock(p).ToString(),
                    Csv(DataStore.GetProductStockStatus(p)),
                    p.Price.ToString("0.00", CultureInfo.InvariantCulture),
                    (p.Price * p.Stock).ToString("0.00", CultureInfo.InvariantCulture)))
            Next

            Try
                File.WriteAllLines(dlg.FileName, lines, New UTF8Encoding(True))
                MessageBox.Show(FindForm(), "Stock exported (" & DataStore.Products.Count.ToString() & " products).",
                                "Export stock", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show(FindForm(), "The file could not be saved:" & vbCrLf & ex.Message,
                                "Export stock", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End Using
    End Sub

End Class