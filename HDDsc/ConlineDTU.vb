Option Strict Off
Option Explicit On
Public Class ConlineDtu

    Private mvarLoginTime As Date '�ֲ�����


    Private mvarSimCardNo As String '�ֲ�����




    Private mvarHeartBeatTime As Date '�ֲ�����


    Public Property HeartBeatTime() As Date
        Get

            If mvarHeartBeatTime = System.DateTime.FromOADate(0) Then
                mvarHeartBeatTime = Now
            End If
            HeartBeatTime = mvarHeartBeatTime

        End Get
        Set(ByVal Value As Date)

            mvarHeartBeatTime = Value
        End Set
    End Property


    Public Property Phonenumber() As String
        Get

            Phonenumber = mvarSimCardNo
        End Get
        Set(ByVal Value As String)

            mvarSimCardNo = Value
        End Set
    End Property


    Public Property LoginTime() As Date
        Get

            LoginTime = mvarLoginTime
        End Get
        Set(ByVal Value As Date)

            mvarLoginTime = Value
        End Set
    End Property
End Class