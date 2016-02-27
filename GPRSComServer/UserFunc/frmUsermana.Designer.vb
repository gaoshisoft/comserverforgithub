Namespace UserFunc
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmUsermana
#Region "Windows 窗体设计器生成的代码 "
        <System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
            MyBase.New()
            '此调用是 Windows 窗体设计器所必需的。
            InitializeComponent()
        End Sub
        'Form 重写 Dispose，以清理组件列表。
        <System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
            If Disposing Then
                If Not components Is Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(Disposing)
        End Sub
        'Windows 窗体设计器所必需的
        Private components As System.ComponentModel.IContainer
        Public ToolTip1 As System.Windows.Forms.ToolTip
        Public WithEvents cmdLogin As System.Windows.Forms.Button
        Public WithEvents cmdedituser As System.Windows.Forms.Button
        Public WithEvents CmdClose As System.Windows.Forms.Button
        Public WithEvents cmdUserLogin As System.Windows.Forms.Button
        Public WithEvents cbouser As System.Windows.Forms.ComboBox
        Public WithEvents txtpass As System.Windows.Forms.TextBox
        Public WithEvents cmdRemoveuser As System.Windows.Forms.Button
        Public WithEvents cmdAdduser As System.Windows.Forms.Button
        Public WithEvents cbolevel As System.Windows.Forms.ComboBox
        Public WithEvents txtPassword As System.Windows.Forms.TextBox
        Public WithEvents TreeView1 As System.Windows.Forms.TreeView
        Public WithEvents Label4 As System.Windows.Forms.Label
        Public WithEvents Label3 As System.Windows.Forms.Label
        Public WithEvents Frame1 As System.Windows.Forms.GroupBox
        Public WithEvents Label2 As System.Windows.Forms.Label
        Public WithEvents Label1 As System.Windows.Forms.Label
        '注意: 以下过程是 Windows 窗体设计器所必需的
        '可以使用 Windows 窗体设计器来修改它。
        '不要使用代码编辑器修改它。
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.cmdLogin = New System.Windows.Forms.Button()
            Me.cmdedituser = New System.Windows.Forms.Button()
            Me.CmdClose = New System.Windows.Forms.Button()
            Me.cmdUserLogin = New System.Windows.Forms.Button()
            Me.cbouser = New System.Windows.Forms.ComboBox()
            Me.txtpass = New System.Windows.Forms.TextBox()
            Me.Frame1 = New System.Windows.Forms.GroupBox()
            Me.cmdRemoveuser = New System.Windows.Forms.Button()
            Me.cmdAdduser = New System.Windows.Forms.Button()
            Me.cbolevel = New System.Windows.Forms.ComboBox()
            Me.txtPassword = New System.Windows.Forms.TextBox()
            Me.TreeView1 = New System.Windows.Forms.TreeView()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.cmdSaveToXml = New System.Windows.Forms.Button()
            Me.Frame1.SuspendLayout()
            Me.SuspendLayout()
            '
            'cmdLogin
            '
            Me.cmdLogin.BackColor = System.Drawing.SystemColors.Control
            Me.cmdLogin.Cursor = System.Windows.Forms.Cursors.Default
            Me.cmdLogin.ForeColor = System.Drawing.SystemColors.ControlText
            Me.cmdLogin.Location = New System.Drawing.Point(42, 94)
            Me.cmdLogin.Name = "cmdLogin"
            Me.cmdLogin.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.cmdLogin.Size = New System.Drawing.Size(69, 23)
            Me.cmdLogin.TabIndex = 16
            Me.cmdLogin.Text = "登录"
            Me.cmdLogin.UseVisualStyleBackColor = False
            '
            'cmdedituser
            '
            Me.cmdedituser.BackColor = System.Drawing.SystemColors.Control
            Me.cmdedituser.Cursor = System.Windows.Forms.Cursors.Default
            Me.cmdedituser.ForeColor = System.Drawing.SystemColors.ControlText
            Me.cmdedituser.Location = New System.Drawing.Point(246, 94)
            Me.cmdedituser.Name = "cmdedituser"
            Me.cmdedituser.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.cmdedituser.Size = New System.Drawing.Size(77, 23)
            Me.cmdedituser.TabIndex = 8
            Me.cmdedituser.Text = ">>编辑用户"
            Me.cmdedituser.UseVisualStyleBackColor = False
            '
            'CmdClose
            '
            Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
            Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
            Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
            Me.CmdClose.Location = New System.Drawing.Point(176, 94)
            Me.CmdClose.Name = "CmdClose"
            Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.CmdClose.Size = New System.Drawing.Size(69, 23)
            Me.CmdClose.TabIndex = 7
            Me.CmdClose.Text = "取消"
            Me.CmdClose.UseVisualStyleBackColor = False
            '
            'cmdUserLogin
            '
            Me.cmdUserLogin.BackColor = System.Drawing.SystemColors.Control
            Me.cmdUserLogin.Cursor = System.Windows.Forms.Cursors.Default
            Me.cmdUserLogin.ForeColor = System.Drawing.SystemColors.ControlText
            Me.cmdUserLogin.Location = New System.Drawing.Point(108, 94)
            Me.cmdUserLogin.Name = "cmdUserLogin"
            Me.cmdUserLogin.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.cmdUserLogin.Size = New System.Drawing.Size(69, 23)
            Me.cmdUserLogin.TabIndex = 6
            Me.cmdUserLogin.Text = "进入配置"
            Me.cmdUserLogin.UseVisualStyleBackColor = False
            '
            'cbouser
            '
            Me.cbouser.BackColor = System.Drawing.SystemColors.Window
            Me.cbouser.Cursor = System.Windows.Forms.Cursors.Default
            Me.cbouser.ForeColor = System.Drawing.SystemColors.WindowText
            Me.cbouser.Location = New System.Drawing.Point(146, 18)
            Me.cbouser.Name = "cbouser"
            Me.cbouser.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.cbouser.Size = New System.Drawing.Size(125, 20)
            Me.cbouser.TabIndex = 3
            Me.cbouser.Text = "Combo1"
            '
            'txtpass
            '
            Me.txtpass.AcceptsReturn = True
            Me.txtpass.BackColor = System.Drawing.SystemColors.Window
            Me.txtpass.Cursor = System.Windows.Forms.Cursors.IBeam
            Me.txtpass.ForeColor = System.Drawing.SystemColors.WindowText
            Me.txtpass.ImeMode = System.Windows.Forms.ImeMode.Disable
            Me.txtpass.Location = New System.Drawing.Point(144, 58)
            Me.txtpass.MaxLength = 0
            Me.txtpass.Name = "txtpass"
            Me.txtpass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
            Me.txtpass.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.txtpass.Size = New System.Drawing.Size(125, 21)
            Me.txtpass.TabIndex = 2
            '
            'Frame1
            '
            Me.Frame1.BackColor = System.Drawing.SystemColors.Control
            Me.Frame1.Controls.Add(Me.cmdRemoveuser)
            Me.Frame1.Controls.Add(Me.cmdAdduser)
            Me.Frame1.Controls.Add(Me.cbolevel)
            Me.Frame1.Controls.Add(Me.txtPassword)
            Me.Frame1.Controls.Add(Me.TreeView1)
            Me.Frame1.Controls.Add(Me.Label4)
            Me.Frame1.Controls.Add(Me.Label3)
            Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Frame1.Location = New System.Drawing.Point(2, 134)
            Me.Frame1.Name = "Frame1"
            Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
            Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Frame1.Size = New System.Drawing.Size(369, 197)
            Me.Frame1.TabIndex = 0
            Me.Frame1.TabStop = False
            Me.Frame1.Text = "用户编辑"
            '
            'cmdRemoveuser
            '
            Me.cmdRemoveuser.BackColor = System.Drawing.SystemColors.Control
            Me.cmdRemoveuser.Cursor = System.Windows.Forms.Cursors.Default
            Me.cmdRemoveuser.ForeColor = System.Drawing.SystemColors.ControlText
            Me.cmdRemoveuser.Location = New System.Drawing.Point(218, 10)
            Me.cmdRemoveuser.Name = "cmdRemoveuser"
            Me.cmdRemoveuser.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.cmdRemoveuser.Size = New System.Drawing.Size(59, 23)
            Me.cmdRemoveuser.TabIndex = 15
            Me.cmdRemoveuser.Text = "删除"
            Me.cmdRemoveuser.UseVisualStyleBackColor = False
            '
            'cmdAdduser
            '
            Me.cmdAdduser.BackColor = System.Drawing.SystemColors.Control
            Me.cmdAdduser.Cursor = System.Windows.Forms.Cursors.Default
            Me.cmdAdduser.ForeColor = System.Drawing.SystemColors.ControlText
            Me.cmdAdduser.Location = New System.Drawing.Point(158, 10)
            Me.cmdAdduser.Name = "cmdAdduser"
            Me.cmdAdduser.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.cmdAdduser.Size = New System.Drawing.Size(59, 23)
            Me.cmdAdduser.TabIndex = 14
            Me.cmdAdduser.Text = "增加"
            Me.cmdAdduser.UseVisualStyleBackColor = False
            '
            'cbolevel
            '
            Me.cbolevel.BackColor = System.Drawing.SystemColors.Window
            Me.cbolevel.Cursor = System.Windows.Forms.Cursors.Default
            Me.cbolevel.ForeColor = System.Drawing.SystemColors.WindowText
            Me.cbolevel.Location = New System.Drawing.Point(214, 118)
            Me.cbolevel.Name = "cbolevel"
            Me.cbolevel.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.cbolevel.Size = New System.Drawing.Size(111, 20)
            Me.cbolevel.TabIndex = 13
            Me.cbolevel.Text = "1"
            '
            'txtPassword
            '
            Me.txtPassword.AcceptsReturn = True
            Me.txtPassword.BackColor = System.Drawing.SystemColors.Window
            Me.txtPassword.Cursor = System.Windows.Forms.Cursors.IBeam
            Me.txtPassword.ForeColor = System.Drawing.SystemColors.WindowText
            Me.txtPassword.Location = New System.Drawing.Point(214, 86)
            Me.txtPassword.MaxLength = 0
            Me.txtPassword.Name = "txtPassword"
            Me.txtPassword.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.txtPassword.Size = New System.Drawing.Size(109, 21)
            Me.txtPassword.TabIndex = 12
            Me.txtPassword.Text = "password"
            '
            'TreeView1
            '
            Me.TreeView1.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.TreeView1.LabelEdit = True
            Me.TreeView1.Location = New System.Drawing.Point(4, 36)
            Me.TreeView1.Name = "TreeView1"
            Me.TreeView1.Size = New System.Drawing.Size(115, 155)
            Me.TreeView1.TabIndex = 9
            '
            'Label4
            '
            Me.Label4.BackColor = System.Drawing.SystemColors.Control
            Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
            Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Label4.Location = New System.Drawing.Point(164, 120)
            Me.Label4.Name = "Label4"
            Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Label4.Size = New System.Drawing.Size(43, 15)
            Me.Label4.TabIndex = 11
            Me.Label4.Text = "权限："
            '
            'Label3
            '
            Me.Label3.BackColor = System.Drawing.SystemColors.Control
            Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
            Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Label3.Location = New System.Drawing.Point(166, 86)
            Me.Label3.Name = "Label3"
            Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Label3.Size = New System.Drawing.Size(37, 13)
            Me.Label3.TabIndex = 10
            Me.Label3.Text = "密码："
            '
            'Label2
            '
            Me.Label2.BackColor = System.Drawing.SystemColors.Control
            Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
            Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Label2.Location = New System.Drawing.Point(66, 62)
            Me.Label2.Name = "Label2"
            Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Label2.Size = New System.Drawing.Size(77, 13)
            Me.Label2.TabIndex = 5
            Me.Label2.Text = "请输入密码："
            '
            'Label1
            '
            Me.Label1.BackColor = System.Drawing.SystemColors.Control
            Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
            Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Label1.Location = New System.Drawing.Point(64, 22)
            Me.Label1.Name = "Label1"
            Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Label1.Size = New System.Drawing.Size(75, 15)
            Me.Label1.TabIndex = 4
            Me.Label1.Text = "请选择用户："
            '
            'cmdSaveToXml
            '
            Me.cmdSaveToXml.BackColor = System.Drawing.SystemColors.Control
            Me.cmdSaveToXml.Cursor = System.Windows.Forms.Cursors.Default
            Me.cmdSaveToXml.ForeColor = System.Drawing.SystemColors.ControlText
            Me.cmdSaveToXml.Location = New System.Drawing.Point(256, 337)
            Me.cmdSaveToXml.Name = "cmdSaveToXml"
            Me.cmdSaveToXml.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.cmdSaveToXml.Size = New System.Drawing.Size(83, 25)
            Me.cmdSaveToXml.TabIndex = 1
            Me.cmdSaveToXml.Text = "确定"
            Me.cmdSaveToXml.UseVisualStyleBackColor = False
            '
            'FrmUsermana
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.ClientSize = New System.Drawing.Size(370, 127)
            Me.Controls.Add(Me.cmdLogin)
            Me.Controls.Add(Me.cmdedituser)
            Me.Controls.Add(Me.CmdClose)
            Me.Controls.Add(Me.cmdUserLogin)
            Me.Controls.Add(Me.cbouser)
            Me.Controls.Add(Me.txtpass)
            Me.Controls.Add(Me.cmdSaveToXml)
            Me.Controls.Add(Me.Frame1)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.Label1)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
            Me.Location = New System.Drawing.Point(3, 19)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FrmUsermana"
            Me.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "用户登录"
            Me.Frame1.ResumeLayout(False)
            Me.Frame1.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Public WithEvents cmdSaveToXml As System.Windows.Forms.Button
#End Region
    End Class
End Namespace