Imports System
Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports Microsoft.VisualBasic
Public Class MyWinSockClient


    'Inherits System.Net.Sockets.TcpClient
    Public TcpClnt As TcpClient
    Dim WithEvents Tmr100ms As New Timer()

    Public Event DataArrival(ByRef Sender As MyWinSockClient, ByVal BytesTotal As Long, ByVal Data() As Byte)
    Dim InBuff(511) As Byte
    Dim Rl As Long '接收到的字节数
    


 
    Public ReadOnly Property Ifconnected() As Boolean
        Get
            Dim s As Socket = TcpClnt.Client
            If s.Connected = True Then
                Ifconnected = True
            Else
                Ifconnected = False

            End If
        End Get
    End Property

    Public Sub SendData(ByVal Data() As Byte)
        If Not TcpClnt Is Nothing Then
            TcpClnt.GetStream.Write(Data, 0, Data.GetLength(0))
        End If
       
    End Sub

    Public Sub New()
        Tmr100ms.Interval = 100
        Tmr100ms.Enabled = True

    End Sub

    Private Sub Tmr100ms_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tmr100ms.Tick
        If Not TcpClnt Is Nothing Then
            If Me.Ifconnected = True Then
                'If TcpClnt.GetStream.DataAvailable Then
                If TcpClnt.Available Then
                    Dim i As Long
                    Rl = TcpClnt.GetStream.Read(InBuff, 0, InBuff.Length)
                    Dim B(Rl - 1) As Byte
                    For i = 0 To Rl - 1
                        B(i) = InBuff(i)

                    Next

                    RaiseEvent DataArrival(Me, Rl, B)

                End If
            End If
        End If
    End Sub
    Sub CloseMe()
        Tmr100ms.Enabled = False
        If Not TcpClnt Is Nothing Then
            TcpClnt.Close()
        End If

    End Sub
   
    Protected Overrides Sub Finalize()
        CloseMe()

        'Tmr100ms = Nothing
        MyBase.Finalize()
    End Sub
End Class 'MyTcpClientDerivedClass

Public Class MyWinSockListener


    Inherits System.Net.Sockets.TcpListener
    'Dim myLsnr As System.Net.Sockets.TcpListener
    Dim WithEvents Tmr100ms As Timer
    Public Event ConnectionRequest(ByVal TcpClnt As System.Net.Sockets.TcpClient)
    Shared Mywinsocklsnr As MyWinSockListener '使用单例模式，避免重建listener
    Dim InBuff(511) As Byte
    Dim Rl As Long '接收到的字节数
    Dim Ipaddr As IPAddress
    Dim Prt As Integer
    Public ReadOnly Property Ifconnected() As Boolean
        Get
            Dim s As Socket = Me.Server
            If s.Connected = True Then
                Ifconnected = True
            Else
                Ifconnected = False

            End If
        End Get
    End Property



    Private Sub New(ByVal Prt As Integer)

        MyBase.New(New IPEndPoint(IPAddress.Any, Prt))

        Me.Ipaddr = Ipaddr
        Me.Prt = Prt
        Tmr100ms = New Timer
        Tmr100ms.Interval = 100
        Tmr100ms.Enabled = True
    End Sub
    Public Shared Function GetMyListener(ByVal Prt As Integer)
        If Mywinsocklsnr Is Nothing Then
            Mywinsocklsnr = New MyWinSockListener(Prt)
            Return Mywinsocklsnr
        Else
            If Mywinsocklsnr.Prt = Prt Then
                Return Mywinsocklsnr
            Else
                Mywinsocklsnr = New MyWinSockListener(Prt)
                Return Mywinsocklsnr
            End If
        End If

    End Function

  
    Private Sub Tmr100ms_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tmr100ms.Tick

        If Me.Pending Then
            Dim client As System.Net.Sockets.TcpClient

            client = Me.AcceptTcpClient

            RaiseEvent ConnectionRequest(client)

        End If

    End Sub
    Public Sub StopMe()
        Me.Stop()

        Tmr100ms.Enabled = False
        'Tmr100ms = Nothing
    End Sub
End Class 'MyTcpClientDerivedClass
