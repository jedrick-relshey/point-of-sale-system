Option Strict On
Option Explicit On

Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports Guna.UI2.WinForms

' ForestUi.vb  -  NEW FILE
' Shared look for the Forest Roast screens (colors, brand, small builders and a few reusable controls).
' Navigation background = 1A0F0D, brand = "Forest Roast".

Public Module ForestUi

    Public Const AppName As String = "Forest Roast"
    Public Const AppSub As String = "CAFE POS"
    Public Const StoreName As String = "Forest Roast Cafe"

    ' ---- navigation (dark brown, sampled from the design) ----
    Public ReadOnly Navy As Color = Color.FromArgb(44, 24, 16)          ' #2C1810  <- change this one line to use #1A0F0D
    Public ReadOnly NavCard As Color = Color.FromArgb(61, 35, 20)       ' #3D2314
    Public ReadOnly NavActive As Color = Color.FromArgb(85, 57, 40)     ' #553928
    Public ReadOnly NavHover As Color = Color.FromArgb(62, 38, 27)
    Public ReadOnly NavText As Color = Color.FromArgb(186, 155, 134)    ' #BA9B86
    Public ReadOnly NavMuted As Color = Color.FromArgb(139, 107, 85)    ' #8B6B55
    Public ReadOnly NavSub As Color = Color.FromArgb(160, 133, 110)     ' #A0856E

    ' ---- accent + neutrals (cream / terracotta) ----
    Public ReadOnly Accent As Color = Color.FromArgb(198, 124, 78)      ' #C67C4E
    Public ReadOnly AccentDark As Color = Color.FromArgb(172, 101, 58)
    Public ReadOnly AccentSoft As Color = Color.FromArgb(245, 230, 216) ' #F5E6D8
    Public ReadOnly Ink As Color = Color.FromArgb(28, 16, 9)            ' #1C1009
    Public ReadOnly Muted As Color = Color.FromArgb(139, 107, 85)       ' #8B6B55
    Public ReadOnly MutedLight As Color = Color.FromArgb(184, 162, 144) ' #B8A290
    Public ReadOnly CardFill As Color = Color.FromArgb(255, 250, 245)   ' #FFFAF5
    Public ReadOnly Border As Color = Color.FromArgb(236, 226, 215)     ' #ECE2D7
    Public ReadOnly Page As Color = Color.FromArgb(245, 239, 230)       ' #F5EFE6
    Public ReadOnly ChatBg As Color = Color.FromArgb(245, 239, 230)
    Public ReadOnly AvatarGray As Color = Color.FromArgb(234, 221, 208) ' #EADDD0
    Public ReadOnly Danger As Color = Color.FromArgb(204, 74, 88)       ' #CC4A58
    Public ReadOnly Green As Color = Color.FromArgb(123, 174, 116)      ' #7BAE74
    Public ReadOnly Bubble As Color = Color.FromArgb(44, 24, 16)
    Public ReadOnly HeaderRow As Color = Color.FromArgb(245, 239, 230)
    Public ReadOnly RowLine As Color = Color.FromArgb(242, 235, 224)
    Public ReadOnly BarColor As Color = Color.FromArgb(222, 201, 179)   ' #DEC9B3

    ' ---- status colors ----
    Public ReadOnly OkBack As Color = Color.FromArgb(232, 244, 232)
    Public ReadOnly OkFore As Color = Color.FromArgb(74, 140, 74)
    Public ReadOnly WarnBack As Color = Color.FromArgb(245, 230, 216)
    Public ReadOnly WarnFore As Color = Color.FromArgb(198, 124, 78)
    Public ReadOnly BadBack As Color = Color.FromArgb(253, 235, 237)
    Public ReadOnly BadFore As Color = Color.FromArgb(204, 74, 88)

    ' ---- Segoe MDL2 Assets glyphs ----
    Public ReadOnly GlyphSearch As String = ChrW(&HE721)
    Public ReadOnly GlyphPower As String = ChrW(&HE7E8)
    Public ReadOnly GlyphPhone As String = ChrW(&HE717)
    Public ReadOnly GlyphMore As String = ChrW(&HE712)
    Public ReadOnly GlyphHelp As String = ChrW(&HE897)
    Public ReadOnly GlyphEdit As String = ChrW(&HE70F)
    Public ReadOnly GlyphBell As String = ChrW(&HEA8F)
    Public ReadOnly GlyphDownload As String = ChrW(&HE896)
    Public ReadOnly GlyphAdd As String = ChrW(&HE710)
    Public ReadOnly GlyphDelete As String = ChrW(&HE74D)
    Public ReadOnly GlyphAccept As String = ChrW(&HE73E)
    Public ReadOnly GlyphMinus As String = ChrW(&HE738)
    Public ReadOnly GlyphClose As String = ChrW(&HE711)
    Public ReadOnly GlyphBox As String = ChrW(&HE7B8)
    Public ReadOnly GlyphWarn As String = ChrW(&HE7BA)
    Public ReadOnly GlyphError As String = ChrW(&HEA39)
    Public ReadOnly GlyphChat As String = ChrW(&HE8BD)

    '=================================================================
    ' SMALL BUILDERS
    '=================================================================
    Public Function Lbl(text As String, size As Single, style As FontStyle, fore As Color) As Label
        Dim l As New Label()
        l.AutoSize = True
        l.BackColor = Color.Transparent
        l.ForeColor = fore
        l.Font = New Font("Segoe UI", size, style)
        l.UseMnemonic = False
        l.Text = text
        Return l
    End Function

    Public Function Lbl(text As String, size As Single, style As FontStyle, fore As Color, x As Integer, y As Integer) As Label
        Dim l As Label = Lbl(text, size, style, fore)
        l.Location = New Point(x, y)
        Return l
    End Function

    Public Function Card(w As Integer, h As Integer, radius As Integer, fill As Color) As Guna2Panel
        Dim p As New Guna2Panel()
        p.Size = New Size(w, h)
        p.BorderRadius = radius
        p.FillColor = fill
        p.BackColor = Color.Transparent
        p.BorderThickness = 0
        Return p
    End Function

    Public Function Card(w As Integer, h As Integer, radius As Integer, fill As Color, edge As Color) As Guna2Panel
        Dim p As Guna2Panel = Card(w, h, radius, fill)
        p.BorderThickness = 1
        p.BorderColor = edge
        Return p
    End Function

    Public Function GlyphButton(glyph As String, size As Integer, back As Color, fore As Color) As Guna2Button
        Dim b As New Guna2Button()
        b.Size = New Size(size, size)
        b.BorderRadius = 10
        b.FillColor = back
        b.ForeColor = fore
        b.Font = New Font("Segoe MDL2 Assets", 11.0F)
        b.Text = glyph
        b.Animated = False
        b.Cursor = Cursors.Hand
        b.HoverState.FillColor = Shade(back, -0.06F)
        b.HoverState.ForeColor = fore
        b.PressedColor = Shade(back, -0.1F)
        Return b
    End Function

    ''' <summary>Rectangular text button: primary = teal filled, otherwise white with a border.</summary>
    Public Function TextButton(text As String, w As Integer, h As Integer, primary As Boolean) As Guna2Button
        Dim b As New Guna2Button()
        b.Size = New Size(w, h)
        b.BorderRadius = 10
        b.Animated = False
        b.Cursor = Cursors.Hand
        b.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        b.Text = text
        If primary Then
            b.FillColor = Accent
            b.ForeColor = Color.White
            b.HoverState.FillColor = AccentDark
            b.HoverState.ForeColor = Color.White
        Else
            b.FillColor = ForestUi.CardFill
            b.ForeColor = Ink
            b.BorderThickness = 1
            b.BorderColor = Border
            b.HoverState.FillColor = Page
            b.HoverState.ForeColor = Ink
        End If
        Return b
    End Function

    Public Function Shade(c As Color, amount As Single) As Color
        Dim r As Integer = CInt(c.R + (If(amount < 0, c.R, 255 - c.R)) * amount)
        Dim g As Integer = CInt(c.G + (If(amount < 0, c.G, 255 - c.G)) * amount)
        Dim b As Integer = CInt(c.B + (If(amount < 0, c.B, 255 - c.B)) * amount)
        Return Color.FromArgb(255, Math.Max(0, Math.Min(255, r)), Math.Max(0, Math.Min(255, g)), Math.Max(0, Math.Min(255, b)))
    End Function

    Public Function Initials(fullName As String) As String
        Dim result As String = ""
        For Each part As String In If(fullName, "").Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
            result &= Char.ToUpper(part(0))
            If result.Length = 2 Then Exit For
        Next
        If result = "" Then result = "?"
        Return result
    End Function

    Public Function Avatar(text As String, size As Integer, back As Color, fore As Color) As Guna2Panel
        Dim p As Guna2Panel = Card(size, size, size \ 2, back)
        Dim l As New Label()
        l.Dock = DockStyle.Fill
        l.BackColor = Color.Transparent
        l.ForeColor = fore
        l.Font = New Font("Segoe UI", Math.Max(7.0F, size * 0.28F), FontStyle.Bold)
        l.TextAlign = ContentAlignment.MiddleCenter
        l.Text = text
        p.Controls.Add(l)
        Return p
    End Function

    Public Function TimeAgo(t As DateTime) As String
        Dim span As TimeSpan = DateTime.Now - t
        If span.TotalMinutes < 1 Then Return "now"
        If span.TotalMinutes < 60 Then Return CInt(span.TotalMinutes).ToString() & " min"
        If span.TotalHours < 24 Then Return CInt(span.TotalHours).ToString() & " hr"
        If t.Date = DateTime.Today.AddDays(-1) Then Return "Yesterday"
        Return t.ToString("MMM d")
    End Function

    ''' <summary>Rounded text badge ("In stock", "Completed" ...).</summary>
    Public Function Pill(text As String, back As Color, fore As Color) As Guna2Panel
        Dim f As New Font("Segoe UI", 8.0F, FontStyle.Bold)
        Dim sz As Size = TextRenderer.MeasureText(text, f)
        Dim p As Guna2Panel = Card(sz.Width + 20, 22, 11, back)
        Dim l As New Label()
        l.Dock = DockStyle.Fill
        l.BackColor = Color.Transparent
        l.ForeColor = fore
        l.Font = f
        l.TextAlign = ContentAlignment.MiddleCenter
        l.Text = text
        p.Controls.Add(l)
        Return p
    End Function

    Public Function StatusPill(status As String) As Guna2Panel
        If status = StockStatus.OutOfStock Then Return Pill("Out of stock", BadBack, BadFore)
        If status = StockStatus.LowStock Then Return Pill("Low stock", WarnBack, WarnFore)
        Return Pill("In stock", OkBack, OkFore)
    End Function

    ''' <summary>White rounded search box with a magnifier. Returns the host; the text box comes back in txt.</summary>
    Public Function SearchBox(w As Integer, h As Integer, placeholder As String, ByRef txt As Guna2TextBox) As Guna2Panel
        Dim host As Guna2Panel = Card(w, h, 10, ForestUi.CardFill, Border)
        Dim icon As New Label()
        icon.AutoSize = True
        icon.BackColor = Color.Transparent
        icon.ForeColor = Muted
        icon.Font = New Font("Segoe MDL2 Assets", 11.0F)
        icon.Text = GlyphSearch
        icon.Location = New Point(14, (h - 20) \ 2)
        host.Controls.Add(icon)

        Dim t As New Guna2TextBox()
        t.BorderThickness = 0
        t.BorderRadius = 0
        t.FillColor = ForestUi.CardFill
        t.Font = New Font("Segoe UI", 9.5F)
        t.ForeColor = Ink
        t.PlaceholderText = placeholder
        t.PlaceholderForeColor = Muted
        t.Location = New Point(40, (h - 32) \ 2)
        t.Size = New Size(w - 52, 32)
        t.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top
        host.Controls.Add(t)
        txt = t
        Return host
    End Function

    Public Sub StyleCombo(cb As Guna2ComboBox, w As Integer, h As Integer)
        cb.Size = New Size(w, h)
        cb.BorderRadius = 10
        cb.BorderColor = Border
        cb.BorderThickness = 1
        cb.FillColor = ForestUi.CardFill
        cb.ForeColor = Ink
        cb.Font = New Font("Segoe UI", 9.0F)
        cb.DropDownStyle = ComboBoxStyle.DropDownList
        cb.ItemHeight = Math.Max(20, h - 12)
        cb.BackColor = Color.Transparent
        cb.FocusedState.BorderColor = Accent
        cb.HoverState.BorderColor = Accent
    End Sub

    ''' <summary>Rounded text input used in dialogs (a host panel with a plain TextBox inside, so it never mis-sizes).</summary>
    Public Function TextInput(w As Integer, h As Integer, ByRef txt As TextBox) As Guna2Panel
        Dim host As Guna2Panel = Card(w, h, 10, ForestUi.CardFill, Border)
        Dim t As New TextBox()
        t.BorderStyle = BorderStyle.None
        t.BackColor = ForestUi.CardFill
        t.ForeColor = Ink
        t.Font = New Font("Segoe UI", 9.5F)
        t.Width = w - 24
        t.Location = New Point(12, (h - t.PreferredHeight) \ 2)
        t.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top
        AddHandler t.Enter, Sub(s As Object, ev As EventArgs) host.BorderColor = Accent
        AddHandler t.Leave, Sub(s As Object, ev As EventArgs) host.BorderColor = Border
        host.Controls.Add(t)
        txt = t
        Return host
    End Function

    Public Function RoundedPath(r As Rectangle, radius As Integer) As GraphicsPath
        Dim gp As New GraphicsPath()
        Dim d As Integer = radius * 2
        gp.AddArc(r.X, r.Y, d, d, 180, 90)
        gp.AddArc(r.Right - d, r.Y, d, d, 270, 90)
        gp.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90)
        gp.AddArc(r.X, r.Bottom - d, d, d, 90, 90)
        gp.CloseFigure()
        Return gp
    End Function

    Public Function PesoText(amount As Decimal) As String
        Return ChrW(&H20B1) & amount.ToString("#,##0.00")
    End Function

End Module

'=====================================================================
' KPI CARD  (title, big number, change text bottom-right, small icon top-right)
'=====================================================================
Public Class ForestKpi
    Inherits Guna2Panel

    Public ReadOnly ValueLabel As Label
    Public ReadOnly DeltaLabel As Label
    Private ReadOnly iconTile As Guna2Panel

    Public Sub New(title As String, glyph As String, iconBack As Color, iconFore As Color)
        MyBase.New()
        Me.BorderRadius = 14
        Me.FillColor = ForestUi.CardFill
        Me.BorderThickness = 1
        Me.BorderColor = ForestUi.Border
        Me.BackColor = Color.Transparent
        Me.Size = New Size(240, 88)

        Me.Controls.Add(ForestUi.Lbl(title, 8.75F, FontStyle.Regular, ForestUi.Muted, 16, 14))

        ValueLabel = ForestUi.Lbl("0", 18.0F, FontStyle.Bold, ForestUi.Ink, 15, 40)
        Me.Controls.Add(ValueLabel)

        DeltaLabel = ForestUi.Lbl("", 8.0F, FontStyle.Bold, ForestUi.Green)
        Me.Controls.Add(DeltaLabel)

        iconTile = ForestUi.Card(30, 30, 8, iconBack)
        Dim g As New Label()
        g.Dock = DockStyle.Fill
        g.BackColor = Color.Transparent
        g.ForeColor = iconFore
        If glyph.Length > 0 AndAlso AscW(glyph(0)) >= &HE000 Then
            g.Font = New Font("Segoe MDL2 Assets", 11.0F)
        Else
            g.Font = New Font("Segoe UI Symbol", 11.0F, FontStyle.Bold)
        End If
        g.TextAlign = ContentAlignment.MiddleCenter
        g.Text = glyph
        iconTile.Controls.Add(g)
        Me.Controls.Add(iconTile)
        PlaceParts()
    End Sub

    ''' <summary>deltaText such as "+12.8%"; positive is green, negative is red, empty hides it.</summary>
    Public Sub SetValue(valueText As String, deltaText As String, positive As Boolean)
        ValueLabel.Text = valueText
        DeltaLabel.Text = deltaText
        DeltaLabel.ForeColor = If(positive, ForestUi.OkFore, ForestUi.Danger)
        PlaceParts()
    End Sub

    Private Sub PlaceParts()
        iconTile.Location = New Point(Me.Width - iconTile.Width - 14, 12)
        DeltaLabel.Location = New Point(Me.Width - DeltaLabel.Width - 16, Me.Height - DeltaLabel.Height - 16)
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        If iconTile IsNot Nothing AndAlso DeltaLabel IsNot Nothing Then PlaceParts()
    End Sub
End Class

'=====================================================================
' BAR CHART  (one bar per bucket, highlighted bar in accent color)
'=====================================================================
Public Class ForestBarChart
    Inherits Panel

    Private values As Decimal() = New Decimal() {}
    Private labels As String() = New String() {}
    Private highlight As Integer = -1
    Private hoverIndex As Integer = -1
    Private ReadOnly tip As New ToolTip()

    Public Sub New()
        MyBase.New()
        Me.DoubleBuffered = True
        Me.ResizeRedraw = True
        Me.BackColor = ForestUi.CardFill
    End Sub

    Public Sub SetData(vals As Decimal(), labs As String(), highlightIndex As Integer)
        values = vals
        labels = labs
        highlight = highlightIndex
        Me.Invalidate()
    End Sub

    Private Function BarRect(i As Integer, maxValue As Decimal) As Rectangle
        Dim n As Integer = values.Length
        Dim labelH As Integer = 24
        Dim gap As Integer = Math.Max(6, Me.Width \ (n * 7 + 1))
        Dim barW As Integer = Math.Max(4, (Me.Width - gap * (n - 1)) \ n)
        Dim areaH As Integer = Math.Max(10, Me.Height - labelH - 4)
        Dim h As Integer = 6
        If maxValue > 0 Then h = Math.Max(6, CInt(areaH * 0.96 * CDbl(values(i) / maxValue)))
        Dim x As Integer = i * (barW + gap)
        Return New Rectangle(x, areaH - h + 2, barW, h)
    End Function

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim n As Integer = values.Length
        If n = 0 Then Return
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        Dim maxValue As Decimal = 0
        For Each v As Decimal In values
            If v > maxValue Then maxValue = v
        Next
        Dim labelFont As New Font("Segoe UI", 7.75F)
        Dim every As Integer = If(n > 16, 2, 1)
        For i As Integer = 0 To n - 1
            Dim r As Rectangle = BarRect(i, maxValue)
            Dim fill As Color = If(i = highlight, ForestUi.Accent, ForestUi.BarColor)
            If i = hoverIndex AndAlso i <> highlight Then fill = ForestUi.Shade(ForestUi.BarColor, -0.08F)
            Using gp As New GraphicsPath()
                Dim rad As Integer = Math.Max(1, Math.Min(5, r.Width \ 2))
                gp.AddArc(r.X, r.Y, rad * 2, rad * 2, 180, 90)
                gp.AddArc(r.Right - rad * 2, r.Y, rad * 2, rad * 2, 270, 90)
                gp.AddLine(r.Right, r.Bottom, r.X, r.Bottom)
                gp.CloseFigure()
                Using br As New SolidBrush(fill)
                    g.FillPath(br, gp)
                End Using
            End Using
            If i Mod every = 0 AndAlso i < labels.Length Then
                Dim sz As Size = TextRenderer.MeasureText(labels(i), labelFont)
                TextRenderer.DrawText(g, labels(i), labelFont, New Point(r.X + (r.Width - sz.Width) \ 2, Me.Height - 20), ForestUi.MutedLight)
            End If
        Next
        labelFont.Dispose()
    End Sub

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        Dim n As Integer = values.Length
        If n = 0 Then Return
        Dim maxValue As Decimal = 0
        For Each v As Decimal In values
            If v > maxValue Then maxValue = v
        Next
        Dim found As Integer = -1
        For i As Integer = 0 To n - 1
            Dim r As Rectangle = BarRect(i, maxValue)
            If e.X >= r.X AndAlso e.X <= r.Right Then
                found = i
                Exit For
            End If
        Next
        If found <> hoverIndex Then
            hoverIndex = found
            If found >= 0 AndAlso found < labels.Length Then
                tip.SetToolTip(Me, labels(found) & ": " & ForestUi.PesoText(values(found)))
            End If
            Me.Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        If hoverIndex <> -1 Then
            hoverIndex = -1
            Me.Invalidate()
        End If
    End Sub
End Class

'=====================================================================
' SEGMENTED SWITCH  (All | Low Stock | Out of Stock, Daily | Weekly ...)
'=====================================================================
Public Class ForestSegmented
    Inherits Guna2Panel

    Public Event SelectedChanged As EventHandler
    Private ReadOnly buttons As New List(Of Guna2Button)
    Private current As Integer = 0

    Public Sub New(items As String())
        MyBase.New()
        Me.BorderRadius = 10
        Me.BorderThickness = 1
        Me.BorderColor = ForestUi.Border
        Me.FillColor = ForestUi.HeaderRow
        Me.BackColor = Color.Transparent

        Dim f As New Font("Segoe UI", 8.75F, FontStyle.Bold)
        Dim x As Integer = 3
        For i As Integer = 0 To items.Length - 1
            Dim w As Integer = TextRenderer.MeasureText(items(i), f).Width + 30
            Dim b As New Guna2Button()
            b.Text = items(i)
            b.Font = f
            b.Size = New Size(w, 30)
            b.Location = New Point(x, 3)
            b.BorderRadius = 8
            b.Animated = False
            b.Cursor = Cursors.Hand
            b.Tag = i
            AddHandler b.Click, AddressOf Segment_Click
            buttons.Add(b)
            Me.Controls.Add(b)
            x += w
        Next
        Me.Size = New Size(x + 3, 36)
        ApplyLook()
    End Sub

    Public Property SelectedIndex As Integer
        Get
            Return current
        End Get
        Set(value As Integer)
            If value < 0 OrElse value >= buttons.Count OrElse value = current Then Return
            current = value
            ApplyLook()
            RaiseEvent SelectedChanged(Me, EventArgs.Empty)
        End Set
    End Property

    Private Sub Segment_Click(sender As Object, e As EventArgs)
        Dim b As Guna2Button = TryCast(sender, Guna2Button)
        If b Is Nothing Then Return
        Me.SelectedIndex = CInt(b.Tag)
    End Sub

    Private Sub ApplyLook()
        For i As Integer = 0 To buttons.Count - 1
            Dim b As Guna2Button = buttons(i)
            Dim on1 As Boolean = (i = current)
            b.FillColor = If(on1, ForestUi.CardFill, Color.Transparent)
            b.ForeColor = If(on1, ForestUi.Ink, ForestUi.Muted)
            b.HoverState.FillColor = If(on1, ForestUi.CardFill, Color.FromArgb(236, 241, 245))
            b.HoverState.ForeColor = If(on1, ForestUi.Ink, ForestUi.Ink)
            b.BorderThickness = If(on1, 1, 0)
            b.BorderColor = ForestUi.Border
        Next
    End Sub
End Class

'=====================================================================
' PAGE HEADER  (title, subtitle, action buttons, bell, user, logout)
'=====================================================================
Public Class ForestHeader
    Inherits Panel

    Public Event LogoutClicked As EventHandler
    Public ReadOnly Bell As Guna2Button
    Public ReadOnly TitleLabel As Label
    Public ReadOnly SubtitleLabel As Label
    Private ReadOnly logoutBtn As Guna2Button
    Private ReadOnly divider As Panel
    Private ReadOnly nameLbl As Label
    Private ReadOnly roleLbl As Label
    Private avatarHost As Panel
    Private ReadOnly actions As New List(Of Control)

    Public Sub New(title As String, subtitle As String)
        MyBase.New()
        Me.Dock = DockStyle.Top
        Me.Height = 76
        Me.BackColor = ForestUi.CardFill

        Dim line As New Panel()
        line.Dock = DockStyle.Bottom
        line.Height = 1
        line.BackColor = ForestUi.Border
        Me.Controls.Add(line)

        TitleLabel = ForestUi.Lbl(title, 16.0F, FontStyle.Bold, ForestUi.Ink, 26, 11)
        SubtitleLabel = ForestUi.Lbl(subtitle, 9.0F, FontStyle.Regular, ForestUi.Muted, 27, 44)
        Me.Controls.Add(TitleLabel)
        Me.Controls.Add(SubtitleLabel)

        Bell = ForestUi.GlyphButton(ForestUi.GlyphBell, 38, ForestUi.CardFill, ForestUi.Ink)
        Bell.BorderThickness = 1
        Bell.BorderColor = ForestUi.Border
        Me.Controls.Add(Bell)

        divider = New Panel()
        divider.Size = New Size(1, 38)
        divider.BackColor = ForestUi.Border
        Me.Controls.Add(divider)

        nameLbl = ForestUi.Lbl("", 9.5F, FontStyle.Bold, ForestUi.Ink)
        roleLbl = ForestUi.Lbl("", 8.0F, FontStyle.Regular, ForestUi.Muted)
        Me.Controls.Add(nameLbl)
        Me.Controls.Add(roleLbl)

        logoutBtn = ForestUi.GlyphButton(ForestUi.GlyphPower, 34, ForestUi.CardFill, ForestUi.Danger)
        Me.Controls.Add(logoutBtn)
        AddHandler logoutBtn.Click, Sub(s As Object, ev As EventArgs) RaiseEvent LogoutClicked(Me, EventArgs.Empty)

        SetUser("", "")
    End Sub

    Public Sub AddAction(c As Control)
        actions.Add(c)
        Me.Controls.Add(c)
        LayoutItems()
    End Sub

    Public Sub SetUser(fullName As String, role As String)
        If avatarHost IsNot Nothing Then
            Me.Controls.Remove(avatarHost)
            avatarHost.Dispose()
        End If
        avatarHost = ForestUi.Avatar(ForestUi.Initials(fullName), 38, ForestUi.AccentSoft, ForestUi.Accent)
        Me.Controls.Add(avatarHost)
        nameLbl.Text = fullName
        roleLbl.Text = role
        LayoutItems()
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        LayoutItems()
    End Sub

    Private Sub LayoutItems()
        If logoutBtn Is Nothing OrElse avatarHost Is Nothing Then Return
        Dim x As Integer = Me.ClientSize.Width - 22

        logoutBtn.Location = New Point(x - logoutBtn.Width, 20)
        x -= logoutBtn.Width + 12

        Dim tw As Integer = Math.Max(nameLbl.Width, roleLbl.Width)
        nameLbl.Location = New Point(x - tw, 20)
        roleLbl.Location = New Point(x - tw, 40)
        x -= tw + 10

        avatarHost.Location = New Point(x - avatarHost.Width, 19)
        x -= avatarHost.Width + 16

        divider.Location = New Point(x, 19)
        x -= 16
        Bell.Location = New Point(x - Bell.Width, 19)
        x -= Bell.Width + 12

        For i As Integer = actions.Count - 1 To 0 Step -1
            Dim c As Control = actions(i)
            c.Location = New Point(x - c.Width, (Me.Height - c.Height) \ 2)
            x -= c.Width + 10
        Next
    End Sub
End Class

'=====================================================================
' TABLE  (header row + rows made of ordinary controls, scrolls when needed)
'=====================================================================
Public Class TableColumn
    Public Title As String
    Public Weight As Single
    Public MinWidth As Integer

    Public Sub New(title As String, weight As Single, minWidth As Integer)
        Me.Title = title
        Me.Weight = weight
        Me.MinWidth = minWidth
    End Sub
End Class

Public Class ForestTable
    Inherits Panel

    Public RowHeight As Integer = 54
    Private ReadOnly header As Panel
    Private ReadOnly body As Panel
    Private ReadOnly cols As New List(Of TableColumn)
    Private ReadOnly headerLabels As New List(Of Label)
    Private ReadOnly rows As New List(Of Panel)
    Private ReadOnly rowCells As New List(Of Control())
    Private colLeft As Integer() = New Integer() {}
    Private colWidth As Integer() = New Integer() {}

    Public Sub New()
        MyBase.New()
        Me.BackColor = ForestUi.CardFill
        header = New Panel()
        header.Dock = DockStyle.Top
        header.Height = 40
        header.BackColor = ForestUi.HeaderRow
        body = New Panel()
        body.Dock = DockStyle.Fill
        body.AutoScroll = True
        body.BackColor = ForestUi.CardFill
        Me.Controls.Add(body)
        Me.Controls.Add(header)
    End Sub

    Public Sub SetColumns(columns As TableColumn())
        cols.Clear()
        For Each h As Label In headerLabels
            header.Controls.Remove(h)
            h.Dispose()
        Next
        headerLabels.Clear()
        For Each c As TableColumn In columns
            cols.Add(c)
            Dim l As Label = ForestUi.Lbl(c.Title, 8.5F, FontStyle.Regular, ForestUi.Muted)
            header.Controls.Add(l)
            headerLabels.Add(l)
        Next
        LayoutAll()
    End Sub

    Public Sub ClearRows()
        body.SuspendLayout()
        For i As Integer = rows.Count - 1 To 0 Step -1
            body.Controls.Remove(rows(i))
            rows(i).Dispose()
        Next
        rows.Clear()
        rowCells.Clear()
        body.AutoScrollPosition = New Point(0, 0)
        body.ResumeLayout()
    End Sub

    ''' <summary>Adds one row. cells(i) goes under column i (use Nothing to leave a column empty).</summary>
    Public Sub AddRow(cells As Control())
        Dim r As New Panel()
        r.Size = New Size(Math.Max(100, body.ClientSize.Width), RowHeight)
        r.Location = New Point(0, rows.Count * RowHeight)
        r.BackColor = ForestUi.CardFill
        Dim line As New Panel()
        line.Dock = DockStyle.Bottom
        line.Height = 1
        line.BackColor = ForestUi.RowLine
        r.Controls.Add(line)
        For Each c As Control In cells
            If c IsNot Nothing Then r.Controls.Add(c)
        Next
        body.Controls.Add(r)
        rows.Add(r)
        rowCells.Add(cells)
        PlaceRow(rows.Count - 1)
    End Sub

    Public Sub ShowEmptyMessage(text As String)
        Dim l As Label = ForestUi.Lbl(text, 10.0F, FontStyle.Regular, ForestUi.Muted)
        Dim r As New Panel()
        r.Size = New Size(Math.Max(100, body.ClientSize.Width), 90)
        r.Location = New Point(0, 0)
        r.BackColor = ForestUi.CardFill
        l.Location = New Point(24, 34)
        r.Controls.Add(l)
        body.Controls.Add(r)
        rows.Add(r)
        rowCells.Add(New Control() {})
    End Sub

    Private Sub ComputeColumns()
        Dim n As Integer = cols.Count
        ReDim colLeft(Math.Max(0, n - 1))
        ReDim colWidth(Math.Max(0, n - 1))
        If n = 0 Then Return
        Dim total As Single = 0
        For Each c As TableColumn In cols
            total += c.Weight
        Next
        Dim avail As Integer = Math.Max(300, body.ClientSize.Width - 48)
        Dim x As Integer = 24
        For i As Integer = 0 To n - 1
            Dim w As Integer = Math.Max(cols(i).MinWidth, CInt(avail * cols(i).Weight / total))
            colLeft(i) = x
            colWidth(i) = w
            x += w
        Next
    End Sub

    Private Sub PlaceRow(index As Integer)
        If index < 0 OrElse index >= rows.Count Then Return
        If colLeft.Length = 0 Then Return
        Dim r As Panel = rows(index)
        r.Width = Math.Max(100, body.ClientSize.Width)
        Dim cells As Control() = rowCells(index)
        For i As Integer = 0 To cells.Length - 1
            If i >= colLeft.Length Then Exit For
            Dim c As Control = cells(i)
            If c Is Nothing Then Continue For
            Dim lb As Label = TryCast(c, Label)
            If lb IsNot Nothing AndAlso Not lb.AutoSize Then lb.Width = Math.Max(20, colWidth(i) - 12)
            c.Location = New Point(colLeft(i), Math.Max(0, (RowHeight - 1 - c.Height) \ 2))
        Next
    End Sub

    Private Sub LayoutAll()
        ComputeColumns()
        For i As Integer = 0 To headerLabels.Count - 1
            If i < colLeft.Length Then headerLabels(i).Location = New Point(colLeft(i), (header.Height - headerLabels(i).Height) \ 2)
        Next
        For i As Integer = 0 To rows.Count - 1
            PlaceRow(i)
        Next
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        If body IsNot Nothing Then LayoutAll()
    End Sub

    Public ReadOnly Property ColumnLeftOf(i As Integer) As Integer
        Get
            If i < 0 OrElse i >= colLeft.Length Then Return 0
            Return colLeft(i)
        End Get
    End Property

    Public ReadOnly Property ColumnWidthOf(i As Integer) As Integer
        Get
            If i < 0 OrElse i >= colWidth.Length Then Return 100
            Return colWidth(i)
        End Get
    End Property
End Class

'=====================================================================
' PAGER  ("Showing 1-8 of 248"   Previous 1 2 3 Next)
'=====================================================================
Public Class ForestPager
    Inherits Panel

    Public Event PageChanged As EventHandler
    Public PageSize As Integer = 8
    Private total As Integer = 0
    Private pageNo As Integer = 1
    Private ReadOnly info As Label
    Private ReadOnly navHost As Panel

    Public Sub New()
        MyBase.New()
        Me.Dock = DockStyle.Bottom
        Me.Height = 58
        Me.BackColor = ForestUi.CardFill
        info = ForestUi.Lbl("", 8.5F, FontStyle.Regular, ForestUi.Muted, 24, 21)
        Me.Controls.Add(info)
        navHost = New Panel()
        navHost.BackColor = ForestUi.CardFill
        navHost.Height = 36
        Me.Controls.Add(navHost)
    End Sub

    Public ReadOnly Property CurrentPage As Integer
        Get
            Return pageNo
        End Get
    End Property

    Public ReadOnly Property PageCount As Integer
        Get
            Return Math.Max(1, CInt(Math.Ceiling(total / CDbl(Math.Max(1, PageSize)))))
        End Get
    End Property

    Public Sub SetTotal(count As Integer, noun As String)
        total = count
        If pageNo > PageCount Then pageNo = PageCount
        If pageNo < 1 Then pageNo = 1
        Dim first As Integer = If(total = 0, 0, (pageNo - 1) * PageSize + 1)
        Dim last As Integer = Math.Min(total, pageNo * PageSize)
        info.Text = "Showing " & first.ToString() & ChrW(&H2013) & last.ToString() & " of " & total.ToString() & " " & noun
        BuildButtons()
    End Sub

    Public Sub GoToFirst()
        pageNo = 1
    End Sub

    Private Sub BuildButtons()
        For i As Integer = navHost.Controls.Count - 1 To 0 Step -1
            Dim c As Control = navHost.Controls(i)
            navHost.Controls.RemoveAt(i)
            c.Dispose()
        Next
        Dim x As Integer = 0

        Dim prev As Guna2Button = ForestUi.TextButton("Previous", 84, 36, False)
        prev.Location = New Point(x, 0)
        prev.Enabled = (pageNo > 1)
        AddHandler prev.Click, Sub(s As Object, ev As EventArgs) Goto1(pageNo - 1)
        navHost.Controls.Add(prev)
        x += 84 + 10

        Dim shownPages As Integer = Math.Min(PageCount, 5)
        Dim startPage As Integer = Math.Max(1, Math.Min(pageNo - 2, PageCount - shownPages + 1))
        For p As Integer = startPage To startPage + shownPages - 1
            Dim n As Integer = p
            Dim isOn As Boolean = (n = pageNo)
            Dim b As New Guna2Button()
            b.Size = New Size(28, 28)
            b.BorderRadius = 14
            b.Animated = False
            b.Cursor = Cursors.Hand
            b.Font = New Font("Segoe UI", 8.5F, FontStyle.Bold)
            b.Text = n.ToString()
            b.FillColor = If(isOn, ForestUi.Accent, ForestUi.AvatarGray)
            b.ForeColor = If(isOn, Color.White, ForestUi.Ink)
            b.HoverState.FillColor = If(isOn, ForestUi.AccentDark, ForestUi.Border)
            b.HoverState.ForeColor = If(isOn, Color.White, ForestUi.Ink)
            b.Location = New Point(x, 4)
            AddHandler b.Click, Sub(s As Object, ev As EventArgs) Goto1(n)
            navHost.Controls.Add(b)
            x += 28 + 6
        Next
        x += 4

        Dim nxt As Guna2Button = ForestUi.TextButton("Next", 64, 36, False)
        nxt.Location = New Point(x, 0)
        nxt.Enabled = (pageNo < PageCount)
        AddHandler nxt.Click, Sub(s As Object, ev As EventArgs) Goto1(pageNo + 1)
        navHost.Controls.Add(nxt)
        x += 64

        navHost.Width = x
        PlaceNav()
    End Sub

    Private Sub Goto1(n As Integer)
        If n < 1 OrElse n > PageCount OrElse n = pageNo Then Return
        pageNo = n
        RaiseEvent PageChanged(Me, EventArgs.Empty)
    End Sub

    Private Sub PlaceNav()
        navHost.Location = New Point(Me.ClientSize.Width - navHost.Width - 24, (Me.Height - navHost.Height) \ 2)
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        If navHost IsNot Nothing Then PlaceNav()
    End Sub
End Class

'=====================================================================
' DIALOG  (borderless rounded window used for Add / Edit forms)
'=====================================================================
Public Class ForestDialog
    Inherits Form

    Public ReadOnly Body As Panel
    Public ReadOnly Footer As Panel

    Public Sub New(title As String, subtitle As String, w As Integer, h As Integer)
        MyBase.New()
        Me.FormBorderStyle = FormBorderStyle.None
        Me.StartPosition = FormStartPosition.CenterParent
        Me.ShowInTaskbar = False
        Me.Size = New Size(w, h)
        Me.BackColor = ForestUi.Border
        Me.Padding = New Padding(1)
        Me.KeyPreview = True

        Dim inner As New Panel()
        inner.Dock = DockStyle.Fill
        inner.BackColor = ForestUi.CardFill

        Footer = New Panel()
        Footer.Dock = DockStyle.Bottom
        Footer.Height = 70
        Footer.BackColor = ForestUi.CardFill

        Body = New Panel()
        Body.Dock = DockStyle.Fill
        Body.BackColor = ForestUi.CardFill
        Body.AutoScroll = True

        Dim head As New Panel()
        head.Dock = DockStyle.Top
        head.Height = If(subtitle = "", 62, 84)
        head.BackColor = ForestUi.CardFill
        head.Controls.Add(ForestUi.Lbl(title, 15.0F, FontStyle.Bold, ForestUi.Ink, 26, 18))
        If subtitle <> "" Then head.Controls.Add(ForestUi.Lbl(subtitle, 9.0F, FontStyle.Regular, ForestUi.Muted, 27, 50))

        inner.Controls.Add(Body)
        inner.Controls.Add(Footer)
        inner.Controls.Add(head)
        Me.Controls.Add(inner)
    End Sub

    Protected Overrides Sub OnShown(e As EventArgs)
        MyBase.OnShown(e)
        Try
            Using gp As GraphicsPath = ForestUi.RoundedPath(New Rectangle(0, 0, Me.Width, Me.Height), 14)
                Me.Region = New Region(gp)
            End Using
        Catch ex As ArgumentException
            ' keep square corners if the region cannot be created
        End Try
    End Sub

    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
        MyBase.OnKeyDown(e)
        If e.KeyCode = Keys.Escape Then Me.DialogResult = DialogResult.Cancel
    End Sub
End Class