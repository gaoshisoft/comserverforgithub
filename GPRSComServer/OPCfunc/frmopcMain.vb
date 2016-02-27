Option Strict Off
Option Explicit On

Imports System.Xml
Imports System.Collections.Generic
Friend Class frmOpc
    Inherits System.Windows.Forms.Form
 
    Dim mListItem As System.Windows.Forms.ListViewItem 'current selected ListItem
    Dim WithEvents Tmr As Timer
    Dim selectedItems As Collection
    Dim stanamelist As List(Of String) = New List(Of String)

    Private Sub additem(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command1.Click

        ItemDlg.Cleartext()
        ItemDlg.ShowDialog()
        If ItemDlg.mok = False Then
            Exit Sub
        End If
        Dim CitemRenamed As CItem
        CitemRenamed = GItemCol.Add((ItemDlg.mName), ItemDlg.ChineseDis, ItemDlg.HandleExpresion, ItemDlg.UnitStr, Now, (ItemDlg.mDev), (ItemDlg.mMBAD), (ItemDlg.mDataType),
                                      (ItemDlg.mNeedconvert), (ItemDlg.mAirangedown), (ItemDlg.mAirangeup),
                                      (ItemDlg.mConverteddown), (ItemDlg.mConvertedUP), (ItemDlg.mIfswapbyte),
                                     ItemDlg.Uplimit, ItemDlg.DownLimit, ItemDlg.mName)

        If CitemRenamed Is Nothing Then
            MsgBox("参数名称重复，添加失败！", MsgBoxStyle.OkOnly)
        End If
        ListRefresh(eventSender, eventArgs)

        refreshtreeview()
        additem(eventSender, eventArgs)
    End Sub

    Private Sub deleteitem(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command2.Click

        mnuRemoveItem_Click(mnuRemoveItem, New System.EventArgs())
        ListRefresh(eventSender, eventArgs)
        refreshtreeview()
    End Sub

    Sub refreshtreeview()
        Dim selnodetext As String = TreeView1.SelectedNode.Text
        stanamelist.Clear()
        For Each Objnewmember As CItem In GItemCol
            Dim staname As String = Objnewmember.ItemName.Split("_")(0)
            If Not stanamelist.Contains(staname) Then
                stanamelist.Add(staname)
            End If
        Next Objnewmember
        TreeView1.Nodes.Clear()
        For Each s As String In stanamelist
            TreeView1.Nodes.Add(s, s)

        Next
        If selnodetext <> "" Then
            If TreeView1.Nodes.ContainsKey(selnodetext) Then
                TreeView1.SelectedNode = TreeView1.Nodes(selnodetext)
            End If
        End If

    End Sub
    Private Sub Command4_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command4.Click

        m_save_Click(m_save, New System.EventArgs())
        InitDBsaveConfig()
        Me.Close()

    End Sub

    Private Sub ListRefresh(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command5.Click

        lvListView.Items.Clear()


        Dim citm As CItem
        For Each citm In GItemCol

            If citm.ItemName Like Trim(Me.txtdisplaychoose.Text) Then

                Dim Itm As ListViewItem
                Itm = lvListView.Items.Add(citm.ItemName)
                Itm.Name = citm.ItemName
                'Itm.SubItems.Add(citem_Renamed.ItemName)
                Itm.SubItems.Add(citm.ChineseDis)
                Itm.SubItems.Add(citm.UnitStr)
                Itm.SubItems.Add(citm.ItemDataType)
                Itm.SubItems.Add(citm.ItemValue)
                Itm.SubItems.Add(citm.ItemTimeStamp)
                Itm.SubItems.Add(citm.devad)
                Itm.SubItems.Add(citm.MBAD)
            End If
         

        Next citm
     
    End Sub

    Private Sub frmOpc_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
        Handles MyBase.Activated
        SizeControls()
    End Sub

    Private Sub frmOpc_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        '    '*********************** Product registration ( registered user only ) **********
        '    Dim strName(50) As Byte
        '    Dim strCode(50) As Byte
        '
        '    StringToByte "UserName", strName()
        '    StringToByte "UserCode", strCode()
        '    'KOS_Active strName(0), strCode(0)
        '    '********************** registration end **************************************
        stanamelist.Clear()
        mListItem = Nothing


        lvListView.Columns.Insert(0, "Name", "Name", 130)
        lvListView.Columns.Insert(1, "ChiniesDis", "ChineseDis", 130)
        lvListView.Columns.Insert(2, "UnitStr", "UnitStr", 130)
        lvListView.Columns.Insert(3, "DataType", "DataType", 130)
        lvListView.Columns.Insert(4, "Value", "Value", 130)
        lvListView.Columns.Insert(5, "TimeStamp", "TimeStamp", 125)
        lvListView.Columns.Insert(6, "DeviceAD", "DeviceAD", 125)
        lvListView.Columns.Insert(7, "MBAD", "MBAD", 125)
      


        For Each Objnewmember As CItem In GItemCol


            Dim Itm As ListViewItem
            Itm = lvListView.Items.Add(Objnewmember.ItemName) 'listview的ADD方法会添加一个条目，参数是这个条目的第一列的text，返回对象是添加的条目，subitems.add方法会

            '添加其他列的文本，顺序为 index 从1增加。第一列index 为 0
            Itm.Name = Objnewmember.ItemName
            Itm.SubItems.Add(Objnewmember.ChineseDis)
            Itm.SubItems.Add(Objnewmember.UnitStr)
            Itm.SubItems.Add(Objnewmember.ItemDataType)
            Itm.SubItems.Add(Objnewmember.ItemValue)
            Itm.SubItems.Add(Objnewmember.ItemTimeStamp)
            Itm.SubItems.Add(Objnewmember.devad)
            Itm.SubItems.Add(Objnewmember.MBAD)
            Dim staname As String = Objnewmember.ItemName.Split("_")(0)
            If Not stanamelist.Contains(staname) Then
                stanamelist.Add(staname)
            End If
        Next Objnewmember
        TreeView1.Nodes.Clear()
        For Each s As String In stanamelist
            TreeView1.Nodes.Add(s, s)

        Next
        Tmr = New Timer
        Tmr.Interval = 1000
        Tmr.Start()
    End Sub


    Private Sub frmOpc_FormClosing(ByVal EventSender As System.Object,
                                   ByVal EventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim Cancel As Boolean = eventArgs.Cancel

        eventArgs.Cancel = Cancel
    End Sub


    Private Sub frmOpc_Resize(ByVal EventSender As System.Object, ByVal EventArgs As System.EventArgs) _
        Handles MyBase.Resize
        On Error Resume Next
        Me.Width = 1800

        SizeControls()
    End Sub


    Sub SizeControls()

        lvListView.Left = 2
        lvListView.Width = Me.Width - 7
        lvListView.Height = sbStatusBar.Top - lvListView.Top - Frame1.Height
        Frame1.Top = lvListView.Top + lvListView.Height
    End Sub

    Private Sub lvListView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvListView.Click
        If lvListView.SelectedItems.Count > 0 Then
            lvListView_ItemClick(lvListView.SelectedItems(0))
        End If
    End Sub

    Private Sub lvListView_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lvListView.DoubleClick

        If Not mListItem Is Nothing Then
            ItemDlg.ShowDialog()
            'ItemDlg.Caption = "Edit Item"
            If ItemDlg.mok = True Then

                If mListItem.Name <> ItemDlg.mName Then
                    GItemCol.Remove((mListItem.Name))
                    GItemCol.Add((ItemDlg.mName), ItemDlg.ChineseDis, ItemDlg.HandleExpresion, ItemDlg.UnitStr, Now, ItemDlg.mDev, (ItemDlg.mMBAD), (ItemDlg.mDataType),
                                  (ItemDlg.mNeedconvert), (ItemDlg.mAirangedown), (ItemDlg.mAirangeup),
                                  (ItemDlg.mConverteddown), (ItemDlg.mConvertedUP), (ItemDlg.mIfswapbyte), ItemDlg.Uplimit, ItemDlg.DownLimit, ItemDlg.mName)
                Else
                    GItemCol((mListItem.Name)).devad = ItemDlg.mDev
                    GItemCol((mListItem.Name)).MBAD = ItemDlg.mMBAD
                    GItemCol((mListItem.Name)).ChineseDis = ItemDlg.ChineseDis
                    GItemCol((mListItem.Name)).ItemDataType = ItemDlg.mDataType
                    GItemCol((mListItem.Name)).NeedConvert = ItemDlg.mNeedconvert
                    GItemCol((mListItem.Name)).Airangedown = ItemDlg.mAirangedown
                    GItemCol((mListItem.Name)).Airangeup = ItemDlg.mAirangeup
                    GItemCol((mListItem.Name)).ConvertedDown = ItemDlg.mConverteddown
                    GItemCol((mListItem.Name)).convertedUP = ItemDlg.mConvertedUP
                    GItemCol((mListItem.Name)).IfSwapByte = ItemDlg.mIfswapbyte
                    GItemCol(mListItem.Name).HandleExpretion = ItemDlg.HandleExpresion
                    GItemCol(mListItem.Name).UnitStr = ItemDlg.UnitStr
                    GItemCol(mListItem.Name).DownLimit = ItemDlg.DownLimit
                    GItemCol(mListItem.Name).Uplimit = ItemDlg.Uplimit
                End If
            End If
        End If

    End Sub

    Private Sub lvListView_ItemClick(ByVal Item As System.Windows.Forms.ListViewItem)
        mListItem = Item
        Dim CitemRenamed As CItem
        CitemRenamed = GItemCol((mListItem.Name))
        ItemDlg.mName = CitemRenamed.ItemName
        ItemDlg.mDataType = CitemRenamed.ItemDataType
        ItemDlg.mDev = CitemRenamed.devad
        ItemDlg.mMBAD = CitemRenamed.MBAD
        ItemDlg.mNeedconvert = CitemRenamed.NeedConvert
        ItemDlg.mAirangedown = CitemRenamed.Airangedown
        ItemDlg.mAirangeup = CitemRenamed.Airangeup
        ItemDlg.mConverteddown = CitemRenamed.ConvertedDown
        ItemDlg.mConvertedUP = CitemRenamed.convertedUP
        ItemDlg.mIfswapbyte = CitemRenamed.IfSwapByte
        ItemDlg.ChineseDis = CitemRenamed.ChineseDis
        ItemDlg.HandleExpresion = CitemRenamed.HandleExpretion
        ItemDlg.UnitStr = CitemRenamed.UnitStr
        ItemDlg.Uplimit = CitemRenamed.Uplimit
        ItemDlg.DownLimit = CitemRenamed.DownLimit
    End Sub

    Public Sub m_save_Click(ByVal EventSender As System.Object, ByVal EventArgs As System.EventArgs) _
        Handles m_save.Click


        If SaveNewItemconfigtoXml() Then
            MsgBox("OPC server 参数配置保存成功！", MsgBoxStyle.OkOnly)
        Else
            MsgBox("OPC server 参数配置保存失败！", MsgBoxStyle.OkOnly)
        End If
    End Sub

    Public Sub mnuAddItem_Click(ByVal EventSender As System.Object, ByVal EventArgs As System.EventArgs) _
        Handles mnuAddItem.Click

      

    End Sub

    Public Sub mnuRegister_Click(ByVal EventSender As System.Object, ByVal EventArgs As System.EventArgs) _
        Handles mnuRegister.Click

        'mbReturn = KOS_RegisterB(GUID(0), svrName(0), SvrDesc(0), ExePath(0))
    End Sub

    Public Sub mnuRemoveItem_Click(ByVal EventSender As System.Object, ByVal EventArgs As System.EventArgs) _
        Handles mnuRemoveItem.Click

        If mListItem Is Nothing Then Exit Sub

        Dim SName As String
        'Dim pItem As CItem
        If MsgBox("确认要删除这些OPC条目吗？", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            For Each mListItem In lvListView.SelectedItems
                SName = mListItem.Name
                GItemCol.Remove(SName)
            Next
            mListItem = Nothing

        End If
    End Sub

    Public Sub mnuShutdownClients_Click(ByVal EventSender As System.Object, ByVal EventArgs As System.EventArgs) _
        Handles mnuShutdownClients.Click

        'KOS_ShutdownClients()
    End Sub

    Public Sub mnuUnRegister_Click(ByVal EventSender As System.Object, ByVal EventArgs As System.EventArgs) _
        Handles mnuUnRegister.Click
    End Sub






    Public Sub mnuFileClose_Click(ByVal EventSender As System.Object, ByVal EventArgs As System.EventArgs) _
        Handles mnuFileClose.Click


        Me.Close()
    End Sub



    Function SaveNewItemconfigtoXml() As Boolean
        Dim Configdoc As New XmlDocument
        Configdoc.Load((My.Application.Info.DirectoryPath & "\RTUconfig.xml"))
        Dim Xmlparent As XmlElement

        Xmlparent = Configdoc.SelectSingleNode("root/opcitems")
        If Xmlparent Is Nothing Then
            Xmlparent = Configdoc.SelectSingleNode("root").AppendChild(Configdoc.CreateElement("opcitems"))
        End If


        Xmlparent.RemoveAll()
        Try
            Dim xmlItmele As XmlElement
            Dim itm As CItem
            For i As Long = 1 To GItemCol.Count
                itm = GItemCol(i)

                xmlItmele = Xmlparent.AppendChild(Configdoc.CreateElement("opcitem"))
                xmlItmele.SetAttribute("itemname", itm.ItemName)
                xmlItmele.SetAttribute("itemchinesedis", itm.ChineseDis)
                xmlItmele.SetAttribute("handleexpression", itm.HandleExpretion)
                xmlItmele.SetAttribute("unitstr", itm.UnitStr)
                xmlItmele.SetAttribute("itemdatatype", itm.ItemDataType)
                xmlItmele.SetAttribute("itemdevad", itm.devad)
                xmlItmele.SetAttribute("itemmbad", itm.MBAD)
                xmlItmele.SetAttribute("itemneedconvert", itm.NeedConvert)
                xmlItmele.SetAttribute("itemairangedown", itm.Airangedown)
                xmlItmele.SetAttribute("itemairangeup", itm.Airangeup)
                xmlItmele.SetAttribute("itemconverteddown", itm.ConvertedDown)
                xmlItmele.SetAttribute("itemconvertedup", itm.convertedUP)
                xmlItmele.SetAttribute("itemswapbyte", itm.IfSwapByte)
                xmlItmele.SetAttribute("uplimit", itm.Uplimit)
                xmlItmele.SetAttribute("downlimit", itm.DownLimit)

            Next i
            Configdoc.Save((My.Application.Info.DirectoryPath & "\RTUconfig.xml"))
            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub Tmr_Tick(ByVal Sender As Object, ByVal e As System.EventArgs) Handles Tmr.Tick

        Dim Citem As CItem
        Dim ListItem As System.Windows.Forms.ListViewItem
        If lvListView.Items.Count = 0 Then
            Exit Sub
        End If
        Dim i As Int32, j As Integer
        Try

            GItemCol.ReadDevice()

            If Me.Visible = True Then
                j = lvListView.TopItem.Index
                lvListView.BeginUpdate()
                For i = 0 To 200

                    If i + j >= lvListView.Items.Count Then
                        Exit For
                    End If
                    ListItem = lvListView.Items.Item(i + j)
                    If ListItem.Position.Y > lvListView.Height Then
                        Exit For
                    End If
                    Citem = GItemCol((ListItem.Name))

                    ListItem.Text = Citem.ItemName
                    ListItem.SubItems(1).Text = Citem.ChineseDis
                    ListItem.SubItems(2).Text = Citem.UnitStr
                    ListItem.SubItems(3).Text = Citem.ItemDataType
                    ListItem.SubItems(4).Text = Citem.StrValue
                    ListItem.SubItems(5).Text = Citem.ItemTimeStamp
                    ListItem.SubItems(6).Text = Citem.devad
                    ListItem.SubItems(7).Text = Citem.MBAD

                Next i
                lvListView.EndUpdate()
            End If


        Catch Ex As Exception
            'MsgBox(Ex.Message & i & " " & j & Citem.ItemName)

        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Protected Overrides Sub Finalize()
        Tmr.Enabled = False
        Tmr = Nothing
        MyBase.Finalize()
    End Sub

   

  
    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect

        lvListView.Items.Clear()


        Dim citm As CItem
        For Each citm In GItemCol

            If citm.ItemName Like Trim(TreeView1.SelectedNode.Text & "*") Then

                Dim Itm As ListViewItem
                Itm = lvListView.Items.Add(citm.ItemName)
                Itm.Name = citm.ItemName

                Itm.SubItems.Add(citm.ChineseDis)
                Itm.SubItems.Add(citm.UnitStr)
                Itm.SubItems.Add(citm.ItemDataType)
                Itm.SubItems.Add(citm.ItemValue)
                Itm.SubItems.Add(citm.ItemTimeStamp)
                Itm.SubItems.Add(citm.devad)
                Itm.SubItems.Add(citm.MBAD)
            End If


        Next citm
    End Sub
End Class