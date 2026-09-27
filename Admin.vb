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

        Decimal.TryParse(txtProductPrice.Text, price)
        Integer.TryParse(txtProductStock.Text, stock)

        Dim newProduct As New Product()

        newProduct.Name = txtProductName.Text
        newProduct.Price = price
        newProduct.Stock = stock
        newProduct.Description = txtProductDescription.Text

        DataStore.Products.Add(newProduct)

        LoadProducts()
    End Sub

    Private Function ValidateProduct() As Boolean

        Dim textBoxes() As Control = {
        txtProductName,
        txtProductPrice,
        txtProductStock,
        txtProductDescription
    }

        Dim errorLabels() As Control = {
        lblProductNameError,
        lblPriceError,
        lblStockError,
        lblCategoryError,
        lblDescriptionError
    }

        Dim valid As Boolean = True

        For i As Integer = 0 To textBoxes.Length - 1

            errorLabels(i).Visible = False

            If textBoxes(i).Text.Trim() = "" Then
                errorLabels(i).Text = "This field is required."
                errorLabels(i).Visible = True
                valid = False

            ElseIf i = 1 AndAlso Not Decimal.TryParse(textBoxes(i).Text, Nothing) Then
                errorLabels(i).Text = "Enter a valid price."
                errorLabels(i).Visible = True
                valid = False

            ElseIf i = 2 AndAlso Not Integer.TryParse(textBoxes(i).Text, Nothing) Then
                errorLabels(i).Text = "Enter a valid stock."
                errorLabels(i).Visible = True
                valid = False
            End If

        Next

        Return valid

    End Function

    Private Sub LoadProducts()

        dgvProducts.Rows.Clear()

        For Each item In DataStore.Products

            Dim row As Integer = dgvProducts.Rows.Add()

            dgvProducts.Rows(row).Cells("Product").Value = item.Name
            dgvProducts.Rows(row).Cells("Price").Value = item.Price
            dgvProducts.Rows(row).Cells("Stock").Value = item.Stock
            dgvProducts.Rows(row).Cells("Description").Value = item.Description

        Next

    End Sub

    Private Sub btnChooseImage_Click(sender As Object, e As EventArgs) Handles btnChooseImage.Click
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            productImage.Image = Image.FromFile(OpenFileDialog1.FileName)
        End If
    End Sub

    'Dashboard Panel
    Private Sub ShowPanel(panelToShow As Panel)

        For Each panel As Panel In {
        pnl_dashboard_system,
        pnlProductInput,
        pnl_Products,
        pnl_Messages,
        pnlCashiers
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

    Private Sub btn_AddProduct_Click(sender As Object, e As EventArgs) Handles btn_AddProduct.Click
        ShowPanel(pnlProductInput)
    End Sub
    Private Sub btnMessages_Click(sender As Object, e As EventArgs) Handles btnMessages.Click
        ShowPanel(pnl_Messages) 'button to show messages panel
    End Sub
    Private Sub btnCashiers_Click(sender As Object, e As EventArgs) Handles btnCashiers.Click
        ShowPanel(pnlCashiers) 'button to show cashiers panel)
    End Sub

    Private Sub LoadChatMessages()

        flpAdminMessages.Controls.Clear()

        For Each chat As ChatMessage In DataStore.ChatMessages

            If chat.Receiver = "Admin" Then
                Dim messageLabel As New Label()
                messageLabel.AutoSize = True
                messageLabel.MaximumSize = New Size(flpAdminMessages.ClientSize.Width - 20, 0)
                messageLabel.Text = chat.Sender & ": " & chat.Message
                messageLabel.Padding = New Padding(10)
                messageLabel.Margin = New Padding(5)
                messageLabel.Font = New Font("Segoe UI", 10, FontStyle.Regular)
                flpAdminMessages.Controls.Add(messageLabel)

            End If

        Next

    End Sub

    'Admin Load Event
    Private Sub Admin_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        LoadChatMessages()

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
End Class