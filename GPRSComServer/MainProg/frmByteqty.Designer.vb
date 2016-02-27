Namespace MainProg
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmByteqty
#Region "Windows 窗体设计器生成的代码 "
        <System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
            MyBase.New()
            '此调用是 Windows 窗体设计器所必需的。
            InitializeComponent()
            '此窗体是 MDI 子窗体。
            '此代码模拟 VB6 
            ' 的自动加载和显示
            ' MDI 子级的父级
            ' 的功能。
            'Me.MDIParent = GPRSComServer.MDIfmain
            GPRSComServer.MainProg.MdIfmain.Show()
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
        Public WithEvents Command3 As System.Windows.Forms.Button
        Public WithEvents Command1 As System.Windows.Forms.Button
        Public WithEvents Command2 As System.Windows.Forms.Button
        Public WithEvents txtTotalbytes As System.Windows.Forms.TextBox
        Public WithEvents _ListView1_ColumnHeader_1 As System.Windows.Forms.ColumnHeader
        Public WithEvents _ListView1_ColumnHeader_2 As System.Windows.Forms.ColumnHeader
        Public WithEvents _ListView1_ColumnHeader_3 As System.Windows.Forms.ColumnHeader
        Public WithEvents _ListView1_ColumnHeader_4 As System.Windows.Forms.ColumnHeader
        Public WithEvents ListView1 As System.Windows.Forms.ListView
        Public WithEvents Label2 As System.Windows.Forms.Label
        Public WithEvents Label1 As System.Windows.Forms.Label
        '注意: 以下过程是 Windows 窗体设计器所必需的
        '可以使用 Windows 窗体设计器来修改它。
        '不要使用代码编辑器修改它。
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container
            Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.Command3 = New System.Windows.Forms.Button
            Me.Command1 = New System.Windows.Forms.Button
            Me.Command2 = New System.Windows.Forms.Button
            Me.txtTotalbytes = New System.Windows.Forms.TextBox
            Me.ListView1 = New System.Windows.Forms.ListView
            Me._ListView1_ColumnHeader_1 = New System.Windows.Forms.ColumnHeader
            Me._ListView1_ColumnHeader_2 = New System.Windows.Forms.ColumnHeader
            Me._ListView1_ColumnHeader_3 = New System.Windows.Forms.ColumnHeader
            Me._ListView1_ColumnHeader_4 = New System.Windows.Forms.ColumnHeader
            Me.Label2 = New System.Windows.Forms.Label
            Me.Label1 = New System.Windows.Forms.Label
            Me.SuspendLayout()
            '
            'Command3
            '
            Me.Command3.BackColor = System.Drawing.SystemColors.Control
            Me.Command3.Cursor = System.Windows.Forms.Cursors.Default
            Me.Command3.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Command3.Location = New System.Drawing.Point(217, 296)
            Me.Command3.Name = "Command3"
            Me.Command3.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Command3.Size = New System.Drawing.Size(85, 25)
            Me.Command3.TabIndex = 5
            Me.Command3.Text = "历史数据查询"
            Me.Command3.UseVisualStyleBackColor = False
            '
            'Command1
            '
            Me.Command1.BackColor = System.Drawing.SystemColors.Control
            Me.Command1.Cursor = System.Windows.Forms.Cursors.Default
            Me.Command1.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Command1.Location = New System.Drawing.Point(392, 296)
            Me.Command1.Name = "Command1"
            Me.Command1.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Command1.Size = New System.Drawing.Size(85, 25)
            Me.Command1.TabIndex = 4
            Me.Command1.Text = "确定"
            Me.Command1.UseVisualStyleBackColor = False
            '
            'Command2
            '
            Me.Command2.BackColor = System.Drawing.SystemColors.Control
            Me.Command2.Cursor = System.Windows.Forms.Cursors.Default
            Me.Command2.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Command2.Location = New System.Drawing.Point(42, 296)
            Me.Command2.Name = "Command2"
            Me.Command2.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Command2.Size = New System.Drawing.Size(85, 25)
            Me.Command2.TabIndex = 3
            Me.Command2.Text = "刷新"
            Me.Command2.UseVisualStyleBackColor = False
            '
            'txtTotalbytes
            '
            Me.txtTotalbytes.AcceptsReturn = True
            Me.txtTotalbytes.BackColor = System.Drawing.SystemColors.Window
            Me.txtTotalbytes.Cursor = System.Windows.Forms.Cursors.IBeam
            Me.txtTotalbytes.ForeColor = System.Drawing.SystemColors.WindowText
            Me.txtTotalbytes.Location = New System.Drawing.Point(132, 266)
            Me.txtTotalbytes.MaxLength = 0
            Me.txtTotalbytes.Name = "txtTotalbytes"
            Me.txtTotalbytes.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.txtTotalbytes.Size = New System.Drawing.Size(129, 21)
            Me.txtTotalbytes.TabIndex = 2
            '
            'ListView1
            '
            Me.ListView1.BackColor = System.Drawing.Color.White
            Me.ListView1.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me._ListView1_ColumnHeader_1, Me._ListView1_ColumnHeader_2, Me._ListView1_ColumnHeader_3, Me._ListView1_ColumnHeader_4})
            Me.ListView1.ForeColor = System.Drawing.Color.Black
            Me.ListView1.GridLines = True
            Me.ListView1.LabelEdit = True
            Me.ListView1.Location = New System.Drawing.Point(2, 2)
            Me.ListView1.Name = "ListView1"
            Me.ListView1.Size = New System.Drawing.Size(509, 253)
            Me.ListView1.TabIndex = 0
            Me.ListView1.UseCompatibleStateImageBehavior = False
            Me.ListView1.View = System.Windows.Forms.View.Details
            '
            '_ListView1_ColumnHeader_1
            '
            Me._ListView1_ColumnHeader_1.Text = "站点名"
            Me._ListView1_ColumnHeader_1.Width = 170
            '
            '_ListView1_ColumnHeader_2
            '
            Me._ListView1_ColumnHeader_2.Text = "发送字节数"
            Me._ListView1_ColumnHeader_2.Width = 294
            '
            '_ListView1_ColumnHeader_3
            '
            Me._ListView1_ColumnHeader_3.Text = "接收字节数"
            Me._ListView1_ColumnHeader_3.Width = 170
            '
            '_ListView1_ColumnHeader_4
            '
            Me._ListView1_ColumnHeader_4.Text = "本站总收发字节数"
            Me._ListView1_ColumnHeader_4.Width = 170
            '
            'Label2
            '
            Me.Label2.BackColor = System.Drawing.SystemColors.Control
            Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
            Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Label2.Location = New System.Drawing.Point(264, 268)
            Me.Label2.Name = "Label2"
            Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Label2.Size = New System.Drawing.Size(51, 15)
            Me.Label2.TabIndex = 6
            Me.Label2.Text = "字节"
            '
            'Label1
            '
            Me.Label1.BackColor = System.Drawing.SystemColors.Control
            Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
            Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Label1.Location = New System.Drawing.Point(6, 268)
            Me.Label1.Name = "Label1"
            Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Label1.Size = New System.Drawing.Size(129, 15)
            Me.Label1.TabIndex = 1
            Me.Label1.Text = "所有站总收发字节数："
            '
            'frmByteqty
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.ClientSize = New System.Drawing.Size(508, 331)
            Me.Controls.Add(Me.Command3)
            Me.Controls.Add(Me.Command1)
            Me.Controls.Add(Me.Command2)
            Me.Controls.Add(Me.txtTotalbytes)
            Me.Controls.Add(Me.ListView1)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.Label1)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Location = New System.Drawing.Point(3, 22)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frmByteqty"
            Me.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "今日数据流量统计"
            Me.TopMost = True
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
#End Region
    End Class
End Namespace