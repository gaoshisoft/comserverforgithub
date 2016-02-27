<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmConnstatequery
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
    'Public WithEvents DTPicker1 As MSComCtl2.DTPicker
    Public WithEvents Command1 As System.Windows.Forms.Button
    Public WithEvents _ListView1_ColumnHeader_1 As System.Windows.Forms.ColumnHeader
    Public WithEvents _ListView1_ColumnHeader_2 As System.Windows.Forms.ColumnHeader
    Public WithEvents _ListView1_ColumnHeader_3 As System.Windows.Forms.ColumnHeader
    Public WithEvents ListView1 As System.Windows.Forms.ListView
    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器来修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConnstatequery))
        Me.Command1 = New System.Windows.Forms.Button
        Me.ListView1 = New System.Windows.Forms.ListView
        Me._ListView1_ColumnHeader_1 = New System.Windows.Forms.ColumnHeader(1)
        Me._ListView1_ColumnHeader_2 = New System.Windows.Forms.ColumnHeader
        Me._ListView1_ColumnHeader_3 = New System.Windows.Forms.ColumnHeader
        Me.DTPicker1 = New System.Windows.Forms.DateTimePicker
        Me.SuspendLayout()
        '
        'Command1
        '
        Me.Command1.BackColor = System.Drawing.SystemColors.Control
        Me.Command1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command1.Location = New System.Drawing.Point(216, 356)
        Me.Command1.Name = "Command1"
        Me.Command1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command1.Size = New System.Drawing.Size(73, 23)
        Me.Command1.TabIndex = 1
        Me.Command1.Text = "关闭"
        Me.Command1.UseVisualStyleBackColor = False
        '
        'ListView1
        '
        Me.ListView1.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.ListView1.BackColor = System.Drawing.Color.White
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me._ListView1_ColumnHeader_1, Me._ListView1_ColumnHeader_2, Me._ListView1_ColumnHeader_3})
        Me.ListView1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.HotTracking = True
        Me.ListView1.HoverSelection = True
        Me.ListView1.LabelEdit = True
        Me.ListView1.Location = New System.Drawing.Point(0, 25)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(523, 325)
        Me.ListView1.TabIndex = 0
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        '_ListView1_ColumnHeader_1
        '
        Me._ListView1_ColumnHeader_1.Text = "站名"
        Me._ListView1_ColumnHeader_1.Width = 170
        '
        '_ListView1_ColumnHeader_2
        '
        Me._ListView1_ColumnHeader_2.Text = "时间"
        Me._ListView1_ColumnHeader_2.Width = 236
        '
        '_ListView1_ColumnHeader_3
        '
        Me._ListView1_ColumnHeader_3.Text = "事件"
        Me._ListView1_ColumnHeader_3.Width = 170
        '
        'DTPicker1
        '
        Me.DTPicker1.Location = New System.Drawing.Point(0, -2)
        Me.DTPicker1.Name = "DTPicker1"
        Me.DTPicker1.Size = New System.Drawing.Size(145, 21)
        Me.DTPicker1.TabIndex = 2
        '
        'frmConnstatequery
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(525, 387)
        Me.Controls.Add(Me.DTPicker1)
        Me.Controls.Add(Me.Command1)
        Me.Controls.Add(Me.ListView1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "frmConnstatequery"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "连接状态查询"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DTPicker1 As System.Windows.Forms.DateTimePicker
#End Region
End Class