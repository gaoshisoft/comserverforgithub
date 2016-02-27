Imports MBsrv
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System


Public Class HZCDataBlock
    Implements IDataBlock

    Public BKLJ As Double

    Public YULIANG As Double

    Public BKSS As Double

    Public GKLL As Double

    Public WD As Double

    Public YL As Double

    Private mMeterAddr As Integer

    Private mBlockName As String

    Private mEnable As Boolean

    Private mSvrLen As Integer

    Private mSvrDevAd As Integer

    Private mSvrMBADStart As Integer
    Private mvarLength As Integer
    Private mvarStartAD As String

    Public Property Addr() As Integer Implements IDataBlock.Addr
        Get
            Return Me.mMeterAddr
        End Get
        Set(ByVal value As Integer)
            Me.mMeterAddr = value
        End Set
    End Property

    Public Property BlockName() As String Implements IDataBlock.BlockName
        Get
            Return Me.mBlockName
        End Get
        Set(ByVal value As String)
            Me.mBlockName = value
        End Set
    End Property

    Public Property Enable() As Boolean Implements IDataBlock.Enable
        Get
            Return Me.mEnable
        End Get
        Set(ByVal value As Boolean)
            Me.mEnable = value
        End Set
    End Property

    Public Property SvrAddrLength() As Integer Implements IDataBlock.SvrAddrLength
        Get
            Return Me.mSvrLen
        End Get
        Set(ByVal value As Integer)
            Me.mSvrLen = value
        End Set
    End Property

    Public Property SvrDevAd() As Integer Implements IDataBlock.SvrDevAd
        Get
            Return Me.mSvrDevAd
        End Get
        Set(ByVal value As Integer)
            Me.mSvrDevAd = value
        End Set
    End Property

    Public Property SvrMBADStart() As Integer Implements IDataBlock.SvrMBADStart
        Get
            Return Me.mSvrMBADStart
        End Get
        Set(ByVal value As Integer)
            Me.mSvrMBADStart = value
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


    Public Property startAD() As String Implements IDataBlock.startAd
        Get
            Return Me.mvarStartAD
        End Get
        Set(ByVal value As String)
            Me.mvarStartAD = value
        End Set
    End Property

    Public Property ParaAddr() As String Implements IDataBlock.ParaAddr
        Get
            ' The following expression was wrapped in a checked-expression
            Return _
                String.Concat(
                    ChrW(21704) & ChrW(23572) & ChrW(28392) & ChrW(20013) & ChrW(36784) & ChrW(27969) & ChrW(37327) &
                    ChrW(35745) & "Modbus TCP server " & ChrW(22320) & ChrW(22336) & ChrW(65306),
                    Conversions.ToString(Me.SvrDevAd),
                    " " & ChrW(32047) & ChrW(35745) & ChrW(27969) & ChrW(37327) & ChrW(65306),
                    Conversions.ToString(400000 + Me.SvrMBADStart + 1),
                    " " & ChrW(26631) & ChrW(20917) & ChrW(30636) & ChrW(26102) & ChrW(65306),
                    Conversions.ToString(400000 + Me.SvrMBADStart + 3), " " & ChrW(28201) & ChrW(24230) & ChrW(65306),
                    Conversions.ToString(400000 + Me.SvrMBADStart + 5),
                    " " & ChrW(21387) & ChrW(21147) & ChrW(65288) & "Kpa" & ChrW(65289) & ChrW(65306),
                    Conversions.ToString(400000 + Me.SvrMBADStart + 7))
        End Get
        Set(ByVal value As String)
        End Set
    End Property

    Private Function CheckSum(ByVal V As Byte()) As Byte
        Dim arg_09_0 As Integer = 0
        Dim num As Integer = Information.UBound(V, 1)
        ' The following expression was wrapped in a checked-statement
        Dim num2 As Long
        For i As Integer = arg_09_0 To num
            ' The following expression was wrapped in a unchecked-expression
            num2 += CLng(CULng(V(i)))
        Next
        Return CByte((num2 Mod 256L))
    End Function

    Public Function GetCommandBytes() As Object Implements IDataBlock.GetCommandBytes
        Dim array(7) As Byte
        Dim array2(4) As Byte
        ' The following expression was wrapped in a checked-statement
        array2(0) = CByte(Me.mMeterAddr)
        array2(1) = CByte(Math.Round(Conversion.Val(44)))
        array2(2) = 0
        array2(3) = Me.CheckSum(array2)
        Dim arg_68_0 As String = ":"
        Dim num As Short = 4
        Dim num2 As Integer = 0
        Dim text As String = arg_68_0 + MdlFuncTools.HextoStr(array2, num, num2) + vbCrLf
        array(0) = 58
        Dim num3 As Long = 0L
        Do
            array(CInt((num3 + 1L))) = array2(CInt(num3))
            num3 += 1L
        Loop While num3 <= 3L
        array(5) = 13
        array(6) = 10
        Dim array3() As Byte
        ReDim array3(text.Length - 1 + 1)
        Dim arg_BA_0 As Long = 0L
        Dim num4 As Long = CLng(array3.GetUpperBound(0))
        For num5 As Long = arg_BA_0 To num4
            array3(CInt(num5)) = CByte(Strings.Asc(Strings.Mid(text, CInt((num5 + 1L)), 1)))
        Next
        Return array3
    End Function

    Public Function GetValueFromRvData(ByVal L As Integer, ByVal v As Byte()) As Boolean _
        Implements IDataBlock.GetValueFromRvData
        Dim result As Boolean
        If CInt(v(0)) <> Me.Addr Then
            result = False
        Else
            If L >= 79 And v(0) = 58 Then
                Dim array(4) As Byte
                Dim text As String = ""
                Dim arg_40_0 As Long = 0L
                Dim num As Long = CLng(v.GetUpperBound(0))
                ' The following expression was wrapped in a checked-statement
                For num2 As Long = arg_40_0 To num
                    text += Conversions.ToString(Strings.Chr(CInt(v(CInt(num2)))))
                Next
                Dim text2 As String = text.Substring(33, 8)
                Me.BKLJ = Conversions.ToUInteger("&H" + text2)
                Me.BKLJ /= 10.0
                Moddeclare.Mbs.WritevaluebyAd(Me.SvrDevAd, Conversions.ToString(400000 + Me.SvrMBADStart + 1),
                                              Device.Datatype.浮点数, Me.BKLJ)
                text2 = text.Substring(41, 8)
                Me.BKSS = Conversion.Val("&H" + text2)
                Me.BKSS /= 10.0
                Moddeclare.Mbs.WritevaluebyAd(Me.SvrDevAd, Conversions.ToString(400000 + Me.SvrMBADStart + 3),
                                              Device.Datatype.浮点数, Me.BKSS)
                text2 = text.Substring(49, 4)
                Dim array2() As Byte = {0, CByte(Math.Round(Conversion.Val("&H" + text2.Substring(0, 2))))}
                array2(0) = CByte(Math.Round(Conversion.Val("&H" + text2.Substring(2, 2))))
                Me.WD = CDec(BitConverter.ToInt16(array2, 0))
                Me.WD /= 10.0
                Moddeclare.Mbs.WritevaluebyAd(Me.SvrDevAd, Conversions.ToString(400000 + Me.SvrMBADStart + 5),
                                              Device.Datatype.浮点数, Me.WD)
                text2 = text.Substring(53, 4)
                array2(1) = CByte(Math.Round(Conversion.Val("&H" + text2.Substring(0, 2))))
                array2(0) = CByte(Math.Round(Conversion.Val("&H" + text2.Substring(2, 2))))
                Me.YL = CDec(BitConverter.ToUInt16(array2, 0))
                Me.YL /= 10.0
                Moddeclare.Mbs.WritevaluebyAd(Me.SvrDevAd, Conversions.ToString(400000 + Me.SvrMBADStart + 7),
                                              Device.Datatype.浮点数, Me.YL)
                result = True
            Else
                result = False
            End If
        End If
        Return result
    End Function

    Public Sub AddItm(ByVal ItmName As String) Implements IDataBlock.AddItm
    End Sub
End Class

