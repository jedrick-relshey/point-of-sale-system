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
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Guna2AnimateWindow1 = New Guna.UI2.WinForms.Guna2AnimateWindow(Me.components)
        Me.loginPanel = New Guna.UI2.WinForms.Guna2Panel()
        Me.cbShowHidePass = New Guna.UI2.WinForms.Guna2CheckBox()
        Me.Guna2Panel1 = New Guna.UI2.WinForms.Guna2Panel()
        Me.Guna2Panel2 = New Guna.UI2.WinForms.Guna2Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.rbAdmin = New Guna.UI2.WinForms.Guna2RadioButton()
        Me.rbCashier = New Guna.UI2.WinForms.Guna2RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnLogin = New Guna.UI2.WinForms.Guna2GradientButton()
        Me.txtUser = New Guna.UI2.WinForms.Guna2TextBox()
        Me.txtPass = New Guna.UI2.WinForms.Guna2TextBox()
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
        Me.Label6.Location = New System.Drawing.Point(540, 107)
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
        Me.Label7.Location = New System.Drawing.Point(537, 153)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(259, 24)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Please sign in to your account"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.DimGray
        Me.Label10.Location = New System.Drawing.Point(560, 622)
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
        Me.Label9.Location = New System.Drawing.Point(501, 298)
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
        Me.Label8.Location = New System.Drawing.Point(501, 202)
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
        Me.loginPanel.Controls.Add(Me.cbShowHidePass)
        Me.loginPanel.Controls.Add(Me.Guna2Panel1)
        Me.loginPanel.Controls.Add(Me.rbAdmin)
        Me.loginPanel.Controls.Add(Me.rbCashier)
        Me.loginPanel.Controls.Add(Me.Label4)
        Me.loginPanel.Controls.Add(Me.Label9)
        Me.loginPanel.Controls.Add(Me.btnLogin)
        Me.loginPanel.Controls.Add(Me.txtUser)
        Me.loginPanel.Controls.Add(Me.Label7)
        Me.loginPanel.Controls.Add(Me.txtPass)
        Me.loginPanel.Controls.Add(Me.Label6)
        Me.loginPanel.Controls.Add(Me.Label10)
        Me.loginPanel.Controls.Add(Me.Label8)
        Me.loginPanel.FillColor = System.Drawing.Color.White
        Me.loginPanel.Location = New System.Drawing.Point(87, 52)
        Me.loginPanel.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.loginPanel.Name = "loginPanel"
        Me.loginPanel.ShadowDecoration.BorderRadius = 40
        Me.loginPanel.ShadowDecoration.Color = System.Drawing.Color.Gray
        Me.loginPanel.ShadowDecoration.Enabled = True
        Me.loginPanel.ShadowDecoration.Shadow = New System.Windows.Forms.Padding(0, 0, 7, 7)
        Me.loginPanel.Size = New System.Drawing.Size(900, 650)
        Me.loginPanel.TabIndex = 27
        '
        'cbShowHidePass
        '
        Me.cbShowHidePass.AutoSize = True
        Me.cbShowHidePass.CheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cbShowHidePass.CheckedState.BorderRadius = 0
        Me.cbShowHidePass.CheckedState.BorderThickness = 0
        Me.cbShowHidePass.CheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(93, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.cbShowHidePass.Font = New System.Drawing.Font("Verdana", 7.8!)
        Me.cbShowHidePass.ForeColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.cbShowHidePass.Location = New System.Drawing.Point(508, 380)
        Me.cbShowHidePass.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cbShowHidePass.Name = "cbShowHidePass"
        Me.cbShowHidePass.Size = New System.Drawing.Size(132, 20)
        Me.cbShowHidePass.TabIndex = 24
        Me.cbShowHidePass.Text = "Show Password"
        Me.cbShowHidePass.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.cbShowHidePass.UncheckedState.BorderRadius = 0
        Me.cbShowHidePass.UncheckedState.BorderThickness = 0
        Me.cbShowHidePass.UncheckedState.FillColor = System.Drawing.Color.Transparent
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
        Me.Guna2Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Guna2Panel1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Guna2Panel1.Name = "Guna2Panel1"
        Me.Guna2Panel1.Size = New System.Drawing.Size(451, 650)
        Me.Guna2Panel1.TabIndex = 21
        '
        'Guna2Panel2
        '
        Me.Guna2Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Guna2Panel2.BorderColor = System.Drawing.Color.Transparent
        Me.Guna2Panel2.FillColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.Guna2Panel2.Location = New System.Drawing.Point(401, 0)
        Me.Guna2Panel2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Guna2Panel2.Name = "Guna2Panel2"
        Me.Guna2Panel2.Size = New System.Drawing.Size(51, 650)
        Me.Guna2Panel2.TabIndex = 29
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.LightGray
        Me.Label3.Location = New System.Drawing.Point(164, 465)
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
        Me.Label2.Location = New System.Drawing.Point(101, 412)
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
        Me.Label1.Location = New System.Drawing.Point(100, 366)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(270, 34)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "BrewHaven POS"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.Vb_POS.My.Resources.Resources.cafe_logo
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(153, 225)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(152, 137)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'rbAdmin
        '
        Me.rbAdmin.AutoSize = True
        Me.rbAdmin.CheckedState.BorderColor = System.Drawing.Color.Black
        Me.rbAdmin.CheckedState.BorderThickness = 1
        Me.rbAdmin.CheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.rbAdmin.CheckedState.InnerColor = System.Drawing.Color.White
        Me.rbAdmin.CheckedState.InnerOffset = -4
        Me.rbAdmin.Font = New System.Drawing.Font("Verdana", 7.8!)
        Me.rbAdmin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.rbAdmin.Location = New System.Drawing.Point(613, 462)
        Me.rbAdmin.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.rbAdmin.Name = "rbAdmin"
        Me.rbAdmin.Size = New System.Drawing.Size(115, 20)
        Me.rbAdmin.TabIndex = 23
        Me.rbAdmin.Text = "Administrator"
        Me.rbAdmin.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.rbAdmin.UncheckedState.BorderThickness = 1
        Me.rbAdmin.UncheckedState.FillColor = System.Drawing.Color.Transparent
        Me.rbAdmin.UncheckedState.InnerColor = System.Drawing.Color.Transparent
        '
        'rbCashier
        '
        Me.rbCashier.AutoSize = True
        Me.rbCashier.CheckedState.BorderColor = System.Drawing.Color.Black
        Me.rbCashier.CheckedState.BorderThickness = 1
        Me.rbCashier.CheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(93, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.rbCashier.CheckedState.InnerColor = System.Drawing.Color.White
        Me.rbCashier.CheckedState.InnerOffset = -4
        Me.rbCashier.Font = New System.Drawing.Font("Verdana", 7.8!)
        Me.rbCashier.ForeColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.rbCashier.Location = New System.Drawing.Point(509, 462)
        Me.rbCashier.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.rbCashier.Name = "rbCashier"
        Me.rbCashier.Size = New System.Drawing.Size(76, 20)
        Me.rbCashier.TabIndex = 22
        Me.rbCashier.Text = "Cashier"
        Me.rbCashier.UncheckedState.BorderColor = System.Drawing.Color.Black
        Me.rbCashier.UncheckedState.BorderThickness = 1
        Me.rbCashier.UncheckedState.FillColor = System.Drawing.Color.Transparent
        Me.rbCashier.UncheckedState.InnerColor = System.Drawing.Color.Transparent
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Verdana", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(509, 433)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(81, 16)
        Me.Label4.TabIndex = 21
        Me.Label4.Text = "Login Role"
        '
        'btnLogin
        '
        Me.btnLogin.Animated = True
        Me.btnLogin.BackColor = System.Drawing.Color.Transparent
        Me.btnLogin.BorderRadius = 12
        Me.btnLogin.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnLogin.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnLogin.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnLogin.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnLogin.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnLogin.FillColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.btnLogin.FillColor2 = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(99, Byte), Integer))
        Me.btnLogin.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold)
        Me.btnLogin.ForeColor = System.Drawing.Color.White
        Me.btnLogin.Image = Global.Vb_POS.My.Resources.Resources.icons8_login_30
        Me.btnLogin.Location = New System.Drawing.Point(505, 508)
        Me.btnLogin.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(328, 50)
        Me.btnLogin.TabIndex = 10
        Me.btnLogin.Text = "LOGIN"
        '
        'txtUser
        '
        Me.txtUser.Animated = True
        Me.txtUser.BackColor = System.Drawing.Color.Transparent
        Me.txtUser.BorderColor = System.Drawing.Color.Gainsboro
        Me.txtUser.BorderRadius = 10
        Me.txtUser.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUser.DefaultText = ""
        Me.txtUser.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtUser.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtUser.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtUser.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtUser.FillColor = System.Drawing.Color.WhiteSmoke
        Me.txtUser.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUser.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUser.ForeColor = System.Drawing.Color.Gray
        Me.txtUser.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUser.IconLeft = Global.Vb_POS.My.Resources.Resources.pfp_icon_removebg_preview
        Me.txtUser.Location = New System.Drawing.Point(508, 225)
        Me.txtUser.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.txtUser.Name = "txtUser"
        Me.txtUser.PlaceholderForeColor = System.Drawing.Color.DimGray
        Me.txtUser.PlaceholderText = "Enter your Username"
        Me.txtUser.SelectedText = ""
        Me.txtUser.Size = New System.Drawing.Size(328, 50)
        Me.txtUser.TabIndex = 9
        '
        'txtPass
        '
        Me.txtPass.Animated = True
        Me.txtPass.BackColor = System.Drawing.Color.Transparent
        Me.txtPass.BorderColor = System.Drawing.Color.Gainsboro
        Me.txtPass.BorderRadius = 10
        Me.txtPass.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPass.DefaultText = ""
        Me.txtPass.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtPass.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtPass.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtPass.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtPass.FillColor = System.Drawing.Color.WhiteSmoke
        Me.txtPass.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtPass.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPass.ForeColor = System.Drawing.Color.Gray
        Me.txtPass.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtPass.IconLeft = Global.Vb_POS.My.Resources.Resources.lock_icon_removebg_preview1
        Me.txtPass.Location = New System.Drawing.Point(508, 321)
        Me.txtPass.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.txtPass.Name = "txtPass"
        Me.txtPass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPass.PlaceholderForeColor = System.Drawing.Color.DimGray
        Me.txtPass.PlaceholderText = "Enter your Password"
        Me.txtPass.SelectedText = ""
        Me.txtPass.Size = New System.Drawing.Size(328, 50)
        Me.txtPass.TabIndex = 11
        '
        'LoginPage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1083, 753)
        Me.Controls.Add(Me.loginPanel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
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
    Friend WithEvents txtUser As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents btnLogin As Guna.UI2.WinForms.Guna2GradientButton
    Friend WithEvents txtPass As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents Guna2AnimateWindow1 As Guna.UI2.WinForms.Guna2AnimateWindow
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents loginPanel As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Guna2Panel1 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Guna2Panel2 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents rbAdmin As Guna.UI2.WinForms.Guna2RadioButton
    Friend WithEvents rbCashier As Guna.UI2.WinForms.Guna2RadioButton
    Friend WithEvents cbShowHidePass As Guna.UI2.WinForms.Guna2CheckBox
End Class
