Imports GPRSComServer.GprsCom.DtNode
Imports GPRSComServer.GprsCom
Imports VB = Microsoft.VisualBasic
Class MRDtNode
    Inherits System.Windows.Forms.TreeNode
    Public DtBlock As GPRSComServer.MRDataBlock
    Public WithEvents txtSlaveDevID As TextBox
    Public WithEvents txtStartAD As TextBox
    Public WithEvents txtLength As TextBox
    Function GetDistinctNodeText(ByVal T As String) As String
        'Dim T As String
        Dim i As Integer
        'T = "NewDataBlock1"
        Dim T1 As String
        T1 = T
        Do Until Not Me.Parent.Nodes.ContainsKey(T1)

            i += 1
            T1 = T & CStr(i)


        Loop
        GetDistinctNodeText = T1
    End Function


    Sub New(ByVal P As TreeNode, ByVal NodText As String, ByVal Dt As MRDataBlock)
        MyBase.New()

        P.Nodes.Add(Me)
        Dim k As String
        k = Me.GetDistinctNodeText(NodText)


        MyBase.Name = k
        MyBase.Text = k
        Me.Checked = True
        Me.EnsureVisible()
        Me.DtBlock = Dt
        Dt.BlockName = k
        txtSlaveDevID = New TextBox
        txtSlaveDevID.Text = Me.DtBlock.Addr
        CopyCtrlposition(Fconfig.tabMR, Fconfig.txtMRDBID, txtSlaveDevID)
        'txtStartAD = New TextBox
        'txtStartAD.Text = Me.DtBlock.SlaveStartAD.ToString
        'CopyCtrl(Fconfig.tabMR, Fconfig.txtMRStart, txtStartAD)

        txtLength = New TextBox
        txtLength.Text = DtBlock.SvrAddrLength
        CopyCtrlposition(Fconfig.tabMR, Fconfig.txtMRDBlength, txtLength)


    End Sub

    Sub DisDataBlock()

        Fconfig.fdata.Text = "数据块配置-" & Me.Parent.Text & "-" & Me.Text
        Fconfig.Tab.SelectedIndex = 4
        'Fconfig.Tab1 .SelectTab (
        txtSlaveDevID.BringToFront()
        txtLength.BringToFront()
        'txtStartAD.BringToFront()
        Fconfig.txtSvrAddrLength.Text = Me.DtBlock.SvrAddrLength.ToString
        Fconfig.txtSvrMBstartAD.Text = Me.DtBlock.SvrMBADStart
        'Fconfig()
    End Sub



    Private Sub txtLength_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtLength.Validating
        Dim Cancel As Boolean = e.Cancel
        'Dim Index As Short = txtLength.GetIndex(eventSender)
        If Not Isnumber(txtLength.Text) Or Val(txtLength.Text) > 512 Then
            MsgBox("请输入小于511的数", MsgBoxStyle.OkOnly)
            Cancel = True
        End If
        Me.DtBlock.SvrAddrLength = Val(txtLength.Text)
        Fconfig.txtSvrAddrLength.Text = Me.DtBlock.SvrAddrLength
        Dim R As RtuNode
        R = Me.Parent
        R.Rtu.DTblocks.CalcuDataBlockaddr()
        e.Cancel = Cancel
    End Sub

    Private Sub txtStartAD_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtStartAD.Validating
        Dim Cancel As Boolean = e.Cancel
        If Not Isnumber(txtStartAD.Text) Then
            MsgBox("请输入数字", MsgBoxStyle.OkOnly)
            Cancel = True
            GoTo EventExitSub
        End If
        If InStr(1, "0,1,3,4", VB.Left(txtStartAD.Text, 1)) = 0 Or Len(txtStartAD.Text) <> 6 Then
            MsgBox("请输入0x,1x,3x或4x的六位数地址！", MsgBoxStyle.OkOnly)
            Cancel = True
            GoTo EventExitSub
        End If

EventExitSub:
        e.Cancel = Cancel

    End Sub
End Class
