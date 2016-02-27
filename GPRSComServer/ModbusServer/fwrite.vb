Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic

Friend Class fwrite


    Inherits System.Windows.Forms.Form
    Dim MBDevices As Devices = Mbs.Devices
    'UPGRADE_WARNING: Event Combo2.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
    Private Sub Combo2_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Combo2.SelectedIndexChanged
        If Trim(Combo2.Text) = "二进制" And InStr(1, "3,4", VB.Left(txtstartad.Text, 1)) <> 0 Then
            FrameB.Visible = True
            FrameV.Visible = False
        Else
            FrameV.Visible = True
            FrameB.Visible = False
        End If
    End Sub

    Private Sub Combo2_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles Combo2.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If VB.Left(Trim(Combo2.Text), 3) = "浮点数" And InStr(1, "3,4", VB.Left(txtstartad.Text, 1)) <> 0 Then
            'If Val(txtstartad.Text) Mod 2 = 0 Then
            'MsgBox "浮点数地址须为奇数"
            'Cancel = True
            'End If
        End If

        eventArgs.Cancel = Cancel
    End Sub

    Private Sub Command1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command1.Click
        Dim i As Object
        Dim value As Object
        If Combo2.Text = "二进制" Then
            For i = 0 To 15
                'UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                'UPGRADE_WARNING: Couldn't resolve default property of object value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                value = value + IIf(Check1(i).CheckState, 2 ^ i, 0)
            Next i
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            value = Val(Text2.Text)
        End If

        MBdevices.GetDevicefromAd(CInt(Combo1.Text)).WriteModbusbyAD(Trim(txtstartad.Text), value, Combo2.SelectedIndex)
        'frmModbusserver.RefreshDataDisplay
    End Sub

    Private Sub Command2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command2.Click
        Me.Close()
    End Sub



    Private Sub fwrite_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim i As Object
        'Text1.Text = Fmain.Listview2CurrentPosition
        If MBdevices.Count = 0 Then
            Exit Sub
        End If
        For i = 1 To MBdevices.Count
            Combo1.Items.Add(CStr(MBdevices(i).deviceAddr))
        Next i
        Combo1.SelectedIndex = 0
        Combo2.Items.Add("整型")
        Combo2.Items.Add("二进制")
        Combo2.Items.Add("浮点数")
        Combo2.Items.Add("浮点数高低字交换")
        Combo2.SelectedIndex = 0
        MBDevices = Mbs.Devices
    End Sub

    Private Sub Text2_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles Text2.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Val(Text2.Text) > 65535 Then
            MsgBox("请输入小于等于65535的数！")
            Cancel = True
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtstartad_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtstartad.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Not IsNumeric(txtstartad.Text) Then
            MsgBox("请输入数字", MsgBoxStyle.OkOnly)
            Cancel = True
            GoTo EventExitSub
        End If
        If InStr(1, "0,1,3,4", VB.Left(txtstartad.Text, 1)) = 0 Or Len(txtstartad.Text) <> 6 Then



            MsgBox("请输入0x,1x,3x或4x的六位数地址！", MsgBoxStyle.OkOnly)
            Cancel = True
            GoTo EventExitSub
        End If
        If Val(VB.Right(txtstartad.Text, Len(txtstartad.Text) - 1)) > MBdevices.GetDevicefromAd(CInt(Combo1.Text)).MBadressQuantity Or Val(VB.Right(txtstartad.Text, Len(txtstartad.Text) - 1)) < 1 Then
            MsgBox("请输入1-" & MBdevices.GetDevicefromAd(CInt(Combo1.Text)).MBadressQuantity & "的地址！")
            Cancel = True
        End If
        If VB.Left(Trim(Combo2.Text), 3) = "浮点数" And InStr(1, "3,4", VB.Left(txtstartad.Text, 1)) <> 0 Then
            'If Val(txtstartad.Text) Mod 2 = 0 Then
            'MsgBox "浮点数地址须为奇数"
            'Cancel = True
            'End If
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class