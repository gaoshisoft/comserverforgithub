Option Strict Off
Option Explicit On

Imports System.Xml

Namespace DBfunc


    Friend Class FrmDBsave
        Inherits System.Windows.Forms.Form
        Dim tbl As Ctable

        Dim Mnode As Windows.Forms.TreeNode
        Public Mytmr As New Mytimer




        Private Sub cboConnectType_SelectedIndexChanged(ByVal eventSender As System.Object,
                                                        ByVal eventArgs As System.EventArgs) _
            Handles cboConnectType.SelectedIndexChanged
            dbconn.DBType = CType(cboConnectType.SelectedIndex, DBconnect.DataBasetype)

            DisDBc(dbconn)
        End Sub



        Private Sub cmdifStart_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
            Handles cmdifStart.Click
            dbconn.IfStart = Not dbconn.IfStart
            DisDBc(dbconn)
        End Sub



        Private Sub CmdTestaccess_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
            Handles CmdTestaccess.Click

            dbconn.MyDBO.CreateDB()
            MsgBox("数据库建立成功！", MsgBoxStyle.OKOnly)

        End Sub

        Private Sub Command1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
            Handles Command1.Click
            Dim i As Object

            opcItemview.ShowDialog()
            Dim Itmstr() As String
            If opcItemview.mok = True Then
                Itmstr = Split(opcItemview.mChoose, ",")

                For i = 0 To UBound(Itmstr)
                    If tbl.AddField(GItemCol(Itmstr(i))) = False Then
                        MsgBox("已添加过这个条目！" & Itmstr(i))

                    End If
                Next i
                DisTbl(tbl)
            End If
        End Sub


        Private Sub Command2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
            Handles Command2.Click
            Dim i As Object
            'lstFields.RemoveItem lstFields.ListIndex

            For i = 0 To lstFields.Items.Count - 1
                If lstFields.GetSelected(i) = True Then
                    If Not tbl Is Nothing Then
                        tbl.Removefield(lstFields.Items(i).ToString)

                    End If
                End If
            Next i
            DisTbl(tbl)
        End Sub


        Private Sub Command3_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
            Handles Command3.Click

            Me.Close()
        End Sub

        Private Sub SaveConfig(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
            Handles cmdSave.Click

            SaveArchiveconfigtoXml()
            DBconn.MyDBO.SaveParainfotoDB()

            MsgBox("配置已保存，配置已生效！")
            Me.Close()
        End Sub

        Private Sub Command5_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
            Handles Command5.Click

            Try
                If dbconn.MyDBO.ConnectToserverTest Then
                    MsgBox("测试连接成功！")

                Else
                    MsgBox("测试连接失败！")

                End If
            Catch e As Exception
                MsgBox(e.Message)
            End Try

        End Sub

        Private Sub Command6_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
            Handles Command6.Click
            Try


                dbconn.MyDBO.CreateDB()
                MsgBox("数据库表建立成功！", MsgBoxStyle.OkOnly)


            Catch e As Exception
                MsgBox("数据库表建立失败！" & e.Message, MsgBoxStyle.OkOnly)
            End Try
        End Sub



        Private Sub frmDBsave_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
            Handles MyBase.Load

            dbconn = DBconn
            '初始化界面元素
            Dim nodx As System.Windows.Forms.TreeNode '创建变量。

            Me.TreeView1.ShowRootLines = False 'Linestyle 2.

            If Me.TreeView1.Nodes.Count < 1 Then

                nodx = Me.TreeView1.Nodes.Add(DBconn.ConnectName, "数据库连接")
                nodx.Checked = True
            End If


            Try

                '初始化界面元素

                Dim tbl As Ctable
                For Each tbl In dbconn.TableCol


                    If Not Me.TreeView1.Nodes(dbconn.ConnectName).Nodes.ContainsKey(tbl.Tablename) Then
                        nodx = Me.TreeView1.Nodes.Find(DBconn.ConnectName, True)(0).Nodes.Add(tbl.Tablename, tbl.Tablename)

                        nodx.EnsureVisible()
                    End If

                Next
                cboConnectType.Items.Clear()
                cboConnectType.Items.Add(DBconnect.DataBasetype.Mysql5.ToString)
                cboConnectType.Items.Add(DBconnect.DataBasetype.SQlserver2008.ToString)
                'cboConnectType.Items.Add(DBconnect.DataBasetype.Access.ToString)
                DisDBc(dbconn)
            Catch e As Exception
                MsgBox(e.Message)
            End Try
        End Sub

        Private Sub frmDBsave_FormClosing(ByVal eventSender As System.Object,
                                          ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) _
            Handles Me.FormClosing
            Dim Cancel As Boolean = eventArgs.Cancel
            Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason

            eventArgs.Cancel = Cancel
        End Sub

        Private Sub lstFields_Validating(ByVal eventSender As System.Object,
                                         ByVal eventArgs As System.ComponentModel.CancelEventArgs) _
            Handles lstFields.Validating
            Dim Cancel As Boolean = eventArgs.Cancel


            eventArgs.Cancel = Cancel
        End Sub

        Function CheckFieldname(ByRef str_Renamed As String) As Boolean
            Dim illegalstr As String
            illegalstr = ".![]" & Chr(13)
            Dim i As Integer
            Dim s As String

            For i = 1 To Len(str_Renamed)
                s = Mid(str_Renamed, i, 1)
                If InStr(1, illegalstr, s) > 0 Then
                    MsgBox("请确认字段名称中没有点（.）叹号（!）中括号（[]）以及不可打印字符如回车。如果名称是从别的应用中粘过来的，请尝试手动重新输入。", MsgBoxStyle.Information)
                    CheckFieldname = False
                    Exit For
                Else
                    CheckFieldname = True
                End If
            Next i
        End Function





        Public Sub m_rename_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
            Handles menurename.Click
            If TreeView1.SelectedNode.Text = "数据库连接" Then
                Exit Sub
            End If
            If TreeView1.SelectedNode.Parent.Name = DBconn.ConnectName Then

                TreeView1.SelectedNode.BeginEdit()

            End If
        End Sub

        Public Sub mnuaddconnect_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
            If TreeView1.Nodes.Count < 1 Then

                dbconn = DBconn.Init("Mysql", "sqlserver1", "新的连接", "DB1", "User1", "User1", "ODBCdatasource1", 180, 10, False,
                                  "D:\")

                DisDBc(dbconn)
                '           setifchange (True)
            End If
        End Sub

        Public Sub mnuaddtable_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
            Handles menuaddtable.Click
            If Not dbconn Is Nothing Then
                tbl = dbconn.Addtbl("NewTable", "", "")

                DisTbl(tbl)


            End If
        End Sub

        Public Sub mnudelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
            Handles menuDelete.Click

            If Not Mnode Is Nothing Then

                If Mnode.Name = DBconn.ConnectName Then
                    Exit Sub
                End If
                If dbconn.IfStart = True Then
                    MsgBox("必须先停止运行")

                    Exit Sub
                End If




                TreeView1.Nodes(DBconn.ConnectName).Nodes.Remove(Mnode)

                DBconn.TableCol.Remove(tbl.Tablename)
            End If
        End Sub


        Public Sub mnuRename_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        End Sub


        Private Sub TreeView1_AfterLabelEdit(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.NodeLabelEditEventArgs) Handles TreeView1.AfterLabelEdit


            Dim Cancel As Boolean = eventArgs.CancelEdit
            Dim NewString As String = eventArgs.Label
            If CheckFieldname(NewString) = False Then
                Cancel = True
                Exit Sub
            End If
            If InStr(NewString, " ") > 0 Then
                MsgBox("名称中不能有空格！")
                Cancel = True
                Exit Sub
            End If

            TreeView1.SelectedNode.Name = NewString
            dbconn.Remove(tbl.Tablename)

            tbl.Tablename = NewString
            tbl = DBconn.Addtbl(tbl.Tablename, tbl.GetFieldsString, tbl.StaName, tbl.Tablename)
            DisTbl(tbl)
        End Sub


        Private Sub TreeView1_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
            Handles TreeView1.DoubleClick
            TreeView1.SelectedNode.BeginEdit()
        End Sub

        Private Sub TreeView1_MouseUp(ByVal eventSender As System.Object,
                                      ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles TreeView1.MouseUp
            Dim Button As Short = eventArgs.Button \ &H100000



            If Button = 2 Then
                If TreeView1.Nodes.Count < 2 Then
                Else
                End If
                menuall.Show(Me.TreeView1, New Point(eventArgs.X, eventArgs.Y))

            End If
        End Sub


        Private Sub TreeView1_NodeClick(ByVal eventSender As System.Object,
                                        ByVal eventArgs As System.Windows.Forms.TreeNodeMouseClickEventArgs) _
            Handles TreeView1.NodeMouseClick
            Dim node As System.Windows.Forms.TreeNode = eventArgs.Node
            Mnode = node
            Select Case node.Name  '根据关键字前三个字符确定节点类型

                Case "MyDBConn" '是连接定义节点


                    DisDBc(dbconn)
                Case Else '是表结构定义节点




                    tbl = dbconn.Item(node.Name)
                    DisTbl(tbl)
            End Select

        End Sub

        Private Sub txtAccessDBpath_Validating(ByVal eventSender As System.Object,
                                               ByVal eventArgs As System.ComponentModel.CancelEventArgs) _
            Handles txtAccessDBpath.Validating
            Dim Cancel As Boolean = eventArgs.Cancel
            dbconn.AccessDbpath = txtAccessDBpath.Text
            dbconn.MyDBO.Init((dbconn.AccessDbpath), "", "", "")
            eventArgs.Cancel = Cancel
        End Sub

        Private Sub txtDbname_Validating(ByVal eventSender As System.Object,
                                         ByVal eventArgs As System.ComponentModel.CancelEventArgs) _
            Handles txtDbname.Validating
            Dim Cancel As Boolean = eventArgs.Cancel
            If txtDbname.Text = "" Then
                MsgBox("数据库名不能为空！")
                Cancel = True
            Else
                dbconn.sqldbname = txtDbname.Text
                dbconn.MyDBO.Init((dbconn.SQLServerName), (dbconn.sqldbname), (dbconn.UserName), (dbconn.Password))

            End If

            eventArgs.Cancel = Cancel
        End Sub


        Private Sub txtPassword_Validating(ByVal eventSender As System.Object,
                                           ByVal eventArgs As System.ComponentModel.CancelEventArgs) _
            Handles txtPassword.Validating
            Dim Cancel As Boolean = eventArgs.Cancel
            dbconn.Password = txtPassword.Text
            dbconn.MyDBO.Init((dbconn.SQLServerName), (dbconn.sqldbname), (dbconn.UserName), (dbconn.Password))

            eventArgs.Cancel = Cancel
        End Sub

        Private Sub txtSavecycle_Validating(ByVal eventSender As System.Object,
                                            ByVal eventArgs As System.ComponentModel.CancelEventArgs) _
            Handles txtSavecycle.Validating
            Dim Cancel As Boolean = eventArgs.Cancel
            If Not IsNumeric(txtSavecycle.Text) Then
                MsgBox("存库周期须为数字！")
                Cancel = True
            Else
                dbconn.SaveCycle = Val(txtSavecycle.Text)
            End If
            eventArgs.Cancel = Cancel
        End Sub

        Private Sub txtServerName_Validating(ByVal eventSender As System.Object,
                                             ByVal eventArgs As System.ComponentModel.CancelEventArgs) _
            Handles txtServerName.Validating
            Dim Cancel As Boolean = eventArgs.Cancel
            If txtServerName.Text = "" Then
                MsgBox("服务器实例名不能为空!")
            Else
                dbconn.SQLServerName = txtServerName.Text
                dbconn.MyDBO.Init((dbconn.SQLServerName), (dbconn.sqldbname), (dbconn.UserName), (dbconn.Password))

            End If
            eventArgs.Cancel = Cancel
        End Sub

        Private Sub txtUpdateCycle_Validating(ByVal eventSender As System.Object,
                                              ByVal eventArgs As System.ComponentModel.CancelEventArgs) _
            Handles txtUpdateCycle.Validating
            Dim Cancel As Boolean = eventArgs.Cancel
            dbconn.UpdateCycle = Val(txtUpdateCycle.Text)
            eventArgs.Cancel = Cancel
        End Sub


        Private Sub txtUsername_Validating(ByVal eventSender As System.Object,
                                           ByVal eventArgs As System.ComponentModel.CancelEventArgs) _
            Handles txtUsername.Validating
            Dim Cancel As Boolean = eventArgs.Cancel

            dbconn.UserName = txtUsername.Text
            dbconn.MyDBO.Init((dbconn.SQLServerName), (dbconn.sqldbname), (dbconn.UserName), (dbconn.Password))
            eventArgs.Cancel = Cancel
        End Sub

        Sub DisDBc(ByRef seldbc As DBconnect)
            If seldbc Is Nothing Then
                fraconnect.Visible = False
                fratable.Visible = False
                fraAccess.Visible = False
                Exit Sub
            End If
            fraconnect.Visible = True

            fratable.Visible = False

            Select Case seldbc.DBType
                Case DBconnect.DataBasetype.Mysql5
                    'fraMysqlset.Visible = True
                    frasqlset.Visible = True
                    fraAccess.Visible = False
                Case DBconnect.DataBasetype.SQlserver2008
                    frasqlset.Visible = True
                    'fraMysqlset.Visible = False
                    fraAccess.Visible = False
                Case DBconnect.DataBasetype.Access
                    fraAccess.Visible = True
                    'fraMysqlset.Visible = False
                    frasqlset.Visible = False

            End Select
            cboConnectType.Text = seldbc.DBType.ToString
            txtServerName.Text = seldbc.SQLServerName
            txtDbname.Text = seldbc.sqldbname
            txtUsername.Text = seldbc.UserName
            txtPassword.Text = seldbc.Password
            'TxtIP.Text = seldbc.SQLServerName
            txtAccessDBpath.Text = seldbc.AccessDbpath
            txtUpdateCycle.Text = CStr(seldbc.UpdateCycle)
            txtSavecycle.Text = CStr(seldbc.SaveCycle)
            If seldbc.IfStart Then
                'fraMysqlset.Enabled = False
                frasqlset.Enabled = False
                fratable.Enabled = False
                cboConnectType.Enabled = False
                cmdifStart.Text = "停止历史数据存库"
                txtSavecycle.Enabled = False
                txtUpdateCycle.Enabled = False
                txtAccessDBpath.Enabled = False
            Else
                'fraMysqlset.Enabled = True
                fratable.Enabled = True
                frasqlset.Enabled = True
                cboConnectType.Enabled = True
                cmdifStart.Text = "启动历史数据存库"
                txtSavecycle.Enabled = True
                txtUpdateCycle.Enabled = True
                txtAccessDBpath.Enabled = True
            End If
        End Sub


        Sub DisTbl(ByRef tbl As Ctable)
            fraconnect.Visible = False
            fratable.Visible = True
          
            If Not TreeView1.Nodes(DBconn.ConnectName).Nodes.ContainsKey(tbl.Tablename) Then
                Me.TreeView1.Nodes(DBconn.ConnectName).Nodes.Add(tbl.Tablename, tbl.Tablename)
            End If
            Dim i As Integer
            lstFields.Items.Clear()
            For i = 1 To tbl.ItemCol.Count()
                lstFields.Items.Add(tbl.ItemCol.Item(i).ItemName)
            Next i
          
            txtstaname.Text = tbl.staName
           
        End Sub


        Private Sub menuall_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) _
            Handles menuall.Opening
        End Sub

        Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) _
            Handles TreeView1.AfterSelect
        End Sub


        Function SaveArchiveconfigtoXml() As Boolean
            Dim configdoc As New XmlDocument
            configdoc.Load((My.Application.Info.DirectoryPath & "\RTUconfig.xml"))
            Dim xmlparent As XmlElement
            xmlparent = configdoc.SelectSingleNode("root/archiveconfig")
            If xmlparent Is Nothing Then
                xmlparent = configdoc.SelectSingleNode("root").AppendChild(configdoc.CreateElement("archiveconfig"))
            End If
            xmlparent.RemoveAll()

            Try

                Dim con As DBconnect
                con = DBconn
                xmlparent.SetAttribute("connecttype", con.DBType.ToString)
                xmlparent.SetAttribute("sqlservername", con.SQLServerName)

                xmlparent.SetAttribute("dbconnectname", con.ConnectName)
                xmlparent.SetAttribute("sqldbname", con.sqldbname)
                xmlparent.SetAttribute("username", con.UserName)
                xmlparent.SetAttribute("password", con.Password)
                xmlparent.SetAttribute("accessdbpath", con.AccessDbpath)
                'xmlTableele.SetAttribute("odbcdatasourcename", con.MysqlDBinfo)
                xmlparent.SetAttribute("savecycle", con.SaveCycle)
                xmlparent.SetAttribute("updatecycle", con.UpdateCycle)
                xmlparent.SetAttribute("ifstart", con.IfStart)
                Dim fn As String
                Dim T As _Itable
                For j As Int16 = 1 To con.Count
                    fn = ""
                    For k As Int16 = 1 To con.Item(j).ItemCol.Count()
                        If fn = "" Then
                            fn = con.Item(j).ItemCol.Item(k).ItemName
                        Else
                            fn = fn & "," & con.Item(j).ItemCol.Item(k).ItemName
                        End If
                    Next k
                    Dim xmlTableele As XmlElement
                    xmlTableele = xmlparent.AppendChild(configdoc.CreateElement("tableinfo"))


                    xmlTableele.SetAttribute("tablename", con.Item(j).Tablename)
                    xmlTableele.SetAttribute("staname", con.Item(j).staName)

                    T = con.Item(j)
                    xmlTableele.SetAttribute("tabletype", T.TableType.ToString)
                    xmlTableele.SetAttribute("fields", fn)



                Next j

                configdoc.Save((My.Application.Info.DirectoryPath & "\RTUconfig.xml"))
                SaveArchiveconfigtoXml = True
            Catch ex As Exception
                SaveArchiveconfigtoXml = False
            Finally



            End Try



        End Function

        Private Sub txtstaname_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtstaname.Leave
            tbl.StaName = txtstaname.Text
        End Sub

        Private Sub txtstaname_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtstaname.LostFocus
            tbl.StaName = txtstaname.Text
        End Sub




        Private Sub txtstaname_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtstaname.Validated
            tbl.staName = txtstaname.Text
        End Sub



       
       
        Protected Overrides Sub Finalize()

            Mytmr = Nothing
            MyBase.Finalize()
        End Sub
    End Class
End Namespace