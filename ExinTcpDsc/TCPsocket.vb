Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Net.Sockets
Friend Class TCPsocket
	Inherits System.Windows.Forms.Form
    Public Tcpport As Integer
    Public Ip As System.Net.IPAddress
    Public WithEvents myTcpClient As MyWinSockClient = New MyWinSockClient
    Public WithEvents myTcpLisener As MyWinSockListener
    Public ConnCol As New System.Collections.Generic.List(Of MyWinSockClient)

	Public Tmr As New Mytimer
	
	Private Sub cmdhide_Click()
		Me.Hide()
	End Sub
	
	
	
	
	Private Sub Command1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command1.Click
		Me.Hide()
	End Sub
	

	
	Private Sub TCPsocket_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		'Winsock1(0).LocalPort = Dsc.LocalPort
		'Winsock1(0).RemotePort = Tcpport
		'Winsock1(0).Listen
        'Me.Show


        myTcpLisener = New MyWinSockListener(Ip, Tcpport)
	End Sub
	
	Private Sub TCPsocket_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
		Dim Cancel As Boolean = eventArgs.Cancel
		Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
		Me.Hide()
		Cancel = True
		eventArgs.Cancel = Cancel
	End Sub
	
	
	
	Private Sub Timer1_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Timer1.Tick
		Tmr.Tell()
	End Sub
	
  
	
	
	
	

    

    Private Sub myTcpClient_DataArrival(ByRef Sender As MyWinSockClient, ByVal BytesTotal As Long, ByVal Data() As Byte) Handles myTcpClient.DataArrival
        'Dim Index As Short = Winsock1.GetIndex(eventSender)
        Dim find As ConlineDTU = Nothing
        Dim b As Byte = 0
        Dim i As Integer

        Dim ResponsedataFrame() As Byte

       
     
        Dim s As String = ""
        For i = 0 To UBound(Data)
            s = s & Chr(Data(i))
        Next i
        '-----------
        'Dim s2 As String
        's2 = Left(s, Len(s) - 1) & " " & CStr(Now)
        'Text1.Text = s2

        If Me.Visible Then
            txtRv.Text = BitConverter.ToString(Data, 0, Data.Length) & Now
        End If
        '-----------
        For i = 1 To OnlineDtus.Count

            If OnlineDtus(i).WinSock Is Sender Then
                find = OnlineDtus(i)
                Exit For
            End If
        Next i
        Dim R(0) As Byte
        If find Is Nothing Then '新上线的DTU
            If Len(s) = 33 And VB.Left(s, 2) = "**" Then '为BlDTU
                '          MsgBox s
                OnlineDtus.AddNewLoginBLDTU(Data, Sender)

            ElseIf Len(s) > 33 Then 'hxDTU
                OnlineDtus.AddNewLoginHXDTU(Data, Sender)
                R(0) = 49
                ' R(1) = 13

                ResponsedataFrame = VB6.CopyArray(R)
                Sender.SendData(ResponsedataFrame)
            ElseIf VB.Left(s, 2) = "@@" And VB.Right(s, 2) = "##" Then '@@13831521345IP:123.23.43.34## 蓝迪DTU
                OnlineDtus.AddNewLoginLDDTU(Data, Sender)

            End If
        Else '为已上线DTU
            If s = "####" Then '为博联DTU
                find.HeartBeatTime = Now



            ElseIf VB.Left(s, 2) = "ok" Then  '宏信DTU

                find.HeartBeatTime = Now


                R(0) = 50
                '    R(1) = 13
                ResponsedataFrame = VB6.CopyArray(R)
                Sender.SendData(ResponsedataFrame)

            Else '为返回的数据
                OnlineDtus.informDataArrival(find.SimCardNo, Data, BytesTotal)

            End If

        End If
    End Sub

    Private Sub myTcpLisener_ConnectionRequest(ByRef TcpClnt As System.Net.Sockets.TcpClient) Handles myTcpLisener.ConnectionRequest
        Dim Clnt As New MyWinSockClient


        Clnt.TcpClnt = TcpClnt
        AddHandler Clnt.DataArrival, AddressOf Me.myTcpClient_DataArrival
        ConnCol.Add(Clnt)
    End Sub
End Class