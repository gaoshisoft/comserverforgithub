<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmOnlineDTU
#Region "Windows ������������ɵĴ��� "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'�˵����� Windows ���������������ġ�
		InitializeComponent()
		'�˴����� MDI �Ӵ��塣
		'�˴���ģ�� VB6 
		' ���Զ����غ���ʾ
		' MDI �Ӽ��ĸ���
		' �Ĺ��ܡ�
        Me.MdiParent = GPRSComServer.MainProg.MDIfmain
        GPRSComServer.MainProg.MDIfmain.Show()
        'Me.InitGprsRTUs()
      
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
    Public WithEvents Command1 As System.Windows.Forms.Button
    Public WithEvents Label1 As System.Windows.Forms.Label
	'ע��: ���¹����� Windows ����������������
	'����ʹ�� Windows ������������޸�����
	'��Ҫʹ�ô���༭���޸�����
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOnlineDTU))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Command1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Itmmenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.����������ϢToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.�����ش�RTUToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.��������RTUͨ��ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ListView1 = New GPRSComServer.DoubleBufferListView(Me.components)
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Itmmenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'Command1
        '
        Me.Command1.BackColor = System.Drawing.SystemColors.Control
        Me.Command1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command1.Location = New System.Drawing.Point(300, 717)
        Me.Command1.Name = "Command1"
        Me.Command1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command1.Size = New System.Drawing.Size(79, 21)
        Me.Command1.TabIndex = 0
        Me.Command1.Text = "�ر�"
        Me.Command1.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(8, 576)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(393, 33)
        Me.Label1.TabIndex = 2
        '
        'Itmmenu
        '
        Me.Itmmenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.����������ϢToolStripMenuItem, Me.�����ش�RTUToolStripMenuItem, Me.��������RTUͨ��ToolStripMenuItem})
        Me.Itmmenu.Name = "ContextMenuStrip1"
        Me.Itmmenu.Size = New System.Drawing.Size(173, 70)
        '
        '����������ϢToolStripMenuItem
        '
        Me.����������ϢToolStripMenuItem.Name = "����������ϢToolStripMenuItem"
        Me.����������ϢToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.����������ϢToolStripMenuItem.Text = "����������Ϣ"
        '
        '�����ش�RTUToolStripMenuItem
        '
        Me.�����ش�RTUToolStripMenuItem.Name = "�����ش�RTUToolStripMenuItem"
        Me.�����ش�RTUToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.�����ش�RTUToolStripMenuItem.Text = "�����ش�RTU"
        '
        '��������RTUͨ��ToolStripMenuItem
        '
        Me.��������RTUͨ��ToolStripMenuItem.Name = "��������RTUͨ��ToolStripMenuItem"
        Me.��������RTUͨ��ToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.��������RTUͨ��ToolStripMenuItem.Text = "��������RTUͨ��"
        '
        'ListView1
        '
        Me.ListView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5})
        Me.ListView1.ContextMenuStrip = Me.Itmmenu
        Me.ListView1.Cursor = System.Windows.Forms.Cursors.Default
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.Location = New System.Drawing.Point(0, 0)
        Me.ListView1.Margin = New System.Windows.Forms.Padding(1)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(824, 750)
        Me.ListView1.TabIndex = 3
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "վ��"
        Me.ColumnHeader1.Width = 100
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "��¼ʱ��"
        Me.ColumnHeader2.Width = 150
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "ͨѶ��ʽ"
        Me.ColumnHeader3.Width = 220
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Ѳ������"
        Me.ColumnHeader4.Width = 80
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "״̬"
        '
        'frmOnlineDTU
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(824, 750)
        Me.ControlBox = False
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.Command1)
        Me.Controls.Add(Me.Label1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.ForeColor = System.Drawing.SystemColors.Control
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOnlineDTU"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Itmmenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ListView1 As GPRSComServer.DoubleBufferListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Itmmenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ����������ϢToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents �����ش�RTUToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ��������RTUͨ��ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
#End Region 
End Class