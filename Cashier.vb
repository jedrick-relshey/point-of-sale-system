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
'   Panel1      = Inventory screen (filled in code)
'   pnl_History = Transaction history (filled by HistoryView)
Public Class Cashier

    Private cart As New Dictionary(Of Product, Integer)
    Private selectedCategory As String = "All"

    Private history As HistoryView
    Private isProcessing As Boolean = False
    Private closingForLogout As Boolean = False

    Private invGrid As DataGridView
    Private invSearch As Guna2TextBox

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

        BuildInventoryUi()
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
        If history IsNot Nothing Then history.Dispose()
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
        history.Reload()
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
    Private Sub LoadTransactions()

        dgv_Recent_Transactions.Rows.Clear()

        Dim recent As New List(Of POS_Transaction)(DataStore.Transactions)
        recent.Sort(Function(a, b) b.TransactionDate.CompareTo(a.TransactionDate))

        Dim count As Integer = 0
        For Each t As POS_Transaction In recent
            Dim timeText As String = If(t.TransactionDate.Date = DateTime.Today,
                                        t.TransactionDate.ToString("hh:mm tt"),
                                        t.TransactionDate.ToString("MMM d, hh:mm tt"))
            dgv_Recent_Transactions.Rows.Add(t.TransactionID, timeText, DataStore.BuildItemsText(t),
                                             t.PaymentMethod, Peso(t.Total), t.Status)
            count += 1
            If count >= 15 Then Exit For
        Next
    End Sub

    Private Sub RefreshCashierDashboard()
        lbl_AverageOrder_Cashier.Text = Peso(DataStore.GetTodayAverageOrder())
        lbl_Today_Cashier.Text = Peso(DataStore.GetTodaySales())
        lbl_LowStock_Cashier.Text = DataStore.GetLowStockCount().ToString()
        lbl_TotalOrders_Cashier.Text = DataStore.GetTodayOrderCount().ToString()
    End Sub

    Private Sub RefreshCashierData()
        LoadTransactions()
        RefreshCashierDashboard()
    End Sub

    '=================================================================
    ' HISTORY (shared HistoryView - read-only for cashiers)
    '=================================================================
    Private Sub SetupHistory()
        Guna2TextBox1.PlaceholderText = "Search ID, cashier, product..."
        pnl_History.BackColor = CafeUi.Cream
        Label4.Font = New Font("Segoe UI", 18.0F, FontStyle.Bold)
        Label4.ForeColor = CafeUi.Coffee
        Label4.Location = New Point(31, 18)
        Label15.Font = New Font("Segoe UI", 9.5F)
        Label15.ForeColor = Color.FromArgb(105, 79, 70)
        Label15.Location = New Point(36, 54)
        Guna2Panel15.Location = New Point(34, 105)
        Guna2Panel15.Size = New Size(934, 498)
        Guna2Panel15.FillColor = Color.White
        Guna2Panel15.BorderColor = Color.FromArgb(190, 164, 154)
        Guna2Panel15.BorderThickness = 1
        Guna2DataGridView1.Location = New Point(1, 42)
        Guna2DataGridView1.Size = New Size(Guna2Panel15.Width - 2, Guna2Panel15.Height - 95)
        Guna2DataGridView1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        For Each c As Control In Guna2Panel15.Controls
            If c IsNot Label16 AndAlso c IsNot Guna2DataGridView1 Then c.Visible = False
        Next
        Label16.Location = New Point(14, 11)
        Label16.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        Label16.ForeColor = CafeUi.Coffee

        history = New HistoryView(Me,
                                  pnl_History, New Point(36, 94),
                                  Guna2DataGridView1, Guna2TextBox1,
                                  Guna2ComboBox1, Guna2ComboBox2,
                                  Guna2Panel15, New Point(14, 466),
                                  False)
    End Sub

    Private Sub BuildMessagingLayout()
        CafeUi.StyleMessaging(pnl_CashierMessages, FlowLayoutPanel3, pnl_MainChat,
                              flpMessages, CashierName, Guna2HtmlLabel60,
                              txtChat, btnSend, "Admin Support",
                              "Terminal Communications",
                              "Send a message to the administrator for support or store updates.")
    End Sub

    '=================================================================
    ' INVENTORY (read-only view for the cashier; built in code in Panel1)
    '=================================================================
    Private Sub BuildInventoryUi()

        Panel1.Controls.Add(New Label With {
            .Text = "Inventory", .AutoSize = True, .Location = New Point(31, 30),
            .Font = New Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold), .ForeColor = brown})
        Panel1.Controls.Add(New Label With {
            .Text = "Live stock levels. Ask the administrator to restock low items.",
            .AutoSize = True, .Location = New Point(33, 64),
            .Font = New Font("Segoe UI", 9.5F), .ForeColor = Color.FromArgb(110, 90, 85)})

        invSearch = New Guna2TextBox With {
            .PlaceholderText = "Search stock...",
            .Location = New Point(740, 28), .Size = New Size(228, 36), .BorderRadius = 6,
            .Font = New Font("Segoe UI", 9.5F)
        }
        Panel1.Controls.Add(invSearch)
        AddHandler invSearch.TextChanged, Sub(s As Object, ev As EventArgs) RefreshCashierInventory()

        invGrid = New DataGridView With {
            .Location = New Point(35, 100), .Size = New Size(934, 500),
            .AllowUserToAddRows = False, .AllowUserToDeleteRows = False, .AllowUserToResizeRows = False,
            .ReadOnly = True, .RowHeadersVisible = False,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect, .MultiSelect = False,
            .BackgroundColor = Color.White, .BorderStyle = BorderStyle.FixedSingle,
            .EnableHeadersVisualStyles = False, .GridColor = Color.Gainsboro,
            .RowTemplate = New DataGridViewRow With {.Height = 46},
            .ColumnHeadersHeight = 36,
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        }
        invGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(216, 203, 199)
        invGrid.ColumnHeadersDefaultCellStyle.ForeColor = brown
        invGrid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        invGrid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(216, 203, 199)
        invGrid.DefaultCellStyle.Font = New Font("Segoe UI", 10.0F)
        invGrid.DefaultCellStyle.ForeColor = brown
        invGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 233, 230)
        invGrid.DefaultCellStyle.SelectionForeColor = brown

        invGrid.Columns.Add(New DataGridViewImageColumn With {.Name = "colCInvPic", .HeaderText = "Pic", .Width = 60, .ImageLayout = DataGridViewImageCellLayout.Zoom})
        invGrid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "colCInvName", .HeaderText = "Product Name", .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, .FillWeight = 160})
        invGrid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "colCInvPrice", .HeaderText = "Price", .Width = 110})
        invGrid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "colCInvStock", .HeaderText = "Current Stock", .Width = 120})
        invGrid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "colCInvMin", .HeaderText = "Min. Stock", .Width = 100})
        invGrid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "colCInvStatus", .HeaderText = "Status", .Width = 130})
        invGrid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "colCInvUpdated", .HeaderText = "Last Updated", .Width = 160})
        Panel1.Controls.Add(invGrid)
    End Sub

    Private Sub RefreshCashierInventory()
        If invGrid Is Nothing Then Return
        Dim q As String = invSearch.Text.Trim()
        invGrid.Rows.Clear()
        For Each p As Product In DataStore.Products
            If q <> "" AndAlso p.Name.IndexOf(q, StringComparison.OrdinalIgnoreCase) < 0 Then Continue For
            Dim st As String = DataStore.GetProductStockStatus(p)
            Dim updated As String = If(p.LastUpdated.Date = DateTime.Today,
                                       "Today, " & p.LastUpdated.ToString("hh:mm tt"),
                                       p.LastUpdated.ToString("MMM d, hh:mm tt"))
            Dim idx As Integer = invGrid.Rows.Add(p.Image, p.Name, Peso(p.Price), p.Stock, DataStore.GetMinStock(p), st, updated)
            invGrid.Rows(idx).Cells("colCInvStatus").Style.ForeColor = StatusColor(st)
            invGrid.Rows(idx).Cells("colCInvStatus").Style.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        Next
        invGrid.ClearSelection()
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
            g.Clear(CardFill)
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