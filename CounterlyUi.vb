Option Strict On
Option Explicit On

Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing.Text
Imports System.IO
Imports System.Windows.Forms
Imports Guna.UI2.WinForms

' CounterlyUi.vb  -  NEW FILE. Add it to your project (Project > Add Existing Item).
' Colours + tiny control factories used by Admin.Counterly.vb.
Friend Module Counterly

    ' ---- brand text (change these two lines to rename the app / store) ----
    Friend Const AppName As String = "Counterly"
    Friend Const StoreName As String = "Downtown store"

    ' ---- palette (sampled from the design) ----
    Friend ReadOnly Navy As Color = Color.FromArgb(10, 37, 64)         ' sidebar
    Friend ReadOnly NavHover As Color = Color.FromArgb(20, 55, 82)
    Friend ReadOnly NavActive As Color = Color.FromArgb(18, 66, 88)    ' selected menu item
    Friend ReadOnly NavCard As Color = Color.FromArgb(16, 48, 74)      ' "Need help?" card
    Friend ReadOnly NavText As Color = Color.FromArgb(205, 218, 228)
    Friend ReadOnly NavMuted As Color = Color.FromArgb(140, 160, 178)
    Friend ReadOnly Teal As Color = Color.FromArgb(18, 184, 160)
    Friend ReadOnly TealDark As Color = Color.FromArgb(12, 150, 130)
    Friend ReadOnly TealSoft As Color = Color.FromArgb(220, 246, 241)  ' selected conversation
    Friend ReadOnly Page As Color = Color.FromArgb(246, 248, 250)
    Friend ReadOnly ChatBg As Color = Color.FromArgb(248, 250, 252)
    Friend ReadOnly Border As Color = Color.FromArgb(226, 232, 238)
    Friend ReadOnly Ink As Color = Color.FromArgb(15, 42, 63)
    Friend ReadOnly Muted As Color = Color.FromArgb(107, 122, 138)
    Friend ReadOnly Bubble As Color = Color.FromArgb(15, 48, 72)       ' sent message
    Friend ReadOnly AvatarGray As Color = Color.FromArgb(232, 238, 245)
    Friend ReadOnly Danger As Color = Color.FromArgb(232, 90, 90)

    ' Segoe MDL2 Assets glyphs (built into Windows 10 / 11)
    Friend ReadOnly GlyphBell As String = ChrW(&HE7ED)
    Friend ReadOnly GlyphPhone As String = ChrW(&HE717)
    Friend ReadOnly GlyphMore As String = ChrW(&HE712)
    Friend ReadOnly GlyphSearch As String = ChrW(&HE721)
    Friend ReadOnly GlyphEdit As String = ChrW(&HE70F)
    Friend ReadOnly GlyphPower As String = ChrW(&HE7E8)
    Friend ReadOnly GlyphHelp As String = ChrW(&HE897)
    Friend ReadOnly GlyphAddUser As String = ChrW(&HE8FA)
    Friend ReadOnly GlyphTrash As String = ChrW(&HE74D)
    Friend ReadOnly GlyphCheck As String = ChrW(&HE73E)
    Friend ReadOnly GlyphEye As String = ChrW(&HE7B3)
    Friend ReadOnly GlyphLock As String = ChrW(&HE72E)
    Friend ReadOnly GlyphUser As String = ChrW(&HE77B)
    Friend ReadOnly GlyphClose As String = ChrW(&HE711)
    Friend ReadOnly GlyphPeople As String = ChrW(&HE716)
    Friend ReadOnly GlyphCamera As String = ChrW(&HE722)
    Friend ReadOnly GlyphSave As String = ChrW(&HE74E)

    ''' <summary>Rounded card (Guna2Panel) with optional 1px border.</summary>
    Friend Function Card(w As Integer, h As Integer, radius As Integer, fill As Color,
                         Optional borderColor As Color = Nothing) As Guna2Panel
        Dim p As New Guna2Panel()
        p.Size = New Size(w, h)
        p.BorderRadius = radius
        p.FillColor = fill
        If Not borderColor.IsEmpty Then
            p.BorderColor = borderColor
            p.BorderThickness = 1
        End If
        Return p
    End Function

    ''' <summary>AutoSize label with transparent background.</summary>
    Friend Function Lbl(text As String, size As Single, style As FontStyle, fg As Color,
                        Optional x As Integer = 0, Optional y As Integer = 0) As Label
        Dim l As New Label()
        l.Text = text
        l.AutoSize = True
        l.BackColor = Color.Transparent
        l.ForeColor = fg
        l.Font = New Font("Segoe UI", size, style)
        l.Location = New Point(x, y)
        Return l
    End Function

    ''' <summary>Round avatar with initials.</summary>
    Friend Function Avatar(initials As String, size As Integer, fill As Color, fg As Color) As Guna2Panel
        Dim p As Guna2Panel = Card(size, size, size \ 2, fill)
        Dim l As New Label()
        l.Dock = DockStyle.Fill
        l.BackColor = Color.Transparent
        l.ForeColor = fg
        l.Text = initials
        l.TextAlign = ContentAlignment.MiddleCenter
        l.Font = New Font("Segoe UI", CSng(size) / 4.4F, FontStyle.Bold)
        p.Controls.Add(l)
        Return p
    End Function

    ''' <summary>Round avatar: the cashier's photo when there is one, otherwise initials.</summary>
    Friend Function AvatarFor(initials As String, size As Integer, fill As Color, fg As Color, photo As Image) As Control
        If photo Is Nothing Then Return Avatar(initials, size, fill, fg)
        Dim pb As New Guna2CirclePictureBox()
        pb.Size = New Size(size, size)
        pb.SizeMode = PictureBoxSizeMode.StretchImage
        pb.BackColor = Color.Transparent
        pb.Image = photo
        Return pb
    End Function

    ''' <summary>Draws a Segoe MDL2 glyph into a small bitmap (for button icons).</summary>
    Friend Function GlyphImage(glyph As String, px As Integer, c As Color) As Bitmap
        Dim bmp As New Bitmap(px, px)
        Using g As Graphics = Graphics.FromImage(bmp)
            g.TextRenderingHint = TextRenderingHint.AntiAlias
            Using f As New Font("Segoe MDL2 Assets", px * 0.72F, FontStyle.Regular, GraphicsUnit.Pixel)
                Using br As New SolidBrush(c)
                    Using sf As New StringFormat()
                        sf.Alignment = StringAlignment.Center
                        sf.LineAlignment = StringAlignment.Center
                        g.DrawString(glyph, f, br, New RectangleF(0, 0, px, px), sf)
                    End Using
                End Using
            End Using
        End Using
        Return bmp
    End Function

    ''' <summary>Square icon button that draws a Segoe MDL2 glyph.</summary>
    Friend Function GlyphButton(glyph As String, size As Integer, fill As Color, fg As Color) As Guna2Button
        Dim b As New Guna2Button()
        b.Size = New Size(size, size)
        b.BorderRadius = 10
        b.FillColor = fill
        b.ForeColor = fg
        b.Font = New Font("Segoe MDL2 Assets", 12.0F)
        b.Text = glyph
        b.Cursor = Cursors.Hand
        b.HoverState.FillColor = ControlPaint.Dark(fill, 0.04F)
        Return b
    End Function

    Friend Function InitialsOf(name As String) As String
        If String.IsNullOrWhiteSpace(name) Then Return "?"
        Dim parts() As String = name.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
        If parts.Length = 0 Then Return "?"
        If parts.Length = 1 Then Return parts(0).Substring(0, 1).ToUpper()
        Return (parts(0).Substring(0, 1) & parts(parts.Length - 1).Substring(0, 1)).ToUpper()
    End Function

    Friend Function TimeAgo(t As DateTime) As String
        Dim d As TimeSpan = DateTime.Now - t
        If d.TotalMinutes < 1 Then Return "now"
        If d.TotalMinutes < 60 Then Return CInt(d.TotalMinutes).ToString() & " min"
        If t.Date = DateTime.Today Then Return CInt(d.TotalHours).ToString() & " hr"
        If t.Date = DateTime.Today.AddDays(-1) Then Return "Yesterday"
        Return t.ToString("MMM d")
    End Function

End Module


''' <summary>
''' Cashier profile photos. One PNG per cashier Id in a "CashierPhotos" folder next to the .exe.
''' Use CashierPhotos.Load(cashier.Id) anywhere (e.g. the cashier screen) to show the photo.
''' </summary>
Friend Module CashierPhotos

    Friend ReadOnly PhotoDir As String = Path.Combine(Application.StartupPath, "CashierPhotos")
    Private ReadOnly cache As New Dictionary(Of String, Image)

    Private Function PathFor(id As String) As String
        Dim safe As String = id
        For Each ch As Char In Path.GetInvalidFileNameChars()
            safe = safe.Replace(ch, "_"c)
        Next
        Return Path.Combine(PhotoDir, safe & ".png")
    End Function

    ''' <summary>The saved photo, or Nothing when the cashier has none.</summary>
    Friend Function Load(id As String) As Image
        If String.IsNullOrEmpty(id) Then Return Nothing

        Dim img As Image = Nothing
        If cache.TryGetValue(id, img) Then Return img

        Dim f As String = PathFor(id)
        If File.Exists(f) Then
            Try
                Using ms As New MemoryStream(File.ReadAllBytes(f))
                    Using src As Image = Image.FromStream(ms)
                        img = New Bitmap(src)
                    End Using
                End Using
            Catch
                img = Nothing
            End Try
        End If

        cache(id) = img
        Return img
    End Function

    ''' <summary>Reads any image file and returns a 256x256 centre-cropped square.</summary>
    Friend Function LoadFile(path As String) As Image
        Using ms As New MemoryStream(File.ReadAllBytes(path))
            Using src As Image = Image.FromStream(ms)
                Return SquareThumb(src, 256)
            End Using
        End Using
    End Function

    Friend Sub Save(id As String, img As Image)
        If String.IsNullOrEmpty(id) OrElse img Is Nothing Then Return
        Directory.CreateDirectory(PhotoDir)
        Using thumb As Image = SquareThumb(img, 256)
            thumb.Save(PathFor(id), ImageFormat.Png)
        End Using
        cache.Remove(id)
    End Sub

    Friend Sub Delete(id As String)
        If String.IsNullOrEmpty(id) Then Return
        Try
            Dim f As String = PathFor(id)
            If File.Exists(f) Then File.Delete(f)
        Catch
        End Try
        cache.Remove(id)
    End Sub

    Private Function SquareThumb(src As Image, size As Integer) As Bitmap
        Dim side As Integer = Math.Min(src.Width, src.Height)
        Dim sx As Integer = (src.Width - side) \ 2
        Dim sy As Integer = (src.Height - side) \ 2
        Dim bmp As New Bitmap(size, size, PixelFormat.Format32bppArgb)
        Using g As Graphics = Graphics.FromImage(bmp)
            g.InterpolationMode = InterpolationMode.HighQualityBicubic
            g.SmoothingMode = SmoothingMode.HighQuality
            g.PixelOffsetMode = PixelOffsetMode.HighQuality
            g.DrawImage(src, New Rectangle(0, 0, size, size), New Rectangle(sx, sy, side, side), GraphicsUnit.Pixel)
        End Using
        Return bmp
    End Function

End Module