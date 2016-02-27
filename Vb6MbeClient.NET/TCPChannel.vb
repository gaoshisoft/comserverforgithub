Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
<System.Runtime.InteropServices.ProgId("TCPChannel_NET.TCPChannel")> Public Class TCPChannel


    Public ChannelID As Integer



    Private mvarChannelName As String '局部复制

    Public Comstate As Boolean '表这个RTU的数据收发状态，真表示已发送但还未收到数据也没有超时也就是正在等待数据的返回，假表示没有发送或已超时
    Public Timeout As Double
    Dim Sendtime As Double
    Private mComminfo As String

    Public WithEvents Mtimer As Timer
    Private WithEvents mWsk As TcpWsk
    Public Keystr As String

    Private mvarChannelState As Boolean '局部复制
    Private mvarEnable As Boolean

    Public Event DataArrival(ByVal CommInfo As String, ByVal mValue As Object, ByVal bytesTotal As Integer)


    Public Property ChannelState() As Boolean
        Get


            mvarChannelState = Me.Wsk.State

            ChannelState = mvarChannelState
        End Get
        Set(ByVal Value As Boolean)

            mvarChannelState = Value
        End Set
    End Property

    Public Property ChannelName() As String
        Get

            ChannelName = mvarChannelName
        End Get
        Set(ByVal Value As String)

            mvarChannelName = Value
        End Set
    End Property



    'Public Property Enable() As Boolean
    '    Get

    '        Enable = mvarEnable
    '    End Get
    '    Set(ByVal Value As Boolean)

    '        mvarEnable = Value
    '    End Set
    'End Property

    Public ReadOnly Property Wsk() As TcpWsk
        Get
            If mWsk Is Nothing Then

                mWsk = New TcpWsk
            End If
            Wsk = mWsk

        End Get
    End Property

    Public Sub New()
        'MyBase.New()
        'Dim mvarIfonline As Boolean

        'mvarIfonline = False

        
    End Sub

    Protected Overrides Sub Finalize()

        MyBase.Finalize()
    End Sub

    Private Property Comminfo() As String
        Get
            Comminfo = mComminfo
        End Get
        Set(ByVal value As String)
            mComminfo = value
        End Set
    End Property

    Sub SendByteData(ByVal data() As Byte)
        Wsk.SendData(data)
    End Sub
    Private Sub mWsk_DataArrival(ByRef mValue As Object, ByRef bytesTotal As Integer) Handles mWsk.DataArrival

        RaiseEvent DataArrival(Me.Comminfo, mValue, bytesTotal)

    End Sub

    Private Sub Mytimer_Renamed() Handles Mtimer.Tick

        Try
            If Wsk.State = False Then
                Me.Wsk.CloseMe()

                Wsk.ConnectTO(Me.Comminfo) '如果状态不正常重新进行连接
                Exit Sub
            End If

        Catch ex As Exception
        End Try
    End Sub

    Sub init(ByVal comminfo As String, ByVal ChannelName As String)
        Me.mComminfo = comminfo



        Me.ChannelName = ChannelName

        Mtimer = New Timer
        Mtimer.Interval = 500
        'Mtimer.Enabled = True

        Me.Mtimer.Enabled = True

    End Sub

End Class