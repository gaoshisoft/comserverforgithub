Option Strict Off
Option Explicit On
Public Class mscommWsk
    Inherits System.IO.Ports.SerialPort
    '处理真正的通讯工作
    Public Event DataSend(ByRef mvalue As Object)
    Public Event DataArrival(ByRef mvalue As Object, ByRef bytesTotal As Integer)
    Public Index As Integer
    Dim WithEvents Tmr As Timer
    Public CommSetting As String

    Dim CommandBuffer As New Collection
    Private ExpectedRvLength As Integer '应该接收的字节数
    Private commandsend As Boolean
    Public PortNo As Integer
    Public Name As String
    Function GetRightRvlength(ByRef Mbcommand As Object) As Integer '因为只能通过是否收到正确长度来确定是否通讯正常，所以判断长度也成为这个类的本职工作
        Dim Rightlength As Short
        Dim FC As Integer
        Dim RigisterLength As Integer
        FC = Mbcommand(1)
        RigisterLength = Mbcommand(4) * 256 + Mbcommand(5)
        Select Case FC
            Case 3, 4
                Rightlength = 1 + 1 + 1 + RigisterLength * 2 + 2
            Case 1, 2
                Rightlength = 1 + 1 + 1 + IIf(RigisterLength Mod 8 = 0, RigisterLength \ 8, RigisterLength \ 8 + 1) + 2
            Case 15, 16, 5, 6
                Rightlength = 8
        End Select
        GetRightRvlength = Rightlength
    End Function


    Sub SendData(ByRef Data As Object)
        CommandBuffer.Add(Data)
    End Sub
    ReadOnly Property State() As Integer
        Get
            Dim Sp As System.IO.Ports.SerialPort
            Sp = DirectCast(Me, System.IO.Ports.SerialPort)
            If Sp.IsOpen Then
                '.
                'If MsCommSocket.MSComm1(Me.Index).PortOpen = True Then
                State = 7
            End If
        End Get
    End Property
    Sub CloseMe()
        'If MsCommSocket.MSComm1(Me.Index).PortOpen = True Then
        If Me.IsOpen = True Then
            Me.Close()
        End If
        'MsCommSocket.MSComm1(Me.Index).PortOpen = False
        'End If
    End Sub
    Function ConnectTO(ByVal Settings As String, ByVal PortNo As Integer) As String
        'On Error GoTo err_Renamed
        Dim sp As System.IO.Ports.SerialPort
        sp = DirectCast(Me, System.IO.Ports.SerialPort)
        Dim St() As String
        St = Settings.Split(",")

        sp.BaudRate = St(0)
        sp.DataBits = St(1)
        sp.Parity = St(2)
        sp.StopBits = St(3)
        sp.PortName = "COM" & CStr(PortNo)

        'MsCommSocket.MSComm1(Me.Index).Settings = Trim(Settings)
        'MsCommSocket.MSComm1(Me.Index).CommPort = PortNo
        sp.Open()
        'MsCommSocket.MSComm1(Me.Index).PortOpen = True
        '        Exit Function
        'err_Renamed:
        '        ConnectTO = Err.Description
    End Function

    Sub InformDataArrival(ByRef mvalue As Object, ByRef bytesTotal As Integer)
        RaiseEvent DataArrival(mvalue, bytesTotal)

    End Sub


    Public Sub New()
        MyBase.New()

    End Sub

    'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    'Private Sub Class_Terminate_Renamed()

    'If MsCommSocket.MSComm1(Me.Index).PortOpen Then
    '    MsCommSocket.MSComm1(Me.Index).PortOpen = False
    'End If
    'MsCommSocket.MSComm1.Unload(Me.Index)
    'End Sub
    Protected Overrides Sub Finalize()
        'Class_Terminate_Renamed()
        MyBase.Close()
        MyBase.Finalize()
    End Sub



    Private Function SendBufferData() As Boolean
        If Me.State <> 7 Then Exit Function
        Dim D() As Byte
        If CommandBuffer.Count() >= 1 Then
            'UPGRADE_WARNING: Couldn't resolve default property of object CommandBuffer(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            'UPGRADE_WARNING: Couldn't resolve default property of object D. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            D = CommandBuffer.Item(1)
            CommandBuffer.Remove(1)
            'SendBufferData = True
            RaiseEvent DataSend(D)
            'UPGRADE_WARNING: Couldn't resolve default property of object D. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            'MsCommSocket.MSComm1(Me.Index).Output = D
            Me.Write(D, 0, D.Length)
            'ExpectedRvLength = Me.GetRightRvlength(CommandBuffer(1))



        End If

    End Function






    Private Sub mscommWsk_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles Me.DataReceived
        Dim Inputdata() As Byte
        Dim MBResponsedataFrame() As Byte

        ' Static havesendmsec As Long
        ' If commandsend = False Then
        'On Error Resume Next
        commandsend = SendBufferData() '如果发送成功，则commandsend=true
        'End If

        'If commandsend = True Then
        Dim rvLength As Integer
        'rvLength = MsCommSocket.MSComm1(Me.Index).InBufferCount
        rvLength = Me.ReadBufferSize

        If rvLength >= 8 Then '标准modbus 命令长度均为8

            '     commandsend = False
            '     havesendmsec = 0
            'UPGRADE_WARNING: Couldn't resolve default property of object MsCommSocket.MSComm1().Input. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            Me.Read(Inputdata, 0, 8)
            'Me.re()
            InformDataArrival(Inputdata, UBound(Inputdata) + 1)
            'UPGRADE_WARNING: Couldn't resolve default property of object MBS.GetRTUResponseFrame(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            MBResponsedataFrame = Mbs.GetRTUResponseFrame(Inputdata) '获取本地反应数据帧
            If UBound(MBResponsedataFrame) > 0 Then
                SendData(MBResponsedataFrame)
            End If
        End If
        '    havesendmsec = havesendmsec + 100
        '    If havesendmsec > 10000 Then '如果发送后10s后没有响应，则内部认为超时
        '       commandsend = False
        '       havesendmsec = 0
        '       End If
        'End If
    End Sub
End Class