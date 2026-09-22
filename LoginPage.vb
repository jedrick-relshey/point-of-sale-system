Imports System.Drawing.Drawing2D

Public Class LoginPage
    Private Sub btn_Login_Click(sender As Object, e As EventArgs) Handles btn_Login.Click

        Dim username As String = txt_Username_Login.Text
        Dim password As String = txt_Password_Login.Text



    End Sub

    Private Sub btnAdmin_Click(sender As Object, e As EventArgs) Handles btnAdmin.Click

        adminPanel.Visible = True
        loginPanel.Visible = False

    End Sub

    Private Sub bckbtn_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles bckbtn.LinkClicked

        adminPanel.Visible = False
        loginPanel.Visible = True

    End Sub
End Class
