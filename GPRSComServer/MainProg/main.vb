Option Strict Off
Option Explicit On
Friend Class Fview
	Inherits System.Windows.Forms.Form
	
	
	'UPGRADE_WARNING: 初始化窗体时可能激发事件 chkGprscomdisplay.CheckStateChanged。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"”
	Private Sub chkGprscomdisplay_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkGprscomdisplay.CheckStateChanged
		If chkGprscomdisplay.CheckState = 1 Then
		Else
			Text1.Text = ""
			Text2.Text = ""
		End If
	End Sub
	
	'UPGRADE_WARNING: 初始化窗体时可能激发事件 chkmbComdisplay.CheckStateChanged。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"”
	Private Sub chkmbComdisplay_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkmbComdisplay.CheckStateChanged
		txtmbrv.Text = ""
		txtmbsend.Text = ""
		
	End Sub
	
	
	Private Sub chkMbeEnable_Click()
		
	End Sub
	
	'UPGRADE_WARNING: 初始化窗体时可能激发事件 chkPollenable.CheckStateChanged。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"”
	Public Sub chkPollenable_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPollenable.CheckStateChanged
		If chkPollenable.CheckState = 1 Then
			FGetherRtuData.StartPoll()
			Text1.Enabled = True
			Text2.Enabled = True
			
		Else
			FGetherRtuData.StopPoll()
			Text1.Enabled = False
			Text2.Enabled = False
		End If
		MDIfmain.m_datapoll.Checked = chkPollenable.CheckState
	End Sub
	
	Private Sub Command1_Click()
		' Me.Hide
		'Form1.Show
	End Sub
	
	Private Sub cmdhide_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdhide.Click
		Me.Hide()
	End Sub
	
	Private Sub Command2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command2.Click
		If usr.Level = 3 Then
			Fconfig.Show()
		Else
			MsgBox("您的权限不够!")
		End If
		
	End Sub
	
	Private Sub Command3_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command3.Click
		frmByteqty.Show()
	End Sub
	
	Private Sub Command4_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command4.Click
		comquality.Show()
	End Sub
	
	Private Sub Fview_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		
		chkPollenable.CheckState = System.Windows.Forms.CheckState.Checked
		chkGprscomdisplay.CheckState = System.Windows.Forms.CheckState.Checked
		chkmbComdisplay.CheckState = System.Windows.Forms.CheckState.Checked
        Frame1.Text = "Modbus TCP server:" & Mbs.GetServerIP
	End Sub
End Class