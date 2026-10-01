Public Class Admin

    Private isLoadingProducts As Boolean = True
    Private editingProduct As Product = Nothing
    Private isLoading As Boolean = True

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

        If editingProduct Is Nothing Then

            'NEW PRODUCT
            Dim newProduct As New Product With {
            .Name = txtProductName.Text.Trim(),
            .Price = price,
            .Stock = stock,
            .Category = txtCategory.Text.Trim(),
            .Description = txtProductDescription.Text.Trim(),
            .Image = productImage.Image
        }

            If stock > 0 Then
                newProduct.Status = "Available"
            Else
                newProduct.Status = "Unavailable"
            End If

            DataStore.Products.Add(newProduct)

        Else

            'EDIT EXISTING PRODUCT
            editingProduct.Name = txtProductName.Text.Trim()
            editingProduct.Price = price
            editingProduct.Stock = stock
            editingProduct.Category = txtCategory.Text.Trim()
            editingProduct.Description = txtProductDescription.Text.Trim()
            editingProduct.Image = productImage.Image

            If stock > 0 Then
                editingProduct.Status = "Available"
            Else
                editingProduct.Status = "Unavailable"
            End If

        End If

        editingProduct = Nothing

        LoadFilterOptions()
        FilterProducts()

        txtProductName.Clear()
        txtProductPrice.Clear()
        txtProductStock.Clear()
        txtCategory.Clear()
        txtProductDescription.Clear()

        productImage.Image = Nothing
    End Sub

    '========================================
    ' ADMIN TRANSACTION DISPLAY
    '========================================

    Private Sub LoadAdminTransactions()

        adminDataGrid.Rows.Clear()

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

            adminDataGrid.Rows.Add(
            transaction.TransactionID,
            transaction.TransactionDate.ToString("hh:mm tt"),
            transaction.Cashier,
            itemsText,
            "₱" & transaction.Total.ToString("N2"),
            transaction.Status
        )

        Next

    End Sub


    '========================================
    ' ADMIN KPI
    '========================================

    Private Sub RefreshAdminDashboard()

        lbl_Average_Order.Text =
        "₱" & DataStore.GetTodayAverageOrder().ToString("N2")

        lbl_TodaySales.Text =
        "₱" & DataStore.GetTodaySales().ToString("N2")

        lbl_TotalOrders.Text =
        DataStore.GetTodayOrderCount().ToString()

        lbl_LowStock.Text =
        DataStore.GetLowStockCount().ToString()

    End Sub


    '========================================
    ' REFRESH ADMIN DATA
    '========================================

    Private Sub RefreshAdminData()

        LoadAdminTransactions()
        RefreshAdminDashboard()

    End Sub

    '========================================
    ' TRANSACTION UPDATE EVENT
    '========================================

    Private Sub Admin_TransactionsChanged(
    sender As Object,
    e As EventArgs
    )

        RefreshAdminData()

    End Sub

    'Loads the filter options for product type, inventory status, and product status into the respective combo boxes. It also preserves the current selections if they exist in the new list of options.
    Private Sub LoadFilterOptions()

        Dim currentProductType As String = cmb_ProductType.Text
        Dim currentInventory As String = cmb_Inventory.Text
        Dim currentStatus As String = cmb_Status.Text

        cmb_ProductType.Items.Clear()
        cmb_Inventory.Items.Clear()
        cmb_Status.Items.Clear()

        cmb_ProductType.Items.Add("All")

        Dim categories As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

        For Each item As Product In DataStore.Products

            If Not String.IsNullOrWhiteSpace(item.Category) Then
                categories.Add(item.Category)
            End If

        Next

        For Each category As String In categories
            cmb_ProductType.Items.Add(category)
        Next


        'Inventory choices
        cmb_Inventory.Items.Add("All")
        cmb_Inventory.Items.Add("In Stock")
        cmb_Inventory.Items.Add("Low Stock")
        cmb_Inventory.Items.Add("Out of Stock")


        'Status choices
        cmb_Status.Items.Add("All")

        Dim statuses As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

        For Each item As Product In DataStore.Products

            If Not String.IsNullOrWhiteSpace(item.Status) Then
                statuses.Add(item.Status)
            End If

        Next

        For Each status As String In statuses
            cmb_Status.Items.Add(status)
        Next


        'Restore previous selections
        If cmb_ProductType.Items.Contains(currentProductType) Then
            cmb_ProductType.SelectedItem = currentProductType
        Else
            cmb_ProductType.SelectedIndex = 0
        End If

        If cmb_Inventory.Items.Contains(currentInventory) Then
            cmb_Inventory.SelectedItem = currentInventory
        Else
            cmb_Inventory.SelectedIndex = 0
        End If

        If cmb_Status.Items.Contains(currentStatus) Then
            cmb_Status.SelectedItem = currentStatus
        Else
            cmb_Status.SelectedIndex = 0
        End If

    End Sub

    Private Sub FilterProducts()

        If isLoading AndAlso dgvProducts.Columns.Count = 0 Then Exit Sub

        dgvProducts.Rows.Clear()

        Dim searchText As String = txtSearch.Text.Trim().ToLower()
        Dim selectedType As String = cmb_ProductType.Text
        Dim selectedInventory As String = cmb_Inventory.Text
        Dim selectedStatus As String = cmb_Status.Text

        For Each item As Product In DataStore.Products

            'SEARCH FILTER
            If searchText <> "" Then

                Dim productName As String = item.Name.ToLower()
                Dim category As String = item.Category.ToLower()
                Dim description As String = item.Description.ToLower()

                If Not productName.Contains(searchText) AndAlso
               Not category.Contains(searchText) AndAlso
               Not description.Contains(searchText) Then

                    Continue For

                End If

            End If

            'PRODUCT TYPE FILTER
            If selectedType <> "" AndAlso
           selectedType <> "All" AndAlso
           Not item.Category.Equals(selectedType, StringComparison.OrdinalIgnoreCase) Then

                Continue For

            End If

            'INVENTORY FILTER
            If selectedInventory = "In Stock" AndAlso item.Stock <= 0 Then

                Continue For

            ElseIf selectedInventory = "Low Stock" AndAlso
               (item.Stock <= 0 OrElse item.Stock > 5) Then

                Continue For

            ElseIf selectedInventory = "Out of Stock" AndAlso item.Stock > 0 Then

                Continue For

            End If

            'STATUS FILTER
            If selectedStatus <> "" AndAlso
           selectedStatus <> "All" AndAlso
           Not item.Status.Equals(selectedStatus, StringComparison.OrdinalIgnoreCase) Then

                Continue For

            End If

            'ADD ROW
            Dim row As Integer = dgvProducts.Rows.Add()

            dgvProducts.Rows(row).Cells("dgvImage").Value = item.Image
            dgvProducts.Rows(row).Cells("Product").Value = item.Name
            dgvProducts.Rows(row).Cells("Price").Value = item.Price
            dgvProducts.Rows(row).Cells("Category").Value = item.Category
            dgvProducts.Rows(row).Cells("Stock").Value = item.Stock
            dgvProducts.Rows(row).Cells("Status").Value = item.Status
            dgvProducts.Rows(row).Cells("Actions").Value = "Edit | Delete"

            'SAVE THE ACTUAL PRODUCT IN THE ROW
            dgvProducts.Rows(row).Tag = item

        Next

    End Sub

    Private Sub dgvProducts_CellClick(
    sender As Object,
    e As DataGridViewCellEventArgs
) Handles dgvProducts.CellClick

        If e.RowIndex < 0 Then Exit Sub

        If e.ColumnIndex = dgvProducts.Columns("Actions").Index Then

            Dim product As Product =
            TryCast(dgvProducts.Rows(e.RowIndex).Tag, Product)

            If product Is Nothing Then Exit Sub

            Dim cellWidth As Integer =
            dgvProducts.Columns("Actions").Width

            Dim mouseX As Integer =
            dgvProducts.PointToClient(Cursor.Position).X

            Dim cellLeft As Integer =
            dgvProducts.GetCellDisplayRectangle(
                e.ColumnIndex,
                e.RowIndex,
                True
            ).Left

            Dim clickPosition As Integer = mouseX - cellLeft

            If clickPosition < cellWidth / 2 Then

                EditProduct(product)

            Else

                DeleteProduct(product)

            End If

        End If

    End Sub

    Private Sub DeleteProduct(product As Product)

        Dim result As DialogResult = MessageBox.Show(
        "Are you sure you want to delete """ & product.Name & """?",
        "Delete Product",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning
    )

        If result = DialogResult.No Then Exit Sub

        DataStore.Products.Remove(product)

        LoadFilterOptions()
        FilterProducts()

        MessageBox.Show(
        "Product deleted successfully.",
        "Product",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information
    )

    End Sub

    Private Sub EditProduct(product As Product)

        editingProduct = product

        txtProductName.Text = product.Name
        txtProductPrice.Text = product.Price.ToString()
        txtProductStock.Text = product.Stock.ToString()
        txtCategory.Text = product.Category
        txtProductDescription.Text = product.Description

        productImage.Image = product.Image

    End Sub

    Private Sub cmb_ProductType_SelectedIndexChanged(
        sender As Object,
        e As EventArgs
    ) Handles cmb_ProductType.SelectedIndexChanged

        If isLoadingProducts Then Exit Sub

        FilterProducts()

    End Sub


    Private Sub cmb_Inventory_SelectedIndexChanged(
        sender As Object,
        e As EventArgs
    ) Handles cmb_Inventory.SelectedIndexChanged

        If isLoadingProducts Then Exit Sub

        FilterProducts()

    End Sub


    Private Sub cmb_Status_SelectedIndexChanged(
        sender As Object,
        e As EventArgs
    ) Handles cmb_Status.SelectedIndexChanged

        If isLoadingProducts Then Exit Sub

        FilterProducts()

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

        If String.IsNullOrWhiteSpace(txtCategory.Text) Then
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

    Private Sub btnChooseImage_Click(sender As Object, e As EventArgs) Handles btnChooseImage.Click

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
        pnl_History
    }
            panel.Visible = False
        Next

        panelToShow.Visible = True

    End Sub

    'Buttons
    Private Sub btn_dashboardAdmin_Click(
    sender As Object,
    e As EventArgs
    ) Handles btn_dashboardAdmin.Click

        ShowPanel(pnl_dashboard_system)

        RefreshAdminData()

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

            If account(0) <> "Jedrick Miclat" Then
                cashierDataGridView.Rows.Add(account(0), account(1), "Active", currentDateTime.ToString("MM/dd/yyyy"))
            End If
        Next
    End Sub

    'Admin Load Event
    Private Sub Admin_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        LoadChatMessages()
        displayCashiers()
        LoadFilterOptions()
        FilterProducts()

        isLoadingProducts = False
        isLoading = False

        'TRANSACTION UPDATE EVENT
        AddHandler DataStore.TransactionsChanged,
        AddressOf Admin_TransactionsChanged

        'LOAD DASHBOARD DATA
        RefreshAdminData()

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

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        FilterProducts()
    End Sub
End Class