Imports System.Drawing
Imports Guna.UI2.WinForms

Public Class Cashier

    Private cart As New Dictionary(Of Product, Integer)
    Private selectedCategory As String = "All"

    Private Sub HideAllPanels()

        main_pnl.Visible = False
        pnl_CashierMessages.Visible = False

    End Sub

    Private Sub ResetButtonColors()

        btn_DashBoard.BackColor = Color.Transparent
        btn_Point_Of_Sale.BackColor = Color.Transparent
        btn_invtry.BackColor = Color.Transparent
        btn_hstry.BackColor = Color.Transparent

        btn_DashBoard.ForeColor = Color.DarkGray

    End Sub

    Private Sub btn_DashBoard_Click(
    sender As Object,
    e As EventArgs
    ) Handles btn_DashBoard.Click

        HideAllPanels()
        main_pnl.Visible = True

        ResetButtonColors()

        btn_DashBoard.BackColor = Color.MistyRose
        btn_DashBoard.ForeColor = Color.Black

        RefreshCashierData()

    End Sub

    Private Sub btn_Point_Of_Sale_Click(sender As Object, e As EventArgs) Handles btn_Point_Of_Sale.Click

        HideAllPanels()

        ResetButtonColors()

        btn_Point_Of_Sale.BackColor = Color.MistyRose
        btn_Point_Of_Sale.ForeColor = Color.Black

        'Load latest products from DataStore
        LoadProducts()

    End Sub

    Private Sub btnCashierLogout_Click(sender As Object, e As EventArgs) Handles btnCashierLogout.Click

        Dim result As DialogResult = MessageBox.Show(
            "Are you sure you want to logout?",
            "Logout Confirmation",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        )

        If result = DialogResult.Yes Then

            Dim loginPage As New LoginPage()
            loginPage.Show()

            Me.Hide()

            MessageBox.Show(
                "Logout successful.",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )

        Else

            MessageBox.Show(
                "Logout canceled.",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )

        End If

    End Sub

    '========================================
    ' TRANSACTION DISPLAY
    '========================================

    Private Sub LoadTransactions()

        dgv_Recent_Transactions.Rows.Clear()

        For Each transaction As POS_Transaction In DataStore.Transactions

            Dim itemsText As String = ""

            For Each item As TransactionItem In transaction.Items

                If itemsText <> "" Then
                    itemsText &= ", "
                End If

                itemsText &= item.ProductName &
                         " x" &
                         item.Quantity

            Next

            dgv_Recent_Transactions.Rows.Add(
            transaction.TransactionID,
            transaction.TransactionDate.ToString("hh:mm tt"),
            itemsText,
            transaction.PaymentMethod,
            "₱" & transaction.Total.ToString("N2"),
            transaction.Status
        )

        Next

    End Sub

    '========================================
    ' TRANSACTION UPDATE EVENT
    '========================================

    Private Sub Cashier_TransactionsChanged(
    sender As Object,
    e As EventArgs
    )

        RefreshCashierData()

    End Sub

    '========================================
    ' CASHIER DASHBOARD KPI
    '========================================

    Private Sub RefreshCashierDashboard()

        lbl_AverageOrder_Cashier.Text =
        "₱" & DataStore.GetTodayAverageOrder().ToString("N2")

        lbl_Today_Cashier.Text =
        "₱" & DataStore.GetTodaySales().ToString("N2")

        lbl_LowStock_Cashier.Text =
        DataStore.GetLowStockCount().ToString()

        lbl_TotalOrders_Cashier.Text =
        DataStore.GetTodayOrderCount().ToString()

    End Sub


    '========================================
    ' REFRESH CASHIER DATA
    '========================================

    Private Sub RefreshCashierData()

        LoadTransactions()
        RefreshCashierDashboard()

    End Sub


    '========================================
    ' PRODUCT MENU
    '========================================

    Private Sub LoadProducts()

        fl_Menu.Controls.Clear()

        Dim searchText As String = txt_SearchMenu.Text.Trim().ToLower()

        For Each product As Product In DataStore.Products

            '========================================
            ' SEARCH FILTER
            '========================================

            If searchText <> "" Then

                Dim currentProductName As String = product.Name.ToLower()
                Dim category As String = product.Category.ToLower()
                Dim description As String = product.Description.ToLower()

                If Not currentProductName.Contains(searchText) AndAlso
               Not category.Contains(searchText) AndAlso
               Not description.Contains(searchText) Then

                    Continue For

                End If

            End If


            '========================================
            ' CATEGORY FILTER
            '========================================

            If selectedCategory <> "All" Then

                If Not CategoryMatches(product.Category, selectedCategory) Then
                    Continue For
                End If

            End If


            '========================================
            ' PRODUCT CARD
            '========================================

            Dim productCard As New Panel()

            productCard.Width = 165
            productCard.Height = 220
            productCard.Margin = New Padding(8)
            productCard.BorderStyle = BorderStyle.FixedSingle
            productCard.BackColor = Color.White


            '========================================
            ' IMAGE
            '========================================

            Dim productPicture As New PictureBox()

            productPicture.Width = 145
            productPicture.Height = 85
            productPicture.Location = New Point(9, 8)
            productPicture.SizeMode = PictureBoxSizeMode.Zoom
            productPicture.BackColor = Color.White

            If product.Image IsNot Nothing Then
                productPicture.Image = product.Image
            End If


            '========================================
            ' NAME
            '========================================

            Dim productName As New Label()

            productName.Width = 145
            productName.Height = 30
            productName.Location = New Point(9, 98)
            productName.Text = product.Name
            productName.TextAlign = ContentAlignment.MiddleCenter
            productName.Font = New Font(
            "Segoe UI",
            10,
            FontStyle.Bold
        )
            productName.ForeColor = Color.FromArgb(62, 39, 35)


            '========================================
            ' PRICE
            '========================================

            Dim productPrice As New Label()

            productPrice.Width = 145
            productPrice.Height = 25
            productPrice.Location = New Point(9, 128)
            productPrice.Text = "₱" & product.Price.ToString("N2")
            productPrice.TextAlign = ContentAlignment.MiddleCenter
            productPrice.Font = New Font(
            "Segoe UI",
            10,
            FontStyle.Bold
        )
            productPrice.ForeColor = Color.FromArgb(62, 39, 35)


            '========================================
            ' STOCK
            '========================================

            Dim productStock As New Label()

            productStock.Width = 145
            productStock.Height = 22
            productStock.Location = New Point(9, 153)
            productStock.Text = "Stock: " & product.Stock.ToString()
            productStock.TextAlign = ContentAlignment.MiddleCenter
            productStock.Font = New Font(
            "Segoe UI",
            9,
            FontStyle.Regular
        )
            productStock.ForeColor = Color.Gray


            '========================================
            ' ORDER BUTTON
            '========================================

            Dim orderButton As New Button()

            orderButton.Width = 145
            orderButton.Height = 30
            orderButton.Location = New Point(9, 181)

            orderButton.Font = New Font(
            "Segoe UI",
            9,
            FontStyle.Bold
        )

            orderButton.FlatStyle = FlatStyle.Flat
            orderButton.FlatAppearance.BorderSize = 0

            orderButton.Tag = product


            If product.Stock > 0 Then

                orderButton.Text = "ORDER"
                orderButton.BackColor = Color.FromArgb(62, 39, 35)
                orderButton.ForeColor = Color.White
                orderButton.Enabled = True

            Else

                orderButton.Text = "OUT OF STOCK"
                orderButton.BackColor = Color.LightGray
                orderButton.ForeColor = Color.Gray
                orderButton.Enabled = False

            End If


            AddHandler orderButton.Click, AddressOf OrderButton_Click


            '========================================
            ' ADD TO CARD
            '========================================

            productCard.Controls.Add(productPicture)
            productCard.Controls.Add(productName)
            productCard.Controls.Add(productPrice)
            productCard.Controls.Add(productStock)
            productCard.Controls.Add(orderButton)

            fl_Menu.Controls.Add(productCard)

        Next

    End Sub

    Private Function CategoryMatches(
    productCategory As String,
    selectedCategory As String
    ) As Boolean

        Dim category As String = productCategory.Trim().ToLower()
        Dim selected As String = selectedCategory.Trim().ToLower()

        If selected = "all" Then
            Return True
        End If

        If selected = "hot coffee" Then
            Return category.Contains("hot")
        End If

        If selected = "iced coffee" Then
            Return category.Contains("iced")
        End If

        If selected = "specialty" Then
            Return category.Contains("special")
        End If

        If selected = "non coffee" Then
            Return category.Contains("non")
        End If

        Return category = selected

    End Function



    '========================================
    ' ORDER BUTTON
    '========================================

    Private Sub OrderButton_Click(
    sender As Object,
    e As EventArgs
)

        Dim orderButton As Button = TryCast(sender, Button)

        If orderButton Is Nothing Then Exit Sub

        Dim product As Product =
        TryCast(orderButton.Tag, Product)

        If product Is Nothing Then Exit Sub


        '========================================
        ' CHECK STOCK
        '========================================

        If product.Stock <= 0 Then

            MessageBox.Show(
            "This product is out of stock.",
            "Order",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning
        )

            LoadProducts()
            Exit Sub

        End If


        '========================================
        ' CHECK IF ALREADY IN CART
        '========================================

        If cart.ContainsKey(product) Then

            If cart(product) >= product.Stock Then

                MessageBox.Show(
                "You cannot order more than the available stock.",
                "Stock Limit",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

                Exit Sub

            End If

            cart(product) += 1

        Else

            cart.Add(product, 1)

        End If


        UpdateCartDisplay()

    End Sub

    Private Sub UpdateCartDisplay()

        fl_MenuProduct.Controls.Clear()

        For Each item As KeyValuePair(Of Product, Integer) In cart

            Dim product As Product = item.Key
            Dim quantity As Integer = item.Value

            '========================================
            ' CART ITEM PANEL
            '========================================

            Dim itemPanel As New Panel()

            itemPanel.Width = fl_MenuProduct.ClientSize.Width - 25
            itemPanel.Height = 65
            itemPanel.Margin = New Padding(3)
            itemPanel.BackColor = Color.White


            '========================================
            ' PRODUCT NAME
            '========================================

            Dim nameLabel As New Label()

            nameLabel.Text = product.Name
            nameLabel.Width = 120
            nameLabel.Height = 25
            nameLabel.Location = New Point(5, 5)

            nameLabel.Font = New Font(
            "Segoe UI",
            9,
            FontStyle.Bold
        )

            nameLabel.ForeColor = Color.FromArgb(62, 39, 35)


            '========================================
            ' PRODUCT PRICE
            '========================================

            Dim priceLabel As New Label()

            priceLabel.Text = "₱" & product.Price.ToString("N2")
            priceLabel.Width = 80
            priceLabel.Height = 20
            priceLabel.Location = New Point(5, 32)

            priceLabel.Font = New Font(
            "Segoe UI",
            8,
            FontStyle.Regular
        )

            priceLabel.ForeColor = Color.Gray


            '========================================
            ' MINUS BUTTON
            '========================================

            Dim minusButton As New Button()

            minusButton.Text = "-"
            minusButton.Width = 28
            minusButton.Height = 28
            minusButton.Location = New Point(125, 18)
            minusButton.Tag = product

            minusButton.FlatStyle = FlatStyle.Flat
            minusButton.FlatAppearance.BorderSize = 0


            '========================================
            ' QUANTITY
            '========================================

            Dim quantityLabel As New Label()

            quantityLabel.Text = quantity.ToString()
            quantityLabel.Width = 30
            quantityLabel.Height = 28
            quantityLabel.Location = New Point(155, 18)

            quantityLabel.TextAlign = ContentAlignment.MiddleCenter

            quantityLabel.Font = New Font(
            "Segoe UI",
            9,
            FontStyle.Bold
        )


            '========================================
            ' PLUS BUTTON
            '========================================

            Dim plusButton As New Button()

            plusButton.Text = "+"
            plusButton.Width = 28
            plusButton.Height = 28
            plusButton.Location = New Point(187, 18)
            plusButton.Tag = product

            plusButton.FlatStyle = FlatStyle.Flat
            plusButton.FlatAppearance.BorderSize = 0


            '========================================
            ' ITEM TOTAL
            '========================================

            Dim itemTotalLabel As New Label()

            itemTotalLabel.Text =
            "₱" & (product.Price * quantity).ToString("N2")

            itemTotalLabel.Width = 80
            itemTotalLabel.Height = 25
            itemTotalLabel.Location = New Point(220, 19)

            itemTotalLabel.TextAlign = ContentAlignment.MiddleRight

            itemTotalLabel.Font = New Font(
            "Segoe UI",
            9,
            FontStyle.Bold
        )

            itemTotalLabel.ForeColor =
            Color.FromArgb(62, 39, 35)


            '========================================
            ' DELETE BUTTON
            '========================================

            Dim deleteButton As New Button()

            deleteButton.Text = "X"
            deleteButton.Width = 28
            deleteButton.Height = 28
            deleteButton.Location = New Point(
            itemPanel.Width - 35,
            18
        )

            deleteButton.Tag = product

            deleteButton.FlatStyle = FlatStyle.Flat
            deleteButton.FlatAppearance.BorderSize = 0

            deleteButton.Font = New Font(
            "Segoe UI",
            8,
            FontStyle.Bold
        )

            deleteButton.ForeColor = Color.DarkRed


            '========================================
            ' BUTTON EVENTS
            '========================================

            AddHandler minusButton.Click,
            Sub()
                DecreaseQuantity(product)
            End Sub

            AddHandler plusButton.Click,
            Sub()
                IncreaseQuantity(product)
            End Sub

            AddHandler deleteButton.Click,
            Sub()
                DeleteCartItem(product)
            End Sub


            '========================================
            ' ADD CONTROLS
            '========================================

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

            MessageBox.Show(
            "You cannot order more than the available stock.",
            "Stock Limit",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning
        )

            Exit Sub

        End If

        cart(product) += 1

        UpdateCartDisplay()

    End Sub

    Private Sub DecreaseQuantity(product As Product)

        If Not cart.ContainsKey(product) Then Exit Sub

        cart(product) -= 1


        If cart(product) <= 0 Then

            cart.Remove(product)

        End If


        UpdateCartDisplay()

    End Sub

    Private Sub UpdateTotals()

        Dim subtotal As Decimal = 0D


        For Each item As KeyValuePair(Of Product, Integer) In cart

            subtotal += item.Key.Price * item.Value

        Next


        Dim tax As Decimal = subtotal * 0.12D

        Dim total As Decimal = subtotal + tax


        lbl_Subtotal.Text = "₱" & subtotal.ToString("N2")
        lbl_Tax.Text = "₱" & tax.ToString("N2")
        lbl_Total.Text = "₱" & total.ToString("N2")


        UpdateChange()

    End Sub

    Private Sub UpdateChange()

        Dim total As Decimal = GetCartTotal()

        Dim cashReceived As Decimal

        If Decimal.TryParse(
        txt_Cash_Receive.Text.Trim(),
        cashReceived
    ) Then

            Dim change As Decimal =
            cashReceived - total

            If change >= 0 Then

                lbl_Change.Text =
                "₱" & change.ToString("N2")

            Else

                lbl_Change.Text = "₱0.00"

            End If

        Else

            lbl_Change.Text = "₱0.00"

        End If

    End Sub

    Private Function GetCartTotal() As Decimal

        Dim subtotal As Decimal = 0D

        For Each item As KeyValuePair(Of Product, Integer) In cart

            subtotal += item.Key.Price * item.Value

        Next

        Dim tax As Decimal = subtotal * 0.12D

        Return subtotal + tax

    End Function

    Private Sub txt_Cash_Receive_TextChanged(
    sender As Object,
    e As EventArgs
    ) Handles txt_Cash_Receive.TextChanged

        UpdateChange()

    End Sub

    Private Sub btn_Clear_Click(
    sender As Object,
    e As EventArgs
    ) Handles btn_Clear.Click

        cart.Clear()

        fl_MenuProduct.Controls.Clear()

        lbl_Subtotal.Text = "₱0.00"
        lbl_Tax.Text = "₱0.00"
        lbl_Total.Text = "₱0.00"

        txt_Cash_Receive.Clear()

        lbl_Change.Text = "₱0.00"

    End Sub

    Private Sub btn_chkout_Click(
    sender As Object,
    e As EventArgs
    ) Handles btn_chkout.Click

        '========================================
        ' CHECK CART
        '========================================

        If cart.Count = 0 Then

            MessageBox.Show(
            "Your cart is empty.",
            "Checkout",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning
        )

            Exit Sub

        End If


        '========================================
        ' CHECK STOCK
        '========================================

        For Each item As KeyValuePair(Of Product, Integer) In cart

            If item.Value > item.Key.Stock Then

                MessageBox.Show(
                "Not enough stock for " & item.Key.Name & ".",
                "Checkout",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

                LoadProducts()
                Exit Sub

            End If

        Next


        '========================================
        ' GET TOTAL
        '========================================

        Dim total As Decimal = GetCartTotal()

        Dim cashReceived As Decimal


        '========================================
        ' VALIDATE CASH
        '========================================

        If Not Decimal.TryParse(
        txt_Cash_Receive.Text.Trim(),
        cashReceived
    ) Then

            MessageBox.Show(
            "Please enter the cash received.",
            "Checkout",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning
        )

            txt_Cash_Receive.Focus()
            Exit Sub

        End If


        If cashReceived < total Then

            MessageBox.Show(
            "Cash received is not enough.",
            "Checkout",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning
        )

            txt_Cash_Receive.Focus()
            Exit Sub

        End If


        '========================================
        ' DEDUCT STOCK
        '========================================

        For Each item As KeyValuePair(Of Product, Integer) In cart

            item.Key.Stock -= item.Value

            If item.Key.Stock <= 0 Then

                item.Key.Stock = 0
                item.Key.Status = "Unavailable"

            Else

                item.Key.Status = "Available"

            End If

        Next

        '========================================
        ' CREATE TRANSACTION
        '========================================

        Dim newTransaction As New POS_Transaction With {
        .TransactionDate = DateTime.Now,
        .Cashier = GlobalData.userName,
        .PaymentMethod = "Cash",
        .Total = total,
        .Status = "Completed"
        }

        '========================================
        ' SAVE ORDER ITEMS
        '========================================

        For Each item As KeyValuePair(Of Product, Integer) In cart

            Dim transactionItem As New TransactionItem With {
        .ProductName = item.Key.Name,
        .Quantity = item.Value,
        .Price = item.Key.Price
        }

            newTransaction.Items.Add(transactionItem)

        Next

        '========================================
        ' SAVE TRANSACTION TO SHARED DATASTORE
        '========================================

        DataStore.AddTransaction(newTransaction)

        '========================================
        ' SHOW CHANGE
        '========================================

        Dim change As Decimal =
        cashReceived - total

        lbl_Change.Text =
        "₱" & change.ToString("N2")

        '========================================
        ' CLEAR CART
        '========================================

        cart.Clear()

        fl_MenuProduct.Controls.Clear()

        lbl_Subtotal.Text = "₱0.00"
        lbl_Tax.Text = "₱0.00"
        lbl_Total.Text = "₱0.00"

        txt_Cash_Receive.Clear()

        '========================================
        ' REFRESH PRODUCT MENU
        '========================================

        LoadProducts()


        MessageBox.Show(
        "Checkout successful!",
        "Checkout",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information
    )

    End Sub

    Private Sub txt_SearchMenu_TextChanged(
    sender As Object,
    e As EventArgs
    ) Handles txt_SearchMenu.TextChanged

        LoadProducts()

    End Sub

    Private Sub btn_All_Click(
    sender As Object,
    e As EventArgs
    ) Handles btn_All.Click

        selectedCategory = "All"
        LoadProducts()

    End Sub

    Private Sub btn_hotCoffee_Click(
    sender As Object,
    e As EventArgs
    ) Handles btn_hotCoffee.Click

        selectedCategory = "Hot Coffee"
        LoadProducts()

    End Sub

    Private Sub btn_IcedCoffee_Click(
    sender As Object,
    e As EventArgs
    ) Handles btn_IcedCoffee.Click

        selectedCategory = "Iced Coffee"
        LoadProducts()

    End Sub

    Private Sub btn_Specialty_Click(
    sender As Object,
    e As EventArgs
    ) Handles btn_Specialty.Click

        selectedCategory = "Specialty"
        LoadProducts()

    End Sub

    Private Sub btn_Non_Coffee_Click(
    sender As Object,
    e As EventArgs
    ) Handles btn_Non_Coffee.Click

        selectedCategory = "Non Coffee"
        LoadProducts()

    End Sub

    '========================================
    ' CASHIER LOAD
    '========================================

    Private Sub Cashier_Load(
    sender As Object,
    e As EventArgs
    ) Handles MyBase.Load

        'PRODUCT MENU
        fl_Menu.FlowDirection = FlowDirection.LeftToRight
        fl_Menu.WrapContents = True
        fl_Menu.AutoScroll = True

        'CART
        fl_MenuProduct.FlowDirection = FlowDirection.TopDown
        fl_MenuProduct.WrapContents = False
        fl_MenuProduct.AutoScroll = True

        'TRANSACTION UPDATE EVENT
        AddHandler DataStore.TransactionsChanged,
        AddressOf Cashier_TransactionsChanged

        LoadProducts()

        fl_MenuProduct.Controls.Clear()

        lbl_Subtotal.Text = "₱0.00"
        lbl_Tax.Text = "₱0.00"
        lbl_Total.Text = "₱0.00"

        lbl_Change.Text = "₱0.00"

        'LOAD DASHBOARD DATA
        RefreshCashierData()

    End Sub


    '========================================
    ' CASHIER CHAT
    '========================================

    Private Sub btn_CashierMessages_Click(
        sender As Object,
        e As EventArgs
    ) Handles btn_CashierMessages.Click

        pnl_CashierMessages.Visible = True
        LoadCashierChat()

    End Sub


    Private Sub btnSend_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnSend.Click

        If String.IsNullOrWhiteSpace(txtChat.Text) Then
            Return
        End If

        Dim newMessage As New ChatMessage With {
            .Sender = "Cashier",
            .Receiver = "Admin",
            .Message = txtChat.Text.Trim(),
            .TimeSent = DateTime.Now
        }

        DataStore.ChatMessages.Add(newMessage)

        txtChat.Clear()

        LoadCashierChat()

    End Sub


    Private Sub LoadCashierChat()

        flpMessages.Controls.Clear()

        For Each chat As ChatMessage In DataStore.ChatMessages

            Dim messagePanel As New RoundedPanel()

            messagePanel.Width = flpMessages.ClientSize.Width - 120
            messagePanel.Height = 90
            messagePanel.Margin = New Padding(5)
            messagePanel.Padding = New Padding(10)
            messagePanel.BackColor = Color.FromArgb(62, 39, 35)


            'Sender
            Dim senderLabel As New Label()

            senderLabel.Text = chat.Sender
            senderLabel.AutoSize = True
            senderLabel.Location = New Point(10, 8)
            senderLabel.Font = New Font(
                "Segoe UI",
                9,
                FontStyle.Bold
            )
            senderLabel.ForeColor = Color.White


            'Message
            Dim messageLabel As New Label()

            messageLabel.Text = chat.Message
            messageLabel.AutoSize = False
            messageLabel.Width = messagePanel.Width - 20
            messageLabel.Height = 40
            messageLabel.Location = New Point(10, 28)
            messageLabel.Font = New Font(
                "Segoe UI",
                10,
                FontStyle.Regular
            )
            messageLabel.ForeColor = Color.White


            'Time
            Dim timeLabel As New Label()

            timeLabel.Text = chat.TimeSent.ToString("hh:mm tt")
            timeLabel.AutoSize = True
            timeLabel.Location = New Point(10, 68)
            timeLabel.Font = New Font(
                "Segoe UI",
                8,
                FontStyle.Regular
            )
            timeLabel.ForeColor = Color.LightGray


            messagePanel.Controls.Add(senderLabel)
            messagePanel.Controls.Add(messageLabel)
            messagePanel.Controls.Add(timeLabel)

            flpMessages.Controls.Add(messagePanel)

        Next


        If flpMessages.Controls.Count > 0 Then

            flpMessages.ScrollControlIntoView(
                flpMessages.Controls(
                    flpMessages.Controls.Count - 1
                )
            )

        End If

    End Sub
End Class