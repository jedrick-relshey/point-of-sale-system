Option Strict On
Option Explicit On

Imports System.Drawing
Imports System.Windows.Forms
Imports Guna.UI2.WinForms

' Admin.Cashiers.vb  -  NEW FILE (third part of the Admin class).
' Rebuilds the Cashiers page in code to match the "Cashier management" design.
' The old Cashiers controls stay in Admin_Designer.vb but are hidden, so nothing else breaks.
Partial Public Class Admin

    Private cashSearch As Guna2TextBox
    Private cashStatus As Guna2ComboBox
    Private cashRows As FlowLayoutPanel
    Private cashHeaderLabels(6) As Label
    Private cashStatValue(3) As Label
    Private cashStatIcon(3) As Label
    Private cashStatCards(3) As Guna2Panel
    Private statsRow As Panel
    Private ReadOnly cashTip As New ToolTip()
    Private lastCashW As Integer = -1

    Private Const CashRowH As Integer = 74

    '=================================================================
    ' PAGE LAYOUT
    '=================================================================
    Private Sub BuildCashierPage()

        Dim pg As Panel = pnlCashiers
        pg.SuspendLayout()
        pg.BackColor = ForestUi.Page
        pg.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right

        ' hide the old cashier controls (they still exist, so old handlers keep compiling)
        For Each c As Control In pg.Controls
            c.Visible = False
        Next

        ' ---------- header with "Add cashier" ----------
        Dim addBtn As New Guna2Button()
        addBtn.Text = "Add cashier"
        addBtn.Size = New Size(150, 42)
        addBtn.BorderRadius = 10
        addBtn.FillColor = ForestUi.Accent
        addBtn.ForeColor = Color.White
        addBtn.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        addBtn.HoverState.FillColor = ForestUi.AccentDark
        addBtn.Image = Counterly.GlyphImage(Counterly.GlyphAddUser, 18, Color.White)
        addBtn.ImageSize = New Size(18, 18)
        addBtn.ImageAlign = HorizontalAlignment.Left
        addBtn.ImageOffset = New Point(12, 0)
        addBtn.Cursor = Cursors.Hand
        AddHandler addBtn.Click, AddressOf AddCashier_Click

        ' same header as the Dashboard: shows the admin who is really logged in (CurrentSession), not a fixed "Admin"
        Dim hdr As New ForestHeader("Cashier management", "Manage team access, shifts, and register performance")
        hdr.AddAction(addBtn)
        hdr.SetUser(DashUserName(), "Administrator")
        AddHandler hdr.LogoutClicked, Sub(s As Object, ev As EventArgs) DoLogout()
        AddHandler hdr.Bell.Click, Sub(s As Object, ev As EventArgs) btnMessages.PerformClick()

        ' ---------- body ----------
        Dim body As New Panel()
        body.Dock = DockStyle.Fill
        body.BackColor = ForestUi.Page
        body.Padding = New Padding(24, 22, 24, 22)

        Dim tableCard As Guna2Panel = ForestUi.Card(100, 100, 14, ForestUi.CardFill, ForestUi.Border)
        tableCard.Dock = DockStyle.Fill
        tableCard.Padding = New Padding(1)

        ' rows (Fill first, then the top docks: last added = outermost)
        cashRows = New FlowLayoutPanel()
        cashRows.Dock = DockStyle.Fill
        cashRows.FlowDirection = FlowDirection.TopDown
        cashRows.WrapContents = False
        cashRows.AutoScroll = True
        cashRows.BackColor = ForestUi.CardFill
        cashRows.Padding = Padding.Empty
        tableCard.Controls.Add(cashRows)

        Dim colHeader As New Panel()
        colHeader.Dock = DockStyle.Top
        colHeader.Height = 42
        colHeader.BackColor = ForestUi.HeaderRow
        Dim titles() As String = {"Cashier", "Username", "Date added", "Transactions", "Sales today", "Status", "Actions"}
        For i As Integer = 0 To 6
            cashHeaderLabels(i) = ForestUi.Lbl(titles(i), 8.5F, FontStyle.Bold, ForestUi.Accent)
            colHeader.Controls.Add(cashHeaderLabels(i))
        Next
        tableCard.Controls.Add(colHeader)

        Dim toolbar As New Panel()
        toolbar.Dock = DockStyle.Top
        toolbar.Height = 88
        toolbar.BackColor = ForestUi.CardFill

        Dim searchBox As Guna2Panel = ForestUi.Card(470, 46, 10, ForestUi.CardFill, ForestUi.Border)
        searchBox.Location = New Point(22, 22)
        Dim sIcon As New Label()
        sIcon.AutoSize = True
        sIcon.BackColor = Color.Transparent
        sIcon.ForeColor = ForestUi.Muted
        sIcon.Font = New Font("Segoe MDL2 Assets", 11.0F)
        sIcon.Text = ForestUi.GlyphSearch
        sIcon.Location = New Point(14, 13)
        searchBox.Controls.Add(sIcon)

        cashSearch = New Guna2TextBox()
        cashSearch.BorderThickness = 0
        cashSearch.BorderRadius = 0
        cashSearch.FillColor = ForestUi.CardFill
        cashSearch.Font = New Font("Segoe UI", 10.0F)
        cashSearch.ForeColor = ForestUi.Ink
        cashSearch.PlaceholderText = "Search cashier name or username"
        cashSearch.PlaceholderForeColor = ForestUi.Muted
        cashSearch.Location = New Point(42, 7)
        cashSearch.Size = New Size(470 - 42 - 12, 32)
        searchBox.Controls.Add(cashSearch)
        toolbar.Controls.Add(searchBox)

        cashStatus = New Guna2ComboBox()
        cashStatus.DropDownStyle = ComboBoxStyle.DropDownList
        cashStatus.BorderRadius = 10
        cashStatus.BorderColor = ForestUi.Border
        cashStatus.FillColor = ForestUi.CardFill
        cashStatus.ForeColor = ForestUi.Ink
        cashStatus.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        cashStatus.ItemHeight = 34
        cashStatus.Size = New Size(160, 40)
        cashStatus.Items.AddRange(New Object() {"Status: All", "Status: Active", "Status: Inactive"})
        cashStatus.SelectedIndex = 0
        toolbar.Controls.Add(cashStatus)
        AddHandler toolbar.Resize, Sub(s As Object, e As EventArgs)
                                       cashStatus.Location = New Point(toolbar.ClientSize.Width - 22 - cashStatus.Width, 24)
                                   End Sub
        cashStatus.Location = New Point(Math.Max(300, tableCard.Width - 22 - cashStatus.Width), 24)
        tableCard.Controls.Add(toolbar)

        ' ---------- stat cards ----------
        statsRow = New Panel()
        statsRow.Dock = DockStyle.Top
        statsRow.Height = 112
        statsRow.BackColor = ForestUi.Page

        Dim captions() As String = {"Team members", "Currently active", "Inactive", "Sales today"}
        Dim icons() As String = {Counterly.GlyphPeople, Counterly.GlyphCheck, ForestUi.GlyphClose, ChrW(&H20B1)}
        For i As Integer = 0 To 3
            Dim card As Guna2Panel = ForestUi.Card(200, 88, 14, ForestUi.CardFill, ForestUi.Border)
            card.Controls.Add(ForestUi.Lbl(captions(i), 8.5F, FontStyle.Regular, ForestUi.Muted, 18, 16))

            cashStatValue(i) = ForestUi.Lbl("0", 18.0F, FontStyle.Regular, ForestUi.Ink, 17, 36)
            card.Controls.Add(cashStatValue(i))

            cashStatIcon(i) = New Label()
            cashStatIcon(i).AutoSize = True
            cashStatIcon(i).BackColor = Color.Transparent
            cashStatIcon(i).ForeColor = ForestUi.Accent
            If i = 3 Then
                cashStatIcon(i).Font = New Font("Segoe UI", 17.0F, FontStyle.Bold)
            Else
                cashStatIcon(i).Font = New Font("Segoe MDL2 Assets", 17.0F)
            End If
            cashStatIcon(i).Text = icons(i)
            card.Controls.Add(cashStatIcon(i))

            cashStatCards(i) = card
            statsRow.Controls.Add(card)
        Next
        AddHandler statsRow.Resize, AddressOf StatsRow_Resize

        body.Controls.Add(tableCard)    ' Fill first
        body.Controls.Add(statsRow)     ' then the top dock

        pg.Controls.Add(body)
        pg.Controls.Add(hdr)
        pg.ResumeLayout(True)

        StatsRow_Resize(Nothing, EventArgs.Empty)

        AddHandler cashSearch.TextChanged, AddressOf CashFilter_Changed
        AddHandler cashStatus.SelectedIndexChanged, AddressOf CashFilter_Changed
        AddHandler cashRows.Resize, AddressOf CashRows_Resize

        RefreshCashierPage()
    End Sub

    Private Sub StatsRow_Resize(sender As Object, e As EventArgs)
        If statsRow Is Nothing Then Return
        Dim gap As Integer = 16
        Dim cw As Integer = Math.Max(120, (statsRow.ClientSize.Width - 3 * gap) \ 4)
        For i As Integer = 0 To 3
            cashStatCards(i).Size = New Size(cw, 88)
            cashStatCards(i).Location = New Point(i * (cw + gap), 0)
            cashStatIcon(i).Location = New Point(cw - cashStatIcon(i).Width - 18, 30)
        Next
    End Sub

    Private Sub CashFilter_Changed(sender As Object, e As EventArgs)
        RefreshCashierPage()
    End Sub

    Private Sub CashRows_Resize(sender As Object, e As EventArgs)
        If cashRows.Width = lastCashW OrElse cashRows.Width <= 0 Then Return
        RefreshCashierPage()
    End Sub

    '=================================================================
    ' TABLE
    '=================================================================
    ' column x-positions (shared by the header labels and every row)
    Private Function ColX(i As Integer, w As Integer) As Integer
        Dim fr() As Double = {0.0, 0.3, 0.47, 0.62, 0.75, 0.87}
        Dim cw As Integer = w - 24 - 24 - 80
        Return 24 + CInt(cw * fr(i))
    End Function

    Private Sub LayoutCashHeader(w As Integer)
        For i As Integer = 0 To 5
            cashHeaderLabels(i).Location = New Point(ColX(i, w), (42 - cashHeaderLabels(i).Height) \ 2)
        Next
        cashHeaderLabels(6).Location = New Point(w - 24 - 80, (42 - cashHeaderLabels(6).Height) \ 2)
    End Sub

    ''' <summary>Rebuilds the stat cards and the cashier rows (search + status filter applied).</summary>
    Private Sub RefreshCashierPage()
        If cashRows Is Nothing Then Return

        ' ---- stat cards ----
        Dim total As Integer = 0
        Dim active As Integer = 0
        For Each acc As CashierAccount In DataStore.Cashiers
            total += 1
            If acc.IsActive Then active += 1
        Next
        cashStatValue(0).Text = total.ToString()
        cashStatValue(1).Text = active.ToString()
        cashStatValue(2).Text = (total - active).ToString()
        cashStatValue(3).Text = Peso(DataStore.GetTodaySales())

        ' ---- today's transactions per cashier (matched by full name or username) ----
        Dim txCount As New Dictionary(Of String, Integer)
        Dim txSales As New Dictionary(Of String, Decimal)
        For Each t As POS_Transaction In DataStore.Transactions
            If t.TransactionDate.Date <> DateTime.Today Then Continue For

            Dim st As String = Convert.ToString(t.Status)
            If st.IndexOf("void", StringComparison.OrdinalIgnoreCase) >= 0 _
               OrElse st.IndexOf("refund", StringComparison.OrdinalIgnoreCase) >= 0 _
               OrElse st.IndexOf("cancel", StringComparison.OrdinalIgnoreCase) >= 0 Then Continue For

            Dim key As String = Convert.ToString(t.Cashier).Trim().ToLowerInvariant()
            If key = "" Then Continue For

            If txCount.ContainsKey(key) Then txCount(key) += 1 Else txCount(key) = 1
            Dim amt As Decimal = CDec(t.Total)
            If txSales.ContainsKey(key) Then txSales(key) += amt Else txSales(key) = amt
        Next

        ' ---- rows ----
        Dim q As String = cashSearch.Text.Trim()
        Dim mode As Integer = Math.Max(0, cashStatus.SelectedIndex)

        lastCashW = cashRows.Width
        Dim w As Integer = Math.Max(520, cashRows.Width - SystemInformation.VerticalScrollBarWidth)
        LayoutCashHeader(w)

        cashRows.SuspendLayout()
        For i As Integer = cashRows.Controls.Count - 1 To 0 Step -1
            Dim c As Control = cashRows.Controls(i)
            cashRows.Controls.RemoveAt(i)
            c.Dispose()
        Next

        Dim idx As Integer = 0
        For Each acc As CashierAccount In DataStore.Cashiers
            If q <> "" AndAlso acc.FullName.IndexOf(q, StringComparison.OrdinalIgnoreCase) < 0 _
               AndAlso acc.Username.IndexOf(q, StringComparison.OrdinalIgnoreCase) < 0 Then Continue For
            If mode = 1 AndAlso Not acc.IsActive Then Continue For
            If mode = 2 AndAlso acc.IsActive Then Continue For

            Dim k1 As String = acc.FullName.Trim().ToLowerInvariant()
            Dim k2 As String = acc.Username.Trim().ToLowerInvariant()
            Dim cnt As Integer = 0
            Dim sales As Decimal = 0D
            If txCount.ContainsKey(k1) Then
                cnt = txCount(k1) : sales = txSales(k1)
            ElseIf txCount.ContainsKey(k2) Then
                cnt = txCount(k2) : sales = txSales(k2)
            End If

            cashRows.Controls.Add(BuildCashierRow(acc, idx, cnt, sales, w))
            idx += 1
        Next

        If idx = 0 Then
            Dim empty As Label = ForestUi.Lbl("No cashiers found.", 10.0F, FontStyle.Regular, ForestUi.Muted)
            empty.AutoSize = False
            empty.Size = New Size(w, 90)
            empty.TextAlign = ContentAlignment.MiddleCenter
            cashRows.Controls.Add(empty)
        End If
        cashRows.ResumeLayout(True)
    End Sub

    Private Function BuildCashierRow(acc As CashierAccount, idx As Integer, txCount As Integer,
                                     sales As Decimal, w As Integer) As Panel

        Dim row As New Panel()
        row.Size = New Size(w, CashRowH)
        row.Margin = Padding.Empty
        row.BackColor = ForestUi.CardFill

        Dim line As New Panel()
        line.Dock = DockStyle.Bottom
        line.Height = 1
        line.BackColor = ForestUi.RowLine
        row.Controls.Add(line)

        ' avatar (photo when there is one) + name
        Dim even As Boolean = (idx Mod 2 = 0)
        Dim av As Control = Counterly.AvatarFor(Counterly.InitialsOf(acc.FullName), 44,
                                                If(even, ForestUi.AccentSoft, ForestUi.AvatarGray),
                                                If(even, ForestUi.Accent, ForestUi.Ink),
                                                CashierPhotos.Load(acc.Id))
        av.Location = New Point(ColX(0, w), (CashRowH - 44) \ 2)
        row.Controls.Add(av)

        Dim nameLbl As Label = ForestUi.Lbl(acc.FullName, 10.0F, FontStyle.Bold, ForestUi.Ink)
        nameLbl.AutoSize = False
        nameLbl.AutoEllipsis = True
        nameLbl.Size = New Size(Math.Max(60, ColX(1, w) - ColX(0, w) - 56 - 8), 22)
        nameLbl.Location = New Point(ColX(0, w) + 56, (CashRowH - 22) \ 2)
        nameLbl.TextAlign = ContentAlignment.MiddleLeft
        row.Controls.Add(nameLbl)

        row.Controls.Add(CellText(acc.Username, 1, w, ForestUi.Muted, False))
        row.Controls.Add(CellText(acc.DateAdded.ToString("MMM d, yyyy"), 2, w, ForestUi.Muted, False))
        row.Controls.Add(CellText(txCount.ToString(), 3, w, ForestUi.Ink, False))
        row.Controls.Add(CellText(Peso(sales), 4, w, ForestUi.Ink, False))

        ' status pill
        Dim pill As Guna2Panel
        If acc.IsActive Then
            pill = StatusPill("Active", ForestUi.OkBack, ForestUi.OkFore)
        Else
            pill = StatusPill("Inactive", ForestUi.AvatarGray, ForestUi.Muted)
        End If
        pill.Location = New Point(ColX(5, w), (CashRowH - pill.Height) \ 2)
        row.Controls.Add(pill)

        ' actions
        Dim ax As Integer = w - 24 - 80
        Dim ay As Integer = (CashRowH - 36) \ 2

        Dim btnEdit As Guna2Button = ForestUi.GlyphButton(ForestUi.GlyphEdit, 36, ForestUi.AccentSoft, ForestUi.Accent)
        btnEdit.Location = New Point(ax, ay)
        btnEdit.Tag = acc.Id
        cashTip.SetToolTip(btnEdit, "Edit cashier")
        AddHandler btnEdit.Click, AddressOf CashierEdit_Click
        row.Controls.Add(btnEdit)

        Dim btnDelete As Guna2Button = ForestUi.GlyphButton(ForestUi.GlyphDelete, 36, ForestUi.BadBack, ForestUi.Danger)
        btnDelete.Location = New Point(ax + 44, ay)
        btnDelete.Tag = acc.Id
        cashTip.SetToolTip(btnDelete, "Delete cashier account")
        AddHandler btnDelete.Click, AddressOf CashierDelete_Click
        row.Controls.Add(btnDelete)

        Return row
    End Function

    Private Function CellText(text As String, col As Integer, w As Integer, fg As Color, bold As Boolean) As Label
        Dim l As Label = ForestUi.Lbl(text, 9.0F, If(bold, FontStyle.Bold, FontStyle.Regular), fg)
        l.AutoSize = False
        l.AutoEllipsis = True
        Dim nextX As Integer = If(col >= 5, w, ColX(col + 1, w))
        l.Size = New Size(Math.Max(40, nextX - ColX(col, w) - 8), 22)
        l.Location = New Point(ColX(col, w), (CashRowH - 22) \ 2)
        l.TextAlign = ContentAlignment.MiddleLeft
        Return l
    End Function

    Private Function StatusPill(text As String, bg As Color, fg As Color) As Guna2Panel
        Dim f As New Font("Segoe UI", 8.5F, FontStyle.Bold)
        Dim sz As Size = TextRenderer.MeasureText(text, f)
        Dim p As Guna2Panel = ForestUi.Card(sz.Width + 24, 26, 13, bg)
        Dim l As New Label()
        l.Dock = DockStyle.Fill
        l.BackColor = Color.Transparent
        l.ForeColor = fg
        l.Font = f
        l.Text = text
        l.TextAlign = ContentAlignment.MiddleCenter
        p.Controls.Add(l)
        Return p
    End Function

    '=================================================================
    ' ACTIONS
    '=================================================================
    Private Sub AddCashier_Click(sender As Object, e As EventArgs)
        OpenCashierDialog(Nothing)
    End Sub

    Private Sub CashierEdit_Click(sender As Object, e As EventArgs)
        Dim b As Control = TryCast(sender, Control)
        If b Is Nothing OrElse b.Tag Is Nothing Then Return
        Dim acc As CashierAccount = DataStore.FindCashierById(CStr(b.Tag))
        If acc Is Nothing Then Return
        OpenCashierDialog(acc)
    End Sub

    ''' <summary>Trash icon: permanently deletes the cashier account (use the pencil > "Set as inactive" to only block the login).</summary>
    Private Sub CashierDelete_Click(sender As Object, e As EventArgs)
        Dim b As Control = TryCast(sender, Control)
        If b Is Nothing OrElse b.Tag Is Nothing Then Return
        Dim acc As CashierAccount = DataStore.FindCashierById(CStr(b.Tag))
        If acc Is Nothing OrElse Not ShopContext.CanAccess(acc.ShopId) Then Return

        If MessageBox.Show("Delete the account of " & acc.FullName & "?" & vbCrLf & vbCrLf &
                           "The cashier will no longer be able to log in. Past transactions stay in the records." & vbCrLf &
                           "This cannot be undone.", "Delete cashier",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) <> DialogResult.Yes Then Return

        Dim err As String = ""
        If Not DataStore.DeleteCashier(acc.Id, err) Then
            MessageBox.Show(err, "Delete cashier", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Try
            CashierPhotos.Delete(acc.Id)
        Catch
            ' the photo file is optional; the account is already gone
        End Try
        RefreshCashierPage()
        RefreshConversationList()
    End Sub

    ' ---- "Set as inactive / active" lives inside the Edit cashier dialog ----
    Private Sub CollectButtons(parent As Control, found As List(Of Control))
        For Each c As Control In parent.Controls
            If TypeOf c Is ButtonBase OrElse c.GetType().Name = "Guna2Button" Then found.Add(c)
            If c.HasChildren Then CollectButtons(c, found)
        Next
    End Sub

    Private Sub AttachStatusButton(dlg As Form, accId As String)
        Dim acc As CashierAccount = DataStore.FindCashierById(accId)
        If acc Is Nothing Then Return

        Dim btns As New List(Of Control)
        CollectButtons(dlg, btns)

        ' the Save / Cancel row = the lowest buttons of the dialog; the new button goes to the LEFT of that row
        Dim lowest As Integer = -1
        For Each b As Control In btns
            If b.Visible Then lowest = Math.Max(lowest, b.Top)
        Next
        Dim refBtn As Control = Nothing
        For Each b As Control In btns
            If b.Visible AndAlso b.Top >= lowest - 8 AndAlso (refBtn Is Nothing OrElse b.Left < refBtn.Left) Then refBtn = b
        Next

        Dim w As Integer = 150
        Dim h As Integer = If(refBtn IsNot Nothing, refBtn.Height, 40)
        Dim st As Guna2Button = ForestUi.TextButton(If(acc.IsActive, "Set as inactive", "Set as active"), w, h, False)
        Dim parentCtl As Control = dlg
        If refBtn IsNot Nothing Then
            parentCtl = refBtn.Parent
            st.Location = New Point(Math.Max(12, refBtn.Left - 12 - w), refBtn.Top)
        Else
            st.Location = New Point(20, Math.Max(0, dlg.ClientSize.Height - h - 20))
        End If
        parentCtl.Controls.Add(st)
        st.BringToFront()
        AddHandler st.Click, Sub(o As Object, ev As EventArgs) ToggleCashierStatus(dlg, accId)
    End Sub

    Private Sub ToggleCashierStatus(dlg As Form, accId As String)
        Dim acc As CashierAccount = DataStore.FindCashierById(accId)
        If acc Is Nothing Then Return
        Dim makeInactive As Boolean = acc.IsActive
        If MessageBox.Show("Set " & acc.FullName & " as " & If(makeInactive, "inactive?" & vbCrLf & "The cashier will not be able to log in until you set the account active again.", "active?"),
                           "Cashier status", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
        Dim err As String = ""
        If Not DataStore.SetCashierStatus(acc.Id, If(makeInactive, AccountStatus.Inactive, AccountStatus.Active), err) Then
            MessageBox.Show(err, "Cashier status", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        dlg.Close()
    End Sub

    ''' <summary>acc = Nothing opens "Add cashier", otherwise "Edit cashier".</summary>
    Private Sub OpenCashierDialog(acc As CashierAccount)
        Dim isEdit As Boolean = (acc IsNot Nothing)
        Dim photo As Image = If(isEdit, CashierPhotos.Load(acc.Id), Nothing)

        Using dlg As New CashierDialog(isEdit, If(isEdit, acc.FullName, ""), If(isEdit, acc.Username, ""), photo)
            dlg.SaveHandler = Function(d As CashierDialog) SaveCashierFromDialog(acc, d)
            If isEdit Then
                Dim accId As String = acc.Id
                AddHandler dlg.Shown, Sub(o As Object, ev As EventArgs) AttachStatusButton(dlg, accId)
            End If
            dlg.ShowDialog(Me)
        End Using
        RefreshCashierPage()
    End Sub

    Private Function SaveCashierFromDialog(acc As CashierAccount, d As CashierDialog) As String

        Dim err As String = ""
        Dim id As String = ""

        If acc Is Nothing Then
            If Not DataStore.AddCashier(d.FullNameText, d.UsernameText, d.PasswordText, AccountStatus.Active, err) Then
                Return If(err = "", "Could not add the cashier.", err)
            End If
            id = FindCashierIdByUsername(d.UsernameText)
        Else
            Dim status As String = If(acc.IsActive, AccountStatus.Active, AccountStatus.Inactive)
            If Not DataStore.UpdateCashier(acc.Id, d.FullNameText, d.UsernameText, d.PasswordText, status, err) Then
                Return If(err = "", "Could not update the cashier.", err)
            End If
            id = acc.Id
        End If

        ' profile photo
        If id <> "" Then
            If d.PhotoRemoved Then
                CashierPhotos.Delete(id)
            ElseIf d.PhotoChanged AndAlso d.PhotoImage IsNot Nothing Then
                Try
                    CashierPhotos.Save(id, d.PhotoImage)
                Catch ex As Exception
                    Return "Cashier saved, but the photo could not be stored: " & ex.Message
                End Try
            End If
        End If

        RefreshCashierPage()
        RefreshConversationList()
        Return ""
    End Function

    Private Function FindCashierIdByUsername(username As String) As String
        For Each acc As CashierAccount In DataStore.Cashiers
            If String.Equals(acc.Username, username, StringComparison.OrdinalIgnoreCase) Then Return acc.Id
        Next
        Return ""
    End Function

    Private Function CashierAvatar(name As String, size As Integer, fill As Color, fg As Color) As Control
        Dim photo As Image = Nothing
        For Each acc As CashierAccount In DataStore.Cashiers
            If String.Equals(acc.FullName, name, StringComparison.OrdinalIgnoreCase) Then
                photo = CashierPhotos.Load(acc.Id)
                Exit For
            End If
        Next
        Return Counterly.AvatarFor(Counterly.InitialsOf(name), size, fill, fg, photo)
    End Function

End Class