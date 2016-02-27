Option Strict Off
Option Explicit On

Imports GPRSComServer.GprsCom
Imports MBsrv
Imports GPRSComServer.DBfunc
Imports System.Threading
Imports GPRSComServer.OPCfunc

Namespace MainProg

    Friend Class MdIfmain
        Inherits System.Windows.Forms.Form
        Private IntMax As Integer

        Dim WithEvents mbserver As MBsrv.MBserver

        Dim WithEvents Tmrtime As Windows.Forms.Timer = New Windows.Forms.Timer With {.Interval = 1000, .Enabled = True}


     

        Public Sub m_atnow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles m_atnow.Click
            frmportion.Show()
            frmportion.Visible = True
            frmportion.BringToFront()
        End Sub

        Public Sub m_ComControl_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
            Fview.Visible = True
        End Sub

        Public Sub m_comquality_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles m_comquality.Click
            comquality.Show()
        End Sub

      

        Public Sub m_ConnectRecord_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles m_ConnectRecord.Click
            frmConnstatequery.Show()
        End Sub

        Public Sub m_datapoll_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
            Fview.chkPollenable.CheckState = IIf(Fview.chkPollenable.CheckState = 1, 0, 1)
            m_datapoll.Checked = Fview.chkPollenable.CheckState
        End Sub

        Public Sub m_dbsave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles m_dbsave.Click
            If CurrUsr.Power = 3 Then
                frmDBsave.DisDBc(DBconn)
                frmDBsave.ShowDialog()
            Else
                MsgBox("您的权限级别不够!请先登录")
            End If


        End Sub

        Public Sub m_exincompany_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles m_exincompany.Click
            frmAbout.Show()
        End Sub

    

        Public Sub m_mbserver_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles m_mbserver.Click
            Mbs.ShowMBserver()
        End Sub

        Public Sub m_Flow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
            FrmByteqty.Show()
        End Sub



      

        Public Sub m_OnlineDTU_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles m_OnlineDTU.Click
            frmOnlineDTU.Show()
            frmOnlineDTU.Visible = True
            frmOnlineDTU.BringToFront()
        End Sub

        Private Sub m_pingpu_Click()
            Me.LayoutMdi(System.Windows.Forms.MdiLayout.TileHorizontal)

        End Sub

        Public Sub m_opcCfg_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles m_opcCfg.Click
            If CurrUsr.Power = 3 Then
                frmOpc.Show()
            Else
                MsgBox("您的权限级别不够!请先登录")
            End If


        End Sub

        Public Sub m_q_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles m_q.Click
          
          

            Me.Close()


        End Sub

     

        Public Sub m_rearrange_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles m_rearrange.Click
            ArrangeWindow()
        End Sub

        Public Sub m_register_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles m_register.Click
            UserFunc.frmUsermana.Show()
        End Sub

   

        Public Sub m_stationset_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles m_stationset.Click
            If CurrUsr.Power = 3 Then
                Fconfig.Show()
            Else
                MsgBox("您的权限级别不够!请先登录")
            End If
        End Sub

        Public Sub m_unregister_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles m_unregister.Click
            CurrUsr.Power = 1
            CurrUsr.UserName = ""
            CurrUsr.Password = ""
            MsgBox("退出登录成功")
        End Sub

        Private Sub MBserver_DataArrival(ByRef Data As Object) Handles mbserver.DataArrival
            If Fview.Visible = True Then
                If Fview.chkmbComdisplay.CheckState = 1 Then
                    Fview.txtmbrv.Text = Data
                End If
            End If
        End Sub

        Private Sub MBserver_DataResponse(ByRef Data As Object) Handles mbserver.DataResponse
            If Fview.Visible = True Then
                If Fview.chkmbComdisplay.CheckState = 1 Then
                    Fview.txtmbsend.Text = Data
                End If
            End If
        End Sub

        'Sub Writebitvalue(ByRef deviceID As Integer, ByRef wordAd As String, ByRef bit As Integer, ByRef value As Boolean)
        '    '该程序为指定字的指字位写入值，1或0,bit为从0开始的位地址
        '    Dim i As Integer
        '    i = Mbs.ReadvalueByAd(deviceID, wordAd, Device.Datatype.无符号整型)
        '    Select Case i And (2 ^ bit)
        '        Case Is = 2 ^ bit '表明这位原来为1
        '            If value = False Then '要把这位置0
        '                i = i - (2 ^ bit)
        '            Else '要把这位置1,原来是1 ,所以什么也不做
        '            End If
        '        Case Is = 0 '表明这位原来是0
        '            If value = False Then '什么也不做
        '            Else '
        '                i = i + (2 ^ bit)
        '            End If
        '    End Select
        '    Mbs.WritevaluebyAd(deviceID, wordAd, Device.Datatype.无符号整型, i)
        'End Sub

        Private Sub mbserver_Writedata(ByRef DeviceAD As Integer, ByRef startAD As String, ByRef Length As Integer, ByRef value As Object) Handles mbserver.Writedata
           
            Select Case DeviceAD
                Case 255 '是部分巡测等控制命令
                   
                Case Else
                    '只支持写单个地址
                    RTUs.WriteToRTU(DeviceAD, startAD, Mbs.ReadvalueByAd(DeviceAD, startAD, Device.Datatype.无符号整型)) '

            End Select
        End Sub

       

        Private Sub MDIfmain_Load(ByVal EventSender As System.Object, ByVal EventArgs As System.EventArgs) Handles MyBase.Load

            m_datapoll.Checked = True


            Sb1.Items.Item(1).Text = Mydsc.Dscstate




            Me.SetBounds(0, 0, 0, 0, Windows.Forms.BoundsSpecified.X Or Windows.Forms.BoundsSpecified.Y)
            mbserver = Mbs
            RTUs.StartPoll()
            TreeView1.ExpandAll()
            Sb1.Items.Item(0).Text = "鞍山高仕软件 版权所有"
            Sb1.Items.Item(4).Text = "巡测超时设置：" & RTUs.Timeout & "s"
            Sb1.Items.Item(4).BackColor = Sb1.Items(2).BackColor
            Sb1.Items.Item(2).DisplayStyle = ToolStripItemDisplayStyle.Text

            Sb1.Items.Item(3).Text = "巡测周期设置：" & RTUs.StationPolltime & "s"
            Sb1.Items.Item(3).BackColor = Sb1.Items(2).BackColor
        End Sub



        Private Sub MDIfmain_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

          

            Quit()

        End Sub
      
  


        Public Sub mnufreecom_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnufreecom.Click
            TCPsocket.Show()
        End Sub

      

        Private Sub tmrtime_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Tmrtime.Tick
            
            Sb1.Items.Item(4).Text = CStr(Now)

           

        End Sub




        Private Sub NotifyIcon1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles NotifyIcon1.DoubleClick
            Me.Show()
        End Sub


        Private Sub MDIfmain_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
            If Me.WindowState = FormWindowState.Minimized Then
                'Me.Hide()
            End If
        End Sub

        Private Sub MDIfmain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

            Dim UnloadMode As System.Windows.Forms.CloseReason = CloseReason.UserClosing
            Dim i As Integer
            If UnloadMode = CloseReason.UserClosing Then
                If CurrUsr.Power < 3 Then
                    MsgBox("您无权退出系统！")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
            If CurrUsr.Power = 3 Then
                i = MsgBox("真的要退出本系统吗？将停止一切数据通讯服务！", MsgBoxStyle.OkCancel)
                If i = MsgBoxResult.Ok Then
                    NotifyIcon1.Visible = False
                    NotifyIcon1.Dispose()
                Else

                    e.Cancel = True
                End If
            End If
        End Sub

        Private Sub MBTCP通信监控ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MBTCP通信监控ToolStripMenuItem.Click
            Mbs.ShowComdata()
        End Sub








        Protected Overrides Sub Finalize()
            Tmrtime.Enabled = False
            Tmrtime = Nothing
            mbserver = Nothing
            MyBase.Finalize()

        End Sub



        Private Sub MdIfmain_SystemColorsChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SystemColorsChanged

        End Sub

        Private Sub _Sb1_Panel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _Sb1_Panel1.Click

        End Sub

        Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect

        End Sub

        Private Sub TreeView1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TreeView1.MouseUp
            If e.Button = MouseButtons.Right Then
                If TreeView1.SelectedNode.Name = "rtusetup" Then
                    If CurrUsr.Power = 3 Then
                        Fconfig.Show()
                        Fconfig.Visible = True
                    Else
                        MsgBox("您的权限级别不够!请先登录")
                    End If

                End If
                If TreeView1.SelectedNode.Name = "opcItemdis" Then

                    If CurrUsr.Power = 3 Then
                        frmOpc.Show()

                    Else
                        MsgBox("您的权限级别不够!请先登录")
                    End If
                End If
                If TreeView1.SelectedNode.Name = "DBfuncnode" Then
                    If CurrUsr.Power = 3 Then
                        FrmDBsave.Show()

                    Else
                        MsgBox("您的权限级别不够!请先登录")
                    End If

                End If

                If TreeView1.SelectedNode.Name = "modbusservernode" Then
                    Mbs.ShowMBserver()
                End If
                If TreeView1.SelectedNode.Name = "cominspect" Then
                    TCPsocket.Show()
                End If
                If TreeView1.SelectedNode.Name = "commrecord" Then
                    ComRecord.Show()
                End If
                If TreeView1.SelectedNode.Name = "longinnode" Then
                    UserFunc.FrmUsermana.Show()
                End If
                If TreeView1.SelectedNode.Name = "opcserversetup" Then

                    If CurrUsr.Power = 3 Then
                        frmopcsvrnameset.Show()
                    Else
                        MsgBox("您的权限级别不够!请先登录")
                    End If
                End If
            End If
        End Sub

        Private Sub TreeView1_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick


        End Sub

        Private Sub TreeView1_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseDoubleClick
            If TreeView1.SelectedNode.Name = "rtusetup" Then
                If CurrUsr.Power = 3 Then
                    Fconfig.Show()
                Else
                    MsgBox("您的权限级别不够!请先登录")
                End If

            End If
            If TreeView1.SelectedNode.Name = "opcItemdis" Then

                If CurrUsr.Power = 3 Then
                    frmOpc.Show()
                Else
                    MsgBox("您的权限级别不够!请先登录")
                End If
            End If
            If TreeView1.SelectedNode.Name = "DBfuncnode" Then

                FrmDBsave.Show()

            End If

            If TreeView1.SelectedNode.Name = "modbusservernode" Then
                Mbs.ShowMBserver()
            End If
            If TreeView1.SelectedNode.Name = "cominspect" Then
                TCPsocket.Show()
            End If
            If TreeView1.SelectedNode.Name = "commrecord" Then
                ComRecord.Show()
            End If
            If TreeView1.SelectedNode.Name = "longinnode" Then
                UserFunc.FrmUsermana.Show()
            End If
            If TreeView1.SelectedNode.Name = "opcserversetup" Then

                If CurrUsr.Power = 3 Then
                    frmopcsvrnameset.Show()
                Else
                    MsgBox("您的权限级别不够!请先登录")
                End If
            End If
        End Sub


        Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click

            Try
                RestartApp()
                DBconn.MyDBO.SaveParainfotoDB()
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
            End Try

        End Sub
        Sub Quit()

          
            Tmrtime.Enabled = False



            Sb1.Items.Item(2).Text = "正在停止轮询与存库操作!"

            RTUs.StopMe()
            DBconn.Stopme()
            GDoComWork.StopMe()

            Sb1.Items.Item(2).Text = "正在保存信息!"

            SaveDatatoDB()
            Sb1.Items.Item(2).Text = "正在停止DTU数据中心!"
            Mydsc.Close()
            Mydsc = Nothing
            Sb1.Items.Item(2).Text = "正在关闭系统组件!"
            OpcSvr.Uninit()
            RTUs = Nothing
            Mbs = Nothing
            DBconn = Nothing
            GItemCol = Nothing
            OpcSvr = Nothing


            Application.ExitThread()
            Application.Exit()




        End Sub
        Sub RestartApp()
            RTUs.StopMe()
            DBconn.Stopme()
            GDoComWork.StopMe()





            OpcSvr.Uninit()
            RTUs = Nothing
            Mbs = Nothing
            DBconn = Nothing
            GItemCol = Nothing
            OpcSvr = Nothing



            '构造dsc,同时也启动服务,也就是在此端口上侦听
            Mydsc = DataServerCenter.GetMyDsc()
            RTUs = New GPRSRTUs
            OpcSvr = New MyOPCserver
            GItemCol = New ItemCollection
            DBconn = New DBconnect
            RTUs.InitRtu(Mydsc)
            RTUs.StartPoll()
            RTUs.StartMe()






            '初始化mbeserver
            Mbs = mbserver.GetMBS(RTUs.ModbusSvrAdapter, RTUs.ModbusSvrPort)


            'Mbs.serialComms.Add "9600,n,8,1", 1 "comm1"
            'Mbs.ShowComdata()

            'Mbs.Hidecomdata()

            InitMBserver(RTUs)
            Me.Show()

            InitMBopcServer()
            InitDBsaveConfig()
            ArrangeWindow()
            InitTcpSocket()
            ReaddataFromDB()
            CurrUsr.Power = 3
        End Sub

        Private Sub 隐藏ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 隐藏ToolStripMenuItem.Click
            Me.Hide()
        End Sub
    End Class
End Namespace