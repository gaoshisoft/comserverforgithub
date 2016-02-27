Public Class TXMeterDataBlock
    Implements IDataBlock
    Public SSLL As Double
    Public LJLL As Double
    Public YL As Double
    Public WD As Double
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
        F1 = 2 ^ ZS

        F2 = Val("&H" & Right(S, 6))
        F3 = (Val("&H" & "FFFFFF") + 1) / 2
        If F2 > F3 Then
            F2 = F2 - Val("&H" & "FFFFFF") - 1
        End If
        F2 = F2 / Val("&H" & "800000")
        If Val("&H" & Left(S, 2)) = 0 Then
            F1 = 0
        End If
        GetTXFloat = F1 * F2

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

        'CC 05 30 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 EE 
        Dim s As String
        Dim V() As Byte
        s = "CC 05 30 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 EE"
        Dim s1() As String
        s1 = Split(s, " ")
        ReDim V(UBound(s1))
        Dim i As Long
        For i = 0 To s1.GetUpperBound(0)
            V(i) = Val("&H" & s1(i))
        Next i
        Dim V1() As Byte
        ReDim V1(UBound(V) - 3)
        For i = 0 To V1.GetUpperBound(0)
            V1(i) = V(i)

        Next
        V1(1) = Me.mMeterAddr
        Dim ChkSum As Byte
        ChkSum = CheckSum(V1)
        V(17) = ChkSum
        V(1) = Me.mMeterAddr
        GetCommandBytes = V
    End Function

    Public Function GetValueFromRvData(ByVal L As Integer, ByVal v() As Byte) As Boolean Implements IDataBlock.GetValueFromRvData
        'Sub LetValue(ByVal v() As Byte)
        'CC 02 30 1C 00 20 09 01 14 16 47 37 00 00 00 00 00 05 11 42 7D 01 03 78 0D F8 07 61 82 80 B9 60 C6 8B 07 EE
        If v.GetUpperBound(0) >= 35 And v(0) = 204 And v(1) = Me.mMeterAddr Then
            Dim v1(3) As Byte
            Dim i As Integer
            For i = 0 To 3
                v1(i) = v(12 + i)

            Next
            Me.SSLL = GetTXFloat(v1, 0)
            Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 1), Device.Datatype.浮点数, Me.SSLL)
            For i = 0 To 3
                v1(i) = v(18 + i)


            Next
            Me.LJLL = GetTXFloat(v1, 0)
            Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 3), Device.Datatype.浮点数, Me.LJLL)

            Dim IntPartLJll As Long
            IntPartLJll = v(16) * 100 + v(17)
            Me.LJLL = Me.LJLL + IntPartLJll * 10 ^ 6
            For i = 0 To 3
                v1(i) = v(22 + i)

            Next
            Me.WD = GetTXFloat(v1, 0)
            Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 7), Device.Datatype.浮点数, Me.WD)
            For i = 0 To 3
                v1(i) = v(26 + i)

            Next
            Me.YL = GetTXFloat(v1, 0)
            Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 5), Device.Datatype.浮点数, Me.YL)
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

End Class


