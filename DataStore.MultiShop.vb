Option Strict On
Option Explicit On

Imports System.Globalization
Imports System.IO
Imports System.Security
Imports System.Text

'=====================================================================
' DATASTORE.MULTISHOP.VB  -  the SAME DataStore module (Partial), extended for:
'   Shops, Ingredient inventory, Product Variants + Recipes, Inventory history.
' No second DataStore. Raw lists are All*; the names forms already use
' (Products, Transactions, Cashiers, ChatMessages, StockAdjustments) are now
' FILTERED VIEWS of the current shop (ShopContext), so branches never mix.
'=====================================================================
Partial Public Module DataStore

    '------------------------------------------------------------------
    ' RAW DATA (all shops).  Use these only for login / Super Admin work.
    '------------------------------------------------------------------
    Public AllShops As New List(Of Shop)
    Public AllIngredients As New List(Of Ingredient)
    Public AllVariants As New List(Of ProductVariant)
    Public AllInventoryAdjustments As New List(Of InventoryAdjustment)

    Public Event ShopsChanged As EventHandler
    Public Event IngredientsChanged As EventHandler

    '------------------------------------------------------------------
    ' FILTERED VIEWS (what the existing Admin / Cashier forms read)
    '------------------------------------------------------------------
    Public ReadOnly Property Products As List(Of Product)
        Get
            Return ShopContext.ForCurrentShop(AllProducts, Function(x) x.ShopId)
        End Get
    End Property

    Public ReadOnly Property Transactions As List(Of POS_Transaction)
        Get
            Return ShopContext.ForCurrentShop(AllTransactions, Function(x) x.ShopId)
        End Get
    End Property

    Public ReadOnly Property Cashiers As List(Of CashierAccount)
        Get
            Return ShopContext.ForCurrentShop(AllCashiers, Function(x) x.ShopId)
        End Get
    End Property

    Public ReadOnly Property ChatMessages As List(Of ChatMessage)
        Get
            Return ShopContext.ForCurrentShop(AllChatMessages, Function(x) x.ShopId)
        End Get
    End Property

    Public ReadOnly Property StockAdjustments As List(Of StockAdjustment)
        Get
            Return ShopContext.ForCurrentShop(AllStockAdjustments, Function(x) x.ShopId)
        End Get
    End Property

    ' --- new data, same rule ---
    Public ReadOnly Property Ingredients As List(Of Ingredient)
        Get
            Return ShopContext.ForCurrentShop(AllIngredients, Function(x) x.ShopId)
        End Get
    End Property

    Public ReadOnly Property InventoryHistory As List(Of InventoryAdjustment)
        Get
            Dim list As List(Of InventoryAdjustment) = ShopContext.ForCurrentShop(AllInventoryAdjustments, Function(x) x.ShopId)
            list.Sort(Function(a, b) b.RecordedAt.CompareTo(a.RecordedAt))
            Return list
        End Get
    End Property

    '==================================================================
    ' PERSISTENCE  (same pipe-delimited TXT style as the rest)
    '==================================================================
    Private Sub LoadMultiShop()
        LoadShops()
        LoadIngredients()
        LoadVariantsAndRecipes()
        LoadInventoryAdjustments()
    End Sub

    Private Sub SaveMultiShop()
        SaveShops()
        SaveIngredientData()
        SaveRecipeData()
    End Sub

    Public Sub SaveIngredientData()
        SaveIngredients()
        SaveInventoryAdjustments()
    End Sub

    Public Sub SaveRecipeData()
        File.WriteAllLines(FilePathOf("variants.txt"), AllVariants.Select(Function(v) String.Join("|",
            SafeField(v.VariantId), SafeField(v.ProductId), SafeField(v.ShopId), SafeField(v.Size),
            v.Price.ToString(CultureInfo.InvariantCulture))).ToArray(), Encoding.UTF8)
        Dim lines As New List(Of String)
        For Each v As ProductVariant In AllVariants
            For Each r As RecipeItem In v.Recipe
                lines.Add(String.Join("|", SafeField(v.VariantId), SafeField(r.IngredientId),
                          r.Quantity.ToString(CultureInfo.InvariantCulture), SafeField(r.Unit)))
            Next
        Next
        File.WriteAllLines(FilePathOf("recipes.txt"), lines.ToArray(), Encoding.UTF8)
    End Sub

    Private Sub LoadShops()
        AllShops = New List(Of Shop)
        Dim path As String = FilePathOf("shops.txt")
        If Not File.Exists(path) Then Return
        For Each line As String In File.ReadAllLines(path, Encoding.UTF8)
            Dim f As String() = Fields(line)
            ' ShopId|Name|BranchCode|Address|ManagerUsername|Status|Created
            If f.Length >= 6 Then
                Dim created As DateTime = DateTime.Now
                If f.Length >= 7 Then DateTime.TryParse(f(6), CultureInfo.InvariantCulture, DateTimeStyles.None, created)
                AllShops.Add(New Shop With {.ShopId = f(0), .ShopName = f(1), .BranchCode = f(2), .Address = f(3),
                                            .ManagerUsername = f(4), .Status = f(5), .CreatedDate = created})
            End If
        Next
    End Sub

    Public Sub SaveShops()
        File.WriteAllLines(FilePathOf("shops.txt"), AllShops.Select(Function(s) String.Join("|",
            SafeField(s.ShopId), SafeField(s.ShopName), SafeField(s.BranchCode), SafeField(s.Address),
            SafeField(s.ManagerUsername), SafeField(s.Status),
            s.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture))).ToArray(), Encoding.UTF8)
    End Sub

    Private Sub LoadIngredients()
        AllIngredients = New List(Of Ingredient)
        Dim path As String = FilePathOf("ingredients.txt")
        If Not File.Exists(path) Then Return
        For Each line As String In File.ReadAllLines(path, Encoding.UTF8)
            Dim f As String() = Fields(line)
            Dim cur, min As Decimal
            ' Id|ShopId|Name|Unit|Current|Minimum|LastUpdated
            If f.Length >= 6 AndAlso Decimal.TryParse(f(4), NumberStyles.Number, CultureInfo.InvariantCulture, cur) AndAlso
               Decimal.TryParse(f(5), NumberStyles.Number, CultureInfo.InvariantCulture, min) Then
                Dim stamp As DateTime = DateTime.Now
                If f.Length >= 7 Then DateTime.TryParse(f(6), CultureInfo.InvariantCulture, DateTimeStyles.None, stamp)
                AllIngredients.Add(New Ingredient With {.IngredientId = f(0), .ShopId = f(1), .Name = f(2), .Unit = f(3),
                                                        .CurrentStock = cur, .MinimumStock = min, .LastUpdated = stamp})
            End If
        Next
    End Sub

    Private Sub SaveIngredients()
        File.WriteAllLines(FilePathOf("ingredients.txt"), AllIngredients.Select(Function(i) String.Join("|",
            SafeField(i.IngredientId), SafeField(i.ShopId), SafeField(i.Name), SafeField(i.Unit),
            i.CurrentStock.ToString(CultureInfo.InvariantCulture), i.MinimumStock.ToString(CultureInfo.InvariantCulture),
            i.LastUpdated.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture))).ToArray(), Encoding.UTF8)
    End Sub

    Private Sub LoadVariantsAndRecipes()
        AllVariants = New List(Of ProductVariant)
        Dim vPath As String = FilePathOf("variants.txt")
        If File.Exists(vPath) Then
            For Each line As String In File.ReadAllLines(vPath, Encoding.UTF8)
                Dim f As String() = Fields(line)
                Dim price As Decimal
                ' VariantId|ProductId|ShopId|Size|Price
                If f.Length >= 5 AndAlso Decimal.TryParse(f(4), NumberStyles.Number, CultureInfo.InvariantCulture, price) Then
                    AllVariants.Add(New ProductVariant With {.VariantId = f(0), .ProductId = f(1), .ShopId = f(2), .Size = f(3), .Price = price})
                End If
            Next
        End If
        Dim rPath As String = FilePathOf("recipes.txt")
        If File.Exists(rPath) Then
            For Each line As String In File.ReadAllLines(rPath, Encoding.UTF8)
                Dim f As String() = Fields(line)
                Dim qty As Decimal
                ' VariantId|IngredientId|Quantity|Unit
                If f.Length >= 4 AndAlso Decimal.TryParse(f(2), NumberStyles.Number, CultureInfo.InvariantCulture, qty) Then
                    Dim v As ProductVariant = FindVariant(f(0))
                    If v IsNot Nothing Then v.Recipe.Add(New RecipeItem(f(1), qty, f(3)))
                End If
            Next
        End If
    End Sub

    Private Sub LoadInventoryAdjustments()
        AllInventoryAdjustments = New List(Of InventoryAdjustment)
        Dim path As String = FilePathOf("inventory_adjustments.txt")
        If Not File.Exists(path) Then Return
        For Each line As String In File.ReadAllLines(path, Encoding.UTF8)
            Dim f As String() = Fields(line)
            Dim delta As Decimal : Dim whenValue As DateTime
            ' Id|ShopId|IngredientId|IngredientName|Delta|Unit|Reason|Note|By|When|Reference
            If f.Length >= 10 AndAlso Decimal.TryParse(f(4), NumberStyles.Number, CultureInfo.InvariantCulture, delta) AndAlso
               DateTime.TryParse(f(9), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, whenValue) Then
                AllInventoryAdjustments.Add(New InventoryAdjustment With {
                    .AdjustmentId = f(0), .ShopId = f(1), .IngredientId = f(2), .IngredientName = f(3), .Delta = delta,
                    .Unit = f(5), .Reason = f(6), .Note = f(7), .RecordedBy = f(8), .RecordedAt = whenValue,
                    .ReferenceId = If(f.Length >= 11, f(10), "")})
            End If
        Next
    End Sub

    Private Sub SaveInventoryAdjustments()
        File.WriteAllLines(FilePathOf("inventory_adjustments.txt"), AllInventoryAdjustments.Select(Function(a) String.Join("|",
            SafeField(a.AdjustmentId), SafeField(a.ShopId), SafeField(a.IngredientId), SafeField(a.IngredientName),
            a.Delta.ToString(CultureInfo.InvariantCulture), SafeField(a.Unit), SafeField(a.Reason), SafeField(a.Note),
            SafeField(a.RecordedBy), a.RecordedAt.ToString("o", CultureInfo.InvariantCulture), SafeField(a.ReferenceId))).ToArray(), Encoding.UTF8)
    End Sub

    '==================================================================
    ' FIRST-RUN MIGRATION  (old single-shop data -> "Main Branch")
    '==================================================================
    Private Sub MigrateToMultiShop()
        Try
            Dim changed As Boolean = False

            If AllShops.Count = 0 Then
                AllShops.Add(New Shop With {.ShopName = "Main Branch", .BranchCode = "BR-001", .Status = ShopStatus.Active})
                changed = True
            End If
            Dim def As Shop = AllShops(0)

            For Each p As Product In AllProducts
                If String.IsNullOrWhiteSpace(p.ShopId) Then p.ShopId = def.ShopId : changed = True
            Next
            For Each c As CashierAccount In AllCashiers
                If String.IsNullOrWhiteSpace(c.ShopId) Then c.ShopId = def.ShopId : changed = True
            Next
            For Each t As POS_Transaction In AllTransactions
                If String.IsNullOrWhiteSpace(t.ShopId) Then t.ShopId = def.ShopId : changed = True
            Next
            For Each m As ChatMessage In AllChatMessages
                If String.IsNullOrWhiteSpace(m.ShopId) Then m.ShopId = def.ShopId : changed = True
            Next
            For Each s As StockAdjustment In AllStockAdjustments
                If String.IsNullOrWhiteSpace(s.ShopId) Then s.ShopId = def.ShopId : changed = True
            Next

            ' existing admin accounts become Managers of the first shop
            Dim hasSuper As Boolean = False
            For Each a As AdminAccount In AllAdmins
                If UserRoles.Normalize(a.Role) = UserRoles.SuperAdmin Then
                    hasSuper = True
                ElseIf String.IsNullOrWhiteSpace(a.ShopId) Then
                    a.ShopId = def.ShopId : changed = True
                End If
                If UserRoles.Normalize(a.Role) = UserRoles.Manager AndAlso a.ShopId = def.ShopId AndAlso def.ManagerUsername = "" Then
                    def.ManagerUsername = a.Username : changed = True
                End If
            Next

            ' a Super Admin must exist  (CHANGE THIS PASSWORD after first login)
            If Not hasSuper Then
                AllAdmins.Add(New AdminAccount With {.Id = "ADM-" & (NextAdminNumber()).ToString("D3"), .FullName = "Super Admin",
                    .Username = "owner", .PasswordHash = "owner123", .Status = AccountStatus.Active,
                    .ShopId = "", .Role = UserRoles.SuperAdmin})
                changed = True
            End If

            ' every product gets one default ("Regular") variant that the recipe editor can use later
            For Each p As Product In AllProducts
                If VariantsOf(p.Id).Count = 0 Then
                    AllVariants.Add(New ProductVariant With {.ProductId = p.Id, .ShopId = p.ShopId, .Size = "Regular", .Price = p.Price})
                    changed = True
                End If
            Next

            If changed Then
                SaveProducts() : SaveCashiers() : SaveAdmins() : SaveTransactions()
                SaveMessages() : SaveStockAdjustments() : SaveMultiShop()
            End If
        Catch ex As Exception
            MessageBox.Show("Multi-shop migration failed:" & vbCrLf & ex.Message, "BrewPoint POS", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Function NextAdminNumber() As Integer
        Dim n As Integer = 0
        For Each a As AdminAccount In AllAdmins
            n = Math.Max(n, NumberFromId(a.Id))
        Next
        Return n + 1
    End Function

    '==================================================================
    ' SHOPS  (Super Admin)
    '==================================================================
    Public Function GetShops() As List(Of Shop)
        Return New List(Of Shop)(AllShops)
    End Function

    Public Function FindShop(shopId As String) As Shop
        If String.IsNullOrWhiteSpace(shopId) Then Return Nothing
        For Each s As Shop In AllShops
            If String.Equals(s.ShopId, shopId, StringComparison.OrdinalIgnoreCase) Then Return s
        Next
        Return Nothing
    End Function

    Public Function GetShopName(shopId As String) As String
        Dim s As Shop = FindShop(shopId)
        Return If(s Is Nothing, "", s.ShopName)
    End Function

    Private Function ShopNameOrCodeTaken(name As String, code As String, Optional excludeId As String = "") As Boolean
        For Each s As Shop In AllShops
            If s.ShopId = excludeId Then Continue For
            If String.Equals(s.ShopName, name, StringComparison.OrdinalIgnoreCase) Then Return True
            If code <> "" AndAlso String.Equals(s.BranchCode, code, StringComparison.OrdinalIgnoreCase) Then Return True
        Next
        Return False
    End Function

    Public Function AddShop(name As String, code As String, address As String, ByRef errorMessage As String) As Shop
        errorMessage = ""
        If Not ShopContext.Can(Permissions.ManageShops) Then errorMessage = "Only the Super Admin can add shops." : Return Nothing
        If String.IsNullOrWhiteSpace(name) Then errorMessage = "Shop name is required." : Return Nothing
        code = If(code, "").Trim()
        If code = "" Then
            Dim n As Integer = AllShops.Count + 1
            Do
                code = "BR-" & n.ToString("D3")
                n += 1
            Loop While ShopNameOrCodeTaken("", code)
        End If
        If ShopNameOrCodeTaken(name.Trim(), code) Then errorMessage = "A shop with that name or branch code already exists." : Return Nothing

        Dim s As New Shop With {.ShopName = name.Trim(), .BranchCode = code, .Address = If(address, "").Trim(), .Status = ShopStatus.Active}
        AllShops.Add(s)
        Try
            SaveShops()
        Catch ex As Exception
            AllShops.Remove(s)
            errorMessage = ex.Message
            Return Nothing
        End Try
        RaiseEvent ShopsChanged(Nothing, EventArgs.Empty)
        Return s
    End Function

    Public Function UpdateShop(shopId As String, name As String, code As String, address As String,
                               status As String, ByRef errorMessage As String) As Boolean
        errorMessage = ""
        If Not ShopContext.Can(Permissions.ManageShops) Then errorMessage = "Only the Super Admin can edit shops." : Return False
        Dim s As Shop = FindShop(shopId)
        If s Is Nothing Then errorMessage = "Shop not found." : Return False
        If String.IsNullOrWhiteSpace(name) Then errorMessage = "Shop name is required." : Return False
        If ShopNameOrCodeTaken(name.Trim(), If(code, "").Trim(), shopId) Then errorMessage = "A shop with that name or branch code already exists." : Return False
        s.ShopName = name.Trim() : s.BranchCode = If(code, "").Trim() : s.Address = If(address, "").Trim() : s.Status = status
        Try
            SaveShops()
        Catch ex As Exception
            errorMessage = ex.Message
            Return False
        End Try
        RaiseEvent ShopsChanged(Nothing, EventArgs.Empty)
        Return True
    End Function

    Public Function GetShopManager(shopId As String) As AdminAccount
        For Each a As AdminAccount In AllAdmins
            If UserRoles.Normalize(a.Role) = UserRoles.Manager AndAlso String.Equals(a.ShopId, shopId, StringComparison.OrdinalIgnoreCase) Then Return a
        Next
        Return Nothing
    End Function

    ''' <summary>One manager per shop.</summary>
    Public Function AddManager(shopId As String, fullName As String, username As String, password As String,
                               ByRef errorMessage As String) As Boolean
        errorMessage = ""
        If Not ShopContext.Can(Permissions.ManageManagers) Then errorMessage = "Only the Super Admin can add managers." : Return False
        Dim s As Shop = FindShop(shopId)
        If s Is Nothing Then errorMessage = "Shop not found." : Return False
        If GetShopManager(shopId) IsNot Nothing Then errorMessage = s.ShopName & " already has a manager." : Return False
        If String.IsNullOrWhiteSpace(fullName) Then errorMessage = "Full name is required." : Return False
        If String.IsNullOrWhiteSpace(username) Then errorMessage = "Username is required." : Return False
        If String.IsNullOrEmpty(password) Then errorMessage = "Password is required." : Return False
        If UsernameExists(username) Then errorMessage = "That username is already in use." : Return False

        Dim acc As New AdminAccount With {.Id = "ADM-" & NextAdminNumber().ToString("D3"), .FullName = fullName.Trim(),
            .Username = username.Trim(), .PasswordHash = password, .Status = AccountStatus.Active,
            .ShopId = shopId, .Role = UserRoles.Manager}
        AllAdmins.Add(acc)
        s.ManagerUsername = acc.Username
        Try
            SaveAdmins()
            SaveShops()
        Catch ex As Exception
            AllAdmins.Remove(acc)
            s.ManagerUsername = ""
            errorMessage = ex.Message
            Return False
        End Try
        RaiseEvent ShopsChanged(Nothing, EventArgs.Empty)
        Return True
    End Function

    '------------------------------------------------------------------
    ' PER-SHOP NUMBERS (explicit shopId - used by the Super Admin dashboard)
    '------------------------------------------------------------------
    Public Function ShopSales(shopId As String, Optional todayOnly As Boolean = False) As Decimal
        Dim sum As Decimal = 0D
        For Each t As POS_Transaction In AllTransactions
            If t.ShopId = shopId AndAlso t.Status = TransactionStatus.Completed AndAlso (Not todayOnly OrElse t.TransactionDate.Date = DateTime.Today) Then sum += t.Total
        Next
        Return sum
    End Function

    Public Function ShopOrders(shopId As String, Optional todayOnly As Boolean = False) As Integer
        Dim n As Integer = 0
        For Each t As POS_Transaction In AllTransactions
            If t.ShopId = shopId AndAlso t.Status = TransactionStatus.Completed AndAlso (Not todayOnly OrElse t.TransactionDate.Date = DateTime.Today) Then n += 1
        Next
        Return n
    End Function

    Public Function ShopCashierCount(shopId As String) As Integer
        Dim n As Integer = 0
        For Each c As CashierAccount In AllCashiers
            If String.Equals(c.ShopId, shopId, StringComparison.OrdinalIgnoreCase) Then n += 1
        Next
        Return n
    End Function

    Public Function ManagerCount() As Integer
        Dim n As Integer = 0
        For Each a As AdminAccount In AllAdmins
            If UserRoles.Normalize(a.Role) = UserRoles.Manager AndAlso a.ShopId <> "" Then n += 1
        Next
        Return n
    End Function

    Public Function GetShopLowIngredients(shopId As String) As List(Of Ingredient)
        Dim result As New List(Of Ingredient)
        For Each i As Ingredient In AllIngredients
            If String.Equals(i.ShopId, shopId, StringComparison.OrdinalIgnoreCase) AndAlso i.Status <> IngredientStatus.InStock Then result.Add(i)
        Next
        Return result
    End Function

    ''' <summary>Low / out-of-stock ingredients of the CURRENT shop (Manager and Cashier).</summary>
    Public Function GetLowIngredients() As List(Of Ingredient)
        Dim result As New List(Of Ingredient)
        For Each i As Ingredient In Ingredients
            If i.Status <> IngredientStatus.InStock Then result.Add(i)
        Next
        Return result
    End Function

    '==================================================================
    ' INGREDIENT INVENTORY
    '==================================================================
    Public Function FindIngredient(ingredientId As String) As Ingredient
        For Each i As Ingredient In AllIngredients
            If String.Equals(i.IngredientId, ingredientId, StringComparison.OrdinalIgnoreCase) Then Return i
        Next
        Return Nothing
    End Function

    Private Function NextIngredientId() As String
        Dim n As Integer = 0
        For Each i As Ingredient In AllIngredients
            n = Math.Max(n, NumberFromId(i.IngredientId))
        Next
        Return "ING-" & (n + 1).ToString("D4")
    End Function

    Private Sub LogAdjustment(ing As Ingredient, delta As Decimal, reason As String, note As String, by As String, refId As String,
                              bucket As List(Of InventoryAdjustment))
        Dim entry As New InventoryAdjustment With {.ShopId = ing.ShopId, .IngredientId = ing.IngredientId, .IngredientName = ing.Name,
            .Delta = delta, .Unit = ing.Unit, .Reason = reason, .Note = If(note, ""), .RecordedBy = If(by, ""),
            .RecordedAt = DateTime.Now, .ReferenceId = If(refId, "")}
        AllInventoryAdjustments.Add(entry)
        If bucket IsNot Nothing Then bucket.Add(entry)
    End Sub

    ''' <summary>Manager only. Quantities are entered in any unit (kg, g, L, ml, pcs) and stored in the base unit.</summary>
    Public Function AddIngredient(name As String, unitEntered As String, openingQty As Decimal, minimumQty As Decimal,
                                  ByRef errorMessage As String) As Ingredient
        errorMessage = ""
        If Not ShopContext.Can(Permissions.ManageIngredients) Then errorMessage = "Only a Manager can create ingredients." : Return Nothing
        Dim shopId As String = ShopContext.CurrentShopId
        If shopId = "" Then errorMessage = "Select a shop first." : Return Nothing
        If String.IsNullOrWhiteSpace(name) Then errorMessage = "Ingredient name is required." : Return Nothing
        If openingQty < 0D OrElse minimumQty < 0D Then errorMessage = "Quantities cannot be negative." : Return Nothing
        For Each i As Ingredient In AllIngredients
            If i.ShopId = shopId AndAlso String.Equals(i.Name, name.Trim(), StringComparison.OrdinalIgnoreCase) Then
                errorMessage = "That ingredient already exists in this shop." : Return Nothing
            End If
        Next

        Dim ing As New Ingredient With {.IngredientId = NextIngredientId(), .ShopId = shopId, .Name = name.Trim(),
            .Unit = UnitHelper.BaseUnitOf(unitEntered), .CurrentStock = UnitHelper.ToBase(openingQty, unitEntered),
            .MinimumStock = UnitHelper.ToBase(minimumQty, unitEntered), .LastUpdated = DateTime.Now}
        AllIngredients.Add(ing)
        If ing.CurrentStock > 0D Then LogAdjustment(ing, ing.CurrentStock, AdjustmentReason.Restock, "Opening stock", CurrentSession.FullName, "", Nothing)
        Try
            SaveIngredientData()
        Catch ex As Exception
            AllIngredients.Remove(ing)
            errorMessage = ex.Message
            Return Nothing
        End Try
        RaiseEvent IngredientsChanged(Nothing, EventArgs.Empty)
        Return ing
    End Function

    ''' <summary>Manager only. minimumBase is in the ingredient's base unit.</summary>
    Public Function UpdateIngredient(ing As Ingredient, name As String, minimumBase As Decimal, ByRef errorMessage As String) As Boolean
        errorMessage = ""
        If ing Is Nothing OrElse Not ShopContext.CanAccess(ing.ShopId) Then errorMessage = "Ingredient not found." : Return False
        If Not ShopContext.Can(Permissions.ManageIngredients) Then errorMessage = "Only a Manager can edit ingredients." : Return False
        If String.IsNullOrWhiteSpace(name) Then errorMessage = "Ingredient name is required." : Return False
        If minimumBase < 0D Then errorMessage = "Minimum stock cannot be negative." : Return False
        For Each i As Ingredient In AllIngredients
            If i IsNot ing AndAlso i.ShopId = ing.ShopId AndAlso String.Equals(i.Name, name.Trim(), StringComparison.OrdinalIgnoreCase) Then
                errorMessage = "Another ingredient already has that name." : Return False
            End If
        Next
        ing.Name = name.Trim() : ing.MinimumStock = minimumBase : ing.LastUpdated = DateTime.Now
        Try
            SaveIngredientData()
        Catch ex As Exception
            errorMessage = ex.Message
            Return False
        End Try
        RaiseEvent IngredientsChanged(Nothing, EventArgs.Empty)
        Return True
    End Function

    Public Function DeleteIngredient(ing As Ingredient, ByRef errorMessage As String) As Boolean
        errorMessage = ""
        If ing Is Nothing OrElse Not ShopContext.CanAccess(ing.ShopId) Then errorMessage = "Ingredient not found." : Return False
        If Not ShopContext.Can(Permissions.ManageIngredients) Then errorMessage = "Only a Manager can delete ingredients." : Return False
        For Each v As ProductVariant In AllVariants
            For Each r As RecipeItem In v.Recipe
                If r.IngredientId = ing.IngredientId Then
                    errorMessage = "This ingredient is used in a recipe. Remove it from the recipe first." : Return False
                End If
            Next
        Next
        AllIngredients.Remove(ing)
        Try
            SaveIngredientData()
        Catch ex As Exception
            AllIngredients.Add(ing)
            errorMessage = ex.Message
            Return False
        End Try
        RaiseEvent IngredientsChanged(Nothing, EventArgs.Empty)
        Return True
    End Function

    ''' <summary>
    ''' The ONE place that changes ingredient stock by hand.
    ''' deltaBase is in base units. Restock = +, Wastage/Damaged/Lost = - (sign is forced).
    ''' Cashiers may only report Wastage / Damaged / Lost; Managers may do all.
    ''' </summary>
    Public Function AdjustIngredient(ing As Ingredient, deltaBase As Decimal, reason As String, note As String,
                                     by As String, ByRef errorMessage As String) As Boolean
        errorMessage = ""
        If ing Is Nothing OrElse Not ShopContext.CanAccess(ing.ShopId) Then errorMessage = "Ingredient not found." : Return False

        Dim isLoss As Boolean = (reason = AdjustmentReason.Wastage OrElse reason = AdjustmentReason.Damaged OrElse reason = AdjustmentReason.Lost)
        If isLoss Then
            If Not ShopContext.Can(Permissions.ReportLoss) Then errorMessage = "You are not allowed to report losses." : Return False
            deltaBase = -Math.Abs(deltaBase)
            If String.IsNullOrWhiteSpace(note) Then errorMessage = "A reason / note is required." : Return False
        ElseIf reason = AdjustmentReason.Restock Then
            If Not ShopContext.Can(Permissions.AdjustStock) Then errorMessage = "Only a Manager can restock or correct stock." : Return False
            deltaBase = Math.Abs(deltaBase)
        ElseIf reason = AdjustmentReason.Correction Then
            If Not ShopContext.Can(Permissions.AdjustStock) Then errorMessage = "Only a Manager can restock or correct stock." : Return False
        Else
            errorMessage = "Invalid adjustment type." : Return False
        End If
        If deltaBase = 0D Then errorMessage = "Quantity must not be zero." : Return False
        If ing.CurrentStock + deltaBase < 0D Then
            errorMessage = "Only " & UnitHelper.Display(ing.CurrentStock, ing.Unit) & " of " & ing.Name & " is in stock." : Return False
        End If

        Dim before As Decimal = ing.CurrentStock
        Dim bucket As New List(Of InventoryAdjustment)
        ing.CurrentStock += deltaBase
        ing.LastUpdated = DateTime.Now
        LogAdjustment(ing, deltaBase, reason, note, by, "", bucket)
        RefreshRecipeAvailability(ing.ShopId)
        Try
            SaveIngredientData()
            SaveProducts()
        Catch ex As Exception
            ing.CurrentStock = before
            RollbackIngredientAdjustments(bucket)
            RefreshRecipeAvailability(ing.ShopId)
            errorMessage = "The adjustment could not be saved: " & ex.Message
            Return False
        End Try

        ' same behaviour as the old stock adjustment: tell the manager when a cashier reports a loss
        If isLoss AndAlso ShopContext.CurrentRole = UserRoles.Cashier Then
            AddMessage(New ChatMessage With {.Sender = "Cashier", .Receiver = "Admin", .TimeSent = DateTime.Now,
                .Message = "[Ingredient " & reason & "] " & by & ": " & ing.Name & " -" &
                           UnitHelper.Display(Math.Abs(deltaBase), ing.Unit) & ". Reason: " & note.Trim()})
        End If
        RaiseEvent IngredientsChanged(Nothing, EventArgs.Empty)
        RaiseEvent ProductsChanged(Nothing, EventArgs.Empty)
        Return True
    End Function

    '==================================================================
    ' PRODUCT -> VARIANT -> RECIPE -> INGREDIENT
    '==================================================================
    Public Function FindVariant(variantId As String) As ProductVariant
        For Each v As ProductVariant In AllVariants
            If String.Equals(v.VariantId, variantId, StringComparison.OrdinalIgnoreCase) Then Return v
        Next
        Return Nothing
    End Function

    Public Function VariantsOf(productId As String) As List(Of ProductVariant)
        Dim result As New List(Of ProductVariant)
        For Each v As ProductVariant In AllVariants
            If String.Equals(v.ProductId, productId, StringComparison.OrdinalIgnoreCase) Then result.Add(v)
        Next
        result.Sort(Function(a, b) a.Price.CompareTo(b.Price))
        Return result
    End Function

    ''' <summary>
    ''' The variant whose recipe is used when the POS sells this product.
    ''' (Smallest-priced variant that HAS a recipe; Nothing = product has no recipe -> old stock logic.)
    ''' When the POS gets a size picker, CreateSale will receive the chosen variant instead.
    ''' </summary>
    Public Function RecipeVariantOf(p As Product) As ProductVariant
        If p Is Nothing Then Return Nothing
        For Each v As ProductVariant In VariantsOf(p.Id)
            If v.HasRecipe Then Return v
        Next
        Return Nothing
    End Function

    ''' <summary>Add or update a variant with its recipe. Manager only; ingredients must belong to the same shop.</summary>
    Public Function SaveVariant(v As ProductVariant, ByRef errorMessage As String) As Boolean
        errorMessage = ""
        If v Is Nothing Then errorMessage = "Nothing to save." : Return False
        If Not ShopContext.Can(Permissions.ManageRecipes) Then errorMessage = "Only a Manager can edit recipes." : Return False
        If Not ShopContext.CanAccess(v.ShopId) Then errorMessage = "That product belongs to another shop." : Return False
        If String.IsNullOrWhiteSpace(v.Size) Then errorMessage = "Size is required." : Return False
        If v.Price < 0D Then errorMessage = "Price cannot be negative." : Return False
        For Each r As RecipeItem In v.Recipe
            Dim ing As Ingredient = FindIngredient(r.IngredientId)
            If ing Is Nothing OrElse Not String.Equals(ing.ShopId, v.ShopId, StringComparison.OrdinalIgnoreCase) Then
                errorMessage = "A recipe ingredient does not belong to this shop." : Return False
            End If
            If r.Quantity <= 0D Then errorMessage = "Recipe quantities must be greater than zero." : Return False
            r.Unit = ing.Unit
        Next
        If Not AllVariants.Contains(v) Then AllVariants.Add(v)
        Try
            SaveRecipeData()
        Catch ex As Exception
            errorMessage = ex.Message
            Return False
        End Try
        RefreshRecipeAvailability(v.ShopId)
        Try
            SaveProducts()
        Catch
        End Try
        RaiseEvent ProductsChanged(Nothing, EventArgs.Empty)
        Return True
    End Function

    ''' <summary>How many servings of this variant the ingredients still allow.</summary>
    Public Function MaxServings(v As ProductVariant) As Integer
        If v Is Nothing OrElse Not v.HasRecipe Then Return 0
        Dim best As Decimal = Decimal.MaxValue
        For Each r As RecipeItem In v.Recipe
            Dim ing As Ingredient = FindIngredient(r.IngredientId)
            If ing Is Nothing OrElse r.Quantity <= 0D Then Return 0
            best = Math.Min(best, Math.Floor(ing.CurrentStock / r.Quantity))
        Next
        If best = Decimal.MaxValue Then Return 0
        Return CInt(Math.Min(best, 99999D))
    End Function

    ''' <summary>
    ''' Products with a recipe show "servings left" in Product.Stock so the existing POS / inventory
    ''' screens keep working unchanged. The real stock lives in the ingredients.
    ''' shopId "" = every shop.
    ''' </summary>
    Public Sub RefreshRecipeAvailability(shopId As String)
        For Each p As Product In AllProducts
            If shopId <> "" AndAlso Not String.Equals(p.ShopId, shopId, StringComparison.OrdinalIgnoreCase) Then Continue For
            Dim v As ProductVariant = RecipeVariantOf(p)
            If v Is Nothing Then Continue For
            Dim servings As Integer = MaxServings(v)
            If p.Stock <> servings Then
                p.Stock = servings
                SyncAvailability(p)
            End If
        Next
    End Sub

    '------------------------------------------------------------------
    ' SALE SUPPORT  (used by CreateSale / refund / void)
    '------------------------------------------------------------------
    Private Function BuildIngredientNeeds(lines As List(Of KeyValuePair(Of Product, Integer))) As Dictionary(Of String, Decimal)
        Dim needs As New Dictionary(Of String, Decimal)
        For Each line As KeyValuePair(Of Product, Integer) In lines
            Dim v As ProductVariant = RecipeVariantOf(line.Key)
            If v Is Nothing Then Continue For
            For Each r As RecipeItem In v.Recipe
                Dim add As Decimal = r.Quantity * line.Value
                If needs.ContainsKey(r.IngredientId) Then needs(r.IngredientId) += add Else needs(r.IngredientId) = add
            Next
        Next
        Return needs
    End Function

    Private Function CheckIngredientNeeds(needs As Dictionary(Of String, Decimal), ByRef errorMessage As String) As Boolean
        errorMessage = ""
        For Each kv As KeyValuePair(Of String, Decimal) In needs
            Dim ing As Ingredient = FindIngredient(kv.Key)
            If ing Is Nothing OrElse Not String.Equals(ing.ShopId, ShopContext.CurrentShopId, StringComparison.OrdinalIgnoreCase) Then
                errorMessage = "A recipe ingredient is missing from this shop's inventory." : Return False
            End If
            If ing.CurrentStock < kv.Value Then
                errorMessage = "Not enough " & ing.Name & " (need " & UnitHelper.Display(kv.Value, ing.Unit) &
                               ", available " & UnitHelper.Display(ing.CurrentStock, ing.Unit) & ")."
                Return False
            End If
        Next
        Return True
    End Function

    Private Function ApplyIngredientNeeds(needs As Dictionary(Of String, Decimal), trxId As String, by As String,
                                          reason As String) As List(Of InventoryAdjustment)
        Dim bucket As New List(Of InventoryAdjustment)
        For Each kv As KeyValuePair(Of String, Decimal) In needs
            Dim ing As Ingredient = FindIngredient(kv.Key)
            If ing Is Nothing Then Continue For
            ing.CurrentStock -= kv.Value
            ing.LastUpdated = DateTime.Now
            LogAdjustment(ing, -kv.Value, reason, "", by, trxId, bucket)
        Next
        Return bucket
    End Function

    ''' <summary>Undo in memory: put the stock back and drop the history rows that were just added.</summary>
    Private Sub RollbackIngredientAdjustments(entries As List(Of InventoryAdjustment))
        If entries Is Nothing Then Return
        For Each e As InventoryAdjustment In entries
            Dim ing As Ingredient = FindIngredient(e.IngredientId)
            If ing IsNot Nothing Then ing.CurrentStock -= e.Delta
            AllInventoryAdjustments.Remove(e)
        Next
    End Sub

    Private Function HasSaleAdjustments(trxId As String) As Boolean
        For Each a As InventoryAdjustment In AllInventoryAdjustments
            If a.ReferenceId = trxId AndAlso a.Reason = AdjustmentReason.Sale Then Return True
        Next
        Return False
    End Function

    ''' <summary>Refund / void: give back exactly what that sale took (even if the recipe changed since).</summary>
    Private Function ReverseSaleAdjustments(trxId As String, by As String, newStatus As String) As List(Of InventoryAdjustment)
        Dim bucket As New List(Of InventoryAdjustment)
        Dim originals As New List(Of InventoryAdjustment)
        For Each a As InventoryAdjustment In AllInventoryAdjustments
            If a.ReferenceId = trxId AndAlso a.Reason = AdjustmentReason.Sale Then originals.Add(a)
        Next
        For Each a As InventoryAdjustment In originals
            Dim ing As Ingredient = FindIngredient(a.IngredientId)
            If ing Is Nothing Then Continue For
            ing.CurrentStock += -a.Delta
            ing.LastUpdated = DateTime.Now
            LogAdjustment(ing, -a.Delta, AdjustmentReason.Refund, newStatus & " " & trxId, by, trxId, bucket)
        Next
        Return bucket
    End Function

End Module
