Option Strict Off
Option Explicit On

Imports System.Net.Sockets
Imports System.Windows.Forms
Imports System.Linq.Expressions

Namespace TCPClientDSC

    <System.Runtime.InteropServices.ProgId("TcpWsk_NET.TcpWsk")>
    Public Class TcpWsk
        Implements IComm

        Public Event DataArrival(ByRef mValue As Object, ByRef bytesTotal As Integer) Implements IComm.DataArrival
        ReadOnly _ipstr As String
        ReadOnly _port As Int16
        Dim _tcpC As TcpClient
        Dim WithEvents Tmr100ms As New Timer()
        Dim InBuff(511) As Byte
        Dim Rl As Long '接收到的字节数
        ReadOnly _callback As AsyncCallback = New AsyncCallback(AddressOf ProcessR)

        'Dim Stat As Object
        'Public Index As Integer

        Private _merrmsg As String

        Private Sub ProcessR(ByVal ar As IAsyncResult)
            'FGetherRtuData.Label1.Text = "ssssssssssss"
            'Dim t As Channelclient
            't = ar.AsyncState
        End Sub


        Public Sub New(ByVal commInfo As String)
            commInfo = commInfo.Replace("TCP-", "")
            'MyBase.New()
            Me._tcpC = New TcpClient

            _ipstr = commInfo.Split(":")(0)
            _port = commInfo.Split(":")(1)
            ConnectTo()
            Tmr100ms.Interval = 100
            Tmr100ms.Enabled = True
        End Sub

        Private Sub Tmr100ms_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tmr100ms.Tick
            'TcpC.Client.BeginReceive()'可以用异步接收来代替定时器接收方法，用回调（即委托方法）实时性应该更好。
            Try
                If Me._tcpC.Connected = False Then
                    Me._tcpC.Close()
                    ConnectTo()
                    Exit Sub
                End If

                If Me._tcpC.GetStream.DataAvailable Then
                    Dim i As Long
                    Rl = Me._tcpC.GetStream.Read(InBuff, 0, InBuff.Length)
                    Dim B(Rl - 1) As Byte
                    For i = 0 To Rl - 1
                        B(i) = InBuff(i)

                    Next

                    RaiseEvent DataArrival(B, Rl)

                End If
            Catch
            End Try
        End Sub

        Public Sub CloseMe() Implements IComm.CloseMe

            Me._tcpC.Close()


            Me._tcpC = New TcpClient
        End Sub

        Public Property Comminfo() As String Implements IComm.Comminfo

        Public Sub ConnectTo() Implements IComm.ConnectTo
            '13610983709
            'TCP:192.168.1.2-80
            'SComm:COM1-9600,n,8,1

            Try
                Me._tcpC.BeginConnect(_ipstr, _port, _callback, _tcpC) '异步连接，防卡死
            Catch ex As Exception
                _merrmsg = ex.Message
            End Try
        End Sub


        Public Sub SendData(ByVal data As Object) Implements IComm.SendData

            Try
                If Me.State = False Then Exit Sub

                Me._tcpC.Client.Send(data)

            Catch ex As Exception
            End Try
        End Sub

        Public ReadOnly Property State As Boolean Implements IComm.State
            Get
                Try
                    State = Me._tcpC.Connected
                Catch E As Exception
                    State = False
                End Try
            End Get
        End Property

        Public Property ErrMsg As String Implements IComm.ErrMsg
            Get
                ErrMsg = _merrmsg
            End Get
            Set(ByVal value As String)
            End Set
        End Property
    End Class
End Namespace