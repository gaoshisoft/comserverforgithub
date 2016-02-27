Option Strict Off
Option Explicit On

Imports System.Net.Sockets

<System.Runtime.InteropServices.ProgId("TcpWsk_NET.TcpWsk")>
Public Class TcpWsk
    Public Event DataArrival(ByRef mValue As Object, ByRef bytesTotal As Integer)
    Dim _tcpC As TcpClient
    Dim WithEvents Tmr100ms As New Timer()
    ReadOnly _inBuff(511) As Byte
    Dim _rl As Long '接收到的字节数
    ReadOnly _callback As AsyncCallback = New AsyncCallback(AddressOf ProcessR)


    Public Index As Integer

    Sub ProcessR(ByVal ar As IAsyncResult)
        'Dim t As TcpClient
        't = ar.AsyncState
    End Sub

    Sub SendData(ByRef data As Object)
        If Me.State = False Then Exit Sub

        Me._tcpC.Client.Send(data)
    End Sub

    ReadOnly Property State() As Boolean
        Get

            State = Me._tcpC.Connected
        End Get
    End Property

    Sub CloseMe()

        Me._tcpC.Close()
        Me._tcpC = New TcpClient
    End Sub

    Sub ConnectTO(ByVal IPstr As String, ByVal port As Integer)

        Me._tcpC.BeginConnect(IPstr, port, _callback, _tcpC) '异步连接，防卡死
        'Me.TcpC.Connect(IPstr, port)
    End Sub


    Public Sub New()
        MyBase.New()
        Me._tcpC = New TcpClient
        Tmr100ms.Interval = 100
        Tmr100ms.Enabled = True
    End Sub

    Private Sub Tmr100ms_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tmr100ms.Tick
        If Me._tcpC.Connected = False Then
            Exit Sub
        End If
        If Me._tcpC.GetStream.DataAvailable Then
            Dim i As Long
            _rl = Me._tcpC.GetStream.Read(_inBuff, 0, _inBuff.Length)
            Dim B(_rl - 1) As Byte
            For i = 0 To _rl - 1
                B(i) = _inBuff(i)

            Next

            RaiseEvent DataArrival(B, _rl)

        End If
    End Sub

    Protected Overrides Sub Finalize()
        Tmr100ms.Enabled = False
        Tmr100ms = Nothing
        _tcpC.Close()
        MyBase.Finalize()
    End Sub
End Class