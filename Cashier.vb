Imports System.Drawing
Public Class Cashier

    Private Sub HideAllPanels()

        main_pnl.Visible = False
        pnl_CashierMessages.Visible = False

    End Sub

    Private Sub ResetButtonColors()
        btn_DashBoard.BackColor = Color.Transparent
        btn_Point_Of_Sale.BackColor = Color.Transparent
        btn_prdt.BackColor = Color.Transparent
        btn_invtry.BackColor = Color.Transparent
        btn_hstry.BackColor = Color.Transparent

        btn_DashBoard.ForeColor = Color.DarkGray
    End Sub

    Private Sub btn_DashBoard_Click(sender As Object, e As EventArgs) Handles btn_DashBoard.Click

        HideAllPanels()
        main_pnl.Visible = True

        ResetButtonColors()

        btn_DashBoard.BackColor = Color.MistyRose
        btn_DashBoard.ForeColor = Color.Black

    End Sub

    Private Sub btn_Point_Of_Sale_Click(sender As Object, e As EventArgs) Handles btn_Point_Of_Sale.Click

        HideAllPanels()

        ResetButtonColors()

        btn_Point_Of_Sale.BackColor = Color.MistyRose
        btn_Point_Of_Sale.ForeColor = Color.Black

    End Sub

    Private Sub btnCashierLogout_Click(sender As Object, e As EventArgs) Handles btnCashierLogout.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Dim loginPage As New LoginPage()
            loginPage.Show()
            Me.Hide()
            MessageBox.Show("Logout successful.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Logout canceled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub btn_CashierMessages_Click(sender As Object, e As EventArgs) Handles btn_CashierMessages.Click

        pnl_CashierMessages.Visible = True
        LoadCashierChat()

    End Sub

    Private Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click

        'Check kung walang laman ang message
        If String.IsNullOrWhiteSpace(txtChat.Text) Then
            Return
        End If

        'Create new chat message
        Dim newMessage As New ChatMessage With {
        .Sender = "Cashier",
        .Receiver = "Admin",
        .Message = txtChat.Text.Trim(),
        .TimeSent = DateTime.Now
    }

        'Store the message
        DataStore.ChatMEssages.Add(newMessage)

        'Clear textbox
        txtChat.Clear()

        'refresh chat
        LoadCashierChat()

    End Sub

    'Private Sub Cashier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    '    cashierNAme.Text = GlobalData.userName
    '    cashierName.Text = cashierName.Text.ToUpper()
    'End Sub

    Private Sub LoadCashierChat()

        flpMessages.Controls.Clear()

        For Each chat As ChatMessage In DataStore.ChatMessages

            Dim messageLabel As New Label()

            messageLabel.Text = chat.Message
            messageLabel.AutoSize = True
            messageLabel.MaximumSize = New Size(flpMessages.ClientSize.Width - 40, 0)

            messageLabel.Padding = New Padding(10)
            messageLabel.Margin = New Padding(5)
            messageLabel.Font = New Font("Segoe UI", 10, FontStyle.Regular)

            'Cashier = right side
            If chat.Sender = "Cashier" Then
                messageLabel.TextAlign = ContentAlignment.MiddleRight
                messageLabel.Anchor = AnchorStyles.Right

                'Admin = left side
            ElseIf chat.Sender = "Admin" Then
                messageLabel.TextAlign = ContentAlignment.MiddleLeft
                messageLabel.Anchor = AnchorStyles.Left
            End If

            flpMessages.Controls.Add(messageLabel)

        Next

    End Sub
End Class