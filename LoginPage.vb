Imports System.Drawing.Drawing2D
Imports System.Security.Principal
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
                    Dim cashierFullName As String = "Jedrick Miclat"
                    username = "jedrick"
                    password = "cashier123"
                    Dim cashierAccount() As String = {cashierFullName, username, password}
                    GlobalData.registerAccount.Add(cashierAccount)

                    If Not (txtUser.Text = "" Or txtPass.Text = "") Then
                        For i As Integer = 0 To GlobalData.registerAccount.Count - 1
                            For j As Integer = 0 To GlobalData.registerAccount(i).Length - 1
                                If txtUser.Text = GlobalData.registerAccount(i)(1) And txtPass.Text = GlobalData.registerAccount(i)(2) Then
                                    username = GlobalData.registerAccount(i)(1)
                                    password = GlobalData.registerAccount(i)(2)
                                End If
                            Next
                        Next

                        If txtUser.Text = username And txtPass.Text = password Then
                            GlobalData.userName = txtUser.Text
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
                    Dim adminFullName As String = "Justine Fritz Bucong"
                    username = "fritz"
                    password = "admin123"
                    Dim adminAccount() As String = {adminFullName, username, password}
                    GlobalData.adminAccount.Add(adminAccount)

                    If Not (txtUser.Text = "" Or txtPass.Text = "") Then
                        If txtUser.Text = username And txtPass.Text = password Then
                            GlobalData.userName = txtUser.Text
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

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim forgotPass As New ForgotPassword()
        Dim userName As String = ""

        For i As Integer = 0 To GlobalData.registerAccount.Count - 1
            For j As Integer = 0 To GlobalData.registerAccount(i).Length - 1
                If txtUser.Text = GlobalData.registerAccount(i)(1) Then
                    userName = GlobalData.registerAccount(i)(1)
                End If
            Next
        Next
        If txtUser.Text = "" Then
            MessageBox.Show("Please enter your credentials.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            If rbCashier.Checked Then
                If txtUser.Text = userName Or txtUser.Text = "jedrick" Then
                    forgotPass.ShowDialog(Me)
                Else
                    MessageBox.Show("Username not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            ElseIf rbAdmin.Checked Then
                If txtUser.Text = "fritz" Then
                    forgotPass.ShowDialog(Me)
                Else
                    MessageBox.Show("Username not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                MessageBox.Show("Please select a role first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub


End Class
