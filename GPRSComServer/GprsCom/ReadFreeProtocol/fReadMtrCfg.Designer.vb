<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fReadMtrCfg
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(fReadMtrCfg))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Frame4 = New System.Windows.Forms.GroupBox
        Me.TreeView1 = New System.Windows.Forms.TreeView
        Me.AddRtu = New System.Windows.Forms.Button
        Me.Command5 = New System.Windows.Forms.Button
        Me.Command1 = New System.Windows.Forms.Button
        Me.Command4 = New System.Windows.Forms.Button
        Me.DeleteObj = New System.Windows.Forms.Button
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.frtu = New System.Windows.Forms.GroupBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me._txtBaseAd_0 = New System.Windows.Forms.TextBox
        Me._txtMbadqty_0 = New System.Windows.Forms.TextBox
        Me._txtDeviceAD_0 = New System.Windows.Forms.TextBox
        Me._cboPolltime_0 = New System.Windows.Forms.ComboBox
        Me.TXTrtuid = New System.Windows.Forms.TextBox
        Me._Txtphonenumber_0 = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.fdata = New System.Windows.Forms.GroupBox
        Me.TxtDataRTuID = New System.Windows.Forms.TextBox
        Me._TxtAD_0 = New System.Windows.Forms.TextBox
        Me.txtDatablockid = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Frame4.SuspendLayout()
        Me.frtu.SuspendLayout()
        Me.fdata.SuspendLayout()
        Me.SuspendLayout()
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.TreeView1)
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(4, 45)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(177, 358)
        Me.Frame4.TabIndex = 30
        Me.Frame4.TabStop = False
        '
        'TreeView1
        '
        Me.TreeView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TreeView1.CheckBoxes = True
        Me.TreeView1.Location = New System.Drawing.Point(5, 6)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.Size = New System.Drawing.Size(169, 349)
        Me.TreeView1.TabIndex = 5
        '
        'AddRtu
        '
        Me.AddRtu.BackColor = System.Drawing.SystemColors.Control
        Me.AddRtu.Cursor = System.Windows.Forms.Cursors.Default
        Me.AddRtu.ForeColor = System.Drawing.SystemColors.ControlText
        Me.AddRtu.Location = New System.Drawing.Point(8, 13)
        Me.AddRtu.Name = "AddRtu"
        Me.AddRtu.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.AddRtu.Size = New System.Drawing.Size(97, 25)
        Me.AddRtu.TabIndex = 27
        Me.AddRtu.Text = "增加GPRS RTU"
        Me.AddRtu.UseVisualStyleBackColor = False
        '
        'Command5
        '
        Me.Command5.BackColor = System.Drawing.SystemColors.Control
        Me.Command5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command5.Location = New System.Drawing.Point(341, 398)
        Me.Command5.Name = "Command5"
        Me.Command5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command5.Size = New System.Drawing.Size(58, 25)
        Me.Command5.TabIndex = 32
        Me.Command5.Text = "确定"
        Me.Command5.UseVisualStyleBackColor = False
        '
        'Command1
        '
        Me.Command1.BackColor = System.Drawing.SystemColors.Control
        Me.Command1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command1.Location = New System.Drawing.Point(107, 13)
        Me.Command1.Name = "Command1"
        Me.Command1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command1.Size = New System.Drawing.Size(97, 25)
        Me.Command1.TabIndex = 35
        Me.Command1.Text = "增加仪表"
        Me.Command1.UseVisualStyleBackColor = False
        '
        'Command4
        '
        Me.Command4.BackColor = System.Drawing.SystemColors.Control
        Me.Command4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command4.Location = New System.Drawing.Point(419, 398)
        Me.Command4.Name = "Command4"
        Me.Command4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command4.Size = New System.Drawing.Size(55, 25)
        Me.Command4.TabIndex = 34
        Me.Command4.Text = "取消"
        Me.Command4.UseVisualStyleBackColor = False
        '
        'DeleteObj
        '
        Me.DeleteObj.BackColor = System.Drawing.SystemColors.Control
        Me.DeleteObj.Cursor = System.Windows.Forms.Cursors.Default
        Me.DeleteObj.ForeColor = System.Drawing.SystemColors.ControlText
        Me.DeleteObj.Location = New System.Drawing.Point(206, 13)
        Me.DeleteObj.Name = "DeleteObj"
        Me.DeleteObj.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DeleteObj.Size = New System.Drawing.Size(97, 25)
        Me.DeleteObj.TabIndex = 33
        Me.DeleteObj.Text = "删除"
        Me.DeleteObj.UseVisualStyleBackColor = False
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ImageList1.Images.SetKeyName(0, "p1")
        Me.ImageList1.Images.SetKeyName(1, "p2")
        Me.ImageList1.Images.SetKeyName(2, "p3")
        '
        'frtu
        '
        Me.frtu.BackColor = System.Drawing.SystemColors.Control
        Me.frtu.Controls.Add(Me.Label20)
        Me.frtu.Controls.Add(Me.Label14)
        Me.frtu.Controls.Add(Me.Label15)
        Me.frtu.Controls.Add(Me._txtBaseAd_0)
        Me.frtu.Controls.Add(Me._txtMbadqty_0)
        Me.frtu.Controls.Add(Me._txtDeviceAD_0)
        Me.frtu.Controls.Add(Me._cboPolltime_0)
        Me.frtu.Controls.Add(Me.TXTrtuid)
        Me.frtu.Controls.Add(Me._Txtphonenumber_0)
        Me.frtu.Controls.Add(Me.Label2)
        Me.frtu.Controls.Add(Me.Label18)
        Me.frtu.Controls.Add(Me.Label10)
        Me.frtu.Controls.Add(Me.Label3)
        Me.frtu.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frtu.Location = New System.Drawing.Point(187, 62)
        Me.frtu.Name = "frtu"
        Me.frtu.Padding = New System.Windows.Forms.Padding(0)
        Me.frtu.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frtu.Size = New System.Drawing.Size(329, 329)
        Me.frtu.TabIndex = 29
        Me.frtu.TabStop = False
        Me.frtu.Text = "RTU配置"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(47, 159)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(70, 17)
        Me.Label20.TabIndex = 50
        Me.Label20.Text = "基地址："
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(7, 186)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(107, 16)
        Me.Label14.TabIndex = 49
        Me.Label14.Text = "Modbus 地址数量："
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(49, 133)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(67, 19)
        Me.Label15.TabIndex = 48
        Me.Label15.Text = "设备地址："
        '
        '_txtBaseAd_0
        '
        Me._txtBaseAd_0.AcceptsReturn = True
        Me._txtBaseAd_0.BackColor = System.Drawing.SystemColors.Window
        Me._txtBaseAd_0.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtBaseAd_0.ForeColor = System.Drawing.SystemColors.WindowText
        Me._txtBaseAd_0.Location = New System.Drawing.Point(123, 159)
        Me._txtBaseAd_0.MaxLength = 0
        Me._txtBaseAd_0.Name = "_txtBaseAd_0"
        Me._txtBaseAd_0.ReadOnly = True
        Me._txtBaseAd_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtBaseAd_0.Size = New System.Drawing.Size(112, 21)
        Me._txtBaseAd_0.TabIndex = 47
        '
        '_txtMbadqty_0
        '
        Me._txtMbadqty_0.AcceptsReturn = True
        Me._txtMbadqty_0.BackColor = System.Drawing.SystemColors.Window
        Me._txtMbadqty_0.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtMbadqty_0.ForeColor = System.Drawing.SystemColors.WindowText
        Me._txtMbadqty_0.Location = New System.Drawing.Point(123, 186)
        Me._txtMbadqty_0.MaxLength = 0
        Me._txtMbadqty_0.Name = "_txtMbadqty_0"
        Me._txtMbadqty_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtMbadqty_0.Size = New System.Drawing.Size(111, 21)
        Me._txtMbadqty_0.TabIndex = 46
        Me._txtMbadqty_0.Text = "256"
        '
        '_txtDeviceAD_0
        '
        Me._txtDeviceAD_0.AcceptsReturn = True
        Me._txtDeviceAD_0.BackColor = System.Drawing.SystemColors.Window
        Me._txtDeviceAD_0.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtDeviceAD_0.ForeColor = System.Drawing.SystemColors.WindowText
        Me._txtDeviceAD_0.Location = New System.Drawing.Point(123, 132)
        Me._txtDeviceAD_0.MaxLength = 0
        Me._txtDeviceAD_0.Name = "_txtDeviceAD_0"
        Me._txtDeviceAD_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtDeviceAD_0.Size = New System.Drawing.Size(113, 21)
        Me._txtDeviceAD_0.TabIndex = 45
        Me._txtDeviceAD_0.Text = "1"
        '
        '_cboPolltime_0
        '
        Me._cboPolltime_0.BackColor = System.Drawing.SystemColors.Window
        Me._cboPolltime_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._cboPolltime_0.ForeColor = System.Drawing.SystemColors.WindowText
        Me._cboPolltime_0.Location = New System.Drawing.Point(122, 57)
        Me._cboPolltime_0.Name = "_cboPolltime_0"
        Me._cboPolltime_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cboPolltime_0.Size = New System.Drawing.Size(83, 20)
        Me._cboPolltime_0.TabIndex = 31
        Me._cboPolltime_0.Text = "5"
        '
        'TXTrtuid
        '
        Me.TXTrtuid.AcceptsReturn = True
        Me.TXTrtuid.BackColor = System.Drawing.SystemColors.Window
        Me.TXTrtuid.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TXTrtuid.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TXTrtuid.Location = New System.Drawing.Point(123, 16)
        Me.TXTrtuid.MaxLength = 0
        Me.TXTrtuid.Name = "TXTrtuid"
        Me.TXTrtuid.ReadOnly = True
        Me.TXTrtuid.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TXTrtuid.Size = New System.Drawing.Size(113, 21)
        Me.TXTrtuid.TabIndex = 22
        Me.TXTrtuid.Text = "1"
        '
        '_Txtphonenumber_0
        '
        Me._Txtphonenumber_0.AcceptsReturn = True
        Me._Txtphonenumber_0.BackColor = System.Drawing.SystemColors.Window
        Me._Txtphonenumber_0.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._Txtphonenumber_0.ForeColor = System.Drawing.SystemColors.WindowText
        Me._Txtphonenumber_0.Location = New System.Drawing.Point(123, 37)
        Me._Txtphonenumber_0.MaxLength = 0
        Me._Txtphonenumber_0.Name = "_Txtphonenumber_0"
        Me._Txtphonenumber_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Txtphonenumber_0.Size = New System.Drawing.Size(113, 21)
        Me._Txtphonenumber_0.TabIndex = 12
        Me._Txtphonenumber_0.Text = "13941209780"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(210, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 33
        Me.Label2.Text = "周期"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(48, 60)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(65, 17)
        Me.Label18.TabIndex = 32
        Me.Label18.Text = "巡测周期："
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(66, 20)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(49, 17)
        Me.Label10.TabIndex = 21
        Me.Label10.Text = "RTUID："
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(54, 39)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(57, 17)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "SIM卡号："
        '
        'fdata
        '
        Me.fdata.BackColor = System.Drawing.SystemColors.Control
        Me.fdata.Controls.Add(Me.TxtDataRTuID)
        Me.fdata.Controls.Add(Me._TxtAD_0)
        Me.fdata.Controls.Add(Me.txtDatablockid)
        Me.fdata.Controls.Add(Me.Label11)
        Me.fdata.Controls.Add(Me.Label9)
        Me.fdata.Controls.Add(Me.Label5)
        Me.fdata.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fdata.Location = New System.Drawing.Point(187, 59)
        Me.fdata.Name = "fdata"
        Me.fdata.Padding = New System.Windows.Forms.Padding(0)
        Me.fdata.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fdata.Size = New System.Drawing.Size(329, 329)
        Me.fdata.TabIndex = 28
        Me.fdata.TabStop = False
        Me.fdata.Text = "数据块配置"
        '
        'TxtDataRTuID
        '
        Me.TxtDataRTuID.AcceptsReturn = True
        Me.TxtDataRTuID.BackColor = System.Drawing.SystemColors.Window
        Me.TxtDataRTuID.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtDataRTuID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtDataRTuID.Location = New System.Drawing.Point(130, 32)
        Me.TxtDataRTuID.MaxLength = 0
        Me.TxtDataRTuID.Name = "TxtDataRTuID"
        Me.TxtDataRTuID.ReadOnly = True
        Me.TxtDataRTuID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtDataRTuID.Size = New System.Drawing.Size(113, 21)
        Me.TxtDataRTuID.TabIndex = 24
        Me.TxtDataRTuID.Text = "0"
        '
        '_TxtAD_0
        '
        Me._TxtAD_0.AcceptsReturn = True
        Me._TxtAD_0.BackColor = System.Drawing.SystemColors.Window
        Me._TxtAD_0.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._TxtAD_0.ForeColor = System.Drawing.SystemColors.WindowText
        Me._TxtAD_0.Location = New System.Drawing.Point(129, 104)
        Me._TxtAD_0.MaxLength = 0
        Me._TxtAD_0.Name = "_TxtAD_0"
        Me._TxtAD_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._TxtAD_0.Size = New System.Drawing.Size(113, 21)
        Me._TxtAD_0.TabIndex = 17
        Me._TxtAD_0.Text = "1"
        '
        'txtDatablockid
        '
        Me.txtDatablockid.AcceptsReturn = True
        Me.txtDatablockid.BackColor = System.Drawing.SystemColors.Window
        Me.txtDatablockid.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDatablockid.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDatablockid.Location = New System.Drawing.Point(130, 64)
        Me.txtDatablockid.MaxLength = 0
        Me.txtDatablockid.Name = "txtDatablockid"
        Me.txtDatablockid.ReadOnly = True
        Me.txtDatablockid.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDatablockid.Size = New System.Drawing.Size(113, 21)
        Me.txtDatablockid.TabIndex = 16
        Me.txtDatablockid.Text = "0"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(40, 32)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(65, 17)
        Me.Label11.TabIndex = 23
        Me.Label11.Text = "RTU编号："
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(40, 64)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(73, 17)
        Me.Label9.TabIndex = 19
        Me.Label9.Text = "数据块编号："
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(28, 107)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(83, 21)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "从设备地址："
        '
        'fReadMtrCfg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(537, 436)
        Me.Controls.Add(Me.fdata)
        Me.Controls.Add(Me.frtu)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.AddRtu)
        Me.Controls.Add(Me.Command5)
        Me.Controls.Add(Me.Command1)
        Me.Controls.Add(Me.Command4)
        Me.Controls.Add(Me.DeleteObj)
        Me.Name = "fReadMtrCfg"
        Me.Text = "浙江天信流量计配置"
        Me.Frame4.ResumeLayout(False)
        Me.frtu.ResumeLayout(False)
        Me.frtu.PerformLayout()
        Me.fdata.ResumeLayout(False)
        Me.fdata.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents TreeView1 As System.Windows.Forms.TreeView
    Public WithEvents AddRtu As System.Windows.Forms.Button
    Public WithEvents Command5 As System.Windows.Forms.Button
    Public WithEvents Command1 As System.Windows.Forms.Button
    Public WithEvents Command4 As System.Windows.Forms.Button
    Public WithEvents DeleteObj As System.Windows.Forms.Button
    Public WithEvents ImageList1 As System.Windows.Forms.ImageList
    Public WithEvents frtu As System.Windows.Forms.GroupBox
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents _txtBaseAd_0 As System.Windows.Forms.TextBox
    Public WithEvents _txtMbadqty_0 As System.Windows.Forms.TextBox
    Public WithEvents _txtDeviceAD_0 As System.Windows.Forms.TextBox
    Public WithEvents _cboPolltime_0 As System.Windows.Forms.ComboBox
    Public WithEvents TXTrtuid As System.Windows.Forms.TextBox
    Public WithEvents _Txtphonenumber_0 As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents fdata As System.Windows.Forms.GroupBox
    Public WithEvents TxtDataRTuID As System.Windows.Forms.TextBox
    Public WithEvents _TxtAD_0 As System.Windows.Forms.TextBox
    Public WithEvents txtDatablockid As System.Windows.Forms.TextBox
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
End Class
