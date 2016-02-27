Namespace GprsCom
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmPortionPoll
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
        Public WithEvents Command1 As System.Windows.Forms.Button
        Public WithEvents lbltimepass As System.Windows.Forms.Label
        'Public WithEvents Shape1 As Microsoft.VisualBasic.PowerPacks.RectangleShape
        Public WithEvents Label2 As System.Windows.Forms.Label
        Public WithEvents Label1 As System.Windows.Forms.Label
        'Public WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
        '注意: 以下过程是 Windows 窗体设计器所必需的
        '可以使用 Windows 窗体设计器来修改它。
        '不要使用代码编辑器修改它。
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(FrmPortionPoll))
            Me.components = New System.ComponentModel.Container()
            Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
            'Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer
            Me.Command1 = New System.Windows.Forms.Button
            Me.lbltimepass = New System.Windows.Forms.Label
            'Me.Shape1 = New Microsoft.VisualBasic.PowerPacks.RectangleShape
            Me.Label2 = New System.Windows.Forms.Label
            Me.Label1 = New System.Windows.Forms.Label
            Me.SuspendLayout()
            Me.ToolTip1.Active = True
            Me.BackColor = System.Drawing.SystemColors.ActiveBorder
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.ClientSize = New System.Drawing.Size(345, 149)
            Me.Location = New System.Drawing.Point(0, 0)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ControlBox = True
            Me.Enabled = True
            Me.KeyPreview = False
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Me.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.HelpButton = False
            Me.WindowState = System.Windows.Forms.FormWindowState.Normal
            Me.Name = "frmPortionPoll"
            Me.Command1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.Command1.Text = "结束单点巡检"
            Me.Command1.Size = New System.Drawing.Size(101, 21)
            Me.Command1.Location = New System.Drawing.Point(108, 118)
            Me.Command1.TabIndex = 0
            Me.Command1.BackColor = System.Drawing.SystemColors.Control
            Me.Command1.CausesValidation = True
            Me.Command1.Enabled = True
            Me.Command1.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Command1.Cursor = System.Windows.Forms.Cursors.Default
            Me.Command1.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Command1.TabStop = True
            Me.Command1.Name = "Command1"
            Me.lbltimepass.Size = New System.Drawing.Size(317, 23)
            Me.lbltimepass.Location = New System.Drawing.Point(14, 92)
            Me.lbltimepass.TabIndex = 3
            Me.lbltimepass.TextAlign = System.Drawing.ContentAlignment.TopLeft
            Me.lbltimepass.BackColor = System.Drawing.Color.Transparent
            Me.lbltimepass.Enabled = True
            Me.lbltimepass.ForeColor = System.Drawing.SystemColors.ControlText
            Me.lbltimepass.Cursor = System.Windows.Forms.Cursors.Default
            Me.lbltimepass.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.lbltimepass.UseMnemonic = True
            Me.lbltimepass.Visible = True
            Me.lbltimepass.AutoSize = False
            Me.lbltimepass.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.lbltimepass.Name = "lbltimepass"
            'Me.Shape1.Size = New System.Drawing.Size(341, 141)
            'Me.Shape1.Location = New System.Drawing.Point(2, 4)
            'Me.Shape1.BackColor = System.Drawing.SystemColors.Window
            'Me.Shape1.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Transparent
            'Me.Shape1.BorderColor = System.Drawing.SystemColors.WindowText
            'Me.Shape1.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid
            'Me.Shape1.BorderWidth = 1
            'Me.Shape1.FillColor = System.Drawing.Color.Black
            'Me.Shape1.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Transparent
            'Me.Shape1.Visible = True
            'Me.Shape1.Name = "Shape1"
            Me.Label2.Text = "Label2"
            Me.Label2.Size = New System.Drawing.Size(309, 59)
            Me.Label2.Location = New System.Drawing.Point(14, 28)
            Me.Label2.TabIndex = 2
            Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopLeft
            Me.Label2.BackColor = System.Drawing.Color.Transparent
            Me.Label2.Enabled = True
            Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
            Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Label2.UseMnemonic = True
            Me.Label2.Visible = True
            Me.Label2.AutoSize = False
            Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.Label2.Name = "Label2"
            Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
            Me.Label1.Text = "Label1"
            Me.Label1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
            Me.Label1.ForeColor = System.Drawing.Color.Black
            Me.Label1.Size = New System.Drawing.Size(179, 17)
            Me.Label1.Location = New System.Drawing.Point(76, 8)
            Me.Label1.TabIndex = 1
            Me.Label1.BackColor = System.Drawing.Color.Transparent
            Me.Label1.Enabled = True
            Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
            Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Label1.UseMnemonic = True
            Me.Label1.Visible = True
            Me.Label1.AutoSize = False
            Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.Label1.Name = "Label1"
            Me.Controls.Add(Command1)
            Me.Controls.Add(lbltimepass)
            'Me.ShapeContainer1.Shapes.Add(Shape1)
            Me.Controls.Add(Label2)
            Me.Controls.Add(Label1)
            'Me.Controls.Add(ShapeContainer1)
            Me.ResumeLayout(False)
            Me.PerformLayout()
        End Sub
#End Region
    End Class
End Namespace