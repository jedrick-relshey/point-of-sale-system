Option Strict On
Option Explicit On

' LoginPage.vb  -  REPLACES your old LoginPage.vb (Forest Roast redesign)
Public Class LoginPage

    Private Sub LoginPage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Creates the TXT data files on first launch and loads everything.
        DataStore.Initialize()
        txtPass.PasswordChar = ChrW(&H25CF)
        UpdateRoleStyle()
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
        ' cbShowHidePass is a hidden helper; the eye icon (lblEye) toggles it.
        If cbShowHidePass.Checked Then
            txtPass.PasswordChar = ChrW(0)
            lblEye.ForeColor = Color.FromArgb(184, 106, 58)
        Else
            txtPass.PasswordChar = ChrW(&H25CF)
            lblEye.ForeColor = Color.FromArgb(125, 110, 100)
        End If
    End Sub

    Private Sub lblEye_Click(sender As Object, e As EventArgs) Handles lblEye.Click
        cbShowHidePass.Checked = Not cbShowHidePass.Checked
    End Sub

    ' Highlights the selected role card.
    Private Sub rbRole_CheckedChanged(sender As Object, e As EventArgs) Handles rbAdmin.CheckedChanged, rbCashier.CheckedChanged
        UpdateRoleStyle()
    End Sub

    Private Sub UpdateRoleStyle()
        StyleRole(pnlRoleAdmin, rbAdmin.Checked)
        StyleRole(pnlRoleCashier, rbCashier.Checked)
    End Sub

    Private Sub StyleRole(pnl As Guna.UI2.WinForms.Guna2Panel, selected As Boolean)
        If selected Then
            pnl.BorderColor = Color.FromArgb(184, 106, 58)
            pnl.FillColor = Color.FromArgb(251, 241, 232)
        Else
            pnl.BorderColor = Color.FromArgb(228, 219, 208)
            pnl.FillColor = Color.FromArgb(254, 252, 248)
        End If
    End Sub

    Private Sub lnkHelp_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkHelp.LinkClicked
        MessageBox.Show("Need help logging in? Please ask your manager or the system administrator.",
                        "Need help?", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub lnkManager_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkManager.LinkClicked
        MessageBox.Show("Accounts are created by your manager or administrator." & vbCrLf &
                        "Please contact them to get access.",
                        "No account yet?", MessageBoxButtons.OK, MessageBoxIcon.Information)
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