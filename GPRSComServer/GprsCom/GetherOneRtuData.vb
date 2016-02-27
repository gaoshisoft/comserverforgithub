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
		'UPGRADE_WARNING: ��ʱ������ tmrPoll.Interval ��ֵ����Ϊ 0�� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="169ECF4A-1968-402D-B243-16603CC08604"��
		tmrPoll.Interval = RTUs.StationPolltime * 1000
		tmrPoll.Enabled = True
		Pollcounter = 0
		tmrpoll_Tick(tmrpoll, New System.EventArgs()) '��������
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
			'If ReadvalueByAd(255, "400001", ����) <> 0 Then  'ȷ���ǲ��ǵ���Ѳ��,����ǵ���Ѳ�������������õ�Ӱ��
			If r.ifportionPoll = False Then
				If Pollcounter Mod r.polltime = 0 Then '����Ѳ��������վѲ��ò��ò���
					r.PollEnable = True
				End If
				
				
				
			End If
			'   End If
		Next r
	End Sub
	
	
	
	
	Private Sub TmrPortionPoll_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TmrPortionPoll.Tick '����Ѳ��
		Dim r As GPRSRTU
		For	Each r In RTUs
			'UPGRADE_WARNING: δ�ܽ������� ReadvalueByAd(255, 400001, ����) ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
            If Mbs.ReadvalueByAd(255, "400001", Datatype.����) = 0 Then 'ȷ���ǲ��ǵ���Ѳ��,����ǵ���Ѳ�������������õ�Ӱ��
                If r.ifportionPoll = True Then

                    r.PollEnable = True
                End If
            End If
		Next r
	End Sub
	Sub StartMyportionPolltimer(ByVal interval As Integer)
		'UPGRADE_WARNING: ��ʱ������ TmrPortionPoll.Interval ��ֵ����Ϊ 0�� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="169ECF4A-1968-402D-B243-16603CC08604"��
		TmrPortionPoll.Interval = interval
		
		TmrPortionPoll.Enabled = True
	End Sub
	Sub StopMyPortionPolltimer()
		TmrPortionPoll.Enabled = False
	End Sub
End Class