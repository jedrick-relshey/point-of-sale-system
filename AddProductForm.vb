Option Strict On
Option Explicit On

Imports System.Drawing
Imports System.Windows.Forms

' ============================================================
'  AddProductForm.vb
'  "Add product" dialog (also used for Edit when you pass a product).
'  It only COLLECTS the values - ProductPageControl then calls
'  DataStore.AddProduct / DataStore.UpdateProduct.
' ============================================================
Public Class AddProductForm
    Inherits Form

    Private ReadOnly _existing As Product

    Private txtName As TextBox
    Private txtPrice As TextBox
    Private numStock As NumericUpDown
    Private cboCategory As ComboBox
    Private txtDescription As TextBox
    Private picImage As PictureBox
    Private lblError As Label

    Private _imagePath As String = ""
    Private _name As String = ""
    Private _price As Decimal
    Private _stock As Integer
    Private _category As String = ""
    Private _description As String = ""

    ' ---- results (read these after ShowDialog = OK) ----
    Public Shadows ReadOnly Property ProductName As String
        Get
            Return _name
        End Get
    End Property

    Public ReadOnly Property ProductPrice As Decimal
        Get
            Return _price
        End Get
    End Property

    Public ReadOnly Property ProductStock As Integer
        Get
            Return _stock
        End Get
    End Property

    Public ReadOnly Property ProductCategory As String
        Get
            Return _category
        End Get
    End Property

    Public ReadOnly Property ProductDescription As String
        Get
            Return _description
        End Get
    End Property

    ''' <summary>Full path of a newly chosen picture, or "" when the picture was not changed.</summary>
    Public ReadOnly Property SelectedImagePath As String
        Get
            Return _imagePath
        End Get
    End Property

    Public Sub New(Optional existing As Product = Nothing)
        _existing = existing
        BuildUi()
        If _existing IsNot Nothing Then LoadExisting()
    End Sub

    Private Sub BuildUi()
        Dim isNew As Boolean = (_existing Is Nothing)
        Text = If(isNew, "Add product", "Edit product")
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        BackColor = Color.White
        Font = Theme.UiFont(9.0F)
        ClientSize = New Size(520, 436)
        KeyPreview = True
        AddHandler KeyDown, Sub(s, e)
                                If e.KeyCode = Keys.Escape Then Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
                            End Sub

        Dim lblTitle As New Label()
        lblTitle.Text = If(isNew, "Add product", "Edit product")
        lblTitle.Font = Theme.UiFont(14.0F, FontStyle.Bold)
        lblTitle.ForeColor = Theme.TextDark
        lblTitle.AutoSize = True
        lblTitle.Location = New Point(24, 16)
        Controls.Add(lblTitle)

        Dim lblSub As New Label()
        lblSub.Text = "Fill in the product details. All fields are required."
        lblSub.Font = Theme.UiFont(8.5F)
        lblSub.ForeColor = Theme.TextMuted
        lblSub.AutoSize = True
        lblSub.Location = New Point(24, 46)
        Controls.Add(lblSub)

        Caption("Product name", 24, 82)
        txtName = New TextBox()
        txtName.Location = New Point(24, 102)
        txtName.Width = 472
        txtName.MaxLength = 120
        Controls.Add(txtName)

        Caption("Price (" & ChrW(&H20B1).ToString() & ")", 24, 140)
        txtPrice = New TextBox()
        txtPrice.Location = New Point(24, 160)
        txtPrice.Width = 150
        Controls.Add(txtPrice)

        Caption("Stock", 186, 140)
        numStock = New NumericUpDown()
        numStock.Minimum = 0D
        numStock.Maximum = 1000000D
        numStock.Location = New Point(186, 160)
        numStock.Width = 110
        Controls.Add(numStock)

        Caption("Category", 308, 140)
        cboCategory = New ComboBox()
        cboCategory.DropDownStyle = ComboBoxStyle.DropDown      ' can type a new category
        cboCategory.Location = New Point(308, 160)
        cboCategory.Width = 188
        Dim seen As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        For Each p As Product In DataStore.Products
            If Not String.IsNullOrWhiteSpace(p.Category) AndAlso seen.Add(p.Category.Trim()) Then
                cboCategory.Items.Add(p.Category.Trim())
            End If
        Next
        Controls.Add(cboCategory)

        Caption("Description", 24, 198)
        txtDescription = New TextBox()
        txtDescription.Multiline = True
        txtDescription.ScrollBars = ScrollBars.Vertical
        txtDescription.Location = New Point(24, 218)
        txtDescription.Size = New Size(340, 96)
        Controls.Add(txtDescription)

        Caption("Picture", 380, 198)
        picImage = New PictureBox()
        picImage.Location = New Point(380, 218)
        picImage.Size = New Size(116, 96)
        picImage.SizeMode = PictureBoxSizeMode.Zoom
        picImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        picImage.BackColor = Theme.HeaderRowBg
        picImage.Cursor = Cursors.Hand
        AddHandler picImage.Click, Sub(s, e) ChooseImage()
        AddHandler picImage.Paint, Sub(s, e)
                                       If picImage.Image Is Nothing Then
                                           TextRenderer.DrawText(e.Graphics, "Click to add picture", Theme.UiFont(8.0F),
                                                                 picImage.ClientRectangle, Theme.TextMuted,
                                                                 TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or TextFormatFlags.WordBreak)
                                       End If
                                   End Sub
        Controls.Add(picImage)

        Dim lnkChoose As New LinkLabel()
        lnkChoose.Text = "Choose image"
        lnkChoose.AutoSize = True
        lnkChoose.Location = New Point(380, 320)
        AddHandler lnkChoose.LinkClicked, Sub(s, e) ChooseImage()
        Controls.Add(lnkChoose)

        lblError = New Label()
        lblError.ForeColor = Theme.Red
        lblError.AutoSize = False
        lblError.Location = New Point(24, 346)
        lblError.Size = New Size(472, 18)
        Controls.Add(lblError)

        Dim btnCancel As New RoundedButton()
        btnCancel.Text = "Cancel"
        btnCancel.FillColor = Color.White
        btnCancel.HoverColor = Theme.HoverGray
        btnCancel.BorderColor = Theme.Border
        btnCancel.TextColor = Theme.TextDark
        btnCancel.Size = New Size(90, 36)
        btnCancel.Location = New Point(268, 376)
        AddHandler btnCancel.Click, Sub(s, e) Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Controls.Add(btnCancel)

        Dim btnSave As New RoundedButton()
        btnSave.Text = "Save product"
        btnSave.Size = New Size(130, 36)
        btnSave.Location = New Point(366, 376)
        AddHandler btnSave.Click, Sub(s, e) SaveClicked()
        Controls.Add(btnSave)

        ActiveControl = txtName
    End Sub

    Private Sub Caption(text As String, x As Integer, y As Integer)
        Dim l As New Label()
        l.Text = text
        l.AutoSize = True
        l.Font = Theme.UiFont(8.0F)
        l.ForeColor = Theme.TextMuted
        l.Location = New Point(x, y)
        Controls.Add(l)
    End Sub

    Private Sub LoadExisting()
        txtName.Text = _existing.Name
        txtPrice.Text = _existing.Price.ToString("0.00")
        numStock.Value = Math.Min(numStock.Maximum, Math.Max(numStock.Minimum, CDec(_existing.Stock)))
        cboCategory.Text = _existing.Category
        txtDescription.Text = _existing.Description
        picImage.Image = _existing.Image
    End Sub

    Private Sub ChooseImage()
        Using dlg As New OpenFileDialog()
            dlg.Title = "Choose product picture"
            dlg.Filter = "Images|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
            If dlg.ShowDialog(Me) <> System.Windows.Forms.DialogResult.OK Then Return

            Dim preview As Image = DataStore.LoadImageCopy(dlg.FileName)
            If preview Is Nothing Then
                Fail("That file is not a valid image.", Nothing)
                Return
            End If
            _imagePath = dlg.FileName
            picImage.Image = preview
            picImage.Invalidate()
            lblError.Text = ""
        End Using
    End Sub

    Private Sub Fail(message As String, target As Control)
        lblError.Text = message
        If target IsNot Nothing Then target.Focus()
    End Sub

    Private Sub SaveClicked()
        lblError.Text = ""

        If String.IsNullOrWhiteSpace(txtName.Text) Then
            Fail("Product name is required.", txtName)
            Return
        End If

        Dim price As Decimal
        If String.IsNullOrWhiteSpace(txtPrice.Text) Then
            Fail("Price is required.", txtPrice)
            Return
        End If
        If Not TryParseMoney(txtPrice.Text, price) Then
            Fail("Enter a valid price.", txtPrice)
            Return
        End If
        If price < 0D Then
            Fail("Price cannot be negative.", txtPrice)
            Return
        End If

        If String.IsNullOrWhiteSpace(cboCategory.Text) Then
            Fail("Category is required.", cboCategory)
            Return
        End If

        If String.IsNullOrWhiteSpace(txtDescription.Text) Then
            Fail("Description is required.", txtDescription)
            Return
        End If

        _name = txtName.Text.Trim()
        _price = Math.Round(price, 2)
        _stock = CInt(numStock.Value)
        _category = cboCategory.Text.Trim()
        _description = txtDescription.Text.Trim()

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub

End Class
