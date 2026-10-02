Option Strict On
Option Explicit On

Imports System.Drawing
Imports System.Security.Cryptography
Imports System.Text

'=====================================================================
' MODELS.VB  -  all persistent model classes for BrewPoint POS
'
' This file REPLACES your old Product.vb (delete the old Product class)
' and also takes over ChatMessage from ChatMessage.vb
' (delete ChatMessage.vb or empty it) and the POS_Transaction /
' TransactionItem classes that used to live at the bottom of DataStore.vb.
'=====================================================================

'---------------------------------------------------------------------
' Constants shared by every form
'---------------------------------------------------------------------
Public Class AccountStatus
    Public Const Active As String = "Active"
    Public Const Inactive As String = "Inactive"
    Public Const Suspended As String = "Suspended"
End Class

Public Class TransactionStatus
    Public Const Completed As String = "Completed"
    Public Const Refunded As String = "Refunded"
    Public Const Voided As String = "Voided"
End Class

Public Class StockStatus
    Public Const InStock As String = "In Stock"
    Public Const LowStock As String = "Low Stock"
    Public Const OutOfStock As String = "Out of Stock"
End Class

Public Class PaymentMethods
    Public Const Cash As String = "Cash"
    Public Const Card As String = "Card"
End Class

'---------------------------------------------------------------------
' PRODUCT
' Image is NOT serialized. Only ImagePath (relative to Data\Images) is.
'---------------------------------------------------------------------
Public Class Product

    Public Property Id As String = ""
    Public Property Name As String = ""
    Public Property Price As Decimal = 0D
    Public Property Stock As Integer = 0

    ''' <summary>Per-product minimum stock. 0 = use the system default.</summary>
    Public Property MinStock As Integer = 0

    Public Property Category As String = ""
    Public Property Description As String = ""

    ''' <summary>"Available" / "Unavailable" (kept for the existing Admin UI).</summary>
    Public Property Status As String = "Available"

    ''' <summary>File name inside Data\Images (e.g. product_001.jpg).</summary>
    Public Property ImagePath As String = ""

    Public Property LastUpdated As DateTime = DateTime.Now

    ''' <summary>Runtime-only picture. Kept so existing UI code (product.Image) still works.</summary>
    Public Property Image As Image

End Class

'---------------------------------------------------------------------
' ACCOUNTS
'---------------------------------------------------------------------
Public Class CashierAccount

    Public Property Id As String = ""
    Public Property FullName As String = ""
    Public Property Username As String = ""
    Public Property PasswordHash As String = ""
    Public Property PasswordSalt As String = ""
    Public Property Status As String = AccountStatus.Active
    Public Property DateAdded As DateTime = DateTime.Now
    Public Property LastLogin As DateTime? = Nothing

    Public ReadOnly Property IsActive As Boolean
        Get
            Return String.Equals(Status, AccountStatus.Active, StringComparison.OrdinalIgnoreCase)
        End Get
    End Property

End Class

Public Class AdminAccount

    Public Property Id As String = ""
    Public Property FullName As String = ""
    Public Property Username As String = ""
    Public Property PasswordHash As String = ""
    Public Property PasswordSalt As String = ""
    Public Property Status As String = AccountStatus.Active
    Public Property DateAdded As DateTime = DateTime.Now
    Public Property LastLogin As DateTime? = Nothing

    Public ReadOnly Property IsActive As Boolean
        Get
            Return String.Equals(Status, AccountStatus.Active, StringComparison.OrdinalIgnoreCase)
        End Get
    End Property

End Class

'---------------------------------------------------------------------
' TRANSACTIONS  (price snapshot is stored in every TransactionItem)
'---------------------------------------------------------------------
Public Class POS_Transaction

    Public Property TransactionID As String = ""
    Public Property TransactionDate As DateTime = DateTime.Now
    Public Property Cashier As String = ""
    Public Property CashierUsername As String = ""
    Public Property Items As New List(Of TransactionItem)
    Public Property PaymentMethod As String = PaymentMethods.Cash

    Public Property Subtotal As Decimal = 0D
    Public Property Tax As Decimal = 0D
    Public Property Total As Decimal = 0D

    Public Property CashReceived As Decimal = 0D
    Public Property ChangeGiven As Decimal = 0D

    Public Property Status As String = TransactionStatus.Completed
    Public Property StatusChangedBy As String = ""
    Public Property StatusChangedDate As DateTime? = Nothing

End Class

Public Class TransactionItem

    Public Property ProductId As String = ""
    Public Property ProductName As String = ""
    Public Property Quantity As Integer = 0

    ''' <summary>Unit price AT THE TIME OF SALE (never changes afterwards).</summary>
    Public Property Price As Decimal = 0D

    Public ReadOnly Property LineTotal As Decimal
        Get
            Return Price * Quantity
        End Get
    End Property

End Class

'---------------------------------------------------------------------
' MESSAGES (same shape as before, now persisted)
'---------------------------------------------------------------------
Public Class ChatMessage

    Public Property Sender As String = ""
    Public Property Receiver As String = ""
    Public Property Message As String = ""
    Public Property TimeSent As DateTime = DateTime.Now

End Class

'---------------------------------------------------------------------
' AUDIT / SETTINGS / REPORTING
'---------------------------------------------------------------------
Public Class PriceChangeLog

    Public Property ProductId As String = ""
    Public Property ProductName As String = ""
    Public Property OldPrice As Decimal = 0D
    Public Property NewPrice As Decimal = 0D
    Public Property ChangedBy As String = ""
    Public Property ChangedDate As DateTime = DateTime.Now

End Class

Public Class SystemSettings

    Public Property DefaultMinStock As Integer = 10
    Public Property TaxRate As Decimal = 0.12D
    Public Property NextTransactionNumber As Integer = 1
    Public Property NextProductNumber As Integer = 1
    Public Property NextCashierNumber As Integer = 1

End Class

Public Class SalesSummary

    Public Property TransactionCount As Integer = 0
    Public Property TotalSales As Decimal = 0D

    Public ReadOnly Property AverageOrder As Decimal
        Get
            If TransactionCount = 0 Then Return 0D
            Return TotalSales / TransactionCount
        End Get
    End Property

End Class

'---------------------------------------------------------------------
' PASSWORD HASHING  (salted, iterated SHA-256 - no plaintext storage)
'---------------------------------------------------------------------
Public Class PasswordHasher

    Private Const Iterations As Integer = 10000

    Public Shared Function CreateSalt() As String
        Dim bytes(15) As Byte
        Using rng As RandomNumberGenerator = RandomNumberGenerator.Create()
            rng.GetBytes(bytes)
        End Using
        Return Convert.ToBase64String(bytes)
    End Function

    Public Shared Function Hash(password As String, salt As String) As String
        Dim data As Byte() = Encoding.UTF8.GetBytes(salt & password)
        Using sha As SHA256 = SHA256.Create()
            Dim current As Byte() = sha.ComputeHash(data)
            For i As Integer = 1 To Iterations
                Dim combined(current.Length + data.Length - 1) As Byte
                Buffer.BlockCopy(current, 0, combined, 0, current.Length)
                Buffer.BlockCopy(data, 0, combined, current.Length, data.Length)
                current = sha.ComputeHash(combined)
            Next
            Return Convert.ToBase64String(current)
        End Using
    End Function

    Public Shared Function Verify(password As String, salt As String, expectedHash As String) As Boolean
        If String.IsNullOrEmpty(salt) OrElse String.IsNullOrEmpty(expectedHash) Then Return False
        Dim actual As Byte() = Convert.FromBase64String(Hash(password, salt))
        Dim expected As Byte()
        Try
            expected = Convert.FromBase64String(expectedHash)
        Catch ex As FormatException
            Return False
        End Try
        If actual.Length <> expected.Length Then Return False
        Dim diff As Integer = 0
        For i As Integer = 0 To actual.Length - 1
            diff = diff Or (actual(i) Xor expected(i))   ' constant-time compare
        Next
        Return diff = 0
    End Function

End Class
