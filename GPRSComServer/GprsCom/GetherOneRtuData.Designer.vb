<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FGetherRtuData
#Region "Windows ������������ɵĴ��� "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'�˵����� Windows ���������������ġ�
		InitializeComponent()
	End Sub
	'Form ��д Dispose������������б�
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Windows ����������������
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents TmrPortionPoll As System.Windows.Forms.Timer
	Public WithEvents tmr100ms As System.Windows.Forms.Timer
	Public WithEvents tmrPoll As System.Windows.Forms.Timer
	'ע��: ���¹����� Windows ����������������
	'����ʹ�� Windows ������������޸�����
	'��Ҫʹ�ô���༭���޸�����
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(FGetherRtuData))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.TmrPortionPoll = New System.Windows.Forms.Timer(components)
		Me.tmr100ms = New System.Windows.Forms.Timer(components)
		Me.tmrPoll = New System.Windows.Forms.Timer(components)
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		Me.BackColor = System.Drawing.SystemColors.Info
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
		Me.Text = "Form2"
		Me.ClientSize = New System.Drawing.Size(355, 70)
		Me.Location = New System.Drawing.Point(0, 7)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ControlBox = True
		Me.Enabled = True
		Me.KeyPreview = False
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.HelpButton = False
		Me.WindowState = System.Windows.Forms.FormWindowState.Normal
		Me.Name = "FGetherRtuData"
		Me.TmrPortionPoll.Enabled = False
		Me.TmrPortionPoll.Interval = 500
		Me.tmr100ms.Interval = 100
		Me.tmr100ms.Enabled = True
		Me.tmrPoll.Interval = 60000
		Me.tmrPoll.Enabled = True
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class