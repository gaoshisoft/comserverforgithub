Imports System
Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports Microsoft.VisualBasic
Public Class MyWinSockClient


    Public TcpClnt As System.Net.Sockets.Socket
    Dim WithEvents Tmr100ms As New Windows.Forms.Timer()

    Public Event DataArrival(ByVal Sender As MyWinSockClient, ByVal BytesTotal As Long, ByVal Data() As Byte)
    Dim InBuff(511) As Byte
    Dim Rl As Long '接收到的字节数
    Public MyWaitTime As Integer   '等待数据的时间，秒为单位


    Public ReadOnly Property TestIfconnected() As Boolean
        Get


            Dim B(0) As Byte
            ReDim B(0)
            Me.SendData(B)
            If TcpClnt.Connected = True Then
                TestIfconnected = True
            Else
                TestIfconnected = False

            End If
        End Get
    End Property

    Public Sub SendData(ByVal Data() As Byte)
        On Error Resume Next
        TcpClnt.Send(Data)
    End Sub

    Public Sub New()
        Tmr100ms.Interval = 100
        Tmr100ms.Enabled = True

        TcpClnt = New System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)

    End Sub

    Private Sub Tmr100ms_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tmr100ms.Tick
     
        If Me.TcpClnt.Connected = True Then
            If TcpClnt.Available Then
                Dim i As Integer

                Rl = TcpClnt.Receive(InBuff)
                Dim B(Rl - 1) As Byte
                For i = 0 To Rl - 1
                    B(i) = InBuff(i)

                Next

                RaiseEvent DataArrival(Me, Rl, B)

            End If
        End If
    End Sub

    Sub StopMe()
        Tmr100ms.Enabled = False


    End Sub
End Class 'MyTcpClientDerivedClass

Public Class MyWinSockListener


    Inherits System.Net.Sockets.TcpListener
    Dim WithEvents Tmr100ms As New Windows.Forms.Timer()

    'Public Event DataArrival(ByVal BytesTotal As Long, ByVal Data() As Byte)
    Public Event ConnectionRequest(ByVal TcpClnt As System.Net.Sockets.Socket)
    Dim InBuff(511) As Byte
    Dim Rl As Long '接收到的字节数
    'Dim WithEvents Socket As Socket
    'Sub testmy()

    'End Sub
    Public ReadOnly Property Ifconnected() As Boolean
        Get
            Dim s As Socket = MyBase.Server
            If s.Connected = True Then
                Ifconnected = True
            Else
                Ifconnected = False

            End If
        End Get
    End Property

   

    Public Sub New(ByVal Ipaddr As IPAddress, ByVal Prt As Integer)
        MyBase.New(Ipaddr, Prt)
        'MyBase.Server.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.IPOptions, 0)
        Tmr100ms.Interval = 100
        Tmr100ms.Enabled = True
    End Sub
   


    Private Sub Tmr100ms_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tmr100ms.Tick

        If Me.Pending Then
            Dim client As System.Net.Sockets.Socket

            client = Me.AcceptSocket

            RaiseEvent ConnectionRequest(client)

        End If
    End Sub
End Class 'MyTcpClientDerivedClass
