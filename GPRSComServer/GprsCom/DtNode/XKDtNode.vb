﻿Imports GPRSComServer.GprsCom

Class XKDtNode
    Inherits System.Windows.Forms.TreeNode
    Public DtBlock As XKDataBlock
    Public WithEvents txtSlaveDevID As TextBox

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

    Sub New(ByVal P As TreeNode, ByVal NodText As String, ByVal D As XKDataBlock)
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
        txtSlaveDevID = New TextBox
        txtSlaveDevID.Text = Me.DtBlock.Addr
        CopyCtrlposition(Fconfig.PalXKLLJ, Fconfig.txtXKAD, txtSlaveDevID)
    End Sub

    Sub DisDataBlock()

        Fconfig.fdata.Text = "数据块配置-" & Me.Parent.Text & "-" & Me.Text
        'Fconfig.PalXKLLJ.BringToFront()
        Fconfig.Tab.SelectedIndex = 3
        Fconfig.txtSvrAddrLength.Text = Me.DtBlock.SvrAddrLength
        Fconfig.txtSvrMBstartAD.Text = Me.DtBlock.SvrMBADStart

        txtSlaveDevID.BringToFront()
        Fconfig.lblXKparaaddr.Text = " 电源状态：" & CStr(400000 + Me.DtBlock.SvrMBADStart + 1) & " 累计流量(浮点)：" &
                                     CStr(400000 + Me.DtBlock.SvrMBADStart + 3) &
                                     " 瞬时流量：" & CStr(400000 + Me.DtBlock.SvrMBADStart + 5) &
                                     " 压力：" & CStr(400000 + Me.DtBlock.SvrMBADStart + 7) &
                                     " 温度：" & CStr(400000 + Me.DtBlock.SvrMBADStart + 9) &
                                     " 工况累计：" & CStr(400000 + Me.DtBlock.SvrMBADStart + 11) &
                                     " 工况瞬时：" & CStr(400000 + Me.DtBlock.SvrMBADStart + 13)
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
