Imports System.Drawing.Drawing2D

Public Class LoginPage
    Private Sub btn_Login_Click(sender As Object, e As EventArgs) Handles btn_Login.Click

        Dim username As String = txt_Username_Login.Text
        Dim password As String = txt_Password_Login.Text

    End Sub

    Private Sub btnAdmin_Click(sender As Object, e As EventArgs)
        loginPanel.Visible = False

    End Sub

    Private Sub bckbtn_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        loginPanel.Visible = True

    End Sub

    Private Sub Guna2RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2RadioButton2.CheckedChanged

    End Sub

    Private Sub Guna2ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Guna2ContextMenuStrip1.Opening

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub

    Private Sub chk_shw_psswrd_CheckedChanged(sender As Object, e As EventArgs) Handles chk_shw_psswrd.CheckedChanged

    End Sub

    Private Sub lblError_Login_Click(sender As Object, e As EventArgs) Handles lblError_Login.Click

    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click

    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub

    Private Sub loginPanel_Paint(sender As Object, e As PaintEventArgs) Handles loginPanel.Paint

    End Sub

    Private Sub Guna2RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2RadioButton1.CheckedChanged

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub txt_Username_Login_TextChanged(sender As Object, e As EventArgs) Handles txt_Username_Login.TextChanged

    End Sub

    Private Sub txt_Password_Login_TextChanged(sender As Object, e As EventArgs) Handles txt_Password_Login.TextChanged

    End Sub

    Private Sub Guna2Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Guna2Panel1.Paint

    End Sub

    Private Sub Guna2Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Guna2Panel2.Paint

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub
End Class
