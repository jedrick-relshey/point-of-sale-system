Imports System.Drawing

Public Class Cashier

    Private Sub HideAllPanels()

        main_pnl.Visible = False
        pnl_CashierMessages.Visible = False

    End Sub

    Private Sub ResetButtonColors()

        btn_DashBoard.BackColor = Color.Transparent
        btn_Point_Of_Sale.BackColor = Color.Transparent
        btn_prdt.BackColor = Color.Transparent
        btn_invtry.BackColor = Color.Transparent
        btn_hstry.BackColor = Color.Transparent

        btn_DashBoard.ForeColor = Color.DarkGray

    End Sub

    Private Sub btn_DashBoard_Click(sender As Object, e As EventArgs) Handles btn_DashBoard.Click

        HideAllPanels()
        main_pnl.Visible = True

        ResetButtonColors()

        btn_DashBoard.BackColor = Color.MistyRose
        btn_DashBoard.ForeColor = Color.Black

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
    ' PRODUCT MENU
    '========================================

    Private Sub LoadProducts()

        'Remove old product cards first
        fl_Menu.Controls.Clear()

        'Create a card for every product
        For Each product As Product In DataStore.Products

            Dim productCard As New Panel()

            productCard.Width = 165
            productCard.Height = 220
            productCard.Margin = New Padding(8)
            productCard.BorderStyle = BorderStyle.FixedSingle
            productCard.BackColor = Color.White


            '========================================
            ' PRODUCT IMAGE
            '========================================

            Dim productPicture As New PictureBox()

            productPicture.Width = 145
            productPicture.Height = 85
            productPicture.Location = New Point(9, 8)
            productPicture.SizeMode = PictureBoxSizeMode.Zoom
            productPicture.BackColor = Color.White

            If product.Image IsNot Nothing Then
                productPicture.Image = product.Image
            Else
                productPicture.Image = Nothing
            End If


            '========================================
            ' PRODUCT NAME
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

            'Store the actual Product object
            orderButton.Tag = product


            'Stock checking
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
            ' ADD CONTROLS TO CARD
            '========================================

            productCard.Controls.Add(productPicture)
            productCard.Controls.Add(productName)
            productCard.Controls.Add(productPrice)
            productCard.Controls.Add(productStock)
            productCard.Controls.Add(orderButton)


            'Add card to menu
            fl_Menu.Controls.Add(productCard)

        Next

    End Sub


    '========================================
    ' ORDER BUTTON
    '========================================

    Private Sub OrderButton_Click(sender As Object, e As EventArgs)

        Dim orderButton As Button = TryCast(sender, Button)

        If orderButton Is Nothing Then Exit Sub

        'Get the actual Product object
        Dim product As Product =
            TryCast(orderButton.Tag, Product)

        If product Is Nothing Then Exit Sub

        'Check stock again before ordering
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

        MessageBox.Show(
            "You selected " & product.Name,
            "Order",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information
        )

    End Sub


    '========================================
    ' CASHIER LOAD
    '========================================

    Private Sub Cashier_Load(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Load

        LoadProducts()

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