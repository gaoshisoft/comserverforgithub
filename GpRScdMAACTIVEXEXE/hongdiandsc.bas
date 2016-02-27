Attribute VB_Name = "hddll"
Option Explicit

' *** Den Artikel zu diesem Modul finden Sie unter http://www.aboutvb.de/khw/artikel/khwaddressof.htm ***


Private Const GWL_WNDPROC = -4
Private Const GWL_USERDATA = (-21)
Private Const WM_SIZE = &H5
Public Const WM_USER = &H400
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
'Public Declare Function start_server Lib "gprs_dll.dll" _
'(ByVal hw As Long, ByVal Msg As Long, ByVal serport As Long, _
'            mess As Byte, Optional ByVal iPollTime As Integer = 0, Optional ByVal iTimerval As Integer = 30 _
'            ) As Long
Public Declare Function start_gprs_server Lib "gprs_dll.dll" _
(ByVal hw As Long, ByVal Msg As Long, ByVal serport As Long, _
            mess As Byte) As Long

'服务器停止函数
Public Declare Function stop_gprs_server Lib "gprs_dll.dll" _
(mess As Byte) _
As Long

'获得用户信息
Public Declare Function get_user_at Lib "gprs_dll.dll" _
(ByVal Index As Long, userinfo As user_info) _
As Long

'读数据函数
Public Declare Function do_read_proc Lib "gprs_dll.dll" _
(datarecord As data_record, mess As Byte, ByVal answer As Boolean) _
As Long

'发送数据函数
Public Declare Function do_send_user_data Lib "gprs_dll.dll" _
(userid As Byte, src As Byte, ByVal Srclen As Long, mess As Byte) _
As Long

'关闭某个终端
Public Declare Function do_close_one_user Lib "gprs_dll.dll" _
(userid As Byte, mess As Byte) _
As Long

'关闭全部终端
Public Declare Function do_close_all_user Lib "gprs_dll.dll" _
(mess As Byte) _
As Long

'得到当前在线用户的数量
Public Declare Function get_online_user_amount Lib "gprs_dll.dll" () As Long
'得到允许的最大在线数量
Public Declare Function get_max_user_amount Lib "gprs_dll.dll" () As Long


'windos API函数，可以查看MSDN
Public Declare Function CallWindowProc Lib "user32" Alias _
"CallWindowProcA" (ByVal lpPrevWndFunc As Long, _
   ByVal hwnd As Long, ByVal Msg As Long, _
   ByVal wParam As Long, ByVal lParam As Long) As Long


'windos API函数，可以查看MSDN
Public Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongA" _
      (ByVal hwnd As Long, ByVal nIndex As Long) As Long
      
'windos API函数，可以查看MSDN
Public Declare Function SetWindowLong Lib "user32" Alias _
"SetWindowLongA" (ByVal hwnd As Long, _
ByVal nIndex As Long, ByVal dwNewLong As Long) As Long


'windos API函数，可以查看MSDN
'Public Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Long, ByVal wMsg As Long, ByVal wParam As Long, lParam As Any) As Long

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

Public Function SenddataByPhon(ByVal PhoneNumber As String, ByVal length As Long, mess As Byte) As Boolean
Dim i As Long
Dim sendMess(1 To 1024) As Byte
Dim Dtuinfo As user_info
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

 t = get_user_at(i, Dtuinfo)
'  If IfThisDtuonline(Dtuinfo, 120) Then '如果超过70秒仍未刷新时间，则认为已下线

     sPhon = Left(StrConv(Dtuinfo.m_userid, vbUnicode), 11)
     sPhon = Left(Trim(sPhon), 11)
    If sPhon = Trim(PhoneNumber) Then
'    userid = dtuinfo.m_userid
      fsendresult = do_send_user_data(Dtuinfo.m_userid(1), mess, length, sendMess(1))
    SenddataByPhon = IIf(fsendresult = 0, True, False)
    Exit Function
    End If
'  Else
'  End If
Next i
End If

End Function

Function DSgetnextdataAndPhonnumber(Data As data_record, sPhon As String, ByVal timeoutseconds As Integer) As Long
Dim i As Long
Dim mess(1 To 1024) As Byte
   
DSgetnextdataAndPhonnumber = IIf(hddll.do_read_proc(Data, mess(1), False) = 0, True, False)
sPhon = Left(Trim(StrConv(Data.m_userid, vbUnicode)), 11)
End Function
Function IfThisDtuonline(PhoneNumber As String, Waittime As Long) As Boolean
Dim temp As Long
Dim B As Date
Dim i As Long
Dim count As Long
Dim Dtuinfo As user_info
Dim sPhon As String
Dim t As Long
Dim closeonemess(1 To 512) As Byte
Dim t_Update As Long
     B = #1/1/1970#
  Dim m1 As Long, m2 As Long
    m1 = 256
    m2 = m1 * 256
    count = hddll.get_max_user_amount
    
     IfThisDtuonline = False '先初始化为假
    
    For i = 0 To 200 - 1

 t = get_user_at(i, Dtuinfo)
  t_Update = (Dtuinfo.m_update_date(1)) _
                         + m1 * (Dtuinfo.m_update_date(2)) _
                         + m2 * (Dtuinfo.m_update_date(3)) _
                         + m2 * (Dtuinfo.m_update_date(4)) * 256 _
                         + 3600 * 8
                 If (Now - B) * 3600 * 24 - t_Update < Waittime Then
                     sPhon = Left(StrConv(Dtuinfo.m_userid, vbUnicode), 11)
                      sPhon = Left(Trim(sPhon), 11)
                        If sPhon = Trim(PhoneNumber) Then
                           IfThisDtuonline = True '在在线中找到即为真
                           Exit Function
                        End If
                 End If

  
  Next i



               
End Function



