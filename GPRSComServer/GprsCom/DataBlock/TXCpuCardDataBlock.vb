Imports MBsrv

Public Class TXCpuCardDataBlock
    Implements IDataBlock

    Public BKZL As Double
    Public YULIANG As Double
    Public BKLL As Double
    Public GKLL As Double
    Public WD As Double
    Public YL As Double
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


    Private Function GetTXFloat(ByVal V() As Byte, ByVal start As Integer) As Single
        Dim S As String
        S = HextoStr(V, 4, start)
        Dim F1 As Single
        Dim F2 As Double
        Dim ZS As Single
        Dim F3 As Double
        ZS = Val("&H" & Left(S, 2))
        If ZS > 128 Then
            ZS = ZS - 256
        End If
        F1 = 2^ZS

        F2 = Val("&H" & Right(S, 6))
        F3 = (Val("&H" & "FFFFFF") + 1)/2
        If F2 > F3 Then
            F2 = F2 - Val("&H" & "FFFFFF") - 1
        End If
        F2 = F2/Val("&H" & "800000")
        If Val("&H" & Left(S, 2)) = 0 Then
            F1 = 0
        End If
        GetTXFloat = F1*F2
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
        'Function GetCommand() As Byte()

        'CC 02 31 FF EE
        '序号	数据意义	字节数	字节顺序	数据内容（十六进制）
        '1	起始符	1	1	CC
        '2	子机号	1	2	00～99(BCD码)
        '3	功能码	1	3	见表2
        '4	字节数	0/2	3/（4-5）	数据域字节数，当命令回传时字节数为0，设置参数时为2
        '5	数据域	0～n字节		设置参数时的数据
        '6	校验和	1	4（6）+n	前面所有字节的和（忽略溢出）
        '7	结束符	1	5（7）+n	EE

        Dim s As String
        Dim V() As Byte
        s = "CC 02 31 FF EE"
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
        V(3) = ChkSum
        V(1) = Me.mMeterAddr
        GetCommandBytes = V
    End Function

    Public Function GetValueFromRvData(ByVal L As Integer, ByVal v() As Byte) As Boolean _
        Implements IDataBlock.GetValueFromRvData
        'Sub LetValue(ByVal v() As Byte)
        ' DD 02 31 00 1D 00 00 0C 54 38 01 00 00 00 00 05 B8 00 00 00 00 00 00 00 00 06 43 B9 00 07 68 35 00 02 2B FF
        'If v.GetUpperBound(0) >= 35 And v(0) = 204 And v(1) = Me.mMeterAddr Then
        If L = 35 And v(0) = Me.mMeterAddr Then '有时会发生少传第一个字节的情况，但其它数据是正确的,这样第一个字节就是地址了
            '可通过将其整体向右移一个字节来解决
            Dim tempv() As Byte
            ReDim tempv(35)

            For j As Integer = 1 To 35
                tempv(j) = v(j - 1)
            Next
            ReDim Preserve v(35)
            Buffer.BlockCopy(tempv, 0, v, 0, 36)
            v(0) = &H33
            L = 36
        End If
        If L >= 35 And v(1) = Me.mMeterAddr Then 'v.length=36

            Dim v1(3) As Byte

            Dim IntPartLJll As Long
            'IntPartLJll = v(16) * 100 + v(17)
            Dim IntpStr As String
            IntpStr = BitConverter.ToString(v, 5, 2)
            IntPartLJll = Val(Left(IntpStr, 2))*100 + Val(Right(IntpStr, 2))
            Dim i As Integer
            For i = 0 To 3
                v1(i) = v(7 + i)

            Next
            Me.BKZL = GetTXFloat(v1, 0)
            Me.BKZL = Me.BKZL + IntPartLJll*10^6
            Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 1), Device.Datatype.浮点数, Me.BKZL)
            For i = 0 To 3
                v1(i) = v(13 + i)


            Next
            Me.YULIANG = v1(3) + v1(2)*256 + v1(1)*65536 + v1(0)*16777216
            If v(11) = 1 Then
                Me.YULIANG = 0 - Me.YULIANG
            End If


            Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 3), Device.Datatype.浮点数, Me.YULIANG)

            For i = 0 To 3
                v1(i) = v(17 + i)

            Next
            Me.BKLL = GetTXFloat(v1, 0)
            Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 5), Device.Datatype.浮点数, Me.BKLL)
            For i = 0 To 3
                v1(i) = v(21 + i)

            Next
            Me.GKLL = GetTXFloat(v1, 0)
            Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 7), Device.Datatype.浮点数, Me.GKLL)
            For i = 0 To 3
                v1(i) = v(25 + i)

            Next
            Me.WD = GetTXFloat(v1, 0)
            Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 9), Device.Datatype.浮点数, Me.WD)
            For i = 0 To 3
                v1(i) = v(29 + i)

            Next
            Me.YL = GetTXFloat(v1, 0)
            Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 11), Device.Datatype.浮点数, Me.YL)
            GetValueFromRvData = True

        Else
            GetValueFromRvData = False

        End If
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
            ParaAddr = " 总量：" & CStr(400000 + Me.SvrMBADStart + 1) &
                       " 余量(浮点)：" & CStr(400000 + Me.SvrMBADStart + 3) &
                       " 标况流量：" & CStr(400000 + Me.SvrMBADStart + 5) &
                       " 工况流量：" & CStr(400000 + Me.SvrMBADStart + 7) &
                       " 温度：" & CStr(400000 + Me.SvrMBADStart + 9) &
                       " 压力：" & CStr(400000 + Me.SvrMBADStart + 11)
        End Get
        Set(ByVal value As String)
        End Set
    End Property
End Class


