Option Strict Off
Option Explicit On
Friend Class iffirst
    Dim b As Boolean

    Function FirstTime() As Boolean 'ȱʡ����

        If b = False Then
            FirstTime = True
            b = True
            Exit Function
        End If
        FirstTime = False
    End Function

    Sub resetToOrigin()

        b = False
    End Sub
End Class