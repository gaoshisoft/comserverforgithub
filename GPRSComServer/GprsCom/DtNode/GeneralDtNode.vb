Namespace GprsCom.DtNode
    Public Class GeneralDtNode
        Inherits Windows.Forms.TreeNode
        Public DtBlock As IDataBlock
        Public WithEvents TxtSlaveDevId As TextBox

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

        Sub New(ByVal P As TreeNode, ByVal nodText As String, ByVal d As IDataBlock)
            MyBase.New()

            P.Nodes.Add(Me)
            Dim k As String
            k = Me.GetDistinctNodeText(nodText)

            MyBase.Name = k
            MyBase.Text = k
            Me.Checked = True
            Me.EnsureVisible()
            Me.DtBlock = d
            Me.DtBlock.BlockName = k
            TxtSlaveDevId = New TextBox
            TxtSlaveDevId.Text = Me.DtBlock.Addr
            CopyCtrlposition(Fconfig.GeneralTab, Fconfig.txtaddr, TxtSlaveDevId)
        End Sub

        Sub DisDataBlock()

            Fconfig.fdata.Text = "数据块配置-" & Me.Parent.Text & "-" & Me.Text
            'Fconfig.PalTXLLJ.BringToFront()
            Fconfig.Tab.SelectedIndex = 13
            Fconfig.Tab.SelectedTab.Text = Me.Text
            Fconfig.txtSvrAddrLength.Text = Me.DtBlock.SvrAddrLength
            Fconfig.txtSvrMBstartAD.Text = Me.DtBlock.SvrMBADStart
            Fconfig.lbllljname.Text = Me.Text
            'txtSlaveDevID.Text = Me.DtBlock.Addr
            txtSlaveDevID.BringToFront()
            'Fconfig.lblt()
            Fconfig.lbllljparadis.Text = Me.DtBlock.ParaAddr
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
                MeterAddr = CInt(Me.txtSlaveDevID.Text)
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