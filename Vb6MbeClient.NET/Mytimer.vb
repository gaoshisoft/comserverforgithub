Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("Mytimer_NET.Mytimer")> Public Class Mytimer
	'UPGRADE_NOTE: Timer �������� Timer_Renamed�� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"��
	Public Event Timer_Renamed()
	
	Sub Tell()
		RaiseEvent Timer_Renamed()
		
	End Sub
End Class