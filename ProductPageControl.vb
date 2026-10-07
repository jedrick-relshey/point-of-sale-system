Option Strict On
Option Explicit On

Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms

' ============================================================
'  ProductPageControl.vb
'  The "Product management" page (header, search + filters,
'  product table, pagination, Add product button).
'  Built fully in code - no Designer needed.
'
'  Usage (in any form):
'      Dim page As New ProductPageControl()
'      page.Dock = DockStyle.Fill
'      page.SetUser("Alex Morgan", "Admin " & ChrW(&HB7) & " Morning shift")
'      Me.Controls.Add(page)
' ============================================================
Public Class ProductPageControl
    Inherits UserControl

    Private Const PageSize As Integer = 8
    Private Const RowHeight As Integer = 49

    ' column widths in % : name, price, category, stock, pricing, actions
    Private ReadOnly _colWidths As Single() = {30.0F, 16.0F, 16.0F, 12.0F, 13.0F, 13.0F}

    ' header
    Private pnlHeader As BorderedPanel
    Private btnAdd As RoundedButton
    Private btnBell As RoundedButton
    Private divider As Panel
    Private avatar As AvatarCircle
    Private lblUserName As Label
    Private lblUserRole As Label
    Private btnLogout As RoundedButton

    ' toolbar
    Private pnlToolbar As Panel
    Private pnlSearch As CardPanel
    Private txtSearch As TextBox
    Private lblSearchHint As Label
    Private btnCategory As RoundedButton
    Private btnPricing As RoundedButton

    ' card
    Private pnlCardHeader As Panel
    Private lblCardSub As Label
    Private badgeSync As Badge
    Private pnlRows As Panel
    Private lblShowing As Label
    Private lblPrev As Label
    Private lblPage As Label
    Private lblNext As Label

    ' state
    Private _page As Integer = 0
    Private _totalPages As Integer = 1
    Private _category As String = ""
    Private _pricing As String = "All"
    Private _lastChange As DateTime? = Nothing

    ''' <summary>Raised when the user clicks the logout icon in the header.</summary>
    Public Event LogoutRequested()

    Public Sub New()
        DoubleBuffered = True
        BackColor = Theme.PageBg
        Font = Theme.UiFont(9.0F)
        BuildUi()
        SetUser(Environment.UserName, "Administrator")
        RefreshList()
    End Sub

    ' ------------------------------------------------------------
    '  Public API
    ' ------------------------------------------------------------
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

    ''' <summary>Rebuilds the table from ProductStore. Call after loading data.</summary>
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
            empty.Text = If(ProductStore.Products.Count = 0,
                            "No products yet. Click ""Add product"" to create your first one.",
                            "No products match your search / filters.")
            empty.ForeColor = Theme.TextMuted
            empty.TextAlign = ContentAlignment.MiddleCenter
            empty.SetBounds(0, 40, pnlRows.ClientSize.Width, 40)
            pnlRows.Controls.Add(empty)
        End If
        pnlRows.ResumeLayout()

        ' footer text
        Dim firstNo As Integer = If(items.Count = 0, 0, _page * PageSize + 1)
        Dim lastNo As Integer = _page * PageSize + pageItems.Count
        lblShowing.Text = "Showing " & firstNo.ToString() & ChrW(&H2013).ToString() & lastNo.ToString() &
                          " of " & items.Count.ToString() & " products"
        lblPage.Text = "Page " & (_page + 1).ToString() & " of " & _totalPages.ToString()
        lblPrev.ForeColor = If(_page > 0, Theme.TextDark, Theme.TextMuted)
        lblNext.ForeColor = If(_page < _totalPages - 1, Theme.TextDark, Theme.TextMuted)

        ' card subtitle
        Dim activeCount As Integer = ProductStore.Products.Where(
            Function(x) Not String.Equals(x.Status, "Inactive", StringComparison.OrdinalIgnoreCase)).Count()
        Dim dynCount As Integer = ProductStore.Products.Where(Function(x) ProductStore.IsDynamic(x)).Count()
        lblCardSub.Text = activeCount.ToString() & " active products " & ChrW(&HB7).ToString() & " " &
                          dynCount.ToString() & " use dynamic pricing"
    End Sub

    ' ------------------------------------------------------------
    '  Build the UI
    ' ------------------------------------------------------------
    Private Sub BuildUi()
        SuspendLayout()

        ' root: header (fixed) + body (fill)
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
        lblUserName.Size = New Size(130, 18)
        lblUserName.Top = 21
        pnlHeader.Controls.Add(lblUserName)

        lblUserRole = New Label()
        lblUserRole.Font = Theme.UiFont(7.5F)
        lblUserRole.ForeColor = Theme.TextMuted
        lblUserRole.AutoEllipsis = True
        lblUserRole.Size = New Size(130, 16)
        lblUserRole.Top = 39
        pnlHeader.Controls.Add(lblUserRole)

        btnLogout = New RoundedButton()
        btnLogout.Size = New Size(32, 36)
        btnLogout.Top = 19
        btnLogout.FillColor = Color.Transparent
        btnLogout.HoverColor = Theme.RedSoft
        btnLogout.Glyph = Theme.GlyphLogout
        btnLogout.GlyphColor = Theme.Red
        btnLogout.GlyphFont = Theme.IconFont(10.0F)
        AddHandler btnLogout.Click, Sub(s, e) RaiseEvent LogoutRequested()
        pnlHeader.Controls.Add(btnLogout)

        AddHandler pnlHeader.Resize, Sub(s, e) LayoutHeader()
        LayoutHeader()
    End Sub

    Private Sub LayoutHeader()
        Dim x As Integer = pnlHeader.Width - 24
        x -= btnLogout.Width : btnLogout.Left = x
        x -= 8 + lblUserName.Width : lblUserName.Left = x : lblUserRole.Left = x
        x -= 10 + avatar.Width : avatar.Left = x
        x -= 16 + divider.Width : divider.Left = x
        x -= 16 + btnBell.Width : btnBell.Left = x
        x -= 12 + btnAdd.Width : btnAdd.Left = x
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

        btnPricing = MakeFilterButton("Pricing: All", Theme.GlyphFilter)
        AddHandler btnPricing.Click, Sub(s, e) ShowPricingMenu()
        pnlToolbar.Controls.Add(btnPricing)

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
        btnPricing.Left = pnlToolbar.Width - btnPricing.Width
        btnCategory.Left = btnPricing.Left - 10 - btnCategory.Width
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

        ' -- card header: title, subtitle, sync badge --
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

        badgeSync = New Badge()
        badgeSync.Setup("No changes yet", Theme.TealSoft, Theme.TealDark)
        badgeSync.Top = 18
        pnlCardHeader.Controls.Add(badgeSync)

        AddHandler pnlCardHeader.Resize, Sub(s, e) badgeSync.Left = pnlCardHeader.Width - badgeSync.Width - 20
        t.Controls.Add(pnlCardHeader, 0, 0)

        ' -- column header --
        Dim colHeader As New Panel()
        colHeader.Dock = DockStyle.Fill
        colHeader.Margin = New Padding(0)
        colHeader.BackColor = Theme.HeaderRowBg

        Dim hdr As TableLayoutPanel = NewColumnsTable()
        Dim titles As String() = {"Product name", "Price", "Category", "Stock", "Pricing", "Actions"}
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

        ' -- footer: "Showing x of y" + pagination --
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

        ' 1 - price (+ pencil icon if dynamic)
        Dim dynamic As Boolean = ProductStore.IsDynamic(p)
        Dim priceText As String = ProductStore.PriceText(p)
        Dim priceFont As Font = Theme.UiFont(8.5F, FontStyle.Bold)
        Dim cellPrice As Panel = NewCell()
        cellPrice.Controls.Add(CellLabel(priceText, priceFont, Theme.TextDark))
        If dynamic Then
            Dim tw As Integer = TextRenderer.MeasureText(priceText, priceFont, New Size(1000, 100), TextFormatFlags.NoPadding).Width
            Dim pencil As New Label()
            pencil.Text = Theme.GlyphEdit
            pencil.Font = Theme.IconFont(8.0F)
            pencil.ForeColor = Theme.TealDark
            pencil.AutoSize = False
            pencil.Size = New Size(18, 18)
            pencil.TextAlign = ContentAlignment.MiddleCenter
            pencil.Location = New Point(tw + 6, (innerH - 18) \ 2)
            cellPrice.Controls.Add(pencil)
            pencil.BringToFront()
        End If
        t.Controls.Add(cellPrice, 1, 0)

        ' 2 - category
        t.Controls.Add(CellLabel(If(p.Category, ""), Theme.UiFont(8.0F), Theme.TextMuted), 2, 0)

        ' 3 - stock (red when 0)
        t.Controls.Add(CellLabel(p.Stock.ToString(), Theme.UiFont(8.5F),
                                 If(p.Stock <= 0, Theme.Red, Theme.TextDark)), 3, 0)

        ' 4 - pricing badge
        Dim cellBadge As Panel = NewCell()
        Dim pill As New Badge()
        If dynamic Then
            pill.Setup("Dynamic", Theme.TealSoft, Theme.TealDark)
        Else
            pill.Setup("Fixed", Theme.BadgeFixedBg, Theme.BadgeFixedText)
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
        btnEdit.HoverColor = Color.FromArgb(186, 235, 226)
        btnEdit.Glyph = Theme.GlyphEdit
        btnEdit.GlyphColor = Theme.TealDark
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
        Dim fills As Color() = {Color.FromArgb(254, 243, 199), Color.FromArgb(226, 232, 240),
                                Color.FromArgb(204, 251, 241), Color.FromArgb(252, 231, 243),
                                Color.FromArgb(224, 231, 255)}
        Dim fores As Color() = {Color.FromArgb(217, 119, 6), Color.FromArgb(30, 41, 59),
                                Theme.TealDark, Color.FromArgb(190, 24, 93),
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

        For Each p As Product In ProductStore.Products
            If q.Length > 0 Then
                Dim hay As String = If(p.Name, "") & " " & If(p.Category, "")
                If hay.IndexOf(q, StringComparison.OrdinalIgnoreCase) < 0 Then Continue For
            End If
            If _category.Length > 0 AndAlso
               Not String.Equals(p.Category, _category, StringComparison.OrdinalIgnoreCase) Then Continue For

            Dim dyn As Boolean = ProductStore.IsDynamic(p)
            If _pricing = "Fixed" AndAlso dyn Then Continue For
            If _pricing = "Dynamic" AndAlso Not dyn Then Continue For

            result.Add(p)
        Next
        Return result
    End Function

    Private Sub ShowCategoryMenu()
        Dim m As New ContextMenuStrip()
        m.Font = Theme.UiFont(9.0F)

        Dim all As New ToolStripMenuItem("All categories")
        all.Checked = (_category.Length = 0)
        AddHandler all.Click, Sub(s, e) SetCategory("")
        m.Items.Add(all)

        For Each c As String In ProductStore.Categories()
            Dim name As String = c
            Dim item As New ToolStripMenuItem(name)
            item.Checked = String.Equals(name, _category, StringComparison.OrdinalIgnoreCase)
            AddHandler item.Click, Sub(s, e) SetCategory(name)
            m.Items.Add(item)
        Next

        AddHandler m.Closed, Sub(s, e) BeginInvoke(New MethodInvoker(Sub() m.Dispose()))
        m.Show(btnCategory, New Point(0, btnCategory.Height + 2))
    End Sub

    Private Sub ShowPricingMenu()
        Dim m As New ContextMenuStrip()
        m.Font = Theme.UiFont(9.0F)

        For Each opt As String In New String() {"All", "Fixed", "Dynamic"}
            Dim name As String = opt
            Dim item As New ToolStripMenuItem(name)
            item.Checked = (name = _pricing)
            AddHandler item.Click, Sub(s, e) SetPricing(name)
            m.Items.Add(item)
        Next

        AddHandler m.Closed, Sub(s, e) BeginInvoke(New MethodInvoker(Sub() m.Dispose()))
        m.Show(btnPricing, New Point(0, btnPricing.Height + 2))
    End Sub

    Private Sub SetCategory(name As String)
        _category = name
        btnCategory.Text = If(name.Length = 0, "All categories", name)
        FitButton(btnCategory)
        LayoutToolbar()
        _page = 0
        RefreshList()
    End Sub

    Private Sub SetPricing(name As String)
        _pricing = name
        btnPricing.Text = "Pricing: " & name
        FitButton(btnPricing)
        LayoutToolbar()
        _page = 0
        RefreshList()
    End Sub

    ' ------------------------------------------------------------
    '  Add / Edit / Delete
    ' ------------------------------------------------------------
    Private Sub AddProduct()
        Using dlg As New AddProductForm()
            If dlg.ShowDialog(FindForm()) <> System.Windows.Forms.DialogResult.OK Then Return

            ProductStore.SaveProduct(dlg.ResultProduct, dlg.ResultVariants)

            ' clear filters so the new product is visible, then jump to its page
            _category = ""
            btnCategory.Text = "All categories"
            FitButton(btnCategory)
            _pricing = "All"
            btnPricing.Text = "Pricing: All"
            FitButton(btnPricing)
            LayoutToolbar()
            txtSearch.Text = ""

            Dim idx As Integer = GetFiltered().IndexOf(dlg.ResultProduct)
            _page = If(idx < 0, 0, idx \ PageSize)
            Touch()
        End Using
    End Sub

    Private Sub EditProduct(p As Product)
        Using dlg As New AddProductForm(p)
            If dlg.ShowDialog(FindForm()) <> System.Windows.Forms.DialogResult.OK Then Return
            ProductStore.SaveProduct(dlg.ResultProduct, dlg.ResultVariants)
            Touch()
        End Using
    End Sub

    Private Sub DeleteProduct(p As Product)
        Dim answer As System.Windows.Forms.DialogResult = MessageBox.Show(
            FindForm(), "Delete """ & p.Name & """? This cannot be undone.", "Delete product",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If answer <> System.Windows.Forms.DialogResult.Yes Then Return

        ProductStore.DeleteProduct(p)
        Touch()
    End Sub

    ' refresh table + "last saved" badge after any change
    Private Sub Touch()
        _lastChange = DateTime.Now
        badgeSync.Setup("Last saved " & _lastChange.Value.ToString("h:mm tt"), Theme.TealSoft, Theme.TealDark)
        badgeSync.Left = pnlCardHeader.Width - badgeSync.Width - 20
        RefreshList()
    End Sub

End Class