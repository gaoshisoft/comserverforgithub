Option Strict Off
Option Explicit On
<Runtime.InteropServices.ProgId("onlineDTU_NET.onlineDTU")>
Public Class onlineDTU
    '��������ֵ�ľֲ�����
    Public DTUid As Integer
    Private mvarPhonenumber As String '�ֲ�����
    '��������ֵ�ľֲ�����
    Private mvarLoginTime As Date '�ֲ�����


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


    Public Property PhoneNumber() As String
        Get

            'Syntax: Debug.Print X.commInfo
            PhoneNumber = mvarPhonenumber
        End Get
        Set(ByVal Value As String)

            'Syntax: X.commInfo = 5
            mvarPhonenumber = Value
        End Set
    End Property
End Class