Option Strict Off
Option Explicit On
Friend Class frmModbusserver
	Inherits System.Windows.Forms.Form
	
	Private Colno As Integer
	Private Itemno As Integer
    Public MBDevices As Devices = Mbs.Devices
    Dim WithEvents tmr As Timer

    Private Sub chkStartAd_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStartAd.CheckStateChanged
        RefreshDataDisplay()
    End Sub

	
	Private Sub Combo1_Enter(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Combo1.Enter
		Dim i As Integer
		Combo1.Items.Clear()
		Dim j As Integer
		j = MBS.Devices.Count
		For i = 1 To j
			Combo1.Items.Add(CStr(MBS.Devices(i).deviceAddr))
		Next i
		If Combo1.Items.Count > 0 Then
			Combo1.SelectedIndex = 0
		End If
	End Sub
	
	Private Sub Command1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command1.Click
		Me.Close()
	End Sub
	
	Private Sub Command2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command2.Click
		fwrite.Show()
	End Sub
	

	
	Private Sub frmModbusserver_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        'Text5.Text = MBTCPsocket.Winsock1(0).LocalIP
        Me.MBDevices = Mbs.Devices
        tmr = New Timer
        tmr.Interval = 3000
        tmr.Start()
		InitMBdatadisplay()
        RefreshDataDisplay()
		
	End Sub
	
	
	Private Sub frmModbusserver_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
		Dim Cancel As Boolean = eventArgs.Cancel
		Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
		Cancel = True
		Me.Hide()
		eventArgs.Cancel = Cancel
	End Sub
	
    Private Sub Option1_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        If eventSender.Checked Then
            'Me.MBDevices = Mbs.Devices

            RefreshDataDisplay()

        End If
    End Sub
	

	
    Private Sub Option3_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        If eventSender.Checked Then
            Me.MBDevices = Mbs.Devices

            RefreshDataDisplay()

        End If
    End Sub
	
    Private Sub Option4_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        If eventSender.Checked Then
            Me.MBDevices = Mbs.Devices

            RefreshDataDisplay()
        End If
    End Sub
	
    Private Sub Timer1_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles tmr.Tick
        Dim itmX As System.Windows.Forms.ListViewItem
        Dim colnum As Integer
        Dim Lin As Integer
        Dim i As Integer
        If MBDevices.Count = 0 Then
            Exit Sub
        End If
        Dim D As Device
        D = Mbs.Devices.GetDevicefromAd(CInt(Combo1.Text))
        If D Is Nothing Then
            ListView2.Items.Clear()
        Else
            TxtNum.Text = CStr(D.MBadressQuantity)
            For i = 0 To 3
                If Option2.Checked = True Then
                    ListView2.Columns.Item(i).Width = VB6.TwipsToPixelsX(3000)
                Else
                    ListView2.Columns.Item(i).Width = VB6.TwipsToPixelsX(2000)
                End If
            Next i
            i = ListView2.Columns.Count
            colnum = i
            Lin = Val(TxtNum.Text) \ colnum
            Lin = IIf(Val(TxtNum.Text) Mod colnum = 0, Lin, Lin + 1)
            Dim j As Integer = ListView2.TopItem.Index

            For i = j To 200
                If i > Lin - 1 Then
                    Exit For
                End If
                itmX = GetLineItem(i)

                itmX.Text = GetCellTxt(i, 0, colnum)

                itmX.SubItems(1).Text = GetCellTxt(i, 1, colnum)


                itmX.SubItems(2).Text = GetCellTxt(i, 2, colnum)


                itmX.SubItems(3).Text = GetCellTxt(i, 3, colnum)



            Next i
        End If
    End Sub
	
	Private Sub TxtNum_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtNum.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		
	
		
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	

    Function GetLineItem(ByVal lin As Int16) As ListViewItem
        If ListView2.Items.ContainsKey(lin) Then
            GetLineItem = ListView2.Items(lin)
        Else
            GetLineItem = ListView2.Items.Add("col0")
            GetLineItem.SubItems.Add("col1")
            GetLineItem.SubItems.Add("col2")
            GetLineItem.SubItems.Add("col3")
            GetLineItem.Name = lin
        End If
    End Function
	
	Sub RefreshDataDisplay()
		Dim itmX As System.Windows.Forms.ListViewItem
		Dim colnum As Integer
		Dim Lin As Integer
		Dim i As Integer
		If MBdevices.Count = 0 Then
			Exit Sub
		End If
		Dim D As Device
		D = MBS.Devices.GetDevicefromAd(CInt(Combo1.Text))
		If D Is Nothing Then
			ListView2.Items.Clear()
        Else
            ListView2.Items.Clear()
            TxtNum.Text = CStr(D.MBadressQuantity)
            For i = 0 To 3
                If Option2.Checked = True Then
                    ListView2.Columns.Item(i).Width = VB6.TwipsToPixelsX(3000)
                Else
                    ListView2.Columns.Item(i).Width = VB6.TwipsToPixelsX(2000)
                End If
            Next i
            colnum = ListView2.Columns.Count

            Lin = Val(TxtNum.Text) \ colnum
            Lin = IIf(Val(TxtNum.Text) Mod colnum = 0, Lin, Lin + 1)
            For i = 0 To Lin - 1


                itmX = GetLineItem(i)
                itmX.Text = GetCellTxt(i, 0, colnum)
                itmX.SubItems(1).Text = GetCellTxt(i, 1, colnum)
                itmX.SubItems(2).Text = GetCellTxt(i, 2, colnum)

                itmX.SubItems(3).Text = GetCellTxt(i, 3, colnum)

            Next i
            txtTcpPort.Text = CStr(Mbs.GetServerPort)
        End If
        '---------------

	End Sub
	Sub InitMBdatadisplay()
		Dim i As Integer
		Combo1.Items.Clear()
		Dim j As Integer
		MBdevices = MBS.Devices
		j = MBdevices.Count
		For i = 1 To j
			Combo1.Items.Add(CStr(MBdevices(i).deviceAddr))
		Next i
		If Combo1.Items.Count > 0 Then
			Combo1.SelectedIndex = 0
		End If
		Combo2.Items.Clear()
		Combo2.Items.Add("Coil Status")
		Combo2.Items.Add("Input Status")
		Combo2.Items.Add("Holding Registers")
		Combo2.Items.Add("Input Registers")
        Combo2.SelectedItem = Combo2.Items(2)

		TxtNum.Text = CStr(10)

        AddHandler Combo1.SelectedIndexChanged, AddressOf Combo1_SelectedIndexChanged
        AddHandler Combo2.SelectedIndexChanged, AddressOf Combo2_SelectedIndexChanged
        AddHandler Option1.Click, AddressOf Option1_CheckedChanged
        AddHandler Option2.CheckedChanged, AddressOf Option2_CheckedChanged
        AddHandler Option3.CheckedChanged, AddressOf Option3_CheckedChanged
        AddHandler Option4.CheckedChanged, AddressOf Option4_CheckedChanged
        AddHandler chkStartAd.CheckedChanged, AddressOf chkStartAd_CheckStateChanged


	End Sub
    Function GetCellTxt(ByVal Lin As Integer, ByVal colID As Integer, ByVal colnum As Integer) As String
        Dim D As Device
        Dim MBad As String
        Dim Celltext As String
        Dim CurrentPosition As Integer
        If chkStartAd.CheckState = 1 Then '如果是偶地址起始则modbus 地址比奇地址起始时加1
            CurrentPosition = Lin * colnum + colID + 1 '既是Listview中当前单元格的位置，也是modbus地址的当前位置（不过要加1)
        Else
            CurrentPosition = Lin * colnum + colID
        End If
        Dim Datatype_Renamed As Device.Datatype
        If MBDevices.Count > 0 Then
            D = MBDevices.GetDevicefromAd(CInt(Combo1.Text))
            If CurrentPosition + 1 <= D.MBadressQuantity Then
                Datatype_Renamed = CShort(-Option1.Checked) * 1 + CShort(-Option2.Checked) * 2 + CShort(-Option3.Checked) * 3 + CShort(-Option4.Checked) * 4 - 1
                Select Case Combo2.Text
                    Case "Holding Registers"

                        MBad = Str(400000 + CurrentPosition + 1)
                        Celltext = MBad & ":" & D.ReadModbusbyAD(MBad, Datatype_Renamed)
                        If chkStartAd.CheckState = 0 Then
                            If Datatype_Renamed = Device.Datatype.浮点数 Or Datatype_Renamed = Device.Datatype.浮点数高低字交换 Then
                                If Val(MBad) Mod 2 <> 0 Then '是否奇地址
                                    Celltext = MBad & ":" & D.ReadModbusbyAD(MBad, Datatype_Renamed)
                                Else
                                    Celltext = MBad & ":"
                                End If
                            End If
                        ElseIf chkStartAd.CheckState = 1 Then

                            If Datatype_Renamed = Device.Datatype.浮点数 Or Datatype_Renamed = Device.Datatype.浮点数高低字交换 Then
                                If Val(MBad) Mod 2 <> 0 Then '是否奇地址
                                    Celltext = MBad & ":"
                                Else
                                    Celltext = MBad & ":" & D.ReadModbusbyAD(MBad, Datatype_Renamed)
                                End If
                            End If
                        End If
                    Case "Input Registers"
                        MBad = Str(300000 + CurrentPosition + 1)

                        Celltext = MBad & ":" & D.ReadModbusbyAD(MBad, Datatype_Renamed)
                        If chkStartAd.CheckState = 0 Then
                            If Datatype_Renamed = Device.Datatype.浮点数 Or Datatype_Renamed = Device.Datatype.浮点数高低字交换 Then
                                If Val(MBad) Mod 2 <> 0 Then '是否奇地址
                                    'UPGRADE_WARNING: Couldn't resolve default property of object D.ReadModbusbyAD(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                                    Celltext = MBad & ":" & D.ReadModbusbyAD(MBad, Datatype_Renamed)
                                Else
                                    Celltext = MBad & ":"
                                End If
                            ElseIf chkStartAd.CheckState = 1 Then

                            ElseIf Datatype_Renamed = Device.Datatype.浮点数 Or Datatype_Renamed = Device.Datatype.浮点数高低字交换 Then
                                If Val(MBad) Mod 2 <> 0 Then '是否奇地址
                                    Celltext = MBad & ":"
                                Else
                                    Celltext = MBad & ":" & D.ReadModbusbyAD(MBad, Datatype_Renamed)
                                End If
                            End If
                        End If
                    Case "Coil Status"

                        MBad = VB6.Format(0 + CurrentPosition + 1, "000000")

                        Celltext = MBad & ":" & D.ReadModbusbyAD(MBad, Datatype_Renamed)
                    Case "Input Status"

                        MBad = Str(100000 + CurrentPosition + 1)

                        Celltext = MBad & ":" & D.ReadModbusbyAD(MBad, Datatype_Renamed)
                End Select
            End If
        End If
        GetCellTxt = Celltext
        Label1.Text = D.devicedescription
    End Function
 





 

   

    Private Sub Combo2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RefreshDataDisplay()
    End Sub

    Private Sub Combo1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RefreshDataDisplay()
    End Sub

    Private Sub Option2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RefreshDataDisplay()
    End Sub

    
End Class