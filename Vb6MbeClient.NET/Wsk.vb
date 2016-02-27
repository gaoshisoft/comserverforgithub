Option Strict Off
Option Explicit On
Imports System.Net.Sockets
<System.Runtime.InteropServices.ProgId("TcpWsk_NET.TcpWsk")> Public Class TcpWsk
    Implements IComm

    Public Event DataArrival(ByRef mValue As Object, ByRef bytesTotal As Integer) Implements IComm.DataArrival
    Dim ipstr As String
    Dim port As Int16
    Dim TcpC As TcpClient
    Dim WithEvents Tmr100ms As New Timer()
    Dim InBuff(511) As Byte
    Dim Rl As Long '接收到的字节数
    Dim callback As AsyncCallback = New AsyncCallback(AddressOf ProcessR)

    Dim Stat As Object
    'Public Index As Integer

    Private merrmsg As String
    Private Sub ProcessR(ByVal ar As IAsyncResult)
        'FGetherRtuData.Label1.Text = "ssssssssssss"
        Dim t As TcpChannelClient
        t = ar.AsyncState

    End Sub







    Public Sub New(ByVal CommInfo As String)
        MyBase.New()
        Me.TcpC = New TcpClient
       
        ipstr = CommInfo.Split(":")(1).Split("-")(0)
        port = CommInfo.Split(":")(1).Split("-")(1)
        Tmr100ms.Interval = 100
        Tmr100ms.Enabled = True

    End Sub

    Private Sub Tmr100ms_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tmr100ms.Tick
        'TcpC.Client.BeginReceive()'可以用异步接收来代替定时器接收方法，用回调（即委托方法）实时性应该更好。
        If Me.TcpC.Connected = False Then
            Exit Sub
        End If

        If Me.TcpC.GetStream.DataAvailable Then
            Dim i As Long
            Rl = Me.TcpC.GetStream.Read(InBuff, 0, InBuff.Length)
            Dim B(Rl - 1) As Byte
            For i = 0 To Rl - 1
                B(i) = InBuff(i)

            Next

            RaiseEvent DataArrival(B, Rl)

        End If
    End Sub

    Public Sub CloseMe() Implements IComm.CloseMe
        
        Me.TcpC.Close()


        Me.TcpC = New TcpClient

    End Sub

    Public Sub ConnectTO(ByVal CommInfo As String) Implements IComm.ConnectTO
        'GPRS:13610983709
        'TCP:192.168.1.2-80
        'SComm:COM1-9600,n,8,1
       
        Try
            Me.TcpC.BeginConnect(ipstr, port, callback, TcpC) '异步连接，防卡死
        Catch ex As Exception
            merrmsg = ex.Message
        End Try

    End Sub



    Public Sub SendData(ByRef data As Object) Implements IComm.SendData

        Try
            If Me.State = False Then Exit Sub

            Me.TcpC.Client.Send(data)

        Catch ex As Exception
        End Try

    End Sub

    Public ReadOnly Property State As Boolean Implements IComm.State
        Get
            State = Me.TcpC.Connected
        End Get
    End Property

    Public Property ErrMsg As String Implements IComm.ErrMsg
        Get
            ErrMsg = merrmsg
        End Get
        Set(ByVal value As String)

        End Set
    End Property
End Class