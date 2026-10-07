Option Strict On
Option Explicit On

' PHASE 1 (new file).  Role names + permission rules in ONE place.
' Existing "admin" role in your TXT/login = "Manager" now.
Public NotInheritable Class UserRoles
    Private Sub New()
    End Sub
    Public Const SuperAdmin As String = "superadmin"
    Public Const Manager As String = "manager"
    Public Const Cashier As String = "cashier"

    ''' <summary>Old data used "admin"; treat it as Manager.</summary>
    Public Shared Function Normalize(role As String) As String
        Dim r As String = (If(role, "")).Trim().ToLowerInvariant()
        If r = "admin" Then Return Manager
        Return r
    End Function
End Class

Public NotInheritable Class Permissions
    Private Sub New()
    End Sub

    ' ---- action names ----
    Public Const ManageShops As String = "ManageShops"
    Public Const ManageManagers As String = "ManageManagers"
    Public Const ManageCashiers As String = "ManageCashiers"
    Public Const ManageProducts As String = "ManageProducts"       ' create/delete/price
    Public Const ManageRecipes As String = "ManageRecipes"
    Public Const ManageIngredients As String = "ManageIngredients" ' create/edit/delete
    Public Const AdjustStock As String = "AdjustStock"            ' restock / set min
    Public Const ReportLoss As String = "ReportLoss"              ' wastage/damage/lost
    Public Const ViewInventory As String = "ViewInventory"
    Public Const UsePOS As String = "UsePOS"
    Public Const RefundVoid As String = "RefundVoid"
    Public Const ViewAllShops As String = "ViewAllShops"

    Public Shared Function Can(role As String, action As String) As Boolean
        Select Case UserRoles.Normalize(role)
            Case UserRoles.SuperAdmin
                Return True
            Case UserRoles.Manager
                Return action <> ManageShops AndAlso action <> ManageManagers AndAlso action <> ViewAllShops
            Case UserRoles.Cashier
                Return action = ViewInventory OrElse action = ReportLoss OrElse action = UsePOS
            Case Else
                Return False
        End Select
    End Function
End Class
