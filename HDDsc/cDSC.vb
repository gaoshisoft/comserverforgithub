Option Strict Off
Option Explicit On

Public Class cDSC
    Public Event DataReturn(ByVal PhoneNumber As String, ByRef Value As Object, ByVal length As Integer)
    '保持属性值的局部变量
    'Private mvarLocalPort As Integer '局部复制
    Private WithEvents mvarConlineDtus As ConlineDtus
    Dim WithEvents Tmr As Timer
    Dim WithEvents TmrRead As Timer
    '保持属性值的局部变量
    Private mvarWaitTime As Integer '局部复制
    Dim mvarState As String
    'Dim TcpSocket As frmSocket = New frmSocket
    Dim mess(1024) As Byte
    Dim result As Short

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





    'Public Property LocalPort() As Integer
    '    Get

    '        'Syntax: Debug.Print X.LocalPort
    '        LocalPort = mvarLocalPort
    '    End Get
    '    Set(ByVal Value As Integer)

    '        'Syntax: X.LocalPort = 5
    '        mvarLocalPort = Value
    '    End Set
    'End Property

    Sub Init(ByVal Ip As System.Net.IPAddress, ByVal ServerPort As Integer, ByVal waittime As Integer)

        Me.waittime = waittime

     
        SetCustomIP(inet_addr(Trim(Ip.ToString))) '设定服务IP
        SelectProtocol(0)             '选择服务类型，0-UDP，1-TCP
        result = SetWorkMode(1)     '选择模式，0-阻塞，1-非阻塞，2-消息
        'If (serv_mode = 2) Then
        '    '''''''''''''''''''''''''''''开发包函数，消息模式启动服务，返回0启动成功
        '    result = start_net_service(Form1.DefInstance.Handle.ToInt32, WM_USER + 103, serv_port, mess(0))
        'Else '''''''''''''''''''''''''''''开发包函数，非消息模式启动服务
        result = start_net_service(10000, 0, ServerPort, mess(0))
        If result = 0 Then
            mvarState = "宏电DSC启动成功 " & ServerPort
        Else
            mvarState = "宏电DSC启动失败 " & ServerPort
        End If

        'End If
    End Sub
    Sub Close()
        result = stop_net_service(mess(0))
        result = stop_gprs_server(mess(0))
    End Sub


    Public Sub New()
        'MyBase.New()
        mvarConlineDtus = New ConlineDtus
        'OnlineDtus = mvarConlineDtus

        Tmr = New Timer With {.Interval = 1000, .Enabled = True}
        TmrRead = New Timer With {.Interval = 100, .Enabled = True}





    End Sub


    Protected Overrides Sub Finalize()
        Close()
        mvarConlineDtus = Nothing
        MyBase.Finalize()
    End Sub
    Sub informDataArrival(ByVal PhoneNumber As String, ByRef Value As Object, ByVal length As Integer)
        RaiseEvent DataReturn(PhoneNumber, Value, length)

    End Sub
    Public Function SenddataByPhon(ByVal PhoneNumber As String, ByVal length As Long, ByVal Dmess() As Byte) As Boolean
        Dim mess(1024) As Byte
        Dim i As Integer

        Dim senduserid(12) As Byte
        Dim sendsrc(1024) As Byte
        Dim sendsrclen As Integer
        Dim src(2048) As Byte
        '判断DTU ID号是否正确，判断发送内容是否为空
        If length <> 0 Then
            sendsrclen = length
            For i = 1 To 11
                senduserid(i) = Asc(Mid(PhoneNumber, i, 1))
            Next
            senduserid(12) = 0

            If (do_send_user_data(senduserid(1), Dmess(0), sendsrclen, mess(1)) = 0) Then '开发包函数，向DTU发送数据

                SenddataByPhon = True
            Else
                SenddataByPhon = False
            End If

        End If
    End Function
    Public Function DscState() As String
        DscState = mvarState
    End Function
    Private Sub mvarConlineDtus_DataReturn(ByVal PhoneNumber As String, ByVal Value As Object, ByVal length As Integer) Handles mvarConlineDtus.DataReturn
        RaiseEvent DataReturn(PhoneNumber, Value, length)
        Me.ConlineDtus(PhoneNumber).HeartBeatTime = Now
    End Sub

    Private Sub RefreshOnlineTable() Handles Tmr.Tick

        Dim i As Integer

        Dim closeonemess(512) As Byte
        Dim temp As Integer
        Dim tucount As Short
        Dim tuserinfo As user_info
        Dim tmess(1024) As Byte
        Dim b As Date
        Dim t_update As Long
        Dim t_now As Long
        Dim m1 As Integer
        Dim m2 As Integer
        Dim P As String
        b = #1/1/1970#

        tucount = get_max_user_amount() '开发包函数，获得中心最大连接DTU数量
        If tucount < 1 Then
            Exit Sub
        End If
        'ListView1.Items.Clear()

        m1 = 256
        m2 = m1 * 256
        For i = 0 To tucount - 1
            temp = get_user_at(i, tuserinfo) '开发包函数，通过DTU的顺序号获得DTU信息
            P = Left(Trim(System.Text.Encoding.Default.GetString(tuserinfo.m_userid)), 11)
            If tuserinfo.m_status = 1 Then
                'waittime = connect_time * 60
                t_update = (tuserinfo.m_update_date(0)) + m1 * (tuserinfo.m_update_date(1)) + m2 * (tuserinfo.m_update_date(2)) + m2 * (tuserinfo.m_update_date(3)) * 256 + 3600 * 8
                t_now = (DateTime.Now.ToOADate() - 25569) * 3600 * 24
                '判断DTU是否超时没有注册，若是则认为该DTU不在线，调用开发包函数使其下线
                If (t_now - t_update) < waittime Then


                    If Not Me.ConlineDtus.ContainsObj(P) Then
                        Me.ConlineDtus.Add(P, System.Text.Encoding.Default.GetString(tuserinfo.m_logon_date), P)
                    End If
                Else
                    If Me.ConlineDtus.ContainsObj(P) Then
                        Me.ConlineDtus.Remove(P)
                        temp = do_close_one_user2(tuserinfo.m_userid(1), closeonemess(0)) '开发包函数，使某个DTU下线
                    End If

                End If

            Else
                If Me.ConlineDtus.ContainsObj(P) Then
                    Me.ConlineDtus.Remove(P)
                    temp = do_close_one_user2(tuserinfo.m_userid(1), closeonemess(0)) '开发包函数，使某个DTU下线(此函数较费CPU资源，放到条件里面去）
                End If

            End If
        Next

    End Sub

    Function IfThisDtuOline(ByVal PhoneNumber As Object) As Boolean

        IfThisDtuOline = Me.ConlineDtus.ContainsObj(PhoneNumber)
    End Function

    Private Sub TmrRead_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TmrRead.Tick


        Dim rvdata As data_record
        Dim mess(1024) As Byte
        Dim result As Integer
        Dim P As String


        result = do_read_proc(rvdata, mess(0), False)
        If result = 0 Then

            If rvdata.m_data_type = 9 Then

                P = Left(Trim(System.Text.UnicodeEncoding.Default.GetString(rvdata.m_userid)), 11)


                Me.informDataArrival(P, rvdata.m_data_buf, rvdata.m_data_len)
            End If
            Select Case (rvdata.m_data_type)
                Case &H1S      '收到注册包，刷新终端登陆列表

                    RefreshOnlineTable()
                Case &H2S      '收到注销包，刷新终端登陆列表

                    RefreshOnlineTable()
                Case &HBS     '收到查询参数成功应答包，取消查询参数超时定时器并将参数读取到DEMO
                    'config.Timer2.Enabled = False
                    'config.OnGetParam()
                    'Form1.DefInstance.addtext(System.Text.UnicodeEncoding.Default.GetString(rvdata.m_userid))
                    'TextBox1.AppendText("参数查询成功!!!")
                    'MsgBox("参数查询已完成!!!")
                    'config.Button1.Enabled = True

                Case &HDS     '收到设置参数成功应答包，取消设置参数超时定时器
                    'config.Timer2.Enabled = False
                    'Form1.DefInstance.addtext(System.Text.UnicodeEncoding.Default.GetString(rvdata.m_userid))
                    'TextBox1.AppendText(" 参数设置成功!!!")
                    'MsgBox(" 参数设置成功!!!")
                    'config.Button2.Enabled = True


                Case &H4S
                    'config.Timer2.Enabled = False
                    'Beep()
                    'MsgBox(" 参数查询(或设置)失败!!!")

                Case 6
                    'Form1.DefInstance.addtext(System.Text.UnicodeEncoding.Default.GetString(rvdata.m_userid))
                    'TextBox1.AppendText("---断开PPP连接成功")
                Case 7
                    'Form1.DefInstance.addtext(System.Text.UnicodeEncoding.Default.GetString(rvdata.m_userid))
                    'TextBox1.AppendText("---停止向DSC发送数据")
                Case 8
                    'Form1.DefInstance.addtext(System.Text.UnicodeEncoding.Default.GetString(rvdata.m_userid))
                    'TextBox1.AppendText("---允许向DSC发送数据")
                Case 10

                    'Form1.DefInstance.addtext(System.Text.UnicodeEncoding.Default.GetString(rvdata.m_userid))
                    'TextBox1.AppendText("---丢弃用户数据")

            End Select
        End If
        If rvdata.m_data_len = 0 Then
            RefreshOnlineTable()
        End If
    End Sub
End Class