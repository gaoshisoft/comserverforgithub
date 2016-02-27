<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class Fview
#Region "Windows 窗体设计器生成的代码 "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'此调用是 Windows 窗体设计器所必需的。
		InitializeComponent()
		'此窗体是 MDI 子窗体。
		'此代码模拟 VB6 
		' 的自动加载和显示
		' MDI 子级的父级
		' 的功能。
		Me.MDIParent = GPRSComServer.MDIfmain
		GPRSComServer.MDIfmain.Show
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
	Public WithEvents chkmbComdisplay As System.Windows.Forms.CheckBox
	Public WithEvents txtmbrv As System.Windows.Forms.TextBox
	Public WithEvents txtmbsend As System.Windows.Forms.TextBox
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Label5 As System.Windows.Forms.Label
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents cmdhide As System.Windows.Forms.Button
	Public WithEvents Command5 As System.Windows.Forms.Button
	Public WithEvents Command2 As System.Windows.Forms.Button
	Public WithEvents Command3 As System.Windows.Forms.Button
	Public WithEvents Command4 As System.Windows.Forms.Button
	Public WithEvents Frame5 As System.Windows.Forms.GroupBox
	Public WithEvents chkGprscomdisplay As System.Windows.Forms.CheckBox
	Public WithEvents txtRvtime As System.Windows.Forms.TextBox
	Public WithEvents chkPollenable As System.Windows.Forms.CheckBox
	Public WithEvents txtRtuname As System.Windows.Forms.TextBox
	Public WithEvents Text2 As System.Windows.Forms.TextBox
	Public WithEvents Text1 As System.Windows.Forms.TextBox
	Public WithEvents Label23 As System.Windows.Forms.Label
	Public WithEvents Label22 As System.Windows.Forms.Label
	Public WithEvents Label7 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Label6 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents Frame2 As System.Windows.Forms.GroupBox
	'注意: 以下过程是 Windows 窗体设计器所必需的
	'可以使用 Windows 窗体设计器来修改它。
	'不要使用代码编辑器修改它。
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Fview))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Frame1 = New System.Windows.Forms.GroupBox
        Me.chkmbComdisplay = New System.Windows.Forms.CheckBox
        Me.txtmbrv = New System.Windows.Forms.TextBox
        Me.txtmbsend = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmdhide = New System.Windows.Forms.Button
        Me.Command5 = New System.Windows.Forms.Button
        Me.Frame2 = New System.Windows.Forms.GroupBox
        Me.Frame5 = New System.Windows.Forms.GroupBox
        Me.Command2 = New System.Windows.Forms.Button
        Me.Command3 = New System.Windows.Forms.Button
        Me.Command4 = New System.Windows.Forms.Button
        Me.chkGprscomdisplay = New System.Windows.Forms.CheckBox
        Me.txtRvtime = New System.Windows.Forms.TextBox
        Me.chkPollenable = New System.Windows.Forms.CheckBox
        Me.txtRtuname = New System.Windows.Forms.TextBox
        Me.Text2 = New System.Windows.Forms.TextBox
        Me.Text1 = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Frame1.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame5.SuspendLayout()
        Me.SuspendLayout()
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.chkmbComdisplay)
        Me.Frame1.Controls.Add(Me.txtmbrv)
        Me.Frame1.Controls.Add(Me.txtmbsend)
        Me.Frame1.Controls.Add(Me.Label4)
        Me.Frame1.Controls.Add(Me.Label5)
        Me.Frame1.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Frame1.ForeColor = System.Drawing.Color.Blue
        Me.Frame1.Location = New System.Drawing.Point(3, 310)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(480, 265)
        Me.Frame1.TabIndex = 19
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "MODBUS SERVER"
        '
        'chkmbComdisplay
        '
        Me.chkmbComdisplay.BackColor = System.Drawing.SystemColors.Control
        Me.chkmbComdisplay.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkmbComdisplay.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.chkmbComdisplay.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkmbComdisplay.Location = New System.Drawing.Point(176, 16)
        Me.chkmbComdisplay.Name = "chkmbComdisplay"
        Me.chkmbComdisplay.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkmbComdisplay.Size = New System.Drawing.Size(109, 18)
        Me.chkmbComdisplay.TabIndex = 24
        Me.chkmbComdisplay.Text = "显示通讯细节"
        Me.chkmbComdisplay.UseVisualStyleBackColor = False
        '
        'txtmbrv
        '
        Me.txtmbrv.AcceptsReturn = True
        Me.txtmbrv.BackColor = System.Drawing.SystemColors.Window
        Me.txtmbrv.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtmbrv.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.txtmbrv.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtmbrv.Location = New System.Drawing.Point(8, 40)
        Me.txtmbrv.MaxLength = 0
        Me.txtmbrv.Multiline = True
        Me.txtmbrv.Name = "txtmbrv"
        Me.txtmbrv.ReadOnly = True
        Me.txtmbrv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtmbrv.Size = New System.Drawing.Size(464, 60)
        Me.txtmbrv.TabIndex = 21
        '
        'txtmbsend
        '
        Me.txtmbsend.AcceptsReturn = True
        Me.txtmbsend.BackColor = System.Drawing.SystemColors.Window
        Me.txtmbsend.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtmbsend.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.txtmbsend.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtmbsend.Location = New System.Drawing.Point(8, 136)
        Me.txtmbsend.MaxLength = 0
        Me.txtmbsend.Multiline = True
        Me.txtmbsend.Name = "txtmbsend"
        Me.txtmbsend.ReadOnly = True
        Me.txtmbsend.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtmbsend.Size = New System.Drawing.Size(466, 117)
        Me.txtmbsend.TabIndex = 20
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(8, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(57, 16)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "接收命令"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(8, 112)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(73, 11)
        Me.Label5.TabIndex = 22
        Me.Label5.Text = "发送数据"
        '
        'cmdhide
        '
        Me.cmdhide.BackColor = System.Drawing.SystemColors.Control
        Me.cmdhide.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdhide.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.cmdhide.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdhide.Location = New System.Drawing.Point(344, 584)
        Me.cmdhide.Name = "cmdhide"
        Me.cmdhide.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdhide.Size = New System.Drawing.Size(106, 25)
        Me.cmdhide.TabIndex = 18
        Me.cmdhide.Text = "隐藏"
        Me.cmdhide.UseVisualStyleBackColor = False
        '
        'Command5
        '
        Me.Command5.BackColor = System.Drawing.SystemColors.Control
        Me.Command5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command5.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Command5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command5.Location = New System.Drawing.Point(878, 744)
        Me.Command5.Name = "Command5"
        Me.Command5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command5.Size = New System.Drawing.Size(87, 23)
        Me.Command5.TabIndex = 8
        Me.Command5.Text = "退出系统"
        Me.Command5.UseVisualStyleBackColor = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.Frame5)
        Me.Frame2.Controls.Add(Me.chkGprscomdisplay)
        Me.Frame2.Controls.Add(Me.txtRvtime)
        Me.Frame2.Controls.Add(Me.chkPollenable)
        Me.Frame2.Controls.Add(Me.txtRtuname)
        Me.Frame2.Controls.Add(Me.Text2)
        Me.Frame2.Controls.Add(Me.Text1)
        Me.Frame2.Controls.Add(Me.Label23)
        Me.Frame2.Controls.Add(Me.Label22)
        Me.Frame2.Controls.Add(Me.Label7)
        Me.Frame2.Controls.Add(Me.Label2)
        Me.Frame2.Controls.Add(Me.Label6)
        Me.Frame2.Controls.Add(Me.Label1)
        Me.Frame2.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Frame2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Frame2.Location = New System.Drawing.Point(3, 3)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(482, 289)
        Me.Frame2.TabIndex = 0
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "GPRS/CDMA数据服务中心"
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.Command2)
        Me.Frame5.Controls.Add(Me.Command3)
        Me.Frame5.Controls.Add(Me.Command4)
        Me.Frame5.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(8, 232)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(447, 41)
        Me.Frame5.TabIndex = 14
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "状态查看与设置"
        '
        'Command2
        '
        Me.Command2.BackColor = System.Drawing.SystemColors.Control
        Me.Command2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command2.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Command2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command2.Location = New System.Drawing.Point(272, 14)
        Me.Command2.Name = "Command2"
        Me.Command2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command2.Size = New System.Drawing.Size(73, 21)
        Me.Command2.TabIndex = 17
        Me.Command2.Text = "设置..."
        Me.Command2.UseVisualStyleBackColor = False
        '
        'Command3
        '
        Me.Command3.BackColor = System.Drawing.SystemColors.Control
        Me.Command3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command3.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Command3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command3.Location = New System.Drawing.Point(24, 14)
        Me.Command3.Name = "Command3"
        Me.Command3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command3.Size = New System.Drawing.Size(73, 21)
        Me.Command3.TabIndex = 16
        Me.Command3.Text = "收发字节数"
        Me.Command3.UseVisualStyleBackColor = False
        '
        'Command4
        '
        Me.Command4.BackColor = System.Drawing.SystemColors.Control
        Me.Command4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command4.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Command4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command4.Location = New System.Drawing.Point(148, 14)
        Me.Command4.Name = "Command4"
        Me.Command4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command4.Size = New System.Drawing.Size(73, 21)
        Me.Command4.TabIndex = 15
        Me.Command4.Text = "通讯质量"
        Me.Command4.UseVisualStyleBackColor = False
        '
        'chkGprscomdisplay
        '
        Me.chkGprscomdisplay.BackColor = System.Drawing.SystemColors.Control
        Me.chkGprscomdisplay.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkGprscomdisplay.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.chkGprscomdisplay.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGprscomdisplay.Location = New System.Drawing.Point(186, 20)
        Me.chkGprscomdisplay.Name = "chkGprscomdisplay"
        Me.chkGprscomdisplay.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkGprscomdisplay.Size = New System.Drawing.Size(197, 21)
        Me.chkGprscomdisplay.TabIndex = 13
        Me.chkGprscomdisplay.Text = "显示通讯细节"
        Me.chkGprscomdisplay.UseVisualStyleBackColor = False
        '
        'txtRvtime
        '
        Me.txtRvtime.AcceptsReturn = True
        Me.txtRvtime.BackColor = System.Drawing.SystemColors.Window
        Me.txtRvtime.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRvtime.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.txtRvtime.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRvtime.Location = New System.Drawing.Point(344, 208)
        Me.txtRvtime.MaxLength = 0
        Me.txtRvtime.Name = "txtRvtime"
        Me.txtRvtime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRvtime.Size = New System.Drawing.Size(73, 21)
        Me.txtRvtime.TabIndex = 10
        '
        'chkPollenable
        '
        Me.chkPollenable.BackColor = System.Drawing.SystemColors.Control
        Me.chkPollenable.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPollenable.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.chkPollenable.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPollenable.Location = New System.Drawing.Point(14, 18)
        Me.chkPollenable.Name = "chkPollenable"
        Me.chkPollenable.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPollenable.Size = New System.Drawing.Size(177, 21)
        Me.chkPollenable.TabIndex = 9
        Me.chkPollenable.Text = "启动数据采集轮询"
        Me.chkPollenable.UseVisualStyleBackColor = False
        '
        'txtRtuname
        '
        Me.txtRtuname.AcceptsReturn = True
        Me.txtRtuname.BackColor = System.Drawing.SystemColors.Window
        Me.txtRtuname.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRtuname.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.txtRtuname.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRtuname.Location = New System.Drawing.Point(71, 208)
        Me.txtRtuname.MaxLength = 0
        Me.txtRtuname.Name = "txtRtuname"
        Me.txtRtuname.ReadOnly = True
        Me.txtRtuname.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRtuname.Size = New System.Drawing.Size(203, 21)
        Me.txtRtuname.TabIndex = 7
        Me.txtRtuname.Text = "站点名称"
        '
        'Text2
        '
        Me.Text2.AcceptsReturn = True
        Me.Text2.BackColor = System.Drawing.SystemColors.Window
        Me.Text2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.Text2.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Text2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Text2.Location = New System.Drawing.Point(0, 99)
        Me.Text2.MaxLength = 0
        Me.Text2.Multiline = True
        Me.Text2.Name = "Text2"
        Me.Text2.ReadOnly = True
        Me.Text2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text2.Size = New System.Drawing.Size(473, 92)
        Me.Text2.TabIndex = 5
        '
        'Text1
        '
        Me.Text1.AcceptsReturn = True
        Me.Text1.BackColor = System.Drawing.SystemColors.Window
        Me.Text1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.Text1.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Text1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Text1.Location = New System.Drawing.Point(0, 51)
        Me.Text1.MaxLength = 0
        Me.Text1.Multiline = True
        Me.Text1.Name = "Text1"
        Me.Text1.ReadOnly = True
        Me.Text1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text1.Size = New System.Drawing.Size(469, 30)
        Me.Text1.TabIndex = 4
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(424, 208)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(23, 21)
        Me.Label23.TabIndex = 12
        Me.Label23.Text = "s"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(280, 208)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(82, 21)
        Me.Label22.TabIndex = 11
        Me.Label22.Text = "返回时间："
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(8, 208)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(81, 17)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "正在巡测"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(3, 84)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "接收数据"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(6, 39)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(52, 12)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "发送命令"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(8, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(97, 25)
        Me.Label1.TabIndex = 1
        '
        'Fview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(487, 624)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.cmdhide)
        Me.Controls.Add(Me.Command5)
        Me.Controls.Add(Me.Frame2)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(10, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Fview"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "通讯实时监控"
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame5.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
#End Region 
End Class