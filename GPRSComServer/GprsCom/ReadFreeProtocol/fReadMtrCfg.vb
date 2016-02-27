Imports System.Xml.Linq
'Imports System.Xml
Public Class fReadMtrCfg
  
    Function ReadFromXML() As AllMeterDataToMB
        Dim Xe As XElement = XElement.Load("config.xml")
        Dim Stns As AllMeterDataToMB = New AllMeterDataToMB

        Dim XN As XElement
        Dim XN1 As XElement
        'XN = Xe.FirstNode
        For Each XN In Xe.Nodes
            Dim Flow As ZJTXFlow = New ZJTXFlow
            Dim XA As XAttribute
            Dim XA1 As XAttribute
            For Each XA In XN.Attributes
                Select Case XA.Name
                    Case "StationName"
                        Flow.StationName = XA.Value
                        'Case "MBAD"
                        '    Flow.MBAD = XA.Value

                    Case "PhoneNumber"
                        Flow.PhoneNumber = XA.Value
                    Case "PollTime"
                        Flow.PollTime = XA.Value
                    Case "TimeOut"
                        Flow.TimeOut = XA.Value
                    Case "Enable"
                        Flow.Enable = XA.Value

                End Select

            Next
            If XN.Nodes.Count > 0 Then
                For Each XN1 In XN.Nodes
                    Dim DT As TXDataBlock = New TXDataBlock
                    For Each XA1 In XN1.Attributes
                        Select Case XA1.Name
                            Case "BlockName"
                                DT.BlockName = XA1.Value
                            Case "MeterAddr"
                                DT.Addr = XA1.Value
                            Case "Enable"
                                DT.Enable = XA1.Value

                        End Select
                    Next
                    Flow.DtBlocks.Add(DT)

                Next
            End If
            Stns.AddStn(Flow)
        Next

        ReadFromXML = Stns

        ''Dim XR As New Xml.XmlReader
        'XD = XR


    End Function




    Private Sub fReadMtrCfg_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'txtStationPoll.Text = CStr(RTUs.StationPolltime)
        'txtTimeout.Text = CStr(RTUs.Timeout)
        Dim i As Integer
        Dim j As Integer
        Dim AMD As AllMeterDataToMB
        Dim nodx As System.Windows.Forms.TreeNode '创建变量。
        TreeView1.ShowRootLines = False 'Linestyle 2.
        AMD = Me.ReadFromXML
        Dim Rn As stationNode
        Dim Dtn As TXDtNode
        nodx = TreeView1.Nodes.Add("r", "GPRS通讯服务器")
        nodx.Checked = True
        For i = 0 To AMD.FlowMeters.Count - 1
            Rn = New stationNode(nodx, AMD.FlowMeters(i).StationName, AMD.FlowMeters(i))

            If AMD.FlowMeters(i).DtBlocks.Count > 0 Then
                For j = 0 To AMD.FlowMeters(i).DtBlocks.Count - 1
                    Dtn = New TXDtNode(Rn, AMD.FlowMeters(i).DtBlocks(j).BlockName, AMD.FlowMeters(i).DtBlocks(j))
                Next j
            End If
        Next i
        '显示默认设置
        'fall.Visible = True
        fdata.Visible = False
        frtu.Visible = True
    End Sub

    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
        Dim Rn As stationNode
        Dim Dtn As TXDtNode
        If TreeView1.SelectedNode.Text = "GPRS通讯服务器" Then

            'fall.Visible = True
            frtu.Visible = True
            fdata.Visible = False
        ElseIf TreeView1.SelectedNode.Parent.Text = "GPRS通讯服务器" Then

            Rn = TreeView1.SelectedNode
            TXTrtuid.Text = Rn.Index.ToString
            Rn.DisRtu()
            'fall.Visible = False
            frtu.Visible = True
            fdata.Visible = False
        Else

            Dtn = TreeView1.SelectedNode
            TxtDataRTuID.Text = Dtn.Parent.Index.ToString
            txtDatablockid.Text = Dtn.Index.ToString
            Dtn.DisDataBlock()
            'fall.Visible = False
            frtu.Visible = False
            fdata.Visible = True
        End If
    End Sub

    Private Sub AddRtu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddRtu.Click

        Dim R As New ZJTXFlow
        R.Enable = True
        'R.MBAD = 201
        R.PhoneNumber = "13804126691"
        'R.PollEnable = True
        R.PollTime = 5
        R.StationName = "NewRtu"
        'R.MBadressQuantity = 100
        'R.

        Dim RtuN As stationNode

        RtuN = New stationNode(TreeView1.Nodes(0), "站点", R)

    End Sub

    Private Sub Command1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Command1.Click
        Dim R As New TXDataBlock
        R.Addr = 2
        R.BlockName = "NewDataBlock"
        R.Enable = True
        'R.Length = 10
        'R.startAD = "400001"

        Dim N As TXDtNode
        'On Error GoTo errHandle
        If TreeView1.SelectedNode.Parent.Name = "r" Then

            N = New TXDtNode(TreeView1.SelectedNode, "天信流量仪表", R)

        End If
    End Sub

    Private Sub Command4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Command4.Click
        Me.Close()
    End Sub

    Private Sub Command5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Command5.Click
        If SaveNewRtuconfigtoDB = True Then
            'SaveDatatoDB()
            'InitRtu()
            'ReaddataFromDB()
            'InitMBserver()
            'InitTX()
            'My.Forms.frmOnlineDTU.InitGprsRTUs()
            MsgBox("新配置已生效！", MsgBoxStyle.OkOnly)
            'FGetherRtuData.StartPoll()
            Me.Close()
        End If
    End Sub
    Function SaveNewRtuconfigtoDB() As Boolean

        Dim i As Integer




        Dim nodx As TreeNode
        Dim NodR As stationNode
        'Dim NodD As MeterDtNode
        Dim Xe As XElement = XElement.Load("Config.xml")
        Xe.RemoveAll()
        For i = 0 To TreeView1.Nodes(0).Nodes.Count - 1
            nodx = TreeView1.Nodes(0).Nodes(i)
            NodR = DirectCast(nodx, stationNode)
            Dim R As stationNode
            R = NodR


            Dim Xe1 As XElement = New XElement("TXFlowMeter")
            Xe1.SetAttributeValue("StationName", R.StationName)
            Xe1.SetAttributeValue("PhoneNumber", R.PhoneNumber)
            Xe1.SetAttributeValue("PollTime", R.PollTime)
            Xe1.SetAttributeValue("Enable", R.Enable)
            If R.Nodes.Count > 0 Then
                Dim Mt As TXDtNode
                For Each Mt In R.Nodes
                    Dim Xe2 As XElement = New XElement("DTBlock")
                    Xe2.SetAttributeValue("BlockName", Mt.BlockName)
                    Xe2.SetAttributeValue("MeterAddr", Mt.MeterAddr)
                    Xe2.SetAttributeValue("Enable", Mt.Enable)
                    Xe1.Add(Xe2)

                Next
            End If
            Xe.Add(Xe1)
        Next i


        Xe.Save("Config.xml")

        SaveNewRtuconfigtoDB = True
        Exit Function
        'errhand:
        '        MsgBox("输入有错误，请仔细检查！", MsgBoxStyle.OKOnly)
        '        SaveNewRtuconfigtoDB = False

    End Function

    Private Sub DeleteObj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteObj.Click
        On Error Resume Next
        If TreeView1.SelectedNode Is TreeView1.Nodes(0) Then
            Exit Sub
        End If



        TreeView1.SelectedNode.Remove()
        TreeView1.Refresh()
    End Sub

    Private Sub TreeView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeView1.DoubleClick
        TreeView1.LabelEdit = True
        TreeView1.SelectedNode.BeginEdit()
    End Sub
End Class

Class stationNode
    Inherits System.Windows.Forms.TreeNode
    Public GPRSStn As ZJTXFlow
    Public WithEvents txtPhoneNumber As TextBox
    Public WithEvents cboPollTime As ComboBox
    'Public WithEvents txtPollTime As TextBox
    'Public WithEvents txtCodeName As TextBox
    Public WithEvents txtSvrDevID As TextBox
    Public WithEvents txtSvrBaseStartAD As TextBox
    Public WithEvents txtSvrAddrNum As TextBox
    Function GetDistinctNodeText(ByVal T As String) As String
        'Dim T As String
        Dim i As Integer
        Dim T1 As String
        T1 = T
        Dim T2 As String
        T2 = T1

        'T = "NewRtu"
        Do Until Not Me.Parent.Nodes.ContainsKey(T2)

            i += 1
            T2 = T1 & CStr(i)


        Loop
        GetDistinctNodeText = T2

    End Function
    Sub DisRtu()

        Me.txtPhoneNumber.BringToFront()

        'Me.txtCodeName.BringToFront()
        ' txtCodeName.Text = (Me.Index + 1).ToString
        Me.txtSvrDevID.BringToFront()
        Me.txtSvrBaseStartAD.BringToFront()
        Me.txtSvrAddrNum.BringToFront()
        Me.cboPollTime.BringToFront()
    End Sub
    Sub New(ByVal P As TreeNode, ByVal NodText As String, ByVal TRtu As ZJTXFlow)
        MyBase.New()

        P.Nodes.Add(Me)
        Me.Checked = True
        Me.EnsureVisible()
        Dim K As String
        K = Me.GetDistinctNodeText(NodText)
        MyBase.Text = K
        MyBase.Name = K
        Me.GPRSStn = TRtu
        'TRtu.MBStartAD = Me.Index * 50 + 1
        Me.txtPhoneNumber = New TextBox
        txtPhoneNumber.Text = GPRSStn.PhoneNumber
        CopyCtrl(fReadMtrCfg._Txtphonenumber_0, Me.txtPhoneNumber)

        Me.cboPollTime = New ComboBox
        cboPollTime.Text = GPRSStn.PollTime
        CopyCtrl(fReadMtrCfg._cboPolltime_0, Me.cboPollTime)

        'Me.txtCodeName = New TextBox
        'txtCodeName.Text = GPRSStn.CodeName
        'CopyCtrl(Fconfig._txtCodeName_0, Me.txtCodeName)
        Me.txtSvrDevID = New TextBox
        txtSvrDevID.Text = ZJTXFlow.MBAD

        CopyCtrl(fReadMtrCfg._txtDeviceAD_0, Me.txtSvrDevID)
        Me.txtSvrBaseStartAD = New TextBox
        Me.txtSvrBaseStartAD.Text = GPRSStn.MBStartAD
        CopyCtrl(fReadMtrCfg._txtBaseAd_0, Me.txtSvrBaseStartAD)
        Me.txtSvrAddrNum = New TextBox
        txtSvrAddrNum.Text = ZJTXFlow.MBAddrQty.ToString
        CopyCtrl(fReadMtrCfg._txtMbadqty_0, Me.txtSvrAddrNum)





    End Sub
    Sub CopyCtrl(ByVal obj1 As TextBox, ByVal obj2 As TextBox)

        fReadMtrCfg.frtu.Controls.Add(obj2)
        'AddHandler obj1.Validated, AddressOf 
        'obj2=New 
        obj2.Left = obj1.Left
        obj2.Top = obj1.Top
        obj2.Width = obj1.Width
        'obj2.Parent = obj1.Parent
    End Sub
    Sub CopyCtrl(ByVal obj1 As ComboBox, ByVal obj2 As ComboBox)

        fReadMtrCfg.frtu.Controls.Add(obj2)
        obj2.Left = obj1.Left
        obj2.Top = obj1.Top
        obj2.Width = obj1.Width
        'obj2.Parent = obj1.Parent
    End Sub

    Private Sub txtSvrBaseStartAD_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSvrBaseStartAD.Validated

    End Sub

    Private Sub txtSvrBaseStartAD_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtSvrBaseStartAD.Validating
        Dim Cancel As Boolean = e.Cancel
        'Dim Index As Short = TxtAD.GetIndex(eventSender)
        If Not Isnumber(txtSvrBaseStartAD.Text) Then
            MsgBox("请输入数字", MsgBoxStyle.OkOnly)
            Cancel = True
        End If

        e.Cancel = Cancel
    End Sub

    Private Sub txtPhoneNumber_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtPhoneNumber.Validating
        Dim Cancel As Boolean = e.Cancel
        'Dim Index As Short = txtPhoneNumber.GetIndex(eventSender)
        If Not Isnumber(txtPhoneNumber.Text) Then
            MsgBox("请输入数字", MsgBoxStyle.OkOnly)
            Cancel = True
        End If
        If Len(txtPhoneNumber.Text) <> 11 Then
            MsgBox("请输入11位数字手机号", MsgBoxStyle.OkOnly)
            Cancel = True
        End If
        e.Cancel = Cancel
    End Sub

    Private Sub txtSvrDevID_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtSvrDevID.Validating
        Dim Cancel As Boolean = e.Cancel
        'Dim Index As Short = txtDeviceAD.GetIndex(eventSender)
        'If Not IsNumeric(txtDeviceAD(Index).Text) Or Val(txtDeviceAD.Text) > 256 Then
        'MsgBox "请输入256以内数字"
        'Cancel = True
        'End If
        e.Cancel = Cancel
    End Sub

    Public Property StationName() As String
        Get
            StationName = Me.Text

        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Public Property PhoneNumber() As String
        Get
            PhoneNumber = txtPhoneNumber.Text
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Public Property PollTime() As Integer
        Get
            PollTime = CInt(Me.cboPollTime.Text)
        End Get
        Set(ByVal value As Integer)

        End Set
    End Property

    Public Property Enable() As Boolean
        Get
            Enable = Me.Checked
        End Get
        Set(ByVal value As Boolean)

        End Set
    End Property
End Class
