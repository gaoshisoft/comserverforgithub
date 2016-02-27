Option Strict Off
Option Explicit On
Friend Class Mytimer
    Public Event TmrTick()

    Sub Tell()
        RaiseEvent TmrTick()
    End Sub
End Class