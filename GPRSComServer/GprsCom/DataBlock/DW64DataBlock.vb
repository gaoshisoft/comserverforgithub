Public Class DW64DataBlock
    '保持属性值的局部变量
    Implements IDataBlock

    Private mvarblockName As String '局部复制
    '保持属性值的局部变量
    Private mvarAD As Byte '局部复制
    Private mvarFC As Byte '局部复制
    Private mvarLength As Integer '局部复制
    '保持属性值的局部变量
    Private mvarEnable As Boolean '局部复制
    '保持属性值的局部变量
    Private mvarStartAD As String '局部复制
    '保持属性值的局部变量
    'Private mvarMBWrite As Boolean '局部复制
    'Public Property Let MBWrite(ByVal vData As Boolean)
    '
    ''Syntax: X.MBWrite = 5
    '    mvarMBWrite = vData
    'End Property
    '
    '
    'Public Property Get MBWrite() As Boolean
    '
    ''Syntax: Debug.Print X.MBWrite
    '    MBWrite = mvarMBWrite
    'End Property
    Public Sendtime As Integer
    Public LjllXS As Double


    Public Property startAD() As String Implements IDataBlock.startAd
        Get

            'Syntax: Debug.Print X.StartAD
            startAD = mvarStartAD
        End Get
        Set(ByVal Value As String)

            'Syntax: X.StartAD = 5
            mvarStartAD = Value
        End Set
    End Property
    'Public ReadOnly Property LngStartAD() As String
    '	Get
    '		
    '		'Syntax: Debug.Print X.StartAD
    '		LngStartAD = CStr(Val(Right(mvarStartAD, Len(mvarStartAD) - 1)) - 1)
    '	End Get
    'End Property


    Public Property Enable() As Boolean Implements IDataBlock.Enable
        Get

            'Syntax: Debug.Print X.Enable
            Enable = mvarEnable
        End Get
        Set(ByVal Value As Boolean)

            'Syntax: X.Enable = 5
            mvarEnable = Value
        End Set
    End Property


    Public Property Length() As Integer Implements IDataBlock.Length
        Get

            'Syntax: Debug.Print X.Length
            Length = mvarLength
        End Get
        Set(ByVal Value As Integer)

            'Syntax: X.Length = 5
            mvarLength = Value
        End Set
    End Property


    Public ReadOnly Property FC() As Byte
        Get

            'Syntax: Debug.Print X.FC
            Select Case Left(Me.startAD, 1)
                Case "0"

                    '    FC = IIf(Me.MBWrite, 15, 1)
                    FC = 1
                Case "1"
                    FC = 2
                Case "3"
                    FC = 4
                Case "4"
                    '     FC = IIf(Me.MBWrite, 16, 3)
                    FC = 3
            End Select
        End Get
    End Property


    Public Property Addr() As Integer Implements IDataBlock.Addr
        Get

            'Syntax: Debug.Print X.AD
            Addr = mvarAD
        End Get
        Set(ByVal Value As Integer)

            'Syntax: X.AD = 5
            mvarAD = Value
        End Set
    End Property


    Public Property BlockName() As String Implements IDataBlock.BlockName
        Get

            'Syntax: Debug.Print X.blockName
            BlockName = mvarblockName
        End Get
        Set(ByVal Value As String)

            'Syntax: X.blockName = 5
            mvarblockName = Value
        End Set
    End Property

    Public SSLL As Double
    Public LJLL As Double
    Public YL As Double
    Public WD As Double
    Public PowerState As Double
    Public GKLJ As Double
    Public GKSS As Double

    Function GetValueFromRvData(ByVal length As Integer, ByVal Rvdata() As Byte) As Boolean _
        Implements IDataBlock.GetValueFromRvData
        Dim StartByteAd As Object
        Dim i, j As Integer
        'Dim Rvdata As ModemDataStruct
        On Error Resume Next
        Dim ByteLen As Short
        Dim Rightlength As Short
        Dim Dt As DW64DataBlock
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
            GetValueFromRvData = False
            Exit Function
        End If
        If length > Rightlength And (length Mod Rightlength <> 0) Then '因为是TCP面向连接的通讯是可靠连接所以此处错误只能是modbus server的返回数据不对
            GetValueFromRvData = False
            Exit Function
        End If
        Dim F As Byte
        F = Rvdata(1)
        If F <> Me.FC Then
            GetValueFromRvData = False
            Exit Function
        End If

        Select Case Me.FC
            Case 3

                'For i = 3 To length - 3 '去掉地址码，功能码，长度,校验

                'StartByteAd = CDbl(me.SvrMBADStart) * 2
                Me.GKLJ = GetUint32Value(Rvdata, 17)
                Me.LJLL = GetDoubleValue(Rvdata, 9)
                'Me.GKLJ = Me.GKLJ * Me.LjllXS
                'Me.LJLL = Me.LJLL * Me.LjllXS
                Me.GKSS = GetFloatValueByad(29, True, Rvdata)
                Me.SSLL = GetFloatValueByad(33, True, Rvdata)
                Me.YL = GetFloatValueByad(21, True, Rvdata)
                Me.WD = GetFloatValueByad(25, True, Rvdata)
                'Me.PowerState = Rvdata(56)

                Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 1), MBsrv.Device.Datatype.浮点数, Me.GKLJ)

                Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 3), MBsrv.Device.Datatype.浮点数, Me.LJLL)
                Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 5), MBsrv.Device.Datatype.浮点数, Me.GKSS)
                Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 7), MBsrv.Device.Datatype.浮点数, Me.SSLL)
                Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 9), MBsrv.Device.Datatype.浮点数, Me.YL)
                Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 11), MBsrv.Device.Datatype.浮点数, Me.WD)
                'Mbs.WritevaluebyAd(Me.SvrDevAd, CStr(400000 + Me.SvrMBADStart + 13), MBsrv.Device.Datatype.浮点数, Me.PowerState)
        End Select
        GetValueFromRvData = True
    End Function

    Public Function GetFloatValueByad(ByVal byteAdr As Integer, ByVal Swapword As Boolean, ByVal Rv() As Byte) As Single _
        'adr表示第几个字（word)
        Dim i As Single


        Dim Tmp(3) As Byte


        Dim byteAD As Integer
        byteAD = byteAdr

        If Not Swapword Then
            Tmp(1) = Rv(byteAD)
            Tmp(0) = Rv(byteAD + 1)
            Tmp(3) = Rv(byteAD + 2)
            Tmp(2) = Rv(byteAD + 3)
        Else
            Tmp(3) = Rv(byteAD)
            Tmp(2) = Rv(byteAD + 1)
            Tmp(1) = Rv(byteAD + 2)
            Tmp(0) = Rv(byteAD + 3)
        End If


        i = BitConverter.ToSingle(Tmp, 0)
        GetFloatValueByad = i
    End Function


    Dim msvrAddrlength As Integer
    Dim msvrDevAd As Integer
    Dim mSvrMBADstart As String


    Public Property SvrAddrLength() As Integer Implements IDataBlock.SvrAddrLength
        Get
            SvrAddrLength = msvrAddrlength
        End Get
        Set(ByVal value As Integer)
            msvrAddrlength = value
        End Set
    End Property

    Public Property SvrDevAd() As Integer Implements IDataBlock.SvrDevAd
        Get
            SvrDevAd = msvrDevAd
        End Get
        Set(ByVal value As Integer)
            msvrDevAd = value
        End Set
    End Property

    ''' <summary>
    ''' server start address lng
    ''' </summary>
    ''' <value>asddfasd</value>
    Public Property SvrMBADStart() As Integer Implements IDataBlock.SvrMBADStart
        Get
            SvrMBADStart = mSvrMBADstart
        End Get
        Set(ByVal value As Integer)
            mSvrMBADstart = value
        End Set
    End Property


    Function GetCommandBytes() As Object Implements IDataBlock.GetCommandBytes

        Dim Mbcd() As Byte
        Dim tMbcd() As Byte

        Me.startAD = "400001"
        Me.Length = 21


        'Dim dt As DW64DataBlock
        ''Me.CurrentDatablockID = DataBlockID
        'dt = Me
        Select Case Me.FC

            Case 1, 2, 3, 4, 15, 16
                ReDim Mbcd(7)
                ReDim tMbcd(7 - 2)
                tMbcd(0) = Me.Addr
                tMbcd(1) = Me.FC
                tMbcd(2) = (Val(Right(Me.startAD, Len(Me.startAD) - 1)) - 1)\256
                tMbcd(3) = (Val(Right(Me.startAD, Len(Me.startAD) - 1)) - 1) Mod 256
                tMbcd(4) = Me.Length\256
                tMbcd(5) = Me.Length Mod 256
                Mbcd(0) = Me.Addr
                Mbcd(1) = Me.FC
                Mbcd(2) = (Val(Right(Me.startAD, Len(Me.startAD) - 1)) - 1)\256
                Mbcd(3) = (Val(Right(Me.startAD, Len(Me.startAD) - 1)) - 1) Mod 256
                Mbcd(4) = Me.Length\256
                Mbcd(5) = Me.Length Mod 256
                Mbcd(6) = CRC16(tMbcd)(1)
                Mbcd(7) = CRC16(tMbcd)(0)

        End Select
        GetCommandBytes = Mbcd
    End Function


    Public Sub AddItm(ByVal ItmName As String) Implements IDataBlock.AddItm
    End Sub


    Public Property ParaAddr As String Implements IDataBlock.ParaAddr
        Get
        End Get
        Set(ByVal value As String)
        End Set
    End Property
End Class
