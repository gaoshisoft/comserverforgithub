<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmOpc
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
    Public WithEvents lvwsImageList As System.Windows.Forms.ImageList
    Public WithEvents lvwbImageList As System.Windows.Forms.ImageList
    Public WithEvents _sbStatusBar_Panel1 As System.Windows.Forms.ToolStripStatusLabel
    Public WithEvents _sbStatusBar_Panel2 As System.Windows.Forms.ToolStripStatusLabel
    Public WithEvents _sbStatusBar_Panel3 As System.Windows.Forms.ToolStripStatusLabel
    Public WithEvents sbStatusBar As System.Windows.Forms.StatusStrip
    Public dlgCommonDialogOpen As System.Windows.Forms.OpenFileDialog
    Public dlgCommonDialogSave As System.Windows.Forms.SaveFileDialog
    Public dlgCommonDialogFont As System.Windows.Forms.FontDialog
    Public dlgCommonDialogColor As System.Windows.Forms.ColorDialog
    Public dlgCommonDialogPrint As System.Windows.Forms.PrintDialog
    Public WithEvents imlToolbarIcons As System.Windows.Forms.ImageList
    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器来修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOpc))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.lvwsImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.lvwbImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.sbStatusBar = New System.Windows.Forms.StatusStrip()
        Me._sbStatusBar_Panel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me._sbStatusBar_Panel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me._sbStatusBar_Panel3 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.dlgCommonDialogOpen = New System.Windows.Forms.OpenFileDialog()
        Me.dlgCommonDialogSave = New System.Windows.Forms.SaveFileDialog()
        Me.dlgCommonDialogFont = New System.Windows.Forms.FontDialog()
        Me.dlgCommonDialogColor = New System.Windows.Forms.ColorDialog()
        Me.dlgCommonDialogPrint = New System.Windows.Forms.PrintDialog()
        Me.imlToolbarIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRegister = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUnRegister = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnusep2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuAddItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRemoveItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnusep = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuShutdownClients = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSep0 = New System.Windows.Forms.ToolStripSeparator()
        Me.m_save = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFileClose = New System.Windows.Forms.ToolStripMenuItem()
        Me.MainMenu1 = New System.Windows.Forms.MenuStrip()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.Frame1 = New System.Windows.Forms.Panel()
        Me.Command1 = New System.Windows.Forms.Button()
        Me.Command2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Command4 = New System.Windows.Forms.Button()
        Me.Command5 = New System.Windows.Forms.Button()
        Me.txtdisplaychoose = New System.Windows.Forms.TextBox()
        Me.lvListView = New GPRSComServer.DoubleBufferListView(Me.components)
        Me.sbStatusBar.SuspendLayout()
        Me.MainMenu1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lvwsImageList
        '
        Me.lvwsImageList.ImageStream = CType(resources.GetObject("lvwsImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.lvwsImageList.TransparentColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lvwsImageList.Images.SetKeyName(0, "")
        '
        'lvwbImageList
        '
        Me.lvwbImageList.ImageStream = CType(resources.GetObject("lvwbImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.lvwbImageList.TransparentColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lvwbImageList.Images.SetKeyName(0, "")
        '
        'sbStatusBar
        '
        Me.sbStatusBar.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.sbStatusBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me._sbStatusBar_Panel1, Me._sbStatusBar_Panel2, Me._sbStatusBar_Panel3})
        Me.sbStatusBar.Location = New System.Drawing.Point(0, 726)
        Me.sbStatusBar.Name = "sbStatusBar"
        Me.sbStatusBar.Size = New System.Drawing.Size(1018, 22)
        Me.sbStatusBar.TabIndex = 0
        '
        '_sbStatusBar_Panel1
        '
        Me._sbStatusBar_Panel1.AutoSize = False
        Me._sbStatusBar_Panel1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me._sbStatusBar_Panel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me._sbStatusBar_Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me._sbStatusBar_Panel1.Name = "_sbStatusBar_Panel1"
        Me._sbStatusBar_Panel1.Size = New System.Drawing.Size(907, 22)
        Me._sbStatusBar_Panel1.Spring = True
        Me._sbStatusBar_Panel1.Text = "Status"
        Me._sbStatusBar_Panel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        '_sbStatusBar_Panel2
        '
        Me._sbStatusBar_Panel2.AutoSize = False
        Me._sbStatusBar_Panel2.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me._sbStatusBar_Panel2.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me._sbStatusBar_Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me._sbStatusBar_Panel2.Name = "_sbStatusBar_Panel2"
        Me._sbStatusBar_Panel2.Size = New System.Drawing.Size(96, 22)
        Me._sbStatusBar_Panel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        '_sbStatusBar_Panel3
        '
        Me._sbStatusBar_Panel3.AutoSize = False
        Me._sbStatusBar_Panel3.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me._sbStatusBar_Panel3.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me._sbStatusBar_Panel3.Margin = New System.Windows.Forms.Padding(0)
        Me._sbStatusBar_Panel3.Name = "_sbStatusBar_Panel3"
        Me._sbStatusBar_Panel3.Size = New System.Drawing.Size(96, 18)
        Me._sbStatusBar_Panel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'imlToolbarIcons
        '
        Me.imlToolbarIcons.ImageStream = CType(resources.GetObject("imlToolbarIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlToolbarIcons.TransparentColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.imlToolbarIcons.Images.SetKeyName(0, "Back")
        Me.imlToolbarIcons.Images.SetKeyName(1, "Forward")
        Me.imlToolbarIcons.Images.SetKeyName(2, "Cut")
        Me.imlToolbarIcons.Images.SetKeyName(3, "Copy")
        Me.imlToolbarIcons.Images.SetKeyName(4, "Paste")
        Me.imlToolbarIcons.Images.SetKeyName(5, "Delete")
        Me.imlToolbarIcons.Images.SetKeyName(6, "Properties")
        Me.imlToolbarIcons.Images.SetKeyName(7, "View Large Icons")
        Me.imlToolbarIcons.Images.SetKeyName(8, "View Small Icons")
        Me.imlToolbarIcons.Images.SetKeyName(9, "View List")
        Me.imlToolbarIcons.Images.SetKeyName(10, "View Details")
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuRegister, Me.mnuUnRegister, Me.mnusep2, Me.mnuAddItem, Me.mnuRemoveItem, Me.mnusep, Me.mnuShutdownClients, Me.mnuSep0, Me.m_save, Me.mnuFileClose})
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(45, 21)
        Me.mnuFile.Text = "&OPC"
        Me.mnuFile.Visible = False
        '
        'mnuRegister
        '
        Me.mnuRegister.Name = "mnuRegister"
        Me.mnuRegister.Size = New System.Drawing.Size(175, 22)
        Me.mnuRegister.Text = "&Register"
        Me.mnuRegister.Visible = False
        '
        'mnuUnRegister
        '
        Me.mnuUnRegister.Name = "mnuUnRegister"
        Me.mnuUnRegister.Size = New System.Drawing.Size(175, 22)
        Me.mnuUnRegister.Text = "&UnRegister"
        Me.mnuUnRegister.Visible = False
        '
        'mnusep2
        '
        Me.mnusep2.Name = "mnusep2"
        Me.mnusep2.Size = New System.Drawing.Size(172, 6)
        '
        'mnuAddItem
        '
        Me.mnuAddItem.Name = "mnuAddItem"
        Me.mnuAddItem.Size = New System.Drawing.Size(175, 22)
        Me.mnuAddItem.Text = "&Add Item"
        '
        'mnuRemoveItem
        '
        Me.mnuRemoveItem.Name = "mnuRemoveItem"
        Me.mnuRemoveItem.Size = New System.Drawing.Size(175, 22)
        Me.mnuRemoveItem.Text = "Re&move Item"
        '
        'mnusep
        '
        Me.mnusep.Name = "mnusep"
        Me.mnusep.Size = New System.Drawing.Size(172, 6)
        '
        'mnuShutdownClients
        '
        Me.mnuShutdownClients.Name = "mnuShutdownClients"
        Me.mnuShutdownClients.Size = New System.Drawing.Size(175, 22)
        Me.mnuShutdownClients.Text = "&Shutdown Clients"
        Me.mnuShutdownClients.Visible = False
        '
        'mnuSep0
        '
        Me.mnuSep0.Name = "mnuSep0"
        Me.mnuSep0.Size = New System.Drawing.Size(172, 6)
        '
        'm_save
        '
        Me.m_save.Name = "m_save"
        Me.m_save.Size = New System.Drawing.Size(175, 22)
        Me.m_save.Text = "SaveConfig"
        '
        'mnuFileClose
        '
        Me.mnuFileClose.Name = "mnuFileClose"
        Me.mnuFileClose.Size = New System.Drawing.Size(175, 22)
        Me.mnuFileClose.Text = "&Hide"
        '
        'MainMenu1
        '
        Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile})
        Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu1.Name = "MainMenu1"
        Me.MainMenu1.Size = New System.Drawing.Size(1018, 25)
        Me.MainMenu1.TabIndex = 4
        Me.MainMenu1.Visible = False
        '
        'SplitContainer1
        '
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
        Me.SplitContainer1.Size = New System.Drawing.Size(1018, 726)
        Me.SplitContainer1.SplitterDistance = 216
        Me.SplitContainer1.TabIndex = 6
        '
        'TreeView1
        '
        Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView1.Location = New System.Drawing.Point(0, 0)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.Size = New System.Drawing.Size(216, 726)
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.Frame1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.lvListView)
        Me.SplitContainer2.Size = New System.Drawing.Size(798, 726)
        Me.SplitContainer2.SplitterDistance = 36
        Me.SplitContainer2.TabIndex = 3
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.Command1)
        Me.Frame1.Controls.Add(Me.Command2)
        Me.Frame1.Controls.Add(Me.Button1)
        Me.Frame1.Controls.Add(Me.Command4)
        Me.Frame1.Controls.Add(Me.Command5)
        Me.Frame1.Controls.Add(Me.txtdisplaychoose)
        Me.Frame1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Frame1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(798, 36)
        Me.Frame1.TabIndex = 4
        Me.Frame1.Text = "Frame1"
        '
        'Command1
        '
        Me.Command1.BackColor = System.Drawing.SystemColors.Control
        Me.Command1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command1.Location = New System.Drawing.Point(185, 5)
        Me.Command1.Name = "Command1"
        Me.Command1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command1.Size = New System.Drawing.Size(105, 25)
        Me.Command1.TabIndex = 9
        Me.Command1.Text = "增加条目"
        Me.Command1.UseVisualStyleBackColor = False
        '
        'Command2
        '
        Me.Command2.BackColor = System.Drawing.SystemColors.Control
        Me.Command2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command2.Location = New System.Drawing.Point(296, 5)
        Me.Command2.Name = "Command2"
        Me.Command2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command2.Size = New System.Drawing.Size(97, 25)
        Me.Command2.TabIndex = 8
        Me.Command2.Text = "删除条目"
        Me.Command2.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.Control
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Button1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Button1.Location = New System.Drawing.Point(494, 5)
        Me.Button1.Name = "Button1"
        Me.Button1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Button1.Size = New System.Drawing.Size(89, 25)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "退出"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Command4
        '
        Me.Command4.BackColor = System.Drawing.SystemColors.Control
        Me.Command4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command4.Location = New System.Drawing.Point(399, 5)
        Me.Command4.Name = "Command4"
        Me.Command4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command4.Size = New System.Drawing.Size(89, 25)
        Me.Command4.TabIndex = 6
        Me.Command4.Text = "保存退出"
        Me.Command4.UseVisualStyleBackColor = False
        '
        'Command5
        '
        Me.Command5.BackColor = System.Drawing.SystemColors.Control
        Me.Command5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command5.Location = New System.Drawing.Point(90, 5)
        Me.Command5.Name = "Command5"
        Me.Command5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command5.Size = New System.Drawing.Size(89, 25)
        Me.Command5.TabIndex = 5
        Me.Command5.Text = "应用显示条件"
        Me.Command5.UseVisualStyleBackColor = False
        '
        'txtdisplaychoose
        '
        Me.txtdisplaychoose.AcceptsReturn = True
        Me.txtdisplaychoose.BackColor = System.Drawing.SystemColors.Window
        Me.txtdisplaychoose.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtdisplaychoose.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtdisplaychoose.Location = New System.Drawing.Point(3, 8)
        Me.txtdisplaychoose.MaxLength = 0
        Me.txtdisplaychoose.Name = "txtdisplaychoose"
        Me.txtdisplaychoose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtdisplaychoose.Size = New System.Drawing.Size(81, 21)
        Me.txtdisplaychoose.TabIndex = 4
        Me.txtdisplaychoose.Text = "*"
        Me.txtdisplaychoose.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lvListView
        '
        Me.lvListView.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.lvListView.AllowColumnReorder = True
        Me.lvListView.BackColor = System.Drawing.SystemColors.Window
        Me.lvListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvListView.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lvListView.FullRowSelect = True
        Me.lvListView.GridLines = True
        Me.lvListView.HideSelection = False
        Me.lvListView.Location = New System.Drawing.Point(0, 0)
        Me.lvListView.Name = "lvListView"
        Me.lvListView.Size = New System.Drawing.Size(798, 686)
        Me.lvListView.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvListView.TabIndex = 3
        Me.lvListView.UseCompatibleStateImageBehavior = False
        Me.lvListView.View = System.Windows.Forms.View.Details
        '
        'frmOpc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1018, 748)
        Me.ControlBox = False
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.sbStatusBar)
        Me.Controls.Add(Me.MainMenu1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Location = New System.Drawing.Point(10, 9)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOpc"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "MODBUS to OPC server"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.sbStatusBar.ResumeLayout(False)
        Me.sbStatusBar.PerformLayout()
        Me.MainMenu1.ResumeLayout(False)
        Me.MainMenu1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents mnuFile As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuRegister As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuUnRegister As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnusep2 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents mnuAddItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuRemoveItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnusep As System.Windows.Forms.ToolStripSeparator
    Public WithEvents mnuShutdownClients As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuSep0 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents m_save As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuFileClose As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MainMenu1 As System.Windows.Forms.MenuStrip
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Public WithEvents Frame1 As System.Windows.Forms.Panel
    Public WithEvents Command1 As System.Windows.Forms.Button
    Public WithEvents Command2 As System.Windows.Forms.Button
    Public WithEvents Command4 As System.Windows.Forms.Button
    Public WithEvents Command5 As System.Windows.Forms.Button
    Public WithEvents txtdisplaychoose As System.Windows.Forms.TextBox
    Public WithEvents lvListView As GPRSComServer.DoubleBufferListView
    Public WithEvents Button1 As System.Windows.Forms.Button
#End Region
End Class