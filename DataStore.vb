Public Module DataStore

    '========================================
    ' PRODUCT STORE
    '========================================

    Public Products As New List(Of Product)


    '========================================
    ' MESSAGE STORE
    '========================================

    Public ChatMessages As New List(Of ChatMessage)


    '========================================
    ' TRANSACTION STORE
    '========================================

    Public Transactions As New List(Of POS_Transaction)

    Private transactionNumber As Integer = 0

    Public Event TransactionsChanged As EventHandler


    '========================================
    ' ADD TRANSACTION
    '========================================

    Public Sub AddTransaction(transaction As POS_Transaction)

        transactionNumber += 1

        transaction.TransactionID =
            "TRX-" & transactionNumber.ToString("D4")

        Transactions.Add(transaction)

        RaiseEvent TransactionsChanged(Nothing, EventArgs.Empty)

    End Sub


    '========================================
    ' TODAY'S COMPLETED TRANSACTIONS
    '========================================

    Public Function GetTodayCompletedTransactions() As List(Of POS_Transaction)

        Dim result As New List(Of POS_Transaction)

        For Each transaction As POS_Transaction In Transactions

            If transaction.Status = "Completed" AndAlso
               transaction.TransactionDate.Date = DateTime.Today Then

                result.Add(transaction)

            End If

        Next

        Return result

    End Function


    '========================================
    ' TODAY'S SALES
    '========================================

    Public Function GetTodaySales() As Decimal

        Dim totalSales As Decimal = 0D

        For Each transaction As POS_Transaction In Transactions

            If transaction.Status = "Completed" AndAlso
               transaction.TransactionDate.Date = DateTime.Today Then

                totalSales += transaction.Total

            End If

        Next

        Return totalSales

    End Function


    '========================================
    ' TODAY'S ORDERS
    '========================================

    Public Function GetTodayOrderCount() As Integer

        Dim orderCount As Integer = 0

        For Each transaction As POS_Transaction In Transactions

            If transaction.Status = "Completed" AndAlso
               transaction.TransactionDate.Date = DateTime.Today Then

                orderCount += 1

            End If

        Next

        Return orderCount

    End Function


    '========================================
    ' AVERAGE ORDER
    '========================================

    Public Function GetTodayAverageOrder() As Decimal

        Dim orderCount As Integer = GetTodayOrderCount()

        If orderCount = 0 Then
            Return 0D
        End If

        Return GetTodaySales() / orderCount

    End Function


    '========================================
    ' LOW STOCK
    '========================================

    Public Function GetLowStockCount() As Integer

        Dim count As Integer = 0

        For Each product As Product In Products

            If product.Stock <= 5 Then
                count += 1
            End If

        Next

        Return count

    End Function

End Module


'========================================
' TRANSACTION CLASS
'========================================

Public Class POS_Transaction

    Public Property TransactionID As String

    Public Property TransactionDate As DateTime

    Public Property Cashier As String

    Public Property Items As New List(Of TransactionItem)

    Public Property PaymentMethod As String

    Public Property Total As Decimal

    Public Property Status As String

End Class


'========================================
' TRANSACTION ITEM
'========================================

Public Class TransactionItem

    Public Property ProductName As String

    Public Property Quantity As Integer

    Public Property Price As Decimal

End Class