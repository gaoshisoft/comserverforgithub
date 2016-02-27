
Public Interface IDataBlock
    Property SvrDevAd() As Integer

    Property SvrMBADStart() As Integer

    Property SvrAddrLength() As Integer

    Property BlockName() As String

    Property Enable() As Boolean

    Property Addr() As Integer
    Property startAd() As String
    Property Length() As Integer

    Function GetCommandBytes() As Object

    Function GetValueFromRvData(ByVal length As Integer, ByVal Rvdata() As Byte) As Boolean

    Sub AddItm(ByVal ItmName As String)
    Property ParaAddr() As String '获得关于参数存储地址的描述
End Interface
