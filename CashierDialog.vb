Option Strict On
Option Explicit On

Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports Guna.UI2.WinForms

' CashierDialog.vb  -  NEW FILE. The "Add cashier" / "Edit cashier" pop-up (built in code,
' no designer file needed). Admin.Cashiers.vb opens it.
Friend Class CashierDialog
    Inherits Form

    ''' <summary>Called when Save is pressed. Return "" on success, or an error message to show in the dialog.</summary>
    Friend Property SaveHandler As Func(Of CashierDialog, String)
    Friend Property PhotoChanged As Boolean
    Friend Property PhotoRemoved As Boolean

    Private ReadOnly isEditMode As Boolean
    Private photoImg As Image
    Private txtName As Guna2TextBox
    Private txtUser As Guna2TextBox
    Private txtPass As Guna2TextBox
    Private lblError As Label
    Private btnSave As Guna2Button
    Private btnRemove As Guna2Button
    Private previewHost As Panel

    Friend ReadOnly Property FullNameText As String
        Get
            Return txtName.Text.Trim()
        End Get
    End Property

    Friend ReadOnly Property UsernameText As String
        Get
            Return txtUser.Text.Trim()
        End Get
    End Property

    Friend ReadOnly Property PasswordText As String
        Get
            Return txtPass.Text
        End Get
    End Property

    Friend ReadOnly Property PhotoImage As Image
        Get
            Return photoImg
        End Get
    End Property

    Public Sub New(isEdit As Boolean, fullName As String, username As String, photo As Image)

        isEditMode = isEdit
        photoImg = photo

        Me.FormBorderStyle = FormBorderStyle.None
        Me.StartPosition = FormStartPosition.CenterParent
        Me.ShowInTaskbar = False
        Me.BackColor = Color.White
        Me.Font = New Font("Segoe UI", 9.0F)
        Me.ClientSize = New Size(500, 566)
        Me.DoubleBuffered = True
        Me.Region = New Region(RoundedPath(New Rectangle(0, 0, Me.Width, Me.Height), 22))

        ' ---- title ----
        Me.Controls.Add(Counterly.Lbl(If(isEdit, "Edit cashier", "Add cashier"), 19.0F, FontStyle.Regular, Counterly.Ink, 30, 22))
        Me.Controls.Add(Counterly.Lbl(If(isEdit, "Update login details or change the profile photo",
                                         "Create login credentials for a new team member"),
                                      9.5F, FontStyle.Regular, Counterly.Muted, 32, 62))

        Dim closeBtn As Guna2Button = Counterly.GlyphButton(Counterly.GlyphClose, 38, Counterly.Page, Counterly.Muted)
        closeBtn.Location = New Point(500 - 32 - 38, 26)
        AddHandler closeBtn.Click, AddressOf Close_Click
        Me.Controls.Add(closeBtn)

        ' ---- profile photo ----
        previewHost = New Panel()
        previewHost.Size = New Size(76, 76)
        previewHost.Location = New Point(32, 106)
        previewHost.BackColor = Color.White
        Me.Controls.Add(previewHost)

        Me.Controls.Add(Counterly.Lbl("Profile photo", 9.5F, FontStyle.Bold, Counterly.Ink, 126, 104))
        Me.Controls.Add(Counterly.Lbl("PNG or JPG. A square picture works best.", 8.5F, FontStyle.Regular, Counterly.Muted, 127, 125))

        Dim btnChoose As New Guna2Button()
        btnChoose.Text = "Choose photo"
        btnChoose.Size = New Size(150, 38)
        btnChoose.Location = New Point(126, 148)
        btnChoose.BorderRadius = 10
        btnChoose.FillColor = Color.White
        btnChoose.BorderColor = Counterly.Border
        btnChoose.BorderThickness = 1
        btnChoose.ForeColor = Counterly.Ink
        btnChoose.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        btnChoose.HoverState.FillColor = Counterly.Page
        btnChoose.Image = Counterly.GlyphImage(Counterly.GlyphCamera, 16, Counterly.Ink)
        btnChoose.ImageSize = New Size(16, 16)
        btnChoose.ImageAlign = HorizontalAlignment.Left
        btnChoose.ImageOffset = New Point(10, 0)
        btnChoose.Cursor = Cursors.Hand
        AddHandler btnChoose.Click, AddressOf Choose_Click
        Me.Controls.Add(btnChoose)

        btnRemove = New Guna2Button()
        btnRemove.Text = "Remove"
        btnRemove.Size = New Size(90, 38)
        btnRemove.Location = New Point(286, 148)
        btnRemove.BorderRadius = 10
        btnRemove.FillColor = Color.White
        btnRemove.ForeColor = Counterly.Danger
        btnRemove.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        btnRemove.HoverState.FillColor = Color.FromArgb(253, 238, 238)
        btnRemove.Cursor = Cursors.Hand
        AddHandler btnRemove.Click, AddressOf Remove_Click
        Me.Controls.Add(btnRemove)

        ' ---- fields ----
        txtName = AddField("Full Name", Counterly.GlyphUser, 204, False, fullName, "Full name")
        txtUser = AddField("Username", "@", 288, False, username, "username")
        txtPass = AddField("Password", Counterly.GlyphLock, 372, True, "",
                           If(isEdit, "Leave blank to keep current", "Password"))

        AddHandler txtName.TextChanged, AddressOf Name_TextChanged

        ' ---- error line ----
        lblError = Counterly.Lbl("", 9.0F, FontStyle.Regular, Counterly.Danger, 34, 450)
        Me.Controls.Add(lblError)

        ' ---- footer ----
        Dim divider As New Panel()
        divider.BackColor = Counterly.Border
        divider.Size = New Size(436, 1)
        divider.Location = New Point(32, 478)
        Me.Controls.Add(divider)

        Dim btnCancel As New Guna2Button()
        btnCancel.Text = "Cancel"
        btnCancel.Size = New Size(100, 46)
        btnCancel.Location = New Point(32, 496)
        btnCancel.BorderRadius = 12
        btnCancel.FillColor = Color.White
        btnCancel.BorderColor = Counterly.Border
        btnCancel.BorderThickness = 1
        btnCancel.ForeColor = Counterly.Ink
        btnCancel.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnCancel.HoverState.FillColor = Counterly.Page
        btnCancel.Cursor = Cursors.Hand
        AddHandler btnCancel.Click, AddressOf Close_Click
        Me.Controls.Add(btnCancel)

        btnSave = New Guna2Button()
        btnSave.Text = If(isEdit, "Save changes", "Save cashier")
        btnSave.Size = New Size(170, 46)
        btnSave.Location = New Point(500 - 32 - 170, 496)
        btnSave.BorderRadius = 12
        btnSave.FillColor = Counterly.Teal
        btnSave.ForeColor = Color.White
        btnSave.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnSave.HoverState.FillColor = Counterly.TealDark
        btnSave.Image = Counterly.GlyphImage(If(isEdit, Counterly.GlyphSave, Counterly.GlyphAddUser), 18, Color.White)
        btnSave.ImageSize = New Size(18, 18)
        btnSave.ImageAlign = HorizontalAlignment.Left
        btnSave.ImageOffset = New Point(14, 0)
        btnSave.Cursor = Cursors.Hand
        AddHandler btnSave.Click, AddressOf Save_Click
        Me.Controls.Add(btnSave)

        RefreshPreview()
    End Sub

    '-----------------------------------------------------------------
    Private Function AddField(caption As String, icon As String, y As Integer, isPassword As Boolean,
                              value As String, placeholder As String) As Guna2TextBox

        Me.Controls.Add(Counterly.Lbl(caption, 9.5F, FontStyle.Regular, Counterly.Ink, 32, y))

        Dim card As Guna2Panel = Counterly.Card(436, 48, 12, Color.White, Counterly.Border)
        card.Location = New Point(32, y + 22)

        Dim ic As New Label()
        ic.AutoSize = True
        ic.BackColor = Color.Transparent
        ic.ForeColor = Counterly.Muted
        If AscW(icon.Chars(0)) >= &HE000 Then
            ic.Font = New Font("Segoe MDL2 Assets", 12.0F)
            ic.Location = New Point(16, 14)
        Else
            ic.Font = New Font("Segoe UI", 13.0F, FontStyle.Bold)
            ic.Location = New Point(15, 10)
        End If
        ic.Text = icon
        card.Controls.Add(ic)

        Dim tb As New Guna2TextBox()
        tb.BorderThickness = 0
        tb.BorderRadius = 0
        tb.FillColor = Color.White
        tb.Font = New Font("Segoe UI", 10.5F)
        tb.ForeColor = Counterly.Ink
        tb.PlaceholderText = placeholder
        tb.PlaceholderForeColor = Counterly.Muted
        tb.Text = value
        tb.Location = New Point(46, 8)
        tb.Size = New Size(If(isPassword, 436 - 46 - 52, 436 - 46 - 14), 32)
        tb.UseSystemPasswordChar = isPassword
        card.Controls.Add(tb)

        AddHandler tb.Enter, AddressOf Field_Enter
        AddHandler tb.Leave, AddressOf Field_Leave
        AddHandler tb.KeyDown, AddressOf Field_KeyDown

        If isPassword Then
            Dim eye As Guna2Button = Counterly.GlyphButton(Counterly.GlyphEye, 36, Color.White, Counterly.Muted)
            eye.Location = New Point(436 - 44, 6)
            AddHandler eye.Click, AddressOf Eye_Click
            card.Controls.Add(eye)
        End If

        Me.Controls.Add(card)
        Return tb
    End Function

    Private Sub Field_Enter(sender As Object, e As EventArgs)
        Dim card As Guna2Panel = TryCast(DirectCast(sender, Control).Parent, Guna2Panel)
        If card IsNot Nothing Then card.BorderColor = Counterly.Teal
    End Sub

    Private Sub Field_Leave(sender As Object, e As EventArgs)
        Dim card As Guna2Panel = TryCast(DirectCast(sender, Control).Parent, Guna2Panel)
        If card IsNot Nothing Then card.BorderColor = Counterly.Border
    End Sub

    Private Sub Field_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            btnSave.PerformClick()
        End If
    End Sub

    Private Sub Eye_Click(sender As Object, e As EventArgs)
        txtPass.UseSystemPasswordChar = Not txtPass.UseSystemPasswordChar
    End Sub

    Private Sub Name_TextChanged(sender As Object, e As EventArgs)
        If photoImg Is Nothing Then RefreshPreview()
    End Sub

    '-----------------------------------------------------------------
    ' PHOTO
    '-----------------------------------------------------------------
    Private Sub RefreshPreview()
        For i As Integer = previewHost.Controls.Count - 1 To 0 Step -1
            Dim c As Control = previewHost.Controls(i)
            previewHost.Controls.RemoveAt(i)
            c.Dispose()
        Next
        Dim nm As String = If(txtName Is Nothing, "", txtName.Text)
        previewHost.Controls.Add(Counterly.AvatarFor(Counterly.InitialsOf(nm), 76, Counterly.TealSoft, Counterly.Teal, photoImg))
        If btnRemove IsNot Nothing Then btnRemove.Visible = (photoImg IsNot Nothing)
    End Sub

    Private Sub Choose_Click(sender As Object, e As EventArgs)
        Using ofd As New OpenFileDialog()
            ofd.Title = "Choose profile photo"
            ofd.Filter = "Images|*.png;*.jpg;*.jpeg;*.bmp;*.gif|All files|*.*"
            If ofd.ShowDialog(Me) <> DialogResult.OK Then Return

            Try
                photoImg = CashierPhotos.LoadFile(ofd.FileName)
                PhotoChanged = True
                PhotoRemoved = False
                lblError.Text = ""
                RefreshPreview()
            Catch
                lblError.Text = "That file could not be opened as an image."
            End Try
        End Using
    End Sub

    Private Sub Remove_Click(sender As Object, e As EventArgs)
        photoImg = Nothing
        PhotoChanged = False
        PhotoRemoved = True
        RefreshPreview()
    End Sub

    '-----------------------------------------------------------------
    ' SAVE / CLOSE
    '-----------------------------------------------------------------
    Private Sub Save_Click(sender As Object, e As EventArgs)

        If FullNameText = "" Then
            lblError.Text = "Full name is required."
            txtName.Focus()
            Return
        End If
        If UsernameText = "" Then
            lblError.Text = "Username is required."
            txtUser.Focus()
            Return
        End If
        If Not isEditMode AndAlso PasswordText = "" Then
            lblError.Text = "Password is required."
            txtPass.Focus()
            Return
        End If

        Dim err As String = ""
        If SaveHandler IsNot Nothing Then err = SaveHandler(Me)

        If err <> "" Then
            lblError.Text = err
            Return
        End If

        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub Close_Click(sender As Object, e As EventArgs)
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = Keys.Escape Then
            Me.DialogResult = DialogResult.Cancel
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Protected Overrides Sub OnShown(e As EventArgs)
        MyBase.OnShown(e)
        txtName.Focus()
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        Using gp As GraphicsPath = RoundedPath(New Rectangle(0, 0, Me.Width - 1, Me.Height - 1), 22)
            Using pen As New Pen(Counterly.Border, 2.0F)
                e.Graphics.DrawPath(pen, gp)
            End Using
        End Using
    End Sub

    Private Shared Function RoundedPath(r As Rectangle, radius As Integer) As GraphicsPath
        Dim d As Integer = radius * 2
        Dim gp As New GraphicsPath()
        gp.AddArc(r.X, r.Y, d, d, 180, 90)
        gp.AddArc(r.Right - d, r.Y, d, d, 270, 90)
        gp.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90)
        gp.AddArc(r.X, r.Bottom - d, d, d, 90, 90)
        gp.CloseFigure()
        Return gp
    End Function

End Class