Option Strict Off
Option Explicit On
Module SRdll
	
	
	Private Const GWL_WNDPROC As Short = -4
	Private Const GWL_USERDATA As Short = (-21)
	Private Const WM_SIZE As Integer = &H5
	'Public Const WM_USER = &H400
	Private Const DLLNAME As String = "gprsdll.dll"
	
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
	Const MAX_RECEIVE_BUF As Short = 1450
	
	
	
	Public Structure ModemInfoStruct
		Dim m_modemid As Integer 'Modemģ���ID��
		<VBFixedArray(10)> Dim m_phoneno() As Byte 'Modem��11λ�绰���룬������'\0'�ַ���β
		<VBFixedArray(3)> Dim m_dynip() As Byte 'Modem��4λ��̬ip��ַ
		Dim m_conn_time As Integer 'Modemģ�����һ�ν���TCP���ӵ�ʱ��
		Dim m_refresh_time As Integer '���յ������ݰ�����
		
		'UPGRADE_TODO: "Initialize" must be called to initialize instances of this structure. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B4BFF9E0-8631-45CF-910E-62AB3970F27B"'
		Public Sub Initialize()
			ReDim m_phoneno(10)
			ReDim m_dynip(3)
		End Sub
	End Structure
	
	Public Structure ModemDataStruct
		Dim m_modemid As Integer 'Modemģ���ID��
		Dim m_recv_time As Integer '���յ����ݰ���ʱ��
		<VBFixedArray(MAX_RECEIVE_BUF + 1)> Dim m_data_buf() As Byte '�洢���յ�������
		Dim m_data_len As Short '���յ������ݰ�����
		Dim m_data_type As Byte '���յ������ݰ�����,0x01���û����ݰ�    0x02���Կ�������֡�Ļ�Ӧ
		
		'UPGRADE_TODO: "Initialize" must be called to initialize instances of this structure. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B4BFF9E0-8631-45CF-910E-62AB3970F27B"'
		Public Sub Initialize()
			ReDim m_data_buf(MAX_RECEIVE_BUF + 1)
		End Sub
	End Structure
	'����������
	'Public Declare Function start_gprs_server Lib "gprsdll.dll" '(ByVal hw As Long, ByVal Msg As Long, ByVal serport As Long, '            mess As Byte, Optional ByVal iPollTime As Integer = 0, Optional ByVal iTimerval As Integer = 30 '            ) As Long
	Public Declare Function DSStartService Lib "gprsdll.dll" (ByVal listenport As Integer) As Integer '//û��16λ���޷�����������ֻ�в���32λ���з�����
	
	'ֹͣ������
	Public Declare Function DSStopService Lib "gprsdll.dll" () As Integer
	
	'����û���Ϣ
	'UPGRADE_WARNING: Structure ModemInfoStruct may require marshalling attributes to be passed as an argument in this Declare statement. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="C429C3A5-5D47-4CD9-8F51-74A1616405DC"'
	Public Declare Function DSGetModemByPosition Lib "gprsdll.dll" (ByVal Index As Integer, ByRef modeminfo As ModemInfoStruct) As Integer
	
	'�����ݺ���
	'UPGRADE_WARNING: Structure ModemDataStruct may require marshalling attributes to be passed as an argument in this Declare statement. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="C429C3A5-5D47-4CD9-8F51-74A1616405DC"'
	Public Declare Function DSGetNextData Lib "gprsdll.dll" (ByRef Data As ModemDataStruct, ByVal timeoutseconds As Short) As Integer
	
	'�������ݺ���
	Public Declare Function DSSendData Lib "gprsdll.dll" (ByVal id As Integer, ByVal length As Short, ByRef mess As Byte) As Integer
	
	'�õ���ǰ�����û�������
	Public Declare Function DSGetModemCount Lib "gprsdll.dll" () As Integer
	
	'���Ϳ�������
	Public Declare Function DSSendControl Lib "gprsdll.dll" (ByVal modemId As Integer, ByVal Ctrlen As Short, ByRef buf As Byte) As Integer
	
	'windos API���������Բ鿴MSDN
	Public Declare Function CallWindowProc Lib "user32"  Alias "CallWindowProcA"(ByVal lpPrevWndFunc As Integer, ByVal hwnd As Integer, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
	
	
	'windos API���������Բ鿴MSDN
	Public Declare Function GetWindowLong Lib "user32"  Alias "GetWindowLongA"(ByVal hwnd As Integer, ByVal nIndex As Integer) As Integer
	
	'windos API���������Բ鿴MSDN
	Public Declare Function SetWindowLong Lib "user32"  Alias "SetWindowLongA"(ByVal hwnd As Integer, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer
	
	
	'windos API���������Բ鿴MSDN
	'Public Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Long, ByVal wMsg As Long, ByVal wParam As Long, lParam As Any) As Long
	
	Public Function GetAddressOf(ByVal AddressOfProc As Integer) As Integer
		GetAddressOf = AddressOfProc
	End Function
	Public Function SenddataByPhon(ByVal PhoneNumber As String, ByVal length As Integer, ByRef mess As Byte) As Boolean
		Dim i As Integer
		'UPGRADE_WARNING: Arrays in structure Dtuinfo may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		Dim Dtuinfo As ModemInfoStruct
		Dim DtuCount As Integer
		Dim t As Integer
		Dim sPhon As String
		Dim B As Date
		Dim userid As Integer
		Dim fsendresult As Integer
		B = #1/1/1970#
		
		DtuCount = DSGetModemCount()
		If DtuCount = 0 Then
			'û������DTU
			
			Exit Function
		Else
			For i = 0 To DtuCount - 1
				
				t = DSGetModemByPosition(i, Dtuinfo)
				If (System.Date.FromOADate(Now.ToOADate - B.ToOADate).ToOADate * 3600 * 24 - Dtuinfo.m_refresh_time - 28800) < 70 Then '�������70����δˢ��ʱ�䣬����Ϊ������
					
					'UPGRADE_ISSUE: Constant vbUnicode was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
                    sPhon = StrConv(System.Text.UnicodeEncoding.Unicode.GetString(Dtuinfo.m_phoneno), VbStrConv.SimplifiedChinese)
					sPhon = Left(Trim(sPhon), 11)
					If sPhon = Trim(PhoneNumber) Then
						userid = CInt(Dtuinfo.m_modemid)
						fsendresult = DSSendData(userid, length, mess)
						SenddataByPhon = IIf(fsendresult = 1, True, False)
						Exit Function
					End If
				Else
				End If
			Next i
		End If
		
	End Function
	
	Function DSgetnextdataAndPhonnumber(ByRef Data As ModemDataStruct, ByRef sPhon As String, ByVal timeoutseconds As Short) As Integer
		Dim i As Integer
		'UPGRADE_WARNING: Arrays in structure Dtuinfo may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		Dim Dtuinfo As ModemInfoStruct
		Dim DtuCount As Integer
		Dim t As Integer
		'Dim sPhon As String
		Dim B As Date
		'Dim UserID As Long
		Dim fsendresult As Integer
		B = #1/1/1970#
		DSgetnextdataAndPhonnumber = IIf(DSGetNextData(Data, 0) = 1, True, False)
		If DSgetnextdataAndPhonnumber <> 0 Then
			DtuCount = DSGetModemCount()
			If DtuCount = 0 Then
				'û������DTU
				
				Exit Function
			Else
				For i = 0 To DtuCount - 1
					
					t = DSGetModemByPosition(i, Dtuinfo)
					If (System.Date.FromOADate(Now.ToOADate - B.ToOADate).ToOADate * 3600 * 24 - Dtuinfo.m_refresh_time - 28800) < 70 Then '�������70����δˢ��ʱ�䣬����Ϊ������
						
						
						
						If Dtuinfo.m_modemid = Data.m_modemid Then
							'UPGRADE_ISSUE: Constant vbUnicode was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
                            sPhon = Left(Trim(StrConv(System.Text.UnicodeEncoding.Unicode.GetString(Dtuinfo.m_phoneno), VbStrConv.SimplifiedChinese)), 11)
							
							'SenddataByPhon = fsendresult
							Exit Function
						End If
						
					End If
				Next i
			End If
		End If
	End Function
	Public Function IfThisDtuonline(ByVal Phone As String, ByRef Waittime As Integer) As Boolean '�ú������û���ҵ���Ӧ��userid,�᷵��û�ҵ���ԭ��ֱ�Ϊ0,1,2
		'UPGRADE_WARNING: Arrays in structure Dtuinfo may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		Dim Dtuinfo As ModemInfoStruct
		Dim DtuCount As Integer
		Dim t As Integer
		Dim sPhon As String
		Dim B As Date
		B = #1/1/1970#
		Dim i As Integer
		DtuCount = DSGetModemCount()
		If DtuCount = 0 Then
			IfThisDtuonline = False 'û������DTU
			
			Exit Function
		Else
			For i = 0 To DtuCount - 1
				
				t = DSGetModemByPosition(i, Dtuinfo)
				If (System.Date.FromOADate(Now.ToOADate - B.ToOADate).ToOADate * 3600 * 24 - Dtuinfo.m_refresh_time - 28800) < Waittime Then '�������70����δˢ��ʱ�䣬����Ϊ������
					
					'UPGRADE_ISSUE: Constant vbUnicode was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
                    sPhon = StrConv(System.Text.UnicodeEncoding.Unicode.GetString(Dtuinfo.m_phoneno), VbStrConv.SimplifiedChinese)
					sPhon = Left(Trim(sPhon), 11)
					If sPhon = Trim(Phone) Then
						IfThisDtuonline = True
						Exit Function
					End If
				Else
					IfThisDtuonline = False '���DTU������
				End If
			Next i
		End If
		IfThisDtuonline = False '����DTU��û���ҵ�������������
		
	End Function
End Module