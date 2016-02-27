Public Interface IChannel

   




    Property Comstate As Boolean '表这个通道的数据收发状态，真表示已发送但还未收到数据也没有超时也就是正在等待数据的返回，假表示没有发送或已超时
    Property Timeout As Double
   

    Event DataArrival(ByVal CommInfo As String, ByVal mValue As Object, ByVal bytesTotal As Integer)

    Property ChannelState() As Boolean
        

    Property ChannelName() As String
  





    'ReadOnly Property Wsk() As TcpWsk
   





    Property Comminfo() As String
    

Sub SendByteData(ByVal data() As Byte)
   



Sub init(ByVal comminfo As String, ByVal ChannelName As String)
    
End Interface
