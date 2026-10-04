Option Strict On
Option Explicit On

Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Text

'=====================================================================
' DATASTORE.VB - file-backed POS data access.
' Persistent records are pipe-delimited text files in the application folder.
'
' Forms must NEVER read/write files themselves - they call this module.
'=====================================================================
Public Module DataStore

    '------------------------------------------------------------------
    ' IN-MEMORY DATA (public names kept for compatibility with old forms)
    '------------------------------------------------------------------
    Public Products As New List(Of Product)
    Public Transactions As New List(Of POS_Transaction)
    Public ChatMessages As New List(Of ChatMessage)
    Public Cashiers As New List(Of CashierAccount)
    Public Admins As New List(Of AdminAccount)
    Public PriceLogs As New List(Of PriceChangeLog)
    Public PosSettings As New SystemSettings

    '------------------------------------------------------------------
    ' EVENTS (forms subscribe so every screen refreshes without restart)
    '------------------------------------------------------------------
    Public Event TransactionsChanged As EventHandler
    Public Event ProductsChanged As EventHandler
    Public Event CashiersChanged As EventHandler
    Public Event MessagesChanged As EventHandler

    Private ReadOnly SyncLock_ As New Object()
    Private initialized As Boolean = False

    '------------------------------------------------------------------
    ' PATHS
    '------------------------------------------------------------------
    Public ReadOnly Property DataFolder As String
        Get
            Return Application.StartupPath
        End Get
    End Property

    Public ReadOnly Property ImageFolder As String
        Get
            Return Path.Combine(DataFolder, "Images")
        End Get
    End Property

    Public ReadOnly Property BackupFolder As String
        Get
            Return Path.Combine(DataFolder, "Backup")
        End Get
    End Property

    Private Function FilePathOf(fileName As String) As String
        Return Path.Combine(DataFolder, fileName)
    End Function

    Private Function SafeField(value As String) As String
        Return If(value, "").Replace("|", "/").Replace(vbCr, " ").Replace(vbLf, " ").Trim()
    End Function

    Private Function Fields(line As String) As String()
        Return line.Split(New Char() {"|"c})
    End Function

    '==================================================================
    ' STARTUP
    '==================================================================
    ''' <summary>Safe to call many times; only the first call loads data.</summary>
    Public Sub Initialize()
        If initialized Then Return
        SyncLock SyncLock_
            If initialized Then Return
            Try
                Directory.CreateDirectory(DataFolder)
                Directory.CreateDirectory(ImageFolder)
            Catch ex As Exception
                MessageBox.Show("Unable to create the Data folder:" & vbCrLf & ex.Message,
                                "BrewPoint POS", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            LoadAllData()
            initialized = True
        End SyncLock
    End Sub

    Public Sub LoadAllData()
        Try
            LoadSettings()
            LoadProducts()
            LoadCashiers()
            LoadAdmins()
            LoadTransactions()
            LoadMessages()
            LoadPriceLogs()
            SyncCounters()
        Catch ex As Exception
            MessageBox.Show("Unable to load POS data." & vbCrLf & vbCrLf &
                            "The system will attempt to recover the data." & vbCrLf & ex.Message,
                            "BrewPoint POS", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Public Sub SaveAllData()
        SaveSettings()
        SaveProducts()
        SaveCashiers()
        SaveAdmins()
        SaveTransactions()
        SaveMessages()
        SavePriceLogs()
    End Sub

    ' All persistent data uses readable pipe-delimited TXT files in the app folder.
    Public Sub LoadSettings()
        PosSettings = New SystemSettings()
        Dim path = FilePathOf("settings.txt")
        If File.Exists(path) Then
            Dim f = Fields(File.ReadAllText(path, Encoding.UTF8).Trim().TrimStart(ChrW(&HFEFF)))
            If f.Length >= 5 Then
                Integer.TryParse(f(0), PosSettings.DefaultMinStock)
                Decimal.TryParse(f(1), NumberStyles.Number, CultureInfo.InvariantCulture, PosSettings.TaxRate)
                Integer.TryParse(f(2), PosSettings.NextTransactionNumber)
                Integer.TryParse(f(3), PosSettings.NextProductNumber)
                Integer.TryParse(f(4), PosSettings.NextCashierNumber)
            End If
        Else
            SaveSettings()
        End If
    End Sub

    Public Sub SaveSettings()
        File.WriteAllText(FilePathOf("settings.txt"), String.Join("|", PosSettings.DefaultMinStock, PosSettings.TaxRate.ToString(CultureInfo.InvariantCulture), PosSettings.NextTransactionNumber, PosSettings.NextProductNumber, PosSettings.NextCashierNumber), Encoding.UTF8)
    End Sub

    Public Sub LoadProducts()
        Dim path = FilePathOf("products.txt")
        Products = New List(Of Product)
        If File.Exists(path) Then
            For Each line In File.ReadAllLines(path, Encoding.UTF8)
                Dim f = Fields(line) : Dim price As Decimal : Dim stock As Integer
                If f.Length >= 5 AndAlso Decimal.TryParse(f(2), NumberStyles.Number, CultureInfo.InvariantCulture, price) AndAlso Integer.TryParse(f(3), stock) Then
                    ' columns: Id|Name|Price|Stock|Category|Description|ImagePath|LastUpdated  (last 3 are optional: old files still load)
                    Dim p As New Product With {.Id = f(0), .Name = f(1), .Price = price, .Stock = stock, .Category = f(4), .Status = If(stock > 0, "Available", "Unavailable")}
                    If f.Length >= 6 Then p.Description = f(5)
                    If f.Length >= 7 Then p.ImagePath = f(6)
                    Dim stamp As DateTime
                    If f.Length >= 8 AndAlso DateTime.TryParse(f(7), CultureInfo.InvariantCulture, DateTimeStyles.None, stamp) Then
                        p.LastUpdated = stamp
                    Else
                        p.LastUpdated = DateTime.Now
                    End If
                    LoadProductImage(p)
                    Products.Add(p)
                End If
            Next
        Else
            Products = BuildDefaultProducts()
            SaveProducts()
        End If
    End Sub

    Public Sub SaveProducts()
        File.WriteAllLines(FilePathOf("products.txt"), Products.Select(Function(p) String.Join("|", SafeField(p.Id), SafeField(p.Name), p.Price.ToString(CultureInfo.InvariantCulture), p.Stock, SafeField(p.Category), SafeField(p.Description), SafeField(p.ImagePath), p.LastUpdated.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture))).ToArray(), Encoding.UTF8)
    End Sub

    Public Sub LoadCashiers()
        Cashiers = New List(Of CashierAccount) : Dim path = FilePathOf("cashier_accounts.txt")
        If File.Exists(path) Then
            For Each line In File.ReadAllLines(path, Encoding.UTF8)
                Dim f = Fields(line)
                If f.Length >= 3 Then Cashiers.Add(New CashierAccount With {.Username = f(0), .PasswordHash = f(1), .FullName = f(2), .Id = "CSH-" & (Cashiers.Count + 1).ToString("D3"), .Status = AccountStatus.Active})
            Next
        Else
            Cashiers.Add(New CashierAccount With {.Id = "CSH-001", .FullName = "Jedrick Miclat", .Username = "jedrick", .PasswordHash = "cashier123", .Status = AccountStatus.Active})
            SaveCashiers()
        End If
    End Sub

    Public Sub SaveCashiers()
        File.WriteAllLines(FilePathOf("cashier_accounts.txt"), Cashiers.Select(Function(c) String.Join("|", SafeField(c.Username), SafeField(c.PasswordHash), SafeField(c.FullName))).ToArray(), Encoding.UTF8)
    End Sub

    Public Sub LoadAdmins()
        Admins = New List(Of AdminAccount) : Dim path = FilePathOf("admin_accounts.txt")
        If File.Exists(path) Then
            For Each line In File.ReadAllLines(path, Encoding.UTF8)
                Dim f = Fields(line)
                If f.Length >= 3 Then Admins.Add(New AdminAccount With {.Username = f(0), .PasswordHash = f(1), .FullName = f(2), .Id = "ADM-" & (Admins.Count + 1).ToString("D3"), .Status = AccountStatus.Active})
            Next
        Else
            Admins.Add(New AdminAccount With {.Id = "ADM-001", .FullName = "Justine Fritz Bucong", .Username = "fritz", .PasswordHash = "admin123", .Status = AccountStatus.Active})
            SaveAdmins()
        End If
    End Sub

    Public Sub SaveAdmins()
        File.WriteAllLines(FilePathOf("admin_accounts.txt"), Admins.Select(Function(a) String.Join("|", SafeField(a.Username), SafeField(a.PasswordHash), SafeField(a.FullName))).ToArray(), Encoding.UTF8)
    End Sub

    Public Sub LoadTransactions()
        Transactions = New List(Of POS_Transaction) : Dim ordersPath = FilePathOf("orders.txt")
        If File.Exists(ordersPath) Then
            Dim itemPath = FilePathOf("order_items.txt")
            For Each line In File.ReadAllLines(ordersPath, Encoding.UTF8)
                Dim f = Fields(line) : Dim dt As DateTime : Dim total As Decimal
                If f.Length >= 4 AndAlso DateTime.TryParse(f(2), CultureInfo.InvariantCulture, DateTimeStyles.None, dt) AndAlso Decimal.TryParse(f(3), NumberStyles.Number, CultureInfo.InvariantCulture, total) Then
                    Dim t As New POS_Transaction With {.TransactionID = f(0), .Cashier = f(1), .CashierUsername = f(1), .TransactionDate = dt, .Total = total, .Subtotal = total, .Status = TransactionStatus.Completed}
                    If File.Exists(itemPath) Then
                        For Each il In File.ReadAllLines(itemPath, Encoding.UTF8)
                            Dim it = Fields(il) : Dim qty As Integer : Dim price As Decimal
                            If it.Length >= 5 AndAlso it(0) = t.TransactionID AndAlso Integer.TryParse(it(2), qty) AndAlso Decimal.TryParse(it(3), NumberStyles.Number, CultureInfo.InvariantCulture, price) Then t.Items.Add(New TransactionItem With {.ProductName = it(1), .Quantity = qty, .Price = price})
                        Next
                    End If
                    Transactions.Add(t)
                End If
            Next
        End If
    End Sub

    Public Sub SaveTransactions()
        File.WriteAllLines(FilePathOf("orders.txt"), Transactions.Select(Function(t) String.Join("|", SafeField(t.TransactionID), SafeField(t.Cashier), t.TransactionDate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), t.Total.ToString(CultureInfo.InvariantCulture))).ToArray(), Encoding.UTF8)
        File.WriteAllLines(FilePathOf("order_items.txt"), Transactions.SelectMany(Function(t) t.Items.Select(Function(i) String.Join("|", SafeField(t.TransactionID), SafeField(i.ProductName), i.Quantity, i.Price.ToString(CultureInfo.InvariantCulture), i.LineTotal.ToString(CultureInfo.InvariantCulture)))).ToArray(), Encoding.UTF8)
        File.WriteAllLines(FilePathOf("sales.txt"), Transactions.Where(Function(t) t.Status = TransactionStatus.Completed).GroupBy(Function(t) t.TransactionDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)).OrderBy(Function(g) g.Key).Select(Function(g) g.Key & "|" & g.Sum(Function(t) t.Total).ToString(CultureInfo.InvariantCulture)).ToArray(), Encoding.UTF8)
    End Sub

    Public Sub LoadMessages()
        ChatMessages = New List(Of ChatMessage)()
        Dim path = FilePathOf("messages.txt")
        If Not File.Exists(path) Then Return
        For Each line In File.ReadAllLines(path, Encoding.UTF8)
            Dim f = Fields(line) : Dim sent As DateTime
            If f.Length >= 4 AndAlso DateTime.TryParse(f(3), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, sent) Then ChatMessages.Add(New ChatMessage With {.Sender = f(0), .Receiver = f(1), .Message = f(2), .TimeSent = sent})
        Next
    End Sub

    Public Sub SaveMessages()
        File.WriteAllLines(FilePathOf("messages.txt"), ChatMessages.Select(Function(m) String.Join("|", SafeField(m.Sender), SafeField(m.Receiver), SafeField(m.Message), m.TimeSent.ToString("o", CultureInfo.InvariantCulture))).ToArray(), Encoding.UTF8)
    End Sub

    Public Sub LoadPriceLogs()
        PriceLogs = New List(Of PriceChangeLog)()
        Dim path = FilePathOf("price_log.txt")
        If Not File.Exists(path) Then Return
        For Each line In File.ReadAllLines(path, Encoding.UTF8)
            Dim f = Fields(line) : Dim oldPrice As Decimal : Dim newPrice As Decimal : Dim changed As DateTime
            If f.Length >= 6 AndAlso Decimal.TryParse(f(2), NumberStyles.Number, CultureInfo.InvariantCulture, oldPrice) AndAlso Decimal.TryParse(f(3), NumberStyles.Number, CultureInfo.InvariantCulture, newPrice) AndAlso DateTime.TryParse(f(5), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, changed) Then PriceLogs.Add(New PriceChangeLog With {.ProductId = f(0), .ProductName = f(1), .OldPrice = oldPrice, .NewPrice = newPrice, .ChangedBy = f(4), .ChangedDate = changed})
        Next
    End Sub

    Public Sub SavePriceLogs()
        File.WriteAllLines(FilePathOf("price_log.txt"), PriceLogs.Select(Function(x) String.Join("|", SafeField(x.ProductId), SafeField(x.ProductName), x.OldPrice.ToString(CultureInfo.InvariantCulture), x.NewPrice.ToString(CultureInfo.InvariantCulture), SafeField(x.ChangedBy), x.ChangedDate.ToString("o", CultureInfo.InvariantCulture))).ToArray(), Encoding.UTF8)
    End Sub

    '------------------------------------------------------------------
    ' Default menu used only when products.txt does not exist yet.
    '------------------------------------------------------------------
    Private Function BuildDefaultProducts() As List(Of Product)
        Dim list As New List(Of Product)
        Dim seed As String(,) = {
            {"Cappuccino", "140", "45", "Hot Coffee", "Espresso with steamed milk and foam."},
            {"Americano", "100", "62", "Hot Coffee", "Espresso diluted with hot water."},
            {"Caffe Latte", "160", "30", "Hot Coffee", "Espresso with plenty of steamed milk."},
            {"Mocha", "170", "0", "Specialty", "Espresso, chocolate and steamed milk."},
            {"Caramel Macchiato", "150", "3", "Specialty", "Vanilla, milk, espresso and caramel drizzle."},
            {"Iced Coffee", "110", "8", "Iced Coffee", "Chilled brewed coffee over ice."}
        }
        For i As Integer = 0 To seed.GetLength(0) - 1
            Dim stockValue As Integer = Integer.Parse(seed(i, 2), CultureInfo.InvariantCulture)
            list.Add(New Product With {
                .Id = "PRD-" & PosSettings.NextProductNumber.ToString("D3"),
                .Name = seed(i, 0),
                .Price = Decimal.Parse(seed(i, 1), CultureInfo.InvariantCulture),
                .Stock = stockValue,
                .Category = seed(i, 3),
                .Description = seed(i, 4),
                .Status = If(stockValue > 0, "Available", "Unavailable"),
                .LastUpdated = DateTime.Now
            })
            PosSettings.NextProductNumber += 1
        Next
        SaveSettings()
        Return list
    End Function

    ''' <summary>Makes sure counters never fall behind the data already stored.</summary>
    Private Sub SyncCounters()
        Dim maxTrx As Integer = 0
        For Each t As POS_Transaction In Transactions
            maxTrx = Math.Max(maxTrx, NumberFromId(t.TransactionID))
        Next
        If PosSettings.NextTransactionNumber <= maxTrx Then
            PosSettings.NextTransactionNumber = maxTrx + 1
        End If

        Dim maxPrd As Integer = 0
        For Each p As Product In Products
            If String.IsNullOrWhiteSpace(p.Id) Then
                p.Id = "PRD-" & PosSettings.NextProductNumber.ToString("D3")
                PosSettings.NextProductNumber += 1
            End If
            maxPrd = Math.Max(maxPrd, NumberFromId(p.Id))
        Next
        If PosSettings.NextProductNumber <= maxPrd Then
            PosSettings.NextProductNumber = maxPrd + 1
        End If

        Dim maxCsh As Integer = 0
        For Each c As CashierAccount In Cashiers
            maxCsh = Math.Max(maxCsh, NumberFromId(c.Id))
        Next
        If PosSettings.NextCashierNumber <= maxCsh Then
            PosSettings.NextCashierNumber = maxCsh + 1
        End If

        Try
            SaveSettings()
        Catch
        End Try
    End Sub

    Private Function NumberFromId(id As String) As Integer
        If String.IsNullOrEmpty(id) Then Return 0
        Dim digits As New StringBuilder()
        For Each ch As Char In id
            If Char.IsDigit(ch) Then digits.Append(ch)
        Next
        Dim n As Integer
        If Integer.TryParse(digits.ToString(), n) Then Return n
        Return 0
    End Function

    '==================================================================
    ' IMAGES  (stored as files, only the file name is saved in JSON)
    '==================================================================
    ''' <summary>Loads an image WITHOUT keeping the file locked.</summary>
    Public Function LoadImageCopy(fullPath As String) As Image
        Try
            If String.IsNullOrWhiteSpace(fullPath) OrElse Not File.Exists(fullPath) Then Return Nothing
            Using fs As New FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read)
                Using img As Image = Image.FromStream(fs)
                    Return New Bitmap(img)
                End Using
            End Using
        Catch
            Return Nothing
        End Try
    End Function

    Private Sub LoadProductImage(p As Product)
        ' older products.txt files did not store the picture name: look for the file saved as product_<number>.*
        If String.IsNullOrWhiteSpace(p.ImagePath) Then
            Try
                Dim found As String() = Directory.GetFiles(ImageFolder, "product_" & NumberFromId(p.Id).ToString("D3") & ".*")
                If found.Length > 0 Then p.ImagePath = Path.GetFileName(found(0))
            Catch
            End Try
        End If

        If String.IsNullOrWhiteSpace(p.ImagePath) Then
            p.Image = Nothing
        Else
            p.Image = LoadImageCopy(Path.Combine(ImageFolder, p.ImagePath))
        End If
    End Sub

    Private Sub StoreProductImage(p As Product, sourcePath As String)
        If String.IsNullOrWhiteSpace(sourcePath) OrElse Not File.Exists(sourcePath) Then Return

        Dim ext As String = Path.GetExtension(sourcePath).ToLowerInvariant()
        If ext = "" Then ext = ".jpg"
        Dim newName As String = "product_" & NumberFromId(p.Id).ToString("D3") & ext
        Dim dest As String = Path.Combine(ImageFolder, newName)

        Directory.CreateDirectory(ImageFolder)
        If Not String.Equals(Path.GetFullPath(sourcePath), Path.GetFullPath(dest), StringComparison.OrdinalIgnoreCase) Then
            File.Copy(sourcePath, dest, True)
        End If

        ' remove the old file if the extension changed
        If Not String.IsNullOrWhiteSpace(p.ImagePath) AndAlso
           Not String.Equals(p.ImagePath, newName, StringComparison.OrdinalIgnoreCase) Then
            Try
                File.Delete(Path.Combine(ImageFolder, p.ImagePath))
            Catch
            End Try
        End If

        p.ImagePath = newName
        p.Image = LoadImageCopy(dest)
    End Sub

    '==================================================================
    ' STOCK RULES (single place for status logic)
    '==================================================================
    Public Function GetMinStock(p As Product) As Integer
        If p.MinStock > 0 Then Return p.MinStock
        Return PosSettings.DefaultMinStock
    End Function

    Public Function GetProductStockStatus(p As Product) As String
        If p.Stock <= 0 Then Return StockStatus.OutOfStock
        If p.Stock <= GetMinStock(p) Then Return StockStatus.LowStock
        Return StockStatus.InStock
    End Function

    Public Function GetLowStockProducts() As List(Of Product)
        Dim result As New List(Of Product)
        For Each p As Product In Products
            If GetProductStockStatus(p) <> StockStatus.InStock Then result.Add(p)
        Next
        Return result
    End Function

    Public Function GetLowStockCount() As Integer
        Return GetLowStockProducts().Count
    End Function

    Public Function GetSuggestedRestockQty(p As Product) As Integer
        Dim minimum As Integer = GetMinStock(p)
        Dim needed As Integer = Math.Max(minimum * 2 - p.Stock, minimum)
        Return CInt(Math.Ceiling(needed / 5.0R)) * 5
    End Function

    Public Sub SetDefaultMinStock(value As Integer)
        If value < 1 Then value = 1
        PosSettings.DefaultMinStock = value
        Try
            SaveSettings()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "BrewPoint POS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        RaiseEvent ProductsChanged(Nothing, EventArgs.Empty)
    End Sub

    Private Sub SyncAvailability(p As Product)
        p.Status = If(p.Stock > 0, "Available", "Unavailable")
        p.LastUpdated = DateTime.Now
    End Sub

    '==================================================================
    ' PRODUCT CRUD
    '==================================================================
    Public Function AddProduct(p As Product, Optional imageSourcePath As String = "") As Boolean
        Try
            Do
                p.Id = "PRD-" & PosSettings.NextProductNumber.ToString("D3")
                PosSettings.NextProductNumber += 1
            Loop While Products.Exists(Function(x) x.Id = p.Id)

            If Not String.IsNullOrWhiteSpace(imageSourcePath) Then StoreProductImage(p, imageSourcePath)
            SyncAvailability(p)
            Products.Add(p)

            SaveProducts()
            SaveSettings()
            RaiseEvent ProductsChanged(Nothing, EventArgs.Empty)
            Return True
        Catch ex As Exception
            Products.Remove(p)
            MessageBox.Show(ex.Message, "BrewPoint POS", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Call AFTER changing the product's properties. oldPrice is the price before the edit,
    ''' it is used for the price-change audit log.
    ''' </summary>
    Public Function UpdateProduct(p As Product, oldPrice As Decimal,
                                  Optional imageSourcePath As String = "",
                                  Optional changedBy As String = "") As Boolean
        Try
            If Not String.IsNullOrWhiteSpace(imageSourcePath) Then StoreProductImage(p, imageSourcePath)
            SyncAvailability(p)

            If oldPrice <> p.Price Then
                PriceLogs.Add(New PriceChangeLog With {
                    .ProductId = p.Id, .ProductName = p.Name,
                    .OldPrice = oldPrice, .NewPrice = p.Price,
                    .ChangedBy = changedBy, .ChangedDate = DateTime.Now})
                SavePriceLogs()
            End If

            SaveProducts()
            RaiseEvent ProductsChanged(Nothing, EventArgs.Empty)
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "BrewPoint POS", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    ''' <summary>Removes the product from the CATALOG only. Transactions are untouched.</summary>
    Public Function DeleteProduct(p As Product) As Boolean
        Try
            Products.Remove(p)
            SaveProducts()
            If Not String.IsNullOrWhiteSpace(p.ImagePath) Then
                Try
                    File.Delete(Path.Combine(ImageFolder, p.ImagePath))
                Catch
                End Try
            End If
            RaiseEvent ProductsChanged(Nothing, EventArgs.Empty)
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "BrewPoint POS", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Public Function RestockProduct(p As Product, quantity As Integer) As Boolean
        If quantity <= 0 Then Return False
        Try
            p.Stock += quantity
            SyncAvailability(p)
            SaveProducts()
            RaiseEvent ProductsChanged(Nothing, EventArgs.Empty)
            Return True
        Catch ex As Exception
            p.Stock -= quantity
            MessageBox.Show(ex.Message, "BrewPoint POS", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Public Function FindProduct(id As String, name As String) As Product
        If Not String.IsNullOrWhiteSpace(id) Then
            For Each p As Product In Products
                If String.Equals(p.Id, id, StringComparison.OrdinalIgnoreCase) Then Return p
            Next
        End If
        For Each p As Product In Products
            If String.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase) Then Return p
        Next
        Return Nothing
    End Function

    '==================================================================
    ' ACCOUNTS
    '==================================================================
    Public Function UsernameExists(username As String, Optional excludeCashierId As String = "") As Boolean
        Dim u As String = username.Trim()
        For Each c As CashierAccount In Cashiers
            If c.Id <> excludeCashierId AndAlso String.Equals(c.Username, u, StringComparison.OrdinalIgnoreCase) Then Return True
        Next
        For Each a As AdminAccount In Admins
            If String.Equals(a.Username, u, StringComparison.OrdinalIgnoreCase) Then Return True
        Next
        Return False
    End Function

    Public Function FindCashierById(id As String) As CashierAccount
        For Each c As CashierAccount In Cashiers
            If c.Id = id Then Return c
        Next
        Return Nothing
    End Function

    Public Function AddCashier(fullName As String, username As String, password As String,
                               status As String, ByRef errorMessage As String) As Boolean
        errorMessage = ""
        If String.IsNullOrWhiteSpace(fullName) Then errorMessage = "Full name is required." : Return False
        If String.IsNullOrWhiteSpace(username) Then errorMessage = "Username is required." : Return False
        If String.IsNullOrEmpty(password) Then errorMessage = "Password is required." : Return False
        If UsernameExists(username) Then errorMessage = "That username is already in use." : Return False

        Dim acc As New CashierAccount With {
            .Id = "CSH-" & PosSettings.NextCashierNumber.ToString("D3"),
            .FullName = fullName.Trim(),
            .Username = username.Trim(),
            .PasswordHash = password,
            .Status = status,
            .DateAdded = DateTime.Now
        }
        PosSettings.NextCashierNumber += 1
        Cashiers.Add(acc)
        Try
            SaveCashiers()
            SaveSettings()
        Catch ex As Exception
            Cashiers.Remove(acc)
            errorMessage = ex.Message
            Return False
        End Try
        RaiseEvent CashiersChanged(Nothing, EventArgs.Empty)
        Return True
    End Function

    ''' <summary>newPassword may be empty = keep the current password.</summary>
    Public Function UpdateCashier(id As String, fullName As String, username As String,
                                  newPassword As String, status As String,
                                  ByRef errorMessage As String) As Boolean
        errorMessage = ""
        Dim acc As CashierAccount = FindCashierById(id)
        If acc Is Nothing Then errorMessage = "Cashier not found." : Return False
        If String.IsNullOrWhiteSpace(fullName) Then errorMessage = "Full name is required." : Return False
        If String.IsNullOrWhiteSpace(username) Then errorMessage = "Username is required." : Return False
        If UsernameExists(username, id) Then errorMessage = "That username is already in use." : Return False

        Dim oldName As String = acc.FullName
        Dim oldUser As String = acc.Username
        Dim oldHash As String = acc.PasswordHash
        Dim oldSalt As String = acc.PasswordSalt
        Dim oldStatus As String = acc.Status

        acc.FullName = fullName.Trim()
        acc.Username = username.Trim()
        acc.Status = status
        If Not String.IsNullOrEmpty(newPassword) Then
            acc.PasswordHash = newPassword
        End If

        Try
            SaveCashiers()
        Catch ex As Exception
            acc.FullName = oldName : acc.Username = oldUser
            acc.PasswordHash = oldHash : acc.PasswordSalt = oldSalt : acc.Status = oldStatus
            errorMessage = ex.Message
            Return False
        End Try
        RaiseEvent CashiersChanged(Nothing, EventArgs.Empty)
        Return True
    End Function

    Public Function SetCashierStatus(id As String, status As String, ByRef errorMessage As String) As Boolean
        errorMessage = ""
        Dim acc As CashierAccount = FindCashierById(id)
        If acc Is Nothing Then errorMessage = "Cashier not found." : Return False
        Dim old As String = acc.Status
        acc.Status = status
        Try
            SaveCashiers()
        Catch ex As Exception
            acc.Status = old
            errorMessage = ex.Message
            Return False
        End Try
        RaiseEvent CashiersChanged(Nothing, EventArgs.Empty)
        Return True
    End Function

    Public Function DeleteCashier(id As String, ByRef errorMessage As String) As Boolean
        errorMessage = ""
        Dim acc As CashierAccount = FindCashierById(id)
        If acc Is Nothing Then errorMessage = "Cashier not found." : Return False
        Cashiers.Remove(acc)
        Try
            SaveCashiers()
        Catch ex As Exception
            Cashiers.Add(acc)
            errorMessage = ex.Message
            Return False
        End Try
        RaiseEvent CashiersChanged(Nothing, EventArgs.Empty)
        Return True
    End Function

    '------------------------------------------------------------------
    ' LOGIN
    '------------------------------------------------------------------
    Public Function Authenticate(username As String, password As String,
                                 role As String, ByRef message As String) As Boolean
        message = ""
        Dim u As String = username.Trim()

        If role = "admin" Then
            For Each a As AdminAccount In Admins
                If String.Equals(a.Username, u, StringComparison.OrdinalIgnoreCase) Then
                    If Not String.Equals(password, a.PasswordHash, StringComparison.Ordinal) Then Exit For
                    If Not a.IsActive Then
                        message = "Your administrator account is currently inactive."
                        Return False
                    End If
                    a.LastLogin = DateTime.Now
                    Try
                        SaveAdmins()
                    Catch
                    End Try
                    CurrentSession.Start(a.Id, a.Username, a.FullName, "admin")
                    Return True
                End If
            Next
        Else
            For Each c As CashierAccount In Cashiers
                If String.Equals(c.Username, u, StringComparison.OrdinalIgnoreCase) Then
                    If Not String.Equals(password, c.PasswordHash, StringComparison.Ordinal) Then Exit For
                    If Not c.IsActive Then
                        message = "Your cashier account is currently inactive." & vbCrLf &
                                  "Please contact the administrator."
                        Return False
                    End If
                    c.LastLogin = DateTime.Now
                    Try
                        SaveCashiers()
                    Catch
                    End Try
                    CurrentSession.Start(c.Id, c.Username, c.FullName, "cashier")
                    Return True
                End If
            Next
        End If

        message = "Invalid username or password."
        Return False
    End Function

    '==================================================================
    ' MESSAGES
    '==================================================================
    Public Sub AddMessage(msg As ChatMessage)
        ChatMessages.Add(msg)
        Try
            SaveMessages()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "BrewPoint POS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        RaiseEvent MessagesChanged(Nothing, EventArgs.Empty)
    End Sub

    '==================================================================
    ' TRANSACTIONS
    '==================================================================
    Private Function NextTransactionId() As String
        Dim id As String
        Do
            id = "TRX-" & PosSettings.NextTransactionNumber.ToString("D4")
            PosSettings.NextTransactionNumber += 1
        Loop While Transactions.Exists(Function(t) t.TransactionID = id)
        Return id
    End Function

    ''' <summary>Compatibility wrapper: stores a ready-made transaction.</summary>
    Public Sub AddTransaction(transaction As POS_Transaction)
        transaction.TransactionID = NextTransactionId()
        Transactions.Add(transaction)
        SaveTransactions()
        SaveSettings()
        RaiseEvent TransactionsChanged(Nothing, EventArgs.Empty)
    End Sub

    Public Sub UpdateTransaction(transaction As POS_Transaction)
        SaveTransactions()
        RaiseEvent TransactionsChanged(Nothing, EventArgs.Empty)
    End Sub

    ''' <summary>
    ''' Complete checkout in one safe step: validates, deducts stock, snapshots prices,
    ''' creates the transaction and saves everything. Returns Nothing + errorMessage on failure.
    ''' </summary>
    Public Function CreateSale(cashierName As String, cashierUsername As String,
                               lines As List(Of KeyValuePair(Of Product, Integer)),
                               paymentMethod As String, cashReceived As Decimal,
                               ByRef errorMessage As String) As POS_Transaction
        errorMessage = ""
        If lines Is Nothing OrElse lines.Count = 0 Then
            errorMessage = "Your cart is empty."
            Return Nothing
        End If
        If paymentMethod <> PaymentMethods.Cash AndAlso paymentMethod <> PaymentMethods.Card Then
            errorMessage = "Invalid payment method."
            Return Nothing
        End If

        Dim subtotal As Decimal = 0D
        For Each line As KeyValuePair(Of Product, Integer) In lines
            If Not Products.Contains(line.Key) Then
                errorMessage = """" & line.Key.Name & """ is no longer available in the product catalog."
                Return Nothing
            End If
            If line.Value <= 0 Then
                errorMessage = "Invalid quantity for " & line.Key.Name & "."
                Return Nothing
            End If
            If line.Value > line.Key.Stock Then
                errorMessage = "Not enough stock for " & line.Key.Name & " (available: " & line.Key.Stock & ")."
                Return Nothing
            End If
            subtotal += line.Key.Price * line.Value
        Next

        Dim tax As Decimal = Math.Round(subtotal * PosSettings.TaxRate, 2, MidpointRounding.AwayFromZero)
        Dim total As Decimal = subtotal + tax

        If paymentMethod = PaymentMethods.Cash AndAlso cashReceived < total Then
            errorMessage = "Cash received is not enough."
            Return Nothing
        End If

        Dim trx As New POS_Transaction With {
            .TransactionDate = DateTime.Now,
            .Cashier = cashierName,
            .CashierUsername = cashierUsername,
            .PaymentMethod = paymentMethod,
            .Subtotal = subtotal,
            .Tax = tax,
            .Total = total,
            .CashReceived = If(paymentMethod = PaymentMethods.Cash, cashReceived, 0D),
            .ChangeGiven = If(paymentMethod = PaymentMethods.Cash, cashReceived - total, 0D),
            .Status = TransactionStatus.Completed
        }

        Dim originalStocks As New Dictionary(Of Product, Integer)
        For Each line As KeyValuePair(Of Product, Integer) In lines
            trx.Items.Add(New TransactionItem With {
                .ProductId = line.Key.Id,
                .ProductName = line.Key.Name,
                .Quantity = line.Value,
                .Price = line.Key.Price          ' PRICE SNAPSHOT
            })
            originalStocks(line.Key) = line.Key.Stock
            line.Key.Stock -= line.Value
            SyncAvailability(line.Key)
        Next

        Dim previousNumber As Integer = PosSettings.NextTransactionNumber
        trx.TransactionID = NextTransactionId()
        Transactions.Add(trx)

        Try
            SaveProducts()
            SaveTransactions()
            SaveSettings()
        Catch ex As Exception
            ' roll back memory so nothing is half-saved
            Transactions.Remove(trx)
            PosSettings.NextTransactionNumber = previousNumber
            For Each kv As KeyValuePair(Of Product, Integer) In originalStocks
                kv.Key.Stock = kv.Value
                SyncAvailability(kv.Key)
            Next
            errorMessage = "The sale could not be saved: " & ex.Message
            Return Nothing
        End Try

        RaiseEvent ProductsChanged(Nothing, EventArgs.Empty)
        RaiseEvent TransactionsChanged(Nothing, EventArgs.Empty)
        Return trx
    End Function

    Public Function FindTransaction(id As String) As POS_Transaction
        For Each t As POS_Transaction In Transactions
            If String.Equals(t.TransactionID, id, StringComparison.OrdinalIgnoreCase) Then Return t
        Next
        Return Nothing
    End Function

    Public Function RefundTransaction(id As String, changedBy As String, ByRef errorMessage As String) As Boolean
        Return ChangeTransactionStatus(id, TransactionStatus.Refunded, changedBy, errorMessage)
    End Function

    Public Function VoidTransaction(id As String, changedBy As String, ByRef errorMessage As String) As Boolean
        Return ChangeTransactionStatus(id, TransactionStatus.Voided, changedBy, errorMessage)
    End Function

    ''' <summary>Refund/void keep the record, restore stock, and are only allowed once.</summary>
    Private Function ChangeTransactionStatus(id As String, newStatus As String, changedBy As String,
                                             ByRef errorMessage As String) As Boolean
        errorMessage = ""
        Dim trx As POS_Transaction = FindTransaction(id)
        If trx Is Nothing Then errorMessage = "Transaction not found." : Return False
        If trx.Status <> TransactionStatus.Completed Then
            errorMessage = "Only completed transactions can be changed. This one is already " & trx.Status & "."
            Return False
        End If

        Dim restored As New Dictionary(Of Product, Integer)
        For Each item As TransactionItem In trx.Items
            Dim p As Product = FindProduct(item.ProductId, item.ProductName)
            If p IsNot Nothing Then
                If Not restored.ContainsKey(p) Then restored(p) = 0
                restored(p) += item.Quantity
            End If
        Next

        Dim oldStatus As String = trx.Status
        trx.Status = newStatus
        trx.StatusChangedBy = changedBy
        trx.StatusChangedDate = DateTime.Now
        For Each kv As KeyValuePair(Of Product, Integer) In restored
            kv.Key.Stock += kv.Value
            SyncAvailability(kv.Key)
        Next

        Try
            SaveProducts()
            SaveTransactions()
        Catch ex As Exception
            trx.Status = oldStatus
            trx.StatusChangedBy = ""
            trx.StatusChangedDate = Nothing
            For Each kv As KeyValuePair(Of Product, Integer) In restored
                kv.Key.Stock -= kv.Value
                SyncAvailability(kv.Key)
            Next
            errorMessage = ex.Message
            Return False
        End Try

        RaiseEvent ProductsChanged(Nothing, EventArgs.Empty)
        RaiseEvent TransactionsChanged(Nothing, EventArgs.Empty)
        Return True
    End Function

    '==================================================================
    ' TRANSACTION QUERIES  (all filtering lives here, not in the forms)
    '==================================================================
    Public Function GetWeekStart(d As DateTime) As DateTime
        Dim diff As Integer = (CInt(d.DayOfWeek) + 6) Mod 7        ' Monday = 0
        Return d.Date.AddDays(-diff)
    End Function

    Public Function GetTransactionsInRange(fromInclusive As DateTime, toExclusive As DateTime) As List(Of POS_Transaction)
        Dim result As New List(Of POS_Transaction)
        For Each t As POS_Transaction In Transactions
            If t.TransactionDate >= fromInclusive AndAlso t.TransactionDate < toExclusive Then result.Add(t)
        Next
        result.Sort(Function(a, b) b.TransactionDate.CompareTo(a.TransactionDate))
        Return result
    End Function

    Public Function GetTransactionsByDate(dateValue As DateTime) As List(Of POS_Transaction)
        Return GetTransactionsInRange(dateValue.Date, dateValue.Date.AddDays(1))
    End Function

    Public Function GetDailyTransactions(dateValue As DateTime) As List(Of POS_Transaction)
        Return GetTransactionsByDate(dateValue)
    End Function

    Public Function GetWeeklyTransactions(dateValue As DateTime) As List(Of POS_Transaction)
        Dim start As DateTime = GetWeekStart(dateValue)
        Return GetTransactionsInRange(start, start.AddDays(7))
    End Function

    Public Function GetMonthlyTransactions(year As Integer, month As Integer) As List(Of POS_Transaction)
        Dim start As New DateTime(year, month, 1)
        Return GetTransactionsInRange(start, start.AddMonths(1))
    End Function

    Public Function GetTransactionsByStatus(status As String) As List(Of POS_Transaction)
        Dim result As New List(Of POS_Transaction)
        For Each t As POS_Transaction In Transactions
            If String.Equals(t.Status, status, StringComparison.OrdinalIgnoreCase) Then result.Add(t)
        Next
        result.Sort(Function(a, b) b.TransactionDate.CompareTo(a.TransactionDate))
        Return result
    End Function

    ''' <summary>Searches transaction ID, cashier and product names.</summary>
    Public Function SearchTransactions(searchText As String) As List(Of POS_Transaction)
        Dim result As New List(Of POS_Transaction)
        Dim q As String = searchText.Trim()
        For Each t As POS_Transaction In Transactions
            If TransactionMatches(t, q) Then result.Add(t)
        Next
        result.Sort(Function(a, b) b.TransactionDate.CompareTo(a.TransactionDate))
        Return result
    End Function

    Private Function TransactionMatches(t As POS_Transaction, q As String) As Boolean
        If q = "" Then Return True
        If t.TransactionID.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0 Then Return True
        If t.Cashier IsNot Nothing AndAlso t.Cashier.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0 Then Return True
        For Each item As TransactionItem In t.Items
            If item.ProductName.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0 Then Return True
        Next
        Return False
    End Function

    ''' <summary>period: "Daily", "Weekly", "Monthly" or "All". status: "All Statuses" or a status name.</summary>
    Public Function QueryTransactions(period As String, anchor As DateTime,
                                      status As String, searchText As String) As List(Of POS_Transaction)
        Dim source As List(Of POS_Transaction)
        Select Case period
            Case "Daily" : source = GetDailyTransactions(anchor)
            Case "Weekly" : source = GetWeeklyTransactions(anchor)
            Case "Monthly" : source = GetMonthlyTransactions(anchor.Year, anchor.Month)
            Case Else
                source = New List(Of POS_Transaction)(Transactions)
                source.Sort(Function(a, b) b.TransactionDate.CompareTo(a.TransactionDate))
        End Select

        Dim q As String = If(searchText, "").Trim()
        Dim filterStatus As Boolean = Not String.IsNullOrEmpty(status) AndAlso
                                      Not status.StartsWith("All", StringComparison.OrdinalIgnoreCase)
        Dim result As New List(Of POS_Transaction)
        For Each t As POS_Transaction In source
            If filterStatus AndAlso Not String.Equals(t.Status, status, StringComparison.OrdinalIgnoreCase) Then Continue For
            If Not TransactionMatches(t, q) Then Continue For
            result.Add(t)
        Next
        Return result
    End Function

    '==================================================================
    ' REPORTING  (only Completed transactions count as sales)
    '==================================================================
    Public Function GetSalesSummary(list As IEnumerable(Of POS_Transaction)) As SalesSummary
        Dim s As New SalesSummary
        For Each t As POS_Transaction In list
            If t.Status = TransactionStatus.Completed Then
                s.TransactionCount += 1
                s.TotalSales += t.Total
            End If
        Next
        Return s
    End Function

    Public Function GetTodayCompletedTransactions() As List(Of POS_Transaction)
        Dim result As New List(Of POS_Transaction)
        For Each t As POS_Transaction In GetDailyTransactions(DateTime.Today)
            If t.Status = TransactionStatus.Completed Then result.Add(t)
        Next
        Return result
    End Function

    Public Function GetTodaySales() As Decimal
        Return GetSalesSummary(GetDailyTransactions(DateTime.Today)).TotalSales
    End Function

    Public Function GetTodayOrderCount() As Integer
        Return GetSalesSummary(GetDailyTransactions(DateTime.Today)).TransactionCount
    End Function

    Public Function GetTodayAverageOrder() As Decimal
        Return GetSalesSummary(GetDailyTransactions(DateTime.Today)).AverageOrder
    End Function

    Public Function BuildItemsText(t As POS_Transaction) As String
        Dim sb As New StringBuilder()
        For Each item As TransactionItem In t.Items
            If sb.Length > 0 Then sb.Append(", ")
            sb.Append(item.Quantity).Append("x ").Append(item.ProductName)
        Next
        Return sb.ToString()
    End Function

End Module

'=====================================================================
' CURRENT LOGGED-IN USER
'=====================================================================
Public Module CurrentSession

    Public Property UserId As String = ""
    Public Property Username As String = ""
    Public Property FullName As String = ""
    Public Property Role As String = ""

    Public Sub Start(id As String, user As String, name As String, roleName As String)
        UserId = id
        Username = user
        FullName = name
        Role = roleName
    End Sub

    Public Sub Clear()
        UserId = ""
        Username = ""
        FullName = ""
        Role = ""
    End Sub

End Module

'=====================================================================
' SMALL UI HELPERS SHARED BY ALL FORMS
'=====================================================================
Public Module UiHelpers

    Public Function Peso(value As Decimal) As String
        Return ChrW(&H20B1) & value.ToString("N2", CultureInfo.InvariantCulture)
    End Function

    ''' <summary>Parses "1,234.50", "₱ 100", "100" etc.</summary>
    Public Function TryParseMoney(text As String, ByRef value As Decimal) As Boolean
        value = 0D
        If String.IsNullOrWhiteSpace(text) Then Return False
        Dim cleaned As String = text.Replace(ChrW(&H20B1), "").Replace("PHP", "").Replace(" ", "").Trim()
        If Decimal.TryParse(cleaned, NumberStyles.Number, CultureInfo.CurrentCulture, value) Then Return True
        Return Decimal.TryParse(cleaned, NumberStyles.Number, CultureInfo.InvariantCulture, value)
    End Function

    Public Function StatusColor(status As String) As Color
        Select Case status
            Case TransactionStatus.Completed, StockStatus.InStock, AccountStatus.Active
                Return Color.FromArgb(46, 125, 50)
            Case TransactionStatus.Refunded, StockStatus.LowStock
                Return Color.FromArgb(239, 108, 0)
            Case Else
                Return Color.FromArgb(198, 40, 40)
        End Select
    End Function

End Module

'=====================================================================
' NAVIGATION (logout returns to the ONE hidden login page)
'=====================================================================
Public Module AppNavigation

    Public Sub ShowLogin()
        CurrentSession.Clear()
        For Each f As Form In Application.OpenForms
            Dim login As LoginPage = TryCast(f, LoginPage)
            If login IsNot Nothing Then
                login.ResetAndShow()
                Return
            End If
        Next
        Dim fresh As New LoginPage()
        fresh.Show()
    End Sub

End Module