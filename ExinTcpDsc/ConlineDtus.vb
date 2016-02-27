Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("ConlineDtus_NET.ConlineDtus")> Public Class ConlineDtus
	Implements System.Collections.IEnumerable
	
	Public Event DataReturn(ByVal PhoneNumber As String, ByVal Value As Object, ByVal length As Integer)
	'局部变量，保存集合
	Private mCol As Collection
	
	Sub informDataArrival(ByVal PhoneNumber As String, ByVal Value As Object, ByVal length As Integer)
		RaiseEvent DataReturn(PhoneNumber, Value, length)
	End Sub
    Public Function Add(ByVal LoginTime As Date, ByVal deviceno As String, ByVal SimCardNo As String, ByVal DSCportNo As String, ByVal ModuleNo As String, ByRef WinSock As MyWinSockClient, Optional ByRef sKey As String = "") As ConlineDTU
        '创建新对象
        Dim objNewMember As ConlineDTU
        objNewMember = New ConlineDTU
        Dim DTU As ConlineDTU
        For Each DTU In mCol
            If Not DTU.WinSock.TcpClnt Is Nothing Then
                If DTU.SimCardNo = SimCardNo Then
                    DTU.WinSock.TcpClnt.Close()
                    mCol.Remove(DTU.SimCardNo)

                    Exit For
                End If
            End If
            'If Not DTU.WinSock.UdpClnt Is Nothing Then
            '    If DTU.SimCardNo = SimCardNo Then
            '        DTU.WinSock.UdpClnt = Nothing
            '        mCol.Remove(DTU.SimCardNo)
            '        Exit For
            '    End If
            'End If

        Next DTU
        objNewMember.HeartBeatTime = Now

        '设置传入方法的属性
        objNewMember.LoginTime = LoginTime
        objNewMember.deviceno = deviceno
        objNewMember.SimCardNo = SimCardNo
        objNewMember.DSCportNo = DSCportNo
        objNewMember.ModuleNo = ModuleNo
        objNewMember.WinSock = WinSock
        If Len(sKey) = 0 Then
            mCol.Add(objNewMember)
        Else
            mCol.Add(objNewMember, sKey)
        End If


        '返回已创建的对象
        Add = objNewMember
        objNewMember = Nothing


    End Function
    Public Function AddNewLoginBLDTU(ByRef Registerinfo() As Byte, ByRef WinSock As MyWinSockClient) As Byte()
        Dim d() As Byte
        Dim R(0) As Byte
        d = Registerinfo
        Dim s As String = ""
        Dim i As Integer
        For i = 0 To UBound(d)
            s = s & Chr(d(i))
        Next i
        'MsgBox s
        Dim Deviceno As String
        Dim SimNo As String
        Dim DSCportNo As String
        Dim ModuleNo As String
        Deviceno = Mid(s, 3, 11)
        SimNo = Mid(s, 3, 11)
        DSCportNo = Right(s, 5)
        ModuleNo = Mid(s, 14, 15)
        Me.Add(Now, Deviceno, SimNo, DSCportNo, ModuleNo, WinSock, SimNo)
        R(0) = 31
        'AddNewLoginDTU = R
    End Function
    Public Function AddNewLoginLDDTU(ByRef Registerinfo() As Byte, ByRef WinSock As MyWinSockClient) As Byte()
        Dim d() As Byte
        Dim R(0) As Byte
        d = Registerinfo
        Dim s As String = ""
        Dim i As Integer
        For i = 0 To UBound(d)
            s = s & Chr(d(i))
        Next i
        'MsgBox s'@@13831521345IP:123.23.43.34## 蓝迪DTU
        Dim Deviceno As String
        Dim SimNo As String
        Dim DSCportNo As String
        Dim ModuleNo As String
        Deviceno = Mid(s, 3, 11)
        SimNo = Mid(s, 3, 11)
        DSCportNo = 1000
        ModuleNo = Mid(s, 6, 8)
        Me.Add(Now, Deviceno, SimNo, DSCportNo, ModuleNo, WinSock, SimNo)
        R(0) = 31
        'AddNewLoginDTU = R
    End Function
    Public Function AddNewLoginHD3GDTU(ByRef Registerinfo() As Byte, ByRef WinSock As MyWinSockClient) As Byte() '注册信息与心跳一样均为 exin13610983709 格式
        Dim d() As Byte
        Dim R(0) As Byte
        d = Registerinfo
        Dim s As String = ""
        Dim i As Integer
        For i = 0 To UBound(d)
            s = s & Chr(d(i))
        Next i
        '注册信息与心跳一样均为 exin13610983709 格式
        Dim deviceno As String
        Dim SimNo As String
        Dim DSCportNo As String
        Dim ModuleNo As String
        deviceno = Mid(s, 5, 11)
        SimNo = Mid(s, 5, 11)
        DSCportNo = 1000
        ModuleNo = Mid(s, 5, 11)
        Me.Add(Now, deviceno, SimNo, DSCportNo, ModuleNo, WinSock, SimNo)
        'R(0) = 31
        'AddNewLoginDTU = R
    End Function
    Public Function AddNewLoginHXDTU(ByRef Registerinfo() As Byte, ByRef Winsock As MyWinSockClient) As Byte()
        Dim d() As Byte
        Dim R(0) As Byte
        d = Registerinfo
        Dim s As String = ""
        Dim i As Integer
        For i = 0 To UBound(d)
            s = s & Chr(d(i))
        Next i
        Dim deviceno As String
        Dim SimNo As String
        Dim DSCportNo As String
        Dim ModuleNo As String
        deviceno = Mid(s, 1, 8)
        SimNo = Mid(s, 9, 11)
        DSCportNo = Mid(s, 20, 5)
        ModuleNo = deviceno
        Me.Add(Now, deviceno, SimNo, DSCportNo, ModuleNo, winsock, SimNo)
        R(0) = 31
        'AddNewLoginDTU = R
    End Function
    Function AddNewLoginUSRwifiDTU(ByRef Registerinfo() As Byte, ByRef Winsock As MyWinSockClient) As Byte()
        Dim d() As Byte
        Dim R(0) As Byte
        d = Registerinfo
        Dim s As String = ""
        Dim i As Integer
        'For i = 0 To UBound(d)
        '    s = s & Chr(d(i))
        'Next i
        s = HextoStr(d, 6, 0)
        '注册信息与心跳一样均为 exin13610983709 格式
        Dim deviceno As String
        Dim SimNo As String
        Dim DSCportNo As String
        Dim ModuleNo As String
        deviceno = s
        SimNo = s
        DSCportNo = 1000
        ModuleNo = s
        Me.Add(Now, deviceno, SimNo, DSCportNo, ModuleNo, Winsock, SimNo)
    End Function

    Private Function HextoStr(ByRef src() As Byte, ByRef Srclen As Short, Optional ByRef start As Integer = 0) As String
        Dim i As Short
        Dim st As String = ""
        Dim temp As Short
        Dim j As Integer
        j = Srclen - 1
        If IsNothing(start) Then
            start = 0
        End If
        For i = start To j '看src这个数组是从0开始还是从1开始,宏电的是从1开始
            temp = src(i) \ 16
            If temp > 9 Then
                temp = temp + 55
            Else
                temp = temp + 48
            End If
            st = st & Chr(temp)

            temp = src(i) Mod 16
            If temp > 9 Then
                temp = temp + 55
            Else
                temp = temp + 48
            End If
            st = st & Chr(temp)

            'st = st & " "
        Next
        HextoStr = st
    End Function
    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As ConlineDTU
        Get
            '引用集合中的一个元素时使用。
            'vntIndexKey 包含集合的索引或关键字，
            '这是为什么要声明为 Variant 的原因
            '语法：Set foo = x.Item(xyz) or Set foo = x.Item(5)
            On Error Resume Next
            Item = mCol.Item(vntIndexKey)
        End Get
    End Property



    Public ReadOnly Property Count() As Integer
        Get
            '检索集合中的元素数时使用。语法：Debug.Print x.Count
            Count = mCol.Count()
        End Get
    End Property

    Public Function Containsobj(ByVal key As String) As Boolean
        Containsobj = mCol.Contains(key)
    End Function


    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
        GetEnumerator = mCol.GetEnumerator
    End Function


    Public Sub Remove(ByRef vntIndexKey As Object)
        '删除集合中的元素时使用。
        'vntIndexKey 包含索引或关键字，这是为什么要声明为 Variant 的原因
        '语法：x.Remove(xyz)


        mCol.Remove(vntIndexKey)
    End Sub


    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        '创建类后创建集合
        mCol = New Collection
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub


    'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Terminate_Renamed()
        '类终止后破坏集合
        'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mCol = Nothing
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub
End Class