Option Strict On
Option Explicit On

' PHASE 1 (new file).  One line of a recipe: "Coffee Beans 18 g".
Public Class RecipeItem

    Public Property IngredientId As String = ""

    ' Quantity in the ingredient's BASE unit (g / ml / pcs).
    Public Property Quantity As Decimal = 0D

    Public Property Unit As String = UnitHelper.Gram

    Public Sub New()
    End Sub

    Public Sub New(ingredientId As String, quantity As Decimal, unit As String)
        Me.IngredientId = ingredientId
        Me.Quantity = quantity
        Me.Unit = unit
    End Sub

End Class
