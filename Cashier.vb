Public Class Cashier

    Private Sub HideAllPanels()

<<<<<<< HEAD
        pnltest.Visible = False
        dashboardpanel.Visible = False
=======
        dashbrd_pnl.Visible = False
>>>>>>> 6856b2770ed86d2059fd5316ea8307889915beff

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
<<<<<<< HEAD
        dashboardpanel.Visible = True
=======
        dashbrd_pnl.Visible = True
>>>>>>> 6856b2770ed86d2059fd5316ea8307889915beff

        ResetButtonColors()

        btn_DashBoard.BackColor = Color.MistyRose
        btn_DashBoard.ForeColor = Color.Black

    End Sub

    Private Sub btn_Point_Of_Sale_Click(sender As Object, e As EventArgs) Handles btn_Point_Of_Sale.Click

        HideAllPanels()
<<<<<<< HEAD
        pnltest.Visible = True
=======
>>>>>>> 6856b2770ed86d2059fd5316ea8307889915beff

        ResetButtonColors()

        btn_Point_Of_Sale.BackColor = Color.MistyRose
        btn_Point_Of_Sale.ForeColor = Color.Black
<<<<<<< HEAD
=======

    End Sub

    Private Sub dashbrd_pnl_Paint(sender As Object, e As PaintEventArgs) Handles dashbrd_pnl.Paint
>>>>>>> 6856b2770ed86d2059fd5316ea8307889915beff

    End Sub
End Class