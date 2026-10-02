Imports System.Drawing.Drawing2D
Imports System.Security.Principal

Module GlobalData
    Public userName As String = ""
    Public registerAccount As New List(Of String())
    Public AdminAccount As New List(Of String())

    Public AdminFilePath As String = IO.Path.Combine(Application.StartupPath, "admin_accounts.txt")
    Public CashierFilePath As String = IO.Path.Combine(Application.StartupPath, "cashier_accounts.txt")
End Module
Public Class LoginPage

    Private Sub btn_Login_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim inputUser As String = txtUser.Text.Trim()
        Dim inputPass As String = txtPass.Text.Trim()

        Dim isValid As Boolean = False
        Dim fullName As String = ""



        Dim userType As String = ""

        If (rbCashier.Checked Or rbAdmin.Checked) Then
            If rbCashier.Checked Then
                userType = "cashier"
            ElseIf rbAdmin.Checked Then
                userType = "admin"
            End If

            Select Case userType
                Case "cashier"
                    SaveCashierAccounts()
                    LoadCashierAccountsFromFile()

                    Dim cashierFullName As String = "Jedrick Miclat"
                    Dim cashierName As String = "jedrick"
                    Dim cashierPass As String = "cashier123"
                    Dim cashierAccount() As String = {cashierFullName, cashierName, cashierPass}
                    GlobalData.registerAccount.Add(cashierAccount)


                    For Each acc As String() In GlobalData.registerAccount

                        Dim username As String = acc(1)
                        Dim password As String = acc(2)

                        If inputUser = username And inputPass = password Then

                            isValid = True
                            fullName = acc(0)
                            Exit For

                        End If

                    Next

                    If isValid Then

                        GlobalData.userName = fullName

                        MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        Dim cashier As New Cashier()
                        cashier.Show()
                        Me.Hide()

                    Else

                        MessageBox.Show("Invalid username or password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                    End If


                Case "admin"

                    Dim adminFullName As String = "Justine Fritz Bucong"
                    Dim adminName As String = "fritz"
                    Dim adminPass As String = "admin123"
                    Dim newAdmin() As String = {adminFullName, adminName, adminPass}
                    GlobalData.AdminAccount.Add(newAdmin)
                    SaveAdminAccounts()
                    SaveCashierAccountsToFile()

                    For Each acc As String() In GlobalData.AdminAccount

                        Dim username As String = acc(1)
                        Dim password As String = acc(2)

                        If inputUser = username And inputPass = password Then

                            isValid = True
                            fullName = acc(0)
                            Exit For

                        End If

                    Next

                    If Not (txtUser.Text = "" Or txtPass.Text = "") Then
                        If isValid Then

                            GlobalData.userName = fullName

                            MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                            Dim admin As New Admin()
                            admin.Show()
                            Me.Hide()

                        Else

                            MessageBox.Show("Invalid username or password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

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

    Private Sub LoginPage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAdminAccountsFromFile()
        LoadCashierAccountsFromFile()
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
    Public Sub SaveAdminAccounts()

        Dim lines As New List(Of String)

        For Each acc As String() In GlobalData.AdminAccount

            Dim line As String =
            acc(0) & "|" & acc(1) & "|" & acc(2) & "|admin"

            lines.Add(line)

        Next

        IO.File.WriteAllLines(GlobalData.AdminFilePath, lines)

    End Sub

    Public Sub LoadCashierAccountsFromFile()

        If Not IO.File.Exists(CashierFilePath) Then Exit Sub

        GlobalData.registerAccount.Clear()


        For Each line In IO.File.ReadAllLines(CashierFilePath)

            Dim data = line.Split("|")

            If data.Length < 4 Then Continue For

            Dim role As String = data(0)
            Dim fullname As String = data(1)
            Dim username As String = data(2)
            Dim password As String = data(3)

            Dim acc() As String = {fullname, username, password}

            If role = "cashier" Then
                GlobalData.registerAccount.Add(acc)
            End If

        Next

    End Sub

    Public Sub LoadAdminAccountsFromFile()

        If Not IO.File.Exists(AdminFilePath) Then Exit Sub

        GlobalData.registerAccount.Clear()


        For Each line In IO.File.ReadAllLines(AdminFilePath)

            Dim data = line.Split("|")

            If data.Length < 4 Then Continue For

            Dim role As String = data(0)
            Dim fullname As String = data(1)
            Dim username As String = data(2)
            Dim password As String = data(3)

            Dim acc() As String = {fullname, username, password}

            If role = "admin" Then
                GlobalData.AdminAccount.Add(acc)
            End If

        Next

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

        '========================
        ' ADMINS
        '========================
        For Each acc As String() In GlobalData.AdminAccount

            Dim line As String =
                "admin|" & acc(0) & "|" & acc(1) & "|" & acc(2)

            lines.Add(line)

        Next

        IO.File.WriteAllLines(CashierFilePath, lines)

    End Sub
End Class
