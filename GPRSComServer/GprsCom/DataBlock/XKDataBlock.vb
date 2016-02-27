
Imports MBsrv

Public Class XKDataBlock
    Implements IDataBlock
    Public SSLL As Double
    Public LJLL As Double
    Public YL As Double
    Public WD As Double
    Public PowerState As Double
    Public GKLJ As Double
    Public GKSS As Double
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


        '"!12"t
        Dim s As String
        Dim adr As String
        adr = Format(Me.Addr, "00")
        s = "!" & adr

        Dim V() As Byte
        V = System.Text.ASCIIEncoding.ASCII.GetBytes(s)


        GetCommandBytes = V
    End Function

    Public Function GetValueFromRvData(ByVal L As Integer, ByVal v() As Byte) As Boolean _
        Implements IDataBlock.GetValueFromRvData
        '
        '[ 12 0 0000363447 00000000 003650 000000 000000 +0190 ]
        Dim Rvs As String
        Rvs = System.Text.ASCIIEncoding.ASCII.GetString(v, 0, L)
        If Left(Rvs, 2) = CStr(Me.Addr) Then
            Rvs = "[" & Rvs
        End If
        If Left(Rvs, 3) = "[" & CStr(Me.Addr) Then

            '--------------
            Me.PowerState = Val(Mid(Rvs, 4, 1))
            Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 1), Device.Datatype.浮点数, Me.PowerState)
            '--------------
            Dim LJLLs As String
            LJLLs = Mid(Rvs, 5, 10)
            Me.LJLL = Val(LJLLs)
            Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 3), Device.Datatype.浮点数, Me.LJLL)
            '----------
            Dim SSLLs As String
            SSLLs = Mid(Rvs, 15, 8)
            Me.SSLL = Val(SSLLs)/100
            Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 5), Device.Datatype.浮点数, Me.SSLL)

            '--------------
            Dim YLs As String
            YLs = Mid(Rvs, 23, 6)
            Me.YL = Val(YLs)/10
            Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 7), Device.Datatype.浮点数, Me.YL)

            '-----------------
            Dim Wds As String
            Wds = Mid(Rvs, 41, 5)

            Me.WD = Val(Wds)/10

            Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 9), Device.Datatype.浮点数, Me.WD)
            '-------------------------
            Dim GKLJs As String
            GKLJs = Mid(Rvs, 29, 6)
            Me.GKLJ = Val(GKLJs)
            Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 11), Device.Datatype.浮点数, Me.GKLJ)
            '---------------------------
            Dim GKSSs As String
            GKSSs = Mid(Rvs, 35, 6)
            Me.GKSS = Val(GKSS)
            Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 13), Device.Datatype.浮点数, Me.GKSS)

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

    Public Property ParaAddr As String Implements IDataBlock.ParaAddr '获得关于参数存储地址的描述
        Get
        End Get
        Set(ByVal value As String)
        End Set
    End Property
End Class
