Imports Microsoft.VisualBasic.ApplicationServices

Public Class ForgotPassword

    Private Sub btnChangePass_Click(sender As Object, e As EventArgs) Handles btnChangePass.Click

        If txtNewPass.Text = "" Or txtConfirmNewPass.Text = "" Then
            MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        ElseIf txtNewPass.Text <> txtConfirmNewPass.Text Then
            MessageBox.Show("Passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim newPass As String = txtNewPass.Text
        For i As Integer = 0 To GlobalData.registerAccount.Count - 1
            If GlobalData.userName = GlobalData.registerAccount(i)(1) Then
                UpdatePassword(GlobalData.registerAccount, GlobalData.registerAccount(i)(1), newPass)
                MessageBox.Show("Password changed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            End If
        Next
        For i As Integer = 0 To GlobalData.AdminAccount.Count - 1
            If GlobalData.userName = GlobalData.AdminAccount(i)(1) Then
                UpdatePassword(GlobalData.AdminAccount, GlobalData.AdminAccount(i)(1), newPass)
                MessageBox.Show("Password changed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            End If
        Next

        ' For i As Integer = 0 To GlobalData.AdminAccount.Count - 1
        '     If UpdatePassword(GlobalData.registerAccount, GlobalData.registerAccount(i)(1), newPass) OrElse
        'UpdatePassword(GlobalData.AdminAccount, GlobalData.AdminAccount(i)(1), newPass) Then
        '         MessageBox.Show("Password changed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '         Me.Close()
        '     Else
        '         MessageBox.Show("Username not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '     End If
        ' Next

    End Sub
    Private Function UpdatePassword(accounts As List(Of String()), user As String, newPass As String) As Boolean
        For Each acc As String() In accounts
            If acc(1).Equals(user, StringComparison.OrdinalIgnoreCase) Then
                acc(2) = newPass
                Return True
            End If
        Next
        Return False
    End Function

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