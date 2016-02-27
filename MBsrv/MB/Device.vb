Option Strict Off
Option Explicit On

Imports VB6 = Microsoft.VisualBasic.Compatibility.VB6
<System.Runtime.InteropServices.ProgId("Device_NET.Device")> Public Class Device
    '保持属性值的局部变量
    Dim HoldingRegisters() As Byte
    Dim InputRegisters() As Byte
    Dim InputStatus() As Boolean
    Dim CoilStatus() As Boolean
    Private mvarMBadressQuantity As Integer '局部复制
    Public Enum Datatype '数据类型
        有符号整型 = 0
        二进制 = 1 '实际是字符串表示的二进制
        浮点数 = 2
        浮点数高低字交换 = 3
        双精度 = 4
        双精度高低字交换 = 5
        长整型 = 6
        长整型高低字交换 = 7
        无符号整型 = 8
        布尔 = 9
    End Enum
    '保持属性值的局部变量
    '保持属性值的局部变量
    Private mvarDeviceAddr As Integer '局部复制
    '保持属性值的局部变量
    Private mvarDeviceDescription As String '局部复制

    Public Property devicedescription() As String
        Get

            'Syntax: Debug.Print X.DeviceDescription
            devicedescription = mvarDeviceDescription
        End Get
        Set(ByVal Value As String)

            'Syntax: X.DeviceDescription = 5
            mvarDeviceDescription = Value
        End Set
    End Property

    Public Property deviceAddr() As Integer
        Get

            'Syntax: Debug.Print X.DeviceAddr
            deviceAddr = mvarDeviceAddr
        End Get
        Set(ByVal Value As Integer)

            'Syntax: X.DeviceAddr = 5
            mvarDeviceAddr = Value
        End Set
    End Property







    Public Property MBadressQuantity() As Integer
        Get

            'Syntax: Debug.Print X.MBadressQuantity
            MBadressQuantity = mvarMBadressQuantity
            If mvarMBadressQuantity < 5 Then
                MBadressQuantity = 5
            End If
        End Get
        Set(ByVal Value As Integer)

            'Syntax: X.MBadressQuantity = MBadressQuantity
            mvarMBadressQuantity = Value
            ReDim HoldingRegisters(MBadressQuantity * 2)
            ReDim InputRegisters(MBadressQuantity * 2)
            ReDim InputStatus(MBadressQuantity)
            ReDim CoilStatus(MBadressQuantity)

        End Set
    End Property


    ReadOnly Property MbadByteQty() As Integer
        Get
            MbadByteQty = Me.MBadressQuantity * 2
        End Get
    End Property
    Sub WriteHoldingByte(ByVal i As Integer, ByVal V As Byte)
        HoldingRegisters(i) = V
    End Sub
    Sub WriteInputRegisterByte(ByVal i As Integer, ByVal V As Byte)
        InputRegisters(i) = V
    End Sub
    Sub WriteInputStatus(ByVal i As Integer, ByVal V As Boolean)
        InputStatus(i) = V
    End Sub
    Sub WriteCoilStatus(ByVal i As Integer, ByVal V As Boolean)
        CoilStatus(i) = V
    End Sub
    Function GetWordValue(ByVal Adr As String, ByVal SwapByte As Boolean) As UInt16
        Dim V As UInt16
        'Dim AD As Integer


        Dim Tmp(1) As Byte

        Dim mName As String
        mName = Trim(Adr)
        Dim byteAD As Integer
        byteAD = Val(CStr(CDbl(Right(mName, Len(mName) - 1)) * 2 - 2))
        If byteAD + 1 < Me.MBadressQuantity * 2 Then
            Select Case Left(mName, 1)
                Case "4"
                    '           If byteAD Mod 4 = 0 Then

                    If Not SwapByte Then
                        Tmp(1) = HoldingRegisters(byteAD)
                        Tmp(0) = HoldingRegisters(byteAD + 1)

                    Else
                        Tmp(0) = HoldingRegisters(byteAD)
                        Tmp(1) = HoldingRegisters(byteAD + 1)


                    End If

                Case "3"
                    '          If byteAD Mod 4 = 0 Then

                    If Not SwapByte Then
                        Tmp(1) = InputRegisters(byteAD)
                        Tmp(0) = InputRegisters(byteAD + 1)

                    Else
                        Tmp(0) = InputRegisters(byteAD)
                        Tmp(1) = InputRegisters(byteAD + 1)
                    End If
                    '          End If
            End Select
        End If

        V = BitConverter.ToUInt16(Tmp, 0)
        GetWordValue = V


    End Function
    Function GetIntvalue(ByVal Adr As String, ByVal SwapByte As Boolean) As Int16
        Dim V As Int16
        'Dim AD As Integer


        Dim Tmp(1) As Byte

        Dim mName As String
        mName = Trim(Adr)
        Dim byteAD As Integer
        byteAD = Val(CStr(CDbl(Right(mName, Len(mName) - 1)) * 2 - 2))
        If byteAD + 1 < Me.MBadressQuantity * 2 Then
            Select Case Left(mName, 1)
                Case "4"
                    '           If byteAD Mod 4 = 0 Then

                    If Not SwapByte Then
                        Tmp(0) = HoldingRegisters(byteAD)
                        Tmp(1) = HoldingRegisters(byteAD + 1)
                    Else

                        Tmp(1) = HoldingRegisters(byteAD)
                        Tmp(0) = HoldingRegisters(byteAD + 1)


                    End If

                Case "3"
                    '          If byteAD Mod 4 = 0 Then

                    If Not SwapByte Then
                        Tmp(0) = InputRegisters(byteAD)
                        Tmp(1) = InputRegisters(byteAD + 1)
                    Else
                        Tmp(1) = InputRegisters(byteAD)
                        Tmp(0) = InputRegisters(byteAD + 1)


                    End If
                    '          End If
            End Select
        End If

        V = BitConverter.ToInt16(Tmp, 0)
        GetIntvalue = V

    End Function



    Public Function ReadModbusbyAD(ByVal mbName As String, ByVal Datatype As Datatype, Optional ByVal Bit As Object = Nothing) As String
        Dim i As Object
        Dim Tmp(3) As Byte
        Dim mName As String
        mName = Trim(mbName)
        Select Case Left(mName, 1)
            Case "4"
                Select Case Datatype
                    Case 0 '整型

                        'i = Str(256 * HoldingRegisters(Val(CStr(CDbl(Right(mName, Len(mName) - 1)) * 2 - 2))) + HoldingRegisters(Val(CStr(CDbl(Right(mName, Len(mName) - 1)) * 2 - 1))))
                        i = Me.GetIntvalue(mName, True)

                    Case 1 '二进制形式

                        i = GetBitstrByad(mName)
                    Case 2 '浮点数形式

                        i = GetFloatValueByad(mName, False)
                    Case 3

                        i = GetFloatValueByad(mName, True)
                    Case 4
                        i = GetDoubleValue(mName, True, False)
                    Case 5
                        i = GetDoubleValue(mName, False, False)
                    Case 6
                        i = Getlongvalue(mName, False)
                    Case 7
                        i = GetlongvalueSwapword(mName, False)
                    Case 8
                        i = GetWordValue(mName, False)

                    Case 9
                        If Not IsNothing(Bit) Then

                            i = Me.GetBitValue(mbName, CInt(Bit))
                        End If

                End Select

            Case "3"
                Select Case Datatype
                    Case 0 '整型

                        'i = Str(256 * InputRegisters(Val(CStr(CDbl(Right(mName, Len(mName) - 1)) * 2 - 2))) + InputRegisters(Val(CStr(CDbl(Right(mName, Len(mName) - 1)) * 2 - 1))))
                        i = Me.GetIntvalue(mName, True)

                    Case 1

                        i = GetBitstrByad(mName)
                    Case 2

                        i = GetFloatValueByad(mName, False)
                    Case 3

                        i = GetFloatValueByad(mName, True)

                    Case 4
                        i = GetDoubleValue(mName, True, False)
                    Case 5
                        i = GetDoubleValue(mName, False, False)
                    Case 6
                        i = Getlongvalue(mName, False)
                    Case 7
                        i = GetlongvalueSwapword(mName, False)
                    Case 8
                        i = GetWordValue(mName, False)

                    Case 9
                        If Not IsNothing(Bit) Then

                            i = Me.GetBitValue(mbName, CInt(Bit))
                        End If

                End Select
            Case "0"

                i = IIf((CoilStatus(Val(CStr(CDbl(Right(mName, Len(mName) - 1)) - 1)))), 1, 0)
            Case "1"

                i = IIf((InputStatus(Val(CStr(CDbl(Right(mName, Len(mName) - 1)) - 1)))), 1, 0)
        End Select

        ReadModbusbyAD = i
    End Function
    Function ReadValueSwapByte(ByVal mbName As String, ByVal Datatype As Datatype) As String

        Dim RegV As Object
        Dim Tmp(3) As Byte
        Dim mName As String
        mName = Trim(mbName)
        Select Case Left(mName, 1)
            Case "4"
                Select Case Datatype
                    Case 0 '整型

                        'i = 256 * HoldingRegisters(Val(CStr(CDbl(Right(mName, Len(mName) - 1)) * 2 - 1))) + HoldingRegisters(Val(CStr(CDbl(Right(mName, Len(mName) - 1)) * 2 - 2)))
                        RegV = GetIntvalue(mName, False)

                    Case 2 '浮点数形式

                        RegV = GetFloatSwapByteByad(mName, False)
                    Case 3

                        RegV = GetFloatSwapByteByad(mName, True)
                    Case 4
                        RegV = GetDoubleValue(mName, True, True)
                    Case 5
                        RegV = GetDoubleValue(mName, False, True)
                    Case 6
                        RegV = Getlongvalue(mName, True)
                    Case 7
                        RegV = GetlongvalueSwapword(mName, True)
                    Case 8
                        RegV = GetWordValue(mName, True)
                End Select

            Case "3"
                Select Case Datatype
                    Case 0 '整型

                        'i = Str(256 * InputRegisters(Val(CStr(CDbl(Right(mName, Len(mName) - 1)) * 2 - 2))) + InputRegisters(Val(CStr(CDbl(Right(mName, Len(mName) - 1)) * 2 - 1))))
                        RegV = GetIntvalue(mName, False)


                    Case 2 '浮点数形式

                        RegV = GetFloatSwapByteByad(mName, False)
                    Case 3

                        RegV = GetFloatSwapByteByad(mName, True)
                    Case 4
                        RegV = GetDoubleValue(mName, True, True)
                    Case 5
                        RegV = GetDoubleValue(mName, False, True)
                    Case 6
                        RegV = Getlongvalue(mName, True)
                    Case 7
                        RegV = GetlongvalueSwapword(mName, True)
                    Case 8
                        RegV = GetWordValue(mName, True)

                End Select
        End Select

        ReadValueSwapByte = RegV
    End Function
    Public Function WriteModbusbyAD(ByVal MbName As String, ByVal Mbvalue As Object, ByVal Datatype As Datatype) As String
        'Dim i As Integer
        Dim AD As Integer
        Dim V As Single
        Dim Tmp(3) As Byte
        Try
            If Mbvalue > 65535 And Datatype = Datatype.无符号整型 Then
                Exit Function
            End If
            If Mbvalue < 0 And Datatype = Datatype.无符号整型 Then
                Exit Function
            End If

            mbName = Trim(mbName)
            AD = CInt(Right(mbName, Len(mbName) - 1)) * 2 - 2

            'On Error GoTo Errh
            Select Case Left(mbName, 1)
                Case "4"
                    '         i = 256& * HoldingRegisters(Val(Right(mName, Len(mName) - 1) * 2 - 2)) + HoldingRegisters(Val(Right(mName, Len(mName) - 1) * 2 - 1))
                    If AD > Me.MbadByteQty Then
                        WriteModbusbyAD = "地址太大！"
                        Exit Function
                    End If
                    If Datatype = Datatype.浮点数 Then

                        'UPGRADE_WARNING: Couldn't resolve default property of object Mbvalue. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        V = Mbvalue

                        Tmp = BitConverter.GetBytes(V)
                        HoldingRegisters(AD) = Tmp(1)
                        HoldingRegisters(AD + 1) = Tmp(0)
                        HoldingRegisters(AD + 2) = Tmp(3)
                        HoldingRegisters(AD + 3) = Tmp(2)

                    ElseIf Datatype = Datatype.浮点数高低字交换 Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object Mbvalue. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        V = Mbvalue

                        Tmp = BitConverter.GetBytes(V)
                        HoldingRegisters(AD) = Tmp(3)
                        HoldingRegisters(AD + 1) = Tmp(2)
                        HoldingRegisters(AD + 2) = Tmp(1)
                        HoldingRegisters(AD + 3) = Tmp(0)
                    ElseIf Datatype = Datatype.长整型 Then
                        Dim Upword As UInt32
                        Dim Lowword As UInt32
                        Upword = Mbvalue \ 65536
                        Lowword = Mbvalue Mod 65536
                        Me.WriteModbusbyAD(mbName, Upword, Device.Datatype.无符号整型)
                        Me.WriteModbusbyAD(mbName + 1, Lowword, Device.Datatype.无符号整型)

                    Else

                        'UPGRADE_WARNING: Couldn't resolve default property of object Mbvalue. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        HoldingRegisters(AD) = Mbvalue \ 256
                        'UPGRADE_WARNING: Couldn't resolve default property of object Mbvalue. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        'UPGRADE_WARNING: Mod has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
                        HoldingRegisters(AD + 1) = Mbvalue Mod 256
                    End If
                Case "3"
                    If AD > Me.MbadByteQty Then
                        WriteModbusbyAD = "地址太大！"
                        Exit Function
                    End If
                    If Datatype = Datatype.浮点数 Then

                        'UPGRADE_WARNING: Couldn't resolve default property of object Mbvalue. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        V = Mbvalue

                        Tmp = BitConverter.GetBytes(V)
                        InputRegisters(AD) = Tmp(1)
                        InputRegisters(AD + 1) = Tmp(0)
                        InputRegisters(AD + 2) = Tmp(3)
                        InputRegisters(AD + 3) = Tmp(2)

                    ElseIf Datatype = Datatype.浮点数高低字交换 Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object Mbvalue. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        V = Mbvalue

                        Tmp = BitConverter.GetBytes(V)
                        InputRegisters(AD) = Tmp(3)
                        InputRegisters(AD + 1) = Tmp(2)
                        InputRegisters(AD + 2) = Tmp(1)
                        InputRegisters(AD + 3) = Tmp(0)
                    ElseIf Datatype = Datatype.长整型 Then
                        Dim Upword As Int16
                        Dim Lowword As Int16
                        Upword = Mbvalue \ 65536
                        Lowword = Mbvalue Mod 65536
                        Me.WriteModbusbyAD(mbName, Upword, Device.Datatype.无符号整型)
                        Me.WriteModbusbyAD(mbName + 1, Lowword, Device.Datatype.无符号整型)
                    Else
                        'UPGRADE_WARNING: Couldn't resolve default property of object Mbvalue. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        InputRegisters(AD) = Mbvalue \ 256
                        'UPGRADE_WARNING: Couldn't resolve default property of object Mbvalue. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        'UPGRADE_WARNING: Mod has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
                        InputRegisters(AD + 1) = Mbvalue Mod 256
                    End If
                Case "0"
                    If AD > Me.MBadressQuantity Then
                        WriteModbusbyAD = "地址太大！"
                        Exit Function
                    End If
                    '         i = IIf((CoilStatus(Val(Right(mName, Len(mName) - 1) - 1))), 1, 0)
                    'UPGRADE_WARNING: Couldn't resolve default property of object Mbvalue. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    CoilStatus(Val(CStr(CDbl(Right(mbName, Len(mbName) - 1)) - 1))) = IIf(Mbvalue > 0, True, False)
                Case "1"
                    If AD > Me.MBadressQuantity Then
                        WriteModbusbyAD = "地址太大！"
                        Exit Function
                    End If
                    'UPGRADE_WARNING: Couldn't resolve default property of object Mbvalue. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    InputStatus(Val(CStr(CDbl(Right(mbName, Len(mbName) - 1)) - 1))) = IIf(Mbvalue > 0, True, False)
            End Select
        Catch

        Finally
        End Try

        'Errh: 
        'MsgBox("请将RTU的Modbus地址设为足够大！")

    End Function


    Function CRC16(ByVal Data() As Byte) As Object
        Dim CRC16Hi As Byte
        Dim CRC16Lo As Byte
        CRC16Hi = &HFF
        CRC16Lo = &HFF
        Dim i As Short
        Dim iIndex As Integer
        For i = 0 To UBound(Data)
            iIndex = CRC16Lo Xor Data(i)
            CRC16Lo = CRC16Hi Xor GetCRCLo(iIndex) '低位处理
            CRC16Hi = GetCRCHi(iIndex) '高位处理
        Next i
        Dim ReturnData(1) As Byte
        ReturnData(0) = CRC16Hi 'CRC高位
        ReturnData(1) = CRC16Lo 'CRC低位
        'UPGRADE_WARNING: Couldn't resolve default property of object CRC16. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        CRC16 = ReturnData
    End Function

    'CRC低位字节值表
    Private Function GetCRCLo(ByVal Ind As Integer) As Byte
        'UPGRADE_WARNING: Couldn't resolve default property of object Choose(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        GetCRCLo = Choose(Ind + 1, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40)
    End Function

    'CRC高位字节值表
    Private Function GetCRCHi(ByVal Ind As Integer) As Byte
        'UPGRADE_WARNING: Couldn't resolve default property of object Choose(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        GetCRCHi = Choose(Ind + 1, &H0, &HC0, &HC1, &H1, &HC3, &H3, &H2, &HC2, &HC6, &H6, &H7, &HC7, &H5, &HC5, &HC4, &H4, &HCC, &HC, &HD, &HCD, &HF, &HCF, &HCE, &HE, &HA, &HCA, &HCB, &HB, &HC9, &H9, &H8, &HC8, &HD8, &H18, &H19, &HD9, &H1B, &HDB, &HDA, &H1A, &H1E, &HDE, &HDF, &H1F, &HDD, &H1D, &H1C, &HDC, &H14, &HD4, &HD5, &H15, &HD7, &H17, &H16, &HD6, &HD2, &H12, &H13, &HD3, &H11, &HD1, &HD0, &H10, &HF0, &H30, &H31, &HF1, &H33, &HF3, &HF2, &H32, &H36, &HF6, &HF7, &H37, &HF5, &H35, &H34, &HF4, &H3C, &HFC, &HFD, &H3D, &HFF, &H3F, &H3E, &HFE, &HFA, &H3A, &H3B, &HFB, &H39, &HF9, &HF8, &H38, &H28, &HE8, &HE9, &H29, &HEB, &H2B, &H2A, &HEA, &HEE, &H2E, &H2F, &HEF, &H2D, &HED, &HEC, &H2C, &HE4, &H24, &H25, &HE5, &H27, &HE7, &HE6, &H26, &H22, &HE2, &HE3, &H23, &HE1, &H21, &H20, &HE0, &HA0, &H60, &H61, &HA1, &H63, &HA3, &HA2, &H62, &H66, &HA6, &HA7, &H67, &HA5, &H65, &H64, &HA4, &H6C, &HAC, &HAD, &H6D, &HAF, &H6F, &H6E, &HAE, &HAA, &H6A, &H6B, &HAB, &H69, &HA9, &HA8, &H68, &H78, &HB8, &HB9, &H79, &HBB, &H7B, &H7A, &HBA, &HBE, &H7E, &H7F, &HBF, &H7D, &HBD, &HBC, &H7C, &HB4, &H74, &H75, &HB5, &H77, &HB7, &HB6, &H76, &H72, &HB2, &HB3, &H73, &HB1, &H71, &H70, &HB0, &H50, &H90, &H91, &H51, &H93, &H53, &H52, &H92, &H96, &H56, &H57, &H97, &H55, &H95, &H94, &H54, &H9C, &H5C, &H5D, &H9D, &H5F, &H9F, &H9E, &H5E, &H5A, &H9A, &H9B, &H5B, &H99, &H59, &H58, &H98, &H88, &H48, &H49, &H89, &H4B, &H8B, &H8A, &H4A, &H4E, &H8E, &H8F, &H4F, &H8D, &H4D, &H4C, &H8C, &H44, &H84, &H85, &H45, &H87, &H47, &H46, &H86, &H82, &H42, &H43, &H83, &H41, &H81, &H80, &H40)
    End Function

    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        'Set mvarparameters = New cParameters
        ReDim HoldingRegisters(5)
        ReDim InputRegisters(5)
        ReDim InputStatus(5)
        ReDim CoilStatus(5)

    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Terminate_Renamed()


    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub
    Function GetMBTCPResponsedataFrame(ByVal mbdata() As Byte) As Object
        Dim RLad As Object
        Dim i As Integer
        Dim j As Integer
        Dim L As Integer
        Dim b As Integer
        Dim Mbresponse() As Byte
        Dim Realdatabytelength As Integer
        Dim RealdataStart As Integer
        Dim exceptioncode As Byte
        '   Dim rtuid As Long
        '---------------
        '   rtuid = 1
        '确定是否支持这个功能码。
        On Error Resume Next
        Dim FC As String
        FC = Format(mbdata(7), "00")
        If InStr(1, "01,02,03,04,05,06,15,16", FC) = 0 Then

            'UPGRADE_WARNING: Couldn't resolve default property of object ExceptionResponse(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            GetMBTCPResponsedataFrame = ExceptionResponse(mbdata, exceptioncode)
            Exit Function
        End If


        'ModbusTCP返回帧格式:mbapheader(7个字节）+fc(1个字节)+length(1个字节）+真正的数据mbdata(10)和mbdata(11)
        Dim RLvl As Integer
        Select Case mbdata(7)
            Case 3
                '确定是否有正确的读取数量
                L = (256 * mbdata(10) + mbdata(11)) * 2
                If L < &H1 Or L > &H7D * 2 Then
                    exceptioncode = 3
                    'UPGRADE_WARNING: Couldn't resolve default property of object ExceptionResponse(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    GetMBTCPResponsedataFrame = ExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '确定是否有正确的起始地址
                RealdataStart = (256 * mbdata(8) + mbdata(9)) * 2
                If RealdataStart > Me.MbadByteQty Or RealdataStart + L > Me.MbadByteQty Then
                    exceptioncode = 2
                    'UPGRADE_WARNING: Couldn't resolve default property of object ExceptionResponse(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    GetMBTCPResponsedataFrame = ExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '-----------------获取真正数据帧
                Realdatabytelength = (256 * mbdata(10) + mbdata(11)) * 2

                RealdataStart = (256 * mbdata(8) + mbdata(9)) * 2
                ReDim Mbresponse(7 + 1 + 1 + Realdatabytelength - 1)
                '确定mbap header
                For i = 0 To 6
                    Mbresponse(i) = mbdata(i)
                Next i
                Mbresponse(5) = 1 + 1 + 1 + Realdatabytelength

                '确定Mbdata
                Mbresponse(7) = 3 '功能码
                Mbresponse(8) = Realdatabytelength '长度
                For i = 0 To Realdatabytelength - 1
                    Mbresponse(9 + i) = HoldingRegisters(RealdataStart + i)
                Next i
            Case 4
                '确定是否有正确的读取数量
                L = (256 * mbdata(10) + mbdata(11)) * 2
                If L < &H1 Or L > &H7D * 2 Then
                    exceptioncode = 3
                    'UPGRADE_WARNING: Couldn't resolve default property of object ExceptionResponse(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    GetMBTCPResponsedataFrame = ExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '确定是否有正确的起始地址
                RealdataStart = (256 * mbdata(8) + mbdata(9)) * 2
                If RealdataStart > Me.MbadByteQty Or RealdataStart + L > Me.MbadByteQty Then
                    exceptioncode = 2
                    'UPGRADE_WARNING: Couldn't resolve default property of object ExceptionResponse(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    GetMBTCPResponsedataFrame = ExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '------------------------------------获取真正的数据帧
                Realdatabytelength = (256 * mbdata(10) + mbdata(11)) * 2
                RealdataStart = (256 * mbdata(8) + mbdata(9)) * 2
                ReDim Mbresponse(7 + 1 + 1 + Realdatabytelength - 1)
                '确定mbap header
                For i = 0 To 6
                    Mbresponse(i) = mbdata(i)
                Next i
                Mbresponse(5) = 1 + 1 + 1 + Realdatabytelength

                '确定Mbdata
                Mbresponse(7) = 4 '功能码
                Mbresponse(8) = Realdatabytelength '长度
                For i = 0 To Realdatabytelength - 1
                    Mbresponse(9 + i) = InputRegisters(RealdataStart + i)
                Next i
            Case 1
                '确定是否有正确的读取数量
                L = 256 * mbdata(10) + mbdata(11)
                If L < &H1 Or L > &H7D0 Then
                    exceptioncode = 3
                    'UPGRADE_WARNING: Couldn't resolve default property of object ExceptionResponse(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    GetMBTCPResponsedataFrame = ExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '确定是否有正确的起始地址
                RealdataStart = 256 * mbdata(8) + mbdata(9)
                If RealdataStart > Me.MBadressQuantity Or RealdataStart + L > Me.MBadressQuantity Then
                    exceptioncode = 2
                    'UPGRADE_WARNING: Couldn't resolve default property of object ExceptionResponse(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    GetMBTCPResponsedataFrame = ExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '-------------------------
                '获取真正的数据帖
                If 256 * mbdata(10) + mbdata(11) Mod 8 = 0 Then

                    Realdatabytelength = (256 * mbdata(10) + mbdata(11)) \ 8
                Else
                    Realdatabytelength = (256 * mbdata(10) + mbdata(11)) \ 8 + 1
                End If
                RealdataStart = 256 * mbdata(8) + mbdata(9)
                ReDim Mbresponse(7 + 1 + 1 + Realdatabytelength)
                '确定mbap header
                For i = 0 To 6
                    Mbresponse(i) = mbdata(i)
                Next i
                Mbresponse(5) = 1 + 1 + 1 + Realdatabytelength

                '确定Mbdata
                Mbresponse(7) = 1 '功能码
                Mbresponse(8) = Realdatabytelength '长度


                For i = 0 To Realdatabytelength - 1 ' * 8 - 1
                    For j = 0 To 7
                        Mbresponse(9 + i) = Mbresponse(9 + i) + (2 ^ j) * IIf(CoilStatus(RealdataStart + i * 8 + j), 1, 0)
                    Next j
                Next i

            Case 2
                '确定是否有正确的读取数量
                L = 256 * mbdata(10) + mbdata(11)
                If L < &H1 Or L > &H7D0 Then
                    exceptioncode = 3
                    'UPGRADE_WARNING: Couldn't resolve default property of object ExceptionResponse(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    GetMBTCPResponsedataFrame = ExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '确定是否有正确的起始地址
                RealdataStart = 256 * mbdata(8) + mbdata(9)
                If RealdataStart > Me.MBadressQuantity Or RealdataStart + L > Me.MBadressQuantity Then
                    exceptioncode = 2
                    'UPGRADE_WARNING: Couldn't resolve default property of object ExceptionResponse(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    GetMBTCPResponsedataFrame = ExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '------------------------------
                If 256 * mbdata(10) + mbdata(11) Mod 8 = 0 Then

                    Realdatabytelength = (256 * mbdata(10) + mbdata(11)) \ 8
                Else
                    Realdatabytelength = (256 * mbdata(10) + mbdata(11)) \ 8 + 1
                End If
                RealdataStart = 256 * mbdata(8) + mbdata(9)
                ReDim Mbresponse(7 + 1 + 1 + Realdatabytelength)
                '确定mbap header
                For i = 0 To 6
                    Mbresponse(i) = mbdata(i)
                Next i
                Mbresponse(5) = 1 + 1 + 1 + Realdatabytelength

                '确定Mbdata
                Mbresponse(7) = 2 '功能码
                Mbresponse(8) = Realdatabytelength '长度
                For i = 0 To Realdatabytelength '* 8 - 1
                    For j = 0 To 7
                        Mbresponse(9 + i) = Mbresponse(9 + i) + (2 ^ j) * IIf(InputStatus(RealdataStart + i * 8 + j), 1, 0)
                    Next j
                Next i
            Case 5

                '确定是否有正确的值
                RLvl = 256 * mbdata(10) + mbdata(11)
                If RLvl <> 256 * &HFF + 0 And RLvl <> 0 Then
                    exceptioncode = 3
                    GetMBTCPResponsedataFrame = ExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '确定是否有正确的地址
                'Dim RLad As Long
                RLad = 256 * mbdata(8) + mbdata(9)
                If RLad < 0 Or RLad + L > Me.MBadressQuantity Then
                    exceptioncode = 2
                    GetMBTCPResponsedataFrame = ExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '执行并获取反应帧
                If RLvl = 65280 Then
                    CoilStatus(RLad) = 1
                Else
                    CoilStatus(RLad) = 0
                End If
                Mbresponse = mbdata
                '       Me.NextcommandShouldbeWrite = True
            Case 6
                '确定是否有正确的值
                '     Dim RLvl As Long
                RLvl = 256 * mbdata(10) + mbdata(11)
                If RLvl > 65535 Or RLvl < 0 Then
                    exceptioncode = 3
                    GetMBTCPResponsedataFrame = ExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '确定是否有正确的地址
                RLad = (256 * mbdata(8) + mbdata(9)) * 2
                If RLad < 0 Or RLad + L > Me.MbadByteQty Then
                    exceptioncode = 2
                    GetMBTCPResponsedataFrame = ExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '执行并获取反应帧
                For i = 0 To 1
                    HoldingRegisters(RLad + i) = mbdata(10 + i)
                Next i
                Mbresponse = mbdata
                '      Me.NextcommandShouldbeWrite = True
            Case 15
                '确定是否有正确的值数量
                L = 256 * mbdata(10) + mbdata(11)
                If L > &H7B0 Or L < 1 Then
                    exceptioncode = 3
                    GetMBTCPResponsedataFrame = ExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '确定是否有正确的地址
                'Dim RLad As Long
                RLad = 256 * mbdata(8) + mbdata(9)
                If RLad < 0 Or RLad + L > Me.MBadressQuantity Then
                    exceptioncode = 2
                    GetMBTCPResponsedataFrame = ExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '执行并获取反应帧
                For i = 0 To mbdata(12) - 1
                    For j = 0 To 7
                        CoilStatus(RLad + i * 8 + j) = IIf((mbdata(13 + i) And 2 ^ j) = 2 ^ j, 1, 0)
                    Next j
                Next i
                ReDim Mbresponse(7 + 5 - 1) '7为mbap header 5为pdu,减1因为从0开始
                For i = 0 To 11
                    Mbresponse(i) = mbdata(i)
                Next i
                Mbresponse(5) = 1 + 5 '
                '    Me.NextcommandShouldbeWrite = True
            Case 16
                '确定是否有正确的值数量
                L = 256 * mbdata(10) + mbdata(11)
                If L > 123 Or L < 1 Or mbdata(12) <> L * 2 Then
                    exceptioncode = 3
                    GetMBTCPResponsedataFrame = ExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '确定是否有正确的地址
                'Dim RLad As Long
                RLad = (256 * mbdata(8) + mbdata(9)) * 2
                If RLad < 0 Or RLad + L > Me.MbadByteQty Then
                    exceptioncode = 2
                    GetMBTCPResponsedataFrame = ExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '执行并获取反应帧
                For i = 0 To mbdata(12) - 1

                    HoldingRegisters(RLad + i) = mbdata(13 + i)

                Next i
                ReDim Mbresponse(7 + 5 - 1) '7为mbap header 5为pdu,减1因为从0开始
                For i = 0 To 11
                    Mbresponse(i) = mbdata(i)
                Next i
                Mbresponse(5) = 1 + 5 '

                '   Me.NextcommandShouldbeWrite = True
        End Select

        GetMBTCPResponsedataFrame = Mbresponse


    End Function

    Function GetMBRTUResponsedataFrame(ByVal mbdata() As Byte) As Object
        Dim RLad As Object
        Dim i As Integer
        Dim j As Integer
        Dim L As Integer
        Dim b As Integer
        Dim Mbresponse() As Byte
        Dim Realdatabytelength As Integer
        Dim RealdataStart As Integer
        Dim exceptioncode As Byte
        '   Dim rtuid As Long
        '---------------
        '   rtuid = 1
        '确定是否支持这个功能码。
        On Error Resume Next
        Dim FC As String
        FC = Format(mbdata(1), "00")
        If InStr(1, "01,02,03,04,05,06,15,16", FC) = 0 Then
            exceptioncode = 1

            GetMBRTUResponsedataFrame = RTUExceptionResponse(mbdata, exceptioncode)
            Exit Function
        End If


        'ModbusTCP返回帧格式:mbapheader(7个字节）+fc(1个字节)+length(1个字节）+真正的数据mbdata(10)和mbdata(11)
        ' modbus rtu 返回帖格式，从设备地址（1个字节），功能码（1），后面的有效数据字节数（1），数据，crc
        Dim RLvl As Integer
        Select Case mbdata(1)
            Case 3
                '确定是否有正确的读取数量
                L = (256 * mbdata(4) + mbdata(5)) * 2
                If L < &H1 Or L > &H7D * 2 Then
                    exceptioncode = 3
                    GetMBRTUResponsedataFrame = RTUExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '确定是否有正确的起始地址
                RealdataStart = (256 * mbdata(2) + mbdata(3)) * 2
                If RealdataStart > Me.MbadByteQty Or RealdataStart + L > Me.MbadByteQty Then
                    exceptioncode = 2
                    GetMBRTUResponsedataFrame = RTUExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '-----------------获取真正数据帧
                Realdatabytelength = (256 * mbdata(4) + mbdata(5)) * 2

                RealdataStart = (256 * mbdata(2) + mbdata(3)) * 2
                ReDim Mbresponse(1 + 1 + 1 + Realdatabytelength - 1)

                Mbresponse(0) = mbdata(0)

                '确定Mbdata
                Mbresponse(1) = 3 '功能码
                Mbresponse(2) = Realdatabytelength '长度
                For i = 0 To Realdatabytelength - 1
                    Mbresponse(3 + i) = HoldingRegisters(RealdataStart + i)
                Next i
                GetMBRTUResponsedataFrame = getCrcframe(Mbresponse)
            Case 4
                '确定是否有正确的读取数量
                L = (256 * mbdata(4) + mbdata(5)) * 2
                If L < &H1 Or L > &H7D * 2 Then
                    exceptioncode = 3
                    GetMBRTUResponsedataFrame = RTUExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '确定是否有正确的起始地址
                RealdataStart = (256 * mbdata(2) + mbdata(3)) * 2
                If RealdataStart > Me.MbadByteQty Or RealdataStart + L > Me.MbadByteQty Then
                    exceptioncode = 2
                    GetMBRTUResponsedataFrame = RTUExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '------------------------------------获取真正的数据帧
                Realdatabytelength = (256 * mbdata(4) + mbdata(5)) * 2
                RealdataStart = (256 * mbdata(2) + mbdata(3)) * 2
                ReDim Mbresponse(1 + 1 + 1 + Realdatabytelength - 1)

                Mbresponse(0) = mbdata(0)

                '确定Mbdata
                Mbresponse(1) = 4 '功能码
                Mbresponse(2) = Realdatabytelength '长度
                For i = 0 To Realdatabytelength - 1
                    Mbresponse(3 + i) = InputRegisters(RealdataStart + i)
                Next i
                GetMBRTUResponsedataFrame = getCrcframe(Mbresponse)
            Case 1
                '确定是否有正确的读取数量
                L = 256 * mbdata(4) + mbdata(5)
                If L < &H1 Or L > &H7D0 Then
                    exceptioncode = 3
                    GetMBRTUResponsedataFrame = RTUExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '确定是否有正确的起始地址
                RealdataStart = 256 * mbdata(2) + mbdata(3)
                If RealdataStart > Me.MBadressQuantity Or RealdataStart + L > Me.MBadressQuantity Then
                    exceptioncode = 2
                    GetMBRTUResponsedataFrame = RTUExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '-------------------------
                '获取真正的数据帖
                If 256 * mbdata(4) + mbdata(5) Mod 8 = 0 Then

                    Realdatabytelength = (256 * mbdata(4) + mbdata(5)) \ 8
                Else
                    Realdatabytelength = (256 * mbdata(4) + mbdata(5)) \ 8 + 1
                End If
                RealdataStart = 256 * mbdata(2) + mbdata(3)
                ReDim Mbresponse(1 + 1 + 1 + Realdatabytelength - 1)

                Mbresponse(0) = mbdata(0)

                '确定Mbdata
                Mbresponse(1) = 1 '功能码
                Mbresponse(2) = Realdatabytelength '长度


                For i = 0 To Realdatabytelength - 1 ' * 8 - 1
                    For j = 0 To 7
                        Mbresponse(3 + i) = Mbresponse(3 + i) + (2 ^ j) * IIf(CoilStatus(RealdataStart + i * 8 + j), 1, 0)
                    Next j
                Next i
                GetMBRTUResponsedataFrame = getCrcframe(Mbresponse)
            Case 2
                '确定是否有正确的读取数量
                L = 256 * mbdata(4) + mbdata(5)
                If L < &H1 Or L > &H7D0 Then
                    exceptioncode = 3
                    GetMBRTUResponsedataFrame = RTUExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '确定是否有正确的起始地址
                RealdataStart = 256 * mbdata(2) + mbdata(3)
                If RealdataStart > Me.MBadressQuantity Or RealdataStart + L > Me.MBadressQuantity Then
                    exceptioncode = 2
                    GetMBRTUResponsedataFrame = RTUExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '------------------------------
                If 256 * mbdata(4) + mbdata(5) Mod 8 = 0 Then

                    Realdatabytelength = (256 * mbdata(4) + mbdata(5)) \ 8
                Else
                    Realdatabytelength = (256 * mbdata(4) + mbdata(5)) \ 8 + 1
                End If
                RealdataStart = 256 * mbdata(2) + mbdata(3)
                ReDim Mbresponse(1 + 1 + 1 + Realdatabytelength - 1)

                Mbresponse(0) = mbdata(0)


                '确定Mbdata
                Mbresponse(1) = 2 '功能码
                Mbresponse(2) = Realdatabytelength '长度
                For i = 0 To Realdatabytelength - 1 '* 8 - 1
                    For j = 0 To 7
                        Mbresponse(3 + i) = Mbresponse(3 + i) + (2 ^ j) * IIf(InputStatus(RealdataStart + i * 8 + j), 1, 0)
                    Next j
                Next i
                GetMBRTUResponsedataFrame = getCrcframe(Mbresponse)
            Case 5

                '确定是否有正确的值
                RLvl = 256 * mbdata(4) + mbdata(5)
                If RLvl <> 256 * &HFF + 0 And RLvl <> 0 Then
                    exceptioncode = 3
                    GetMBRTUResponsedataFrame = RTUExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '确定是否有正确的地址
                'Dim RLad As Long
                RLad = 256 * mbdata(2) + mbdata(3)
                If RLad < 0 Or RLad + L > Me.MBadressQuantity Then
                    exceptioncode = 2
                    GetMBRTUResponsedataFrame = RTUExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '执行并获取反应帧
                If RLvl = 65280 Then
                    CoilStatus(RLad) = 1
                Else
                    CoilStatus(RLad) = 0
                End If

                GetMBRTUResponsedataFrame = mbdata
                '       Me.NextcommandShouldbeWrite = True
            Case 6
                '确定是否有正确的值
                '     Dim RLvl As Long
                RLvl = 256 * mbdata(4) + mbdata(5)
                If RLvl > 65535 Or RLvl < 0 Then
                    exceptioncode = 3
                    GetMBRTUResponsedataFrame = RTUExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '确定是否有正确的地址
                RLad = (256 * mbdata(2) + mbdata(3)) * 2
                If RLad < 0 Or RLad + L > Me.MbadByteQty Then
                    exceptioncode = 2
                    GetMBRTUResponsedataFrame = RTUExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '执行并获取反应帧
                For i = 0 To 1
                    HoldingRegisters(RLad + i) = mbdata(4 + i)
                Next i
                Mbresponse = mbdata
                GetMBRTUResponsedataFrame = mbdata
                '      Me.NextcommandShouldbeWrite = True
            Case 15
                '确定是否有正确的值数量
                L = 256 * mbdata(4) + mbdata(5)
                If L > &H7B0 Or L < 1 Then
                    exceptioncode = 3
                    GetMBRTUResponsedataFrame = RTUExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '确定是否有正确的地址
                'Dim RLad As Long
                RLad = 256 * mbdata(2) + mbdata(3)
                If RLad < 0 Or RLad + L > Me.MBadressQuantity Then
                    exceptioncode = 2
                    GetMBRTUResponsedataFrame = RTUExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '执行并获取反应帧
                For i = 0 To mbdata(6) - 1
                    For j = 0 To 7
                        CoilStatus(RLad + i * 8 + j) = IIf((mbdata(7 + i) And 2 ^ j) = 2 ^ j, 1, 0)
                    Next j
                Next i
                ReDim Mbresponse(6) '
                For i = 0 To 5
                    Mbresponse(i) = mbdata(i)
                Next i
                GetMBRTUResponsedataFrame = getCrcframe(Mbresponse)
            Case 16
                '确定是否有正确的值数量
                L = 256 * mbdata(4) + mbdata(5)
                If L > 123 Or L < 1 Or mbdata(12) <> L * 2 Then
                    exceptioncode = 3
                    GetMBRTUResponsedataFrame = RTUExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '确定是否有正确的地址
                'Dim RLad As Long
                RLad = (256 * mbdata(2) + mbdata(3)) * 2
                If RLad < 0 Or RLad + L > Me.MbadByteQty Then
                    exceptioncode = 2
                    GetMBRTUResponsedataFrame = RTUExceptionResponse(mbdata, exceptioncode)
                    Exit Function
                End If
                '执行并获取反应帧
                For i = 0 To mbdata(6) - 1

                    HoldingRegisters(RLad + i) = mbdata(7 + i)

                Next i
                ReDim Mbresponse(6) '7为mbap header 5为pdu,减1因为从0开始
                For i = 0 To 5
                    Mbresponse(i) = mbdata(i)
                Next i
                GetMBRTUResponsedataFrame = getCrcframe(Mbresponse)
        End Select


    End Function
    Function ExceptionResponse(ByVal mbdata() As Byte, ByVal exceptioncode As Byte) As Object
        Dim L As Object
        Dim i As Object
        Dim Mbresponse() As Byte
        exceptioncode = 1
        ReDim Mbresponse(7 + 3 - 1)
        For i = 0 To 6

            Mbresponse(i) = mbdata(i)
        Next i
        '后面的字节数
        'UPGRADE_WARNING: Couldn't resolve default property of object L. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        L = 1 + 1 + 1
        'UPGRADE_WARNING: Couldn't resolve default property of object L. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        Mbresponse(4) = L \ 256
        'UPGRADE_WARNING: Couldn't resolve default property of object L. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        'UPGRADE_WARNING: Mod has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        Mbresponse(5) = L Mod 256
        Mbresponse(7) = mbdata(7) + &H80 '出现错误，功能码要加十六进制80
        Mbresponse(8) = exceptioncode
        'UPGRADE_WARNING: Couldn't resolve default property of object ExceptionResponse. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        ExceptionResponse = Mbresponse
    End Function
    Function RTUExceptionResponse(ByVal mbdata() As Byte, ByVal exceptioncode As Byte) As Object
        Dim Mbresponse() As Byte
        'exceptioncode = 1
        ReDim Mbresponse(3)
        '       For i = 0 To 6
        Mbresponse(0) = mbdata(0)
        '       Next i
        '后面的字节数
        '       L = 1 + 1 + 1
        '       Mbresponse(1) = L \ 256
        '       Mbresponse(2) = L Mod 256
        Mbresponse(1) = mbdata(1) + &H80 '出现错误，功能码要加十六进制80
        Mbresponse(2) = exceptioncode

        RTUExceptionResponse = getCrcframe(Mbresponse)
    End Function
    Private Function getCrcframe(ByVal mbdata() As Byte) As Object
        Dim mbf() As Byte
        Dim crcb() As Byte
        Dim i As Integer
        Dim j As Integer
        i = UBound(mbdata)
        ReDim mbf(i + 2)
        crcb = CRC16(mbdata)
        For j = 0 To i
            mbf(j) = mbdata(j)
        Next j

        mbf(i + 1) = crcb(1)
        mbf(i + 2) = crcb(0)

        getCrcframe = mbf
    End Function


    Public Function GetBitValue(ByVal adr As String, Optional ByVal BitADr As Integer = 0) As Short 'bitadr为从0开始
        Dim i As Integer
        Dim mName As String
        mName = Trim(Adr)
        Select Case Left(mName, 1)
            Case "4"

                i = 256 * HoldingRegisters(Val(Right(mName, Len(mName) - 1)) * 2 - 2) + HoldingRegisters(Val(CStr(CDbl(Right(mName, Len(mName) - 1)) * 2 - 1)))
                GetBitValue = IIf((i And (2 ^ BitADr)) = 2 ^ BitADr, 1, 0)

            Case "3"
                i = 256 * InputRegisters(Val(CStr(CDbl(Right(mName, Len(mName) - 1)) * 2 - 2))) + InputRegisters(Val(CStr(CDbl(Right(mName, Len(mName) - 1)) * 2 - 1)))
                GetBitValue = IIf((i And (2 ^ BitADr)) = 2 ^ BitADr, 1, 0)
            Case "0"
                GetBitValue = IIf((CoilStatus(Val(CStr(CDbl(Right(mName, Len(mName) - 1)) - 1)))), 1, 0)
            Case "1"
                GetBitValue = IIf((InputStatus(Val(CStr(CDbl(Right(mName, Len(mName) - 1)) - 1)))), 1, 0)
        End Select
    End Function
    Public Function GetBitstrByad(ByVal Adr As String) As String
        Dim i As Integer
        Dim mName As String
        mName = Trim(Adr)
        Select Case Left(mName, 1)
            Case "4"
                For i = 15 To 0 Step -1
                    If (i + 1) Mod 4 = 0 Then '每四位一组
                        GetBitstrByad = GetBitstrByad & " "
                    End If
                    GetBitstrByad = GetBitstrByad & Trim(Str(GetBitValue(Adr, i)))
                Next i

            Case "3"


                For i = 15 To 0 Step -1
                    If (i + 1) Mod 4 = 0 Then
                        GetBitstrByad = GetBitstrByad & " "
                    End If
                    GetBitstrByad = GetBitstrByad & Trim(Str(GetBitValue(Adr, i)))
                Next i
            Case "0"
                GetBitstrByad = IIf((CoilStatus(Val(CStr(CDbl(Right(mName, Len(mName) - 1)) - 1)))), 1, 0)
            Case "1"
                GetBitstrByad = IIf((InputStatus(Val(CStr(CDbl(Right(mName, Len(mName) - 1)) - 1)))), 1, 0)
        End Select

    End Function
    Public Function GetFloatValueByad(ByVal Adr As String, ByVal Swapword As Boolean) As Single
        Dim i As Single
        'Dim AD As Integer


        Dim Tmp(3) As Byte
        'Dim result As String
        Dim mName As String
        mName = Trim(Adr)
        Dim byteAD As Integer
        byteAD = Val(CStr(CDbl(Right(mName, Len(mName) - 1)) * 2 - 2))
        If byteAD + 3 < Me.MBadressQuantity * 2 Then
            Select Case Left(mName, 1)
                Case "4"
                    '           If byteAD Mod 4 = 0 Then
                    If Not Swapword Then
                        Tmp(1) = HoldingRegisters(byteAD)
                        Tmp(0) = HoldingRegisters(byteAD + 1)
                        Tmp(3) = HoldingRegisters(byteAD + 2)
                        Tmp(2) = HoldingRegisters(byteAD + 3)
                    Else
                        Tmp(3) = HoldingRegisters(byteAD)
                        Tmp(2) = HoldingRegisters(byteAD + 1)
                        Tmp(1) = HoldingRegisters(byteAD + 2)
                        Tmp(0) = HoldingRegisters(byteAD + 3)
                    End If
                    '           End If
                Case "3"
                    '          If byteAD Mod 4 = 0 Then
                    If Not Swapword Then
                        Tmp(1) = InputRegisters(byteAD)
                        Tmp(0) = InputRegisters(byteAD + 1)
                        Tmp(3) = InputRegisters(byteAD + 2)
                        Tmp(2) = InputRegisters(byteAD + 3)
                    Else
                        Tmp(3) = InputRegisters(byteAD)
                        Tmp(2) = InputRegisters(byteAD + 1)
                        Tmp(1) = InputRegisters(byteAD + 2)
                        Tmp(0) = InputRegisters(byteAD + 3)
                    End If
                    '          End If
            End Select
        End If

        i = BitConverter.ToSingle(Tmp, 0)
        GetFloatValueByad = i
    End Function

    Public Function GetFloatSwapByteByad(ByVal Adr As String, ByVal Swapword As Boolean) As Single
        Dim i As Single
        'Dim AD As Integer


        Dim Tmp(3) As Byte
        'Dim result As String
        Dim mName As String
        mName = Trim(Adr)
        Dim byteAD As Integer
        byteAD = Val(CStr(CDbl(Right(mName, Len(mName) - 1)) * 2 - 2))
        If byteAD + 3 < Me.MBadressQuantity * 2 Then
            Select Case Left(mName, 1)
                Case "4"
                    '           If byteAD Mod 4 = 0 Then
                    If Not Swapword Then
                        Tmp(0) = HoldingRegisters(byteAD)
                        Tmp(1) = HoldingRegisters(byteAD + 1)
                        Tmp(2) = HoldingRegisters(byteAD + 2)
                        Tmp(3) = HoldingRegisters(byteAD + 3)
                    Else
                        Tmp(2) = HoldingRegisters(byteAD)
                        Tmp(3) = HoldingRegisters(byteAD + 1)
                        Tmp(0) = HoldingRegisters(byteAD + 2)
                        Tmp(1) = HoldingRegisters(byteAD + 3)
                    End If
                    '           End If
                Case "3"
                    '          If byteAD Mod 4 = 0 Then
                    If Not Swapword Then
                        Tmp(0) = InputRegisters(byteAD)
                        Tmp(1) = InputRegisters(byteAD + 1)
                        Tmp(2) = InputRegisters(byteAD + 2)
                        Tmp(3) = InputRegisters(byteAD + 3)
                    Else
                        Tmp(2) = InputRegisters(byteAD)
                        Tmp(3) = InputRegisters(byteAD + 1)
                        Tmp(0) = InputRegisters(byteAD + 2)
                        Tmp(1) = InputRegisters(byteAD + 3)
                    End If
                    '          End If
            End Select
        End If
        i = BitConverter.ToSingle(Tmp, 0)
        GetFloatSwapByteByad = i
    End Function
    Function GetDoubleValue(ByVal Adr As String, ByVal SwapWord As Boolean, ByVal SwapByte As Boolean) As Double
        Dim V As Double
        'Dim AD As Integer


        Dim Tmp(3) As UInt16
        Dim Tbytes(7) As Byte

        Dim mName As String
        mName = Trim(Adr)
        Dim wordAD As UInt32
        wordAD = Val(Adr)
        If Val(Right(Adr, Len(Adr) - 1)) < Me.MBadressQuantity Then


            If Not SwapWord Then

                Tmp(3) = GetWordValue(CStr(wordAD), SwapByte)

                Tmp(2) = GetWordValue(CStr(wordAD + 1), SwapByte)
                Tmp(1) = GetWordValue(CStr(wordAD + 2), SwapByte)
                Tmp(0) = GetWordValue(CStr(wordAD + 3), SwapByte)
            Else

                Tmp(0) = GetWordValue(CStr(wordAD), SwapByte)

                Tmp(1) = GetWordValue(CStr(wordAD + 1), SwapByte)
                Tmp(2) = GetWordValue(CStr(wordAD + 2), SwapByte)
                Tmp(3) = GetWordValue(CStr(wordAD + 3), SwapByte)
            End If
            '           End If

        End If
        Buffer.BlockCopy(Tmp, 0, Tbytes, 0, 8)


        V = BitConverter.ToDouble(Tbytes, 0)
        GetDoubleValue = V

    End Function
    Function Getlongvalue(ByVal Adr As String, ByVal SwapByte As Boolean) As UInt32
        Dim upv As UInt32
        upv = Me.GetWordValue(Adr, SwapByte)
        upv = upv * 65536
        Getlongvalue = upv + GetWordValue(Adr + 1, SwapByte)

    End Function
    Function GetlongvalueSwapword(ByVal Adr As String, ByVal SwapByte As Boolean) As UInt32
        Dim upv As UInt32
        upv = Me.GetWordValue(Adr + 1, SwapByte)
        upv = upv * 65536
        GetlongvalueSwapword = Me.GetWordValue(Adr, SwapByte) + upv

    End Function





End Class