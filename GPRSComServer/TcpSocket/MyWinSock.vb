Imports System.Net.Sockets
Imports System.Timers
Imports System.Runtime.InteropServices
Public Class Mywinsock
    Inherits System.Net.Sockets.TcpClient
    Dim WithEvents Tmr100ms As New Timer(100)

    Public Event DataArrival(ByVal BytesTotal As Long, ByVal Data() As Byte)
    Dim InBuff(511) As Byte
    Dim Rl As Long '接收到的字节数


    Public ReadOnly Property Ifconnected() As Boolean
        Get
            Dim s As Socket = MyBase.Client
            If s.Connected = True Then
                Ifconnected = True
            Else
                Ifconnected = False

            End If
        End Get
    End Property

    Private Sub Tmr100ms_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles Tmr100ms.Elapsed
        If Me.GetStream.DataAvailable Then
            Dim i As Long
            Rl = Me.GetStream.Read(InBuff, 0, InBuff.Length)
            Dim B(Rl - 1) As Byte
            For i = 0 To Rl - 1

            Next
            B(i) = InBuff(i)
            'If Chr(B(i)) = "}" Then
            '    Dim Ifend As Boolean
            '    Ifend = True

        End If
            Next i
        'If ifend = True Then
        RaiseEvent DataArrival(Rl, B)
        'End If

        End If
    End Sub

    Public Sub New()
        Tmr100ms.Enabled = True
    End Sub
End Class 'MyTcpClientDerivedClass

