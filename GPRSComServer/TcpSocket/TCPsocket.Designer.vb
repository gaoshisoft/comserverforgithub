Imports System.Net
Imports System.Net.Sockets
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class TCPsocket

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
    Public WithEvents Command1 As System.Windows.Forms.Button
    Public WithEvents cmdhide As System.Windows.Forms.Button
    Public WithEvents txtmbrv As System.Windows.Forms.TextBox
    Public WithEvents txtmbsend As System.Windows.Forms.TextBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    'Public WithEvents _Winsock1_0 As AxMSWinsockLib.AxWinsock
    'Public WithEvents Winsock1 As AxWinsockArray
    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器来修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Command1 = New System.Windows.Forms.Button
        Me.cmdhide = New System.Windows.Forms.Button
        Me.Frame1 = New System.Windows.Forms.GroupBox
        Me.txtmbrv = New System.Windows.Forms.TextBox
        Me.txtmbsend = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Frame1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Command1
        '
        Me.Command1.BackColor = System.Drawing.SystemColors.Control
        Me.Command1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command1.Location = New System.Drawing.Point(420, 442)
        Me.Command1.Name = "Command1"
        Me.Command1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command1.Size = New System.Drawing.Size(105, 25)
        Me.Command1.TabIndex = 6
        Me.Command1.Text = "清空"
        Me.Command1.UseVisualStyleBackColor = False
        '
        'cmdhide
        '
        Me.cmdhide.BackColor = System.Drawing.SystemColors.Control
        Me.cmdhide.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdhide.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdhide.Location = New System.Drawing.Point(608, 442)
        Me.cmdhide.Name = "cmdhide"
        Me.cmdhide.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdhide.Size = New System.Drawing.Size(106, 25)
        Me.cmdhide.TabIndex = 5
        Me.cmdhide.Text = "关闭"
        Me.cmdhide.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.txtmbrv)
        Me.Frame1.Controls.Add(Me.txtmbsend)
        Me.Frame1.Controls.Add(Me.Label4)
        Me.Frame1.Controls.Add(Me.Label5)
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(3, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(1134, 436)
        Me.Frame1.TabIndex = 0
        Me.Frame1.TabStop = False
        Me.Frame1.Text = " TCP SERVER"
        '
        'txtmbrv
        '
        Me.txtmbrv.AcceptsReturn = True
        Me.txtmbrv.BackColor = System.Drawing.SystemColors.Window
        Me.txtmbrv.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtmbrv.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtmbrv.Location = New System.Drawing.Point(8, 31)
        Me.txtmbrv.MaxLength = 0
        Me.txtmbrv.Multiline = True
        Me.txtmbrv.Name = "txtmbrv"
        Me.txtmbrv.ReadOnly = True
        Me.txtmbrv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtmbrv.Size = New System.Drawing.Size(1112, 56)
        Me.txtmbrv.TabIndex = 2
        '
        'txtmbsend
        '
        Me.txtmbsend.AcceptsReturn = True
        Me.txtmbsend.BackColor = System.Drawing.SystemColors.Window
        Me.txtmbsend.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtmbsend.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtmbsend.Location = New System.Drawing.Point(8, 104)
        Me.txtmbsend.MaxLength = 0
        Me.txtmbsend.Multiline = True
        Me.txtmbsend.Name = "txtmbsend"
        Me.txtmbsend.ReadOnly = True
        Me.txtmbsend.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtmbsend.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtmbsend.Size = New System.Drawing.Size(1112, 329)
        Me.txtmbsend.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(8, 13)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(57, 16)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "接收命令"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(8, 90)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(73, 11)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "发送数据"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(25, 443)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Label1"
        '
        'TCPsocket
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1149, 479)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Command1)
        Me.Controls.Add(Me.cmdhide)
        Me.Controls.Add(Me.Frame1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TCPsocket"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "自定义数据通讯监视"
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
#End Region
End Class