Option Strict On
Option Explicit On

Imports System.Drawing
Imports Guna.UI2.WinForms

' Cashier.vb  -  REPLACES your old Cashier.vb.
' Cashier_Designer.vb is NOT changed. Existing controls are re-used:
'   btn_chkout        = "Cash" checkout button
'   btnOnlinePayment  = "Card" checkout button (text is changed to "Card" at runtime)
'   Panel1            = Inventory screen (was empty, filled in code)
'   pnl_History       = Transaction history (filled by HistoryView)
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

        btnOnlinePayment.Text = "Card"
        btn_chkout.Text = "Cash"

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

        For Each c As Control In {pnl_PointOfSale, pnl_CashierMessages, dashbrd_pnl, Panel1, pnl_History}
            c.Visible = (c Is target)
        Next
        target.BringToFront()

        For Each b As Guna2Button In navButtons
            b.FillColor = originalFill(b)
            b.ForeColor = originalFore(b)
        Next
        activeButton.FillColor = Color.MistyRose
        activeButton.ForeColor = Color.Black
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
        fl_Menu.Controls.Clear()

        Dim searchText As String = txt_SearchMenu.Text.Trim().ToLower()

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

            Dim productCard As New Panel()
            productCard.Width = 165
            productCard.Height = 220
            productCard.Margin = New Padding(8)
            productCard.BorderStyle = BorderStyle.FixedSingle
            productCard.BackColor = Color.White

            Dim productPicture As New PictureBox()
            productPicture.Width = 145
            productPicture.Height = 85
            productPicture.Location = New Point(9, 8)
            productPicture.SizeMode = PictureBoxSizeMode.Zoom
            productPicture.BackColor = Color.White
            If product.Image IsNot Nothing Then productPicture.Image = product.Image

            Dim productName As New Label()
            productName.Width = 145
            productName.Height = 30
            productName.Location = New Point(9, 98)
            productName.Text = product.Name
            productName.TextAlign = ContentAlignment.MiddleCenter
            productName.Font = New Font("Segoe UI", 10, FontStyle.Bold)
            productName.ForeColor = brown

            Dim productPrice As New Label()
            productPrice.Width = 145
            productPrice.Height = 25
            productPrice.Location = New Point(9, 128)
            productPrice.Text = Peso(product.Price)
            productPrice.TextAlign = ContentAlignment.MiddleCenter
            productPrice.Font = New Font("Segoe UI", 10, FontStyle.Bold)
            productPrice.ForeColor = brown

            Dim stockLevel As String = DataStore.GetProductStockStatus(product)
            Dim productStock As New Label()
            productStock.Width = 145
            productStock.Height = 22
            productStock.Location = New Point(9, 153)
            productStock.Text = "Stock: " & product.Stock.ToString()
            productStock.TextAlign = ContentAlignment.MiddleCenter
            productStock.Font = New Font("Segoe UI", 9, FontStyle.Regular)
            productStock.ForeColor = If(stockLevel = StockStatus.InStock, Color.Gray, StatusColor(stockLevel))

            Dim orderButton As New Button()
            orderButton.Width = 145
            orderButton.Height = 30
            orderButton.Location = New Point(9, 181)
            orderButton.Font = New Font("Segoe UI", 9, FontStyle.Bold)
            orderButton.FlatStyle = FlatStyle.Flat
            orderButton.FlatAppearance.BorderSize = 0
            orderButton.Tag = product

            If product.Stock > 0 Then
                orderButton.Text = "ORDER"
                orderButton.BackColor = brown
                orderButton.ForeColor = Color.White
                orderButton.Enabled = True
            Else
                orderButton.Text = "OUT OF STOCK"
                orderButton.BackColor = Color.LightGray
                orderButton.ForeColor = Color.Gray
                orderButton.Enabled = False
            End If

            AddHandler orderButton.Click, AddressOf OrderButton_Click

            productCard.Controls.Add(productPicture)
            productCard.Controls.Add(productName)
            productCard.Controls.Add(productPrice)
            productCard.Controls.Add(productStock)
            productCard.Controls.Add(orderButton)

            fl_Menu.Controls.Add(productCard)
        Next

        fl_Menu.ResumeLayout()
    End Sub

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

        Dim orderButton As Button = TryCast(sender, Button)
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

        fl_MenuProduct.Controls.Clear()

        For Each item As KeyValuePair(Of Product, Integer) In cart

            Dim product As Product = item.Key
            Dim quantity As Integer = item.Value

            Dim itemPanel As New Panel()
            itemPanel.Width = fl_MenuProduct.ClientSize.Width - 25
            itemPanel.Height = 65
            itemPanel.Margin = New Padding(3)
            itemPanel.BackColor = Color.White

            Dim nameLabel As New Label()
            nameLabel.Text = product.Name
            nameLabel.Width = 120
            nameLabel.Height = 25
            nameLabel.Location = New Point(5, 5)
            nameLabel.Font = New Font("Segoe UI", 9, FontStyle.Bold)
            nameLabel.ForeColor = brown

            Dim priceLabel As New Label()
            priceLabel.Text = Peso(product.Price)
            priceLabel.Width = 80
            priceLabel.Height = 20
            priceLabel.Location = New Point(5, 32)
            priceLabel.Font = New Font("Segoe UI", 8, FontStyle.Regular)
            priceLabel.ForeColor = Color.Gray

            Dim minusButton As New Button()
            minusButton.Text = "-"
            minusButton.Width = 28
            minusButton.Height = 28
            minusButton.Location = New Point(125, 18)
            minusButton.Tag = product
            minusButton.FlatStyle = FlatStyle.Flat
            minusButton.FlatAppearance.BorderSize = 0

            Dim quantityLabel As New Label()
            quantityLabel.Text = quantity.ToString()
            quantityLabel.Width = 30
            quantityLabel.Height = 28
            quantityLabel.Location = New Point(155, 18)
            quantityLabel.TextAlign = ContentAlignment.MiddleCenter
            quantityLabel.Font = New Font("Segoe UI", 9, FontStyle.Bold)

            Dim plusButton As New Button()
            plusButton.Text = "+"
            plusButton.Width = 28
            plusButton.Height = 28
            plusButton.Location = New Point(187, 18)
            plusButton.Tag = product
            plusButton.FlatStyle = FlatStyle.Flat
            plusButton.FlatAppearance.BorderSize = 0

            Dim itemTotalLabel As New Label()
            itemTotalLabel.Text = Peso(product.Price * quantity)
            itemTotalLabel.Width = 80
            itemTotalLabel.Height = 25
            itemTotalLabel.Location = New Point(220, 19)
            itemTotalLabel.TextAlign = ContentAlignment.MiddleRight
            itemTotalLabel.Font = New Font("Segoe UI", 9, FontStyle.Bold)
            itemTotalLabel.ForeColor = brown

            Dim deleteButton As New Button()
            deleteButton.Text = "X"
            deleteButton.Width = 28
            deleteButton.Height = 28
            deleteButton.Location = New Point(itemPanel.Width - 35, 18)
            deleteButton.Tag = product
            deleteButton.FlatStyle = FlatStyle.Flat
            deleteButton.FlatAppearance.BorderSize = 0
            deleteButton.Font = New Font("Segoe UI", 8, FontStyle.Bold)
            deleteButton.ForeColor = Color.DarkRed

            AddHandler minusButton.Click, Sub(s As Object, ev As EventArgs) DecreaseQuantity(product)
            AddHandler plusButton.Click, Sub(s As Object, ev As EventArgs) IncreaseQuantity(product)
            AddHandler deleteButton.Click, Sub(s As Object, ev As EventArgs) DeleteCartItem(product)

            itemPanel.Controls.Add(nameLabel)
            itemPanel.Controls.Add(priceLabel)
            itemPanel.Controls.Add(minusButton)
            itemPanel.Controls.Add(quantityLabel)
            itemPanel.Controls.Add(plusButton)
            itemPanel.Controls.Add(itemTotalLabel)
            itemPanel.Controls.Add(deleteButton)

            fl_MenuProduct.Controls.Add(itemPanel)
        Next

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
    End Sub

    Private Sub txt_Cash_Receive_TextChanged(sender As Object, e As EventArgs) Handles txt_Cash_Receive.TextChanged
        UpdateChange()
    End Sub

    Private Sub ResetPaymentDisplay()
        lbl_Subtotal.Text = Peso(0D)
        lbl_Tax.Text = Peso(0D)
        lbl_Total.Text = Peso(0D)
        lbl_Change.Text = Peso(0D)
    End Sub

    Private Sub ClearCart()
        cart.Clear()
        fl_MenuProduct.Controls.Clear()
        txt_Cash_Receive.Clear()
        ResetPaymentDisplay()
    End Sub

    Private Sub btn_Clear_Click(sender As Object, e As EventArgs) Handles btn_Clear.Click
        ClearCart()
    End Sub

    '=================================================================
    ' CHECKOUT  (btn_chkout = Cash, btnOnlinePayment = Card)
    '=================================================================
    Private Sub btn_chkout_Click(sender As Object, e As EventArgs) Handles btn_chkout.Click
        ProcessCheckout(PaymentMethods.Cash)
    End Sub

    Private Sub btnOnlinePayment_Click(sender As Object, e As EventArgs) Handles btnOnlinePayment.Click
        ProcessCheckout(PaymentMethods.Card)
    End Sub

    Private Sub ProcessCheckout(method As String)

        If isProcessing Then Return                 ' blocks double-click / duplicate checkout
        isProcessing = True
        btn_chkout.Enabled = False
        btnOnlinePayment.Enabled = False

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

            ClearCart()
            LoadProducts()

            Dim info As String = "Checkout successful!" & vbCrLf & vbCrLf &
                                 "Transaction: " & trx.TransactionID & vbCrLf &
                                 "Payment: " & trx.PaymentMethod & vbCrLf &
                                 "Total: " & Peso(trx.Total)
            If trx.PaymentMethod = PaymentMethods.Cash Then
                info &= vbCrLf & "Cash: " & Peso(trx.CashReceived) & vbCrLf & "Change: " & Peso(trx.ChangeGiven)
            End If
            MessageBox.Show(info, "Checkout", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Checkout failed: " & ex.Message, "Checkout", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            isProcessing = False
            btn_chkout.Enabled = True
            btnOnlinePayment.Enabled = True
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
            .sender = "Cashier",
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
