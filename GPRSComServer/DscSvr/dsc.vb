Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
<System.Runtime.InteropServices.ProgId("DSC_NET.DSC")> Public Class DSC
	'保持属性值的局部变量
	Public Enum dsctype
		宏电 = 1
		桑荣 = 2
		'  利士达 = 3
	End Enum
	Public Structure DtuDataStruct
		Dim PhoneNumber As String 'Modem模块的ID号
		<VBFixedArray(1024 + 1)> Dim Databuff() As Byte '存储接收到的数据
		Dim Datalen As Short '接收到的数据包长度
		Dim DataType As Byte '接收到的数据包类型,0x01：用户数据包    0x02：对控制命令帧的回应
		
		'UPGRADE_TODO: "Initialize" must be called to initialize instances of this structure. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B4BFF9E0-8631-45CF-910E-62AB3970F27B"'
		Public Sub Initialize()
			ReDim Databuff(1024 + 1)
		End Sub
	End Structure
	Public Waittime As Integer '达到等待时间仍未刷新时间则认为已下线
	Private mvarDSCtype As dsctype '局部复制
	Private mvarOnlineDtus As OnlineDtus
	
	
	
	
	
	
	
	Public Property onlinedtus() As OnlineDtus
		Get
			If mvarOnlineDtus Is Nothing Then
				mvarOnlineDtus = New OnlineDtus
			End If
			
			
			onlinedtus = mvarOnlineDtus
		End Get
		Set(ByVal Value As OnlineDtus)
			mvarOnlineDtus = Value
		End Set
	End Property
	
	
	
    Public Property SHDsctype() As dsctype
        Get
            '检索属性值时使用，位于赋值语句的右边。
            'Syntax: Debug.Print X.DSCtype
            SHDsctype = mvarDSCtype
        End Get
        Set(ByVal Value As dsctype)
            '向属性指派值时使用，位于赋值语句的左边。
            'Syntax: X.DSCtype = 5
            mvarDSCtype = Value
        End Set
    End Property
	
	
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mvarOnlineDtus may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mvarOnlineDtus = Nothing
		
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	
	
	
	Public Function StopService() As Boolean
		Dim Result As Object
		Dim Result1 As Integer
		Dim Result2 As Integer
		'UPGRADE_WARNING: Lower bound of array mess was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
		Dim mess(1024) As Byte
		
        Select Case Me.SHDsctype

            Case 1

                Result1 = hddll.do_close_all_user(mess(1))




                Result2 = hddll.stop_gprs_server(mess(1))


                If Result1 = 0 And Result2 = 0 Then
                    StopService = True
                Else
                    StopService = False
                End If

            Case 2
                'UPGRADE_WARNING: Couldn't resolve default property of object Result. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                Result = SRdll.DSStopService()
                'UPGRADE_WARNING: Couldn't resolve default property of object Result. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                If Result = 1 Then
                    StopService = True
                Else
                    StopService = False
                End If

        End Select
	End Function
	
	Public Function StartService(ByRef ServerPort As Integer) As Boolean
		Dim Result As Integer
		'UPGRADE_WARNING: Lower bound of array mess was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
		Dim mess(1024) As Byte
		
        Select Case Me.SHDsctype

            Case 1
                Result = hddll.start_gprs_server(frmrefreshONlinedtu.Handle.ToInt32, WM_USER + 103, ServerPort, mess(1))
                If Result = 0 Then
                    frmrefreshONlinedtu.tmrReadheartbeat.Enabled = True
                    frmrefreshONlinedtu.tmrRefreshonlinedtu.Enabled = True
                    StartService = True
                Else
                    StartService = False
                End If

            Case 2
                Result = SRdll.DSStartService(ServerPort)
                If Result = 1 Then
                    StartService = True
                    frmrefreshONlinedtu.tmrRefreshonlinedtu.Enabled = True

                Else
                    StartService = False
                End If

        End Select
		
	End Function
	'UPGRADE_NOTE: command was upgraded to command_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Public Function GetherData(ByRef command_Renamed() As Byte, ByVal PhoneNumber As String, ByVal Timeout As Integer, ByRef receiveData() As Byte, ByRef rvLength As Integer, ByRef Usetime As Double) As Boolean
		Dim Gprsresult As Object
		Dim Start As Object
		Dim i As Integer
		Dim L As Integer
		Dim Result As Integer
		Dim Rvstr As String
		Dim sPhon As String
		Dim mess(1024) As Byte
		L = UBound(command_Renamed)
		Dim ByteLen As Short
		'UPGRADE_WARNING: Arrays in structure srRvdata may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		Dim srRvdata As ModemDataStruct
		'UPGRADE_WARNING: Arrays in structure rvData may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		Dim rvData As data_record
        Select Case Me.SHDsctype
            Case dsctype.宏电


                For i = 0 To L
                    mess(i + 1) = command_Renamed(i)
                Next i
                Result = hddll.SenddataByPhon(PhoneNumber, L + 1, mess(1))

                If Result = 0 Then '发送成功
                    frmrefreshONlinedtu.tmrReadheartbeat.Enabled = False
                End If

                '      Dim Gprsresult As Long
                '--------------------------等待数据返回
                'UPGRADE_WARNING: Couldn't resolve default property of object Start. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                Start = VB.Timer()
                Do
                    'UPGRADE_WARNING: Couldn't resolve default property of object Gprsresult. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    Gprsresult = hddll.DSgetnextdataAndPhonnumber(rvData, sPhon, 0)
                    '                DoEvents
                    'UPGRADE_WARNING: Couldn't resolve default property of object Start. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Gprsresult. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                Loop While Gprsresult <> 0 And VB.Timer() < Start + Timeout
                '---------------------------是否有数据返回
                frmrefreshONlinedtu.tmrReadheartbeat.Enabled = True '先打开清空缓冲区也是为了读心跳包的timer

                'UPGRADE_WARNING: Couldn't resolve default property of object Gprsresult. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                If Gprsresult = 0 And Left(sPhon, 11) = Left(Trim(PhoneNumber), 11) Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object Start. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    Usetime = VB.Timer() - Start
                    If rvData.m_data_len > 0 Then
                        '                     For k = 0 To rvData.m_data_len '因为宏电的返回数组下标从1开始，所以在这里挪了一下
                        '                         Rvstr = Rvstr & Chr(rvData.m_data_buf(k + 1))
                        '                     Next k
                        receiveData = VB6.CopyArray(rvData.m_data_buf)
                        rvLength = rvData.m_data_len
                    End If
                    GetherData = True
                Else
                    GetherData = False
                End If


            Case dsctype.桑荣

                For i = 0 To L
                    mess(i) = command_Renamed(i)
                Next i
                Result = SRdll.SenddataByPhon(PhoneNumber, L + 1, mess(0))

                If Result = 1 Then '发送成功
                    frmrefreshONlinedtu.tmrReadheartbeat.Enabled = False
                End If

                '      Dim Gprsresult As Long
                '--------------------------等待数据返回
                'UPGRADE_WARNING: Couldn't resolve default property of object Start. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                Start = VB.Timer()
                Do
                    'UPGRADE_WARNING: Couldn't resolve default property of object Gprsresult. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    Gprsresult = SRdll.DSgetnextdataAndPhonnumber(srRvdata, sPhon, 0)
                    '                DoEvents
                    'UPGRADE_WARNING: Couldn't resolve default property of object Start. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Gprsresult. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                Loop While Gprsresult <> 1 And VB.Timer() < Start + Timeout
                '---------------------------是否有数据返回
                frmrefreshONlinedtu.tmrReadheartbeat.Enabled = True '先打开清空缓冲区也是为了读心跳包的timer

                'UPGRADE_WARNING: Couldn't resolve default property of object Gprsresult. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                If Gprsresult = 1 And sPhon = PhoneNumber Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object Start. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    Usetime = VB.Timer() - Start
                    If srRvdata.m_data_len > 0 Then
                        '                     For k = 0 To srRvdata.m_data_len
                        '                         Rvstr = Rvstr & Chr(srRvdata.m_data_buf(k + 1))
                        '                     Next k
                        receiveData = VB6.CopyArray(srRvdata.m_data_buf)
                        rvLength = srRvdata.m_data_len

                    End If
                    GetherData = True
                Else
                    GetherData = False
                End If
        End Select
	End Function
	
	Public Function SendbyteData(ByVal PhoneNumber As String, ByVal length As Integer, ByRef mess As Byte) As Boolean
        Select Case Me.SHDsctype
            Case dsctype.宏电


                SendbyteData = hddll.SenddataByPhon(PhoneNumber, length, mess)
            Case dsctype.桑荣

                SendbyteData = SRdll.SenddataByPhon(PhoneNumber, length, mess)
        End Select
	End Function
	Public Function RvbyteData(ByRef DtuData As DtuDataStruct) As Boolean
		Dim i As Integer
		Dim sPhon As String
		'UPGRADE_WARNING: Arrays in structure hddata may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		Dim hddata As data_record
		'UPGRADE_WARNING: Arrays in structure Srdata may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		Dim Srdata As ModemDataStruct
		Dim timeoutseconds As Integer
		timeoutseconds = 0
        Select Case Me.SHDsctype
            Case dsctype.宏电

                RvbyteData = hddll.DSgetnextdataAndPhonnumber(hddata, sPhon, timeoutseconds)
                DtuData.Datalen = hddata.m_data_len
                DtuData.PhoneNumber = sPhon
                DtuData.DataType = hddata.m_data_type
                If DtuData.DataType = 9 Then
                    For i = 0 To DtuData.Datalen
                        DtuData.Databuff(i) = hddata.m_data_buf(i + 1)
                    Next i
                    RvbyteData = True
                Else
                    RvbyteData = False '不是用户数据包不认为接收到数据
                End If




            Case dsctype.桑荣

                RvbyteData = SRdll.DSgetnextdataAndPhonnumber(Srdata, sPhon, timeoutseconds)
                DtuData.Datalen = Srdata.m_data_len
                DtuData.PhoneNumber = sPhon
                DtuData.DataType = Srdata.m_data_type
                If DtuData.DataType = 1 Then
                    For i = 0 To DtuData.Datalen
                        DtuData.Databuff(i) = Srdata.m_data_buf(i)
                    Next i
                    RvbyteData = True
                Else
                    RvbyteData = False '不是用户数据包不认为接收到数据
                End If
        End Select
		
	End Function
	
	'UPGRADE_NOTE: Str was upgraded to Str_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Public Function SendString(ByVal PhoneNumber As String, ByVal length As Integer, ByRef Str_Renamed As String) As Boolean
		Dim i As Integer
		'UPGRADE_WARNING: Lower bound of array mess was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
		Dim mess(1024) As Byte
		For i = 1 To length
			mess(i) = Asc(Mid(Str_Renamed, i, 1))
		Next i
        Select Case Me.SHDsctype
            Case dsctype.宏电
                hddll.SenddataByPhon(PhoneNumber, length, mess(1))
            Case dsctype.桑荣
                SRdll.SenddataByPhon(PhoneNumber, length, mess(1))
        End Select
		
	End Function
	Function ReceiveString(ByVal PhoneNumber As String, ByRef Rvstr As String) As Boolean
		Dim Data As Object
		Dim k As Object
		Dim Usetime As Object
		Dim Start As Object
		Dim i As Integer
		Dim L As Integer
		Dim Result As Integer
		'Dim Rvstr As String
		Dim sPhon As String
		Dim mess(1024) As Byte
		L = Len(VB.Command())
		Dim ByteLen As Short
		Dim Gprsresult As Integer
		'UPGRADE_WARNING: Arrays in structure srRvdata may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		Dim srRvdata As ModemDataStruct
		'UPGRADE_WARNING: Arrays in structure rvData may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		Dim rvData As data_record
        Select Case Me.SHDsctype
            Case dsctype.宏电

                '--------------------------等待数据返回
                'Start = Timer
                'Do
                Gprsresult = hddll.DSgetnextdataAndPhonnumber(rvData, sPhon, 0)
                'DoEvents
                'Loop While Gprsresult <> 0 And Timer < Start + Timeout
                '---------------------------是否有数据返回
                'frmrefreshONlinedtu.tmrReadheartbeat.Enabled = True  '先打开清空缓冲区也是为了读心跳包的timer

                If Gprsresult = 0 And Left(sPhon, 11) = Left(Trim(PhoneNumber), 11) Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object Start. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Usetime. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    Usetime = VB.Timer() - Start
                    If rvData.m_data_len > 0 Then
                        For k = 0 To rvData.m_data_len '因为宏电的返回数组下标从1开始，所以在这里挪了一下
                            'UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                            Rvstr = Rvstr & Chr(rvData.m_data_buf(k + 1))
                        Next k
                        'UPGRADE_WARNING: Couldn't resolve default property of object Data. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        Data = Rvstr
                    End If
                    ReceiveString = True
                Else
                    ReceiveString = False
                End If

            Case dsctype.桑荣

                '      Dim Gprsresult As Long
                '--------------------------等待数据返回
                'Start = Timer
                'Do
                Gprsresult = SRdll.DSgetnextdataAndPhonnumber(srRvdata, sPhon, 0)
                'DoEvents
                'Loop While Gprsresult <> 1 And Timer < Start + Timeout
                '---------------------------是否有数据返回
                'frmrefreshONlinedtu.tmrReadheartbeat.Enabled = True '先打开清空缓冲区也是为了读心跳包的timer

                If Gprsresult = 1 And sPhon = PhoneNumber Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object Start. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Usetime. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    Usetime = VB.Timer() - Start
                    If srRvdata.m_data_len > 0 Then
                        For k = 0 To srRvdata.m_data_len '因为宏电的返回数组下标从1开始，所以在这里挪了一下
                            'UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                            Rvstr = Rvstr & Chr(srRvdata.m_data_buf(k + 1))
                        Next k
                        'UPGRADE_WARNING: Couldn't resolve default property of object Data. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        Data = Rvstr

                    End If
                    ReceiveString = True
                Else
                    ReceiveString = False
                End If
        End Select
	End Function
	'UPGRADE_NOTE: command was upgraded to command_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Public Function AskForDataBycommand(ByVal command_Renamed As String, ByVal PhoneNumber As String, ByVal Timeout As Integer, ByRef Data As String, ByRef Usetime As Double) As Boolean
		Dim k As Object
		Dim Start As Object
		Dim i As Integer
		Dim L As Integer
		Dim Result As Integer
		Dim Rvstr As String
		Dim sPhon As String
		Dim mess(1024) As Byte
		L = Len(command_Renamed)
		Dim ByteLen As Short
		Dim Gprsresult As Integer
		'UPGRADE_WARNING: Arrays in structure srRvdata may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		Dim srRvdata As ModemDataStruct
		'UPGRADE_WARNING: Arrays in structure rvData may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		Dim rvData As data_record
        Select Case Me.SHDsctype
            Case dsctype.宏电


                For i = 1 To L
                    mess(i) = Asc(Mid(command_Renamed, i, 1))
                Next i
                Result = hddll.SenddataByPhon(PhoneNumber, L, mess(1))

                If Result = 0 Then '发送成功
                    frmrefreshONlinedtu.tmrReadheartbeat.Enabled = False
                End If

                '--------------------------等待数据返回
                'UPGRADE_WARNING: Couldn't resolve default property of object Start. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                Start = VB.Timer()
                Do
                    Gprsresult = hddll.DSgetnextdataAndPhonnumber(rvData, sPhon, 0)
                    System.Windows.Forms.Application.DoEvents()
                    'UPGRADE_WARNING: Couldn't resolve default property of object Start. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                Loop While Gprsresult <> 0 And VB.Timer() < Start + Timeout
                '---------------------------是否有数据返回
                frmrefreshONlinedtu.tmrReadheartbeat.Enabled = True '先打开清空缓冲区也是为了读心跳包的timer

                If Gprsresult = 0 And Left(sPhon, 11) = Left(Trim(PhoneNumber), 11) Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object Start. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    Usetime = VB.Timer() - Start
                    If rvData.m_data_len > 0 Then
                        For k = 0 To rvData.m_data_len '因为宏电的返回数组下标从1开始，所以在这里挪了一下
                            'UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                            Rvstr = Rvstr & Chr(rvData.m_data_buf(k + 1))
                        Next k
                        Data = Rvstr
                    End If
                    AskForDataBycommand = True
                Else
                    AskForDataBycommand = False
                End If









            Case dsctype.桑荣

                For i = 0 To L - 1
                    mess(i) = Asc(Mid(command_Renamed, i + 1, 1))
                Next i
                Result = SRdll.SenddataByPhon(PhoneNumber, L, mess(0))

                If Result = 1 Then '发送成功
                    frmrefreshONlinedtu.tmrReadheartbeat.Enabled = False
                End If

                '      Dim Gprsresult As Long
                '--------------------------等待数据返回
                'UPGRADE_WARNING: Couldn't resolve default property of object Start. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                Start = VB.Timer()
                Do
                    Gprsresult = SRdll.DSgetnextdataAndPhonnumber(srRvdata, sPhon, 0)
                    System.Windows.Forms.Application.DoEvents()
                    'UPGRADE_WARNING: Couldn't resolve default property of object Start. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                Loop While Gprsresult <> 1 And VB.Timer() < Start + Timeout
                '---------------------------是否有数据返回
                frmrefreshONlinedtu.tmrReadheartbeat.Enabled = True '先打开清空缓冲区也是为了读心跳包的timer

                If Gprsresult = 1 And sPhon = PhoneNumber Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object Start. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    Usetime = VB.Timer() - Start
                    If srRvdata.m_data_len > 0 Then
                        For k = 0 To srRvdata.m_data_len '因为宏电的返回数组下标从1开始，所以在这里挪了一下
                            'UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                            Rvstr = Rvstr & Chr(srRvdata.m_data_buf(k + 1))
                        Next k
                        Data = Rvstr

                    End If
                    AskForDataBycommand = True
                Else
                    AskForDataBycommand = False
                End If
        End Select
		
		
	End Function
	Sub RefreshonlineDTUTable()
		Dim temp As Object
		Dim i As Object
		Dim tucount As Integer
		Dim B As Date
		Dim p As String
		'UPGRADE_WARNING: Arrays in structure Dtuinfo may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		Dim Dtuinfo As SRdll.ModemInfoStruct
		'UPGRADE_WARNING: Arrays in structure tmodeminfo may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		Dim tmodeminfo As user_info
        Select Case Me.SHDsctype
            Case dsctype.宏电

                tucount = hddll.get_max_user_amount()

                For i = 0 To tucount - 1 '永远自动刷
                    'UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object temp. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    temp = get_user_at(i, tmodeminfo)
                    'UPGRADE_ISSUE: Constant vbUnicode was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
                    p = Left(Trim(StrConv(System.Text.UnicodeEncoding.Unicode.GetString(tmodeminfo.m_userid),VbStrConv.SimplifiedChinese)), 11) '宏电的userid设为与手机号相同

                    If hddll.IfThisDtuonline(p, (Me.Waittime)) Then '如果超过120秒仍未刷新时间，则认为已下线
                        On Error Resume Next
                        'UPGRADE_ISSUE: Constant vbUnicode was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
                        SHDscs("宏电").onlinedtus.Add(p, CDate(StrConv(System.Text.UnicodeEncoding.Unicode.GetString(tmodeminfo.m_logon_date), VbStrConv.SimplifiedChinese)), p)

                    Else
                        SHDscs("宏电").onlinedtus.Remove(p)
                        On Error GoTo 0
                    End If
                Next i
            Case dsctype.桑荣
                '      Dim i As Long
                '    Dim temp As Long
                '    Dim tucount As Long
                '    Dim b As Date
                '    Me.ListView1.ListItems.Clear
                tucount = SRdll.DSGetModemCount()
                If tucount < 1 Then
                    Exit Sub
                End If
                B = #1/1/1970#
                For i = 0 To tucount - 1 '永远自动刷
                    'UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object temp. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    temp = DSGetModemByPosition(i, Dtuinfo)

                    '                   Dim p As String
                    'UPGRADE_ISSUE: Constant vbUnicode was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
                    p = Left(Trim(StrConv(System.Text.UnicodeEncoding.Unicode.GetString(Dtuinfo.m_phoneno), VbStrConv.SimplifiedChinese)), 11)

                    If (System.DateTime.FromOADate(Now.ToOADate - B.ToOADate).ToOADate * 3600 * 24 - Dtuinfo.m_refresh_time - 28800) < Me.Waittime Then '如果超过70秒仍未刷新时间，则认为已下线
                        On Error Resume Next
                        SHDscs("桑荣").onlinedtus.Add(p, DateAdd(Microsoft.VisualBasic.DateInterval.Second, CInt(Dtuinfo.m_conn_time), CDate("1970-1-1 8:00:00")), p)
                        SHDscs("桑荣").onlinedtus(p).DTUid = Dtuinfo.m_modemid
                    Else
                        SHDscs("桑荣").onlinedtus.Remove(p)
                        On Error GoTo 0
                    End If
                Next i

        End Select
	End Sub
End Class