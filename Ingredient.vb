Option Strict On
Option Explicit On

' PHASE 1 (new file).  INGREDIENT INVENTORY item (raw material).
' NOT a POS product. Always belongs to exactly one shop.
Public Class Ingredient

    Public Property IngredientId As String = Guid.NewGuid().ToString("N")
    Public Property ShopId As String = ""
    Public Property Name As String = ""

    ' Base unit only: "g", "ml" or "pcs"  (see UnitHelper)
    Public Property Unit As String = UnitHelper.Gram
    Public Property CurrentStock As Decimal = 0D
    Public Property MinimumStock As Decimal = 0D
    Public Property LastUpdated As DateTime = DateTime.Now

    Public ReadOnly Property Status As String
        Get
            If CurrentStock <= 0D Then Return IngredientStatus.OutOfStock
            If CurrentStock <= MinimumStock Then Return IngredientStatus.LowStock
            Return IngredientStatus.InStock
        End Get
    End Property

    Public ReadOnly Property StockText As String
        Get
            Return UnitHelper.Display(CurrentStock, Unit)
        End Get
    End Property

    Public ReadOnly Property MinimumText As String
        Get
            Return UnitHelper.Display(MinimumStock, Unit)
        End Get
    End Property

End Class

Public NotInheritable Class IngredientStatus
    Private Sub New()
    End Sub
    Public Const InStock As String = "In Stock"
    Public Const LowStock As String = "Low Stock"
    Public Const OutOfStock As String = "Out of Stock"
End Class
