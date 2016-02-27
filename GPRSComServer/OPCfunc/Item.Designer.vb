<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class ItemDlg
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
	Public WithEvents chkifswapbyte As System.Windows.Forms.CheckBox
	Public WithEvents txtBit As System.Windows.Forms.TextBox
	Public WithEvents Label7 As System.Windows.Forms.Label
	Public WithEvents frabit As System.Windows.Forms.Panel
	Public WithEvents chkneedconvert As System.Windows.Forms.CheckBox
	Public WithEvents txtConvertedUP As System.Windows.Forms.TextBox
	Public WithEvents txtConvertedDown As System.Windows.Forms.TextBox
	Public WithEvents txtAIrangeUP As System.Windows.Forms.TextBox
	Public WithEvents txtAIrangedown As System.Windows.Forms.TextBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Fraconvert As System.Windows.Forms.GroupBox
	Public WithEvents txtMBAD As System.Windows.Forms.TextBox
	Public WithEvents cboDevad As System.Windows.Forms.ComboBox
	Public WithEvents cboDataType As System.Windows.Forms.ComboBox
    Public WithEvents txtstaName As System.Windows.Forms.TextBox
    Public WithEvents CancelButton_Renamed As System.Windows.Forms.Button
    Public WithEvents OKButton As System.Windows.Forms.Button
    Public WithEvents lblMBAD As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器来修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ItemDlg))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.chkifswapbyte = New System.Windows.Forms.CheckBox()
        Me.frabit = New System.Windows.Forms.Panel()
        Me.txtBit = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chkneedconvert = New System.Windows.Forms.CheckBox()
        Me.Fraconvert = New System.Windows.Forms.GroupBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtConvertedUP = New System.Windows.Forms.TextBox()
        Me.txtConvertedDown = New System.Windows.Forms.TextBox()
        Me.txtAIrangeUP = New System.Windows.Forms.TextBox()
        Me.txtAIrangedown = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtMBAD = New System.Windows.Forms.TextBox()
        Me.cboDevad = New System.Windows.Forms.ComboBox()
        Me.cboDataType = New System.Windows.Forms.ComboBox()
        Me.txtstaName = New System.Windows.Forms.TextBox()
        Me.CancelButton_Renamed = New System.Windows.Forms.Button()
        Me.OKButton = New System.Windows.Forms.Button()
        Me.lblMBAD = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtChineseDis = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtExpresion = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cbounitstr = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtuplimit = New System.Windows.Forms.TextBox()
        Me.txtDownlimit = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtparaname = New System.Windows.Forms.TextBox()
        Me.frabit.SuspendLayout()
        Me.Fraconvert.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkifswapbyte
        '
        Me.chkifswapbyte.BackColor = System.Drawing.SystemColors.Control
        Me.chkifswapbyte.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkifswapbyte.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkifswapbyte.Location = New System.Drawing.Point(178, 166)
        Me.chkifswapbyte.Name = "chkifswapbyte"
        Me.chkifswapbyte.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkifswapbyte.Size = New System.Drawing.Size(129, 17)
        Me.chkifswapbyte.TabIndex = 13
        Me.chkifswapbyte.Text = "高低字节交换"
        Me.chkifswapbyte.UseVisualStyleBackColor = False
        '
        'frabit
        '
        Me.frabit.BackColor = System.Drawing.SystemColors.Control
        Me.frabit.Controls.Add(Me.txtBit)
        Me.frabit.Controls.Add(Me.Label7)
        Me.frabit.Cursor = System.Windows.Forms.Cursors.Default
        Me.frabit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frabit.Location = New System.Drawing.Point(320, 134)
        Me.frabit.Name = "frabit"
        Me.frabit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frabit.Size = New System.Drawing.Size(81, 25)
        Me.frabit.TabIndex = 20
        Me.frabit.Text = "Frame2"
        '
        'txtBit
        '
        Me.txtBit.AcceptsReturn = True
        Me.txtBit.BackColor = System.Drawing.SystemColors.Window
        Me.txtBit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBit.Location = New System.Drawing.Point(24, 0)
        Me.txtBit.MaxLength = 0
        Me.txtBit.Name = "txtBit"
        Me.txtBit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBit.Size = New System.Drawing.Size(33, 21)
        Me.txtBit.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(0, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(25, 17)
        Me.Label7.TabIndex = 22
        Me.Label7.Text = "位："
        '
        'chkneedconvert
        '
        Me.chkneedconvert.BackColor = System.Drawing.SystemColors.Control
        Me.chkneedconvert.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkneedconvert.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkneedconvert.Location = New System.Drawing.Point(8, 224)
        Me.chkneedconvert.Name = "chkneedconvert"
        Me.chkneedconvert.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkneedconvert.Size = New System.Drawing.Size(65, 17)
        Me.chkneedconvert.TabIndex = 10
        Me.chkneedconvert.Text = "需转换"
        Me.chkneedconvert.UseVisualStyleBackColor = False
        '
        'Fraconvert
        '
        Me.Fraconvert.BackColor = System.Drawing.SystemColors.Control
        Me.Fraconvert.Controls.Add(Me.Label9)
        Me.Fraconvert.Controls.Add(Me.Label8)
        Me.Fraconvert.Controls.Add(Me.txtConvertedUP)
        Me.Fraconvert.Controls.Add(Me.txtConvertedDown)
        Me.Fraconvert.Controls.Add(Me.txtAIrangeUP)
        Me.Fraconvert.Controls.Add(Me.txtAIrangedown)
        Me.Fraconvert.Controls.Add(Me.Label6)
        Me.Fraconvert.Controls.Add(Me.Label4)
        Me.Fraconvert.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Fraconvert.Location = New System.Drawing.Point(90, 184)
        Me.Fraconvert.Name = "Fraconvert"
        Me.Fraconvert.Padding = New System.Windows.Forms.Padding(0)
        Me.Fraconvert.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Fraconvert.Size = New System.Drawing.Size(329, 97)
        Me.Fraconvert.TabIndex = 10
        Me.Fraconvert.TabStop = False
        Me.Fraconvert.Text = "转换"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(184, 67)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(17, 12)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "到"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(184, 24)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(17, 12)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = "到"
        '
        'txtConvertedUP
        '
        Me.txtConvertedUP.AcceptsReturn = True
        Me.txtConvertedUP.BackColor = System.Drawing.SystemColors.Window
        Me.txtConvertedUP.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtConvertedUP.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtConvertedUP.Location = New System.Drawing.Point(216, 64)
        Me.txtConvertedUP.MaxLength = 0
        Me.txtConvertedUP.Name = "txtConvertedUP"
        Me.txtConvertedUP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtConvertedUP.Size = New System.Drawing.Size(89, 21)
        Me.txtConvertedUP.TabIndex = 9
        Me.txtConvertedUP.Text = "Text4"
        '
        'txtConvertedDown
        '
        Me.txtConvertedDown.AcceptsReturn = True
        Me.txtConvertedDown.BackColor = System.Drawing.SystemColors.Window
        Me.txtConvertedDown.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtConvertedDown.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtConvertedDown.Location = New System.Drawing.Point(88, 64)
        Me.txtConvertedDown.MaxLength = 0
        Me.txtConvertedDown.Name = "txtConvertedDown"
        Me.txtConvertedDown.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtConvertedDown.Size = New System.Drawing.Size(89, 21)
        Me.txtConvertedDown.TabIndex = 8
        Me.txtConvertedDown.Text = "Text3"
        '
        'txtAIrangeUP
        '
        Me.txtAIrangeUP.AcceptsReturn = True
        Me.txtAIrangeUP.BackColor = System.Drawing.SystemColors.Window
        Me.txtAIrangeUP.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAIrangeUP.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAIrangeUP.Location = New System.Drawing.Point(216, 24)
        Me.txtAIrangeUP.MaxLength = 0
        Me.txtAIrangeUP.Name = "txtAIrangeUP"
        Me.txtAIrangeUP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAIrangeUP.Size = New System.Drawing.Size(89, 21)
        Me.txtAIrangeUP.TabIndex = 7
        Me.txtAIrangeUP.Text = "Text2"
        '
        'txtAIrangedown
        '
        Me.txtAIrangedown.AcceptsReturn = True
        Me.txtAIrangedown.BackColor = System.Drawing.SystemColors.Window
        Me.txtAIrangedown.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAIrangedown.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAIrangedown.Location = New System.Drawing.Point(88, 24)
        Me.txtAIrangedown.MaxLength = 0
        Me.txtAIrangedown.Name = "txtAIrangedown"
        Me.txtAIrangedown.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAIrangedown.Size = New System.Drawing.Size(89, 21)
        Me.txtAIrangedown.TabIndex = 6
        Me.txtAIrangedown.Text = "Text1"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(16, 64)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(121, 21)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "转换后范围："
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(16, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(81, 17)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "裸数据范围："
        '
        'txtMBAD
        '
        Me.txtMBAD.AcceptsReturn = True
        Me.txtMBAD.BackColor = System.Drawing.SystemColors.Window
        Me.txtMBAD.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMBAD.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMBAD.Location = New System.Drawing.Point(168, 134)
        Me.txtMBAD.MaxLength = 0
        Me.txtMBAD.Name = "txtMBAD"
        Me.txtMBAD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMBAD.Size = New System.Drawing.Size(139, 21)
        Me.txtMBAD.TabIndex = 2
        '
        'cboDevad
        '
        Me.cboDevad.BackColor = System.Drawing.SystemColors.Window
        Me.cboDevad.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDevad.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDevad.Location = New System.Drawing.Point(168, 102)
        Me.cboDevad.Name = "cboDevad"
        Me.cboDevad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDevad.Size = New System.Drawing.Size(160, 20)
        Me.cboDevad.TabIndex = 4
        '
        'cboDataType
        '
        Me.cboDataType.BackColor = System.Drawing.SystemColors.Window
        Me.cboDataType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDataType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDataType.Location = New System.Drawing.Point(168, 70)
        Me.cboDataType.Name = "cboDataType"
        Me.cboDataType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDataType.Size = New System.Drawing.Size(160, 20)
        Me.cboDataType.TabIndex = 3
        '
        'txtstaName
        '
        Me.txtstaName.AcceptsReturn = True
        Me.txtstaName.BackColor = System.Drawing.SystemColors.Window
        Me.txtstaName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtstaName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtstaName.Location = New System.Drawing.Point(168, 12)
        Me.txtstaName.MaxLength = 0
        Me.txtstaName.Name = "txtstaName"
        Me.txtstaName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtstaName.Size = New System.Drawing.Size(67, 21)
        Me.txtstaName.TabIndex = 1
        '
        'CancelButton_Renamed
        '
        Me.CancelButton_Renamed.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton_Renamed.Cursor = System.Windows.Forms.Cursors.Default
        Me.CancelButton_Renamed.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CancelButton_Renamed.Location = New System.Drawing.Point(276, 408)
        Me.CancelButton_Renamed.Name = "CancelButton_Renamed"
        Me.CancelButton_Renamed.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CancelButton_Renamed.Size = New System.Drawing.Size(81, 25)
        Me.CancelButton_Renamed.TabIndex = 12
        Me.CancelButton_Renamed.Text = "取消"
        Me.CancelButton_Renamed.UseVisualStyleBackColor = False
        '
        'OKButton
        '
        Me.OKButton.BackColor = System.Drawing.SystemColors.Control
        Me.OKButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.OKButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OKButton.Location = New System.Drawing.Point(140, 408)
        Me.OKButton.Name = "OKButton"
        Me.OKButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OKButton.Size = New System.Drawing.Size(81, 25)
        Me.OKButton.TabIndex = 11
        Me.OKButton.Text = "确定"
        Me.OKButton.UseVisualStyleBackColor = False
        '
        'lblMBAD
        '
        Me.lblMBAD.BackColor = System.Drawing.SystemColors.Control
        Me.lblMBAD.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMBAD.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMBAD.Location = New System.Drawing.Point(88, 134)
        Me.lblMBAD.Name = "lblMBAD"
        Me.lblMBAD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMBAD.Size = New System.Drawing.Size(73, 17)
        Me.lblMBAD.TabIndex = 8
        Me.lblMBAD.Text = "MBAD："
        Me.lblMBAD.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(89, 105)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(73, 17)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "DEVAD："
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(88, 73)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(73, 17)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Data Type："
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(88, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(73, 17)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "中文描述："
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(88, 12)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(73, 17)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Item Name："
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtChineseDis
        '
        Me.txtChineseDis.AcceptsReturn = True
        Me.txtChineseDis.BackColor = System.Drawing.SystemColors.Window
        Me.txtChineseDis.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtChineseDis.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtChineseDis.Location = New System.Drawing.Point(167, 41)
        Me.txtChineseDis.MaxLength = 0
        Me.txtChineseDis.Name = "txtChineseDis"
        Me.txtChineseDis.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChineseDis.Size = New System.Drawing.Size(234, 21)
        Me.txtChineseDis.TabIndex = 1
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(106, 304)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(53, 12)
        Me.Label10.TabIndex = 21
        Me.Label10.Text = "表达式："
        '
        'txtExpresion
        '
        Me.txtExpresion.AcceptsReturn = True
        Me.txtExpresion.BackColor = System.Drawing.SystemColors.Window
        Me.txtExpresion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExpresion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtExpresion.Location = New System.Drawing.Point(166, 287)
        Me.txtExpresion.MaxLength = 0
        Me.txtExpresion.Multiline = True
        Me.txtExpresion.Name = "txtExpresion"
        Me.txtExpresion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExpresion.Size = New System.Drawing.Size(235, 47)
        Me.txtExpresion.TabIndex = 1
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(88, 343)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(73, 17)
        Me.Label11.TabIndex = 8
        Me.Label11.Text = "单位："
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cbounitstr
        '
        Me.cbounitstr.BackColor = System.Drawing.SystemColors.Window
        Me.cbounitstr.Cursor = System.Windows.Forms.Cursors.Default
        Me.cbounitstr.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cbounitstr.Items.AddRange(New Object() {"℃", "Kpa", "Mpa", "m3", "m3/h", "Nm3", "Nm3/h", "%", "Kg", "t"})
        Me.cbounitstr.Location = New System.Drawing.Point(166, 340)
        Me.cbounitstr.Name = "cbounitstr"
        Me.cbounitstr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cbounitstr.Size = New System.Drawing.Size(235, 20)
        Me.cbounitstr.TabIndex = 4
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(86, 374)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(73, 17)
        Me.Label12.TabIndex = 8
        Me.Label12.Text = "上限："
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(231, 374)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(73, 17)
        Me.Label13.TabIndex = 8
        Me.Label13.Text = "下限："
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtuplimit
        '
        Me.txtuplimit.AcceptsReturn = True
        Me.txtuplimit.BackColor = System.Drawing.SystemColors.Window
        Me.txtuplimit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtuplimit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtuplimit.Location = New System.Drawing.Point(165, 371)
        Me.txtuplimit.MaxLength = 0
        Me.txtuplimit.Name = "txtuplimit"
        Me.txtuplimit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtuplimit.Size = New System.Drawing.Size(89, 21)
        Me.txtuplimit.TabIndex = 8
        Me.txtuplimit.Text = "99999999999999999"
        '
        'txtDownlimit
        '
        Me.txtDownlimit.AcceptsReturn = True
        Me.txtDownlimit.BackColor = System.Drawing.SystemColors.Window
        Me.txtDownlimit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDownlimit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDownlimit.Location = New System.Drawing.Point(306, 371)
        Me.txtDownlimit.MaxLength = 0
        Me.txtDownlimit.Name = "txtDownlimit"
        Me.txtDownlimit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDownlimit.Size = New System.Drawing.Size(89, 21)
        Me.txtDownlimit.TabIndex = 8
        Me.txtDownlimit.Text = "-99999999999999999999"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(241, 21)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(11, 12)
        Me.Label14.TabIndex = 22
        Me.Label14.Text = "_"
        '
        'txtparaname
        '
        Me.txtparaname.AcceptsReturn = True
        Me.txtparaname.BackColor = System.Drawing.SystemColors.Window
        Me.txtparaname.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtparaname.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtparaname.Location = New System.Drawing.Point(261, 12)
        Me.txtparaname.MaxLength = 0
        Me.txtparaname.Name = "txtparaname"
        Me.txtparaname.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtparaname.Size = New System.Drawing.Size(140, 21)
        Me.txtparaname.TabIndex = 1
        '
        'ItemDlg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(523, 456)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.chkifswapbyte)
        Me.Controls.Add(Me.frabit)
        Me.Controls.Add(Me.txtDownlimit)
        Me.Controls.Add(Me.txtuplimit)
        Me.Controls.Add(Me.chkneedconvert)
        Me.Controls.Add(Me.Fraconvert)
        Me.Controls.Add(Me.txtMBAD)
        Me.Controls.Add(Me.cbounitstr)
        Me.Controls.Add(Me.cboDevad)
        Me.Controls.Add(Me.cboDataType)
        Me.Controls.Add(Me.txtChineseDis)
        Me.Controls.Add(Me.txtparaname)
        Me.Controls.Add(Me.txtExpresion)
        Me.Controls.Add(Me.txtstaName)
        Me.Controls.Add(Me.CancelButton_Renamed)
        Me.Controls.Add(Me.OKButton)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.lblMBAD)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(184, 250)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ItemDlg"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Add Item"
        Me.frabit.ResumeLayout(False)
        Me.frabit.PerformLayout()
        Me.Fraconvert.ResumeLayout(False)
        Me.Fraconvert.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents txtChineseDis As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents txtExpresion As System.Windows.Forms.TextBox
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents cbounitstr As System.Windows.Forms.ComboBox
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents txtuplimit As System.Windows.Forms.TextBox
    Public WithEvents txtDownlimit As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents txtparaname As System.Windows.Forms.TextBox
#End Region
End Class