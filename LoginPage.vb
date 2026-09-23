Imports System.Drawing.Drawing2D

Public Class LoginPage
    Private Sub btn_Login_Click(sender As Object, e As EventArgs) Handles btnLogin.Click

        Dim username As String = txtUser.Text
        Dim password As String = txtPass.Text
        Dim userType As String = ""

        If (rbCashier.Checked Or rbAdmin.Checked) Then
            If rbCashier.Checked Then
                userType = "cashier"
            ElseIf rbAdmin.Checked Then
                userType = "admin"
            End If

            Select Case userType
                Case "cashier"
                    If Not (txtUser.Text = "" Or txtPass.Text = "") Then
                        If txtUser.Text = "cashier" And txtPass.Text = "cashierpass" Then
                            MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Dim cashier As New Cashier()
                            cashier.Show()
                            Me.Hide()
                        Else
                            MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                    Else
                        MessageBox.Show("Please enter your username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Case "admin"
                    If Not (txtUser.Text = "" Or txtPass.Text = "") Then
                        If txtUser.Text = "admin" And txtPass.Text = "adminpass" Then
                            MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Dim admin As New Admin()
                            admin.Show()
                            Me.Hide()
                        Else
                            MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                    Else
                        MessageBox.Show("Please enter your username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
            End Select
        Else
            MessageBox.Show("Please select a login role.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub btnAdmin_Click(sender As Object, e As EventArgs)
        loginPanel.Visible = False

    End Sub

    Private Sub bckbtn_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        loginPanel.Visible = True

    End Sub

    Private Sub cbShowHidePass_CheckedChanged(sender As Object, e As EventArgs) Handles cbShowHidePass.CheckedChanged
        If cbShowHidePass.Checked Then
            txtPass.PasswordChar = ""
            cbShowHidePass.Text = "Hide Password"
        Else
            txtPass.PasswordChar = "*"
            cbShowHidePass.Text = "Show Password"
        End If
    End Sub
End Class
