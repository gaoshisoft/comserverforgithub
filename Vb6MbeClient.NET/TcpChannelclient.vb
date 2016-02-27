Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("TcpChannelClient_NET.TcpChannelClient")> Public Class TcpChannelClient
    Public TcpClns As TCPChannels


    Dim WithEvents M As TCPChannel
    Public Event Dataarrival(ByVal comminfo As String, ByVal data() As Byte)

    Property Timeout() As Integer
        Get
            Timeout = TcpClns.Timeout

        End Get
        Set(ByVal Value As Integer)
            TcpClns.Timeout = Value
        End Set
    End Property

    Public Function AddnewTcpClient(ByVal ChannelName As String, ByVal comminfo As String, ByVal polltime As Integer, ByVal Enable As Boolean) As TCPChannel
        'mbers 中的Key都是 192.168.1.5:502 格式的
        Dim Skey As String



        Dim Ipaddr As String
        Dim Port As Int16
        '正常comminfo=192.168.1.2:502 格式
        '冗余comminfo=192.168.1.2:502(192.168.1.3:502) 格式
        If Not comminfo.Contains("(") Then
            Ipaddr = comminfo.Split(":")(0)
            Port = comminfo.Split(":")(1)

            Skey = comminfo
            If TcpClns.checkKey(Skey) = False Then

                AddnewTcpClient = TcpClns.Add(comminfo, Ipaddr, Port, polltime, Me.Timeout, Enable, Skey) 'skey 做关键字

                AddHandler AddnewTcpClient.DataArrival, AddressOf Me.M_DataArrival
            Else
                AddHandler TcpClns(Skey).DataArrival, AddressOf Me.M_DataArrival
            End If

        Else
            Dim s1 As String
            Dim s2 As String
            s1 = comminfo.Split("(")(0)
            s2 = comminfo.Split("(")(1).Replace(")", "")
            Ipaddr = s1.Split(":")(0)
            Port = s1.Split(":")(1)

            Skey = s1
            If TcpClns.checkKey(Skey) = False Then

                AddnewTcpClient = TcpClns.Add(s1, Ipaddr, Port, polltime, Me.Timeout, Enable, Skey) 'skey 做关键字

                AddHandler AddnewTcpClient.DataArrival, AddressOf Me.M_DataArrival
            Else
                AddHandler TcpClns(Skey).DataArrival, AddressOf Me.M_DataArrival
            End If
            Ipaddr = s2.Split(":")(0)
            Port = s2.Split(":")(1)

            Skey = s2
            If TcpClns.checkKey(Skey) = False Then

                AddnewTcpClient = TcpClns.Add(s2, Ipaddr, Port, polltime, Me.Timeout, Enable, Skey) 'skey 做关键字

                AddHandler AddnewTcpClient.DataArrival, AddressOf Me.M_DataArrival
            Else
                AddHandler TcpClns(Skey).DataArrival, AddressOf Me.M_DataArrival
            End If
        End If
    End Function
    Sub SendByteData(ByVal comminfoKey As String, ByVal Data() As Byte)
        Dim m As TCPChannel
        Dim m1 As TCPChannel
        Dim s1 As String
        Dim s2 As String

        If comminfoKey.Contains("(") Then
            s1 = comminfoKey.Split("(")(0)
            s2 = comminfoKey.Split("(")(1).Replace(")", "")
            m = TcpClns.Item(s1)
            m.SendByteData(Data)
            m1 = TcpClns.Item(s2)
            m1.SendByteData(Data)
        Else
            m = TcpClns.Item(comminfoKey)
            m.SendByteData(Data)
        End If
    End Sub

    Function GetChannelFromName(ByVal Name As String) As TCPChannel '这要求每个对象的名称是不同的

        GetChannelFromName = TcpClns.GetChannelObjbySKey(Name)
    End Function




    Public Sub New()

        TcpClns = New TCPChannels




    End Sub
    Sub WakeUp()

        Dim c As TCPChannel
        For Each c In TcpClns

            c.Mtimer.Enabled = True



        Next c
        'Polltimer = FGetherRtuData.PollTmr
    End Sub
    Sub Sleep()
        'Polltimer = Nothing
        Dim c As TCPChannel
        For Each c In TcpClns

            c.Mtimer.Enabled = False



        Next c
    End Sub

    'Sub showState()
    '    FGetherRtuData.Show()
    'End Sub




    Function GetChannelState(ByRef Comminfo As String) As Boolean

        Dim s1 As String
        Dim s2 As String
        Dim b As Boolean
        If Comminfo.Contains("(") Then
            Dim b1 As Boolean
            Dim b2 As Boolean
            Try
                s1 = Comminfo.Split("(")(0)
                s2 = Comminfo.Split("(")(1).Replace(")", "")
                b1 = TcpClns.Item(s1).ChannelState
                b2 = TcpClns.Item(s2).ChannelState
                b = b1 Or b2
            Catch
                b = b1 Or b2
            End Try
        Else
            Try

                b = TcpClns(Comminfo).ChannelState
            Catch
                b = False
            End Try
        End If


        GetChannelState = b

    End Function



    Private Sub M_DataArrival(ByVal CommInfo As String, ByVal mValue As Object, ByVal bytesTotal As Integer) Handles M.DataArrival
        RaiseEvent Dataarrival(CommInfo, mValue)
    End Sub
End Class