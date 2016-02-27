Option Strict Off
Option Explicit On

Imports MBsrv

Namespace GprsCom
    'Imports MBsrv.Device

    Friend Class GPRSRTU
        Implements IGPRSRTU
        Public mComSuccesstimes As Double
        Public mComfaileTimes As Double
        Public SendoutByteQty As Double
        Public ReceiveByteQty As Double
        Private mPollEnable As Boolean
        Public ifportionPoll As Boolean
        Private mvarpolltime As Integer
        Private mvarIfonline As Integer
        '保持属性值的局部变量
        Private mvarComminfo As String '局部复制
        Private mvarRTuName As String '局部复制
        '保持属性值的局部变量
        Private mvarEnable As Boolean '局部复制
        Private mvarDeviceAD As Short
        '保持属性值的局部变量
        Private mvarDTblock As DataBlocks '局部复制
        '保持属性值的局部变量
        'Private mvarNextcommandShouldbeWrite As Boolean '局部复制
        Private mvarMBadressQuantity As Integer '局部复制
        '保持属性值的局部变量
        Private mvarBaseAd As Integer '局部复制
        '保持属性值的局部变量
        Private mvarCodeName As Integer '局部复制
        Dim f As New iffirst
        Private MdtID As Integer
        Public WithEvents Dsc As DataServerCenter
        Public Comstate As Boolean '表这个RTU的数据收发状态，真表示已发送但还未收到数据也没有超时也就是正在等待数据的返回，假表示没有发送或已超时
        Public WithEvents Mytmr As Timer = New Timer With {.Interval = 100, .Enabled = True}
        Public Timeout As Double
        Dim _sendtime As Double
        Dim firstOnline As iffirst = New iffirst
        Public UpLoadRecordBuff As Microsoft.VisualBasic.Collection
        Public BuffNum As Integer
        Public Property Comminfo2 As String

        'Public DataMove As New DataMove





        Public Property CodeName() As Integer

            Get


                'Syntax: Debug.Print X.CodeName
                '     if mvarcoden
                'me.
                CodeName = mvarCodeName
            End Get
            Set(ByVal Value As Integer)

                'Syntax: X.CodeName = 5
                mvarCodeName = Value
            End Set
        End Property





        Public Property BaseAD() As Integer
            Get

                'Syntax: Debug.Print X.BaseAd
                BaseAD = mvarBaseAd
            End Get
            Set(ByVal Value As Integer)

                'Syntax: X.BaseAd = 5
                mvarBaseAd = Value
                Me.DTblocks.RtuBaseAD = mvarBaseAd
                Me.DTblocks.CalcuDataBlockaddr()
            End Set
        End Property



        Public Property IfOnline() As Boolean Implements IGPRSRTU.IfOnline

            Get

                IfOnline = mvarIfonline
            End Get

            Set(ByVal Value As Boolean)

                'Syntax: X.Enable = 5
                Dim statechange As Boolean

                If Value <> mvarIfonline Then
                    statechange = True
                    SaveconnstatetoDB((Me.RtuName), IIf(Value, "上线", "下线"))
                Else
                    statechange = False
                End If

                mvarIfonline = Value







            End Set
        End Property
        Public Property polltime() As Integer Implements IGPRSRTU.PollTime
            Get

                'Syntax: Debug.Print X.Enable
                If mvarpolltime = 0 Then
                    mvarpolltime = 0
                End If
                polltime = mvarpolltime
            End Get
            Set(ByVal Value As Integer)

                'Syntax: X.Enable = 5

                mvarpolltime = Value
            End Set
        End Property



        Public Property MBadressQuantity() As Integer
            Get

                'Syntax: Debug.Print X.MBadressQuantity
                MBadressQuantity = mvarMBadressQuantity
                If mvarMBadressQuantity < 5 Then
                    MBadressQuantity = 5
                End If
            End Get
            Set(ByVal Value As Integer)

                'Syntax: X.MBadressQuantity = MBadressQuantity
                mvarMBadressQuantity = Value

            End Set
        End Property




        Public Property DTblocks() As DataBlocks
            Get

                'Syntax: Debug.Print X.DTblock
                DTblocks = mvarDTblock
            End Get
            Set(ByVal Value As DataBlocks)
                '向属性指派对象时使用，位于 Set 语句的左边。
                'Syntax: Set x.DTblock = Form1
                mvarDTblock = Value
            End Set
        End Property





        Public Property Enable() As Boolean
            Get

                'Syntax: Debug.Print X.Enable
                Enable = mvarEnable
            End Get
            Set(ByVal Value As Boolean)

                'Syntax: X.Enable = 5
                mvarEnable = Value
            End Set
        End Property


        Public Property DeviceAD() As Short
            Get
                DeviceAD = mvarDeviceAD
            End Get
            Set(ByVal Value As Short)

                mvarDeviceAD = Value
            End Set
        End Property




        Public Property RtuName() As String Implements IGPRSRTU.RtuName
            Get

                'Syntax: Debug.Print X.RtuName
                RtuName = mvarRTuName
            End Get
            Set(ByVal Value As String)

                'Syntax: X.RtuName = 5
                mvarRTuName = Value
            End Set
        End Property





        Public Property CommInfo() As String Implements IGPRSRTU.CommInfo
            Get

                'Syntax: Debug.Print X.commInfo
                CommInfo = mvarComminfo
            End Get
            Set(ByVal Value As String)

                'Syntax: X.commInfo = 5
                mvarComminfo = Value
            End Set
        End Property
        Public Property CurrentDatablockID() As Integer
            Get
                If MdtID < 1 Then
                    MdtID = 1
                End If
                If MdtID > Me.DTblocks.Count Then
                    MdtID = Me.DTblocks.Count
                End If

                CurrentDatablockID = MdtID
            End Get
            Set(ByVal Value As Integer)
                If Value < 1 Then
                    Value = 1
                End If
                If Value > Me.DTblocks.Count Then
                    Value = 1
                    Me.PollEnable = False '已巡测完最后一个数据块结束此次通讯
                End If

                MdtID = Value

            End Set
        End Property



        ReadOnly Property MbadByteQty() As Integer
            Get
                MbadByteQty = Me.MBadressQuantity * 2
            End Get
        End Property

        Function GetWriteCommandByte(ByVal MBAD As String, ByRef Value As Object) As Object
            Dim i As Object
            Dim Mbcd() As Byte
            Dim tMbcd() As Byte
            Dim svrAd As Integer
            Dim start As Integer
            Dim SlaveAd As Integer
            Dim dt As IDataBlock
            svrAd = Val(Right(MBAD, 5))
            For Each dt In Me.DTblocks
                If Left(dt.startAd, 1) = Left(MBAD, 1) Then
                    If svrAd >= dt.SvrMBADStart And svrAd <= dt.SvrMBADStart + dt.SvrAddrLength Then
                        SlaveAd = dt.Addr
                        Exit For
                    End If
                End If
            Next dt


            Select Case Left(MBAD, 1)
                Case CStr(0)

                    start = Val(Right(MBAD, 5)) - 1

                    ReDim Mbcd(7)
                    ReDim tMbcd(UBound(Mbcd) - 2)
                    tMbcd(0) = SlaveAd

                    tMbcd(1) = 5
                    tMbcd(2) = start \ 256
                    tMbcd(3) = start Mod 256
                    tMbcd(4) = IIf(Value = 1, 255, 0)

                    tMbcd(5) = 0
                    For i = 0 To UBound(tMbcd)
                        Mbcd(i) = tMbcd(i)
                    Next i
                    Mbcd(i) = CRC16(tMbcd)(1)
                    Mbcd(i + 1) = CRC16(tMbcd)(0)
                Case CStr(4)
                    start = Val(Right(MBAD, 5)) - 1

                    ReDim Mbcd(7)
                    ReDim tMbcd(UBound(Mbcd) - 2)
                    tMbcd(0) = SlaveAd

                    tMbcd(1) = 6
                    tMbcd(2) = start \ 256
                    tMbcd(3) = start Mod 256
                    tMbcd(4) = Value \ 256

                    tMbcd(5) = Value Mod 256
                    For i = 0 To UBound(tMbcd)
                        Mbcd(i) = tMbcd(i)
                    Next i
                    Mbcd(i) = CRC16(tMbcd)(1)
                    Mbcd(i + 1) = CRC16(tMbcd)(0)
            End Select
            GetWriteCommandByte = mbcd
        End Function
        Sub ReceiveCommandReturn(ByRef commandReturn() As Byte)
            Dim mb() As Byte
            mb = commandReturn
            Dim MBAD As String
            Dim v As UInt16
            Select Case mb(1)
                Case 5
                    MBAD = Format(mb(2) * 256 + mb(3) + 1, "000000")
                    v = IIf(mb(4) = 255, 1, 0)
                    Mbs.WritevaluebyAd((Me.DeviceAD), MBAD, Device.Datatype.无符号整型, v)
                Case 6
                    MBAD = "4" & Format(mb(2) * 256 + mb(3) + 1, "00000")
                    v = mb(4) * 256 + mb(5)
                    Mbs.WritevaluebyAd((Me.DeviceAD), MBAD, Device.Datatype.无符号整型, v)
            End Select

        End Sub

        Private Sub Class_Initialize_Renamed()
            mvarDTblock = New DataBlocks
            mvarIfonline = False
            Dsc = Mydsc


        End Sub
        Public Sub New()
            MyBase.New()
            Class_Initialize_Renamed()
            UpLoadRecordBuff = New Collection
            'Mytmr.AutoReset = True
        End Sub

       
        Protected Overrides Sub Finalize()
            Mytmr.Enabled = False
            Mytmr = Nothing
            mvarDTblock = Nothing
            MyBase.Finalize()
        End Sub
        Function ExistDataBlocks() As Boolean
            On Error GoTo errhand
            If mvarDTblock.Count <> 0 Then
                ExistDataBlocks = True
            Else
errhand:
                ExistDataBlocks = False
            End If
        End Function
        Function ExceptionResponse(ByRef mbdata() As Byte, ByRef exceptioncode As Byte) As Object
            Dim L As Integer
            Dim i As Integer
            Dim Mbresponse() As Byte
            exceptioncode = 1
            ReDim Mbresponse(7 + 3 - 1)
            For i = 0 To 6
                Mbresponse(i) = mbdata(i)
            Next i
            '后面的字节数
            L = 1 + 1 + 1
            Mbresponse(4) = L \ 256
            Mbresponse(5) = L Mod 256
            Mbresponse(7) = mbdata(7) + &H80 '出现错误，功能码要加十六进制80
            Mbresponse(8) = exceptioncode
            ExceptionResponse = Mbresponse
        End Function

        Sub AddToBuffer(ByVal V As Byte(), ByVal L As Integer)
            Dim Rd() As Byte
            ReDim Rd(L - 1)
            Buffer.BlockCopy(V, 0, Rd, 0, L)

            Me.UpLoadRecordBuff.Add(Rd)
            If BuffNum < 2 Then BuffNum = 2

            If Me.UpLoadRecordBuff.Count > BuffNum Then
                Me.UpLoadRecordBuff.Remove(1)

            End If
        End Sub
        Private Sub Dsc_DataReturn(ByVal comminfo As String, ByVal Value As Byte(), ByVal length As Integer) Handles Dsc.DataReturn
            'Dim ByteLen As Short
            'Dim RtuName As String
            'Dim r As GPRSRTU
            Dim dt As IDataBlock
            Dim mValue() As Byte
            Dim find As Boolean
            mValue = Value

            Try


                If length > 0 Then
                    If Me.CommInfo.Contains(comminfo) Then
                        '先显示
                        If Fview.Visible = True Then
                            If Fview.chkGprscomdisplay.CheckState = 1 Then

                                Fview.Text2.Text = HextoStr(mValue, length, 0) & Me.RtuName & "-" & comminfo
                                Fview.txtRtuname.Text = "(" & Now & ")"
                                If Not Me.polltime = 0 Then
                                    Fview.txtRvtime.Text = Format(DateDiff(DateInterval.Second, #1/1/2000#, Today) + Microsoft.VisualBasic.Timer - _sendtime, "0.00")
                                End If
                            End If
                        End If
                        '记录
                        AddToBuffer(mValue, length)

                        If Me.polltime = 0 Then '为主动上传模式
                            'For i As Int16 = 1 To Me.DTblocks.Count
                            'If mValue(0) = Me.DTblocks(i).Addr Then '根据从设备地址来相对应
                            dt = Me.DTblocks(1) '我们的主动上传和蓝迪的现在不支持多数据块，也就是一个站点只能有一个数据块
                            'End If
                            'Next
                        Else
                            dt = Me.DTblocks((Me.CurrentDatablockID)) '确定当前数据块
                        End If

                        find = dt.GetValueFromRvData(length, mValue)


                        If find = True Then '是轮询数据正常接收
                            If Me.polltime = 0 Then '是主动上传模式要回应时间给DTU
                                Dim C() As Byte

                                Dim s As String
                                s = TypeName(Me.DTblocks(1))
                                Select Case TypeName(Me.DTblocks(1))
                                    Case "LDDataBlock"  ' 蓝迪回应
                                        C = dt.GetCommandBytes
                                        Dsc.SenddataByPhon(Me.CommInfo, C.Length, C)
                                    Case "MRDataBlock"
                                        'Dim S As String
                                        'S = "SendOK" & vbCrLf
                                        'C = System.Text.ASCIIEncoding.ASCII.GetBytes(S)

                                        'Dsc.SenddataByPhon(Me.commInfo, C.Length, C)
                                End Select

                            End If
                            '显示给用户
                            If Fview.Visible = True Then
                                If Fview.chkGprscomdisplay.CheckState = 1 Then

                                    Fview.Text2.Text = HextoStr(mValue, length, 0) & Me.RtuName & "-" & comminfo & " " & dt.BlockName & " 通信成功"
                                    Fview.txtRtuname.Text = Me.RtuName & "(" & Now & ")"
                                    'UPGRADE_WARNING: DateDiff 行为可能不同。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6B38EC3F-686D-4B2E-B5A5-9E8E7A762E32"”
                                    If Not Me.polltime = 0 Then
                                        Fview.txtRvtime.Text = Format(DateDiff(DateInterval.Second, #1/1/2000#, Today) + Microsoft.VisualBasic.Timer - _sendtime, "0.00")
                                    End If
                                End If
                            End If

                            '记数
                            Me.ReceiveByteQty = Me.ReceiveByteQty + length
                            '              Me.DataMove.DataMove
                            Me.mComSuccesstimes = Me.mComSuccesstimes + 1
                            Me.Comstate = False '表示一次通讯完成,复位通讯状态
                            Me.CurrentDatablockID = Me.CurrentDatablockID + 1
                        Else '是控制命令的返回,赋值相应寄存器,同时显示,
                            Me.ReceiveCommandReturn(mValue)

                            If Fview.Visible = True Then
                                If Fview.chkGprscomdisplay.CheckState = 1 Then

                                    Fview.Text2.Text = HextoStr(mValue, length, 0) & "  " & Me.RtuName '& "控制命令已被执行！"
                                    Fview.txtRtuname.Text = Me.RtuName & "(" & Now & ")"
                                End If
                            End If

                            '记数
                            Me.ReceiveByteQty = Me.ReceiveByteQty + length

                            Me.mComSuccesstimes = Me.mComSuccesstimes + 1
                        End If



                    End If
                End If
            Catch ex As Exception
            End Try

        End Sub

        Private Sub MytimerSend()

            Dim Rv(1023) As Byte

            Dim dt As IDataBlock

            Dim Fsendresult As Boolean

            Dim Dbs As DataBlocks
            Dim Dtid As Integer

            Dbs = Me.DTblocks
            If Dbs Is Nothing Then '这个站没有定义数据块
                Exit Sub
            End If
            If Dbs.Count < 1 Then
                Exit Sub
            End If

            Try

                Dim Mbcd() As Byte
                If Me.Enable = True Then '在设置窗体中可设定，决定这个站是巡测还是不巡测,这个属性是给用户使用的，用户设定部分巡测时也是用这个属性。
                    If Me.polltime = 0 Then '为主动上传模式，第一次上线要给回应，以通告RTU端可以进行通信了,改为第一次就用时间确认。
                        If Me.IfOnline Then
                            If Me.firstOnline.FirstTime Then
                                Dim B() As Byte = Me.DTblocks.Item(1).GetCommandBytes
                                Mydsc.SenddataByPhon(Me.CommInfo, B.Length, B)
                            End If
                        Else
                            Me.firstOnline.resetToOrigin()
                        End If

                    End If
                    If Me.PollEnable = True Then '在单点巡检或巡测周期中决定，如在巡测周期时间到时设为真或要单点巡检时设为真,而在本站所有块巡完后自动设为假，这个属性是为了系统内部使用的。
                        If Me.IfOnline Then


                            Dtid = Me.CurrentDatablockID

                            dt = Me.DTblocks(Dtid)
                            If dt.Enable = True Then 'Enable为true的数据块则轮询
                                If Me.Comstate = False Then '表此时可发送数据

                                    Mbcd = dt.GetCommandBytes
                                    'Try
                                    Fsendresult = Dsc.SenddataByPhon(Me.CommInfo, UBound(Mbcd) + 1, Mbcd)
                                    'Catch ex As Exception
                                    '    MsgBox(ex.Message)
                                    'End Try

                                    If Fsendresult = True Then '发送成功
                                        '记时
                                        'Sendtime = DateDiff(Microsoft.VisualBasic.DateInterval.Second, #1/1/2010#, Today) + Microsoft.VisualBasic.Timer()
                                        _sendtime = DateDiff(DateInterval.Second, #1/1/2000#, Today) + Microsoft.VisualBasic.Timer
                                        '显示
                                        If Fview.Visible = True Then
                                            If Fview.chkGprscomdisplay.CheckState = 1 Then
                                                Fview.Text1.Text = HextoStr(Mbcd, UBound(Mbcd) + 1, 0)
                                                Fview.txtRtuname.Text = Me.RtuName
                                            End If
                                        End If
                                        '记数
                                        Me.SendoutByteQty = Me.SendoutByteQty + UBound(Mbcd)
                                        Me.Comstate = True '表示正等待数据的返回
                                    End If
                                    '\\\\\\\\\\\\\\\\\\\\\\\\\\\\\发送完毕


                                End If
                                If DateDiff(DateInterval.Second, #1/1/2000#, Today) + Microsoft.VisualBasic.Timer - _sendtime > Me.Timeout Then '内部监视已超时,强制此次通讯结束，并将通讯状态复位
                                    '显示给用户
                                    If Fview.Visible = True Then
                                        If Fview.chkGprscomdisplay.CheckState = 1 Then

                                            Fview.Text2.Text = Me.RtuName & "块" & Me.CurrentDatablockID & "通讯超时"
                                            Fview.txtRtuname.Text = Me.RtuName & "(" & Now & ")"
                                            Fview.txtRvtime.Text = Format(DateDiff(DateInterval.Second, #1/1/2000#, Today) + Microsoft.VisualBasic.Timer - _sendtime, "0.00")
                                        End If
                                    End If
                                    Me.mComfaileTimes = Me.mComfaileTimes + 1
                                    Me.CurrentDatablockID = Me.CurrentDatablockID + 1
                                    Me.Comstate = False

                                End If
                            Else '如果块的enable属性为false 那么进行下一个
                                Me.CurrentDatablockID = Me.CurrentDatablockID + 1
                            End If

                        End If
                    End If
                End If
            Catch ex As Exception
            End Try
        End Sub
        Function MBWrite(ByVal MBAD As String, ByRef Value As Object) As Boolean '向下写入RTU的命令
            Dim Mbcd() As Byte
            Dim Fsendresult As Boolean
            Try
                If Me.CommInfo.Contains(":") Then
                    Mbcd = Me.GetMBTCPWriteCommandByte(MBAD, 1, Value)
                Else
                    Mbcd = Me.GetWriteCommandByte(MBAD, Value)
                End If

                '                       If Me.Comstate = False Then '表此时可发送数据
                If Mydsc.IfThisChannelUseable((Me.CommInfo)) Then
                    Fsendresult = Dsc.SenddataByPhon(Me.CommInfo, UBound(Mbcd) + 1, Mbcd)

                    If Fsendresult = True Then '发送成功
                        '记时
                        ''Sendtime = DateDiff("s", #1/1/1970#, Date) + Timer
                        '显示
                        If Fview.Visible = True Then
                            If Fview.chkGprscomdisplay.CheckState = 1 Then
                                Fview.Text1.Text = HextoStr(Mbcd, UBound(Mbcd) + 1, 0)
                                Fview.txtRtuname.Text = Me.RtuName
                            End If
                        End If
                        '记数
                        Me.SendoutByteQty = Me.SendoutByteQty + UBound(Mbcd)
                        'Me.Comstate = True '表示正等待数据的返回
                    End If
                End If
                '                      End If
            Catch ex As Exception
            End Try
        End Function
        Function GetMBTCPWriteCommandByte(ByVal MBAD As String, ByRef Length As Integer, ByVal Value As Object) As Object
            Dim i As Object
            Dim Mbcd() As Byte

            Dim Start As Integer
            Dim SlaveAd As Integer
            'SlaveAd = Me.ad





            Dim dt As IDataBlock
            For Each dt In Me.DTblocks

                If Left(dt.startAd, 1) = Left(MBAD, 1) Then
                    'Dim j As Integer = CInt(MBAD.Substring(1))
                    If CInt(MBAD.Substring(1)) > dt.SvrMBADStart And CInt(MBAD.Substring(1)) < dt.SvrMBADStart + dt.SvrAddrLength Then
                        SlaveAd = dt.Addr
                        MBAD = (CInt(MBAD) - dt.SvrMBADStart).ToString
                        Exit For
                    End If
                End If


            Next dt

            If Length = 1 Then
                ReDim Mbcd(11)
                Mbcd(0) = 1 \ 256 '标识巡测哪个数据块
                Mbcd(1) = 1 Mod 256
                Mbcd(2) = 0
                Mbcd(3) = 0
                Mbcd(4) = 6 \ 256
                Mbcd(5) = 6 Mod 256
                Select Case Left(MBAD, 1)
                    Case CStr(0)

                        Start = Val(Right(MBAD, 5)) - 1
                        Mbcd(6) = SlaveAd

                        Mbcd(7) = 5
                        Mbcd(8) = Start \ 256
                        Mbcd(9) = Start Mod 256
                        'UPGRADE_WARNING: 未能解析对象 Value() 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
                        Mbcd(10) = CInt(Value)

                        Mbcd(11) = 0
                    Case CStr(4)
                        Start = Val(Right(MBAD, 5)) - 1

                        Mbcd(6) = SlaveAd

                        Mbcd(7) = 6
                        Mbcd(8) = Start \ 256
                        Mbcd(9) = Start Mod 256
                        'UPGRADE_WARNING: 未能解析对象 Value() 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
                        Mbcd(10) = CInt(Value) \ 256

                        'UPGRADE_WARNING: 未能解析对象 Value() 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
                        Mbcd(11) = CInt(Value) Mod 256
                End Select

            Else

                Select Case Left(MBAD, 1)
                    Case CStr(0)
                        ReDim Mbcd(13 + UBound(Value))
                        Mbcd(0) = 1 \ 256 '标识巡测哪个数据块
                        Mbcd(1) = 1 Mod 256
                        Mbcd(2) = 0
                        Mbcd(3) = 0
                        Mbcd(4) = 6 \ 256
                        Mbcd(5) = 6 Mod 256
                        Start = Val(Right(MBAD, 5)) - 1


                        Mbcd(6) = SlaveAd

                        Mbcd(7) = 15
                        Mbcd(8) = Start \ 256
                        Mbcd(9) = Start Mod 256
                        Mbcd(10) = Length \ 256

                        Mbcd(11) = Length Mod 256
                        Mbcd(12) = UBound(Value) + 1
                        For i = 0 To UBound(Value)
                            'UPGRADE_WARNING: 未能解析对象 i 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
                            'UPGRADE_WARNING: 未能解析对象 Value() 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
                            Mbcd(13 + i) = Value(i)
                        Next i
                    Case CStr(4)
                        ReDim Mbcd(13 + UBound(Value))
                        Mbcd(0) = 1 \ 256 '标识巡测哪个数据块
                        Mbcd(1) = 1 Mod 256
                        Mbcd(2) = 0
                        Mbcd(3) = 0
                        Mbcd(4) = 6 \ 256
                        Mbcd(5) = 6 Mod 256
                        Start = Val(Right(MBAD, 5)) - 1


                        Mbcd(6) = SlaveAd

                        Mbcd(7) = 16
                        Mbcd(8) = Start \ 256
                        Mbcd(9) = Start Mod 256
                        Mbcd(10) = Length \ 256

                        Mbcd(11) = Length Mod 256
                        Mbcd(12) = UBound(Value) + 1
                        For i = 0 To UBound(Value)
                            'UPGRADE_WARNING: 未能解析对象 i 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
                            'UPGRADE_WARNING: 未能解析对象 Value() 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
                            Mbcd(13 + i) = Value(i)
                        Next i


                End Select



            End If
            'UPGRADE_WARNING: 未能解析对象 GetWriteCommandByte 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
            Return Mbcd


        End Function


        Public Property PollEnable() As Boolean Implements IGPRSRTU.PollEnable
            Get
                PollEnable = mPollEnable
            End Get
            Set(ByVal value As Boolean)
                mPollEnable = value
            End Set
        End Property

        Public Property ComfaileTimes() As Integer Implements IGPRSRTU.ComfaileTimes
            Get
                ComfaileTimes = mComfaileTimes
            End Get
            Set(ByVal value As Integer)
                mComfaileTimes = value
            End Set
        End Property

        Public Property ComSuccesstimes() As Integer Implements IGPRSRTU.ComSuccesstimes
            Get
                ComSuccesstimes = mComSuccesstimes
            End Get
            Set(ByVal value As Integer)
                mComSuccesstimes = value
            End Set
        End Property




      

     

        Private Sub Mytmr_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Mytmr.Tick
            MytimerSend()
        End Sub
    End Class
End Namespace