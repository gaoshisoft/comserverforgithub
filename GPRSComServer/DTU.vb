Option Strict Off
Option Explicit On
<Runtime.InteropServices.ProgId("onlineDTU_NET.onlineDTU")>
Public Class onlineDTU
    '保持属性值的局部变量
    Public DTUid As Integer
    Private mvarPhonenumber As String '局部复制
    '保持属性值的局部变量
    Private mvarLoginTime As Date '局部复制


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