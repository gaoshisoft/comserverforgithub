Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("onlineDTU_NET.onlineDTU")> Public Class onlineDTU
	'保持属性值的局部变量
	Public DTUid As Integer
	Private mvarPhonenumber As String '局部复制
	'保持属性值的局部变量
	Private mvarLoginTime As Date '局部复制
	
	
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
	
	
	
	
	
	
	
	
	Public Property PhoneNumber() As String
		Get
			'检索属性值时使用，位于赋值语句的右边。
			'Syntax: Debug.Print X.Phonenumber
			PhoneNumber = mvarPhonenumber
		End Get
		Set(ByVal Value As String)
			'向属性指派值时使用，位于赋值语句的左边。
			'Syntax: X.Phonenumber = 5
			mvarPhonenumber = Value
		End Set
	End Property
End Class