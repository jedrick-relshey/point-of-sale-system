Option Strict On
Option Explicit On

Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

' ============================================================
'  UiKit.vb  -  colors, fonts and small custom controls used by
'  ProductPageControl and AddProductForm (rounded cards/buttons,
'  badges, avatar, thumbnail).
' ============================================================

Public Module Theme

    ' ---- colors (match the screenshot) ----
    Public ReadOnly Teal As Color = Color.FromArgb(20, 184, 166)
    Public ReadOnly TealDark As Color = Color.FromArgb(13, 148, 126)
    Public ReadOnly TealSoft As Color = Color.FromArgb(213, 245, 239)
    Public ReadOnly PageBg As Color = Color.FromArgb(240, 244, 248)
    Public ReadOnly Border As Color = Color.FromArgb(226, 232, 240)
    Public ReadOnly TextDark As Color = Color.FromArgb(17, 24, 39)
    Public ReadOnly TextMuted As Color = Color.FromArgb(107, 114, 128)
    Public ReadOnly Red As Color = Color.FromArgb(220, 38, 38)
    Public ReadOnly RedSoft As Color = Color.FromArgb(254, 226, 228)
    Public ReadOnly HoverGray As Color = Color.FromArgb(241, 245, 249)
    Public ReadOnly HeaderRowBg As Color = Color.FromArgb(248, 250, 252)
    Public ReadOnly BadgeFixedBg As Color = Color.FromArgb(232, 238, 245)
    Public ReadOnly BadgeFixedText As Color = Color.FromArgb(30, 41, 59)

    ' ---- icon glyphs (Segoe MDL2 Assets, built into Windows 10/11) ----
    Public ReadOnly GlyphAdd As String = ChrW(&HE710)
    Public ReadOnly GlyphEdit As String = ChrW(&HE70F)
    Public ReadOnly GlyphDelete As String = ChrW(&HE74D)
    Public ReadOnly GlyphSearch As String = ChrW(&HE721)
    Public ReadOnly GlyphBell As String = ChrW(&HE7ED)
    Public ReadOnly GlyphChevron As String = ChrW(&HE70D)
    Public ReadOnly GlyphFilter As String = ChrW(&HE71C)
    Public ReadOnly GlyphLogout As String = ChrW(&HE7E8)
    Public ReadOnly GlyphShop As String = ChrW(&HE719)

    ' ---- fonts (cached, shared - never Dispose these) ----
    Private ReadOnly _fonts As New Dictionary(Of String, Font)()

    Public Function UiFont(size As Single, Optional style As FontStyle = FontStyle.Regular) As Font
        Dim key As String = "ui|" & size.ToString() & "|" & CInt(style).ToString()
        If Not _fonts.ContainsKey(key) Then
            _fonts(key) = New Font("Segoe UI", size, style, GraphicsUnit.Point)
        End If
        Return _fonts(key)
    End Function

    Public Function IconFont(size As Single) As Font
        Dim key As String = "icon|" & size.ToString()
        If Not _fonts.ContainsKey(key) Then
            _fonts(key) = New Font("Segoe MDL2 Assets", size, FontStyle.Regular, GraphicsUnit.Point)
        End If
        Return _fonts(key)
    End Function

    ' ---- helpers ----
    Public Function Money(value As Decimal) As String
        Return ChrW(&H20B1).ToString() & value.ToString("N2")
    End Function

    Public Function RoundedPath(r As Rectangle, radius As Integer) As GraphicsPath
        Dim d As Integer = Math.Max(2, radius * 2)
        Dim path As New GraphicsPath()
        path.AddArc(r.X, r.Y, d, d, 180, 90)
        path.AddArc(r.Right - d, r.Y, d, d, 270, 90)
        path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90)
        path.AddArc(r.X, r.Bottom - d, d, d, 90, 90)
        path.CloseFigure()
        Return path
    End Function

End Module


' ------------------------------------------------------------
'  Panel with rounded corners + border (the white "card")
' ------------------------------------------------------------
Public Class CardPanel
    Inherits Panel

    Public Property Radius As Integer = 12
    Public Property FillColor As Color = Color.White
    Public Property BorderColor As Color = Theme.Border

    Public Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or
                 ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw Or
                 ControlStyles.SupportsTransparentBackColor, True)
        BackColor = Color.Transparent
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        Dim r As New Rectangle(0, 0, Width - 1, Height - 1)
        Using path As GraphicsPath = Theme.RoundedPath(r, Radius)
            Using b As New SolidBrush(FillColor)
                g.FillPath(b, path)
            End Using
            Using p As New Pen(BorderColor)
                g.DrawPath(p, path)
            End Using
        End Using
    End Sub

End Class


' ------------------------------------------------------------
'  Plain panel with a 1px bottom line (header bar / table rows)
' ------------------------------------------------------------
Public Class BorderedPanel
    Inherits Panel

    Public Sub New()
        DoubleBuffered = True
        BackColor = Color.White
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Using p As New Pen(Theme.Border)
            e.Graphics.DrawLine(p, 0, Height - 1, Width, Height - 1)
        End Using
    End Sub

End Class


' ------------------------------------------------------------
'  Rounded button with optional icon glyph + text
' ------------------------------------------------------------
Public Class RoundedButton
    Inherits Control

    Public Property Radius As Integer = 8
    Public Property FillColor As Color = Theme.Teal
    Public Property HoverColor As Color = Theme.TealDark
    Public Property BorderColor As Color = Color.Empty
    Public Property TextColor As Color = Color.White
    Public Property Glyph As String = ""
    Public Property GlyphColor As Color = Color.White
    Public Property GlyphFont As Font = Theme.IconFont(10.0F)

    Private _hover As Boolean = False

    Public Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or
                 ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw Or
                 ControlStyles.SupportsTransparentBackColor, True)
        BackColor = Color.Transparent
        Cursor = Cursors.Hand
        Font = Theme.UiFont(9.0F, FontStyle.Bold)
        Size = New Size(100, 34)
    End Sub

    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)
        _hover = True
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        _hover = False
        Invalidate()
    End Sub

    Protected Overrides Sub OnTextChanged(e As EventArgs)
        MyBase.OnTextChanged(e)
        Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias

        Dim r As New Rectangle(0, 0, Width - 1, Height - 1)
        Dim fill As Color = If(_hover, HoverColor, FillColor)
        Using path As GraphicsPath = Theme.RoundedPath(r, Radius)
            Using b As New SolidBrush(fill)
                g.FillPath(b, path)
            End Using
            If BorderColor.A <> 0 Then
                Using p As New Pen(BorderColor)
                    g.DrawPath(p, path)
                End Using
            End If
        End Using

        Dim flags As TextFormatFlags = TextFormatFlags.NoPadding Or TextFormatFlags.NoPrefix
        Dim big As New Size(1000, 1000)
        Dim gSize As Size = Size.Empty
        Dim tSize As Size = Size.Empty
        If Glyph.Length > 0 Then gSize = TextRenderer.MeasureText(g, Glyph, GlyphFont, big, flags)
        If Text.Length > 0 Then tSize = TextRenderer.MeasureText(g, Text, Font, big, flags)

        Dim gap As Integer = If(gSize.Width > 0 AndAlso tSize.Width > 0, 8, 0)
        Dim x As Integer = (Width - (gSize.Width + gap + tSize.Width)) \ 2

        If gSize.Width > 0 Then
            TextRenderer.DrawText(g, Glyph, GlyphFont, New Point(x, (Height - gSize.Height) \ 2), GlyphColor, flags)
            x += gSize.Width + gap
        End If
        If tSize.Width > 0 Then
            TextRenderer.DrawText(g, Text, Font, New Point(x, (Height - tSize.Height) \ 2), TextColor, flags)
        End If
    End Sub

End Class


' ------------------------------------------------------------
'  Small pill label ("Fixed" / "Dynamic" / "Last saved ...")
' ------------------------------------------------------------
Public Class Badge
    Inherits Control

    Public Property FillColor As Color = Theme.BadgeFixedBg
    Public Property TextColor As Color = Theme.BadgeFixedText

    Public Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or
                 ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw Or
                 ControlStyles.SupportsTransparentBackColor, True)
        BackColor = Color.Transparent
        Font = Theme.UiFont(7.5F, FontStyle.Bold)
        Size = New Size(50, 22)
    End Sub

    Public Sub Setup(caption As String, fill As Color, foreground As Color)
        Text = caption
        FillColor = fill
        TextColor = foreground
        Dim w As Integer = TextRenderer.MeasureText(caption, Font, New Size(1000, 100), TextFormatFlags.NoPadding).Width
        Size = New Size(w + 22, 22)
        Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        Dim r As New Rectangle(0, 0, Width - 1, Height - 1)
        Using path As GraphicsPath = Theme.RoundedPath(r, (Height - 1) \ 2)
            Using b As New SolidBrush(FillColor)
                g.FillPath(b, path)
            End Using
        End Using
        TextRenderer.DrawText(g, Text, Font, New Rectangle(0, 0, Width, Height), TextColor,
                              TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or
                              TextFormatFlags.NoPadding Or TextFormatFlags.NoPrefix)
    End Sub

End Class


' ------------------------------------------------------------
'  Round avatar with initials ("AM")
' ------------------------------------------------------------
Public Class AvatarCircle
    Inherits Control

    Private _initials As String = ""

    Public Property Initials As String
        Get
            Return _initials
        End Get
        Set(value As String)
            _initials = If(value, "")
            Invalidate()
        End Set
    End Property

    Public Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or
                 ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw Or
                 ControlStyles.SupportsTransparentBackColor, True)
        BackColor = Color.Transparent
        Font = Theme.UiFont(8.5F, FontStyle.Bold)
        Size = New Size(36, 36)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        Using b As New SolidBrush(Theme.TealSoft)
            g.FillEllipse(b, 0, 0, Width - 1, Height - 1)
        End Using
        TextRenderer.DrawText(g, _initials, Font, New Rectangle(0, 0, Width, Height), Theme.TealDark,
                              TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or
                              TextFormatFlags.NoPadding)
    End Sub

End Class


' ------------------------------------------------------------
'  Rounded square thumbnail: product picture, or an icon if none
' ------------------------------------------------------------
Public Class ThumbBox
    Inherits Control

    Public Property Picture As Image
    Public Property FillColor As Color = Theme.TealSoft
    Public Property GlyphColor As Color = Theme.TealDark
    Public Property Glyph As String = Theme.GlyphShop

    Public Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or
                 ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw Or
                 ControlStyles.SupportsTransparentBackColor, True)
        BackColor = Color.Transparent
        Size = New Size(36, 36)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        Dim r As New Rectangle(0, 0, Width - 1, Height - 1)

        Using path As GraphicsPath = Theme.RoundedPath(r, 9)
            Using b As New SolidBrush(FillColor)
                g.FillPath(b, path)
            End Using

            If Picture IsNot Nothing Then
                g.SetClip(path)
                g.InterpolationMode = InterpolationMode.HighQualityBicubic
                Dim scale As Single = Math.Max(CSng(Width) / Picture.Width, CSng(Height) / Picture.Height)
                Dim sw As Single = Width / scale
                Dim sh As Single = Height / scale
                Dim sx As Single = (Picture.Width - sw) / 2.0F
                Dim sy As Single = (Picture.Height - sh) / 2.0F
                g.DrawImage(Picture, New Rectangle(0, 0, Width, Height), sx, sy, sw, sh, GraphicsUnit.Pixel)
                g.ResetClip()
            Else
                TextRenderer.DrawText(g, Glyph, Theme.IconFont(13.0F), New Rectangle(0, 0, Width, Height), GlyphColor,
                                      TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or
                                      TextFormatFlags.NoPadding)
            End If
        End Using
    End Sub

End Class