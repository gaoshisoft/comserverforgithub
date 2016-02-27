Option Strict Off
Option Explicit On
Namespace UserFunc
    Friend Class User
        Enum Plevel
            普通用户 = 1
            管理员 = 2
        End Enum

        Public UserName As String
        Public Password As String
        Public Power As Integer
    End Class
End Namespace