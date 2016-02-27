Option Strict Off
Option Explicit On 
Module Module2
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' Copyright ?996-2000 VBnet, Randy Birch, All Rights Reserved.
    ' Some pages may also contain other copyrights by the author.
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' You are free to use this code within your own applications,
    ' but you are expressly forbidden from selling or otherwise
    ' distributing this source code without prior written consent.
    ' This includes both posting free demo projects made from this
    ' code as well as reproducing the code in text or html format.
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Public Const MAX_WSADescription As Short = 256
    Public Const MAX_WSASYSStatus As Short = 128
    Public Const ERROR_SUCCESS As Integer = 0
    Public Const WS_VERSION_REQD As Integer = &H101S
    Public Const WS_VERSION_MAJOR As Integer = WS_VERSION_REQD \ &H100S And &HFF
    Public Const WS_VERSION_MINOR As Integer = WS_VERSION_REQD And &HFF
    Public Const MIN_SOCKETS_REQD As Integer = 1
    Public Const SOCKET_ERROR As Integer = -1

    Public Structure HOSTENT
        Dim hName As Integer
        Dim hAliases As Integer
        Dim hAddrType As Short
        Dim hLen As Short
        Dim hAddrList As Integer
    End Structure

    Public Structure WSADATA
        Dim wVersion As Short
        Dim wHighVersion As Short
        <VBFixedArray(MAX_WSADescription)> Dim szDescription() As Byte
        <VBFixedArray(MAX_WSASYSStatus)> Dim szSystemStatus() As Byte
        Dim wMaxSockets As Short
        Dim wMaxUDPDG As Short
        Dim dwVendorInfo As Integer

        Public Sub Initialize()
            ReDim szDescription(MAX_WSADescription)
            ReDim szSystemStatus(MAX_WSASYSStatus)
        End Sub
    End Structure


    Public Declare Function WSAGetLastError Lib "WSOCK32.DLL" () As Integer

    Public Declare Function WSAStartup Lib "WSOCK32.DLL" (ByVal wVersionRequired As Integer, ByRef lpWSADATA As WSADATA) As Integer

    Public Declare Function WSACleanup Lib "WSOCK32.DLL" () As Integer

    Public Declare Function gethostname Lib "WSOCK32.DLL" (ByVal szHost As String, ByVal dwHostLen As Integer) As Integer

    Public Declare Function gethostbyname Lib "WSOCK32.DLL" (ByVal szHost As String) As Integer

    Public Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" (ByRef hpvDest As HOSTENT, ByVal hpvSource As Integer, ByVal cbCopy As Integer)
    Public Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" (ByRef hpvDest As Integer, ByVal hpvSource As Integer, ByVal cbCopy As Integer)
    Public Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" (ByRef hpvDest As Byte, ByVal hpvSource As Integer, ByVal cbCopy As Integer)

    'Public Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" (hpvDest As Any, hpvSource As Any, ByVal cbCopy As Long)

    Public Declare Function htonl Lib "WSOCK32.DLL" (ByVal lVal As Integer) As Integer

    Public Declare Function inet_addr Lib "WSOCK32.DLL" (ByVal szIPAddr As String) As Integer





    Public Function HexStrToStr(ByRef src() As Byte, ByRef ln As Short) As Object
        Dim i As Short
        Dim st As String
        Dim flag As Short
        Dim temp As Short
        Dim temp1 As Short

        flag = 0

        For i = 1 To ln
            temp1 = -1
            '0-9
            If (src(i) >= &H30S And src(i) <= &H39S) Then
                temp1 = src(i) - &H30S
            'A-F
            ElseIf (src(i) >= &H41S And src(i) <= &H46S) Then
                temp1 = src(i) - &H37S
            'a-f
            ElseIf (src(i) >= &H61S And src(i) <= &H66S) Then
                temp1 = src(i) - &H57S
            ElseIf (ChrW(src(i)) <> " ") Then
                MsgBox("发送数据中有超出16进制范围的字符！")
                HexStrToStr = ""
                Exit Function
            End If

            If (temp1 >= 0) Then

                If flag = 0 Then
                    temp = temp1
                End If

                If flag = 1 Then
                    temp = temp * 16 + temp1
                    st = st & Chr(temp)
                End If
                flag = (flag + 1) Mod 2
            End If
        Next
        HexStrToStr = st
    End Function


    Public Function StrToHexStr(ByRef src() As Byte, ByRef ln As Short) As Object
        Dim i As Short
        Dim st As String
        Dim temp As Short

        For i = 0 To (ln - 1)
            temp = src(i) \ 16
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

            st = st & " "
        Next
        StrToHexStr = st
    End Function

    Public Function CByteToLong(ByRef bytBuf() As Byte, ByRef iBufLen As Short) As Integer
        Dim i As Short
        Dim lValue As Integer
        lValue = 0
        For i = 1 To iBufLen
            lValue = lValue + CInt(bytBuf(i)) * (256 ^ (iBufLen - i))
        Next
        CByteToLong = lValue
    End Function

    Public Function CStrToByte(ByRef bytBuf() As Byte, ByVal strValue As String) As Object
        Dim i As Short
        For i = 1 To Len(strValue)
            bytBuf(i) = Asc(Mid(strValue, i, 1))
        Next
        bytBuf(Len(strValue) + 1) = 0
    End Function
    Public Function CmpStr(ByVal strValue As String) As Integer
        Dim i As Short
        Dim Buf(130) As Byte
        CStrToByte(Buf, strValue)
        For i = 1 To Len(strValue)
            If (Buf(i) < 48) Or (Buf(i) > 57) Then
                CmpStr = 0
                Exit Function
            End If
        Next
        CmpStr = 1
    End Function


    Public Function IsValidateIP(ByVal strIP As String) As Boolean
        Dim nPos As Short

        nPos = 0
        nPos = InStr(1, strIP, ".", 1)
        If nPos <> 0 Then
            nPos = InStr(nPos + 1, strIP, ".", 1)
        End If
        If nPos <> 0 Then
            nPos = InStr(nPos + 1, strIP, ".", 1)
        End If
        If nPos = 0 Then
            IsValidateIP = False
            Exit Function
        End If

        If inet_addr(strIP) = -1 Then
            IsValidateIP = False
        Else
            IsValidateIP = True
        End If

    End Function
End Module