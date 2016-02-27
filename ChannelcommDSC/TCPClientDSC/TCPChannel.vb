Option Strict Off
Option Explicit On

Imports System.Windows.Forms

Namespace TCPClientDSC

    <System.Runtime.InteropServices.ProgId("TCPChannel_NET.TCPChannel")>
    Public Class TcpChannel
        Implements IChannel

        Public ChannelId As Integer


        Private _mvarChannelName As String '局部复制

        'Public Comstate As Boolean '表这个RTU的数据收发状态，真表示已发送但还未收到数据也没有超时也就是正在等待数据的返回，假表示没有发送或已超时

        'Dim Sendtime As Double
        Private _mComminfo As String

        Public WithEvents Mtimer As Timer
        Private WithEvents myWsk As TcpWsk
        'Public Keystr As String

        Private _mvarChannelState As Boolean '局部复制
        'Private mvarEnable As Boolean
        Dim _mTimeout As Double

        Private Property Mcomstate As Boolean


        Public Event DataArrival(ByVal theCommInfo As String, ByVal mValue As Object, ByVal bytesTotal As Integer) _
            Implements IChannel.DataArrival


        Public ReadOnly Property Wsk() As TcpWsk
            Get


                Wsk = myWsk
            End Get
        End Property

        Public Sub New(ByVal comminfo As String)
            myWsk = New TcpWsk(comminfo)
            Me.Comminfo = comminfo

        End Sub


        Sub SendByteData(ByVal data() As Byte) Implements IChannel.SendByteData
            Wsk.SendData(data)
        End Sub

        Private Sub mWsk_DataArriva(ByRef mValue As Object, ByRef bytesTotal As Integer) Handles myWsk.DataArrival

            RaiseEvent DataArrival(Me.Comminfo, mValue, bytesTotal)
        End Sub

        Private Sub Mytimer_Renamed() Handles Mtimer.Tick

            Try
                If Wsk.State = False Then
                    Me.Wsk.CloseMe()

                    Wsk.ConnectTO() '如果状态不正常重新进行连接
                    Exit Sub
                End If

            Catch ex As Exception
            End Try
        End Sub

        Sub Init(ByVal thecomminfo As String, ByVal theChannelName As String) Implements IChannel.Init
            Me._mComminfo = thecomminfo


            Me.ChannelName = theChannelName

            Mtimer = New Timer
            Mtimer.Interval = 500
            'Mtimer.Enabled = True

            Me.Mtimer.Enabled = True
        End Sub

        Public Property KeyStr() As String Implements IChannel.KeyStr

        Public Property ChannelName As String Implements IChannel.ChannelName
            Get
                ChannelName = _mvarChannelName
            End Get
            Set(ByVal value As String)
                _mvarChannelName = value
            End Set
        End Property

        Public Property ChannelState As Boolean Implements IChannel.ChannelState
            Get
                _mvarChannelState = Me.Wsk.State

                ChannelState = _mvarChannelState
            End Get
            Set(ByVal value As Boolean)
                _mvarChannelState = value
            End Set
        End Property

        Public Property Comminfo As String Implements IChannel.Comminfo
            Get
                Comminfo = _mComminfo
            End Get
            Set(ByVal value As String)
                _mComminfo = value
            End Set
        End Property

        Public Property Comstate As Boolean Implements IChannel.Comstate
            Get
                Comstate = Mcomstate
            End Get
            Set(ByVal value As Boolean)
                Mcomstate = value
            End Set
        End Property


        Public Property Timeout As Double Implements IChannel.Timeout
            Get
                Timeout = _mTimeout
            End Get
            Set(ByVal value As Double)
                _mTimeout = value
            End Set
        End Property
    End Class
End Namespace