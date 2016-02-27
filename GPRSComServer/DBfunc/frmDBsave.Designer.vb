Namespace DBfunc
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmDBsave
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
        Public WithEvents Command3 As System.Windows.Forms.Button
        Public WithEvents Timer1 As System.Windows.Forms.Timer
        Public WithEvents cmdSave As System.Windows.Forms.Button
        Public WithEvents txtUpdateCycle As System.Windows.Forms.TextBox
        Public WithEvents cboConnectType As System.Windows.Forms.ComboBox
        Public WithEvents txtSavecycle As System.Windows.Forms.TextBox
        Public WithEvents cmdifStart As System.Windows.Forms.Button
        Public WithEvents txtUsername As System.Windows.Forms.TextBox
        Public WithEvents txtDbname As System.Windows.Forms.TextBox
        Public WithEvents txtPassword As System.Windows.Forms.TextBox
        Public WithEvents Command5 As System.Windows.Forms.Button
        Public WithEvents txtServerName As System.Windows.Forms.TextBox
        Public WithEvents Command6 As System.Windows.Forms.Button
        Public WithEvents Label1 As System.Windows.Forms.Label
        Public WithEvents Label2 As System.Windows.Forms.Label
        Public WithEvents Label3 As System.Windows.Forms.Label
        Public WithEvents Label4 As System.Windows.Forms.Label
        Public WithEvents frasqlset As System.Windows.Forms.Panel
        Public WithEvents CmdTestaccess As System.Windows.Forms.Button
        Public WithEvents txtAccessDBpath As System.Windows.Forms.TextBox
        Public WithEvents Label11 As System.Windows.Forms.Label
        Public WithEvents fraAccess As System.Windows.Forms.Panel
        Public WithEvents Label13 As System.Windows.Forms.Label
        Public WithEvents Label12 As System.Windows.Forms.Label
        Public WithEvents Label9 As System.Windows.Forms.Label
        Public WithEvents Label8 As System.Windows.Forms.Label
        Public WithEvents Label7 As System.Windows.Forms.Label
        Public WithEvents fraconnect As System.Windows.Forms.GroupBox
        Public WithEvents Command2 As System.Windows.Forms.Button
        Public WithEvents Command1 As System.Windows.Forms.Button
        Public WithEvents lstFields As System.Windows.Forms.ListBox
        Public WithEvents Label5 As System.Windows.Forms.Label
        Public WithEvents fratable As System.Windows.Forms.GroupBox
        Public WithEvents TreeView1 As System.Windows.Forms.TreeView
        '注意: 以下过程是 Windows 窗体设计器所必需的
        '可以使用 Windows 窗体设计器来修改它。
        '不要使用代码编辑器修改它。
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.Command3 = New System.Windows.Forms.Button()
            Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
            Me.cmdSave = New System.Windows.Forms.Button()
            Me.fraconnect = New System.Windows.Forms.GroupBox()
            Me.txtUpdateCycle = New System.Windows.Forms.TextBox()
            Me.frasqlset = New System.Windows.Forms.Panel()
            Me.txtUsername = New System.Windows.Forms.TextBox()
            Me.txtDbname = New System.Windows.Forms.TextBox()
            Me.txtPassword = New System.Windows.Forms.TextBox()
            Me.Command5 = New System.Windows.Forms.Button()
            Me.txtServerName = New System.Windows.Forms.TextBox()
            Me.Command6 = New System.Windows.Forms.Button()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.cboConnectType = New System.Windows.Forms.ComboBox()
            Me.txtSavecycle = New System.Windows.Forms.TextBox()
            Me.cmdifStart = New System.Windows.Forms.Button()
            Me.fraAccess = New System.Windows.Forms.Panel()
            Me.CmdTestaccess = New System.Windows.Forms.Button()
            Me.txtAccessDBpath = New System.Windows.Forms.TextBox()
            Me.Label11 = New System.Windows.Forms.Label()
            Me.Label15 = New System.Windows.Forms.Label()
            Me.Label14 = New System.Windows.Forms.Label()
            Me.Label10 = New System.Windows.Forms.Label()
            Me.Label13 = New System.Windows.Forms.Label()
            Me.Label12 = New System.Windows.Forms.Label()
            Me.Label9 = New System.Windows.Forms.Label()
            Me.Label8 = New System.Windows.Forms.Label()
            Me.Label7 = New System.Windows.Forms.Label()
            Me.fratable = New System.Windows.Forms.GroupBox()
            Me.txtstaname = New System.Windows.Forms.TextBox()
            Me.Command2 = New System.Windows.Forms.Button()
            Me.Command1 = New System.Windows.Forms.Button()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.lstFields = New System.Windows.Forms.ListBox()
            Me.TreeView1 = New System.Windows.Forms.TreeView()
            Me.menuall = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.menuaddtable = New System.Windows.Forms.ToolStripMenuItem()
            Me.menurename = New System.Windows.Forms.ToolStripMenuItem()
            Me.menuDelete = New System.Windows.Forms.ToolStripMenuItem()
            Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
            Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
            Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem()
            Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem()
            Me.fraconnect.SuspendLayout()
            Me.frasqlset.SuspendLayout()
            Me.fraAccess.SuspendLayout()
            Me.fratable.SuspendLayout()
            Me.menuall.SuspendLayout()
            Me.SuspendLayout()
            '
            'Command3
            '
            Me.Command3.BackColor = System.Drawing.SystemColors.Control
            Me.Command3.Cursor = System.Windows.Forms.Cursors.Default
            Me.Command3.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Command3.Location = New System.Drawing.Point(472, 560)
            Me.Command3.Name = "Command3"
            Me.Command3.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Command3.Size = New System.Drawing.Size(57, 25)
            Me.Command3.TabIndex = 39
            Me.Command3.Text = "取消"
            Me.Command3.UseVisualStyleBackColor = False
            '
            'Timer1
            '
            Me.Timer1.Enabled = True
            '
            'cmdSave
            '
            Me.cmdSave.BackColor = System.Drawing.SystemColors.Control
            Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
            Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
            Me.cmdSave.Location = New System.Drawing.Point(384, 560)
            Me.cmdSave.Name = "cmdSave"
            Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.cmdSave.Size = New System.Drawing.Size(65, 25)
            Me.cmdSave.TabIndex = 1
            Me.cmdSave.Text = "确定"
            Me.cmdSave.UseVisualStyleBackColor = False
            '
            'fraconnect
            '
            Me.fraconnect.BackColor = System.Drawing.SystemColors.Control
            Me.fraconnect.Controls.Add(Me.txtUpdateCycle)
            Me.fraconnect.Controls.Add(Me.frasqlset)
            Me.fraconnect.Controls.Add(Me.cboConnectType)
            Me.fraconnect.Controls.Add(Me.txtSavecycle)
            Me.fraconnect.Controls.Add(Me.cmdifStart)
            Me.fraconnect.Controls.Add(Me.fraAccess)
            Me.fraconnect.Controls.Add(Me.Label15)
            Me.fraconnect.Controls.Add(Me.Label14)
            Me.fraconnect.Controls.Add(Me.Label10)
            Me.fraconnect.Controls.Add(Me.Label13)
            Me.fraconnect.Controls.Add(Me.Label12)
            Me.fraconnect.Controls.Add(Me.Label9)
            Me.fraconnect.Controls.Add(Me.Label8)
            Me.fraconnect.Controls.Add(Me.Label7)
            Me.fraconnect.ForeColor = System.Drawing.SystemColors.ControlText
            Me.fraconnect.Location = New System.Drawing.Point(236, 12)
            Me.fraconnect.Name = "fraconnect"
            Me.fraconnect.Padding = New System.Windows.Forms.Padding(0)
            Me.fraconnect.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.fraconnect.Size = New System.Drawing.Size(471, 513)
            Me.fraconnect.TabIndex = 0
            Me.fraconnect.TabStop = False
            Me.fraconnect.Text = "连接设置"
            '
            'txtUpdateCycle
            '
            Me.txtUpdateCycle.AcceptsReturn = True
            Me.txtUpdateCycle.BackColor = System.Drawing.SystemColors.Window
            Me.txtUpdateCycle.Cursor = System.Windows.Forms.Cursors.IBeam
            Me.txtUpdateCycle.ForeColor = System.Drawing.SystemColors.WindowText
            Me.txtUpdateCycle.Location = New System.Drawing.Point(376, 352)
            Me.txtUpdateCycle.MaxLength = 0
            Me.txtUpdateCycle.Name = "txtUpdateCycle"
            Me.txtUpdateCycle.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.txtUpdateCycle.Size = New System.Drawing.Size(49, 21)
            Me.txtUpdateCycle.TabIndex = 29
            '
            'frasqlset
            '
            Me.frasqlset.BackColor = System.Drawing.SystemColors.Control
            Me.frasqlset.Controls.Add(Me.txtUsername)
            Me.frasqlset.Controls.Add(Me.txtDbname)
            Me.frasqlset.Controls.Add(Me.txtPassword)
            Me.frasqlset.Controls.Add(Me.Command5)
            Me.frasqlset.Controls.Add(Me.txtServerName)
            Me.frasqlset.Controls.Add(Me.Command6)
            Me.frasqlset.Controls.Add(Me.Label1)
            Me.frasqlset.Controls.Add(Me.Label2)
            Me.frasqlset.Controls.Add(Me.Label3)
            Me.frasqlset.Controls.Add(Me.Label4)
            Me.frasqlset.Cursor = System.Windows.Forms.Cursors.Default
            Me.frasqlset.ForeColor = System.Drawing.SystemColors.ControlText
            Me.frasqlset.Location = New System.Drawing.Point(11, 36)
            Me.frasqlset.Name = "frasqlset"
            Me.frasqlset.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.frasqlset.Size = New System.Drawing.Size(414, 286)
            Me.frasqlset.TabIndex = 6
            '
            'txtUsername
            '
            Me.txtUsername.AcceptsReturn = True
            Me.txtUsername.BackColor = System.Drawing.SystemColors.Window
            Me.txtUsername.Cursor = System.Windows.Forms.Cursors.IBeam
            Me.txtUsername.ForeColor = System.Drawing.SystemColors.WindowText
            Me.txtUsername.Location = New System.Drawing.Point(128, 82)
            Me.txtUsername.MaxLength = 0
            Me.txtUsername.Name = "txtUsername"
            Me.txtUsername.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.txtUsername.Size = New System.Drawing.Size(145, 21)
            Me.txtUsername.TabIndex = 16
            '
            'txtDbname
            '
            Me.txtDbname.AcceptsReturn = True
            Me.txtDbname.BackColor = System.Drawing.SystemColors.Window
            Me.txtDbname.Cursor = System.Windows.Forms.Cursors.IBeam
            Me.txtDbname.ForeColor = System.Drawing.SystemColors.WindowText
            Me.txtDbname.Location = New System.Drawing.Point(128, 41)
            Me.txtDbname.MaxLength = 0
            Me.txtDbname.Name = "txtDbname"
            Me.txtDbname.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.txtDbname.Size = New System.Drawing.Size(145, 21)
            Me.txtDbname.TabIndex = 11
            '
            'txtPassword
            '
            Me.txtPassword.AcceptsReturn = True
            Me.txtPassword.BackColor = System.Drawing.SystemColors.Window
            Me.txtPassword.Cursor = System.Windows.Forms.Cursors.IBeam
            Me.txtPassword.ForeColor = System.Drawing.SystemColors.WindowText
            Me.txtPassword.ImeMode = System.Windows.Forms.ImeMode.Disable
            Me.txtPassword.Location = New System.Drawing.Point(128, 130)
            Me.txtPassword.MaxLength = 0
            Me.txtPassword.Name = "txtPassword"
            Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
            Me.txtPassword.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.txtPassword.Size = New System.Drawing.Size(145, 21)
            Me.txtPassword.TabIndex = 10
            '
            'Command5
            '
            Me.Command5.BackColor = System.Drawing.SystemColors.Control
            Me.Command5.Cursor = System.Windows.Forms.Cursors.Default
            Me.Command5.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Command5.Location = New System.Drawing.Point(124, 217)
            Me.Command5.Name = "Command5"
            Me.Command5.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Command5.Size = New System.Drawing.Size(153, 25)
            Me.Command5.TabIndex = 9
            Me.Command5.Text = "测试连接"
            Me.Command5.UseVisualStyleBackColor = False
            '
            'txtServerName
            '
            Me.txtServerName.AcceptsReturn = True
            Me.txtServerName.BackColor = System.Drawing.SystemColors.Window
            Me.txtServerName.Cursor = System.Windows.Forms.Cursors.IBeam
            Me.txtServerName.ForeColor = System.Drawing.SystemColors.WindowText
            Me.txtServerName.Location = New System.Drawing.Point(128, 0)
            Me.txtServerName.MaxLength = 0
            Me.txtServerName.Name = "txtServerName"
            Me.txtServerName.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.txtServerName.Size = New System.Drawing.Size(145, 21)
            Me.txtServerName.TabIndex = 8
            '
            'Command6
            '
            Me.Command6.BackColor = System.Drawing.SystemColors.Control
            Me.Command6.Cursor = System.Windows.Forms.Cursors.Default
            Me.Command6.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Command6.Location = New System.Drawing.Point(125, 257)
            Me.Command6.Name = "Command6"
            Me.Command6.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Command6.Size = New System.Drawing.Size(153, 25)
            Me.Command6.TabIndex = 7
            Me.Command6.Text = "在该数据库中建表"
            Me.Command6.UseVisualStyleBackColor = False
            '
            'Label1
            '
            Me.Label1.BackColor = System.Drawing.SystemColors.Control
            Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
            Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Label1.Location = New System.Drawing.Point(56, 42)
            Me.Label1.Name = "Label1"
            Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Label1.Size = New System.Drawing.Size(65, 17)
            Me.Label1.TabIndex = 15
            Me.Label1.Text = "数据库名："
            Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'Label2
            '
            Me.Label2.BackColor = System.Drawing.SystemColors.Control
            Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
            Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Label2.Location = New System.Drawing.Point(64, 82)
            Me.Label2.Name = "Label2"
            Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Label2.Size = New System.Drawing.Size(57, 17)
            Me.Label2.TabIndex = 14
            Me.Label2.Text = "用户名："
            Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'Label3
            '
            Me.Label3.BackColor = System.Drawing.SystemColors.Control
            Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
            Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Label3.Location = New System.Drawing.Point(72, 130)
            Me.Label3.Name = "Label3"
            Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Label3.Size = New System.Drawing.Size(49, 17)
            Me.Label3.TabIndex = 13
            Me.Label3.Text = "密码："
            Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'Label4
            '
            Me.Label4.BackColor = System.Drawing.SystemColors.Control
            Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
            Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Label4.Location = New System.Drawing.Point(16, 10)
            Me.Label4.Name = "Label4"
            Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Label4.Size = New System.Drawing.Size(105, 17)
            Me.Label4.TabIndex = 12
            Me.Label4.Text = "服务器实例名称："
            Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'cboConnectType
            '
            Me.cboConnectType.BackColor = System.Drawing.SystemColors.Window
            Me.cboConnectType.Cursor = System.Windows.Forms.Cursors.Default
            Me.cboConnectType.ForeColor = System.Drawing.SystemColors.WindowText
            Me.cboConnectType.Location = New System.Drawing.Point(134, 10)
            Me.cboConnectType.Name = "cboConnectType"
            Me.cboConnectType.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.cboConnectType.Size = New System.Drawing.Size(165, 20)
            Me.cboConnectType.TabIndex = 22
            Me.cboConnectType.Text = "Combo1"
            '
            'txtSavecycle
            '
            Me.txtSavecycle.AcceptsReturn = True
            Me.txtSavecycle.BackColor = System.Drawing.SystemColors.Window
            Me.txtSavecycle.Cursor = System.Windows.Forms.Cursors.IBeam
            Me.txtSavecycle.ForeColor = System.Drawing.SystemColors.WindowText
            Me.txtSavecycle.Location = New System.Drawing.Point(376, 328)
            Me.txtSavecycle.MaxLength = 0
            Me.txtSavecycle.Name = "txtSavecycle"
            Me.txtSavecycle.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.txtSavecycle.Size = New System.Drawing.Size(49, 21)
            Me.txtSavecycle.TabIndex = 3
            '
            'cmdifStart
            '
            Me.cmdifStart.BackColor = System.Drawing.SystemColors.Control
            Me.cmdifStart.Cursor = System.Windows.Forms.Cursors.Default
            Me.cmdifStart.ForeColor = System.Drawing.SystemColors.ControlText
            Me.cmdifStart.Location = New System.Drawing.Point(136, 336)
            Me.cmdifStart.Name = "cmdifStart"
            Me.cmdifStart.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.cmdifStart.Size = New System.Drawing.Size(153, 25)
            Me.cmdifStart.TabIndex = 2
            Me.cmdifStart.Text = "启动数据存储"
            Me.cmdifStart.UseVisualStyleBackColor = False
            '
            'fraAccess
            '
            Me.fraAccess.BackColor = System.Drawing.SystemColors.Control
            Me.fraAccess.Controls.Add(Me.CmdTestaccess)
            Me.fraAccess.Controls.Add(Me.txtAccessDBpath)
            Me.fraAccess.Controls.Add(Me.Label11)
            Me.fraAccess.Cursor = System.Windows.Forms.Cursors.Default
            Me.fraAccess.ForeColor = System.Drawing.SystemColors.ControlText
            Me.fraAccess.Location = New System.Drawing.Point(72, 36)
            Me.fraAccess.Name = "fraAccess"
            Me.fraAccess.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.fraAccess.Size = New System.Drawing.Size(297, 289)
            Me.fraAccess.TabIndex = 25
            Me.fraAccess.Text = "Frame1"
            '
            'CmdTestaccess
            '
            Me.CmdTestaccess.BackColor = System.Drawing.SystemColors.Control
            Me.CmdTestaccess.Cursor = System.Windows.Forms.Cursors.Default
            Me.CmdTestaccess.ForeColor = System.Drawing.SystemColors.ControlText
            Me.CmdTestaccess.Location = New System.Drawing.Point(64, 233)
            Me.CmdTestaccess.Name = "CmdTestaccess"
            Me.CmdTestaccess.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.CmdTestaccess.Size = New System.Drawing.Size(153, 25)
            Me.CmdTestaccess.TabIndex = 28
            Me.CmdTestaccess.Text = "建立数据库并测试连接"
            Me.CmdTestaccess.UseVisualStyleBackColor = False
            '
            'txtAccessDBpath
            '
            Me.txtAccessDBpath.AcceptsReturn = True
            Me.txtAccessDBpath.BackColor = System.Drawing.SystemColors.Window
            Me.txtAccessDBpath.Cursor = System.Windows.Forms.Cursors.IBeam
            Me.txtAccessDBpath.ForeColor = System.Drawing.SystemColors.WindowText
            Me.txtAccessDBpath.Location = New System.Drawing.Point(62, 24)
            Me.txtAccessDBpath.MaxLength = 0
            Me.txtAccessDBpath.Name = "txtAccessDBpath"
            Me.txtAccessDBpath.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.txtAccessDBpath.Size = New System.Drawing.Size(161, 21)
            Me.txtAccessDBpath.TabIndex = 26
            Me.txtAccessDBpath.Text = "D:\"
            '
            'Label11
            '
            Me.Label11.BackColor = System.Drawing.SystemColors.Control
            Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
            Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Label11.Location = New System.Drawing.Point(64, 4)
            Me.Label11.Name = "Label11"
            Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Label11.Size = New System.Drawing.Size(81, 17)
            Me.Label11.TabIndex = 27
            Me.Label11.Text = "MDB文件目录："
            '
            'Label15
            '
            Me.Label15.BackColor = System.Drawing.SystemColors.Control
            Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
            Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Label15.Location = New System.Drawing.Point(55, 472)
            Me.Label15.Name = "Label15"
            Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Label15.Size = New System.Drawing.Size(314, 14)
            Me.Label15.TabIndex = 15
            Me.Label15.Text = "*Mysql 实例名可以填写本机IP，安装时选myisam引擎"
            Me.Label15.TextAlign = System.Drawing.ContentAlignment.BottomLeft
            '
            'Label14
            '
            Me.Label14.BackColor = System.Drawing.SystemColors.Control
            Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
            Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Label14.Location = New System.Drawing.Point(55, 445)
            Me.Label14.Name = "Label14"
            Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Label14.Size = New System.Drawing.Size(257, 14)
            Me.Label14.TabIndex = 15
            Me.Label14.Text = "*SQL server 默认为连接本机实例"
            Me.Label14.TextAlign = System.Drawing.ContentAlignment.BottomLeft
            '
            'Label10
            '
            Me.Label10.BackColor = System.Drawing.SystemColors.Control
            Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
            Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Label10.Location = New System.Drawing.Point(55, 413)
            Me.Label10.Name = "Label10"
            Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Label10.Size = New System.Drawing.Size(257, 31)
            Me.Label10.TabIndex = 15
            Me.Label10.Text = "*如果是windows 集成验证方式，则用户名与密码为空。"
            Me.Label10.TextAlign = System.Drawing.ContentAlignment.BottomLeft
            '
            'Label13
            '
            Me.Label13.BackColor = System.Drawing.SystemColors.Control
            Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
            Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Label13.Location = New System.Drawing.Point(304, 352)
            Me.Label13.Name = "Label13"
            Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Label13.Size = New System.Drawing.Size(65, 17)
            Me.Label13.TabIndex = 31
            Me.Label13.Text = "更新周期："
            '
            'Label12
            '
            Me.Label12.BackColor = System.Drawing.SystemColors.Control
            Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
            Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Label12.Location = New System.Drawing.Point(432, 352)
            Me.Label12.Name = "Label12"
            Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Label12.Size = New System.Drawing.Size(33, 17)
            Me.Label12.TabIndex = 30
            Me.Label12.Text = "秒"
            '
            'Label9
            '
            Me.Label9.BackColor = System.Drawing.SystemColors.Control
            Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
            Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Label9.Location = New System.Drawing.Point(70, 14)
            Me.Label9.Name = "Label9"
            Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Label9.Size = New System.Drawing.Size(61, 11)
            Me.Label9.TabIndex = 23
            Me.Label9.Text = "连接类型："
            '
            'Label8
            '
            Me.Label8.BackColor = System.Drawing.SystemColors.Control
            Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
            Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Label8.Location = New System.Drawing.Point(432, 328)
            Me.Label8.Name = "Label8"
            Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Label8.Size = New System.Drawing.Size(33, 17)
            Me.Label8.TabIndex = 5
            Me.Label8.Text = "秒"
            '
            'Label7
            '
            Me.Label7.BackColor = System.Drawing.SystemColors.Control
            Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
            Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Label7.Location = New System.Drawing.Point(304, 328)
            Me.Label7.Name = "Label7"
            Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Label7.Size = New System.Drawing.Size(65, 17)
            Me.Label7.TabIndex = 4
            Me.Label7.Text = "存库周期："
            '
            'fratable
            '
            Me.fratable.BackColor = System.Drawing.SystemColors.Control
            Me.fratable.Controls.Add(Me.txtstaname)
            Me.fratable.Controls.Add(Me.Command2)
            Me.fratable.Controls.Add(Me.Command1)
            Me.fratable.Controls.Add(Me.Label6)
            Me.fratable.Controls.Add(Me.Label5)
            Me.fratable.Controls.Add(Me.lstFields)
            Me.fratable.ForeColor = System.Drawing.SystemColors.ControlText
            Me.fratable.Location = New System.Drawing.Point(231, 12)
            Me.fratable.Name = "fratable"
            Me.fratable.Padding = New System.Windows.Forms.Padding(0)
            Me.fratable.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.fratable.Size = New System.Drawing.Size(474, 533)
            Me.fratable.TabIndex = 32
            Me.fratable.TabStop = False
            Me.fratable.Text = "表设置"
            '
            'txtstaname
            '
            Me.txtstaname.Location = New System.Drawing.Point(99, 14)
            Me.txtstaname.Name = "txtstaname"
            Me.txtstaname.Size = New System.Drawing.Size(254, 21)
            Me.txtstaname.TabIndex = 39
            '
            'Command2
            '
            Me.Command2.BackColor = System.Drawing.SystemColors.Control
            Me.Command2.Cursor = System.Windows.Forms.Cursors.Default
            Me.Command2.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Command2.Location = New System.Drawing.Point(368, 432)
            Me.Command2.Name = "Command2"
            Me.Command2.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Command2.Size = New System.Drawing.Size(73, 25)
            Me.Command2.TabIndex = 37
            Me.Command2.Text = "删除字段"
            Me.Command2.UseVisualStyleBackColor = False
            '
            'Command1
            '
            Me.Command1.BackColor = System.Drawing.SystemColors.Control
            Me.Command1.Cursor = System.Windows.Forms.Cursors.Default
            Me.Command1.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Command1.Location = New System.Drawing.Point(368, 400)
            Me.Command1.Name = "Command1"
            Me.Command1.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Command1.Size = New System.Drawing.Size(73, 25)
            Me.Command1.TabIndex = 36
            Me.Command1.Text = "增加字段"
            Me.Command1.UseVisualStyleBackColor = False
            '
            'Label6
            '
            Me.Label6.BackColor = System.Drawing.SystemColors.Control
            Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
            Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Label6.Location = New System.Drawing.Point(3, 18)
            Me.Label6.Name = "Label6"
            Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Label6.Size = New System.Drawing.Size(100, 19)
            Me.Label6.TabIndex = 38
            Me.Label6.Text = "表对应的站名："
            '
            'Label5
            '
            Me.Label5.BackColor = System.Drawing.SystemColors.Control
            Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
            Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Label5.Location = New System.Drawing.Point(3, 39)
            Me.Label5.Name = "Label5"
            Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Label5.Size = New System.Drawing.Size(81, 17)
            Me.Label5.TabIndex = 38
            Me.Label5.Text = "表中的字段："
            '
            'lstFields
            '
            Me.lstFields.BackColor = System.Drawing.SystemColors.Window
            Me.lstFields.Cursor = System.Windows.Forms.Cursors.Default
            Me.lstFields.ForeColor = System.Drawing.SystemColors.WindowText
            Me.lstFields.ItemHeight = 12
            Me.lstFields.Location = New System.Drawing.Point(99, 40)
            Me.lstFields.Name = "lstFields"
            Me.lstFields.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.lstFields.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
            Me.lstFields.Size = New System.Drawing.Size(254, 460)
            Me.lstFields.TabIndex = 35
            '
            'TreeView1
            '
            Me.TreeView1.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.TreeView1.LabelEdit = True
            Me.TreeView1.Location = New System.Drawing.Point(0, -1)
            Me.TreeView1.Name = "TreeView1"
            Me.TreeView1.Size = New System.Drawing.Size(225, 546)
            Me.TreeView1.TabIndex = 24
            '
            'menuall
            '
            Me.menuall.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuaddtable, Me.menurename, Me.menuDelete})
            Me.menuall.Name = "menuall"
            Me.menuall.Size = New System.Drawing.Size(113, 70)
            '
            'menuaddtable
            '
            Me.menuaddtable.Name = "menuaddtable"
            Me.menuaddtable.Size = New System.Drawing.Size(112, 22)
            Me.menuaddtable.Text = "增加表"
            '
            'menurename
            '
            Me.menurename.Name = "menurename"
            Me.menurename.Size = New System.Drawing.Size(112, 22)
            Me.menurename.Text = "重命名"
            '
            'menuDelete
            '
            Me.menuDelete.Name = "menuDelete"
            Me.menuDelete.Size = New System.Drawing.Size(112, 22)
            Me.menuDelete.Text = "删除"
            '
            'ToolStripMenuItem2
            '
            Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
            Me.ToolStripMenuItem2.Size = New System.Drawing.Size(118, 22)
            Me.ToolStripMenuItem2.Text = "增加连接"
            '
            'ToolStripMenuItem3
            '
            Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
            Me.ToolStripMenuItem3.Size = New System.Drawing.Size(118, 22)
            Me.ToolStripMenuItem3.Text = "增加表"
            '
            'ToolStripMenuItem4
            '
            Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
            Me.ToolStripMenuItem4.Size = New System.Drawing.Size(118, 22)
            Me.ToolStripMenuItem4.Text = "重命名"
            '
            'ToolStripMenuItem5
            '
            Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
            Me.ToolStripMenuItem5.Size = New System.Drawing.Size(118, 22)
            Me.ToolStripMenuItem5.Text = "删除"
            '
            'FrmDBsave
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.ClientSize = New System.Drawing.Size(709, 594)
            Me.ControlBox = False
            Me.Controls.Add(Me.fraconnect)
            Me.Controls.Add(Me.Command3)
            Me.Controls.Add(Me.cmdSave)
            Me.Controls.Add(Me.fratable)
            Me.Controls.Add(Me.TreeView1)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Location = New System.Drawing.Point(10, 10)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FrmDBsave"
            Me.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "数据存库设置"
            Me.fraconnect.ResumeLayout(False)
            Me.fraconnect.PerformLayout()
            Me.frasqlset.ResumeLayout(False)
            Me.frasqlset.PerformLayout()
            Me.fraAccess.ResumeLayout(False)
            Me.fraAccess.PerformLayout()
            Me.fratable.ResumeLayout(False)
            Me.fratable.PerformLayout()
            Me.menuall.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents menuall As System.Windows.Forms.ContextMenuStrip
        Public WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents menuaddtable As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents menurename As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents menuDelete As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents Label10 As System.Windows.Forms.Label
        Friend WithEvents txtstaname As System.Windows.Forms.TextBox
        Public WithEvents Label6 As System.Windows.Forms.Label
        Public WithEvents Label15 As System.Windows.Forms.Label
        Public WithEvents Label14 As System.Windows.Forms.Label
#End Region
    End Class
End Namespace