Option Strict Off
Option Explicit On
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
'UPGRADE_WARNING: Class instancing was changed to public. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="ED41034B-3890-49FC-8076-BD6FC2F42A85"'
<System.Runtime.InteropServices.ProgId("server_NET.MBserver")> Public Class MBserver

    Public Event Writedata(ByRef DeviceAD As Integer, ByRef startAD As String, ByRef Length As Integer, ByRef value As Object)
    Private mvarDevices As Devices

    Public Event DataArrival(ByRef Data As Object)
    Public Event DataResponse(ByRef Data As Object)
    Private mvarserialComms As serialComms
    'Dim MBTCPsocket As MBTCPsocket




    Public Property serialComms() As serialComms
        Get
            If mvarserialComms Is Nothing Then
                mvarserialComms = New serialComms
            End If


            serialComms = mvarserialComms
        End Get
        Set(ByVal Value As serialComms)
            mvarserialComms = Value
        End Set
    End Property

    Public ReadOnly Property MBDevices() As Devices
        Get
            MBDevices = mvarDevices
        End Get

    End Property




    Public Property Devices() As Devices
        Get
            If mvarDevices Is Nothing Then
                mvarDevices = New Devices
            End If


            Devices = mvarDevices
        End Get
        Set(ByVal Value As Devices)
            mvarDevices = Value
        End Set
    End Property
    'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Terminate_Renamed()
        'UPGRADE_NOTE: Object mvarserialComms may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mvarserialComms = Nothing
        'UPGRADE_NOTE: Object mvarDevices may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mvarDevices = Nothing
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub




    Public Function GetResponseFrame(ByVal MbRequestFrame() As Byte) As Object '返回数据，并同时做相应的动作
        Dim R() As Byte
        'Dim RealdataStart As Long
        'Dim mbadinthisrtu As Long
        Dim Deviceid As Integer
        Dim i As Integer
        Dim DeviceAD As Integer
        Dim Functioncode As Integer
        On Error Resume Next
        '获取反应桢
        DeviceAD = MbRequestFrame(6)
        Functioncode = MbRequestFrame(7)
        Dim startAD As String
        For i = 1 To MBDevices.Count
            If MBDevices(i).deviceAddr = DeviceAD Then
                Deviceid = i
            End If
        Next i
        If Deviceid > 0 Then
            'UPGRADE_WARNING: Couldn't resolve default property of object MBdevices().GetMBTCPResponsedataFrame(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            R = MBDevices(Deviceid).GetMBTCPResponsedataFrame(MbRequestFrame)
            'UPGRADE_WARNING: Couldn't resolve default property of object GetResponseFrame. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            GetResponseFrame = VB6.CopyArray(R)
        End If

        '引发事件,通知调用者
        Dim start As Integer
        Dim L As Integer
        start = 256 * MbRequestFrame(8) + MbRequestFrame(9) + 1
        L = 256 * MbRequestFrame(10) + MbRequestFrame(11)
        Dim ByteCount As Integer
        Dim V() As Byte
        Select Case Functioncode

            Case 15
                ByteCount = MbRequestFrame(12)
                ReDim V(ByteCount - 1)
                For i = 1 To ByteCount
                    V(i - 1) = MbRequestFrame(12 + i)
                Next i
                startAD = "0" & VB6.Format(start, "00000")
                RaiseEvent Writedata(DeviceAD, startAD, L, V)
            Case 16
                ByteCount = MbRequestFrame(12)
                ReDim V(ByteCount - 1)
                For i = 1 To ByteCount
                    V(i - 1) = MbRequestFrame(12 + i)
                Next i
                startAD = "4" & VB6.Format(start, "00000")
                RaiseEvent Writedata(DeviceAD, startAD, L, V)
            Case 5
                ReDim V(1)
                V(0) = MbRequestFrame(12)
                V(1) = MbRequestFrame(13)
                startAD = "0" & VB6.Format(start, "00000")
                RaiseEvent Writedata(DeviceAD, startAD, 1, V)
            Case 6
                ReDim V(1)
                V(0) = MbRequestFrame(12)
                V(1) = MbRequestFrame(13)
                startAD = "4" & VB6.Format(start, "00000")
                RaiseEvent Writedata(DeviceAD, startAD, 1, V)

        End Select

    End Function
    Function GetRTUResponseFrame(ByVal MbRequestFrame() As Byte) As Object '返回数据，并同时做相应的动作
        Dim R() As Byte
        'Dim RealdataStart As Long
        'Dim mbadinthisrtu As Long
        Dim Deviceid As Integer
        Dim i As Integer
        Dim DeviceAD As Integer
        Dim Functioncode As Integer
        On Error Resume Next
        '获取反应桢
        DeviceAD = MbRequestFrame(0)
        Functioncode = MbRequestFrame(1)
        Dim startAD As String
        For i = 1 To MBDevices.Count
            If MBDevices(i).deviceAddr = DeviceAD Then
                Deviceid = i
            End If
        Next i
        If Deviceid > 0 Then
            'UPGRADE_WARNING: Couldn't resolve default property of object MBdevices().GetMBRTUResponsedataFrame(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            R = MBDevices(Deviceid).GetMBRTUResponsedataFrame(MbRequestFrame)
            'UPGRADE_WARNING: Couldn't resolve default property of object GetRTUResponseFrame. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            GetRTUResponseFrame = VB6.CopyArray(R)

        End If

        '引发事件,通知调用者
        Dim start As Integer
        Dim L As Integer
        Dim ByteCount As Integer
        Dim V() As Byte
        If Deviceid > 0 Then
            start = 256 * MbRequestFrame(2) + MbRequestFrame(3) + 1
            L = 256 * MbRequestFrame(4) + MbRequestFrame(5)
            Select Case Functioncode

                Case 15
                    ByteCount = MbRequestFrame(6)
                    ReDim V(ByteCount - 1)
                    For i = 1 To ByteCount
                        V(i - 1) = MbRequestFrame(6 + i)
                    Next i
                    startAD = "0" & VB6.Format(start, "00000")
                    RaiseEvent Writedata(DeviceAD, startAD, L, V)
                Case 16
                    ByteCount = MbRequestFrame(6)
                    ReDim V(ByteCount - 1)
                    For i = 1 To ByteCount
                        V(i - 1) = MbRequestFrame(6 + i)
                    Next i
                    startAD = "4" & VB6.Format(start, "00000")
                    RaiseEvent Writedata(DeviceAD, startAD, L, V)
                Case 5
                    ReDim V(1)
                    V(0) = MbRequestFrame(4)
                    V(1) = MbRequestFrame(5)
                    startAD = "0" & VB6.Format(start, "00000")
                    RaiseEvent Writedata(DeviceAD, startAD, 1, V)
                Case 6
                    ReDim V(1)
                    V(0) = MbRequestFrame(4)
                    V(1) = MbRequestFrame(5)
                    startAD = "4" & VB6.Format(start, "00000")
                    RaiseEvent Writedata(DeviceAD, startAD, 1, V)

            End Select

        End If
        'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        If IsNothing(GetRTUResponseFrame) Then
            ReDim R(0)
            'UPGRADE_WARNING: Couldn't resolve default property of object GetRTUResponseFrame. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            GetRTUResponseFrame = VB6.CopyArray(R)
        End If

    End Function
    Function ReadvalueByAd(ByVal DeviceAD As Integer, ByVal MBad As String, ByVal Datatype As Device.Datatype, Optional ByVal Bit As Object = Nothing) As String
        On Error GoTo Errh
        If Not IsNothing(Bit) Then
            ReadvalueByAd = MBDevices.GetDevicefromAd(DeviceAD).ReadModbusbyAD(MBad, Datatype, Bit)
        Else
            ReadvalueByAd = MBDevices.GetDevicefromAd(DeviceAD).ReadModbusbyAD(MBad, Datatype)
        End If
        Exit Function
Errh:
        ' LogFile Str(DeviceAD) & "  " & MBad & "这个地址不存在，请检查"
    End Function
    Function ReadValueSwapByte(ByVal DeviceAD As Integer, ByVal MBad As String, ByVal Datatype As Device.Datatype) As Object
        ReadValueSwapByte = MBDevices.GetDevicefromAd(DeviceAD).ReadValueSwapByte(MBad, Datatype)
    End Function
    Function WritevaluebyAd(ByVal DeviceAD As Integer, ByVal MBad As String, ByVal Datatype As Device.Datatype, ByVal Mbvalue As Object) As Boolean
        Dim S As String
        On Error GoTo Errh
        S = MBDevices.GetDevicefromAd(DeviceAD).WriteModbusbyAD(MBad, Mbvalue, Datatype)
        If S = "" Then
            WritevaluebyAd = True
        End If
        Exit Function
Errh:
        ' LogFile Str(DeviceAD) & "  " & MBad & "这个地址不存在，请检查"

    End Function
    Sub ShowMBserver()
     

        frmModbusserver.MBDevices = Mbs.Devices

        frmModbusserver.Show()
     
    End Sub
    Sub ShowComdata()

        MBTCPsocket.Show()
       
    End Sub
    Sub ShowMBRTUcomdata()
        MsCommSocket.Show()
    End Sub
    Sub HideMBrtuComdata()
        MsCommSocket.Hide()
    End Sub
    Sub Hidecomdata()
        MBTCPsocket.Hide()
    End Sub
    Sub AddMbserverDevice(ByVal deviceAddr As Integer, ByVal MBadressQuantity As Integer, ByVal devicedescription As String)
        '-------这句话为限制功能而写
        'If Not (deviceAddr = 7 Or deviceAddr = 8 Or deviceAddr = 9 Or deviceAddr = 10 Or deviceAddr = 11 Or deviceAddr = 255) Then
        '    Exit Sub

        'End If
        '-------------------------
        On Error Resume Next
        Dim i As Integer
        For i = 1 To MBDevices.Count
            If MBDevices(i).deviceAddr = deviceAddr Then
                MBDevices(i).MBadressQuantity = MBDevices(i).MBadressQuantity + MBadressQuantity
                MBDevices(i).devicedescription = MBDevices(i).devicedescription & devicedescription
                Exit Sub
            End If
        Next i
        MBDevices.Add(deviceAddr, MBadressQuantity, devicedescription)
    End Sub
    Sub RemoveMbserverDevice(ByVal deviceAddr As Integer)
        Dim i As Integer
        For i = 1 To MBDevices.Count
            If MBDevices(i).deviceAddr = deviceAddr Then
                MBDevices.Remove(i)
            End If
        Next i

    End Sub
    'Function GetEventMbs(ByVal Tcpport As Integer) As MBserver
    '    MBTCPsocket.Tcpport = Tcpport

    '    MBTCPsocket = New MBTCPsocket
    '    MBdevices = New Devices
    '    Mbs = New MBserver
    '    Mbs.Devices = MBdevices

    '    Mbs.AddMbserverDevice(255, 256, "控制命令接收地址区")
    '    GetEventMbs = Mbs
    'End Function
    Sub InformClientDataArrival(ByVal Data As Object)
        RaiseEvent DataArrival(Data)
    End Sub
    Sub InformClientDataResponse(ByVal Data As Object)
        RaiseEvent DataResponse(Data)
    End Sub
    Function GetServerIP() As String
        'Dim EndP As IPEndPoint

        GetServerIP = MBTCPsocket.TcpSvr.LocalEndpoint.ToString
    End Function
    Function GetServerPort() As Integer

        GetServerPort = MBTCPsocket.Tcpport
        'My.Computer .Network .
    End Function

    Public Sub New(ByVal Ada As Int16, ByVal TcpPort As Integer)
        MBTCPsocket.Init(Ada, TcpPort)
        'MBTCPsocket.Tcpport = TcpPort
        'MBTCPsocket.Show()
        'MBTCPsocket.Visible = False
        Me.Devices = New Devices

    End Sub
End Class