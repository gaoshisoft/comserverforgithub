Imports MBsrv


Public Class CNDataBlock
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


    Private Function GetCNFloat(ByVal V() As Byte, ByVal start As Integer) As Single '算法同天信流量计
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
        If Val("&H" & Left(S, 2)) = 0 Then
            F1 = 0
        End If
        F2 = Val("&H" & Right(S, 6))
        F3 = (Val("&H" & "FFFFFF") + 1)/2
        If F2 > F3 Then
            F2 = F2 - Val("&H" & "FFFFFF") - 1
        End If
        F2 = F2/Val("&H" & "800000")

        GetCNFloat = F1*F2
    End Function

    Public Function GetCNLongFloat(ByVal V() As Byte, ByVal start As Integer) As Double '算法同天信流量计
        Dim S As String
        S = HextoStr(V, 6, start)
        Dim F1 As Single
        Dim F2 As Double
        Dim ZS As Single
        Dim F3 As Double
        ZS = Val("&H" & Left(S, 2))
        If ZS > 128 Then
            ZS = ZS - 256
        End If
        F1 = 2^ZS
        If Val("&H" & Left(S, 2)) = 0 Then
            F1 = 0
        End If
        F2 = Val("&H" & Right(S, 10))
        F3 = (Val("&H" & "FFFFFFFFFF") + 1)/2
        If F2 > F3 Then
            F2 = F2 - Val("&H" & "FFFFFFFFFF") - 1
        End If
        F2 = F2/Val("&H" & "8000000000")

        GetCNLongFloat = F1*F2
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
        s = "55 55 17 02 00 19"
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
            V1(i) = V(i + 2)

        Next
        V1(0) = Me.mMeterAddr
        Dim ChkSum As Byte
        ChkSum = CheckSum(V1)
        V(5) = ChkSum
        V(2) = Me.mMeterAddr
        GetCommandBytes = V
    End Function

    Public Function GetValueFromRvData(ByVal L As Integer, ByVal v() As Byte) As Boolean _
        Implements IDataBlock.GetValueFromRvData

        '55 55 17 02 18 1D 00 00 00 00 00 00 00 00 00 05 5B 05 2A 07 6E 1C 16 0B 5C 43 DB 10 00 19
        '55 55 17 02 18 19 00 00 00 00 00 00 00 00 00 05 64 4E 8E 07 5F 2C CE 15 65 7F 94 72 20 0E
        If v.GetUpperBound(0) >= 29 And v(0) = 85 And v(1) = 85 And v(2) = Me.mMeterAddr And CheckSum(v, 2, 27) = v(29) _
            Then

            Dim v1(3) As Byte
            Dim i As Integer
            '----------------

            For i = 0 To 3
                v1(i) = v(7 + i)

            Next
            Me.GKSSLL = GetCNFloat(v1, 0)
            Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 1), Device.Datatype.浮点数, Me.GKSSLL)
            '----------------
            For i = 0 To 3
                v1(i) = v(11 + i)


            Next
            Me.SSLL = GetCNFloat(v1, 0)
            Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 3), Device.Datatype.浮点数, Me.SSLL)
            '-----------------------
            For i = 0 To 3
                v1(i) = v(15 + i)


            Next
            Me.WD = GetCNFloat(v1, 0)
            Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 5), Device.Datatype.浮点数, Me.WD)
            '------------

            For i = 0 To 3
                v1(i) = v(19 + i)


            Next
            Me.YL = GetCNFloat(v1, 0)
            Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 7), Device.Datatype.浮点数, Me.YL)
            '------------------------
            ReDim v1(5)
            For i = 0 To 5
                v1(i) = v(23 + i)


            Next
            Me.LJLL = GetCNLongFloat(v1, 0)
            Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 9), Device.Datatype.浮点数, Me.LJLL)
            '------------------------

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
        End Get
        Set(ByVal value As String)
        End Set
    End Property
End Class


