Imports GPRSComServer.GprsCom

Namespace GprsCom.DtNode
    Class LdDtNode
        Inherits System.Windows.Forms.TreeNode
        Public DtBlock As LDDataBlock
        'Public WithEvents txtSlaveDevID As TextBox

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

        Sub New(ByVal P As TreeNode, ByVal NodText As String, ByVal D As LDDataBlock)
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
            'txtSlaveDevID = New TextBox
            'txtSlaveDevID.Text = Me.DtBlock.Addr
            'CopyCtrl(Fconfig.PalXKLLJ, Fconfig.txtTXMeterAddr, txtSlaveDevID)
        End Sub

        Sub DisDataBlock()
            'Dim i As Integer
            Dim Itm As ListViewItem
            ''Fconfig.PalXKLLJ.BringToFront()
            'Fconfig.Tab1.SelectedIndex = 3
            Fconfig.fdata.Text = "数据块配置-" & Me.Parent.Text & "-" & Me.Text
            Fconfig.txtSvrAddrLength.Text = Me.DtBlock.SvrAddrLength
            Fconfig.txtSvrMBstartAD.Text = Me.DtBlock.SvrMBADStart
            Fconfig.lstRegToMB.Items.Clear()
            Fconfig.Tab.SelectedIndex = 5
            For Each Key As String In Me.DtBlock.RegToMBcol.Keys
                Itm = Fconfig.lstRegToMB.Items.Add("Itm1")
                Itm.SubItems.Add("Modbus 地址")
                Itm.Text = Key
                Itm.SubItems(1).Text = Me.DtBlock.RegToMBcol(Key)
                Itm.EnsureVisible()
            Next
            'Fconfig .lstRegToMB .
        End Sub

        Public Property BlockName() As String
            Get
                BlockName = Me.Text
            End Get
            Set(ByVal value As String)
            End Set
        End Property

        'Public Property MeterAddr() As Integer
        '    Get
        '        MeterAddr = CInt(Me.txtSlaveDevID.Text)
        '    End Get
        '    Set(ByVal value As Integer)

        '    End Set
        'End Property

        Public Property Enable() As Boolean
            Get
                Enable = Me.Checked
            End Get
            Set(ByVal value As Boolean)
            End Set
        End Property
    End Class
End Namespace