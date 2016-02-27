Option Strict Off
Option Explicit On

Imports System.Collections.Generic

Namespace DBfunc

    Friend Class DBconnect
        Public Enum DataBasetype
            Mysql5
            SQlserver2008
            Access
        End Enum
        Public Enum Tbtype
            实时表
            历史表

        End Enum

        Private mvarDBname As String '局部复制
        Private mvarUserName As String '局部复制
        Private mvarPassword As String '局部复制
        Public ConnectName As String
        Public SQLServerName As String
        Public AccessDbpath As String
        Public SaveCycle As Integer
        Public UpdateCycle As Integer
        Private mCol As Collection
        Private MysqlDBO As cManipulateMysqlDB
        Private SQLDBO As cManipulateSQLsvr
        Private AccessDBO As CManipulateAccess
        Public MyDBO As IManipulateDB

        Public WithEvents Mytimer As System.Timers.Timer = New Timers.Timer
        Public FirstTime As New iffirst
        '保持属性值的局部变量

        Private mvarConnectType As DataBasetype  '局部复制
        '保持属性值的局部变量
        Private mvarifstart As Boolean '局部复制

        Public Function Init(ByVal ConnectType As String, ByVal serverName As String, ByVal Connname As String,
                             ByVal dbname As String, ByVal UserName As String, ByVal Password As String,
                             ByVal SaveCycle As Integer, ByVal UpdateCycle As Integer,
                             ByVal IfStart As Boolean, ByVal AccessDbpath As String, Optional ByVal sKey As String = "") _
            As DBconnect
            '创建新对象
            Connname = "MyDBconn"



            Me.SQLServerName = serverName
            Dim dt As DataBasetype
            dt = CType([Enum].Parse(GetType(DataBasetype), ConnectType), DataBasetype)


            '设置传入方法的属性
            Me.sqldbname = dbname
            Me.UserName = UserName
            Me.Password = Password
            'Me.MysqlDBinfo = MysqlDatasourceName
            Me.ConnectName = Connname
            Me.SaveCycle = SaveCycle
            Me.UpdateCycle = UpdateCycle
            Me.AccessDbpath = AccessDbpath

            '初始化参数
            Select Case ConnectType
                Case DataBasetype.Access.ToString
                    AccessDBO = New CManipulateAccess
                    MyDBO = AccessDBO
                    AccessDBO.TableCol = mCol
                    AccessDBO.Init((Me.AccessDbpath))
                Case DataBasetype.Mysql5.ToString
                    MysqlDBO = New cManipulateMysqlDB
                    MyDBO = MysqlDBO
                    MysqlDBO.TableCol = mCol
                    MysqlDBO.Init(Me.SQLServerName, Me.sqldbname, Me.UserName, Me.Password)
                Case DataBasetype.SQlserver2008.ToString
                    SQLDBO = New cManipulateSQLsvr
                    MyDBO = SQLDBO
                    SQLDBO.TableCol = mCol
                    SQLDBO.Init((Me.SQLServerName), (Me.sqldbname), (Me.UserName), (Me.Password))
            End Select
            Me.DBType = dt


            '是否启动必须最后设定,因为它将引发创建数据库的操作
            Me.IfStart = IfStart

            '返回已创建的对象
            Init = Me
            Mytimer.Interval = 100
            Mytimer.AutoReset = True
            Mytimer.Start()
        End Function

        ReadOnly Property TableCol() As Collection
            Get
                TableCol = mCol
            End Get
        End Property


        Public Property IfStart() As Boolean
            Get

                'Syntax: Debug.Print X.ifstart
                IfStart = mvarifstart
            End Get
            Set(ByVal Value As Boolean)

                'Syntax: X.ifstart = 5
                mvarifstart = Value
                If Value = True Then
                    'If CreateDB() = True Then
                    mvarifstart = True
                Else
                    mvarifstart = False
                    'End If
                End If
            End Set
        End Property


        Public Property DBType() As DataBasetype
            Get

                'Syntax: Debug.Print X.ConnectType
                DBType = mvarConnectType
            End Get
            Set(ByVal Value As DataBasetype)

                'Syntax: X.ConnectType = 5
                mvarConnectType = Value
                Select Case mvarConnectType
                    Case DataBasetype.Access

                        AccessDBO = New CManipulateAccess
                        MyDBO = AccessDBO
                        AccessDBO.TableCol = mCol
                        AccessDBO.Init((Me.AccessDbpath))
                    Case DataBasetype.Mysql5
                        MysqlDBO = New cManipulateMysqlDB
                        MyDBO = MysqlDBO
                        MysqlDBO.TableCol = mCol
                        MysqlDBO.Init(Me.SQLServerName, Me.sqldbname, Me.UserName, Me.Password)
                    Case DataBasetype.SQlserver2008
                        SQLDBO = New cManipulateSQLsvr
                        MyDBO = SQLDBO
                        SQLDBO.TableCol = mCol
                        SQLDBO.Init((Me.SQLServerName), (Me.sqldbname), (Me.UserName), (Me.Password))
                End Select
            End Set
        End Property


        'Public Property MysqlDBinfo() As String
        '    Get

        '        'Syntax: Debug.Print X.ODBCDatasourceName
        '        MysqlDBinfo = mvarODBCDatasourceName
        '    End Get
        '    Set(ByVal Value As String)

        '        'Syntax: X.ODBCDatasourceName = 5
        '        mvarODBCDatasourceName = Value
        '        '    Me.ODBCDBO.DataSource = vData
        '    End Set
        'End Property


        Public Property Password() As String
            Get

                'Syntax: Debug.Print X.Password
                Password = mvarPassword
            End Get
            Set(ByVal Value As String)

                'Syntax: X.Password = 5
                mvarPassword = Value
            End Set
        End Property


        Public Property UserName() As String
            Get

                'Syntax: Debug.Print X.Username
                UserName = mvarUserName
            End Get
            Set(ByVal Value As String)

                'Syntax: X.Username = 5
                mvarUserName = Value
            End Set
        End Property


        Public Property sqldbname() As String
            Get

                'Syntax: Debug.Print X.DBname
                sqldbname = mvarDBname
            End Get
            Set(ByVal Value As String)

                'Syntax: X.DBname = 5
                mvarDBname = Value
            End Set
        End Property

        Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Ctable
            Get

                Item = mCol.Item(vntIndexKey)
            End Get
        End Property


        Public ReadOnly Property Count() As Integer
            Get
                Count = mCol.Count()
            End Get
        End Property




        Public Function GetEnumerator() As System.Collections.IEnumerator
            GetEnumerator = mCol.GetEnumerator
        End Function


        Public Function Addtbl(ByRef tablename As String, ByRef Fields As String, ByRef StaName As String, Optional ByRef sKey As String = "") As Ctable
            Dim j As Object
            '创建新对象
            tablename = GetRightTableName(tablename)
            sKey = tablename
            Dim objNewMember As Ctable
            objNewMember = New Ctable


            '设置传入方法的属性
            objNewMember.Tablename = tablename
            objNewMember.staName = StaName
            objNewMember.ItemCol = New Collection
            Dim mFields() As String
            mFields = Split(Fields, ",")
            For j = 0 To UBound(mFields)
                If GItemCol.Exist(mFields(j)) Then
                    objNewMember.ItemCol.Add(GItemCol(mFields(j)), mFields(j))
                End If
            Next j

            If Len(sKey) = 0 Then
                mCol.Add(objNewMember)
            Else
                mCol.Add(objNewMember, tablename)
            End If


            '返回已创建的对象
            Addtbl = objNewMember

        End Function


        Public Sub Remove(ByRef vntIndexKey As Object)


            mCol.Remove(vntIndexKey)
            '               setifchange (True)
        End Sub




        Public Sub New()
            MyBase.New()
            mCol = New Collection
            Me.Init("Mysql5", "127.0.0.1", "Mysql5", "mysql", "root", "root", 10, 10, False, "D:\", "Mysql5")
        End Sub

        Sub Stopme()
            Mytimer.Enabled = False
        End Sub


        Protected Overrides Sub Finalize()
            mCol = Nothing
            Mytimer.Close()
            MyBase.Finalize()
        End Sub

        Private Function checkname(ByVal name As String) As Boolean

            If Me.mCol.Contains(name) Then
                checkname = True
            Else
                checkname = False
            End If
        End Function

        Function GetRightTableName(ByVal name As String) As String

            Dim i As Integer
            Dim tn As String
            tn = name
            Do While checkname(name)
                i = i + 1

                name = tn & i

            Loop
            GetRightTableName = name
        End Function


        Private Sub SaveAndUPdateToDB()  '100ms一次
          
            Dim tnum As Long
            tnum = DateDiff("s", DateSerial(2010, 1, 1), Now)
            If Me.IfStart = True Then
              
                '根据历史存库周期存库
                '---------------------------------------------
                If tnum Mod Me.SaveCycle = 0 Then
                    If Me.FirstTime.FirstTime Then
                        MyDBO.CreateDB()
                    End If
                    MyDBO.SavetoHTDB(Now)


                End If
                '--------------------------------------
                '根据实时更新周期更新
                If tnum Mod Me.UpdateCycle = 0 Then
                    If Me.FirstTime.FirstTime Then
                        MyDBO.CreateDB()
                    End If

                    MyDBO.UpdateToRTDB()
                End If

            End If


        End Sub

        Sub SaveAlarmTodb(ByVal ItemID As String, ByRef fldname As String, ByRef StartTime As Date, ByRef EndTime As Date,
                          ByRef Ifchecked As Boolean, ByRef CheckTime As Date, ByRef AlarmCondition As String)

            If Me.IfStart = True Then


                If MyDBO.ConnectToserverTest Then
                    If Me.FirstTime.FirstTime Then
                        MyDBO.CreateDB()
                    End If
                    MyDBO.SaveAlarmTodb(ItemID, fldname, StartTime, EndTime, Ifchecked, CheckTime, AlarmCondition)
                End If

            End If
        End Sub




    

        Private Sub Mytimer_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles Mytimer.Elapsed
            SaveAndUPdateToDB()
        End Sub
    End Class
End Namespace