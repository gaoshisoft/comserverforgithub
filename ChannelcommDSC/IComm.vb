Public Interface IComm
    Event DataArrival(ByRef mValue As Object, ByRef bytesTotal As Integer)


    Sub SendData(ByVal data As Object)

    ReadOnly Property State() As Boolean

    Sub CloseMe()
    Property Comminfo As String
    'Comminfo 内容
    '13610983709  GPRS的comminfo 格式仍保持不变，是一个手机号，这种模式用不到
    '192.168.1.2:80 TCP模式
    'COM1:9600,8,n,1 串口模式
    Sub ConnectTo()
    Property ErrMsg() As String
End Interface
