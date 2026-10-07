Option Strict On
Option Explicit On

Imports System.Drawing
Imports System.Windows.Forms

'=====================================================================
' RECIPE EDITOR (Manager)  -  Product -> Size/Variant -> Recipe lines
' Latte: Small P100 = Beans 18 g + Milk 180 ml + Sugar 10 g + Cup 1 pcs
'        Large P130 = Beans 24 g + Milk 250 ml + ...
' Only this shop's products and ingredients are listed (DataStore is filtered).
'=====================================================================
Public Class RecipeEditorForm
    Inherits Form

    Private ReadOnly brown As Color = Color.FromArgb(62, 39, 35)

    Private cboProduct As ComboBox, cboVariant As ComboBox, cboIngredient As ComboBox
    Private txtPrice As TextBox, txtQty As TextBox
    Private lblUnit As Label, lblInfo As Label
    Private grid As DataGridView
    Private working As New List(Of RecipeItem)
    Private currentVariant As ProductVariant
    Private loading As Boolean = False

    Public Sub New()
        Text = "Recipes - " & DataStore.GetShopName(ShopContext.CurrentShopId)
        Font = New Font("Segoe UI", 10.0F)
        StartPosition = FormStartPosition.CenterParent
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        ClientSize = New Size(720, 560)

        Controls.Add(New Label With {.Text = "Product", .Left = 20, .Top = 14, .AutoSize = True})
        cboProduct = New ComboBox With {.Left = 20, .Top = 38, .Width = 330, .DropDownStyle = ComboBoxStyle.DropDownList}
        Controls.Add(cboProduct)

        Controls.Add(New Label With {.Text = "Size / variant", .Left = 370, .Top = 14, .AutoSize = True})
        cboVariant = New ComboBox With {.Left = 370, .Top = 38, .Width = 200, .DropDownStyle = ComboBoxStyle.DropDownList}
        Controls.Add(cboVariant)
        Dim btnSize As Button = MakeButton("+ Size", 580, 36, 120)
        AddHandler btnSize.Click, AddressOf AddSize_Click
        Controls.Add(btnSize)

        Controls.Add(New Label With {.Text = "Price of this size", .Left = 20, .Top = 76, .AutoSize = True})
        txtPrice = New TextBox With {.Left = 20, .Top = 100, .Width = 150}
        Controls.Add(txtPrice)
        lblInfo = New Label With {.Left = 190, .Top = 103, .AutoSize = True, .ForeColor = Color.Gray}
        Controls.Add(lblInfo)

        grid = New DataGridView With {.Left = 20, .Top = 142, .Width = 680, .Height = 270, .ReadOnly = True, .AllowUserToAddRows = False,
            .RowHeadersVisible = False, .BackgroundColor = Color.White, .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            .MultiSelect = False, .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill}
        grid.Columns.Add("colIng", "Ingredient")
        grid.Columns.Add("colQty", "Quantity per serving")
        grid.Columns.Add("colUnit", "Unit")
        Controls.Add(grid)

        Controls.Add(New Label With {.Text = "Add ingredient to recipe", .Left = 20, .Top = 424, .AutoSize = True})
        cboIngredient = New ComboBox With {.Left = 20, .Top = 448, .Width = 300, .DropDownStyle = ComboBoxStyle.DropDownList}
        Controls.Add(cboIngredient)
        txtQty = New TextBox With {.Left = 332, .Top = 448, .Width = 90}
        Controls.Add(txtQty)
        lblUnit = New Label With {.Left = 430, .Top = 452, .AutoSize = True}
        Controls.Add(lblUnit)
        Dim btnAdd As Button = MakeButton("Add / Update line", 490, 444, 140)
        AddHandler btnAdd.Click, AddressOf AddLine_Click
        Controls.Add(btnAdd)
        Dim btnRemove As Button = MakeButton("Remove line", 20, 500, 130)
        AddHandler btnRemove.Click, AddressOf RemoveLine_Click
        Controls.Add(btnRemove)
        Dim btnSave As Button = MakeButton("Save recipe", 470, 500, 130)
        AddHandler btnSave.Click, AddressOf Save_Click
        Controls.Add(btnSave)
        Dim btnClose As Button = New Button With {.Text = "Close", .Left = 610, .Top = 500, .Width = 90, .Height = 38, .DialogResult = DialogResult.Cancel}
        Controls.Add(btnClose)
        CancelButton = btnClose

        For Each p As Product In DataStore.Products
            cboProduct.Items.Add(p)
        Next
        cboProduct.DisplayMember = "Name"
        For Each i As Ingredient In DataStore.Ingredients
            cboIngredient.Items.Add(i)
        Next
        cboIngredient.DisplayMember = "Name"

        AddHandler cboProduct.SelectedIndexChanged, AddressOf ProductChanged
        AddHandler cboVariant.SelectedIndexChanged, AddressOf VariantChanged
        AddHandler cboIngredient.SelectedIndexChanged, Sub(o As Object, e As EventArgs) ShowUnit()
        If cboIngredient.Items.Count > 0 Then cboIngredient.SelectedIndex = 0
        If cboProduct.Items.Count > 0 Then cboProduct.SelectedIndex = 0
    End Sub

    Private Function MakeButton(caption As String, x As Integer, y As Integer, w As Integer) As Button
        Dim b As New Button With {.Text = caption, .Left = x, .Top = y, .Width = w, .Height = 38, .FlatStyle = FlatStyle.Flat,
                                  .BackColor = brown, .ForeColor = Color.White}
        b.FlatAppearance.BorderSize = 0
        Return b
    End Function

    Private Sub ShowUnit()
        Dim ing As Ingredient = TryCast(cboIngredient.SelectedItem, Ingredient)
        lblUnit.Text = If(ing Is Nothing, "", ing.Unit)
    End Sub

    Private Function SelectedProduct() As Product
        Return TryCast(cboProduct.SelectedItem, Product)
    End Function

    Private Sub ProductChanged(sender As Object, e As EventArgs)
        ReloadVariants("")
    End Sub

    Private Sub ReloadVariants(selectId As String)
        loading = True
        cboVariant.Items.Clear()
        Dim p As Product = SelectedProduct()
        If p IsNot Nothing Then
            For Each v As ProductVariant In DataStore.VariantsOf(p.Id)
                cboVariant.Items.Add(v)
            Next
            ' a product that has no variant yet gets an (unsaved) "Regular" one
            If cboVariant.Items.Count = 0 Then
                cboVariant.Items.Add(New ProductVariant With {.ProductId = p.Id, .ShopId = p.ShopId, .Size = "Regular", .Price = p.Price})
            End If
        End If
        cboVariant.DisplayMember = "Size"
        loading = False
        Dim idx As Integer = 0
        For i As Integer = 0 To cboVariant.Items.Count - 1
            If DirectCast(cboVariant.Items(i), ProductVariant).VariantId = selectId Then idx = i
        Next
        If cboVariant.Items.Count > 0 Then cboVariant.SelectedIndex = idx
        VariantChanged(Nothing, EventArgs.Empty)
    End Sub

    Private Sub VariantChanged(sender As Object, e As EventArgs)
        If loading Then Return
        currentVariant = TryCast(cboVariant.SelectedItem, ProductVariant)
        working.Clear()
        If currentVariant IsNot Nothing Then
            For Each r As RecipeItem In currentVariant.Recipe
                working.Add(New RecipeItem(r.IngredientId, r.Quantity, r.Unit))
            Next
            txtPrice.Text = currentVariant.Price.ToString("0.##")
            lblInfo.Text = If(currentVariant.HasRecipe, "Servings possible now: " & DataStore.MaxServings(currentVariant).ToString(), "No recipe yet")
        End If
        RedrawGrid()
        ShowUnit()
    End Sub

    Private Sub RedrawGrid()
        grid.Rows.Clear()
        For Each r As RecipeItem In working
            Dim ing As Ingredient = DataStore.FindIngredient(r.IngredientId)
            grid.Rows.Add(If(ing Is Nothing, "(missing)", ing.Name), r.Quantity.ToString("0.##"), r.Unit)
        Next
    End Sub

    Private Sub AddSize_Click(sender As Object, e As EventArgs)
        Dim p As Product = SelectedProduct()
        If p Is Nothing Then Return
        Dim v As String() = UiDialogs.Ask(Me, "Add size", New DialogField() {New DialogField("Size name (e.g. Small, Large)"), New DialogField("Price", DialogFieldKind.Text, p.Price.ToString("0.##"))})
        If v Is Nothing Then Return
        Dim price As Decimal
        If String.IsNullOrWhiteSpace(v(0)) OrElse Not UiDialogs.TryDecimal(v(1), price) Then
            MessageBox.Show("Enter a size name and a valid price.", "Add size", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        For Each ex As ProductVariant In DataStore.VariantsOf(p.Id)
            If String.Equals(ex.Size, v(0).Trim(), StringComparison.OrdinalIgnoreCase) Then
                MessageBox.Show("That size already exists.", "Add size", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        Next
        Dim nv As New ProductVariant With {.ProductId = p.Id, .ShopId = p.ShopId, .Size = v(0).Trim(), .Price = price}
        Dim err As String = ""
        If Not DataStore.SaveVariant(nv, err) Then
            MessageBox.Show(err, "Add size", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        ReloadVariants(nv.VariantId)
    End Sub

    Private Sub AddLine_Click(sender As Object, e As EventArgs)
        Dim ing As Ingredient = TryCast(cboIngredient.SelectedItem, Ingredient)
        Dim qty As Decimal
        If ing Is Nothing OrElse Not UiDialogs.TryDecimal(txtQty.Text, qty) OrElse qty <= 0D Then
            MessageBox.Show("Choose an ingredient and enter a quantity greater than zero (in " & If(ing Is Nothing, "its base unit", ing.Unit) & ").", "Recipe", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        For Each r As RecipeItem In working
            If r.IngredientId = ing.IngredientId Then
                r.Quantity = qty
                RedrawGrid()
                Return
            End If
        Next
        working.Add(New RecipeItem(ing.IngredientId, qty, ing.Unit))
        txtQty.Clear()
        RedrawGrid()
    End Sub

    Private Sub RemoveLine_Click(sender As Object, e As EventArgs)
        If grid.SelectedRows.Count = 0 Then Return
        working.RemoveAt(grid.SelectedRows(0).Index)
        RedrawGrid()
    End Sub

    Private Sub Save_Click(sender As Object, e As EventArgs)
        If currentVariant Is Nothing Then Return
        Dim price As Decimal
        If Not UiDialogs.TryDecimal(txtPrice.Text, price) OrElse price < 0D Then
            MessageBox.Show("Enter a valid price.", "Recipe", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim oldRecipe As New List(Of RecipeItem)(currentVariant.Recipe)
        Dim oldPrice As Decimal = currentVariant.Price
        currentVariant.Recipe.Clear()
        For Each r As RecipeItem In working
            currentVariant.Recipe.Add(New RecipeItem(r.IngredientId, r.Quantity, r.Unit))
        Next
        currentVariant.Price = price
        Dim err As String = ""
        If Not DataStore.SaveVariant(currentVariant, err) Then
            currentVariant.Recipe.Clear()
            currentVariant.Recipe.AddRange(oldRecipe)
            currentVariant.Price = oldPrice
            MessageBox.Show(err, "Recipe", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        MessageBox.Show("Recipe saved.", "Recipe", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ReloadVariants(currentVariant.VariantId)
    End Sub

End Class
