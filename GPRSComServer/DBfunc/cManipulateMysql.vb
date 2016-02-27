Option Strict Off
Option Explicit On

Imports GPRSComServer.DBfunc
Imports MySql.Data.MySqlClient

Friend Interface IManipulateDB
    Property TableCol() As Object

    Sub Init(ByRef svrIPstr As String, ByVal instname As String, ByVal usr As String, ByVal pass As String)
    Function ConnectToserverTest() As Boolean '��Ϊ�ɹ�
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

    '���ݿ⴦��ģ�飬Ŀ�����ݿ� Mysql
    '��Ҫ���ܣ� ���ݸ����ṹ��ָ��ODBC����Դ���Զ����������ݿ��д�ʵʱ����ʷ���ݵȡ�
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

    Public Function ConnectToserverTest() As Boolean Implements IManipulateDB.ConnectToserverTest '��Ϊ�ɹ�


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

        '�������̣��ȼ����Ӧ���Ƿ����û���򽨣��ټ����Ӧ���Ƿ���ڣ�û���򽨣��ټ����Ӧ�ֶΣ�û���򽨡�
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

                '-------------------�����վ�㼯�����������ݿ���û���ñ����ֵ�վ����
                Dim fldname As String
                '����ʷ���ݱ�
                For Each station In TableCol '��һ��һ��ȡ��վ��

                    If Not ts.Contains(station.tablename.ToLower) Then
                        CreateTable(station, cn)
                    Else
                        '------------------��������������������ݿ���Ӧ����û�����ֶ����ֵĲ�����
                        If Not station.ItemCol Is Nothing Then
                            '��ȡԭ���е�����
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
                '��ʵʱ���ݱ�
                For Each station In TableCol '��һ��һ��ȡ��վ��
                    Dim TN As String = (station.tablename & "rt").ToLower
                    If Not ts.Contains(TN) Then
                        CreateRTTable(station, cn)
                    Else
                        '------------------��������������������ݿ���Ӧ����û�����ֶ����ֵĲ�����
                        If Not station.ItemCol Is Nothing Then
                            '��ȡԭ���е�����
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
        '--------------��������
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
        '--------------��������Ϣ��
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
        '--------------��������Ϣ��
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
        '�˴�ԭ���õ��� dataadapter ����fill datatable Ȼ�����updatecommand���������£�����֪ʲôԭ��
        '�����ַ������º�������frmdbsave�����ϵ����������ʱ����ִ�С�show tables �� ���ʱ�᷵�ؿձ�����޷��������ݿ��û�취�ĳ�insert��䡣�ܽ᣺mysql��������ʹ��sql���

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
        '--------------����ʷ���ݱ�
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
        '���ڴ����ݱ���ʵʱ���ݽ������ٶȿ죬����Ӳ��
        Dim cm As MySqlCommand = New MySqlCommand()

        Dim ddlcmd As String
        '--------------��ʵʱ���ݱ�
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
                Fldstr = "," & Item.ItemFieldName & " " & "float8" '˫����
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
                Fldstr = Item.ItemFieldName & " " & "float8" '˫����
        End Select
        '-------------�޸���ʷ��
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