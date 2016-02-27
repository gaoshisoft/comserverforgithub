Option Strict Off
Option Explicit On
Imports GPRSComServer.Device
Friend Class FGetherRtuData
	Inherits System.Windows.Forms.Form
	Dim Pollcounter As Integer
	Public myTmr As New Mytimer
	
	
	
	
	Private Sub FGetherRtuData_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		StartPoll()
	End Sub
	
	
	
	
	
	
	
	
	
	Sub StopPoll()
		tmrPoll.Enabled = False
	End Sub
	
	Sub StartPoll()
		'UPGRADE_WARNING: 计时器属性 tmrPoll.Interval 的值不能为 0。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="169ECF4A-1968-402D-B243-16603CC08604"”
		tmrPoll.Interval = RTUs.StationPolltime * 1000
		tmrPoll.Enabled = True
		Pollcounter = 0
		tmrpoll_Tick(tmrpoll, New System.EventArgs()) '立即启动
	End Sub
	
	
	
	Private Sub tmr100ms_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles tmr100ms.Tick
		myTmr.Tell()
		
    End Sub

    Private Sub tmrPoll_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrPoll.Disposed

    End Sub
	
	Private Sub tmrpoll_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles tmrpoll.Tick
		Pollcounter = Pollcounter + 1
		If Pollcounter > 1000 Then
			Pollcounter = 0
		End If
		Dim r As GPRSRTU
		For	Each r In RTUs
			'If ReadvalueByAd(255, "400001", 整型) <> 0 Then  '确定是不是单点巡检,如果是单点巡检则不受周期设置的影响
			If r.ifportionPoll = False Then
				If Pollcounter Mod r.polltime = 0 Then '根据巡测周期这站巡测该不该不测
					r.PollEnable = True
				End If
				
				
				
			End If
			'   End If
		Next r
	End Sub
	
	
	
	
	Private Sub TmrPortionPoll_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TmrPortionPoll.Tick '部分巡测
		Dim r As GPRSRTU
		For	Each r In RTUs
			'UPGRADE_WARNING: 未能解析对象 ReadvalueByAd(255, 400001, 整型) 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
            If Mbs.ReadvalueByAd(255, "400001", Datatype.整型) = 0 Then '确定是不是单点巡检,如果是单点巡检则不受周期设置的影响
                If r.ifportionPoll = True Then

                    r.PollEnable = True
                End If
            End If
		Next r
	End Sub
	Sub StartMyportionPolltimer(ByVal interval As Integer)
		'UPGRADE_WARNING: 计时器属性 TmrPortionPoll.Interval 的值不能为 0。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="169ECF4A-1968-402D-B243-16603CC08604"”
		TmrPortionPoll.Interval = interval
		
		TmrPortionPoll.Enabled = True
	End Sub
	Sub StopMyPortionPolltimer()
		TmrPortionPoll.Enabled = False
	End Sub
End Class