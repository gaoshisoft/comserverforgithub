Imports GPRSComServer.GprsCom

Namespace GprsCom.DtNode
    Class RtuNode
        Inherits System.Windows.Forms.TreeNode
        Public Rtu As GPRSRTU
        Public WithEvents TxtPhoneNumber As TextBox '这种思路的好处是把代码分解到各个Node中，分解了fconfig的负担，从而更加紧凑，如果很占内存也可以用引用的方法，把控件链接到fconfig的相应控件上
        Public WithEvents TxtMainIP As TextBox      '而且，"控件引用"这种方法在VB6中也可用
        Public WithEvents TxtMainPort As TextBox
        
        Public WithEvents CboCycle As ComboBox

        Public WithEvents TxtCodeName As TextBox
        Public WithEvents TxtSvrDevId As TextBox
        Public WithEvents TxtSvrBaseStartAd As TextBox
        Public WithEvents TxtSvrAddrNum As TextBox
        Public WithEvents RadioGprs As RadioButton
        Public WithEvents RadioTcp As RadioButton
        Public WithEvents RadioSComm As RadioButton
        Public WithEvents Cbocommname As ComboBox
        Public WithEvents CboCommrate As ComboBox
        Public WithEvents Cbodatabits As ComboBox
        Public WithEvents Cboparity As ComboBox
        Public WithEvents CboStopBits As ComboBox

        Dim panel As Panel

       
        Sub DisRtu()

            'HandleCommInfo(Rtu.CommInfo)
            Fconfig.frtu.Text = "RTU配置-" & Me.Text
            Me.TxtPhoneNumber.BringToFront()
            Me.TxtMainIP.BringToFront()
            Me.TxtMainPort.BringToFront()
           
            Me.panel.BringToFront()
            Me.RadioGprs.BringToFront()
            Me.RadioTcp.BringToFront()

            Me.TxtCodeName.BringToFront()
            TxtCodeName.Text = (Me.Index + 1).ToString
            TxtSvrBaseStartAd.Text = Me.Rtu.BaseAD
            Me.CboCycle.BringToFront()
            Me.TxtSvrDevId.BringToFront()
            Me.TxtSvrBaseStartAd.BringToFront()
            Me.TxtSvrAddrNum.BringToFront()
            Me.RadioGprs.BringToFront()
            Me.CboCommrate.BringToFront()
            Me.Cbocommname.BringToFront()
            Me.CboStopBits.BringToFront()
            Me.Cboparity.BringToFront()
            Me.RadioSComm.BringToFront()
            Me.Cbodatabits.BringToFront()

            Fconfig.lblRtudisc.Text = "注意：若要配置成GPRS RTU请在 通信标识 中配置正确的11位手机号，若要配成Modbus TCP RTU请在 通信标识 栏配置上正确的 IP标识)"
        End Sub
        Sub New(ByVal p As TreeNode, ByVal nodText As String, ByVal TRtu As GPRSRTU)
            MyBase.New()
            Me.Rtu = TRtu

            TxtPhoneNumber = New TextBox
            TxtMainIP = New TextBox With {.Text = "192.168.1.2"}
            TxtMainPort = New TextBox With {.Text = "502"}
           
            CboCycle = New ComboBox

            TxtCodeName = New TextBox
            TxtSvrDevId = New TextBox
            TxtSvrBaseStartAd = New TextBox
            TxtSvrAddrNum = New TextBox
            RadioGprs = New RadioButton With {.Text = "SERVER"}
            RadioTcp = New RadioButton With {.Text = "TCP"}
            RadioSComm = New RadioButton With {.Text = "串口"}

            Cbocommname = New ComboBox
            CboCommrate = New ComboBox
            Cbodatabits = New ComboBox
            Cboparity = New ComboBox
            CboStopBits = New ComboBox
            panel = New Panel
            P.Nodes.Add(Me)
            Me.Checked = True
            Me.EnsureVisible()
            Dim K As String
            K = Me.GetDistinctNodeText(nodText)
            MyBase.Text = K
            MyBase.Name = K


            For i As Int16 = 0 To 5
                Me.CboCycle.Items.Add(i)
            Next



            Dim spname() As String = System.IO.Ports.SerialPort.GetPortNames
            For i As Int16 = 0 To spname.GetUpperBound(0)
                Me.Cbocommname.Items.Add(spname(i))
            Next
            Me.CboCommrate.Items.Add("2400")
            Me.CboCommrate.Items.Add("4800")
            Me.CboCommrate.Items.Add("9600")
            Me.CboCommrate.Items.Add("19200")
            Me.Cbodatabits.Items.Add(8)
            Me.Cbodatabits.Items.Add(7)
            Me.CboStopBits.Items.Add(1)
            Me.CboStopBits.Items.Add(2)
            Me.Cboparity.Items.Add("n")
            Me.Cboparity.Items.Add("e")
            Me.Cboparity.Items.Add("o")

            CopyCtrl(Fconfig._Txtphonenumber_0, Me.TxtPhoneNumber, Fconfig.GroupBoxcomm)
            CopyCtrl(Fconfig.txtMainIP, Me.TxtMainIP, Fconfig.GroupBoxcomm)
            CopyCtrl(Fconfig.txtMainPort, Me.TxtMainPort, Fconfig.GroupBoxcomm)
        
            CopyCtrl(Fconfig.Panelradiobutton, Me.panel, Fconfig.GroupBoxcomm)
          
            CopyCtrl(Fconfig.radioGprs, Me.RadioGprs, Me.panel)
            CopyCtrl(Fconfig.radioTcp, Me.RadioTcp, Me.panel)
            CopyCtrl(Fconfig.RadioSComm, Me.RadioSComm, Me.panel)

            TxtCodeName.Text = Rtu.CodeName

            CboCycle.Text = Rtu.polltime
            CopyCtrl(Fconfig._cboPolltime_0, Me.CboCycle, Fconfig.frtu)
            CopyCtrl(Fconfig._txtCodeName_0, Me.TxtCodeName, Fconfig.frtu)

            TxtSvrDevId.Text = Rtu.DeviceAD

            CopyCtrl(Fconfig._txtDeviceAD_0, Me.TxtSvrDevId, Fconfig.GroupBoxMBsvr)

            Me.TxtSvrBaseStartAd.Text = Rtu.BaseAD
            CopyCtrl(Fconfig._txtBaseAd_0, Me.TxtSvrBaseStartAd, Fconfig.GroupBoxMBsvr)

            TxtSvrAddrNum.Text = Rtu.MBadressQuantity
            CopyCtrl(Fconfig._txtMbadqty_0, Me.TxtSvrAddrNum, Fconfig.GroupBoxMBsvr)

            CopyCtrl(Fconfig.cboComName, Me.Cbocommname, Fconfig.GroupBoxcomm)
            CopyCtrl(Fconfig.cbodatabits, Me.Cbodatabits, Fconfig.GroupBoxcomm)
            CopyCtrl(Fconfig.cboparity, Me.Cboparity, Fconfig.GroupBoxcomm)
            CopyCtrl(Fconfig.cborate, Me.CboCommrate, Fconfig.GroupBoxcomm)
            CopyCtrl(Fconfig.cboparity, Me.Cboparity, Fconfig.GroupBoxcomm)
            CopyCtrl(Fconfig.cbostopbits, Me.CboStopBits, Fconfig.GroupBoxcomm)
            HandleCommInfo(Rtu.CommInfo)

        End Sub
        Private Sub HandleCommInfo(ByVal comminfo As String)

            Dim ipm As String
            Dim prtm As String
            Dim spname As String
            Dim sprate As String
            Dim spparity As String
            Dim spdatabits As String
            Dim spstopbits As String

            'Dim ifGprs As Boolean
            'commInfo=TCP-192.168.1.5:502
            '或 SComm-COM1:9600,8,n,1
            If comminfo.Contains("-") Then
                'ifGprs = False
                If comminfo.Contains("SComm") Then
                    RadioSComm.Checked = True
                    spname = comminfo.Split("-")(1).Split(":")(0)
                    sprate = comminfo.Split("-")(1).Split(":")(1).Split(",")(0)
                    spdatabits = comminfo.Split("-")(1).Split(":")(1).Split(",")(1)
                    spparity = comminfo.Split("-")(1).Split(":")(1).Split(",")(2)
                    spstopbits = comminfo.Split("-")(1).Split(":")(1).Split(",")(3)
                    Me.Cbocommname.Text = spname
                    Me.CboCommrate.Text = sprate
                    Me.Cboparity.Text = spparity
                    Me.Cbodatabits.Text = spdatabits
                    Me.CboStopBits.Text = spstopbits
                ElseIf comminfo.Contains("TCP") Then

                    RadioTcp.Checked = True
                    ipm = comminfo.Split("-")(1).Split(":")(0)
                    prtm = comminfo.Split("-")(1).Split(":")(1)
                    Me.TxtMainIP.Text = ipm
                    Me.TxtMainPort.Text = prtm
                End If
            Else
                
                RadioGprs.Checked = True
                Me.TxtPhoneNumber.Text = comminfo
            End If
            Dim e As EventArgs
            If RadioGprs.Checked = True Then
                radioGprs_Click(Me, e)
              
            ElseIf RadioTcp.Checked = True Then
                radioTcp_Click(Me, e)
               
            ElseIf RadioSComm.Checked = True Then
                RadioSComm_Click(Me, e)

            End If



        End Sub
        Function GetDistinctNodeText(ByVal T As String) As String
            'Dim T As String
            Dim i As Integer
            Dim t1 As String
            t1 = T
            Dim t2 As String
            t2 = t1

            'T = "NewRtu"
            Do Until Not Me.Parent.Nodes.ContainsKey(t2)

                i += 1
                t2 = t1 & CStr(i)


            Loop
            GetDistinctNodeText = t2
        End Function
        Sub CopyCtrl(ByVal obj1 As Control, ByVal obj2 As Control, ByRef iContainer As GroupBox)
            iContainer.Controls.Add(obj2)
            obj2.Left = obj1.Left
            obj2.Top = obj1.Top
            obj2.Width = obj1.Width
            obj2.Height = obj1.Height
        End Sub
        Sub CopyCtrl(ByVal obj1 As Control, ByVal obj2 As Control, ByRef iContainer As Panel)
            iContainer.Controls.Add(obj2)
            obj2.Left = obj1.Left
            obj2.Top = obj1.Top
            obj2.Width = obj1.Width
            obj2.Height = obj1.Height
        End Sub


        Private Sub txtPhoneNumber_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtPhoneNumber.Validating
            Dim cancel As Boolean = e.Cancel
            If TxtPhoneNumber.Text.Length = 12 Then

            Else
                If Not Isnumber(TxtPhoneNumber.Text) Then
                    MsgBox("若为GPRS通信，请输入11位手机号", MsgBoxStyle.OkOnly)
                    cancel = True
                End If
                If Len(TxtPhoneNumber.Text) <> 11 Then
                    MsgBox("请输入11位数字手机号", MsgBoxStyle.OkOnly)
                    cancel = True
                End If
                e.Cancel = cancel
            End If
        End Sub

        Private Sub txtSvrDevID_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtSvrDevId.Validating
            Dim cancel As Boolean = e.Cancel
            'Dim Index As Short = txtDeviceAD.GetIndex(eventSender)
            If Not IsNumeric(TxtSvrDevId.Text) Or Val(TxtSvrDevId.Text) > 256 Then
                MsgBox("请输入256以内数字")
                cancel = True
            End If
            e.Cancel = cancel
            Me.Rtu.DeviceAD = Val(TxtSvrDevId.Text)
            Fconfig.MyRtUs.CalcuRTUAddr()
            TxtSvrBaseStartAd.Text = Me.Rtu.BaseAD
        End Sub

        Private Sub txtSvrAddrNum_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtSvrAddrNum.Validating

            If Not IsNumeric(TxtSvrAddrNum.Text) Then
                MsgBox("请输入数字", MsgBoxStyle.OkOnly)
                e.Cancel = True
            End If
            Me.Rtu.MBadressQuantity = Val(TxtSvrAddrNum.Text)
            Fconfig.MyRtUs.CalcuRTUAddr()

        End Sub



        Private Sub radioGprs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioGprs.Click
            If RadioGprs.Checked = True Then
                TxtPhoneNumber.Enabled = True
                TxtMainIP.Enabled = False
                TxtMainPort.Enabled = False
                CboCommrate.Enabled = False
                Cbocommname.Enabled = False
                Cbodatabits.Enabled = False
                CboStopBits.Enabled = False
                Cboparity.Enabled = False

            End If
        End Sub

        Private Sub radioTcp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioTcp.Click
            If RadioTcp.Checked = True Then
                TxtPhoneNumber.Enabled = False
                TxtMainIP.Enabled = True
                TxtMainPort.Enabled = True
                CboCommrate.Enabled = False
                Cbocommname.Enabled = False
                Cbodatabits.Enabled = False
                CboStopBits.Enabled = False
                Cboparity.Enabled = False
            End If
            TxtMainIP.Focus()
        End Sub

        

        Private Sub txtMainIP_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtMainIP.Validating
           
            If TxtMainIP.Text = "" Then
                MsgBox("请输入正确IP", MsgBoxStyle.OkOnly)
                e.Cancel = True
                Exit Sub
            End If
            Dim t As String
            t = TxtMainIP.Text
            Dim ip() As String
            ip = t.Split(":")(0).Split(".")
            If ip.GetUpperBound(0) <> 3 Then
                MsgBox("请输入正确IP", MsgBoxStyle.OkOnly)
                e.Cancel = True
                Exit Sub
            Else
                For i As Int16 = 0 To 3
                    If Not Isnumber(ip(i)) Then
                        MsgBox("请输入正确IP", MsgBoxStyle.OkOnly)
                        e.Cancel = True
                        Exit Sub
                    End If
                Next
            End If

            TxtMainPort.Focus()
        End Sub

        Private Sub txtMainPort_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtMainPort.Validating
            If TxtMainPort.Text = "" Then
                MsgBox("请输入正确端口号", MsgBoxStyle.OkOnly)
                e.Cancel = True
            End If
            If Not Isnumber(TxtMainPort.Text) Then
                MsgBox("请输入正确端口号", MsgBoxStyle.OkOnly)
                e.Cancel = True
            End If
        End Sub

        

        
        Private Sub RadioSComm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles RadioSComm.Click
            If RadioSComm.Checked = True Then
                TxtPhoneNumber.Enabled = False
                TxtMainIP.Enabled = False
                TxtMainPort.Enabled = False
                CboCommrate.Enabled = True
                Cbocommname.Enabled = True
                Cbodatabits.Enabled = True
                CboStopBits.Enabled = True
                Cboparity.Enabled = True
            End If
        End Sub
        Function GetComminfoFromNode() As String
            If Me.RadioGprs.Checked Then
                GetComminfoFromNode = Me.TxtPhoneNumber.Text

            ElseIf Me.RadioTcp.Checked Then
                GetComminfoFromNode = "TCP-" & Me.TxtMainIP.Text & ":" & Me.TxtMainPort.Text
                'If R.chkRedudent.Checked Then
                '    GetComminfoFromNode = R.txtMainIP.Text & ":" & R.txtMainPort.Text & "(" & R.txtRedudentIP.Text & ":" & R.txtRedudentPort.Text & ")"
                'End If
            ElseIf Me.RadioSComm.Checked Then
                GetComminfoFromNode = "SComm-" & Me.Cbocommname.Text & ":" & Me.CboCommrate.Text & "," & Me.Cbodatabits.Text & "," & Me.Cboparity.Text & "," & Me.CboStopBits.Text
            End If
        End Function
    End Class
End Namespace