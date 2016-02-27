Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("cDSC_NET.cDSC")> Public Class cDSC
    Public Event DataReturn(ByVal PhoneNumber As String, ByVal Value As Object, ByVal length As Integer)
    '保持属性值的局部变量
    Private mvarLocalPort As Integer '局部复制
    Private WithEvents mvarConlineDtus As ConlineDtus
    Dim WithEvents Tmr As Mytimer
    '保持属性值的局部变量
    Private mvarWaitTime As Integer '局部复制
    Dim ComSocket As frmSocket


    Public Property waittime() As Integer
        Get

            'Syntax: Debug.Print X.WaitTime
            waittime = mvarWaitTime
        End Get
        Set(ByVal Value As Integer)

            'Syntax: X.WaitTime = 5
            mvarWaitTime = Value
        End Set
    End Property





    Public Property ConlineDtus() As ConlineDtus
        Get



            ConlineDtus = mvarConlineDtus
        End Get
        Set(ByVal Value As ConlineDtus)
            mvarConlineDtus = Value
        End Set
    End Property





    Public Property LocalPort() As Integer
        Get

            'Syntax: Debug.Print X.LocalPort
            LocalPort = mvarLocalPort
        End Get
        Set(ByVal Value As Integer)

            'Syntax: X.LocalPort = 5
            mvarLocalPort = Value
        End Set
    End Property

    Sub Init(ByVal Ip As System.Net.IPAddress, ByVal ServerPort As Integer, ByVal waittime As Integer)
        ComSocket = New frmSocket
        Me.LocalPort = ServerPort
        Me.waittime = waittime
        ComSocket.Udpport = ServerPort
        ComSocket.Ip = Ip
        ComSocket.myLisener = New MyWinSockListener(Ip, ServerPort)

        mvarConlineDtus = New ConlineDtus
        OnlineDtus = mvarConlineDtus

        Tmr = ComSocket.Tmr
     


    End Sub
    Sub Close()
        Tmr = Nothing
        ComSocket.myLisener.Close()
        ComSocket.Close()
        ComSocket = Nothing

    End Sub

  
    Public Sub New()
        MyBase.New()
        mvarConlineDtus = New ConlineDtus
        OnlineDtus = mvarConlineDtus
        'Load(TCPsocket)



    End Sub

    Private Sub Class_Terminate_Renamed()
        mvarConlineDtus = Nothing
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
        mvarConlineDtus = Nothing
    End Sub
    Sub informDataArrival(ByVal PhoneNumber As String, ByVal Value As Object, ByVal length As Integer)
        RaiseEvent DataReturn(PhoneNumber, Value, length)

    End Sub
    Public Function SenddataByPhon(ByVal PhoneNumber As String, ByVal length As Integer, ByVal Data As Object) As Boolean
        Dim d As ConlineDTU
        On Error Resume Next
        d = Me.ConlineDtus(PhoneNumber)
        If Not d Is Nothing Then


            If Not d.WinSock.UdpClnt Is Nothing Then
                d.WinSock.SendData(Data)
                If ComSocket.Visible Then

                    ComSocket.Label2.Text = "发送数据的winsock号：" & d.SimCardNo
                    'TCPsocket.Text2.Text = HextoStr(Data(0), UBound(Data) + 1, 0)
                End If
                SenddataByPhon = True
            Else
                SenddataByPhon = False
            End If

        Else
            SenddataByPhon = False
        End If




    End Function

    Private Sub mvarConlineDtus_DataReturn(ByVal PhoneNumber As String, ByVal Value As Object, ByVal length As Integer) Handles mvarConlineDtus.DataReturn
        RaiseEvent DataReturn(PhoneNumber, Value, length)
        Me.ConlineDtus(PhoneNumber).HeartBeatTime = Now
    End Sub

    Private Sub Tmr_Timer_Renamed() Handles Tmr.Timer
        Dim i As Integer
        Dim d As ConlineDTU

        For i = 1 To Me.ConlineDtus.Count
            If i <= Me.ConlineDtus.Count Then
                d = Me.ConlineDtus(i)



                If DateDiff(Microsoft.VisualBasic.DateInterval.Second, d.HeartBeatTime, Now) > waittime Then

                    If Not d.WinSock.UdpClnt Is Nothing Then
                        'd.WinSock.UdpClnt.Close()
                        d.WinSock.UdpClnt.Client = Nothing
                        d.WinSock.StopMe()
                        ComSocket.ConnCol.Remove(d.WinSock)
                        Me.ConlineDtus.Remove(i)
                    End If
                Else

                End If
            End If

        Next i

        If ComSocket.Visible Then
            ComSocket.Label1.Text = "在线的DTU数：" & Me.ConlineDtus.Count & "  " & "连接数量：" & ComSocket.ConnCol.Count
        End If
    End Sub

    Function IfThisDtuOline(ByVal PhoneNumber As Object) As Boolean

        IfThisDtuOline = OnlineDtus.Containsobj(Trim(PhoneNumber))

    End Function
End Class