Option Strict On
Option Explicit On

Imports System.Drawing
Imports Guna.UI2.WinForms

'=====================================================================
' NEW FILE: HistoryView.vb
' One reusable controller for the "Transaction History" page.
' Admin.vb and Cashier.vb both create one, so filtering code is NOT duplicated.
'
' It re-uses the controls that already exist in your designers:
'   search box, period combo, status combo, history grid
' and creates (in code) the extra controls: Daily/Weekly/Monthly/All buttons,
' a date picker, and the "Showing X of Y" + sales-summary labels.
'=====================================================================
Public Class HistoryView

    Private ReadOnly _owner As Form
    Private ReadOnly _grid As DataGridView
    Private ReadOnly _search As Control
    Private ReadOnly _period As ComboBox
    Private ReadOnly _status As ComboBox
    Private ReadOnly _allowActions As Boolean

    Private ReadOnly _periodButtons As New Dictionary(Of String, Guna2Button)
    Private ReadOnly _datePicker As New DateTimePicker()
    Private ReadOnly _lblShowing As New Label()
    Private ReadOnly _lblSummary As New Label()

    Private _currentPeriod As String = "Daily"
    Private _loading As Boolean = True

    Private ReadOnly brown As Color = Color.FromArgb(62, 39, 35)

    Public Sub New(owner As Form,
                   buttonHost As Control, buttonLocation As Point,
                   grid As DataGridView, search As Control,
                   periodCombo As ComboBox, statusCombo As ComboBox,
                   footerHost As Control, footerLocation As Point,
                   allowActions As Boolean)

        _owner = owner
        _grid = grid
        _search = search
        _period = periodCombo
        _status = statusCombo
        _allowActions = allowActions

        ConfigureGrid()
        ConfigureCombos()
        BuildPeriodButtons(buttonHost, buttonLocation)
        BuildFooter(footerHost, footerLocation)

        AddHandler _search.TextChanged, AddressOf OnFilterChanged
        AddHandler _period.SelectedIndexChanged, AddressOf OnPeriodComboChanged
        AddHandler _status.SelectedIndexChanged, AddressOf OnFilterChanged
        AddHandler _datePicker.ValueChanged, AddressOf OnFilterChanged
        AddHandler _grid.CellClick, AddressOf OnGridCellClick
        AddHandler DataStore.TransactionsChanged, AddressOf OnStoreChanged
        AddHandler _owner.FormClosed, AddressOf OnOwnerClosed

        _loading = False
        SetPeriod("Daily")
    End Sub

    '------------------------------------------------------------------
    ' BUILD
    '------------------------------------------------------------------
    Private Sub ConfigureGrid()
        _grid.Columns.Clear()
        _grid.AllowUserToAddRows = False
        _grid.AllowUserToDeleteRows = False
        _grid.ReadOnly = True
        _grid.RowHeadersVisible = False
        _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        _grid.MultiSelect = False
        _grid.Cursor = Cursors.Hand
        CafeUi.StyleHistoryGrid(_grid)

        _search.Location = New Point(395, 27)
        _search.Size = New Size(190, 40)
        _period.Location = New Point(594, 27)
        _period.Size = New Size(124, 40)
        _status.Location = New Point(875, 27)
        _status.Size = New Size(124, 40)

        AddColumn("colHId", "Transaction ID", 162)
        AddColumn("colHDate", "Date & Time", 143)
        AddColumn("colHCashier", "Cashier", 128)
        AddColumn("colHItems", "Products Ordered", 207)
        AddColumn("colHTotal", "Total", 111)
        AddColumn("colHPayment", "Payment", 105)
        Dim statusCol As DataGridViewTextBoxColumn = AddColumn("colHStatus", "Status", 80)
        statusCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        _grid.Columns("colHId").DefaultCellStyle.Font = New Font("Consolas", 9.5F, FontStyle.Bold)
        _grid.Columns("colHId").DefaultCellStyle.Padding = New Padding(10, 0, 0, 0)
        _grid.Columns("colHTotal").DefaultCellStyle.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        _grid.Columns("colHTotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        _grid.Columns("colHPayment").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        _grid.Columns("colHStatus").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        _grid.Columns("colHItems").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
    End Sub

    Private Function AddColumn(name As String, header As String, width As Integer) As DataGridViewTextBoxColumn
        Dim col As New DataGridViewTextBoxColumn With {
            .Name = name,
            .HeaderText = header,
            .Width = width,
            .SortMode = DataGridViewColumnSortMode.NotSortable
        }
        _grid.Columns.Add(col)
        Return col
    End Function

    Private Sub ConfigureCombos()
        _period.Items.Clear()
        _period.Items.AddRange(New Object() {"Daily", "Weekly", "Monthly", "All"})
        _period.SelectedIndex = 0

        _status.Items.Clear()
        _status.Items.AddRange(New Object() {"All Statuses", TransactionStatus.Completed,
                                             TransactionStatus.Refunded, TransactionStatus.Voided})
        _status.SelectedIndex = 0
    End Sub

    Private Sub BuildPeriodButtons(host As Control, origin As Point)
        Dim names As String() = {"Daily", "Weekly", "Monthly", "All"}
        Dim x As Integer = origin.X
        For Each n As String In names
            Dim b As New Guna2Button With {
                .Text = If(n = "All", "All Time", n),
                .Size = New Size(86, 26),
                .Location = New Point(x, origin.Y),
                .BorderRadius = 4,
                .BorderThickness = 1,
                .BorderColor = brown,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .Tag = n,
                .Cursor = Cursors.Hand
            }
            AddHandler b.Click, AddressOf OnPeriodButtonClick
            host.Controls.Add(b)
            b.BringToFront()
            _periodButtons(n) = b
            x += 92
        Next

        _datePicker.Format = DateTimePickerFormat.Custom
        _datePicker.CustomFormat = "'Date:' MMM yyyy"
        _datePicker.Width = 140
        _datePicker.Location = New Point(726, 27)
        _datePicker.Font = New Font("Segoe UI", 9.5F)
        _datePicker.CalendarTitleBackColor = CafeUi.Coffee
        _datePicker.Value = DateTime.Today
        host.Controls.Add(_datePicker)
        _datePicker.BringToFront()

        For Each button As Guna2Button In _periodButtons.Values
            button.Visible = False
        Next
    End Sub

    Private Sub BuildFooter(host As Control, origin As Point)
        _lblShowing.AutoSize = False
        _lblShowing.Font = New Font("Segoe UI", 9.0F)
        _lblShowing.ForeColor = brown
        _lblShowing.BackColor = Color.Transparent
        _lblShowing.Location = origin
        _lblShowing.Size = New Size(300, 22)
        _lblShowing.TextAlign = ContentAlignment.MiddleLeft

        _lblSummary.AutoSize = False
        _lblSummary.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        _lblSummary.ForeColor = brown
        _lblSummary.BackColor = Color.Transparent
        _lblSummary.Location = New Point(origin.X + 310, origin.Y)
        _lblSummary.Size = New Size(host.Width - origin.X - 330, 22)
        _lblSummary.TextAlign = ContentAlignment.MiddleRight
        _lblSummary.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right

        host.Controls.Add(_lblShowing)
        host.Controls.Add(_lblSummary)
        _lblShowing.BringToFront()
        _lblSummary.BringToFront()
    End Sub

    '------------------------------------------------------------------
    ' EVENTS
    '------------------------------------------------------------------
    Private Sub OnPeriodButtonClick(sender As Object, e As EventArgs)
        Dim b As Guna2Button = TryCast(sender, Guna2Button)
        If b Is Nothing Then Return
        SetPeriod(Convert.ToString(b.Tag))
    End Sub

    Private Sub OnPeriodComboChanged(sender As Object, e As EventArgs)
        If _loading Then Return
        SetPeriod(Convert.ToString(_period.SelectedItem))
    End Sub

    Private Sub OnFilterChanged(sender As Object, e As EventArgs)
        If _loading Then Return
        Reload()
    End Sub

    Private Sub OnStoreChanged(sender As Object, e As EventArgs)
        If _grid.IsDisposed Then Return
        Reload()
    End Sub

    Private Sub OnGridCellClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Return
        Dim id As String = Convert.ToString(_grid.Rows(e.RowIndex).Tag)
        Dim t As POS_Transaction = DataStore.FindTransaction(id)
        If t Is Nothing Then Return
        Using dlg As New TransactionDetailForm(t, _allowActions)
            dlg.ShowDialog(_owner)
        End Using
    End Sub

    '------------------------------------------------------------------
    ' PUBLIC
    '------------------------------------------------------------------
    Public Sub SetPeriod(periodName As String)
        If Not _periodButtons.ContainsKey(periodName) Then periodName = "Daily"
        _currentPeriod = periodName

        _loading = True
        If Convert.ToString(_period.SelectedItem) <> periodName Then _period.SelectedItem = periodName
        _datePicker.Enabled = (periodName <> "All")
        _loading = False

        For Each kv As KeyValuePair(Of String, Guna2Button) In _periodButtons
            Dim active As Boolean = (kv.Key = periodName)
            kv.Value.FillColor = If(active, brown, Color.White)
            kv.Value.ForeColor = If(active, Color.White, brown)
        Next

        Reload()
    End Sub

    Public Sub Reload()
        If _loading Then Return

        Dim statusText As String = Convert.ToString(_status.SelectedItem)
        Dim anchor As DateTime = _datePicker.Value
        Dim rows As List(Of POS_Transaction) =
            DataStore.QueryTransactions(_currentPeriod, anchor, statusText, _search.Text)

        _grid.SuspendLayout()
        _grid.Rows.Clear()
        For Each t As POS_Transaction In rows
            Dim idx As Integer = _grid.Rows.Add(
                t.TransactionID,
                FriendlyDate(t.TransactionDate),
                t.Cashier,
                DataStore.BuildItemsText(t),
                Peso(t.Total),
                t.PaymentMethod,
                t.Status)
            Dim row As DataGridViewRow = _grid.Rows(idx)
            row.Tag = t.TransactionID
            row.Cells("colHStatus").Style.ForeColor = StatusColor(t.Status)
            row.Cells("colHStatus").Style.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        Next
        _grid.ClearSelection()
        _grid.ResumeLayout()

        _lblShowing.Text = "Showing " & rows.Count & " of " & DataStore.Transactions.Count & " total transactions"

        ' sales summary for the selected period (Completed only)
        Dim periodList As List(Of POS_Transaction) =
            DataStore.QueryTransactions(_currentPeriod, anchor, "All Statuses", "")
        Dim s As SalesSummary = DataStore.GetSalesSummary(periodList)
        _lblSummary.Text = PeriodCaption(anchor) & ":  " & s.TransactionCount & " orders   |   Sales " &
                           Peso(s.TotalSales) & "   |   Avg " & Peso(s.AverageOrder)
    End Sub

    Private Function PeriodCaption(anchor As DateTime) As String
        Select Case _currentPeriod
            Case "Daily"
                Return anchor.ToString("MMM d, yyyy")
            Case "Weekly"
                Dim start As DateTime = DataStore.GetWeekStart(anchor)
                Return start.ToString("MMM d") & " - " & start.AddDays(6).ToString("MMM d, yyyy")
            Case "Monthly"
                Return anchor.ToString("MMMM yyyy")
            Case Else
                Return "All time"
        End Select
    End Function

    Private Function FriendlyDate(d As DateTime) As String
        If d.Date = DateTime.Today Then Return "Today, " & d.ToString("hh:mm tt")
        If d.Date = DateTime.Today.AddDays(-1) Then Return "Yesterday, " & d.ToString("hh:mm tt")
        Return d.ToString("MMM d, hh:mm tt")
    End Function

    Private Sub OnOwnerClosed(sender As Object, e As FormClosedEventArgs)
        Dispose()
    End Sub

    Public Sub Dispose()
        RemoveHandler DataStore.TransactionsChanged, AddressOf OnStoreChanged
        RemoveHandler _owner.FormClosed, AddressOf OnOwnerClosed
        RemoveHandler _search.TextChanged, AddressOf OnFilterChanged
        RemoveHandler _period.SelectedIndexChanged, AddressOf OnPeriodComboChanged
        RemoveHandler _status.SelectedIndexChanged, AddressOf OnFilterChanged
        RemoveHandler _datePicker.ValueChanged, AddressOf OnFilterChanged
        RemoveHandler _grid.CellClick, AddressOf OnGridCellClick
    End Sub

End Class
