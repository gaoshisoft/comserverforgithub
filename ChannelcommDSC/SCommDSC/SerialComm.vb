Option Strict Off
Option Explicit On

Imports System.Dynamic
Imports System.Threading

Namespace SCommDSC

    Public Class ScommWsk
        Implements IComm
        Private WithEvents t As System.Windows.Forms.Timer

        Public WithEvents sp As System.IO.Ports.SerialPort
        '处理真正的通讯工作

        Dim inputdata() As Byte
        Dim rvLength As Integer
        Private _mstate As Boolean
        Private _mErrmsg As String


        'Delegate Sub Inform(ByVal data As Object, ByVal length As Int16)
        'Dim _inform As Inform

        Public ReadOnly Property MErrmsg() As String
            Get
                Return _mErrmsg
            End Get
        End Property

        Public Event DataArrival(ByRef mValue As Object, ByRef bytesTotal As Integer) Implements IComm.DataArrival


        Private Sub mscommWsk_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) _
            Handles sp.DataReceived

            't.Enabled = True
            Try
                Thread.Sleep(100)
                inputdata = Text.ASCIIEncoding.ASCII.GetBytes(sp.ReadExisting)
                rvLength = inputdata.GetLength(0)
                '_inform(inputdata, rvLength)

            Catch ex As SystemException

            End Try

        End Sub

        Public Sub New(ByVal commInfo As String)
            'Comminfo 内容
            '13610983709
            '192.168.1.2:80 或 192.168.1.2:80(192.168.1.3:90)  后者是冗余通信的 通信标识 格式
            'COM1:9600,8,n,1
            Me.Comminfo = commInfo
            Dim s As String
            s = commInfo.Replace("SComm-", "")
            sp = New IO.Ports.SerialPort
            Dim st() As String
            st = s.Split(":")(1).Split(",")

            sp.BaudRate = st(0)
            sp.DataBits = st(1)
            Dim P As Int16
            Select Case st(2)
                Case "n"
                    P = 0
                Case "o"
                    P = 1
                Case "e"
                    P = 2

            End Select
            sp.Parity = P
            sp.StopBits = st(3)
            sp.PortName = s.Split(":")(0)

            sp.WriteTimeout = 1000
            sp.ReadTimeout = 1000
            sp.ReadBufferSize = 4096
            sp.ReceivedBytesThreshold = 1
            ConnectTo()
            Me.t = New System.Windows.Forms.Timer With {.Interval = 200, .Enabled = True}

            '_inform = New Inform(AddressOf Me.Raisemyevent)
        End Sub

        Protected Overrides Sub Finalize()

            sp.Close()
            'Sp.Finalize()
        End Sub


        Public Sub CloseMe() Implements IComm.CloseMe


            If sp.IsOpen = True Then
                sp.Close()
            End If
        End Sub

        Public Property Comminfo() As String Implements IComm.Comminfo

        Public Sub ConnectTo() Implements IComm.ConnectTo

            Try



                sp.Open()


                _mstate = True
            Catch ex As Exception

                _mstate = False
                _mErrmsg = ex.Message
            Finally

            End Try
        End Sub


        Public Sub SendData(ByVal data As Object) Implements IComm.SendData
            Try

                Dim d() As Byte
                'If CommandBuffer.Count() >= 1 Then
                d = data
                'CommandBuffer.Remove(1)

                If sp.IsOpen = False Then
                    sp.Open()
                End If

                sp.Write(d, 0, d.Length)


                'End If
            Catch ex As Exception
            End Try

        End Sub

        Public ReadOnly Property State As Boolean Implements IComm.State
            Get


                State = _mstate
            End Get
        End Property

        Public Property ErrMsg As String Implements IComm.ErrMsg
            Get
                ErrMsg = _mErrmsg
            End Get
            Set(ByVal value As String)
            End Set
        End Property
        Sub Raisemyevent(ByVal data As Object, ByVal rvLen As Int16)
            If rvLength > 0 Then
                RaiseEvent DataArrival(data, rvLen)
                rvLength = 0
            End If
        End Sub

        Private Sub t_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles t.Tick
            If rvLength > 0 Then
                RaiseEvent DataArrival(inputdata, rvLength)
                rvLength = 0
            End If
        End Sub
    End Class
End Namespace