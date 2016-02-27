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

    '程序中使用的变量
    Public srvport As Short '服务端口
    Public waittime As Short '轮询时间
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

    Public thread As thread '阻塞模式下读取数据线程
    Public threadrun As Boolean '控制线程处理函数
    '最大接收缓冲区
    Const MAX_RECEIVE_BUF As Short = 1024

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)> _
    Public Structure user_info
        '<VBFixedArray(12)> Dim m_userid() As Byte '终端模块号码
        <VBFixedArray(11), MarshalAs(UnmanagedType.ByValArray, SizeConst:=12)> _
        Dim m_userid() As Byte
        '<VBFixedArray(4)> Dim m_sin_addr() As Byte '终端模块进入Internet的代理主机IP地址
        '<VBFixedArray(3), MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)> _
        Dim m_sin_addr As Integer
        Dim m_sin_port As UInt16'终端模块进入Internet的代理主机IP端口        
        '<VBFixedArray(4)> Dim m_local_addr() As Byte '终端模块在移动网内IP地址
        '<VBFixedArray(3), MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)> _
        Dim m_local_addr As Integer
        Dim m_local_port As UInt16 '终端模块在移动网内IP端口
        '<VBFixedArray(20)> Dim m_logon_date() As Byte '终端模块登录时间
        <VBFixedArray(19), MarshalAs(UnmanagedType.ByValArray, SizeConst:=20)> _
        Dim m_logon_date() As Byte
        '<VBFixedArray(20)> Dim m_update_date() As Byte '用户更新时间
        <VBFixedArray(19), MarshalAs(UnmanagedType.ByValArray, SizeConst:=20)> _
        Dim m_update_date() As Byte
        Dim m_status As Byte '终端模块状态, 1 在线 0 不在线

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
        '<VBFixedArray(11)> Dim m_userid() As Byte '终端模块号码       
        <VBFixedArray(11), MarshalAs(UnmanagedType.ByValArray, SizeConst:=12)> _
        Dim m_userid() As Byte
        '<VBFixedArray(19)> Dim m_recv_date() As Byte '接收到数据包的时间        
        <VBFixedArray(19), MarshalAs(UnmanagedType.ByValArray, SizeConst:=20)> _
        Dim m_recv_date() As Byte
        '<VBFixedArray(MAX_RECEIVE_BUF - 1)> Dim m_data_buf() As Byte '存储接收到的数据
        <VBFixedArray(MAX_RECEIVE_BUF - 1), MarshalAs(UnmanagedType.ByValArray, SizeConst:=MAX_RECEIVE_BUF)> _
        Dim m_data_buf() As Byte
        Dim m_data_len As Short '接收到的数据包长度
        Dim m_data_type As Byte '接收到的数据包类型,0x09：用户数据包 0 不认识类型

        Public Sub Initialize()
            ReDim m_userid(11)
            ReDim m_recv_date(19)
            ReDim m_data_buf(MAX_RECEIVE_BUF - 1)
        End Sub
    End Structure

    '服务器启动函数
    'Public Declare Function start_gprs_server Lib "e:\vss\源代码\gprs_comm2.0_v6\debug\wcomm_dll.dll" _
    ''(ByVal hw As Long, ByVal Msg As Long, ByVal serport As Long, _
    ''            mess As Byte, Optional ByVal iPollTime As Integer = 0, Optional ByVal iTimerval As Integer = 30 _
    ''            ) As Long
    Public Declare Function start_gprs_server Lib "wcomm_dll.dll" (ByVal hw As Integer, ByVal Msg As Integer, ByVal serport As Integer, ByRef mess As Byte) As Integer
    Public Declare Function start_net_service Lib "wcomm_dll.dll" (ByVal hw As Integer, ByVal Msg As Integer, ByVal serport As Integer, ByRef mess As Byte) As Integer

    Public Declare Function SGSA Lib "wcomm_dll.dll" Alias "start_gprs_server" (ByVal hw As Integer, ByVal Msg As Integer, ByVal serport As Integer, ByRef mess As Byte) As Integer


    '服务器停止函数
    Public Declare Function stop_gprs_server Lib "wcomm_dll.dll" (ByRef mess As Byte) As Integer
    Public Declare Function stop_net_service Lib "wcomm_dll.dll" (ByRef mess As Byte) As Integer

    '获得用户信息－－－－－－这是get_user_info函数的定义
    Public Declare Function get_user_info Lib "wcomm_dll.dll" (ByRef userid As Byte, ByRef userinfo As user_info) As Integer
    '获得用户信息

    Public Declare Function get_user_at Lib "wcomm_dll.dll" (ByVal Index As Integer, ByRef userinfo As user_info) As Integer
    '向用户表中添加一个用户，该操作同DTU登录等效

    Public Declare Function AddOneUser Lib "wcomm_dll.dll" (ByRef userinfo As user_info) As Integer

    '读数据函数

    Public Declare Function do_read_proc Lib "wcomm_dll.dll" (<MarshalAs(UnmanagedType.Struct)> ByRef datarecord As data_record, ByRef mess As Byte, ByVal answer As Boolean) As Integer

    '发送数据函数
    Public Declare Function do_send_user_data Lib "wcomm_dll.dll" (ByRef userid As Byte, ByRef src As Byte, ByVal srclen As Integer, ByRef mess As Byte) As Integer


    '关闭某个终端
    Public Declare Function do_close_one_user Lib "wcomm_dll.dll" (ByRef userid As Byte, ByRef mess As Byte) As Integer
    Public Declare Function do_close_one_user2 Lib "wcomm_dll.dll" (ByRef userid As Byte, ByRef mess As Byte) As Integer

    '关闭全部终端
    Public Declare Function do_close_all_user Lib "wcomm_dll.dll" (ByRef mess As Byte) As Integer
    Public Declare Function do_close_all_user2 Lib "wcomm_dll.dll" (ByRef mess As Byte) As Integer

    '得到当前在线用户的数量
    Public Declare Function get_max_user_amount Lib "wcomm_dll.dll" () As Integer

    '设置网络事件工作模式
    Public Declare Function SetWorkMode Lib "wcomm_dll.dll" (ByVal nWorkMode As Integer) As Integer
    Public Declare Function SelectProtocol Lib "wcomm_dll.dll" (ByVal nWorkMode As Integer) As Integer
    Public Declare Function SetCustomIP Lib "wcomm_dll.dll" (ByVal lIPAddr As Integer) As Integer

    '''''''''''''''''''''''''''''''''
    '     开发包参数配置函数         '
    '''''''''''''''''''''''''''''''''
    Public Declare Function ClearParam Lib "wcomm_dll.dll" () As Integer
    Public Declare Sub cancel_read_block Lib "wcomm_dll.dll" ()

    Public Declare Function SetParam Lib "wcomm_dll.dll" (ByVal nParamType As Integer, ByRef cpValue As Byte, ByVal nParamLen As Integer, ByRef iErrorCode As Integer) As Integer

    Public Declare Function GetParam Lib "wcomm_dll.dll" (ByVal nParamType As Integer, ByRef cpValue As Byte, ByRef nParamLen As Integer) As Integer


    Public Declare Function do_read_param Lib "wcomm_dll.dll" (ByRef userid As Byte, ByRef mess As Byte) As Integer

    Public Declare Function do_update_param Lib "wcomm_dll.dll" (ByRef userid As Byte, ByRef mess As Byte) As Integer

    'windos API函数，可以查看MSDN
    Public Declare Function CallWindowProc Lib "user32" Alias "CallWindowProcA" (ByVal lpPrevWndFunc As Integer, ByVal hwnd As Integer, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer


    'windos API函数，可以查看MSDN
    Public Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongA" (ByVal hwnd As Integer, ByVal nIndex As Integer) As Integer

    'windos API函数，可以查看MSDN
    'Public Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (ByVal hwnd As Integer, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer
    '把原函数中的dwNewLong数据类型更换了。
    Public Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (ByVal hwnd As Integer, ByVal nIndex As Integer, ByVal lpfn As HOOKPROC) As Integer
    Public Declare Function SetWindowLong1 Lib "user32" Alias "SetWindowLongA" (ByVal hwnd As Integer, ByVal nIndex As Integer, ByVal lpfn As Long) As Integer


    'windows api函数，转换IP地址为字符串    			
    Public Declare Function inet_ntoa Lib "Ws2_32" Alias "inet_ntoa" (ByVal ip As Integer) As String
    Public Declare Function ntohl Lib "Ws2_32" Alias "ntohl" (ByVal ip As Integer) As Integer
    Public Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer

    Public Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer

    'windos API函数，可以查看MSDN
    Public Declare Function GetCurrentDirectory Lib "kernel32" Alias "GetCurrentDirectoryA" (ByVal nBufferLength As Integer, ByVal lpBuffer As VBFixedStringAttribute) As Integer




    'windos API函数，可以查看MSDN
    'Public Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Long, ByVal wMsg As Long, ByVal wParam As Long, lParam As Any) As Long

    Public Function GetAddressOf(ByVal AddressOfProc As Integer) As Integer
        GetAddressOf = AddressOfProc
    End Function



    '############################新加的########################
    Public Delegate Function HOOKPROC(ByVal hw As Integer, ByVal uMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    '############################新加的########################
    Public Function HextoStr(ByRef src() As Byte, ByRef Srclen As Integer, ByRef Start As Integer) As String
        Dim i As Short
        Dim st As String
        Dim temp As Short
        Dim j As Integer
        j = Srclen - 1
        For i = Start To j '看src这个数组是从0开始还是从1开始,宏电的是从1开始
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
