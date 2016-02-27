Imports MBsrv
Imports MBsrv.Device.Datatype
Imports Microsoft.VisualBasic.DateAndTime

Public Class LDDataBlock
    Implements IDataBlock

    Dim mAddr As Integer
    Dim mBlockName As String
    Dim mEnable As Boolean
    Dim ItmCol As New Microsoft.VisualBasic.Collection
    Dim mSingleRecordLength As Integer
    Public RegToMBcol As Hashtable = New Hashtable

    Sub AddRegisterTomb(ByVal Reg As String, ByVal MBaddr As String)
        RegToMBcol.Add(Reg, MBaddr)
    End Sub

    Sub RemoveRegTomb(ByVal Reg As String)
        If Reg <> "06" And Reg <> "0F" Then
            RegToMBcol.Remove(Reg)
        End If
    End Sub

    Public Property SingleRecordLength() As Integer
        Get
            SingleRecordLength = mSingleRecordLength
        End Get
        Set(ByVal value As Integer)
            mSingleRecordLength = value '
            mSvrAddrLength = value '服务器地址空间长度与单记录长度一致。
        End Set
    End Property

    Public Property Addr() As Integer Implements IDataBlock.Addr
        Get
            Addr = mAddr
        End Get
        Set(ByVal value As Integer)
            mAddr = value
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
        Dim C(0) As Byte '2B4C414E44534841050420161722
        C(0) = &H2B

        AddByteToArray(C, &H4C)
        AddByteToArray(C, &H41)
        AddByteToArray(C, &H4E)
        AddByteToArray(C, &H44)
        AddByteToArray(C, &H53)
        AddByteToArray(C, &H48)
        AddByteToArray(C, &H41)
        AddByteToArray(C, ToBCD(Year(Now) - 2000))
        AddByteToArray(C, ToBCD(Month(Now)))
        AddByteToArray(C, ToBCD(Day(Now)))
        AddByteToArray(C, ToBCD(Hour(Now)))
        AddByteToArray(C, ToBCD(Minute(Now)))
        AddByteToArray(C, ToBCD(Second(Now)))

        GetCommandBytes = C
    End Function

    Private Function ToBCD(ByVal v As Integer) As Byte
        Dim TenP As Byte
        Dim Lessthan9 As Byte

        TenP = v \ 10
        Lessthan9 = v Mod 10
        TenP = TenP << 4
        ToBCD = Lessthan9 + TenP
    End Function


    Private Sub AddByteToArray(ByRef C() As Byte, ByVal V As Byte)
        Dim i As Integer
        i = C.Length
        ReDim Preserve C(i)


        C(i) = V
    End Sub

    Private Function GetMBADstr(ByVal Ad As Integer) As String
        GetMBADstr = (400001 + Ad).ToString
    End Function

    Public Function GetValueFromRvData(ByVal length As Integer, ByVal Rvdata() As Byte) As Boolean _
        Implements IDataBlock.GetValueFromRvData
        'Dim W() As UInt16
        Dim AllLen As Integer
        'Dim WordLen As Integer
        'Dim SingleLen As Integer
        'Rv 684b4b680000 00 00 00 01 80 cd 2171001875320400000083410200000000004173000000000041010000002994417400000081802010000072301100210849500f0000000000006006000903180924023f16
        'AllLen = Rvdata(3) + Rvdata(4) * 256
        AllLen = Rvdata(1) - 8
        Dim Datal As Integer = 0
        Dim FH As Boolean
        Dim RegAd As Byte
        Dim MBad As String
        Do
            Dim L As Byte
            Dim DotP As Integer = 0
            Dim V As Long
            Dim Fv As Double
            Dim i As Integer
            L = Rvdata(12 + Datal)
            L = L And &HF0
            L = L >> 4
            FH = (Rvdata(12 + Datal) And &H4) Xor &H4
            DotP = Rvdata(12 + Datal) And (&H7)

            RegAd = Rvdata(13 + Datal)
            MBad = RegToMBcol(RegAd.ToString("X2"))
            Select Case RegAd
                Case &H6 '为时间
                    Dim Y As Integer
                    Dim M As Integer
                    Dim D As Integer
                    Dim N As Integer
                    Dim H As Integer
                    Dim S As Integer
                    Dim D1 As Date
                    Dim D2 As Int32
                    Y = Rvdata(15 + Datal + 0).ToString("X2")
                    M = Rvdata(15 + Datal + 1).ToString("X2")
                    D = Rvdata(15 + Datal + 2).ToString("X2")
                    H = Rvdata(15 + Datal + 3).ToString("X2")
                    N = Rvdata(15 + Datal + 4).ToString("X2")
                    S = Rvdata(15 + Datal + 5).ToString("X2")
                    D1 = New Date(2000 + Y, M, D, H, N, S)
                    D2 = DateDiff("s", New Date(2005, 1, 1), D1)

                    Mbs.WritevaluebyAd(Me.SvrDevAd, MBad, Device.Datatype.无符号整型, D2 Mod 65536)
                    Mbs.WritevaluebyAd(Me.SvrDevAd, MBad + 1, Device.Datatype.无符号整型, D2 \ 65536)
                Case &HF '为数字量
                    For i = 0 To L - 1
                        V = V + Val(Rvdata(15 + Datal + i).ToString("X2")) * (100 ^ i)
                    Next
                    Mbs.WritevaluebyAd(Me.SvrDevAd, MBad, 浮点数, V)
                Case Else
                    V = 0
                    For i = 0 To L - 1
                        V = V * 100 + Val(Rvdata(15 + Datal + i).ToString("X2"))
                    Next
                    Fv = V / (10 ^ DotP)
                    If Not FH Then
                        V = 0 - V
                    End If
                    If MBad IsNot Nothing Then
                        Mbs.WritevaluebyAd(Me.SvrDevAd, MBad, 浮点数, Fv)
                    End If
            End Select
            Datal += L + 3

        Loop Until Datal >= AllLen
        AddOutDataToBuff()
        GetValueFromRvData = True
    End Function

    Function GetRecordTime() As String
        Dim D1 As UInt16
        D1 = Mbs.ReadvalueByAd(Me.SvrDevAd, RegToMBcol("06"), Device.Datatype.无符号整型)

        Dim D2 As UInt32
        D2 = Mbs.ReadvalueByAd(Me.SvrDevAd, RegToMBcol("06") + 1, Device.Datatype.无符号整型)
        Dim D3 As Date
        D3 = DateAdd("s", D2 * 65536 + D1, New Date(2005, 1, 1))


        GetRecordTime = D3.ToOADate
    End Function

    Function SwapBytes(ByVal V As UInt16) As UInt16
        Dim v1(1) As Byte
        v1 = BitConverter.GetBytes(V)
        Dim v2 As Byte
        v2 = v1(0)
        v1(0) = v1(1)
        v1(1) = v2
        SwapBytes = BitConverter.ToUInt16(v1, 0)
    End Function

    Function AddOutDataToBuff() As Boolean

        Dim R As String
        'Dim i As Integer
        Dim Itmname As String
        Dim Itm As CItem

        R = "N{"
        R = R & "STime:F:" & GetRecordTime.ToString   '"5.0025" '
        For Each Itmname In Me.ItmCol
            Itm = GItemCol(Itmname)

            If R = "N{" Then

                R = R & Itm.ItemName & ":" & Itm.GetDTstr & ":" & CStr(Itm.FloatV)
            Else
                R = R & "," & Itm.ItemName & ":" & Itm.GetDTstr & ":" & CStr(Itm.FloatV)
            End If

        Next
        R = R & "}"
        TCPsocket.RecordBuff.Add(R)

        If TCPsocket.Visible = True Then
            TCPsocket.txtmbsend.Text = TCPsocket.txtmbsend.Text & Chr(13) & Chr(10) & R
        End If
        AddOutDataToBuff = True
        'SendOutData = TCPsocket.BroadCastSend(R)
    End Function

    Dim mSvrAddrLength As Integer

    Public Property SvrAddrLength() As Integer Implements IDataBlock.SvrAddrLength
        Get
            SvrAddrLength = mSvrAddrLength
        End Get
        Set(ByVal value As Integer)
            mSvrAddrLength = value
        End Set
    End Property

    Dim mSvrDevAd As Integer

    Public Property SvrDevAd() As Integer Implements IDataBlock.SvrDevAd
        Get
            SvrDevAd = mSvrDevAd
        End Get
        Set(ByVal value As Integer)
            mSvrDevAd = value
        End Set
    End Property

    Dim mSvrMBadStart As Integer

    Public Property SvrMBADStart() As Integer Implements IDataBlock.SvrMBADStart
        Get
            SvrMBADStart = mSvrMBadStart
        End Get
        Set(ByVal value As Integer)
            mSvrMBadStart = value
        End Set
    End Property

    Sub AddItm(ByVal ItmName As String) Implements IDataBlock.AddItm
        ItmCol.Add(ItmName)
    End Sub

    Public Sub New()
        '0 C(400012)
        '03	400014
        '0E	400016
        '06	400001
        '0F	400003
        '04	400004
        '07	400018
        '01	400006
        '02	400008
        '0B(400010)
        RegToMBcol.Add("06", "400001")
        RegToMBcol.Add("0F", "400003")
        RegToMBcol.Add("04", "400004")
        RegToMBcol.Add("01", "400006")
        RegToMBcol.Add("02", "400008")
        RegToMBcol.Add("0B", "400010")
        RegToMBcol.Add("0C", "400012")
        RegToMBcol.Add("03", "400014")
        RegToMBcol.Add("0E", "400016")
        RegToMBcol.Add("07", "400018")
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
