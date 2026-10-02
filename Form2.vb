Imports System.Security.Principal

Public Class RegisterNewCashierForm

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel registration?", "Cancel Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result.Equals(DialogResult.Yes) Then
            Me.Close()
        End If
    End Sub
    Public Sub SaveCashierAccounts()

        Dim lines As New List(Of String)

        For Each acc As String() In GlobalData.registerAccount

            Dim line As String =
            acc(0) & "|" & acc(1) & "|" & acc(2) & "|cashier"

            lines.Add(line)

        Next

        IO.File.WriteAllLines(GlobalData.CashierFilePath, lines)

    End Sub
    Public Sub SaveAllAccounts()

        Dim lines As New List(Of String)

        ' CASHIERS
        For Each acc As String() In GlobalData.registerAccount
            lines.Add("cashier|" & acc(0) & "|" & acc(1) & "|" & acc(2))
        Next

        ' ADMINS
        For Each acc As String() In GlobalData.AdminAccount
            lines.Add("admin|" & acc(0) & "|" & acc(1) & "|" & acc(2))
        Next

        IO.File.WriteAllLines(GlobalData.CashierFilePath, lines)

    End Sub
    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        Dim fullName As String = txtRegFullName.Text.Trim()
        Dim user As String = txtRegUser.Text.Trim()
        Dim password As String = txtRegPass.Text.Trim()
        Dim confirmPassword As String = txtRegConfirmPass.Text.Trim()

        If fullName = "" Or user = "" Or password = "" Or confirmPassword = "" Then
            MessageBox.Show("Please fill in all fields.")
            Exit Sub
        End If

        ' CHECK DUPLICATE
        If GlobalData.registerAccount.Any(Function(a) a(1).ToLower() = user.ToLower()) Then
            MessageBox.Show("Username already exists.")
            Exit Sub
        End If

        If password <> confirmPassword Then
            MessageBox.Show("Passwords do not match.")
            Exit Sub
        End If

        GlobalData.registerAccount.Add(New String() {fullName, user, password})

        SaveAllAccounts()

        MessageBox.Show("Registration successful!")

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub
    Public Sub SaveCashierAccountsToFile()

        Dim lines As New List(Of String)

        '========================
        ' CASHIERS
        '========================
        For Each acc As String() In GlobalData.registerAccount

            Dim line As String =
                "cashier|" & acc(0) & "|" & acc(1) & "|" & acc(2)

            lines.Add(line)

        Next

        IO.File.WriteAllLines(CashierFilePath, lines)

    End Sub

    Private Sub cbShowPass_CheckedChanged(sender As Object, e As EventArgs) Handles cbShowPass.CheckedChanged
        If cbShowPass.Checked Then
            txtRegPass.PasswordChar = ""
            txtRegConfirmPass.PasswordChar = ""
            cbShowPass.Text = "Hide Password"
        Else
            txtRegPass.PasswordChar = "*"
            txtRegConfirmPass.PasswordChar = "*"
            cbShowPass.Text = "Show Password"
        End If
    End Sub

End Class