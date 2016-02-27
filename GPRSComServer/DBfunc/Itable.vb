Option Strict Off
Option Explicit On
Namespace DBfunc

    Interface _Itable
        Property tablename As String
        Property ItemCol As Collection
        Property TableType As DBconnect.Tbtype
    End Interface

    Friend Class Itable
        Implements _Itable
        Dim tablenamestr As String

        Public Property tablename() As String Implements _Itable.tablename
            Get
                tablename = tablenamestr
            End Get
            Set(ByVal Value As String)
                tablenamestr = Value
            End Set
        End Property

        Dim ItemCol_MemberVariable As Collection

        Public Property ItemCol() As Collection Implements _Itable.ItemCol
            Get
                ItemCol = ItemCol_MemberVariable
            End Get
            Set(ByVal Value As Collection)
                ItemCol_MemberVariable = Value
            End Set
        End Property

        Dim TableType_MemberVariable As String

        Public Property TableType() As DBconnect.Tbtype Implements _Itable.TableType
            Get
                TableType = TableType_MemberVariable
            End Get
            Set(ByVal Value As DBconnect.Tbtype)
                TableType_MemberVariable = Value
            End Set
        End Property
    End Class
End Namespace