Public Class Admin

    Private Sub btnAdminLogout_Click(sender As Object, e As EventArgs) Handles btnAdminLogout.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Dim loginPage As New LoginPage()
            loginPage.Show()
            Me.Hide()
            MessageBox.Show("Logout successful.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Logout canceled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub btnSaveProduct_Click(sender As Object, e As EventArgs) Handles btnSaveProduct.Click

        If Not ValidateProduct() Then Exit Sub

        Dim price As Decimal
        Dim stock As Integer

        Decimal.TryParse(txtProductPrice.Text.Trim(), price)
        Integer.TryParse(txtProductStock.Text.Trim(), stock)

        Dim newProduct As New Product With {
        .Name = txtProductName.Text.Trim(),
        .Price = price,
        .Stock = stock,
        .Category = cmb_Category.Text.Trim(),
        .Description = txtProductDescription.Text.Trim(),
        .Image = productImage.Image
    }

        If stock > 0 Then
            newProduct.Status = "Available"
        Else
            newProduct.Status = "Unavailable"
        End If

        DataStore.Products.Add(newProduct)

        LoadProducts()

        MessageBox.Show(
        "Product saved successfully.",
        "Product",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information
    )

    End Sub


    Private Function ValidateProduct() As Boolean

        Dim valid As Boolean = True

        lblProductNameError.Visible = False
        lblPriceError.Visible = False
        lblStockError.Visible = False
        lblCategoryError.Visible = False
        lblDescriptionError.Visible = False

        If String.IsNullOrWhiteSpace(txtProductName.Text) Then
            lblProductNameError.Text = "This field is required."
            lblProductNameError.Visible = True
            valid = False
        End If

        Dim price As Decimal

        If String.IsNullOrWhiteSpace(txtProductPrice.Text) Then

            lblPriceError.Text = "This field is required."
            lblPriceError.Visible = True
            valid = False

        ElseIf Not Decimal.TryParse(txtProductPrice.Text.Trim(), price) Then

            lblPriceError.Text = "Enter a valid price."
            lblPriceError.Visible = True
            valid = False

        ElseIf price < 0 Then

            lblPriceError.Text = "Price cannot be negative."
            lblPriceError.Visible = True
            valid = False

        End If

        Dim stock As Integer

        If String.IsNullOrWhiteSpace(txtProductStock.Text) Then

            lblStockError.Text = "This field is required."
            lblStockError.Visible = True
            valid = False

        ElseIf Not Integer.TryParse(txtProductStock.Text.Trim(), stock) Then

            lblStockError.Text = "Enter a valid stock."
            lblStockError.Visible = True
            valid = False

        ElseIf stock < 0 Then

            lblStockError.Text = "Stock cannot be negative."
            lblStockError.Visible = True
            valid = False

        End If

        If String.IsNullOrWhiteSpace(cmb_Category.Text) Then
            lblCategoryError.Text = "This field is required."
            lblCategoryError.Visible = True
            valid = False
        End If

        If String.IsNullOrWhiteSpace(txtProductDescription.Text) Then
            lblDescriptionError.Text = "This field is required."
            lblDescriptionError.Visible = True
            valid = False
        End If

        Return valid

    End Function

    Private Sub LoadProducts()

        dgvProducts.Rows.Clear()

        For Each item In DataStore.Products

            Dim row As Integer = dgvProducts.Rows.Add()

            dgvProducts.Rows(row).Cells("Product").Value = item.Name
            dgvProducts.Rows(row).Cells("Price").Value = item.Price
            dgvProducts.Rows(row).Cells("Stock").Value = item.Stock

        Next

    End Sub

    Private Sub btnChooseImage_Click(sender As Object, e As EventArgs)
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            productImage.Image = Image.FromFile(OpenFileDialog1.FileName)
        End If
    End Sub

    'Dashboard Panel and buttons
    Private Sub ShowPanel(panelToShow As Panel)

        For Each panel As Panel In {
        pnl_dashboard_system,
        pnl_Products,
        pnl_Messages,
        pnlCashiers,
        pnl_Inventory,
        pnl_Settings,
        pnl_History
    }
            panel.Visible = False
        Next

        panelToShow.Visible = True

    End Sub

    'Buttons
    Private Sub btn_dashboardAdmin_Click(sender As Object, e As EventArgs) Handles btn_dashboardAdmin.Click
        ShowPanel(pnl_dashboard_system) 'button to show dashboard panel
    End Sub

    Private Sub btn_Product_Click(sender As Object, e As EventArgs) Handles btn_Product.Click
        ShowPanel(pnl_Products) 'button to show products panel
    End Sub

    Private Sub btnMessages_Click(sender As Object, e As EventArgs) Handles btnMessages.Click
        ShowPanel(pnl_Messages) 'button to show messages panel
    End Sub
    Private Sub btnCashiers_Click(sender As Object, e As EventArgs) Handles btnCashiers.Click
        ShowPanel(pnlCashiers) 'button to show cashiers panel)
        displayCashiers()      ' always show the latest list
    End Sub

    Private Sub btnInventory_Click(sender As Object, e As EventArgs) Handles btnInventory.Click
        ShowPanel(pnl_Inventory) 'button to show inventory panel)
    End Sub

    Private Sub Guna2Button5_Click(sender As Object, e As EventArgs) Handles Guna2Button5.Click
        ShowPanel(pnl_Settings) 'button to show settings panel) 
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        ShowPanel(pnl_History) 'button to show history panel
    End Sub

    'Function is the chat box
    Private Sub LoadChatMessages()

        flpAdminMessages.Controls.Clear()

        For Each chat As ChatMessage In DataStore.ChatMessages

            'Main message bubble
            Dim messagePanel As New RoundedPanel()

            messagePanel.Width = flpAdminMessages.ClientSize.Width - 120
            messagePanel.BorderRadius = 15
            messagePanel.AutoSize = True
            messagePanel.Padding = New Padding(10)
            messagePanel.Margin = New Padding(5)

            'Sender
            Dim senderLabel As New Label()

            senderLabel.Text = chat.Sender
            senderLabel.AutoSize = True
            senderLabel.Font = New Font("Segoe UI", 9, FontStyle.Bold)

            'Message
            Dim messageLabel As New Label()


            messageLabel.Text = chat.Message
            messageLabel.AutoSize = True
            messageLabel.MaximumSize = New Size(350, 0)
            messageLabel.Font = New Font("Segoe UI", 10)

            'Time
            Dim timeLabel As New Label()

            timeLabel.Text = chat.TimeSent.ToString("hh:mm tt")
            timeLabel.AutoSize = True
            timeLabel.Font = New Font("Segoe UI", 8)

            'Colors
            If chat.Sender = "Admin" Then

                messagePanel.BackColor = Color.FromKnownColor(KnownColor.ControlLight)

                senderLabel.ForeColor = Color.FromArgb(62, 39, 35)
                messageLabel.ForeColor = Color.FromArgb(62, 39, 35)
                timeLabel.ForeColor = Color.Gray

            Else

                messagePanel.BackColor = Color.FromArgb(62, 39, 35)

                senderLabel.ForeColor = Color.White
                messageLabel.ForeColor = Color.White
                timeLabel.ForeColor = Color.LightGray

            End If

            'Add controls
            messagePanel.Controls.Add(timeLabel)
            messagePanel.Controls.Add(messageLabel)
            messagePanel.Controls.Add(senderLabel)

            'Arrange vertically
            timeLabel.Dock = DockStyle.Top
            messageLabel.Dock = DockStyle.Top
            senderLabel.Dock = DockStyle.Top

            'Add bubble
            flpAdminMessages.Controls.Add(messagePanel)

        Next

        'Scroll to latest message
        If flpAdminMessages.Controls.Count > 0 Then

            flpAdminMessages.ScrollControlIntoView(
            flpAdminMessages.Controls(flpAdminMessages.Controls.Count - 1)
        )

        End If

    End Sub

    Private Sub displayCashiers()
        Dim currentDateTime As DateTime = DateTime.Now
        cashierDataGridView.Rows.Clear()
        cashierDataGridView.Rows.Add("Jedrick Miclat", "jedrick", "Active", "09/28/2026")
        For Each account As String() In GlobalData.registerAccount

            cashierDataGridView.Rows.Add(account(0), account(1), "Active", currentDateTime.ToString("d"))
        Next

    End Sub


    'Admin Load Event
    Private Sub Admin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadChatMessages()
        displayCashiers()
    End Sub

    'Admin Send Button Click Event
    Private Sub btnAdminSend_Click(sender As Object, e As EventArgs) Handles btnAdmin.Click

        If String.IsNullOrWhiteSpace(txtAdminChat.Text) Then
            Return
        End If

        Dim newMessage As New ChatMessage With {
            .Sender = "Admin",
            .Receiver = "Cashier",
            .Message = txtAdminChat.Text.Trim(),
            .TimeSent = DateTime.Now
        }

        DataStore.ChatMessages.Add(newMessage)

        txtAdminChat.Clear()

        LoadChatMessages()

    End Sub

    Private Sub btnRegisterNewCashier_Click(sender As Object, e As EventArgs) Handles btnRegisterNewCashier.Click
        Using registerCashierForm As New RegisterNewCashierForm()
            If registerCashierForm.ShowDialog(Me) = DialogResult.OK Then
                displayCashiers()
            End If
        End Using
    End Sub

End Class