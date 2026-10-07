Option Strict On
Option Explicit On

' PHASE 1 (new file).  Every change to ingredient stock is one record here:
' sales, refunds, restock, wastage, damage, loss, corrections.
' This IS the "inventory history".
Public Class InventoryAdjustment

    Public Property AdjustmentId As String = Guid.NewGuid().ToString("N")
    Public Property ShopId As String = ""
    Public Property IngredientId As String = ""
    Public Property IngredientName As String = ""

    ' Signed, in base units. Milk -500 ml = wastage. Restock = positive.
    Public Property Delta As Decimal = 0D
    Public Property Unit As String = UnitHelper.Gram
    Public Property Reason As String = AdjustmentReason.Correction
    Public Property Note As String = ""
    Public Property RecordedBy As String = ""
    Public Property RecordedAt As DateTime = DateTime.Now

    ' Optional link, e.g. TRX-0001 for Sale / Refund rows.
    Public Property ReferenceId As String = ""

End Class

Public NotInheritable Class AdjustmentReason
    Private Sub New()
    End Sub
    Public Const Sale As String = "Sale"
    Public Const Refund As String = "Refund"
    Public Const Restock As String = "Restock"
    Public Const Wastage As String = "Wastage"
    Public Const Damaged As String = "Damaged"
    Public Const Lost As String = "Lost"
    Public Const Correction As String = "Correction"
End Class
