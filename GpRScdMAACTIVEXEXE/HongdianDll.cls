VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "HongdianDll"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

' *** Den Artikel zu diesem Modul finden Sie unter http://www.aboutvb.de/khw/artikel/khwaddressof.htm ***


Private Const GWL_WNDPROC = -4
Private Const GWL_USERDATA = (-21)
Private Const WM_SIZE = &H5
Private Const DLLNAME = "gprs_dll.dll"

'程序中使用的变量
'Global srvport As Integer     '服务端口
'Global waittime As Integer    '轮询时间
'Global redcolor As Integer
'Global greencolor As Integer
'Global bluecolor As Integer
'Global colorflag As Integer
'Global LineCount As Integer
'Global SysAutoM As Integer

'Global oldwindow As Long

'最大接收缓冲区
Const MAX_RECEIVE_BUF = 1024

Public Type user_info
    m_userid(1 To 12) As Byte        '终端模块号码
    m_sin_addr(1 To 4) As Byte       '终端模块进入Internet的代理主机IP地址
    m_sin_port As Integer            '终端模块进入Internet的代理主机IP端口
    m_nouse As Integer               '作为填充
    m_local_addr(1 To 4) As Byte     '终端模块在移动网内IP地址
    m_local_port As Integer          '终端模块在移动网内IP端口
    m_logon_date(1 To 20) As Byte    '终端模块登录时间
    m_update_date(1 To 20) As Byte   '用户更新时间
    m_status As Byte                 '终端模块状态, 1 在线 0 不在线
End Type

Public Type data_record
    m_userid(1 To 12) As Byte                   '终端模块号码
    m_recv_date(1 To 20) As Byte                '接收到数据包的时间
    m_data_buf(1 To MAX_RECEIVE_BUF) As Byte    '存储接收到的数据
    m_data_len As Integer                       '接收到的数据包长度
    m_data_type   As Byte                       '接收到的数据包类型,0x09：用户数据包 0 不认识类型
End Type

'服务器启动函数
'private declare Function start_server Lib "gprs_dll.dll" _
'(ByVal hw As Long, ByVal Msg As Long, ByVal serport As Long, _
'            mess As Byte, Optional ByVal iPollTime As Integer = 0, Optional ByVal iTimerval As Integer = 30 _
'            ) As Long
Private Declare Function start_gprs_server1 Lib "gprs_dll.dll" _
(ByVal hw As Long, ByVal Msg As Long, ByVal serport As Long, _
            mess As Byte) As Long
Function start_gprs_server(ByVal hw As Long, ByVal Msg As Long, ByVal serport As Long, _
            mess As Byte) As Long

start_gprs_server = start_gprs_server1(hw, Msg, serport, mess)
End Function
'服务器停止函数
Private Declare Function stop_gprs_server1 Lib "gprs_dll.dll" _
(mess As Byte) _
As Long
Function stop_gprs_server(mess As Byte) _
As Long
stop_gprs_server = stop_gprs_server1(mess)
End Function
'获得用户信息
Private Declare Function get_user_at Lib "gprs_dll.dll" _
(ByVal Index As Long, userinfo As user_info) _
As Long

'读数据函数
Private Declare Function do_read_proc Lib "gprs_dll.dll" _
(datarecord As data_record, mess As Byte, ByVal answer As Boolean) _
As Long

'发送数据函数
Private Declare Function do_send_user_data Lib "gprs_dll.dll" _
(userid As Byte, src As Byte, ByVal Srclen As Long, mess As Byte) _
As Long

'关闭某个终端
Private Declare Function do_close_one_user Lib "gprs_dll.dll" _
(userid As Byte, mess As Byte) _
As Long

'关闭全部终端
Private Declare Function do_close_all_user Lib "gprs_dll.dll" _
(mess As Byte) _
As Long

'得到当前在线用户的数量
Private Declare Function get_online_user_amount Lib "gprs_dll.dll" () As Long
'得到允许的最大在线数量
Private Declare Function get_max_user_amount Lib "gprs_dll.dll" () As Long


'windos API函数，可以查看MSDN
Private Declare Function CallWindowProc Lib "user32" Alias _
"CallWindowProcA" (ByVal lpPrevWndFunc As Long, _
   ByVal hwnd As Long, ByVal Msg As Long, _
   ByVal wParam As Long, ByVal lParam As Long) As Long


'windos API函数，可以查看MSDN
Private Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongA" _
      (ByVal hwnd As Long, ByVal nIndex As Long) As Long
      
'windos API函数，可以查看MSDN
Private Declare Function SetWindowLong Lib "user32" Alias _
"SetWindowLongA" (ByVal hwnd As Long, _
ByVal nIndex As Long, ByVal dwNewLong As Long) As Long


'windos API函数，可以查看MSDN
'private declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Long, ByVal wMsg As Long, ByVal wParam As Long, lParam As Any) As Long

Public Function GetAddressOf(ByVal AddressOfProc As Long) As Long
    GetAddressOf = AddressOfProc
End Function



'Public Function NewWindowProc(ByVal hw As Long, ByVal uMsg As Long, _
'      ByVal wParam As Long, ByVal lParam As Long) As Long
'      Dim lpPrevWndProc As Long
'      Dim pcount As Long
'      Dim Rvdata As data_record
'      Dim mess(1 To 1024) As Byte
'      Dim result As Long
'
'
'      If uMsg = (WM_USER + 103) Then
'
'        '调用data_record函数处理结果
'          result = data_record(Rvdata, mess(1), Form1.miAnswer.Checked)
'          If result = 0 Then
'
'              If Rvdata.m_data_len > 0 Then
'
'                  pcount = Val(Form1.Text4.Text)
'
'                  If Form1.miViewData.Checked = True Then
'                      If (pcount Mod 20) = 0 Then Form1.Text3.Text = ""
'
'                      Form1.addtext (vbNewLine)
'                      Form1.addtext ("用户号码：" & StrConv(Rvdata.m_userid, vbUnicode))
'                      Form1.addtext ("接收时间：" & StrConv(Rvdata.m_recv_date, vbUnicode))
'                      Form1.addtext ("数据长度：" & Str(Rvdata.m_data_len))
'                      'Form1.addtext("aaa",true)
'                      If Not Form1.miHEXShow.Checked Then
'                          Form1.addtext ("接收数据：" & StrConv(Rvdata.m_data_buf, vbUnicode))
'                      Else
'                          Form1.addtext ("接收数据：" & Form1.strtohexstr(Rvdata.m_data_buf, Rvdata.m_data_len))
'                      End If
'                  End If
'
'                  If Form1.miCount.Checked = True Then
'                  '显示颜色
'                      Form1.Text4.Text = Str(pcount + 1)
'                      Select Case (colorflag)
'                        Case 1
'                            greencolor = greencolor + 1
'                            If greencolor = 255 Then colorflag = 2
'                        Case 2
'                            redcolor = redcolor - 1
'                            If redcolor = 30 Then colorflag = 3
'                        Case 3
'                            bluecolor = bluecolor + 1
'                            If bluecolor = 255 Then colorflag = 4
'                        Case 4
'                            greencolor = greencolor - 1
'                            If greencolor = 30 Then colorflag = 5
'                        Case 5
'                            redcolor = redcolor + 1
'                            If redcolor = 255 Then colorflag = 6
'                        Case 6
'                            bluecolor = bluecolor - 1
'                            If bluecolor = 30 Then colorflag = 1
'                      End Select
'                      Form1.Text4.BackColor = RGB(redcolor, greencolor, bluecolor)
'                      Form1.Text4.ForeColor = RGB(255 - redcolor, 255 - greencolor, 255 - bluecolor)
'                  End If
'
'              End If
'
'              If Rvdata.m_data_len = 0 Then
'                  Form1.pollusertable
'              End If
'
'          End If
'      Else
'
'      '将消息传递给原来的处理函数，这一行代码是必须的，否则其它消息无法处理
'      NewWindowProc = CallWindowProc(oldwindow, hw, uMsg, wParam, lParam)
'      End If
'End Function
'Public Sub Hook(ByVal hwnd As Long)
'      Dim pOld As Long
'      Dim temp As Long
'      '指定自定义的窗口过程
'      oldwindow = SetWindowLong(hwnd, GWL_WNDPROC, AddressOf NewWindowProc)
'
'    End Sub
'Public Sub Unhook(ByVal hwnd As Long)
'      Dim temp As Long
'      'Cease subclassing.
'      temp = SetWindowLong(hwnd, GWL_WNDPROC, oldwindow)
'End Sub

Public Function SenddataByPhon(ByVal PhoneNumber As String, ByVal length As Long, mess As Byte) As Long
Dim i As Long
Dim sendMess(1 To 1024) As Byte
Dim dtuinfo As user_info
Dim DtuCount As Long
Dim t As Long
Dim sPhon As String
Dim userid As Long
Dim fsendresult As Long
    

DtuCount = get_online_user_amount()
If DtuCount = 0 Then
 '没有在线DTU

Exit Function
Else
For i = 0 To DtuCount - 1

 t = get_user_at(i, dtuinfo)
  If IFthisDTUonline(dtuinfo, 120) Then '如果超过120秒仍未刷新时间，则认为已下线

     sPhon = Left(StrConv(dtuinfo.m_userid, vbUnicode), 11)
     sPhon = Left(Trim(sPhon), 11)
    If sPhon = Trim(PhoneNumber) Then
'    userid = dtuinfo.m_userid
      fsendresult = do_send_user_data(dtuinfo.m_userid(1), mess, length, sendMess(1))
    SenddataByPhon = fsendresult
    Exit Function
    End If
  Else
  End If
Next i
End If

End Function

Function DSgetnextdataAndPhonnumber(Data As data_record, sPhon As String, ByVal timeoutseconds As Integer) As Long
Dim i As Long
Dim mess(1 To 1024) As Byte
   
DSgetnextdataAndPhonnumber = do_read_proc(Data, mess(1), False)
sPhon = StrConv(Data.m_userid, vbUnicode)
End Function
Function IFthisDTUonline(tuserinfo As user_info, waittime As Long) As Boolean
Dim temp As Long
Dim b As Date
Dim closeonemess(1 To 512) As Byte
Dim t_Update As Long
     b = #1/1/1970#
  Dim m1 As Long, m2 As Long
    m1 = 256
    m2 = m1 * 256
                t_Update = (tuserinfo.m_update_date(1)) _
                         + m1 * (tuserinfo.m_update_date(2)) _
                         + m2 * (tuserinfo.m_update_date(3)) _
                         + m2 * (tuserinfo.m_update_date(4)) * 256 _
                         + 3600 * 8
'           If tuserinfo.m_status = 1 Then
                If (Now - b) * 3600 * 24 - t_Update < waittime Then
                    IFthisDTUonline = True
                    Else
                    IFthisDTUonline = False
''                    temp = do_close_one_user(tuserinfo.m_userid(1), closeonemess(1))
               End If
'          Else
'             IFthisDTUonline = False
'          End If
End Function





