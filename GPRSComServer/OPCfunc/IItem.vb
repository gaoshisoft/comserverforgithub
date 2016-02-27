Option Strict Off
Option Explicit On
Interface _IItem
    Property ItemFieldName As String
    Property ItemDataType As String
    Property ItemValue As Object
End Interface

Friend Class IItem
    Implements _IItem
    Dim ItemFieldName_MemberVariable As String

    Public Property ItemFieldName() As String Implements _IItem.ItemFieldName
        Get
            ItemFieldName = ItemFieldName_MemberVariable
        End Get
        Set(ByVal Value As String)
            ItemFieldName_MemberVariable = Value
        End Set
    End Property

    Dim ItemDataType_MemberVariable As String

    Public Property ItemDataType() As String Implements _IItem.ItemDataType
        Get
            ItemDataType = ItemDataType_MemberVariable
        End Get
        Set(ByVal Value As String)
            ItemDataType_MemberVariable = Value
        End Set
    End Property

    Dim ItemValue_MemberVariable As Object

    Public Property ItemValue() As Object Implements _IItem.ItemValue
        Get
            ItemValue = ItemValue_MemberVariable
        End Get
        Set(ByVal Value As Object)
            ItemValue_MemberVariable = Value
        End Set
    End Property
End Class