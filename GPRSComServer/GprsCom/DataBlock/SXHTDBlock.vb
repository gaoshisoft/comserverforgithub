Imports MBsrv


Public Class SXHTDataBlock '陕西航天
    Implements IDataBlock

    Public SSLL As Double
    Public LJLL As Double
    Public YL As Double
    Public WD As Double
    Public YBstate As Double
    Public Fltvalue As Double
    Public GKSSLL As Double

    Private mMeterAddr As Integer

    Private mBlockName As String
    Private mEnable As Boolean
    Private mSvrLen As Integer
    Private mSvrDevAd As Integer
    Private mSvrMBADStart As Integer


    Private Function CheckSum(ByVal V() As Byte) As Byte
        'Dim V() As Byte
        Dim v1 As Long
        For i As Integer = 0 To UBound(V)


            v1 = v1 + V(i)
        Next i
        CheckSum = (v1 Mod 256)
    End Function

    Private Function CheckSum(ByVal V() As Byte, ByVal start As Int16, ByVal len As Int16) As Byte
        Dim v1 As Long
        Dim E As Int16
        E = start + len
        For i As Integer = start To E - 1


            v1 = v1 + V(i)
        Next i
        CheckSum = (v1 Mod 256)
    End Function


    Public Property Addr() As Integer Implements IDataBlock.Addr
        Get
            Addr = mMeterAddr
        End Get
        Set(ByVal value As Integer)
            mMeterAddr = value
        End Set
    End Property

    Public Property BlockName() As String Implements IDataBlock.BlockName
        Get
            BlockName = mBlockName
        End Get
        Set(ByVal value As String)
            mBlockName = value
        End Set
    End Property

    Public Property Enable() As Boolean Implements IDataBlock.Enable
        Get
            Enable = mEnable
        End Get
        Set(ByVal value As Boolean)
            mEnable = value
        End Set
    End Property

    Public Function GetCommandBytes() As Object Implements IDataBlock.GetCommandBytes

        Dim s As String
        Dim V() As Byte
        s = "3C 01 0B 00 48 3E"
        Dim s1() As String
        s1 = Split(s, " ")
        ReDim V(UBound(s1))
        Dim i As Long
        For i = 0 To s1.GetUpperBound(0)
            V(i) = Val("&H" & s1(i))
        Next i
        Dim V1() As Byte
        ReDim V1(UBound(V) - 2)
        For i = 0 To V1.GetUpperBound(0)
            V1(i) = V(i)

        Next
        V1(1) = Me.mMeterAddr
        Dim ChkSum As Byte
        ChkSum = CheckSum(V1)
        V(4) = ChkSum
        V(1) = Me.mMeterAddr
        GetCommandBytes = V
    End Function

    Public Function GetValueFromRvData(ByVal L As Integer, ByVal v0() As Byte) As Boolean _
        Implements IDataBlock.GetValueFromRvData
        '因DTU返回的数据有问题，需要先对收到的数据进行处理。通过观察可知，基本每次收到的数据都有 0B 14 ,所以用0B14做为接收标志，先找到0B14
        Dim i As Integer
        Dim v() As Byte
        Dim iffound As Boolean = False
        Dim position0b As Integer
        For i = 0 To UBound(v0) - 1
            If v0(i) = &HB And v0(i + 1) = &H14 Then
                position0b = i
                iffound = True
            End If
        Next
        If position0b = 2 Then '在正确的位置找到
            v = v0
        Else '重新构造数据组
            ReDim v(25)
            v(0) = &H3C
            v(1) = &H1
            For i = 2 To 25
                v(i) = v0(position0b + i - 2)
            Next
        End If
        If v0.GetUpperBound(0) >= 23 And iffound Then

            '3C 01 0B 14 90 00 23 00 23 01 0A 1C 00 0D 4D 00 32 01 00 2C DF 04 00 06 FB 3E
            '3C 01 0B 14 90 00   // 23 00 / 23 01 0A / 1C 00 0D / 4D 00 32/ 01 00 2C DF 04 00 06  //FB 3E 温度 、压力、 工况瞬时、标况瞬时 累计流量
            'If v0.GetUpperBound(0) >= 25 And v(0) = Val(&H3C) And v(1) = Me.mMeterAddr And CheckSum(v, 0, 24) = v(24) Then

            Dim v1(3) As Byte

            '----------------
            Me.WD = v(6) + v(7)/100
            Me.YL = v(9)*256 + v(8) + v(10)/100
            Me.GKSSLL = v(12)*256 + v(11) + v(13)/100
            Me.SSLL = v(15)*256 + v(14) + v(16)/100
            Me.LJLL = (v(18)*256 + v(17))*1000000 + GetInvertUint32Value(v, 19) + v(23)/100

            Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 1), Device.Datatype.浮点数, Me.GKSSLL)

            Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 3), Device.Datatype.浮点数, Me.SSLL)

            Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 5), Device.Datatype.浮点数, Me.WD)

            Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 7), Device.Datatype.浮点数, Me.YL)

            Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 9), Device.Datatype.浮点数, Me.LJLL)


            GetValueFromRvData = True

        Else
            GetValueFromRvData = False

        End If
    End Function

    Function GetInvertUint32Value(ByVal V As Byte(), ByVal start As Int16) As UInt32
        Dim V1(3) As Byte, V2(3) As Byte

        Dim i As Long
        For i = 0 To 3
            V1(i) = V(start + i)
        Next i

        GetInvertUint32Value = BitConverter.ToUInt32(V1, 0)
    End Function

    Public Property SvrAddrLength() As Integer Implements IDataBlock.SvrAddrLength
        Get
            SvrAddrLength = mSvrLen
        End Get
        Set(ByVal value As Integer)
            mSvrLen = value
        End Set
    End Property

    Public Property SvrDevAd() As Integer Implements IDataBlock.SvrDevAd
        Get
            SvrDevAd = mSvrDevAd
        End Get
        Set(ByVal value As Integer)
            mSvrDevAd = value
        End Set
    End Property

    Public Property SvrMBADStart() As Integer Implements IDataBlock.SvrMBADStart
        Get
            SvrMBADStart = mSvrMBADStart
        End Get
        Set(ByVal value As Integer)
            mSvrMBADStart = value
        End Set
    End Property


    Public Sub AddItm(ByVal ItmName As String) Implements IDataBlock.AddItm
    End Sub

    Public Property Length() As Integer Implements IDataBlock.Length
        Get
        End Get
        Set(ByVal value As Integer)
        End Set
    End Property

    Public Property startAd() As String Implements IDataBlock.startAd
        Get
        End Get
        Set(ByVal value As String)
        End Set
    End Property

    Public Property ParaAddr As String Implements IDataBlock.ParaAddr
        Get

            Return _
                "陕西航天能源Modbus TCP server ID:" & CStr(Me.SvrDevAd) & " " & " 参数地址  工况瞬时:" &
                CStr(400000 + Me.SvrMBADStart + 1) & " 标况瞬时：" & CStr(400000 + Me.SvrMBADStart + 3) &
                " 温度:" & CStr(400000 + Me.SvrMBADStart + 5) & " 压力：" & CStr(400000 + Me.SvrMBADStart + 7) & " 累计流量：" &
                CStr(400000 + Me.SvrMBADStart + 9)
        End Get
        Set(ByVal value As String)
        End Set
    End Property
End Class


