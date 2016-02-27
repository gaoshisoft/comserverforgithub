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
                MsgBox("����Ȩ�޼��𲻹�!���ȵ�¼")
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
                MsgBox("����Ȩ�޼��𲻹�!���ȵ�¼")
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
                MsgBox("����Ȩ�޼��𲻹�!���ȵ�¼")
            End If
        End Sub

        Public Sub m_unregister_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles m_unregister.Click
            CurrUsr.Power = 1
            CurrUsr.UserName = ""
            CurrUsr.Password = ""
            MsgBox("�˳���¼�ɹ�")
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
        '    '�ó���Ϊָ���ֵ�ָ��λд��ֵ��1��0,bitΪ��0��ʼ��λ��ַ
        '    Dim i As Integer
        '    i = Mbs.ReadvalueByAd(deviceID, wordAd, Device.Datatype.�޷�������)
        '    Select Case i And (2 ^ bit)
        '        Case Is = 2 ^ bit '������λԭ��Ϊ1
        '            If value = False Then 'Ҫ����λ��0
        '                i = i - (2 ^ bit)
        '            Else 'Ҫ����λ��1,ԭ����1 ,����ʲôҲ����
        '            End If
        '        Case Is = 0 '������λԭ����0
        '            If value = False Then 'ʲôҲ����
        '            Else '
        '                i = i + (2 ^ bit)
        '            End If
        '    End Select
        '    Mbs.WritevaluebyAd(deviceID, wordAd, Device.Datatype.�޷�������, i)
        'End Sub

        Private Sub mbserver_Writedata(ByRef DeviceAD As Integer, ByRef startAD As String, ByRef Length As Integer, ByRef value As Object) Handles mbserver.Writedata
           
            Select Case DeviceAD
                Case 255 '�ǲ���Ѳ��ȿ�������
                   
                Case Else
                    'ֻ֧��д������ַ
                    RTUs.WriteToRTU(DeviceAD, startAD, Mbs.ReadvalueByAd(DeviceAD, startAD, Device.Datatype.�޷�������)) '

            End Select
        End Sub

       

        Private Sub MDIfmain_Load(ByVal EventSender As System.Object, ByVal EventArgs As System.EventArgs) Handles MyBase.Load

            m_datapoll.Checked = True


            Sb1.Items.Item(1).Text = Mydsc.Dscstate




            Me.SetBounds(0, 0, 0, 0, Windows.Forms.BoundsSpecified.X Or Windows.Forms.BoundsSpecified.Y)
            mbserver = Mbs
            RTUs.StartPoll()
            TreeView1.ExpandAll()
            Sb1.Items.Item(0).Text = "��ɽ������� ��Ȩ����"
            Sb1.Items.Item(4).Text = "Ѳ�ⳬʱ���ã�" & RTUs.Timeout & "s"
            Sb1.Items.Item(4).BackColor = Sb1.Items(2).BackColor
            Sb1.Items.Item(2).DisplayStyle = ToolStripItemDisplayStyle.Text

            Sb1.Items.Item(3).Text = "Ѳ���������ã�" & RTUs.StationPolltime & "s"
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
                    MsgBox("����Ȩ�˳�ϵͳ��")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
            If CurrUsr.Power = 3 Then
                i = MsgBox("���Ҫ�˳���ϵͳ�𣿽�ֹͣһ������ͨѶ����", MsgBoxStyle.OkCancel)
                If i = MsgBoxResult.Ok Then
                    NotifyIcon1.Visible = False
                    NotifyIcon1.Dispose()
                Else

                    e.Cancel = True
                End If
            End If
        End Sub

        Private Sub MBTCPͨ�ż��ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MBTCPͨ�ż��ToolStripMenuItem.Click
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
                        MsgBox("����Ȩ�޼��𲻹�!���ȵ�¼")
                    End If

                End If
                If TreeView1.SelectedNode.Name = "opcItemdis" Then

                    If CurrUsr.Power = 3 Then
                        frmOpc.Show()

                    Else
                        MsgBox("����Ȩ�޼��𲻹�!���ȵ�¼")
                    End If
                End If
                If TreeView1.SelectedNode.Name = "DBfuncnode" Then
                    If CurrUsr.Power = 3 Then
                        FrmDBsave.Show()

                    Else
                        MsgBox("����Ȩ�޼��𲻹�!���ȵ�¼")
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
                        MsgBox("����Ȩ�޼��𲻹�!���ȵ�¼")
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
                    MsgBox("����Ȩ�޼��𲻹�!���ȵ�¼")
                End If

            End If
            If TreeView1.SelectedNode.Name = "opcItemdis" Then

                If CurrUsr.Power = 3 Then
                    frmOpc.Show()
                Else
                    MsgBox("����Ȩ�޼��𲻹�!���ȵ�¼")
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
                    MsgBox("����Ȩ�޼��𲻹�!���ȵ�¼")
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



            Sb1.Items.Item(2).Text = "����ֹͣ��ѯ�������!"

            RTUs.StopMe()
            DBconn.Stopme()
            GDoComWork.StopMe()

            Sb1.Items.Item(2).Text = "���ڱ�����Ϣ!"

            SaveDatatoDB()
            Sb1.Items.Item(2).Text = "����ֹͣDTU��������!"
            Mydsc.Close()
            Mydsc = Nothing
            Sb1.Items.Item(2).Text = "���ڹر�ϵͳ���!"
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



            '����dsc,ͬʱҲ��������,Ҳ�����ڴ˶˿�������
            Mydsc = DataServerCenter.GetMyDsc()
            RTUs = New GPRSRTUs
            OpcSvr = New MyOPCserver
            GItemCol = New ItemCollection
            DBconn = New DBconnect
            RTUs.InitRtu(Mydsc)
            RTUs.StartPoll()
            RTUs.StartMe()






            '��ʼ��mbeserver
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

        Private Sub ����ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ����ToolStripMenuItem.Click
            Me.Hide()
        End Sub
    End Class
End Namespace