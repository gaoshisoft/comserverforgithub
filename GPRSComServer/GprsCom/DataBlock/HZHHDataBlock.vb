Imports MBsrv
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility.VB6
Imports Microsoft.VisualBasic.CompilerServices
Imports System


Friend Class HZHHDataBlock
    Implements IDataBlock

    Private mvarblockName As String

    Private mvarAD As Byte

    Private mvarFC As Byte

    Private mvarLength As Integer

    Private mvarEnable As Boolean

    Private mvarStartAD As String

    Public Sendtime As Integer

    Public LjllXS As Double

    Public BKSS As Double

    Public BKLJ As Double

    Public YL As Double

    Public WD As Double

    Public LLJState As Double

    Public GKLJ As Double

    Public GKSS As Double

    Public KzqZLL As Double

    Public KzqSYJE As Double

    Public KzqGLJE As Double

    Public KzqFCCS As Double

    Public KzqFKCS As Double

    Public kzqFJCS As Double

    Public KzqZT As Double

    Private msvrAddrlength As Integer

    Private msvrDevAd As Integer

    Private mSvrMBADstart As String

    Public Property startAD() As String Implements IDataBlock.startAd
        Get
            Return Me.mvarStartAD
        End Get
        Set(ByVal value As String)
            Me.mvarStartAD = value
        End Set
    End Property

    Public Property Enable() As Boolean Implements IDataBlock.Enable
        Get
            Return Me.mvarEnable
        End Get
        Set(ByVal value As Boolean)
            Me.mvarEnable = value
        End Set
    End Property

    Public Property Length() As Integer Implements IDataBlock.Length
        Get
            Return Me.mvarLength
        End Get
        Set(ByVal value As Integer)
            Me.mvarLength = value
        End Set
    End Property

    Public ReadOnly Property FC() As Byte
        Get
            Dim left As String = Strings.Left(Me.startAD, 1)
            Dim result As Byte
            If Operators.CompareString(left, "0", False) = 0 Then
                result = 1
            Else
                If Operators.CompareString(left, "1", False) = 0 Then
                    result = 2
                Else
                    If Operators.CompareString(left, "3", False) = 0 Then
                        result = 4
                    Else
                        If Operators.CompareString(left, "4", False) = 0 Then
                            result = 3
                        End If
                    End If
                End If
            End If
            Return result
        End Get
    End Property

    Public Property Addr() As Integer Implements IDataBlock.Addr
        Get
            Return CInt(Me.mvarAD)
        End Get
        Set(ByVal value As Integer)
            ' The following expression was wrapped in a checked-expression
            Me.mvarAD = CByte(value)
        End Set
    End Property

    Public Property BlockName() As String Implements IDataBlock.BlockName
        Get
            Return Me.mvarblockName
        End Get
        Set(ByVal value As String)
            Me.mvarblockName = value
        End Set
    End Property

    Public Property SvrAddrLength() As Integer Implements IDataBlock.SvrAddrLength
        Get
            Return Me.msvrAddrlength
        End Get
        Set(ByVal value As Integer)
            Me.msvrAddrlength = value
        End Set
    End Property

    Public Property SvrDevAd() As Integer Implements IDataBlock.SvrDevAd
        Get
            Return Me.msvrDevAd
        End Get
        Set(ByVal value As Integer)
            Me.msvrDevAd = value
        End Set
    End Property

    Public Property SvrMBADStart() As Integer Implements IDataBlock.SvrMBADStart
        Get
            Return Conversions.ToInteger(Me.mSvrMBADstart)
        End Get
        Set(ByVal value As Integer)
            Me.mSvrMBADstart = Conversions.ToString(value)
        End Set
    End Property

    Public Property ParaAddr() As String Implements IDataBlock.ParaAddr
        Get
            ' The following expression was wrapped in a checked-expression
            Return _
                String.Concat(
                    ChrW(26477) & ChrW(24030) & ChrW(40511) & ChrW(40516) & "IC" & ChrW(21345) & ChrW(25511) &
                    ChrW(21046) & ChrW(22120) & "Modbus TCP server " & ChrW(22320) & ChrW(22336) & ChrW(65306),
                    Conversions.ToString(Me.SvrDevAd),
                    " " & ChrW(24037) & ChrW(20917) & ChrW(32047) & ChrW(35745) & ChrW(65306),
                    Conversions.ToString(400000 + Me.SvrMBADStart + 1),
                    " " & ChrW(26631) & ChrW(20917) & ChrW(32047) & ChrW(35745) & ChrW(65306),
                    Conversions.ToString(400000 + Me.SvrMBADStart + 3),
                    " " & ChrW(24037) & ChrW(20917) & ChrW(30636) & ChrW(26102) & ChrW(65306),
                    Conversions.ToString(400000 + Me.SvrMBADStart + 5),
                    " " & ChrW(26631) & ChrW(20917) & ChrW(30636) & ChrW(26102) & ChrW(65306),
                    Conversions.ToString(400000 + Me.SvrMBADStart + 7), " " & ChrW(28201) & ChrW(24230) & ChrW(65306),
                    Conversions.ToString(400000 + Me.SvrMBADStart + 9), " " & ChrW(21387) & ChrW(21147) & ChrW(65306),
                    Conversions.ToString(400000 + Me.SvrMBADStart + 11),
                    " " & ChrW(27969) & ChrW(37327) & ChrW(35745) & ChrW(29366) & ChrW(24577) & ChrW(65306),
                    Conversions.ToString(400000 + Me.SvrMBADStart + 13),
                    " " & ChrW(25511) & ChrW(21046) & ChrW(22120) & ChrW(24635) & ChrW(27969) & ChrW(37327) &
                    ChrW(65306), Conversions.ToString(400000 + Me.SvrMBADStart + 15),
                    " " & ChrW(25511) & ChrW(21046) & ChrW(22120) & ChrW(21097) & ChrW(20313) & ChrW(37329) &
                    ChrW(39069) & ChrW(65306), Conversions.ToString(400000 + Me.SvrMBADStart + 17),
                    " " & ChrW(25511) & ChrW(21046) & ChrW(22120) & ChrW(36807) & ChrW(38646) & ChrW(37329) &
                    ChrW(39069) & ChrW(65306), Conversions.ToString(400000 + Me.SvrMBADStart + 19),
                    " " & ChrW(25511) & ChrW(21046) & ChrW(22120) & ChrW(38450) & ChrW(30913) & ChrW(27425) &
                    ChrW(25968) & ChrW(65306), Conversions.ToString(400000 + Me.SvrMBADStart + 21),
                    " " & ChrW(25511) & ChrW(21046) & ChrW(22120) & ChrW(38450) & ChrW(21345) & ChrW(27425) &
                    ChrW(25968) & ChrW(65306), Conversions.ToString(400000 + Me.SvrMBADStart + 23),
                    " " & ChrW(25511) & ChrW(21046) & ChrW(22120) & ChrW(38450) & ChrW(21098) & ChrW(27425) &
                    ChrW(25968) & ChrW(65306), Conversions.ToString(400000 + Me.SvrMBADStart + 25),
                    " " & ChrW(25511) & ChrW(21046) & ChrW(22120) & ChrW(29366) & ChrW(24577) & ChrW(65306),
                    Conversions.ToString(400000 + Me.SvrMBADStart + 27))
        End Get
        Set(ByVal value As String)
        End Set
    End Property

    Private Function GetCNFloat(ByVal V As Byte(), ByVal start As Integer) As Single
        Dim num As Short = 4
        Dim str As String = MdlFuncTools.HextoStr(V, num, start)
        Dim num2 As Single = CSng(Conversion.Val("&H" + Strings.Left(str, 2)))
        If num2 > 128.0F Then
            num2 -= 256.0F
        End If
        Dim num3 As Single = CSng(Math.Pow(2.0, CDec(num2)))
        If Conversion.Val("&H" + Strings.Left(str, 2)) = 0.0 Then
            num3 = 0.0F
        End If
        Dim num4 As Double = Conversion.Val("&H" + Strings.Right(str, 6))
        Dim num5 As Double = (Conversion.Val("&HFFFFFF") + 1.0)/2.0
        If num4 > num5 Then
            num4 = num4 - Conversion.Val("&HFFFFFF") - 1.0
        End If
        num4 /= Conversion.Val("&H800000")
        Return CSng((CDec(num3)*num4))
    End Function


    Private Function GetHonghuUint32Value(ByVal V As Byte(), ByVal start As Short, ByVal ifSwap As Boolean) As UInteger
        Dim array As Byte()
        ReDim array(4)

        Dim array2 As Byte()
        ReDim array2(4)
        Dim array3 As Byte()
        ReDim array3(4)
        Dim num As Long = 0L
        ' The following expression was wrapped in a checked-statement
        Do
            ' The following expression was wrapped in a unchecked-expression
            array(CInt(num)) = V(CInt((CLng(start) + num + 2L)))
            num += 1L
        Loop While num <= 3L
        If ifSwap Then
            array2(0) = array(1)
            array2(1) = array(0)
            array2(2) = array(3)
            array2(3) = array(2)
        Else
            array2(0) = array(3)
            array2(1) = array(2)
            array2(2) = array(1)
            array2(3) = array(0)
        End If
        Dim num2 As Long = CLng(CULng(BitConverter.ToUInt32(array2, 0)))
        array3(0) = V(CInt((start + 6)))
        array3(1) = V(CInt((start + 7)))
        Dim num3 As Long = CLng(CULng(BitConverter.ToUInt16(array3, 0)))
        Return CUInt(Math.Round(CDec(num3)*4294967296.0 + CDec(num2)))
    End Function

    Private Function GetFloatValueByad(ByVal byteAdr As Integer, ByVal Swapword As Boolean, ByVal Rv As Byte()) _
        As Single
        Dim array As Byte()
        ReDim array(4)
        ' The following expression was wrapped in a checked-statement
        If Not Swapword Then
            array(1) = Rv(byteAdr)
            array(0) = Rv(byteAdr + 1)
            array(3) = Rv(byteAdr + 2)
            array(2) = Rv(byteAdr + 3)
        Else
            array(3) = Rv(byteAdr)
            array(2) = Rv(byteAdr + 1)
            array(1) = Rv(byteAdr + 2)
            array(0) = Rv(byteAdr + 3)
        End If
        Return BitConverter.ToSingle(array, 0)
    End Function

    Public Function GetCommandBytes() As Object Implements IDataBlock.GetCommandBytes
        Me.startAD = "400001"
        Me.Length = 28
        ' The following expression was wrapped in a checked-statement
        Dim array As Byte()
        Select Case Me.FC
            Case 1, 2, 3, 4, 15, 16
                ReDim array(8)

                ' The following expression was wrapped in a checked-expression
                Dim array2 As Byte() =
                        {CByte(Me.Addr), 3,
                         CByte(
                             (CLng(
                                 Math.Round(Conversion.Val(Strings.Right(Me.startAD, Strings.Len(Me.startAD) - 1)) - 1.0))/
                              256L)),
                         CByte(
                             Math.Round(
                                 Conversion.Val(Strings.Right(Me.startAD, Strings.Len(Me.startAD) - 1)) - 1.0 Mod 256.0)),
                         CByte((Me.Length/256)), CByte((Me.Length Mod 256))}
                array(0) = CByte(Me.Addr)
                array(1) = 3
                ' The following expression was wrapped in a checked-expression
                array(2) =
                    CByte(
                        (CLng(Math.Round(Conversion.Val(Strings.Right(Me.startAD, Strings.Len(Me.startAD) - 1)) - 1.0))/
                         256L))
                ' The following expression was wrapped in a checked-expression
                array(3) =
                    CByte(
                        Math.Round(
                            Conversion.Val(Strings.Right(Me.startAD, Strings.Len(Me.startAD) - 1)) - 1.0 Mod 256.0))
                array(4) = CByte((Me.Length/256))
                array(5) = CByte((Me.Length Mod 256))
                array(6) = CRC16(array2)(1)
                array(7) = CRC16(array2)(0)
        End Select
        Return array
    End Function

    Public Sub AddItm(ByVal ItmName As String) Implements IDataBlock.AddItm
    End Sub

    Public Function GetValueFromRvData1(ByVal length As Integer, ByVal Rvdata() As Byte) As Boolean _
        Implements IDataBlock.GetValueFromRvData
        Dim StartByteAd As Object
        Dim i, j As Integer
        'Dim Rvdata As ModemDataStruct
        On Error Resume Next
        Dim ByteLen As Short
        Dim Rightlength As Short
        Dim Dt As HZHHDataBlock
        Dt = Me

        Select Case Me.FC
            Case 3, 4
                Rightlength = 5 + Me.Length*2
            Case 1, 2
                Rightlength = 5 + IIf(Me.Length Mod 8 = 0, Me.Length\8, Me.Length\8 + 1)
            Case 15, 16
                Rightlength = 8
        End Select
        If length < Rightlength Then '因为是TCP面向连接的通讯是可靠连接所以此处错误只能是modbus server的返回数据不对
            GetValueFromRvData1 = False
            Exit Function
        End If
        If length > Rightlength And (length Mod Rightlength <> 0) Then '因为是TCP面向连接的通讯是可靠连接所以此处错误只能是modbus server的返回数据不对
            GetValueFromRvData1 = False
            Exit Function
        End If
        Dim F As Byte
        F = Rvdata(1)
        If F <> Me.FC Then
            GetValueFromRvData1 = False
            Exit Function
        End If

        Select Case Me.FC
            Case 3

                'For i = 3 To length - 3 '去掉地址码，功能码，长度,校验

                'StartByteAd = CDbl(me.SvrMBADStart) * 2

                Me.GKLJ = Me.GetHonghuUint32Value(Rvdata, 3, False)
                Me.BKLJ = Me.GetHonghuUint32Value(Rvdata, 11, False)
                Me.GKSS = CDec(Me.GetFloatValueByad(19, False, Rvdata))
                Me.BKSS = CDec(Me.GetFloatValueByad(23, False, Rvdata))
                Me.WD = CDec(Me.GetFloatValueByad(27, False, Rvdata))
                Me.YL = CDec(Me.GetFloatValueByad(31, False, Rvdata))
                Me.LLJState = CDec(Rvdata(35))
                Me.KzqZLL = Conversion.Val(String.Concat(Rvdata(37).ToString("X2"), Rvdata(38).ToString("X2"),
                                                         Rvdata(39).ToString("X2"), Rvdata(40).ToString("X2"),
                                                         Rvdata(41).ToString("X2"), Rvdata(42).ToString("X2")))

                Me.KzqSYJE =
                    Conversion.Val(String.Concat(Rvdata(43).ToString("X2"), Rvdata(44).ToString("X2"),
                                                 Rvdata(45).ToString("X2"), Rvdata(46).ToString("X2"),
                                                 Rvdata(47).ToString("X2"), Rvdata(48).ToString("X2")))/100.0

                Me.KzqGLJE =
                    Conversion.Val(String.Concat(Rvdata(49).ToString("X2"), Rvdata(50).ToString("X2"),
                                                 Rvdata(51).ToString("X2"), Rvdata(52).ToString("X2"),
                                                 Rvdata(53).ToString("X2"), Rvdata(54).ToString("X2")))/100.0
                Me.KzqFCCS = CDec(Rvdata(55))
                Me.KzqFKCS = CDec(Rvdata(56))
                Me.kzqFJCS = CDec(Rvdata(57))
                Me.KzqZT = CDec(Rvdata(58))
                Moddeclare.Mbs.WritevaluebyAd(Me.SvrDevAd, Conversions.ToString(400000 + Me.SvrMBADStart + 1),
                                              Device.Datatype.浮点数, Me.GKLJ)
                Moddeclare.Mbs.WritevaluebyAd(Me.SvrDevAd, Conversions.ToString(400000 + Me.SvrMBADStart + 3),
                                              Device.Datatype.浮点数, Me.BKLJ)
                Moddeclare.Mbs.WritevaluebyAd(Me.SvrDevAd, Conversions.ToString(400000 + Me.SvrMBADStart + 5),
                                              Device.Datatype.浮点数, Me.GKSS)
                Moddeclare.Mbs.WritevaluebyAd(Me.SvrDevAd, Conversions.ToString(400000 + Me.SvrMBADStart + 7),
                                              Device.Datatype.浮点数, Me.BKSS)
                Moddeclare.Mbs.WritevaluebyAd(Me.SvrDevAd, Conversions.ToString(400000 + Me.SvrMBADStart + 9),
                                              Device.Datatype.浮点数, Me.WD)
                Moddeclare.Mbs.WritevaluebyAd(Me.SvrDevAd, Conversions.ToString(400000 + Me.SvrMBADStart + 11),
                                              Device.Datatype.浮点数, Me.YL)
                Moddeclare.Mbs.WritevaluebyAd(Me.SvrDevAd, Conversions.ToString(400000 + Me.SvrMBADStart + 13),
                                              Device.Datatype.浮点数, Me.LLJState)
                Moddeclare.Mbs.WritevaluebyAd(Me.SvrDevAd, Conversions.ToString(400000 + Me.SvrMBADStart + 15),
                                              Device.Datatype.浮点数, Me.KzqZLL)
                Moddeclare.Mbs.WritevaluebyAd(Me.SvrDevAd, Conversions.ToString(400000 + Me.SvrMBADStart + 17),
                                              Device.Datatype.浮点数, Me.KzqSYJE)
                Moddeclare.Mbs.WritevaluebyAd(Me.SvrDevAd, Conversions.ToString(400000 + Me.SvrMBADStart + 19),
                                              Device.Datatype.浮点数, Me.KzqGLJE)
                Moddeclare.Mbs.WritevaluebyAd(Me.SvrDevAd, Conversions.ToString(400000 + Me.SvrMBADStart + 21),
                                              Device.Datatype.浮点数, Me.KzqFCCS)
                Moddeclare.Mbs.WritevaluebyAd(Me.SvrDevAd, Conversions.ToString(400000 + Me.SvrMBADStart + 23),
                                              Device.Datatype.浮点数, Me.KzqFKCS)
                Moddeclare.Mbs.WritevaluebyAd(Me.SvrDevAd, Conversions.ToString(400000 + Me.SvrMBADStart + 25),
                                              Device.Datatype.浮点数, Me.kzqFJCS)
                Moddeclare.Mbs.WritevaluebyAd(Me.SvrDevAd, Conversions.ToString(400000 + Me.SvrMBADStart + 27),
                                              Device.Datatype.浮点数, Me.KzqZT)
        End Select

        GetValueFromRvData1 = True
    End Function
End Class


