
Public Module MdlFuncTools
    Sub CopyCtrlposition(ByVal Parent As Object, ByVal obj1 As Object, ByVal obj2 As Object)
        'obj2=New 

        Parent.Controls.Add(obj2)
        'AddHandler obj1.Validated, AddressOf 
        obj2.Left = obj1.Left
        obj2.Top = obj1.Top
        obj2.Width = obj1.Width
        'obj2.Parent = obj1.Parent
    End Sub

    Public Function HextoStr(ByRef src() As Byte, ByRef Srclen As Short, Optional ByRef start As Integer = 0) As String
        Dim i As Short
        Dim st As String = ""
        Dim temp As Short
        Dim j As Integer
        j = Srclen - 1
        If IsNothing(start) Then
            start = 0
        End If
        For i = start To j '看src这个数组是从0开始还是从1开始,宏电的是从1开始
            temp = src(i)\16
            If temp > 9 Then
                temp = temp + 55
            Else
                temp = temp + 48
            End If
            st = st & Chr(temp)

            temp = src(i) Mod 16
            If temp > 9 Then
                temp = temp + 55
            Else
                temp = temp + 48
            End If
            st = st & Chr(temp)

            'st = st & " "
        Next
        HextoStr = st
    End Function


    Function Isnumber(ByRef txt As String) As Boolean
        Dim i As Integer
        Isnumber = True
        For i = 1 To Len(txt)
            If Mid(txt, i, 1) >= "0" And Mid(txt, i, 1) <= "9" Then
            Else
                Isnumber = False
            End If
        Next i
    End Function


    Function GetGreaterPrime(ByRef baseNum As Integer) As Integer '获得大于该数(>500)的最小质数
        Dim i As Integer
        Dim find As Boolean
        find = False
        Do While Not find
            baseNum = baseNum + 1
            If baseNum Mod 2 <> 0 Then
                For i = 3 To baseNum Step 2

                    If baseNum Mod i = 0 Then
                        Exit For
                    ElseIf i = baseNum - 2 Then
                        find = True
                        Exit For
                    End If
                Next i
                GetGreaterPrime = baseNum
            End If
        Loop
    End Function


    Function GetCRCHi(ByRef Ind As Integer) As Byte
        GetCRCHi = Choose(Ind + 1, &H0, &HC0, &HC1, &H1, &HC3, &H3, &H2, &HC2, &HC6, &H6, &H7, &HC7, &H5, &HC5, &HC4,
                          &H4, &HCC, &HC, &HD, &HCD, &HF, &HCF, &HCE, &HE, &HA, &HCA, &HCB, &HB, &HC9, &H9, &H8, &HC8,
                          &HD8, &H18, &H19, &HD9, &H1B, &HDB, &HDA, &H1A, &H1E, &HDE, &HDF, &H1F, &HDD, &H1D, &H1C, &HDC,
                          &H14, &HD4, &HD5, &H15, &HD7, &H17, &H16, &HD6, &HD2, &H12, &H13, &HD3, &H11, &HD1, &HD0, &H10,
                          &HF0, &H30, &H31, &HF1, &H33, &HF3, &HF2, &H32, &H36, &HF6, &HF7, &H37, &HF5, &H35, &H34, &HF4,
                          &H3C, &HFC, &HFD, &H3D, &HFF, &H3F, &H3E, &HFE, &HFA, &H3A, &H3B, &HFB, &H39, &HF9, &HF8, &H38,
                          &H28, &HE8, &HE9, &H29, &HEB, &H2B, &H2A, &HEA, &HEE, &H2E, &H2F, &HEF, &H2D, &HED, &HEC, &H2C,
                          &HE4, &H24, &H25, &HE5, &H27, &HE7, &HE6, &H26, &H22, &HE2, &HE3, &H23, &HE1, &H21, &H20, &HE0,
                          &HA0, &H60, &H61, &HA1, &H63, &HA3, &HA2, &H62, &H66, &HA6, &HA7, &H67, &HA5, &H65, &H64, &HA4,
                          &H6C, &HAC, &HAD, &H6D, &HAF, &H6F, &H6E, &HAE, &HAA, &H6A, &H6B, &HAB, &H69, &HA9, &HA8, &H68,
                          &H78, &HB8, &HB9, &H79, &HBB, &H7B, &H7A, &HBA, &HBE, &H7E, &H7F, &HBF, &H7D, &HBD, &HBC, &H7C,
                          &HB4, &H74, &H75, &HB5, &H77, &HB7, &HB6, &H76, &H72, &HB2, &HB3, &H73, &HB1, &H71, &H70, &HB0,
                          &H50, &H90, &H91, &H51, &H93, &H53, &H52, &H92, &H96, &H56, &H57, &H97, &H55, &H95, &H94, &H54,
                          &H9C, &H5C, &H5D, &H9D, &H5F, &H9F, &H9E, &H5E, &H5A, &H9A, &H9B, &H5B, &H99, &H59, &H58, &H98,
                          &H88, &H48, &H49, &H89, &H4B, &H8B, &H8A, &H4A, &H4E, &H8E, &H8F, &H4F, &H8D, &H4D, &H4C, &H8C,
                          &H44, &H84, &H85, &H45, &H87, &H47, &H46, &H86, &H82, &H42, &H43, &H83, &H41, &H81, &H80, &H40)
    End Function


    Function GetCRCLo(ByRef Ind As Integer) As Byte
        GetCRCLo = Choose(Ind + 1, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81,
                          &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80,
                          &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80,
                          &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81,
                          &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80,
                          &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81,
                          &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81,
                          &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80,
                          &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80,
                          &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81,
                          &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81,
                          &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80,
                          &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81,
                          &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80,
                          &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80,
                          &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81,
                          &H40)
    End Function


    Function CRC16(ByRef Data() As Byte) As Object
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
        CRC16 = ReturnData
    End Function

    Function GetUint32Value(ByVal V As Byte(), ByVal start As Int16) As UInt32
        Dim V1(3) As Byte, V2(3) As Byte

        Dim i As Long
        For i = 0 To 3
            V1(i) = V(start + i)
        Next i

        V2(0) = V1(3)
        V2(1) = V1(2)
        V2(2) = V1(1)
        V2(3) = V1(0)
        GetUint32Value = BitConverter.ToUInt32(V2, 0)
    End Function

    Function GetDoubleValue(ByVal V As Byte(), ByVal start As Int16) As Double
        Dim V1(7) As Byte, V2(7) As Byte

        Dim i As Long
        For i = 0 To 7
            V1(i) = V(start + i)
        Next i

        V2(0) = V1(7)
        V2(1) = V1(6)
        V2(2) = V1(5)
        V2(3) = V1(4)
        V2(4) = V1(3)
        V2(5) = V1(2)
        V2(6) = V1(1)
        V2(7) = V1(0)
        GetDoubleValue = BitConverter.ToDouble(V2, 0)
    End Function
End Module
