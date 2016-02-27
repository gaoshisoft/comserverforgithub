<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class comquality
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
        'Me.MDIParent = GPRSComServer.MDIfmain
        GPRSComServer.MainProg.MdIfmain.Show()
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
	Public WithEvents Command2 As System.Windows.Forms.Button
	Public WithEvents Command1 As System.Windows.Forms.Button
	Public WithEvents _ListView1_ColumnHeader_1 As System.Windows.Forms.ColumnHeader
	Public WithEvents _ListView1_ColumnHeader_2 As System.Windows.Forms.ColumnHeader
	Public WithEvents _ListView1_ColumnHeader_3 As System.Windows.Forms.ColumnHeader
	Public WithEvents _ListView1_ColumnHeader_4 As System.Windows.Forms.ColumnHeader
	Public WithEvents ListView1 As System.Windows.Forms.ListView
	'注意: 以下过程是 Windows 窗体设计器所必需的
	'可以使用 Windows 窗体设计器来修改它。
	'不要使用代码编辑器修改它。
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(comquality))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Command2 = New System.Windows.Forms.Button
        Me.Command1 = New System.Windows.Forms.Button
        Me.ListView1 = New System.Windows.Forms.ListView
        Me._ListView1_ColumnHeader_1 = New System.Windows.Forms.ColumnHeader
        Me._ListView1_ColumnHeader_2 = New System.Windows.Forms.ColumnHeader
        Me._ListView1_ColumnHeader_3 = New System.Windows.Forms.ColumnHeader
        Me._ListView1_ColumnHeader_4 = New System.Windows.Forms.ColumnHeader
        Me.SuspendLayout()
        '
        'Command2
        '
        Me.Command2.BackColor = System.Drawing.SystemColors.Control
        Me.Command2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command2.Location = New System.Drawing.Point(114, 296)
        Me.Command2.Name = "Command2"
        Me.Command2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command2.Size = New System.Drawing.Size(73, 25)
        Me.Command2.TabIndex = 2
        Me.Command2.Text = "刷新"
        Me.Command2.UseVisualStyleBackColor = False
        '
        'Command1
        '
        Me.Command1.BackColor = System.Drawing.SystemColors.Control
        Me.Command1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command1.Location = New System.Drawing.Point(290, 296)
        Me.Command1.Name = "Command1"
        Me.Command1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command1.Size = New System.Drawing.Size(73, 25)
        Me.Command1.TabIndex = 0
        Me.Command1.Text = "确定"
        Me.Command1.UseVisualStyleBackColor = False
        '
        'ListView1
        '
        Me.ListView1.Alignment = System.Windows.Forms.ListViewAlignment.Left
        Me.ListView1.BackColor = System.Drawing.SystemColors.Window
        Me.ListView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me._ListView1_ColumnHeader_1, Me._ListView1_ColumnHeader_2, Me._ListView1_ColumnHeader_3, Me._ListView1_ColumnHeader_4})
        Me.ListView1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ListView1.GridLines = True
        Me.ListView1.HoverSelection = True
        Me.ListView1.Location = New System.Drawing.Point(0, 2)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(514, 284)
        Me.ListView1.TabIndex = 1
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        '_ListView1_ColumnHeader_1
        '
        Me._ListView1_ColumnHeader_1.Text = "RTU名称"
        Me._ListView1_ColumnHeader_1.Width = 236
        '
        '_ListView1_ColumnHeader_2
        '
        Me._ListView1_ColumnHeader_2.Text = "通讯成功次数"
        Me._ListView1_ColumnHeader_2.Width = 236
        '
        '_ListView1_ColumnHeader_3
        '
        Me._ListView1_ColumnHeader_3.Text = "通讯失败次数"
        Me._ListView1_ColumnHeader_3.Width = 236
        '
        '_ListView1_ColumnHeader_4
        '
        Me._ListView1_ColumnHeader_4.Width = 236
        '
        'comquality
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(516, 330)
        Me.Controls.Add(Me.Command2)
        Me.Controls.Add(Me.Command1)
        Me.Controls.Add(Me.ListView1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.ForeColor = System.Drawing.Color.Black
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "comquality"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "各站通讯质量"
        Me.ResumeLayout(False)

    End Sub
#End Region 

    Private Sub comquality_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        rtus = rtus
    End Sub
End Class