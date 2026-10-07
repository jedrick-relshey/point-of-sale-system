Option Strict On
Option Explicit On

' PHASE 1/2.  "Who is logged in, and which shop are they in?"
' Set ONCE at login (DataStore.Authenticate). DataStore filters every list by
' ShopContext.CurrentShopId - that is how branch data never mixes.
' A Super Admin has CurrentShopId = "" (sees all shops) until a shop card is picked.
Public NotInheritable Class ShopContext
    Private Sub New()
    End Sub

    Public Shared Property CurrentRole As String = ""
    Public Shared Property CurrentShopId As String = ""

    Public Shared Event ShopChanged As EventHandler

    Public Shared ReadOnly Property IsSuperAdmin As Boolean
        Get
            Return CurrentRole = UserRoles.SuperAdmin
        End Get
    End Property

    ''' <summary>Called by DataStore.Authenticate after a successful login.</summary>
    Public Shared Sub SignIn(role As String, shopId As String)
        CurrentRole = UserRoles.Normalize(role)
        CurrentShopId = If(shopId, "")
        RaiseEvent ShopChanged(Nothing, EventArgs.Empty)
    End Sub

    ''' <summary>Super Admin picks a shop card ("" = back to all shops). Ignored for other roles.</summary>
    Public Shared Sub SelectShop(shopId As String)
        If Not IsSuperAdmin Then Return
        CurrentShopId = If(shopId, "")
        RaiseEvent ShopChanged(Nothing, EventArgs.Empty)
    End Sub

    Public Shared Sub SignOut()
        CurrentRole = ""
        CurrentShopId = ""
    End Sub

    ''' <summary>True if the current user may see/edit data of this shop.</summary>
    Public Shared Function CanAccess(shopId As String) As Boolean
        If IsSuperAdmin Then Return True
        Return CurrentShopId <> "" AndAlso String.Equals(CurrentShopId, shopId, StringComparison.OrdinalIgnoreCase)
    End Function

    Public Shared Function Can(action As String) As Boolean
        Return Permissions.Can(CurrentRole, action)
    End Function

    ''' <summary>
    ''' Isolation filter: shop users get only rows of their shop; a Super Admin with no shop
    ''' selected gets everything; with a shop selected, only that shop.
    ''' </summary>
    Public Shared Function ForCurrentShop(Of T)(source As IEnumerable(Of T), shopIdOf As Func(Of T, String)) As List(Of T)
        Dim result As New List(Of T)()
        Dim everything As Boolean = IsSuperAdmin AndAlso CurrentShopId = ""
        For Each item As T In source
            If everything OrElse (CurrentShopId <> "" AndAlso
                                  String.Equals(shopIdOf(item), CurrentShopId, StringComparison.OrdinalIgnoreCase)) Then
                result.Add(item)
            End If
        Next
        Return result
    End Function

End Class
