Option Strict On
Option Explicit On

Imports System.Windows.Forms

Namespace SCommDSC

    <System.Runtime.InteropServices.ProgId("SCommRTU_NET.SCommRTU")>
    Public Class SCommChannel
        Implements IChannel


        Public ChannelId As Integer


        Private _mvarChannelName As String '�ֲ�����

        


        Private WithEvents mWsk As ScommWsk


        Private _mvarChannelState As Boolean '�ֲ�����

        Dim _mcomminfo As String
        Dim _mcommstate As Boolean
        Dim _mtimeout As Double

        Public Event DataArrival(ByVal theCommInfo As String, ByVal mValue As Object, ByVal bytesTotal As Integer) _
            Implements IChannel.DataArrival


        Public Property ChannelState() As Boolean Implements IChannel.ChannelState
            Get

                _mvarChannelState = Me.Wsk.State

                ChannelState = _mvarChannelState
            End Get

            Set(ByVal value As Boolean)
            End Set
        End Property

        Public Property KeyStr() As String Implements IChannel.KeyStr

        Public Property ChannelName() As String Implements IChannel.ChannelName
            Get

                ChannelName = _mvarChannelName
            End Get
            Set(ByVal value As String)

                _mvarChannelName = Value
            End Set
        End Property


        Public ReadOnly Property Wsk() As ScommWsk
            Get

                Wsk = mWsk
            End Get
        End Property

        Public Sub New(ByVal comminfo As String)
           
            Me.Init(comminfo, comminfo)
           
        End Sub


        Sub SendByteData(ByVal data() As Byte) Implements IChannel.SendByteData
            Try
                If Wsk.State = False Then
                    Me.Wsk.CloseMe()

                    Wsk.ConnectTo() '���״̬���������½�������
                    Exit Sub
                End If

            Catch ex As Exception
            End Try
            Wsk.SendData(data)
        End Sub

        Private Sub mWsk_DataArrival(ByRef mValue As Object, ByRef bytesTotal As Integer) Handles mWsk.DataArrival

            RaiseEvent DataArrival(Me.Comminfo, mValue, bytesTotal)
        End Sub

       

        Sub Init(ByVal thecomminfo As String, ByVal theChannelName As String) Implements IChannel.Init
            Me.Comminfo = thecomminfo


            Me.ChannelName = theChannelName
            mWsk = New ScommWsk(Comminfo)

        End Sub


        Public Property Comminfo As String Implements IChannel.Comminfo
            Get
                Return _mcomminfo
            End Get
            Set(ByVal value As String)
                _mcomminfo = value
            End Set
        End Property

        Public Property Comstate As Boolean Implements IChannel.Comstate
            Get
                '�����RTU�������շ�״̬, ���ʾ�ѷ��͵���δ�յ�����Ҳû�г�ʱҲ�������ڵȴ����ݵķ���, �ٱ�ʾû�з��ͻ��ѳ�ʱ
                Return _mcommstate
            End Get
            Set(ByVal value As Boolean)
                _mcommstate = value
            End Set
        End Property


        Public Property Timeout As Double Implements IChannel.Timeout
            Get
                Timeout = _mtimeout
            End Get
            Set(ByVal value As Double)
                _mtimeout = value
            End Set
        End Property

       
    End Class
End Namespace