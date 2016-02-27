VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "onlineDTU"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Attribute VB_Ext_KEY = "SavedWithClassBuilder6" ,"Yes"
Attribute VB_Ext_KEY = "Top_Level" ,"Yes"
'保持属性值的局部变量
Public DTUid As Long
Private mvarPhonenumber As String '局部复制
'保持属性值的局部变量
Private mvarLoginTime As Date '局部复制
Public Property Let LoginTime(ByVal vData As Date)
'向属性指派值时使用，位于赋值语句的左边。
'Syntax: X.LoginTime = 5
    mvarLoginTime = vData
End Property


Public Property Get LoginTime() As Date
'检索属性值时使用，位于赋值语句的右边。
'Syntax: Debug.Print X.LoginTime
    LoginTime = mvarLoginTime
End Property






Public Property Let PhoneNumber(ByVal vData As String)
'向属性指派值时使用，位于赋值语句的左边。
'Syntax: X.Phonenumber = 5
    mvarPhonenumber = vData
End Property


Public Property Get PhoneNumber() As String
'检索属性值时使用，位于赋值语句的右边。
'Syntax: Debug.Print X.Phonenumber
    PhoneNumber = mvarPhonenumber
End Property



