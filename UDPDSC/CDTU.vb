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
    '��������ֵ�ľֲ�����
    Private mvarHeartBeatTime As Date '�ֲ�����


    Public Property HeartBeatTime() As Date
        Get
            '��������ֵʱʹ�ã�λ�ڸ�ֵ�����ұߡ�
            'Syntax: Debug.Print X.HeartBeatTime
            If mvarHeartBeatTime = System.Date.FromOADate(0) Then
                mvarHeartBeatTime = Now
            End If
            HeartBeatTime = mvarHeartBeatTime

        End Get
        Set(ByVal Value As Date)
            '������ָ��ֵʱʹ�ã�λ�ڸ�ֵ������ߡ�
            'Syntax: X.HeartBeatTime = 5
            mvarHeartBeatTime = Value
        End Set
    End Property






    Public Property WinSock() As MyWinSockClient
        Get
            '��������ֵʱʹ�ã�λ�ڸ�ֵ�����ұߡ�
            'Syntax: Debug.Print X.WinSockIndex
            WinSock = mvarWinSock
        End Get
        Set(ByVal Value As MyWinSockClient)
            '������ָ��ֵʱʹ�ã�λ�ڸ�ֵ������ߡ�
            'Syntax: X.WinSockIndex = 5
            mvarWinSock = Value
        End Set
    End Property
	
	
	
	
	
	Public Property ModuleNo() As String
		Get
			'��������ֵʱʹ�ã�λ�ڸ�ֵ�����ұߡ�
			'Syntax: Debug.Print X.ModuleNo
			ModuleNo = mvarModuleNo
		End Get
		Set(ByVal Value As String)
			'������ָ��ֵʱʹ�ã�λ�ڸ�ֵ������ߡ�
			'Syntax: X.ModuleNo = 5
			mvarModuleNo = Value
		End Set
	End Property
	
	
	
	
	
	Public Property DSCportNo() As String
		Get
			'��������ֵʱʹ�ã�λ�ڸ�ֵ�����ұߡ�
			'Syntax: Debug.Print X.DSCPortNo
			DSCportNo = mvarDSCPortNo
		End Get
		Set(ByVal Value As String)
			'������ָ��ֵʱʹ�ã�λ�ڸ�ֵ������ߡ�
			'Syntax: X.DSCPortNo = 5
			mvarDSCPortNo = Value
		End Set
	End Property
	
	
	
	
	
	Public Property SimCardNo() As String
		Get
			'��������ֵʱʹ�ã�λ�ڸ�ֵ�����ұߡ�
			'Syntax: Debug.Print X.SimCardNo
			SimCardNo = mvarSimCardNo
		End Get
		Set(ByVal Value As String)
			'������ָ��ֵʱʹ�ã�λ�ڸ�ֵ������ߡ�
			'Syntax: X.SimCardNo = 5
			mvarSimCardNo = Value
		End Set
	End Property
	
	
	
	
	
	Public Property deviceno() As String
		Get
			'��������ֵʱʹ�ã�λ�ڸ�ֵ�����ұߡ�
			'Syntax: Debug.Print X.DeviceNo
			deviceno = mvarDeviceNo
		End Get
		Set(ByVal Value As String)
			'������ָ��ֵʱʹ�ã�λ�ڸ�ֵ������ߡ�
			'Syntax: X.DeviceNo = 5
			mvarDeviceNo = Value
		End Set
	End Property
	
	
	
	
	
	Public Property LoginTime() As Date
		Get
			'��������ֵʱʹ�ã�λ�ڸ�ֵ�����ұߡ�
			'Syntax: Debug.Print X.LoginTime
			LoginTime = mvarLoginTime
		End Get
		Set(ByVal Value As Date)
			'������ָ��ֵʱʹ�ã�λ�ڸ�ֵ������ߡ�
			'Syntax: X.LoginTime = 5
			mvarLoginTime = Value
		End Set
	End Property
End Class