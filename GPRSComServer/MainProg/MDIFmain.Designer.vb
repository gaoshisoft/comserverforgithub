Namespace MainProg
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class MdIfmain
#Region "Windows 窗体设计器生成的代码 "
        <System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
            MyBase.New()
            '此调用是 Windows 窗体设计器所必需的。
            InitializeComponent()
        End Sub
        'Form 重写 Dispose，以清理组件列表。
        <System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
            If Disposing Then
                If Not components Is Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(Disposing)
        End Sub
        'Windows 窗体设计器所必需的
        Private components As System.ComponentModel.IContainer
        Public ToolTip1 As System.Windows.Forms.ToolTip
        Public WithEvents m_atnow As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents m_OnlineDTU As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents m_ConnectRecord As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents m_comquality As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents m_ztck As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents m_mbserver As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents m_modbus As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents m_datapoll As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents m_stationset As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents m_opcCfg As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents m_dbsave As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents m_setup As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents mnufreecom As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents m_ComControl As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents m_comdetail As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents m_rearrange As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents m_window As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents mnuReg As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents m_exincompany As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents m_about As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents m_q As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents m_register As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents m_unregister As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents m_other As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents MainMenu1 As System.Windows.Forms.MenuStrip
        Public WithEvents _Sb1_Panel1 As System.Windows.Forms.ToolStripStatusLabel
        Public WithEvents _Sb1_Panel2 As System.Windows.Forms.ToolStripStatusLabel
        Public WithEvents _Sb1_Panel3 As System.Windows.Forms.ToolStripStatusLabel
        Public WithEvents _Sb1_Panel4 As System.Windows.Forms.ToolStripStatusLabel
        Public WithEvents _Sb1_Panel5 As System.Windows.Forms.ToolStripStatusLabel
        Public WithEvents Sb1 As System.Windows.Forms.StatusStrip
        '注意: 以下过程是 Windows 窗体设计器所必需的
        '可以使用 Windows 窗体设计器来修改它。
        '不要使用代码编辑器修改它。
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MdIfmain))
            Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("设定")
            Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("RTU信息", New System.Windows.Forms.TreeNode() {TreeNode1})
            Dim TreeNode3 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("实时数据条目")
            Dim TreeNode4 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("OPCserver设定")
            Dim TreeNode5 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("参数条目信息", New System.Windows.Forms.TreeNode() {TreeNode3, TreeNode4})
            Dim TreeNode6 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("数据库信息")
            Dim TreeNode7 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("modbus server")
            Dim TreeNode8 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("通信实时查看")
            Dim TreeNode9 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("通信记录")
            Dim TreeNode10 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("登录")
            Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.MainMenu1 = New System.Windows.Forms.MenuStrip()
            Me.m_ztck = New System.Windows.Forms.ToolStripMenuItem()
            Me.m_atnow = New System.Windows.Forms.ToolStripMenuItem()
            Me.m_OnlineDTU = New System.Windows.Forms.ToolStripMenuItem()
            Me.m_ConnectRecord = New System.Windows.Forms.ToolStripMenuItem()
            Me.m_comquality = New System.Windows.Forms.ToolStripMenuItem()
            Me.m_modbus = New System.Windows.Forms.ToolStripMenuItem()
            Me.m_mbserver = New System.Windows.Forms.ToolStripMenuItem()
            Me.m_setup = New System.Windows.Forms.ToolStripMenuItem()
            Me.m_datapoll = New System.Windows.Forms.ToolStripMenuItem()
            Me.m_stationset = New System.Windows.Forms.ToolStripMenuItem()
            Me.m_opcCfg = New System.Windows.Forms.ToolStripMenuItem()
            Me.m_dbsave = New System.Windows.Forms.ToolStripMenuItem()
            Me.m_comdetail = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnufreecom = New System.Windows.Forms.ToolStripMenuItem()
            Me.m_ComControl = New System.Windows.Forms.ToolStripMenuItem()
            Me.MBTCP通信监控ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.m_window = New System.Windows.Forms.ToolStripMenuItem()
            Me.m_rearrange = New System.Windows.Forms.ToolStripMenuItem()
            Me.隐藏ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.m_other = New System.Windows.Forms.ToolStripMenuItem()
            Me.m_q = New System.Windows.Forms.ToolStripMenuItem()
            Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
            Me.m_register = New System.Windows.Forms.ToolStripMenuItem()
            Me.m_unregister = New System.Windows.Forms.ToolStripMenuItem()
            Me.m_about = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuReg = New System.Windows.Forms.ToolStripMenuItem()
            Me.m_exincompany = New System.Windows.Forms.ToolStripMenuItem()
            Me.Sb1 = New System.Windows.Forms.StatusStrip()
            Me._Sb1_Panel1 = New System.Windows.Forms.ToolStripStatusLabel()
            Me._Sb1_Panel2 = New System.Windows.Forms.ToolStripStatusLabel()
            Me._Sb1_Panel3 = New System.Windows.Forms.ToolStripStatusLabel()
            Me._Sb1_Panel4 = New System.Windows.Forms.ToolStripStatusLabel()
            Me._Sb1_Panel5 = New System.Windows.Forms.ToolStripStatusLabel()
            Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
            Me.TreeView1 = New System.Windows.Forms.TreeView()
            Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
            Me.MainMenu1.SuspendLayout()
            Me.Sb1.SuspendLayout()
            Me.Panel1.SuspendLayout()
            CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SplitContainer1.Panel1.SuspendLayout()
            Me.SplitContainer1.Panel2.SuspendLayout()
            Me.SplitContainer1.SuspendLayout()
            CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SplitContainer2.SuspendLayout()
            Me.SuspendLayout()
            '
            'MainMenu1
            '
            Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.m_ztck, Me.m_modbus, Me.m_setup, Me.m_comdetail, Me.m_window, Me.m_other, Me.m_about})
            Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
            Me.MainMenu1.Name = "MainMenu1"
            Me.MainMenu1.Size = New System.Drawing.Size(961, 25)
            Me.MainMenu1.TabIndex = 2
            '
            'm_ztck
            '
            Me.m_ztck.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.m_atnow, Me.m_OnlineDTU, Me.m_ConnectRecord, Me.m_comquality})
            Me.m_ztck.MergeAction = System.Windows.Forms.MergeAction.Remove
            Me.m_ztck.Name = "m_ztck"
            Me.m_ztck.Size = New System.Drawing.Size(85, 21)
            Me.m_ztck.Text = "数据中心(&D)"
            '
            'm_atnow
            '
            Me.m_atnow.Name = "m_atnow"
            Me.m_atnow.Size = New System.Drawing.Size(144, 22)
            Me.m_atnow.Text = "单站巡测"
            '
            'm_OnlineDTU
            '
            Me.m_OnlineDTU.Name = "m_OnlineDTU"
            Me.m_OnlineDTU.Size = New System.Drawing.Size(144, 22)
            Me.m_OnlineDTU.Text = "在线DTU(&O)"
            '
            'm_ConnectRecord
            '
            Me.m_ConnectRecord.Name = "m_ConnectRecord"
            Me.m_ConnectRecord.Size = New System.Drawing.Size(144, 22)
            Me.m_ConnectRecord.Text = "连接记录(&M)"
            '
            'm_comquality
            '
            Me.m_comquality.Name = "m_comquality"
            Me.m_comquality.Size = New System.Drawing.Size(144, 22)
            Me.m_comquality.Text = "通讯质量(&S)"
            '
            'm_modbus
            '
            Me.m_modbus.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.m_mbserver})
            Me.m_modbus.MergeAction = System.Windows.Forms.MergeAction.Remove
            Me.m_modbus.Name = "m_modbus"
            Me.m_modbus.Size = New System.Drawing.Size(141, 21)
            Me.m_modbus.Text = "Modbus 服务中心(&M)"
            '
            'm_mbserver
            '
            Me.m_mbserver.Name = "m_mbserver"
            Me.m_mbserver.Size = New System.Drawing.Size(165, 22)
            Me.m_mbserver.Text = "Modbus server"
            '
            'm_setup
            '
            Me.m_setup.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.m_datapoll, Me.m_stationset, Me.m_opcCfg, Me.m_dbsave})
            Me.m_setup.MergeAction = System.Windows.Forms.MergeAction.Remove
            Me.m_setup.Name = "m_setup"
            Me.m_setup.Size = New System.Drawing.Size(59, 21)
            Me.m_setup.Text = "设置(&S)"
            '
            'm_datapoll
            '
            Me.m_datapoll.Checked = True
            Me.m_datapoll.CheckState = System.Windows.Forms.CheckState.Checked
            Me.m_datapoll.Name = "m_datapoll"
            Me.m_datapoll.Size = New System.Drawing.Size(223, 22)
            Me.m_datapoll.Text = "启动数据采集轮询"
            '
            'm_stationset
            '
            Me.m_stationset.Name = "m_stationset"
            Me.m_stationset.Size = New System.Drawing.Size(223, 22)
            Me.m_stationset.Text = "GPRS/MBEserver站点配置"
            '
            'm_opcCfg
            '
            Me.m_opcCfg.Name = "m_opcCfg"
            Me.m_opcCfg.Size = New System.Drawing.Size(223, 22)
            Me.m_opcCfg.Text = "OPC server 参数配置"
            '
            'm_dbsave
            '
            Me.m_dbsave.Name = "m_dbsave"
            Me.m_dbsave.Size = New System.Drawing.Size(223, 22)
            Me.m_dbsave.Text = "数据存库配置"
            '
            'm_comdetail
            '
            Me.m_comdetail.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnufreecom, Me.m_ComControl, Me.MBTCP通信监控ToolStripMenuItem})
            Me.m_comdetail.MergeAction = System.Windows.Forms.MergeAction.Remove
            Me.m_comdetail.Name = "m_comdetail"
            Me.m_comdetail.Size = New System.Drawing.Size(82, 21)
            Me.m_comdetail.Text = "通讯监控(&L)"
            '
            'mnufreecom
            '
            Me.mnufreecom.Name = "mnufreecom"
            Me.mnufreecom.Size = New System.Drawing.Size(166, 22)
            Me.mnufreecom.Text = "自定义通讯"
            '
            'm_ComControl
            '
            Me.m_ComControl.Name = "m_ComControl"
            Me.m_ComControl.Size = New System.Drawing.Size(166, 22)
            Me.m_ComControl.Text = "通讯监控"
            '
            'MBTCP通信监控ToolStripMenuItem
            '
            Me.MBTCP通信监控ToolStripMenuItem.Name = "MBTCP通信监控ToolStripMenuItem"
            Me.MBTCP通信监控ToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
            Me.MBTCP通信监控ToolStripMenuItem.Text = "MBTCP通信监控"
            '
            'm_window
            '
            Me.m_window.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.m_rearrange, Me.隐藏ToolStripMenuItem})
            Me.m_window.MergeAction = System.Windows.Forms.MergeAction.Remove
            Me.m_window.Name = "m_window"
            Me.m_window.Size = New System.Drawing.Size(64, 21)
            Me.m_window.Text = "窗体(&W)"
            '
            'm_rearrange
            '
            Me.m_rearrange.Name = "m_rearrange"
            Me.m_rearrange.Size = New System.Drawing.Size(116, 22)
            Me.m_rearrange.Text = "重排(&R)"
            '
            '隐藏ToolStripMenuItem
            '
            Me.隐藏ToolStripMenuItem.Name = "隐藏ToolStripMenuItem"
            Me.隐藏ToolStripMenuItem.Size = New System.Drawing.Size(116, 22)
            Me.隐藏ToolStripMenuItem.Text = "隐藏"
            '
            'm_other
            '
            Me.m_other.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.m_q, Me.ToolStripMenuItem1, Me.m_register, Me.m_unregister})
            Me.m_other.MergeAction = System.Windows.Forms.MergeAction.Remove
            Me.m_other.Name = "m_other"
            Me.m_other.Size = New System.Drawing.Size(62, 21)
            Me.m_other.Text = "其它(&O)"
            '
            'm_q
            '
            Me.m_q.Name = "m_q"
            Me.m_q.Size = New System.Drawing.Size(152, 22)
            Me.m_q.Text = "退出"
            '
            'ToolStripMenuItem1
            '
            Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
            Me.ToolStripMenuItem1.Size = New System.Drawing.Size(152, 22)
            Me.ToolStripMenuItem1.Text = "重新启动"
            '
            'm_register
            '
            Me.m_register.Name = "m_register"
            Me.m_register.Size = New System.Drawing.Size(152, 22)
            Me.m_register.Text = "登录"
            '
            'm_unregister
            '
            Me.m_unregister.Name = "m_unregister"
            Me.m_unregister.Size = New System.Drawing.Size(152, 22)
            Me.m_unregister.Text = "退出登录"
            '
            'm_about
            '
            Me.m_about.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuReg, Me.m_exincompany})
            Me.m_about.MergeAction = System.Windows.Forms.MergeAction.Remove
            Me.m_about.Name = "m_about"
            Me.m_about.Size = New System.Drawing.Size(60, 21)
            Me.m_about.Text = "关于(&A)"
            Me.m_about.Visible = False
            '
            'mnuReg
            '
            Me.mnuReg.Name = "mnuReg"
            Me.mnuReg.Size = New System.Drawing.Size(136, 22)
            Me.mnuReg.Text = "注册"
            Me.mnuReg.Visible = False
            '
            'm_exincompany
            '
            Me.m_exincompany.Name = "m_exincompany"
            Me.m_exincompany.Size = New System.Drawing.Size(136, 22)
            Me.m_exincompany.Text = "开发商介绍"
            Me.m_exincompany.Visible = False
            '
            'Sb1
            '
            Me.Sb1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me._Sb1_Panel1, Me._Sb1_Panel2, Me._Sb1_Panel3, Me._Sb1_Panel4, Me._Sb1_Panel5})
            Me.Sb1.Location = New System.Drawing.Point(0, 724)
            Me.Sb1.Name = "Sb1"
            Me.Sb1.Size = New System.Drawing.Size(961, 22)
            Me.Sb1.TabIndex = 0
            '
            '_Sb1_Panel1
            '
            Me._Sb1_Panel1.AutoSize = False
            Me._Sb1_Panel1.BackColor = System.Drawing.SystemColors.ButtonFace
            Me._Sb1_Panel1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                        Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                        Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
            Me._Sb1_Panel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
            Me._Sb1_Panel1.Margin = New System.Windows.Forms.Padding(0)
            Me._Sb1_Panel1.Name = "_Sb1_Panel1"
            Me._Sb1_Panel1.Size = New System.Drawing.Size(186, 22)
            Me._Sb1_Panel1.Spring = True
            Me._Sb1_Panel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            '_Sb1_Panel2
            '
            Me._Sb1_Panel2.AutoSize = False
            Me._Sb1_Panel2.BackColor = System.Drawing.SystemColors.ButtonFace
            Me._Sb1_Panel2.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                        Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                        Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
            Me._Sb1_Panel2.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
            Me._Sb1_Panel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
            Me._Sb1_Panel2.Margin = New System.Windows.Forms.Padding(0)
            Me._Sb1_Panel2.Name = "_Sb1_Panel2"
            Me._Sb1_Panel2.Size = New System.Drawing.Size(186, 22)
            Me._Sb1_Panel2.Spring = True
            Me._Sb1_Panel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            '_Sb1_Panel3
            '
            Me._Sb1_Panel3.AutoSize = False
            Me._Sb1_Panel3.BackColor = System.Drawing.SystemColors.ButtonFace
            Me._Sb1_Panel3.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                        Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                        Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
            Me._Sb1_Panel3.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
            Me._Sb1_Panel3.Margin = New System.Windows.Forms.Padding(0)
            Me._Sb1_Panel3.Name = "_Sb1_Panel3"
            Me._Sb1_Panel3.Size = New System.Drawing.Size(186, 22)
            Me._Sb1_Panel3.Spring = True
            Me._Sb1_Panel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            '_Sb1_Panel4
            '
            Me._Sb1_Panel4.AutoSize = False
            Me._Sb1_Panel4.BackColor = System.Drawing.SystemColors.ButtonFace
            Me._Sb1_Panel4.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                        Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                        Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
            Me._Sb1_Panel4.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
            Me._Sb1_Panel4.Margin = New System.Windows.Forms.Padding(0)
            Me._Sb1_Panel4.Name = "_Sb1_Panel4"
            Me._Sb1_Panel4.Size = New System.Drawing.Size(201, 22)
            Me._Sb1_Panel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            '_Sb1_Panel5
            '
            Me._Sb1_Panel5.AutoSize = False
            Me._Sb1_Panel5.BackColor = System.Drawing.SystemColors.ButtonFace
            Me._Sb1_Panel5.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                        Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                        Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
            Me._Sb1_Panel5.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
            Me._Sb1_Panel5.Margin = New System.Windows.Forms.Padding(0)
            Me._Sb1_Panel5.Name = "_Sb1_Panel5"
            Me._Sb1_Panel5.Size = New System.Drawing.Size(186, 22)
            Me._Sb1_Panel5.Spring = True
            Me._Sb1_Panel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'NotifyIcon1
            '
            Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
            Me.NotifyIcon1.Text = "GPRSComServer"
            Me.NotifyIcon1.Visible = True
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.SplitContainer1)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Panel1.Location = New System.Drawing.Point(0, 25)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(961, 699)
            Me.Panel1.TabIndex = 4
            '
            'SplitContainer1
            '
            Me.SplitContainer1.BackColor = System.Drawing.SystemColors.ButtonFace
            Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
            Me.SplitContainer1.Name = "SplitContainer1"
            '
            'SplitContainer1.Panel1
            '
            Me.SplitContainer1.Panel1.Controls.Add(Me.TreeView1)
            '
            'SplitContainer1.Panel2
            '
            Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
            Me.SplitContainer1.Size = New System.Drawing.Size(961, 699)
            Me.SplitContainer1.SplitterDistance = 181
            Me.SplitContainer1.TabIndex = 0
            '
            'TreeView1
            '
            Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.TreeView1.HideSelection = False
            Me.TreeView1.Location = New System.Drawing.Point(0, 0)
            Me.TreeView1.Name = "TreeView1"
            TreeNode1.Name = "RtuSetup"
            TreeNode1.Text = "设定"
            TreeNode2.Name = "节点0"
            TreeNode2.Text = "RTU信息"
            TreeNode3.Name = "opcItemdis"
            TreeNode3.Text = "实时数据条目"
            TreeNode4.Name = "opcserversetup"
            TreeNode4.Text = "OPCserver设定"
            TreeNode5.Name = "节点4"
            TreeNode5.Text = "参数条目信息"
            TreeNode6.Name = "DBfuncnode"
            TreeNode6.Text = "数据库信息"
            TreeNode7.Name = "modbusservernode"
            TreeNode7.Text = "modbus server"
            TreeNode8.Name = "cominspect"
            TreeNode8.Text = "通信实时查看"
            TreeNode9.Name = "commrecord"
            TreeNode9.Text = "通信记录"
            TreeNode10.Name = "longinnode"
            TreeNode10.Text = "登录"
            Me.TreeView1.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode2, TreeNode5, TreeNode6, TreeNode7, TreeNode8, TreeNode9, TreeNode10})
            Me.TreeView1.Size = New System.Drawing.Size(181, 699)
            Me.TreeView1.TabIndex = 0
            '
            'SplitContainer2
            '
            Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
            Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
            Me.SplitContainer2.Name = "SplitContainer2"
            Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'SplitContainer2.Panel1
            '
            Me.SplitContainer2.Panel1.BackColor = System.Drawing.SystemColors.ButtonFace
            Me.SplitContainer2.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No
            '
            'SplitContainer2.Panel2
            '
            Me.SplitContainer2.Panel2.BackColor = System.Drawing.SystemColors.ButtonFace
            Me.SplitContainer2.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.SplitContainer2.Size = New System.Drawing.Size(776, 699)
            Me.SplitContainer2.SplitterDistance = 471
            Me.SplitContainer2.TabIndex = 0
            '
            'MdIfmain
            '
            Me.BackColor = System.Drawing.SystemColors.AppWorkspace
            Me.ClientSize = New System.Drawing.Size(961, 746)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.Sb1)
            Me.Controls.Add(Me.MainMenu1)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.IsMdiContainer = True
            Me.Location = New System.Drawing.Point(11, 30)
            Me.Name = "MdIfmain"
            Me.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "数据通讯服务器"
            Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
            Me.MainMenu1.ResumeLayout(False)
            Me.MainMenu1.PerformLayout()
            Me.Sb1.ResumeLayout(False)
            Me.Sb1.PerformLayout()
            Me.Panel1.ResumeLayout(False)
            Me.SplitContainer1.Panel1.ResumeLayout(False)
            Me.SplitContainer1.Panel2.ResumeLayout(False)
            CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.SplitContainer1.ResumeLayout(False)
            CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
            Me.SplitContainer2.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
        Friend WithEvents MBTCP通信监控ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
        Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
        Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents 隐藏ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
#End Region
    End Class
End Namespace