Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Net.Sockets
Imports System.Net
Friend Class FrmSocket
    Inherits System.Windows.Forms.Form
    Public Tcpport As Integer
    Public Ip As System.Net.IPAddress
    Public WithEvents myTcpClient As MyWinSockClient
    Public WithEvents myLisener As MyWinSockListener
    Public ConnCol As New System.Collections.Generic.List(Of MyWinSockClient)
    Public DSC As cDSC


    Private Sub Command1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command1.Click
        Me.Hide()

    End Sub

    Private Sub frmSocket_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        For Each winsock As MyWinSockClient In ConnCol
            winsock.CloseMe()
        Next
    End Sub



    Public Sub myTcpClient_DataArrival(ByRef Sender As MyWinSockClient, ByVal BytesTotal As Long, ByVal Data() As Byte) Handles myTcpClient.DataArrival
        'Dim Index As Short = Winsock1.GetIndex(eventSender)
        Dim find As ConlineDTU = Nothing
        Dim b As Byte = 0
        Dim i As Integer

        'Dim ResponsedataFrame() As Byte



        Dim s As String = ""
        For i = 0 To UBound(Data)
            s = s & Chr(Data(i))
        Next i
        '-----------
        'Dim s2 As String
        's2 = Left(s, Len(s) - 1) & " " & CStr(Now)
        'Text1.Text = s2

        'If Me.Visible Then
        '    'txtRv.Text = BitConverter.ToString(Data, 0, Data.Length) & Now & Sender.UdpRemoteEnd.Serialize.ToString
        'End If
        '-----------
        For i = 1 To DSC.OnlineDtus.Count
            If Not DSC.OnlineDtus(i).WinSock.TcpClnt Is Nothing Then
                If DSC.OnlineDtus(i).WinSock.TcpClnt Is Sender.TcpClnt Then
                    find = DSC.OnlineDtus(i)
                    Exit For
                End If
            End If

        Next i
        Dim R(0) As Byte
        If find Is Nothing Then '新上线的DTU
            If Len(s) = 33 And VB.Left(s, 2) = "**" Then '为BlDTU

                DSC.OnlineDtus.AddNewLoginBLDTU(Data, Sender)

            ElseIf Len(s) >= 33 Then 'hxDTU
                DSC.OnlineDtus.AddNewLoginHXDTU(Data, Sender)
                ReDim R(1)
                R(0) = 49
                Sender.SendData(R)
            ElseIf VB.Left(s, 2) = "@@" And VB.Right(s, 2) = "##" Then '@@13831521345IP:123.23.43.34## 蓝迪DTU
                DSC.OnlineDtus.AddNewLoginLDDTU(Data, Sender)

            ElseIf VB.Left(s, 4) = "exin" Then
                DSC.OnlineDtus.AddNewLoginHD3GDTU(Data, Sender)
            ElseIf Data.GetLength(0) = 6 Then
                DSC.OnlineDtus.AddNewLoginUSRwifiDTU(Data, Sender)
            End If
        Else '为已上线DTU
            If s = "####" Then '为blDTU
                find.HeartBeatTime = Now



            ElseIf VB.Left(s, 2) = "ok" Then  'hxDTU

                find.HeartBeatTime = Now
                ReDim R(1)

                R(0) = 50
                '    R(1) = 13

                Sender.SendData(R)
                Me.Label4.Text = Label4.Text & find.SimCardNo
            ElseIf VB.Left(s, 4) = "exin" Then '宏电3G dtu
                find.HeartBeatTime = Now
            Else '为返回的数据
                DSC.OnlineDtus.informDataArrival(find.SimCardNo, Data, BytesTotal)
                find.HeartBeatTime = Now
            End If

        End If
    End Sub

    Private Sub myTcpLisener_ConnectionRequest(ByVal TcpClnt As System.Net.Sockets.TcpClient) Handles myLisener.ConnectionRequest
        Dim Clnt As New MyWinSockClient


        Clnt.TcpClnt = TcpClnt
        AddHandler Clnt.DataArrival, AddressOf Me.myTcpClient_DataArrival '委托也是一种阻止窗体关闭，资源释放的强悍关联
        ConnCol.Add(Clnt)
    End Sub


    Protected Overrides Sub Finalize()

        MyBase.Finalize()
    End Sub
End Class