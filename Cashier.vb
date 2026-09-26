Imports System.Drawing
Public Class Cashier

    Private Sub HideAllPanels()

        main_pnl.Visible = False

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
        main_pnl.Visible = True

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

    Private Sub Guna2Panel8_Paint(sender As Object, e As PaintEventArgs) Handles Guna2Panel8.Paint

    End Sub

    Private Sub btnCashierLogout_Click(sender As Object, e As EventArgs) Handles btnCashierLogout.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Dim loginPage As New LoginPage()
            loginPage.Show()
            Me.Hide()
            MessageBox.Show("Logout successful.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Logout canceled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub Cashier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cashierName.Text = GlobalData.userName
        cashierName.Text = cashierName.Text.ToUpper()
    End Sub
End Class