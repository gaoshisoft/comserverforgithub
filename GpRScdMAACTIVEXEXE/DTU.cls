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
'��������ֵ�ľֲ�����
Public DTUid As Long
Private mvarPhonenumber As String '�ֲ�����
'��������ֵ�ľֲ�����
Private mvarLoginTime As Date '�ֲ�����
Public Property Let LoginTime(ByVal vData As Date)
'������ָ��ֵʱʹ�ã�λ�ڸ�ֵ������ߡ�
'Syntax: X.LoginTime = 5
    mvarLoginTime = vData
End Property


Public Property Get LoginTime() As Date
'��������ֵʱʹ�ã�λ�ڸ�ֵ�����ұߡ�
'Syntax: Debug.Print X.LoginTime
    LoginTime = mvarLoginTime
End Property






Public Property Let PhoneNumber(ByVal vData As String)
'������ָ��ֵʱʹ�ã�λ�ڸ�ֵ������ߡ�
'Syntax: X.Phonenumber = 5
    mvarPhonenumber = vData
End Property


Public Property Get PhoneNumber() As String
'��������ֵʱʹ�ã�λ�ڸ�ֵ�����ұߡ�
'Syntax: Debug.Print X.Phonenumber
    PhoneNumber = mvarPhonenumber
End Property


