Option Strict Off
Option Explicit On

'Imports ChannelcommDSC.SCommDSC
Imports ChannelcommDSC.TCPClientDSC

<System.Runtime.InteropServices.ProgId("TcpChannelClient_NET.TcpChannelClient")>
Public Class Channelclient
    Public Channels As Channels




    Public Event Dataarrival(ByVal comminfo As String, ByVal data() As Byte)

    Property Timeout() As Integer
        Get
            Timeout = Channels.Timeout
        End Get
        Set(ByVal value As Integer)
            Channels.Timeout = value
        End Set
    End Property

    Public Function Addnewchannel(ByVal channelName As String, ByVal comminfo As String, ByVal polltime As Integer,
                                    ByVal enable As Boolean) As IChannel

        Dim skey As String
        'Comminfo 内容
        'GPRS-13610983709 这是gprs通信格式
        'TCP-192.168.1.2:80 或 TCP-192.168.1.2:80(192.168.1.3:90)  后者是冗余通信的 通信标识 格式
        '与Icomm不同之处是 Ichannel 可以支持冗余，ichannel真正体现“通道”的 思想
        'SComm-COM1:9600,8,n,1

        '正常comminfo=TCP-192.168.1.2:502 格式
        '冗余comminfo=TCP-192.168.1.2:502(192.168.1.3:502) 格式
        If comminfo.Contains("TCP") Then

            skey = comminfo

            If Channels.CheckKey(skey) = False Then

                Addnewchannel = Channels.Add("TCP", comminfo, skey) 'skey 做关键字

                AddHandler Addnewchannel.DataArrival, AddressOf Me.M_DataArrival
            Else
                AddHandler Channels(skey).DataArrival, AddressOf Me.M_DataArrival
            End If




        ElseIf comminfo.Contains("SComm") Then

            skey = comminfo
            If Channels.CheckKey(skey) = False Then

                Addnewchannel = Channels.Add("SComm", comminfo, skey) 'skey 做关键字

                AddHandler Addnewchannel.DataArrival, AddressOf Me.M_DataArrival
            Else
                AddHandler Channels(skey).DataArrival, AddressOf Me.M_DataArrival
            End If
        End If
    End Function

    Sub SendByteData(ByVal comminfoKey As String, ByVal data() As Byte)
        Dim m As IChannel
        'Dim m1 As TcpChannel
        'Dim s1 As String
        'Dim s2 As String

        'If comminfoKey.Contains("(") Then
        '    s1 = comminfoKey.Split("(")(0)
        '    s2 = comminfoKey.Split("(")(1).Replace(")", "")
        '    m = Channels.Item(s1)
        '    m.SendByteData(data)
        '    m1 = Channels.Item(s2)
        '    m1.SendByteData(data)
        'Else
        m = Channels.Item(comminfoKey)
        m.SendByteData(data)
        'End If
    End Sub

    Function GetChannelFromName(ByVal name As String) As TcpChannel '这要求每个对象的名称是不同的

        GetChannelFromName = Channels.GetChannelObjbySKey(name)
    End Function


    Public Sub New()

        Channels = New Channels

    End Sub

    Sub WakeUp()

        Dim c As TcpChannel
        For Each c In Channels

            c.Mtimer.Enabled = True


        Next c
        'Polltimer = FGetherRtuData.PollTmr
    End Sub

    Sub Sleep()
        'Polltimer = Nothing
        Dim c As TcpChannel
        For Each c In Channels

            c.Mtimer.Enabled = False


        Next c
    End Sub

    'Sub showState()
    '    FGetherRtuData.Show()
    'End Sub


    Function GetChannelState(ByRef comminfo As String) As Boolean

        'Dim s1 As String
        'Dim s2 As String
        Dim b As Boolean



        Try

            b = Channels(comminfo).ChannelState
        Catch
            b = False
        End Try



        GetChannelState = b
    End Function


    Private Sub M_DataArrival(ByVal commInfo As String, ByVal mValue As Object, ByVal bytesTotal As Integer)

        RaiseEvent Dataarrival(commInfo, mValue)
    End Sub
End Class