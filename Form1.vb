Imports System.Drawing.Drawing2D

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        MakeRoundedPanel(pnl_Login_Form, 40)

    End Sub

    Private Sub MakeRoundedPanel(pnl_Login_Form As Panel, radius As Integer)

        Dim path As New Drawing2D.GraphicsPath()
        path.AddArc(0, 0, radius, radius, 180, 90)
        path.AddLine(radius, 0, pnl_Login_Form.Width, 0)
        path.AddLine(pnl_Login_Form.Width, 0, pnl_Login_Form.Width, pnl_Login_Form.Height)
        path.AddLine(pnl_Login_Form.Width, pnl_Login_Form.Height, radius, pnl_Login_Form.Height)
        path.AddArc(0, pnl_Login_Form.Height - radius, radius, radius, 90, 90)
        path.CloseAllFigures()
        pnl_Login_Form.Region = New Region(path)

        'Login btn
        Dim img As Image = My.Resources.login
        btnLogin.Image = New Bitmap(img, New Size(32, 32))

    End Sub

    Private Sub pnl_Login_Form_Paint(sender As Object, e As PaintEventArgs) Handles pnl_Login_Form.Paint

    End Sub
End Class
