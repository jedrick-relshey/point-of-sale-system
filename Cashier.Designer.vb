<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Cashier
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Cashier))
        Me.dshbrd_Pnl = New Guna.UI2.WinForms.Guna2Panel()
        Me.lblManagement = New System.Windows.Forms.Label()
        Me.btnCashierLogout = New Guna.UI2.WinForms.Guna2Button()
        Me.pnlRegister = New Guna.UI2.WinForms.Guna2Panel()
        Me.lblRegShift = New System.Windows.Forms.Label()
        Me.lblRegName = New System.Windows.Forms.Label()
        Me.lblRegOpen = New System.Windows.Forms.Label()
        Me.btn_CashierMessages = New Guna.UI2.WinForms.Guna2Button()
        Me.btn_hstry = New Guna.UI2.WinForms.Guna2Button()
        Me.btn_invtry = New Guna.UI2.WinForms.Guna2Button()
        Me.btn_Point_Of_Sale = New Guna.UI2.WinForms.Guna2Button()
        Me.btn_DashBoard = New Guna.UI2.WinForms.Guna2Button()
        Me.lblWorkspace = New System.Windows.Forms.Label()
        Me.Guna2HtmlLabel5 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.Guna2HtmlLabel7 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.main_pnl = New Guna.UI2.WinForms.Guna2Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pnlInvAttn = New Guna.UI2.WinForms.Guna2Panel()
        Me.flInvAttn = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblInvAttnCount = New System.Windows.Forms.Label()
        Me.lblInvAttnTitle = New System.Windows.Forms.Label()
        Me.pnlInvCat = New Guna.UI2.WinForms.Guna2Panel()
        Me.flInvCat = New System.Windows.Forms.FlowLayoutPanel()
        Me.picInvCatBar = New System.Windows.Forms.PictureBox()
        Me.lblInvCatTotal = New System.Windows.Forms.Label()
        Me.lblInvCatTitle = New System.Windows.Forms.Label()
        Me.pnlInvTable = New Guna.UI2.WinForms.Guna2Panel()
        Me.invGrid = New System.Windows.Forms.DataGridView()
        Me.lblInvShowing = New System.Windows.Forms.Label()
        Me.lblInvCount = New System.Windows.Forms.Label()
        Me.lblInvTableTitle = New System.Windows.Forms.Label()
        Me.cboInvStatus = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.cboInvCategory = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.txtInvSearch = New Guna.UI2.WinForms.Guna2TextBox()
        Me.pnlIK1 = New Guna.UI2.WinForms.Guna2Panel()
        Me.picIK1 = New Guna.UI2.WinForms.Guna2PictureBox()
        Me.lblIK1S = New System.Windows.Forms.Label()
        Me.lblIK1V = New System.Windows.Forms.Label()
        Me.lblIK1T = New System.Windows.Forms.Label()
        Me.pnlIK2 = New Guna.UI2.WinForms.Guna2Panel()
        Me.picIK2 = New Guna.UI2.WinForms.Guna2PictureBox()
        Me.lblIK2S = New System.Windows.Forms.Label()
        Me.lblIK2V = New System.Windows.Forms.Label()
        Me.lblIK2T = New System.Windows.Forms.Label()
        Me.pnlIK3 = New Guna.UI2.WinForms.Guna2Panel()
        Me.picIK3 = New Guna.UI2.WinForms.Guna2PictureBox()
        Me.lblIK3S = New System.Windows.Forms.Label()
        Me.lblIK3V = New System.Windows.Forms.Label()
        Me.lblIK3T = New System.Windows.Forms.Label()
        Me.pnlIK4 = New Guna.UI2.WinForms.Guna2Panel()
        Me.picIK4 = New Guna.UI2.WinForms.Guna2PictureBox()
        Me.lblIK4S = New System.Windows.Forms.Label()
        Me.lblIK4V = New System.Windows.Forms.Label()
        Me.lblIK4T = New System.Windows.Forms.Label()
        Me.btnInvExport = New Guna.UI2.WinForms.Guna2Button()
        Me.lblInvSub = New System.Windows.Forms.Label()
        Me.lblInvTitle = New System.Windows.Forms.Label()
        Me.pnl_History = New System.Windows.Forms.Panel()
        Me.pnlHistDetail = New Guna.UI2.WinForms.Guna2Panel()
        Me.lblDetEmpty = New System.Windows.Forms.Label()
        Me.lblDetReceipt = New System.Windows.Forms.Label()
        Me.pnlDetTotal = New Guna.UI2.WinForms.Guna2Panel()
        Me.lblDetTotal = New System.Windows.Forms.Label()
        Me.lblDetTotalCap = New System.Windows.Forms.Label()
        Me.lblDetCash = New System.Windows.Forms.Label()
        Me.lblDetTax = New System.Windows.Forms.Label()
        Me.lblDetTaxCap = New System.Windows.Forms.Label()
        Me.lblDetSub = New System.Windows.Forms.Label()
        Me.lblDetSubCap = New System.Windows.Forms.Label()
        Me.flDetItems = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblDetSecB = New System.Windows.Forms.Label()
        Me.lblDetPay = New System.Windows.Forms.Label()
        Me.lblDetPayCap = New System.Windows.Forms.Label()
        Me.lblDetCashier = New System.Windows.Forms.Label()
        Me.lblDetCashierCap = New System.Windows.Forms.Label()
        Me.lblDetSecA = New System.Windows.Forms.Label()
        Me.btnDetPrint = New Guna.UI2.WinForms.Guna2Button()
        Me.lblDetDate = New System.Windows.Forms.Label()
        Me.btnDetStatus = New Guna.UI2.WinForms.Guna2Button()
        Me.lblDetTitle = New System.Windows.Forms.Label()
        Me.Guna2Panel15 = New Guna.UI2.WinForms.Guna2Panel()
        Me.lblHistShowing = New System.Windows.Forms.Label()
        Me.histGrid = New System.Windows.Forms.DataGridView()
        Me.lblHistCount = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Guna2ComboBox2 = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.Guna2ComboBox1 = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.dtpHistory = New Guna.UI2.WinForms.Guna2DateTimePicker()
        Me.Guna2TextBox1 = New Guna.UI2.WinForms.Guna2TextBox()
        Me.pnlHK1 = New Guna.UI2.WinForms.Guna2Panel()
        Me.picHK1 = New Guna.UI2.WinForms.Guna2PictureBox()
        Me.lblHK1S = New System.Windows.Forms.Label()
        Me.lblHK1V = New System.Windows.Forms.Label()
        Me.lblHK1T = New System.Windows.Forms.Label()
        Me.pnlHK2 = New Guna.UI2.WinForms.Guna2Panel()
        Me.picHK2 = New Guna.UI2.WinForms.Guna2PictureBox()
        Me.lblHK2S = New System.Windows.Forms.Label()
        Me.lblHK2V = New System.Windows.Forms.Label()
        Me.lblHK2T = New System.Windows.Forms.Label()
        Me.pnlHK3 = New Guna.UI2.WinForms.Guna2Panel()
        Me.picHK3 = New Guna.UI2.WinForms.Guna2PictureBox()
        Me.lblHK3S = New System.Windows.Forms.Label()
        Me.lblHK3V = New System.Windows.Forms.Label()
        Me.lblHK3T = New System.Windows.Forms.Label()
        Me.pnlHK4 = New Guna.UI2.WinForms.Guna2Panel()
        Me.picHK4 = New Guna.UI2.WinForms.Guna2PictureBox()
        Me.lblHK4S = New System.Windows.Forms.Label()
        Me.lblHK4V = New System.Windows.Forms.Label()
        Me.lblHK4T = New System.Windows.Forms.Label()
        Me.btnHistNewOrder = New Guna.UI2.WinForms.Guna2Button()
        Me.btnHistExport = New Guna.UI2.WinForms.Guna2Button()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pnlCardDim = New System.Windows.Forms.Panel()
        Me.pnlCardModal = New Guna.UI2.WinForms.Guna2Panel()
        Me.btnCardConfirm = New Guna.UI2.WinForms.Guna2Button()
        Me.btnCardAmex = New Guna.UI2.WinForms.Guna2Button()
        Me.btnCardJcb = New Guna.UI2.WinForms.Guna2Button()
        Me.btnCardMastercard = New Guna.UI2.WinForms.Guna2Button()
        Me.btnCardVisa = New Guna.UI2.WinForms.Guna2Button()
        Me.btnCardCredit = New Guna.UI2.WinForms.Guna2Button()
        Me.btnCardDebit = New Guna.UI2.WinForms.Guna2Button()
        Me.pnlCardAmount = New Guna.UI2.WinForms.Guna2Panel()
        Me.lblCardAmt = New System.Windows.Forms.Label()
        Me.lblCardAmtCap = New System.Windows.Forms.Label()
        Me.btnCardClose = New Guna.UI2.WinForms.Guna2Button()
        Me.lblCardModalSub = New System.Windows.Forms.Label()
        Me.lblCardModalTitle = New System.Windows.Forms.Label()
        Me.pnl_PointOfSale = New Guna.UI2.WinForms.Guna2Panel()
        Me.pnlProfileMenu = New Guna.UI2.WinForms.Guna2Panel()
        Me.btnMenuLogout = New Guna.UI2.WinForms.Guna2Button()
        Me.btnMenuProfile = New Guna.UI2.WinForms.Guna2Button()
        Me.btnMenuSettings = New Guna.UI2.WinForms.Guna2Button()
        Me.topimage = New Guna.UI2.WinForms.Guna2PictureBox()
        Me.Guna2Panel5 = New Guna.UI2.WinForms.Guna2Panel()
        Me.btn_chkout = New Guna.UI2.WinForms.Guna2Button()
        Me.txt_Cash_Receive = New Guna.UI2.WinForms.Guna2TextBox()
        Me.lbl_Change = New System.Windows.Forms.Label()
        Me.Guna2HtmlLabel24 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.tileCard = New Guna.UI2.WinForms.Guna2Button()
        Me.tileCash = New Guna.UI2.WinForms.Guna2Button()
        Me.lblPayCap = New System.Windows.Forms.Label()
        Me.pnlTotal = New Guna.UI2.WinForms.Guna2Panel()
        Me.lbl_Total = New System.Windows.Forms.Label()
        Me.lblTotalSub = New System.Windows.Forms.Label()
        Me.Guna2HtmlLabel19 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.lbl_Tax = New System.Windows.Forms.Label()
        Me.Guna2HtmlLabel18 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.lbl_Subtotal = New System.Windows.Forms.Label()
        Me.Guna2HtmlLabel17 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.Guna2Panel8 = New Guna.UI2.WinForms.Guna2Panel()
        Me.txt_SearchMenu = New Guna.UI2.WinForms.Guna2TextBox()
        Me.btnBell = New Guna.UI2.WinForms.Guna2Button()
        Me.pnlUserChip = New Guna.UI2.WinForms.Guna2Panel()
        Me.lblChevron = New System.Windows.Forms.Label()
        Me.lblUserRole = New System.Windows.Forms.Label()
        Me.lblUserName = New System.Windows.Forms.Label()
        Me.picAvatar = New Guna.UI2.WinForms.Guna2PictureBox()
        Me.Guna2Panel6 = New Guna.UI2.WinForms.Guna2Panel()
        Me.lblEmpty = New System.Windows.Forms.Label()
        Me.btn_Clear = New Guna.UI2.WinForms.Guna2Button()
        Me.lblCartCount = New System.Windows.Forms.Label()
        Me.lblCurrentOrder = New System.Windows.Forms.Label()
        Me.fl_MenuProduct = New System.Windows.Forms.FlowLayoutPanel()
        Me.pnlCartSep = New Guna.UI2.WinForms.Guna2Panel()
        Me.txtOrderNo = New Guna.UI2.WinForms.Guna2TextBox()
        Me.lblOrderCap = New System.Windows.Forms.Label()
        Me.cboTable = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.lblTableCap = New System.Windows.Forms.Label()
        Me.txtCustomer = New Guna.UI2.WinForms.Guna2TextBox()
        Me.lblCustNameCap = New System.Windows.Forms.Label()
        Me.lblCustInfo = New System.Windows.Forms.Label()
        Me.Guna2HtmlLabel15 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.picCartIcon = New Guna.UI2.WinForms.Guna2PictureBox()
        Me.btn_Non_Coffee = New Guna.UI2.WinForms.Guna2Button()
        Me.btn_Specialty = New Guna.UI2.WinForms.Guna2Button()
        Me.btn_IcedCoffee = New Guna.UI2.WinForms.Guna2Button()
        Me.btn_hotCoffee = New Guna.UI2.WinForms.Guna2Button()
        Me.btn_All = New Guna.UI2.WinForms.Guna2Button()
        Me.lblAvailable = New System.Windows.Forms.Label()
        Me.lblMenuTitle = New System.Windows.Forms.Label()
        Me.lblSeeAll = New System.Windows.Forms.Label()
        Me.lblChoose = New System.Windows.Forms.Label()
        Me.fl_Menu = New System.Windows.Forms.FlowLayoutPanel()
        Me.pnl_CashierMessages = New System.Windows.Forms.Panel()
        Me.FlowLayoutPanel3 = New System.Windows.Forms.FlowLayoutPanel()
        Me.pnl_MainChat = New System.Windows.Forms.Panel()
        Me.Guna2HtmlLabel60 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.flpMessages = New System.Windows.Forms.FlowLayoutPanel()
        Me.flpMessagesdsds = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.lbltimerSender = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.btnSend = New Guna.UI2.WinForms.Guna2Button()
        Me.CashierName = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.txtChat = New Guna.UI2.WinForms.Guna2TextBox()
        Me.dashbrd_pnl = New Guna.UI2.WinForms.Guna2Panel()
        Me.cboDashPeriod = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.pnlDashRecent = New Guna.UI2.WinForms.Guna2Panel()
        Me.dashGrid = New System.Windows.Forms.DataGridView()
        Me.lnkDashViewAll = New System.Windows.Forms.Label()
        Me.lblDashRecentTitle = New System.Windows.Forms.Label()
        Me.pnlDashAlert = New Guna.UI2.WinForms.Guna2Panel()
        Me.flDashAlert = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblDashAlertNote = New System.Windows.Forms.Label()
        Me.lblDashAlertTitle = New System.Windows.Forms.Label()
        Me.pnlDashTop = New Guna.UI2.WinForms.Guna2Panel()
        Me.flDashTop = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblDashTopNote = New System.Windows.Forms.Label()
        Me.lblDashTopTitle = New System.Windows.Forms.Label()
        Me.pnlDashCat = New Guna.UI2.WinForms.Guna2Panel()
        Me.flDashCat = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblDashCatNote = New System.Windows.Forms.Label()
        Me.lblDashCatTitle = New System.Windows.Forms.Label()
        Me.pnlDashTrend = New Guna.UI2.WinForms.Guna2Panel()
        Me.picDashTrend = New System.Windows.Forms.PictureBox()
        Me.lblDashLegToday = New System.Windows.Forms.Label()
        Me.lblDashLegYest = New System.Windows.Forms.Label()
        Me.lblDashTrendSub = New System.Windows.Forms.Label()
        Me.lblDashTrendTitle = New System.Windows.Forms.Label()
        Me.pnlDK1 = New Guna.UI2.WinForms.Guna2Panel()
        Me.picDK1 = New Guna.UI2.WinForms.Guna2PictureBox()
        Me.lblDK1S = New System.Windows.Forms.Label()
        Me.lblDK1V = New System.Windows.Forms.Label()
        Me.lblDK1T = New System.Windows.Forms.Label()
        Me.pnlDK2 = New Guna.UI2.WinForms.Guna2Panel()
        Me.picDK2 = New Guna.UI2.WinForms.Guna2PictureBox()
        Me.lblDK2S = New System.Windows.Forms.Label()
        Me.lblDK2V = New System.Windows.Forms.Label()
        Me.lblDK2T = New System.Windows.Forms.Label()
        Me.pnlDK3 = New Guna.UI2.WinForms.Guna2Panel()
        Me.picDK3 = New Guna.UI2.WinForms.Guna2PictureBox()
        Me.lblDK3S = New System.Windows.Forms.Label()
        Me.lblDK3V = New System.Windows.Forms.Label()
        Me.lblDK3T = New System.Windows.Forms.Label()
        Me.pnlDK4 = New Guna.UI2.WinForms.Guna2Panel()
        Me.picDK4 = New Guna.UI2.WinForms.Guna2PictureBox()
        Me.lblDK4S = New System.Windows.Forms.Label()
        Me.lblDK4V = New System.Windows.Forms.Label()
        Me.lblDK4T = New System.Windows.Forms.Label()
        Me.btnDashExport = New Guna.UI2.WinForms.Guna2Button()
        Me.lblDashSub = New System.Windows.Forms.Label()
        Me.lblDashGreeting = New System.Windows.Forms.Label()
        Me.dashboard_lbl = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.dshbrd_Pnl.SuspendLayout()
        Me.pnlRegister.SuspendLayout()
        Me.main_pnl.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlInvAttn.SuspendLayout()
        Me.pnlInvCat.SuspendLayout()
        CType(Me.picInvCatBar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlInvTable.SuspendLayout()
        CType(Me.invGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlIK1.SuspendLayout()
        CType(Me.picIK1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlIK2.SuspendLayout()
        CType(Me.picIK2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlIK3.SuspendLayout()
        CType(Me.picIK3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlIK4.SuspendLayout()
        CType(Me.picIK4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_History.SuspendLayout()
        Me.pnlHistDetail.SuspendLayout()
        Me.pnlDetTotal.SuspendLayout()
        Me.Guna2Panel15.SuspendLayout()
        CType(Me.histGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHK1.SuspendLayout()
        CType(Me.picHK1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHK2.SuspendLayout()
        CType(Me.picHK2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHK3.SuspendLayout()
        CType(Me.picHK3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHK4.SuspendLayout()
        CType(Me.picHK4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCardDim.SuspendLayout()
        Me.pnlCardModal.SuspendLayout()
        Me.pnlCardAmount.SuspendLayout()
        Me.pnl_PointOfSale.SuspendLayout()
        Me.pnlProfileMenu.SuspendLayout()
        CType(Me.topimage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Guna2Panel5.SuspendLayout()
        Me.pnlTotal.SuspendLayout()
        Me.pnlUserChip.SuspendLayout()
        CType(Me.picAvatar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Guna2Panel6.SuspendLayout()
        CType(Me.picCartIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_CashierMessages.SuspendLayout()
        Me.pnl_MainChat.SuspendLayout()
        Me.flpMessages.SuspendLayout()
        Me.dashbrd_pnl.SuspendLayout()
        Me.pnlDashRecent.SuspendLayout()
        CType(Me.dashGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDashAlert.SuspendLayout()
        Me.pnlDashTop.SuspendLayout()
        Me.pnlDashCat.SuspendLayout()
        Me.pnlDashTrend.SuspendLayout()
        CType(Me.picDashTrend, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDK1.SuspendLayout()
        CType(Me.picDK1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDK2.SuspendLayout()
        CType(Me.picDK2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDK3.SuspendLayout()
        CType(Me.picDK3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDK4.SuspendLayout()
        CType(Me.picDK4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dshbrd_Pnl
        '
        Me.dshbrd_Pnl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dshbrd_Pnl.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(23, Byte), Integer))
        Me.dshbrd_Pnl.Controls.Add(Me.lblManagement)
        Me.dshbrd_Pnl.Controls.Add(Me.btnCashierLogout)
        Me.dshbrd_Pnl.Controls.Add(Me.pnlRegister)
        Me.dshbrd_Pnl.Controls.Add(Me.btn_CashierMessages)
        Me.dshbrd_Pnl.Controls.Add(Me.btn_hstry)
        Me.dshbrd_Pnl.Controls.Add(Me.btn_invtry)
        Me.dshbrd_Pnl.Controls.Add(Me.btn_Point_Of_Sale)
        Me.dshbrd_Pnl.Controls.Add(Me.btn_DashBoard)
        Me.dshbrd_Pnl.Controls.Add(Me.lblWorkspace)
        Me.dshbrd_Pnl.Controls.Add(Me.Guna2HtmlLabel5)
        Me.dshbrd_Pnl.Controls.Add(Me.Guna2HtmlLabel7)
        Me.dshbrd_Pnl.Location = New System.Drawing.Point(0, 0)
        Me.dshbrd_Pnl.Name = "dshbrd_Pnl"
        Me.dshbrd_Pnl.Size = New System.Drawing.Size(199, 844)
        Me.dshbrd_Pnl.TabIndex = 0
        '
        'lblManagement
        '
        Me.lblManagement.AutoSize = True
        Me.lblManagement.BackColor = System.Drawing.Color.Transparent
        Me.lblManagement.Font = New System.Drawing.Font("Segoe UI", 7.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblManagement.ForeColor = System.Drawing.Color.FromArgb(CType(CType(150, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(112, Byte), Integer))
        Me.lblManagement.Location = New System.Drawing.Point(16, 318)
        Me.lblManagement.Name = "lblManagement"
        Me.lblManagement.Size = New System.Drawing.Size(78, 12)
        Me.lblManagement.TabIndex = 1701
        Me.lblManagement.Text = "MANAGEMENT"
        '
        'btnCashierLogout
        '
        Me.btnCashierLogout.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCashierLogout.Animated = True
        Me.btnCashierLogout.BorderRadius = 10
        Me.btnCashierLogout.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCashierLogout.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnCashierLogout.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnCashierLogout.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnCashierLogout.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnCashierLogout.FillColor = System.Drawing.Color.Transparent
        Me.btnCashierLogout.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCashierLogout.ForeColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.btnCashierLogout.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.btnCashierLogout.HoverState.ForeColor = System.Drawing.Color.White
        Me.btnCashierLogout.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnCashierLogout.ImageOffset = New System.Drawing.Point(6, 0)
        Me.btnCashierLogout.ImageSize = New System.Drawing.Size(22, 22)
        Me.btnCashierLogout.Location = New System.Drawing.Point(10, 778)
        Me.btnCashierLogout.Name = "btnCashierLogout"
        Me.btnCashierLogout.Size = New System.Drawing.Size(179, 42)
        Me.btnCashierLogout.TabIndex = 18
        Me.btnCashierLogout.Text = "Log out"
        Me.btnCashierLogout.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnCashierLogout.TextOffset = New System.Drawing.Point(8, 0)
        '
        'pnlRegister
        '
        Me.pnlRegister.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pnlRegister.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(23, Byte), Integer))
        Me.pnlRegister.BorderRadius = 12
        Me.pnlRegister.Controls.Add(Me.lblRegShift)
        Me.pnlRegister.Controls.Add(Me.lblRegName)
        Me.pnlRegister.Controls.Add(Me.lblRegOpen)
        Me.pnlRegister.FillColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(31, Byte), Integer))
        Me.pnlRegister.Location = New System.Drawing.Point(10, 678)
        Me.pnlRegister.Name = "pnlRegister"
        Me.pnlRegister.Size = New System.Drawing.Size(179, 82)
        Me.pnlRegister.TabIndex = 903
        '
        'lblRegShift
        '
        Me.lblRegShift.AutoSize = True
        Me.lblRegShift.BackColor = System.Drawing.Color.Transparent
        Me.lblRegShift.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRegShift.ForeColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.lblRegShift.Location = New System.Drawing.Point(12, 50)
        Me.lblRegShift.Name = "lblRegShift"
        Me.lblRegShift.Size = New System.Drawing.Size(93, 13)
        Me.lblRegShift.TabIndex = 906
        Me.lblRegShift.Text = "Cashier station 1"
        '
        'lblRegName
        '
        Me.lblRegName.AutoSize = True
        Me.lblRegName.BackColor = System.Drawing.Color.Transparent
        Me.lblRegName.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRegName.ForeColor = System.Drawing.Color.White
        Me.lblRegName.Location = New System.Drawing.Point(12, 28)
        Me.lblRegName.Name = "lblRegName"
        Me.lblRegName.Size = New System.Drawing.Size(58, 19)
        Me.lblRegName.TabIndex = 905
        Me.lblRegName.Text = "Cashier"
        '
        'lblRegOpen
        '
        Me.lblRegOpen.AutoSize = True
        Me.lblRegOpen.BackColor = System.Drawing.Color.Transparent
        Me.lblRegOpen.Font = New System.Drawing.Font("Segoe UI", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRegOpen.ForeColor = System.Drawing.Color.FromArgb(CType(CType(150, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(140, Byte), Integer))
        Me.lblRegOpen.Location = New System.Drawing.Point(12, 10)
        Me.lblRegOpen.Name = "lblRegOpen"
        Me.lblRegOpen.Size = New System.Drawing.Size(90, 12)
        Me.lblRegOpen.TabIndex = 904
        Me.lblRegOpen.Text = "● REGISTER OPEN"
        '
        'btn_CashierMessages
        '
        Me.btn_CashierMessages.Animated = True
        Me.btn_CashierMessages.BorderRadius = 10
        Me.btn_CashierMessages.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_CashierMessages.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btn_CashierMessages.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btn_CashierMessages.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btn_CashierMessages.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btn_CashierMessages.FillColor = System.Drawing.Color.Transparent
        Me.btn_CashierMessages.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_CashierMessages.ForeColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.btn_CashierMessages.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.btn_CashierMessages.HoverState.ForeColor = System.Drawing.Color.White
        Me.btn_CashierMessages.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btn_CashierMessages.ImageOffset = New System.Drawing.Point(6, 0)
        Me.btn_CashierMessages.ImageSize = New System.Drawing.Size(22, 22)
        Me.btn_CashierMessages.Location = New System.Drawing.Point(10, 206)
        Me.btn_CashierMessages.Name = "btn_CashierMessages"
        Me.btn_CashierMessages.Size = New System.Drawing.Size(179, 42)
        Me.btn_CashierMessages.TabIndex = 16
        Me.btn_CashierMessages.Text = "Messages"
        Me.btn_CashierMessages.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btn_CashierMessages.TextOffset = New System.Drawing.Point(8, 0)
        '
        'btn_hstry
        '
        Me.btn_hstry.Animated = True
        Me.btn_hstry.BorderRadius = 10
        Me.btn_hstry.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_hstry.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btn_hstry.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btn_hstry.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btn_hstry.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btn_hstry.FillColor = System.Drawing.Color.Transparent
        Me.btn_hstry.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_hstry.ForeColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.btn_hstry.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.btn_hstry.HoverState.ForeColor = System.Drawing.Color.White
        Me.btn_hstry.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btn_hstry.ImageOffset = New System.Drawing.Point(6, 0)
        Me.btn_hstry.ImageSize = New System.Drawing.Size(22, 22)
        Me.btn_hstry.Location = New System.Drawing.Point(10, 252)
        Me.btn_hstry.Name = "btn_hstry"
        Me.btn_hstry.Size = New System.Drawing.Size(179, 42)
        Me.btn_hstry.TabIndex = 11
        Me.btn_hstry.Text = "History"
        Me.btn_hstry.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btn_hstry.TextOffset = New System.Drawing.Point(8, 0)
        '
        'btn_invtry
        '
        Me.btn_invtry.Animated = True
        Me.btn_invtry.BorderRadius = 10
        Me.btn_invtry.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_invtry.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btn_invtry.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btn_invtry.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btn_invtry.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btn_invtry.FillColor = System.Drawing.Color.Transparent
        Me.btn_invtry.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_invtry.ForeColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.btn_invtry.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.btn_invtry.HoverState.ForeColor = System.Drawing.Color.White
        Me.btn_invtry.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btn_invtry.ImageOffset = New System.Drawing.Point(6, 0)
        Me.btn_invtry.ImageSize = New System.Drawing.Size(22, 22)
        Me.btn_invtry.Location = New System.Drawing.Point(10, 340)
        Me.btn_invtry.Name = "btn_invtry"
        Me.btn_invtry.Size = New System.Drawing.Size(179, 42)
        Me.btn_invtry.TabIndex = 4
        Me.btn_invtry.Text = "Inventory"
        Me.btn_invtry.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btn_invtry.TextOffset = New System.Drawing.Point(8, 0)
        '
        'btn_Point_Of_Sale
        '
        Me.btn_Point_Of_Sale.Animated = True
        Me.btn_Point_Of_Sale.BorderRadius = 10
        Me.btn_Point_Of_Sale.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_Point_Of_Sale.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btn_Point_Of_Sale.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btn_Point_Of_Sale.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btn_Point_Of_Sale.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btn_Point_Of_Sale.FillColor = System.Drawing.Color.Transparent
        Me.btn_Point_Of_Sale.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Point_Of_Sale.ForeColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.btn_Point_Of_Sale.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.btn_Point_Of_Sale.HoverState.ForeColor = System.Drawing.Color.White
        Me.btn_Point_Of_Sale.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btn_Point_Of_Sale.ImageOffset = New System.Drawing.Point(6, 0)
        Me.btn_Point_Of_Sale.ImageSize = New System.Drawing.Size(22, 22)
        Me.btn_Point_Of_Sale.Location = New System.Drawing.Point(10, 160)
        Me.btn_Point_Of_Sale.Name = "btn_Point_Of_Sale"
        Me.btn_Point_Of_Sale.Size = New System.Drawing.Size(179, 42)
        Me.btn_Point_Of_Sale.TabIndex = 2
        Me.btn_Point_Of_Sale.Text = "Menu"
        Me.btn_Point_Of_Sale.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btn_Point_Of_Sale.TextOffset = New System.Drawing.Point(8, 0)
        '
        'btn_DashBoard
        '
        Me.btn_DashBoard.Animated = True
        Me.btn_DashBoard.BorderRadius = 10
        Me.btn_DashBoard.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_DashBoard.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btn_DashBoard.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btn_DashBoard.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btn_DashBoard.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btn_DashBoard.FillColor = System.Drawing.Color.Transparent
        Me.btn_DashBoard.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_DashBoard.ForeColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.btn_DashBoard.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.btn_DashBoard.HoverState.ForeColor = System.Drawing.Color.White
        Me.btn_DashBoard.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btn_DashBoard.ImageOffset = New System.Drawing.Point(6, 0)
        Me.btn_DashBoard.ImageSize = New System.Drawing.Size(22, 22)
        Me.btn_DashBoard.Location = New System.Drawing.Point(10, 114)
        Me.btn_DashBoard.Name = "btn_DashBoard"
        Me.btn_DashBoard.Size = New System.Drawing.Size(179, 42)
        Me.btn_DashBoard.TabIndex = 1
        Me.btn_DashBoard.Text = "Dashboard"
        Me.btn_DashBoard.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btn_DashBoard.TextOffset = New System.Drawing.Point(8, 0)
        '
        'lblWorkspace
        '
        Me.lblWorkspace.AutoSize = True
        Me.lblWorkspace.BackColor = System.Drawing.Color.Transparent
        Me.lblWorkspace.Font = New System.Drawing.Font("Segoe UI", 7.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWorkspace.ForeColor = System.Drawing.Color.FromArgb(CType(CType(150, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(112, Byte), Integer))
        Me.lblWorkspace.Location = New System.Drawing.Point(16, 92)
        Me.lblWorkspace.Name = "lblWorkspace"
        Me.lblWorkspace.Size = New System.Drawing.Size(65, 12)
        Me.lblWorkspace.TabIndex = 902
        Me.lblWorkspace.Text = "WORKSPACE"
        '
        'Guna2HtmlLabel5
        '
        Me.Guna2HtmlLabel5.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel5.Font = New System.Drawing.Font("Segoe UI", 11.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel5.ForeColor = System.Drawing.Color.White
        Me.Guna2HtmlLabel5.Location = New System.Drawing.Point(56, 13)
        Me.Guna2HtmlLabel5.Name = "Guna2HtmlLabel5"
        Me.Guna2HtmlLabel5.Size = New System.Drawing.Size(91, 22)
        Me.Guna2HtmlLabel5.TabIndex = 10
        Me.Guna2HtmlLabel5.Text = "Forest Roast"
        '
        'Guna2HtmlLabel7
        '
        Me.Guna2HtmlLabel7.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel7.Font = New System.Drawing.Font("Segoe UI", 7.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.Guna2HtmlLabel7.Location = New System.Drawing.Point(56, 34)
        Me.Guna2HtmlLabel7.Name = "Guna2HtmlLabel7"
        Me.Guna2HtmlLabel7.Size = New System.Drawing.Size(49, 14)
        Me.Guna2HtmlLabel7.TabIndex = 12
        Me.Guna2HtmlLabel7.Text = "CAFE POS"
        '
        'main_pnl
        '
        Me.main_pnl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.main_pnl.BackColor = System.Drawing.Color.WhiteSmoke
        Me.main_pnl.BorderColor = System.Drawing.Color.Black
        Me.main_pnl.Controls.Add(Me.Panel1)
        Me.main_pnl.Controls.Add(Me.pnl_History)
        Me.main_pnl.Controls.Add(Me.pnlCardDim)
        Me.main_pnl.Controls.Add(Me.pnl_PointOfSale)
        Me.main_pnl.Controls.Add(Me.pnl_CashierMessages)
        Me.main_pnl.Controls.Add(Me.dashbrd_pnl)
        Me.main_pnl.Location = New System.Drawing.Point(199, 0)
        Me.main_pnl.Name = "main_pnl"
        Me.main_pnl.Size = New System.Drawing.Size(1241, 844)
        Me.main_pnl.TabIndex = 4
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Panel1.Controls.Add(Me.pnlInvAttn)
        Me.Panel1.Controls.Add(Me.pnlInvCat)
        Me.Panel1.Controls.Add(Me.pnlInvTable)
        Me.Panel1.Controls.Add(Me.cboInvStatus)
        Me.Panel1.Controls.Add(Me.cboInvCategory)
        Me.Panel1.Controls.Add(Me.txtInvSearch)
        Me.Panel1.Controls.Add(Me.pnlIK1)
        Me.Panel1.Controls.Add(Me.pnlIK2)
        Me.Panel1.Controls.Add(Me.pnlIK3)
        Me.Panel1.Controls.Add(Me.pnlIK4)
        Me.Panel1.Controls.Add(Me.btnInvExport)
        Me.Panel1.Controls.Add(Me.lblInvSub)
        Me.Panel1.Controls.Add(Me.lblInvTitle)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1241, 844)
        Me.Panel1.TabIndex = 22
        Me.Panel1.Visible = False
        '
        'pnlInvAttn
        '
        Me.pnlInvAttn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlInvAttn.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.pnlInvAttn.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.pnlInvAttn.BorderRadius = 14
        Me.pnlInvAttn.BorderThickness = 1
        Me.pnlInvAttn.Controls.Add(Me.flInvAttn)
        Me.pnlInvAttn.Controls.Add(Me.lblInvAttnCount)
        Me.pnlInvAttn.Controls.Add(Me.lblInvAttnTitle)
        Me.pnlInvAttn.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlInvAttn.Location = New System.Drawing.Point(934, 436)
        Me.pnlInvAttn.Name = "pnlInvAttn"
        Me.pnlInvAttn.Size = New System.Drawing.Size(293, 397)
        Me.pnlInvAttn.TabIndex = 1185
        '
        'flInvAttn
        '
        Me.flInvAttn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flInvAttn.AutoScroll = True
        Me.flInvAttn.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.flInvAttn.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.flInvAttn.Location = New System.Drawing.Point(16, 44)
        Me.flInvAttn.Name = "flInvAttn"
        Me.flInvAttn.Size = New System.Drawing.Size(265, 344)
        Me.flInvAttn.TabIndex = 1188
        Me.flInvAttn.WrapContents = False
        '
        'lblInvAttnCount
        '
        Me.lblInvAttnCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblInvAttnCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.lblInvAttnCount.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvAttnCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(150, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.lblInvAttnCount.Location = New System.Drawing.Point(206, 13)
        Me.lblInvAttnCount.Name = "lblInvAttnCount"
        Me.lblInvAttnCount.Size = New System.Drawing.Size(71, 20)
        Me.lblInvAttnCount.TabIndex = 1187
        Me.lblInvAttnCount.Text = "0 items"
        Me.lblInvAttnCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblInvAttnTitle
        '
        Me.lblInvAttnTitle.AutoSize = True
        Me.lblInvAttnTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblInvAttnTitle.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvAttnTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblInvAttnTitle.Location = New System.Drawing.Point(16, 14)
        Me.lblInvAttnTitle.Name = "lblInvAttnTitle"
        Me.lblInvAttnTitle.Size = New System.Drawing.Size(115, 19)
        Me.lblInvAttnTitle.TabIndex = 1186
        Me.lblInvAttnTitle.Text = "Needs attention"
        '
        'pnlInvCat
        '
        Me.pnlInvCat.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlInvCat.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.pnlInvCat.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.pnlInvCat.BorderRadius = 14
        Me.pnlInvCat.BorderThickness = 1
        Me.pnlInvCat.Controls.Add(Me.flInvCat)
        Me.pnlInvCat.Controls.Add(Me.picInvCatBar)
        Me.pnlInvCat.Controls.Add(Me.lblInvCatTotal)
        Me.pnlInvCat.Controls.Add(Me.lblInvCatTitle)
        Me.pnlInvCat.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlInvCat.Location = New System.Drawing.Point(934, 226)
        Me.pnlInvCat.Name = "pnlInvCat"
        Me.pnlInvCat.Size = New System.Drawing.Size(293, 200)
        Me.pnlInvCat.TabIndex = 1180
        '
        'flInvCat
        '
        Me.flInvCat.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flInvCat.AutoScroll = True
        Me.flInvCat.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.flInvCat.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.flInvCat.Location = New System.Drawing.Point(16, 66)
        Me.flInvCat.Name = "flInvCat"
        Me.flInvCat.Size = New System.Drawing.Size(265, 126)
        Me.flInvCat.TabIndex = 1184
        Me.flInvCat.WrapContents = False
        '
        'picInvCatBar
        '
        Me.picInvCatBar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picInvCatBar.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.picInvCatBar.Location = New System.Drawing.Point(16, 46)
        Me.picInvCatBar.Name = "picInvCatBar"
        Me.picInvCatBar.Size = New System.Drawing.Size(261, 10)
        Me.picInvCatBar.TabIndex = 1183
        Me.picInvCatBar.TabStop = False
        '
        'lblInvCatTotal
        '
        Me.lblInvCatTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblInvCatTotal.BackColor = System.Drawing.Color.Transparent
        Me.lblInvCatTotal.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvCatTotal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblInvCatTotal.Location = New System.Drawing.Point(150, 18)
        Me.lblInvCatTotal.Name = "lblInvCatTotal"
        Me.lblInvCatTotal.Size = New System.Drawing.Size(127, 16)
        Me.lblInvCatTotal.TabIndex = 1182
        Me.lblInvCatTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblInvCatTitle
        '
        Me.lblInvCatTitle.AutoSize = True
        Me.lblInvCatTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblInvCatTitle.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvCatTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblInvCatTitle.Location = New System.Drawing.Point(16, 14)
        Me.lblInvCatTitle.Name = "lblInvCatTitle"
        Me.lblInvCatTitle.Size = New System.Drawing.Size(132, 19)
        Me.lblInvCatTitle.TabIndex = 1181
        Me.lblInvCatTitle.Text = "Stock by category"
        '
        'pnlInvTable
        '
        Me.pnlInvTable.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlInvTable.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.pnlInvTable.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.pnlInvTable.BorderRadius = 14
        Me.pnlInvTable.BorderThickness = 1
        Me.pnlInvTable.Controls.Add(Me.invGrid)
        Me.pnlInvTable.Controls.Add(Me.lblInvShowing)
        Me.pnlInvTable.Controls.Add(Me.lblInvCount)
        Me.pnlInvTable.Controls.Add(Me.lblInvTableTitle)
        Me.pnlInvTable.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlInvTable.Location = New System.Drawing.Point(20, 226)
        Me.pnlInvTable.Name = "pnlInvTable"
        Me.pnlInvTable.Size = New System.Drawing.Size(906, 607)
        Me.pnlInvTable.TabIndex = 1175
        '
        'invGrid
        '
        Me.invGrid.AllowUserToAddRows = False
        Me.invGrid.AllowUserToDeleteRows = False
        Me.invGrid.AllowUserToResizeRows = False
        Me.invGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.invGrid.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.invGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.invGrid.Location = New System.Drawing.Point(7, 46)
        Me.invGrid.Name = "invGrid"
        Me.invGrid.ReadOnly = True
        Me.invGrid.RowHeadersVisible = False
        Me.invGrid.Size = New System.Drawing.Size(888, 552)
        Me.invGrid.TabIndex = 1178
        '
        'lblInvShowing
        '
        Me.lblInvShowing.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblInvShowing.AutoSize = True
        Me.lblInvShowing.BackColor = System.Drawing.Color.Transparent
        Me.lblInvShowing.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvShowing.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblInvShowing.Location = New System.Drawing.Point(16, 580)
        Me.lblInvShowing.Name = "lblInvShowing"
        Me.lblInvShowing.Size = New System.Drawing.Size(0, 15)
        Me.lblInvShowing.TabIndex = 1179
        '
        'lblInvCount
        '
        Me.lblInvCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblInvCount.BackColor = System.Drawing.Color.Transparent
        Me.lblInvCount.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblInvCount.Location = New System.Drawing.Point(733, 18)
        Me.lblInvCount.Name = "lblInvCount"
        Me.lblInvCount.Size = New System.Drawing.Size(157, 18)
        Me.lblInvCount.TabIndex = 1177
        Me.lblInvCount.Text = "0 items"
        Me.lblInvCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblInvTableTitle
        '
        Me.lblInvTableTitle.AutoSize = True
        Me.lblInvTableTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblInvTableTitle.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvTableTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblInvTableTitle.Location = New System.Drawing.Point(16, 14)
        Me.lblInvTableTitle.Name = "lblInvTableTitle"
        Me.lblInvTableTitle.Size = New System.Drawing.Size(100, 20)
        Me.lblInvTableTitle.TabIndex = 1176
        Me.lblInvTableTitle.Text = "All inventory"
        '
        'cboInvStatus
        '
        Me.cboInvStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboInvStatus.BackColor = System.Drawing.Color.Transparent
        Me.cboInvStatus.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.cboInvStatus.BorderRadius = 10
        Me.cboInvStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cboInvStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboInvStatus.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.cboInvStatus.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.cboInvStatus.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.cboInvStatus.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboInvStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.cboInvStatus.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.cboInvStatus.ItemHeight = 30
        Me.cboInvStatus.Location = New System.Drawing.Point(1050, 176)
        Me.cboInvStatus.Name = "cboInvStatus"
        Me.cboInvStatus.Size = New System.Drawing.Size(170, 36)
        Me.cboInvStatus.TabIndex = 1174
        '
        'cboInvCategory
        '
        Me.cboInvCategory.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboInvCategory.BackColor = System.Drawing.Color.Transparent
        Me.cboInvCategory.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.cboInvCategory.BorderRadius = 10
        Me.cboInvCategory.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cboInvCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboInvCategory.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.cboInvCategory.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.cboInvCategory.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.cboInvCategory.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboInvCategory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.cboInvCategory.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.cboInvCategory.ItemHeight = 30
        Me.cboInvCategory.Location = New System.Drawing.Point(871, 176)
        Me.cboInvCategory.Name = "cboInvCategory"
        Me.cboInvCategory.Size = New System.Drawing.Size(170, 36)
        Me.cboInvCategory.TabIndex = 1173
        '
        'txtInvSearch
        '
        Me.txtInvSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtInvSearch.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.txtInvSearch.BorderRadius = 10
        Me.txtInvSearch.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInvSearch.DefaultText = ""
        Me.txtInvSearch.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.txtInvSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.txtInvSearch.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.txtInvSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.txtInvSearch.Location = New System.Drawing.Point(20, 176)
        Me.txtInvSearch.Name = "txtInvSearch"
        Me.txtInvSearch.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.txtInvSearch.PlaceholderText = "Search item, SKU, or category..."
        Me.txtInvSearch.SelectedText = ""
        Me.txtInvSearch.Size = New System.Drawing.Size(842, 38)
        Me.txtInvSearch.TabIndex = 1172
        Me.txtInvSearch.TextOffset = New System.Drawing.Point(6, 0)
        '
        'pnlIK1
        '
        Me.pnlIK1.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.pnlIK1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.pnlIK1.BorderRadius = 14
        Me.pnlIK1.BorderThickness = 1
        Me.pnlIK1.Controls.Add(Me.picIK1)
        Me.pnlIK1.Controls.Add(Me.lblIK1S)
        Me.pnlIK1.Controls.Add(Me.lblIK1V)
        Me.pnlIK1.Controls.Add(Me.lblIK1T)
        Me.pnlIK1.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlIK1.Location = New System.Drawing.Point(20, 78)
        Me.pnlIK1.Name = "pnlIK1"
        Me.pnlIK1.Size = New System.Drawing.Size(231, 84)
        Me.pnlIK1.TabIndex = 1152
        '
        'picIK1
        '
        Me.picIK1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picIK1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.picIK1.BorderRadius = 10
        Me.picIK1.FillColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.picIK1.ImageRotate = 0!
        Me.picIK1.Location = New System.Drawing.Point(183, 12)
        Me.picIK1.Name = "picIK1"
        Me.picIK1.Size = New System.Drawing.Size(34, 34)
        Me.picIK1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picIK1.TabIndex = 1156
        Me.picIK1.TabStop = False
        '
        'lblIK1S
        '
        Me.lblIK1S.AutoSize = True
        Me.lblIK1S.BackColor = System.Drawing.Color.Transparent
        Me.lblIK1S.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIK1S.ForeColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.lblIK1S.Location = New System.Drawing.Point(14, 62)
        Me.lblIK1S.Name = "lblIK1S"
        Me.lblIK1S.Size = New System.Drawing.Size(0, 15)
        Me.lblIK1S.TabIndex = 1155
        '
        'lblIK1V
        '
        Me.lblIK1V.AutoSize = True
        Me.lblIK1V.BackColor = System.Drawing.Color.Transparent
        Me.lblIK1V.Font = New System.Drawing.Font("Segoe UI", 19.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIK1V.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblIK1V.Location = New System.Drawing.Point(14, 32)
        Me.lblIK1V.Name = "lblIK1V"
        Me.lblIK1V.Size = New System.Drawing.Size(30, 36)
        Me.lblIK1V.TabIndex = 1154
        Me.lblIK1V.Text = "0"
        '
        'lblIK1T
        '
        Me.lblIK1T.AutoSize = True
        Me.lblIK1T.BackColor = System.Drawing.Color.Transparent
        Me.lblIK1T.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIK1T.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblIK1T.Location = New System.Drawing.Point(14, 12)
        Me.lblIK1T.Name = "lblIK1T"
        Me.lblIK1T.Size = New System.Drawing.Size(65, 15)
        Me.lblIK1T.TabIndex = 1153
        Me.lblIK1T.Text = "Total items"
        '
        'pnlIK2
        '
        Me.pnlIK2.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.pnlIK2.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.pnlIK2.BorderRadius = 14
        Me.pnlIK2.BorderThickness = 1
        Me.pnlIK2.Controls.Add(Me.picIK2)
        Me.pnlIK2.Controls.Add(Me.lblIK2S)
        Me.pnlIK2.Controls.Add(Me.lblIK2V)
        Me.pnlIK2.Controls.Add(Me.lblIK2T)
        Me.pnlIK2.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlIK2.Location = New System.Drawing.Point(265, 78)
        Me.pnlIK2.Name = "pnlIK2"
        Me.pnlIK2.Size = New System.Drawing.Size(231, 84)
        Me.pnlIK2.TabIndex = 1157
        '
        'picIK2
        '
        Me.picIK2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picIK2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.picIK2.BorderRadius = 10
        Me.picIK2.FillColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.picIK2.ImageRotate = 0!
        Me.picIK2.Location = New System.Drawing.Point(183, 12)
        Me.picIK2.Name = "picIK2"
        Me.picIK2.Size = New System.Drawing.Size(34, 34)
        Me.picIK2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picIK2.TabIndex = 1161
        Me.picIK2.TabStop = False
        '
        'lblIK2S
        '
        Me.lblIK2S.AutoSize = True
        Me.lblIK2S.BackColor = System.Drawing.Color.Transparent
        Me.lblIK2S.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIK2S.ForeColor = System.Drawing.Color.FromArgb(CType(CType(92, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(84, Byte), Integer))
        Me.lblIK2S.Location = New System.Drawing.Point(14, 62)
        Me.lblIK2S.Name = "lblIK2S"
        Me.lblIK2S.Size = New System.Drawing.Size(0, 15)
        Me.lblIK2S.TabIndex = 1160
        '
        'lblIK2V
        '
        Me.lblIK2V.AutoSize = True
        Me.lblIK2V.BackColor = System.Drawing.Color.Transparent
        Me.lblIK2V.Font = New System.Drawing.Font("Segoe UI", 19.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIK2V.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblIK2V.Location = New System.Drawing.Point(14, 32)
        Me.lblIK2V.Name = "lblIK2V"
        Me.lblIK2V.Size = New System.Drawing.Size(30, 36)
        Me.lblIK2V.TabIndex = 1159
        Me.lblIK2V.Text = "0"
        '
        'lblIK2T
        '
        Me.lblIK2T.AutoSize = True
        Me.lblIK2T.BackColor = System.Drawing.Color.Transparent
        Me.lblIK2T.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIK2T.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblIK2T.Location = New System.Drawing.Point(14, 12)
        Me.lblIK2T.Name = "lblIK2T"
        Me.lblIK2T.Size = New System.Drawing.Size(67, 15)
        Me.lblIK2T.TabIndex = 1158
        Me.lblIK2T.Text = "Stock value"
        '
        'pnlIK3
        '
        Me.pnlIK3.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.pnlIK3.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.pnlIK3.BorderRadius = 14
        Me.pnlIK3.BorderThickness = 1
        Me.pnlIK3.Controls.Add(Me.picIK3)
        Me.pnlIK3.Controls.Add(Me.lblIK3S)
        Me.pnlIK3.Controls.Add(Me.lblIK3V)
        Me.pnlIK3.Controls.Add(Me.lblIK3T)
        Me.pnlIK3.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlIK3.Location = New System.Drawing.Point(510, 78)
        Me.pnlIK3.Name = "pnlIK3"
        Me.pnlIK3.Size = New System.Drawing.Size(231, 84)
        Me.pnlIK3.TabIndex = 1162
        '
        'picIK3
        '
        Me.picIK3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picIK3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.picIK3.BorderRadius = 10
        Me.picIK3.FillColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.picIK3.ImageRotate = 0!
        Me.picIK3.Location = New System.Drawing.Point(183, 12)
        Me.picIK3.Name = "picIK3"
        Me.picIK3.Size = New System.Drawing.Size(34, 34)
        Me.picIK3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picIK3.TabIndex = 1166
        Me.picIK3.TabStop = False
        '
        'lblIK3S
        '
        Me.lblIK3S.AutoSize = True
        Me.lblIK3S.BackColor = System.Drawing.Color.Transparent
        Me.lblIK3S.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIK3S.ForeColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.lblIK3S.Location = New System.Drawing.Point(14, 62)
        Me.lblIK3S.Name = "lblIK3S"
        Me.lblIK3S.Size = New System.Drawing.Size(0, 15)
        Me.lblIK3S.TabIndex = 1165
        '
        'lblIK3V
        '
        Me.lblIK3V.AutoSize = True
        Me.lblIK3V.BackColor = System.Drawing.Color.Transparent
        Me.lblIK3V.Font = New System.Drawing.Font("Segoe UI", 19.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIK3V.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblIK3V.Location = New System.Drawing.Point(14, 32)
        Me.lblIK3V.Name = "lblIK3V"
        Me.lblIK3V.Size = New System.Drawing.Size(30, 36)
        Me.lblIK3V.TabIndex = 1164
        Me.lblIK3V.Text = "0"
        '
        'lblIK3T
        '
        Me.lblIK3T.AutoSize = True
        Me.lblIK3T.BackColor = System.Drawing.Color.Transparent
        Me.lblIK3T.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIK3T.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblIK3T.Location = New System.Drawing.Point(14, 12)
        Me.lblIK3T.Name = "lblIK3T"
        Me.lblIK3T.Size = New System.Drawing.Size(60, 15)
        Me.lblIK3T.TabIndex = 1163
        Me.lblIK3T.Text = "Low stock"
        '
        'pnlIK4
        '
        Me.pnlIK4.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.pnlIK4.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.pnlIK4.BorderRadius = 14
        Me.pnlIK4.BorderThickness = 1
        Me.pnlIK4.Controls.Add(Me.picIK4)
        Me.pnlIK4.Controls.Add(Me.lblIK4S)
        Me.pnlIK4.Controls.Add(Me.lblIK4V)
        Me.pnlIK4.Controls.Add(Me.lblIK4T)
        Me.pnlIK4.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlIK4.Location = New System.Drawing.Point(755, 78)
        Me.pnlIK4.Name = "pnlIK4"
        Me.pnlIK4.Size = New System.Drawing.Size(231, 84)
        Me.pnlIK4.TabIndex = 1167
        '
        'picIK4
        '
        Me.picIK4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picIK4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.picIK4.BorderRadius = 10
        Me.picIK4.FillColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.picIK4.ImageRotate = 0!
        Me.picIK4.Location = New System.Drawing.Point(183, 12)
        Me.picIK4.Name = "picIK4"
        Me.picIK4.Size = New System.Drawing.Size(34, 34)
        Me.picIK4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picIK4.TabIndex = 1171
        Me.picIK4.TabStop = False
        '
        'lblIK4S
        '
        Me.lblIK4S.AutoSize = True
        Me.lblIK4S.BackColor = System.Drawing.Color.Transparent
        Me.lblIK4S.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIK4S.ForeColor = System.Drawing.Color.FromArgb(CType(CType(176, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.lblIK4S.Location = New System.Drawing.Point(14, 62)
        Me.lblIK4S.Name = "lblIK4S"
        Me.lblIK4S.Size = New System.Drawing.Size(0, 15)
        Me.lblIK4S.TabIndex = 1170
        '
        'lblIK4V
        '
        Me.lblIK4V.AutoSize = True
        Me.lblIK4V.BackColor = System.Drawing.Color.Transparent
        Me.lblIK4V.Font = New System.Drawing.Font("Segoe UI", 19.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIK4V.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblIK4V.Location = New System.Drawing.Point(14, 32)
        Me.lblIK4V.Name = "lblIK4V"
        Me.lblIK4V.Size = New System.Drawing.Size(30, 36)
        Me.lblIK4V.TabIndex = 1169
        Me.lblIK4V.Text = "0"
        '
        'lblIK4T
        '
        Me.lblIK4T.AutoSize = True
        Me.lblIK4T.BackColor = System.Drawing.Color.Transparent
        Me.lblIK4T.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIK4T.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblIK4T.Location = New System.Drawing.Point(14, 12)
        Me.lblIK4T.Name = "lblIK4T"
        Me.lblIK4T.Size = New System.Drawing.Size(72, 15)
        Me.lblIK4T.TabIndex = 1168
        Me.lblIK4T.Text = "Out of stock"
        '
        'btnInvExport
        '
        Me.btnInvExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnInvExport.Animated = True
        Me.btnInvExport.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.btnInvExport.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.btnInvExport.BorderRadius = 10
        Me.btnInvExport.BorderThickness = 1
        Me.btnInvExport.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnInvExport.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnInvExport.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInvExport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btnInvExport.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.btnInvExport.HoverState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btnInvExport.Location = New System.Drawing.Point(893, 14)
        Me.btnInvExport.Name = "btnInvExport"
        Me.btnInvExport.Size = New System.Drawing.Size(100, 38)
        Me.btnInvExport.TabIndex = 1151
        Me.btnInvExport.Text = "Export CSV"
        '
        'lblInvSub
        '
        Me.lblInvSub.AutoSize = True
        Me.lblInvSub.BackColor = System.Drawing.Color.Transparent
        Me.lblInvSub.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvSub.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblInvSub.Location = New System.Drawing.Point(20, 46)
        Me.lblInvSub.Name = "lblInvSub"
        Me.lblInvSub.Size = New System.Drawing.Size(322, 15)
        Me.lblInvSub.TabIndex = 1150
        Me.lblInvSub.Text = "Live stock levels. Ask the administrator to restock low items."
        '
        'lblInvTitle
        '
        Me.lblInvTitle.AutoSize = True
        Me.lblInvTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblInvTitle.Font = New System.Drawing.Font("Segoe UI", 17.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblInvTitle.Location = New System.Drawing.Point(20, 12)
        Me.lblInvTitle.Name = "lblInvTitle"
        Me.lblInvTitle.Size = New System.Drawing.Size(118, 31)
        Me.lblInvTitle.TabIndex = 1149
        Me.lblInvTitle.Text = "Inventory"
        '
        'pnl_History
        '
        Me.pnl_History.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnl_History.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.pnl_History.Controls.Add(Me.pnlHistDetail)
        Me.pnl_History.Controls.Add(Me.Guna2Panel15)
        Me.pnl_History.Controls.Add(Me.Guna2ComboBox2)
        Me.pnl_History.Controls.Add(Me.Guna2ComboBox1)
        Me.pnl_History.Controls.Add(Me.dtpHistory)
        Me.pnl_History.Controls.Add(Me.Guna2TextBox1)
        Me.pnl_History.Controls.Add(Me.pnlHK1)
        Me.pnl_History.Controls.Add(Me.pnlHK2)
        Me.pnl_History.Controls.Add(Me.pnlHK3)
        Me.pnl_History.Controls.Add(Me.pnlHK4)
        Me.pnl_History.Controls.Add(Me.btnHistNewOrder)
        Me.pnl_History.Controls.Add(Me.btnHistExport)
        Me.pnl_History.Controls.Add(Me.Label15)
        Me.pnl_History.Controls.Add(Me.Label4)
        Me.pnl_History.Location = New System.Drawing.Point(0, 0)
        Me.pnl_History.Name = "pnl_History"
        Me.pnl_History.Size = New System.Drawing.Size(1241, 844)
        Me.pnl_History.TabIndex = 53
        Me.pnl_History.Visible = False
        '
        'pnlHistDetail
        '
        Me.pnlHistDetail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlHistDetail.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.pnlHistDetail.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.pnlHistDetail.BorderRadius = 14
        Me.pnlHistDetail.BorderThickness = 1
        Me.pnlHistDetail.Controls.Add(Me.lblDetEmpty)
        Me.pnlHistDetail.Controls.Add(Me.lblDetReceipt)
        Me.pnlHistDetail.Controls.Add(Me.pnlDetTotal)
        Me.pnlHistDetail.Controls.Add(Me.lblDetCash)
        Me.pnlHistDetail.Controls.Add(Me.lblDetTax)
        Me.pnlHistDetail.Controls.Add(Me.lblDetTaxCap)
        Me.pnlHistDetail.Controls.Add(Me.lblDetSub)
        Me.pnlHistDetail.Controls.Add(Me.lblDetSubCap)
        Me.pnlHistDetail.Controls.Add(Me.flDetItems)
        Me.pnlHistDetail.Controls.Add(Me.lblDetSecB)
        Me.pnlHistDetail.Controls.Add(Me.lblDetPay)
        Me.pnlHistDetail.Controls.Add(Me.lblDetPayCap)
        Me.pnlHistDetail.Controls.Add(Me.lblDetCashier)
        Me.pnlHistDetail.Controls.Add(Me.lblDetCashierCap)
        Me.pnlHistDetail.Controls.Add(Me.lblDetSecA)
        Me.pnlHistDetail.Controls.Add(Me.btnDetPrint)
        Me.pnlHistDetail.Controls.Add(Me.lblDetDate)
        Me.pnlHistDetail.Controls.Add(Me.btnDetStatus)
        Me.pnlHistDetail.Controls.Add(Me.lblDetTitle)
        Me.pnlHistDetail.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlHistDetail.Location = New System.Drawing.Point(921, 226)
        Me.pnlHistDetail.Name = "pnlHistDetail"
        Me.pnlHistDetail.Size = New System.Drawing.Size(300, 601)
        Me.pnlHistDetail.TabIndex = 1127
        '
        'lblDetEmpty
        '
        Me.lblDetEmpty.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDetEmpty.BackColor = System.Drawing.Color.Transparent
        Me.lblDetEmpty.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetEmpty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblDetEmpty.Location = New System.Drawing.Point(16, 150)
        Me.lblDetEmpty.Name = "lblDetEmpty"
        Me.lblDetEmpty.Size = New System.Drawing.Size(268, 40)
        Me.lblDetEmpty.TabIndex = 1148
        Me.lblDetEmpty.Text = "Select a transaction to see its receipt."
        Me.lblDetEmpty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDetReceipt
        '
        Me.lblDetReceipt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDetReceipt.BackColor = System.Drawing.Color.Transparent
        Me.lblDetReceipt.Font = New System.Drawing.Font("Segoe UI", 7.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetReceipt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblDetReceipt.Location = New System.Drawing.Point(16, 576)
        Me.lblDetReceipt.Name = "lblDetReceipt"
        Me.lblDetReceipt.Size = New System.Drawing.Size(268, 16)
        Me.lblDetReceipt.TabIndex = 1147
        Me.lblDetReceipt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlDetTotal
        '
        Me.pnlDetTotal.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlDetTotal.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlDetTotal.BorderColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.pnlDetTotal.BorderRadius = 10
        Me.pnlDetTotal.Controls.Add(Me.lblDetTotal)
        Me.pnlDetTotal.Controls.Add(Me.lblDetTotalCap)
        Me.pnlDetTotal.FillColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.pnlDetTotal.Location = New System.Drawing.Point(16, 336)
        Me.pnlDetTotal.Name = "pnlDetTotal"
        Me.pnlDetTotal.Size = New System.Drawing.Size(268, 38)
        Me.pnlDetTotal.TabIndex = 1144
        '
        'lblDetTotal
        '
        Me.lblDetTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDetTotal.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblDetTotal.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetTotal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(150, Byte), Integer), CType(CType(78, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.lblDetTotal.Location = New System.Drawing.Point(110, 6)
        Me.lblDetTotal.Name = "lblDetTotal"
        Me.lblDetTotal.Size = New System.Drawing.Size(146, 26)
        Me.lblDetTotal.TabIndex = 1146
        Me.lblDetTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDetTotalCap
        '
        Me.lblDetTotalCap.AutoSize = True
        Me.lblDetTotalCap.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblDetTotalCap.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetTotalCap.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblDetTotalCap.Location = New System.Drawing.Point(12, 9)
        Me.lblDetTotalCap.Name = "lblDetTotalCap"
        Me.lblDetTotalCap.Size = New System.Drawing.Size(70, 17)
        Me.lblDetTotalCap.TabIndex = 1145
        Me.lblDetTotalCap.Text = "Total paid"
        '
        'lblDetCash
        '
        Me.lblDetCash.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDetCash.BackColor = System.Drawing.Color.Transparent
        Me.lblDetCash.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetCash.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblDetCash.Location = New System.Drawing.Point(16, 314)
        Me.lblDetCash.Name = "lblDetCash"
        Me.lblDetCash.Size = New System.Drawing.Size(268, 16)
        Me.lblDetCash.TabIndex = 1143
        '
        'lblDetTax
        '
        Me.lblDetTax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDetTax.BackColor = System.Drawing.Color.Transparent
        Me.lblDetTax.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetTax.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblDetTax.Location = New System.Drawing.Point(120, 296)
        Me.lblDetTax.Name = "lblDetTax"
        Me.lblDetTax.Size = New System.Drawing.Size(164, 16)
        Me.lblDetTax.TabIndex = 1142
        Me.lblDetTax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDetTaxCap
        '
        Me.lblDetTaxCap.AutoSize = True
        Me.lblDetTaxCap.BackColor = System.Drawing.Color.Transparent
        Me.lblDetTaxCap.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetTaxCap.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblDetTaxCap.Location = New System.Drawing.Point(16, 296)
        Me.lblDetTaxCap.Name = "lblDetTaxCap"
        Me.lblDetTaxCap.Size = New System.Drawing.Size(24, 15)
        Me.lblDetTaxCap.TabIndex = 1141
        Me.lblDetTaxCap.Text = "Tax"
        '
        'lblDetSub
        '
        Me.lblDetSub.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDetSub.BackColor = System.Drawing.Color.Transparent
        Me.lblDetSub.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetSub.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblDetSub.Location = New System.Drawing.Point(120, 278)
        Me.lblDetSub.Name = "lblDetSub"
        Me.lblDetSub.Size = New System.Drawing.Size(164, 16)
        Me.lblDetSub.TabIndex = 1140
        Me.lblDetSub.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDetSubCap
        '
        Me.lblDetSubCap.AutoSize = True
        Me.lblDetSubCap.BackColor = System.Drawing.Color.Transparent
        Me.lblDetSubCap.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetSubCap.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblDetSubCap.Location = New System.Drawing.Point(16, 278)
        Me.lblDetSubCap.Name = "lblDetSubCap"
        Me.lblDetSubCap.Size = New System.Drawing.Size(51, 15)
        Me.lblDetSubCap.TabIndex = 1139
        Me.lblDetSubCap.Text = "Subtotal"
        '
        'flDetItems
        '
        Me.flDetItems.AutoScroll = True
        Me.flDetItems.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.flDetItems.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.flDetItems.Location = New System.Drawing.Point(16, 186)
        Me.flDetItems.Name = "flDetItems"
        Me.flDetItems.Size = New System.Drawing.Size(268, 86)
        Me.flDetItems.TabIndex = 1138
        Me.flDetItems.WrapContents = False
        '
        'lblDetSecB
        '
        Me.lblDetSecB.AutoSize = True
        Me.lblDetSecB.BackColor = System.Drawing.Color.Transparent
        Me.lblDetSecB.Font = New System.Drawing.Font("Segoe UI", 7.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetSecB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblDetSecB.Location = New System.Drawing.Point(16, 170)
        Me.lblDetSecB.Name = "lblDetSecB"
        Me.lblDetSecB.Size = New System.Drawing.Size(35, 12)
        Me.lblDetSecB.TabIndex = 1137
        Me.lblDetSecB.Text = "ITEMS"
        '
        'lblDetPay
        '
        Me.lblDetPay.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDetPay.BackColor = System.Drawing.Color.Transparent
        Me.lblDetPay.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetPay.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblDetPay.Location = New System.Drawing.Point(120, 144)
        Me.lblDetPay.Name = "lblDetPay"
        Me.lblDetPay.Size = New System.Drawing.Size(164, 16)
        Me.lblDetPay.TabIndex = 1136
        Me.lblDetPay.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDetPayCap
        '
        Me.lblDetPayCap.AutoSize = True
        Me.lblDetPayCap.BackColor = System.Drawing.Color.Transparent
        Me.lblDetPayCap.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetPayCap.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblDetPayCap.Location = New System.Drawing.Point(16, 144)
        Me.lblDetPayCap.Name = "lblDetPayCap"
        Me.lblDetPayCap.Size = New System.Drawing.Size(54, 15)
        Me.lblDetPayCap.TabIndex = 1135
        Me.lblDetPayCap.Text = "Payment"
        '
        'lblDetCashier
        '
        Me.lblDetCashier.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDetCashier.BackColor = System.Drawing.Color.Transparent
        Me.lblDetCashier.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetCashier.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblDetCashier.Location = New System.Drawing.Point(120, 124)
        Me.lblDetCashier.Name = "lblDetCashier"
        Me.lblDetCashier.Size = New System.Drawing.Size(164, 16)
        Me.lblDetCashier.TabIndex = 1134
        Me.lblDetCashier.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDetCashierCap
        '
        Me.lblDetCashierCap.AutoSize = True
        Me.lblDetCashierCap.BackColor = System.Drawing.Color.Transparent
        Me.lblDetCashierCap.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetCashierCap.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblDetCashierCap.Location = New System.Drawing.Point(16, 124)
        Me.lblDetCashierCap.Name = "lblDetCashierCap"
        Me.lblDetCashierCap.Size = New System.Drawing.Size(46, 15)
        Me.lblDetCashierCap.TabIndex = 1133
        Me.lblDetCashierCap.Text = "Cashier"
        '
        'lblDetSecA
        '
        Me.lblDetSecA.AutoSize = True
        Me.lblDetSecA.BackColor = System.Drawing.Color.Transparent
        Me.lblDetSecA.Font = New System.Drawing.Font("Segoe UI", 7.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetSecA.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblDetSecA.Location = New System.Drawing.Point(16, 106)
        Me.lblDetSecA.Name = "lblDetSecA"
        Me.lblDetSecA.Size = New System.Drawing.Size(80, 12)
        Me.lblDetSecA.TabIndex = 1132
        Me.lblDetSecA.Text = "ORDER DETAILS"
        '
        'btnDetPrint
        '
        Me.btnDetPrint.Animated = True
        Me.btnDetPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnDetPrint.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.btnDetPrint.BorderRadius = 8
        Me.btnDetPrint.BorderThickness = 1
        Me.btnDetPrint.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDetPrint.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnDetPrint.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDetPrint.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btnDetPrint.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.btnDetPrint.HoverState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btnDetPrint.Location = New System.Drawing.Point(16, 62)
        Me.btnDetPrint.Name = "btnDetPrint"
        Me.btnDetPrint.Size = New System.Drawing.Size(120, 32)
        Me.btnDetPrint.TabIndex = 1131
        Me.btnDetPrint.Text = "Print receipt"
        '
        'lblDetDate
        '
        Me.lblDetDate.AutoSize = True
        Me.lblDetDate.BackColor = System.Drawing.Color.Transparent
        Me.lblDetDate.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblDetDate.Location = New System.Drawing.Point(16, 40)
        Me.lblDetDate.Name = "lblDetDate"
        Me.lblDetDate.Size = New System.Drawing.Size(0, 15)
        Me.lblDetDate.TabIndex = 1130
        '
        'btnDetStatus
        '
        Me.btnDetStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDetStatus.Animated = True
        Me.btnDetStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnDetStatus.BorderColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.btnDetStatus.BorderRadius = 12
        Me.btnDetStatus.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDetStatus.FillColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.btnDetStatus.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDetStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.btnDetStatus.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.btnDetStatus.HoverState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.btnDetStatus.Location = New System.Drawing.Point(196, 14)
        Me.btnDetStatus.Name = "btnDetStatus"
        Me.btnDetStatus.Size = New System.Drawing.Size(88, 24)
        Me.btnDetStatus.TabIndex = 1129
        Me.btnDetStatus.Text = "Completed"
        '
        'lblDetTitle
        '
        Me.lblDetTitle.AutoSize = True
        Me.lblDetTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblDetTitle.Font = New System.Drawing.Font("Segoe UI", 12.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblDetTitle.Location = New System.Drawing.Point(16, 14)
        Me.lblDetTitle.Name = "lblDetTitle"
        Me.lblDetTitle.Size = New System.Drawing.Size(57, 23)
        Me.lblDetTitle.TabIndex = 1128
        Me.lblDetTitle.Text = "Order"
        '
        'Guna2Panel15
        '
        Me.Guna2Panel15.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2Panel15.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Guna2Panel15.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.Guna2Panel15.BorderRadius = 14
        Me.Guna2Panel15.BorderThickness = 1
        Me.Guna2Panel15.Controls.Add(Me.lblHistShowing)
        Me.Guna2Panel15.Controls.Add(Me.histGrid)
        Me.Guna2Panel15.Controls.Add(Me.lblHistCount)
        Me.Guna2Panel15.Controls.Add(Me.Label16)
        Me.Guna2Panel15.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Guna2Panel15.Location = New System.Drawing.Point(20, 226)
        Me.Guna2Panel15.Margin = New System.Windows.Forms.Padding(2)
        Me.Guna2Panel15.Name = "Guna2Panel15"
        Me.Guna2Panel15.Size = New System.Drawing.Size(887, 601)
        Me.Guna2Panel15.TabIndex = 2
        '
        'lblHistShowing
        '
        Me.lblHistShowing.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblHistShowing.AutoSize = True
        Me.lblHistShowing.BackColor = System.Drawing.Color.Transparent
        Me.lblHistShowing.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHistShowing.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblHistShowing.Location = New System.Drawing.Point(16, 574)
        Me.lblHistShowing.Name = "lblHistShowing"
        Me.lblHistShowing.Size = New System.Drawing.Size(0, 15)
        Me.lblHistShowing.TabIndex = 1126
        '
        'histGrid
        '
        Me.histGrid.AllowUserToAddRows = False
        Me.histGrid.AllowUserToDeleteRows = False
        Me.histGrid.AllowUserToResizeRows = False
        Me.histGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.histGrid.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.histGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.histGrid.Location = New System.Drawing.Point(1, 46)
        Me.histGrid.Name = "histGrid"
        Me.histGrid.ReadOnly = True
        Me.histGrid.RowHeadersVisible = False
        Me.histGrid.Size = New System.Drawing.Size(885, 516)
        Me.histGrid.TabIndex = 1125
        '
        'lblHistCount
        '
        Me.lblHistCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblHistCount.BackColor = System.Drawing.Color.Transparent
        Me.lblHistCount.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHistCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblHistCount.Location = New System.Drawing.Point(714, 18)
        Me.lblHistCount.Name = "lblHistCount"
        Me.lblHistCount.Size = New System.Drawing.Size(157, 18)
        Me.lblHistCount.TabIndex = 1124
        Me.lblHistCount.Text = "0 records"
        Me.lblHistCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(16, 14)
        Me.Label16.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(97, 20)
        Me.Label16.TabIndex = 1
        Me.Label16.Text = "Transactions"
        '
        'Guna2ComboBox2
        '
        Me.Guna2ComboBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2ComboBox2.BackColor = System.Drawing.Color.Transparent
        Me.Guna2ComboBox2.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.Guna2ComboBox2.BorderRadius = 10
        Me.Guna2ComboBox2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.Guna2ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Guna2ComboBox2.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Guna2ComboBox2.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.Guna2ComboBox2.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.Guna2ComboBox2.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2ComboBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.Guna2ComboBox2.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.Guna2ComboBox2.ItemHeight = 30
        Me.Guna2ComboBox2.Location = New System.Drawing.Point(1081, 176)
        Me.Guna2ComboBox2.Margin = New System.Windows.Forms.Padding(2)
        Me.Guna2ComboBox2.Name = "Guna2ComboBox2"
        Me.Guna2ComboBox2.Size = New System.Drawing.Size(140, 36)
        Me.Guna2ComboBox2.TabIndex = 5
        '
        'Guna2ComboBox1
        '
        Me.Guna2ComboBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2ComboBox1.BackColor = System.Drawing.Color.Transparent
        Me.Guna2ComboBox1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.Guna2ComboBox1.BorderRadius = 10
        Me.Guna2ComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.Guna2ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Guna2ComboBox1.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Guna2ComboBox1.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.Guna2ComboBox1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.Guna2ComboBox1.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2ComboBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.Guna2ComboBox1.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.Guna2ComboBox1.ItemHeight = 30
        Me.Guna2ComboBox1.Location = New System.Drawing.Point(931, 176)
        Me.Guna2ComboBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.Guna2ComboBox1.Name = "Guna2ComboBox1"
        Me.Guna2ComboBox1.Size = New System.Drawing.Size(140, 36)
        Me.Guna2ComboBox1.TabIndex = 4
        '
        'dtpHistory
        '
        Me.dtpHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpHistory.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.dtpHistory.BorderRadius = 10
        Me.dtpHistory.BorderThickness = 1
        Me.dtpHistory.Checked = True
        Me.dtpHistory.CustomFormat = "MMM d, yyyy"
        Me.dtpHistory.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.dtpHistory.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.dtpHistory.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpHistory.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.dtpHistory.Location = New System.Drawing.Point(771, 176)
        Me.dtpHistory.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.dtpHistory.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpHistory.Name = "dtpHistory"
        Me.dtpHistory.ShowCheckBox = True
        Me.dtpHistory.Size = New System.Drawing.Size(150, 38)
        Me.dtpHistory.TabIndex = 1123
        Me.dtpHistory.Value = New Date(2026, 10, 4, 0, 0, 0, 0)
        '
        'Guna2TextBox1
        '
        Me.Guna2TextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2TextBox1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.Guna2TextBox1.BorderRadius = 10
        Me.Guna2TextBox1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.Guna2TextBox1.DefaultText = ""
        Me.Guna2TextBox1.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.Guna2TextBox1.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.Guna2TextBox1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.Guna2TextBox1.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.Guna2TextBox1.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Guna2TextBox1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.Guna2TextBox1.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2TextBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.Guna2TextBox1.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.Guna2TextBox1.Location = New System.Drawing.Point(20, 176)
        Me.Guna2TextBox1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Guna2TextBox1.Name = "Guna2TextBox1"
        Me.Guna2TextBox1.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.Guna2TextBox1.PlaceholderText = "Search order, customer, product, or payment..."
        Me.Guna2TextBox1.SelectedText = ""
        Me.Guna2TextBox1.Size = New System.Drawing.Size(741, 38)
        Me.Guna2TextBox1.TabIndex = 3
        Me.Guna2TextBox1.TextOffset = New System.Drawing.Point(6, 0)
        '
        'pnlHK1
        '
        Me.pnlHK1.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.pnlHK1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.pnlHK1.BorderRadius = 14
        Me.pnlHK1.BorderThickness = 1
        Me.pnlHK1.Controls.Add(Me.picHK1)
        Me.pnlHK1.Controls.Add(Me.lblHK1S)
        Me.pnlHK1.Controls.Add(Me.lblHK1V)
        Me.pnlHK1.Controls.Add(Me.lblHK1T)
        Me.pnlHK1.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlHK1.Location = New System.Drawing.Point(20, 78)
        Me.pnlHK1.Name = "pnlHK1"
        Me.pnlHK1.Size = New System.Drawing.Size(231, 84)
        Me.pnlHK1.TabIndex = 1103
        '
        'picHK1
        '
        Me.picHK1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picHK1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.picHK1.BorderRadius = 10
        Me.picHK1.FillColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.picHK1.ImageRotate = 0!
        Me.picHK1.Location = New System.Drawing.Point(183, 12)
        Me.picHK1.Name = "picHK1"
        Me.picHK1.Size = New System.Drawing.Size(34, 34)
        Me.picHK1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picHK1.TabIndex = 1107
        Me.picHK1.TabStop = False
        '
        'lblHK1S
        '
        Me.lblHK1S.AutoSize = True
        Me.lblHK1S.BackColor = System.Drawing.Color.Transparent
        Me.lblHK1S.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHK1S.ForeColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.lblHK1S.Location = New System.Drawing.Point(14, 62)
        Me.lblHK1S.Name = "lblHK1S"
        Me.lblHK1S.Size = New System.Drawing.Size(0, 15)
        Me.lblHK1S.TabIndex = 1106
        '
        'lblHK1V
        '
        Me.lblHK1V.AutoSize = True
        Me.lblHK1V.BackColor = System.Drawing.Color.Transparent
        Me.lblHK1V.Font = New System.Drawing.Font("Segoe UI", 19.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHK1V.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblHK1V.Location = New System.Drawing.Point(14, 32)
        Me.lblHK1V.Name = "lblHK1V"
        Me.lblHK1V.Size = New System.Drawing.Size(30, 36)
        Me.lblHK1V.TabIndex = 1105
        Me.lblHK1V.Text = "0"
        '
        'lblHK1T
        '
        Me.lblHK1T.AutoSize = True
        Me.lblHK1T.BackColor = System.Drawing.Color.Transparent
        Me.lblHK1T.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHK1T.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblHK1T.Location = New System.Drawing.Point(14, 12)
        Me.lblHK1T.Name = "lblHK1T"
        Me.lblHK1T.Size = New System.Drawing.Size(61, 15)
        Me.lblHK1T.TabIndex = 1104
        Me.lblHK1T.Text = "Total sales"
        '
        'pnlHK2
        '
        Me.pnlHK2.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.pnlHK2.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.pnlHK2.BorderRadius = 14
        Me.pnlHK2.BorderThickness = 1
        Me.pnlHK2.Controls.Add(Me.picHK2)
        Me.pnlHK2.Controls.Add(Me.lblHK2S)
        Me.pnlHK2.Controls.Add(Me.lblHK2V)
        Me.pnlHK2.Controls.Add(Me.lblHK2T)
        Me.pnlHK2.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlHK2.Location = New System.Drawing.Point(265, 78)
        Me.pnlHK2.Name = "pnlHK2"
        Me.pnlHK2.Size = New System.Drawing.Size(231, 84)
        Me.pnlHK2.TabIndex = 1108
        '
        'picHK2
        '
        Me.picHK2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picHK2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.picHK2.BorderRadius = 10
        Me.picHK2.FillColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.picHK2.ImageRotate = 0!
        Me.picHK2.Location = New System.Drawing.Point(183, 12)
        Me.picHK2.Name = "picHK2"
        Me.picHK2.Size = New System.Drawing.Size(34, 34)
        Me.picHK2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picHK2.TabIndex = 1112
        Me.picHK2.TabStop = False
        '
        'lblHK2S
        '
        Me.lblHK2S.AutoSize = True
        Me.lblHK2S.BackColor = System.Drawing.Color.Transparent
        Me.lblHK2S.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHK2S.ForeColor = System.Drawing.Color.FromArgb(CType(CType(92, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(84, Byte), Integer))
        Me.lblHK2S.Location = New System.Drawing.Point(14, 62)
        Me.lblHK2S.Name = "lblHK2S"
        Me.lblHK2S.Size = New System.Drawing.Size(0, 15)
        Me.lblHK2S.TabIndex = 1111
        '
        'lblHK2V
        '
        Me.lblHK2V.AutoSize = True
        Me.lblHK2V.BackColor = System.Drawing.Color.Transparent
        Me.lblHK2V.Font = New System.Drawing.Font("Segoe UI", 19.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHK2V.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblHK2V.Location = New System.Drawing.Point(14, 32)
        Me.lblHK2V.Name = "lblHK2V"
        Me.lblHK2V.Size = New System.Drawing.Size(30, 36)
        Me.lblHK2V.TabIndex = 1110
        Me.lblHK2V.Text = "0"
        '
        'lblHK2T
        '
        Me.lblHK2T.AutoSize = True
        Me.lblHK2T.BackColor = System.Drawing.Color.Transparent
        Me.lblHK2T.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHK2T.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblHK2T.Location = New System.Drawing.Point(14, 12)
        Me.lblHK2T.Name = "lblHK2T"
        Me.lblHK2T.Size = New System.Drawing.Size(117, 15)
        Me.lblHK2T.TabIndex = 1109
        Me.lblHK2T.Text = "Net sales (before tax)"
        '
        'pnlHK3
        '
        Me.pnlHK3.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.pnlHK3.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.pnlHK3.BorderRadius = 14
        Me.pnlHK3.BorderThickness = 1
        Me.pnlHK3.Controls.Add(Me.picHK3)
        Me.pnlHK3.Controls.Add(Me.lblHK3S)
        Me.pnlHK3.Controls.Add(Me.lblHK3V)
        Me.pnlHK3.Controls.Add(Me.lblHK3T)
        Me.pnlHK3.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlHK3.Location = New System.Drawing.Point(510, 78)
        Me.pnlHK3.Name = "pnlHK3"
        Me.pnlHK3.Size = New System.Drawing.Size(231, 84)
        Me.pnlHK3.TabIndex = 1113
        '
        'picHK3
        '
        Me.picHK3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picHK3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.picHK3.BorderRadius = 10
        Me.picHK3.FillColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.picHK3.ImageRotate = 0!
        Me.picHK3.Location = New System.Drawing.Point(183, 12)
        Me.picHK3.Name = "picHK3"
        Me.picHK3.Size = New System.Drawing.Size(34, 34)
        Me.picHK3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picHK3.TabIndex = 1117
        Me.picHK3.TabStop = False
        '
        'lblHK3S
        '
        Me.lblHK3S.AutoSize = True
        Me.lblHK3S.BackColor = System.Drawing.Color.Transparent
        Me.lblHK3S.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHK3S.ForeColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.lblHK3S.Location = New System.Drawing.Point(14, 62)
        Me.lblHK3S.Name = "lblHK3S"
        Me.lblHK3S.Size = New System.Drawing.Size(0, 15)
        Me.lblHK3S.TabIndex = 1116
        '
        'lblHK3V
        '
        Me.lblHK3V.AutoSize = True
        Me.lblHK3V.BackColor = System.Drawing.Color.Transparent
        Me.lblHK3V.Font = New System.Drawing.Font("Segoe UI", 19.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHK3V.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblHK3V.Location = New System.Drawing.Point(14, 32)
        Me.lblHK3V.Name = "lblHK3V"
        Me.lblHK3V.Size = New System.Drawing.Size(30, 36)
        Me.lblHK3V.TabIndex = 1115
        Me.lblHK3V.Text = "0"
        '
        'lblHK3T
        '
        Me.lblHK3T.AutoSize = True
        Me.lblHK3T.BackColor = System.Drawing.Color.Transparent
        Me.lblHK3T.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHK3T.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblHK3T.Location = New System.Drawing.Point(14, 12)
        Me.lblHK3T.Name = "lblHK3T"
        Me.lblHK3T.Size = New System.Drawing.Size(50, 15)
        Me.lblHK3T.TabIndex = 1114
        Me.lblHK3T.Text = "Refunds"
        '
        'pnlHK4
        '
        Me.pnlHK4.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.pnlHK4.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.pnlHK4.BorderRadius = 14
        Me.pnlHK4.BorderThickness = 1
        Me.pnlHK4.Controls.Add(Me.picHK4)
        Me.pnlHK4.Controls.Add(Me.lblHK4S)
        Me.pnlHK4.Controls.Add(Me.lblHK4V)
        Me.pnlHK4.Controls.Add(Me.lblHK4T)
        Me.pnlHK4.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlHK4.Location = New System.Drawing.Point(755, 78)
        Me.pnlHK4.Name = "pnlHK4"
        Me.pnlHK4.Size = New System.Drawing.Size(231, 84)
        Me.pnlHK4.TabIndex = 1118
        '
        'picHK4
        '
        Me.picHK4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picHK4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.picHK4.BorderRadius = 10
        Me.picHK4.FillColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.picHK4.ImageRotate = 0!
        Me.picHK4.Location = New System.Drawing.Point(183, 12)
        Me.picHK4.Name = "picHK4"
        Me.picHK4.Size = New System.Drawing.Size(34, 34)
        Me.picHK4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picHK4.TabIndex = 1122
        Me.picHK4.TabStop = False
        '
        'lblHK4S
        '
        Me.lblHK4S.AutoSize = True
        Me.lblHK4S.BackColor = System.Drawing.Color.Transparent
        Me.lblHK4S.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHK4S.ForeColor = System.Drawing.Color.FromArgb(CType(CType(92, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(84, Byte), Integer))
        Me.lblHK4S.Location = New System.Drawing.Point(14, 62)
        Me.lblHK4S.Name = "lblHK4S"
        Me.lblHK4S.Size = New System.Drawing.Size(0, 15)
        Me.lblHK4S.TabIndex = 1121
        '
        'lblHK4V
        '
        Me.lblHK4V.AutoSize = True
        Me.lblHK4V.BackColor = System.Drawing.Color.Transparent
        Me.lblHK4V.Font = New System.Drawing.Font("Segoe UI", 19.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHK4V.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblHK4V.Location = New System.Drawing.Point(14, 32)
        Me.lblHK4V.Name = "lblHK4V"
        Me.lblHK4V.Size = New System.Drawing.Size(30, 36)
        Me.lblHK4V.TabIndex = 1120
        Me.lblHK4V.Text = "0"
        '
        'lblHK4T
        '
        Me.lblHK4T.AutoSize = True
        Me.lblHK4T.BackColor = System.Drawing.Color.Transparent
        Me.lblHK4T.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHK4T.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblHK4T.Location = New System.Drawing.Point(14, 12)
        Me.lblHK4T.Name = "lblHK4T"
        Me.lblHK4T.Size = New System.Drawing.Size(75, 15)
        Me.lblHK4T.TabIndex = 1119
        Me.lblHK4T.Text = "Tax collected"
        '
        'btnHistNewOrder
        '
        Me.btnHistNewOrder.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnHistNewOrder.Animated = True
        Me.btnHistNewOrder.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.btnHistNewOrder.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.btnHistNewOrder.BorderRadius = 10
        Me.btnHistNewOrder.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnHistNewOrder.FillColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.btnHistNewOrder.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHistNewOrder.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnHistNewOrder.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.btnHistNewOrder.HoverState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnHistNewOrder.Location = New System.Drawing.Point(881, 14)
        Me.btnHistNewOrder.Name = "btnHistNewOrder"
        Me.btnHistNewOrder.Size = New System.Drawing.Size(110, 38)
        Me.btnHistNewOrder.TabIndex = 1102
        Me.btnHistNewOrder.Text = "+ New order"
        '
        'btnHistExport
        '
        Me.btnHistExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnHistExport.Animated = True
        Me.btnHistExport.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.btnHistExport.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.btnHistExport.BorderRadius = 10
        Me.btnHistExport.BorderThickness = 1
        Me.btnHistExport.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnHistExport.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnHistExport.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHistExport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btnHistExport.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.btnHistExport.HoverState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btnHistExport.Location = New System.Drawing.Point(761, 14)
        Me.btnHistExport.Name = "btnHistExport"
        Me.btnHistExport.Size = New System.Drawing.Size(110, 38)
        Me.btnHistExport.TabIndex = 1101
        Me.btnHistExport.Text = "Export CSV"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.Label15.Location = New System.Drawing.Point(20, 46)
        Me.Label15.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(308, 15)
        Me.Label15.TabIndex = 1
        Me.Label15.Text = "Review transactions, payment records, and receipt details"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 17.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(20, 12)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(157, 31)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Order history"
        '
        'pnlCardDim
        '
        Me.pnlCardDim.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlCardDim.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(22, Byte), Integer))
        Me.pnlCardDim.Controls.Add(Me.pnlCardModal)
        Me.pnlCardDim.Location = New System.Drawing.Point(0, 0)
        Me.pnlCardDim.Name = "pnlCardDim"
        Me.pnlCardDim.Size = New System.Drawing.Size(1241, 844)
        Me.pnlCardDim.TabIndex = 1509
        Me.pnlCardDim.Visible = False
        '
        'pnlCardModal
        '
        Me.pnlCardModal.BackColor = System.Drawing.Color.Transparent
        Me.pnlCardModal.BorderColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnlCardModal.BorderRadius = 20
        Me.pnlCardModal.Controls.Add(Me.btnCardConfirm)
        Me.pnlCardModal.Controls.Add(Me.btnCardAmex)
        Me.pnlCardModal.Controls.Add(Me.btnCardJcb)
        Me.pnlCardModal.Controls.Add(Me.btnCardMastercard)
        Me.pnlCardModal.Controls.Add(Me.btnCardVisa)
        Me.pnlCardModal.Controls.Add(Me.btnCardCredit)
        Me.pnlCardModal.Controls.Add(Me.btnCardDebit)
        Me.pnlCardModal.Controls.Add(Me.pnlCardAmount)
        Me.pnlCardModal.Controls.Add(Me.btnCardClose)
        Me.pnlCardModal.Controls.Add(Me.lblCardModalSub)
        Me.pnlCardModal.Controls.Add(Me.lblCardModalTitle)
        Me.pnlCardModal.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnlCardModal.Location = New System.Drawing.Point(283, 120)
        Me.pnlCardModal.Name = "pnlCardModal"
        Me.pnlCardModal.Size = New System.Drawing.Size(440, 392)
        Me.pnlCardModal.TabIndex = 1510
        '
        'btnCardConfirm
        '
        Me.btnCardConfirm.Animated = True
        Me.btnCardConfirm.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCardConfirm.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.btnCardConfirm.BorderRadius = 12
        Me.btnCardConfirm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCardConfirm.FillColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.btnCardConfirm.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCardConfirm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCardConfirm.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(172, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(46, Byte), Integer))
        Me.btnCardConfirm.HoverState.ForeColor = System.Drawing.Color.White
        Me.btnCardConfirm.Location = New System.Drawing.Point(28, 338)
        Me.btnCardConfirm.Name = "btnCardConfirm"
        Me.btnCardConfirm.Size = New System.Drawing.Size(384, 44)
        Me.btnCardConfirm.TabIndex = 1523
        Me.btnCardConfirm.Text = "Confirm card"
        '
        'btnCardAmex
        '
        Me.btnCardAmex.Animated = True
        Me.btnCardAmex.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCardAmex.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.btnCardAmex.BorderRadius = 12
        Me.btnCardAmex.BorderThickness = 1
        Me.btnCardAmex.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCardAmex.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCardAmex.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCardAmex.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btnCardAmex.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.btnCardAmex.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.btnCardAmex.HoverState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btnCardAmex.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnCardAmex.ImageOffset = New System.Drawing.Point(10, 0)
        Me.btnCardAmex.ImageSize = New System.Drawing.Size(26, 26)
        Me.btnCardAmex.Location = New System.Drawing.Point(226, 272)
        Me.btnCardAmex.Name = "btnCardAmex"
        Me.btnCardAmex.Size = New System.Drawing.Size(186, 52)
        Me.btnCardAmex.TabIndex = 1522
        Me.btnCardAmex.Text = "American Express"
        Me.btnCardAmex.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnCardAmex.TextOffset = New System.Drawing.Point(8, 0)
        '
        'btnCardJcb
        '
        Me.btnCardJcb.Animated = True
        Me.btnCardJcb.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCardJcb.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.btnCardJcb.BorderRadius = 12
        Me.btnCardJcb.BorderThickness = 1
        Me.btnCardJcb.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCardJcb.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCardJcb.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCardJcb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btnCardJcb.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.btnCardJcb.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.btnCardJcb.HoverState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btnCardJcb.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnCardJcb.ImageOffset = New System.Drawing.Point(10, 0)
        Me.btnCardJcb.ImageSize = New System.Drawing.Size(26, 26)
        Me.btnCardJcb.Location = New System.Drawing.Point(28, 272)
        Me.btnCardJcb.Name = "btnCardJcb"
        Me.btnCardJcb.Size = New System.Drawing.Size(186, 52)
        Me.btnCardJcb.TabIndex = 1521
        Me.btnCardJcb.Text = "JCB"
        Me.btnCardJcb.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnCardJcb.TextOffset = New System.Drawing.Point(8, 0)
        '
        'btnCardMastercard
        '
        Me.btnCardMastercard.Animated = True
        Me.btnCardMastercard.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCardMastercard.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.btnCardMastercard.BorderRadius = 12
        Me.btnCardMastercard.BorderThickness = 1
        Me.btnCardMastercard.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCardMastercard.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCardMastercard.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCardMastercard.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btnCardMastercard.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.btnCardMastercard.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.btnCardMastercard.HoverState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btnCardMastercard.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnCardMastercard.ImageOffset = New System.Drawing.Point(10, 0)
        Me.btnCardMastercard.ImageSize = New System.Drawing.Size(26, 26)
        Me.btnCardMastercard.Location = New System.Drawing.Point(226, 210)
        Me.btnCardMastercard.Name = "btnCardMastercard"
        Me.btnCardMastercard.Size = New System.Drawing.Size(186, 52)
        Me.btnCardMastercard.TabIndex = 1520
        Me.btnCardMastercard.Text = "Mastercard"
        Me.btnCardMastercard.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnCardMastercard.TextOffset = New System.Drawing.Point(8, 0)
        '
        'btnCardVisa
        '
        Me.btnCardVisa.Animated = True
        Me.btnCardVisa.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCardVisa.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.btnCardVisa.BorderRadius = 12
        Me.btnCardVisa.BorderThickness = 1
        Me.btnCardVisa.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCardVisa.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCardVisa.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCardVisa.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btnCardVisa.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.btnCardVisa.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.btnCardVisa.HoverState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btnCardVisa.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnCardVisa.ImageOffset = New System.Drawing.Point(10, 0)
        Me.btnCardVisa.ImageSize = New System.Drawing.Size(26, 26)
        Me.btnCardVisa.Location = New System.Drawing.Point(28, 210)
        Me.btnCardVisa.Name = "btnCardVisa"
        Me.btnCardVisa.Size = New System.Drawing.Size(186, 52)
        Me.btnCardVisa.TabIndex = 1519
        Me.btnCardVisa.Text = "Visa"
        Me.btnCardVisa.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnCardVisa.TextOffset = New System.Drawing.Point(8, 0)
        '
        'btnCardCredit
        '
        Me.btnCardCredit.Animated = True
        Me.btnCardCredit.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCardCredit.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.btnCardCredit.BorderRadius = 12
        Me.btnCardCredit.BorderThickness = 1
        Me.btnCardCredit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCardCredit.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCardCredit.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCardCredit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btnCardCredit.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.btnCardCredit.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.btnCardCredit.HoverState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btnCardCredit.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnCardCredit.ImageOffset = New System.Drawing.Point(10, 0)
        Me.btnCardCredit.ImageSize = New System.Drawing.Size(26, 26)
        Me.btnCardCredit.Location = New System.Drawing.Point(226, 148)
        Me.btnCardCredit.Name = "btnCardCredit"
        Me.btnCardCredit.Size = New System.Drawing.Size(186, 52)
        Me.btnCardCredit.TabIndex = 1518
        Me.btnCardCredit.Text = "Credit Card"
        Me.btnCardCredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnCardCredit.TextOffset = New System.Drawing.Point(8, 0)
        '
        'btnCardDebit
        '
        Me.btnCardDebit.Animated = True
        Me.btnCardDebit.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCardDebit.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.btnCardDebit.BorderRadius = 12
        Me.btnCardDebit.BorderThickness = 1
        Me.btnCardDebit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCardDebit.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCardDebit.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCardDebit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btnCardDebit.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.btnCardDebit.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.btnCardDebit.HoverState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btnCardDebit.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnCardDebit.ImageOffset = New System.Drawing.Point(10, 0)
        Me.btnCardDebit.ImageSize = New System.Drawing.Size(26, 26)
        Me.btnCardDebit.Location = New System.Drawing.Point(28, 148)
        Me.btnCardDebit.Name = "btnCardDebit"
        Me.btnCardDebit.Size = New System.Drawing.Size(186, 52)
        Me.btnCardDebit.TabIndex = 1517
        Me.btnCardDebit.Text = "Debit Card"
        Me.btnCardDebit.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnCardDebit.TextOffset = New System.Drawing.Point(8, 0)
        '
        'pnlCardAmount
        '
        Me.pnlCardAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnlCardAmount.BorderColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.pnlCardAmount.BorderRadius = 12
        Me.pnlCardAmount.Controls.Add(Me.lblCardAmt)
        Me.pnlCardAmount.Controls.Add(Me.lblCardAmtCap)
        Me.pnlCardAmount.FillColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.pnlCardAmount.Location = New System.Drawing.Point(28, 86)
        Me.pnlCardAmount.Name = "pnlCardAmount"
        Me.pnlCardAmount.Size = New System.Drawing.Size(384, 48)
        Me.pnlCardAmount.TabIndex = 1514
        '
        'lblCardAmt
        '
        Me.lblCardAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.lblCardAmt.Font = New System.Drawing.Font("Segoe UI", 17.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCardAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(150, Byte), Integer), CType(CType(78, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.lblCardAmt.Location = New System.Drawing.Point(170, 8)
        Me.lblCardAmt.Name = "lblCardAmt"
        Me.lblCardAmt.Size = New System.Drawing.Size(200, 32)
        Me.lblCardAmt.TabIndex = 1516
        Me.lblCardAmt.Text = "0.00"
        Me.lblCardAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCardAmtCap
        '
        Me.lblCardAmtCap.AutoSize = True
        Me.lblCardAmtCap.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.lblCardAmtCap.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCardAmtCap.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblCardAmtCap.Location = New System.Drawing.Point(14, 15)
        Me.lblCardAmtCap.Name = "lblCardAmtCap"
        Me.lblCardAmtCap.Size = New System.Drawing.Size(87, 15)
        Me.lblCardAmtCap.TabIndex = 1515
        Me.lblCardAmtCap.Text = "Amount to pay"
        '
        'btnCardClose
        '
        Me.btnCardClose.Animated = True
        Me.btnCardClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCardClose.BorderColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.btnCardClose.BorderRadius = 15
        Me.btnCardClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCardClose.FillColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.btnCardClose.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCardClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnCardClose.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.btnCardClose.HoverState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnCardClose.Location = New System.Drawing.Point(384, 18)
        Me.btnCardClose.Name = "btnCardClose"
        Me.btnCardClose.Size = New System.Drawing.Size(30, 30)
        Me.btnCardClose.TabIndex = 1513
        Me.btnCardClose.Text = "X"
        '
        'lblCardModalSub
        '
        Me.lblCardModalSub.AutoSize = True
        Me.lblCardModalSub.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblCardModalSub.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCardModalSub.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblCardModalSub.Location = New System.Drawing.Point(28, 52)
        Me.lblCardModalSub.Name = "lblCardModalSub"
        Me.lblCardModalSub.Size = New System.Drawing.Size(242, 15)
        Me.lblCardModalSub.TabIndex = 1512
        Me.lblCardModalSub.Text = "Choose the card the customer is paying with"
        '
        'lblCardModalTitle
        '
        Me.lblCardModalTitle.AutoSize = True
        Me.lblCardModalTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblCardModalTitle.Font = New System.Drawing.Font("Segoe UI", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCardModalTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblCardModalTitle.Location = New System.Drawing.Point(28, 22)
        Me.lblCardModalTitle.Name = "lblCardModalTitle"
        Me.lblCardModalTitle.Size = New System.Drawing.Size(144, 28)
        Me.lblCardModalTitle.TabIndex = 1511
        Me.lblCardModalTitle.Text = "Card payment"
        '
        'pnl_PointOfSale
        '
        Me.pnl_PointOfSale.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.pnl_PointOfSale.Controls.Add(Me.pnlProfileMenu)
        Me.pnl_PointOfSale.Controls.Add(Me.topimage)
        Me.pnl_PointOfSale.Controls.Add(Me.Guna2Panel5)
        Me.pnl_PointOfSale.Controls.Add(Me.txt_SearchMenu)
        Me.pnl_PointOfSale.Controls.Add(Me.btnBell)
        Me.pnl_PointOfSale.Controls.Add(Me.pnlUserChip)
        Me.pnl_PointOfSale.Controls.Add(Me.Guna2Panel6)
        Me.pnl_PointOfSale.Controls.Add(Me.btn_Non_Coffee)
        Me.pnl_PointOfSale.Controls.Add(Me.btn_Specialty)
        Me.pnl_PointOfSale.Controls.Add(Me.btn_IcedCoffee)
        Me.pnl_PointOfSale.Controls.Add(Me.btn_hotCoffee)
        Me.pnl_PointOfSale.Controls.Add(Me.btn_All)
        Me.pnl_PointOfSale.Controls.Add(Me.lblAvailable)
        Me.pnl_PointOfSale.Controls.Add(Me.lblMenuTitle)
        Me.pnl_PointOfSale.Controls.Add(Me.lblSeeAll)
        Me.pnl_PointOfSale.Controls.Add(Me.lblChoose)
        Me.pnl_PointOfSale.Controls.Add(Me.fl_Menu)
        Me.pnl_PointOfSale.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_PointOfSale.Location = New System.Drawing.Point(0, 0)
        Me.pnl_PointOfSale.Name = "pnl_PointOfSale"
        Me.pnl_PointOfSale.Size = New System.Drawing.Size(1241, 844)
        Me.pnl_PointOfSale.TabIndex = 22
        '
        'pnlProfileMenu
        '
        Me.pnlProfileMenu.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlProfileMenu.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.pnlProfileMenu.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.pnlProfileMenu.BorderRadius = 12
        Me.pnlProfileMenu.BorderThickness = 1
        Me.pnlProfileMenu.Controls.Add(Me.btnMenuLogout)
        Me.pnlProfileMenu.Controls.Add(Me.btnMenuProfile)
        Me.pnlProfileMenu.Controls.Add(Me.btnMenuSettings)
        Me.pnlProfileMenu.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlProfileMenu.Location = New System.Drawing.Point(702, 56)
        Me.pnlProfileMenu.Name = "pnlProfileMenu"
        Me.pnlProfileMenu.Size = New System.Drawing.Size(168, 126)
        Me.pnlProfileMenu.TabIndex = 952
        Me.pnlProfileMenu.Visible = False
        '
        'btnMenuLogout
        '
        Me.btnMenuLogout.Animated = True
        Me.btnMenuLogout.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnMenuLogout.BorderRadius = 8
        Me.btnMenuLogout.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnMenuLogout.FillColor = System.Drawing.Color.Transparent
        Me.btnMenuLogout.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMenuLogout.ForeColor = System.Drawing.Color.FromArgb(CType(CType(176, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btnMenuLogout.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.btnMenuLogout.HoverState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(176, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btnMenuLogout.Location = New System.Drawing.Point(8, 80)
        Me.btnMenuLogout.Name = "btnMenuLogout"
        Me.btnMenuLogout.Size = New System.Drawing.Size(152, 34)
        Me.btnMenuLogout.TabIndex = 955
        Me.btnMenuLogout.Text = "Log out"
        Me.btnMenuLogout.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnMenuLogout.TextOffset = New System.Drawing.Point(8, 0)
        '
        'btnMenuProfile
        '
        Me.btnMenuProfile.Animated = True
        Me.btnMenuProfile.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnMenuProfile.BorderRadius = 8
        Me.btnMenuProfile.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnMenuProfile.FillColor = System.Drawing.Color.Transparent
        Me.btnMenuProfile.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMenuProfile.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btnMenuProfile.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.btnMenuProfile.HoverState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btnMenuProfile.Location = New System.Drawing.Point(8, 44)
        Me.btnMenuProfile.Name = "btnMenuProfile"
        Me.btnMenuProfile.Size = New System.Drawing.Size(152, 34)
        Me.btnMenuProfile.TabIndex = 954
        Me.btnMenuProfile.Text = "View Profile"
        Me.btnMenuProfile.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnMenuProfile.TextOffset = New System.Drawing.Point(8, 0)
        '
        'btnMenuSettings
        '
        Me.btnMenuSettings.Animated = True
        Me.btnMenuSettings.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnMenuSettings.BorderRadius = 8
        Me.btnMenuSettings.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnMenuSettings.FillColor = System.Drawing.Color.Transparent
        Me.btnMenuSettings.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMenuSettings.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btnMenuSettings.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.btnMenuSettings.HoverState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btnMenuSettings.Location = New System.Drawing.Point(8, 8)
        Me.btnMenuSettings.Name = "btnMenuSettings"
        Me.btnMenuSettings.Size = New System.Drawing.Size(152, 34)
        Me.btnMenuSettings.TabIndex = 953
        Me.btnMenuSettings.Text = "Settings"
        Me.btnMenuSettings.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnMenuSettings.TextOffset = New System.Drawing.Point(8, 0)
        '
        'topimage
        '
        Me.topimage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.topimage.BackColor = System.Drawing.Color.Transparent
        Me.topimage.BorderRadius = 14
        Me.topimage.Image = CType(resources.GetObject("topimage.Image"), System.Drawing.Image)
        Me.topimage.ImageRotate = 0!
        Me.topimage.Location = New System.Drawing.Point(20, 62)
        Me.topimage.Name = "topimage"
        Me.topimage.Size = New System.Drawing.Size(850, 92)
        Me.topimage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.topimage.TabIndex = 26
        Me.topimage.TabStop = False
        '
        'Guna2Panel5
        '
        Me.Guna2Panel5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2Panel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Guna2Panel5.BorderColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Guna2Panel5.Controls.Add(Me.btn_chkout)
        Me.Guna2Panel5.Controls.Add(Me.txt_Cash_Receive)
        Me.Guna2Panel5.Controls.Add(Me.lbl_Change)
        Me.Guna2Panel5.Controls.Add(Me.Guna2HtmlLabel24)
        Me.Guna2Panel5.Controls.Add(Me.tileCard)
        Me.Guna2Panel5.Controls.Add(Me.tileCash)
        Me.Guna2Panel5.Controls.Add(Me.lblPayCap)
        Me.Guna2Panel5.Controls.Add(Me.pnlTotal)
        Me.Guna2Panel5.Controls.Add(Me.lbl_Tax)
        Me.Guna2Panel5.Controls.Add(Me.Guna2HtmlLabel18)
        Me.Guna2Panel5.Controls.Add(Me.lbl_Subtotal)
        Me.Guna2Panel5.Controls.Add(Me.Guna2HtmlLabel17)
        Me.Guna2Panel5.Controls.Add(Me.Guna2Panel8)
        Me.Guna2Panel5.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Guna2Panel5.Location = New System.Drawing.Point(891, 565)
        Me.Guna2Panel5.Name = "Guna2Panel5"
        Me.Guna2Panel5.Size = New System.Drawing.Size(349, 266)
        Me.Guna2Panel5.TabIndex = 23
        '
        'btn_chkout
        '
        Me.btn_chkout.BorderRadius = 12
        Me.btn_chkout.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_chkout.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(178, Byte), Integer), CType(CType(154, Byte), Integer))
        Me.btn_chkout.DisabledState.CustomBorderColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(178, Byte), Integer), CType(CType(154, Byte), Integer))
        Me.btn_chkout.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(178, Byte), Integer), CType(CType(154, Byte), Integer))
        Me.btn_chkout.DisabledState.ForeColor = System.Drawing.Color.White
        Me.btn_chkout.FillColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.btn_chkout.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_chkout.ForeColor = System.Drawing.Color.White
        Me.btn_chkout.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(172, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(46, Byte), Integer))
        Me.btn_chkout.HoverState.ForeColor = System.Drawing.Color.White
        Me.btn_chkout.Location = New System.Drawing.Point(16, 220)
        Me.btn_chkout.Name = "btn_chkout"
        Me.btn_chkout.Size = New System.Drawing.Size(317, 44)
        Me.btn_chkout.TabIndex = 28
        Me.btn_chkout.Text = "Continue to Payment"
        '
        'txt_Cash_Receive
        '
        Me.txt_Cash_Receive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.txt_Cash_Receive.BorderRadius = 8
        Me.txt_Cash_Receive.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txt_Cash_Receive.DefaultText = ""
        Me.txt_Cash_Receive.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txt_Cash_Receive.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txt_Cash_Receive.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txt_Cash_Receive.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txt_Cash_Receive.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txt_Cash_Receive.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Cash_Receive.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.txt_Cash_Receive.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txt_Cash_Receive.Location = New System.Drawing.Point(16, 182)
        Me.txt_Cash_Receive.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.txt_Cash_Receive.Name = "txt_Cash_Receive"
        Me.txt_Cash_Receive.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.txt_Cash_Receive.PlaceholderText = "Cash received (₱)"
        Me.txt_Cash_Receive.SelectedText = ""
        Me.txt_Cash_Receive.Size = New System.Drawing.Size(189, 32)
        Me.txt_Cash_Receive.TabIndex = 27
        '
        'lbl_Change
        '
        Me.lbl_Change.BackColor = System.Drawing.Color.Transparent
        Me.lbl_Change.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Change.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lbl_Change.Location = New System.Drawing.Point(213, 197)
        Me.lbl_Change.Name = "lbl_Change"
        Me.lbl_Change.Size = New System.Drawing.Size(120, 18)
        Me.lbl_Change.TabIndex = 923
        Me.lbl_Change.Text = "₱0.00"
        Me.lbl_Change.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Guna2HtmlLabel24
        '
        Me.Guna2HtmlLabel24.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel24.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.Guna2HtmlLabel24.Location = New System.Drawing.Point(293, 183)
        Me.Guna2HtmlLabel24.Name = "Guna2HtmlLabel24"
        Me.Guna2HtmlLabel24.Size = New System.Drawing.Size(43, 15)
        Me.Guna2HtmlLabel24.TabIndex = 32
        Me.Guna2HtmlLabel24.Text = "Change"
        '
        'tileCard
        '
        Me.tileCard.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tileCard.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.tileCard.BorderRadius = 10
        Me.tileCard.BorderThickness = 1
        Me.tileCard.Cursor = System.Windows.Forms.Cursors.Hand
        Me.tileCard.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tileCard.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tileCard.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.tileCard.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.tileCard.HoverState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.tileCard.ImageOffset = New System.Drawing.Point(0, -10)
        Me.tileCard.ImageSize = New System.Drawing.Size(22, 22)
        Me.tileCard.Location = New System.Drawing.Point(179, 124)
        Me.tileCard.Name = "tileCard"
        Me.tileCard.Size = New System.Drawing.Size(153, 50)
        Me.tileCard.TabIndex = 928
        Me.tileCard.Text = "Card"
        Me.tileCard.TextOffset = New System.Drawing.Point(0, 13)
        '
        'tileCash
        '
        Me.tileCash.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tileCash.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.tileCash.BorderRadius = 10
        Me.tileCash.BorderThickness = 1
        Me.tileCash.Cursor = System.Windows.Forms.Cursors.Hand
        Me.tileCash.FillColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.tileCash.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tileCash.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.tileCash.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.tileCash.HoverState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.tileCash.ImageOffset = New System.Drawing.Point(0, -10)
        Me.tileCash.ImageSize = New System.Drawing.Size(22, 22)
        Me.tileCash.Location = New System.Drawing.Point(16, 124)
        Me.tileCash.Name = "tileCash"
        Me.tileCash.Size = New System.Drawing.Size(153, 50)
        Me.tileCash.TabIndex = 927
        Me.tileCash.Text = "Cash"
        Me.tileCash.TextOffset = New System.Drawing.Point(0, 13)
        '
        'lblPayCap
        '
        Me.lblPayCap.AutoSize = True
        Me.lblPayCap.BackColor = System.Drawing.Color.Transparent
        Me.lblPayCap.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPayCap.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblPayCap.Location = New System.Drawing.Point(16, 102)
        Me.lblPayCap.Name = "lblPayCap"
        Me.lblPayCap.Size = New System.Drawing.Size(124, 19)
        Me.lblPayCap.TabIndex = 926
        Me.lblPayCap.Text = "Payment Method"
        '
        'pnlTotal
        '
        Me.pnlTotal.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlTotal.BorderRadius = 10
        Me.pnlTotal.Controls.Add(Me.lbl_Total)
        Me.pnlTotal.Controls.Add(Me.lblTotalSub)
        Me.pnlTotal.Controls.Add(Me.Guna2HtmlLabel19)
        Me.pnlTotal.FillColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.pnlTotal.Location = New System.Drawing.Point(16, 52)
        Me.pnlTotal.Name = "pnlTotal"
        Me.pnlTotal.Size = New System.Drawing.Size(317, 44)
        Me.pnlTotal.TabIndex = 924
        '
        'lbl_Total
        '
        Me.lbl_Total.BackColor = System.Drawing.Color.Transparent
        Me.lbl_Total.Font = New System.Drawing.Font("Segoe UI", 17.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Total.ForeColor = System.Drawing.Color.FromArgb(CType(CType(150, Byte), Integer), CType(CType(78, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.lbl_Total.Location = New System.Drawing.Point(115, 5)
        Me.lbl_Total.Name = "lbl_Total"
        Me.lbl_Total.Size = New System.Drawing.Size(190, 34)
        Me.lbl_Total.TabIndex = 922
        Me.lbl_Total.Text = "₱0.00"
        Me.lbl_Total.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalSub
        '
        Me.lblTotalSub.AutoSize = True
        Me.lblTotalSub.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalSub.Font = New System.Drawing.Font("Segoe UI", 7.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalSub.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblTotalSub.Location = New System.Drawing.Point(12, 24)
        Me.lblTotalSub.Name = "lblTotalSub"
        Me.lblTotalSub.Size = New System.Drawing.Size(60, 12)
        Me.lblTotalSub.TabIndex = 925
        Me.lblTotalSub.Text = "Tax included"
        '
        'Guna2HtmlLabel19
        '
        Me.Guna2HtmlLabel19.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel19.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.Guna2HtmlLabel19.Location = New System.Drawing.Point(12, 4)
        Me.Guna2HtmlLabel19.Name = "Guna2HtmlLabel19"
        Me.Guna2HtmlLabel19.Size = New System.Drawing.Size(35, 19)
        Me.Guna2HtmlLabel19.TabIndex = 25
        Me.Guna2HtmlLabel19.Text = "Total"
        '
        'lbl_Tax
        '
        Me.lbl_Tax.BackColor = System.Drawing.Color.Transparent
        Me.lbl_Tax.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Tax.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lbl_Tax.Location = New System.Drawing.Point(203, 29)
        Me.lbl_Tax.Name = "lbl_Tax"
        Me.lbl_Tax.Size = New System.Drawing.Size(130, 18)
        Me.lbl_Tax.TabIndex = 921
        Me.lbl_Tax.Text = "₱0.00"
        Me.lbl_Tax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Guna2HtmlLabel18
        '
        Me.Guna2HtmlLabel18.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel18.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.Guna2HtmlLabel18.Location = New System.Drawing.Point(16, 30)
        Me.Guna2HtmlLabel18.Name = "Guna2HtmlLabel18"
        Me.Guna2HtmlLabel18.Size = New System.Drawing.Size(21, 17)
        Me.Guna2HtmlLabel18.TabIndex = 24
        Me.Guna2HtmlLabel18.Text = "Tax"
        '
        'lbl_Subtotal
        '
        Me.lbl_Subtotal.BackColor = System.Drawing.Color.Transparent
        Me.lbl_Subtotal.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Subtotal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lbl_Subtotal.Location = New System.Drawing.Point(203, 9)
        Me.lbl_Subtotal.Name = "lbl_Subtotal"
        Me.lbl_Subtotal.Size = New System.Drawing.Size(130, 18)
        Me.lbl_Subtotal.TabIndex = 920
        Me.lbl_Subtotal.Text = "₱0.00"
        Me.lbl_Subtotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Guna2HtmlLabel17
        '
        Me.Guna2HtmlLabel17.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel17.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.Guna2HtmlLabel17.Location = New System.Drawing.Point(16, 10)
        Me.Guna2HtmlLabel17.Name = "Guna2HtmlLabel17"
        Me.Guna2HtmlLabel17.Size = New System.Drawing.Size(53, 17)
        Me.Guna2HtmlLabel17.TabIndex = 23
        Me.Guna2HtmlLabel17.Text = "Sub Total"
        '
        'Guna2Panel8
        '
        Me.Guna2Panel8.BackColor = System.Drawing.Color.MediumSeaGreen
        Me.Guna2Panel8.BorderColor = System.Drawing.Color.Transparent
        Me.Guna2Panel8.BorderThickness = 1
        Me.Guna2Panel8.FillColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.Guna2Panel8.Location = New System.Drawing.Point(16, 0)
        Me.Guna2Panel8.Name = "Guna2Panel8"
        Me.Guna2Panel8.Size = New System.Drawing.Size(317, 1)
        Me.Guna2Panel8.TabIndex = 23
        '
        'txt_SearchMenu
        '
        Me.txt_SearchMenu.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_SearchMenu.BackColor = System.Drawing.Color.Transparent
        Me.txt_SearchMenu.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.txt_SearchMenu.BorderRadius = 12
        Me.txt_SearchMenu.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txt_SearchMenu.DefaultText = ""
        Me.txt_SearchMenu.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txt_SearchMenu.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txt_SearchMenu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txt_SearchMenu.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txt_SearchMenu.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.txt_SearchMenu.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txt_SearchMenu.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_SearchMenu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.txt_SearchMenu.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txt_SearchMenu.IconLeft = CType(resources.GetObject("txt_SearchMenu.IconLeft"), System.Drawing.Image)
        Me.txt_SearchMenu.IconLeftOffset = New System.Drawing.Point(8, 0)
        Me.txt_SearchMenu.IconLeftSize = New System.Drawing.Size(18, 18)
        Me.txt_SearchMenu.Location = New System.Drawing.Point(20, 12)
        Me.txt_SearchMenu.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.txt_SearchMenu.Name = "txt_SearchMenu"
        Me.txt_SearchMenu.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.txt_SearchMenu.PlaceholderText = "Search menu, category, or item..."
        Me.txt_SearchMenu.SelectedText = ""
        Me.txt_SearchMenu.Size = New System.Drawing.Size(600, 40)
        Me.txt_SearchMenu.TabIndex = 24
        Me.txt_SearchMenu.TextOffset = New System.Drawing.Point(4, 0)
        '
        'btnBell
        '
        Me.btnBell.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBell.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.btnBell.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.btnBell.BorderRadius = 12
        Me.btnBell.BorderThickness = 1
        Me.btnBell.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnBell.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnBell.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBell.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btnBell.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.btnBell.ImageSize = New System.Drawing.Size(22, 22)
        Me.btnBell.Location = New System.Drawing.Point(630, 12)
        Me.btnBell.Name = "btnBell"
        Me.btnBell.Size = New System.Drawing.Size(42, 40)
        Me.btnBell.TabIndex = 907
        '
        'pnlUserChip
        '
        Me.pnlUserChip.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlUserChip.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.pnlUserChip.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.pnlUserChip.BorderRadius = 12
        Me.pnlUserChip.BorderThickness = 1
        Me.pnlUserChip.Controls.Add(Me.lblChevron)
        Me.pnlUserChip.Controls.Add(Me.lblUserRole)
        Me.pnlUserChip.Controls.Add(Me.lblUserName)
        Me.pnlUserChip.Controls.Add(Me.picAvatar)
        Me.pnlUserChip.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pnlUserChip.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlUserChip.Location = New System.Drawing.Point(702, 12)
        Me.pnlUserChip.Name = "pnlUserChip"
        Me.pnlUserChip.Size = New System.Drawing.Size(168, 40)
        Me.pnlUserChip.TabIndex = 908
        '
        'lblChevron
        '
        Me.lblChevron.AutoSize = True
        Me.lblChevron.BackColor = System.Drawing.Color.Transparent
        Me.lblChevron.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblChevron.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChevron.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblChevron.Location = New System.Drawing.Point(147, 13)
        Me.lblChevron.Name = "lblChevron"
        Me.lblChevron.Size = New System.Drawing.Size(16, 13)
        Me.lblChevron.TabIndex = 951
        Me.lblChevron.Text = "▼"
        '
        'lblUserRole
        '
        Me.lblUserRole.BackColor = System.Drawing.Color.Transparent
        Me.lblUserRole.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblUserRole.Font = New System.Drawing.Font("Segoe UI", 7.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUserRole.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblUserRole.Location = New System.Drawing.Point(42, 20)
        Me.lblUserRole.Name = "lblUserRole"
        Me.lblUserRole.Size = New System.Drawing.Size(120, 14)
        Me.lblUserRole.TabIndex = 911
        Me.lblUserRole.Text = "Cashier"
        '
        'lblUserName
        '
        Me.lblUserName.AutoEllipsis = True
        Me.lblUserName.BackColor = System.Drawing.Color.Transparent
        Me.lblUserName.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblUserName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUserName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblUserName.Location = New System.Drawing.Point(42, 4)
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.Size = New System.Drawing.Size(100, 17)
        Me.lblUserName.TabIndex = 910
        Me.lblUserName.Text = "Cashier"
        '
        'picAvatar
        '
        Me.picAvatar.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.picAvatar.BorderRadius = 15
        Me.picAvatar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.picAvatar.FillColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.picAvatar.ImageRotate = 0!
        Me.picAvatar.Location = New System.Drawing.Point(6, 5)
        Me.picAvatar.Name = "picAvatar"
        Me.picAvatar.Size = New System.Drawing.Size(30, 30)
        Me.picAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picAvatar.TabIndex = 909
        Me.picAvatar.TabStop = False
        '
        'Guna2Panel6
        '
        Me.Guna2Panel6.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Guna2Panel6.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.Guna2Panel6.BorderThickness = 1
        Me.Guna2Panel6.Controls.Add(Me.lblEmpty)
        Me.Guna2Panel6.Controls.Add(Me.btn_Clear)
        Me.Guna2Panel6.Controls.Add(Me.lblCartCount)
        Me.Guna2Panel6.Controls.Add(Me.lblCurrentOrder)
        Me.Guna2Panel6.Controls.Add(Me.fl_MenuProduct)
        Me.Guna2Panel6.Controls.Add(Me.pnlCartSep)
        Me.Guna2Panel6.Controls.Add(Me.txtOrderNo)
        Me.Guna2Panel6.Controls.Add(Me.lblOrderCap)
        Me.Guna2Panel6.Controls.Add(Me.cboTable)
        Me.Guna2Panel6.Controls.Add(Me.lblTableCap)
        Me.Guna2Panel6.Controls.Add(Me.txtCustomer)
        Me.Guna2Panel6.Controls.Add(Me.lblCustNameCap)
        Me.Guna2Panel6.Controls.Add(Me.lblCustInfo)
        Me.Guna2Panel6.Controls.Add(Me.Guna2HtmlLabel15)
        Me.Guna2Panel6.Controls.Add(Me.picCartIcon)
        Me.Guna2Panel6.Dock = System.Windows.Forms.DockStyle.Right
        Me.Guna2Panel6.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Guna2Panel6.Location = New System.Drawing.Point(890, 0)
        Me.Guna2Panel6.Name = "Guna2Panel6"
        Me.Guna2Panel6.Size = New System.Drawing.Size(351, 844)
        Me.Guna2Panel6.TabIndex = 5
        '
        'lblEmpty
        '
        Me.lblEmpty.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblEmpty.BackColor = System.Drawing.Color.Transparent
        Me.lblEmpty.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblEmpty.Location = New System.Drawing.Point(16, 252)
        Me.lblEmpty.Name = "lblEmpty"
        Me.lblEmpty.Size = New System.Drawing.Size(317, 44)
        Me.lblEmpty.TabIndex = 919
        Me.lblEmpty.Text = "Your order is empty." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Tap + Add on any item to start."
        Me.lblEmpty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btn_Clear
        '
        Me.btn_Clear.BorderRadius = 6
        Me.btn_Clear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_Clear.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btn_Clear.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btn_Clear.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btn_Clear.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btn_Clear.FillColor = System.Drawing.Color.Transparent
        Me.btn_Clear.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Clear.ForeColor = System.Drawing.Color.FromArgb(CType(CType(176, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btn_Clear.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btn_Clear.HoverState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(176, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btn_Clear.Location = New System.Drawing.Point(279, 185)
        Me.btn_Clear.Name = "btn_Clear"
        Me.btn_Clear.Size = New System.Drawing.Size(56, 22)
        Me.btn_Clear.TabIndex = 24
        Me.btn_Clear.Text = "Clear"
        '
        'lblCartCount
        '
        Me.lblCartCount.BackColor = System.Drawing.Color.Transparent
        Me.lblCartCount.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCartCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblCartCount.Location = New System.Drawing.Point(195, 189)
        Me.lblCartCount.Name = "lblCartCount"
        Me.lblCartCount.Size = New System.Drawing.Size(70, 18)
        Me.lblCartCount.TabIndex = 918
        Me.lblCartCount.Text = "0 items"
        Me.lblCartCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCurrentOrder
        '
        Me.lblCurrentOrder.AutoSize = True
        Me.lblCurrentOrder.BackColor = System.Drawing.Color.Transparent
        Me.lblCurrentOrder.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentOrder.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblCurrentOrder.Location = New System.Drawing.Point(16, 187)
        Me.lblCurrentOrder.Name = "lblCurrentOrder"
        Me.lblCurrentOrder.Size = New System.Drawing.Size(106, 20)
        Me.lblCurrentOrder.TabIndex = 917
        Me.lblCurrentOrder.Text = "Current Order"
        '
        'fl_MenuProduct
        '
        Me.fl_MenuProduct.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.fl_MenuProduct.AutoScroll = True
        Me.fl_MenuProduct.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.fl_MenuProduct.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.fl_MenuProduct.Location = New System.Drawing.Point(0, 214)
        Me.fl_MenuProduct.Name = "fl_MenuProduct"
        Me.fl_MenuProduct.Size = New System.Drawing.Size(349, 346)
        Me.fl_MenuProduct.TabIndex = 26
        '
        'pnlCartSep
        '
        Me.pnlCartSep.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlCartSep.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.pnlCartSep.FillColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.pnlCartSep.Location = New System.Drawing.Point(16, 179)
        Me.pnlCartSep.Name = "pnlCartSep"
        Me.pnlCartSep.Size = New System.Drawing.Size(319, 1)
        Me.pnlCartSep.TabIndex = 1508
        '
        'txtOrderNo
        '
        Me.txtOrderNo.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.txtOrderNo.BorderRadius = 10
        Me.txtOrderNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOrderNo.DefaultText = ""
        Me.txtOrderNo.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.txtOrderNo.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.txtOrderNo.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOrderNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.txtOrderNo.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.txtOrderNo.IconLeftOffset = New System.Drawing.Point(8, 0)
        Me.txtOrderNo.IconLeftSize = New System.Drawing.Size(16, 16)
        Me.txtOrderNo.Location = New System.Drawing.Point(181, 139)
        Me.txtOrderNo.Name = "txtOrderNo"
        Me.txtOrderNo.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.txtOrderNo.PlaceholderText = "Order number"
        Me.txtOrderNo.ReadOnly = True
        Me.txtOrderNo.SelectedText = ""
        Me.txtOrderNo.Size = New System.Drawing.Size(154, 32)
        Me.txtOrderNo.TabIndex = 1507
        Me.txtOrderNo.TextOffset = New System.Drawing.Point(4, 0)
        '
        'lblOrderCap
        '
        Me.lblOrderCap.AutoSize = True
        Me.lblOrderCap.BackColor = System.Drawing.Color.Transparent
        Me.lblOrderCap.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOrderCap.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblOrderCap.Location = New System.Drawing.Point(181, 125)
        Me.lblOrderCap.Name = "lblOrderCap"
        Me.lblOrderCap.Size = New System.Drawing.Size(80, 13)
        Me.lblOrderCap.TabIndex = 1506
        Me.lblOrderCap.Text = "Order number"
        '
        'cboTable
        '
        Me.cboTable.BackColor = System.Drawing.Color.Transparent
        Me.cboTable.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.cboTable.BorderRadius = 10
        Me.cboTable.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cboTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTable.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.cboTable.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.cboTable.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.cboTable.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTable.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.cboTable.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.cboTable.ItemHeight = 30
        Me.cboTable.Location = New System.Drawing.Point(16, 139)
        Me.cboTable.Name = "cboTable"
        Me.cboTable.Size = New System.Drawing.Size(154, 36)
        Me.cboTable.TabIndex = 1505
        '
        'lblTableCap
        '
        Me.lblTableCap.AutoSize = True
        Me.lblTableCap.BackColor = System.Drawing.Color.Transparent
        Me.lblTableCap.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTableCap.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblTableCap.Location = New System.Drawing.Point(16, 125)
        Me.lblTableCap.Name = "lblTableCap"
        Me.lblTableCap.Size = New System.Drawing.Size(33, 13)
        Me.lblTableCap.TabIndex = 1504
        Me.lblTableCap.Text = "Table"
        '
        'txtCustomer
        '
        Me.txtCustomer.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.txtCustomer.BorderRadius = 10
        Me.txtCustomer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustomer.DefaultText = ""
        Me.txtCustomer.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.txtCustomer.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.txtCustomer.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.txtCustomer.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.txtCustomer.IconLeftOffset = New System.Drawing.Point(8, 0)
        Me.txtCustomer.IconLeftSize = New System.Drawing.Size(16, 16)
        Me.txtCustomer.Location = New System.Drawing.Point(16, 87)
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.txtCustomer.PlaceholderText = "Customer name (optional)"
        Me.txtCustomer.SelectedText = ""
        Me.txtCustomer.Size = New System.Drawing.Size(319, 32)
        Me.txtCustomer.TabIndex = 1503
        Me.txtCustomer.TextOffset = New System.Drawing.Point(4, 0)
        '
        'lblCustNameCap
        '
        Me.lblCustNameCap.AutoSize = True
        Me.lblCustNameCap.BackColor = System.Drawing.Color.Transparent
        Me.lblCustNameCap.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustNameCap.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblCustNameCap.Location = New System.Drawing.Point(16, 73)
        Me.lblCustNameCap.Name = "lblCustNameCap"
        Me.lblCustNameCap.Size = New System.Drawing.Size(87, 13)
        Me.lblCustNameCap.TabIndex = 1502
        Me.lblCustNameCap.Text = "Customer name"
        '
        'lblCustInfo
        '
        Me.lblCustInfo.AutoSize = True
        Me.lblCustInfo.BackColor = System.Drawing.Color.Transparent
        Me.lblCustInfo.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblCustInfo.Location = New System.Drawing.Point(16, 52)
        Me.lblCustInfo.Name = "lblCustInfo"
        Me.lblCustInfo.Size = New System.Drawing.Size(156, 19)
        Me.lblCustInfo.TabIndex = 1501
        Me.lblCustInfo.Text = "Customer Information"
        '
        'Guna2HtmlLabel15
        '
        Me.Guna2HtmlLabel15.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel15.Font = New System.Drawing.Font("Segoe UI", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.Guna2HtmlLabel15.Location = New System.Drawing.Point(56, 12)
        Me.Guna2HtmlLabel15.Name = "Guna2HtmlLabel15"
        Me.Guna2HtmlLabel15.Size = New System.Drawing.Size(95, 30)
        Me.Guna2HtmlLabel15.TabIndex = 19
        Me.Guna2HtmlLabel15.Text = "Order Cart"
        '
        'picCartIcon
        '
        Me.picCartIcon.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.picCartIcon.BorderRadius = 8
        Me.picCartIcon.FillColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.picCartIcon.ImageRotate = 0!
        Me.picCartIcon.Location = New System.Drawing.Point(16, 10)
        Me.picCartIcon.Name = "picCartIcon"
        Me.picCartIcon.Size = New System.Drawing.Size(34, 34)
        Me.picCartIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picCartIcon.TabIndex = 916
        Me.picCartIcon.TabStop = False
        '
        'btn_Non_Coffee
        '
        Me.btn_Non_Coffee.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.btn_Non_Coffee.BorderRadius = 10
        Me.btn_Non_Coffee.BorderThickness = 1
        Me.btn_Non_Coffee.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_Non_Coffee.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btn_Non_Coffee.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btn_Non_Coffee.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btn_Non_Coffee.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btn_Non_Coffee.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btn_Non_Coffee.Font = New System.Drawing.Font("Segoe UI", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Non_Coffee.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btn_Non_Coffee.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.btn_Non_Coffee.HoverState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btn_Non_Coffee.ImageOffset = New System.Drawing.Point(0, -12)
        Me.btn_Non_Coffee.ImageSize = New System.Drawing.Size(30, 30)
        Me.btn_Non_Coffee.Location = New System.Drawing.Point(520, 190)
        Me.btn_Non_Coffee.Name = "btn_Non_Coffee"
        Me.btn_Non_Coffee.Size = New System.Drawing.Size(115, 64)
        Me.btn_Non_Coffee.TabIndex = 3
        Me.btn_Non_Coffee.Text = "Non-Coffee"
        Me.btn_Non_Coffee.TextOffset = New System.Drawing.Point(0, 19)
        '
        'btn_Specialty
        '
        Me.btn_Specialty.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.btn_Specialty.BorderRadius = 10
        Me.btn_Specialty.BorderThickness = 1
        Me.btn_Specialty.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_Specialty.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btn_Specialty.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btn_Specialty.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btn_Specialty.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btn_Specialty.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btn_Specialty.Font = New System.Drawing.Font("Segoe UI", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Specialty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btn_Specialty.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.btn_Specialty.HoverState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btn_Specialty.ImageOffset = New System.Drawing.Point(0, -12)
        Me.btn_Specialty.ImageSize = New System.Drawing.Size(30, 30)
        Me.btn_Specialty.Location = New System.Drawing.Point(395, 190)
        Me.btn_Specialty.Name = "btn_Specialty"
        Me.btn_Specialty.Size = New System.Drawing.Size(115, 64)
        Me.btn_Specialty.TabIndex = 2
        Me.btn_Specialty.Text = "Specialty"
        Me.btn_Specialty.TextOffset = New System.Drawing.Point(0, 19)
        '
        'btn_IcedCoffee
        '
        Me.btn_IcedCoffee.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.btn_IcedCoffee.BorderRadius = 10
        Me.btn_IcedCoffee.BorderThickness = 1
        Me.btn_IcedCoffee.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_IcedCoffee.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btn_IcedCoffee.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btn_IcedCoffee.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btn_IcedCoffee.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btn_IcedCoffee.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btn_IcedCoffee.Font = New System.Drawing.Font("Segoe UI", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_IcedCoffee.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btn_IcedCoffee.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.btn_IcedCoffee.HoverState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btn_IcedCoffee.ImageOffset = New System.Drawing.Point(0, -12)
        Me.btn_IcedCoffee.ImageSize = New System.Drawing.Size(30, 30)
        Me.btn_IcedCoffee.Location = New System.Drawing.Point(270, 190)
        Me.btn_IcedCoffee.Name = "btn_IcedCoffee"
        Me.btn_IcedCoffee.Size = New System.Drawing.Size(115, 64)
        Me.btn_IcedCoffee.TabIndex = 4
        Me.btn_IcedCoffee.Text = "Iced Coffee"
        Me.btn_IcedCoffee.TextOffset = New System.Drawing.Point(0, 19)
        '
        'btn_hotCoffee
        '
        Me.btn_hotCoffee.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.btn_hotCoffee.BorderRadius = 10
        Me.btn_hotCoffee.BorderThickness = 1
        Me.btn_hotCoffee.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_hotCoffee.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btn_hotCoffee.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btn_hotCoffee.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btn_hotCoffee.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btn_hotCoffee.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btn_hotCoffee.Font = New System.Drawing.Font("Segoe UI", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_hotCoffee.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btn_hotCoffee.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.btn_hotCoffee.HoverState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btn_hotCoffee.ImageOffset = New System.Drawing.Point(0, -12)
        Me.btn_hotCoffee.ImageSize = New System.Drawing.Size(30, 30)
        Me.btn_hotCoffee.Location = New System.Drawing.Point(145, 190)
        Me.btn_hotCoffee.Name = "btn_hotCoffee"
        Me.btn_hotCoffee.Size = New System.Drawing.Size(115, 64)
        Me.btn_hotCoffee.TabIndex = 1
        Me.btn_hotCoffee.Text = "Hot Coffee"
        Me.btn_hotCoffee.TextOffset = New System.Drawing.Point(0, 19)
        '
        'btn_All
        '
        Me.btn_All.Animated = True
        Me.btn_All.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.btn_All.BorderRadius = 10
        Me.btn_All.BorderThickness = 1
        Me.btn_All.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_All.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btn_All.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btn_All.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btn_All.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btn_All.FillColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.btn_All.Font = New System.Drawing.Font("Segoe UI", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_All.ForeColor = System.Drawing.Color.White
        Me.btn_All.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.btn_All.HoverState.ForeColor = System.Drawing.Color.White
        Me.btn_All.ImageOffset = New System.Drawing.Point(0, -12)
        Me.btn_All.ImageSize = New System.Drawing.Size(30, 30)
        Me.btn_All.IndicateFocus = True
        Me.btn_All.Location = New System.Drawing.Point(20, 190)
        Me.btn_All.Name = "btn_All"
        Me.btn_All.Size = New System.Drawing.Size(115, 64)
        Me.btn_All.TabIndex = 0
        Me.btn_All.Text = "All"
        Me.btn_All.TextOffset = New System.Drawing.Point(0, 19)
        '
        'lblAvailable
        '
        Me.lblAvailable.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAvailable.AutoSize = True
        Me.lblAvailable.BackColor = System.Drawing.Color.Transparent
        Me.lblAvailable.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAvailable.ForeColor = System.Drawing.Color.FromArgb(CType(CType(92, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(84, Byte), Integer))
        Me.lblAvailable.Location = New System.Drawing.Point(762, 272)
        Me.lblAvailable.Name = "lblAvailable"
        Me.lblAvailable.Size = New System.Drawing.Size(101, 13)
        Me.lblAvailable.TabIndex = 915
        Me.lblAvailable.Text = "● 0 items available"
        '
        'lblMenuTitle
        '
        Me.lblMenuTitle.AutoSize = True
        Me.lblMenuTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblMenuTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMenuTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblMenuTitle.Location = New System.Drawing.Point(20, 266)
        Me.lblMenuTitle.Name = "lblMenuTitle"
        Me.lblMenuTitle.Size = New System.Drawing.Size(184, 21)
        Me.lblMenuTitle.TabIndex = 914
        Me.lblMenuTitle.Text = "Special Menu All Items"
        '
        'lblSeeAll
        '
        Me.lblSeeAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSeeAll.AutoSize = True
        Me.lblSeeAll.BackColor = System.Drawing.Color.Transparent
        Me.lblSeeAll.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblSeeAll.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSeeAll.ForeColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.lblSeeAll.Location = New System.Drawing.Point(818, 169)
        Me.lblSeeAll.Name = "lblSeeAll"
        Me.lblSeeAll.Size = New System.Drawing.Size(55, 15)
        Me.lblSeeAll.TabIndex = 913
        Me.lblSeeAll.Text = "See All →"
        '
        'lblChoose
        '
        Me.lblChoose.AutoSize = True
        Me.lblChoose.BackColor = System.Drawing.Color.Transparent
        Me.lblChoose.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChoose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblChoose.Location = New System.Drawing.Point(20, 164)
        Me.lblChoose.Name = "lblChoose"
        Me.lblChoose.Size = New System.Drawing.Size(140, 21)
        Me.lblChoose.TabIndex = 912
        Me.lblChoose.Text = "Choose Category"
        '
        'fl_Menu
        '
        Me.fl_Menu.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.fl_Menu.AutoScroll = True
        Me.fl_Menu.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.fl_Menu.Location = New System.Drawing.Point(20, 294)
        Me.fl_Menu.Name = "fl_Menu"
        Me.fl_Menu.Size = New System.Drawing.Size(850, 531)
        Me.fl_Menu.TabIndex = 25
        '
        'pnl_CashierMessages
        '
        Me.pnl_CashierMessages.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnl_CashierMessages.AutoSize = True
        Me.pnl_CashierMessages.Controls.Add(Me.FlowLayoutPanel3)
        Me.pnl_CashierMessages.Controls.Add(Me.pnl_MainChat)
        Me.pnl_CashierMessages.Location = New System.Drawing.Point(0, 0)
        Me.pnl_CashierMessages.Name = "pnl_CashierMessages"
        Me.pnl_CashierMessages.Size = New System.Drawing.Size(1241, 844)
        Me.pnl_CashierMessages.TabIndex = 22
        Me.pnl_CashierMessages.Visible = False
        '
        'FlowLayoutPanel3
        '
        Me.FlowLayoutPanel3.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.FlowLayoutPanel3.Location = New System.Drawing.Point(27, 71)
        Me.FlowLayoutPanel3.Name = "FlowLayoutPanel3"
        Me.FlowLayoutPanel3.Size = New System.Drawing.Size(348, 535)
        Me.FlowLayoutPanel3.TabIndex = 9
        '
        'pnl_MainChat
        '
        Me.pnl_MainChat.BackColor = System.Drawing.SystemColors.ControlLight
        Me.pnl_MainChat.Controls.Add(Me.Guna2HtmlLabel60)
        Me.pnl_MainChat.Controls.Add(Me.flpMessages)
        Me.pnl_MainChat.Controls.Add(Me.btnSend)
        Me.pnl_MainChat.Controls.Add(Me.CashierName)
        Me.pnl_MainChat.Controls.Add(Me.txtChat)
        Me.pnl_MainChat.Location = New System.Drawing.Point(387, 71)
        Me.pnl_MainChat.Name = "pnl_MainChat"
        Me.pnl_MainChat.Size = New System.Drawing.Size(586, 535)
        Me.pnl_MainChat.TabIndex = 7
        '
        'Guna2HtmlLabel60
        '
        Me.Guna2HtmlLabel60.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel60.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel60.ForeColor = System.Drawing.Color.DimGray
        Me.Guna2HtmlLabel60.Location = New System.Drawing.Point(10, 39)
        Me.Guna2HtmlLabel60.Name = "Guna2HtmlLabel60"
        Me.Guna2HtmlLabel60.Size = New System.Drawing.Size(59, 15)
        Me.Guna2HtmlLabel60.TabIndex = 22
        Me.Guna2HtmlLabel60.Text = "Terminal #1"
        '
        'flpMessages
        '
        Me.flpMessages.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flpMessages.AutoScroll = True
        Me.flpMessages.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.flpMessages.Controls.Add(Me.flpMessagesdsds)
        Me.flpMessages.Controls.Add(Me.lbltimerSender)
        Me.flpMessages.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.flpMessages.Location = New System.Drawing.Point(0, 60)
        Me.flpMessages.Name = "flpMessages"
        Me.flpMessages.Size = New System.Drawing.Size(586, 408)
        Me.flpMessages.TabIndex = 6
        Me.flpMessages.WrapContents = False
        '
        'flpMessagesdsds
        '
        Me.flpMessagesdsds.BackColor = System.Drawing.Color.Transparent
        Me.flpMessagesdsds.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.flpMessagesdsds.ForeColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.flpMessagesdsds.Location = New System.Drawing.Point(3, 3)
        Me.flpMessagesdsds.Name = "flpMessagesdsds"
        Me.flpMessagesdsds.Size = New System.Drawing.Size(45, 22)
        Me.flpMessagesdsds.TabIndex = 23
        Me.flpMessagesdsds.Text = "Name"
        Me.flpMessagesdsds.Visible = False
        '
        'lbltimerSender
        '
        Me.lbltimerSender.BackColor = System.Drawing.Color.Transparent
        Me.lbltimerSender.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltimerSender.ForeColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.lbltimerSender.Location = New System.Drawing.Point(3, 31)
        Me.lbltimerSender.Name = "lbltimerSender"
        Me.lbltimerSender.Size = New System.Drawing.Size(25, 15)
        Me.lbltimerSender.TabIndex = 24
        Me.lbltimerSender.Text = "timer"
        Me.lbltimerSender.Visible = False
        '
        'btnSend
        '
        Me.btnSend.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSend.Animated = True
        Me.btnSend.BorderColor = System.Drawing.Color.Transparent
        Me.btnSend.BorderRadius = 5
        Me.btnSend.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnSend.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnSend.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnSend.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnSend.FillColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.btnSend.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSend.ForeColor = System.Drawing.Color.White
        Me.btnSend.Location = New System.Drawing.Point(471, 484)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(105, 42)
        Me.btnSend.TabIndex = 1
        Me.btnSend.Text = "Send"
        '
        'CashierName
        '
        Me.CashierName.BackColor = System.Drawing.Color.Transparent
        Me.CashierName.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CashierName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.CashierName.Location = New System.Drawing.Point(10, 14)
        Me.CashierName.Name = "CashierName"
        Me.CashierName.Size = New System.Drawing.Size(59, 27)
        Me.CashierName.TabIndex = 22
        Me.CashierName.Text = "Name"
        '
        'txtChat
        '
        Me.txtChat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtChat.BorderColor = System.Drawing.Color.Gray
        Me.txtChat.BorderRadius = 5
        Me.txtChat.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtChat.DefaultText = ""
        Me.txtChat.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtChat.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtChat.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtChat.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtChat.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtChat.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChat.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtChat.Location = New System.Drawing.Point(10, 484)
        Me.txtChat.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtChat.Name = "txtChat"
        Me.txtChat.PlaceholderForeColor = System.Drawing.Color.DimGray
        Me.txtChat.PlaceholderText = "Type message..."
        Me.txtChat.SelectedText = ""
        Me.txtChat.Size = New System.Drawing.Size(455, 42)
        Me.txtChat.TabIndex = 0
        '
        'dashbrd_pnl
        '
        Me.dashbrd_pnl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dashbrd_pnl.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.dashbrd_pnl.Controls.Add(Me.cboDashPeriod)
        Me.dashbrd_pnl.Controls.Add(Me.pnlDashRecent)
        Me.dashbrd_pnl.Controls.Add(Me.pnlDashAlert)
        Me.dashbrd_pnl.Controls.Add(Me.pnlDashTop)
        Me.dashbrd_pnl.Controls.Add(Me.pnlDashCat)
        Me.dashbrd_pnl.Controls.Add(Me.pnlDashTrend)
        Me.dashbrd_pnl.Controls.Add(Me.pnlDK1)
        Me.dashbrd_pnl.Controls.Add(Me.pnlDK2)
        Me.dashbrd_pnl.Controls.Add(Me.pnlDK3)
        Me.dashbrd_pnl.Controls.Add(Me.pnlDK4)
        Me.dashbrd_pnl.Controls.Add(Me.btnDashExport)
        Me.dashbrd_pnl.Controls.Add(Me.lblDashSub)
        Me.dashbrd_pnl.Controls.Add(Me.lblDashGreeting)
        Me.dashbrd_pnl.Location = New System.Drawing.Point(0, 0)
        Me.dashbrd_pnl.Name = "dashbrd_pnl"
        Me.dashbrd_pnl.Size = New System.Drawing.Size(1241, 844)
        Me.dashbrd_pnl.TabIndex = 23
        Me.dashbrd_pnl.Visible = False
        '
        'cboDashPeriod
        '
        Me.cboDashPeriod.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboDashPeriod.BackColor = System.Drawing.Color.Transparent
        Me.cboDashPeriod.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.cboDashPeriod.BorderRadius = 10
        Me.cboDashPeriod.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cboDashPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDashPeriod.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.cboDashPeriod.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.cboDashPeriod.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.cboDashPeriod.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDashPeriod.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.cboDashPeriod.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.cboDashPeriod.ItemHeight = 30
        Me.cboDashPeriod.Location = New System.Drawing.Point(731, 14)
        Me.cboDashPeriod.Name = "cboDashPeriod"
        Me.cboDashPeriod.Size = New System.Drawing.Size(130, 36)
        Me.cboDashPeriod.TabIndex = 1702
        '
        'pnlDashRecent
        '
        Me.pnlDashRecent.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.pnlDashRecent.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.pnlDashRecent.BorderRadius = 14
        Me.pnlDashRecent.BorderThickness = 1
        Me.pnlDashRecent.Controls.Add(Me.dashGrid)
        Me.pnlDashRecent.Controls.Add(Me.lnkDashViewAll)
        Me.pnlDashRecent.Controls.Add(Me.lblDashRecentTitle)
        Me.pnlDashRecent.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlDashRecent.Location = New System.Drawing.Point(20, 388)
        Me.pnlDashRecent.Name = "pnlDashRecent"
        Me.pnlDashRecent.Size = New System.Drawing.Size(590, 262)
        Me.pnlDashRecent.TabIndex = 1334
        '
        'dashGrid
        '
        Me.dashGrid.AllowUserToAddRows = False
        Me.dashGrid.AllowUserToDeleteRows = False
        Me.dashGrid.AllowUserToResizeRows = False
        Me.dashGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dashGrid.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.dashGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dashGrid.Location = New System.Drawing.Point(14, 38)
        Me.dashGrid.Name = "dashGrid"
        Me.dashGrid.ReadOnly = True
        Me.dashGrid.RowHeadersVisible = False
        Me.dashGrid.Size = New System.Drawing.Size(588, 208)
        Me.dashGrid.TabIndex = 1337
        '
        'lnkDashViewAll
        '
        Me.lnkDashViewAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lnkDashViewAll.BackColor = System.Drawing.Color.Transparent
        Me.lnkDashViewAll.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lnkDashViewAll.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkDashViewAll.ForeColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.lnkDashViewAll.Location = New System.Drawing.Point(450, 18)
        Me.lnkDashViewAll.Name = "lnkDashViewAll"
        Me.lnkDashViewAll.Size = New System.Drawing.Size(124, 16)
        Me.lnkDashViewAll.TabIndex = 1336
        Me.lnkDashViewAll.Text = "View all orders →"
        Me.lnkDashViewAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDashRecentTitle
        '
        Me.lblDashRecentTitle.AutoSize = True
        Me.lblDashRecentTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblDashRecentTitle.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDashRecentTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblDashRecentTitle.Location = New System.Drawing.Point(16, 14)
        Me.lblDashRecentTitle.Name = "lblDashRecentTitle"
        Me.lblDashRecentTitle.Size = New System.Drawing.Size(106, 20)
        Me.lblDashRecentTitle.TabIndex = 1335
        Me.lblDashRecentTitle.Text = "Recent orders"
        '
        'pnlDashAlert
        '
        Me.pnlDashAlert.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.pnlDashAlert.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.pnlDashAlert.BorderRadius = 14
        Me.pnlDashAlert.BorderThickness = 1
        Me.pnlDashAlert.Controls.Add(Me.flDashAlert)
        Me.pnlDashAlert.Controls.Add(Me.lblDashAlertNote)
        Me.pnlDashAlert.Controls.Add(Me.lblDashAlertTitle)
        Me.pnlDashAlert.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlDashAlert.Location = New System.Drawing.Point(624, 526)
        Me.pnlDashAlert.Name = "pnlDashAlert"
        Me.pnlDashAlert.Size = New System.Drawing.Size(363, 124)
        Me.pnlDashAlert.TabIndex = 1342
        '
        'flDashAlert
        '
        Me.flDashAlert.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flDashAlert.AutoScroll = True
        Me.flDashAlert.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.flDashAlert.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.flDashAlert.Location = New System.Drawing.Point(16, 38)
        Me.flDashAlert.Name = "flDashAlert"
        Me.flDashAlert.Size = New System.Drawing.Size(331, 78)
        Me.flDashAlert.TabIndex = 1345
        Me.flDashAlert.WrapContents = False
        '
        'lblDashAlertNote
        '
        Me.lblDashAlertNote.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDashAlertNote.BackColor = System.Drawing.Color.Transparent
        Me.lblDashAlertNote.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDashAlertNote.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblDashAlertNote.Location = New System.Drawing.Point(200, 15)
        Me.lblDashAlertNote.Name = "lblDashAlertNote"
        Me.lblDashAlertNote.Size = New System.Drawing.Size(147, 16)
        Me.lblDashAlertNote.TabIndex = 1344
        Me.lblDashAlertNote.Text = "0 need attention"
        Me.lblDashAlertNote.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDashAlertTitle
        '
        Me.lblDashAlertTitle.AutoSize = True
        Me.lblDashAlertTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblDashAlertTitle.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDashAlertTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblDashAlertTitle.Location = New System.Drawing.Point(16, 12)
        Me.lblDashAlertTitle.Name = "lblDashAlertTitle"
        Me.lblDashAlertTitle.Size = New System.Drawing.Size(115, 19)
        Me.lblDashAlertTitle.TabIndex = 1343
        Me.lblDashAlertTitle.Text = "Inventory alerts"
        '
        'pnlDashTop
        '
        Me.pnlDashTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.pnlDashTop.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.pnlDashTop.BorderRadius = 14
        Me.pnlDashTop.BorderThickness = 1
        Me.pnlDashTop.Controls.Add(Me.flDashTop)
        Me.pnlDashTop.Controls.Add(Me.lblDashTopNote)
        Me.pnlDashTop.Controls.Add(Me.lblDashTopTitle)
        Me.pnlDashTop.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlDashTop.Location = New System.Drawing.Point(624, 388)
        Me.pnlDashTop.Name = "pnlDashTop"
        Me.pnlDashTop.Size = New System.Drawing.Size(363, 124)
        Me.pnlDashTop.TabIndex = 1338
        '
        'flDashTop
        '
        Me.flDashTop.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flDashTop.AutoScroll = True
        Me.flDashTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.flDashTop.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.flDashTop.Location = New System.Drawing.Point(16, 38)
        Me.flDashTop.Name = "flDashTop"
        Me.flDashTop.Size = New System.Drawing.Size(331, 80)
        Me.flDashTop.TabIndex = 1341
        Me.flDashTop.WrapContents = False
        '
        'lblDashTopNote
        '
        Me.lblDashTopNote.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDashTopNote.BackColor = System.Drawing.Color.Transparent
        Me.lblDashTopNote.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDashTopNote.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblDashTopNote.Location = New System.Drawing.Point(250, 15)
        Me.lblDashTopNote.Name = "lblDashTopNote"
        Me.lblDashTopNote.Size = New System.Drawing.Size(97, 16)
        Me.lblDashTopNote.TabIndex = 1340
        Me.lblDashTopNote.Text = "Today"
        Me.lblDashTopNote.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDashTopTitle
        '
        Me.lblDashTopTitle.AutoSize = True
        Me.lblDashTopTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblDashTopTitle.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDashTopTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblDashTopTitle.Location = New System.Drawing.Point(16, 12)
        Me.lblDashTopTitle.Name = "lblDashTopTitle"
        Me.lblDashTopTitle.Size = New System.Drawing.Size(123, 19)
        Me.lblDashTopTitle.TabIndex = 1339
        Me.lblDashTopTitle.Text = "Top-selling items"
        '
        'pnlDashCat
        '
        Me.pnlDashCat.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.pnlDashCat.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.pnlDashCat.BorderRadius = 14
        Me.pnlDashCat.BorderThickness = 1
        Me.pnlDashCat.Controls.Add(Me.flDashCat)
        Me.pnlDashCat.Controls.Add(Me.lblDashCatNote)
        Me.pnlDashCat.Controls.Add(Me.lblDashCatTitle)
        Me.pnlDashCat.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlDashCat.Location = New System.Drawing.Point(624, 174)
        Me.pnlDashCat.Name = "pnlDashCat"
        Me.pnlDashCat.Size = New System.Drawing.Size(363, 200)
        Me.pnlDashCat.TabIndex = 1330
        '
        'flDashCat
        '
        Me.flDashCat.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flDashCat.AutoScroll = True
        Me.flDashCat.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.flDashCat.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.flDashCat.Location = New System.Drawing.Point(16, 44)
        Me.flDashCat.Name = "flDashCat"
        Me.flDashCat.Size = New System.Drawing.Size(331, 146)
        Me.flDashCat.TabIndex = 1333
        Me.flDashCat.WrapContents = False
        '
        'lblDashCatNote
        '
        Me.lblDashCatNote.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDashCatNote.BackColor = System.Drawing.Color.Transparent
        Me.lblDashCatNote.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDashCatNote.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblDashCatNote.Location = New System.Drawing.Point(250, 18)
        Me.lblDashCatNote.Name = "lblDashCatNote"
        Me.lblDashCatNote.Size = New System.Drawing.Size(97, 16)
        Me.lblDashCatNote.TabIndex = 1332
        Me.lblDashCatNote.Text = "Item sales today"
        Me.lblDashCatNote.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDashCatTitle
        '
        Me.lblDashCatTitle.AutoSize = True
        Me.lblDashCatTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblDashCatTitle.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDashCatTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblDashCatTitle.Location = New System.Drawing.Point(16, 14)
        Me.lblDashCatTitle.Name = "lblDashCatTitle"
        Me.lblDashCatTitle.Size = New System.Drawing.Size(167, 20)
        Me.lblDashCatTitle.TabIndex = 1331
        Me.lblDashCatTitle.Text = "Category performance"
        '
        'pnlDashTrend
        '
        Me.pnlDashTrend.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.pnlDashTrend.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.pnlDashTrend.BorderRadius = 14
        Me.pnlDashTrend.BorderThickness = 1
        Me.pnlDashTrend.Controls.Add(Me.picDashTrend)
        Me.pnlDashTrend.Controls.Add(Me.lblDashLegToday)
        Me.pnlDashTrend.Controls.Add(Me.lblDashLegYest)
        Me.pnlDashTrend.Controls.Add(Me.lblDashTrendSub)
        Me.pnlDashTrend.Controls.Add(Me.lblDashTrendTitle)
        Me.pnlDashTrend.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlDashTrend.Location = New System.Drawing.Point(20, 174)
        Me.pnlDashTrend.Name = "pnlDashTrend"
        Me.pnlDashTrend.Size = New System.Drawing.Size(590, 200)
        Me.pnlDashTrend.TabIndex = 1324
        '
        'picDashTrend
        '
        Me.picDashTrend.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picDashTrend.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.picDashTrend.Location = New System.Drawing.Point(14, 58)
        Me.picDashTrend.Name = "picDashTrend"
        Me.picDashTrend.Size = New System.Drawing.Size(562, 132)
        Me.picDashTrend.TabIndex = 1329
        Me.picDashTrend.TabStop = False
        '
        'lblDashLegToday
        '
        Me.lblDashLegToday.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDashLegToday.AutoSize = True
        Me.lblDashLegToday.BackColor = System.Drawing.Color.Transparent
        Me.lblDashLegToday.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDashLegToday.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblDashLegToday.Location = New System.Drawing.Point(440, 16)
        Me.lblDashLegToday.Name = "lblDashLegToday"
        Me.lblDashLegToday.Size = New System.Drawing.Size(46, 13)
        Me.lblDashLegToday.TabIndex = 1328
        Me.lblDashLegToday.Text = "● Today"
        '
        'lblDashLegYest
        '
        Me.lblDashLegYest.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDashLegYest.AutoSize = True
        Me.lblDashLegYest.BackColor = System.Drawing.Color.Transparent
        Me.lblDashLegYest.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDashLegYest.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblDashLegYest.Location = New System.Drawing.Point(500, 16)
        Me.lblDashLegYest.Name = "lblDashLegYest"
        Me.lblDashLegYest.Size = New System.Drawing.Size(64, 13)
        Me.lblDashLegYest.TabIndex = 1327
        Me.lblDashLegYest.Text = "● Yesterday"
        '
        'lblDashTrendSub
        '
        Me.lblDashTrendSub.AutoSize = True
        Me.lblDashTrendSub.BackColor = System.Drawing.Color.Transparent
        Me.lblDashTrendSub.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDashTrendSub.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblDashTrendSub.Location = New System.Drawing.Point(16, 36)
        Me.lblDashTrendSub.Name = "lblDashTrendSub"
        Me.lblDashTrendSub.Size = New System.Drawing.Size(244, 13)
        Me.lblDashTrendSub.TabIndex = 1326
        Me.lblDashTrendSub.Text = "Cumulative sales by hour  -  today vs yesterday"
        '
        'lblDashTrendTitle
        '
        Me.lblDashTrendTitle.AutoSize = True
        Me.lblDashTrendTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblDashTrendTitle.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDashTrendTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblDashTrendTitle.Location = New System.Drawing.Point(16, 14)
        Me.lblDashTrendTitle.Name = "lblDashTrendTitle"
        Me.lblDashTrendTitle.Size = New System.Drawing.Size(86, 20)
        Me.lblDashTrendTitle.TabIndex = 1325
        Me.lblDashTrendTitle.Text = "Sales trend"
        '
        'pnlDK1
        '
        Me.pnlDK1.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.pnlDK1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.pnlDK1.BorderRadius = 14
        Me.pnlDK1.BorderThickness = 1
        Me.pnlDK1.Controls.Add(Me.picDK1)
        Me.pnlDK1.Controls.Add(Me.lblDK1S)
        Me.pnlDK1.Controls.Add(Me.lblDK1V)
        Me.pnlDK1.Controls.Add(Me.lblDK1T)
        Me.pnlDK1.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlDK1.Location = New System.Drawing.Point(20, 78)
        Me.pnlDK1.Name = "pnlDK1"
        Me.pnlDK1.Size = New System.Drawing.Size(231, 84)
        Me.pnlDK1.TabIndex = 1304
        '
        'picDK1
        '
        Me.picDK1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picDK1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.picDK1.BorderRadius = 10
        Me.picDK1.FillColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.picDK1.ImageRotate = 0!
        Me.picDK1.Location = New System.Drawing.Point(183, 12)
        Me.picDK1.Name = "picDK1"
        Me.picDK1.Size = New System.Drawing.Size(34, 34)
        Me.picDK1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picDK1.TabIndex = 1308
        Me.picDK1.TabStop = False
        '
        'lblDK1S
        '
        Me.lblDK1S.AutoSize = True
        Me.lblDK1S.BackColor = System.Drawing.Color.Transparent
        Me.lblDK1S.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDK1S.ForeColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.lblDK1S.Location = New System.Drawing.Point(140, 46)
        Me.lblDK1S.Name = "lblDK1S"
        Me.lblDK1S.Size = New System.Drawing.Size(0, 15)
        Me.lblDK1S.TabIndex = 1307
        '
        'lblDK1V
        '
        Me.lblDK1V.AutoSize = True
        Me.lblDK1V.BackColor = System.Drawing.Color.Transparent
        Me.lblDK1V.Font = New System.Drawing.Font("Segoe UI", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDK1V.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblDK1V.Location = New System.Drawing.Point(14, 34)
        Me.lblDK1V.Name = "lblDK1V"
        Me.lblDK1V.Size = New System.Drawing.Size(33, 37)
        Me.lblDK1V.TabIndex = 1306
        Me.lblDK1V.Text = "0"
        '
        'lblDK1T
        '
        Me.lblDK1T.AutoSize = True
        Me.lblDK1T.BackColor = System.Drawing.Color.Transparent
        Me.lblDK1T.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDK1T.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblDK1T.Location = New System.Drawing.Point(14, 12)
        Me.lblDK1T.Name = "lblDK1T"
        Me.lblDK1T.Size = New System.Drawing.Size(66, 15)
        Me.lblDK1T.TabIndex = 1305
        Me.lblDK1T.Text = "Sales today"
        '
        'pnlDK2
        '
        Me.pnlDK2.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.pnlDK2.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.pnlDK2.BorderRadius = 14
        Me.pnlDK2.BorderThickness = 1
        Me.pnlDK2.Controls.Add(Me.picDK2)
        Me.pnlDK2.Controls.Add(Me.lblDK2S)
        Me.pnlDK2.Controls.Add(Me.lblDK2V)
        Me.pnlDK2.Controls.Add(Me.lblDK2T)
        Me.pnlDK2.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlDK2.Location = New System.Drawing.Point(265, 78)
        Me.pnlDK2.Name = "pnlDK2"
        Me.pnlDK2.Size = New System.Drawing.Size(231, 84)
        Me.pnlDK2.TabIndex = 1309
        '
        'picDK2
        '
        Me.picDK2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picDK2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.picDK2.BorderRadius = 10
        Me.picDK2.FillColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.picDK2.ImageRotate = 0!
        Me.picDK2.Location = New System.Drawing.Point(183, 12)
        Me.picDK2.Name = "picDK2"
        Me.picDK2.Size = New System.Drawing.Size(34, 34)
        Me.picDK2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picDK2.TabIndex = 1313
        Me.picDK2.TabStop = False
        '
        'lblDK2S
        '
        Me.lblDK2S.AutoSize = True
        Me.lblDK2S.BackColor = System.Drawing.Color.Transparent
        Me.lblDK2S.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDK2S.ForeColor = System.Drawing.Color.FromArgb(CType(CType(92, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(84, Byte), Integer))
        Me.lblDK2S.Location = New System.Drawing.Point(140, 46)
        Me.lblDK2S.Name = "lblDK2S"
        Me.lblDK2S.Size = New System.Drawing.Size(0, 15)
        Me.lblDK2S.TabIndex = 1312
        '
        'lblDK2V
        '
        Me.lblDK2V.AutoSize = True
        Me.lblDK2V.BackColor = System.Drawing.Color.Transparent
        Me.lblDK2V.Font = New System.Drawing.Font("Segoe UI", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDK2V.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblDK2V.Location = New System.Drawing.Point(14, 34)
        Me.lblDK2V.Name = "lblDK2V"
        Me.lblDK2V.Size = New System.Drawing.Size(33, 37)
        Me.lblDK2V.TabIndex = 1311
        Me.lblDK2V.Text = "0"
        '
        'lblDK2T
        '
        Me.lblDK2T.AutoSize = True
        Me.lblDK2T.BackColor = System.Drawing.Color.Transparent
        Me.lblDK2T.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDK2T.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblDK2T.Location = New System.Drawing.Point(14, 12)
        Me.lblDK2T.Name = "lblDK2T"
        Me.lblDK2T.Size = New System.Drawing.Size(42, 15)
        Me.lblDK2T.TabIndex = 1310
        Me.lblDK2T.Text = "Orders"
        '
        'pnlDK3
        '
        Me.pnlDK3.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.pnlDK3.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.pnlDK3.BorderRadius = 14
        Me.pnlDK3.BorderThickness = 1
        Me.pnlDK3.Controls.Add(Me.picDK3)
        Me.pnlDK3.Controls.Add(Me.lblDK3S)
        Me.pnlDK3.Controls.Add(Me.lblDK3V)
        Me.pnlDK3.Controls.Add(Me.lblDK3T)
        Me.pnlDK3.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlDK3.Location = New System.Drawing.Point(510, 78)
        Me.pnlDK3.Name = "pnlDK3"
        Me.pnlDK3.Size = New System.Drawing.Size(231, 84)
        Me.pnlDK3.TabIndex = 1314
        '
        'picDK3
        '
        Me.picDK3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picDK3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.picDK3.BorderRadius = 10
        Me.picDK3.FillColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.picDK3.ImageRotate = 0!
        Me.picDK3.Location = New System.Drawing.Point(183, 12)
        Me.picDK3.Name = "picDK3"
        Me.picDK3.Size = New System.Drawing.Size(34, 34)
        Me.picDK3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picDK3.TabIndex = 1318
        Me.picDK3.TabStop = False
        '
        'lblDK3S
        '
        Me.lblDK3S.AutoSize = True
        Me.lblDK3S.BackColor = System.Drawing.Color.Transparent
        Me.lblDK3S.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDK3S.ForeColor = System.Drawing.Color.FromArgb(CType(CType(92, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(84, Byte), Integer))
        Me.lblDK3S.Location = New System.Drawing.Point(140, 46)
        Me.lblDK3S.Name = "lblDK3S"
        Me.lblDK3S.Size = New System.Drawing.Size(0, 15)
        Me.lblDK3S.TabIndex = 1317
        '
        'lblDK3V
        '
        Me.lblDK3V.AutoSize = True
        Me.lblDK3V.BackColor = System.Drawing.Color.Transparent
        Me.lblDK3V.Font = New System.Drawing.Font("Segoe UI", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDK3V.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblDK3V.Location = New System.Drawing.Point(14, 34)
        Me.lblDK3V.Name = "lblDK3V"
        Me.lblDK3V.Size = New System.Drawing.Size(33, 37)
        Me.lblDK3V.TabIndex = 1316
        Me.lblDK3V.Text = "0"
        '
        'lblDK3T
        '
        Me.lblDK3T.AutoSize = True
        Me.lblDK3T.BackColor = System.Drawing.Color.Transparent
        Me.lblDK3T.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDK3T.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblDK3T.Location = New System.Drawing.Point(14, 12)
        Me.lblDK3T.Name = "lblDK3T"
        Me.lblDK3T.Size = New System.Drawing.Size(61, 15)
        Me.lblDK3T.TabIndex = 1315
        Me.lblDK3T.Text = "Items sold"
        '
        'pnlDK4
        '
        Me.pnlDK4.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.pnlDK4.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.pnlDK4.BorderRadius = 14
        Me.pnlDK4.BorderThickness = 1
        Me.pnlDK4.Controls.Add(Me.picDK4)
        Me.pnlDK4.Controls.Add(Me.lblDK4S)
        Me.pnlDK4.Controls.Add(Me.lblDK4V)
        Me.pnlDK4.Controls.Add(Me.lblDK4T)
        Me.pnlDK4.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlDK4.Location = New System.Drawing.Point(755, 78)
        Me.pnlDK4.Name = "pnlDK4"
        Me.pnlDK4.Size = New System.Drawing.Size(231, 84)
        Me.pnlDK4.TabIndex = 1319
        '
        'picDK4
        '
        Me.picDK4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picDK4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.picDK4.BorderRadius = 10
        Me.picDK4.FillColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.picDK4.ImageRotate = 0!
        Me.picDK4.Location = New System.Drawing.Point(183, 12)
        Me.picDK4.Name = "picDK4"
        Me.picDK4.Size = New System.Drawing.Size(34, 34)
        Me.picDK4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picDK4.TabIndex = 1323
        Me.picDK4.TabStop = False
        '
        'lblDK4S
        '
        Me.lblDK4S.AutoSize = True
        Me.lblDK4S.BackColor = System.Drawing.Color.Transparent
        Me.lblDK4S.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDK4S.ForeColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.lblDK4S.Location = New System.Drawing.Point(140, 46)
        Me.lblDK4S.Name = "lblDK4S"
        Me.lblDK4S.Size = New System.Drawing.Size(0, 15)
        Me.lblDK4S.TabIndex = 1322
        '
        'lblDK4V
        '
        Me.lblDK4V.AutoSize = True
        Me.lblDK4V.BackColor = System.Drawing.Color.Transparent
        Me.lblDK4V.Font = New System.Drawing.Font("Segoe UI", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDK4V.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblDK4V.Location = New System.Drawing.Point(14, 34)
        Me.lblDK4V.Name = "lblDK4V"
        Me.lblDK4V.Size = New System.Drawing.Size(33, 37)
        Me.lblDK4V.TabIndex = 1321
        Me.lblDK4V.Text = "0"
        '
        'lblDK4T
        '
        Me.lblDK4T.AutoSize = True
        Me.lblDK4T.BackColor = System.Drawing.Color.Transparent
        Me.lblDK4T.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDK4T.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblDK4T.Location = New System.Drawing.Point(14, 12)
        Me.lblDK4T.Name = "lblDK4T"
        Me.lblDK4T.Size = New System.Drawing.Size(81, 15)
        Me.lblDK4T.TabIndex = 1320
        Me.lblDK4T.Text = "Average order"
        '
        'btnDashExport
        '
        Me.btnDashExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDashExport.Animated = True
        Me.btnDashExport.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.btnDashExport.BorderColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.btnDashExport.BorderRadius = 10
        Me.btnDashExport.BorderThickness = 1
        Me.btnDashExport.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDashExport.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnDashExport.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDashExport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btnDashExport.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.btnDashExport.HoverState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btnDashExport.Location = New System.Drawing.Point(871, 14)
        Me.btnDashExport.Name = "btnDashExport"
        Me.btnDashExport.Size = New System.Drawing.Size(120, 38)
        Me.btnDashExport.TabIndex = 1303
        Me.btnDashExport.Text = "Export report"
        '
        'lblDashSub
        '
        Me.lblDashSub.AutoSize = True
        Me.lblDashSub.BackColor = System.Drawing.Color.Transparent
        Me.lblDashSub.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDashSub.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblDashSub.Location = New System.Drawing.Point(20, 46)
        Me.lblDashSub.Name = "lblDashSub"
        Me.lblDashSub.Size = New System.Drawing.Size(211, 15)
        Me.lblDashSub.TabIndex = 1302
        Me.lblDashSub.Text = "Here's how Forest Roast is doing today"
        '
        'lblDashGreeting
        '
        Me.lblDashGreeting.AutoSize = True
        Me.lblDashGreeting.BackColor = System.Drawing.Color.Transparent
        Me.lblDashGreeting.Font = New System.Drawing.Font("Segoe UI", 17.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDashGreeting.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.lblDashGreeting.Location = New System.Drawing.Point(20, 12)
        Me.lblDashGreeting.Name = "lblDashGreeting"
        Me.lblDashGreeting.Size = New System.Drawing.Size(171, 31)
        Me.lblDashGreeting.TabIndex = 1301
        Me.lblDashGreeting.Text = "Good morning"
        '
        'dashboard_lbl
        '
        Me.dashboard_lbl.BackColor = System.Drawing.Color.Transparent
        Me.dashboard_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dashboard_lbl.Location = New System.Drawing.Point(12, 21)
        Me.dashboard_lbl.Name = "dashboard_lbl"
        Me.dashboard_lbl.Size = New System.Drawing.Size(256, 31)
        Me.dashboard_lbl.TabIndex = 10
        Me.dashboard_lbl.Text = "Dashboard Catalogue"
        '
        'Cashier
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1440, 844)
        Me.Controls.Add(Me.main_pnl)
        Me.Controls.Add(Me.dshbrd_Pnl)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Cashier"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cashier"
        Me.dshbrd_Pnl.ResumeLayout(False)
        Me.dshbrd_Pnl.PerformLayout()
        Me.pnlRegister.ResumeLayout(False)
        Me.pnlRegister.PerformLayout()
        Me.main_pnl.ResumeLayout(False)
        Me.main_pnl.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlInvAttn.ResumeLayout(False)
        Me.pnlInvAttn.PerformLayout()
        Me.pnlInvCat.ResumeLayout(False)
        Me.pnlInvCat.PerformLayout()
        CType(Me.picInvCatBar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlInvTable.ResumeLayout(False)
        Me.pnlInvTable.PerformLayout()
        CType(Me.invGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlIK1.ResumeLayout(False)
        Me.pnlIK1.PerformLayout()
        CType(Me.picIK1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlIK2.ResumeLayout(False)
        Me.pnlIK2.PerformLayout()
        CType(Me.picIK2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlIK3.ResumeLayout(False)
        Me.pnlIK3.PerformLayout()
        CType(Me.picIK3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlIK4.ResumeLayout(False)
        Me.pnlIK4.PerformLayout()
        CType(Me.picIK4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_History.ResumeLayout(False)
        Me.pnl_History.PerformLayout()
        Me.pnlHistDetail.ResumeLayout(False)
        Me.pnlHistDetail.PerformLayout()
        Me.pnlDetTotal.ResumeLayout(False)
        Me.pnlDetTotal.PerformLayout()
        Me.Guna2Panel15.ResumeLayout(False)
        Me.Guna2Panel15.PerformLayout()
        CType(Me.histGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHK1.ResumeLayout(False)
        Me.pnlHK1.PerformLayout()
        CType(Me.picHK1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHK2.ResumeLayout(False)
        Me.pnlHK2.PerformLayout()
        CType(Me.picHK2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHK3.ResumeLayout(False)
        Me.pnlHK3.PerformLayout()
        CType(Me.picHK3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHK4.ResumeLayout(False)
        Me.pnlHK4.PerformLayout()
        CType(Me.picHK4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCardDim.ResumeLayout(False)
        Me.pnlCardModal.ResumeLayout(False)
        Me.pnlCardModal.PerformLayout()
        Me.pnlCardAmount.ResumeLayout(False)
        Me.pnlCardAmount.PerformLayout()
        Me.pnl_PointOfSale.ResumeLayout(False)
        Me.pnl_PointOfSale.PerformLayout()
        Me.pnlProfileMenu.ResumeLayout(False)
        CType(Me.topimage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Guna2Panel5.ResumeLayout(False)
        Me.Guna2Panel5.PerformLayout()
        Me.pnlTotal.ResumeLayout(False)
        Me.pnlTotal.PerformLayout()
        Me.pnlUserChip.ResumeLayout(False)
        Me.pnlUserChip.PerformLayout()
        CType(Me.picAvatar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Guna2Panel6.ResumeLayout(False)
        Me.Guna2Panel6.PerformLayout()
        CType(Me.picCartIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_CashierMessages.ResumeLayout(False)
        Me.pnl_MainChat.ResumeLayout(False)
        Me.pnl_MainChat.PerformLayout()
        Me.flpMessages.ResumeLayout(False)
        Me.flpMessages.PerformLayout()
        Me.dashbrd_pnl.ResumeLayout(False)
        Me.dashbrd_pnl.PerformLayout()
        Me.pnlDashRecent.ResumeLayout(False)
        Me.pnlDashRecent.PerformLayout()
        CType(Me.dashGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDashAlert.ResumeLayout(False)
        Me.pnlDashAlert.PerformLayout()
        Me.pnlDashTop.ResumeLayout(False)
        Me.pnlDashTop.PerformLayout()
        Me.pnlDashCat.ResumeLayout(False)
        Me.pnlDashCat.PerformLayout()
        Me.pnlDashTrend.ResumeLayout(False)
        Me.pnlDashTrend.PerformLayout()
        CType(Me.picDashTrend, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDK1.ResumeLayout(False)
        Me.pnlDK1.PerformLayout()
        CType(Me.picDK1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDK2.ResumeLayout(False)
        Me.pnlDK2.PerformLayout()
        CType(Me.picDK2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDK3.ResumeLayout(False)
        Me.pnlDK3.PerformLayout()
        CType(Me.picDK3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDK4.ResumeLayout(False)
        Me.pnlDK4.PerformLayout()
        CType(Me.picDK4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dshbrd_Pnl As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents btn_DashBoard As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btn_invtry As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btn_Point_Of_Sale As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents main_pnl As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents dashboard_lbl As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2HtmlLabel5 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents btn_hstry As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Guna2HtmlLabel7 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents btnCashierLogout As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btn_CashierMessages As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents pnl_PointOfSale As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents btn_hotCoffee As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btn_All As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btn_IcedCoffee As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btn_Specialty As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Guna2Panel6 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Guna2HtmlLabel15 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2Panel5 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Guna2Panel8 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Guna2HtmlLabel19 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2HtmlLabel18 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2HtmlLabel17 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents txt_Cash_Receive As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents btn_Clear As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btn_chkout As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents lbl_Subtotal As System.Windows.Forms.Label
    Friend WithEvents lbl_Tax As System.Windows.Forms.Label
    Friend WithEvents lbl_Total As System.Windows.Forms.Label
    Friend WithEvents txt_SearchMenu As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents lbl_Change As System.Windows.Forms.Label
    Friend WithEvents Guna2HtmlLabel24 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents dashbrd_pnl As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents pnl_CashierMessages As Panel
    Friend WithEvents Guna2HtmlLabel60 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents CashierName As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents flpMessages As FlowLayoutPanel
    Friend WithEvents flpMessagesdsds As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents lbltimerSender As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents pnl_MainChat As Panel
    Friend WithEvents btnSend As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents txtChat As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents FlowLayoutPanel3 As FlowLayoutPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents fl_Menu As FlowLayoutPanel
    Friend WithEvents btn_Non_Coffee As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents pnl_History As Panel
    Friend WithEvents Guna2ComboBox2 As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents Guna2ComboBox1 As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents Guna2TextBox1 As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents Guna2Panel15 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Label16 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents fl_MenuProduct As FlowLayoutPanel
    Friend WithEvents topimage As Guna.UI2.WinForms.Guna2PictureBox
    Friend WithEvents lblWorkspace As System.Windows.Forms.Label
    Friend WithEvents pnlRegister As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblRegOpen As System.Windows.Forms.Label
    Friend WithEvents lblRegName As System.Windows.Forms.Label
    Friend WithEvents lblRegShift As System.Windows.Forms.Label
    Friend WithEvents btnBell As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents pnlUserChip As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents picAvatar As Guna.UI2.WinForms.Guna2PictureBox
    Friend WithEvents lblUserName As System.Windows.Forms.Label
    Friend WithEvents lblUserRole As System.Windows.Forms.Label
    Friend WithEvents lblChoose As System.Windows.Forms.Label
    Friend WithEvents lblSeeAll As System.Windows.Forms.Label
    Friend WithEvents lblMenuTitle As System.Windows.Forms.Label
    Friend WithEvents lblAvailable As System.Windows.Forms.Label
    Friend WithEvents picCartIcon As Guna.UI2.WinForms.Guna2PictureBox
    Friend WithEvents lblCurrentOrder As System.Windows.Forms.Label
    Friend WithEvents lblCartCount As System.Windows.Forms.Label
    Friend WithEvents lblEmpty As System.Windows.Forms.Label
    Friend WithEvents pnlTotal As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblTotalSub As System.Windows.Forms.Label
    Friend WithEvents lblPayCap As System.Windows.Forms.Label
    Friend WithEvents tileCash As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents tileCard As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents lblChevron As System.Windows.Forms.Label
    Friend WithEvents pnlProfileMenu As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents btnMenuSettings As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnMenuProfile As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnMenuLogout As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnHistExport As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnHistNewOrder As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents pnlHK1 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblHK1T As System.Windows.Forms.Label
    Friend WithEvents lblHK1V As System.Windows.Forms.Label
    Friend WithEvents lblHK1S As System.Windows.Forms.Label
    Friend WithEvents picHK1 As Guna.UI2.WinForms.Guna2PictureBox
    Friend WithEvents pnlHK2 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblHK2T As System.Windows.Forms.Label
    Friend WithEvents lblHK2V As System.Windows.Forms.Label
    Friend WithEvents lblHK2S As System.Windows.Forms.Label
    Friend WithEvents picHK2 As Guna.UI2.WinForms.Guna2PictureBox
    Friend WithEvents pnlHK3 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblHK3T As System.Windows.Forms.Label
    Friend WithEvents lblHK3V As System.Windows.Forms.Label
    Friend WithEvents lblHK3S As System.Windows.Forms.Label
    Friend WithEvents picHK3 As Guna.UI2.WinForms.Guna2PictureBox
    Friend WithEvents pnlHK4 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblHK4T As System.Windows.Forms.Label
    Friend WithEvents lblHK4V As System.Windows.Forms.Label
    Friend WithEvents lblHK4S As System.Windows.Forms.Label
    Friend WithEvents picHK4 As Guna.UI2.WinForms.Guna2PictureBox
    Friend WithEvents dtpHistory As Guna.UI2.WinForms.Guna2DateTimePicker
    Friend WithEvents lblHistCount As System.Windows.Forms.Label
    Friend WithEvents histGrid As System.Windows.Forms.DataGridView
    Friend WithEvents lblHistShowing As System.Windows.Forms.Label
    Friend WithEvents pnlHistDetail As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblDetTitle As System.Windows.Forms.Label
    Friend WithEvents btnDetStatus As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents lblDetDate As System.Windows.Forms.Label
    Friend WithEvents btnDetPrint As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents lblDetSecA As System.Windows.Forms.Label
    Friend WithEvents lblDetCashierCap As System.Windows.Forms.Label
    Friend WithEvents lblDetCashier As System.Windows.Forms.Label
    Friend WithEvents lblDetPayCap As System.Windows.Forms.Label
    Friend WithEvents lblDetPay As System.Windows.Forms.Label
    Friend WithEvents lblDetSecB As System.Windows.Forms.Label
    Friend WithEvents flDetItems As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents lblDetSubCap As System.Windows.Forms.Label
    Friend WithEvents lblDetSub As System.Windows.Forms.Label
    Friend WithEvents lblDetTaxCap As System.Windows.Forms.Label
    Friend WithEvents lblDetTax As System.Windows.Forms.Label
    Friend WithEvents lblDetCash As System.Windows.Forms.Label
    Friend WithEvents pnlDetTotal As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblDetTotalCap As System.Windows.Forms.Label
    Friend WithEvents lblDetTotal As System.Windows.Forms.Label
    Friend WithEvents lblDetReceipt As System.Windows.Forms.Label
    Friend WithEvents lblDetEmpty As System.Windows.Forms.Label
    Friend WithEvents lblInvTitle As System.Windows.Forms.Label
    Friend WithEvents lblInvSub As System.Windows.Forms.Label
    Friend WithEvents btnInvExport As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents pnlIK1 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblIK1T As System.Windows.Forms.Label
    Friend WithEvents lblIK1V As System.Windows.Forms.Label
    Friend WithEvents lblIK1S As System.Windows.Forms.Label
    Friend WithEvents picIK1 As Guna.UI2.WinForms.Guna2PictureBox
    Friend WithEvents pnlIK2 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblIK2T As System.Windows.Forms.Label
    Friend WithEvents lblIK2V As System.Windows.Forms.Label
    Friend WithEvents lblIK2S As System.Windows.Forms.Label
    Friend WithEvents picIK2 As Guna.UI2.WinForms.Guna2PictureBox
    Friend WithEvents pnlIK3 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblIK3T As System.Windows.Forms.Label
    Friend WithEvents lblIK3V As System.Windows.Forms.Label
    Friend WithEvents lblIK3S As System.Windows.Forms.Label
    Friend WithEvents picIK3 As Guna.UI2.WinForms.Guna2PictureBox
    Friend WithEvents pnlIK4 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblIK4T As System.Windows.Forms.Label
    Friend WithEvents lblIK4V As System.Windows.Forms.Label
    Friend WithEvents lblIK4S As System.Windows.Forms.Label
    Friend WithEvents picIK4 As Guna.UI2.WinForms.Guna2PictureBox
    Friend WithEvents txtInvSearch As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents cboInvCategory As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents cboInvStatus As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents pnlInvTable As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblInvTableTitle As System.Windows.Forms.Label
    Friend WithEvents lblInvCount As System.Windows.Forms.Label
    Friend WithEvents invGrid As System.Windows.Forms.DataGridView
    Friend WithEvents lblInvShowing As System.Windows.Forms.Label
    Friend WithEvents pnlInvCat As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblInvCatTitle As System.Windows.Forms.Label
    Friend WithEvents lblInvCatTotal As System.Windows.Forms.Label
    Friend WithEvents picInvCatBar As System.Windows.Forms.PictureBox
    Friend WithEvents flInvCat As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents pnlInvAttn As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblInvAttnTitle As System.Windows.Forms.Label
    Friend WithEvents lblInvAttnCount As System.Windows.Forms.Label
    Friend WithEvents flInvAttn As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents lblDashGreeting As System.Windows.Forms.Label
    Friend WithEvents lblDashSub As System.Windows.Forms.Label
    Friend WithEvents btnDashExport As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents pnlDK1 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblDK1T As System.Windows.Forms.Label
    Friend WithEvents lblDK1V As System.Windows.Forms.Label
    Friend WithEvents lblDK1S As System.Windows.Forms.Label
    Friend WithEvents picDK1 As Guna.UI2.WinForms.Guna2PictureBox
    Friend WithEvents pnlDK2 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblDK2T As System.Windows.Forms.Label
    Friend WithEvents lblDK2V As System.Windows.Forms.Label
    Friend WithEvents lblDK2S As System.Windows.Forms.Label
    Friend WithEvents picDK2 As Guna.UI2.WinForms.Guna2PictureBox
    Friend WithEvents pnlDK3 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblDK3T As System.Windows.Forms.Label
    Friend WithEvents lblDK3V As System.Windows.Forms.Label
    Friend WithEvents lblDK3S As System.Windows.Forms.Label
    Friend WithEvents picDK3 As Guna.UI2.WinForms.Guna2PictureBox
    Friend WithEvents pnlDK4 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblDK4T As System.Windows.Forms.Label
    Friend WithEvents lblDK4V As System.Windows.Forms.Label
    Friend WithEvents lblDK4S As System.Windows.Forms.Label
    Friend WithEvents picDK4 As Guna.UI2.WinForms.Guna2PictureBox
    Friend WithEvents pnlDashTrend As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblDashTrendTitle As System.Windows.Forms.Label
    Friend WithEvents lblDashTrendSub As System.Windows.Forms.Label
    Friend WithEvents lblDashLegYest As System.Windows.Forms.Label
    Friend WithEvents lblDashLegToday As System.Windows.Forms.Label
    Friend WithEvents picDashTrend As System.Windows.Forms.PictureBox
    Friend WithEvents pnlDashCat As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblDashCatTitle As System.Windows.Forms.Label
    Friend WithEvents lblDashCatNote As System.Windows.Forms.Label
    Friend WithEvents flDashCat As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents pnlDashRecent As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblDashRecentTitle As System.Windows.Forms.Label
    Friend WithEvents lnkDashViewAll As System.Windows.Forms.Label
    Friend WithEvents dashGrid As System.Windows.Forms.DataGridView
    Friend WithEvents pnlDashTop As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblDashTopTitle As System.Windows.Forms.Label
    Friend WithEvents lblDashTopNote As System.Windows.Forms.Label
    Friend WithEvents flDashTop As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents pnlDashAlert As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblDashAlertTitle As System.Windows.Forms.Label
    Friend WithEvents lblDashAlertNote As System.Windows.Forms.Label
    Friend WithEvents flDashAlert As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents lblCustInfo As System.Windows.Forms.Label
    Friend WithEvents lblCustNameCap As System.Windows.Forms.Label
    Friend WithEvents txtCustomer As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents lblTableCap As System.Windows.Forms.Label
    Friend WithEvents cboTable As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents lblOrderCap As System.Windows.Forms.Label
    Friend WithEvents txtOrderNo As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents pnlCartSep As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents pnlCardDim As System.Windows.Forms.Panel
    Friend WithEvents pnlCardModal As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblCardModalTitle As System.Windows.Forms.Label
    Friend WithEvents lblCardModalSub As System.Windows.Forms.Label
    Friend WithEvents btnCardClose As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents pnlCardAmount As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblCardAmtCap As System.Windows.Forms.Label
    Friend WithEvents lblCardAmt As System.Windows.Forms.Label
    Friend WithEvents btnCardDebit As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnCardCredit As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnCardVisa As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnCardMastercard As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnCardJcb As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnCardAmex As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnCardConfirm As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents lblManagement As System.Windows.Forms.Label
    Friend WithEvents cboDashPeriod As Guna.UI2.WinForms.Guna2ComboBox
End Class