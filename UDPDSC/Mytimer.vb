Option Strict Off
Option Explicit On
Friend Class Mytimer
    Public Event Timer()

    Sub Tell()
        RaiseEvent Timer()

    End Sub
End Class