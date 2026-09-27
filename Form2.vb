Public Class RegisterNewCashierForm
    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel registration?", "Cancel Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result.Equals(DialogResult.Yes) Then
            Me.Hide()
        End If
    End Sub
End Class