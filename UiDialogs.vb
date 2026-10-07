Option Strict On
Option Explicit On

Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms

' PHASE 4/5 (new).  One small reusable input dialog (text / password / dropdown fields).
Public Enum DialogFieldKind
    Text
    Password
    Combo
End Enum

Public Class DialogField
    Public Property Caption As String = ""
    Public Property DefaultValue As String = ""
    Public Property Kind As DialogFieldKind = DialogFieldKind.Text
    Public Property Items As String() = New String() {}

    Public Sub New(caption As String, Optional kind As DialogFieldKind = DialogFieldKind.Text,
                   Optional defaultValue As String = "", Optional items As String() = Nothing)
        Me.Caption = caption
        Me.Kind = kind
        Me.DefaultValue = If(defaultValue, "")
        If items IsNot Nothing Then Me.Items = items
    End Sub
End Class

Public Module UiDialogs

    ''' <summary>Returns the entered values (same order as fields) or Nothing if cancelled.</summary>
    Public Function Ask(owner As IWin32Window, title As String, fields As DialogField(),
                        Optional okText As String = "Save") As String()
        Dim brown As Color = Color.FromArgb(62, 39, 35)
        Using dlg As New Form()
            dlg.Text = title
            dlg.Font = New Font("Segoe UI", 10.0F)
            dlg.FormBorderStyle = FormBorderStyle.FixedDialog
            dlg.StartPosition = FormStartPosition.CenterParent
            dlg.MaximizeBox = False
            dlg.MinimizeBox = False
            dlg.ShowInTaskbar = False
            dlg.ClientSize = New Size(400, 20 + fields.Length * 64 + 64)

            Dim inputs(fields.Length - 1) As Control
            For i As Integer = 0 To fields.Length - 1
                Dim f As DialogField = fields(i)
                dlg.Controls.Add(New Label With {.Text = f.Caption, .Left = 20, .Top = 14 + i * 64, .AutoSize = True})
                If f.Kind = DialogFieldKind.Combo Then
                    Dim cbo As New ComboBox With {.Left = 20, .Top = 38 + i * 64, .Width = 360, .DropDownStyle = ComboBoxStyle.DropDownList}
                    cbo.Items.AddRange(DirectCast(f.Items, Object()))
                    If f.DefaultValue <> "" AndAlso cbo.Items.Contains(f.DefaultValue) Then
                        cbo.SelectedItem = f.DefaultValue
                    ElseIf cbo.Items.Count > 0 Then
                        cbo.SelectedIndex = 0
                    End If
                    inputs(i) = cbo
                Else
                    Dim tb As New TextBox With {.Left = 20, .Top = 38 + i * 64, .Width = 360, .Text = f.DefaultValue}
                    If f.Kind = DialogFieldKind.Password Then tb.UseSystemPasswordChar = True
                    inputs(i) = tb
                End If
                dlg.Controls.Add(inputs(i))
            Next

            Dim y As Integer = 14 + fields.Length * 64
            Dim ok As New Button With {.Text = okText, .Left = 200, .Top = y, .Width = 85, .Height = 34, .DialogResult = DialogResult.OK,
                                       .FlatStyle = FlatStyle.Flat, .BackColor = brown, .ForeColor = Color.White}
            Dim cancel As New Button With {.Text = "Cancel", .Left = 295, .Top = y, .Width = 85, .Height = 34, .DialogResult = DialogResult.Cancel}
            dlg.Controls.Add(ok)
            dlg.Controls.Add(cancel)
            dlg.AcceptButton = ok
            dlg.CancelButton = cancel

            If dlg.ShowDialog(owner) <> DialogResult.OK Then Return Nothing
            Dim result(fields.Length - 1) As String
            For i As Integer = 0 To fields.Length - 1
                result(i) = inputs(i).Text
            Next
            Return result
        End Using
    End Function

    ''' <summary>"1,250.5" or "1250.5" -> Decimal.</summary>
    Public Function TryDecimal(text As String, ByRef value As Decimal) As Boolean
        value = 0D
        If String.IsNullOrWhiteSpace(text) Then Return False
        If Decimal.TryParse(text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, value) Then Return True
        Return Decimal.TryParse(text.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, value)
    End Function

    ''' <summary>Units a user may type for an ingredient whose base unit is g / ml / pcs.</summary>
    Public Function UnitChoices(baseUnit As String) As String()
        Select Case baseUnit
            Case UnitHelper.Gram : Return New String() {"g", "kg"}
            Case UnitHelper.Milliliter : Return New String() {"ml", "L"}
            Case Else : Return New String() {"pcs"}
        End Select
    End Function

End Module
