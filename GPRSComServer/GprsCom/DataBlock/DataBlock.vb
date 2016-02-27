Option Strict Off
Option Explicit On
Friend Class DataBlock
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

    Function GetValueFromRvData(ByVal length As Integer, ByVal Rvdata() As Byte) As Boolean _
        Implements IDataBlock.GetValueFromRvData
        Dim StartByteAd As Object
        Dim i, j As Integer
        'Dim Rvdata As ModemDataStruct
        On Error Resume Next
        Dim ByteLen As Short
        Dim Rightlength As Short
        'Dim Dt As DataBlock
        'Dt = Me
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
        'If length > Rightlength And (length Mod Rightlength <> 0) Then '因为是TCP面向连接的通讯是可靠连接所以此处错误只能是modbus server的返回数据不对
        '    GetValueFromRvData = False
        '    Exit Function
        'End If
        Dim F As Byte
        F = Rvdata(1)
        If F <> Me.FC Then
            GetValueFromRvData = False
            Exit Function
        End If

        Select Case Me.FC
            Case 3

                For i = 3 To length - 3 '去掉地址码，功能码，长度,校验

                    StartByteAd = CDbl(Me.SvrMBADStart)*2
                    Mbs.Devices.GetDevicefromAd((Me.SvrDevAd)).WriteHoldingByte(StartByteAd + i - 3, Rvdata(i))
                Next i
            Case 4
                For i = 3 To length - 3 '去掉地址码，功能码，长度,校验

                    StartByteAd = CDbl(Me.SvrMBADStart)*2
                    Mbs.Devices.GetDevicefromAd((Me.SvrDevAd)).WriteInputRegisterByte(StartByteAd + i - 3, Rvdata(i))
                Next i
            Case 1
                'For i = 3 To length - 3 '去掉地址码，功能码，长度,校验
                StartByteAd = CDbl(Me.SvrMBADStart)
                ByteLen = Rvdata(2)
                For i = 0 To ByteLen - 1
                    For j = 0 To 7
                        Mbs.Devices.GetDevicefromAd((Me.SvrDevAd)).WriteCoilStatus(StartByteAd + i*8 + j,
                                                                                   IIf((Rvdata(3 + i) And (2^j)) = 2^j,
                                                                                       1, 0))
                    Next j
                Next i
            Case 2
                StartByteAd = CDbl(Me.SvrMBADStart)
                ByteLen = Rvdata(2)
                For i = 0 To ByteLen - 1
                    For j = 0 To 7
                        Mbs.Devices.GetDevicefromAd((Me.SvrDevAd)).WriteInputStatus((StartByteAd + i*8 + j),
                                                                                    IIf((Rvdata(3 + i) And (2^j)) = 2^j,
                                                                                        1, 0))
                    Next j
                Next i
        End Select
        GetValueFromRvData = True
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


        'Dim dt As DataBlock
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