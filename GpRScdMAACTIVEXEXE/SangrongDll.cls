VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "SangrongDll"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit


Private Const GWL_WNDPROC = -4
Private Const GWL_USERDATA = (-21)
Private Const WM_SIZE = &H5
'Public Const WM_USER = &H400
Private Const DLLNAME = "gprsdll.dll"

'������ʹ�õı���
'Global srvport As Integer     '����˿�
'
'Global waittime As Integer    '��ѯʱ��
'Global redcolor As Integer
'Global greencolor As Integer
'Global bluecolor As Integer
'Global colorflag As Integer
'Global LineCount As Integer
'Global SysAutoM As Integer
'
'Global oldwindow As Long

'�����ջ�����
Const MAX_RECEIVE_BUF = 1450



Public Type ModemInfoStruct
    m_modemid As Long                           'Modemģ���ID��
    m_phoneno(0 To 10) As Byte                  'Modem��11λ�绰���룬������'\0'�ַ���β
    m_dynip(0 To 3) As Byte                     'Modem��4λ��̬ip��ַ
    m_conn_time As Long                        'Modemģ�����һ�ν���TCP���ӵ�ʱ��
    m_refresh_time As Long                    '���յ������ݰ�����
End Type

Public Type ModemDataStruct
    m_modemid As Long                            'Modemģ���ID��
    m_recv_time As Long                        '���յ����ݰ���ʱ��
    m_data_buf(0 To MAX_RECEIVE_BUF + 1) As Byte '�洢���յ�������
    m_data_len As Integer                        '���յ������ݰ�����
    m_data_type As Byte                          '���յ������ݰ�����,0x01���û����ݰ�    0x02���Կ�������֡�Ļ�Ӧ
                                                 
End Type
'����������
'private declare Function start_gprs_server Lib "gprsdll.dll" '(ByVal hw As Long, ByVal Msg As Long, ByVal serport As Long, '            mess As Byte, Optional ByVal iPollTime As Integer = 0, Optional ByVal iTimerval As Integer = 30 '            ) As Long
Private Declare Function DSStartService Lib "gprsdll.dll" (ByVal listenport As Long) As Long    '//û��16λ���޷�����������ֻ�в���32λ���з�����

'ֹͣ������
Private Declare Function DSStopService Lib "gprsdll.dll" () As Long

'����û���Ϣ
Private Declare Function DSGetModemByPosition Lib "gprsdll.dll" (ByVal Index As Long, modeminfo As ModemInfoStruct) As Long

'�����ݺ���
Private Declare Function DSGetNextData Lib "gprsdll.dll" (Data As ModemDataStruct, ByVal timeoutseconds As Integer) As Long

'�������ݺ���
Private Declare Function DSSendData Lib "gprsdll.dll" (ByVal id As Long, ByVal length As Integer, mess As Byte) As Long

'�õ���ǰ�����û�������
Private Declare Function DSGetModemCount Lib "gprsdll.dll" () As Long

'���Ϳ�������
Private Declare Function DSSendControl Lib "gprsdll.dll" (ByVal modemId As Long, ByVal Ctrlen As Integer, buf As Byte) As Long

'windos API���������Բ鿴MSDN
Private Declare Function CallWindowProc Lib "user32" Alias "CallWindowProcA" (ByVal lpPrevWndFunc As Long, ByVal hwnd As Long, ByVal Msg As Long, ByVal wParam As Long, ByVal lParam As Long) As Long


'windos API���������Բ鿴MSDN
Private Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongA" (ByVal hwnd As Long, ByVal nIndex As Long) As Long
      
'windos API���������Բ鿴MSDN
Private Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (ByVal hwnd As Long, ByVal nIndex As Long, ByVal dwNewLong As Long) As Long


'windos API���������Բ鿴MSDN
'private declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Long, ByVal wMsg As Long, ByVal wParam As Long, lParam As Any) As Long

Public Function GetAddressOf(ByVal AddressOfProc As Long) As Long
    GetAddressOf = AddressOfProc
End Function
Public Function SenddataByPhon(ByVal phoneNumber As String, ByVal length As Long, mess As Byte) As Long
Dim i As Long
Dim dtuinfo As ModemInfoStruct
Dim DtuCount As Long
Dim t As Long
Dim sPhon As String
Dim B As Date
Dim userid As Long
Dim fsendresult As Long
    B = #1/1/1970#

DtuCount = DSGetModemCount()
If DtuCount = 0 Then
 'û������DTU

Exit Function
Else
For i = 0 To DtuCount - 1

 t = DSGetModemByPosition(i, dtuinfo)
  If ((Now - B) * 3600 * 24 - dtuinfo.m_refresh_time - 28800) < 70 Then '�������70����δˢ��ʱ�䣬����Ϊ������

     sPhon = StrConv(dtuinfo.m_phoneno, vbUnicode)
     sPhon = Left(Trim(sPhon), 11)
    If sPhon = Trim(phoneNumber) Then
    userid = CLng(dtuinfo.m_modemid)
      fsendresult = DSSendData(userid, length, mess)
    SenddataByPhon = fsendresult
    Exit Function
    End If
  Else
  End If
Next i
End If

End Function

Function DSgetnextdataAndPhonnumber(Data As ModemDataStruct, sPhon As String, ByVal timeoutseconds As Integer) As Long
Dim i As Long
Dim dtuinfo As ModemInfoStruct
Dim DtuCount As Long
Dim t As Long
'Dim sPhon As String
Dim B As Date
'Dim UserID As Long
Dim fsendresult As Long
    B = #1/1/1970#
DSgetnextdataAndPhonnumber = DSGetNextData(Data, 0)
If DSgetnextdataAndPhonnumber <> 0 Then
DtuCount = DSGetModemCount()
If DtuCount = 0 Then
 'û������DTU

Exit Function
Else
For i = 0 To DtuCount - 1

 t = DSGetModemByPosition(i, dtuinfo)
  If ((Now - B) * 3600 * 24 - dtuinfo.m_refresh_time - 28800) < 70 Then '�������70����δˢ��ʱ�䣬����Ϊ������

     
    
    If dtuinfo.m_modemid = Data.m_modemid Then
    sPhon = Left(StrConv(dtuinfo.m_phoneno, vbUnicode), 11)
      
    'SenddataByPhon = fsendresult
    Exit Function
    End If
  
  End If
Next i
End If
End If
End Function
