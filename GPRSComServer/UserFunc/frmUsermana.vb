Option Strict Off
Option Explicit On

Imports System.Collections.Generic
Imports GPRSComServer.GprsCom
Imports System.Xml

Namespace UserFunc
    Friend Class FrmUsermana
        Inherits System.Windows.Forms.Form


        Dim Us As List(Of User) = New List(Of User)
        Dim _beingEditUser As User
        Function GetuserfromXml() As List(Of User)
            Dim configdoc As New XmlDocument
            configdoc.Load((My.Application.Info.DirectoryPath & "\RTUconfig.xml"))
            Dim xmlparent As XmlElement
            Dim xmlchild As XmlElement
            xmlparent = configdoc.SelectSingleNode("root/users")
            If xmlparent Is Nothing Then
                xmlparent = configdoc.SelectSingleNode("root").AppendChild(configdoc.CreateElement("users"))
            End If

            For i As Int16 = 0 To xmlparent.ChildNodes.Count - 1
                xmlchild = xmlparent.ChildNodes(i)
                Dim U As New User
                U.UserName = xmlchild.GetAttribute("name")
                U.Password = xmlchild.GetAttribute("password")
                U.Power = xmlchild.GetAttribute("power")
                Us.Add(U)
            Next i
            GetuserfromXml = Us

        End Function


        Private Sub cbolevel_Validating(ByVal eventSender As System.Object,
                                        ByVal eventArgs As System.ComponentModel.CancelEventArgs) _
            Handles cbolevel.Validating
            Dim cancel As Boolean = eventArgs.Cancel
            _beingEditUser.Power = cbolevel.Text

            eventArgs.Cancel = cancel
        End Sub

        Private Sub Accept(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)


        End Sub

        Private Sub Login(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
            Handles cmdUserLogin.Click
            If CurrUsr.Power > 1 Then
                Fconfig.Show()
                Me.Close()
            Else
                MsgBox("您的权限不够!")
            End If
        End Sub

        Private Sub CloseMe(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
            Handles CmdClose.Click
            Me.Close()
        End Sub

        Private Sub EditUser(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
            Handles cmdedituser.Click
            If CurrUsr.Power = 3 Then
                If cmdedituser.Text = ">>编辑用户" Then
                    Me.SetBounds(Me.Left, Me.Top, Me.Width, 400)
                    cmdedituser.Text = "<<编辑用户"

                Else
                    Me.SetBounds(Me.Left, Me.Top, Me.Width, 150)
                    cmdedituser.Text = ">>编辑用户"
                End If
            Else
                MsgBox("您的权限级别不够!请先登录")
            End If
        End Sub

        Private Sub Adduser(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
            Handles cmdAdduser.Click
            Dim n As System.Windows.Forms.TreeNode
            Dim u As New User

            u.UserName = "user" & Str(Us.Count)
            u.Password = "1111"
            u.Power = 1
            Us.Add(u)

            _beingEditUser = u

            n = TreeView1.Nodes("r").Nodes.Add(u.UserName)
            n.EnsureVisible()
        End Sub


        Private Sub RemoveUser(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
            Handles cmdRemoveuser.Click
            TreeView1.Focus()

            If TreeView1.SelectedNode.Text = "用户" Then
                Exit Sub
            End If

            Us.RemoveAt(TreeView1.SelectedNode.Index)
            TreeView1.Nodes("r").Nodes.RemoveAt(TreeView1.SelectedNode.Index)


            TreeView1.Nodes("r").Checked = True
            TreeView1.Focus()
            TreeView1.Refresh()

        End Sub

        Private Sub LoginUser(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
            Handles cmdLogin.Click
            Dim find As Boolean
            Dim u As User
            For Each u In Us
                If u.UserName = cbouser.Text Then
                    find = True
                    If u.Password = txtpass.Text Then
                        CurrUsr.UserName = cbouser.Text
                        CurrUsr.Password = txtpass.Text
                        CurrUsr.Power = u.Power
                        MsgBox("登录成功")
                        Exit Sub
                    Else
                        MsgBox("密码错误")
                        Exit Sub
                    End If
                End If



            Next u
            If find = False Then
                MsgBox("用户名不存在")
            End If
        End Sub


        Private Sub frmUsermana_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) _
            Handles Me.FormClosing

        End Sub

        Private Sub frmUsermana_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
            Handles MyBase.Load
            Dim nodx As System.Windows.Forms.TreeNode
            TreeView1.Nodes.Clear()
            nodx = TreeView1.Nodes.Add("r", "用户")

            Dim i As Integer

            GetuserfromXml()
            Dim u As User
            For Each u In Us
                nodx = TreeView1.Nodes("r").Nodes.Add(u.UserName)
                nodx.EnsureVisible()
                cbouser.Items.Add(u.UserName)



            Next u
            For i = 1 To 3
                cbolevel.Items.Add(CStr(i))
            Next i
            cbouser.SelectedIndex = 0
        End Sub

        Private Sub TreeView1_AfterLabelEdit(ByVal eventSender As System.Object,
                                             ByVal eventArgs As System.Windows.Forms.NodeLabelEditEventArgs) _
            Handles TreeView1.AfterLabelEdit
            Dim cancel As Boolean = eventArgs.CancelEdit
            Dim newString As String = eventArgs.Label
            _beingEditUser.UserName = newString

        End Sub

        Private Sub TreeView1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
            Handles TreeView1.Click

        End Sub



        Private Sub TreeView1_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
            Handles TreeView1.DoubleClick
            TreeView1.SelectedNode.BeginEdit()
        End Sub


        Private Sub txtpass_KeyPress(ByVal eventSender As System.Object,
                                     ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtpass.KeyPress
            Dim keyAscii As Short = Asc(eventArgs.KeyChar)
            If keyAscii = 13 Then
                LoginUser(cmdLogin, New System.EventArgs())
            End If
            eventArgs.KeyChar = Chr(keyAscii)
            If keyAscii = 0 Then
                eventArgs.Handled = True
            End If
        End Sub

        Private Sub txtPassword_Validating(ByVal eventSender As System.Object,
                                           ByVal eventArgs As System.ComponentModel.CancelEventArgs) _
            Handles txtPassword.Validating
            Dim cancel As Boolean = eventArgs.Cancel
            _beingEditUser.Password = txtPassword.Text

            MsgBox("密码修改成功！")

            eventArgs.Cancel = cancel
        End Sub

        Private Sub SaveToxml(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSaveToXml.Click
            Dim configdoc As New XmlDocument
            configdoc.Load((My.Application.Info.DirectoryPath & "\RTUconfig.xml"))
            Dim xmlparent As XmlElement
            xmlparent = configdoc.SelectSingleNode("root/users")
            If xmlparent Is Nothing Then
                xmlparent = configdoc.SelectSingleNode("root").AppendChild(configdoc.CreateElement("users"))
            End If
            Dim U As User


            xmlparent.RemoveAll()

            Dim xmluser As XmlElement

            For Each U In Us
                xmluser = xmlparent.AppendChild(configdoc.CreateElement("user"))
                xmluser.SetAttribute("name", U.UserName)
                xmluser.SetAttribute("password", U.Password)
                xmluser.SetAttribute("power", U.Power)
            Next
            configdoc.Save((My.Application.Info.DirectoryPath & "\RTUconfig.xml"))
            Me.SetBounds(Me.Left, Me.Top, Me.Width, 150)
        End Sub

        Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect

            If TreeView1.SelectedNode.Text = "用户" Then
                Exit Sub
            End If

            _beingEditUser = Us(TreeView1.SelectedNode.Index)
            txtPassword.Text = _beingEditUser.Password
            cbolevel.Text = _beingEditUser.Power

        End Sub
    End Class
End Namespace