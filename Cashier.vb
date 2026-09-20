Public Class Cashier

    Private Sub HideAllPanels()

        pnltest.Visible = False
        dashboardpanel.Visible = False

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
        dashboardpanel.Visible = True

        ResetButtonColors()

        btn_DashBoard.BackColor = Color.MistyRose
        btn_DashBoard.ForeColor = Color.Black

    End Sub

    Private Sub btn_Point_Of_Sale_Click(sender As Object, e As EventArgs) Handles btn_Point_Of_Sale.Click

        HideAllPanels()
        pnltest.Visible = True

        ResetButtonColors()

        btn_Point_Of_Sale.BackColor = Color.MistyRose
        btn_Point_Of_Sale.ForeColor = Color.Black

    End Sub
End Class