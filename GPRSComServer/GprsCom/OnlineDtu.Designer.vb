<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmOnlineDTU
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
        Me.MdiParent = GPRSComServer.MainProg.MDIfmain
        GPRSComServer.MainProg.MDIfmain.Show()
        'Me.InitGprsRTUs()
      
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
    Public WithEvents Label1 As System.Windows.Forms.Label
	'注意: 以下过程是 Windows 窗体设计器所必需的
	'可以使用 Windows 窗体设计器来修改它。
	'不要使用代码编辑器修改它。
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOnlineDTU))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Command1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Itmmenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.复制上线信息ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.单点监控此RTUToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.监视所有RTU通信ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ListView1 = New GPRSComServer.DoubleBufferListView(Me.components)
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Itmmenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'Command1
        '
        Me.Command1.BackColor = System.Drawing.SystemColors.Control
        Me.Command1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command1.Location = New System.Drawing.Point(300, 717)
        Me.Command1.Name = "Command1"
        Me.Command1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command1.Size = New System.Drawing.Size(79, 21)
        Me.Command1.TabIndex = 0
        Me.Command1.Text = "关闭"
        Me.Command1.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(8, 576)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(393, 33)
        Me.Label1.TabIndex = 2
        '
        'Itmmenu
        '
        Me.Itmmenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.复制上线信息ToolStripMenuItem, Me.单点监控此RTUToolStripMenuItem, Me.监视所有RTU通信ToolStripMenuItem})
        Me.Itmmenu.Name = "ContextMenuStrip1"
        Me.Itmmenu.Size = New System.Drawing.Size(173, 70)
        '
        '复制上线信息ToolStripMenuItem
        '
        Me.复制上线信息ToolStripMenuItem.Name = "复制上线信息ToolStripMenuItem"
        Me.复制上线信息ToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.复制上线信息ToolStripMenuItem.Text = "复制上线信息"
        '
        '单点监控此RTUToolStripMenuItem
        '
        Me.单点监控此RTUToolStripMenuItem.Name = "单点监控此RTUToolStripMenuItem"
        Me.单点监控此RTUToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.单点监控此RTUToolStripMenuItem.Text = "单点监控此RTU"
        '
        '监视所有RTU通信ToolStripMenuItem
        '
        Me.监视所有RTU通信ToolStripMenuItem.Name = "监视所有RTU通信ToolStripMenuItem"
        Me.监视所有RTU通信ToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.监视所有RTU通信ToolStripMenuItem.Text = "监视所有RTU通信"
        '
        'ListView1
        '
        Me.ListView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5})
        Me.ListView1.ContextMenuStrip = Me.Itmmenu
        Me.ListView1.Cursor = System.Windows.Forms.Cursors.Default
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.Location = New System.Drawing.Point(0, 0)
        Me.ListView1.Margin = New System.Windows.Forms.Padding(1)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(824, 750)
        Me.ListView1.TabIndex = 3
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "站名"
        Me.ColumnHeader1.Width = 100
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "登录时间"
        Me.ColumnHeader2.Width = 150
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "通讯方式"
        Me.ColumnHeader3.Width = 220
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "巡测周期"
        Me.ColumnHeader4.Width = 80
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "状态"
        '
        'frmOnlineDTU
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(824, 750)
        Me.ControlBox = False
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.Command1)
        Me.Controls.Add(Me.Label1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.ForeColor = System.Drawing.SystemColors.Control
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOnlineDTU"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Itmmenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ListView1 As GPRSComServer.DoubleBufferListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Itmmenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents 复制上线信息ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 单点监控此RTUToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 监视所有RTU通信ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
#End Region 
End Class