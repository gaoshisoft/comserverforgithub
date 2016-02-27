Option Strict Off
Option Explicit On

Imports GPRSComServer.GprsCom
Imports MBsrv.Device
'Imports MBsrv.Device

'Imports GPRSComServer.Device

Friend Class GetherData

    Public WithEvents TmrPortionPoll As New Timer

   


   

    Private Sub tmrPoll_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrPoll.Disposed
    End Sub

   


    'Private Sub TmrPortionPoll_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
    '    Handles TmrPortionPoll.Tick '部分巡测
    '    Dim r As GPRSRTU
    '    For Each r In RTUs
    '        'UPGRADE_WARNING: 未能解析对象 ReadvalueByAd(255, 400001, 整型) 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
    '        If Mbs.ReadvalueByAd(255, "400001", Datatype.无符号整型) = 0 Then '确定是不是单点巡检,如果是单点巡检则不受周期设置的影响
    '            If r.ifportionPoll = True Then

    '                r.PollEnable = True
    '            End If
    '        End If
    '    Next r
    'End Sub

 


    Public Sub New()
     


        StartPoll()
    End Sub

    Protected Overrides Sub Finalize()
       
        tmrPoll.Enabled = False
        TmrPortionPoll.Enabled = False
        TmrPortionPoll = Nothing
        tmrPoll = Nothing

        MyBase.Finalize()
    End Sub
End Class