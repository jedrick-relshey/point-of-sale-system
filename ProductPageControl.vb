Option Strict On
Option Explicit On

Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms

' ============================================================
'  ProductPageControl.vb
'  The "Product management" page. It reads from DataStore.Products
'  and saves with DataStore.AddProduct / UpdateProduct / DeleteProduct,
'  and refreshes itself on DataStore.ProductsChanged.
'  Built fully in code - no Designer needed.
' ============================================================
Public Class ProductPageControl
    Inherits UserControl

    Private Const PageSize As Integer = 8
    Private Const RowHeight As Integer = 49

    ' column widths in % : name, price, category, stock, status, actions
    Private ReadOnly _colWidths As Single() = {30.0F, 15.0F, 17.0F, 11.0F, 14.0F, 13.0F}

    ' header
    Private pnlHeader As BorderedPanel
    Private btnAdd As RoundedButton
    Private btnBell As RoundedButton
    Private divider As Panel
    Private avatar As AvatarCircle
    Private lblUserName As Label
    Private lblUserRole As Label

    ' toolbar
    Private pnlToolbar As Panel
    Private pnlSearch As CardPanel
    Private txtSearch As TextBox
    Private lblSearchHint As Label
    Private btnCategory As RoundedButton
    Private btnInventory As RoundedButton
    Private btnStatus As RoundedButton

    ' card
    Private pnlCardHeader As Panel
    Private lblCardSub As Label
    Private badgeStock As Badge
    Private pnlRows As Panel
    Private lblShowing As Label
    Private lblPrev As Label
    Private lblPage As Label
    Private lblNext As Label

    ' state
    Private _page As Integer = 0
    Private _totalPages As Integer = 1
    Private _category As String = ""
    Private _inventory As String = "All"
    Private _status As String = "All"

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
    ''' <summary>Shows the logged-in user (from CurrentSession) in the header.</summary>
    Public Sub RefreshUser()
        SetUser(SessionInfo.DisplayName(), SessionInfo.DisplayRole())
    End Sub

    Public Sub SetUser(fullName As String, role As String)
        lblUserName.Text = fullName
        lblUserRole.Text = role

        Dim parts As String() = fullName.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
        Dim ini As String = ""
        For i As Integer = 0 To Math.Min(1, parts.Length - 1)
            ini &= parts(i).Substring(0, 1).ToUpper()
        Next
        avatar.Initials = ini
    End Sub

    ''' <summary>Rebuilds the table from DataStore.Products.</summary>
    Public Sub RefreshList()
        Dim items As List(Of Product) = GetFiltered()

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
            empty.Text = If(DataStore.Products.Count = 0,
                            "No products yet. Click ""Add product"" to create your first one.",
                            "No products match your search / filters.")
            empty.ForeColor = Theme.TextMuted
            empty.TextAlign = ContentAlignment.MiddleCenter
            empty.SetBounds(0, 40, pnlRows.ClientSize.Width, 40)
            pnlRows.Controls.Add(empty)
        End If
        pnlRows.ResumeLayout()

        ' footer
        Dim firstNo As Integer = If(items.Count = 0, 0, _page * PageSize + 1)
        Dim lastNo As Integer = _page * PageSize + pageItems.Count
        lblShowing.Text = "Showing " & firstNo.ToString() & ChrW(&H2013).ToString() & lastNo.ToString() &
                          " of " & items.Count.ToString() & " products"
        lblPage.Text = "Page " & (_page + 1).ToString() & " of " & _totalPages.ToString()
        lblPrev.ForeColor = If(_page > 0, Theme.TextDark, Theme.TextMuted)
        lblNext.ForeColor = If(_page < _totalPages - 1, Theme.TextDark, Theme.TextMuted)

        ' card subtitle + stock badge
        Dim total As Integer = DataStore.Products.Count
        Dim available As Integer = DataStore.Products.Where(Function(x) String.Equals(x.Status, "Available", StringComparison.OrdinalIgnoreCase)).Count()
        lblCardSub.Text = total.ToString() & " products " & ChrW(&HB7).ToString() & " " & available.ToString() & " available"

        Dim low As Integer = DataStore.GetLowStockCount()
        If low = 0 Then
            badgeStock.Setup("All items in stock", Theme.GreenSoft, Theme.Green)
        Else
            badgeStock.Setup(low.ToString() & " low / out of stock", Theme.RedSoft, Theme.Red)
        End If
        badgeStock.Left = pnlCardHeader.Width - badgeStock.Width - 20
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

        Dim body As New Panel()
        body.Dock = DockStyle.Fill
        body.Margin = New Padding(0)
        body.Padding = New Padding(24, 20, 24, 24)
        body.BackColor = Theme.PageBg

        Dim bodyTable As New TableLayoutPanel()
        bodyTable.Dock = DockStyle.Fill
        bodyTable.ColumnCount = 1
        bodyTable.RowCount = 2
        bodyTable.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        bodyTable.RowStyles.Add(New RowStyle(SizeType.Absolute, 56.0F))
        bodyTable.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        bodyTable.BackColor = Theme.PageBg
        bodyTable.Margin = New Padding(0)

        BuildToolbar()
        pnlToolbar.Dock = DockStyle.Fill
        pnlToolbar.Margin = New Padding(0)
        bodyTable.Controls.Add(pnlToolbar, 0, 0)

        Dim card As CardPanel = BuildCard()
        card.Dock = DockStyle.Fill
        card.Margin = New Padding(0)
        bodyTable.Controls.Add(card, 0, 1)

        body.Controls.Add(bodyTable)
        root.Controls.Add(body, 0, 1)

        Controls.Add(root)
        ResumeLayout()
    End Sub

    ' ---------------- header ----------------
    Private Sub BuildHeader()
        pnlHeader = New BorderedPanel()

        Dim lblTitle As New Label()
        lblTitle.Text = "Product management"
        lblTitle.Font = Theme.UiFont(15.0F)
        lblTitle.ForeColor = Theme.TextDark
        lblTitle.AutoSize = True
        lblTitle.Location = New Point(24, 12)
        pnlHeader.Controls.Add(lblTitle)

        Dim lblSub As New Label()
        lblSub.Text = "Manage catalog details, pricing, categories, and availability"
        lblSub.Font = Theme.UiFont(7.5F)
        lblSub.ForeColor = Theme.TextMuted
        lblSub.AutoSize = True
        lblSub.Location = New Point(25, 44)
        pnlHeader.Controls.Add(lblSub)

        btnAdd = New RoundedButton()
        btnAdd.Text = "Add product"
        btnAdd.Glyph = Theme.GlyphAdd
        btnAdd.GlyphFont = Theme.IconFont(9.0F)
        btnAdd.Size = New Size(124, 36)
        btnAdd.Top = 19
        AddHandler btnAdd.Click, Sub(s, e) AddProduct()
        pnlHeader.Controls.Add(btnAdd)

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
        lblUserName.Size = New Size(150, 18)
        lblUserName.Top = 21
        pnlHeader.Controls.Add(lblUserName)

        lblUserRole = New Label()
        lblUserRole.Font = Theme.UiFont(7.5F)
        lblUserRole.ForeColor = Theme.TextMuted
        lblUserRole.AutoEllipsis = True
        lblUserRole.Size = New Size(150, 16)
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
        x -= 12 + btnAdd.Width : btnAdd.Left = x
    End Sub

    Private Sub ShowLowStock()
        Dim low As List(Of Product) = DataStore.GetLowStockProducts()
        If low.Count = 0 Then
            MessageBox.Show(FindForm(), "All products are well stocked.", "Stock alerts", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Dim sb As New System.Text.StringBuilder()
        For Each p As Product In low.Take(15)
            sb.AppendLine(p.Name & "  (" & p.Stock.ToString() & " left)")
        Next
        If low.Count > 15 Then sb.AppendLine("... and " & (low.Count - 15).ToString() & " more")
        MessageBox.Show(FindForm(), sb.ToString(), "Low / out of stock", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    ' ---------------- toolbar (search + filters) ----------------
    Private Sub BuildToolbar()
        pnlToolbar = New Panel()
        pnlToolbar.BackColor = Theme.PageBg

        pnlSearch = New CardPanel()
        pnlSearch.Radius = 8
        pnlSearch.Size = New Size(300, 36)
        pnlSearch.Location = New Point(0, 0)

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
        txtSearch.Width = 250
        pnlSearch.Controls.Add(txtSearch)

        lblSearchHint = New Label()
        lblSearchHint.Text = "Search product name or category"
        lblSearchHint.Font = Theme.UiFont(8.5F)
        lblSearchHint.ForeColor = Theme.TextMuted
        lblSearchHint.BackColor = Color.White
        lblSearchHint.AutoSize = False
        lblSearchHint.Size = New Size(250, 18)
        lblSearchHint.Location = New Point(36, 9)
        AddHandler lblSearchHint.Click, Sub(s, e) txtSearch.Focus()
        pnlSearch.Controls.Add(lblSearchHint)
        lblSearchHint.BringToFront()

        AddHandler txtSearch.TextChanged, Sub(s, e)
                                              lblSearchHint.Visible = (txtSearch.Text.Length = 0)
                                              _page = 0
                                              RefreshList()
                                          End Sub
        pnlToolbar.Controls.Add(pnlSearch)

        btnCategory = MakeFilterButton("All categories", Theme.GlyphChevron)
        AddHandler btnCategory.Click, Sub(s, e) ShowCategoryMenu()
        pnlToolbar.Controls.Add(btnCategory)

        btnInventory = MakeFilterButton("Inventory: All", Theme.GlyphFilter)
        AddHandler btnInventory.Click, Sub(s, e) ShowInventoryMenu()
        pnlToolbar.Controls.Add(btnInventory)

        btnStatus = MakeFilterButton("Status: All", Theme.GlyphFilter)
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
        b.Top = 1
        b.Height = 34
        FitButton(b)
        Return b
    End Function

    Private Sub FitButton(b As RoundedButton)
        Dim w As Integer = TextRenderer.MeasureText(b.Text, b.Font, New Size(1000, 100), TextFormatFlags.NoPadding).Width
        b.Width = Math.Max(120, w + 58)
    End Sub

    Private Sub LayoutToolbar()
        btnStatus.Left = pnlToolbar.Width - btnStatus.Width
        btnInventory.Left = btnStatus.Left - 10 - btnInventory.Width
        btnCategory.Left = btnInventory.Left - 10 - btnCategory.Width
    End Sub

    ' ---------------- card (table) ----------------
    Private Function BuildCard() As CardPanel
        Dim card As New CardPanel()
        card.Radius = 14
        card.Padding = New Padding(1)

        Dim t As New TableLayoutPanel()
        t.Dock = DockStyle.Fill
        t.ColumnCount = 1
        t.RowCount = 4
        t.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        t.RowStyles.Add(New RowStyle(SizeType.Absolute, 64.0F))
        t.RowStyles.Add(New RowStyle(SizeType.Absolute, 34.0F))
        t.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        t.RowStyles.Add(New RowStyle(SizeType.Absolute, 46.0F))
        t.BackColor = Color.Transparent
        t.Margin = New Padding(0)

        ' -- card header --
        pnlCardHeader = New Panel()
        pnlCardHeader.Dock = DockStyle.Fill
        pnlCardHeader.Margin = New Padding(0)
        pnlCardHeader.BackColor = Color.Transparent

        Dim lblTitle As New Label()
        lblTitle.Text = "Product catalog"
        lblTitle.Font = Theme.UiFont(10.5F)
        lblTitle.ForeColor = Theme.TextDark
        lblTitle.AutoSize = True
        lblTitle.Location = New Point(14, 12)
        pnlCardHeader.Controls.Add(lblTitle)

        lblCardSub = New Label()
        lblCardSub.Font = Theme.UiFont(7.5F)
        lblCardSub.ForeColor = Theme.TextMuted
        lblCardSub.AutoSize = True
        lblCardSub.Location = New Point(15, 36)
        pnlCardHeader.Controls.Add(lblCardSub)

        badgeStock = New Badge()
        badgeStock.Setup("All items in stock", Theme.GreenSoft, Theme.Green)
        badgeStock.Top = 18
        pnlCardHeader.Controls.Add(badgeStock)

        AddHandler pnlCardHeader.Resize, Sub(s, e) badgeStock.Left = pnlCardHeader.Width - badgeStock.Width - 20
        t.Controls.Add(pnlCardHeader, 0, 0)

        ' -- column header --
        Dim colHeader As New Panel()
        colHeader.Dock = DockStyle.Fill
        colHeader.Margin = New Padding(0)
        colHeader.BackColor = Theme.HeaderRowBg

        Dim hdr As TableLayoutPanel = NewColumnsTable()
        Dim titles As String() = {"Product name", "Price", "Category", "Stock", "Status", "Actions"}
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
        lblShowing.Padding = New Padding(14, 0, 0, 0)
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

        lblPrev = MakePagerLabel(ChrW(&H2190).ToString() & " Prev")
        AddHandler lblPrev.Click, Sub(s, e)
                                      If _page > 0 Then
                                          _page -= 1
                                          RefreshList()
                                      End If
                                  End Sub
        lblPage = MakePagerLabel("Page 1 of 1")
        lblPage.Cursor = Cursors.Default
        lblNext = MakePagerLabel("Next " & ChrW(&H2192).ToString())
        AddHandler lblNext.Click, Sub(s, e)
                                      If _page < _totalPages - 1 Then
                                          _page += 1
                                          RefreshList()
                                      End If
                                  End Sub
        flow.Controls.Add(lblPrev)
        flow.Controls.Add(lblPage)
        flow.Controls.Add(lblNext)
        footer.Controls.Add(flow, 1, 0)
        t.Controls.Add(footer, 0, 3)

        card.Controls.Add(t)
        Return card
    End Function

    Private Function MakePagerLabel(caption As String) As Label
        Dim l As New Label()
        l.Text = caption
        l.AutoSize = True
        l.Font = Theme.UiFont(7.5F)
        l.ForeColor = Theme.TextDark
        l.Margin = New Padding(10, 15, 0, 0)
        l.Cursor = Cursors.Hand
        Return l
    End Function

    ' ------------------------------------------------------------
    '  Table rows
    ' ------------------------------------------------------------
    Private Function NewColumnsTable() As TableLayoutPanel
        Dim t As New TableLayoutPanel()
        t.Dock = DockStyle.Fill
        t.Margin = New Padding(0)
        t.Padding = New Padding(14, 0, 8, 0)
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

    Private Function BuildRow(p As Product) As Panel
        Dim innerH As Integer = RowHeight - 1       ' 1px is the bottom border

        Dim row As New BorderedPanel()
        row.Height = RowHeight
        row.Padding = New Padding(0, 0, 0, 1)

        Dim t As TableLayoutPanel = NewColumnsTable()
        row.Controls.Add(t)

        ' 0 - thumbnail + name
        Dim cellName As Panel = NewCell()
        Dim lblName As Label = CellLabel(If(p.Name, ""), Theme.UiFont(8.5F), Theme.TextDark)
        lblName.Padding = New Padding(48, 0, 0, 0)
        cellName.Controls.Add(lblName)

        Dim thumb As New ThumbBox()
        thumb.Size = New Size(36, 36)
        thumb.Location = New Point(0, (innerH - 36) \ 2)
        thumb.Picture = p.Image
        ApplyThumbStyle(thumb, If(p.Category, ""))
        cellName.Controls.Add(thumb)
        thumb.BringToFront()
        t.Controls.Add(cellName, 0, 0)

        ' 1 - price
        t.Controls.Add(CellLabel(Peso(p.Price), Theme.UiFont(8.5F, FontStyle.Bold), Theme.TextDark), 1, 0)

        ' 2 - category
        t.Controls.Add(CellLabel(If(p.Category, ""), Theme.UiFont(8.0F), Theme.TextMuted), 2, 0)

        ' 3 - stock (red = out, orange = low)
        Dim invState As String = DataStore.GetProductStockStatus(p)
        Dim stockColor As Color = Theme.TextDark
        If p.Stock <= 0 Then
            stockColor = Theme.Red
        ElseIf invState = StockStatus.LowStock Then
            stockColor = Theme.Orange
        End If
        t.Controls.Add(CellLabel(p.Stock.ToString(), Theme.UiFont(8.5F), stockColor), 3, 0)

        ' 4 - status badge
        Dim cellBadge As Panel = NewCell()
        Dim pill As New Badge()
        If String.Equals(p.Status, "Available", StringComparison.OrdinalIgnoreCase) Then
            pill.Setup("Available", Theme.GreenSoft, Theme.Green)
        Else
            pill.Setup(If(String.IsNullOrWhiteSpace(p.Status), "Unavailable", p.Status), Theme.RedSoft, Theme.Red)
        End If
        pill.Location = New Point(0, (innerH - pill.Height) \ 2)
        cellBadge.Controls.Add(pill)
        t.Controls.Add(cellBadge, 4, 0)

        ' 5 - actions
        Dim cellActions As Panel = NewCell()

        Dim btnEdit As New RoundedButton()
        btnEdit.Size = New Size(32, 30)
        btnEdit.Location = New Point(0, (innerH - 30) \ 2)
        btnEdit.FillColor = Theme.TealSoft
        btnEdit.HoverColor = Color.FromArgb(225, 212, 205)
        btnEdit.Glyph = Theme.GlyphEdit
        btnEdit.GlyphColor = Theme.Teal
        btnEdit.GlyphFont = Theme.IconFont(9.0F)
        AddHandler btnEdit.Click, Sub(s, e) EditProduct(p)
        cellActions.Controls.Add(btnEdit)

        Dim btnDelete As New RoundedButton()
        btnDelete.Size = New Size(32, 30)
        btnDelete.Location = New Point(38, (innerH - 30) \ 2)
        btnDelete.FillColor = Theme.RedSoft
        btnDelete.HoverColor = Color.FromArgb(252, 205, 210)
        btnDelete.Glyph = Theme.GlyphDelete
        btnDelete.GlyphColor = Theme.Red
        btnDelete.GlyphFont = Theme.IconFont(9.0F)
        AddHandler btnDelete.Click, Sub(s, e) DeleteProduct(p)
        cellActions.Controls.Add(btnDelete)

        t.Controls.Add(cellActions, 5, 0)

        Return row
    End Function

    Private Sub ApplyThumbStyle(thumb As ThumbBox, category As String)
        Dim fills As Color() = {Color.FromArgb(254, 243, 199), Color.FromArgb(239, 230, 225),
                                Color.FromArgb(220, 242, 225), Color.FromArgb(252, 231, 243),
                                Color.FromArgb(224, 231, 255)}
        Dim fores As Color() = {Color.FromArgb(217, 119, 6), Color.FromArgb(62, 39, 35),
                                Color.FromArgb(46, 125, 50), Color.FromArgb(190, 24, 93),
                                Color.FromArgb(67, 56, 202)}
        Dim h As Integer = 0
        For Each ch As Char In category
            h = (h * 31 + AscW(ch)) Mod 997
        Next
        Dim idx As Integer = h Mod fills.Length
        thumb.FillColor = fills(idx)
        thumb.GlyphColor = fores(idx)
    End Sub

    ' ------------------------------------------------------------
    '  Search / filters
    ' ------------------------------------------------------------
    Private Function GetFiltered() As List(Of Product)
        Dim q As String = txtSearch.Text.Trim()
        Dim result As New List(Of Product)()

        For Each p As Product In DataStore.Products
            If q.Length > 0 Then
                Dim hay As String = If(p.Name, "") & " " & If(p.Category, "") & " " & If(p.Description, "")
                If hay.IndexOf(q, StringComparison.OrdinalIgnoreCase) < 0 Then Continue For
            End If
            If _category.Length > 0 AndAlso
               Not String.Equals(p.Category, _category, StringComparison.OrdinalIgnoreCase) Then Continue For
            If _inventory <> "All" AndAlso DataStore.GetProductStockStatus(p) <> _inventory Then Continue For
            If _status <> "All" AndAlso Not String.Equals(p.Status, _status, StringComparison.OrdinalIgnoreCase) Then Continue For

            result.Add(p)
        Next
        Return result
    End Function

    Private Sub ShowMenu(anchor As Control, options As List(Of String), current As String, onPick As Action(Of String))
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
        m.Show(anchor, New Point(0, anchor.Height + 2))
    End Sub

    Private Sub ShowCategoryMenu()
        Dim opts As New List(Of String)()
        opts.Add("All categories")
        Dim seen As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        For Each p As Product In DataStore.Products
            If Not String.IsNullOrWhiteSpace(p.Category) AndAlso seen.Add(p.Category.Trim()) Then opts.Add(p.Category.Trim())
        Next
        ShowMenu(btnCategory, opts, If(_category.Length = 0, "All categories", _category),
                 Sub(v As String) SetCategory(If(v = "All categories", "", v)))
    End Sub

    Private Sub ShowInventoryMenu()
        Dim opts As New List(Of String) From {"All", StockStatus.InStock, StockStatus.LowStock, StockStatus.OutOfStock}
        ShowMenu(btnInventory, opts, _inventory, Sub(v As String) SetInventory(v))
    End Sub

    Private Sub ShowStatusMenu()
        Dim opts As New List(Of String)()
        opts.Add("All")
        Dim seen As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        For Each p As Product In DataStore.Products
            If Not String.IsNullOrWhiteSpace(p.Status) AndAlso seen.Add(p.Status.Trim()) Then opts.Add(p.Status.Trim())
        Next
        ShowMenu(btnStatus, opts, _status, Sub(v As String) SetStatus(v))
    End Sub

    Private Sub SetCategory(name As String)
        _category = name
        btnCategory.Text = If(name.Length = 0, "All categories", name)
        AfterFilterChanged(btnCategory)
    End Sub

    Private Sub SetInventory(name As String)
        _inventory = name
        btnInventory.Text = "Inventory: " & name
        AfterFilterChanged(btnInventory)
    End Sub

    Private Sub SetStatus(name As String)
        _status = name
        btnStatus.Text = "Status: " & name
        AfterFilterChanged(btnStatus)
    End Sub

    Private Sub AfterFilterChanged(b As RoundedButton)
        FitButton(b)
        LayoutToolbar()
        _page = 0
        RefreshList()
    End Sub

    Private Sub ClearFilters()
        _category = "" : btnCategory.Text = "All categories" : FitButton(btnCategory)
        _inventory = "All" : btnInventory.Text = "Inventory: All" : FitButton(btnInventory)
        _status = "All" : btnStatus.Text = "Status: All" : FitButton(btnStatus)
        LayoutToolbar()
        txtSearch.Text = ""
    End Sub

    ' ------------------------------------------------------------
    '  Add / Edit / Delete  (all saving is done by DataStore)
    ' ------------------------------------------------------------
    Private Sub AddProduct()
        Using dlg As New AddProductForm()
            If dlg.ShowDialog(FindForm()) <> System.Windows.Forms.DialogResult.OK Then Return

            Dim newProduct As New Product With {
                .Name = dlg.ProductName,
                .Price = dlg.ProductPrice,
                .Stock = dlg.ProductStock,
                .Category = dlg.ProductCategory,
                .Description = dlg.ProductDescription
            }

            If DataStore.AddProduct(newProduct, dlg.SelectedImagePath) Then
                ' show the new product: clear filters and jump to its page
                ClearFilters()
                Dim idx As Integer = GetFiltered().IndexOf(newProduct)
                _page = If(idx < 0, 0, idx \ PageSize)
                RefreshList()
            End If
        End Using
    End Sub

    Private Sub EditProduct(p As Product)
        Using dlg As New AddProductForm(p)
            If dlg.ShowDialog(FindForm()) <> System.Windows.Forms.DialogResult.OK Then Return

            Dim oldPrice As Decimal = p.Price
            p.Name = dlg.ProductName
            p.Price = dlg.ProductPrice
            p.Stock = dlg.ProductStock
            p.Category = dlg.ProductCategory
            p.Description = dlg.ProductDescription
            DataStore.UpdateProduct(p, oldPrice, dlg.SelectedImagePath, CurrentSession.FullName)
        End Using
    End Sub

    Private Sub DeleteProduct(p As Product)
        Dim answer As System.Windows.Forms.DialogResult = MessageBox.Show(
            FindForm(),
            "Are you sure you want to delete """ & p.Name & """?" & vbCrLf &
            "Past transactions that contain this product will be kept.",
            "Delete Product", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If answer <> System.Windows.Forms.DialogResult.Yes Then Return

        DataStore.DeleteProduct(p)
    End Sub

End Class
