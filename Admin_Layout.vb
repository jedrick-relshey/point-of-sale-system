Option Strict On
Option Explicit On

Imports System.Drawing
Imports System.Windows.Forms

' Admin_Layout.vb  -  NEW FILE (part of the Admin class)
' Makes the Admin screen follow the window size (normal, resized or full screen):
' the sidebar keeps its width and always fills the height, and every page fills the rest.
Partial Public Class Admin

    Private Const ShellMinWidth As Integer = 1456
    Private Const ShellMinHeight As Integer = 883
    Private Const SidebarWidth As Integer = 199

    Private Sub ApplyResponsiveLayout()
        Me.MinimumSize = New Size(ShellMinWidth, ShellMinHeight)
        Me.MaximizeBox = True

        Navigation.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        adminDashboard.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right

        For Each pg As Panel In New Panel() {pnl_dashboard_system, pnl_Products, pnl_Messages, pnlCashiers, pnl_Inventory, pnl_History}
            pg.Dock = DockStyle.Fill
        Next

        FitAdminShell()
        AddHandler Me.Resize, Sub(s As Object, ev As EventArgs) FitAdminShell()
    End Sub

    Private Sub FitAdminShell()
        If Me.ClientSize.Width <= 0 OrElse Me.ClientSize.Height <= 0 Then Return
        Navigation.SetBounds(0, 0, SidebarWidth, Me.ClientSize.Height)
        adminDashboard.SetBounds(SidebarWidth, 0, Math.Max(100, Me.ClientSize.Width - SidebarWidth), Me.ClientSize.Height)
    End Sub

End Class