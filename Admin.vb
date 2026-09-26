Public Class Admin
    Private Sub btnAdminLogout_Click(sender As Object, e As EventArgs) Handles btnAdminLogout.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Dim loginPage As New LoginPage()
            loginPage.Show()
            Me.Hide()
            MessageBox.Show("Logout successful.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Logout canceled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub btnSaveProduct_Click(sender As Object, e As EventArgs) Handles btnSaveProduct.Click

        If Not ValidateProduct() Then Exit Sub

        Dim price As Decimal
        Dim stock As Integer

        Decimal.TryParse(txtProductPrice.Text, price)
        Integer.TryParse(txtProductStock.Text, stock)

        Dim newProduct As New Product()

        newProduct.Name = txtProductName.Text
        newProduct.Price = price
        newProduct.Stock = stock
        newProduct.Description = txtProductDescription.Text

        DataStore.Products.Add(newProduct)

        LoadProducts()
    End Sub

    Private Function ValidateProduct() As Boolean

        Dim textBoxes() As Control = {
        txtProductName,
        txtProductPrice,
        txtProductStock,
        txtCategory,
        txtProductDescription
    }

        Dim errorLabels() As Control = {
        lblProductNameError,
        lblPriceError,
        lblStockError,
        lblCategoryError,
        lblDescriptionError
    }

        Dim valid As Boolean = True

        For i As Integer = 0 To textBoxes.Length - 1

            errorLabels(i).Visible = False

            If textBoxes(i).Text.Trim() = "" Then
                errorLabels(i).Text = "This field is required."
                errorLabels(i).Visible = True
                valid = False

            ElseIf i = 1 AndAlso Not Decimal.TryParse(textBoxes(i).Text, Nothing) Then
                errorLabels(i).Text = "Enter a valid price."
                errorLabels(i).Visible = True
                valid = False

            ElseIf i = 2 AndAlso Not Integer.TryParse(textBoxes(i).Text, Nothing) Then
                errorLabels(i).Text = "Enter a valid stock."
                errorLabels(i).Visible = True
                valid = False
            End If

        Next

        Return valid

    End Function

    Private Sub LoadProducts()

        dgvProducts.Rows.Clear()

        For Each item In DataStore.Products

            Dim row As Integer = dgvProducts.Rows.Add()

            dgvProducts.Rows(row).Cells("Product").Value = item.Name
            dgvProducts.Rows(row).Cells("Price").Value = item.Price
            dgvProducts.Rows(row).Cells("Stock").Value = item.Stock
            dgvProducts.Rows(row).Cells("Description").Value = item.Description

        Next

    End Sub
End Class