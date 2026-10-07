Option Strict On
Option Explicit On

Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms

'=====================================================================
' SUPER ADMIN DASHBOARD  (new screen - Super Admin had no screen before)
' Built in code with the same colours as Admin / Cashier.
' Shops are DATA: the cards are created in a loop from DataStore.AllShops,
' so "Coffee Shop 4" appears automatically after [+ Add Shop].
'=====================================================================
Public Class SuperAdminForm
    Inherits Form

    ' same palette as the existing screens
    Private ReadOnly brown As Color = Color.FromArgb(62, 39, 35)
    Private ReadOnly tan As Color = Color.FromArgb(190, 164, 154)
    Private ReadOnly cream As Color = Color.FromArgb(250, 248, 245)
    Private ReadOnly softBrown As Color = Color.FromArgb(240, 233, 230)
    Private ReadOnly border As Color = Color.FromArgb(216, 203, 199)
    Private ReadOnly good As Color = Color.FromArgb(46, 125, 50)
    Private ReadOnly warn As Color = Color.FromArgb(239, 108, 0)
    Private ReadOnly bad As Color = Color.FromArgb(198, 40, 40)

    Private selectedShopId As String = ""
    Private closingForLogout As Boolean = False

    Private ReadOnly kpi As New Dictionary(Of String, Label)
    Private flpShops As FlowLayoutPanel
    Private pnlDetail As Panel
    Private lblWelcome As Label
    Private lblTitle As Label, lblSub As Label, lblManager As Label, lblCashiers As Label
    Private lblToday As Label, lblLow As Label, lblStatus As Label, lblHint As Label
    Private flpActions As FlowLayoutPanel
    Private btnAddManager As Button

    Public Sub New()
        Text = "BrewPoint POS - Super Admin"
        Font = New Font("Segoe UI", 10.0F)
        BackColor = cream
        StartPosition = FormStartPosition.CenterScreen
        Size = New Size(1200, 760)
        MinimumSize = New Size(1000, 680)

        BuildUi()

        ShopContext.SelectShop("")        ' Super Admin starts with "all shops"
        AddHandler DataStore.ShopsChanged, AddressOf OnDataChanged
        AddHandler DataStore.TransactionsChanged, AddressOf OnDataChanged
        AddHandler DataStore.IngredientsChanged, AddressOf OnDataChanged
        AddHandler DataStore.CashiersChanged, AddressOf OnDataChanged
        AddHandler DataStore.ProductsChanged, AddressOf OnDataChanged
        RefreshAll()
    End Sub

    '==================================================================
    ' UI
    '==================================================================
    Private Sub BuildUi()
        ' ---- header ----
        Dim header As New Panel With {.Dock = DockStyle.Top, .Height = 70, .BackColor = brown}
        Dim title As New Label With {.Text = "Super Admin Dashboard", .ForeColor = Color.White, .AutoSize = True,
                                     .Font = New Font("Segoe UI", 16.0F, FontStyle.Bold), .Left = 24, .Top = 10}
        lblWelcome = New Label With {.ForeColor = tan, .AutoSize = True, .Left = 26, .Top = 42,
                                     .Text = "Welcome, " & CurrentSession.FullName}
        Dim btnLogout As New Button With {.Text = "Logout", .Width = 100, .Height = 36, .FlatStyle = FlatStyle.Flat,
                                          .BackColor = Color.White, .ForeColor = brown, .Anchor = AnchorStyles.Top Or AnchorStyles.Right}
        btnLogout.FlatAppearance.BorderSize = 0
        btnLogout.Top = 17
        AddHandler header.Resize, Sub(o As Object, e As EventArgs) btnLogout.Left = header.Width - btnLogout.Width - 24
        AddHandler btnLogout.Click, AddressOf btnLogout_Click
        header.Controls.AddRange(New Control() {title, lblWelcome, btnLogout})

        ' ---- KPI row ----
        Dim pnlKpi As New FlowLayoutPanel With {.Dock = DockStyle.Top, .Height = 104, .Padding = New Padding(18, 12, 0, 0), .WrapContents = False}
        For Each name As String In New String() {"Total Shops", "Total Sales", "Total Orders", "Total Managers", "Total Cashiers"}
            Dim card As New Panel With {.Width = 214, .Height = 80, .BackColor = Color.White, .Margin = New Padding(6, 0, 6, 0)}
            Dim cap As New Label With {.Text = name, .ForeColor = Color.Gray, .AutoSize = True, .Left = 14, .Top = 10}
            Dim val As New Label With {.Text = "0", .ForeColor = brown, .AutoSize = True, .Left = 14, .Top = 34,
                                       .Font = New Font("Segoe UI", 17.0F, FontStyle.Bold)}
            card.Controls.AddRange(New Control() {cap, val})
            kpi(name) = val
            pnlKpi.Controls.Add(card)
        Next

        ' ---- shop cards ----
        Dim lblShops As New Label With {.Text = "Shops / Branches", .Font = New Font("Segoe UI", 12.0F, FontStyle.Bold),
                                        .ForeColor = brown, .Dock = DockStyle.Top, .Height = 34, .Padding = New Padding(24, 8, 0, 0)}
        flpShops = New FlowLayoutPanel With {.Dock = DockStyle.Top, .Height = 150, .WrapContents = False, .AutoScroll = True,
                                             .Padding = New Padding(18, 4, 0, 0)}

        ' ---- detail ----
        pnlDetail = New Panel With {.Dock = DockStyle.Fill, .BackColor = Color.White, .Padding = New Padding(24, 14, 24, 14)}
        lblTitle = New Label With {.AutoSize = True, .Left = 24, .Top = 14, .Font = New Font("Segoe UI", 15.0F, FontStyle.Bold), .ForeColor = brown}
        lblSub = New Label With {.AutoSize = True, .Left = 26, .Top = 50, .ForeColor = Color.Gray}
        lblManager = New Label With {.AutoSize = True, .Left = 26, .Top = 84}
        btnAddManager = MakeButton("Add Manager", AddressOf btnAddManager_Click)
        btnAddManager.Left = 420 : btnAddManager.Top = 78 : btnAddManager.Width = 130 : btnAddManager.Height = 30
        lblCashiers = New Label With {.AutoSize = True, .Left = 26, .Top = 118}
        lblToday = New Label With {.AutoSize = True, .Left = 26, .Top = 152, .Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)}
        lblLow = New Label With {.AutoSize = True, .Left = 26, .Top = 186, .MaximumSize = New Size(900, 0)}
        lblStatus = New Label With {.AutoSize = True, .Left = 26, .Top = 222, .Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)}
        lblHint = New Label With {.AutoSize = True, .Left = 26, .Top = 20, .ForeColor = Color.Gray, .Text = "Select a shop card to see its manager, cashiers, sales and low-stock items."}
        flpActions = New FlowLayoutPanel With {.Left = 20, .Top = 262, .Width = 1000, .Height = 50, .WrapContents = True}
        flpActions.Controls.Add(MakeButton("Edit Shop", AddressOf btnEditShop_Click))
        flpActions.Controls.Add(MakeButton("Add Cashier", AddressOf btnAddCashier_Click))
        flpActions.Controls.Add(MakeButton("Transactions", AddressOf btnTransactions_Click))
        flpActions.Controls.Add(MakeButton("Inventory", AddressOf btnInventory_Click))
        flpActions.Controls.Add(MakeButton("Low Stock", AddressOf btnLowStock_Click))
        pnlDetail.Controls.AddRange(New Control() {lblTitle, lblSub, lblManager, btnAddManager, lblCashiers, lblToday, lblLow, lblStatus, lblHint, flpActions})

        ' dock order: last added = docked first
        Controls.Add(pnlDetail)
        Controls.Add(flpShops)
        Controls.Add(lblShops)
        Controls.Add(pnlKpi)
        Controls.Add(header)
    End Sub

    Private Function MakeButton(caption As String, handler As EventHandler) As Button
        Dim b As New Button With {.Text = caption, .Width = 140, .Height = 38, .FlatStyle = FlatStyle.Flat,
                                  .BackColor = brown, .ForeColor = Color.White, .Margin = New Padding(4)}
        b.FlatAppearance.BorderSize = 0
        AddHandler b.Click, handler
        Return b
    End Function

    '==================================================================
    ' REFRESH
    '==================================================================
    Private Sub OnDataChanged(sender As Object, e As EventArgs)
        If IsDisposed Then Return
        RefreshAll()
    End Sub

    Private Sub RefreshAll()
        Dim totalSales As Decimal = 0D, totalOrders As Integer = 0
        For Each s As Shop In DataStore.AllShops
            totalSales += DataStore.ShopSales(s.ShopId)
            totalOrders += DataStore.ShopOrders(s.ShopId)
        Next
        kpi("Total Shops").Text = DataStore.AllShops.Count.ToString()
        kpi("Total Sales").Text = UiHelpers.Peso(totalSales)
        kpi("Total Orders").Text = totalOrders.ToString()
        kpi("Total Managers").Text = DataStore.ManagerCount().ToString()
        kpi("Total Cashiers").Text = DataStore.AllCashiers.Count.ToString()
        BuildShopCards()
        RefreshDetail()
    End Sub

    Private Sub BuildShopCards()
        flpShops.SuspendLayout()
        For i As Integer = flpShops.Controls.Count - 1 To 0 Step -1
            flpShops.Controls(i).Dispose()
        Next
        flpShops.Controls.Clear()

        For Each s As Shop In DataStore.GetShops()
            Dim sid As String = s.ShopId
            Dim isSel As Boolean = String.Equals(sid, selectedShopId, StringComparison.OrdinalIgnoreCase)
            Dim card As New Panel With {.Width = 220, .Height = 120, .Margin = New Padding(6), .Cursor = Cursors.Hand,
                                        .BackColor = If(isSel, softBrown, Color.White), .BorderStyle = BorderStyle.FixedSingle}
            Dim nm As New Label With {.Text = s.ShopName, .AutoSize = False, .Left = 12, .Top = 10, .Width = 196, .Height = 26,
                                      .Font = New Font("Segoe UI", 11.0F, FontStyle.Bold), .ForeColor = brown}
            Dim code As New Label With {.Text = s.BranchCode & "  |  " & s.Status, .AutoSize = False, .Left = 12, .Top = 38, .Width = 196, .Height = 20,
                                        .ForeColor = If(s.IsActive, good, bad)}
            Dim sales As New Label With {.Text = "Today: " & UiHelpers.Peso(DataStore.ShopSales(sid, True)), .AutoSize = False, .Left = 12, .Top = 62, .Width = 196, .Height = 20}
            Dim low As Integer = DataStore.GetShopLowIngredients(sid).Count
            Dim lowLbl As New Label With {.Text = If(low = 0, "Stock OK", low.ToString() & " low-stock item(s)"), .AutoSize = False, .Left = 12, .Top = 86, .Width = 196, .Height = 20,
                                          .ForeColor = If(low = 0, Color.Gray, warn)}
            card.Controls.AddRange(New Control() {nm, code, sales, lowLbl})
            WireClick(card, Sub() SelectShopCard(sid))
            flpShops.Controls.Add(card)
        Next

        Dim addCard As New Panel With {.Width = 220, .Height = 120, .Margin = New Padding(6), .Cursor = Cursors.Hand,
                                       .BackColor = cream, .BorderStyle = BorderStyle.FixedSingle}
        Dim plus As New Label With {.Text = "+ Add Shop", .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleCenter,
                                    .Font = New Font("Segoe UI", 12.0F, FontStyle.Bold), .ForeColor = brown}
        addCard.Controls.Add(plus)
        WireClick(addCard, Sub() AddShop())
        flpShops.Controls.Add(addCard)
        flpShops.ResumeLayout()
    End Sub

    Private Sub WireClick(host As Control, action As Action)
        ' BeginInvoke: the cards are rebuilt by the action, so never dispose the clicked control inside its own click event
        AddHandler host.Click, Sub(o As Object, e As EventArgs) BeginInvoke(action)
        For Each c As Control In host.Controls
            AddHandler c.Click, Sub(o As Object, e As EventArgs) BeginInvoke(action)
        Next
    End Sub

    Private Sub SelectShopCard(shopId As String)
        selectedShopId = shopId
        ShopContext.SelectShop(shopId)         ' from now on DataStore.Products/Transactions/... = this shop
        BuildShopCards()
        RefreshDetail()
    End Sub

    Private Sub RefreshDetail()
        Dim s As Shop = DataStore.FindShop(selectedShopId)
        Dim has As Boolean = (s IsNot Nothing)
        For Each c As Control In New Control() {lblTitle, lblSub, lblManager, btnAddManager, lblCashiers, lblToday, lblLow, lblStatus, flpActions}
            c.Visible = has
        Next
        lblHint.Visible = Not has
        If Not has Then Return

        lblTitle.Text = s.ShopName
        lblSub.Text = s.BranchCode & If(s.Address <> "", "  |  " & s.Address, "")
        Dim m As AdminAccount = DataStore.GetShopManager(s.ShopId)
        lblManager.Text = "Manager:  " & If(m Is Nothing, "none yet", m.FullName & "  (" & m.Username & ")")
        btnAddManager.Visible = (m Is Nothing)

        Dim names As New List(Of String)
        For Each c As CashierAccount In DataStore.Cashiers      ' already filtered to this shop
            names.Add(c.FullName)
        Next
        lblCashiers.Text = "Cashiers (" & names.Count.ToString() & "):  " & If(names.Count = 0, "none yet", String.Join(", ", names.ToArray()))

        lblToday.Text = "Today's Sales:  " & UiHelpers.Peso(DataStore.ShopSales(s.ShopId, True)) & "     Orders today:  " & DataStore.ShopOrders(s.ShopId, True).ToString() &
                        "     All-time Sales:  " & UiHelpers.Peso(DataStore.ShopSales(s.ShopId))

        Dim low As List(Of Ingredient) = DataStore.GetShopLowIngredients(s.ShopId)
        If low.Count = 0 Then
            lblLow.Text = "Low Stock Items:  none"
            lblLow.ForeColor = Color.Gray
        Else
            Dim parts As New List(Of String)
            For Each i As Ingredient In low
                parts.Add(i.Name & " (" & i.StockText & " / min " & i.MinimumText & If(i.Status = IngredientStatus.OutOfStock, ", OUT", "") & ")")
                If parts.Count >= 6 Then Exit For
            Next
            lblLow.Text = "Low Stock Items (" & low.Count.ToString() & "):  " & String.Join(", ", parts.ToArray()) & If(low.Count > 6, " ...", "")
            lblLow.ForeColor = warn
        End If

        lblStatus.Text = "Status:  " & s.Status
        lblStatus.ForeColor = If(s.IsActive, good, bad)
    End Sub

    '==================================================================
    ' ACTIONS
    '==================================================================
    Private Sub AddShop()
        Dim v As String() = Ask("Add Shop", New String() {"Shop name", "Branch code (leave empty = automatic)", "Address"}, Nothing, -1)
        If v Is Nothing Then Return
        Dim err As String = ""
        Dim s As Shop = DataStore.AddShop(v(0), v(1), v(2), err)
        If s Is Nothing Then
            MessageBox.Show(err, "Add Shop", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        SelectShopCard(s.ShopId)
    End Sub

    Private Sub btnEditShop_Click(sender As Object, e As EventArgs)
        Dim s As Shop = DataStore.FindShop(selectedShopId)
        If s Is Nothing Then Return
        Dim v As String() = Ask("Edit Shop", New String() {"Shop name", "Branch code", "Address", "Status (Active / Inactive)"},
                                New String() {s.ShopName, s.BranchCode, s.Address, s.Status}, -1)
        If v Is Nothing Then Return
        Dim status As String = If(String.Equals(v(3).Trim(), ShopStatus.Inactive, StringComparison.OrdinalIgnoreCase), ShopStatus.Inactive, ShopStatus.Active)
        Dim err As String = ""
        If Not DataStore.UpdateShop(s.ShopId, v(0), v(1), v(2), status, err) Then
            MessageBox.Show(err, "Edit Shop", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnAddManager_Click(sender As Object, e As EventArgs)
        If selectedShopId = "" Then Return
        Dim v As String() = Ask("Add Manager", New String() {"Full name", "Username", "Password"}, Nothing, 2)
        If v Is Nothing Then Return
        Dim err As String = ""
        If Not DataStore.AddManager(selectedShopId, v(0), v(1), v(2), err) Then
            MessageBox.Show(err, "Add Manager", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnAddCashier_Click(sender As Object, e As EventArgs)
        If selectedShopId = "" Then Return
        Dim v As String() = Ask("Add Cashier", New String() {"Full name", "Username", "Password"}, Nothing, 2)
        If v Is Nothing Then Return
        Dim err As String = ""
        If Not DataStore.AddCashier(v(0), v(1), v(2), AccountStatus.Active, err, selectedShopId) Then
            MessageBox.Show(err, "Add Cashier", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnTransactions_Click(sender As Object, e As EventArgs)
        Dim dt As New DataTable()
        For Each col As String In New String() {"Transaction", "Date", "Cashier", "Items", "Total", "Payment", "Status"}
            dt.Columns.Add(col)
        Next
        Dim list As New List(Of POS_Transaction)(DataStore.Transactions)     ' only the selected shop
        list.Sort(Function(a, b) b.TransactionDate.CompareTo(a.TransactionDate))
        For Each t As POS_Transaction In list
            dt.Rows.Add(t.TransactionID, t.TransactionDate.ToString("MMM d, yyyy hh:mm tt"), t.Cashier, DataStore.BuildItemsText(t),
                        UiHelpers.Peso(t.Total), t.PaymentMethod, t.Status)
        Next
        Dim tabs As New Dictionary(Of String, DataTable)
        tabs("Transactions - " & DataStore.GetShopName(selectedShopId)) = dt
        ShowGrids("Transactions", tabs)
    End Sub

    Private Sub btnInventory_Click(sender As Object, e As EventArgs)
        Dim ing As New DataTable()
        For Each col As String In New String() {"Ingredient", "Current Stock", "Minimum", "Status"}
            ing.Columns.Add(col)
        Next
        For Each i As Ingredient In DataStore.Ingredients
            ing.Rows.Add(i.Name, i.StockText, i.MinimumText, i.Status)
        Next
        Dim prod As New DataTable()
        For Each col As String In New String() {"Product", "Category", "Price", "Servings / Stock", "Status"}
            prod.Columns.Add(col)
        Next
        For Each p As Product In DataStore.Products
            prod.Rows.Add(p.Name, p.Category, UiHelpers.Peso(p.Price), p.Stock.ToString(), DataStore.GetProductStockStatus(p))
        Next
        Dim hist As New DataTable()
        For Each col As String In New String() {"When", "Ingredient", "Change", "Reason", "By", "Reference / Note"}
            hist.Columns.Add(col)
        Next
        Dim n As Integer = 0
        For Each a As InventoryAdjustment In DataStore.InventoryHistory
            hist.Rows.Add(a.RecordedAt.ToString("MMM d, hh:mm tt"), a.IngredientName,
                          If(a.Delta > 0D, "+", "-") & UnitHelper.Display(Math.Abs(a.Delta), a.Unit), a.Reason, a.RecordedBy,
                          If(a.ReferenceId <> "", a.ReferenceId, a.Note))
            n += 1
            If n >= 300 Then Exit For
        Next
        Dim tabs As New Dictionary(Of String, DataTable)
        tabs("Ingredients") = ing
        tabs("Products") = prod
        tabs("History") = hist
        ShowGrids("Inventory - " & DataStore.GetShopName(selectedShopId), tabs)
    End Sub

    Private Sub btnLowStock_Click(sender As Object, e As EventArgs)
        Dim dt As New DataTable()
        For Each col As String In New String() {"Ingredient", "Current Stock", "Minimum", "Status"}
            dt.Columns.Add(col)
        Next
        For Each i As Ingredient In DataStore.GetShopLowIngredients(selectedShopId)
            dt.Rows.Add(i.Name, i.StockText, i.MinimumText, i.Status)
        Next
        Dim tabs As New Dictionary(Of String, DataTable)
        tabs("Low stock ingredients") = dt
        ShowGrids("Low Stock - " & DataStore.GetShopName(selectedShopId), tabs)
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs)
        If MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            closingForLogout = True
            AppNavigation.ShowLogin()
            Close()
        End If
    End Sub

    Private Sub SuperAdminForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        RemoveHandler DataStore.ShopsChanged, AddressOf OnDataChanged
        RemoveHandler DataStore.TransactionsChanged, AddressOf OnDataChanged
        RemoveHandler DataStore.IngredientsChanged, AddressOf OnDataChanged
        RemoveHandler DataStore.CashiersChanged, AddressOf OnDataChanged
        RemoveHandler DataStore.ProductsChanged, AddressOf OnDataChanged
        If Not closingForLogout Then Application.Exit()
    End Sub

    '==================================================================
    ' SMALL DIALOG HELPERS
    '==================================================================
    ''' <summary>Simple text-box dialog. Returns Nothing when cancelled. passwordIndex = -1 for none.</summary>
    Private Function Ask(title As String, labels As String(), defaults As String(), passwordIndex As Integer) As String()
        Using dlg As New Form()
            dlg.Text = title
            dlg.Font = New Font("Segoe UI", 10.0F)
            dlg.FormBorderStyle = FormBorderStyle.FixedDialog
            dlg.StartPosition = FormStartPosition.CenterParent
            dlg.MaximizeBox = False
            dlg.MinimizeBox = False
            dlg.ClientSize = New Size(390, 24 + labels.Length * 64 + 64)
            Dim boxes(labels.Length - 1) As TextBox
            For i As Integer = 0 To labels.Length - 1
                Dim lbl As New Label With {.Text = labels(i), .Left = 20, .Top = 14 + i * 64, .AutoSize = True}
                boxes(i) = New TextBox With {.Left = 20, .Top = 38 + i * 64, .Width = 350,
                                             .Text = If(defaults IsNot Nothing AndAlso i < defaults.Length, defaults(i), "")}
                If i = passwordIndex Then boxes(i).UseSystemPasswordChar = True
                dlg.Controls.Add(lbl)
                dlg.Controls.Add(boxes(i))
            Next
            Dim y As Integer = 14 + labels.Length * 64
            Dim ok As New Button With {.Text = "Save", .Left = 190, .Top = y, .Width = 85, .Height = 34, .DialogResult = DialogResult.OK,
                                       .FlatStyle = FlatStyle.Flat, .BackColor = brown, .ForeColor = Color.White}
            Dim cancel As New Button With {.Text = "Cancel", .Left = 285, .Top = y, .Width = 85, .Height = 34, .DialogResult = DialogResult.Cancel}
            dlg.Controls.Add(ok)
            dlg.Controls.Add(cancel)
            dlg.AcceptButton = ok
            dlg.CancelButton = cancel
            If dlg.ShowDialog(Me) <> DialogResult.OK Then Return Nothing
            Dim result(labels.Length - 1) As String
            For i As Integer = 0 To labels.Length - 1
                result(i) = boxes(i).Text
            Next
            Return result
        End Using
    End Function

    Private Sub ShowGrids(title As String, tables As Dictionary(Of String, DataTable))
        Using dlg As New Form()
            dlg.Text = title
            dlg.Font = New Font("Segoe UI", 10.0F)
            dlg.StartPosition = FormStartPosition.CenterParent
            dlg.Size = New Size(960, 560)
            Dim tabs As New TabControl With {.Dock = DockStyle.Fill}
            For Each kv As KeyValuePair(Of String, DataTable) In tables
                Dim page As New TabPage(kv.Key)
                Dim grid As New DataGridView With {.Dock = DockStyle.Fill, .ReadOnly = True, .AllowUserToAddRows = False,
                    .AllowUserToDeleteRows = False, .RowHeadersVisible = False, .BackgroundColor = Color.White,
                    .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill}
                grid.DataSource = kv.Value
                page.Controls.Add(grid)
                tabs.TabPages.Add(page)
            Next
            dlg.Controls.Add(tabs)
            dlg.ShowDialog(Me)
        End Using
    End Sub

End Class
