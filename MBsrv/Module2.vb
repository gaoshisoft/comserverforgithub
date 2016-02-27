Module Module2
    Public Function HextoStr(ByRef src() As Byte, ByRef Srclen As Short, Optional ByRef start As Integer = 0) As String
        Dim i As Short
        Dim st As String = ""
        Dim temp As Short
        Dim j As Integer
        j = Srclen - 1
        
        For i = start To j '看src这个数组是从0开始还是从1开始,宏电的是从1开始
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
