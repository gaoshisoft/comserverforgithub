Option Strict Off
Option Explicit On
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports VB6 = Microsoft.VisualBasic.Compatibility.VB6
<System.Runtime.InteropServices.ProgId("server_NET.MBserver")> Public Class MBserver

    Public Event Writedata(ByRef DeviceAD As Integer, ByRef startAD As String, ByRef Length As Integer, ByRef value As Object)
    Private mvarDevices As Devices

    Public Event DataArrival(ByRef Data As Object)
    Public Event DataResponse(ByRef Data As Object)
    Private mvarserialComms As serialComms
    'Dim MBTCPsocket As MBTCPsocket
    Private MBTCPsocket As MBTCPsocket = New MBTCPsocket
    Friend Shared _mbs As MBserver



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
    Private Sub Class_Terminate_Renamed()
        mvarserialComms = Nothing
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
            R = MBDevices(Deviceid).GetMBTCPResponsedataFrame(MbRequestFrame)
            GetResponseFrame = (R)
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
                startAD = "0" & start.ToString("00000")
                RaiseEvent Writedata(DeviceAD, startAD, L, V)
            Case 16
                ByteCount = MbRequestFrame(12)
                ReDim V(ByteCount - 1)
                For i = 1 To ByteCount
                    V(i - 1) = MbRequestFrame(12 + i)
                Next i
                startAD = "4" & start.ToString("00000")
                RaiseEvent Writedata(DeviceAD, startAD, L, V)
            Case 5
                ReDim V(1)
                V(0) = MbRequestFrame(12)
                V(1) = MbRequestFrame(13)
                startAD = "0" & start.ToString("00000")
                RaiseEvent Writedata(DeviceAD, startAD, 1, V)
            Case 6
                ReDim V(1)
                V(0) = MbRequestFrame(12)
                V(1) = MbRequestFrame(13)
                startAD = "4" & start.ToString("00000")
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
        'On Error Resume Next
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
            R = MBDevices(Deviceid).GetMBRTUResponsedataFrame(MbRequestFrame)
            GetRTUResponseFrame = (R)

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
                    startAD = "0" & start.ToString("00000")
                    RaiseEvent Writedata(DeviceAD, startAD, L, V)
                Case 16
                    ByteCount = MbRequestFrame(6)
                    ReDim V(ByteCount - 1)
                    For i = 1 To ByteCount
                        V(i - 1) = MbRequestFrame(6 + i)
                    Next i
                    startAD = "4" & start.ToString("00000")
                    RaiseEvent Writedata(DeviceAD, startAD, L, V)
                Case 5
                    ReDim V(1)
                    V(0) = MbRequestFrame(4)
                    V(1) = MbRequestFrame(5)
                    startAD = "0" & start.ToString("00000")
                    RaiseEvent Writedata(DeviceAD, startAD, 1, V)
                Case 6
                    ReDim V(1)
                    V(0) = MbRequestFrame(4)
                    V(1) = MbRequestFrame(5)
                    startAD = "4" & start.ToString("00000")
                    RaiseEvent Writedata(DeviceAD, startAD, 1, V)

            End Select

        End If
        If IsNothing(GetRTUResponseFrame) Then
            ReDim R(0)
            'UPGRADE_WARNING: Couldn't resolve default property of object GetRTUResponseFrame. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            GetRTUResponseFrame = (R)
        End If

    End Function
    Function ReadvalueByAd(ByVal DeviceAD As Integer, ByVal MBad As String, ByVal Datatype As Device.Datatype, Optional ByVal Bit As Object = Nothing) As String
        If Not MBDevices.CheckKey(DeviceAD) Then
            Return 0
            Exit Function
        End If
        Try
            If Not IsNothing(Bit) Then
                ReadvalueByAd = MBDevices.GetDevicefromAd(DeviceAD).ReadModbusbyAD(MBad, Datatype, Bit)
            Else
                ReadvalueByAd = MBDevices.GetDevicefromAd(DeviceAD).ReadModbusbyAD(MBad, Datatype)
            End If
        Catch ex As Exception
        End Try

        ' LogFile Str(DeviceAD) & "  " & MBad & "这个地址不存在，请检查"
    End Function
    Function ReadValueSwapByte(ByVal DeviceAD As Integer, ByVal MBad As String, ByVal Datatype As Device.Datatype) As Object
        If Not MBDevices.CheckKey(DeviceAD) Then
            Return 0
            Exit Function
        End If
        Try
            ReadValueSwapByte = MBDevices.GetDevicefromAd(DeviceAD).ReadValueSwapByte(MBad, Datatype)
        Catch ex As Exception
        End Try
    End Function
    Function WritevaluebyAd(ByVal DeviceAD As Integer, ByVal MBad As String, ByVal Datatype As Device.Datatype, ByVal Mbvalue As Object) As Boolean
        Dim S As String
        Try
            S = MBDevices.GetDevicefromAd(DeviceAD).WriteModbusbyAD(MBad, Mbvalue, Datatype)
            If S = "" Then
                WritevaluebyAd = True
            End If
        Catch ex As Exception
        End Try
        ' LogFile Str(DeviceAD) & "  " & MBad & "这个地址不存在，请检查"

    End Function
    Sub ShowMBserver()
        Dim fmodbus As frmModbusserver = New frmModbusserver With {.MBS = Me}



        fmodbus.Show()

    End Sub
    Sub ShowComdata()

        MBTCPsocket.Show()

    End Sub
    Private MsCommSocket As MsCommSocket = New MsCommSocket
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
        MBDevices.Add(deviceAddr, MBadressQuantity, devicedescription, deviceAddr)
    End Sub
    Sub RemoveMbserverDevice(ByVal deviceAddr As Integer)
        Dim i As Integer
        For i = 1 To MBDevices.Count
            If MBDevices(i).deviceAddr = deviceAddr Then
                MBDevices.Remove(i)
            End If
        Next i

    End Sub

    Sub InformClientDataArrival(ByVal Data As Object)
        RaiseEvent DataArrival(Data)
    End Sub
    Sub InformClientDataResponse(ByVal Data As Object)
        RaiseEvent DataResponse(Data)
    End Sub
    Function GetServerIP() As String


        GetServerIP = MBTCPsocket.TcpSvr.LocalEndpoint.ToString

    End Function
    Function GetServerPort() As Integer

        GetServerPort = MBTCPsocket.Tcpport

    End Function

    Private Sub New(ByVal Ada As Int16, ByVal TcpPort As Integer)
        MBTCPsocket.Init(Ada, TcpPort)

        Me.Devices = New Devices
        MBTCPsocket.MBS = Me
    End Sub
    Public Shared Function GetMBS(ByVal Ada As Int16, ByVal TcpPort As Integer)
        If _mbs Is Nothing Then
            _mbs = New MBserver(Ada, TcpPort)
        End If
        Return _mbs

    End Function
End Class