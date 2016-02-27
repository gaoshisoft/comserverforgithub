Option Strict Off
Option Explicit On

Imports GPRSComServer.GprsCom

Namespace MainProg

    Friend Class FrmByteqty
        Inherits System.Windows.Forms.Form

        Private Sub Command1_Click(ByVal EventSender As System.Object, ByVal EventArgs As System.EventArgs) _
            Handles Command1.Click
            Me.Hide()
        End Sub

        Private Sub Command2_Click(ByVal EventSender As System.Object, ByVal EventArgs As System.EventArgs) _
            Handles Command2.Click
            Dim I As Integer
            Dim Totalbytes As Double
            Dim Itemx As System.Windows.Forms.ListViewItem
            For I = 1 To RTUs.Count
                Itemx = ListView1.Items.Item(I - 1)
                Itemx.Text = RTUs(I).RtuName

                Itemx.SubItems(1).Text = Str(RTUs(I).SendoutByteQty)


                Itemx.SubItems(2).Text = Str(RTUs(I).ReceiveByteQty)

                Itemx.SubItems(3).Text = Str(RTUs(I).SendoutByteQty + RTUs(I).ReceiveByteQty)

                Totalbytes = Totalbytes + RTUs(I).SendoutByteQty + RTUs(I).ReceiveByteQty
            Next I
            txtTotalbytes.Text = CStr(Totalbytes)
        End Sub


        Private Sub Command3_Click(ByVal EventSender As System.Object, ByVal EventArgs As System.EventArgs) _
            Handles Command3.Click
            'frmHisdatashow.Show()
        End Sub

        Private Sub frmByteqty_Activated(ByVal EventSender As System.Object, ByVal EventArgs As System.EventArgs) _
            Handles MyBase.Activated
            Dim Totalbytes As Double
            Dim RTU As GPRSRTU
            Dim Itemx As System.Windows.Forms.ListViewItem
            ListView1.Items.Clear()
            For Each RTU In RTUs
                Itemx = ListView1.Items.Add("itm1")
                Itemx.Text = RTU.RtuName
                If Itemx.SubItems.Count > 1 Then
                    Itemx.SubItems(1).Text = Str(RTU.SendoutByteQty)
                Else
                    Itemx.SubItems.Insert(1, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, Str(RTU.SendoutByteQty)))


                End If
                If Itemx.SubItems.Count > 2 Then
                    Itemx.SubItems(2).Text = Str(RTU.ReceiveByteQty)
                Else
                    Itemx.SubItems.Insert(2, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, Str(RTU.ReceiveByteQty)))
                End If


                If Itemx.SubItems.Count > 3 Then
                    Itemx.SubItems(3).Text = Str(RTU.SendoutByteQty + RTU.ReceiveByteQty)
                Else
                    Itemx.SubItems.Insert(3, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, Str(RTU.SendoutByteQty + RTU.ReceiveByteQty)))




                End If

                Totalbytes = Totalbytes + RTU.SendoutByteQty + RTU.ReceiveByteQty
            Next RTU
            txtTotalbytes.Text = CStr(Totalbytes)
        End Sub

        Private Sub frmByteqty_Paint(ByVal EventSender As System.Object,
                                     ByVal EventArgs As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
            '    ListView1.Move 0, 0, Me.Width, Me.Height - Command1.Height * 2
        End Sub
    End Class
End Namespace