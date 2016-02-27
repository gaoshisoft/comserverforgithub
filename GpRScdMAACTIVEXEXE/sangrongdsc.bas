Attribute VB_Name = "SRdll"
Option Explicit


Private Const GWL_WNDPROC = -4
Private Const GWL_USERDATA = (-21)
Private Const WM_SIZE = &H5
'Public Const WM_USER = &H400
Private Const DLLNAME = "gprsdll.dll"

'程序中使用的变量
'Global srvport As Integer     '服务端口
'
'Global waittime As Integer    '轮询时间
'Global redcolor As Integer
'Global greencolor As Integer
'Global bluecolor As Integer
'Global colorflag As Integer
'Global LineCount As Integer
'Global SysAutoM As Integer
'
'Global oldwindow As Long

'最大接收缓冲区
Const MAX_RECEIVE_BUF = 1450



Public Type ModemInfoStruct
    m_modemid As Long                           'Modem模块的ID号
    m_phoneno(0 To 10) As Byte                  'Modem的11位电话号码，必须以'\0'字符结尾
    m_dynip(0 To 3) As Byte                     'Modem的4位动态ip地址
    m_conn_time As Long                        'Modem模块最后一次建立TCP连接的时间
    m_refresh_time As Long                    '接收到的数据包长度
End Type

Public Type ModemDataStruct
    m_modemid As Long                            'Modem模块的ID号
    m_recv_time As Long                        '接收到数据包的时间
    m_data_buf(0 To MAX_RECEIVE_BUF + 1) As Byte '存储接收到的数据
    m_data_len As Integer                        '接收到的数据包长度
    m_data_type As Byte                          '接收到的数据包类型,0x01：用户数据包    0x02：对控制命令帧的回应
                                                 
End Type
'启动服务函数
'Public Declare Function start_gprs_server Lib "gprsdll.dll" '(ByVal hw As Long, ByVal Msg As Long, ByVal serport As Long, '            mess As Byte, Optional ByVal iPollTime As Integer = 0, Optional ByVal iTimerval As Integer = 30 '            ) As Long
Public Declare Function DSStartService Lib "gprsdll.dll" (ByVal listenport As Long) As Long    '//没有16位的无符号数，所以只有采用32位的有符号数

'停止服务函数
Public Declare Function DSStopService Lib "gprsdll.dll" () As Long

'获得用户信息
Public Declare Function DSGetModemByPosition Lib "gprsdll.dll" (ByVal Index As Long, modeminfo As ModemInfoStruct) As Long

'读数据函数
Public Declare Function DSGetNextData Lib "gprsdll.dll" (Data As ModemDataStruct, ByVal timeoutseconds As Integer) As Long

'发送数据函数
Public Declare Function DSSendData Lib "gprsdll.dll" (ByVal id As Long, ByVal length As Integer, mess As Byte) As Long

'得到当前在线用户的数量
Public Declare Function DSGetModemCount Lib "gprsdll.dll" () As Long

'发送控制命令
Public Declare Function DSSendControl Lib "gprsdll.dll" (ByVal modemId As Long, ByVal Ctrlen As Integer, buf As Byte) As Long

'windos API函数，可以查看MSDN
Public Declare Function CallWindowProc Lib "user32" Alias "CallWindowProcA" (ByVal lpPrevWndFunc As Long, ByVal hwnd As Long, ByVal Msg As Long, ByVal wParam As Long, ByVal lParam As Long) As Long


'windos API函数，可以查看MSDN
Public Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongA" (ByVal hwnd As Long, ByVal nIndex As Long) As Long
      
'windos API函数，可以查看MSDN
Public Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (ByVal hwnd As Long, ByVal nIndex As Long, ByVal dwNewLong As Long) As Long


'windos API函数，可以查看MSDN
'Public Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Long, ByVal wMsg As Long, ByVal wParam As Long, lParam As Any) As Long

Public Function GetAddressOf(ByVal AddressOfProc As Long) As Long
    GetAddressOf = AddressOfProc
End Function
Public Function SenddataByPhon(ByVal PhoneNumber As String, ByVal length As Long, mess As Byte) As Boolean
Dim i As Long
Dim Dtuinfo As ModemInfoStruct
Dim DtuCount As Long
Dim t As Long
Dim sPhon As String
Dim B As Date
Dim userid As Long
Dim fsendresult As Long
    B = #1/1/1970#

DtuCount = DSGetModemCount()
If DtuCount = 0 Then
 '没有在线DTU

Exit Function
Else
For i = 0 To DtuCount - 1

 t = DSGetModemByPosition(i, Dtuinfo)
  If ((Now - B) * 3600 * 24 - Dtuinfo.m_refresh_time - 28800) < 70 Then '如果超过70秒仍未刷新时间，则认为已下线

     sPhon = StrConv(Dtuinfo.m_phoneno, vbUnicode)
     sPhon = Left(Trim(sPhon), 11)
    If sPhon = Trim(PhoneNumber) Then
    userid = CLng(Dtuinfo.m_modemid)
      fsendresult = DSSendData(userid, length, mess)
    SenddataByPhon = IIf(fsendresult = 1, True, False)
    Exit Function
    End If
  Else
  End If
Next i
End If

End Function

Function DSgetnextdataAndPhonnumber(Data As ModemDataStruct, sPhon As String, ByVal timeoutseconds As Integer) As Long
Dim i As Long
Dim Dtuinfo As ModemInfoStruct
Dim DtuCount As Long
Dim t As Long
'Dim sPhon As String
Dim B As Date
'Dim UserID As Long
Dim fsendresult As Long
    B = #1/1/1970#
DSgetnextdataAndPhonnumber = IIf(DSGetNextData(Data, 0) = 1, True, False)
If DSgetnextdataAndPhonnumber <> 0 Then
DtuCount = DSGetModemCount()
If DtuCount = 0 Then
 '没有在线DTU

Exit Function
Else
For i = 0 To DtuCount - 1

 t = DSGetModemByPosition(i, Dtuinfo)
  If ((Now - B) * 3600 * 24 - Dtuinfo.m_refresh_time - 28800) < 70 Then '如果超过70秒仍未刷新时间，则认为已下线

     
    
    If Dtuinfo.m_modemid = Data.m_modemid Then
    sPhon = Left(Trim(StrConv(Dtuinfo.m_phoneno, vbUnicode)), 11)
      
    'SenddataByPhon = fsendresult
    Exit Function
    End If
  
  End If
Next i
End If
End If
End Function
Public Function IfThisDtuonline(ByVal Phone As String, Waittime As Long) As Boolean  '该函数如果没能找到相应的userid,会返回没找到的原因分别为0,1,2
Dim Dtuinfo As ModemInfoStruct
Dim DtuCount As Long
Dim t As Long
Dim sPhon As String
Dim B As Date
    B = #1/1/1970#
Dim i As Long
DtuCount = DSGetModemCount()
If DtuCount = 0 Then
 IfThisDtuonline = False '没有在线DTU

Exit Function
Else
For i = 0 To DtuCount - 1

 t = DSGetModemByPosition(i, Dtuinfo)
  If ((Now - B) * 3600 * 24 - Dtuinfo.m_refresh_time - 28800) < Waittime Then '如果超过70秒仍未刷新时间，则认为已下线

     sPhon = StrConv(Dtuinfo.m_phoneno, vbUnicode)
     sPhon = Left(Trim(sPhon), 11)
    If sPhon = Trim(Phone) Then
    IfThisDtuonline = True
    Exit Function
    End If
  Else
 IfThisDtuonline = False '这个DTU已下线
  End If
Next i
End If
IfThisDtuonline = False '在线DTU中没有找到符合这个号码的

End Function

