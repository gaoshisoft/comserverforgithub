Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("ConlineDTU_NET.ConlineDTU")> Public Class ConlineDTU
	'��������ֵ�ľֲ�����
	Private mvarLoginTime As Date '�ֲ�����
	Private mvarDeviceNo As String '�ֲ�����
	Private mvarSimCardNo As String '�ֲ�����
	Private mvarDSCPortNo As String '�ֲ�����
	Private mvarModuleNo As String '�ֲ�����
	'��������ֵ�ľֲ�����
    Private mvarWinSock As MyWinSockClient  '�ֲ�����
    'Private mUdpClnt As System.Net.Sockets.UdpClient
    '��������ֵ�ľֲ�����
    Private mvarHeartBeatTime As Date '�ֲ�����


    Public Property HeartBeatTime() As Date
        Get

            'Syntax: Debug.Print X.HeartBeatTime
            If mvarHeartBeatTime = System.DateTime.FromOADate(0) Then
                mvarHeartBeatTime = Now
            End If
            HeartBeatTime = mvarHeartBeatTime

        End Get
        Set(ByVal Value As Date)

            'Syntax: X.HeartBeatTime = 5
            mvarHeartBeatTime = Value
        End Set
    End Property






    Public Property WinSock() As MyWinSockClient
        Get

            'Syntax: Debug.Print X.WinSockIndex
            WinSock = mvarWinSock
        End Get
        Set(ByVal Value As MyWinSockClient)

            'Syntax: X.WinSockIndex = 5
            mvarWinSock = Value
        End Set
    End Property





    Public Property ModuleNo() As String
        Get

            'Syntax: Debug.Print X.ModuleNo
            ModuleNo = mvarModuleNo
        End Get
        Set(ByVal Value As String)

            'Syntax: X.ModuleNo = 5
            mvarModuleNo = Value
        End Set
    End Property





    Public Property DSCportNo() As String
        Get

            'Syntax: Debug.Print X.DSCPortNo
            DSCportNo = mvarDSCPortNo
        End Get
        Set(ByVal Value As String)

            'Syntax: X.DSCPortNo = 5
            mvarDSCPortNo = Value
        End Set
    End Property





    Public Property SimCardNo() As String
        Get

            'Syntax: Debug.Print X.SimCardNo
            SimCardNo = mvarSimCardNo
        End Get
        Set(ByVal Value As String)

            'Syntax: X.SimCardNo = 5
            mvarSimCardNo = Value
        End Set
    End Property





    Public Property deviceno() As String
        Get

            'Syntax: Debug.Print X.DeviceNo
            deviceno = mvarDeviceNo
        End Get
        Set(ByVal Value As String)

            'Syntax: X.DeviceNo = 5
            mvarDeviceNo = Value
        End Set
    End Property





    Public Property LoginTime() As Date
        Get

            'Syntax: Debug.Print X.LoginTime
            LoginTime = mvarLoginTime
        End Get
        Set(ByVal Value As Date)

            'Syntax: X.LoginTime = 5
            mvarLoginTime = Value
        End Set
    End Property
End Class