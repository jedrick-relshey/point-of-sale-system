Module GlobalData
    Public userName As String = ""
    Public registerAccount As New List(Of String())
End Module
Public Class RegisterNewCashierForm

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel registration?", "Cancel Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result.Equals(DialogResult.Yes) Then
            Me.Hide()
        End If
    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        Dim fullName As String = txtRegFullName.Text
        Dim user As String = txtRegUser.Text
        Dim password As String = txtRegPass.Text
        Dim confirmPassword As String = txtRegConfirmPass.Text

        Dim account() As String = {fullName, user, password}

        If (fullName = "" Or user = "" Or password = "" Or confirmPassword = "") Then
            MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            If (password = confirmPassword) Then
                MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                GlobalData.registerAccount.Add(account)
                Me.Hide()
            Else
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub
End Class