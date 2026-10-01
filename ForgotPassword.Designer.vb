<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ForgotPassword
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtNewPass = New Guna.UI2.WinForms.Guna2TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtConfirmNewPass = New Guna.UI2.WinForms.Guna2TextBox()
        Me.cbShowHidePass = New Guna.UI2.WinForms.Guna2CheckBox()
        Me.btnChangePass = New Guna.UI2.WinForms.Guna2Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Calibri", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(76, 57)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(218, 35)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Change Password"
        '
        'txtNewPass
        '
        Me.txtNewPass.BorderColor = System.Drawing.Color.FromArgb(CType(CType(179, Byte), Integer), CType(CType(143, Byte), Integer), CType(CType(132, Byte), Integer))
        Me.txtNewPass.BorderRadius = 5
        Me.txtNewPass.BorderThickness = 2
        Me.txtNewPass.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNewPass.DefaultText = ""
        Me.txtNewPass.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtNewPass.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtNewPass.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtNewPass.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtNewPass.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtNewPass.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtNewPass.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtNewPass.Location = New System.Drawing.Point(102, 148)
        Me.txtNewPass.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNewPass.Name = "txtNewPass"
        Me.txtNewPass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtNewPass.PlaceholderText = "Enter new password"
        Me.txtNewPass.SelectedText = ""
        Me.txtNewPass.Size = New System.Drawing.Size(319, 48)
        Me.txtNewPass.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(99, 126)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(108, 20)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "New Password"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(100, 214)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(167, 20)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Confirm New Password"
        '
        'txtConfirmNewPass
        '
        Me.txtConfirmNewPass.BorderColor = System.Drawing.Color.FromArgb(CType(CType(179, Byte), Integer), CType(CType(143, Byte), Integer), CType(CType(132, Byte), Integer))
        Me.txtConfirmNewPass.BorderRadius = 5
        Me.txtConfirmNewPass.BorderThickness = 2
        Me.txtConfirmNewPass.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtConfirmNewPass.DefaultText = ""
        Me.txtConfirmNewPass.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtConfirmNewPass.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtConfirmNewPass.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtConfirmNewPass.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtConfirmNewPass.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtConfirmNewPass.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtConfirmNewPass.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtConfirmNewPass.Location = New System.Drawing.Point(103, 236)
        Me.txtConfirmNewPass.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtConfirmNewPass.Name = "txtConfirmNewPass"
        Me.txtConfirmNewPass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtConfirmNewPass.PlaceholderText = "Confirm new password"
        Me.txtConfirmNewPass.SelectedText = ""
        Me.txtConfirmNewPass.Size = New System.Drawing.Size(319, 48)
        Me.txtConfirmNewPass.TabIndex = 3
        '
        'cbShowHidePass
        '
        Me.cbShowHidePass.AutoSize = True
        Me.cbShowHidePass.CheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.cbShowHidePass.CheckedState.BorderRadius = 0
        Me.cbShowHidePass.CheckedState.BorderThickness = 1
        Me.cbShowHidePass.CheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.cbShowHidePass.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!)
        Me.cbShowHidePass.ForeColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.cbShowHidePass.Location = New System.Drawing.Point(104, 303)
        Me.cbShowHidePass.Name = "cbShowHidePass"
        Me.cbShowHidePass.Size = New System.Drawing.Size(125, 20)
        Me.cbShowHidePass.TabIndex = 5
        Me.cbShowHidePass.Text = "Show Password"
        Me.cbShowHidePass.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.cbShowHidePass.UncheckedState.BorderRadius = 0
        Me.cbShowHidePass.UncheckedState.BorderThickness = 1
        Me.cbShowHidePass.UncheckedState.FillColor = System.Drawing.Color.LightGray
        '
        'btnChangePass
        '
        Me.btnChangePass.BorderRadius = 5
        Me.btnChangePass.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnChangePass.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnChangePass.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnChangePass.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnChangePass.FillColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.btnChangePass.Font = New System.Drawing.Font("Segoe UI Semibold", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnChangePass.ForeColor = System.Drawing.Color.White
        Me.btnChangePass.Location = New System.Drawing.Point(102, 345)
        Me.btnChangePass.Name = "btnChangePass"
        Me.btnChangePass.Size = New System.Drawing.Size(320, 57)
        Me.btnChangePass.TabIndex = 6
        Me.btnChangePass.Text = "Change Password"
        '
        'ForgotPassword
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(689, 544)
        Me.Controls.Add(Me.btnChangePass)
        Me.Controls.Add(Me.cbShowHidePass)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtConfirmNewPass)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtNewPass)
        Me.Controls.Add(Me.Label1)
        Me.Name = "ForgotPassword"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ForgotPassword"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txtNewPass As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtConfirmNewPass As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents cbShowHidePass As Guna.UI2.WinForms.Guna2CheckBox
    Friend WithEvents btnChangePass As Guna.UI2.WinForms.Guna2Button
End Class
