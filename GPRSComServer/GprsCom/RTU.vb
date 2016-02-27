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
        '��������ֵ�ľֲ�����
        Private mvarComminfo As String '�ֲ�����
        Private mvarRTuName As String '�ֲ�����
        '��������ֵ�ľֲ�����
        Private mvarEnable As Boolean '�ֲ�����
        Private mvarDeviceAD As Short
        '��������ֵ�ľֲ�����
        Private mvarDTblock As DataBlocks '�ֲ�����
        '��������ֵ�ľֲ�����
        'Private mvarNextcommandShouldbeWrite As Boolean '�ֲ�����
        Private mvarMBadressQuantity As Integer '�ֲ�����
        '��������ֵ�ľֲ�����
        Private mvarBaseAd As Integer '�ֲ�����
        '��������ֵ�ľֲ�����
        Private mvarCodeName As Integer '�ֲ�����
        Dim f As New iffirst
        Private MdtID As Integer
        Public WithEvents Dsc As DataServerCenter
        Public Comstate As Boolean '�����RTU�������շ�״̬�����ʾ�ѷ��͵���δ�յ�����Ҳû�г�ʱҲ�������ڵȴ����ݵķ��أ��ٱ�ʾû�з��ͻ��ѳ�ʱ
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
                    SaveconnstatetoDB((Me.RtuName), IIf(Value, "����", "����"))
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
                '������ָ�ɶ���ʱʹ�ã�λ�� Set ������ߡ�
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
                    Me.PollEnable = False '��Ѳ�������һ�����ݿ�����˴�ͨѶ
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
                    Mbs.WritevaluebyAd((Me.DeviceAD), MBAD, Device.Datatype.�޷�������, v)
                Case 6
                    MBAD = "4" & Format(mb(2) * 256 + mb(3) + 1, "00000")
                    v = mb(4) * 256 + mb(5)
                    Mbs.WritevaluebyAd((Me.DeviceAD), MBAD, Device.Datatype.�޷�������, v)
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
            '������ֽ���
            L = 1 + 1 + 1
            Mbresponse(4) = L \ 256
            Mbresponse(5) = L Mod 256
            Mbresponse(7) = mbdata(7) + &H80 '���ִ��󣬹�����Ҫ��ʮ������80
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
                        '����ʾ
                        If Fview.Visible = True Then
                            If Fview.chkGprscomdisplay.CheckState = 1 Then

                                Fview.Text2.Text = HextoStr(mValue, length, 0) & Me.RtuName & "-" & comminfo
                                Fview.txtRtuname.Text = "(" & Now & ")"
                                If Not Me.polltime = 0 Then
                                    Fview.txtRvtime.Text = Format(DateDiff(DateInterval.Second, #1/1/2000#, Today) + Microsoft.VisualBasic.Timer - _sendtime, "0.00")
                                End If
                            End If
                        End If
                        '��¼
                        AddToBuffer(mValue, length)

                        If Me.polltime = 0 Then 'Ϊ�����ϴ�ģʽ
                            'For i As Int16 = 1 To Me.DTblocks.Count
                            'If mValue(0) = Me.DTblocks(i).Addr Then '���ݴ��豸��ַ�����Ӧ
                            dt = Me.DTblocks(1) '���ǵ������ϴ������ϵ����ڲ�֧�ֶ����ݿ飬Ҳ����һ��վ��ֻ����һ�����ݿ�
                            'End If
                            'Next
                        Else
                            dt = Me.DTblocks((Me.CurrentDatablockID)) 'ȷ����ǰ���ݿ�
                        End If

                        find = dt.GetValueFromRvData(length, mValue)


                        If find = True Then '����ѯ������������
                            If Me.polltime = 0 Then '�������ϴ�ģʽҪ��Ӧʱ���DTU
                                Dim C() As Byte

                                Dim s As String
                                s = TypeName(Me.DTblocks(1))
                                Select Case TypeName(Me.DTblocks(1))
                                    Case "LDDataBlock"  ' ���ϻ�Ӧ
                                        C = dt.GetCommandBytes
                                        Dsc.SenddataByPhon(Me.CommInfo, C.Length, C)
                                    Case "MRDataBlock"
                                        'Dim S As String
                                        'S = "SendOK" & vbCrLf
                                        'C = System.Text.ASCIIEncoding.ASCII.GetBytes(S)

                                        'Dsc.SenddataByPhon(Me.commInfo, C.Length, C)
                                End Select

                            End If
                            '��ʾ���û�
                            If Fview.Visible = True Then
                                If Fview.chkGprscomdisplay.CheckState = 1 Then

                                    Fview.Text2.Text = HextoStr(mValue, length, 0) & Me.RtuName & "-" & comminfo & " " & dt.BlockName & " ͨ�ųɹ�"
                                    Fview.txtRtuname.Text = Me.RtuName & "(" & Now & ")"
                                    'UPGRADE_WARNING: DateDiff ��Ϊ���ܲ�ͬ�� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6B38EC3F-686D-4B2E-B5A5-9E8E7A762E32"��
                                    If Not Me.polltime = 0 Then
                                        Fview.txtRvtime.Text = Format(DateDiff(DateInterval.Second, #1/1/2000#, Today) + Microsoft.VisualBasic.Timer - _sendtime, "0.00")
                                    End If
                                End If
                            End If

                            '����
                            Me.ReceiveByteQty = Me.ReceiveByteQty + length
                            '              Me.DataMove.DataMove
                            Me.mComSuccesstimes = Me.mComSuccesstimes + 1
                            Me.Comstate = False '��ʾһ��ͨѶ���,��λͨѶ״̬
                            Me.CurrentDatablockID = Me.CurrentDatablockID + 1
                        Else '�ǿ�������ķ���,��ֵ��Ӧ�Ĵ���,ͬʱ��ʾ,
                            Me.ReceiveCommandReturn(mValue)

                            If Fview.Visible = True Then
                                If Fview.chkGprscomdisplay.CheckState = 1 Then

                                    Fview.Text2.Text = HextoStr(mValue, length, 0) & "  " & Me.RtuName '& "���������ѱ�ִ�У�"
                                    Fview.txtRtuname.Text = Me.RtuName & "(" & Now & ")"
                                End If
                            End If

                            '����
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
            If Dbs Is Nothing Then '���վû�ж������ݿ�
                Exit Sub
            End If
            If Dbs.Count < 1 Then
                Exit Sub
            End If

            Try

                Dim Mbcd() As Byte
                If Me.Enable = True Then '�����ô����п��趨���������վ��Ѳ�⻹�ǲ�Ѳ��,��������Ǹ��û�ʹ�õģ��û��趨����Ѳ��ʱҲ����������ԡ�
                    If Me.polltime = 0 Then 'Ϊ�����ϴ�ģʽ����һ������Ҫ����Ӧ����ͨ��RTU�˿��Խ���ͨ����,��Ϊ��һ�ξ���ʱ��ȷ�ϡ�
                        If Me.IfOnline Then
                            If Me.firstOnline.FirstTime Then
                                Dim B() As Byte = Me.DTblocks.Item(1).GetCommandBytes
                                Mydsc.SenddataByPhon(Me.CommInfo, B.Length, B)
                            End If
                        Else
                            Me.firstOnline.resetToOrigin()
                        End If

                    End If
                    If Me.PollEnable = True Then '�ڵ���Ѳ���Ѳ�������о���������Ѳ������ʱ�䵽ʱ��Ϊ���Ҫ����Ѳ��ʱ��Ϊ��,���ڱ�վ���п�Ѳ����Զ���Ϊ�٣����������Ϊ��ϵͳ�ڲ�ʹ�õġ�
                        If Me.IfOnline Then


                            Dtid = Me.CurrentDatablockID

                            dt = Me.DTblocks(Dtid)
                            If dt.Enable = True Then 'EnableΪtrue�����ݿ�����ѯ
                                If Me.Comstate = False Then '���ʱ�ɷ�������

                                    Mbcd = dt.GetCommandBytes
                                    'Try
                                    Fsendresult = Dsc.SenddataByPhon(Me.CommInfo, UBound(Mbcd) + 1, Mbcd)
                                    'Catch ex As Exception
                                    '    MsgBox(ex.Message)
                                    'End Try

                                    If Fsendresult = True Then '���ͳɹ�
                                        '��ʱ
                                        'Sendtime = DateDiff(Microsoft.VisualBasic.DateInterval.Second, #1/1/2010#, Today) + Microsoft.VisualBasic.Timer()
                                        _sendtime = DateDiff(DateInterval.Second, #1/1/2000#, Today) + Microsoft.VisualBasic.Timer
                                        '��ʾ
                                        If Fview.Visible = True Then
                                            If Fview.chkGprscomdisplay.CheckState = 1 Then
                                                Fview.Text1.Text = HextoStr(Mbcd, UBound(Mbcd) + 1, 0)
                                                Fview.txtRtuname.Text = Me.RtuName
                                            End If
                                        End If
                                        '����
                                        Me.SendoutByteQty = Me.SendoutByteQty + UBound(Mbcd)
                                        Me.Comstate = True '��ʾ���ȴ����ݵķ���
                                    End If
                                    '\\\\\\\\\\\\\\\\\\\\\\\\\\\\\�������


                                End If
                                If DateDiff(DateInterval.Second, #1/1/2000#, Today) + Microsoft.VisualBasic.Timer - _sendtime > Me.Timeout Then '�ڲ������ѳ�ʱ,ǿ�ƴ˴�ͨѶ����������ͨѶ״̬��λ
                                    '��ʾ���û�
                                    If Fview.Visible = True Then
                                        If Fview.chkGprscomdisplay.CheckState = 1 Then

                                            Fview.Text2.Text = Me.RtuName & "��" & Me.CurrentDatablockID & "ͨѶ��ʱ"
                                            Fview.txtRtuname.Text = Me.RtuName & "(" & Now & ")"
                                            Fview.txtRvtime.Text = Format(DateDiff(DateInterval.Second, #1/1/2000#, Today) + Microsoft.VisualBasic.Timer - _sendtime, "0.00")
                                        End If
                                    End If
                                    Me.mComfaileTimes = Me.mComfaileTimes + 1
                                    Me.CurrentDatablockID = Me.CurrentDatablockID + 1
                                    Me.Comstate = False

                                End If
                            Else '������enable����Ϊfalse ��ô������һ��
                                Me.CurrentDatablockID = Me.CurrentDatablockID + 1
                            End If

                        End If
                    End If
                End If
            Catch ex As Exception
            End Try
        End Sub
        Function MBWrite(ByVal MBAD As String, ByRef Value As Object) As Boolean '����д��RTU������
            Dim Mbcd() As Byte
            Dim Fsendresult As Boolean
            Try
                If Me.CommInfo.Contains(":") Then
                    Mbcd = Me.GetMBTCPWriteCommandByte(MBAD, 1, Value)
                Else
                    Mbcd = Me.GetWriteCommandByte(MBAD, Value)
                End If

                '                       If Me.Comstate = False Then '���ʱ�ɷ�������
                If Mydsc.IfThisChannelUseable((Me.CommInfo)) Then
                    Fsendresult = Dsc.SenddataByPhon(Me.CommInfo, UBound(Mbcd) + 1, Mbcd)

                    If Fsendresult = True Then '���ͳɹ�
                        '��ʱ
                        ''Sendtime = DateDiff("s", #1/1/1970#, Date) + Timer
                        '��ʾ
                        If Fview.Visible = True Then
                            If Fview.chkGprscomdisplay.CheckState = 1 Then
                                Fview.Text1.Text = HextoStr(Mbcd, UBound(Mbcd) + 1, 0)
                                Fview.txtRtuname.Text = Me.RtuName
                            End If
                        End If
                        '����
                        Me.SendoutByteQty = Me.SendoutByteQty + UBound(Mbcd)
                        'Me.Comstate = True '��ʾ���ȴ����ݵķ���
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
                Mbcd(0) = 1 \ 256 '��ʶѲ���ĸ����ݿ�
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
                        'UPGRADE_WARNING: δ�ܽ������� Value() ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
                        Mbcd(10) = CInt(Value)

                        Mbcd(11) = 0
                    Case CStr(4)
                        Start = Val(Right(MBAD, 5)) - 1

                        Mbcd(6) = SlaveAd

                        Mbcd(7) = 6
                        Mbcd(8) = Start \ 256
                        Mbcd(9) = Start Mod 256
                        'UPGRADE_WARNING: δ�ܽ������� Value() ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
                        Mbcd(10) = CInt(Value) \ 256

                        'UPGRADE_WARNING: δ�ܽ������� Value() ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
                        Mbcd(11) = CInt(Value) Mod 256
                End Select

            Else

                Select Case Left(MBAD, 1)
                    Case CStr(0)
                        ReDim Mbcd(13 + UBound(Value))
                        Mbcd(0) = 1 \ 256 '��ʶѲ���ĸ����ݿ�
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
                            'UPGRADE_WARNING: δ�ܽ������� i ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
                            'UPGRADE_WARNING: δ�ܽ������� Value() ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
                            Mbcd(13 + i) = Value(i)
                        Next i
                    Case CStr(4)
                        ReDim Mbcd(13 + UBound(Value))
                        Mbcd(0) = 1 \ 256 '��ʶѲ���ĸ����ݿ�
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
                            'UPGRADE_WARNING: δ�ܽ������� i ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
                            'UPGRADE_WARNING: δ�ܽ������� Value() ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
                            Mbcd(13 + i) = Value(i)
                        Next i


                End Select



            End If
            'UPGRADE_WARNING: δ�ܽ������� GetWriteCommandByte ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
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