Public Class cFilter
    Dim v(4) As Integer
    Dim No As Integer

    Function Filter(ByVal NewData As Integer, ByRef IfDo As Boolean) As Integer
        Dim i As Integer

        Dim sum As Integer
        Static finalvalue As Integer
        If IfDo Then
            v(No) = NewData
            No = No + 1
            If No >= 5 Then
                Call QuickSort(v)

                For i = 2 To 2
                    sum = sum + v(i)
                Next i
                finalvalue = sum/1
                No = 0
                sum = 0
            End If
            Filter = finalvalue
        Else
            Filter = NewData
        End If
    End Function


    Private Sub QuickSort(ByRef ary As Object)


        Call QSort(ary, 0, UBound(ary))
    End Sub

    Private Sub QSort(ByRef ary As Object, ByVal lLow As Integer, ByVal lHigh As Integer)


        Dim lPivot As Integer

        If lLow < lHigh Then
            lPivot = Part(ary, lLow, lHigh)
            Call QSort(ary, lLow, lPivot - 1)
            Call QSort(ary, lPivot + 1, lHigh)
        End If
    End Sub

    Private Function Part(ByRef ary As Object, ByVal lLow As Integer, ByVal lHigh As Integer) As Integer
        Dim vPivot As Object

        vPivot = ary(lLow)
        Do While lLow < lHigh
            Do While lLow < lHigh And ary(lHigh) >= vPivot
                lHigh = lHigh - 1
            Loop
            If lLow = lHigh Then Exit Do
            ary(lLow) = ary(lHigh)
            Do While lLow < lHigh And ary(lLow) <= vPivot
                lLow = lLow + 1
            Loop
            If lLow = lHigh Then Exit Do
            ary(lHigh) = ary(lLow)
        Loop
        ary(lLow) = vPivot
        Part = lLow
    End Function


    Private Sub Sort(ByRef ary As Object)
        Dim i As Integer
        Dim j As Integer
        Dim vTmp As Object

        For i = 0 To UBound(ary) - 1
            For j = i + 1 To UBound(ary)
                If ary(i) > ary(j) Then
                    vTmp = ary(i)
                    ary(i) = ary(j)
                    ary(j) = vTmp
                End If
            Next
        Next
    End Sub


    Public Sub New()
        No = 0
    End Sub
End Class
