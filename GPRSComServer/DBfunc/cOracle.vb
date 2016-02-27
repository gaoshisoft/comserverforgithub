Option Strict Off
Option Explicit On

Imports GPRSComServer.DBfunc

Friend Class cOracleODBC
    '数据库处理模块，目标数据库 Mysql
    '主要功能： 根据给定结构向指定ODBC数据源中自动建表，向数据库中存实时及历史数据等。
    Private mvarTableCol As Object '局部复制
    Private WithEvents Cn As ADODB.Connection
    Private WithEvents Tmr As Mytimer
    Private cm As ADODB.Command
    Public Rs As ADODB.Recordset
    Private cat As ADOX.Catalog
    Private tbl As ADOX.Table
    Private cln As ADOX.Column
    Private idx As ADOX.Index
    Private DataSourceName As String
    Private fldtype As ADOX.DataTypeEnum
    'Public Command1 As New cCommand
    Public IfStart As Boolean
    'Dim CN As ADODB.Connection
    'Public Event DBerror(error As ADODB.error)
    'Public Event DBconnectsuccess()
    Public FirstTime As New iffirst


    Public Property TableCol() As Object
        Get

            'Syntax: Debug.Print X.Allstations
            TableCol = mvarTableCol
        End Get
        Set(ByVal Value As Object)
            '向属性指派对象时使用，位于 Set 语句的左边。
            'Syntax: Set x.Allstations = Form1
            mvarTableCol = Value
        End Set
    End Property

    Sub Init(ByRef DSN As String)
        DataSourceName = DSN
    End Sub

    Function ConnectToserverTest() As Boolean '真为成功
        Dim Cn As ADODB.Connection
        Cn = Me.getCn()
        If Cn.State = ADODB.ObjectStateEnum.adStateOpen Then
            ConnectToserverTest = True
        Else
            ConnectToserverTest = False
        End If
    End Function

    Public Function CreateODBCDB() As Boolean
        Dim CreateDB As Object
        '建库流程：先检查相应库是否存在没有则建，再检查相应表是否存在，没有则建，再检查相应字段，没有则建。
        cm = New ADODB.Command
        Dim station As _Itable
        Dim parameter As _IItem
        Dim cln As ADOX.Column
        Dim Rs As ADODB.Recordset
        Dim Cn As ADODB.Connection
        Cn = Me.getCn
        cat = New ADOX.Catalog '创建扩展ado对象以操作数据库对象
        cat.ActiveConnection = Cn

        Dim s As String
        Dim fldname As String
        For Each station In TableCol '先一个一个取出站点
            err.Clear()
            On Error Resume Next
            tbl = cat.Tables(station.tablename) '如果这个表不存在则出错
            If Err.Number Then
                On Error GoTo 0
                CreateTable(station, Cn)
            Else
                '------------------如果参数集合中有在数据库相应表中没有用字段体现的参数则建
                If Not station.ItemCol Is Nothing Then

                    For Each parameter In station.ItemCol
                        err.Clear()
                        On Error Resume Next
                        fldname = parameter.ItemFieldName
                        cln = tbl.Columns(fldname) '如果这个字段不存在则出错
                        If Err.Number Then
                            On Error GoTo 0
                            AddField(parameter, tbl, Cn)

                        End If
                        '            DoEvents
                    Next parameter
                End If
            End If
            '   End If
            '   DoEvents
        Next station '站点集合中的下一个站点
        On Error Resume Next

        tbl = cat.Tables("AlarmTable")
        If Err.Number Then
            On Error GoTo 0
            CreateAlarmTable(Cn)
        End If
        'UPGRADE_WARNING: 未能解析对象 CreateDB 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
        CreateDB = True

        Cn.Close()
        'UPGRADE_NOTE: 在对对象 Cn 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
        Cn = Nothing
        err.Clear()
    End Function

    Private Sub CreateAlarmTable(ByRef Cn As ADODB.Connection)
        Dim ddlcmd As String
        '--------------建报警表
        ddlcmd = "CREATE TABLE AlarmTable"
        ddlcmd = ddlcmd & "(ID  int2 auto_increment not null,primary key (ID), " &
                 "ItemID varchar(255),FieldName varchar(255), HappenTime timestamp,EndTime timestamp,Ifchecked boolean,CheckTime timestamp," &
                 "AlarmCondition varchar(255))"
        Cn.Execute(ddlcmd)

        ''CREATE TABLE Sample
        '(
        '    c1inc2  int2 auto_increment not null,
        '    primary key (c1inc2),
        '    cAtmstp timestamp,
        '    c3int1  int1,
        '    c4usgn1 int1 unsigned,
        '    c5int2  int2,
        '    c6usgn2 int2 unsigned,
        '    c7int4  int4,
        '    c8usgn4 int4 unsigned,
        '    c9int8  int8,
        '    c10usgn8    int8 unsigned,
        '    c14dec  decimal(12,4),
        '    c15flt4 float4,
        '    c17flt8 float8,
        '    c19num  numeric(15,7),
        '    c24char1    char(1),
        '    c25char1c   binary(1),
        '    c26char255  char(255),
        '    c27char255c binary(255),
        '    c32zstr2    varchar(1),
        '    c33zstr2c   varbinary(2),
        '    c34zstr255  varchar(255),
        '    c35zstr255c varbinary(255),
        '    c36date     date,
        '    c37time     time,
        '    c38tmstp    timestamp,
        '    c39note1K tinytext
        ')
    End Sub

    Public Sub Initcata(ByRef cata As String)
        Dim temp() As String
        Dim i As Integer
        Dim dire As String
        Dim j As Integer
        temp = Split(cata, "\")
        dire = ""
        For i = 1 To UBound(temp) '驱动器不管
            For j = 0 To i
                dire = dire & temp(j) & "\"
            Next j
            'UPGRADE_WARNING: Dir 有新行为。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"”
            If Dir(dire, FileAttribute.Directory) = "" Then '如果无此目录，则建
                MkDir((dire))
            End If
            dire = ""
        Next i
    End Sub

    Private Sub savetoODBCserver(ByRef station As _Itable, ByRef Cn As ADODB.Connection)
        Dim fieldstr As String = ""
        Dim para As _IItem
        'Dim cn As ADODB.Connection
        'On Error Resume Next
        Dim Sqlstr As String
        If Not station.ItemCol Is Nothing Then

            For Each para In station.ItemCol
                If para.ItemDataType = "String" Or para.ItemDataType = "Date" Then
                    'UPGRADE_WARNING: 未能解析对象 para.ItemValue 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
                    'UPGRADE_WARNING: 未能解析对象 fieldstr 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
                    fieldstr = fieldstr & "," & para.ItemFieldName & "='" & para.ItemValue & "'"
                Else
                    'UPGRADE_WARNING: 未能解析对象 para.ItemValue 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
                    'UPGRADE_WARNING: 未能解析对象 fieldstr 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
                    fieldstr = fieldstr & "," & para.ItemFieldName & "=" & para.ItemValue
                End If
            Next para
            'UPGRADE_WARNING: 未能解析对象 fieldstr 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
            Sqlstr = "Insert " & station.tablename & " set GetherTime='" & Now & "' " & fieldstr & ";"
            'Debug.Print SqlStr
            Cn.Execute(Sqlstr)
        End If
    End Sub


    Public Sub SavetoODBCserverDB()
        On Error GoTo err_Renamed
        Dim station As _Itable
        Dim Cn As ADODB.Connection
        Cn = Me.getCn
        If Cn.State = ADODB.ObjectStateEnum.adStateOpen Then
            For Each station In Me.TableCol
                If station.TableType = "历史表" Then

                    savetoODBCserver(station, Cn)
                End If
                'System.Windows.Forms.Application.DoEvents()
            Next station
            Cn.Close()
        End If
        'UPGRADE_NOTE: 在对对象 Cn 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
        Cn = Nothing
err_Renamed:
    End Sub

    Public Sub UpdateToODBCserverDB()
        On Error GoTo err_Renamed
        Dim station As _Itable
        Dim Cn As ADODB.Connection
        Cn = Me.getCn
        If Cn.State = ADODB.ObjectStateEnum.adStateOpen Then
            For Each station In Me.TableCol
                If station.TableType = "实时表" Then
                    UpdatetoODBCserverNow(station, Cn)
                End If
                'System.Windows.Forms.Application.DoEvents()
            Next station
            Cn.Close()
        End If
        'UPGRADE_NOTE: 在对对象 Cn 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
        Cn = Nothing
err_Renamed:
    End Sub

    Private Sub UpdatetoODBCserverNow(ByRef station As _Itable, ByRef Cn As ADODB.Connection)
        Dim fieldstr As String = ""
        Dim para As _IItem
        Dim Sqlstr As String

        'On Error Resume Next
        Rs = New ADODB.Recordset
        Rs.Open("select * from " & station.tablename, Cn, ADODB.CursorTypeEnum.adOpenKeyset,
                ADODB.LockTypeEnum.adLockOptimistic)
        If Rs.EOF = True Then

            Rs.AddNew()

            Rs.Fields("GetherTime").Value = Now
            Rs.Update()

        Else
            Rs.MoveFirst()
            Rs.Fields("GetherTime").Value = Now
            Rs.Update()


        End If
        For Each para In station.ItemCol
            'UPGRADE_WARNING: 未能解析对象 para.ItemValue 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
            'UPGRADE_WARNING: 未能解析对象 fieldstr 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
            fieldstr = fieldstr & "," & para.ItemFieldName & "=" & para.ItemValue
        Next para
        'UPGRADE_WARNING: 未能解析对象 fieldstr 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
        Sqlstr = "update " & station.tablename & " set GetherTime='" & Now & "' " & fieldstr & " where GetherTime='" &
                 Rs.Fields("GetherTime").Value & "';"
        Cn.Execute(Sqlstr)
        Rs.Close()
    End Sub

    Public Sub SaveAlarmTodb(ByVal ItemID As String, ByRef fldname As String, ByRef StartTime As Date,
                             ByRef EndTime As Date, ByRef Ifchecked As Boolean, ByRef CheckTime As Date,
                             ByRef AlarmCondition As String)
        Dim Sqlstr As String
        Dim Cn As ADODB.Connection
        Cn = Me.getCn
        'ddlcmd = ddlcmd & "(ID  int2 auto_increment not null,primary key (ID), " & _
        ''"ItemID varchar(255),FieldName varchar(255), HappenTime timestamp,EndTime timestamp,Ifchecked boolean," & _
        ''"AlarmValue float8"

        Sqlstr = "Insert AlarmTable set ItemId='" & ItemID & "' ,FieldName='" & fldname & "',HappenTime='" & StartTime &
                 "',EndTime='" & EndTime & "',Ifchecked=" & Ifchecked & ",CheckTime='" & CheckTime &
                 "', AlarmCondition='" & AlarmCondition & "';"
        'Debug.Print SqlStr
        Cn.Execute(Sqlstr)
        Cn.Close()
        'UPGRADE_NOTE: 在对对象 Cn 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
        Cn = Nothing
    End Sub

    Public Sub UpdateAlarmValue(ByRef AlarmId As Integer, ByRef FieldName As String, ByRef Fieldvalue As Object)
        Dim Sqlstr As String
        'UPGRADE_WARNING: 未能解析对象 Fieldvalue 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
        Sqlstr = "update alarmTable set " & FieldName & "='" & Fieldvalue & "';"
        Cn.Execute(Sqlstr)
    End Sub


    Function getCn() As ADODB.Connection
        Dim Cn As ADODB.Connection
        Cn = New ADODB.Connection
        err.Clear()
        On Error Resume Next
        Cn.Open("Provider=MSDASQL.1;Persist Security Info=False;Data Source=" & DataSourceName) _
        ' & ";User id=" & Userid & ";password=" & Password ', , , 16
        If Err.Number Then
            '  MsgBox err.Description
        End If
        getCn = Cn
    End Function


    'UPGRADE_NOTE: Class_Initialize 已升级到 Class_Initialize_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
    Private Sub Class_Initialize_Renamed()
        Rs = New ADODB.Recordset

        Tmr = frmDBsave.myTmr
    End Sub

    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub


    Private Sub CreateTable(ByVal station As _Itable, ByRef Cn As ADODB.Connection)
        Dim ddlcmd As String
        '--------------建历史数据表
        ddlcmd = "CREATE TABLE " & station.tablename
        ddlcmd = ddlcmd & "( GetherTime timestamp,primary key (GetherTime)"

        Dim parameter As _IItem
        If Not station.ItemCol Is Nothing Then

            For Each parameter In station.ItemCol
                CreateSQLField(parameter, ddlcmd)
            Next parameter
            ddlcmd = ddlcmd & ")"
            'Debug.Print ddlcmd
            Cn.Execute(ddlcmd)
        End If
        ''CREATE TABLE Sample
        '(
        '    c1inc2  int2 auto_increment not null,
        '    primary key (c1inc2),
        '    cAtmstp timestamp,
        '    c3int1  int1,
        '    c4usgn1 int1 unsigned,
        '    c5int2  int2,
        '    c6usgn2 int2 unsigned,
        '    c7int4  int4,
        '    c8usgn4 int4 unsigned,
        '    c9int8  int8,
        '    c10usgn8    int8 unsigned,
        '    c14dec  decimal(12,4),
        '    c15flt4 float4,
        '    c17flt8 float8,
        '    c19num  numeric(15,7),
        '    c24char1    char(1),
        '    c25char1c   binary(1),
        '    c26char255  char(255),
        '    c27char255c binary(255),
        '    c32zstr2    varchar(1),
        '    c33zstr2c   varbinary(2),
        '    c34zstr255  varchar(255),
        '    c35zstr255c varbinary(255),
        '    c36date     date,
        '    c37time     time,
        '    c38tmstp    timestamp,
        '    c39note1K tinytext
        ')
    End Sub

    Public Sub CreateSQLField(ByVal Item As _IItem, ByRef Ddlstr As String)
        'Set cln = New ADOX.Column
        'cln.name = Item.ItemName
        Dim Fldstr As String
        Select Case Item.ItemDataType
            Case "Boolean"
                Fldstr = "," & Item.ItemFieldName & " " & "int1 unsigned"

            Case "String"
                Fldstr = "," & Item.ItemFieldName & " " & "TINYTEXT"


            Case "Date"
                Fldstr = "," & Item.ItemFieldName & " " & "date"
            Case Else
                Fldstr = "," & Item.ItemFieldName & " " & "float8" '双精度
        End Select
        Ddlstr = Ddlstr & Fldstr
    End Sub

    Private Sub AddField(ByVal Item As _IItem, ByRef tbl As ADOX.Table, ByRef Cn As ADODB.Connection)
        'Set cln = New ADOX.Column
        'cln.name = Item.ItemName
        Dim Ddlstr As String
        Dim Fldstr As String
        'Module1.SelectedOPCItem.GetItemCanonicalType = vbDate
        Select Case Item.ItemDataType
            Case CStr(VariantType.Boolean)
                Fldstr = Item.ItemFieldName & " " & "int1 unsigned"

            Case CStr(VariantType.String)
                Fldstr = Item.ItemFieldName & " " & "VARCHAR"


            Case CStr(VariantType.Date)
                Fldstr = Item.ItemFieldName & " " & "date"
            Case Else
                Fldstr = Item.ItemFieldName & " " & "float8" '双精度
        End Select
        '-------------修改历史表
        Ddlstr = "Alter table " & tbl.name & " add column ("

        Ddlstr = Ddlstr & Fldstr & ")"

        Cn.Execute(Ddlstr)
    End Sub


    'Sub LogFile(Message As String)
    'Dim LogFile As Integer
    'LogFile = FreeFile
    'Open Me.DBpath & "\SCADA" & Format(Date, "yyyymm") & ".log" For Append As #LogFile
    'Print #LogFile, Now & "   " & Message
    'Close #LogFile
    'LogFile = FreeFile
    'Open App.Path & "\SCADA" & Format(Date, "yyyymm") & ".log" For Append As #LogFile
    'Print #LogFile, Now & "   " & Message
    'Close #LogFile
    '
    'End Sub


    Function Getmonthdaynum(ByRef Y As Integer, ByRef M As Integer) As Integer
        If M < 1 Then
            M = M + 12
        ElseIf M > 12 Then
            M = M - 12
        End If
        Select Case M

            Case 1, 3, 5, 7, 8, 10, 12
                Getmonthdaynum = 31
            Case 2
                If Y Mod 4 = 0 Then
                    Getmonthdaynum = 29
                Else
                    Getmonthdaynum = 28
                End If
            Case Else
                Getmonthdaynum = 30
        End Select
    End Function
End Class
