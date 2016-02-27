<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class opcItemview
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
	Public WithEvents Command5 As System.Windows.Forms.Button
	Public WithEvents txtdisplaychoose As System.Windows.Forms.TextBox
	Public WithEvents Command2 As System.Windows.Forms.Button
	Public WithEvents Command1 As System.Windows.Forms.Button
	Public WithEvents List1 As System.Windows.Forms.ListBox
	Public WithEvents Label1 As System.Windows.Forms.Label
	'注意: 以下过程是 Windows 窗体设计器所必需的
	'可以使用 Windows 窗体设计器来修改它。
	'不要使用代码编辑器修改它。
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(opcItemview))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.Command5 = New System.Windows.Forms.Button
		Me.txtdisplaychoose = New System.Windows.Forms.TextBox
		Me.Command2 = New System.Windows.Forms.Button
		Me.Command1 = New System.Windows.Forms.Button
		Me.List1 = New System.Windows.Forms.ListBox
		Me.Label1 = New System.Windows.Forms.Label
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Text = "OPC server Item存库选择"
		Me.ClientSize = New System.Drawing.Size(300, 538)
		Me.Location = New System.Drawing.Point(3, 19)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.ControlBox = True
		Me.Enabled = True
		Me.KeyPreview = False
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.HelpButton = False
		Me.WindowState = System.Windows.Forms.FormWindowState.Normal
		Me.Name = "opcItemview"
		Me.Command5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.Command5.Text = "应用显示条件"
		Me.Command5.Size = New System.Drawing.Size(89, 25)
		Me.Command5.Location = New System.Drawing.Point(128, 464)
		Me.Command5.TabIndex = 5
		Me.Command5.BackColor = System.Drawing.SystemColors.Control
		Me.Command5.CausesValidation = True
		Me.Command5.Enabled = True
		Me.Command5.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Command5.Cursor = System.Windows.Forms.Cursors.Default
		Me.Command5.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Command5.TabStop = True
		Me.Command5.Name = "Command5"
		Me.txtdisplaychoose.AutoSize = False
		Me.txtdisplaychoose.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		Me.txtdisplaychoose.Size = New System.Drawing.Size(81, 25)
		Me.txtdisplaychoose.Location = New System.Drawing.Point(40, 464)
		Me.txtdisplaychoose.TabIndex = 4
		Me.txtdisplaychoose.Text = "*"
		Me.txtdisplaychoose.AcceptsReturn = True
		Me.txtdisplaychoose.BackColor = System.Drawing.SystemColors.Window
		Me.txtdisplaychoose.CausesValidation = True
		Me.txtdisplaychoose.Enabled = True
		Me.txtdisplaychoose.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtdisplaychoose.HideSelection = True
		Me.txtdisplaychoose.ReadOnly = False
		Me.txtdisplaychoose.Maxlength = 0
		Me.txtdisplaychoose.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtdisplaychoose.MultiLine = False
		Me.txtdisplaychoose.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtdisplaychoose.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtdisplaychoose.TabStop = True
		Me.txtdisplaychoose.Visible = True
		Me.txtdisplaychoose.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.txtdisplaychoose.Name = "txtdisplaychoose"
		Me.Command2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.Command2.Text = "取消"
		Me.Command2.Size = New System.Drawing.Size(65, 25)
		Me.Command2.Location = New System.Drawing.Point(160, 504)
		Me.Command2.TabIndex = 2
		Me.Command2.BackColor = System.Drawing.SystemColors.Control
		Me.Command2.CausesValidation = True
		Me.Command2.Enabled = True
		Me.Command2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Command2.Cursor = System.Windows.Forms.Cursors.Default
		Me.Command2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Command2.TabStop = True
		Me.Command2.Name = "Command2"
		Me.Command1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.Command1.Text = "选择"
		Me.Command1.Size = New System.Drawing.Size(73, 25)
		Me.Command1.Location = New System.Drawing.Point(56, 504)
		Me.Command1.TabIndex = 1
		Me.Command1.BackColor = System.Drawing.SystemColors.Control
		Me.Command1.CausesValidation = True
		Me.Command1.Enabled = True
		Me.Command1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Command1.Cursor = System.Windows.Forms.Cursors.Default
		Me.Command1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Command1.TabStop = True
		Me.Command1.Name = "Command1"
		Me.List1.Size = New System.Drawing.Size(297, 427)
		Me.List1.Location = New System.Drawing.Point(0, 0)
		Me.List1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
		Me.List1.TabIndex = 0
		Me.List1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.List1.BackColor = System.Drawing.SystemColors.Window
		Me.List1.CausesValidation = True
		Me.List1.Enabled = True
		Me.List1.ForeColor = System.Drawing.SystemColors.WindowText
		Me.List1.IntegralHeight = True
		Me.List1.Cursor = System.Windows.Forms.Cursors.Default
		Me.List1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.List1.Sorted = False
		Me.List1.TabStop = True
		Me.List1.Visible = True
		Me.List1.MultiColumn = False
		Me.List1.Name = "List1"
		Me.Label1.Text = "注：双击即可选择，按住shift或ctrl键可多选！"
		Me.Label1.Size = New System.Drawing.Size(281, 25)
		Me.Label1.Location = New System.Drawing.Point(8, 432)
		Me.Label1.TabIndex = 3
		Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label1.BackColor = System.Drawing.SystemColors.Control
		Me.Label1.Enabled = True
		Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label1.UseMnemonic = True
		Me.Label1.Visible = True
		Me.Label1.AutoSize = False
		Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label1.Name = "Label1"
		Me.Controls.Add(Command5)
		Me.Controls.Add(txtdisplaychoose)
		Me.Controls.Add(Command2)
		Me.Controls.Add(Command1)
		Me.Controls.Add(List1)
		Me.Controls.Add(Label1)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class