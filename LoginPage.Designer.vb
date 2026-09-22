<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LoginPage
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LoginPage))
        Me.Guna2ContextMenuStrip1 = New Guna.UI2.WinForms.Guna2ContextMenuStrip()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chk_shw_psswrd = New System.Windows.Forms.CheckBox()
        Me.lblError_Login = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Guna2AnimateWindow1 = New Guna.UI2.WinForms.Guna2AnimateWindow(Me.components)
        Me.loginPanel = New Guna.UI2.WinForms.Guna2Panel()
        Me.Guna2RadioButton2 = New Guna.UI2.WinForms.Guna2RadioButton()
        Me.Guna2RadioButton1 = New Guna.UI2.WinForms.Guna2RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btn_Login = New Guna.UI2.WinForms.Guna2GradientButton()
        Me.txt_Username_Login = New Guna.UI2.WinForms.Guna2TextBox()
        Me.txt_Password_Login = New Guna.UI2.WinForms.Guna2TextBox()
        Me.Guna2Panel1 = New Guna.UI2.WinForms.Guna2Panel()
        Me.Guna2Panel2 = New Guna.UI2.WinForms.Guna2Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.loginPanel.SuspendLayout()
        Me.Guna2Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Guna2ContextMenuStrip1
        '
        Me.Guna2ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.Guna2ContextMenuStrip1.Name = "Guna2ContextMenuStrip1"
        Me.Guna2ContextMenuStrip1.RenderStyle.ArrowColor = System.Drawing.Color.FromArgb(CType(CType(151, Byte), Integer), CType(CType(143, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Guna2ContextMenuStrip1.RenderStyle.BorderColor = System.Drawing.Color.Gainsboro
        Me.Guna2ContextMenuStrip1.RenderStyle.ColorTable = Nothing
        Me.Guna2ContextMenuStrip1.RenderStyle.RoundedEdges = True
        Me.Guna2ContextMenuStrip1.RenderStyle.SelectionArrowColor = System.Drawing.Color.White
        Me.Guna2ContextMenuStrip1.RenderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Guna2ContextMenuStrip1.RenderStyle.SelectionForeColor = System.Drawing.Color.White
        Me.Guna2ContextMenuStrip1.RenderStyle.SeparatorColor = System.Drawing.Color.Gainsboro
        Me.Guna2ContextMenuStrip1.RenderStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault
        Me.Guna2ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Calibri", 22.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(538, 107)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(257, 45)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Welcome Back!"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Gray
        Me.Label7.Location = New System.Drawing.Point(536, 152)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(259, 24)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Please sign in to your account"
        '
        'chk_shw_psswrd
        '
        Me.chk_shw_psswrd.AutoSize = True
        Me.chk_shw_psswrd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.chk_shw_psswrd.Location = New System.Drawing.Point(506, 395)
        Me.chk_shw_psswrd.Margin = New System.Windows.Forms.Padding(4)
        Me.chk_shw_psswrd.Name = "chk_shw_psswrd"
        Me.chk_shw_psswrd.Size = New System.Drawing.Size(125, 20)
        Me.chk_shw_psswrd.TabIndex = 20
        Me.chk_shw_psswrd.Text = "Show Password"
        Me.chk_shw_psswrd.UseVisualStyleBackColor = True
        '
        'lblError_Login
        '
        Me.lblError_Login.AutoSize = True
        Me.lblError_Login.ForeColor = System.Drawing.Color.Red
        Me.lblError_Login.Location = New System.Drawing.Point(503, 503)
        Me.lblError_Login.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblError_Login.Name = "lblError_Login"
        Me.lblError_Login.Size = New System.Drawing.Size(288, 16)
        Me.lblError_Login.TabIndex = 19
        Me.lblError_Login.Text = "Invalid Username or Password. Please try agin."
        Me.lblError_Login.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.DimGray
        Me.Label10.Location = New System.Drawing.Point(570, 614)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(221, 16)
        Me.Label10.TabIndex = 18
        Me.Label10.Text = "Secure login - your data is protected"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(502, 297)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(80, 18)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Password"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(502, 204)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(84, 18)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Username"
        '
        'loginPanel
        '
        Me.loginPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.loginPanel.BackColor = System.Drawing.Color.Transparent
        Me.loginPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(13, Byte), Integer))
        Me.loginPanel.BorderRadius = 40
        Me.loginPanel.BorderThickness = 1
        Me.loginPanel.Controls.Add(Me.Guna2RadioButton2)
        Me.loginPanel.Controls.Add(Me.Guna2RadioButton1)
        Me.loginPanel.Controls.Add(Me.Label4)
        Me.loginPanel.Controls.Add(Me.Label9)
        Me.loginPanel.Controls.Add(Me.btn_Login)
        Me.loginPanel.Controls.Add(Me.txt_Username_Login)
        Me.loginPanel.Controls.Add(Me.chk_shw_psswrd)
        Me.loginPanel.Controls.Add(Me.Label7)
        Me.loginPanel.Controls.Add(Me.txt_Password_Login)
        Me.loginPanel.Controls.Add(Me.lblError_Login)
        Me.loginPanel.Controls.Add(Me.Label6)
        Me.loginPanel.Controls.Add(Me.Label10)
        Me.loginPanel.Controls.Add(Me.Label8)
        Me.loginPanel.FillColor = System.Drawing.Color.White
        Me.loginPanel.Location = New System.Drawing.Point(218, 56)
        Me.loginPanel.Margin = New System.Windows.Forms.Padding(4)
        Me.loginPanel.Name = "loginPanel"
        Me.loginPanel.ShadowDecoration.BorderRadius = 40
        Me.loginPanel.ShadowDecoration.Color = System.Drawing.Color.Gray
        Me.loginPanel.ShadowDecoration.Enabled = True
        Me.loginPanel.ShadowDecoration.Shadow = New System.Windows.Forms.Padding(0, 0, 7, 7)
        Me.loginPanel.Size = New System.Drawing.Size(900, 650)
        Me.loginPanel.TabIndex = 27
        '
        'Guna2RadioButton2
        '
        Me.Guna2RadioButton2.AutoSize = True
        Me.Guna2RadioButton2.CheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Guna2RadioButton2.CheckedState.BorderThickness = 0
        Me.Guna2RadioButton2.CheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Guna2RadioButton2.CheckedState.InnerColor = System.Drawing.Color.White
        Me.Guna2RadioButton2.CheckedState.InnerOffset = -4
        Me.Guna2RadioButton2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.Guna2RadioButton2.Location = New System.Drawing.Point(614, 461)
        Me.Guna2RadioButton2.Name = "Guna2RadioButton2"
        Me.Guna2RadioButton2.Size = New System.Drawing.Size(106, 20)
        Me.Guna2RadioButton2.TabIndex = 23
        Me.Guna2RadioButton2.Text = "Administrator"
        Me.Guna2RadioButton2.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.Guna2RadioButton2.UncheckedState.BorderThickness = 2
        Me.Guna2RadioButton2.UncheckedState.FillColor = System.Drawing.Color.Transparent
        Me.Guna2RadioButton2.UncheckedState.InnerColor = System.Drawing.Color.Transparent
        '
        'Guna2RadioButton1
        '
        Me.Guna2RadioButton1.AutoSize = True
        Me.Guna2RadioButton1.CheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Guna2RadioButton1.CheckedState.BorderThickness = 0
        Me.Guna2RadioButton1.CheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Guna2RadioButton1.CheckedState.InnerColor = System.Drawing.Color.White
        Me.Guna2RadioButton1.CheckedState.InnerOffset = -4
        Me.Guna2RadioButton1.Location = New System.Drawing.Point(509, 461)
        Me.Guna2RadioButton1.Name = "Guna2RadioButton1"
        Me.Guna2RadioButton1.Size = New System.Drawing.Size(74, 20)
        Me.Guna2RadioButton1.TabIndex = 22
        Me.Guna2RadioButton1.Text = "Cashier"
        Me.Guna2RadioButton1.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.Guna2RadioButton1.UncheckedState.BorderThickness = 2
        Me.Guna2RadioButton1.UncheckedState.FillColor = System.Drawing.Color.Transparent
        Me.Guna2RadioButton1.UncheckedState.InnerColor = System.Drawing.Color.Transparent
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(503, 431)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(84, 18)
        Me.Label4.TabIndex = 21
        Me.Label4.Text = "Login Role"
        '
        'btn_Login
        '
        Me.btn_Login.Animated = True
        Me.btn_Login.BackColor = System.Drawing.Color.Transparent
        Me.btn_Login.BorderRadius = 12
        Me.btn_Login.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btn_Login.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btn_Login.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btn_Login.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btn_Login.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btn_Login.FillColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(193, Byte), Integer))
        Me.btn_Login.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold)
        Me.btn_Login.ForeColor = System.Drawing.Color.White
        Me.btn_Login.Image = Global.Vb_POS.My.Resources.Resources.icons8_login_30
        Me.btn_Login.Location = New System.Drawing.Point(506, 532)
        Me.btn_Login.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_Login.Name = "btn_Login"
        Me.btn_Login.Size = New System.Drawing.Size(328, 50)
        Me.btn_Login.TabIndex = 10
        Me.btn_Login.Text = "LOGIN"
        '
        'txt_Username_Login
        '
        Me.txt_Username_Login.Animated = True
        Me.txt_Username_Login.BackColor = System.Drawing.Color.Transparent
        Me.txt_Username_Login.BorderColor = System.Drawing.Color.Gainsboro
        Me.txt_Username_Login.BorderRadius = 10
        Me.txt_Username_Login.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txt_Username_Login.DefaultText = ""
        Me.txt_Username_Login.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txt_Username_Login.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txt_Username_Login.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txt_Username_Login.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txt_Username_Login.FillColor = System.Drawing.Color.WhiteSmoke
        Me.txt_Username_Login.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txt_Username_Login.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txt_Username_Login.ForeColor = System.Drawing.Color.Gray
        Me.txt_Username_Login.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txt_Username_Login.IconLeft = Global.Vb_POS.My.Resources.Resources.user__3_
        Me.txt_Username_Login.Location = New System.Drawing.Point(506, 233)
        Me.txt_Username_Login.Margin = New System.Windows.Forms.Padding(5)
        Me.txt_Username_Login.Name = "txt_Username_Login"
        Me.txt_Username_Login.PlaceholderForeColor = System.Drawing.Color.DimGray
        Me.txt_Username_Login.PlaceholderText = "Enter your Username"
        Me.txt_Username_Login.SelectedText = ""
        Me.txt_Username_Login.Size = New System.Drawing.Size(328, 50)
        Me.txt_Username_Login.TabIndex = 9
        '
        'txt_Password_Login
        '
        Me.txt_Password_Login.Animated = True
        Me.txt_Password_Login.BackColor = System.Drawing.Color.Transparent
        Me.txt_Password_Login.BorderColor = System.Drawing.Color.Gainsboro
        Me.txt_Password_Login.BorderRadius = 10
        Me.txt_Password_Login.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txt_Password_Login.DefaultText = ""
        Me.txt_Password_Login.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txt_Password_Login.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txt_Password_Login.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txt_Password_Login.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txt_Password_Login.FillColor = System.Drawing.Color.WhiteSmoke
        Me.txt_Password_Login.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txt_Password_Login.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txt_Password_Login.ForeColor = System.Drawing.Color.Gray
        Me.txt_Password_Login.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txt_Password_Login.IconLeft = Global.Vb_POS.My.Resources.Resources.lock
        Me.txt_Password_Login.Location = New System.Drawing.Point(506, 329)
        Me.txt_Password_Login.Margin = New System.Windows.Forms.Padding(5)
        Me.txt_Password_Login.Name = "txt_Password_Login"
        Me.txt_Password_Login.PlaceholderForeColor = System.Drawing.Color.DimGray
        Me.txt_Password_Login.PlaceholderText = "Enter your Password"
        Me.txt_Password_Login.SelectedText = ""
        Me.txt_Password_Login.Size = New System.Drawing.Size(328, 50)
        Me.txt_Password_Login.TabIndex = 11
        '
        'Guna2Panel1
        '
        Me.Guna2Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Guna2Panel1.BorderColor = System.Drawing.Color.Transparent
        Me.Guna2Panel1.BorderRadius = 40
        Me.Guna2Panel1.Controls.Add(Me.Guna2Panel2)
        Me.Guna2Panel1.Controls.Add(Me.Label3)
        Me.Guna2Panel1.Controls.Add(Me.Label2)
        Me.Guna2Panel1.Controls.Add(Me.Label1)
        Me.Guna2Panel1.Controls.Add(Me.PictureBox1)
        Me.Guna2Panel1.FillColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.Guna2Panel1.Location = New System.Drawing.Point(218, 56)
        Me.Guna2Panel1.Name = "Guna2Panel1"
        Me.Guna2Panel1.Size = New System.Drawing.Size(450, 650)
        Me.Guna2Panel1.TabIndex = 21
        '
        'Guna2Panel2
        '
        Me.Guna2Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Guna2Panel2.BorderColor = System.Drawing.Color.Transparent
        Me.Guna2Panel2.FillColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.Guna2Panel2.Location = New System.Drawing.Point(400, 0)
        Me.Guna2Panel2.Name = "Guna2Panel2"
        Me.Guna2Panel2.Size = New System.Drawing.Size(50, 650)
        Me.Guna2Panel2.TabIndex = 29
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.LightGray
        Me.Label3.Location = New System.Drawing.Point(162, 465)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(121, 16)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "v2.4.1 Build 2026"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.LightGray
        Me.Label2.Location = New System.Drawing.Point(102, 412)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(267, 20)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "BrewHaven Cafe Management"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(99, 365)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(270, 34)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "BrewHaven POS"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.Vb_POS.My.Resources.Resources.cafe_logo
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(152, 225)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(152, 137)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'LoginPage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1282, 803)
        Me.Controls.Add(Me.Guna2Panel1)
        Me.Controls.Add(Me.loginPanel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "LoginPage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.loginPanel.ResumeLayout(False)
        Me.loginPanel.PerformLayout()
        Me.Guna2Panel1.ResumeLayout(False)
        Me.Guna2Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Guna2ContextMenuStrip1 As Guna.UI2.WinForms.Guna2ContextMenuStrip
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents txt_Username_Login As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents btn_Login As Guna.UI2.WinForms.Guna2GradientButton
    Friend WithEvents txt_Password_Login As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents Guna2AnimateWindow1 As Guna.UI2.WinForms.Guna2AnimateWindow
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents chk_shw_psswrd As CheckBox
    Friend WithEvents lblError_Login As Label
    Friend WithEvents loginPanel As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Guna2Panel1 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Guna2Panel2 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Guna2RadioButton2 As Guna.UI2.WinForms.Guna2RadioButton
    Friend WithEvents Guna2RadioButton1 As Guna.UI2.WinForms.Guna2RadioButton
End Class
