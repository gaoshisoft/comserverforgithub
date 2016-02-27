Imports GPRSComServer.GprsCom

Namespace GprsCom.DtNode
    Class DwDtNode
        Inherits System.Windows.Forms.TreeNode
        Public DtBlock As DWDataBlock
        Public WithEvents TxtSlaveDevId As TextBox
        Public WithEvents TxtXs As TextBox

        Function GetDistinctNodeText(ByVal T As String) As String
            'Dim T As String
            Dim i As Integer

            Dim T1 As String
            T1 = T
            Do Until Not Me.Parent.Nodes.ContainsKey(T1)

                i += 1
                T1 = T & CStr(i)


            Loop
            GetDistinctNodeText = T1
        End Function

        Sub New(ByVal P As TreeNode, ByVal NodText As String, ByVal D As DWDataBlock)
            MyBase.New()

            P.Nodes.Add(Me)
            Dim k As String
            k = Me.GetDistinctNodeText(NodText)

            MyBase.Name = k
            MyBase.Text = k
            Me.Checked = True
            Me.EnsureVisible()
            Me.DtBlock = D
            Me.DtBlock.BlockName = k
            TxtSlaveDevId = New TextBox
            TxtSlaveDevId.Text = Me.DtBlock.Addr
            TxtXs = New TextBox
            TxtXs.Text = Me.DtBlock.LjllXS
            CopyCtrlposition(Fconfig.tabpageDW, Fconfig.txtDWAD, TxtSlaveDevId)
            CopyCtrlposition(Fconfig.tabpageDW, Fconfig.txtDWXS, TxtXs)
        End Sub

        Sub DisDataBlock()
            Fconfig.fdata.Text = "数据块配置-" & Me.Parent.Text & "-" & Me.Text

            'Fconfig.PalXKLLJ.BringToFront()
            Fconfig.Tab.SelectedIndex = 7
            Fconfig.txtSvrAddrLength.Text = Me.DtBlock.SvrAddrLength
            Fconfig.txtSvrMBstartAD.Text = Me.DtBlock.SvrMBADStart

            TxtSlaveDevId.BringToFront()
            TxtXs.BringToFront()
            Fconfig.lbldwparaaddr.Text = " 工况累计(浮点下同)：" & CStr(400000 + Me.DtBlock.SvrMBADStart + 1) & "  标况累计：" &
                                         CStr(400000 + Me.DtBlock.SvrMBADStart + 3) &
                                         " 工况瞬时：" & CStr(400000 + Me.DtBlock.SvrMBADStart + 5) &
                                         " 标况瞬时：" & CStr(400000 + Me.DtBlock.SvrMBADStart + 7) &
                                         " 压力：" & CStr(400000 + Me.DtBlock.SvrMBADStart + 9) &
                                         " 温度：" & CStr(400000 + Me.DtBlock.SvrMBADStart + 11) &
                                         " 电池百分数：" & CStr(400000 + Me.DtBlock.SvrMBADStart + 13)
        End Sub

        Public Property BlockName() As String
            Get
                BlockName = Me.Text
            End Get
            Set(ByVal value As String)
            End Set
        End Property

        Public Property MeterAddr() As Integer
            Get
                MeterAddr = CInt(Me.TxtSlaveDevId.Text)
            End Get
            Set(ByVal value As Integer)
            End Set
        End Property

        Public Property Enable() As Boolean
            Get
                Enable = Me.Checked
            End Get
            Set(ByVal value As Boolean)
            End Set
        End Property
    End Class
End Namespace