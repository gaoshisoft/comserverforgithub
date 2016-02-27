Imports MBsrv

Public Class CSDataBlock
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
        '68H，3XH，3XH，31H，31H，30H，30H  （取当前参数）
        '     地址()

        Dim s1 As String = Chr(&H68)
        Dim s2 As String = "1100"
        Dim s As String

        s = s1 & Format(Me.Addr, "00") & s2

        Dim V() As Byte
        V = System.Text.ASCIIEncoding.ASCII.GetBytes(s)


        GetCommandBytes = V
    End Function

    Public Function GetValueFromRvData(ByVal L As Integer, ByVal v() As Byte) As Boolean _
        Implements IDataBlock.GetValueFromRvData
        '
        '11回：68H；3XH，3XH；31H，31H；33H，34H；
        '开始    地址      命令      
        '3XH，3XH，3XH，3XH，3XH，3XH，3XH，3XH，3XH，3XH，3BH；
        '                 累积量：XXXXXXXXXX m³
        '  3XH，3XH，3XH，3XH，3XH，3XH，2EH，3XH，3XH，3BH；
        '流量：XXXXXX.XX m³/h 
        '  3XH，3XH，3XH，3XH，3XH，2EH，3XH，3BH
        '压力:     XXXXX.X(kPa)
        '  2BH（2DH），3XH，3XH，3XH，2EH，3XH；45H，45H 。
        '  温度：±XXX.X ℃                 EE 结束 。

        Dim Rvs As String
        Rvs = System.Text.ASCIIEncoding.ASCII.GetString(v, 0, L)
        If Left(Rvs, 3) = "h" & Format(Me.Addr, "00") Then
            '--------------
            'Me.PowerState = Val(Mid(Rvs, 4, 1))
            'Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 1), Device.Datatype.浮点数, Me.PowerState)
            '--------------
            Dim LJLLs As String
            LJLLs = Mid(Rvs, 8, 11)
            LJLLs = Replace(LJLLs, ";", "")
            Me.LJLL = Val(LJLLs)
            Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 1), Device.Datatype.浮点数, Me.LJLL)
            '----------
            Dim SSLLs As String
            SSLLs = Mid(Rvs, 19, 10)
            SSLLs = Replace(SSLLs, ";", "")
            Me.SSLL = Val(SSLLs)
            Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 3), Device.Datatype.浮点数, Me.SSLL)

            '--------------
            Dim YLs As String
            YLs = Mid(Rvs, 29, 8)
            YLs = Replace(YLs, ";", "")
            Me.YL = Val(YLs)
            Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 5), Device.Datatype.浮点数, Me.YL)

            '-----------------
            Dim Wds As String
            Wds = Mid(Rvs, 37, 6)
            Wds = Replace(Wds, ";", "")
            Me.WD = Val(Wds)

            Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 7), Device.Datatype.浮点数, Me.WD)
            '-------------------------
            'Dim GKLJs As String
            'GKLJs = Mid(Rvs, 29, 6)
            'Me.GKLJ = Val(GKLJs)
            'Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 11), Device.Datatype.浮点数, Me.GKLJ)
            ''---------------------------
            'Dim GKSSs As String
            'GKSSs = Mid(Rvs, 35, 6)
            'Me.GKSS = Val(GKSS)
            'Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 13), Device.Datatype.浮点数, Me.GKSS)

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


    Public Property startAd() As String Implements IDataBlock.startAd
        Get
        End Get
        Set(ByVal value As String)
        End Set
    End Property

    Public Property Length() As Integer Implements IDataBlock.Length
        Get
        End Get
        Set(ByVal value As Integer)
        End Set
    End Property

    Public Property ParaAddr As String Implements IDataBlock.ParaAddr
        Get
        End Get
        Set(ByVal value As String)
        End Set
    End Property
End Class
