Option Strict Off
Option Explicit On

Imports System.Data.OleDb
Imports GPRSComServer

Namespace DBfunc
    Friend Class cManipulateSQLsvr
        Implements IManipulateDB

        '数据库处理模块，目标数据库SQLserver
        '主要功能： 根据给定结构自动建库，建表，向数据库中存实时及历史数据等。
        Private mvarTableCol As Object '局部复制

        Private DataSource As String
        Private DBpath As String
        Private Userid As String
        Private Password As String
        Private dbname As String

        'Public IfStart As Boolean



        Public Property TableCol() As Object
            Get


                TableCol = mvarTableCol
            End Get
            Set(ByVal Value As Object)

                mvarTableCol = Value
            End Set
        End Property

        Function Init(ByRef serverNodeName As String, ByRef sqldbname As String, ByRef sqlUserName As String,
                      ByRef sqlPassword As String) As Boolean '真为成功

            DataSource = serverNodeName
            dbname = sqldbname
            DBpath = "D:\"
            Userid = sqlUserName
            Password = sqlPassword
        End Function

        Function ConnectToserverTest() As Boolean '真为成功

            Dim Cnstr As String
            Cnstr = getCnstr()
            ConnectToserverTest = True
            Using cn As New OleDb.OleDbConnection(Cnstr)
                Try

                    cn.Open()
                Catch e As Exception

                    ConnectToserverTest = False
                    Throw e
                End Try

            End Using


        End Function

        Function CreatesqlsvrDB() As Boolean

            Me.CreateSQLserverDB(DataSource, dbname, "D:\", Userid, Password)
        End Function
        Public Sub CreateSQLTable(ByVal station As _Itable, ByVal cn As OleDbConnection)

            Dim cm As OleDbCommand = New OleDbCommand()
            Dim ddlcmd As String
            '--------------建历史数据表
            ddlcmd = "CREATE TABLE " & station.tablename
            ddlcmd = ddlcmd & "( GetherTime datetime,primary key (GetherTime)"

            Dim parameter As _IItem
            If Not station.ItemCol Is Nothing Then

                For Each parameter In station.ItemCol
                    CreateSQLField(parameter, ddlcmd)
                Next parameter
                ddlcmd = ddlcmd & ")"
                cm.CommandText = ddlcmd
                cm.Connection = cn
                cm.ExecuteNonQuery()
            End If


        End Sub
        Public Sub CreateSQLRTTable(ByVal station As _Itable, ByVal cn As OleDbConnection)

            Dim cm As OleDbCommand = New OleDbCommand()
            Dim ddlcmd As String
            '--------------建历史数据表
            ddlcmd = "CREATE TABLE " & station.tablename & "RT"
            ddlcmd = ddlcmd & "( GetherTime datetime,primary key (GetherTime)"

            Dim parameter As _IItem
            If Not station.ItemCol Is Nothing Then

                For Each parameter In station.ItemCol
                    CreateSQLField(parameter, ddlcmd)
                Next parameter
                ddlcmd = ddlcmd & ")"
                cm.CommandText = ddlcmd
                cm.Connection = cn
                cm.ExecuteNonQuery()
            End If


        End Sub

        Public Sub CreateSQLField(ByVal Item As _IItem, ByRef ddlstr As String)
            Dim Fldstr As String
            Select Case Item.ItemDataType
                Case "Boolean"
                    Fldstr = "," & Item.ItemFieldName & " " & "float"

                Case "String"
                    Fldstr = "," & Item.ItemFieldName & " " & "VARCHAR(50)"
                Case "Date"
                    Fldstr = "," & Item.ItemFieldName & " " & "datetime"
                Case Else
                    Fldstr = "," & Item.ItemFieldName & " " & "float" '双精度
            End Select
            ddlstr = ddlstr & Fldstr
        End Sub
        Private Sub AddsqlField(ByVal Item As _IItem, ByRef tblname As String, ByRef Cn As OleDbConnection)
            Dim Ddlstr As String
            Dim Fldstr As String
            Select Case Item.ItemDataType
                Case CStr(VariantType.Boolean)
                    Fldstr = Item.ItemFieldName & " " & "float"

                Case CStr(VariantType.String)
                    Fldstr = Item.ItemFieldName & " " & "VARCHAR(50)"


                Case CStr(VariantType.Date)
                    Fldstr = Item.ItemFieldName & " " & "datetime"
                Case Else
                    Fldstr = Item.ItemFieldName & " " & "float" '双精度
            End Select
            '-------------修改历史表
            Ddlstr = "Alter table " & tblname & " add  "

            Ddlstr = Ddlstr & Fldstr & ""
            Dim cm As OleDbCommand = New OleDbCommand(Ddlstr, Cn)
            cm.ExecuteNonQuery()
        End Sub

        Private Sub CreateAlarmTable(ByRef Cn As OleDbConnection)
            Dim ddlcmd As String
            ddlcmd = "CREATE TABLE [AlarmTable] (" & "[ID] [smallint] IDENTITY (1, 1) NOT NULL ," &
                     "[ItemID] [varchar] (255) COLLATE Chinese_PRC_CI_AS NOT NULL ," &
                     "[FieldName] [varchar] (255) COLLATE Chinese_PRC_CI_AS NOT NULL ," &
                     "[HappenTime] [datetime] NOT NULL ," & "[EndTime] [datetime] NOT NULL ," &
                     "[IfChecked] [bit] NOT NULL ," & "[CheckTime] [datetime] NOT NULL ," &
                     "[AlarmCondition] [varchar] (255) COLLATE Chinese_PRC_CI_AS NOT NULL ," &
                     "CONSTRAINT [AlarmTableIndex] PRIMARY KEY  NONCLUSTERED" & "(" & "    [id]" & ")  ON [PRIMARY]" &
                     ") ON [PRIMARY]"
            Dim cm As OleDbCommand = New OleDbCommand(ddlcmd, Cn)
            cm.ExecuteNonQuery()
        End Sub
        Private Sub CreateparainfoTable(ByRef Cn As OleDbConnection)
            Dim ddlcmd As String
            ddlcmd = "CREATE TABLE [parainfo] (" & "[ID] [smallint] IDENTITY (1, 1) NOT NULL ," &
                     "[paraname] [varchar] (255) COLLATE Chinese_PRC_CI_AS NOT NULL ," &
                     "[chinesedis] [varchar] (255) COLLATE Chinese_PRC_CI_AS NOT NULL ," &
                     "[httablename] [varchar] (255) COLLATE Chinese_PRC_CI_AS NOT NULL ," &
                     "[rttablename] [varchar] (255) COLLATE Chinese_PRC_CI_AS NOT NULL ," &
                     "[stationname] [varchar] (255) COLLATE Chinese_PRC_CI_AS NOT NULL ," &
                     "[uplimit] [real]  NULL ," &
                     "[downlimit] [real] NULL ," &
                     "CONSTRAINT [parainfoIndex] PRIMARY KEY  NONCLUSTERED" & "(" & "    [id]" & ")  ON [PRIMARY]" &
                     ") ON [PRIMARY]"
            Dim cm As OleDbCommand = New OleDbCommand(ddlcmd, Cn)
            cm.ExecuteNonQuery()
        End Sub
        Public Sub CreateSQLserverDB(ByVal DataSource As String, ByVal dbname As String, ByVal DBpath As String,
                                     Optional ByVal Userid As Object = Nothing, Optional ByVal Password As Object = Nothing)
            '建库流程：先检查相应库是否存在没有则建，再检查相应表是否存在，没有则建，再检查相应字段，没有则建。
            Dim ts As String = ""
            Dim colms As String
            Dim station As _Itable
            Dim parameter As _IItem
            Dim Cnstr As String
            Cnstr = getCnstr()
            Try
                Using cn As New OleDbConnection(Cnstr)
                    Dim cm As New OleDbCommand()
                    cn.Open()
                    Dim dt As DataTable = cn.GetSchema("Tables")
                    For Each dr As DataRow In dt.Rows
                        If ts = "" Then
                            ts = dr("table_name")
                        Else
                            ts = ts & "," & dr("table_name")
                        End If
                    Next
                    '-------------------如果在站点集合中有在数据库中没有用表体现的站点则建
                    Dim fldname As String
                    '建历史数据表
                    For Each station In TableCol '先一个一个取出站点

                        If Not ts.Contains(station.tablename) Then
                            CreateSQLTable(station, cn)
                        Else
                            '------------------如果参数集合中有在数据库相应表中没有用字段体现的参数则建
                            If Not station.ItemCol Is Nothing Then
                                '获取原表中的列名
                                Dim columns As DataTable = cn.GetSchema("Columns", {cn.Database, "", station.tablename})
                                For Each dr As DataRow In columns.Rows
                                    colms = colms & dr("Column_Name")
                                Next
                                For Each parameter In station.ItemCol
                                    fldname = parameter.ItemFieldName
                                    If Not colms.Contains(fldname) Then
                                        AddsqlField(parameter, station.tablename, cn)
                                    End If
                                Next parameter
                            End If
                        End If
                    Next station
                    '建实时数据表
                    For Each station In TableCol '先一个一个取出站点

                        If Not ts.Contains(station.tablename) Then
                            CreateSQLRTTable(station, cn)
                        Else
                            '------------------如果参数集合中有在数据库相应表中没有用字段体现的参数则建
                            If Not station.ItemCol Is Nothing Then
                                '获取原表中的列名
                                Dim columns As DataTable = cn.GetSchema("Columns", {cn.Database, "", station.tablename & "RT"})
                                For Each dr As DataRow In columns.Rows
                                    colms = colms & dr("Column_Name")
                                Next
                                For Each parameter In station.ItemCol
                                    fldname = parameter.ItemFieldName
                                    If Not colms.Contains(fldname) Then
                                        AddsqlField(parameter, station.tablename & "RT", cn)
                                    End If
                                Next parameter
                            End If
                        End If
                    Next station
                    If Not ts.Contains("AlarmTable") Then
                        CreateAlarmTable(cn)
                    End If
                    If Not ts.Contains("parainfo") Then
                        CreateparainfoTable(cn)
                    End If
                End Using
            Catch e As Exception
            End Try
        End Sub



        Public Sub SavetoSqLserver(ByVal savetime As DateTime, ByRef station As _Itable, ByRef Cn As OleDbConnection)

            Dim para As _IItem

            Dim cm As OleDbCommand

            Dim Sqlstr As String
            Dim F As String
            Dim V As String
            Dim Vstr As String
            cm = New OleDbCommand()
            If Not station.ItemCol Is Nothing Then
                F = "GetherTime"
                V = "'" & CStr(savetime) & "'"

                For Each para In station.ItemCol
                    If para.ItemDataType = "String" Or para.ItemDataType = "Date" Then
                        Vstr = "'" & CDbl(para.ItemValue) & "'"
                    Else
                        Vstr = CStr(CDbl(para.ItemValue))
                    End If

                    If F = "" Then
                        F = para.ItemFieldName
                    Else
                        F = F & "," & para.ItemFieldName
                    End If
                    If V = "" Then
                        V = Vstr
                    Else
                        V = V & "," & Vstr
                    End If

                Next para
                Sqlstr = "Insert into " & station.tablename & " (" & F & ")" & " values (" & V & ");"
                cm.CommandText = Sqlstr
                cm.Connection = Cn
                cm.ExecuteNonQuery()
            End If
        End Sub
        Sub saveparainfotodb() Implements IManipulateDB.SaveParainfotoDB
            Dim cm As OleDbCommand = New OleDbCommand()

            'Dim fieldstr As String = ""
            Dim para As CItem
            Dim T As Ctable
            Dim Sqlstr As String
            Using cn As OleDbConnection = New OleDbConnection(getCnstr)
                cn.Open()

                cm.CommandText = "delete  from parainfo"
                cm.Connection = cn
                cm.ExecuteNonQuery()
                cm.CommandText = "select top 1 * from parainfo"
                Dim da As New OleDbDataAdapter(cm)
                Dim ds As New DataTable
                da.Fill(ds)
                For Each T In Me.TableCol
                    For Each para In T.ItemCol
                        Dim dr As DataRow = ds.NewRow()
                        dr("paraname") = para.ItemName
                        dr("chinesedis") = para.ChineseDis
                        dr("httablename") = T.Tablename
                        dr("rttablename") = T.Tablename & "rt"
                        dr("stationname") = T.staName
                        dr("uplimit") = para.convertedUP
                        dr("downlimit") = para.ConvertedDown

                        'fieldstr = para.ItemName & "," & para.ChineseDis & ", " & T.Tablename & "," & T.Tablename & "RT," & T.staName &
                        ' "," & para.convertedUP & "," & para.ConvertedDown

                        'Sqlstr = "Insert into parainfo values (" & fieldstr & ");"
                        'cm.CommandText = Sqlstr
                        ds.Rows.Add(dr)
                        Dim cb As OleDbCommandBuilder = New OleDbCommandBuilder(da)
                        cb.GetUpdateCommand()
                        da.Update(ds)

                    Next

                Next


            End Using
        End Sub

        Private Sub UpdatetoSQLserver(ByRef station As _Itable, ByRef Cn As OleDbConnection)
            Dim para As _IItem
            Dim dr As DataRow
            If Not station.ItemCol Is Nothing Then
                Dim rttblname As String = station.tablename & "rt"
                Dim ds As New DataSet
                Dim da As OleDbDataAdapter = New OleDbDataAdapter("select * from " & rttblname, Cn)

                da.Fill(ds, rttblname)
                If ds.Tables(station.tablename).Rows.Count = 0 Then

                    dr = ds.Tables(rttblname).NewRow
                    ds.Tables(rttblname).Rows.Add(dr)
                Else
                    dr = ds.Tables(rttblname).Rows(0)
                End If
                dr("GetherTime") = Now

                For Each para In station.ItemCol
                    dr(para.ItemFieldName) = para.ItemValue
                Next para

                Dim cb As New OleDbCommandBuilder
                cb = New OleDbCommandBuilder(da)

                cb.GetUpdateCommand()
                'Dim s As String = da.InsertCommand.CommandText
                da.Update(ds, rttblname)

            End If
        End Sub


        Public Sub SavetoSQLserverDB(ByVal Savetime As DateTime)

            Dim Station As _Itable
            Try
                Using Cn As OleDbConnection = New OleDbConnection(getCnstr)
                    Cn.Open()
                    If Cn.State = ConnectionState.Open Then
                        If Not Me.TableCol Is Nothing Then
                            For Each Station In Me.TableCol
                                'If station.TableType = "历史表" Then
                                SavetoSqLserver(Savetime, Station, Cn)
                                'End If
                            Next Station
                        End If
                    End If
                End Using
            Catch e As Exception
            End Try
        End Sub

        Public Sub UpdatetoSQLserverDB()
            Dim Station As _Itable

            Try
                Using Cn As OleDbConnection = New OleDbConnection(getCnstr)
                    Cn.Open()
                    If Not Me.TableCol Is Nothing Then
                        For Each Station In Me.TableCol
                            'If station.TableType = "实时表" Then
                            UpdatetoSQLserver(Station, Cn)
                            'End If
                        Next Station
                    End If
                End Using
            Catch e As Exception
            End Try
        End Sub



        Function GetCnstr() As String
            Dim Localname As String
            Localname = System.Net.Dns.GetHostName
            If Len(Userid) <> 0 Then
                If Len(Password) <> 0 Then

                    GetCnstr = "Provider=SQLOLEDB;Data Source=" & Localname & "\" & DataSource & ";User Id=" & Userid & "; Password=" & Password & ";Initial Catalog=" & dbname

                Else

                    'getCnstriing = "Provider=SQLOLEDB;Data Source=GSP-PC\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=dataPJCNG"
                End If
            Else

                GetCnstr = "Provider=SQLOLEDB;Data Source=" & Localname & "\" & DataSource & ";" & "Integrated Security=SSPI ;Initial Catalog=" & dbname
            End If

        End Function




        Public Sub New()
            MyBase.New()

        End Sub




        Public Sub SaveAlarmTodb(ByVal ItemID As String, ByRef fldname As String, ByRef StartTime As Date,
                                 ByRef EndTime As Date, ByRef Ifchecked As Boolean, ByRef CheckTime As Date,
                                 ByRef AlarmCondition As String)
            Dim Sqlstr As String
            Try
                Using Cn As OleDbConnection = New OleDbConnection(getCnstr)
                    Sqlstr =
                        "Insert into AlarmTable (ItemId ,FieldName,HappenTime,EndTime,Ifchecked,CheckTime,AlarmCondition) values( '" &
                        ItemID & " ','" & fldname & "','" & StartTime & "','" & EndTime & "'," & IIf(Ifchecked = True, 1, 0) & ",'" &
                        CheckTime & "','" & AlarmCondition & "');"
                    Dim cm As OleDbCommand = New OleDbCommand(Sqlstr, Cn)

                    cm.ExecuteNonQuery()
                End Using
            Catch e As Exception
            End Try
        End Sub






        Public Function ConnectToserverTest1() As Boolean Implements IManipulateDB.ConnectToserverTest
            Return Me.ConnectToserverTest
        End Function

        Public Function CreatesqlDB() As Boolean Implements IManipulateDB.CreateDB
            CreatesqlsvrDB()
            Return True
        End Function

        Public Sub Init1(ByRef svrIPstr As String, ByVal instname As String, ByVal usr As String, ByVal pass As String) Implements IManipulateDB.Init
            Me.Init(svrIPstr, instname, usr, pass)
        End Sub

        Public Sub SaveAlarmTodb1(ByVal ItemID As String, ByRef fldname As String, ByRef StartTime As Date, ByRef EndTime As Date, ByRef Ifchecked As Boolean, ByRef CheckTime As Date, ByRef AlarmCondition As String) Implements IManipulateDB.SaveAlarmTodb
            Me.SaveAlarmTodb(ItemID, fldname, StartTime, EndTime, Ifchecked, CheckTime, AlarmCondition)
        End Sub

        Public Sub SavetosqlserverHTDB(ByVal savetime As DateTime) Implements IManipulateDB.SavetoHTDB
            Me.SavetoSQLserverDB(savetime)
        End Sub

        Public Property TableCol1 As Object Implements IManipulateDB.TableCol
            Get
                Return Me.TableCol
            End Get
            Set(ByVal value As Object)
                Me.TableCol = value
            End Set
        End Property

        Public Sub UpdateTosqlserverRTDB() Implements IManipulateDB.UpdateToRTDB
            Me.UpdatetoSQLserverDB()
        End Sub
    End Class
End Namespace