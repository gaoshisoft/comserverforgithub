Option Strict Off
Option Explicit On
Friend Class MBTCPDataBlock
    '��������ֵ�ľֲ�����
    Implements IDataBlock

    Private mvarblockName As String '�ֲ�����
    '��������ֵ�ľֲ�����
    Private mvarAD As Byte '�ֲ�����
    Private mvarFC As Byte '�ֲ�����
    Private mvarLength As Integer '�ֲ�����
    '��������ֵ�ľֲ�����
    Private mvarEnable As Boolean '�ֲ�����
    '��������ֵ�ľֲ�����
    Private mvarStartAD As String '�ֲ�����
    '��������ֵ�ľֲ�����
    'Private mvarMBWrite As Boolean '�ֲ�����
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
    Public DBNo As Int16


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

    Function GetMBByteValue(ByVal MBElength As Integer, ByVal MBERvdata() As Byte) As Boolean _
        Implements IDataBlock.GetValueFromRvData
        Dim i, j As Integer
        Dim rvData() As Byte
        Dim Length As Integer
        'Dim StartByteAd As Integer
        Length = MBElength - 6 + 2 'ȥ��6���ֽڵ�mbap header,���������ֽڵ�CRC
        ReDim rvData(Length - 1)
        For i = 0 To Length - 1 - 2 '�ټ�2����Ϊmbeû��CRC
            rvData(i) = MBERvdata(6 + i)
        Next i
        'Dim Rvdata As ModemDataStruct
        Dim ByteLen As Short
        Dim Rightlength As Short
        Select Case Me.FC
            Case 3, 4
                Rightlength = 1 + 1 + 1 + Me.Length*2 + 2
            Case 1, 2
                Rightlength = 1 + 1 + 1 + IIf(Me.Length Mod 8 = 0, Me.Length\8, Me.Length\8 + 1) + 2
            Case 15, 16
                Rightlength = 8
        End Select
        If Length < Rightlength Then '��Ϊ��TCP�������ӵ�ͨѶ�ǿɿ��������Դ˴�����ֻ����modbus server�ķ������ݲ���
            GetMBByteValue = False
            Exit Function
        End If

        'Dim dt As MBTCPDataBlock
        'dt = Me
        Dim StartByteAd As Int16
        Select Case Me.FC
            Case 3

                For i = 3 To Length - 3 'ȥ����ַ�룬�����룬����,У��

                    StartByteAd = CDbl(Me.SvrMBADStart)*2
                    Mbs.Devices.GetDevicefromAd((Me.SvrDevAd)).WriteHoldingByte(StartByteAd + i - 3, rvData(i))
                Next i
            Case 4
                For i = 3 To Length - 3 'ȥ����ַ�룬�����룬����,У��

                    StartByteAd = CDbl(Me.SvrMBADStart)*2
                    Mbs.Devices.GetDevicefromAd((Me.SvrDevAd)).WriteInputRegisterByte(StartByteAd + i - 3, rvData(i))
                Next i
            Case 1
                'For i = 3 To length - 3 'ȥ����ַ�룬�����룬����,У��
                StartByteAd = CDbl(Me.SvrMBADStart)
                ByteLen = rvData(2)
                For i = 0 To ByteLen - 1
                    For j = 0 To 7
                        Mbs.Devices.GetDevicefromAd((Me.SvrDevAd)).WriteCoilStatus(StartByteAd + i*8 + j,
                                                                                   IIf((rvData(3 + i) And (2^j)) = 2^j,
                                                                                       1, 0))
                    Next j
                Next i
            Case 2
                StartByteAd = CDbl(Me.SvrMBADStart)
                ByteLen = rvData(2)
                For i = 0 To ByteLen - 1
                    For j = 0 To 7
                        Mbs.Devices.GetDevicefromAd((Me.SvrDevAd)).WriteInputStatus((StartByteAd + i*8 + j),
                                                                                    IIf((rvData(3 + i) And (2^j)) = 2^j,
                                                                                        1, 0))
                    Next j
                Next i
        End Select
        GetMBByteValue = True
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

        'Dim Dt As DBlock
        'Me.CurrentDatablockID = DataBlockID
        'Dt = Me.DtBlocks(DataBlockID)
        Select Case Me.FC
            Case 1, 2, 3, 4
                ReDim Mbcd(11)

                Mbcd(0) = DBNo\256 '��ʶѲ���ĸ����ݿ�
                Mbcd(1) = DBNo Mod 256
                Mbcd(2) = 0
                Mbcd(3) = 0
                Mbcd(4) = 6\256
                Mbcd(5) = 6 Mod 256
                Mbcd(6) = Me.Addr


                Mbcd(7) = Me.FC
                Mbcd(8) = (Val(Right(Me.startAD, Len(Me.startAD) - 1)) - 1)\256
                'UPGRADE_WARNING: Mod ������Ϊ�� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"��
                Mbcd(9) = (Val(Right(Me.startAD, Len(Me.startAD) - 1)) - 1) Mod 256
                Mbcd(10) = Me.Length\256
                Mbcd(11) = Me.Length Mod 256
                '   Mbcd(6) = CRC16(tMbcd)(1)
                '   Mbcd(7) = CRC16(tMbcd)(0)

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