Imports System.Drawing
Public Class Cashier

    Private Sub HideAllPanels()

        dashbrd_pnl.Visible = False

    End Sub

    Private Sub ResetButtonColors()
        btn_DashBoard.BackColor = Color.Transparent
        btn_Point_Of_Sale.BackColor = Color.Transparent
        btn_prdt.BackColor = Color.Transparent
        btn_invtry.BackColor = Color.Transparent
        btn_hstry.BackColor = Color.Transparent

        btn_DashBoard.ForeColor = Color.DarkGray
    End Sub

    Private Sub btn_DashBoard_Click(sender As Object, e As EventArgs) Handles btn_DashBoard.Click

        HideAllPanels()
        dashbrd_pnl.Visible = True

        ResetButtonColors()

        btn_DashBoard.BackColor = Color.MistyRose
        btn_DashBoard.ForeColor = Color.Black

    End Sub

    Private Sub btn_Point_Of_Sale_Click(sender As Object, e As EventArgs) Handles btn_Point_Of_Sale.Click

        HideAllPanels()

        ResetButtonColors()

        btn_Point_Of_Sale.BackColor = Color.MistyRose
        btn_Point_Of_Sale.ForeColor = Color.Black

    End Sub

    Private Sub Guna2Button6_Click(sender As Object, e As EventArgs) Handles Guna2Button6.Click
        Dim login As New LoginPage()
        login.Show()
        Me.Hide()
    End Sub
End Class