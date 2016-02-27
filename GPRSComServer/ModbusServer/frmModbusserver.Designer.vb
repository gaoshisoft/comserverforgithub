<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmModbusserver
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub
	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents txtTcpPort As System.Windows.Forms.TextBox
	Public WithEvents Command2 As System.Windows.Forms.Button
    Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents Command1 As System.Windows.Forms.Button
	Public WithEvents Text5 As System.Windows.Forms.TextBox
	Public WithEvents Combo1 As System.Windows.Forms.ComboBox
	Public WithEvents Combo2 As System.Windows.Forms.ComboBox
	Public WithEvents TxtNum As System.Windows.Forms.TextBox
	Public WithEvents chkStartAd As System.Windows.Forms.CheckBox
	Public WithEvents Option4 As System.Windows.Forms.RadioButton
	Public WithEvents Option2 As System.Windows.Forms.RadioButton
	Public WithEvents Option1 As System.Windows.Forms.RadioButton
	Public WithEvents Option3 As System.Windows.Forms.RadioButton
	Public WithEvents Frame4 As System.Windows.Forms.GroupBox
	Public WithEvents _ListView2_ColumnHeader_1 As System.Windows.Forms.ColumnHeader
	Public WithEvents _ListView2_ColumnHeader_2 As System.Windows.Forms.ColumnHeader
	Public WithEvents _ListView2_ColumnHeader_3 As System.Windows.Forms.ColumnHeader
	Public WithEvents _ListView2_ColumnHeader_4 As System.Windows.Forms.ColumnHeader
	Public WithEvents ListView2 As System.Windows.Forms.ListView
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Label20 As System.Windows.Forms.Label
	Public WithEvents _Label12_0 As System.Windows.Forms.Label
	Public WithEvents Label13 As System.Windows.Forms.Label
	Public WithEvents Label14 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmModbusserver))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtTcpPort = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Text5 = New System.Windows.Forms.TextBox
        Me.Combo1 = New System.Windows.Forms.ComboBox
        Me.TxtNum = New System.Windows.Forms.TextBox
        Me.chkStartAd = New System.Windows.Forms.CheckBox
        Me.Option4 = New System.Windows.Forms.RadioButton
        Me.Option2 = New System.Windows.Forms.RadioButton
        Me.Option3 = New System.Windows.Forms.RadioButton
        Me.Command2 = New System.Windows.Forms.Button
        Me.Frame1 = New System.Windows.Forms.GroupBox
        Me.Command1 = New System.Windows.Forms.Button
        Me.Combo2 = New System.Windows.Forms.ComboBox
        Me.Frame4 = New System.Windows.Forms.GroupBox
        Me.Option1 = New System.Windows.Forms.RadioButton
        Me.ListView2 = New System.Windows.Forms.ListView
        Me._ListView2_ColumnHeader_1 = New System.Windows.Forms.ColumnHeader
        Me._ListView2_ColumnHeader_2 = New System.Windows.Forms.ColumnHeader
        Me._ListView2_ColumnHeader_3 = New System.Windows.Forms.ColumnHeader
        Me._ListView2_ColumnHeader_4 = New System.Windows.Forms.ColumnHeader
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me._Label12_0 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Frame1.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtTcpPort
        '
        Me.txtTcpPort.AcceptsReturn = True
        Me.txtTcpPort.BackColor = System.Drawing.SystemColors.Window
        Me.txtTcpPort.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTcpPort.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTcpPort.Location = New System.Drawing.Point(336, 424)
        Me.txtTcpPort.MaxLength = 0
        Me.txtTcpPort.Name = "txtTcpPort"
        Me.txtTcpPort.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTcpPort.Size = New System.Drawing.Size(65, 21)
        Me.txtTcpPort.TabIndex = 20
        Me.ToolTip1.SetToolTip(Me.txtTcpPort, "显示当前Modbus Tcp server的端口号")
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(16, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(606, 33)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Label1"
        Me.ToolTip1.SetToolTip(Me.Label1, "显示采集数据与Modbus Tcp server数据的地址对应关系")
        '
        'Text5
        '
        Me.Text5.AcceptsReturn = True
        Me.Text5.BackColor = System.Drawing.SystemColors.Window
        Me.Text5.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.Text5.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Text5.Location = New System.Drawing.Point(144, 424)
        Me.Text5.MaxLength = 0
        Me.Text5.Name = "Text5"
        Me.Text5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text5.Size = New System.Drawing.Size(112, 21)
        Me.Text5.TabIndex = 11
        Me.Text5.Text = "127.0.0.1"
        Me.ToolTip1.SetToolTip(Me.Text5, "显示当前modbus Tcp server的IP")
        '
        'Combo1
        '
        Me.Combo1.BackColor = System.Drawing.SystemColors.Window
        Me.Combo1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Combo1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Combo1.Location = New System.Drawing.Point(64, 8)
        Me.Combo1.Name = "Combo1"
        Me.Combo1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Combo1.Size = New System.Drawing.Size(105, 20)
        Me.Combo1.TabIndex = 7
        Me.Combo1.Text = "255"
        Me.ToolTip1.SetToolTip(Me.Combo1, "选择DeviceID(设备地址)")
        '
        'TxtNum
        '
        Me.TxtNum.AcceptsReturn = True
        Me.TxtNum.BackColor = System.Drawing.SystemColors.Window
        Me.TxtNum.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtNum.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtNum.Location = New System.Drawing.Point(464, 8)
        Me.TxtNum.MaxLength = 0
        Me.TxtNum.Name = "TxtNum"
        Me.TxtNum.ReadOnly = True
        Me.TxtNum.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtNum.Size = New System.Drawing.Size(89, 21)
        Me.TxtNum.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.TxtNum, "显示设定的地址数量")
        '
        'chkStartAd
        '
        Me.chkStartAd.BackColor = System.Drawing.SystemColors.Control
        Me.chkStartAd.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkStartAd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkStartAd.Location = New System.Drawing.Point(64, 16)
        Me.chkStartAd.Name = "chkStartAd"
        Me.chkStartAd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkStartAd.Size = New System.Drawing.Size(81, 17)
        Me.chkStartAd.TabIndex = 17
        Me.chkStartAd.Text = "偶起始地址"
        Me.ToolTip1.SetToolTip(Me.chkStartAd, "有时浮点数的起始地址是偶数地址，必须选择偶起始地址才能正确显示")
        Me.chkStartAd.UseVisualStyleBackColor = False
        '
        'Option4
        '
        Me.Option4.BackColor = System.Drawing.SystemColors.Control
        Me.Option4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Option4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Option4.Location = New System.Drawing.Point(384, 16)
        Me.Option4.Name = "Option4"
        Me.Option4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Option4.Size = New System.Drawing.Size(106, 17)
        Me.Option4.TabIndex = 13
        Me.Option4.TabStop = True
        Me.Option4.Text = "浮点高低字交换"
        Me.ToolTip1.SetToolTip(Me.Option4, "以标准浮点数显示（32位），不过是先把高低字交换一下")
        Me.Option4.UseVisualStyleBackColor = False
        '
        'Option2
        '
        Me.Option2.BackColor = System.Drawing.SystemColors.Control
        Me.Option2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Option2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Option2.Location = New System.Drawing.Point(248, 16)
        Me.Option2.Name = "Option2"
        Me.Option2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Option2.Size = New System.Drawing.Size(64, 19)
        Me.Option2.TabIndex = 3
        Me.Option2.TabStop = True
        Me.Option2.Text = "二进制"
        Me.ToolTip1.SetToolTip(Me.Option2, "以二进制形式显示")
        Me.Option2.UseVisualStyleBackColor = False
        '
        'Option3
        '
        Me.Option3.BackColor = System.Drawing.SystemColors.Control
        Me.Option3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Option3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Option3.Location = New System.Drawing.Point(312, 16)
        Me.Option3.Name = "Option3"
        Me.Option3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Option3.Size = New System.Drawing.Size(61, 19)
        Me.Option3.TabIndex = 1
        Me.Option3.TabStop = True
        Me.Option3.Text = "浮点数"
        Me.ToolTip1.SetToolTip(Me.Option3, "以标准浮点数显示（32位）")
        Me.Option3.UseVisualStyleBackColor = False
        '
        'Command2
        '
        Me.Command2.BackColor = System.Drawing.SystemColors.Control
        Me.Command2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command2.Location = New System.Drawing.Point(440, 424)
        Me.Command2.Name = "Command2"
        Me.Command2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command2.Size = New System.Drawing.Size(83, 22)
        Me.Command2.TabIndex = 18
        Me.Command2.Text = "输入测试值"
        Me.Command2.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.Label1)
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 360)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(635, 55)
        Me.Frame1.TabIndex = 14
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "设备说明"
        '
        'Command1
        '
        Me.Command1.BackColor = System.Drawing.SystemColors.Control
        Me.Command1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command1.Location = New System.Drawing.Point(536, 424)
        Me.Command1.Name = "Command1"
        Me.Command1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command1.Size = New System.Drawing.Size(89, 25)
        Me.Command1.TabIndex = 12
        Me.Command1.Text = "关闭"
        Me.Command1.UseVisualStyleBackColor = False
        '
        'Combo2
        '
        Me.Combo2.BackColor = System.Drawing.SystemColors.Window
        Me.Combo2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Combo2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Combo2.Items.AddRange(New Object() {"Holding Registers"})
        Me.Combo2.Location = New System.Drawing.Point(272, 8)
        Me.Combo2.Name = "Combo2"
        Me.Combo2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Combo2.Size = New System.Drawing.Size(105, 20)
        Me.Combo2.TabIndex = 6
        Me.Combo2.Text = "Holding Registers"
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.chkStartAd)
        Me.Frame4.Controls.Add(Me.Option4)
        Me.Frame4.Controls.Add(Me.Option2)
        Me.Frame4.Controls.Add(Me.Option1)
        Me.Frame4.Controls.Add(Me.Option3)
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 320)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(635, 40)
        Me.Frame4.TabIndex = 0
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "显示设置"
        '
        'Option1
        '
        Me.Option1.BackColor = System.Drawing.SystemColors.Control
        Me.Option1.Checked = True
        Me.Option1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Option1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Option1.Location = New System.Drawing.Point(160, 16)
        Me.Option1.Name = "Option1"
        Me.Option1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Option1.Size = New System.Drawing.Size(91, 22)
        Me.Option1.TabIndex = 2
        Me.Option1.TabStop = True
        Me.Option1.Text = "无符号整数"
        Me.Option1.UseVisualStyleBackColor = False
        '
        'ListView2
        '
        Me.ListView2.Alignment = System.Windows.Forms.ListViewAlignment.Left
        Me.ListView2.BackColor = System.Drawing.SystemColors.Window
        Me.ListView2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListView2.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me._ListView2_ColumnHeader_1, Me._ListView2_ColumnHeader_2, Me._ListView2_ColumnHeader_3, Me._ListView2_ColumnHeader_4})
        Me.ListView2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ListView2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.ListView2.HoverSelection = True
        Me.ListView2.Location = New System.Drawing.Point(3, 32)
        Me.ListView2.Name = "ListView2"
        Me.ListView2.Size = New System.Drawing.Size(632, 281)
        Me.ListView2.TabIndex = 4
        Me.ListView2.UseCompatibleStateImageBehavior = False
        Me.ListView2.View = System.Windows.Forms.View.Details
        '
        '_ListView2_ColumnHeader_1
        '
        Me._ListView2_ColumnHeader_1.Width = 236
        '
        '_ListView2_ColumnHeader_2
        '
        Me._ListView2_ColumnHeader_2.Width = 236
        '
        '_ListView2_ColumnHeader_3
        '
        Me._ListView2_ColumnHeader_3.Width = 236
        '
        '_ListView2_ColumnHeader_4
        '
        Me._ListView2_ColumnHeader_4.Width = 236
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(272, 424)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(49, 17)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "端口号："
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(8, 424)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(129, 16)
        Me.Label20.TabIndex = 16
        Me.Label20.Text = "Modbus Tcp Server IP:"
        '
        '_Label12_0
        '
        Me._Label12_0.BackColor = System.Drawing.SystemColors.Control
        Me._Label12_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label12_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label12_0.Location = New System.Drawing.Point(3, 12)
        Me._Label12_0.Name = "_Label12_0"
        Me._Label12_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label12_0.Size = New System.Drawing.Size(57, 17)
        Me._Label12_0.TabIndex = 10
        Me._Label12_0.Text = "DeviceID："
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(174, 12)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(97, 17)
        Me.Label13.TabIndex = 9
        Me.Label13.Text = "选择寄存器类型："
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(416, 8)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(41, 17)
        Me.Label14.TabIndex = 8
        Me.Label14.Text = "数量："
        '
        'frmModbusserver
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(645, 481)
        Me.Controls.Add(Me.txtTcpPort)
        Me.Controls.Add(Me.Command2)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Command1)
        Me.Controls.Add(Me.Text5)
        Me.Controls.Add(Me.Combo1)
        Me.Controls.Add(Me.Combo2)
        Me.Controls.Add(Me.TxtNum)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.ListView2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me._Label12_0)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label14)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmModbusserver"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Modbus Tcp Server实时数据显示"
        Me.Frame1.ResumeLayout(False)
        Me.Frame4.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region 
End Class