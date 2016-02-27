Option Strict Off
Option Explicit On

Imports System.Data.OleDb
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Tcp
Imports System.Net
Imports GPRSComServer.DBfunc
Imports GPRSComServer.GprsCom
Imports MBsrv.Device
Imports MBsrv
Imports GPRSComServer.OPCfunc


Imports GPRSComServer.UserFunc
Imports System.Xml


Module Moddeclare
''加密狗声明---------------------------------
'Lock32_Function is for checking Lock and your software
    Declare Function Lock32_Function Lib "cdll5.dll"(ByVal x As Integer) As Integer
'ReadLock(long)  :read data from Lock,the return Value is the data
    'Lockaddr(Integer):address for reading
    'Password(String):password for reading
    Declare Sub UnShieldLock Lib "cdll5.dll" ()
    Private Declare Function GetSystemDefaultLCID Lib "kernel32" () As Integer

    '-----------------------------------------------
    Public Mydsc As DataServerCenter
    Public Mbs As MBserver
    Public RTUs As GPRSRTUs
    Public CurrUsr As New User

    Public DBconn As DBconnect
    Public GItemCol As ItemCollection
    Public OpcSvr As MyOPCserver
    Public GDoComWork As New DoComWork




    Public Sub Main()


        Appinit()
        Application.Run()

    End Sub
    Sub Appinit()

        '构造dsc,同时也启动服务,也就是在此端口上侦听
        Mydsc = DataServerCenter.GetMyDsc()
        RTUs = New GPRSRTUs
        OpcSvr = New MyOPCserver
        GItemCol = New ItemCollection
        DBconn = New DBconnect
        RTUs.InitRtu(Mydsc)






        '初始化mbeserver
        Mbs = MBserver.GetMBS(RTUs.ModbusSvrAdapter, RTUs.ModbusSvrPort)


        'Mbs.serialComms.Add "9600,n,8,1", 1 "comm1"
        'Mbs.ShowComdata()

        'Mbs.Hidecomdata()

        InitMBserver(RTUs)
        MainProg.MdIfmain.Show()

        InitMBopcServer()
        InitDBsaveConfig()
        ArrangeWindow()
        InitTcpSocket()
        ReaddataFromDB()
        CurrUsr.Power = 3
        'InitRemoting()
    End Sub

    Sub BindItemToDataBlock()
        'Dim i As Int16
        'Dim j As Int16
        Dim itm As CItem
        Dim R As GPRSRTU
        For Each itm In GItemCol
            For Each R In RTUs
                If R.polltime = 0 Then
                    If itm.devad = R.DeviceAD Then '一般一个自动上传站只有一个块，
                        Dim D As IDataBlock
                        For Each D In R.DTblocks

                            D.AddItm(itm.ItemName)
                        Next
                    End If
                End If
            Next
        Next
    End Sub

    Sub InitTcpSocket()
        TCPsocket.Tcpport = RTUs.SocketSvrPort
        Dim Ip As IPAddress
        Ip = Dns.GetHostAddresses(Dns.GetHostName)(RTUs.SocketSvrAdapter - 1)
        TCPsocket.Ip = Ip
        TCPsocket.Visible = True
        TCPsocket.Visible = False
    End Sub
    'Sub InitTcp8084() '主动上传模式
    '    Dim Tcp8084 As TCPsocket = New TCPsocket
    '    Tcp8084.Tcpport = 8084
    '    Tcp8084.Visible = True
    '    Tcp8084.Visible = False
    'End Sub
    Sub InitRemoting()
        Dim TcpCh As TcpChannel = New TcpChannel(RTUs.RemotingTcpPort)
        ChannelServices.RegisterChannel(TcpCh, False)
        RemotingConfiguration.RegisterWellKnownServiceType(GetType(RemGetSetData), "RemGetSetData",
                                                           WellKnownObjectMode.SingleCall)
    End Sub



  

    Sub InitMBserver(ByVal allRTUs As GPRSRTUs)
        Dim i As Integer
        Dim j As Integer
        On Error Resume Next

        j = Mbs.Devices.Count
        For i = j To 1 Step -1 '先清空
            'If Mbs.Devices(i).deviceAddr <> 255 Then
            Mbs.Devices.Remove(i) '因为集合会自动修改每一项的索引，所以用最大的索引向下排就可以了
            'End If
        Next i

        Mbs.AddMbserverDevice(255, 256, "控制命令")
        Mbs.AddMbserverDevice(254, 255, "GPRS站点状态")

        For i = 1 To allRTUs.Count
            Mbs.AddMbserverDevice((allRTUs(i).DeviceAD), (allRTUs(i).MBadressQuantity),
                                  allRTUs(i).RtuName & "数据(" & allRTUs(i).BaseAD + 1 & ")；")

        Next i
        Mbs.WritevaluebyAd(255, "400001", Datatype.无符号整型, 1) '默认为标准巡测
        Mbs.WritevaluebyAd(255, "400003", Datatype.无符号整型, 2) '巡测时间至少两分钟
        Mbs.WritevaluebyAd(255, "400002", Datatype.无符号整型, 1) '巡测周期至少一秒
    End Sub

    Sub InitMBopcServer()


        Dim itemnum As Integer


        GItemCol.Clear()


        OpcSvr.Uninit()
        Try
            Dim configdoc As New XmlDocument
            configdoc.Load((My.Application.Info.DirectoryPath & "\RTUconfig.xml"))
            'xml文档全部按照小写来读取，写入时也是全部按小写写入，以免出现读取大小写不对错误
            Dim xmlparent As XmlElement

            xmlparent = configdoc.SelectSingleNode("root/opcitems")
            itemnum = xmlparent.ChildNodes.Count
            Dim HashSize As Integer
            If itemnum > 500 Then
                HashSize = OpcSvr.SetHashSize(GetGreaterPrime(System.Math.Round(itemnum * 1.2)))
            End If

            OpcSvr.Init(RTUs.OPCsvrName)


            Dim name As String


            Dim itmele As XmlElement
            For Each itmele In xmlparent.ChildNodes

                name = Trim(itmele.GetAttribute("itemname"))


                GItemCol.Add(name, itmele.GetAttribute("itemchinesedis"), itmele.GetAttribute("handleexpression"), itmele.GetAttribute("unitstr"), Now, itmele.GetAttribute("itemdevad"), itmele.GetAttribute("itemmbad"),
                              itmele.GetAttribute("itemdatatype"), itmele.GetAttribute("itemneedconvert"),
                              itmele.GetAttribute("itemairangedown"), itmele.GetAttribute("itemairangeup"),
                              itmele.GetAttribute("itemconverteddown"), itmele.GetAttribute("itemconvertedup"),
                              itmele.GetAttribute("itemswapbyte"), itmele.GetAttribute("uplimit"), itmele.GetAttribute("downlimit"), name)

            Next itmele
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally

        End Try
        BindItemToDataBlock()
    End Sub


    Sub InitDBsaveConfig() '必须放在OPC初始化之后


        Try
            Dim configdoc As New XmlDocument
            configdoc.Load((My.Application.Info.DirectoryPath & "\RTUconfig.xml"))
            Dim xmlparent As XmlElement
            Dim xmlTbl As XmlElement
            xmlparent = configdoc.SelectSingleNode("root/archiveconfig")

            Dim tname As String

            Dim f As String
            Dim tbl As Ctable
            Dim mtbl As _Itable

            DBconn.Init(xmlparent.GetAttribute("connecttype"), xmlparent.GetAttribute("sqlservername"),
                                            xmlparent.GetAttribute("dbconnectname"), xmlparent.GetAttribute("sqldbname"),
                                            xmlparent.GetAttribute("username"), xmlparent.GetAttribute("password"),
                                            xmlparent.GetAttribute("savecycle"),
                                            xmlparent.GetAttribute("updatecycle"), xmlparent.GetAttribute("ifstart"),
                                            xmlparent.GetAttribute("accessdbpath"), xmlparent.GetAttribute("dbconnectname"))
            DBconn.TableCol.Clear()
            For Each xmlTbl In xmlparent.ChildNodes


                tname = xmlTbl.GetAttribute("tablename")

                frmDBsave.Visible = False
              
                If IsDBNull(xmlTbl.GetAttribute("fields")) Then
                    f = ""
                Else
                    f = xmlTbl.GetAttribute("fields")
                End If
                tbl = DBconn.Addtbl(tname, f, xmlTbl.GetAttribute("staname"), tname)
                mtbl = tbl
                mtbl.TableType = CType([Enum].Parse(GetType(DBconnect.Tbtype), xmlTbl.GetAttribute("tabletype")), DBconnect.Tbtype)


            Next xmlTbl
        Catch ex As Exception
            MsgBox(ex.Message & ex.Source) '& ex.Errors.ToString)

        Finally

        End Try
    End Sub
'Function SaveDbsaveconfigtoDB() As Boolean
'    Dim DBpath As String
'    Dim DatabaseName As String
'    Dim Sqlstr As String

'    Dim j As Integer
'    Dim k As Integer
'    Dim Rs As ADODB.Recordset
'    Dim Cn As New ADODB.Connection
'    Dim cm As New ADODB.Command

'    Rs = New ADODB.Recordset
'    Try
'        DBpath = My.Application.Info.DirectoryPath
'        DatabaseName = "Rtuconfig"

'        Rs.let_ActiveConnection("provider=microsoft.jet.oledb.4.0;data source=" & DBpath & "\" & DatabaseName & ".mdb")
'        Rs.LockType = ADODB.LockTypeEnum.adLockOptimistic
'        cm.ActiveConnection = Rs.ActiveConnection
'        cm.CommandText = "delete * from DBsaveconfig;"
'        cm.Execute()
'        Sqlstr = "Select * from DBsaveconfig;"
'        Rs.Open(Sqlstr, , ADODB.CursorTypeEnum.adOpenKeyset) '必须不用adoforwardonly 否则查不到数据

'        Dim con As DBconnect
'        con = DBconn

'        Dim fn As String
'        Dim T As _Itable
'        For j = 1 To con.Count
'            fn = ""
'            For k = 1 To con.Item(j).ItemCol.Count()
'                If fn = "" Then
'                    fn = con.Item(j).ItemCol.Item(k).ItemName
'                Else
'                    fn = fn & "," & con.Item(j).ItemCol.Item(k).ItemName
'                End If
'            Next k

'            Rs.AddNew()
'            'End If
'            Rs.Fields("ConnectType") = con.ConnectType
'            Rs.Fields("dbconnectname") = con.ConnectName
'            Rs.Fields("sqlDbname") = con.sqldbname
'            Rs.Fields("username") = con.UserName
'            Rs.Fields("password") = con.Password
'            Rs.Fields("AccessDBPath") = con.AccessDbpath
'            Rs.Fields("ODBCDatasourceName") = con.ODBCDatasourceName
'            Rs.Fields("sqlServerName") = con.SQLServerName
'            Rs.Fields("Tablename") = con.Item(j).Tablename
'            T = con.Item(j)
'            Rs.Fields("TableType") = T.TableType

'            Rs.Fields("fields") = fn
'            Rs.Fields("savecycle") = con.SaveCycle
'            Rs.Fields("UpdateCycle") = con.UpdateCycle
'            Rs.Fields("ifstart") = con.IfStart
'        Next j

'        Rs.Update()
'        SaveDbsaveconfigtoDB = True
'    Catch ex As Exception
'        SaveDbsaveconfigtoDB = False
'    Finally


'        Rs.Close()
'        Rs = Nothing
'    End Try


'End Function


    Function Str_Int(ByRef a As String) As Short '字符转换成hex码
        Select Case a
            Case "0" To "9"
                Str_Int = Asc(a) - 48
            Case "A" To "F"
                Str_Int = Asc(a) - 55
            Case "a" To "f"
                Str_Int = Asc(a) - 87
            Case Else
                Str_Int = 0
        End Select
    End Function


    Function SaveDatatoDB() As Integer
        Dim dBpath As String
        Dim databaseName As String
        Dim sqlstr As String
        Dim rtunum As Integer
        Dim i As Integer
        Dim rs As ADODB.Recordset

        rs = New ADODB.Recordset
        Try
            dBpath = My.Application.Info.DirectoryPath
            databaseName = "GPRSDATA"

            rs.let_ActiveConnection(
                "provider=microsoft.jet.oledb.4.0;data source=" & dBpath & "\" & databaseName & ".mdb")
            rs.LockType = ADODB.LockTypeEnum.adLockOptimistic


            sqlstr = "Select * from RTU;"
            rs.Open(sqlstr, , ADODB.CursorTypeEnum.adOpenStatic) '必须不用adoforwardonly 否则查不到数据

            rtunum = rs.RecordCount

            For i = 1 To rtunum
                If Not rs.EOF Then
                    rs.Fields("comsuccesstimes").Value = RTUs(rs.Fields("rtuname").Value).mComSuccesstimes
                    rs.Fields("comFailetimes").Value = RTUs(rs.Fields("rtuname").Value).mComfaileTimes
                    rs.Fields("rvbytes").Value = RTUs(rs.Fields("rtuname").Value).ReceiveByteQty
                    rs.Fields("sendbytes").Value = RTUs(rs.Fields("rtuname").Value).SendoutByteQty
                    rs.Update()


                End If
                rs.MoveNext()

            Next i
        Catch ex As Exception

        Finally
            rs.Close()
            rs = Nothing
        End Try
    End Function

    Sub ReaddataFromDB()
        Dim DBpath As String
        Dim databaseName As String
        Dim sqlstr As String
        Dim rtunum As Integer
        Dim i As Integer
        Dim rs As ADODB.Recordset
        rs = New ADODB.Recordset
        Try
            DBpath = My.Application.Info.DirectoryPath
            databaseName = "GPRSDATA"

            rs.let_ActiveConnection(
                "provider=microsoft.jet.oledb.4.0;data source=" & DBpath & "\" & databaseName & ".mdb")


            sqlstr = "Select * from RTU;"
            rs.Open(sqlstr, , ADODB.CursorTypeEnum.adOpenStatic) '必须不用adoforwardonly 否则查不到数据
            rtunum = rs.RecordCount

            For i = 1 To rtunum
                If Not rs.EOF Then
                    RTUs(rs.Fields("rtuname").Value).mComSuccesstimes = rs.Fields("comsuccesstimes").Value
                    RTUs(rs.Fields("rtuname").Value).mComfaileTimes = rs.Fields("comFailetimes").Value
                    RTUs(rs.Fields("rtuname").Value).ReceiveByteQty = rs.Fields("rvbytes").Value
                    RTUs(rs.Fields("rtuname").Value).SendoutByteQty = rs.Fields("sendbytes").Value
                    rs.MoveNext()

                End If


            Next i
        Catch ex As Exception
        Finally

            rs.Close()
            rs = Nothing
        End Try
    End Sub

    Sub ResetDBdata()
        Dim DBpath As String
        Dim DatabaseName As String
        Dim Sqlstr As String
        Dim rtunum As Integer
        Dim i As Integer
        Dim Rs As ADODB.Recordset
        Rs = New ADODB.Recordset
        Try
            DBpath = My.Application.Info.DirectoryPath
            DatabaseName = "GPRSDATA"

            Rs.let_ActiveConnection(
                "provider=microsoft.jet.oledb.4.0;data source=" & DBpath & "\" & DatabaseName & ".mdb")
            Rs.LockType = ADODB.LockTypeEnum.adLockOptimistic

            Sqlstr = "Select * from RTU;"
            Rs.Open(Sqlstr, , ADODB.CursorTypeEnum.adOpenStatic) '必须不用adoforwardonly 否则查不到数据
            rtunum = Rs.RecordCount
            For i = 1 To rtunum
                If Not Rs.EOF Then
                    Rs.Fields("comsuccesstimes").Value = 0
                    Rs.Fields("comFailetimes").Value = 0
                    Rs.Fields("rvbytes").Value = 0
                    Rs.Fields("sendbytes").Value = 0
                    Rs.Update()
                    Rs.MoveNext()

                End If


            Next i
        Catch ex As Exception

        Finally

            Rs.Close()
            Rs = Nothing
        End Try
    End Sub

    Function SaveGPRSDatatoHisDB() As Integer
        Dim DBpath As String
        Dim DatabaseName As String
        Dim Sqlstr As String
        Dim rtunum As Integer
        Dim i As Integer
        Dim Rs As ADODB.Recordset

        Rs = New ADODB.Recordset
        Dim dt As New DataBlocks
        DBpath = My.Application.Info.DirectoryPath
        DatabaseName = "GPRSDATA"
        Try
            Rs.let_ActiveConnection(
                "provider=microsoft.jet.oledb.4.0;data source=" & DBpath & "\" & DatabaseName & ".mdb")
            Rs.LockType = ADODB.LockTypeEnum.adLockOptimistic

            Sqlstr = "Select TOP 1 * from bytesqty;"
            Rs.Open(Sqlstr, , ADODB.CursorTypeEnum.adOpenDynamic) '必须不用adoforwardonly 否则查不到数据
            rtunum = RTUs.Count
            For i = 1 To rtunum
                Rs.AddNew()
                Rs.Fields("日期").Value = DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, Today)
                Rs.Fields("rtuname").Value = RTUs(i).RtuName
                Rs.Fields("comsuccesstimes").Value = RTUs((Rs.Fields("rtuname"))).mComSuccesstimes
                Rs.Fields("comFailetimes").Value = RTUs((Rs.Fields("rtuname"))).mComfaileTimes
                Rs.Fields("rvbytes").Value = RTUs((Rs.Fields("rtuname"))).ReceiveByteQty
                Rs.Fields("sendbytes").Value = RTUs((Rs.Fields("rtuname"))).SendoutByteQty
            Next i
            Rs.Update()
        Catch ex As Exception

        Finally
            Rs.Close()
            Rs = Nothing
        End Try
    End Function

    Function GetMonthBytessum(ByRef Rs As ADODB.Recordset, ByVal month_Renamed As Integer) As Double
        Dim DBpath As String
        Dim DatabaseName As String
        Dim Sqlstr As String
'Dim rtunum As Integer
        Dim i As Integer
'Dim rs As adodb.Recordset

        Rs = New ADODB.Recordset
        Dim dt As New DataBlocks
        Try
            DBpath = My.Application.Info.DirectoryPath
            DatabaseName = "GPRSDATA"

            Rs.let_ActiveConnection(
                "provider=microsoft.jet.oledb.4.0;data source=" & DBpath & "\" & DatabaseName & ".mdb")

            Sqlstr =
                "SELECT DISTINCTROW Format$([BytesQty].[日期],'mm') AS [日期 按月], BytesQty.RTUName, Sum(BytesQty.RVbytes) AS [Sum 之 RVbytes], Sum(BytesQty.SendBytes) AS [Sum 之 SendBytes], Sum(BytesQty.ComSuccessTimes) AS [Sum 之 ComSuccessTimes], Sum(BytesQty.ComFaileTimes) AS [Sum 之 ComFaileTimes]" &
                " From BytesQty" & " GROUP BY Format$([BytesQty].[日期],'mm'),bytesqty.rtuname" &
                " HAVING (((Format$([BytesQty].[日期],'mm'))=" & month_Renamed & "));"
            Rs.Open(Sqlstr, , ADODB.CursorTypeEnum.adOpenStatic) '必须不用adoforwardonly 否则查不到数据
            For i = 1 To Rs.RecordCount

                GetMonthBytessum = GetMonthBytessum + Rs.Fields(2).Value + Rs.Fields(3).Value
                Rs.MoveNext()
            Next i
        Catch ex As Exception
        Finally
            Rs.Close()
            Rs = Nothing
        End Try
    End Function

    Function GetDayBytessum(ByRef Rs As ADODB.Recordset, ByVal D As Date) As Double
        Dim DBpath As String
        Dim DatabaseName As String
        Dim Sqlstr As String
        Dim i As Integer
        Dim myDay As Date

        Rs = New ADODB.Recordset
        Try
            DBpath = My.Application.Info.DirectoryPath
            DatabaseName = "GPRSDATA"
            myDay = D
            Rs.let_ActiveConnection(
                "provider=microsoft.jet.oledb.4.0;data source=" & DBpath & "\" & DatabaseName & ".mdb")
            Sqlstr =
                "SELECT DISTINCTROW Format$([BytesQty].[日期],'Short Date') AS [日期 按日], BytesQty.RTUName, Sum(BytesQty.RVbytes) AS [Sum 之 RVbytes], Sum(BytesQty.SendBytes) AS [Sum 之 SendBytes], Sum(BytesQty.ComSuccessTimes) AS [Sum 之 ComSuccessTimes], Sum(BytesQty.ComFaileTimes) AS [Sum 之 ComFaileTimes]" &
                " From BytesQty" & " GROUP BY Format$([BytesQty].[日期],'short Date'),bytesqty.RTUname" &
                " HAVING (((Format$([BytesQty].[日期],'Short Date'))=#" & Format(myDay, "yyyy/mm/dd") & "#));"
            Rs.Open(Sqlstr, , ADODB.CursorTypeEnum.adOpenStatic) '必须不用adoforwardonly 否则查不到数据
            For i = 1 To Rs.RecordCount

                GetDayBytessum = GetDayBytessum + Rs.Fields(2).Value + Rs.Fields(3).Value
                Rs.MoveNext()
            Next i
        Catch ex As Exception
        Finally
            Rs.Close()
            Rs = Nothing
        End Try
    End Function

    Sub ArrangeWindow()
        frmOnlineDTU.SetBounds(0, 0, 0, 0, Windows.Forms.BoundsSpecified.X Or Windows.Forms.BoundsSpecified.Y)
        Fview.SetBounds(0, 0, 0, 0, Windows.Forms.BoundsSpecified.X Or Windows.Forms.BoundsSpecified.Y)

        frmOnlineDTU.Visible = True
        Fview.Visible = True
        frmOnlineDTU.Parent = MainProg.MdIfmain.SplitContainer2.Panel1
        frmOnlineDTU.Width = MainProg.MdIfmain.SplitContainer2.Panel1.Width
        frmOnlineDTU.Height = MainProg.MdIfmain.SplitContainer2.Panel1.Height
        Fview.Parent = MainProg.MdIfmain.SplitContainer2.Panel2
        Fview.Width = MainProg.MdIfmain.SplitContainer2.Panel2.Width



    End Sub

    Function SaveconnstatetoDB(ByRef rname As String, ByRef act As String) As Integer
        Dim dBpath As String
        Dim databaseName As String
        Dim sqlstr As String
        Dim rs As ADODB.Recordset

        rs = New ADODB.Recordset
        Dim dt As New DataBlocks
        Try
            dBpath = My.Application.Info.DirectoryPath
            databaseName = "GPRSDATA"

            rs.let_ActiveConnection(
                "provider=microsoft.jet.oledb.4.0;data source=" & dBpath & "\" & databaseName & ".mdb")
            rs.LockType = ADODB.LockTypeEnum.adLockOptimistic


            sqlstr = "Select top 1 * from connectionstate;"
            rs.Open(sqlstr, , ADODB.CursorTypeEnum.adOpenStatic) '必须不用adoforwardonly 否则查不到数据
            rs.AddNew()

            rs.Fields("time").Value = Now
            rs.Fields("rtuname").Value = rname
            rs.Fields("action").Value = act
            rs.Update()
        Catch ex As Exception
        Finally


            rs.Close()
            rs = Nothing
        End Try
    End Function

    Function QueryconnstatefromDB(ByRef Rs As ADODB.Recordset, ByRef D As Date) As Integer
        Dim DBpath As String
        Dim DatabaseName As String
        Dim Sqlstr As String
        Try
            DBpath = My.Application.Info.DirectoryPath
            DatabaseName = "GPRSDATA"

            Rs.let_ActiveConnection(
                "provider=microsoft.jet.oledb.4.0;data source=" & DBpath & "\" & DatabaseName & ".mdb")
            Rs.LockType = ADODB.LockTypeEnum.adLockOptimistic


            Sqlstr = "Select  * from connectionstate where time>#" & D & "# and time<#" &
                     DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, D) & "# order by time desc;"

            Rs.Open(Sqlstr, , ADODB.CursorTypeEnum.adOpenStatic) '必须不用adoforwardonly 否则查不到数据

        Catch ex As Exception
        Finally

        End Try
    End Function

'Function shieldpc(ByRef outdata As Object) As Object
'	Dim key3, key1, key2, key4 As Object
'       Dim step10, step8, step6, step4, step2, count1, dataX2, y22, y1, y2, y11, dataX1, step1, step3, step5, step7, step9, step11 As Integer

'	'replace the follow key with key.txt
'       key1 = 18357
'       key2 = 17922
'       key3 = 1822
'       key4 = 50122

'       dataX2 = outdata And &HFFFF0000
'	For count1 = 0 To 3
'           dataX2 = dataX2 / 16
'	Next count1
'       dataX2 = &H1FFFF And dataX2

'       If dataX2 > 65535 Then
'           dataX2 = dataX2 - 65536
'       End If

'       dataX1 = &H1FFFF And outdata

'       If dataX1 > 65535 Then
'           dataX1 = dataX1 - 65536
'       End If

'       step1 = dataX1 Xor key2

'       step2 = dataX2 Xor key1

'       step3 = step1 + step2

'       If step3 > 65535 Then
'           step3 = step3 - 65536
'       End If

'       step4 = step3 * 16 '<< 4
'       While step4 > 65535
'           step4 = step4 - 65536
'       End While

'       step5 = step4 Mod key4


'       step6 = step5 * key3

'       While step6 > 2147483647
'           step6 = step6 - 2147483647 - 1
'       End While
'       While step6 < -2147483647
'           step6 = step6 + 2147483647 + 1
'       End While
'       step7 = dataX1 + key1
'       If step7 > 65535 Then
'           step7 = step7 - 65536
'       End If
'       step8 = step7 Mod key3

'       step9 = key4 Xor dataX2

'       step10 = step8 * step9
'       While step10 > 2147483647
'           step10 = step10 - 2147483647 - 1
'       End While

'       While step10 < -2147483647
'           step10 = step10 + 2147483647 + 1
'       End While


'       step11 = step10 Xor step6

'       shieldpc = step11

'End Function
'Function CheckDog() As Boolean
'	Dim y1, retVal, y2 As Object
'	Dim intx As Short
'	Dim outdata2, outdata, y7 As Object


'	'Creat Random to retVal From 1 to 2094967295


'	Randomize()
'       retVal = Int((2094967295 * Rnd()) + 1)
'       outdata = retVal


'	'Label8.Caption = "X =  " & Hex(outdata)
'	'Call Function ShieldPc of PC
'       y1 = Hex(shieldpc(outdata))
'	'Text3.Text = "y1 =   " & y1
'	'Call Function Lock32_Function of Lock in cdll.dll
'       y2 = Hex(Lock32_Function(outdata) And &H7FFFFFFF)
'	'Text1.Text = "y2 =   " & y2

'       If y1 = y2 Then
'           '    Text1.ForeColor = &H8000&
'           '    Text2.ForeColor = &H8000&
'           '    Text3.ForeColor = &H8000&
'           '    Text2.Text = "y1 = y2,  " & "加密锁校验正确!"
'           CheckDog = True
'       Else
'           'Text1.ForeColor = &HFF&
'           'Text2.ForeColor = &HFF&
'           'Text3.ForeColor = &HFF&
'           'Text2.Text = " y1 <> y2,  " & "Key值错误或加密狗不存在!"
'           CheckDog = True


'           'MsgBox "加密锁错误！"
'           'End

'       End If
'	If CheckDog = False Then
'		MsgBox("加密锁错误！")
'		End
'	End If


'End Function


    Sub LogFile(ByRef Message As String)
        Dim LogFile As Short
        LogFile = FreeFile()
        FileOpen(LogFile, My.Application.Info.DirectoryPath & "\MBEserver" & Format(Today, "yyyymm") & ".log",
                 OpenMode.Append)
        PrintLine(LogFile, Now & "   " & Message)
        FileClose(LogFile)
    End Sub

End Module