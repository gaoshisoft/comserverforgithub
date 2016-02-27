Option Strict Off
Option Explicit On
Module hddll
	
	' *** Den Artikel zu diesem Modul finden Sie unter http://www.aboutvb.de/khw/artikel/khwaddressof.htm ***
	
	
	Private Const GWL_WNDPROC As Short = -4
	Private Const GWL_USERDATA As Short = (-21)
	Private Const WM_SIZE As Integer = &H5
	Public Const WM_USER As Integer = &H400
	Private Const DLLNAME As String = "gprs_dll.dll"
	
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
	Const MAX_RECEIVE_BUF As Short = 1024
	
	Public Structure user_info
        <VBFixedArray(12)> Dim m_userid() As Byte '终端模块号码
		<VBFixedArray(4)> Dim m_sin_addr() As Byte '终端模块进入Internet的代理主机IP地址
		Dim m_sin_port As Short '终端模块进入Internet的代理主机IP端口
		Dim m_nouse As Short '作为填充
		<VBFixedArray(4)> Dim m_local_addr() As Byte '终端模块在移动网内IP地址
		Dim m_local_port As Short '终端模块在移动网内IP端口
		<VBFixedArray(20)> Dim m_logon_date() As Byte '终端模块登录时间
		<VBFixedArray(20)> Dim m_update_date() As Byte '用户更新时间
		Dim m_status As Byte '终端模块状态, 1 在线 0 不在线
		
		'UPGRADE_TODO: "Initialize" must be called to initialize instances of this structure. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B4BFF9E0-8631-45CF-910E-62AB3970F27B"'
		Public Sub Initialize()
			'UPGRADE_WARNING: Lower bound of array m_userid was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
			ReDim m_userid(12)
			'UPGRADE_WARNING: Lower bound of array m_sin_addr was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
			ReDim m_sin_addr(4)
			'UPGRADE_WARNING: Lower bound of array m_local_addr was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
			ReDim m_local_addr(4)
			'UPGRADE_WARNING: Lower bound of array m_logon_date was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
			ReDim m_logon_date(20)
			'UPGRADE_WARNING: Lower bound of array m_update_date was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
			ReDim m_update_date(20)
		End Sub
	End Structure
	
	Public Structure data_record
		<VBFixedArray(12)> Dim m_userid() As Byte '终端模块号码
		<VBFixedArray(20)> Dim m_recv_date() As Byte '接收到数据包的时间
		<VBFixedArray(MAX_RECEIVE_BUF)> Dim m_data_buf() As Byte '存储接收到的数据
		Dim m_data_len As Short '接收到的数据包长度
		Dim m_data_type As Byte '接收到的数据包类型,0x09：用户数据包 0 不认识类型
		
		'UPGRADE_TODO: "Initialize" must be called to initialize instances of this structure. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B4BFF9E0-8631-45CF-910E-62AB3970F27B"'
		Public Sub Initialize()
			'UPGRADE_WARNING: Lower bound of array m_userid was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
            ReDim m_userid(12)

			'UPGRADE_WARNING: Lower bound of array m_recv_date was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
			ReDim m_recv_date(20)
			'UPGRADE_WARNING: Lower bound of array m_data_buf was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
			ReDim m_data_buf(MAX_RECEIVE_BUF)
		End Sub
	End Structure
	
	'服务器启动函数
	'Public Declare Function start_server Lib "gprs_dll.dll" _
	''(ByVal hw As Long, ByVal Msg As Long, ByVal serport As Long, _
	''            mess As Byte, Optional ByVal iPollTime As Integer = 0, Optional ByVal iTimerval As Integer = 30 _
	''            ) As Long
	Public Declare Function start_gprs_server Lib "gprs_dll.dll" (ByVal hw As Integer, ByVal Msg As Integer, ByVal serport As Integer, ByRef mess As Byte) As Integer
	
	'服务器停止函数
	Public Declare Function stop_gprs_server Lib "gprs_dll.dll" (ByRef mess As Byte) As Integer
	
	'获得用户信息
	'UPGRADE_WARNING: Structure user_info may require marshalling attributes to be passed as an argument in this Declare statement. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="C429C3A5-5D47-4CD9-8F51-74A1616405DC"'
	Public Declare Function get_user_at Lib "gprs_dll.dll" (ByVal Index As Integer, ByRef userinfo As user_info) As Integer
	
	'读数据函数
	'UPGRADE_WARNING: Structure data_record may require marshalling attributes to be passed as an argument in this Declare statement. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="C429C3A5-5D47-4CD9-8F51-74A1616405DC"'
	Public Declare Function do_read_proc Lib "gprs_dll.dll" (ByRef datarecord As data_record, ByRef mess As Byte, ByVal answer As Boolean) As Integer
	
	'发送数据函数
	Public Declare Function do_send_user_data Lib "gprs_dll.dll" (ByRef userid As Byte, ByRef src As Byte, ByVal Srclen As Integer, ByRef mess As Byte) As Integer
	
	'关闭某个终端
	Public Declare Function do_close_one_user Lib "gprs_dll.dll" (ByRef userid As Byte, ByRef mess As Byte) As Integer
	
	'关闭全部终端
	Public Declare Function do_close_all_user Lib "gprs_dll.dll" (ByRef mess As Byte) As Integer
	
	'得到当前在线用户的数量
	Public Declare Function get_online_user_amount Lib "gprs_dll.dll" () As Integer
	'得到允许的最大在线数量
	Public Declare Function get_max_user_amount Lib "gprs_dll.dll" () As Integer
	
	
	'windos API函数，可以查看MSDN
	Public Declare Function CallWindowProc Lib "user32"  Alias "CallWindowProcA"(ByVal lpPrevWndFunc As Integer, ByVal hwnd As Integer, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
	
	
	'windos API函数，可以查看MSDN
	Public Declare Function GetWindowLong Lib "user32"  Alias "GetWindowLongA"(ByVal hwnd As Integer, ByVal nIndex As Integer) As Integer
	
	'windos API函数，可以查看MSDN
	Public Declare Function SetWindowLong Lib "user32"  Alias "SetWindowLongA"(ByVal hwnd As Integer, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer
	
	
	'windos API函数，可以查看MSDN
	'Public Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Long, ByVal wMsg As Long, ByVal wParam As Long, lParam As Any) As Long
	
	Public Function GetAddressOf(ByVal AddressOfProc As Integer) As Integer
		GetAddressOf = AddressOfProc
	End Function
	
	
	
	'Public Function NewWindowProc(ByVal hw As Long, ByVal uMsg As Long, _
	''      ByVal wParam As Long, ByVal lParam As Long) As Long
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
	
	Public Function SenddataByPhon(ByVal PhoneNumber As String, ByVal length As Integer, ByRef mess As Byte) As Boolean
		Dim i As Integer
		'UPGRADE_WARNING: Lower bound of array sendMess was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
		Dim sendMess(1024) As Byte
		'UPGRADE_WARNING: Arrays in structure Dtuinfo may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
        Dim Dtuinfo As New user_info
		Dim DtuCount As Integer
		Dim t As Integer
		Dim sPhon As String
		Dim userid As Integer
		Dim fsendresult As Integer
		
		
		DtuCount = get_online_user_amount()
		If DtuCount = 0 Then
			'没有在线DTU
			
			Exit Function
		Else
			For i = 0 To DtuCount - 1
				
				t = get_user_at(i, Dtuinfo)
				'  If IfThisDtuonline(Dtuinfo, 120) Then '如果超过70秒仍未刷新时间，则认为已下线
				
				'UPGRADE_ISSUE: Constant vbUnicode was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
                sPhon = Left(StrConv(System.Text.UnicodeEncoding.Unicode.GetString(Dtuinfo.m_userid), VbStrConv.SimplifiedChinese), 11)
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
	
	Function DSgetnextdataAndPhonnumber(ByRef Data As data_record, ByRef sPhon As String, ByVal timeoutseconds As Short) As Integer
		Dim i As Integer
		'UPGRADE_WARNING: Lower bound of array mess was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
		Dim mess(1024) As Byte
		
		DSgetnextdataAndPhonnumber = IIf(hddll.do_read_proc(Data, mess(1), False) = 0, True, False)
		'UPGRADE_ISSUE: Constant vbUnicode was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
        sPhon = StrConv(System.Text.UnicodeEncoding.Unicode.GetString(Data.m_userid), VbStrConv.SimplifiedChinese)
	End Function
	Function IfThisDtuonline(ByRef PhoneNumber As String, ByRef Waittime As Integer) As Boolean
		Dim temp As Integer
		Dim B As Date
		Dim i As Integer
		Dim count As Integer
		'UPGRADE_WARNING: Arrays in structure Dtuinfo may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
        Dim Dtuinfo As New user_info
		Dim sPhon As String
		Dim t As Integer
		'UPGRADE_WARNING: Lower bound of array closeonemess was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
		Dim closeonemess(512) As Byte
		Dim t_Update As Integer
		B = #1/1/1970#
		Dim m1, m2 As Integer
		m1 = 256
		m2 = m1 * 256
		count = hddll.get_max_user_amount
		
		IfThisDtuonline = False '先初始化为假
		
		For i = 0 To 200 - 1
			
			t = get_user_at(i, Dtuinfo)
			t_Update = (Dtuinfo.m_update_date(1)) + m1 * (Dtuinfo.m_update_date(2)) + m2 * (Dtuinfo.m_update_date(3)) + m2 * (Dtuinfo.m_update_date(4)) * 256 + 3600 * 8
			If System.Date.FromOADate(Now.ToOADate - B.ToOADate).ToOADate * 3600 * 24 - t_Update < Waittime Then
				'UPGRADE_ISSUE: Constant vbUnicode was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
                sPhon = Left(StrConv(System.Text.UnicodeEncoding.Unicode.GetString(Dtuinfo.m_userid), VbStrConv.SimplifiedChinese), 11)
				sPhon = Left(Trim(sPhon), 11)
				If sPhon = Trim(PhoneNumber) Then
					IfThisDtuonline = True '在在线中找到即为真
					Exit Function
				End If
			End If
			
			
		Next i
		
		
		
		
	End Function
End Module