Imports System
Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports Microsoft.VisualBasic
Public Class MyWinSockClient


    
    Public UdpClnt As UdpClient
    Public UdpRemoteEnd As EndPoint
    Dim WithEvents Tmr100ms As New Timer()

    Public Event DataArrival(ByRef Sender As MyWinSockClient, ByVal BytesTotal As Long, ByVal Data() As Byte)
    Dim InBuff(511) As Byte
    Dim Rl As Long '接收到的字节数
    


 
   
    Public Sub SendData(ByVal Data() As Byte)
       
        If Not UdpClnt Is Nothing Then


            UdpClnt.Send(Data, Data.Length, Me.UdpRemoteEnd)


        End If
    End Sub

    Public Sub New()
        Tmr100ms.Interval = 100
        Tmr100ms.Enabled = True

    End Sub

   
    Sub StopMe()
        Tmr100ms.Enabled = False
    End Sub
   
End Class 'MyTcpClientDerivedClass

Public Class MyWinSockListener


    'Inherits System.Net.Sockets.TcpListener
    Dim WithEvents Tmr100ms As New Timer()
    Public Event ConnectionRequest(ByVal TcpClnt As System.Net.Sockets.TcpClient)
    Public Event DataArrival(ByRef Sender As MyWinSockClient, ByVal BytesTotal As Long, ByVal Data() As Byte)

    Dim InBuff(511) As Byte
    Dim Rl As Long '接收到的字节数
    Public UdpSrv As System.Net.Sockets.Socket
    Dim Ipaddr As IPAddress
    Dim Prt As Integer
    



    Public Sub New(ByVal Ipaddr As IPAddress, ByVal Prt As Integer)


        Me.Ipaddr = Ipaddr
        Me.Prt = Prt

        InitUDP(Ipaddr, Prt)
        Tmr100ms.Interval = 100
        Tmr100ms.Enabled = True
    End Sub
    Public Sub Close()
        Tmr100ms.Stop()
        UdpSrv.Close()
    End Sub
    Private Sub InitUDP(ByVal Ipaddr As IPAddress, ByVal Prt As Integer)
        UdpSrv = New Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)

        Dim IPendPoint As IPEndPoint
        IPendPoint = New IPEndPoint(IPAddress.Any, Prt)
        UdpSrv.Bind(IPendPoint)

    End Sub

    Private Sub Tmr100ms_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tmr100ms.Tick
       
        If UdpSrv.Available Then
            Dim i As Integer
            Try
                Dim sender1 As New IPEndPoint(Me.Ipaddr, Me.Prt)
                Dim senderRemote As EndPoint = CType(sender1, EndPoint)
                Rl = UdpSrv.ReceiveFrom(InBuff, senderRemote)
            

                Dim B(Rl - 1) As Byte
                For i = 0 To Rl - 1
                    B(i) = InBuff(i)

                Next
                Dim Clnt As MyWinSockClient = New MyWinSockClient
                Dim Udp As New UdpClient
                Udp.Client = UdpSrv


                Clnt.UdpRemoteEnd = senderRemote
                Clnt.UdpClnt = Udp
                RaiseEvent DataArrival(Clnt, Rl, B)
            Catch ex As Exception

            End Try
        End If

    End Sub
End Class 'MyTcpClientDerivedClass
