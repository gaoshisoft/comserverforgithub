Option Strict Off
Option Explicit On
Friend Class Mytimer
	'UPGRADE_NOTE: Timer was upgraded to Timer_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Event Timer()

    Sub Tell()
        RaiseEvent Timer()

    End Sub
End Class