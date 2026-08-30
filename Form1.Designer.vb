<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.pnl_Login_Form = New System.Windows.Forms.Panel()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.pnl_Login_Form.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnl_Login_Form
        '
        Me.pnl_Login_Form.Controls.Add(Me.btnLogin)
        Me.pnl_Login_Form.Location = New System.Drawing.Point(702, -2)
        Me.pnl_Login_Form.Name = "pnl_Login_Form"
        Me.pnl_Login_Form.Size = New System.Drawing.Size(507, 705)
        Me.pnl_Login_Form.TabIndex = 0
        '
        'btnLogin
        '
        Me.btnLogin.BackColor = System.Drawing.Color.Fuchsia
        Me.btnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnLogin.Font = New System.Drawing.Font("Microsoft JhengHei UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogin.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnLogin.Image = Global.Vb_POS.My.Resources.Resources.login
        Me.btnLogin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLogin.Location = New System.Drawing.Point(71, 512)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(387, 60)
        Me.btnLogin.TabIndex = 0
        Me.btnLogin.Text = "Login"
        Me.btnLogin.UseVisualStyleBackColor = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.BackgroundImage = Global.Vb_POS.My.Resources.Resources.pos_bg
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1206, 702)
        Me.Controls.Add(Me.pnl_Login_Form)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.pnl_Login_Form.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnl_Login_Form As Panel
    Friend WithEvents btnLogin As Button
End Class
