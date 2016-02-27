<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmHeartBeat
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
    Public WithEvents tmrPoll As System.Windows.Forms.Timer
    Public WithEvents Command1 As System.Windows.Forms.Button
    Public WithEvents Text1 As System.Windows.Forms.TextBox
    Public WithEvents Text2 As System.Windows.Forms.TextBox
    Public WithEvents txtRtuname As System.Windows.Forms.TextBox
    Public WithEvents chkPollenable As System.Windows.Forms.CheckBox
    Public WithEvents txtRvtime As System.Windows.Forms.TextBox
    Public WithEvents chkGprscomdisplay As System.Windows.Forms.CheckBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label22 As System.Windows.Forms.Label
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    'Public WithEvents _WskMBClient_0 As AxMSWinsockLib.AxWinsock
    'Public WithEvents WskMBClient As AxWinsockArray
    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器来修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHeartBeat))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Frame2 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Command1 = New System.Windows.Forms.Button
        Me.Text1 = New System.Windows.Forms.TextBox
        Me.Text2 = New System.Windows.Forms.TextBox
        Me.txtRtuname = New System.Windows.Forms.TextBox
        Me.chkPollenable = New System.Windows.Forms.CheckBox
        Me.txtRvtime = New System.Windows.Forms.TextBox
        Me.chkGprscomdisplay = New System.Windows.Forms.CheckBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.tmrPoll = New System.Windows.Forms.Timer(Me.components)
        Me.Frame2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.Label1)
        Me.Frame2.Controls.Add(Me.Command1)
        Me.Frame2.Controls.Add(Me.Text1)
        Me.Frame2.Controls.Add(Me.Text2)
        Me.Frame2.Controls.Add(Me.txtRtuname)
        Me.Frame2.Controls.Add(Me.chkPollenable)
        Me.Frame2.Controls.Add(Me.txtRvtime)
        Me.Frame2.Controls.Add(Me.chkGprscomdisplay)
        Me.Frame2.Controls.Add(Me.Label6)
        Me.Frame2.Controls.Add(Me.Label2)
        Me.Frame2.Controls.Add(Me.Label7)
        Me.Frame2.Controls.Add(Me.Label22)
        Me.Frame2.Controls.Add(Me.Label23)
        Me.Frame2.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Frame2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Frame2.Location = New System.Drawing.Point(0, 0)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(512, 315)
        Me.Frame2.TabIndex = 0
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Modbus TCP client"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(294, 248)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 14)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Label1"
        '
        'Command1
        '
        Me.Command1.BackColor = System.Drawing.SystemColors.Control
        Me.Command1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command1.Location = New System.Drawing.Point(202, 264)
        Me.Command1.Name = "Command1"
        Me.Command1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command1.Size = New System.Drawing.Size(79, 25)
        Me.Command1.TabIndex = 12
        Me.Command1.Text = "确定"
        Me.Command1.UseVisualStyleBackColor = False
        '
        'Text1
        '
        Me.Text1.AcceptsReturn = True
        Me.Text1.BackColor = System.Drawing.SystemColors.Window
        Me.Text1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.Text1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Text1.Location = New System.Drawing.Point(0, 51)
        Me.Text1.MaxLength = 0
        Me.Text1.Multiline = True
        Me.Text1.Name = "Text1"
        Me.Text1.ReadOnly = True
        Me.Text1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Text1.Size = New System.Drawing.Size(475, 30)
        Me.Text1.TabIndex = 6
        '
        'Text2
        '
        Me.Text2.AcceptsReturn = True
        Me.Text2.BackColor = System.Drawing.SystemColors.Window
        Me.Text2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.Text2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Text2.Location = New System.Drawing.Point(0, 99)
        Me.Text2.MaxLength = 0
        Me.Text2.Multiline = True
        Me.Text2.Name = "Text2"
        Me.Text2.ReadOnly = True
        Me.Text2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Text2.Size = New System.Drawing.Size(473, 92)
        Me.Text2.TabIndex = 5
        '
        'txtRtuname
        '
        Me.txtRtuname.AcceptsReturn = True
        Me.txtRtuname.BackColor = System.Drawing.SystemColors.Window
        Me.txtRtuname.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRtuname.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRtuname.Location = New System.Drawing.Point(64, 208)
        Me.txtRtuname.MaxLength = 0
        Me.txtRtuname.Name = "txtRtuname"
        Me.txtRtuname.ReadOnly = True
        Me.txtRtuname.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRtuname.Size = New System.Drawing.Size(214, 23)
        Me.txtRtuname.TabIndex = 4
        Me.txtRtuname.Text = "站点名称"
        '
        'chkPollenable
        '
        Me.chkPollenable.BackColor = System.Drawing.SystemColors.Control
        Me.chkPollenable.Checked = True
        Me.chkPollenable.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPollenable.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPollenable.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPollenable.Location = New System.Drawing.Point(8, 16)
        Me.chkPollenable.Name = "chkPollenable"
        Me.chkPollenable.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPollenable.Size = New System.Drawing.Size(177, 21)
        Me.chkPollenable.TabIndex = 3
        Me.chkPollenable.Text = "启动数据采集轮询"
        Me.chkPollenable.UseVisualStyleBackColor = False
        '
        'txtRvtime
        '
        Me.txtRvtime.AcceptsReturn = True
        Me.txtRvtime.BackColor = System.Drawing.SystemColors.Window
        Me.txtRvtime.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRvtime.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRvtime.Location = New System.Drawing.Point(344, 208)
        Me.txtRvtime.MaxLength = 0
        Me.txtRvtime.Name = "txtRvtime"
        Me.txtRvtime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRvtime.Size = New System.Drawing.Size(73, 23)
        Me.txtRvtime.TabIndex = 2
        '
        'chkGprscomdisplay
        '
        Me.chkGprscomdisplay.BackColor = System.Drawing.SystemColors.Control
        Me.chkGprscomdisplay.Checked = True
        Me.chkGprscomdisplay.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkGprscomdisplay.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkGprscomdisplay.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGprscomdisplay.Location = New System.Drawing.Point(192, 16)
        Me.chkGprscomdisplay.Name = "chkGprscomdisplay"
        Me.chkGprscomdisplay.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkGprscomdisplay.Size = New System.Drawing.Size(197, 21)
        Me.chkGprscomdisplay.TabIndex = 1
        Me.chkGprscomdisplay.Text = "显示通讯细节"
        Me.chkGprscomdisplay.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(6, 40)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(52, 12)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "发送命令"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(3, 84)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "接收数据"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(8, 208)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(81, 17)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "正在巡测"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(280, 208)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(61, 15)
        Me.Label22.TabIndex = 8
        Me.Label22.Text = "返回时间："
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(424, 208)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(23, 21)
        Me.Label23.TabIndex = 7
        Me.Label23.Text = "s"
        '
        'tmrPoll
        '
        Me.tmrPoll.Enabled = True
        Me.tmrPoll.Interval = 1000
        '
        'frmHeartBeat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(516, 317)
        Me.Controls.Add(Me.Frame2)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmHeartBeat"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MODBUS TCP通讯状态"
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
#End Region
End Class