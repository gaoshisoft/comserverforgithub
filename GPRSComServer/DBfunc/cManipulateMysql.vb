Option Strict Off
Option Explicit On

Imports GPRSComServer.DBfunc
Imports MySql.Data.MySqlClient

Friend Interface IManipulateDB
    Property TableCol() As Object

    Sub Init(ByRef svrIPstr As String, ByVal instname As String, ByVal usr As String, ByVal pass As String)
    Function ConnectToserverTest() As Boolean '真为成功
    Function CreateDB() As Boolean
    Sub SavetoHTDB(ByVal Savetime As DateTime)
    Sub UpdateToRTDB()
    Sub SaveParainfotoDB()
    Sub SaveAlarmTodb(ByVal ItemID As String, ByRef fldname As String, ByRef StartTime As Date,
                             ByRef EndTime As Date, ByRef Ifchecked As Boolean, ByRef CheckTime As Date,
                             ByRef AlarmCondition As String)
End Interface

Friend Class cManipulateMysqlDB
    Implements IManipulateDB

    '数据库处理模块，目标数据库 Mysql
    '主要功能： 根据给定结构向指定ODBC数据源中自动建表，向数据库中存实时及历史数据等。
    Dim cnb As MySqlConnectionStringBuilder = New MySqlConnectionStringBuilder
    Private mvarTableCol As Object 
    Public FirstTime As New iffirst


    Public Property TableCol() As Object Implements IManipulateDB.TableCol
        Get
            TableCol = mvarTableCol
        End Get
        Set(ByVal Value As Object)
            mvarTableCol = Value
        End Set
    End Property

    Public Sub Init(ByRef svrIPstr As String, ByVal instname As String, ByVal usr As String, ByVal pass As String) Implements IManipulateDB.Init
        'DBIpstr = IPstr

        cnb.UserID = usr
        cnb.Database = instname
        cnb.Password = pass
        cnb.Server = svrIPstr
    End Sub

    Public Function ConnectToserverTest() As Boolean Implements IManipulateDB.ConnectToserverTest '真为成功


        ConnectToserverTest = True
        Using Cn As MySqlConnection = New MySqlConnection(getCnstr)
            Try

                Cn.Open()
            Catch e As Exception

                ConnectToserverTest = False
                Throw e
            End Try
        End Using
    End Function

    Public Function CreateMysqlDB() As Boolean Implements IManipulateDB.CreateDB

        '建库流程：先检查相应库是否存在没有则建，再检查相应表是否存在，没有则建，再检查相应字段，没有则建。
        Dim ts As String = ""
        Dim colms As String
        Dim station As _Itable
        Dim parameter As _IItem
        Dim Cnstr As String
        Dim cn As MySqlConnection
        Cnstr = getCnstr()

        Try
            cn = New MySqlConnection(Cnstr)
            Using cn

                Dim cm As New MySqlCommand()
                cn.Open()
                'cm.CommandText = "select table_name from information_schema.tables where table_schema='" + cn.Database + "' and table_type='base table'"
                cm.CommandText = "show tables from zgzy;"
                cm.Connection = cn
                Dim dt As New DataTable
                Dim Da1 As MySqlDataAdapter = New MySqlDataAdapter(cm)
                Da1.Fill(dt)
                For i As Int16 = 0 To dt.Rows.Count - 1
                    If ts = "" Then
                        ts = dt.Rows(i)(0).ToString
                    Else
                        ts = ts & "," & dt.Rows(i)(0).ToString
                    End If
                Next

                '-------------------如果在站点集合中有在数据库中没有用表体现的站点则建
                Dim fldname As String
                '建历史数据表
                For Each station In TableCol '先一个一个取出站点

                    If Not ts.Contains(station.tablename.ToLower) Then
                        CreateTable(station, cn)
                    Else
                        '------------------如果参数集合中有在数据库相应表中没有用字段体现的参数则建
                        If Not station.ItemCol Is Nothing Then
                            '获取原表中的列名
                            Dim columns As New DataTable '= cn.GetSchema("Columns", {cn.Database.ToLower, "", station.tablename.ToLower})
                            Dim da As MySqlDataAdapter = New MySqlDataAdapter(New MySqlCommand("select * from " & station.tablename & " limit 1;", cn))

                            da.Fill(columns)
                            colms = ""
                            For Each dr As DataColumn In columns.Columns
                                colms = colms & dr.ColumnName
                            Next
                            For Each parameter In station.ItemCol
                                fldname = parameter.ItemFieldName
                                If Not colms.Contains(fldname) Then
                                    AddField(parameter, station.tablename, cn)
                                End If
                            Next parameter
                        End If
                    End If
                Next station
                '建实时数据表
                For Each station In TableCol '先一个一个取出站点
                    Dim TN As String = (station.tablename & "rt").ToLower
                    If Not ts.Contains(TN) Then
                        CreateRTTable(station, cn)
                    Else
                        '------------------如果参数集合中有在数据库相应表中没有用字段体现的参数则建
                        If Not station.ItemCol Is Nothing Then
                            '获取原表中的列名
                            Dim columns As New DataTable ' = cn.GetSchema("Columns", {cn.Database.ToLower, "", (station.tablename & "RT").ToLower})
                            Dim da As MySqlDataAdapter = New MySqlDataAdapter(New MySqlCommand("select * from " & TN & " limit 1;", cn))

                            da.Fill(columns)
                            colms = ""
                            For Each dr As DataColumn In columns.Columns
                                colms = colms & dr.ColumnName
                            Next
                            For Each parameter In station.ItemCol
                                fldname = parameter.ItemFieldName
                                If Not colms.Contains(fldname) Then
                                    AddField(parameter, TN, cn)
                                End If
                            Next parameter
                        End If
                    End If
                Next station
              
                If Not ts.Contains("parainfo") Then
                    CreateparainfoTable(cn)
                End If
              
            End Using
        Catch e As Exception
            Throw e
        Finally
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try

    End Function

    Private Sub CreateAlarmTable(ByRef Cn As MySqlConnection)
        Dim cm As MySqlCommand = New MySqlCommand()

        Dim ddlcmd As String
        '--------------建报警表
        ddlcmd = "CREATE TABLE AlarmTable"
        ddlcmd = ddlcmd & "(ID  int2 auto_increment not null,primary key (ID), " &
                 "ItemID varchar(255),paraName varchar(255),StationName varchar(255), HappenTime timestamp,AlarmType varchar(255),Alarminfo varchar(255),Ifchecked boolean,CheckTime timestamp,checkuser varchar(255),checkinfo varchar(255)" &
                 ") ENGINE=MyISAM DEFAULT CHARSET=utf8;"
        cm.Connection = Cn
        cm.CommandText = ddlcmd
        cm.ExecuteNonQuery()


    End Sub
    Private Sub CreateparainfoTable(ByRef Cn As MySqlConnection)
        Dim cm As MySqlCommand = New MySqlCommand()

        Dim ddlcmd As String
        '--------------建参数信息表
        ddlcmd = "DROP TABLE IF EXISTS parainfo;"

        ddlcmd = ddlcmd & "CREATE TABLE parainfo"
        ddlcmd = ddlcmd & " (ID int(11) NOT NULL auto_increment, ParaName text NOT NULL, ChineseDis text,HandleExp text,UnitStr text, HTTableName text NOT NULL, RTTableName text NOT NULL, StationName text, UpLimit float  NULL," &
            "DownLimit float NULL, PRIMARY KEY(ID)) ENGINE=MyISAM DEFAULT CHARSET=utf8;" 'IfAlarmHH boolean,IfAlarmH boolean,IfAlarmL boolean,IfAlarmLL boolean, AlarmHH float  NULL,AlarmH float NULL,AlarmL float NULL,AlarmLL float
        cm.Connection = Cn
        cm.CommandText = ddlcmd
        cm.ExecuteNonQuery()
    End Sub
    Private Sub CreateAlarmSetupTable(ByRef Cn As MySqlConnection)
        Dim cm As MySqlCommand = New MySqlCommand()

        Dim ddlcmd As String
        '--------------建参数信息表
        ddlcmd = "DROP TABLE IF EXISTS AlarmSetup;"

        ddlcmd = ddlcmd & "CREATE TABLE AlarmSetup"
        ddlcmd = ddlcmd & " (ID int(11) NOT NULL auto_increment, ParaName text NOT NULL, ChineseDis text,HandleExp text, HTTableName text NOT NULL,  StationName text," &
            " IfAlarmHH boolean,IfAlarmH boolean,IfAlarmL boolean,IfAlarmLL boolean, AlarmHH float  NULL,AlarmH float NULL,AlarmL float NULL,AlarmLL float, PRIMARY KEY(ID)) ENGINE=MyISAM DEFAULT CHARSET=utf8;"
        cm.Connection = Cn
        cm.CommandText = ddlcmd
        cm.ExecuteNonQuery()
    End Sub
    Sub saveparainfotodb() Implements IManipulateDB.SaveParainfotoDB
        Dim cm As MySqlCommand = New MySqlCommand()

        Dim fieldstr As String = ""
        Dim para As CItem
        Dim T As Ctable
        Dim Sqlstr As String
     
        Using cn As MySqlConnection = New MySqlConnection(getCnstr)
            cn.Open()



            cm.Connection = cn
           
            cm.CommandText = "delete from parainfo"
            cm.ExecuteNonQuery()
            For Each T In Me.TableCol
                For Each para In T.ItemCol

                   
                    fieldstr = "paraname='" & para.ItemName & "', chinesedis='" & para.ChineseDis & "',HandleExp='" & para.HandleExpretion & "',UnitStr='" & para.UnitStr & "', HTtableName='" & T.Tablename & "', RTtableName='" & T.Tablename & "RT', StationName='" & T.staName &
                     "', Uplimit=" & para.Uplimit & ", DownLimit=" & para.DownLimit
                    Sqlstr = "insert parainfo set " & fieldstr & ";"

                    cm.CommandText = Sqlstr

                    cm.ExecuteNonQuery()
                Next

            Next


        End Using

    End Sub

    Private Sub savetoMysqlserver(ByRef station As _Itable, ByVal savetime As Date, ByRef Cn As MySqlConnection)
        Dim cm As MySqlCommand = New MySqlCommand()

        Dim fieldstr As String = ""
        Dim para As _IItem

        Dim Sqlstr As String
        If Not station.ItemCol Is Nothing Then

            For Each para In station.ItemCol
                If para.ItemDataType = "String" Or para.ItemDataType = "Date" Then
                    fieldstr = fieldstr & "," & para.ItemFieldName & "='" & para.ItemValue & "'"
                Else
                    fieldstr = fieldstr & "," & para.ItemFieldName & "=" & para.ItemValue
                End If
            Next para
            Sqlstr = "Insert " & station.tablename & " set GetherTime='" & savetime & "' " & fieldstr & ";"

            cm.CommandText = Sqlstr
            cm.Connection = Cn
            cm.ExecuteNonQuery()

        End If
    End Sub


    Public Sub SavetoMysqlserverDB(ByVal Savetime As DateTime) Implements IManipulateDB.SavetoHTDB

        Dim station As _Itable
        'Dim Cn AsMysqlconnection
        'Dim SaveTime As Date
        Using cn As MySqlConnection = New MySqlConnection(getCnstr)
            cn.Open()

            If cn.State = ConnectionState.Open Then
                If Not Me.TableCol Is Nothing Then
                    For Each station In Me.TableCol
                        savetoMysqlserver(station, savetime, cn)
                    Next station
                End If
            End If

        End Using
    End Sub

    Public Sub UpdateToMysqlserverDB() Implements IManipulateDB.UpdateToRTDB

        Dim station As _Itable
        Using cn As MySqlConnection = New MySqlConnection(getCnstr)
            cn.Open()
            If cn.State = ConnectionState.Open Then
                For Each station In Me.TableCol
                    UpdatetoMysqlserverRTtbl(station, cn)
                Next station

            End If
        End Using
    End Sub

    Private Sub UpdatetoMysqlserverRTtbl(ByRef station As _Itable, ByRef Cn As MySqlConnection)
        '此处原来用的是 dataadapter 对象，fill datatable 然后调用updatecommand方法来更新，但不知什么原因，
        '用这种方法更新后，马上在frmdbsave窗体上点击“创建表”时，在执行“show tables “ 语句时会返回空表。造成无法创建数据库表。没办法改成insert语句。总结：mysql操作尽量使用sql语句

        Dim para As _IItem
        Dim dr As DataRow
        If Not station.ItemCol Is Nothing Then
            Dim rttblname As String = station.tablename & "rt"
            Dim dt As New DataTable
            Dim cm As MySqlCommand = New MySqlCommand("delete from " & rttblname & ";", Cn)
            cm.ExecuteNonQuery()
            Dim fieldstr As String = ""

            Dim Sqlstr As String
            If Not station.ItemCol Is Nothing Then

                For Each para In station.ItemCol
                    If para.ItemDataType = "String" Or para.ItemDataType = "Date" Then
                        fieldstr = fieldstr & "," & para.ItemFieldName & "='" & para.ItemValue & "'"
                    Else
                        fieldstr = fieldstr & "," & para.ItemFieldName & "=" & para.ItemValue
                    End If
                Next para
                Sqlstr = "Insert " & rttblname & " set GetherTime='" & Now & "' " & fieldstr & ";"

                cm.CommandText = Sqlstr

                cm.ExecuteNonQuery()

            End If

        End If

    End Sub

    Public Sub SaveAlarmTodb(ByVal ItemID As String, ByRef fldname As String, ByRef StartTime As Date,
                             ByRef EndTime As Date, ByRef Ifchecked As Boolean, ByRef CheckTime As Date,
                             ByRef AlarmCondition As String) Implements IManipulateDB.SaveAlarmTodb
        Dim cm As MySqlCommand = New MySqlCommand()
        Dim Sqlstr As String
        Using cn As MySqlConnection = New MySqlConnection(getCnstr)
            cn.Open()
            If cn.State = ConnectionState.Open Then
                Sqlstr = "Insert AlarmTable set ItemId='" & ItemID & "' ,FieldName='" & fldname & "',HappenTime='" & StartTime &
                         "',EndTime='" & EndTime & "',Ifchecked=" & Ifchecked & ",CheckTime='" & CheckTime &
                         "', AlarmCondition='" & AlarmCondition & "';"
                cm.Connection = cn
                cm.CommandText = Sqlstr
                cm.ExecuteNonQuery()
            End If
        End Using
    End Sub



    Function getCnstr() As String

        'cnb.UserID = "root"
        'cnb.Database = "Mysql"
        'cnb.Password = "gsp790201"
        'cnb.Server = "127.0.0.1"
        cnb.Port = "3306"
        cnb.CharacterSet = "utf8"

        getCnstr = cnb.ConnectionString
    End Function
    Public Sub New()
        MyBase.New()

    End Sub

    Private Sub CreateTable(ByVal station As _Itable, ByRef Cn As MySqlConnection)
        Dim cm As MySqlCommand = New MySqlCommand()

        Dim ddlcmd As String
        '--------------建历史数据表
        ddlcmd = "CREATE TABLE " & station.tablename
        ddlcmd = ddlcmd & "( GetherTime timestamp,primary key (GetherTime)"

        Dim parameter As _IItem
        If Not station.ItemCol Is Nothing Then

            For Each parameter In station.ItemCol
                CreateField(parameter, ddlcmd)
            Next parameter
            ddlcmd = ddlcmd & ") ENGINE=MyISAM DEFAULT CHARSET=utf8;"
            'Debug.Print ddlcmd
            cm.CommandText = ddlcmd

            cm.Connection = Cn
            cm.ExecuteNonQuery()
        End If

    End Sub
    Private Sub CreateRTTable(ByVal station As _Itable, ByRef Cn As MySqlConnection)
        '用内存数据表做实时数据交换，速度快，不伤硬盘
        Dim cm As MySqlCommand = New MySqlCommand()

        Dim ddlcmd As String
        '--------------建实时数据表
        ddlcmd = "CREATE TABLE " & station.tablename & "RT"
        ddlcmd = ddlcmd & "( GetherTime timestamp,primary key (GetherTime)"

        Dim parameter As _IItem
        If Not station.ItemCol Is Nothing Then

            For Each parameter In station.ItemCol
                CreateField(parameter, ddlcmd)
            Next parameter
            ddlcmd = ddlcmd & ")ENGINE=MEMORY DEFAULT CHARSET=utf8 MAX_ROWS=100;"
            'Debug.Print ddlcmd
            cm.CommandText = ddlcmd

            cm.Connection = Cn
            cm.ExecuteNonQuery()
        End If

    End Sub

    Private Sub CreateField(ByVal Item As _IItem, ByRef Ddlstr As String)

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

    Private Sub AddField(ByVal Item As _IItem, ByRef tblname As String, ByRef Cn As MySqlConnection)
        Dim cm As MySqlCommand = New MySqlCommand()

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
        Ddlstr = "Alter table " & tblname & " add column ("

        Ddlstr = Ddlstr & Fldstr & ");"
        cm.CommandText = Ddlstr
        cm.Connection = Cn
        cm.ExecuteNonQuery()
    End Sub





   
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class