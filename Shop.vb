Option Strict On
Option Explicit On

' PHASE 1 (new file, additive).  A branch is DATA, never hardcoded.
' "Coffee Shop 4" = just another Shop object added at runtime.
Public Class Shop

    Public Property ShopId As String = Guid.NewGuid().ToString("N")
    Public Property ShopName As String = ""
    Public Property BranchCode As String = ""
    Public Property Address As String = ""

    ' Username of the Manager assigned to this shop (one manager per shop).
    Public Property ManagerUsername As String = ""

    Public Property Status As String = ShopStatus.Active
    Public Property CreatedDate As DateTime = DateTime.Now

    Public ReadOnly Property IsActive As Boolean
        Get
            Return String.Equals(Status, ShopStatus.Active, StringComparison.OrdinalIgnoreCase)
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return ShopName
    End Function

End Class

Public NotInheritable Class ShopStatus
    Private Sub New()
    End Sub
    Public Const Active As String = "Active"
    Public Const Inactive As String = "Inactive"
End Class
