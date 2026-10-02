Option Strict On
Option Explicit On

' LoginPage.vb  -  REPLACES your old LoginPage.vb
' (LoginPage_Designer.vb is unchanged - all control names are the same.)
Public Class LoginPage

    Private Sub LoginPage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Creates the TXT data files on first launch and loads everything.
        DataStore.Initialize()
        txtPass.PasswordChar = "*"c
    End Sub

    ''' <summary>Called by AppNavigation.ShowLogin() after a logout.</summary>
    Public Sub ResetAndShow()
        txtUser.Clear()
        txtPass.Clear()
        cbShowHidePass.Checked = False
        rbAdmin.Checked = False
        rbCashier.Checked = False
        Me.Show()
        Me.BringToFront()
        txtUser.Focus()
    End Sub

    Private Sub btn_Login_Click(sender As Object, e As EventArgs) Handles btnLogin.Click

        If Not (rbCashier.Checked OrElse rbAdmin.Checked) Then
            MessageBox.Show("Please select a login role.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If String.IsNullOrWhiteSpace(txtUser.Text) OrElse String.IsNullOrEmpty(txtPass.Text) Then
            MessageBox.Show("Please enter your username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim role As String = If(rbAdmin.Checked, "admin", "cashier")
        Dim message As String = ""

        If Not DataStore.Authenticate(txtUser.Text, txtPass.Text, role, message) Then
            MessageBox.Show(message, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtPass.Clear()
            txtPass.Focus()
            Return
        End If

        MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        If role = "admin" Then
            Dim adminForm As New Admin()
            adminForm.Show()
        Else
            Dim cashierForm As New Cashier()
            cashierForm.Show()
        End If

        txtPass.Clear()
        Me.Hide()
    End Sub

    Private Sub cbShowHidePass_CheckedChanged(sender As Object, e As EventArgs) Handles cbShowHidePass.CheckedChanged
        If cbShowHidePass.Checked Then
            txtPass.PasswordChar = ChrW(0)
            cbShowHidePass.Text = "Hide Password"
        Else
            txtPass.PasswordChar = "*"c
            cbShowHidePass.Text = "Show Password"
        End If
    End Sub

    ' "Forgot password" - the account list is persistent now, so recovery is done by the admin.
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked

        If String.IsNullOrWhiteSpace(txtUser.Text) Then
            MessageBox.Show("Please enter your username first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If rbCashier.Checked Then
            Dim found As Boolean = False
            For Each c As CashierAccount In DataStore.Cashiers
                If String.Equals(c.Username, txtUser.Text.Trim(), StringComparison.OrdinalIgnoreCase) Then found = True
            Next
            If found Then
                MessageBox.Show("Please ask the administrator to reset your password." & vbCrLf &
                                "(Admin > Cashiers > Edit > enter a new password.)",
                                "Forgot Password", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Username not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        ElseIf rbAdmin.Checked Then
            MessageBox.Show("Administrator passwords can only be reset by the system owner." & vbCrLf &
                            "(Restore admin_accounts.txt from your system owner backup.)",
                            "Forgot Password", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Please select a role first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

End Class
