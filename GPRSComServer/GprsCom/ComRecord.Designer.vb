Namespace GprsCom
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class ComRecord
        Inherits System.Windows.Forms.Form

        'Form 重写 Dispose，以清理组件列表。
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

        'Windows 窗体设计器所必需的
        Private components As System.ComponentModel.IContainer

        '注意: 以下过程是 Windows 窗体设计器所必需的
        '可以使用 Windows 窗体设计器修改它。
        '不要使用代码编辑器修改它。
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container
            Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
            Me.TreeView1 = New System.Windows.Forms.TreeView
            Me.ListView1 = New System.Windows.Forms.ListView
            Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
            Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.清空ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.缓冲区大小ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.SplitContainer1.Panel1.SuspendLayout()
            Me.SplitContainer1.Panel2.SuspendLayout()
            Me.SplitContainer1.SuspendLayout()
            Me.ContextMenuStrip1.SuspendLayout()
            Me.SuspendLayout()
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
            Me.SplitContainer1.Panel2.Controls.Add(Me.ListView1)
            Me.SplitContainer1.Size = New System.Drawing.Size(715, 512)
            Me.SplitContainer1.SplitterDistance = 238
            Me.SplitContainer1.TabIndex = 0
            '
            'TreeView1
            '
            Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.TreeView1.Location = New System.Drawing.Point(0, 0)
            Me.TreeView1.Name = "TreeView1"
            Me.TreeView1.Size = New System.Drawing.Size(238, 512)
            Me.TreeView1.TabIndex = 0
            '
            'ListView1
            '
            Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
            Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ListView1.Location = New System.Drawing.Point(0, 0)
            Me.ListView1.Name = "ListView1"
            Me.ListView1.Size = New System.Drawing.Size(473, 512)
            Me.ListView1.TabIndex = 0
            Me.ListView1.UseCompatibleStateImageBehavior = False
            Me.ListView1.View = System.Windows.Forms.View.Details
            '
            'ColumnHeader1
            '
            Me.ColumnHeader1.Text = "数据"
            Me.ColumnHeader1.Width = 1000
            '
            'ContextMenuStrip1
            '
            Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.清空ToolStripMenuItem, Me.缓冲区大小ToolStripMenuItem})
            Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
            Me.ContextMenuStrip1.Size = New System.Drawing.Size(131, 48)
            '
            '清空ToolStripMenuItem
            '
            Me.清空ToolStripMenuItem.Name = "清空ToolStripMenuItem"
            Me.清空ToolStripMenuItem.Size = New System.Drawing.Size(130, 22)
            Me.清空ToolStripMenuItem.Text = "清空"
            '
            '缓冲区大小ToolStripMenuItem
            '
            Me.缓冲区大小ToolStripMenuItem.Name = "缓冲区大小ToolStripMenuItem"
            Me.缓冲区大小ToolStripMenuItem.Size = New System.Drawing.Size(130, 22)
            Me.缓冲区大小ToolStripMenuItem.Text = "缓冲区大小"
            '
            'ComRecord
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(715, 512)
            Me.Controls.Add(Me.SplitContainer1)
            Me.Name = "ComRecord"
            Me.Text = "上传数据记录"
            Me.SplitContainer1.Panel1.ResumeLayout(False)
            Me.SplitContainer1.Panel2.ResumeLayout(False)
            Me.SplitContainer1.ResumeLayout(False)
            Me.ContextMenuStrip1.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
        Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
        Friend WithEvents ListView1 As System.Windows.Forms.ListView
        Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
        Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents 清空ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents 缓冲区大小ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    End Class
End Namespace