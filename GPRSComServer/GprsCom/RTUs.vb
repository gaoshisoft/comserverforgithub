Option Strict Off
Option Explicit On

Imports System.Collections.Generic
Imports MBsrv
Imports System.Xml

Namespace GprsCom
    Friend Class GPRSRTUs
        Implements IEnumerable
        Implements IDisposable

        '局部变量，保存集合
        Private mCol As Collection
        '保持属性值的局部变量
        Private mvarStationPolltime As Integer '局部复制
        Private mvarDataBlockPolltime As Integer '局部复制
        Private mvartimeout As Integer '局部复制

    

        Public SocketSvrPort As Integer

       
        Public ModbusSvrAdapter As Integer
        Public SocketSvrAdapter As Integer
        Public ModbusSvrPort As Integer
        Public RemotingTcpPort As Integer
        Public OPCsvrName As String
       
        Public RegToMBTbl As Hashtable = New Hashtable
        Dim Pollcounter As Integer
        Dim WithEvents Tmr As Timer
        Public WithEvents tmrPoll As New Timer
        Sub StopPoll()
            tmrPoll.Enabled = False
        End Sub

        Sub StartPoll()
            tmrPoll.Interval = RTUs.StationPolltime * 1000
            tmrPoll.Enabled = True
            Pollcounter = 0

            tmrpoll_Tick(tmrPoll, New System.EventArgs()) '立即启动
        End Sub
        Sub StopMe()
            Tmr.Enabled = False
            tmrPoll.Enabled = False
            For Each r As GPRSRTU In mCol
                r.Mytmr.Enabled = False
            Next
        End Sub
        Sub StartMe()
            Tmr.Enabled = True
            tmrPoll.Enabled = True
            For Each r As GPRSRTU In mCol
                r.Mytmr.Enabled = True
            Next
        End Sub
        Private Sub tmrpoll_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
       Handles tmrPoll.Tick

            If Pollcounter > 1000 Then
                Pollcounter = 0
            End If
            Dim r As GPRSRTU
            For Each r In RTUs
                If r.ifportionPoll = False Then
                    If r.polltime <> 0 Then '为主动上传模式
                        If Pollcounter Mod r.polltime = 0 Then '根据巡测周期这站巡测该不该不测
                            r.PollEnable = True
                        End If

                    End If


                End If
            Next r
            Pollcounter = Pollcounter + 1
        End Sub

        Public Property DataBlockPolltime() As Integer
            Get

                'Syntax: Debug.Print X.DataBlockPolltime
                DataBlockPolltime = mvarDataBlockPolltime
            End Get
            Set(ByVal Value As Integer)

                'Syntax: X.DataBlockPolltime = 5
                mvarDataBlockPolltime = Value
            End Set
        End Property


        Public Property Timeout() As Integer
            Get

                'Syntax: Debug.Print X.StationPolltime
                Timeout = mvartimeout
            End Get
            Set(ByVal Value As Integer)

                'Syntax: X.StationPolltime = 5
                mvartimeout = Value
            End Set
        End Property


        Public Property StationPolltime() As Integer
            Get

                'Syntax: Debug.Print X.StationPolltime
                StationPolltime = mvarStationPolltime
            End Get
            Set(ByVal Value As Integer)

                'Syntax: X.StationPolltime = 5
                mvarStationPolltime = Value
            End Set
        End Property

        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As GPRSRTU
            Get

                'On Error Resume Next

                Item = mCol(vntIndexKey)
            End Get
        End Property


        Public ReadOnly Property Count() As Integer
            Get
                '检索集合中的元素数时使用。语法：Debug.Print x.Count
                Count = mCol.Count()

            End Get
        End Property


        Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            GetEnumerator = mCol.GetEnumerator()
        End Function


        Public Function Add(ByVal PhoneNumber As String, ByVal RtuName As String, ByVal DeviceAD As Short,
                            ByVal Polltime As Integer, ByVal Mbadqty As Integer, ByVal Enable As Boolean,
                            Optional ByVal SKey As String = "", Optional ByVal DTblock As DataBlocks = Nothing) _
            As GPRSRTU
            '创建新对象
            Dim ObjNewMember As GPRSRTU
            ObjNewMember = New GPRSRTU


            '设置传入方法的属性
            Dim i As Integer
            Dim Adqty As Integer
            ObjNewMember.DeviceAD = DeviceAD
            ObjNewMember.CodeName = Me.Count + 1
            ObjNewMember.CommInfo = PhoneNumber
            ObjNewMember.RtuName = RtuName
            ObjNewMember.MBadressQuantity = Mbadqty
            ObjNewMember.polltime = Polltime
            'objNewMember.BaseAD = 0
            ObjNewMember.Enable = Enable
            'UPGRADE_NOTE: IsMissing() 已更改为 IsNothing()。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="8AE1CB93-37AB-439A-A4FF-BE3B6760BB23"”
            If Not IsNothing(DTblock) Then
                ObjNewMember.DTblocks = DTblock
            End If

            If Len(SKey) = 0 Then
                mCol.Add(ObjNewMember)
            Else
                mCol.Add(ObjNewMember, SKey)

            End If
            For i = 1 To Me.Count
                If Me(i).DeviceAD = DeviceAD Then
                    Adqty = Me(i).MBadressQuantity + Adqty
                    '             Adqty = Me(i).MBadressQuantity
                End If
            Next i


            ObjNewMember.BaseAD = Adqty - Mbadqty
            '            objNewMember.MBadressQuantity = Adqty


            '返回已创建的对象
            Add = ObjNewMember

        End Function

        Function Add(ByVal G As GPRSRTU, Optional ByVal Skey As String = "") As GPRSRTU
            G.CodeName = Me.Count + 1
            If Len(Skey) = 0 Then
                mCol.Add(G)
            Else
                mCol.Add(G, Skey)
            End If
            Me.CalcuRTUAddr()
            Add = G
        End Function


        Public Sub Remove(ByVal vntIndexKey As Object)



            mCol.Remove(vntIndexKey)
            Me.CalcuRTUAddr()
        End Sub


      

        Public Sub New()
            MyBase.New()
            mCol = New Collection
            Tmr = New Timer

            Tmr.Interval = 2000
            Tmr.Enabled = True


        End Sub
        Sub InitRtu(ByVal DSC As DataServerCenter)


            Dim Dts As New DataBlocks
            Dim RTU As GPRSRTU

            Dim Configdoc As New XmlDocument
            Configdoc.Load((My.Application.Info.DirectoryPath & "\RTUconfig.xml"))
            Dim Xmlparent As XmlElement

            Xmlparent = Configdoc.SelectSingleNode("root/rtucomminfo")


            Me.StationPolltime = Xmlparent.GetAttribute("rtupollcycle")


            Me.Timeout = Xmlparent.GetAttribute("timeout")
            Xmlparent = Configdoc.SelectSingleNode("root/gprscomm")

            Xmlparent = Configdoc.SelectSingleNode("root/msremoting")
            Me.RemotingTcpPort = Xmlparent.GetAttribute("remotingtcpport")
            Xmlparent = Configdoc.SelectSingleNode("root/tcpserver")
            Me.SocketSvrPort = Xmlparent.GetAttribute("tcpport")
            Me.SocketSvrAdapter = Xmlparent.GetAttribute("socketsvradapter")
            Xmlparent = Configdoc.SelectSingleNode("root/modbustcpserver")
            Me.ModbusSvrAdapter = Xmlparent.GetAttribute("modbussvradapter")
            Me.ModbusSvrPort = Xmlparent.GetAttribute("modbussvrport")
            Xmlparent = Configdoc.SelectSingleNode("root/opcserver")
            Me.OPCsvrName = Xmlparent.GetAttribute("opcservername")


            Xmlparent = Configdoc.SelectSingleNode("root/rtucomminfo")
            If Me.Count > 0 Then
                For Each r As GPRSRTU In Me
                    Me.Remove(r.RtuName)
                Next
            End If
            Dim RTUEle As XmlElement
            Dim DBele As XmlElement
            For Each RTUEle In Xmlparent.ChildNodes
                RTU = Me.Add(RTUEle.GetAttribute("comminfo"), RTUEle.GetAttribute("rtuname"),
                                 RTUEle.GetAttribute("devicead"), RTUEle.GetAttribute("polltime"),
                                 RTUEle.GetAttribute("mbadqty"), RTUEle.GetAttribute("enable"),
                                 RTUEle.GetAttribute("rtuname"))

            Next RTUEle


            For Each RTU In Me
                Dts = New DataBlocks
                RTU.Timeout = Me.Timeout
                Xmlparent = Configdoc.SelectSingleNode("root/rtucomminfo/rtu[@rtuname='" & RTU.RtuName & "']")

                For Each DBele In Xmlparent.ChildNodes

                    Select Case DBele.GetAttribute("blocktype")

                        Case "Modbus"
                            Dim TDt As DataBlock
                            TDt = Dts.Add(DBele.GetAttribute("blockname"), DBele.GetAttribute("addr"),
                                          DBele.GetAttribute("startad"), DBele.GetAttribute("length"),
                                          DBele.GetAttribute("enable"), DBele.GetAttribute("blockname"))
                            TDt.SvrDevAd = RTU.DeviceAD

                            TDt.SvrAddrLength = DBele.GetAttribute("length")
                            'TDt.SvrMBADStart = rtu.BaseAD + dt.GetaddrSpace - TDt.SvraddrLength + 1
                            'dt.CalcuDataBlockaddr()
                        Case "TXLLJ"
                            Dim D As New TXDataBlock

                            D.BlockName = DBele.GetAttribute("blockname")
                            D.Addr = DBele.GetAttribute("addr")
                            D.Enable = DBele.GetAttribute("enable")

                            D.SvrDevAd = RTU.DeviceAD
                            D.SvrAddrLength = 10
                            'D.SvrMBADStart = rtu.BaseAD + dt.GetaddrSpace + 1
                            Dts.Add(D, D.BlockName)
                            'dt.CalcuDataBlockaddr()
                        Case "CNLLJ"
                            Dim D As New CNDataBlock

                            D.BlockName = DBele.GetAttribute("blockname")
                            D.Addr = DBele.GetAttribute("addr")
                            D.Enable = DBele.GetAttribute("enable")

                            D.SvrDevAd = RTU.DeviceAD
                            D.SvrAddrLength = 10
                            'D.SvrMBADStart = rtu.BaseAD + dt.GetaddrSpace + 1
                            Dts.Add(D, D.BlockName)
                            'dt.CalcuDataBlockaddr()
                        Case "XKLLJ"
                            Dim D As New XKDataBlock
                            D.BlockName = DBele.GetAttribute("blockname")
                            D.Addr = DBele.GetAttribute("addr")
                            D.Enable = DBele.GetAttribute("enable")

                            D.SvrDevAd = RTU.DeviceAD
                            D.SvrAddrLength = 20
                            'D.SvrMBADStart = rtu.BaseAD + dt.GetaddrSpace + 1
                            Dts.Add(D, D.BlockName)
                            'dt.CalcuDataBlockaddr()
                        Case "MultiRecord"
                            Dim D As New MRDataBlock
                            D.BlockName = DBele.GetAttribute("blockname")
                            D.Addr = DBele.GetAttribute("addr")
                            D.Enable = DBele.GetAttribute("enable")
                            D.SvrDevAd = RTU.DeviceAD
                            'Dim SA As String
                            'SA = DBele.GetAttribute("startAD")
                            'D.SlaveStartAD = SA
                            D.SingleRecordLength = Convert.ToInt16(DBele.GetAttribute("length"))
                            D.SvrAddrLength = Convert.ToInt16(DBele.GetAttribute("length"))
                            Dts.Add(D, D.BlockName)
                            D = Nothing
                        Case "LDDataBlock"
                            Dim D As New LDDataBlock
                            D.BlockName = DBele.GetAttribute("blockname")
                            D.Addr = DBele.GetAttribute("addr")
                            D.Enable = DBele.GetAttribute("enable")
                            D.SvrDevAd = RTU.DeviceAD
                            'Dim SA As String
                            'SA = DBele.GetAttribute("startAD")
                            'D.SlaveStartAD = SA
                            D.SingleRecordLength = 100
                            D.SvrAddrLength = 100
                            D.RegToMBcol = Me.RegToMBTbl
                            Dts.Add(D, D.BlockName)
                            D = Nothing
                        Case "CSLLJ"
                            Dim D As New CSDataBlock
                            D.BlockName = DBele.GetAttribute("blockname")
                            D.Addr = DBele.GetAttribute("addr")
                            D.Enable = DBele.GetAttribute("enable")

                            D.SvrDevAd = RTU.DeviceAD
                            D.SvrAddrLength = 20
                            'D.SvrMBADStart = rtu.BaseAD + dt.GetaddrSpace + 1
                            Dts.Add(D, D.BlockName)
                        Case "DWLLJ"
                            Dim D As New DWDataBlock
                            D.BlockName = DBele.GetAttribute("blockname")
                            D.Addr = DBele.GetAttribute("addr")
                            D.LjllXS = DBele.GetAttribute("length")
                            D.Enable = DBele.GetAttribute("enable")

                            D.SvrDevAd = RTU.DeviceAD
                            D.SvrAddrLength = 20
                            'D.SvrMBADStart = rtu.BaseAD + dt.GetaddrSpace + 1
                            Dts.Add(D, D.BlockName)
                        Case "ModbusTCP" 'ModbusTCP
                            Dim TDt As New MBTCPDataBlock

                            TDt.BlockName = DBele.GetAttribute("blockname")
                            TDt.startAD = DBele.GetAttribute("startad")
                            TDt.Addr = DBele.GetAttribute("addr")
                            TDt.Length = DBele.GetAttribute("length")
                            TDt.Enable = DBele.GetAttribute("enable")

                            TDt.SvrDevAd = RTU.DeviceAD

                            TDt.SvrAddrLength = DBele.GetAttribute("length")
                            Dts.Add(TDt, TDt.BlockName)
                        Case "DW64LLJ"
                            Dim D As New DW64DataBlock

                            D.BlockName = DBele.GetAttribute("blockname")
                            D.Addr = DBele.GetAttribute("addr")
                            D.LjllXS = DBele.GetAttribute("length")
                            D.Enable = DBele.GetAttribute("enable")

                            D.SvrDevAd = RTU.DeviceAD
                            D.SvrAddrLength = 20
                            'D.SvrMBADStart = rtu.BaseAD + dt.GetaddrSpace + 1
                            Dts.Add(D, D.BlockName)
                        Case "CorusLLJ"
                            Dim D As New CorusDataBlock


                            D.BlockName = DBele.GetAttribute("blockname")
                            D.Addr = DBele.GetAttribute("addr")
                            D.LjllXS = DBele.GetAttribute("length")
                            D.Enable = DBele.GetAttribute("enable")

                            D.SvrDevAd = RTU.DeviceAD
                            D.SvrAddrLength = 20
                            'D.SvrMBADStart = rtu.BaseAD + dt.GetaddrSpace + 1
                            Dts.Add(D, D.BlockName)
                        Case "XiXiangLLJ"
                            Dim D As New XiXiangDataBlock

                            D.BlockName = DBele.GetAttribute("blockname")
                            D.Addr = DBele.GetAttribute("addr")
                            D.LjllXS = DBele.GetAttribute("length")
                            D.Enable = DBele.GetAttribute("enable")

                            D.SvrDevAd = RTU.DeviceAD
                            D.SvrAddrLength = 20
                            'D.SvrMBADStart = rtu.BaseAD + dt.GetaddrSpace + 1
                            Dts.Add(D, D.BlockName)
                        Case "TJWJXLLJ"
                            Dim D As New TJWJXDataBlock
                            D.BlockName = DBele.GetAttribute("blockname")
                            D.Addr = DBele.GetAttribute("addr")
                            D.Enable = DBele.GetAttribute("enable")

                            D.SvrDevAd = RTU.DeviceAD
                            D.SvrAddrLength = 20
                            'D.SvrMBADStart = rtu.BaseAD + dt.GetaddrSpace + 1
                            Dts.Add(D, D.BlockName)
                            'dt.CalcuDataBlockaddr()
                        Case "TXCpuCardDataBlock"
                            '---------------------反射
                            'Dim s As String
                            's = DBele.GetAttribute("BlockType")
                            'Dim D As IDataBlock
                            'D = Activator.CreateInstance(Type.GetType(s))

                            'D.BlockName = DBele.GetAttribute("blockname")
                            'D.addr = DBele.GetAttribute("addr")
                            'D.Enable = DBele.GetAttribute("enable")

                            'D.SvrDevAd = rtu.DeviceAD
                            'D.SvraddrLength = 20
                            ''D.SvrMBADStart = rtu.BaseAD + dt.GetaddrSpace + 1
                            'dts.Add(D, D.BlockName)
                            '-------------------------------
                            Dim D As New TXCpuCardDataBlock
                            D.BlockName = DBele.GetAttribute("blockname")
                            D.Addr = DBele.GetAttribute("addr")
                            D.Enable = DBele.GetAttribute("enable")

                            D.SvrDevAd = RTU.DeviceAD
                            D.SvrAddrLength = 20
                            'D.SvrMBADStart = rtu.BaseAD + dt.GetaddrSpace + 1
                            Dts.Add(D, D.BlockName)

                        Case "HZCDataBlock"

                            Dim D As New HZCDataBlock
                            D.BlockName = DBele.GetAttribute("blockname")
                            D.Addr = DBele.GetAttribute("addr")
                            D.Enable = DBele.GetAttribute("enable")

                            D.SvrDevAd = RTU.DeviceAD
                            D.SvrAddrLength = 20
                            'D.SvrMBADStart = rtu.BaseAD + dt.GetaddrSpace + 1
                            Dts.Add(D, D.BlockName)
                        Case "SXHTDataBlock"

                            Dim D As New SXHTDataBlock
                            D.BlockName = DBele.GetAttribute("blockname")
                            D.Addr = DBele.GetAttribute("addr")
                            D.Enable = DBele.GetAttribute("enable")

                            D.SvrDevAd = RTU.DeviceAD
                            D.SvrAddrLength = 30
                            'D.SvrMBADStart = rtu.BaseAD + dt.GetaddrSpace + 1
                            Dts.Add(D, D.BlockName)

                        Case "HZHHDataBlock"

                            Dim D As New HZHHDataBlock
                            D.BlockName = DBele.GetAttribute("blockname")
                            D.Addr = DBele.GetAttribute("addr")
                            D.Enable = DBele.GetAttribute("enable")

                            D.SvrDevAd = RTU.DeviceAD
                            D.SvrAddrLength = 30
                            'D.SvrMBADStart = rtu.BaseAD + dt.GetaddrSpace + 1
                            Dts.Add(D, D.BlockName)
                    End Select
                    RTU.DTblocks = Dts
                    Dts.RtuBaseAD = RTU.BaseAD
                    Dts.CalcuDataBlockaddr()


                    RTU = Nothing
                Next DBele

                Dts = Nothing
            Next RTU
            For i As Int32 = 1 To Me.Count
                Me(i).Dsc = dsc
                If RTUs(i).CommInfo.Contains(":") Then
                    DSC.AddTcpClient(RTUs(i).CommInfo)
                End If
            Next
        End Sub
      
        Public Sub Clear()
            mCol.Clear()
        End Sub

        Protected Overrides Sub Finalize()
            Tmr.Enabled = False
            Tmr.Dispose()
            Tmr.Enabled = False
            tmrPoll.Dispose()
            mCol.Clear()
            mCol = Nothing
            MyBase.Finalize()
        End Sub

        Public Function GetServerADstart(ByVal RtuID As Integer) As Integer


            GetServerADstart = Me(RtuID).BaseAD + 1
        End Function


        Public Function GetServerADend(ByVal RtuID As Integer) As Integer

            GetServerADend = Me(RtuID).BaseAD + Me(RtuID).MBadressQuantity
        End Function

        Sub RecordReset()
            Dim rtu As GPRSRTU
            For Each rtu In Me
                '   rtu.Comsendtimes = 0
                rtu.mComfaileTimes = 0
                rtu.mComSuccesstimes = 0
                rtu.ReceiveByteQty = 0
                rtu.SendoutByteQty = 0
            Next rtu
        End Sub

        Sub comRecordReset()
            Dim rtu As GPRSRTU
            For Each rtu In Me
                rtu.mComfaileTimes = 0
                rtu.mComSuccesstimes = 0
            Next rtu
        End Sub

        Function GetMostdtNumber() As Integer
            Dim i As Integer
            Dim Maxnum As Integer
            Maxnum = Me(1).DTblocks.Count
            For i = 1 To Me.Count
                If Maxnum < Me(i).DTblocks.Count Then
                    Maxnum = Me(i).DTblocks.Count
                End If
            Next i
            GetMostdtNumber = Maxnum
        End Function

        Sub WriteToRTU(ByVal devad As Integer, ByVal MBAD As String, ByVal value As Object)
            Dim i As Integer
            Dim ad As Integer
            Dim RTUmbad As String

            ad = Val(Right(MBAD, 5))
            Dim r As GPRSRTU
            r = Nothing
            For i = 1 To Me.Count

                If ad >= Me.GetServerADstart(i) And ad <= Me.GetServerADend(i) And Me(i).DeviceAD = devad Then
                    r = Me(i)
                    Exit For
                End If
            Next i

            If Not r Is Nothing Then
                RTUmbad = Left(MBAD, 1) & Format(ad - r.BaseAD, "00000")
                r.MBWrite(RTUmbad, value)
            End If
        End Sub

        Function GetIDfromName(ByRef RtuName As String) As Integer
            Dim i As Integer
            For i = 1 To Me.Count
                If Me(i).RtuName = Trim(RtuName) Then
                    GetIDfromName = i
                    Exit For
                End If
            Next i
        End Function

        Public Sub CalcuRTUAddr()
            Dim R As GPRSRTU
            Dim i As Integer

            For i = 1 To Me.Count
                If i = 1 Then
                    R = Me(i)

                    R.BaseAD = 0
                Else
                    R = Me(i)
                    Dim j As Integer
                    Dim B As Integer
                    B = 0
                    For j = 1 To i - 1
                        If Me(j).DeviceAD = R.DeviceAD Then
                            B = B + Me(j).MBadressQuantity
                        End If
                    Next
                    R.BaseAD = B
                    R.DTblocks.CalcuDataBlockaddr()
                End If
            Next
        End Sub

        Sub DisPoseMe()
            'Dim i As Integer
            Dim R As GPRSRTU
            For Each R In Me
                R.Dsc = Nothing
                R.Enable = False
                'Me.Remove(R.RtuName)
                R = Nothing

            Next
            Me.Dispose()
        End Sub

        Private disposedValue As Boolean = False        ' 检测冗余的调用

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                End If

            End If
            Me.disposedValue = True
        End Sub

#Region " IDisposable Support "
        ' Visual Basic 添加此代码是为了正确实现可处置模式。
        Public Sub Dispose() Implements IDisposable.Dispose
            ' 不要更改此代码。请将清理代码放入上面的 Dispose(ByVal disposing As Boolean) 中。
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

#End Region

        Private Sub UpdateState()
            Dim R As GPRSRTU
            Dim i As Int16, j As Int16
            For Each R In Me
                '确定DTU是否在线
                If Mydsc.IfThisChannelUseable(R.CommInfo) Then
                    R.IfOnline = True
                Else
                    R.IfOnline = False
                End If
            Next


            '将DTU状态存到Modbus TCP server中
            Dim v As Integer
            Dim Wordnum As Integer
            '在通讯编码中，每个站占用一位，从255设备的400200地址开始。对站点数没有限制.
            '先求出所有站将占用的字数,然后每16个站为一组看它们是否在线，算出它们所在字的值，写入从400200开始的地址中。

            Wordnum = Me.Count \ 16 + 1 '算出总字数，16站为一组，也是总组数
            For j = 1 To Wordnum '一组一组地算出每组对应的字的值
                For i = 0 To 15
                    If i + (j - 1) * 16 + 1 <= Me.Count Then
                        If Me(i + (j - 1) * 16 + 1).IfOnline = True Then
                            v = v + (2 ^ i)
                        End If

                    End If

                Next i
                Mbs.WritevaluebyAd(255, CStr(400200 + j - 1), Device.Datatype.无符号整型, v)
                v = 0
            Next j
            '与Citect软件的通讯，因为Citect软件不能识别4x位所以还要写到1x地址中。从255设备的100001地址开始。对站点数没有限制.(即站点状态同时也写到255设备的1x地址里）
            '同时也写到254设备的400001起始的地址里。
            For i = 1 To Me.Count
                If Me(i).IfOnline = True Then
                    Mbs.WritevaluebyAd(255, "10000" & i, Device.Datatype.无符号整型, 1)
                    Mbs.WritevaluebyAd(254, "40000" & i, Device.Datatype.无符号整型, 1)

                Else
                    Mbs.WritevaluebyAd(255, "10000" & i, Device.Datatype.无符号整型, 0)
                    Mbs.WritevaluebyAd(254, "40000" & i, Device.Datatype.无符号整型, 0)

                End If
            Next i
        End Sub

        Function Containsobj(ByVal key As String) As Boolean
          
            Containsobj = mCol.Contains(key)
        End Function

        Function GetRTunamebyphon(ByVal phon As String) As String
            Dim i As Integer
            For i = 1 To mCol.Count
                If mCol(i).CommInfo = phon Then
                    GetRTunamebyphon = mCol(i).RtuName
                    Exit Function
                End If
            Next i
        End Function


        Private Sub Tmr_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tmr.Tick
            Static d1 As Date

            Dim d2 As Date
            d2 = Today
            If d2 <> d1 Then
                If System.DateTime.FromOADate(d2.ToOADate - d1.ToOADate) = System.DateTime.FromOADate(1) Then '第一次软件启动时不要执行这三个子过程

                    RTUs.RecordReset()
                End If
                d1 = d2

            End If
            UpdateState()
        End Sub
    End Class
End Namespace