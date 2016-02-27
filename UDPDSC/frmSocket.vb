Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Net.Sockets
Imports System.Net
Friend Class frmSocket
    Inherits System.Windows.Forms.Form
    Public Udpport As Integer
    Public Ip As System.Net.IPAddress
    Public WithEvents myUDPClient As MyWinSockClient = New MyWinSockClient
    Public WithEvents myLisener As MyWinSockListener
    Public ConnCol As New System.Collections.Generic.List(Of MyWinSockClient)

    Public Tmr As New Mytimer
    Dim WithEvents T As Timer = New Timer With {.Interval = 100, .Enabled = True}

    Private Sub cmdhide_Click()
        Me.Hide()
    End Sub




    Private Sub Command1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command1.Click
        Me.Hide()
    End Sub



    Private Sub TCPsocket_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load



        'myLisener = New MyWinSockListener(Ip, Udpport)


    End Sub

    Private Sub TCPsocket_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'Dim Cancel As Boolean = eventArgs.Cancel
        'Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        'Me.Hide()
        'Cancel = True
        'eventArgs.Cancel = Cancel
    End Sub



    Private Sub Timer1_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles T.Tick
        Tmr.Tell()

    End Sub



    'Private Function GetSimNo(ByVal S As String) As String

    'End Function





    Private Sub myTcpClient_DataArrival(ByRef Sender As MyWinSockClient, ByVal BytesTotal As Long, ByVal Data() As Byte) Handles myUDPClient.DataArrival

        Dim find As ConlineDTU = Nothing
        Dim b As Byte = 0
        Dim i As Integer
        Dim R(0) As Byte




        Dim s As String = ""
        For i = 0 To UBound(Data)
            s = s & Chr(Data(i))
        Next i
        '-----------
     

        If Me.Visible Then
            txtRv.Text = BitConverter.ToString(Data, 0, Data.Length) & Now & Sender.UdpRemoteEnd.Serialize.ToString
        End If
        '----------- 
        Dim SimStr As String
        Dim SimNo(10) As Byte
        Array.Copy(Data, SimNo, 11)
        SimStr = Replace(System.Text.ASCIIEncoding.ASCII.GetString(SimNo), " ", "")
        For i = 1 To OnlineDtus.Count

            If Not OnlineDtus(i).WinSock.UdpRemoteEnd Is Nothing Then





                If OnlineDtus(i).SimCardNo = SimStr Then
                    find = OnlineDtus(i)
                    Dim Realdata(Data.Length - 12) As Byte
                    Array.Copy(Data, 11, Realdata, 0, Realdata.Length)
                    ReDim Data(Realdata.Length - 1)
                    Array.Copy(Realdata, Data, Realdata.Length)
                    Exit For
                End If
            End If
        Next i

        If Len(s) >= 33 And VB.Mid(s, 12, 6) = "DTUREG" Then 'hxDTU
            If find Is Nothing Then
                OnlineDtus.AddNewLoginHXDTU(Data, Sender)
            End If
            ReDim R(1)
            R(0) = 49
            Sender.SendData(R)


        ElseIf Mid(s, 12, 2) = "ok" Then  '宏信DTU
            find.HeartBeatTime = Now
            ReDim R(1)

            R(0) = 50
            '    R(1) = 13

            Sender.SendData(R)
            Me.Label4.Text = Label4.Text & find.SimCardNo
        Else '为返回的数据

            If find Is Nothing Then '新上线的DTU
                'If Len(s) = 33 And VB.Left(s, 2) = "**" Then '为BlDTU

                '    OnlineDtus.AddNewLoginBLDTU(Data, Sender)

                'If Len(s) >= 33 And VB.Mid(s, 12, 6) = "DTUREG" Then 'hxDTU
                '    OnlineDtus.AddNewLoginHXDTU(Data, Sender)
                '    ReDim R(1)
                '    R(0) = 49
                '    Sender.SendData(R)
                '    'ElseIf VB.Left(s, 2) = "@@" And VB.Right(s, 2) = "##" Then '@@13831521345IP:123.23.43.34## 蓝迪DTU
                '    '    OnlineDtus.AddNewLoginLDDTU(Data, Sender)

                'End If
            Else '为已上线DTU
              



                'ElseIf Mid(s, 12, 2) = "ok" Then  '宏信DTU

                OnlineDtus.informDataArrival(find.SimCardNo, Data, BytesTotal - 11)

            End If

        End If
    End Sub

   


    Private Sub myLisener_DataArrival(ByRef Sender As MyWinSockClient, ByVal BytesTotal As Long, ByVal Data() As Byte) Handles myLisener.DataArrival
        myTcpClient_DataArrival(Sender, BytesTotal, Data)
    End Sub
End Class