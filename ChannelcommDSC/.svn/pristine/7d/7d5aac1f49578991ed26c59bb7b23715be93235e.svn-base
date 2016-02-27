Option Strict Off
Option Explicit On

Imports ChannelcommDSC.TCPClientDSC

<System.Runtime.InteropServices.ProgId("TcpRtus_NET.TcpRtus")>
Public Class TCPChannels
    Implements System.Collections.IEnumerable

    Private mCol As Collection

    Private mvartimeout As Integer '局部复制


    Public Property Timeout() As Integer
        Get
            '检索属性值时使用，位于赋值语句的右边。
            'Syntax: Debug.Print X.StationPolltime
            Timeout = mvartimeout
        End Get
        Set(ByVal Value As Integer)
            '向属性指派值时使用，位于赋值语句的左边。
            'Syntax: X.StationPolltime = 5
            mvartimeout = Value
        End Set
    End Property


    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As TCPChannel
        Get

            On Error Resume Next
            Item = mCol.Item(vntIndexKey)
        End Get
    End Property


    Public ReadOnly Property Count() As Integer
        Get

            Count = mCol.Count()
        End Get
    End Property


    Public Function GetEnumerator() As System.Collections.IEnumerator _
        Implements System.Collections.IEnumerable.GetEnumerator

        GetEnumerator = mCol.GetEnumerator
    End Function

    Function changeName(ByRef ch As TCPChannel, ByRef newName As String) As String
        If newName <> ch.ChannelName Then

            ch.ChannelName = GetRightName(newName)
            changeName = ch.ChannelName
        Else
            changeName = newName
        End If
    End Function

    Private Function GetRightName(ByRef Name As String) As String

        Dim i As Integer
        Dim tn As String
        tn = Name
        Do While checkname(Name) = False
            i = i + 1

            Name = tn & "_" & i

        Loop
        GetRightName = Name
    End Function

    Private Function checkname(ByRef Name As String) As Boolean
        Dim i As Integer

        checkname = True
        For i = 0 To Me.Count - 1
            If Me(i).ChannelName = Name Then
                checkname = False
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

    Function checkKey(ByRef key As String) As Boolean

        checkKey = mCol.Contains(key)
    End Function


    Public Function Add(ByVal RtuName As String, ByVal comminfo As String, ByVal Tout As Double,
                        Optional ByVal Skey As Object = Nothing) As TCPChannel
        '创建新对象
        Dim objNewMember As TCPChannel
        objNewMember = New TCPChannel(comminfo)


        '设置传入方法的属性
        Dim i As Integer
        objNewMember.ChannelID = Me.Count + 1
        'objNewMember.Ipaddr = Ipaddr
        'objNewMember.portno = portno
        'objNewMember.RtuName = RtuName
        'objNewMember.polltime = polltime
        'objNewMember.Enable = Enable
        objNewMember.Timeout = Tout
        If Len(Skey) = 0 Then
            mCol.Add(objNewMember)
        Else
            'UPGRADE_WARNING: 未能解析对象 Skey 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
            objNewMember.Keystr = Skey
            mCol.Add(objNewMember, Skey)
        End If


        '返回已创建的对象
        Add = objNewMember
        'UPGRADE_NOTE: 在对对象 objNewMember 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
        objNewMember = Nothing
    End Function


    Public Sub Remove(ByRef vntIndexKey As Object)


        mCol.Remove(vntIndexKey)
    End Sub


    Public Sub New()


        mCol = New Collection
    End Sub

    Protected Overrides Sub Finalize()
        mCol = Nothing
    End Sub


    Function GetChannelObjbySKey(ByRef Skey As String) As TCPChannel
        'Dim c As TCPChannel

        GetChannelObjbySKey = mCol(Skey)
    End Function
End Class