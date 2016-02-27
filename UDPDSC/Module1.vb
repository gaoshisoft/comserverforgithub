Option Strict Off
Option Explicit On
Module Module1
	
	'UPGRADE_ISSUE: Declaring a parameter 'As Any' is not supported. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"'
	'UPGRADE_ISSUE: Declaring a parameter 'As Any' is not supported. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"'
    'Declare Sub CopyMemory Lib "KERNEL32"  Alias "RtlMoveMemory"(ByRef hpvDest As Any, ByRef hpvSource As Any, ByVal cbCopy As Integer)
	
	
	Public OnlineDtus As ConlineDtus
	Public Function HextoStr(ByRef src() As Byte, ByRef Srclen As Integer, ByRef Start As Integer) As String
		Dim i As Short
		Dim st As String
		Dim temp As Short
		Dim j As Integer
		j = Srclen - 1
		For i = Start To j '看src这个数组是从0开始还是从1开始,宏电的是从1开始
			temp = src(i) \ 16
			If temp > 9 Then
				temp = temp + 55
			Else
				temp = temp + 48
			End If
			st = st & Chr(temp)
			
			temp = src(i) Mod 16
			If temp > 9 Then
				temp = temp + 55
			Else
				temp = temp + 48
			End If
			st = st & Chr(temp)
			
			'st = st & " "
		Next 
		HextoStr = st
	End Function
End Module