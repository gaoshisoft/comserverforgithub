Option Strict Off
Option Explicit On
Namespace DBfunc
    'Imports system.Data.ado
    Friend Class CManipulateAccess
        Implements IManipulateDB



        '保持属性值的局部变量
        Private mvarTableCol As Object '局部复制
        Private cm As ADODB.Command
        Public Rs As ADODB.Recordset
        Private cat As ADOX.Catalog
        Private tbl As ADOX.Table
        Private cln As ADOX.Column
        Private idx As ADOX.Index
        Private DBpath As String
        Private fldtype As ADOX.DataTypeEnum
        'Public Command1 As New cCommand
        Private Accesscn As ADODB.Connection

        Public Function getCn() As ADODB.Connection
            getCn = Me.GetaccessCN
        End Function

        Sub Init(ByRef AccessDbpath As String)
            DBpath = AccessDbpath
        End Sub


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

        Public Sub CreateAccessDB()
            Dim dbname As Object
            Dim cnAccess As Object
            '建库流程：先检查相应库是否存在没有则建，再检查相应表是否存在，没有则建，再检查相应字段，没有则建。
            'Dim cnAccess As ADODB.Connection
            cnAccess = New ADODB.Connection
            cm = New ADODB.Command
            Dim station As _Itable
            Dim parameter As _IItem
            Dim cln As ADOX.Column
            Dim fldtype As ADOX.DataTypeEnum
            If DBpath = "" Then
                DBpath = "D:\"
            End If
            dbname = "SCADAHIS" '& Format(Date, "yyyymm")
            '-------------------如果指定的目录不存在则建
            Initcata((DBpath))
            cat = New ADOX.Catalog
            '-------------------如果当月数据库不存在则建
            If Dir(DBpath & "\" & dbname & ".mdb") = "" Then
                cat.Create("provider=microsoft.jet.oledb.4.0;data source=" & DBpath & "\" & dbname & ".mdb")
            End If
            '-------------------
            cnAccess.Open("provider=microsoft.jet.oledb.4.0;data source=" & DBpath & "\" & dbname & ".mdb")
            cat.ActiveConnection = cnAccess
            For Each station In Me.TableCol
                Err.Clear()
                On Error Resume Next
                tbl = cat.Tables(station.tablename) '如果这个表不存在则出错
                If Err.Number Then
                    On Error GoTo 0
                    CreateTable(station, cat)
                Else
                    '------------------如果参数集合中有在数据库相应表中没有用字段体现的参数则建
                    If Not station.ItemCol Is Nothing Then
                        For Each parameter In station.ItemCol
                            Err.Clear()
                            On Error Resume Next
                            cln = tbl.Columns(parameter.ItemFieldName) '如果这个字段不存在则出错
                            If Err.Number Then
                                On Error GoTo 0
                                CreateField(parameter, tbl)
                            End If
                        Next parameter
                    End If
                End If
            Next station '站点集合中的下一个站点
            '------------------建立报警表
            Err.Clear()
            On Error Resume Next
            tbl = cat.Tables("Alarmtable")
            If Err.Number Then
                On Error GoTo 0
                CreateAlarmTable(cat)
            End If
        End Sub

        Public Sub Initcata(ByRef cata As String)
            Dim temp() As String
            Dim i As Integer
            Dim dire As String
            Dim j As Integer
            '    If Right(cata, 1) <> "\" Then
            '    cata = cata & "\"
            '    End If
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

        Private Sub savetoAccess(ByVal savetime As DateTime, ByRef station As _Itable)
            Dim para As _IItem
            Dim cnAccess As ADODB.Connection
            'If station.ComSuccess = False Then
            '   Exit Sub
            'End If
            cnAccess = New ADODB.Connection
            cnAccess.Open(GetConnectString("SCADAHIS"))
            Rs = New ADODB.Recordset
            Rs.Open("select top 1 * from " & station.tablename, cnAccess, ADODB.CursorTypeEnum.adOpenKeyset,
                    ADODB.LockTypeEnum.adLockOptimistic)
            Rs.AddNew()
            Rs.Fields("GetherTime").Value = savetime
            Err.Clear()
            On Error GoTo errh
            If Not station.ItemCol Is Nothing Then
                For Each para In station.ItemCol

                    'UPGRADE_WARNING: 未能解析对象 para.ItemValue 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
                    Rs.Fields(para.ItemFieldName).Value = para.ItemValue

                Next para
            End If
            Rs.Update()
            Rs.Close()
            cnAccess.Close()
            Exit Sub
errh:
            If Err.Number Then
                'Me.LogFile "SavetoAccess出现错误" & err.Description & station.rtuname ' & para.Paraname
            End If
        End Sub

        Private Sub UpdatetoAccess(ByRef station As _Itable)
            Dim para As _IItem
            Dim cnAccess As ADODB.Connection
            cnAccess = New ADODB.Connection
            cnAccess.Open(GetConnectString("SCADAHIS"))
            Rs = New ADODB.Recordset
            Rs.Open("select  * from " & station.tablename, cnAccess, ADODB.CursorTypeEnum.adOpenKeyset,
                    ADODB.LockTypeEnum.adLockOptimistic)
            If Rs.BOF = True Then
                Rs.AddNew()
            Else

                Rs.MoveFirst()
            End If
            Rs.Fields("GetherTime").Value = Now
            Err.Clear()
            On Error GoTo errh
            If Not station.ItemCol Is Nothing Then
                For Each para In station.ItemCol

                    'UPGRADE_WARNING: 未能解析对象 para.ItemValue 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
                    Rs.Fields(para.ItemFieldName).Value = para.ItemValue

                Next para
            End If
            Rs.Update()
            Rs.Close()
            cnAccess.Close()
            Exit Sub
errh:
            If Err.Number Then
                'Me.LogFile "SavetoAccess出现错误" & err.Description & station.rtuname ' & para.Paraname
            End If
        End Sub

        Public Sub SavetoAccessDB(ByVal savetime As DateTime)
            Dim station As _Itable
            For Each station In Me.TableCol
                If station.TableType = "历史表" Then
                    savetoAccess(savetime, station)
                End If
            Next station
        End Sub

        Public Sub UpdateToAccessDB()
            Dim station As _Itable
            For Each station In Me.TableCol
                If station.TableType = "实时表" Then
                    UpdatetoAccess(station)
                End If
            Next station
        End Sub




        Private Sub Class_Initialize_Renamed()
            Rs = New ADODB.Recordset
        End Sub

        Public Sub New()
            MyBase.New()
            Class_Initialize_Renamed()
        End Sub

        Public Sub CreateTable(ByVal station As _Itable, ByVal cat As ADOX.Catalog)
            tbl = New ADOX.Table

            tbl.Name = station.tablename

            cln = New ADOX.Column
            cln.Name = "GetherTime"
            cln.Type = ADODB.DataTypeEnum.adDate
            tbl.Columns.Append(cln)
            cln = Nothing
            Dim parameter As _IItem
            If Not station.ItemCol Is Nothing Then
                For Each parameter In station.ItemCol
                    CreateField(parameter, tbl)
                Next parameter
            End If
            idx = New ADOX.Index
            idx.Name = tbl.Name & "time"
            idx.Columns.Append(("GetherTime"))
            idx.PrimaryKey = True
            idx.Unique = True
            tbl.Indexes.Append(idx)
            cat.Tables.Append(tbl)
            'UPGRADE_NOTE: 在对对象 tbl 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
            tbl = Nothing
        End Sub

        Public Sub CreateField(ByVal para As _IItem, ByVal tbl As ADOX.Table)
            cln = New ADOX.Column
            cln.Name = para.ItemFieldName
            Select Case para.ItemDataType
                Case "Boolean"
                    cln.Type = ADODB.DataTypeEnum.adBoolean
                    'cln.Attributes = adColFixed

                Case "String"
                    fldtype = ADODB.DataTypeEnum.adWChar
                    cln.Attributes = ADOX.ColumnAttributesEnum.adColNullable
                Case "Date"
                    cln.Type = ADODB.DataTypeEnum.adDate
                    cln.Attributes = ADOX.ColumnAttributesEnum.adColNullable
                Case Else
                    cln.Type = ADODB.DataTypeEnum.adSingle
                    cln.Attributes = ADOX.ColumnAttributesEnum.adColNullable
            End Select

            'cln.Type = fldtype

            tbl.Columns.Append(cln)
            'UPGRADE_NOTE: 在对对象 cln 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
            cln = Nothing
        End Sub


        Private Sub CreateAlarmTable(ByVal cat As ADOX.Catalog)
            tbl = New ADOX.Table

            tbl.Name = "AlarmTable"

            cln = New ADOX.Column
            cln.Name = "ID"
            cln.Type = ADODB.DataTypeEnum.adSmallInt

            tbl.Columns.Append(cln)
            cln = Nothing
            cln = New ADOX.Column
            cln.Name = "ItemID"
            cln.Type = ADODB.DataTypeEnum.adWChar
            tbl.Columns.Append(cln)
            cln = Nothing
            cln = New ADOX.Column
            cln.Name = "FieldName"
            cln.Type = ADODB.DataTypeEnum.adWChar
            tbl.Columns.Append(cln)
            cln = Nothing
            cln = New ADOX.Column
            cln.Name = "HappenTime"
            cln.Type = ADODB.DataTypeEnum.adDate
            tbl.Columns.Append(cln)
            cln = Nothing
            cln = New ADOX.Column
            cln.Name = "EndTime"
            cln.Type = ADODB.DataTypeEnum.adDate
            tbl.Columns.Append(cln)
            cln = Nothing
            cln = New ADOX.Column
            cln.Name = "IfChecked"
            cln.Type = ADODB.DataTypeEnum.adBoolean
            tbl.Columns.Append(cln)
            cln = Nothing
            cln = New ADOX.Column
            cln.Name = "CheckTime"
            cln.Type = ADODB.DataTypeEnum.adDate
            tbl.Columns.Append(cln)
            cln = Nothing
            cln = New ADOX.Column
            cln.Name = "AlarmCondition"
            cln.Type = ADODB.DataTypeEnum.adWChar

            tbl.Columns.Append(cln)
            cln = Nothing

            idx = New ADOX.Index
            idx.Name = tbl.Name & "Index"
            idx.Columns.Append(("ID"))
            idx.PrimaryKey = True
            idx.Unique = True
            tbl.Indexes.Append(idx)
            cat.Tables.Append(tbl)
            tbl = Nothing
        End Sub

        Public Sub SaveAlarmTodb(ByVal ItemID As String, ByRef fldname As String, ByRef StartTime As Date,
                                 ByRef EndTime As Date, ByRef Ifchecked As Boolean, ByRef CheckTime As Date,
                                 ByRef AlarmCondition As String)
            Dim SqlStr As String
            Dim id As Integer
            'ddlcmd = ddlcmd & "(ID  int2 auto_increment not null,primary key (ID), " & _
            ''"ItemID varchar(255),FieldName varchar(255), HappenTime timestamp,EndTime timestamp,Ifchecked boolean," & _
            ''"AlarmValue float8"
            Dim Cn As ADODB.Connection
            Cn = Me.getCn
            Dim Rs As New ADODB.Recordset
            Rs.Open("select max(id) from alarmtable", Cn, ADODB.CursorTypeEnum.adOpenForwardOnly,
                    ADODB.LockTypeEnum.adLockReadOnly)
            If Rs.EOF = True Then
                id = 0
            ElseIf IsDBNull(Rs.Fields(0).Value) Then
                id = 0
            Else

                id = Rs.Fields(0).Value + 1
            End If

            SqlStr =
                "Insert into AlarmTable (Id,ItemId ,FieldName,HappenTime,EndTime,Ifchecked,CheckTime,AlarmCondition) values( " &
                id & ",'" & ItemID & "','" & fldname & "','" & StartTime & "','" & EndTime & "'," & Ifchecked & ",'" &
                CheckTime & "','" & AlarmCondition & "');"
            'Debug.Print sqlstr
            Cn.Execute(SqlStr)
            Cn.Close()
        End Sub


        Private Function GetConnectString(ByRef dbname As String) As String
            GetConnectString = "provider=microsoft.jet.oledb.4.0;data source=" & DBpath & "\" & dbname & ".mdb"
        End Function

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

        Function GetaccessCN() As ADODB.Connection
            Dim Connectstring As String
            Dim Accesscn As ADODB.Connection = New ADODB.Connection
            Connectstring = "provider=microsoft.jet.oledb.4.0;data source=" & DBpath & "\" & "SCADAHIS.mdb"

            If Accesscn Is Nothing Then

                Accesscn.Open(Connectstring)
            Else

                If Accesscn.ConnectionString <> Connectstring Then
                    If Accesscn.State = ADODB.ObjectStateEnum.adStateOpen Then
                        Accesscn.Close()
                        Accesscn.Open(Connectstring)
                    End If
                End If
            End If


            GetaccessCN = Accesscn
        End Function

        Public Function ConnectToserverTest() As Boolean Implements IManipulateDB.ConnectToserverTest

        End Function

        Public Function CreateMysqlDB() As Boolean Implements IManipulateDB.CreateDB
            Me.CreateAccessDB()
        End Function

        Public Sub Init1(ByRef svrstr As String, ByVal instname As String, ByVal usr As String, ByVal pass As String) Implements IManipulateDB.Init
            Me.Init(svrstr)
        End Sub

        Public Sub SaveAlarmTodb1(ByVal ItemID As String, ByRef fldname As String, ByRef StartTime As Date, ByRef EndTime As Date, ByRef Ifchecked As Boolean, ByRef CheckTime As Date, ByRef AlarmCondition As String) Implements IManipulateDB.SaveAlarmTodb
            Me.SaveAlarmTodb(ItemID, fldname, StartTime, EndTime, Ifchecked, CheckTime, AlarmCondition)
        End Sub

        Public Sub SavetoHTDB(ByVal savetime As DateTime) Implements IManipulateDB.SavetoHTDB
            Me.SavetoAccessDB(savetime)
        End Sub

        Public Property TableCol1 As Object Implements IManipulateDB.TableCol
            Get
                Return Me.TableCol
            End Get
            Set(ByVal value As Object)
                Me.TableCol = value
            End Set
        End Property

        Public Sub UpdateToRTDB() Implements IManipulateDB.UpdateToRTDB
            Me.UpdateToAccessDB()
        End Sub

        Public Sub SaveParainfotoDB() Implements IManipulateDB.SaveParainfotoDB

        End Sub
    End Class
End Namespace