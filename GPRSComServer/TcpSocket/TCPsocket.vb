Option Strict Off
Option Explicit On

Imports VB = Microsoft.VisualBasic
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports MBsrv

'Imports 

Friend Class TCPsocket
    Inherits System.Windows.Forms.Form
    Public ConnCol As System.Collections.Generic.List(Of MyWinSockClient)
    Dim WithEvents TcpClnt As MyWinSockClient


    Public WithEvents TcpSvr As MyWinSockListener
    'Dim WithEvents Svr127 As MyWinSockListener
    Public Tcpport As Integer
    Public Ip As IPAddress
    Public RecordBuff As New Microsoft.VisualBasic.Collection

    Dim WithEvents Tmr As Timer

    Private Sub Tmr_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tmr.Tick
        On Error Resume Next
        ''���������ϴ���ͨ��
        If RecordBuff.Count > 0 Then
            If BroadCastSend(RecordBuff(1)) Then

                RecordBuff.Remove(1)
            End If
            If RecordBuff.Count > 1000 Then '�����ʱ�����ˣ���1000���Ļ���
                RecordBuff.Remove(1)
            End If
        End If
        '�����ѻ����������
        Dim i As Integer
        For i = ConnCol.Count - 1 To 0 Step - 1
            ConnCol(i).MyWaitTime = ConnCol(i).MyWaitTime + 1

            If ConnCol(i).MyWaitTime >= 70 Then
                Dim TC As MyWinSockClient

                TC = ConnCol(i)

                'If TC.Ifconnected = False Then
                ConnCol.RemoveAt(i)

                TC.TcpClnt.Shutdown(SocketShutdown.Both)
                TC.TcpClnt.Close()


                RemoveHandler TC.DataArrival, AddressOf Me.TcpClnt_DataArrival 'ֻ�н�ί�й�ϵ�Ƴ������ܳ��׽�����ù�ϵ
                TC.StopMe()
                TC.TcpClnt = Nothing
                TC = Nothing


                'GC.Collect()


            End If

        Next


        Label1.Text = ConnCol.Count
    End Sub


    Private Sub Command1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
        Handles Command1.Click
        txtmbsend.Text = ""
        txtmbrv.Text = ""
    End Sub


    Private Sub Tcpsocket_FormClosing(ByVal eventSender As System.Object,
                                      ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) _
        Handles Me.FormClosing
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        Me.Hide()
        Cancel = True
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub TcpClnt_DataArrival(ByVal Sender As MyWinSockClient, ByVal BytesTotal As Long, ByVal Data() As Byte) _
        Handles TcpClnt.DataArrival

        Dim commanddata() As Byte

        Sender.MyWaitTime = 0
        Try
            commanddata = Data
            '��ʾ
            If Me.Visible = True Then
                txtmbrv.Text = System.Text.Encoding.ASCII.GetString(commanddata)
            End If

            '��÷�Ӧ֡
            'Dim rtuid As Long
            Dim Rvs As String
            Dim SendS As String
            Rvs = System.Text.Encoding.ASCII.GetString(commanddata)

            SendS = GDoComWork.GetResponseFrame(Rvs) '��ȡ���ط�Ӧ����֡

            '-----------------------
            '��ʾ
            If Me.Visible Then
                txtmbsend.Text = SendS
            End If


            '����
            Sender.SendData(System.Text.ASCIIEncoding.ASCII.GetBytes(SendS))

        Catch ex As Exception


        End Try
    End Sub

    Function BroadCastSend(ByVal data As String) As Boolean
        If ConnCol.Count > 0 Then
            For i As Int16 = 0 To ConnCol.Count - 1

                ConnCol(i).SendData(System.Text.ASCIIEncoding.ASCII.GetBytes(data))

            Next
            BroadCastSend = True
        Else
            BroadCastSend = False
        End If
    End Function

    Private Sub TCPsocket_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TcpClnt = New MyWinSockClient


        TcpSvr = New MyWinSockListener(IPAddress.Any, Tcpport)

        TcpSvr.Start()

        ConnCol = New System.Collections.Generic.List(Of MyWinSockClient)

        Tmr = New Timer
        Tmr.Interval = 1000

        Tmr.Start()
    End Sub


    Private Sub cmdhide_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdhide.Click
        Me.Hide()
    End Sub




    Private Sub TcpSvr_ConnectionRequest(ByVal TcpClnt As System.Net.Sockets.Socket) Handles TcpSvr.ConnectionRequest
        Dim Clnt As New MyWinSockClient


        Clnt.TcpClnt = TcpClnt
        AddHandler Clnt.DataArrival, AddressOf Me.TcpClnt_DataArrival
        ConnCol.Add(Clnt)
    End Sub

    Protected Overrides Sub Finalize()
        Tmr.Enabled = False
        Tmr = Nothing
        MyBase.Finalize()
    End Sub
End Class