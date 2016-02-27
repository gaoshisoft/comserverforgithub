Option Strict Off
Option Explicit On

Imports GPRSComServer.GprsCom

Friend Class comquality
    Inherits System.Windows.Forms.Form
    'Dim rtus As GPRSRTUs

    Private Sub Command1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
        Handles Command1.Click
        Me.Hide()
    End Sub

    Private Sub Command2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
        Handles Command2.Click
        Dim i As Integer
        Dim itemx As System.Windows.Forms.ListViewItem
        For i = 1 To RTUs.Count
            itemx = ListView1.Items.Item(i - 1)
            itemx.Text = RTUs(i).RtuName

            itemx.SubItems(1).Text = Str(RTUs(i).ComSuccesstimes)

            itemx.SubItems(2).Text = Str(RTUs(i).ComfaileTimes)

        Next i
    End Sub

    'Private Sub Command3_Click()
    'RTUs.comRecordReset
    'End Sub

    Private Sub comquality_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
        Handles MyBase.Activated
        Dim rtu As IGPRSRTU
        Dim itemx As System.Windows.Forms.ListViewItem
        ListView1.Items.Clear()

        For Each rtu In rtus
            itemx = ListView1.Items.Add("itm1")
            itemx.Text = rtu.RtuName
            If itemx.SubItems.Count > 1 Then
                itemx.SubItems(1).Text = Str(rtu.ComSuccesstimes)
            Else
                itemx.SubItems.Insert(1,
                                      New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing,
                                                                                            Str(rtu.ComSuccesstimes)))
            End If
            If itemx.SubItems.Count > 2 Then
                itemx.SubItems(2).Text = Str(rtu.ComfaileTimes)
            Else
                itemx.SubItems.Insert(2,
                                      New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing,
                                                                                            Str(rtu.ComfaileTimes)))
            End If
        Next rtu
        '    ListView1.Move 0, 0, Me.Width, Me.Height - Command1.Height * 2
    End Sub
End Class