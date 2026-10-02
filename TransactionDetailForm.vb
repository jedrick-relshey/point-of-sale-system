Option Strict On
Option Explicit On

' NEW FORM: shows the stored values of one transaction and (for Admin) refund / void.
Public Class TransactionDetailForm

    Private ReadOnly _transactionId As String
    Private ReadOnly _allowActions As Boolean

    Public Sub New(transaction As POS_Transaction, allowActions As Boolean)
        InitializeComponent()
        _transactionId = transaction.TransactionID
        _allowActions = allowActions
    End Sub

    Private Sub TransactionDetailForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ShowTransaction()
    End Sub

    Private Sub ShowTransaction()
        Dim t As POS_Transaction = DataStore.FindTransaction(_transactionId)
        If t Is Nothing Then
            Me.Close()
            Return
        End If

        lblTitle.Text = "Transaction: " & t.TransactionID
        lblDate.Text = "Date:  " & t.TransactionDate.ToString("MMMM d, yyyy  hh:mm tt")
        lblCashier.Text = "Cashier:  " & t.Cashier
        lblPayment.Text = "Payment:  " & t.PaymentMethod
        lblStatus.Text = t.Status
        lblStatus.ForeColor = StatusColor(t.Status)

        dgvItems.Rows.Clear()
        Dim computedSubtotal As Decimal = 0D
        For Each item As TransactionItem In t.Items
            dgvItems.Rows.Add(item.ProductName, item.Quantity, Peso(item.Price), Peso(item.LineTotal))
            computedSubtotal += item.LineTotal
        Next

        ' older records may not have Subtotal / Tax saved - derive them
        Dim subtotal As Decimal = If(t.Subtotal > 0D, t.Subtotal, computedSubtotal)
        Dim tax As Decimal = If(t.Tax > 0D OrElse t.Subtotal > 0D, t.Tax, t.Total - subtotal)

        lblSubtotal.Text = "Subtotal:  " & Peso(subtotal)
        lblTax.Text = "Tax:  " & Peso(tax)
        lblTotal.Text = "Total:  " & Peso(t.Total)

        If t.PaymentMethod = PaymentMethods.Cash Then
            lblCashInfo.Text = "Cash received: " & Peso(t.CashReceived) & vbCrLf & "Change: " & Peso(t.ChangeGiven)
        Else
            lblCashInfo.Text = "Paid by card"
        End If

        If t.StatusChangedDate.HasValue Then
            lblAudit.Text = t.Status & " by " & t.StatusChangedBy & " on " &
                            t.StatusChangedDate.Value.ToString("MMM d, yyyy hh:mm tt")
        Else
            lblAudit.Text = ""
        End If

        Dim canChange As Boolean = _allowActions AndAlso t.Status = TransactionStatus.Completed
        btnRefund.Visible = _allowActions
        btnVoid.Visible = _allowActions
        btnRefund.Enabled = canChange
        btnVoid.Enabled = canChange
    End Sub

    Private Sub btnRefund_Click(sender As Object, e As EventArgs) Handles btnRefund.Click
        If MessageBox.Show("Are you sure you want to refund this transaction?" & vbCrLf &
                           "The sold items will be returned to inventory.",
                           "Refund Transaction", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return

        Dim err As String = ""
        If DataStore.RefundTransaction(_transactionId, CurrentSession.FullName, err) Then
            MessageBox.Show("Transaction refunded. Stock has been restored.", "Refund",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            ShowTransaction()
        Else
            MessageBox.Show(err, "Refund", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnVoid_Click(sender As Object, e As EventArgs) Handles btnVoid.Click
        If MessageBox.Show("Are you sure you want to VOID this transaction?" & vbCrLf &
                           "The record is kept for auditing and the items return to inventory.",
                           "Void Transaction", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then Return

        Dim err As String = ""
        If DataStore.VoidTransaction(_transactionId, CurrentSession.FullName, err) Then
            MessageBox.Show("Transaction voided.", "Void", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ShowTransaction()
        Else
            MessageBox.Show(err, "Void", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class