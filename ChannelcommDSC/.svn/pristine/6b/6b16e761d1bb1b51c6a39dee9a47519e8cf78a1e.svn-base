Public Interface IChannel
    Property Comstate As Boolean '表这个通道的数据收发状态，真表示已发送但还未收到数据也没有超时也就是正在等待数据的返回，假表示没有发送或已超时
    Property Timeout As Double


    Event DataArrival(ByVal theCommInfo As String, ByVal mValue As Object, ByVal bytesTotal As Integer)

    Property ChannelState() As Boolean

    Property KeyStr() As String
    Property ChannelName() As String


    'ReadOnly Property Wsk() As TcpWsk


    Property Comminfo() As String
    'Comminfo 内容 在整个程序内保持一致
    '13610983709 这是gprs通信格式
    'TCP-192.168.1.2:80 
    'SComm-COM1:9600,8,n,1
    '原本想做成支持冗余即这种模式 TCP-192.168.1.2:80(192.168.1.3:90) 但效果不太理想
    '后来关于冗余我有两个想法：
    '1 是把通信结构（即树形列表再增加一级）每个站可拥有多个通道，每个通道拥有多个块，如果冗余，则两个通道拥有的块一样
    '2  仍保持现有3级结构，但在每个站上右键可查看这个站的冗余通道内容，或在配置界面上，做相应按钮配置和查看亦可
    '现在想在RTU对象内保存两个comminfo 一个为主，一个为从，
    '现在先不去实现冗余功能，等有时间再去做
    '与Icomm不同之处是 ichannel真正体现“通道”的 思想，Ichannel需要区分TCP 和 串口模式




    Sub SendByteData(ByVal data() As Byte)


    Sub Init(ByVal thecomminfo As String, ByVal theChannelName As String)
End Interface
