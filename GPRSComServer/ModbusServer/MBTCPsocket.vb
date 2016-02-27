Option Strict Off
Option Explicit On
Imports System
Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports Microsoft.VisualBasic
Friend Class MBTCPsocket
    Inherits System.Windows.Forms.Form
    Public Tcpport As Integer
    Public ConnCol As System.Collections.Generic.List(Of MyWinSockClient)
    Dim WithEvents TcpClnt As MyWinSockClient
    Friend WithEvents txtmbrv As System.Windows.Forms.TextBox
    Friend WithEvents txtmbsend As System.Windows.Forms.TextBox
    Friend WithEvents CmdHide As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Dim WithEvents Tmr5s As New Timer()
    Public WithEvents TcpSvr As MyWinSockListener
    Private Sub cmdhide_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdHide.Click
        Me.Hide()
    End Sub

    Private Sub Command3_Click()
        frmModbusserver.Show()
    End Sub


    'UPGRADE_WARNING: Form event Tcpsocket.Activate has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
    Private Sub Tcpsocket_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'Me.Visible = False
    End Sub

    Private Sub Tcpsocket_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Tcpsocket_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        Me.Hide()
        Cancel = True
        eventArgs.Cancel = Cancel
    End Sub






    Private Sub TcpSvr_ConnectionRequest(ByVal TcpClnt As System.Net.Sockets.Socket) Handles TcpSvr.ConnectionRequest

        Dim Clnt As MyWinSockClient
        'Dim Cnt As System.Net.Sockets.TcpClient
        Clnt = New MyWinSockClient
        Clnt.TcpClnt = TcpClnt
        AddHandler Clnt.DataArrival, AddressOf Me.TcpClnt_DataArrival
        ConnCol.Add(Clnt)


    End Sub



    Private Sub InitializeComponent()
        Me.txtmbrv = New System.Windows.Forms.TextBox
        Me.txtmbsend = New System.Windows.Forms.TextBox
        Me.CmdHide = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'txtmbrv
        '
        Me.txtmbrv.Location = New System.Drawing.Point(12, 32)
        Me.txtmbrv.Multiline = True
        Me.txtmbrv.Name = "txtmbrv"
        Me.txtmbrv.Size = New System.Drawing.Size(502, 97)
        Me.txtmbrv.TabIndex = 0
        '
        'txtmbsend
        '
        Me.txtmbsend.Location = New System.Drawing.Point(12, 135)
        Me.txtmbsend.Multiline = True
        Me.txtmbsend.Name = "txtmbsend"
        Me.txtmbsend.Size = New System.Drawing.Size(498, 120)
        Me.txtmbsend.TabIndex = 1
        '
        'CmdHide
        '
        Me.CmdHide.Location = New System.Drawing.Point(181, 271)
        Me.CmdHide.Name = "CmdHide"
        Me.CmdHide.Size = New System.Drawing.Size(125, 32)
        Me.CmdHide.TabIndex = 3
        Me.CmdHide.Text = "隐藏"
        Me.CmdHide.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(24, 323)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 12)
        Me.Label1.TabIndex = 4
        '
        'MBTCPsocket
        '
        Me.ClientSize = New System.Drawing.Size(546, 376)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CmdHide)
        Me.Controls.Add(Me.txtmbsend)
        Me.Controls.Add(Me.txtmbrv)
        Me.Name = "MBTCPsocket"
        Me.Text = "MBTCP 通信监视"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private Sub TcpClnt_DataArrival(ByVal Sender As MyWinSockClient, ByVal BytesTotal As Long, ByVal Data() As Byte) Handles TcpClnt.DataArrival
        Dim B As Byte
        Dim j As Long
        Dim mbdata() As Byte
        Dim MBResponsedataFrame() As Byte
        Sender.MyWaitTime = 0
        Try
            mbdata = Data
            '显示
            If Me.Visible = True Then
                txtmbrv.Text = HextoStr(mbdata, mbdata.GetLength(0), 0)
            End If
            Mbs.InformClientDataArrival(HextoStr(mbdata, BytesTotal, 0))
            '获得反应帧
            'Dim rtuid As Long

            MBResponsedataFrame = Mbs.GetResponseFrame(mbdata) '获取本地反应数据帧

            '-----------------------
            '显示
            If Me.Visible = True Then
                txtmbsend.Text = HextoStr(MBResponsedataFrame, MBResponsedataFrame.GetLength(0), 0)
            End If
            Mbs.InformClientDataResponse(HextoStr(MBResponsedataFrame, MBResponsedataFrame.GetLength(0), 0))

            '发送
            Sender.SendData(MBResponsedataFrame)
        Catch


        End Try

    End Sub

    Public Sub New()

    End Sub
    Public Sub New(ByVal Ada As Int16, ByVal TcpPort As Integer)
        'MyBase.New()
        Me.InitializeComponent()
        TcpClnt = New MyWinSockClient
        Me.Tcpport = TcpPort
        Dim Ip As IPAddress
        Ip = Dns.GetHostAddresses(Dns.GetHostName)(Ada - 1)
        TcpSvr = New MyWinSockListener(Ip, TcpPort)
        ConnCol = New System.Collections.Generic.List(Of MyWinSockClient)

        TcpSvr.Start()
        Tmr5s.Interval = 5000
        Tmr5s.Enabled = True
    End Sub
    Sub Init(ByVal Ada As Int16, ByVal TcpPort As Integer)
        Me.InitializeComponent()
        TcpClnt = New MyWinSockClient
        Me.Tcpport = TcpPort
        Dim Ip As IPAddress
        Ip = Dns.GetHostAddresses(Dns.GetHostName)(Ada - 1)
        TcpSvr = New MyWinSockListener(Ip, TcpPort)
        ConnCol = New System.Collections.Generic.List(Of MyWinSockClient)

        TcpSvr.Start()
        Tmr5s.Interval = 5000
        Tmr5s.Enabled = True
    End Sub

    Private Sub Tmr5s_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tmr5s.Tick
        Dim i As Integer
        For i = ConnCol.Count - 1 To 0 Step -1
            ConnCol(i).MyWaitTime = ConnCol(i).MyWaitTime + 5
           
            If ConnCol(i).MyWaitTime >= 20 Then
                Dim TC As MyWinSockClient

                TC = ConnCol(i)

                'If TC.Ifconnected = False Then
                ConnCol.RemoveAt(i)

                TC.TcpClnt.Shutdown(SocketShutdown.Both)
                TC.TcpClnt.Close()


                RemoveHandler TC.DataArrival, AddressOf Me.TcpClnt_DataArrival
                TC.StopMe()
                TC.TcpClnt = Nothing
                TC = Nothing



                GC.Collect()
                

            End If

        Next
        Dim Cstate As String
        For Each c As MyWinSockClient In ConnCol
            Cstate = Cstate & " " & c.TcpClnt.Connected
        Next
        Label1.Text = GC.GetTotalMemory(True) & " " & ConnCol.Count & Cstate
    End Sub

    
End Class