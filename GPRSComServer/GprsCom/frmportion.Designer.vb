Namespace GprsCom
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmportion
#Region "Windows ������������ɵĴ��� "
        <System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
            MyBase.New()
            '�˵����� Windows ���������������ġ�
            InitializeComponent()
            'mRtus = New System.Collections.Generic.List(Of IGPRSRTU)
        End Sub
        'Form ��д Dispose������������б�
        <System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
            If Disposing Then
                If Not components Is Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(Disposing)
        End Sub
        'Windows ����������������
        Private components As System.ComponentModel.IContainer
        Public ToolTip1 As System.Windows.Forms.ToolTip
        Public WithEvents Command3 As System.Windows.Forms.Button
        Public WithEvents Command2 As System.Windows.Forms.Button
        Public WithEvents Command1 As System.Windows.Forms.Button
        Public WithEvents List1 As System.Windows.Forms.ListBox
        Public WithEvents Label1 As System.Windows.Forms.Label
        'ע��: ���¹����� Windows ����������������
        '����ʹ�� Windows ������������޸�����
        '��Ҫʹ�ô���༭���޸�����
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.Command3 = New System.Windows.Forms.Button()
            Me.Command2 = New System.Windows.Forms.Button()
            Me.Command1 = New System.Windows.Forms.Button()
            Me.List1 = New System.Windows.Forms.ListBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.cmdClose = New System.Windows.Forms.Button()
            Me.SuspendLayout()
            '
            'Command3
            '
            Me.Command3.BackColor = System.Drawing.SystemColors.Control
            Me.Command3.Cursor = System.Windows.Forms.Cursors.Default
            Me.Command3.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Command3.Location = New System.Drawing.Point(86, 499)
            Me.Command3.Name = "Command3"
            Me.Command3.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Command3.Size = New System.Drawing.Size(73, 23)
            Me.Command3.TabIndex = 4
            Me.Command3.Text = "����Ѳ��"
            Me.Command3.UseVisualStyleBackColor = False
            '
            'Command2
            '
            Me.Command2.BackColor = System.Drawing.SystemColors.Control
            Me.Command2.Cursor = System.Windows.Forms.Cursors.Default
            Me.Command2.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Command2.Location = New System.Drawing.Point(7, 499)
            Me.Command2.Name = "Command2"
            Me.Command2.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Command2.Size = New System.Drawing.Size(73, 23)
            Me.Command2.TabIndex = 3
            Me.Command2.Text = "ֹͣѲ��"
            Me.Command2.UseVisualStyleBackColor = False
            '
            'Command1
            '
            Me.Command1.BackColor = System.Drawing.SystemColors.Control
            Me.Command1.Cursor = System.Windows.Forms.Cursors.Default
            Me.Command1.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Command1.Location = New System.Drawing.Point(165, 499)
            Me.Command1.Name = "Command1"
            Me.Command1.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Command1.Size = New System.Drawing.Size(73, 23)
            Me.Command1.TabIndex = 2
            Me.Command1.Text = "ȫ��Ѳ��"
            Me.Command1.UseVisualStyleBackColor = False
            '
            'List1
            '
            Me.List1.BackColor = System.Drawing.SystemColors.Window
            Me.List1.Cursor = System.Windows.Forms.Cursors.Default
            Me.List1.ForeColor = System.Drawing.SystemColors.WindowText
            Me.List1.ItemHeight = 12
            Me.List1.Location = New System.Drawing.Point(7, 2)
            Me.List1.Name = "List1"
            Me.List1.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.List1.Size = New System.Drawing.Size(312, 472)
            Me.List1.TabIndex = 0
            '
            'Label1
            '
            Me.Label1.BackColor = System.Drawing.SystemColors.Control
            Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
            Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Label1.Location = New System.Drawing.Point(32, 479)
            Me.Label1.Name = "Label1"
            Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Label1.Size = New System.Drawing.Size(287, 17)
            Me.Label1.TabIndex = 1
            Me.Label1.Text = "ע��˫���б��е�ĳվ��ɵ����վ�㣡"
            '
            'cmdClose
            '
            Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
            Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
            Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
            Me.cmdClose.Location = New System.Drawing.Point(244, 499)
            Me.cmdClose.Name = "cmdClose"
            Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.cmdClose.Size = New System.Drawing.Size(75, 23)
            Me.cmdClose.TabIndex = 15
            Me.cmdClose.Text = "ȡ��"
            Me.cmdClose.UseVisualStyleBackColor = False
            '
            'frmportion
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.ClientSize = New System.Drawing.Size(331, 534)
            Me.Controls.Add(Me.cmdClose)
            Me.Controls.Add(Me.Command3)
            Me.Controls.Add(Me.Command2)
            Me.Controls.Add(Me.Command1)
            Me.Controls.Add(Me.List1)
            Me.Controls.Add(Me.Label1)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
            Me.Location = New System.Drawing.Point(3, 19)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frmportion"
            Me.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
            Me.Text = "����Ѳ��"
            Me.ResumeLayout(False)

        End Sub
        Public WithEvents cmdClose As System.Windows.Forms.Button
#End Region
    End Class
End Namespace