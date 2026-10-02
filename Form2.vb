Imports System.Security.Principal

Public Class RegisterNewCashierForm

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel registration?", "Cancel Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result.Equals(DialogResult.Yes) Then
            Me.Close()
        End If
    End Sub
    Public Sub SaveCashierAccounts()
        GlobalData.SaveAccounts()
    End Sub

    Public Sub SaveAllAccounts()
        GlobalData.SaveAccounts()
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
        GlobalData.SaveAccounts()
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