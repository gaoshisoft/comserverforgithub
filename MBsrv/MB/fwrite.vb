Option Strict Off
Option Explicit On

Imports System.Windows.Forms
Imports VB = Microsoft.VisualBasic

Friend Class fwrite


    Inherits System.Windows.Forms.Form
    Public MBDevices As Devices
    Dim mbs As MBserver

    Private Sub Combo2_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Combo2.SelectedIndexChanged
        If Trim(Combo2.Text) = "二进制" And InStr(1, "3,4", VB.Left(txtstartad.Text, 1)) <> 0 Then
            FrameB.Visible = True
            FrameV.Visible = True
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
        Dim i As Int16

        Dim value As Object
        'Dim chk() As CheckBox
        If Combo2.Text = "二进制" Then


            For Each c As Control In frameB.Controls
                i = c.Name.Replace("CheckBox", "") - 1
                Dim ch As CheckBox = CType(c, CheckBox)
                value = value + IIf(ch.CheckState, 2 ^ i, 0)
            Next


        Else

            value = Val(Text2.Text)
        End If
        '有符号整型 = 0
        '二进制 = 1 '实际是字符串表示的二进制
        '浮点数 = 2
        '浮点数高低字交换 = 3
        '双精度 = 4
        '双精度高低字交换 = 5
        '长整型 = 6
        '长整型高低字交换 = 7
        '无符号整型 = 8
        '布尔 = 9
        Dim dp As Int16
        Select Case Combo2.Text
            Case "二进制"
                dp = 1
            Case "整型"
                dp = 8
            Case "浮点数"
                dp = 2
            Case "浮点数高低字交换"
                dp = 3
            Case "长整型"
                dp = 6
        End Select

        MBDevices.GetDevicefromAd(CInt(Combo1.Text)).WriteModbusbyAD(Trim(txtstartad.Text), value, dp)
        'frmModbusserver.RefreshDataDisplay
        MsgBox("更新完成")
    End Sub

    Private Sub Command2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command2.Click
        Me.Close()
    End Sub



    Private Sub fwrite_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim i As Object
        'Text1.Text = Fmain.Listview2CurrentPosition

        'mbs = MBserver._mbs
        If MBDevices.Count = 0 Then
            Exit Sub
        End If
        For i = 1 To MBdevices.Count
            Combo1.Items.Add(CStr(MBdevices(i).deviceAddr))
        Next i
        Combo1.SelectedIndex = 1
        Combo2.Items.Add("二进制")
        Combo2.Items.Add("整型")

        Combo2.Items.Add("浮点数")
        Combo2.Items.Add("浮点数高低字交换")
        Combo2.Items.Add("长整型")

        Combo2.SelectedIndex = 0

    End Sub

    Private Sub Text2_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles Text2.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Val(Text2.Text) > 65535 And Combo2.Text = "整型" Then
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