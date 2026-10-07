Option Strict On
Option Explicit On

Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Globalization
Imports System.Linq
Imports System.Windows.Forms

' ============================================================
'  AddProductForm.vb
'  "Add product" dialog (also used for Edit when you pass a product).
'
'    Using dlg As New AddProductForm()
'        If dlg.ShowDialog(Me) = DialogResult.OK Then
'            ProductStore.SaveProduct(dlg.ResultProduct, dlg.ResultVariants)
'        End If
'    End Using
'
'  Pricing:
'    Fixed price      -> 1 ProductVariant (Size = "Regular")
'    Price by size    -> 2+ ProductVariants (e.g. Small 100 / Large 130)
'                        shown as "Dynamic" in the product list.
' ============================================================
Public Class AddProductForm
    Inherits Form

    Private ReadOnly _product As Product
    Private ReadOnly _isNew As Boolean
    Private _image As Image

    Private txtName As TextBox
    Private cboCategory As ComboBox
    Private numStock As NumericUpDown
    Private cboStatus As ComboBox
    Private txtDescription As TextBox
    Private picImage As PictureBox
    Private rdoFixed As RadioButton
    Private rdoSizes As RadioButton
    Private pnlFixed As Panel
    Private pnlSizes As Panel
    Private numPrice As NumericUpDown
    Private gridSizes As DataGridView
    Private lblError As Label

    Public ReadOnly Property ResultProduct As Product
        Get
            Return _product
        End Get
    End Property

    Public ReadOnly Property ResultVariants As New List(Of ProductVariant)()

    Public Sub New(Optional existing As Product = Nothing)
        _isNew = (existing Is Nothing)
        _product = If(existing, New Product())
        BuildUi()
        If Not _isNew Then LoadExisting()
        UpdatePricingMode()
    End Sub

    ' ------------------------------------------------------------
    '  UI
    ' ------------------------------------------------------------
    Private Sub BuildUi()
        Text = If(_isNew, "Add product", "Edit product")
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        BackColor = Color.White
        Font = Theme.UiFont(9.0F)
        ClientSize = New Size(520, 606)
        KeyPreview = True
        AddHandler KeyDown, Sub(s, e)
                                If e.KeyCode = Keys.Escape Then Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
                            End Sub

        ' title
        Dim lblTitle As New Label()
        lblTitle.Text = If(_isNew, "Add product", "Edit product")
        lblTitle.Font = Theme.UiFont(14.0F, FontStyle.Bold)
        lblTitle.ForeColor = Theme.TextDark
        lblTitle.AutoSize = True
        lblTitle.Location = New Point(24, 16)
        Controls.Add(lblTitle)

        Dim lblSub As New Label()
        lblSub.Text = "Fill in the details. Use ""price by size"" if the price depends on the size."
        lblSub.Font = Theme.UiFont(8.5F)
        lblSub.ForeColor = Theme.TextMuted
        lblSub.AutoSize = True
        lblSub.Location = New Point(24, 46)
        Controls.Add(lblSub)

        ' name
        Caption("Product name", Me, 24, 82)
        txtName = New TextBox()
        txtName.Location = New Point(24, 102)
        txtName.Width = 472
        txtName.MaxLength = 120
        Controls.Add(txtName)

        ' category / stock / status
        Caption("Category", Me, 24, 140)
        cboCategory = New ComboBox()
        cboCategory.DropDownStyle = ComboBoxStyle.DropDown      ' can type a new category
        cboCategory.Location = New Point(24, 160)
        cboCategory.Width = 220
        For Each c As String In ProductStore.Categories()
            cboCategory.Items.Add(c)
        Next
        Controls.Add(cboCategory)

        Caption("Stock", Me, 256, 140)
        numStock = New NumericUpDown()
        numStock.Minimum = 0D
        numStock.Maximum = 1000000D
        numStock.Location = New Point(256, 160)
        numStock.Width = 110
        Controls.Add(numStock)

        Caption("Status", Me, 378, 140)
        cboStatus = New ComboBox()
        cboStatus.DropDownStyle = ComboBoxStyle.DropDownList
        cboStatus.Items.Add("Active")
        cboStatus.Items.Add("Inactive")
        cboStatus.SelectedIndex = 0
        cboStatus.Location = New Point(378, 160)
        cboStatus.Width = 118
        Controls.Add(cboStatus)

        ' description + picture
        Caption("Description (optional)", Me, 24, 198)
        txtDescription = New TextBox()
        txtDescription.Multiline = True
        txtDescription.ScrollBars = ScrollBars.Vertical
        txtDescription.Location = New Point(24, 218)
        txtDescription.Size = New Size(340, 84)
        Controls.Add(txtDescription)

        Caption("Picture", Me, 380, 198)
        picImage = New PictureBox()
        picImage.Location = New Point(380, 218)
        picImage.Size = New Size(116, 84)
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
        lnkChoose.Text = "Choose"
        lnkChoose.AutoSize = True
        lnkChoose.Location = New Point(380, 307)
        AddHandler lnkChoose.LinkClicked, Sub(s, e) ChooseImage()
        Controls.Add(lnkChoose)

        Dim lnkRemove As New LinkLabel()
        lnkRemove.Text = "Remove"
        lnkRemove.AutoSize = True
        lnkRemove.Location = New Point(440, 307)
        AddHandler lnkRemove.LinkClicked, Sub(s, e)
                                              _image = Nothing
                                              picImage.Image = Nothing
                                              picImage.Invalidate()
                                          End Sub
        Controls.Add(lnkRemove)

        ' pricing mode
        Caption("Pricing", Me, 24, 334)
        rdoFixed = New RadioButton()
        rdoFixed.Text = "Fixed price"
        rdoFixed.AutoSize = True
        rdoFixed.Checked = True
        rdoFixed.Location = New Point(24, 354)
        Controls.Add(rdoFixed)

        rdoSizes = New RadioButton()
        rdoSizes.Text = "Price by size (dynamic)"
        rdoSizes.AutoSize = True
        rdoSizes.Location = New Point(140, 354)
        Controls.Add(rdoSizes)

        AddHandler rdoFixed.CheckedChanged, Sub(s, e) UpdatePricingMode()
        AddHandler rdoSizes.CheckedChanged, Sub(s, e) UpdatePricingMode()

        ' fixed price panel
        pnlFixed = New Panel()
        pnlFixed.Location = New Point(24, 386)
        pnlFixed.Size = New Size(472, 56)
        Caption("Price (" & ChrW(&H20B1).ToString() & ")", pnlFixed, 0, 0)
        numPrice = New NumericUpDown()
        numPrice.DecimalPlaces = 2
        numPrice.Minimum = 0D
        numPrice.Maximum = 1000000D
        numPrice.Location = New Point(0, 20)
        numPrice.Width = 140
        pnlFixed.Controls.Add(numPrice)
        Controls.Add(pnlFixed)

        ' sizes panel
        pnlSizes = New Panel()
        pnlSizes.Location = New Point(24, 386)
        pnlSizes.Size = New Size(472, 136)
        Caption("One row per size, e.g. Small = 100, Large = 130. Select a row + press Delete to remove.", pnlSizes, 0, 0)

        gridSizes = New DataGridView()
        gridSizes.Location = New Point(0, 22)
        gridSizes.Size = New Size(472, 112)
        gridSizes.AllowUserToAddRows = True
        gridSizes.AllowUserToDeleteRows = True
        gridSizes.AllowUserToResizeRows = False
        gridSizes.RowHeadersVisible = False
        gridSizes.BackgroundColor = Color.White
        gridSizes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        gridSizes.GridColor = Theme.Border
        gridSizes.EnableHeadersVisualStyles = False
        gridSizes.ColumnHeadersDefaultCellStyle.BackColor = Theme.HeaderRowBg
        gridSizes.ColumnHeadersDefaultCellStyle.ForeColor = Theme.TextMuted
        gridSizes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        gridSizes.ColumnHeadersHeight = 26
        gridSizes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        gridSizes.DefaultCellStyle.SelectionBackColor = Theme.TealSoft
        gridSizes.DefaultCellStyle.SelectionForeColor = Theme.TextDark
        gridSizes.Columns.Add("colSize", "Size")
        gridSizes.Columns.Add("colPrice", "Price")
        pnlSizes.Controls.Add(gridSizes)
        Controls.Add(pnlSizes)

        ' error message
        lblError = New Label()
        lblError.ForeColor = Theme.Red
        lblError.AutoSize = False
        lblError.Location = New Point(24, 530)
        lblError.Size = New Size(472, 18)
        Controls.Add(lblError)

        ' buttons
        Dim btnCancel As New RoundedButton()
        btnCancel.Text = "Cancel"
        btnCancel.FillColor = Color.White
        btnCancel.HoverColor = Theme.HoverGray
        btnCancel.BorderColor = Theme.Border
        btnCancel.TextColor = Theme.TextDark
        btnCancel.Size = New Size(90, 36)
        btnCancel.Location = New Point(268, 556)
        AddHandler btnCancel.Click, Sub(s, e) Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Controls.Add(btnCancel)

        Dim btnSave As New RoundedButton()
        btnSave.Text = "Save product"
        btnSave.Size = New Size(130, 36)
        btnSave.Location = New Point(366, 556)
        AddHandler btnSave.Click, Sub(s, e) SaveClicked()
        Controls.Add(btnSave)

        ActiveControl = txtName
    End Sub

    Private Function Caption(text As String, parent As Control, x As Integer, y As Integer) As Label
        Dim l As New Label()
        l.Text = text
        l.AutoSize = True
        l.Font = Theme.UiFont(8.0F)
        l.ForeColor = Theme.TextMuted
        l.Location = New Point(x, y)
        parent.Controls.Add(l)
        Return l
    End Function

    Private Sub UpdatePricingMode()
        If pnlFixed Is Nothing OrElse pnlSizes Is Nothing Then Return
        pnlFixed.Visible = rdoFixed.Checked
        pnlSizes.Visible = rdoSizes.Checked
    End Sub

    ' ------------------------------------------------------------
    '  Edit mode: fill the fields from the existing product
    ' ------------------------------------------------------------
    Private Sub LoadExisting()
        txtName.Text = _product.Name
        cboCategory.Text = _product.Category
        numStock.Value = ClampTo(numStock, _product.Stock)
        cboStatus.SelectedIndex = If(String.Equals(_product.Status, "Inactive", StringComparison.OrdinalIgnoreCase), 1, 0)
        txtDescription.Text = _product.Description

        _image = _product.Image
        picImage.Image = _image

        Dim vs As List(Of ProductVariant) = ProductStore.VariantsOf(_product.Id)
        If vs.Count > 1 Then
            rdoSizes.Checked = True
            For Each v As ProductVariant In vs
                gridSizes.Rows.Add(v.Size, v.Price.ToString("N2"))
            Next
        Else
            numPrice.Value = ClampTo(numPrice, If(vs.Count = 1, vs(0).Price, _product.Price))
        End If
    End Sub

    Private Shared Function ClampTo(n As NumericUpDown, value As Decimal) As Decimal
        Return Math.Min(n.Maximum, Math.Max(n.Minimum, value))
    End Function

    ' ------------------------------------------------------------
    '  Picture
    ' ------------------------------------------------------------
    Private Sub ChooseImage()
        Using dlg As New OpenFileDialog()
            dlg.Title = "Choose product picture"
            dlg.Filter = "Images|*.png;*.jpg;*.jpeg;*.bmp;*.gif"
            If dlg.ShowDialog(Me) <> System.Windows.Forms.DialogResult.OK Then Return

            Try
                Using src As Image = Image.FromFile(dlg.FileName)
                    _image = ScaleDown(src, 512)
                End Using
                picImage.Image = _image
                picImage.Invalidate()
            Catch
                Fail("That file could not be opened as an image.", Nothing)
            End Try
        End Using
    End Sub

    Private Shared Function ScaleDown(src As Image, maxSide As Integer) As Image
        Dim factor As Double = Math.Min(1.0, CDbl(maxSide) / Math.Max(src.Width, src.Height))
        Dim w As Integer = Math.Max(1, CInt(src.Width * factor))
        Dim h As Integer = Math.Max(1, CInt(src.Height * factor))
        Dim bmp As New Bitmap(w, h)
        Using g As Graphics = Graphics.FromImage(bmp)
            g.InterpolationMode = InterpolationMode.HighQualityBicubic
            g.DrawImage(src, 0, 0, w, h)
        End Using
        Return bmp
    End Function

    ' ------------------------------------------------------------
    '  Save
    ' ------------------------------------------------------------
    Private Sub Fail(message As String, target As Control)
        lblError.Text = message
        If target IsNot Nothing Then target.Focus()
    End Sub

    Private Sub SaveClicked()
        lblError.Text = ""

        Dim name As String = txtName.Text.Trim()
        If name.Length = 0 Then
            Fail("Please enter the product name.", txtName)
            Return
        End If

        Dim duplicate As Boolean = ProductStore.Products.Any(
            Function(x) Not Object.ReferenceEquals(x, _product) AndAlso
                        String.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase))
        If duplicate Then
            Fail("A product with this name already exists.", txtName)
            Return
        End If

        Dim category As String = cboCategory.Text.Trim()
        If category.Length = 0 Then
            Fail("Please choose or type a category.", cboCategory)
            Return
        End If

        ' ---- build the variants ----
        Dim vars As New List(Of ProductVariant)()

        If rdoFixed.Checked Then
            If numPrice.Value <= 0D Then
                Fail("Please enter a price greater than 0.", numPrice)
                Return
            End If
            Dim v As New ProductVariant()
            v.Size = "Regular"
            v.Price = numPrice.Value
            vars.Add(v)
        Else
            Dim seen As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
            For Each row As DataGridViewRow In gridSizes.Rows
                If row.IsNewRow Then Continue For

                Dim sizeText As String = Convert.ToString(row.Cells(0).Value).Trim()
                Dim priceText As String = Convert.ToString(row.Cells(1).Value).Trim()
                If sizeText.Length = 0 AndAlso priceText.Length = 0 Then Continue For

                Dim price As Decimal
                If sizeText.Length = 0 OrElse
                   Not Decimal.TryParse(priceText, NumberStyles.Number, CultureInfo.CurrentCulture, price) OrElse
                   price <= 0D Then
                    Fail("Every row needs a size name and a price greater than 0.", gridSizes)
                    Return
                End If
                If Not seen.Add(sizeText) Then
                    Fail("Size """ & sizeText & """ is listed twice.", gridSizes)
                    Return
                End If

                Dim v As New ProductVariant()
                v.Size = sizeText
                v.Price = price
                vars.Add(v)
            Next

            If vars.Count < 2 Then
                Fail("Add at least two sizes, or choose ""Fixed price"".", gridSizes)
                Return
            End If
        End If

        ' ---- copy to the product ----
        _product.Name = name
        _product.Category = category
        _product.Stock = CInt(numStock.Value)
        _product.Status = cboStatus.Text
        _product.Description = txtDescription.Text.Trim()
        _product.Image = _image

        ResultVariants.Clear()
        ResultVariants.AddRange(vars)

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub

End Class