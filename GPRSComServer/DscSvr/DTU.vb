Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("onlineDTU_NET.onlineDTU")> Public Class onlineDTU
	'��������ֵ�ľֲ�����
	Public DTUid As Integer
	Private mvarPhonenumber As String '�ֲ�����
	'��������ֵ�ľֲ�����
	Private mvarLoginTime As Date '�ֲ�����
	
	
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
	
	
	
	
	
	
	
	
	Public Property PhoneNumber() As String
		Get
			'��������ֵʱʹ�ã�λ�ڸ�ֵ�����ұߡ�
			'Syntax: Debug.Print X.Phonenumber
			PhoneNumber = mvarPhonenumber
		End Get
		Set(ByVal Value As String)
			'������ָ��ֵʱʹ�ã�λ�ڸ�ֵ������ߡ�
			'Syntax: X.Phonenumber = 5
			mvarPhonenumber = Value
		End Set
	End Property
End Class