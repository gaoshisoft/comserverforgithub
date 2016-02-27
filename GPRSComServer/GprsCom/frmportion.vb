Option Strict Off
Option Explicit On
'Imports MBRTUsvr = Device
Namespace GprsCom

    Friend Class frmportion
        Inherits System.Windows.Forms.Form
        Dim _mRtus As GPRSRTUs

        Private Sub Command1_Click(ByVal EventSender As System.Object, ByVal EventArgs As System.EventArgs) _
            Handles Command1.Click
            RTUs.tmrPoll.Enabled = True
        End Sub

        Private Sub Command2_Click(ByVal EventSender As System.Object, ByVal EventArgs As System.EventArgs) _
            Handles Command2.Click
            Stopexpress()
            RTUs.StopPoll()
        End Sub

        Private Sub Command3_Click(ByVal EventSender As System.Object, ByVal EventArgs As System.EventArgs) _
            Handles Command3.Click
            Runexpress()
            RTUs.StartPoll()
        End Sub


        Private Sub Closeme(ByVal EventSender As System.Object, ByVal EventArgs As System.EventArgs) _
            Handles cmdClose.Click
            'MDIfmain.StopPortionPoll
            Me.Close()
        End Sub



        Private Sub frmportion_Load(ByVal EventSender As System.Object, ByVal EventArgs As System.EventArgs) _
            Handles MyBase.Load
            Dim i As Integer
            _mRtus = RTUs
            For i = 1 To _mRtus.Count
                List1.Items.Add(_mRtus(i).RtuName)
            Next i
            Runexpress()

        End Sub

        Private Sub frmportion_FormClosed(ByVal EventSender As System.Object,
                                          ByVal EventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
            RTUs.StartPoll()
        End Sub

        Private Sub List1_DoubleClick(ByVal EventSender As System.Object, ByVal EventArgs As System.EventArgs) _
            Handles List1.DoubleClick
            _mRtus(List1.SelectedIndex + 1).PollEnable = True
        End Sub

        Sub Runexpress()
            Command3.Enabled = False
            List1.Enabled = False
            Command1.Enabled = False
            Command2.Enabled = True
        End Sub

        Sub Stopexpress()
            Command3.Enabled = True
            List1.Enabled = True
            Command1.Enabled = True
            Command2.Enabled = False
        End Sub

        Private Sub List1_SelectedIndexChanged(ByVal Sender As System.Object, ByVal e As System.EventArgs) _
            Handles List1.SelectedIndexChanged
        End Sub


    End Class
End Namespace