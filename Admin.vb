Public Class Admin
    Private Sub btnAdminLogout_Click_1(sender As Object, e As EventArgs) Handles btnAdminLogout.Click
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

    Private Sub Admin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        adminName.Text = GlobalData.userName
        adminName.Text = adminName.Text.ToUpper()

        adminDataGridView.Font = New Font("Segoe UI", 10, FontStyle.Regular)
        adminDataGridView.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)

        adminDataGridView.ColumnCount = 5
        adminDataGridView.Columns(0).HeaderText = "Transaction ID"
        adminDataGridView.Columns(1).HeaderText = "Time"
        adminDataGridView.Columns(2).HeaderText = "Item Ordered"
        adminDataGridView.Columns(3).HeaderText = "Total"
        adminDataGridView.Columns(4).HeaderText = "Status"

        adminDataGridView.Rows.Add("TXN001", "2024-06-01 10:30 AM", "Item A, Item B", "$50.00", "Completed")
    End Sub
End Class