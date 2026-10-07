Option Strict On
Option Explicit On

Imports System.Drawing
Imports System.Windows.Forms
Imports Guna.UI2.WinForms

'=====================================================================
' [Products] [Ingredients] switch (PHASE 4/5)
' It does NOT rebuild the existing inventory page. It only
'   - remembers the controls that are already on the page,
'   - adds a 2-button toggle and the IngredientInventoryView,
'   - shows one side or the other.
' The existing product inventory (grid, restock list, KPI cards, adjust panel) is untouched.
'=====================================================================
Public Class InventoryModeSwitch

    Private ReadOnly accent As Color = Color.FromArgb(62, 39, 35)
    Private ReadOnly page As Control
    Private ReadOnly view As IngredientInventoryView
    Private ReadOnly keep As New List(Of Control)
    Private ReadOnly originals As New List(Of Control)
    Private ReadOnly wasVisible As New Dictionary(Of Control, Boolean)
    Private ReadOnly btnProducts As Guna2Button
    Private ReadOnly btnIngredients As Guna2Button
    Private ingredientMode As Boolean = False

    ''' <param name="keepVisible">title / subtitle controls that must stay visible in both modes</param>
    Public Sub New(page As Control, view As IngredientInventoryView, toggleLocation As Point,
                   keepVisible As IEnumerable(Of Control), startWithIngredients As Boolean)
        Me.page = page
        Me.view = view
        keep.AddRange(keepVisible)
        For Each c As Control In page.Controls
            originals.Add(c)
        Next

        btnProducts = MakeToggle("Products", toggleLocation.X, toggleLocation.Y)
        btnIngredients = MakeToggle("Ingredients", toggleLocation.X + 112, toggleLocation.Y)
        AddHandler btnProducts.Click, Sub(o As Object, e As EventArgs) SetMode(False)
        AddHandler btnIngredients.Click, Sub(o As Object, e As EventArgs) SetMode(True)

        view.Visible = False
        page.Controls.Add(view)
        page.Controls.Add(btnProducts)
        page.Controls.Add(btnIngredients)
        btnProducts.BringToFront()
        btnIngredients.BringToFront()
        SetMode(startWithIngredients)
    End Sub

    Private Function MakeToggle(caption As String, x As Integer, y As Integer) As Guna2Button
        Dim b As New Guna2Button With {.Text = caption, .Location = New Point(x, y), .Size = New Size(108, 34), .BorderRadius = 6,
                                       .Font = New Font("Segoe UI", 9.5F, FontStyle.Bold), .Cursor = Cursors.Hand}
        Return b
    End Function

    Private Sub SetMode(ingredients As Boolean)
        If ingredients AndAlso Not ingredientMode Then
            For Each c As Control In originals
                If keep.Contains(c) Then Continue For
                wasVisible(c) = c.Visible
                c.Visible = False
            Next
        ElseIf Not ingredients AndAlso ingredientMode Then
            For Each c As Control In originals
                If keep.Contains(c) Then Continue For
                Dim v As Boolean = True
                If wasVisible.TryGetValue(c, v) Then c.Visible = v
            Next
        End If
        ingredientMode = ingredients
        view.Visible = ingredients
        If ingredients Then
            view.BringToFront()
            view.RefreshData()
        End If
        btnProducts.BringToFront()
        btnIngredients.BringToFront()
        Style(btnProducts, Not ingredients)
        Style(btnIngredients, ingredients)
    End Sub

    Private Sub Style(b As Guna2Button, active As Boolean)
        b.FillColor = If(active, accent, Color.White)
        b.ForeColor = If(active, Color.White, accent)
        b.BorderThickness = 1
        b.BorderColor = accent
    End Sub

End Class
