Imports System.Drawing.Drawing2D

Public Class RoundedPanel
    Inherits Panel

    Public Property BorderRadius As Integer = 15

    Protected Overrides Sub OnPaint(e As PaintEventArgs)

        MyBase.OnPaint(e)

        Dim path As New GraphicsPath()

        Dim radius As Integer = BorderRadius
        Dim rect As Rectangle = New Rectangle(0, 0, Width - 1, Height - 1)

        path.AddArc(rect.X, rect.Y, radius, radius, 180, 90)
        path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90)
        path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90)
        path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90)

        path.CloseFigure()

        Region = New Region(path)

    End Sub

End Class