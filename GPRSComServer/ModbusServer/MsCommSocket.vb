Option Strict Off
Option Explicit On
Friend Class MsCommSocket
	Inherits System.Windows.Forms.Form
    Public Tmr As New Mytimer
    Dim WithEvents Scomm As mscommWsk
    Dim WithEvents Timer1 As Timer
    'UPGRADE_WARNING: Event Combo1.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
    Private Sub Combo1_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Combo1.SelectedIndexChanged
        txtmbrv.Text = ""
        txtmbsend.Text = ""
        Scomm = Mbs.serialComms((Combo1.Text))
    End Sub

    Private Sub Command1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command1.Click
        Me.Hide()
    End Sub

    'UPGRADE_WARNING: Form event MsCommSocket.Activate has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
    Private Sub MsCommSocket_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        Dim i As Integer
        Combo1.Items.Clear()
        For i = 1 To Mbs.serialComms.Count
            Combo1.Items.Add(Mbs.serialComms.Item(i).Name)
        Next i
        If Combo1.Items.Count > 0 Then
            Combo1.SelectedIndex = 0
            Scomm = Mbs.serialComms((Combo1.Text))
            Label2.Text = "PortNo:" & Scomm.PortNo & "  commSetting:" & Scomm.CommSetting
        End If
    End Sub

    Private Sub Scomm_DataArrival(ByRef mvalue As Object, ByRef bytesTotal As Integer) Handles Scomm.DataArrival
        Dim D() As Byte
        Dim i As Short
        i = bytesTotal
        'UPGRADE_WARNING: Couldn't resolve default property of object mvalue. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        D = mvalue
        txtmbrv.Text = HextoStr(D, i, 0)
    End Sub

    Private Sub Scomm_DataSend(ByRef mvalue As Object) Handles Scomm.DataSend
        Dim D() As Byte
        'UPGRADE_WARNING: Couldn't resolve default property of object mvalue. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        D = mvalue
        txtmbsend.Text = HextoStr(D, UBound(D) + 1, 0)
    End Sub

   


    Private Sub Timer1_Tick1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Tmr.Tell()
    End Sub

    Private Sub MsCommSocket_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Timer1 = New Timer
        Timer1.Interval = 1000
        Timer1.Start()
    End Sub
End Class