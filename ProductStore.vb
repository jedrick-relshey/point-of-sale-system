Option Strict On
Option Explicit On

Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms

' ============================================================
'  ProductStore.vb
'  Holds the products + their variants (sizes/prices) in memory
'  and saves the product picture to  Data\Images\<Id>.png
'  (same convention as the comment in your Product.vb).
'
'  >>> To save to your database / file, put your code in Persist().
'  >>> To load existing products on startup, fill
'      ProductStore.Products and ProductStore.Variants, then call
'      ProductPageControl.RefreshList().
' ============================================================
Public Module ProductStore

    Public ReadOnly Products As New List(Of Product)()
    Public ReadOnly Variants As New List(Of ProductVariant)()

    ' Put the current shop id here if you use one (copied to every new variant)
    Public Property CurrentShopId As String = ""

    Public Function VariantsOf(productId As String) As List(Of ProductVariant)
        Return Variants.Where(Function(x) x.ProductId = productId).ToList()
    End Function

    ' "Dynamic pricing" = the product has more than one size/price
    Public Function IsDynamic(p As Product) As Boolean
        Return VariantsOf(p.Id).Count > 1
    End Function

    Public Function Categories() As List(Of String)
        Dim list As New List(Of String)()
        For Each p As Product In Products
            If String.IsNullOrWhiteSpace(p.Category) Then Continue For
            Dim c As String = p.Category.Trim()
            If Not list.Any(Function(x) String.Equals(x, c, StringComparison.OrdinalIgnoreCase)) Then list.Add(c)
        Next
        list.Sort(StringComparer.OrdinalIgnoreCase)
        Return list
    End Function

    Public Function PriceText(p As Product) As String
        Dim vs As List(Of ProductVariant) = VariantsOf(p.Id)
        If vs.Count = 0 Then Return Theme.Money(p.Price)

        Dim lo As Decimal = vs.Min(Function(x) x.Price)
        Dim hi As Decimal = vs.Max(Function(x) x.Price)
        If lo = hi Then
            Return If(vs.Count > 1, "From " & Theme.Money(lo), Theme.Money(lo))
        End If
        Return Theme.Money(lo) & ChrW(&H2013).ToString() & Theme.Money(hi)
    End Function

    ' Add a new product, or update an existing one (same object)
    Public Sub SaveProduct(p As Product, newVariants As List(Of ProductVariant))
        If Not Products.Contains(p) Then Products.Add(p)

        Variants.RemoveAll(Function(x) x.ProductId = p.Id)
        For Each v As ProductVariant In newVariants
            v.ProductId = p.Id
            v.ShopId = CurrentShopId
            Variants.Add(v)
        Next

        ' keep Product.Price meaningful for any other code that reads it
        If newVariants.Count > 0 Then
            p.Price = newVariants.Min(Function(x) x.Price)
        End If

        SaveImageFile(p)
        Persist()
    End Sub

    Public Sub DeleteProduct(p As Product)
        Products.Remove(p)
        Variants.RemoveAll(Function(x) x.ProductId = p.Id)
        Try
            Dim f As String = ImagePath(p)
            If File.Exists(f) Then File.Delete(f)
        Catch
            ' ignore - picture file is optional
        End Try
        Persist()
    End Sub

    Public Function ImagePath(p As Product) As String
        Return Path.Combine(Application.StartupPath, "Data", "Images", p.Id & ".png")
    End Function

    ' Call this once at startup if you want pictures loaded from Data\Images
    Public Sub LoadImage(p As Product)
        Try
            Dim f As String = ImagePath(p)
            If File.Exists(f) Then
                Using src As Image = Image.FromFile(f)
                    p.Image = New Bitmap(src)   ' copy so the file isn't locked
                End Using
            End If
        Catch
        End Try
    End Sub

    Private Sub SaveImageFile(p As Product)
        Try
            Dim f As String = ImagePath(p)
            If p.Image Is Nothing Then
                If File.Exists(f) Then File.Delete(f)
                Return
            End If
            Directory.CreateDirectory(Path.GetDirectoryName(f))
            p.Image.Save(f, ImageFormat.Png)
        Catch
            ' picture could not be saved - product is still kept
        End Try
    End Sub

    ' TODO: save Products / Variants to your database or file here.
    Public Sub Persist()
    End Sub

End Module