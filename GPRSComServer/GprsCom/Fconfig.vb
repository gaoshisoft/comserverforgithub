Option Strict Off
Option Explicit On

Imports System.ComponentModel
Imports System.Data.OleDb
Imports System.IO
Imports System.Xml.Serialization
Imports ADODB
Imports GPRSComServer.GprsCom.DtNode
Imports System.Xml

Namespace GprsCom

    Friend Class Fconfig
        Inherits System.Windows.Forms.Form
        Public MyRtUs As GPRSRTUs






        Private Sub Command2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles DeleteObj.Click
            'On Error Resume Next
            If TreeView1.SelectedNode Is TreeView1.Nodes(0) Then
                Exit Sub
            End If
            If TreeView1.SelectedNode.Parent Is TreeView1.Nodes(0) Then
                Dim R As RtuNode
                R = TreeView1.SelectedNode
                MyRtUs.Remove(R.Rtu.RtuName)
            ElseIf TypeName(TreeView1.SelectedNode.Parent) = "RtuNode" Then
                Dim R As RtuNode

                R = TreeView1.SelectedNode.Parent


                R.Rtu.DTblocks.Remove(TreeView1.SelectedNode.Name)
            End If




            TreeView1.SelectedNode.Remove()

            TreeView1.Refresh()

        End Sub

        Private Sub Cmdaddrtu(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles AddRtu.Click
            Dim rtuN As RtuNode
            Dim r As New GPRSRTU
            r.Enable = True
            r.DeviceAD = 2
            r.CommInfo = "13610983709"
            r.polltime = 5
            r.RtuName = "NewRtu"
            r.MBadressQuantity = 100

            r.CommInfo = r.CommInfo.Substring(0, 8) + Format(Me.MyRtUs.Count + 1, "000").ToString

            rtuN = New RtuNode(TreeView1.Nodes(0), "NewRTU", r)
            Me.MyRtUs.Add(r, rtuN.Name)


        End Sub

        Private Sub CmdClose(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command4.Click
            Me.Close()
        End Sub

        Private Sub CmdSave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command5.Click
            If SaveConfigToXml() = True Then

                SaveDatatoDB()
                RTUs.DisPoseMe()
                RTUs.DisPoseMe()
                RTUs.Clear()
                RTUs.InitRtu(Mydsc)

                For i As Int32 = 1 To RTUs.Count
                    RTUs(i).Dsc = Mydsc
                    If RTUs(i).CommInfo.Contains(":") Then
                        Mydsc.AddTcpClient(RTUs(i).CommInfo)
                    End If
                Next

                ReaddataFromDB()
                InitMBserver(RTUs)
                InitMBopcServer()
                InitDBsaveConfig()

                BindItemToDataBlock() '重新绑定opc 条目与MRdatablock的关系
                MsgBox("新配置已生效！", MsgBoxStyle.OkOnly)

                RTUs.StartPoll()
                frmOnlineDTU.ListView1.Items.Clear()
                Me.Close()
            End If
        End Sub






        Private Sub Fconfig_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
            MyRtUs = New GPRSRTUs
            MyRtUs.InitRtu(Mydsc)
            'RTUs.DisposeMe()
            RTUs.StopPoll()
            '获得真实配置值
            txtStationPoll.Text = CStr(MyRtUs.StationPolltime)
            txtTimeout.Text = CStr(MyRtUs.Timeout)
            Dim i As Integer
            Dim j As Integer
            Dim nodx As System.Windows.Forms.TreeNode '创建变量。
            TreeView1.ShowRootLines = False 'Linestyle 2.

            Dim Rn As RtuNode
            Dim Dtn As Object
            nodx = TreeView1.Nodes.Add("r", "GPRS通讯服务器")
            nodx.Checked = True
            For i = 1 To MyRtUs.Count
                Rn = New RtuNode(nodx, MyRtUs(i).RtuName, MyRtUs(i))

                If MyRtUs(i).ExistDataBlocks Then
                    For j = 1 To MyRtUs(i).DTblocks.Count
                        'Debug.Print(RTUs(i).DTblocks(j).GetType.ToString)
                        Select Case TypeName(MyRtUs(i).DTblocks(j))

                            Case "DataBlock"
                                Dtn = New GPRSComServer.DtNode(Rn, MyRtUs(i).DTblocks(j).BlockName, MyRtUs(i).DTblocks(j))
                            Case "TXDataBlock"
                                Dtn = New TXDtNode(Rn, MyRtUs(i).DTblocks(j).BlockName, MyRtUs(i).DTblocks(j))
                            Case "CNDataBlock"
                                Dtn = New CNDtNode(Rn, MyRtUs(i).DTblocks(j).BlockName, MyRtUs(i).DTblocks(j))
                            Case "XKDataBlock"
                                Dtn = New XKDtNode(Rn, MyRtUs(i).DTblocks(j).BlockName, MyRtUs(i).DTblocks(j))
                            Case "MRDataBlock"
                                Dtn = New MRDtNode(Rn, MyRtUs(i).DTblocks(j).BlockName, MyRtUs(i).DTblocks(j))
                            Case "LDDataBlock"

                                Dtn = New LdDtNode(Rn, MyRtUs(i).DTblocks(j).BlockName, MyRtUs(i).DTblocks(j))
                            Case "CSDataBlock"
                                Dtn = New CSDtNode(Rn, MyRtUs(i).DTblocks(j).BlockName, MyRtUs(i).DTblocks(j))
                            Case "DWDataBlock"
                                Dtn = New DwDtNode(Rn, MyRtUs(i).DTblocks(j).BlockName, MyRtUs(i).DTblocks(j))
                            Case "MBTCPDataBlock"
                                'Dim m As MBTCPDataBlock 
                                Dtn = New MBTCPDtNode(Rn, MyRtUs(i).DTblocks(j).BlockName, MyRtUs(i).DTblocks(j))
                            Case "DW64DataBlock"
                                Dtn = New DW64DtNode(Rn, MyRtUs(i).DTblocks(j).BlockName, MyRtUs(i).DTblocks(j))
                            Case "CorusDataBlock"
                                Dtn = New CorusDtNode(Rn, MyRtUs(i).DTblocks(j).BlockName, MyRtUs(i).DTblocks(j))
                            Case "XiXiangDataBlock"
                                Dtn = New XiXiangDtNode(Rn, MyRtUs(i).DTblocks(j).BlockName, MyRtUs(i).DTblocks(j))
                            Case "TJWJXDataBlock"
                                Dtn = New TJWJXDtNode(Rn, MyRtUs(i).DTblocks(j).BlockName, MyRtUs(i).DTblocks(j))
                            Case "TXCpuCardDataBlock"
                                Dtn = New GeneralDtNode(Rn, MyRtUs(i).DTblocks(j).BlockName, MyRtUs(i).DTblocks(j))
                            Case "HZCDataBlock"
                                Dtn = New GeneralDtNode(Rn, MyRtUs(i).DTblocks(j).BlockName, MyRtUs(i).DTblocks(j))
                            Case "HZHHDataBlock"
                                Dtn = New GeneralDtNode(Rn, MyRtUs(i).DTblocks(j).BlockName, MyRtUs(i).DTblocks(j))
                            Case "SXHTDataBlock"
                                Dtn = New GeneralDtNode(Rn, MyRtUs(i).DTblocks(j).BlockName, MyRtUs(i).DTblocks(j))

                        End Select

                    Next j
                End If
            Next i
            '显示默认设置
            fall.Visible = True
            fdata.Visible = False
            frtu.Visible = False

        End Sub





        Private Sub Fconfig_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
            RTUs.StartPoll()
            MyRtUs.DisPoseMe()
            'myRTUs = Nothing
        End Sub

        Private Sub TreeView1_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TreeView1.DoubleClick
            TreeView1.LabelEdit = True
            TreeView1.SelectedNode.BeginEdit()
        End Sub

        Public Sub AddDataBlockRowsToXml(ByVal xmlDoc As XmlDocument, ByVal xmlRTU As XmlElement, ByVal nodR As RtuNode, ByVal dtNode As TreeNode)

            Dim nodD As GPRSComServer.DtNode
            Dim txNodD As TXDtNode
            Dim cnNodD As CNDtNode
            Dim xkNodD As XKDtNode
            Dim csNodD As CSDtNode
            Dim dwNodD As DwDtNode
            Dim mbTCPnodD As MBTCPDtNode
            Dim dw64Nod As DW64DtNode
            Dim corusNod As CorusDtNode
            Dim xixiangNod As XiXiangDtNode
            Dim tjwjxNodD As TJWJXDtNode
            Dim gDtNode As GeneralDtNode

            Dim xmlDBele As XmlElement
            xmlDBele = xmlRTU.AppendChild(xmlDoc.CreateElement("datablock"))


            Select Case TypeName(dtNode)
                Case "DtNode"

                    nodD = DirectCast(dtNode, GPRSComServer.DtNode)
                    xmlDBele.SetAttribute("blocktype", "Modbus")
                    xmlDBele.SetAttribute("rtuname", dtNode.Parent.Text)
                    xmlDBele.SetAttribute("blockname", dtNode.Text)
                    xmlDBele.SetAttribute("addr", nodD.txtSlaveDevID.Text)
                    xmlDBele.SetAttribute("startad", nodD.txtStartAD.Text)
                    xmlDBele.SetAttribute("length", nodD.txtLength.Text)
                    xmlDBele.SetAttribute("enable", dtNode.Checked)


                Case "TXDtNode"
                    txNodD = DirectCast(dtNode, TXDtNode)

                    xmlDBele.SetAttribute("blocktype", "TXLLJ")
                    xmlDBele.SetAttribute("rtuname", dtNode.Parent.Text)
                    xmlDBele.SetAttribute("blockname", dtNode.Text)
                    xmlDBele.SetAttribute("addr", txNodD.txtSlaveDevID.Text)
                    xmlDBele.SetAttribute("startad", "400001")
                    xmlDBele.SetAttribute("length", 10)
                    xmlDBele.SetAttribute("enable", dtNode.Checked)
                Case "CNDtNode"
                    cnNodD = DirectCast(dtNode, CNDtNode)

                    xmlDBele.SetAttribute("blocktype", "CNLLJ")
                    xmlDBele.SetAttribute("rtuname", dtNode.Parent.Text)
                    xmlDBele.SetAttribute("blockname", dtNode.Text)
                    xmlDBele.SetAttribute("addr", cnNodD.txtSlaveDevID.Text)
                    xmlDBele.SetAttribute("startad", "400001")
                    xmlDBele.SetAttribute("length", 10)
                    xmlDBele.SetAttribute("enable", dtNode.Checked)
                Case "XKDtNode"
                    xkNodD = DirectCast(dtNode, XKDtNode)

                    xmlDBele.SetAttribute("blocktype", "XKLLJ")
                    xmlDBele.SetAttribute("rtuname", dtNode.Parent.Text)
                    xmlDBele.SetAttribute("blockname", dtNode.Text)
                    xmlDBele.SetAttribute("addr", xkNodD.txtSlaveDevID.Text)
                    xmlDBele.SetAttribute("startad", "400001")
                    xmlDBele.SetAttribute("length", 10)
                    xmlDBele.SetAttribute("enable", dtNode.Checked)
                Case "MRDtNode"
                    Dim mrNodD As MRDtNode
                    mrNodD = DirectCast(dtNode, MRDtNode)

                    xmlDBele.SetAttribute("blocktype", "MultiRecord")
                    xmlDBele.SetAttribute("rtuname", dtNode.Parent.Text)
                    xmlDBele.SetAttribute("blockname", dtNode.Text)
                    xmlDBele.SetAttribute("addr", mrNodD.txtSlaveDevID.Text)
                    xmlDBele.SetAttribute("startad", "400001") '主动上传模式没有起始地址，所以存储时永远为40001
                    xmlDBele.SetAttribute("length", 10)
                    xmlDBele.SetAttribute("enable", dtNode.Checked)
                Case "LDDtNode"


                    xmlDBele.SetAttribute("blocktype", "LDDataBlock")
                    xmlDBele.SetAttribute("rtuname", dtNode.Parent.Text)
                    xmlDBele.SetAttribute("blockname", dtNode.Text)
                    xmlDBele.SetAttribute("addr", 2)
                    xmlDBele.SetAttribute("startad", "400001") '主动上传模式没有起始地址，所以存储时永远为40001
                    xmlDBele.SetAttribute("length", 10)
                    xmlDBele.SetAttribute("enable", dtNode.Checked)
                Case "CSDtNode"
                    csNodD = DirectCast(dtNode, CSDtNode)

                    xmlDBele.SetAttribute("blocktype", "CSLLJ")
                    xmlDBele.SetAttribute("rtuname", dtNode.Parent.Text)
                    xmlDBele.SetAttribute("blockname", dtNode.Text)
                    xmlDBele.SetAttribute("addr", csNodD.txtSlaveDevID.Text)
                    xmlDBele.SetAttribute("startad", "400001")
                    xmlDBele.SetAttribute("length", 10)
                    xmlDBele.SetAttribute("enable", dtNode.Checked)
                Case "DWDtNode"
                    dwNodD = DirectCast(dtNode, DwDtNode)


                    xmlDBele.SetAttribute("blocktype", "DWLLJ")
                    xmlDBele.SetAttribute("rtuname", dtNode.Parent.Text)
                    xmlDBele.SetAttribute("blockname", dtNode.Text)
                    xmlDBele.SetAttribute("addr", dwNodD.TxtSlaveDevId.Text)
                    xmlDBele.SetAttribute("startad", "400001")
                    xmlDBele.SetAttribute("length", Val(dwNodD.TxtXs.Text))
                    xmlDBele.SetAttribute("enable", dtNode.Checked)
                Case "MBTCPDtNode"
                    mbTCPnodD = DirectCast(dtNode, MBTCPDtNode)

                    xmlDBele.SetAttribute("blocktype", "ModbusTCP")
                    xmlDBele.SetAttribute("rtuname", dtNode.Parent.Text)
                    xmlDBele.SetAttribute("blockname", dtNode.Text)
                    xmlDBele.SetAttribute("addr", mbTCPnodD.txtSlaveDevID.Text)
                    xmlDBele.SetAttribute("startad", mbTCPnodD.txtStartAD.Text)
                    xmlDBele.SetAttribute("length", mbTCPnodD.txtLength.Text)
                    xmlDBele.SetAttribute("enable", dtNode.Checked)
                Case "DW64DtNode"
                    dw64Nod = DirectCast(dtNode, DW64DtNode)

                    xmlDBele.SetAttribute("blocktype", "DW64LLJ")
                    xmlDBele.SetAttribute("rtuname", dtNode.Parent.Text)
                    xmlDBele.SetAttribute("blockname", dtNode.Text)
                    xmlDBele.SetAttribute("addr", dw64Nod.txtSlaveDevID.Text)
                    xmlDBele.SetAttribute("startad", "400001")
                    xmlDBele.SetAttribute("length", Val(dw64Nod.txtXs.Text))
                    xmlDBele.SetAttribute("enable", dtNode.Checked)
                Case "CorusDtNode"

                    corusNod = DirectCast(dtNode, CorusDtNode)

                    xmlDBele.SetAttribute("blocktype", "CorusLLJ")
                    xmlDBele.SetAttribute("rtuname", dtNode.Parent.Text)
                    xmlDBele.SetAttribute("blockname", dtNode.Text)
                    xmlDBele.SetAttribute("addr", corusNod.txtSlaveDevID.Text)
                    xmlDBele.SetAttribute("startad", "400001")
                    xmlDBele.SetAttribute("length", 24)
                    xmlDBele.SetAttribute("enable", dtNode.Checked)
                Case "XiXiangDtNode"

                    xixiangNod = DirectCast(dtNode, XiXiangDtNode)

                    xmlDBele.SetAttribute("blocktype", "XiXiangLLJ")
                    xmlDBele.SetAttribute("rtuname", dtNode.Parent.Text)
                    xmlDBele.SetAttribute("blockname", dtNode.Text)
                    xmlDBele.SetAttribute("addr", xixiangNod.txtSlaveDevID.Text)
                    xmlDBele.SetAttribute("startad", "400001")
                    xmlDBele.SetAttribute("length", 24)
                    xmlDBele.SetAttribute("enable", dtNode.Checked)
                Case "TJWJXDtNode"
                    tjwjxNodD = DirectCast(dtNode, TJWJXDtNode)

                    xmlDBele.SetAttribute("blocktype", "TJWJXLLJ")
                    xmlDBele.SetAttribute("rtuname", dtNode.Parent.Text)
                    xmlDBele.SetAttribute("blockname", dtNode.Text)
                    xmlDBele.SetAttribute("addr", tjwjxNodD.txtSlaveDevID.Text)
                    xmlDBele.SetAttribute("startad", "400001")
                    xmlDBele.SetAttribute("length", 24)
                    xmlDBele.SetAttribute("enable", dtNode.Checked)
                Case "GeneralDtNode"
                    gDtNode = DirectCast(dtNode, GeneralDtNode)

                    xmlDBele.SetAttribute("blocktype", TypeName(gDtNode.DtBlock))
                    xmlDBele.SetAttribute("rtuname", dtNode.Parent.Text)
                    xmlDBele.SetAttribute("blockname", dtNode.Text)
                    xmlDBele.SetAttribute("addr", gDtNode.TxtSlaveDevId.Text)
                    xmlDBele.SetAttribute("startad", "400001")
                    xmlDBele.SetAttribute("length", 10)
                    xmlDBele.SetAttribute("enable", dtNode.Checked)
            End Select



        End Sub

        Private Sub txtStationPoll_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtStationPoll.Validating
            Dim Cancel As Boolean = eventArgs.Cancel
            If Not Isnumber((txtStationPoll.Text)) Then
                MsgBox("请输入数字", MsgBoxStyle.OkOnly)
                Cancel = True
            End If
            If Val(txtStationPoll.Text) = 0 Or Val(txtStationPoll.Text) > 65535 Then
                MsgBox("请输入0--65535之间的数字", MsgBoxStyle.OkOnly)
                Cancel = True
            End If

            eventArgs.Cancel = Cancel
        End Sub


        Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
            Dim Rn As RtuNode
            Dim Dtn As GPRSComServer.DtNode
            Dim TXDtn As TXDtNode
            Dim GDtn As GeneralDtNode
            Dim CNDtn As CNDtNode
            Dim XKDtn As XKDtNode
            Dim TJWJXDtn As TJWJXDtNode
            If TreeView1.SelectedNode.Text = "GPRS通讯服务器" Then

                fall.Visible = True
                frtu.Visible = False
                fdata.Visible = False
            ElseIf TreeView1.SelectedNode.Parent.Text = "GPRS通讯服务器" Then

                Rn = TreeView1.SelectedNode
                TXTrtuid.Text = Rn.Index.ToString
                Rn.DisRtu()
                fall.Visible = False
                frtu.Visible = True
                fdata.Visible = False
            Else


                'If TypeOf (TreeView1.SelectedNode) Is MeterDtNode Then

                Select Case TypeName(TreeView1.SelectedNode)

                    Case "DtNode"

                        Dtn = TreeView1.SelectedNode
                        TxtDataRTuID.Text = Dtn.Parent.Index.ToString
                        txtDatablockid.Text = Dtn.Index.ToString
                        Dtn.DisDataBlock()
                    Case "TXDtNode"
                        TXDtn = TreeView1.SelectedNode
                        TxtDataRTuID.Text = TXDtn.Parent.Index.ToString
                        txtDatablockid.Text = TXDtn.Index.ToString
                        TXDtn.DisDataBlock()

                    Case "CNDtNode"
                        CNDtn = TreeView1.SelectedNode
                        TxtDataRTuID.Text = CNDtn.Parent.Index.ToString
                        txtDatablockid.Text = CNDtn.Index.ToString
                        CNDtn.DisDataBlock()
                    Case "XKDtNode"
                        XKDtn = TreeView1.SelectedNode
                        TxtDataRTuID.Text = XKDtn.Parent.Index.ToString
                        txtDatablockid.Text = XKDtn.Index.ToString
                        XKDtn.DisDataBlock()
                    Case "MRDtNode"
                        Dim MRdtn As MRDtNode
                        MRdtn = TreeView1.SelectedNode
                        TxtDataRTuID.Text = MRdtn.Parent.Index.ToString
                        txtDatablockid.Text = MRdtn.Index.ToString
                        MRdtn.DisDataBlock()
                    Case "LDDtNode"
                        Dim LDdtn As LdDtNode
                        LDdtn = TreeView1.SelectedNode
                        TxtDataRTuID.Text = LDdtn.Parent.Index.ToString
                        txtDatablockid.Text = LDdtn.Index.ToString
                        LDdtn.DisDataBlock()
                    Case "CSDtNode"
                        Dim CSdtn As CSDtNode
                        CSdtn = TreeView1.SelectedNode
                        TxtDataRTuID.Text = CSdtn.Parent.Index.ToString
                        txtDatablockid.Text = CSdtn.Index.ToString
                        CSdtn.DisDataBlock()
                    Case "DWDtNode"
                        Dim DWdtn As DwDtNode
                        DWdtn = TreeView1.SelectedNode
                        TxtDataRTuID.Text = DWdtn.Parent.Index.ToString
                        txtDatablockid.Text = DWdtn.Index.ToString
                        DWdtn.DisDataBlock()
                    Case "MBTCPDtNode"
                        Dim MBTD As MBTCPDtNode
                        MBTD = TreeView1.SelectedNode
                        TxtDataRTuID.Text = MBTD.Parent.Index.ToString
                        txtDatablockid.Text = MBTD.Index.ToString
                        MBTD.DisDataBlock()
                    Case "DW64DtNode"
                        Dim DWdtn As DW64DtNode
                        DWdtn = TreeView1.SelectedNode
                        TxtDataRTuID.Text = DWdtn.Parent.Index.ToString
                        txtDatablockid.Text = DWdtn.Index.ToString
                        DWdtn.DisDataBlock()
                    Case "CorusDtNode"
                        Dim DWdtn As CorusDtNode
                        DWdtn = TreeView1.SelectedNode
                        TxtDataRTuID.Text = DWdtn.Parent.Index.ToString
                        txtDatablockid.Text = DWdtn.Index.ToString
                        DWdtn.DisDataBlock()
                    Case "XiXiangDtNode"
                        Dim DWdtn As XiXiangDtNode
                        DWdtn = TreeView1.SelectedNode
                        TxtDataRTuID.Text = DWdtn.Parent.Index.ToString
                        txtDatablockid.Text = DWdtn.Index.ToString
                        DWdtn.DisDataBlock()
                    Case "TJWJXDtNode"
                        TJWJXDtn = TreeView1.SelectedNode
                        TxtDataRTuID.Text = TJWJXDtn.Parent.Index.ToString
                        txtDatablockid.Text = TJWJXDtn.Index.ToString
                        TJWJXDtn.DisDataBlock()
                    Case "GeneralDtNode"
                        GDtn = TreeView1.SelectedNode
                        TxtDataRTuID.Text = GDtn.Parent.Index.ToString
                        txtDatablockid.Text = GDtn.Index.ToString
                        GDtn.DisDataBlock()

                End Select

                fall.Visible = False
                frtu.Visible = False
                fdata.Visible = True
            End If

        End Sub


        Private Sub Command1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles AddDB.MouseUp
            mnuAddDB.Show(Me.AddDB, New Point(e.X, e.Y))
        End Sub

        Private Sub mnuAddDB_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles mnuAddDB.Opening

        End Sub

        Private Sub 增加Modbus数据块_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 增加Modbus数据块.Click


            If TreeView1.SelectedNode.Parent.Name <> "r" Then
                Exit Sub
            End If
            Dim N As RtuNode
            N = TreeView1.SelectedNode
            Dim D As New DataBlock
            D.Addr = 1
            D.BlockName = "MBDataBlock"
            D.Enable = True
            D.Length = 10
            D.startAD = "400001"
            D.SvrAddrLength = 10
            D.SvrDevAd = N.Rtu.DeviceAD

            Dim Nod As GPRSComServer.DtNode
            On Error GoTo errHandle
            If TreeView1.SelectedNode.Parent.Name = "r" Then

                Nod = New GPRSComServer.DtNode(TreeView1.SelectedNode, "MBDataBlock", D)

            End If

            N.Rtu.DTblocks.Add(D, D.BlockName)
            N.Rtu.DTblocks.CalcuDataBlockaddr()
errHandle:
        End Sub

        Private Sub 增加天信流量计数据_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 增加天信流量计数据.Click

            If TreeView1.SelectedNode.Parent.Name <> "r" Then
                Exit Sub
            End If
            Dim N As RtuNode

            N = TreeView1.SelectedNode
            Dim D As New TXDataBlock
            D.Addr = 2
            D.BlockName = "TXDataBlock"
            D.Enable = True

            D.SvrDevAd = N.Rtu.DeviceAD
            D.SvrAddrLength = 10 '固定为10个,然后把读到的参数解析后存到这十个地址里，存不满空着

            Dim Nod As TXDtNode
            On Error GoTo errHandle
            If TreeView1.SelectedNode.Parent.Name = "r" Then

                Nod = New TXDtNode(TreeView1.SelectedNode, "TXDataBlock", D)

            End If

            N.Rtu.DTblocks.Add(D, D.BlockName)
            N.Rtu.DTblocks.CalcuDataBlockaddr()
errHandle:
        End Sub


        Private Sub 增加苍南流量计数据ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 增加苍南流量计数据ToolStripMenuItem.Click
            If TreeView1.SelectedNode.Parent.Name <> "r" Then
                Exit Sub
            End If
            Dim N As RtuNode

            N = TreeView1.SelectedNode
            Dim D As New CNDataBlock
            D.Addr = 23
            D.BlockName = "CNDataBlock"
            D.Enable = True

            D.SvrDevAd = N.Rtu.DeviceAD
            D.SvrAddrLength = 20

            Dim Nod As CNDtNode
            On Error GoTo errHandle
            If TreeView1.SelectedNode.Parent.Name = "r" Then

                Nod = New CNDtNode(TreeView1.SelectedNode, "CNDataBlock", D)

            End If

            N.Rtu.DTblocks.Add(D, D.BlockName)
            N.Rtu.DTblocks.CalcuDataBlockaddr()
errHandle:
        End Sub

        Private Sub 增加新科流量计数据ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 增加新科流量计数据ToolStripMenuItem.Click

            If TreeView1.SelectedNode.Parent.Name <> "r" Then
                Exit Sub
            End If
            Dim N As RtuNode

            N = TreeView1.SelectedNode
            Dim D As New XKDataBlock
            D.Addr = 12
            D.BlockName = "XKDataBlock"
            D.Enable = True
            'R.Length = 10
            'R.startad = "400001"
            D.SvrDevAd = N.Rtu.DeviceAD
            D.SvrAddrLength = 20

            Dim Nod As XKDtNode

            If TreeView1.SelectedNode.Parent.Name = "r" Then

                Nod = New XKDtNode(TreeView1.SelectedNode, "XKDataBlock", D)

            End If

            N.Rtu.DTblocks.Add(D, D.BlockName)
            N.Rtu.DTblocks.CalcuDataBlockaddr()
        End Sub



        Private Sub 自定义主动上传ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 自定义主动上传ToolStripMenuItem.Click
            '主动上传模式与其它模式不同点在于两点，第一点为主动上传，这样就不用去指定起始地址了，只须指定单记录长度即可，第二点为不是单记录而是每次上传的数据是多条记录的组合。
            '我们所要做的是把这个数据包中的数据一条一条地分解出来然后再按照单条记录的处理办法去做，这样做会最大限度的利用已有的代码。
            If TreeView1.SelectedNode.Parent.Name <> "r" Then
                Exit Sub
            End If
            Dim N As RtuNode
            N = TreeView1.SelectedNode
            Dim D As New MRDataBlock
            D.Addr = 1
            D.BlockName = "MRDataBlock"
            D.Enable = True
            D.SingleRecordLength = 5
            D.SvrAddrLength = 5

            D.SvrDevAd = N.Rtu.DeviceAD

            Dim Nod As MRDtNode
            On Error GoTo errHandle
            If TreeView1.SelectedNode.Parent.Name = "r" Then

                Nod = New MRDtNode(TreeView1.SelectedNode, "MRDataBlock", D)

            End If

            N.Rtu.DTblocks.Add(D, D.BlockName)
            N.Rtu.DTblocks.CalcuDataBlockaddr()
errHandle:
        End Sub



        Private Sub 蓝迪主动上传ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 蓝迪主动上传ToolStripMenuItem.Click

            If TreeView1.SelectedNode.Parent.Name <> "r" Then
                Exit Sub
            End If
            Dim N As RtuNode
            N = TreeView1.SelectedNode
            Dim D As New LDDataBlock
            D.Addr = 1
            D.BlockName = "LDDataBlock"
            D.Enable = True
            D.SingleRecordLength = 100
            D.SvrAddrLength = 100

            D.SvrDevAd = N.Rtu.DeviceAD
            D.RegToMBcol = RTUs.RegToMBTbl
            'D.SvrMBADStart = N.Rtu.DTblocks.GetaddrSpace + 1
            Dim Nod As LdDtNode
            On Error GoTo errHandle
            If TreeView1.SelectedNode.Parent.Name = "r" Then

                Nod = New LdDtNode(TreeView1.SelectedNode, "LDDataBlock", D)

            End If
            'D.BlockName = Nod.DtBlock.BlockName
            N.Rtu.DTblocks.Add(D, D.BlockName)
            N.Rtu.DTblocks.CalcuDataBlockaddr()
errHandle:
        End Sub



        Private Sub lstRegToMB_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstRegToMB.DoubleClick
            'lstRegToMB.SelectedItems(0).SubItems(0).

        End Sub


        'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        '    Dim Itm As ListViewItem
        '    Itm = lstRegToMB.Items.Add("Itmx")
        '    Itm.SubItems.Add("test")
        '    Itm.Text = "00"
        '    Itm.SubItems(1).Text = "400001"
        'End Sub

        Private Sub NingboToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NingboToolStripMenuItem.Click
            If TreeView1.SelectedNode.Parent.Name <> "r" Then
                Exit Sub
            End If
            Dim N As RtuNode

            N = TreeView1.SelectedNode
            Dim D As New CSDataBlock
            D.Addr = "11"
            D.BlockName = "CSDataBlock"
            D.Enable = True
            'R.Length = 10
            'R.startad = "400001"
            D.SvrDevAd = N.Rtu.DeviceAD
            D.SvrAddrLength = 20
            'D.SvrMBADStart = N.Rtu.DTblocks.GetaddrSpace + 1
            Dim Nod As CSDtNode
            'On Error GoTo errHandle
            If TreeView1.SelectedNode.Parent.Name = "r" Then

                Nod = New CSDtNode(TreeView1.SelectedNode, "CSDataBlock", D)

            End If
            'D.BlockName = Nod.DtBlock.BlockName
            N.Rtu.DTblocks.Add(D, D.BlockName)
            N.Rtu.DTblocks.CalcuDataBlockaddr()
        End Sub


        Private Sub btntxgenItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        End Sub

        Private Sub 德闻流量计ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 德闻流量计ToolStripMenuItem.Click
            If TreeView1.SelectedNode.Parent.Name <> "r" Then
                Exit Sub
            End If
            Dim N As RtuNode

            N = TreeView1.SelectedNode
            Dim D As New DWDataBlock
            D.Addr = "11"
            D.LjllXS = 1
            D.BlockName = "DWDataBlock"
            D.Enable = True
            'R.Length = 10
            'R.startad = "400001"
            D.SvrDevAd = N.Rtu.DeviceAD
            D.SvrAddrLength = 20
            'D.SvrMBADStart = N.Rtu.DTblocks.GetaddrSpace + 1
            Dim Nod As DwDtNode
            'On Error GoTo errHandle
            If TreeView1.SelectedNode.Parent.Name = "r" Then

                Nod = New DwDtNode(TreeView1.SelectedNode, "DWDataBlock", D)

            End If
            'D.BlockName = Nod.DtBlock.BlockName
            N.Rtu.DTblocks.Add(D, D.BlockName)
            N.Rtu.DTblocks.CalcuDataBlockaddr()
        End Sub


        Private Sub ModbusTcp数据块ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ModbusTcp数据块ToolStripMenuItem.Click
            If TreeView1.SelectedNode.Parent.Name <> "r" Then
                Exit Sub
            End If
            Dim N As RtuNode
            N = TreeView1.SelectedNode
            Dim D As New MBTCPDataBlock
            D.Addr = 2
            D.BlockName = "MBDataBlock"
            D.Enable = True
            D.Length = 10
            D.startAD = "400001"
            D.SvrAddrLength = 20
            D.SvrDevAd = N.Rtu.DeviceAD
            'D.SvrMBADStart = N.Rtu.DTblocks.GetaddrSpace + 1
            Dim Nod As MBTCPDtNode
            On Error GoTo errHandle
            If TreeView1.SelectedNode.Parent.Name = "r" Then

                Nod = New MBTCPDtNode(TreeView1.SelectedNode, "MBTCPDataBlock", D)

            End If
            'D.BlockName = Nod.DtBlock.BlockName
            N.Rtu.DTblocks.Add(D, D.BlockName)
            N.Rtu.DTblocks.CalcuDataBlockaddr()
errHandle:
        End Sub


        Private Sub 德闻64位浮点ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 德闻64位浮点ToolStripMenuItem.Click
            If TreeView1.SelectedNode.Parent.Name <> "r" Then
                Exit Sub
            End If
            Dim N As RtuNode

            N = TreeView1.SelectedNode
            Dim D As New DW64DataBlock
            D.Addr = "1"
            D.LjllXS = 1
            D.BlockName = "DW64DataBlock"
            D.Enable = True
            'R.Length = 10
            'R.startad = "400001"
            D.SvrDevAd = N.Rtu.DeviceAD
            D.SvrAddrLength = 20
            'D.SvrMBADStart = N.Rtu.DTblocks.GetaddrSpace + 1
            Dim Nod As DW64DtNode
            'On Error GoTo errHandle
            If TreeView1.SelectedNode.Parent.Name = "r" Then

                Nod = New DW64DtNode(TreeView1.SelectedNode, "DW64DataBlock", D)

            End If
            'D.BlockName = Nod.DtBlock.BlockName
            N.Rtu.DTblocks.Add(D, D.BlockName)
            N.Rtu.DTblocks.CalcuDataBlockaddr()
        End Sub

        Private Sub Corus流量计ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Corus流量计ToolStripMenuItem.Click
            If TreeView1.SelectedNode.Parent.Name <> "r" Then
                Exit Sub
            End If
            Dim N As RtuNode

            N = TreeView1.SelectedNode
            Dim D As New CorusDataBlock
            D.Addr = "11"
            D.LjllXS = 1
            D.BlockName = "CorusDataBlock"
            D.Enable = True
            'R.Length = 10
            'R.startad = "400001"
            D.SvrDevAd = N.Rtu.DeviceAD
            D.SvrAddrLength = 20
            'D.SvrMBADStart = N.Rtu.DTblocks.GetaddrSpace + 1
            Dim Nod As CorusDtNode
            'On Error GoTo errHandle
            If TreeView1.SelectedNode.Parent.Name = "r" Then

                Nod = New CorusDtNode(TreeView1.SelectedNode, "CorusDataBlock", D)

            End If
            'D.BlockName = Nod.DtBlock.BlockName
            N.Rtu.DTblocks.Add(D, D.BlockName)
            N.Rtu.DTblocks.CalcuDataBlockaddr()
        End Sub

        Private Sub 矽翔流量计ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 矽翔流量计ToolStripMenuItem.Click
            If TreeView1.SelectedNode.Parent.Name <> "r" Then
                Exit Sub
            End If
            Dim N As RtuNode

            N = TreeView1.SelectedNode
            Dim D As New XiXiangDataBlock
            D.Addr = "1"
            D.LjllXS = 1
            D.BlockName = "XiXiangDataBlock"
            D.Enable = True
            'R.Length = 10
            'R.startad = "400001"
            D.SvrDevAd = N.Rtu.DeviceAD
            D.SvrAddrLength = 20
            'D.SvrMBADStart = N.Rtu.DTblocks.GetaddrSpace + 1
            Dim Nod As XiXiangDtNode
            'On Error GoTo errHandle
            If TreeView1.SelectedNode.Parent.Name = "r" Then

                Nod = New XiXiangDtNode(TreeView1.SelectedNode, "XiXiangDataBlock", D)

            End If
            'D.BlockName = Nod.DtBlock.BlockName
            N.Rtu.DTblocks.Add(D, D.BlockName)
            N.Rtu.DTblocks.CalcuDataBlockaddr()
        End Sub



        Private Sub 天津第五机械厂流量计ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 天津第五机械厂流量计ToolStripMenuItem.Click

            If TreeView1.SelectedNode.Parent.Name <> "r" Then
                Exit Sub
            End If
            Dim N As RtuNode

            N = TreeView1.SelectedNode
            Dim D As New TJWJXDataBlock
            D.Addr = 1
            D.BlockName = "TJWJXDataBlock"
            D.Enable = True
            'R.Length = 10
            'R.startad = "400001"
            D.SvrDevAd = N.Rtu.DeviceAD
            D.SvrAddrLength = 20
            'D.SvrMBADStart = N.Rtu.DTblocks.GetaddrSpace + 1
            Dim Nod As TJWJXDtNode
            'On Error GoTo errHandle
            If TreeView1.SelectedNode.Parent.Name = "r" Then

                Nod = New TJWJXDtNode(TreeView1.SelectedNode, "TJWJXDataBlock", D)

            End If
            'D.BlockName = Nod.DtBlock.BlockName
            N.Rtu.DTblocks.Add(D, D.BlockName)
            N.Rtu.DTblocks.CalcuDataBlockaddr()
        End Sub



        Private Sub ToolStrip天信CPU卡_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStrip天信CPU卡.Click
            If TreeView1.SelectedNode.Parent.Name <> "r" Then
                Exit Sub
            End If
            Dim N As RtuNode

            N = TreeView1.SelectedNode
            Dim D As New TXCpuCardDataBlock
            D.Addr = 2
            D.BlockName = TypeName(D)
            D.Enable = True
            'R.Length = 10
            'R.startad = "400001"
            D.SvrDevAd = N.Rtu.DeviceAD
            D.SvrAddrLength = 20 '固定为20个,然后把读到的参数解析后存到这20个地址里，存不满空着
            'D.SvrMBADStart = N.Rtu.DTblocks.GetaddrSpace + 1
            Dim Nod As GeneralDtNode
            On Error GoTo errHandle
            If TreeView1.SelectedNode.Parent.Name = "r" Then

                Nod = New GeneralDtNode(TreeView1.SelectedNode, D.BlockName, D)

            End If
            'D.BlockName = Nod.DtBlock.BlockName
            N.Rtu.DTblocks.Add(D, D.BlockName)
            N.Rtu.DTblocks.CalcuDataBlockaddr()
errHandle:
        End Sub

        Private Sub 哈尔滨中辰ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 哈尔滨中辰ToolStripMenuItem.Click
            If TreeView1.SelectedNode.Parent.Name <> "r" Then
                Exit Sub
            End If
            Dim N As RtuNode

            N = TreeView1.SelectedNode
            Dim D As New HZCDataBlock
            D.Addr = 0
            D.BlockName = TypeName(D)
            D.Enable = True
            'R.Length = 10
            'R.startad = "400001"
            D.SvrDevAd = N.Rtu.DeviceAD
            D.SvrAddrLength = 20 '固定为20个,然后把读到的参数解析后存到这20个地址里，存不满空着
            'D.SvrMBADStart = N.Rtu.DTblocks.GetaddrSpace + 1
            Dim Nod As GeneralDtNode
            Try
                If TreeView1.SelectedNode.Parent.Name = "r" Then

                    Nod = New GeneralDtNode(TreeView1.SelectedNode, D.BlockName, D)

                End If
                'D.BlockName = Nod.DtBlock.BlockName
                N.Rtu.DTblocks.Add(D, D.BlockName)
                N.Rtu.DTblocks.CalcuDataBlockaddr()
            Catch exc As System.Exception
                Throw exc
            Finally
            End Try
        End Sub

        Private Sub 杭州鸿鹄ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 杭州鸿鹄ToolStripMenuItem.Click
            If TreeView1.SelectedNode.Parent.Name <> "r" Then
                Exit Sub
            End If
            Dim N As RtuNode

            N = TreeView1.SelectedNode
            Dim D As New HZHHDataBlock
            D.Addr = 1
            D.BlockName = TypeName(D)
            D.Enable = True
            'R.Length = 10
            'R.startad = "400001"
            D.SvrDevAd = N.Rtu.DeviceAD
            D.SvrAddrLength = 30 '固定为30个,然后把读到的参数解析后存到这20个地址里，存不满空着
            'D.SvrMBADStart = N.Rtu.DTblocks.GetaddrSpace + 1
            Dim Nod As GeneralDtNode
            Try
                If TreeView1.SelectedNode.Parent.Name = "r" Then

                    Nod = New GeneralDtNode(TreeView1.SelectedNode, D.BlockName, D)

                End If
                'D.BlockName = Nod.DtBlock.BlockName
                N.Rtu.DTblocks.Add(D, D.BlockName)
                N.Rtu.DTblocks.CalcuDataBlockaddr()
            Catch exc As System.Exception
                Throw exc
            Finally
            End Try
        End Sub

        Private Sub 陕西航天能源ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 陕西航天能源ToolStripMenuItem.Click
            If TreeView1.SelectedNode.Parent.Name <> "r" Then
                Exit Sub
            End If
            Dim N As RtuNode

            N = TreeView1.SelectedNode
            Dim D As New SXHTDataBlock
            D.Addr = 1
            D.BlockName = TypeName(D)
            D.Enable = True
            'R.Length = 10
            'R.startad = "400001"
            D.SvrDevAd = N.Rtu.DeviceAD
            D.SvrAddrLength = 30 '固定为30个,然后把读到的参数解析后存到这20个地址里，存不满空着
            'D.SvrMBADStart = N.Rtu.DTblocks.GetaddrSpace + 1
            Dim Nod As GeneralDtNode
            Try
                If TreeView1.SelectedNode.Parent.Name = "r" Then

                    Nod = New GeneralDtNode(TreeView1.SelectedNode, D.BlockName, D)

                End If
                'D.BlockName = Nod.DtBlock.BlockName
                N.Rtu.DTblocks.Add(D, D.BlockName)
                N.Rtu.DTblocks.CalcuDataBlockaddr()
            Catch exc As System.Exception
                Throw exc
            Finally
            End Try
        End Sub


        Function SaveConfigToXml() As Boolean
            Dim configDoc As XmlDocument
            configDoc = New XmlDocument

            'configDoc.LoadXml("<?xml version=""1.0"" encoding=""UTF-8""?><root/>")
            configDoc.Load((My.Application.Info.DirectoryPath & "\RTUconfig.xml"))
            Dim xmlparent As XmlElement
            Dim xmlchild As XmlElement
            xmlparent = configDoc.SelectSingleNode("root/rtucomminfo")
            If xmlparent Is Nothing Then
                xmlparent = configDoc.SelectSingleNode("root").AppendChild(configDoc.CreateElement("rtucomminfo"))
            End If



            xmlparent.RemoveAll()

            Dim i As Integer
            Dim j As Integer



            Dim nodx As TreeNode
            Dim NodR As RtuNode

            For i = 0 To TreeView1.Nodes(0).Nodes.Count - 1
                nodx = TreeView1.Nodes(0).Nodes(i)

                NodR = DirectCast(nodx, RtuNode)


                xmlchild = xmlparent.AppendChild(configDoc.CreateElement("rtu"))
                xmlchild.SetAttribute("rtuname", NodR.Text)
                xmlchild.SetAttribute("comminfo", NodR.GetComminfoFromNode)
                xmlchild.SetAttribute("mbadqty", NodR.TxtSvrAddrNum.Text)
                xmlchild.SetAttribute("devicead", NodR.TxtSvrDevId.Text)
                xmlchild.SetAttribute("polltime", NodR.CboCycle.Text)
                xmlchild.SetAttribute("enable", NodR.Checked)
                xmlchild.SetAttribute("sourcestartad", 0)
                xmlchild.SetAttribute("sourcelen", 0)
                xmlchild.SetAttribute("destinstartad", 0)


                If nodx.Nodes.Count > 0 Then


                    For j = 0 To nodx.Nodes.Count - 1


                        NodR = DirectCast(nodx, RtuNode)
                        AddDataBlockRowsToXml(configDoc, xmlchild, NodR, nodx.Nodes(j))

                    Next j

                End If

            Next i


            xmlparent.SetAttribute("rtupollcycle", txtStationPoll.Text)
            xmlparent.SetAttribute("timeout", txtTimeout.Text)



            configDoc.Save(My.Application.Info.DirectoryPath & "\RTUconfig.xml")
            SaveConfigToXml = True
        End Function

       
    End Class
End Namespace