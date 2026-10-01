Public Class ForgotPassword

    Private Sub btnChangePass_Click(sender As Object, e As EventArgs) Handles btnChangePass.Click

        If txtNewPass.Text = "" Or txtConfirmNewPass.Text = "" Then
            MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Else
            If txtNewPass.Text = txtConfirmNewPass.Text Then
                Dim newPass As String = txtNewPass.Text

                For i As Integer = 0 To GlobalData.registerAccount.Count - 1
                    If GlobalData.userName = GlobalData.registerAccount(i)(1) Then
                        GlobalData.registerAccount(i)(2) = newPass
                        MessageBox.Show("Password changed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.Close()

                    ElseIf GlobalData.userName = GlobalData.AdminAccount(i)(1) Then
                        GlobalData.AdminAccount(i)(2) = newPass
                        MessageBox.Show("Password changed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.Close()

                    End If
                Next
            Else
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub cbShowHidePass_CheckedChanged(sender As Object, e As EventArgs) Handles cbShowHidePass.CheckedChanged
        If cbShowHidePass.Checked Then
            txtNewPass.PasswordChar = ""
            txtConfirmNewPass.PasswordChar = ""
            cbShowHidePass.Text = "Hide Password"
        Else
            txtNewPass.PasswordChar = "*"
            txtConfirmNewPass.PasswordChar = "*"
            cbShowHidePass.Text = "Show Password"
        End If
    End Sub
End Class