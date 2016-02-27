'Imports ModbusServer.Device.Datatype
Imports Microsoft.VisualBasic.DateAndTime
Imports MBsrv

Public Class MRDataBlock
    Implements IDataBlock

    Dim mAddr As Integer
    Dim mBlockName As String
    Dim mEnable As Boolean
    Dim ItmCol As New Microsoft.VisualBasic.Collection
    Dim mSingleRecordLength As Integer


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
        Dim C(0) As Byte
        C(0) = &HFF

        AddByteToArray(C, &HFF)
        AddByteToArray(C, ToBCD(Year(Now) - 2000))
        AddByteToArray(C, ToBCD(Month(Now)))
        AddByteToArray(C, ToBCD(Day(Now)))
        AddByteToArray(C, ToBCD(Hour(Now)))
        AddByteToArray(C, ToBCD(Minute(Now)))
        AddByteToArray(C, ToBCD(Second(Now)))
        AddByteToArray(C, ToBCD(0))
        AddByteToArray(C, ToBCD(Weekday(Now)))
        AddByteToArray(C, &HFF)
        AddByteToArray(C, &HFF)
        GetCommandBytes = C
    End Function

    Function ToBCD(ByVal v As Integer) As Byte
        Dim TenP As Byte
        Dim Lessthan9 As Byte

        TenP = v \ 10
        Lessthan9 = v Mod 10
        TenP = TenP << 4
        ToBCD = Lessthan9 + TenP
    End Function

    Sub AddByteToArray(ByRef C() As Byte, ByVal V As Byte)
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
        Dim W() As UInt16
        Dim AllLen As Integer
        Dim WordLen As Integer
        'Dim SingleLen As Integer

        AllLen = length - 5 '减5，
        WordLen = AllLen / 2
        ReDim W(WordLen - 1)
        Buffer.BlockCopy(Rvdata, 5, W, 0, AllLen)

        If WordLen = Me.SingleRecordLength Then '长度对

            GetValueFromRvData = True

            For j As Int16 = 0 To Me.mSvrAddrLength - 1
                Mbs.WritevaluebyAd(Me.SvrDevAd, GetMBADstr(Me.SvrMBADStart + j + 1), Device.Datatype.无符号整型, W(j))

            Next

            AddOutDataToBuff()


        Else
            GetValueFromRvData = False
        End If
    End Function

    Function GetRecordTime() As String
        Dim Y As Integer
        Dim M As Integer
        Dim D As Integer
        Dim N As Integer
        Dim H As Integer
        Dim S As Integer
        Dim AD As String
        Dim V1 As String
        AD = Me.GetMBADstr(Me.SvrMBADStart + 1)
        Dim V As Long
        V = Mbs.ReadvalueByAd(Me.SvrDevAd, AD, MBsrv.Device.Datatype.无符号整型)
        V1 = V.ToString("X4")

        M = Left(V1, 2)

        Y = Right(V1, 2)
        V = Mbs.ReadvalueByAd(Me.SvrDevAd, AD + 1, MBsrv.Device.Datatype.无符号整型)
        V1 = V.ToString("X4")
        H = Left(V1, 2)

        D = Right(V1, 2)
        V = Mbs.ReadvalueByAd(Me.SvrDevAd, AD + 2, MBsrv.Device.Datatype.无符号整型)
        V1 = V.ToString("X4")
        S = Left(V1, 2)

        N = Right(V1, 2)
        Dim D1 As Date


        D1 = New Date(2000 + Y, M, D, H, N, S)

        GetRecordTime = D1.ToOADate
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
