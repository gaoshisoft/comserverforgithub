Option Strict Off
Option Explicit On 
Imports System.Runtime.InteropServices
Imports System.Threading
Module Module1

    ' *** Den Artikel zu diesem Modul finden Sie unter http://www.aboutvb.de/khw/artikel/khwaddressof.htm ***


    Private Const GWL_WNDPROC As Short = -4
    Private Const GWL_USERDATA As Short = (-21)
    Private Const WM_SIZE As Short = &H5S
    Public Const WM_USER As Short = &H400S

    '������ʹ�õı���
    Public srvport As Short '����˿�
    Public waittime As Short '��ѯʱ��
    Public redcolor As Short
    Public greencolor As Short
    Public bluecolor As Short
    Public colorflag As Short
    Public LineCount As Short
    Public SysAutoM As Short

    Public serv_ip As String
    Public connect_time As Integer
    Public refresh_time As Integer
    Public serv_port As Integer
    Public serv_type As Integer
    Public serv_mode As Integer
    Public serv_start As Integer

    Public config As Object
    Public setting As Object

    Public oldwindow As Long

    Public thread As thread '����ģʽ�¶�ȡ�����߳�
    Public threadrun As Boolean '�����̴߳�����
    '�����ջ�����
    Const MAX_RECEIVE_BUF As Short = 1024

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)> _
    Public Structure user_info
        '<VBFixedArray(12)> Dim m_userid() As Byte '�ն�ģ�����
        <VBFixedArray(11), MarshalAs(UnmanagedType.ByValArray, SizeConst:=12)> _
        Dim m_userid() As Byte
        '<VBFixedArray(4)> Dim m_sin_addr() As Byte '�ն�ģ�����Internet�Ĵ�������IP��ַ
        '<VBFixedArray(3), MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)> _
        Dim m_sin_addr As Integer
        Dim m_sin_port As UInt16'�ն�ģ�����Internet�Ĵ�������IP�˿�        
        '<VBFixedArray(4)> Dim m_local_addr() As Byte '�ն�ģ�����ƶ�����IP��ַ
        '<VBFixedArray(3), MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)> _
        Dim m_local_addr As Integer
        Dim m_local_port As UInt16 '�ն�ģ�����ƶ�����IP�˿�
        '<VBFixedArray(20)> Dim m_logon_date() As Byte '�ն�ģ���¼ʱ��
        <VBFixedArray(19), MarshalAs(UnmanagedType.ByValArray, SizeConst:=20)> _
        Dim m_logon_date() As Byte
        '<VBFixedArray(20)> Dim m_update_date() As Byte '�û�����ʱ��
        <VBFixedArray(19), MarshalAs(UnmanagedType.ByValArray, SizeConst:=20)> _
        Dim m_update_date() As Byte
        Dim m_status As Byte '�ն�ģ��״̬, 1 ���� 0 ������

        Public Sub Initialize()
            ReDim m_userid(11)
            'ReDim m_sin_addr(3)
            'ReDim m_local_addr(3)
            ReDim m_logon_date(19)
            ReDim m_update_date(19)
        End Sub
    End Structure


    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)> _
    Public Structure data_record
        '<VBFixedArray(11)> Dim m_userid() As Byte '�ն�ģ�����       
        <VBFixedArray(11), MarshalAs(UnmanagedType.ByValArray, SizeConst:=12)> _
        Dim m_userid() As Byte
        '<VBFixedArray(19)> Dim m_recv_date() As Byte '���յ����ݰ���ʱ��        
        <VBFixedArray(19), MarshalAs(UnmanagedType.ByValArray, SizeConst:=20)> _
        Dim m_recv_date() As Byte
        '<VBFixedArray(MAX_RECEIVE_BUF - 1)> Dim m_data_buf() As Byte '�洢���յ�������
        <VBFixedArray(MAX_RECEIVE_BUF - 1), MarshalAs(UnmanagedType.ByValArray, SizeConst:=MAX_RECEIVE_BUF)> _
        Dim m_data_buf() As Byte
        Dim m_data_len As Short '���յ������ݰ�����
        Dim m_data_type As Byte '���յ������ݰ�����,0x09���û����ݰ� 0 ����ʶ����

        Public Sub Initialize()
            ReDim m_userid(11)
            ReDim m_recv_date(19)
            ReDim m_data_buf(MAX_RECEIVE_BUF - 1)
        End Sub
    End Structure

    '��������������
    'Public Declare Function start_gprs_server Lib "e:\vss\Դ����\gprs_comm2.0_v6\debug\wcomm_dll.dll" _
    ''(ByVal hw As Long, ByVal Msg As Long, ByVal serport As Long, _
    ''            mess As Byte, Optional ByVal iPollTime As Integer = 0, Optional ByVal iTimerval As Integer = 30 _
    ''            ) As Long
    Public Declare Function start_gprs_server Lib "wcomm_dll.dll" (ByVal hw As Integer, ByVal Msg As Integer, ByVal serport As Integer, ByRef mess As Byte) As Integer
    Public Declare Function start_net_service Lib "wcomm_dll.dll" (ByVal hw As Integer, ByVal Msg As Integer, ByVal serport As Integer, ByRef mess As Byte) As Integer

    Public Declare Function SGSA Lib "wcomm_dll.dll" Alias "start_gprs_server" (ByVal hw As Integer, ByVal Msg As Integer, ByVal serport As Integer, ByRef mess As Byte) As Integer


    '������ֹͣ����
    Public Declare Function stop_gprs_server Lib "wcomm_dll.dll" (ByRef mess As Byte) As Integer
    Public Declare Function stop_net_service Lib "wcomm_dll.dll" (ByRef mess As Byte) As Integer

    '����û���Ϣ����������������get_user_info�����Ķ���
    Public Declare Function get_user_info Lib "wcomm_dll.dll" (ByRef userid As Byte, ByRef userinfo As user_info) As Integer
    '����û���Ϣ

    Public Declare Function get_user_at Lib "wcomm_dll.dll" (ByVal Index As Integer, ByRef userinfo As user_info) As Integer
    '���û��������һ���û����ò���ͬDTU��¼��Ч

    Public Declare Function AddOneUser Lib "wcomm_dll.dll" (ByRef userinfo As user_info) As Integer

    '�����ݺ���

    Public Declare Function do_read_proc Lib "wcomm_dll.dll" (<MarshalAs(UnmanagedType.Struct)> ByRef datarecord As data_record, ByRef mess As Byte, ByVal answer As Boolean) As Integer

    '�������ݺ���
    Public Declare Function do_send_user_data Lib "wcomm_dll.dll" (ByRef userid As Byte, ByRef src As Byte, ByVal srclen As Integer, ByRef mess As Byte) As Integer


    '�ر�ĳ���ն�
    Public Declare Function do_close_one_user Lib "wcomm_dll.dll" (ByRef userid As Byte, ByRef mess As Byte) As Integer
    Public Declare Function do_close_one_user2 Lib "wcomm_dll.dll" (ByRef userid As Byte, ByRef mess As Byte) As Integer

    '�ر�ȫ���ն�
    Public Declare Function do_close_all_user Lib "wcomm_dll.dll" (ByRef mess As Byte) As Integer
    Public Declare Function do_close_all_user2 Lib "wcomm_dll.dll" (ByRef mess As Byte) As Integer

    '�õ���ǰ�����û�������
    Public Declare Function get_max_user_amount Lib "wcomm_dll.dll" () As Integer

    '���������¼�����ģʽ
    Public Declare Function SetWorkMode Lib "wcomm_dll.dll" (ByVal nWorkMode As Integer) As Integer
    Public Declare Function SelectProtocol Lib "wcomm_dll.dll" (ByVal nWorkMode As Integer) As Integer
    Public Declare Function SetCustomIP Lib "wcomm_dll.dll" (ByVal lIPAddr As Integer) As Integer

    '''''''''''''''''''''''''''''''''
    '     �������������ú���         '
    '''''''''''''''''''''''''''''''''
    Public Declare Function ClearParam Lib "wcomm_dll.dll" () As Integer
    Public Declare Sub cancel_read_block Lib "wcomm_dll.dll" ()

    Public Declare Function SetParam Lib "wcomm_dll.dll" (ByVal nParamType As Integer, ByRef cpValue As Byte, ByVal nParamLen As Integer, ByRef iErrorCode As Integer) As Integer

    Public Declare Function GetParam Lib "wcomm_dll.dll" (ByVal nParamType As Integer, ByRef cpValue As Byte, ByRef nParamLen As Integer) As Integer


    Public Declare Function do_read_param Lib "wcomm_dll.dll" (ByRef userid As Byte, ByRef mess As Byte) As Integer

    Public Declare Function do_update_param Lib "wcomm_dll.dll" (ByRef userid As Byte, ByRef mess As Byte) As Integer

    'windos API���������Բ鿴MSDN
    Public Declare Function CallWindowProc Lib "user32" Alias "CallWindowProcA" (ByVal lpPrevWndFunc As Integer, ByVal hwnd As Integer, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer


    'windos API���������Բ鿴MSDN
    Public Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongA" (ByVal hwnd As Integer, ByVal nIndex As Integer) As Integer

    'windos API���������Բ鿴MSDN
    'Public Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (ByVal hwnd As Integer, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer
    '��ԭ�����е�dwNewLong�������͸����ˡ�
    Public Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (ByVal hwnd As Integer, ByVal nIndex As Integer, ByVal lpfn As HOOKPROC) As Integer
    Public Declare Function SetWindowLong1 Lib "user32" Alias "SetWindowLongA" (ByVal hwnd As Integer, ByVal nIndex As Integer, ByVal lpfn As Long) As Integer


    'windows api������ת��IP��ַΪ�ַ���    			
    Public Declare Function inet_ntoa Lib "Ws2_32" Alias "inet_ntoa" (ByVal ip As Integer) As String
    Public Declare Function ntohl Lib "Ws2_32" Alias "ntohl" (ByVal ip As Integer) As Integer
    Public Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer

    Public Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer

    'windos API���������Բ鿴MSDN
    Public Declare Function GetCurrentDirectory Lib "kernel32" Alias "GetCurrentDirectoryA" (ByVal nBufferLength As Integer, ByVal lpBuffer As VBFixedStringAttribute) As Integer




    'windos API���������Բ鿴MSDN
    'Public Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Long, ByVal wMsg As Long, ByVal wParam As Long, lParam As Any) As Long

    Public Function GetAddressOf(ByVal AddressOfProc As Integer) As Integer
        GetAddressOf = AddressOfProc
    End Function



    '############################�¼ӵ�########################
    Public Delegate Function HOOKPROC(ByVal hw As Integer, ByVal uMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    '############################�¼ӵ�########################
    Public Function HextoStr(ByRef src() As Byte, ByRef Srclen As Integer, ByRef Start As Integer) As String
        Dim i As Short
        Dim st As String
        Dim temp As Short
        Dim j As Integer
        j = Srclen - 1
        For i = Start To j '��src��������Ǵ�0��ʼ���Ǵ�1��ʼ,�����Ǵ�1��ʼ
            temp = src(i) \ 16
            If temp > 9 Then
                temp = temp + 55
            Else
                temp = temp + 48
            End If
            st = st & Chr(temp)

            temp = src(i) Mod 16
            If temp > 9 Then
                temp = temp + 55
            Else
                temp = temp + 48
            End If
            st = st & Chr(temp)

            'st = st & " "
        Next
        HextoStr = st
    End Function
  
End Module
