Option Strict On
Option Explicit On

Imports System.Drawing
Imports Guna.UI2.WinForms

' Admin.vb  -  REPLACES your old Admin.vb.
' Admin_Designer.vb is NOT changed: every control that is new (inventory screen,
' Daily/Weekly/Monthly buttons, date picker, cashier Edit/Activate buttons...)
' is created in code below, using the control names that already exist.
Public Class Admin

    Private isLoadingProducts As Boolean = True
    Private editingProduct As Product = Nothing
    Private isLoading As Boolean = True
    Private selectedImagePath As String = ""
    Private editingCashierId As String = ""
    Private closingForLogout As Boolean = False

    Private history As HistoryView
    Private newProductPage As ProductPageControl      ' new Product management design

    ' ---- controls created in code (inventory screen) ----
    Private invSearch As Guna2TextBox
    Private invStatus As Guna2ComboBox
    Private invGrid As DataGridView
    Private flpRestock As FlowLayoutPanel
    Private lblRestockCount As Label
    Private nudMinStock As NumericUpDown
    Private syncingMinStock As Boolean = False

    Private ReadOnly brown As Color = Color.FromArgb(62, 39, 35)

    '=================================================================
    ' FORM LOAD / CLOSE
    '=================================================================
    Private Sub Admin_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DataStore.Initialize()
        ApplyResponsiveLayout()

        txtPass.UseSystemPasswordChar = True
        txtConfirmPass.UseSystemPasswordChar = True
        txtSearch.PlaceholderText = "Search products..."

        SetupCashierGrid()
        StyleSidebar()
        BuildDashboardUi()
        BuildInventoryUi()
        SetupHistory()
        BuildMessagingLayout()
        BuildCashierPage()
        InstallProductPage()

        LoadChatMessages()
        displayCashiers()
        LoadFilterOptions()
        FilterProducts()
        RefreshInventory()

        isLoadingProducts = False
        isLoading = False

        AddHandler DataStore.TransactionsChanged, AddressOf Admin_TransactionsChanged
        AddHandler DataStore.ProductsChanged, AddressOf Admin_ProductsChanged
        AddHandler DataStore.CashiersChanged, AddressOf Admin_CashiersChanged
        AddHandler DataStore.MessagesChanged, AddressOf Admin_MessagesChanged

        RefreshAdminData()
    End Sub

    Private Sub Admin_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        RemoveHandler DataStore.TransactionsChanged, AddressOf Admin_TransactionsChanged
        RemoveHandler DataStore.ProductsChanged, AddressOf Admin_ProductsChanged
        RemoveHandler DataStore.CashiersChanged, AddressOf Admin_CashiersChanged
        RemoveHandler DataStore.MessagesChanged, AddressOf Admin_MessagesChanged
        If history IsNot Nothing Then history.Dispose()

        If Not closingForLogout Then Application.Exit()
    End Sub

    Private Sub btnAdminLogout_Click(sender As Object, e As EventArgs) Handles btnAdminLogout.Click
        DoLogout()
    End Sub

    Private Sub DoLogout()
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to logout?",
            "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            closingForLogout = True
            AppNavigation.ShowLogin()
            Me.Close()
        End If
    End Sub

    '=================================================================
    ' DATA-CHANGED EVENTS
    '=================================================================
    Private Sub Admin_TransactionsChanged(sender As Object, e As EventArgs)
        RefreshAdminData()
        RefreshCashierPage()
    End Sub

    Private Sub Admin_ProductsChanged(sender As Object, e As EventArgs)
        isLoadingProducts = True
        LoadFilterOptions()
        isLoadingProducts = False
        FilterProducts()
        RefreshInventory()
        RefreshAdminDashboard()
    End Sub

    Private Sub Admin_CashiersChanged(sender As Object, e As EventArgs)
        displayCashiers()
        RefreshConversationList()
    End Sub

    Private Sub Admin_MessagesChanged(sender As Object, e As EventArgs)
        LoadChatMessages()
    End Sub

    '=================================================================
    ' DASHBOARD
    '=================================================================
    Private Sub LoadAdminTransactions()

        adminDataGrid.Rows.Clear()

        Dim recent As New List(Of POS_Transaction)(DataStore.Transactions)
        recent.Sort(Function(a, b) b.TransactionDate.CompareTo(a.TransactionDate))

        Dim count As Integer = 0
        For Each t As POS_Transaction In recent
            Dim timeText As String = If(t.TransactionDate.Date = DateTime.Today,
                                        t.TransactionDate.ToString("hh:mm tt"),
                                        t.TransactionDate.ToString("MMM d, hh:mm tt"))
            adminDataGrid.Rows.Add(t.TransactionID, timeText, t.Cashier,
                                   DataStore.BuildItemsText(t), Peso(t.Total), t.Status)
            count += 1
            If count >= 15 Then Exit For
        Next
    End Sub

    Private Sub RefreshAdminDashboard()
        lbl_Average_Order.Text = Peso(DataStore.GetTodayAverageOrder())
        lbl_TodaySales.Text = Peso(DataStore.GetTodaySales())
        lbl_TotalOrders.Text = DataStore.GetTodayOrderCount().ToString()
        lbl_LowStock.Text = DataStore.GetLowStockCount().ToString()
        RefreshDashboardUi()
    End Sub

    Private Sub RefreshAdminData()
        LoadAdminTransactions()
        RefreshAdminDashboard()
    End Sub

    '=================================================================
    ' NEW PRODUCT MANAGEMENT PAGE (replaces the look of the old products panel)
    ' The old controls stay in the Designer (hidden) so the old code still compiles.
    '=================================================================
    Private Sub InstallProductPage()
        For Each c As Control In pnl_Products.Controls
            c.Visible = False
        Next
        pnl_Products.AutoScroll = False

        newProductPage = New ProductPageControl()
        newProductPage.Dock = DockStyle.Fill
        pnl_Products.Controls.Add(newProductPage)
        newProductPage.BringToFront()
    End Sub

    '=================================================================
    ' PRODUCTS
    '=================================================================
    Private Sub btnSaveProduct_Click(sender As Object, e As EventArgs) Handles btnSaveProduct.Click

        If Not ValidateProduct() Then Exit Sub

        Dim price As Decimal
        Dim stock As Integer
        TryParseMoney(txtProductPrice.Text, price)
        Integer.TryParse(txtProductStock.Text.Trim(), stock)
        price = Math.Round(price, 2)

        Dim saved As Boolean

        If editingProduct Is Nothing Then

            Dim newProduct As New Product With {
                .Name = txtProductName.Text.Trim(),
                .Price = price,
                .Stock = stock,
                .Category = txtCategory.Text.Trim(),
                .Description = txtProductDescription.Text.Trim()
            }
            saved = DataStore.AddProduct(newProduct, selectedImagePath)

        Else

            Dim oldPrice As Decimal = editingProduct.Price
            editingProduct.Name = txtProductName.Text.Trim()
            editingProduct.Price = price
            editingProduct.Stock = stock
            editingProduct.Category = txtCategory.Text.Trim()
            editingProduct.Description = txtProductDescription.Text.Trim()
            saved = DataStore.UpdateProduct(editingProduct, oldPrice, selectedImagePath, CurrentSession.FullName)

        End If

        If saved Then
            ClearProductForm()
            MessageBox.Show("Product saved successfully.", "Product", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub btn_AddProduct_Click(sender As Object, e As EventArgs) Handles btn_AddProduct.Click
        ClearProductForm()
        txtProductName.Focus()
    End Sub

    Private Sub ClearProductForm()
        editingProduct = Nothing
        selectedImagePath = ""

        txtProductName.Clear()
        txtProductPrice.Clear()
        txtProductStock.Clear()
        txtCategory.Clear()
        txtProductDescription.Clear()
        productImage.Image = Nothing

        lblProductNameError.Visible = False
        lblPriceError.Visible = False
        lblStockError.Visible = False
        lblCategoryError.Visible = False
        lblDescriptionError.Visible = False
    End Sub

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
            If Not String.IsNullOrWhiteSpace(item.Category) Then categories.Add(item.Category)
        Next
        For Each category As String In categories
            cmb_ProductType.Items.Add(category)
        Next

        cmb_Inventory.Items.Add("All")
        cmb_Inventory.Items.Add(StockStatus.InStock)
        cmb_Inventory.Items.Add(StockStatus.LowStock)
        cmb_Inventory.Items.Add(StockStatus.OutOfStock)

        cmb_Status.Items.Add("All")
        Dim statuses As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        For Each item As Product In DataStore.Products
            If Not String.IsNullOrWhiteSpace(item.Status) Then statuses.Add(item.Status)
        Next
        For Each status As String In statuses
            cmb_Status.Items.Add(status)
        Next

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
        dgvProducts.Columns("Price").DefaultCellStyle.Format = ChrW(&H20B1) & "#,##0.00"

        Dim searchText As String = txtSearch.Text.Trim().ToLower()
        Dim selectedType As String = cmb_ProductType.Text
        Dim selectedInventory As String = cmb_Inventory.Text
        Dim selectedStatus As String = cmb_Status.Text

        For Each item As Product In DataStore.Products

            If searchText <> "" Then
                If Not item.Name.ToLower().Contains(searchText) AndAlso
                   Not item.Category.ToLower().Contains(searchText) AndAlso
                   Not item.Description.ToLower().Contains(searchText) Then
                    Continue For
                End If
            End If

            If selectedType <> "" AndAlso selectedType <> "All" AndAlso
               Not item.Category.Equals(selectedType, StringComparison.OrdinalIgnoreCase) Then
                Continue For
            End If

            If selectedInventory <> "" AndAlso selectedInventory <> "All" AndAlso
               DataStore.GetProductStockStatus(item) <> selectedInventory Then
                Continue For
            End If

            If selectedStatus <> "" AndAlso selectedStatus <> "All" AndAlso
               Not item.Status.Equals(selectedStatus, StringComparison.OrdinalIgnoreCase) Then
                Continue For
            End If

            Dim row As Integer = dgvProducts.Rows.Add()
            dgvProducts.Rows(row).Cells("dgvImage").Value = item.Image
            dgvProducts.Rows(row).Cells("Product").Value = item.Name
            dgvProducts.Rows(row).Cells("Price").Value = item.Price
            dgvProducts.Rows(row).Cells("Category").Value = item.Category
            dgvProducts.Rows(row).Cells("Stock").Value = item.Stock
            dgvProducts.Rows(row).Cells("Status").Value = item.Status
            dgvProducts.Rows(row).Cells("Actions").Value = "Edit | Delete"
            dgvProducts.Rows(row).Tag = item
        Next
    End Sub

    Private Sub dgvProducts_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvProducts.CellClick

        If e.RowIndex < 0 Then Exit Sub

        If e.ColumnIndex = dgvProducts.Columns("Actions").Index Then

            Dim product As Product = TryCast(dgvProducts.Rows(e.RowIndex).Tag, Product)
            If product Is Nothing Then Exit Sub

            Dim cellWidth As Integer = dgvProducts.Columns("Actions").Width
            Dim mouseX As Integer = dgvProducts.PointToClient(Cursor.Position).X
            Dim cellLeft As Integer = dgvProducts.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, True).Left
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
            "Are you sure you want to delete """ & product.Name & """?" & vbCrLf &
            "Past transactions that contain this product will be kept.",
            "Delete Product", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.No Then Exit Sub

        If editingProduct Is product Then ClearProductForm()

        If DataStore.DeleteProduct(product) Then
            MessageBox.Show("Product deleted successfully.", "Product", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub EditProduct(product As Product)

        editingProduct = product
        selectedImagePath = ""

        txtProductName.Text = product.Name
        txtProductPrice.Text = product.Price.ToString("0.00")
        txtProductStock.Text = product.Stock.ToString()
        txtCategory.Text = product.Category
        txtProductDescription.Text = product.Description
        productImage.Image = product.Image
    End Sub

    Private Sub cmb_ProductType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_ProductType.SelectedIndexChanged
        If isLoadingProducts Then Exit Sub
        FilterProducts()
    End Sub

    Private Sub cmb_Inventory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Inventory.SelectedIndexChanged
        If isLoadingProducts Then Exit Sub
        FilterProducts()
    End Sub

    Private Sub cmb_Status_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Status.SelectedIndexChanged
        If isLoadingProducts Then Exit Sub
        FilterProducts()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
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
        ElseIf Not TryParseMoney(txtProductPrice.Text, price) Then
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

        OpenFileDialog1.Filter = "Images|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim preview As Image = DataStore.LoadImageCopy(OpenFileDialog1.FileName)
            If preview Is Nothing Then
                MessageBox.Show("That file is not a valid image.", "Image", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            selectedImagePath = OpenFileDialog1.FileName
            productImage.Image = preview
        End If
    End Sub

    '=================================================================
    ' NAVIGATION
    '=================================================================
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
        HighlightNav(panelToShow)
    End Sub

    Private Sub btn_dashboardAdmin_Click(sender As Object, e As EventArgs) Handles btn_dashboardAdmin.Click
        ShowPanel(pnl_dashboard_system)
        RefreshAdminData()
    End Sub

    Private Sub btn_Product_Click(sender As Object, e As EventArgs) Handles btn_Product.Click
        ShowPanel(pnl_Products)
        FilterProducts()
        If newProductPage IsNot Nothing Then
            newProductPage.RefreshList()
        End If
    End Sub

    Private Sub btnMessages_Click(sender As Object, e As EventArgs) Handles btnMessages.Click
        ShowPanel(pnl_Messages)
        LoadChatMessages()
    End Sub

    Private Sub btnCashiers_Click(sender As Object, e As EventArgs) Handles btnCashiers.Click
        ShowPanel(pnlCashiers)
        displayCashiers()
    End Sub

    Private Sub btnInventory_Click(sender As Object, e As EventArgs) Handles btnInventory.Click
        ShowPanel(pnl_Inventory)
        RefreshInventory()
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        ShowPanel(pnl_History)
        If history IsNot Nothing Then history.Reload()
    End Sub

    '=================================================================
    ' MESSAGES
    '=================================================================
    Private Sub btnAdminSend_Click(sender As Object, e As EventArgs) Handles btnAdmin.Click
        If String.IsNullOrWhiteSpace(txtAdminChat.Text) Then Return

        Dim newMessage As New ChatMessage With {
            .Sender = "Admin",
            .Receiver = "Cashier",
            .Message = txtAdminChat.Text.Trim(),
            .TimeSent = DateTime.Now
        }

        txtAdminChat.Clear()
        DataStore.AddMessage(newMessage)      ' saved + MessagesChanged refreshes this screen
    End Sub

    '=================================================================
    ' CASHIERS  (persistent; the right-hand panel is the Add / Edit form)
    '=================================================================
    Private Sub SetupCashierGrid()
        cashierDataGridView.AllowUserToAddRows = False
        cashierDataGridView.ReadOnly = True
        cashierDataGridView.RowHeadersVisible = False
        cashierDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        If Not cashierDataGridView.Columns.Contains("colCashEdit") Then
            Dim editCol As New DataGridViewButtonColumn With {
                .Name = "colCashEdit", .HeaderText = "Actions", .Width = 60,
                .FlatStyle = FlatStyle.Flat
            }
            Dim toggleCol As New DataGridViewButtonColumn With {
                .Name = "colCashToggle", .HeaderText = "", .Width = 80,
                .FlatStyle = FlatStyle.Flat
            }
            cashierDataGridView.Columns.Add(editCol)
            cashierDataGridView.Columns.Add(toggleCol)
        End If

        Column1.Width = 130
        Column2.Width = 110
        Column3.Width = 70
        Column4.Width = 90
    End Sub

    Private Sub displayCashiers()
        RefreshCashierPage()
    End Sub

    Private Sub cashierDataGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles cashierDataGridView.CellContentClick

        If e.RowIndex < 0 Then Return

        Dim id As String = Convert.ToString(cashierDataGridView.Rows(e.RowIndex).Tag)
        Dim acc As CashierAccount = DataStore.FindCashierById(id)
        If acc Is Nothing Then Return

        Dim colName As String = cashierDataGridView.Columns(e.ColumnIndex).Name

        If colName = "colCashEdit" Then
            LoadCashierIntoForm(acc)

        ElseIf colName = "colCashToggle" Then
            Dim newStatus As String = If(acc.IsActive, AccountStatus.Inactive, AccountStatus.Active)
            Dim verb As String = If(acc.IsActive, "deactivate", "activate")

            If MessageBox.Show("Do you want to " & verb & " " & acc.FullName & "?", "Cashier",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return

            Dim err As String = ""
            If Not DataStore.SetCashierStatus(acc.Id, newStatus, err) Then
                MessageBox.Show(err, "Cashier", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ElseIf editingCashierId = acc.Id Then
                LoadCashierIntoForm(acc)
            End If
        End If
    End Sub

    Private Sub LoadCashierIntoForm(acc As CashierAccount)
        editingCashierId = acc.Id
        txtFullName.Text = acc.FullName
        txtUser.Text = acc.Username
        txtPass.Clear()
        txtConfirmPass.Clear()
        txtPass.PlaceholderText = "Leave blank to keep current"
        txtConfirmPass.PlaceholderText = "Leave blank to keep current"
        rbActive.Checked = acc.IsActive
        rbInac.Checked = Not acc.IsActive
    End Sub

    Private Sub ClearCashierForm()
        editingCashierId = ""
        txtFullName.Clear()
        txtUser.Clear()
        txtPass.Clear()
        txtConfirmPass.Clear()
        txtPass.PlaceholderText = ""
        txtConfirmPass.PlaceholderText = ""
        rbActive.Checked = True
        rbInac.Checked = False
    End Sub

    ' "+ Register New Cashier" = start a blank Add form (the right-hand panel)
    Private Sub btnRegisterNewCashier_Click(sender As Object, e As EventArgs) Handles btnRegisterNewCashier.Click
        ClearCashierForm()
        txtFullName.Focus()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ClearCashierForm()
    End Sub

    Private Sub btnSaveUser_Click(sender As Object, e As EventArgs) Handles btnSaveUser.Click

        Dim fullName As String = txtFullName.Text.Trim()
        Dim username As String = txtUser.Text.Trim()
        Dim password As String = txtPass.Text
        Dim confirm As String = txtConfirmPass.Text
        Dim status As String = If(rbActive.Checked, AccountStatus.Active, AccountStatus.Inactive)
        Dim isNew As Boolean = (editingCashierId = "")

        If fullName = "" Then
            MessageBox.Show("Full name is required.", "Cashier", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtFullName.Focus() : Return
        End If
        If username = "" Then
            MessageBox.Show("Username is required.", "Cashier", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtUser.Focus() : Return
        End If
        If isNew AndAlso password = "" Then
            MessageBox.Show("Password is required.", "Cashier", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPass.Focus() : Return
        End If
        If isNew AndAlso confirm = "" Then
            MessageBox.Show("Please confirm the password.", "Cashier", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtConfirmPass.Focus() : Return
        End If
        If (password <> "" OrElse confirm <> "") AndAlso password <> confirm Then
            MessageBox.Show("Passwords do not match.", "Cashier", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtConfirmPass.Focus() : Return
        End If

        Dim err As String = ""
        Dim ok As Boolean
        If isNew Then
            ok = DataStore.AddCashier(fullName, username, password, status, err)
        Else
            ok = DataStore.UpdateCashier(editingCashierId, fullName, username, password, status, err)
        End If

        If ok Then
            MessageBox.Show(If(isNew, "Cashier registered successfully.", "Cashier updated successfully."),
                            "Cashier", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearCashierForm()
            displayCashiers()
        Else
            MessageBox.Show(err, "Cashier", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    '=================================================================
    ' HISTORY  (Daily / Weekly / Monthly + search + status + detail view)
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
        Guna2Panel15.Size = New Size(934, 450)
        Guna2Panel15.FillColor = Color.White
        Guna2Panel15.BorderColor = Color.FromArgb(190, 164, 154)
        Guna2Panel15.BorderThickness = 1
        Guna2Panel15.Controls.Add(datadgridHistory)
        datadgridHistory.Location = New Point(1, 42)
        datadgridHistory.Size = New Size(Guna2Panel15.Width - 2, Guna2Panel15.Height - 43)
        datadgridHistory.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        For Each c As Control In Guna2Panel15.Controls
            If c IsNot Label16 AndAlso c IsNot datadgridHistory Then c.Visible = False
        Next
        Label16.Location = New Point(14, 11)
        Label16.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        Label16.ForeColor = CafeUi.Coffee
        Guna2Panel17.Location = New Point(34, 558)
        Guna2Panel17.Size = New Size(934, 52)
        Guna2Panel17.FillColor = Color.FromArgb(250, 248, 247)

        history = New HistoryView(Me,
                                  pnl_History, New Point(36, 94),
                                  datadgridHistory, Guna2TextBox1,
                                  Guna2ComboBox1, Guna2ComboBox2,
                                  Guna2Panel17, New Point(16, 12),
                                  True)

    End Sub

    '=================================================================
    ' INVENTORY  (screen is created in code; pnl_Inventory only had a label)
    '=================================================================
    Private Sub BuildInventoryUi()

        Label3.Visible = False

        pnl_Inventory.Controls.Add(MakeLabel("Inventory Management", 31, 26, New Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold), brown))
        pnl_Inventory.Controls.Add(MakeLabel("Real-time ingredient usage tracker and stock audit controls.", 33, 60, New Font("Segoe UI", 9.5F), Color.FromArgb(110, 90, 85)))

        invSearch = New Guna2TextBox With {
            .PlaceholderText = "Search stock...",
            .Location = New Point(560, 24),
            .Size = New Size(210, 36),
            .BorderRadius = 6,
            .Font = New Font("Segoe UI", 9.5F)
        }
        invStatus = New Guna2ComboBox With {
            .Location = New Point(782, 24),
            .Size = New Size(186, 36),
            .BorderRadius = 6,
            .DropDownStyle = ComboBoxStyle.DropDownList,
            .Font = New Font("Segoe UI", 9.5F)
        }
        invStatus.Items.AddRange(New Object() {"All Statuses", StockStatus.InStock, StockStatus.LowStock, StockStatus.OutOfStock})
        invStatus.SelectedIndex = 0

        pnl_Inventory.Controls.Add(invSearch)
        pnl_Inventory.Controls.Add(invStatus)
        AddHandler invSearch.TextChanged, AddressOf InventoryFilterChanged
        AddHandler invStatus.SelectedIndexChanged, AddressOf InventoryFilterChanged

        pnl_Inventory.Controls.Add(MakeLabel("Default minimum stock:", 600, 68, New Font("Segoe UI", 9.0F), brown))
        nudMinStock = New NumericUpDown With {
            .Minimum = 1, .Maximum = 9999,
            .Value = DataStore.PosSettings.DefaultMinStock,
            .Location = New Point(745, 64), .Width = 70
        }
        pnl_Inventory.Controls.Add(nudMinStock)
        AddHandler nudMinStock.ValueChanged, AddressOf MinStockChanged

        ' ---- stock table ----
        invGrid = New DataGridView With {
            .Location = New Point(35, 100),
            .Size = New Size(934, 255),
            .AllowUserToAddRows = False,
            .AllowUserToDeleteRows = False,
            .AllowUserToResizeRows = False,
            .ReadOnly = True,
            .RowHeadersVisible = False,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            .MultiSelect = False,
            .BackgroundColor = Color.White,
            .BorderStyle = BorderStyle.FixedSingle,
            .EnableHeadersVisualStyles = False,
            .GridColor = Color.Gainsboro,
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

        Dim picCol As New DataGridViewImageColumn With {.Name = "colInvPic", .HeaderText = "Pic", .Width = 60, .ImageLayout = DataGridViewImageCellLayout.Zoom}
        invGrid.Columns.Add(picCol)
        invGrid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "colInvName", .HeaderText = "Product Name", .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, .FillWeight = 160})
        invGrid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "colInvStock", .HeaderText = "Current Stock", .Width = 120})
        invGrid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "colInvMin", .HeaderText = "Min. Stock", .Width = 110})
        invGrid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "colInvStatus", .HeaderText = "Status", .Width = 130})
        invGrid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "colInvUpdated", .HeaderText = "Last Updated", .Width = 170})
        invGrid.Columns("colInvStock").DefaultCellStyle.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        pnl_Inventory.Controls.Add(invGrid)

        ' ---- restock panel ----
        Dim restockPanel As New Guna2Panel With {
            .Location = New Point(35, 370),
            .Size = New Size(934, 240),
            .BorderRadius = 10,
            .BorderThickness = 1,
            .BorderColor = Color.LightGray,
            .FillColor = Color.FromArgb(250, 248, 245)
        }
        restockPanel.Controls.Add(MakeLabel(ChrW(&H26A0) & " Restock Trigger Needed (Below Safety Thresholds)", 14, 12, New Font("Segoe UI", 10.5F, FontStyle.Bold), Color.FromArgb(198, 40, 40)))
        lblRestockCount = MakeLabel("", 700, 14, New Font("Segoe UI", 9.0F, FontStyle.Bold), brown)
        lblRestockCount.AutoSize = False
        lblRestockCount.Size = New Size(215, 20)
        lblRestockCount.TextAlign = ContentAlignment.MiddleRight
        restockPanel.Controls.Add(lblRestockCount)

        flpRestock = New FlowLayoutPanel With {
            .Location = New Point(12, 42),
            .Size = New Size(910, 188),
            .FlowDirection = FlowDirection.TopDown,
            .WrapContents = False,
            .AutoScroll = True,
            .BackColor = Color.Transparent
        }
        restockPanel.Controls.Add(flpRestock)
        pnl_Inventory.Controls.Add(restockPanel)

        AttachIngredientInventory()          ' MULTI-BRANCH: adds [Products] [Ingredients] to this same page
    End Sub

    '=================================================================
    ' MULTI-BRANCH: [Products] [Ingredients] switch on the EXISTING inventory page.
    ' Products side = everything above, unchanged. Ingredients side = IngredientInventoryView.
    '=================================================================
    Private Sub AttachIngredientInventory()
        Dim titles As New List(Of Control)
        For Each l As Label In pnl_Inventory.Controls.OfType(Of Label)()
            If l.Visible AndAlso l.Top < 66 Then titles.Add(l)
        Next
        Dim view As New IngredientInventoryView(pnl_Inventory.BackColor)
        view.SetBounds(35, 100, 934, 540)
        view.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Dim modeSwitch As New InventoryModeSwitch(pnl_Inventory, view, New Point(325, 24), titles, False)
    End Sub

    Private Function MakeLabel(text As String, x As Integer, y As Integer, f As Font, c As Color) As Label
        Return New Label With {
            .Text = text, .AutoSize = True, .Location = New Point(x, y),
            .Font = f, .ForeColor = c, .BackColor = Color.Transparent
        }
    End Function

    Private Sub InventoryFilterChanged(sender As Object, e As EventArgs)
        RefreshInventory()
    End Sub

    Private Sub MinStockChanged(sender As Object, e As EventArgs)
        If syncingMinStock Then Return
        DataStore.SetDefaultMinStock(CInt(nudMinStock.Value))     ' raises ProductsChanged -> refresh
    End Sub

    Private Sub RefreshInventory()
        If invGrid Is Nothing Then Return

        syncingMinStock = True
        nudMinStock.Value = Math.Max(nudMinStock.Minimum, Math.Min(nudMinStock.Maximum, DataStore.PosSettings.DefaultMinStock))
        syncingMinStock = False

        Dim q As String = invSearch.Text.Trim()
        Dim statusFilter As String = Convert.ToString(invStatus.SelectedItem)

        invGrid.Rows.Clear()
        For Each p As Product In DataStore.Products
            Dim st As String = DataStore.GetProductStockStatus(p)
            If q <> "" AndAlso p.Name.IndexOf(q, StringComparison.OrdinalIgnoreCase) < 0 AndAlso
               p.Category.IndexOf(q, StringComparison.OrdinalIgnoreCase) < 0 Then Continue For
            If statusFilter <> "" AndAlso statusFilter <> "All Statuses" AndAlso st <> statusFilter Then Continue For

            Dim updated As String = If(p.LastUpdated.Date = DateTime.Today,
                                       "Today, " & p.LastUpdated.ToString("hh:mm tt"),
                                       p.LastUpdated.ToString("MMM d, hh:mm tt"))
            Dim idx As Integer = invGrid.Rows.Add(p.Image, p.Name, p.Stock, DataStore.GetMinStock(p), st, updated)
            invGrid.Rows(idx).Cells("colInvStatus").Style.ForeColor = StatusColor(st)
            invGrid.Rows(idx).Cells("colInvStatus").Style.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        Next
        invGrid.ClearSelection()

        ' ---- restock list ----
        flpRestock.SuspendLayout()
        flpRestock.Controls.Clear()
        Dim lowList As List(Of Product) = DataStore.GetLowStockProducts()
        lblRestockCount.Text = lowList.Count & " Items Require Procurement"

        For Each p As Product In lowList
            Dim rowPanel As New Panel With {
                .Size = New Size(888, 44), .BackColor = Color.White,
                .BorderStyle = BorderStyle.FixedSingle, .Margin = New Padding(0, 0, 0, 6)
            }
            Dim suggested As Integer = DataStore.GetSuggestedRestockQty(p)

            rowPanel.Controls.Add(MakeLabel(p.Name, 12, 12, New Font("Segoe UI", 10.0F, FontStyle.Bold), brown))
            rowPanel.Controls.Add(MakeLabel("Current: " & p.Stock, 270, 13, New Font("Segoe UI", 9.5F, FontStyle.Bold), Color.FromArgb(198, 40, 40)))
            rowPanel.Controls.Add(MakeLabel("Required Min: " & DataStore.GetMinStock(p), 390, 13, New Font("Segoe UI", 9.5F), brown))
            rowPanel.Controls.Add(MakeLabel("Restock Order Qty: +" & suggested, 530, 13, New Font("Segoe UI", 9.5F, FontStyle.Bold), Color.FromArgb(46, 125, 50)))

            Dim btn As New Guna2Button With {
                .Text = "Execute Restock",
                .Size = New Size(150, 32),
                .Location = New Point(724, 5),
                .BorderRadius = 4,
                .FillColor = brown,
                .ForeColor = Color.White,
                .Font = New Font("Segoe UI", 9.5F, FontStyle.Bold),
                .Tag = p,
                .Cursor = Cursors.Hand
            }
            AddHandler btn.Click, AddressOf ExecuteRestock_Click
            rowPanel.Controls.Add(btn)

            flpRestock.Controls.Add(rowPanel)
        Next
        flpRestock.ResumeLayout()
    End Sub

    Private Sub ExecuteRestock_Click(sender As Object, e As EventArgs)
        Dim btn As Guna2Button = TryCast(sender, Guna2Button)
        If btn Is Nothing Then Return
        Dim p As Product = TryCast(btn.Tag, Product)
        If p Is Nothing Then Return

        Dim suggested As Integer = DataStore.GetSuggestedRestockQty(p)
        Dim input As String = Microsoft.VisualBasic.Interaction.InputBox(
            "Restock quantity for " & p.Name & ":" & vbCrLf & "(current stock: " & p.Stock & ")",
            "Execute Restock", suggested.ToString())

        If input.Trim() = "" Then Return      ' cancelled

        Dim qty As Integer
        If Not Integer.TryParse(input.Trim(), qty) OrElse qty <= 0 Then
            MessageBox.Show("Enter a whole number greater than zero.", "Restock", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If DataStore.RestockProduct(p, qty) Then
            MessageBox.Show(p.Name & " restocked: +" & qty & " (now " & p.Stock & ").",
                            "Restock", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
End Class