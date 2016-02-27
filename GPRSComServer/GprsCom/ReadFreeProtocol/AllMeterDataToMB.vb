Imports GPRSComServer.Device
Public Class AllMeterDataToMB

    'Private WithEvents Tmr As Mytimer
    Private Pollcounter As Integer
    Private WithEvents Tmr As Timer = New Timer()
    Public FlowMeters As System.Collections.Generic.List(Of ZJTXFlow)
    Private Sub Tmr_TmrTick() Handles Tmr.Tick
        Pollcounter = Pollcounter + 1
        If Pollcounter > 1000 Then
            Pollcounter = 0
        End If
        Dim r As ZJTXFlow
        For Each r In FlowMeters

            If Pollcounter Mod r.PollTime = 0 Then '根据巡测周期这站巡测该不该不测
                r.mPollEnable = True
            End If

        Next r
        For Each r In FlowMeters
            Dim D As TXDataBlock
            For i As Integer = 0 To r.DtBlocks.Count - 1
                D = r.DtBlocks(i)
                Dim Mbaddr As String
                Mbaddr = (400000 + r.MBStartAD + 1 + i * 10).ToString
                Mbs.WritevaluebyAd(ZJTXFlow.MBAD, Mbaddr, Datatype.浮点数, CDbl(D.SSLL))
                Mbaddr = (400000 + r.MBStartAD + 1 + i * 10 + 2).ToString
                Mbs.WritevaluebyAd(ZJTXFlow.MBAD, Mbaddr, Datatype.浮点数, CDbl(D.LJLL))
                Mbaddr = (400000 + r.MBStartAD + 1 + i * 10 + 4).ToString
                Mbs.WritevaluebyAd(ZJTXFlow.MBAD, Mbaddr, Datatype.浮点数, CDbl(D.YL))
                Mbaddr = (400000 + r.MBStartAD + 1 + i * 10 + 6).ToString
                Mbs.WritevaluebyAd(ZJTXFlow.MBAD, Mbaddr, Datatype.浮点数, CDbl(D.WD))
            Next
        Next
    End Sub
    Sub AddStn(ByVal Flow As ZJTXFlow)
        Flow.MBStartAD = FlowMeters.Count * 50
        Flow.mPollEnable = True
        FlowMeters.Add(Flow)


    End Sub
    Sub ReMoveStn(ByVal Flow As ZJTXFlow)
        FlowMeters.Remove(Flow)

    End Sub

    Public Sub New()
        FlowMeters = New System.Collections.Generic.List(Of ZJTXFlow)
        'Me.Tmr = FGetherRtuData.myTmr
        Tmr.Interval = RTUs.StationPolltime * 1000
        Tmr.Enabled = True
        If Mbs.Devices.GetDevicefromAd(201) Is Nothing Then
            Mbs.AddMbserverDevice(201, 10000, "天信流量计数据")
        End If
    End Sub
End Class
