Option Strict On
Option Explicit On

' PHASE 1 (new file).  Product -> Variant/Size -> Recipe.
' Latte Small (P100) and Latte Large (P130) are two variants with
' DIFFERENT recipes. Products without sizes (Bread) get one variant,
' Size = "Regular".
Public Class ProductVariant

    Public Property VariantId As String = Guid.NewGuid().ToString("N")
    Public Property ProductId As String = ""
    Public Property ShopId As String = ""
    Public Property Size As String = "Regular"
    Public Property Price As Decimal = 0D

    Public ReadOnly Property Recipe As New List(Of RecipeItem)()

    Public ReadOnly Property HasRecipe As Boolean
        Get
            Return Recipe.Count > 0
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return Size & " - " & Price.ToString("N2")
    End Function

End Class
