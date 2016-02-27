Option Strict Off
Option Explicit On

<System.Runtime.InteropServices.ProgId("cDSC_NET.cDSC")> Public Class cDSC
    Public Event DataReturn(ByVal phoneNumber As String, ByVal value As Object, ByVal length As Integer)
    Private _mvarLocalPort As Integer
    Private WithEvents mvarConlineDtus As ConlineDtus
    Private _mvarWaitTime As Integer
    Private _tcpSocket As FrmSocket
    Private Shared DSC As cDSC
    Dim WithEvents Tmr As Timer = New Timer With {.Interval = 100, .Enabled = True}
    Public Shared Function GetTCPsvrDSC() As cDSC  '单例模式
        If DSC Is Nothing Then
            DSC = New cDSC
            Return DSC
        Else
            Return DSC
        End If

    End Function



    Public Property Waittime() As Integer
        Get

            waittime = _mvarWaitTime
        End Get
        Set(ByVal Value As Integer)

            _mvarWaitTime = Value
        End Set
    End Property
    Public Property DSCstate() As String
        Get
            If _tcpSocket.myLisener.Ifconnected Then
                DSCstate = "USR TCP DSC 启动成功 端口：" & Me.LocalPort
            End If
        End Get
        Set(ByVal value As String)

        End Set

    End Property





    Public Property OnlineDtus() As ConlineDtus
        Get



            OnlineDtus = mvarConlineDtus
        End Get
        Set(ByVal Value As ConlineDtus)
            mvarConlineDtus = Value
        End Set
    End Property





    Public Property LocalPort() As Integer
        Get

            LocalPort = _mvarLocalPort
        End Get
        Set(ByVal Value As Integer)

            _mvarLocalPort = Value
        End Set
    End Property

    Sub Init(ByVal ServerPort As Integer, ByVal waittime As Integer)
       



            _tcpSocket.Tcpport = ServerPort

            _tcpSocket.myLisener = MyWinSockListener.GetMyListener(ServerPort) '单例模式
            Me.LocalPort = ServerPort
      




        Me.Waittime = waittime


        _tcpSocket.myLisener.Start()
        Tmr.Enabled = True

    End Sub
    Sub Restart(ByVal ServerPort As Integer, ByVal waittime As Integer)
        If ServerPort <> Me.LocalPort Then '
            Close()

            _tcpSocket = New FrmSocket

            _tcpSocket.Tcpport = ServerPort

            _tcpSocket.myLisener = MyWinSockListener.GetMyListener(ServerPort) '单例模式
            Me.LocalPort = ServerPort

        End If



        Me.Waittime = Waittime


        _tcpSocket.myLisener.Start()
        Tmr.Enabled = True
    End Sub
    Sub Close() 'close操作与finalize ，close是停止运作，释放端口等资源，finalize是最终释放所有资源，

        Tmr.Enabled = False
        _tcpSocket.myLisener.StopMe()
        For Each clnt As MyWinSockClient In _tcpSocket.ConnCol
            RemoveHandler clnt.DataArrival, AddressOf _tcpSocket.myTcpClient_DataArrival
            clnt.CloseMe()

        Next
        _tcpSocket.ConnCol.Clear()
        _tcpSocket.Close()



    End Sub


    Private Sub New()
        MyBase.New()
        mvarConlineDtus = New ConlineDtus
        _tcpSocket = New FrmSocket
        _tcpSocket.DSC = Me

    End Sub


    Protected Overrides Sub Finalize()
        Close()
        MyBase.Finalize()
        mvarConlineDtus = Nothing

        _tcpSocket = Nothing
        Tmr = Nothing
    End Sub
    Sub InformDataArrival(ByVal PhoneNumber As String, ByVal Value As Object, ByVal length As Integer)
        RaiseEvent DataReturn(PhoneNumber, Value, length)

    End Sub
    Public Function SenddataByPhon(ByVal PhoneNumber As String, ByVal length As Integer, ByVal Data As Object) As Boolean
        Dim d As ConlineDTU
        On Error Resume Next
        d = OnlineDtus(PhoneNumber)
        If Not d Is Nothing Then
            'UPGRADE_NOTE: State was upgraded to CtlState. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
            If Not d.WinSock.TcpClnt Is Nothing Then
                If d.WinSock.TcpClnt.Connected = True Then
                    d.WinSock.SendData(Data)
                    If _tcpSocket.Visible Then

                        _tcpSocket.Label2.Text = "发送数据的winsock号：" & d.SimCardNo

                    End If
                    SenddataByPhon = True
                Else
                    SenddataByPhon = False
                End If


            Else
                SenddataByPhon = False
            End If

        Else
            SenddataByPhon = False
        End If




    End Function

    Private Sub mvarConlineDtus_DataReturn(ByVal PhoneNumber As String, ByVal Value As Object, ByVal length As Integer) Handles mvarConlineDtus.DataReturn
        RaiseEvent DataReturn(PhoneNumber, Value, length)
        OnlineDtus(PhoneNumber).HeartBeatTime = Now
    End Sub

    Private Sub Tmr_Timer_Renamed() Handles Tmr.Tick
        Dim i As Integer
        Dim d As ConlineDTU

        For i = 1 To OnlineDtus.Count
            If i <= OnlineDtus.Count Then
                d = OnlineDtus(i)



                If DateDiff(Microsoft.VisualBasic.DateInterval.Second, d.HeartBeatTime, Now) > Waittime Then
                    If Not d.WinSock.TcpClnt Is Nothing Then

                        d.WinSock.CloseMe()
                        _tcpSocket.ConnCol.Remove(d.WinSock)
                        OnlineDtus.Remove(i)
                    End If

                Else
                    If Not d.WinSock.TcpClnt Is Nothing Then
                        If d.WinSock.TcpClnt.Connected = False Then

                            d.WinSock.CloseMe()
                            _tcpSocket.ConnCol.Remove(d.WinSock)
                            OnlineDtus.Remove(i)
                        End If
                    End If
                End If
            End If

        Next i

        If _tcpSocket.Visible Then
            _tcpSocket.Label1.Text = "在线的DTU数：" & OnlineDtus.Count & "  " & "连接数量：" & _tcpSocket.ConnCol.Count
        End If
    End Sub

    Function IfThisDtuOline(ByVal PhoneNumber As Object) As Boolean

        IfThisDtuOline = mvarConlineDtus.Containsobj(Trim(PhoneNumber))

    End Function

  
End Class