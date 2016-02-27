Option Strict Off
Option Explicit On
Friend Class frmHeartBeat
    Inherits System.Windows.Forms.Form


    Public PollTmr As New Timer




    'UPGRADE_WARNING: 初始化窗体时可能激发事件 chkPollenable.CheckStateChanged。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"”
    Private Sub chkPollenable_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPollenable.CheckStateChanged
        tmrPoll.Enabled = IIf(chkPollenable.CheckState = 1, True, False)

    End Sub

    Private Sub Command1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command1.Click
        Me.Hide()
    End Sub

    Private Sub FGetherRtuData_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        StartPoll()
    End Sub









    Sub StopPoll()
        tmrPoll.Enabled = False
    End Sub

    Sub StartPoll()
        tmrPoll.Interval = 1000
        tmrPoll.Enabled = True


    End Sub



  



    Private Sub tmrPoll_Tick_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrPoll.Tick
        'PollTmr.Tell()
    End Sub
End Class