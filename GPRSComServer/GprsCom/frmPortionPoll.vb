Option Strict Off
Option Explicit On
Namespace GprsCom
    'Imports Microsoft.VisualBasic.PowerPacks
    Friend Class FrmPortionPoll
        Inherits System.Windows.Forms.Form

        Private Sub Command1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
            Handles Command1.Click
            'MainProg.MdIfmain.StopPortionPoll()
            Me.Close()
        End Sub
    End Class
End Namespace