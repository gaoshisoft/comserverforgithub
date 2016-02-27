Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("ConlineDTU_NET.ConlineDTU")> Public Class ConlineDTU
	'保持属性值的局部变量
	Private mvarLoginTime As Date '局部复制
	Private mvarDeviceNo As String '局部复制
	Private mvarSimCardNo As String '局部复制
	Private mvarDSCPortNo As String '局部复制
	Private mvarModuleNo As String '局部复制
	'保持属性值的局部变量
    Private mvarWinSock As MyWinSockClient  '局部复制
    '保持属性值的局部变量
    Private mvarHeartBeatTime As Date '局部复制


    Public Property HeartBeatTime() As Date
        Get
            '检索属性值时使用，位于赋值语句的右边。
            'Syntax: Debug.Print X.HeartBeatTime
            If mvarHeartBeatTime = System.Date.FromOADate(0) Then
                mvarHeartBeatTime = Now
            End If
            HeartBeatTime = mvarHeartBeatTime

        End Get
        Set(ByVal Value As Date)
            '向属性指派值时使用，位于赋值语句的左边。
            'Syntax: X.HeartBeatTime = 5
            mvarHeartBeatTime = Value
        End Set
    End Property






    Public Property WinSock() As MyWinSockClient
        Get
            '检索属性值时使用，位于赋值语句的右边。
            'Syntax: Debug.Print X.WinSockIndex
            WinSock = mvarWinSock
        End Get
        Set(ByVal Value As MyWinSockClient)
            '向属性指派值时使用，位于赋值语句的左边。
            'Syntax: X.WinSockIndex = 5
            mvarWinSock = Value
        End Set
    End Property
	
	
	
	
	
	Public Property ModuleNo() As String
		Get
			'检索属性值时使用，位于赋值语句的右边。
			'Syntax: Debug.Print X.ModuleNo
			ModuleNo = mvarModuleNo
		End Get
		Set(ByVal Value As String)
			'向属性指派值时使用，位于赋值语句的左边。
			'Syntax: X.ModuleNo = 5
			mvarModuleNo = Value
		End Set
	End Property
	
	
	
	
	
	Public Property DSCportNo() As String
		Get
			'检索属性值时使用，位于赋值语句的右边。
			'Syntax: Debug.Print X.DSCPortNo
			DSCportNo = mvarDSCPortNo
		End Get
		Set(ByVal Value As String)
			'向属性指派值时使用，位于赋值语句的左边。
			'Syntax: X.DSCPortNo = 5
			mvarDSCPortNo = Value
		End Set
	End Property
	
	
	
	
	
	Public Property SimCardNo() As String
		Get
			'检索属性值时使用，位于赋值语句的右边。
			'Syntax: Debug.Print X.SimCardNo
			SimCardNo = mvarSimCardNo
		End Get
		Set(ByVal Value As String)
			'向属性指派值时使用，位于赋值语句的左边。
			'Syntax: X.SimCardNo = 5
			mvarSimCardNo = Value
		End Set
	End Property
	
	
	
	
	
	Public Property deviceno() As String
		Get
			'检索属性值时使用，位于赋值语句的右边。
			'Syntax: Debug.Print X.DeviceNo
			deviceno = mvarDeviceNo
		End Get
		Set(ByVal Value As String)
			'向属性指派值时使用，位于赋值语句的左边。
			'Syntax: X.DeviceNo = 5
			mvarDeviceNo = Value
		End Set
	End Property
	
	
	
	
	
	Public Property LoginTime() As Date
		Get
			'检索属性值时使用，位于赋值语句的右边。
			'Syntax: Debug.Print X.LoginTime
			LoginTime = mvarLoginTime
		End Get
		Set(ByVal Value As Date)
			'向属性指派值时使用，位于赋值语句的左边。
			'Syntax: X.LoginTime = 5
			mvarLoginTime = Value
		End Set
	End Property
End Class