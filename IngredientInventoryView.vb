Option Strict On
Option Explicit On

Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports Guna.UI2.WinForms

'=====================================================================
' INGREDIENT INVENTORY VIEW  (PHASE 4/5)
' One control used by BOTH the Manager (Admin form) and the Cashier form.
' What each role sees is decided by ShopContext.Can(...), not by the form:
'   Manager : add / edit / delete ingredient, restock or count, report loss, recipes, history
'   Cashier : view, search, low stock, report wastage / damage / lost, history
' Everything it shows is already limited to the current shop by DataStore.
'=====================================================================
Public Class IngredientInventoryView
    Inherits Panel

    Private ReadOnly brown As Color = Color.FromArgb(62, 39, 35)
    Private ReadOnly good As Color = Color.FromArgb(46, 125, 50)
    Private ReadOnly warn As Color = Color.FromArgb(239, 108, 0)
    Private ReadOnly bad As Color = Color.FromArgb(198, 40, 40)

    Private txtSearch As Guna2TextBox
    Private cboStatus As Guna2ComboBox
    Private lblSummary As Label
    Private flpButtons As FlowLayoutPanel
    Private grid As DataGridView
    Private busy As Boolean = False

    Public Sub New(pageBackColor As Color)
        BackColor = pageBackColor
        DoubleBuffered = True

        txtSearch = New Guna2TextBox With {.PlaceholderText = "Search ingredients...", .Location = New Point(0, 0), .Size = New Size(280, 36),
                                           .BorderRadius = 6, .Font = New Font("Segoe UI", 9.5F)}
        cboStatus = New Guna2ComboBox With {.Location = New Point(292, 0), .Size = New Size(170, 36), .BorderRadius = 6,
                                            .DropDownStyle = ComboBoxStyle.DropDownList, .Font = New Font("Segoe UI", 9.5F)}
        cboStatus.Items.AddRange(New Object() {"All statuses", IngredientStatus.InStock, IngredientStatus.LowStock, IngredientStatus.OutOfStock})
        cboStatus.SelectedIndex = 0
        lblSummary = New Label With {.AutoSize = True, .Location = New Point(480, 9), .ForeColor = brown, .Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)}

        flpButtons = New FlowLayoutPanel With {.Location = New Point(0, 44), .Height = 46, .WrapContents = False, .BackColor = Color.Transparent}
        If ShopContext.Can(Permissions.ManageIngredients) Then
            flpButtons.Controls.Add(MakeButton("Add Ingredient", 130, AddressOf AddIngredient_Click))
            flpButtons.Controls.Add(MakeButton("Edit", 70, AddressOf EditIngredient_Click))
            flpButtons.Controls.Add(MakeButton("Delete", 74, AddressOf DeleteIngredient_Click))
        End If
        If ShopContext.Can(Permissions.AdjustStock) Then flpButtons.Controls.Add(MakeButton("Restock / Count", 130, AddressOf Restock_Click))
        If ShopContext.Can(Permissions.ReportLoss) Then flpButtons.Controls.Add(MakeButton("Report Wastage / Damage / Lost", 232, AddressOf ReportLoss_Click))
        If ShopContext.Can(Permissions.ManageRecipes) Then flpButtons.Controls.Add(MakeButton("Recipes", 90, AddressOf Recipes_Click))
        flpButtons.Controls.Add(MakeButton("History", 84, AddressOf History_Click))

        grid = New DataGridView With {
            .Location = New Point(0, 98), .AllowUserToAddRows = False, .AllowUserToDeleteRows = False, .AllowUserToResizeRows = False,
            .ReadOnly = True, .RowHeadersVisible = False, .SelectionMode = DataGridViewSelectionMode.FullRowSelect, .MultiSelect = False,
            .BackgroundColor = Color.White, .BorderStyle = BorderStyle.FixedSingle, .EnableHeadersVisualStyles = False,
            .GridColor = Color.Gainsboro, .RowTemplate = New DataGridViewRow With {.Height = 44}, .ColumnHeadersHeight = 36,
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing}
        grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(216, 203, 199)
        grid.ColumnHeadersDefaultCellStyle.ForeColor = brown
        grid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(216, 203, 199)
        grid.DefaultCellStyle.Font = New Font("Segoe UI", 10.0F)
        grid.DefaultCellStyle.ForeColor = brown
        grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 233, 230)
        grid.DefaultCellStyle.SelectionForeColor = brown
        grid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "colIngName", .HeaderText = "Ingredient", .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, .FillWeight = 160})
        grid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "colIngStock", .HeaderText = "Current Stock", .Width = 140})
        grid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "colIngMin", .HeaderText = "Minimum", .Width = 120})
        grid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "colIngStatus", .HeaderText = "Status", .Width = 130})
        grid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "colIngUpdated", .HeaderText = "Last Updated", .Width = 170})
        grid.Columns("colIngStock").DefaultCellStyle.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)

        Controls.AddRange(New Control() {txtSearch, cboStatus, lblSummary, flpButtons, grid})
        AddHandler txtSearch.TextChanged, AddressOf FilterChanged
        AddHandler cboStatus.SelectedIndexChanged, AddressOf FilterChanged
        AddHandler DataStore.IngredientsChanged, AddressOf OnIngredientsChanged
        LayoutChildren()
        RefreshData()
    End Sub

    Private Function MakeButton(caption As String, width As Integer, handler As EventHandler) As Guna2Button
        Dim b As New Guna2Button With {.Text = caption, .Size = New Size(width, 38), .BorderRadius = 6, .FillColor = brown,
                                       .ForeColor = Color.White, .Font = New Font("Segoe UI", 9.5F, FontStyle.Bold),
                                       .Cursor = Cursors.Hand, .Margin = New Padding(0, 0, 8, 0)}
        AddHandler b.Click, handler
        Return b
    End Function

    Protected Overrides Sub OnResize(eventargs As EventArgs)
        MyBase.OnResize(eventargs)
        LayoutChildren()
    End Sub

    Private Sub LayoutChildren()
        If grid Is Nothing Then Return
        flpButtons.Width = Width
        grid.SetBounds(0, 98, Width, Math.Max(100, Height - 98))
    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then RemoveHandler DataStore.IngredientsChanged, AddressOf OnIngredientsChanged
        MyBase.Dispose(disposing)
    End Sub

    '==================================================================
    ' DATA
    '==================================================================
    Private Sub OnIngredientsChanged(sender As Object, e As EventArgs)
        If IsDisposed Then Return
        RefreshData()
    End Sub

    Private Sub FilterChanged(sender As Object, e As EventArgs)
        If Not busy Then RefreshData()
    End Sub

    Public Sub RefreshData()
        If grid Is Nothing Then Return
        Dim keepId As String = ""
        Dim sel As Ingredient = SelectedIngredient()
        If sel IsNot Nothing Then keepId = sel.IngredientId

        Dim q As String = txtSearch.Text.Trim()
        Dim statusFilter As String = If(cboStatus.SelectedIndex > 0, cboStatus.Text, "")

        Dim list As New List(Of Ingredient)(DataStore.Ingredients)       ' current shop only
        list.Sort(Function(a, b) String.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase))

        grid.Rows.Clear()
        Dim low As Integer = 0, out As Integer = 0
        For Each i As Ingredient In list
            If i.Status = IngredientStatus.LowStock Then low += 1
            If i.Status = IngredientStatus.OutOfStock Then out += 1
            If q <> "" AndAlso i.Name.IndexOf(q, StringComparison.OrdinalIgnoreCase) < 0 Then Continue For
            If statusFilter <> "" AndAlso i.Status <> statusFilter Then Continue For

            Dim updated As String = If(i.LastUpdated.Date = DateTime.Today, "Today, " & i.LastUpdated.ToString("hh:mm tt"), i.LastUpdated.ToString("MMM d, hh:mm tt"))
            Dim idx As Integer = grid.Rows.Add(i.Name, i.StockText, i.MinimumText, i.Status, updated)
            grid.Rows(idx).Tag = i
            Dim c As DataGridViewCell = grid.Rows(idx).Cells("colIngStatus")
            c.Style.ForeColor = If(i.Status = IngredientStatus.InStock, good, If(i.Status = IngredientStatus.LowStock, warn, bad))
            c.Style.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
            If keepId <> "" AndAlso i.IngredientId = keepId Then grid.Rows(idx).Selected = True
        Next
        lblSummary.Text = list.Count.ToString() & " ingredients   |   " & low.ToString() & " low   |   " & out.ToString() & " out of stock"
        lblSummary.ForeColor = If(low + out > 0, warn, brown)
    End Sub

    Private Function SelectedIngredient() As Ingredient
        If grid Is Nothing OrElse grid.SelectedRows.Count = 0 Then Return Nothing
        Return TryCast(grid.SelectedRows(0).Tag, Ingredient)
    End Function

    Private Function NeedSelection() As Ingredient
        Dim ing As Ingredient = SelectedIngredient()
        If ing Is Nothing Then MessageBox.Show("Select an ingredient in the table first.", "Ingredients", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Return ing
    End Function

    '==================================================================
    ' ACTIONS
    '==================================================================
    Private Sub AddIngredient_Click(sender As Object, e As EventArgs)
        Dim v As String() = UiDialogs.Ask(FindForm(), "Add Ingredient", New DialogField() {
            New DialogField("Ingredient name"),
            New DialogField("Unit", DialogFieldKind.Combo, "g", New String() {"g", "kg", "ml", "L", "pcs"}),
            New DialogField("Opening stock (in the unit above)", DialogFieldKind.Text, "0"),
            New DialogField("Minimum stock (in the unit above)", DialogFieldKind.Text, "0")})
        If v Is Nothing Then Return
        Dim opening, minimum As Decimal
        If Not UiDialogs.TryDecimal(v(2), opening) OrElse Not UiDialogs.TryDecimal(v(3), minimum) Then
            MessageBox.Show("Enter valid numbers for the stock quantities.", "Add Ingredient", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim err As String = ""
        If DataStore.AddIngredient(v(0), v(1), opening, minimum, err) Is Nothing Then
            MessageBox.Show(err, "Add Ingredient", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub EditIngredient_Click(sender As Object, e As EventArgs)
        Dim ing As Ingredient = NeedSelection()
        If ing Is Nothing Then Return
        Dim units As String() = UiDialogs.UnitChoices(ing.Unit)
        Dim v As String() = UiDialogs.Ask(FindForm(), "Edit Ingredient", New DialogField() {
            New DialogField("Ingredient name", DialogFieldKind.Text, ing.Name),
            New DialogField("Minimum stock", DialogFieldKind.Text, (ing.MinimumStock).ToString("0.##")),
            New DialogField("Unit of the minimum", DialogFieldKind.Combo, ing.Unit, units)})
        If v Is Nothing Then Return
        Dim minimum As Decimal
        If Not UiDialogs.TryDecimal(v(1), minimum) Then
            MessageBox.Show("Enter a valid minimum stock.", "Edit Ingredient", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim err As String = ""
        If Not DataStore.UpdateIngredient(ing, v(0), UnitHelper.ToBase(minimum, v(2)), err) Then
            MessageBox.Show(err, "Edit Ingredient", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub DeleteIngredient_Click(sender As Object, e As EventArgs)
        Dim ing As Ingredient = NeedSelection()
        If ing Is Nothing Then Return
        If MessageBox.Show("Delete " & ing.Name & "?", "Delete Ingredient", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
        Dim err As String = ""
        If Not DataStore.DeleteIngredient(ing, err) Then MessageBox.Show(err, "Delete Ingredient", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    ''' <summary>Manager: add delivered stock, or enter the counted stock to correct it.</summary>
    Private Sub Restock_Click(sender As Object, e As EventArgs)
        Dim ing As Ingredient = NeedSelection()
        If ing Is Nothing Then Return
        Dim v As String() = UiDialogs.Ask(FindForm(), "Restock / Count - " & ing.Name, New DialogField() {
            New DialogField("Type", DialogFieldKind.Combo, "Restock (add stock)", New String() {"Restock (add stock)", "Count (set to counted stock)"}),
            New DialogField("Quantity (current: " & ing.StockText & ")"),
            New DialogField("Unit", DialogFieldKind.Combo, ing.Unit, UiDialogs.UnitChoices(ing.Unit)),
            New DialogField("Note", DialogFieldKind.Text, "")})
        If v Is Nothing Then Return
        Dim qty As Decimal
        If Not UiDialogs.TryDecimal(v(1), qty) OrElse qty < 0D Then
            MessageBox.Show("Enter a valid quantity.", "Restock / Count", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim baseQty As Decimal = UnitHelper.ToBase(qty, v(2))
        Dim err As String = ""
        Dim ok As Boolean
        If v(0).StartsWith("Restock", StringComparison.Ordinal) Then
            ok = DataStore.AdjustIngredient(ing, baseQty, AdjustmentReason.Restock, v(3), CurrentSession.FullName, err)
        Else
            ok = DataStore.AdjustIngredient(ing, baseQty - ing.CurrentStock, AdjustmentReason.Correction, If(v(3) = "", "Stock count", v(3)), CurrentSession.FullName, err)
        End If
        If Not ok Then MessageBox.Show(err, "Restock / Count", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    ''' <summary>Manager AND Cashier.</summary>
    Private Sub ReportLoss_Click(sender As Object, e As EventArgs)
        Dim ing As Ingredient = NeedSelection()
        If ing Is Nothing Then Return
        Dim v As String() = UiDialogs.Ask(FindForm(), "Report loss - " & ing.Name, New DialogField() {
            New DialogField("Type", DialogFieldKind.Combo, AdjustmentReason.Wastage, New String() {AdjustmentReason.Wastage, AdjustmentReason.Damaged, AdjustmentReason.Lost}),
            New DialogField("Quantity lost (stock: " & ing.StockText & ")"),
            New DialogField("Unit", DialogFieldKind.Combo, ing.Unit, UiDialogs.UnitChoices(ing.Unit)),
            New DialogField("Reason (required)")}, "Report")
        If v Is Nothing Then Return
        Dim qty As Decimal
        If Not UiDialogs.TryDecimal(v(1), qty) OrElse qty <= 0D Then
            MessageBox.Show("Enter a quantity greater than zero.", "Report loss", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim err As String = ""
        If DataStore.AdjustIngredient(ing, UnitHelper.ToBase(qty, v(2)), v(0), v(3), CurrentSession.FullName, err) Then
            MessageBox.Show(ing.Name & ": -" & UnitHelper.Display(UnitHelper.ToBase(qty, v(2)), ing.Unit) & " (" & v(0) & ") recorded.", "Report loss", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show(err, "Report loss", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub Recipes_Click(sender As Object, e As EventArgs)
        Using f As New RecipeEditorForm()
            f.ShowDialog(FindForm())
        End Using
    End Sub

    Private Sub History_Click(sender As Object, e As EventArgs)
        Dim dt As New DataTable()
        For Each col As String In New String() {"When", "Ingredient", "Change", "Reason", "By", "Reference / Note"}
            dt.Columns.Add(col)
        Next
        Dim n As Integer = 0
        For Each a As InventoryAdjustment In DataStore.InventoryHistory
            dt.Rows.Add(a.RecordedAt.ToString("MMM d, hh:mm tt"), a.IngredientName,
                        If(a.Delta > 0D, "+", "-") & UnitHelper.Display(Math.Abs(a.Delta), a.Unit), a.Reason, a.RecordedBy,
                        If(a.ReferenceId <> "", a.ReferenceId, a.Note))
            n += 1
            If n >= 500 Then Exit For
        Next
        Using dlg As New Form()
            dlg.Text = "Inventory History"
            dlg.Font = New Font("Segoe UI", 10.0F)
            dlg.StartPosition = FormStartPosition.CenterParent
            dlg.Size = New Size(900, 540)
            Dim g As New DataGridView With {.Dock = DockStyle.Fill, .ReadOnly = True, .AllowUserToAddRows = False, .RowHeadersVisible = False,
                .BackgroundColor = Color.White, .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill}
            g.DataSource = dt
            dlg.Controls.Add(g)
            dlg.ShowDialog(FindForm())
        End Using
    End Sub

End Class
