Option Strict Off
Option Explicit On

Imports ChannelcommDSC.TCPClientDSC
Imports ChannelcommDSC.SCommDSC

<System.Runtime.InteropServices.ProgId("TcpRtus_NET.TcpRtus")>
Public Class Channels
    Implements IEnumerable

    Private _mCol As Collection

    Private _mvartimeout As Integer


    Public Property Timeout() As Integer
        Get
            
            Timeout = _mvartimeout
        End Get
        Set(ByVal value As Integer)

            _mvartimeout = Value
        End Set
    End Property


    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As IChannel
        Get

            'On Error Resume Next
            Item = _mCol.Item(vntIndexKey)
        End Get
    End Property


    Public ReadOnly Property Count() As Integer
        Get

            Count = _mCol.Count()
        End Get
    End Property


    Public Function GetEnumerator() As IEnumerator _
        Implements IEnumerable.GetEnumerator

        GetEnumerator = _mCol.GetEnumerator
    End Function

    Function ChangeName(ByRef ch As TcpChannel, ByRef newName As String) As String
        If newName <> ch.ChannelName Then

            ch.ChannelName = GetRightName(newName)
            changeName = ch.ChannelName
        Else
            changeName = newName
        End If
    End Function

    Private Function GetRightName(ByRef name As String) As String

        Dim i As Integer
        Dim tn As String
        tn = Name
        Do While checkname(Name) = False
            i = i + 1

            Name = tn & "_" & i

        Loop
        GetRightName = Name
    End Function

    Private Function Checkname(ByRef name As String) As Boolean
        Dim i As Integer

        Checkname = True
        For i = 0 To Me.Count - 1
            If Me(i).ChannelName = Name Then
                Checkname = False
            End If
        Next i
    End Function

    Private Function GetRightKey(ByRef key As String) As String

        Dim i As Integer
        Dim tn As String
        tn = key
        Do While checkKey(key) = False
            i = i + 1

            key = tn & "_" & i

        Loop
        GetRightKey = key
    End Function

    Function CheckKey(ByRef key As String) As Boolean

        checkKey = _mCol.Contains(key)
    End Function


    Public Function Add(ByVal channeltype As String, ByVal comminfo As String,
                        Optional ByVal skey As Object = Nothing) As IChannel
        '创建新对象
        Dim objNewMember As IChannel
        If channeltype = "TCP" Then
            objNewMember = New TcpChannel(comminfo)
        Else
            objNewMember = New SCommChannel(comminfo)
        End If


        '设置传入方法的属性


        If Len(skey) = 0 Then
            _mCol.Add(objNewMember)
        Else

            objNewMember.KeyStr = skey
            _mCol.Add(objNewMember, skey)
        End If


        '返回已创建的对象
        Return objNewMember


    End Function


    Public Sub Remove(ByRef vntIndexKey As Object)


        _mCol.Remove(vntIndexKey)
    End Sub


    Public Sub New()


        _mCol = New Collection
    End Sub

    Protected Overrides Sub Finalize()
        _mCol = Nothing
    End Sub


    Function GetChannelObjbySKey(ByRef skey As String) As TcpChannel

        GetChannelObjbySKey = _mCol(skey)
    End Function
End Class