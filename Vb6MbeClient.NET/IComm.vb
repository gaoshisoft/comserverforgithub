Public Interface IComm
    Event DataArrival(ByRef mValue As Object, ByRef bytesTotal As Integer)
    
    

    Sub SendData(ByRef data As Object)
        
ReadOnly Property State() As Boolean
    
Sub CloseMe()
    
    Sub ConnectTO(ByVal CommInfo As String)
    Property ErrMsg() As String
End Interface
