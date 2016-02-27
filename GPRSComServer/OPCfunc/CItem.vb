Option Strict Off
Option Explicit On
'Imports GPRSComServer.device
Imports System.Collections.Generic
Imports MBsrv.Device

'Imports GPRSComServer.Device

Friend Class CItem
    Implements _IItem
    Private mvarItemName As String
    Private mvarItemHandle As Integer
    Private mvarItemValue As Object
    Private mvarItemQuality As Short
    Private mvarItemConnected As Boolean
    Private mvarItemTimeStamp As Date
    Private mvarDevAD As Integer
    Private mvarMBAD As String
    Private mvarItemDatatype As String
    Private mvarAIrangeDown As Single
    Private mvarAIrangeUP As Single
    Private mvarConvertedDown As Single
    Private mvarConvertedUP As Single
    Private mvarNeedConvert As Boolean
    Private mvarIfSwapByte As Boolean
    Dim PreValue As Double
    Dim Filt As cFilter

    Public Property ChineseDis As String '中文说明，及单位等
    Public Property UnitStr As String
    Public Property Uplimit As Double
    Public Property DownLimit As Double
    Public Property HandleExpretion As String '处理表达式  格式（0：下线；1：在线）或（1：自动；2：手动；4：编程；8：停用）



    Public ReadOnly Property StrValue As String '值的中文表达
        Get
            If Me.HandleExpretion <> "" Then
                Dim exp() As String = Me.HandleExpretion.Split(";")
                Dim dic As New Dictionary(Of Double, String)
                For Each S As String In exp
                    dic.Add(S.Split(":")(0), S.Split(":")(1))
                Next
                If dic.ContainsKey(Me.ItemValue) Then
                    StrValue = dic(Me.ItemValue)
                Else
                    StrValue = Me.ItemValue.ToString
                End If

            Else
                StrValue = Convert.ToString(Me.ItemValue)
            End If
        End Get

    End Property



    Public Property IfSwapByte() As Boolean
        Get

            'Syntax: Debug.Print X.IfSwapByte
            IfSwapByte = mvarIfSwapByte
        End Get
        Set(ByVal Value As Boolean)

            'Syntax: X.IfSwapByte = 5
            mvarIfSwapByte = Value
        End Set
    End Property


    Public Property NeedConvert() As Boolean
        Get

            'Syntax: Debug.Print X.NeedConvert
            NeedConvert = mvarNeedConvert
        End Get
        Set(ByVal Value As Boolean)

            'Syntax: X.NeedConvert = 5
            mvarNeedConvert = Value
        End Set
    End Property


    Public Property convertedUP() As Single
        Get

            'Syntax: Debug.Print X.ConvertedUP
            convertedUP = mvarConvertedUP
        End Get
        Set(ByVal Value As Single)

            'Syntax: X.ConvertedUP = 5
            mvarConvertedUP = Value
        End Set
    End Property


    Public Property ConvertedDown() As Single
        Get

            'Syntax: Debug.Print X.ConvertedDown
            ConvertedDown = mvarConvertedDown
        End Get
        Set(ByVal Value As Single)

            'Syntax: X.ConvertedDown = 5
            mvarConvertedDown = Value
        End Set
    End Property


    Public Property Airangeup() As Single
        Get

            'Syntax: Debug.Print X.AIrangeUP
            Airangeup = mvarAIrangeUP
        End Get
        Set(ByVal Value As Single)

            'Syntax: X.AIrangeUP = 5
            mvarAIrangeUP = Value
        End Set
    End Property


    Public Property Airangedown() As Single
        Get

            'Syntax: Debug.Print X.AIrangeDown
            Airangedown = mvarAIrangeDown
        End Get
        Set(ByVal Value As Single)

            'Syntax: X.AIrangeDown = 5
            mvarAIrangeDown = Value
        End Set
    End Property


    Public Property ItemDataType() As String
        Get

            'Syntax: Debug.Print X.ItemDatatype
            ItemDataType = mvarItemDatatype
        End Get
        Set(ByVal Value As String)

            'Syntax: X.ItemDatatype = 5
            mvarItemDatatype = Value
        End Set
    End Property


    Public Property MBAD() As String
        Get

            'Syntax: Debug.Print X.MBAD
            MBAD = mvarMBAD
        End Get
        Set(ByVal Value As String)

            'Syntax: X.MBAD = 5
            mvarMBAD = Value
        End Set
    End Property


    Public Property devad() As Integer
        Get

            'Syntax: Debug.Print X.DevAD
            devad = mvarDevAD
        End Get
        Set(ByVal Value As Integer)

            'Syntax: X.DevAD = 5
            mvarDevAD = Value
        End Set
    End Property


    Public Property ItemTimeStamp() As Date
        Get

            'Syntax: Debug.Print X.ItemTimeStamp
            ItemTimeStamp = mvarItemTimeStamp
        End Get
        Set(ByVal Value As Date)

            'Syntax: X.ItemTimeStamp = 5
            mvarItemTimeStamp = Value
        End Set
    End Property


    Public Property ItemConnected() As Boolean
        Get

            'Syntax: Debug.Print X.ItemConnected
            ItemConnected = mvarItemConnected
        End Get
        Set(ByVal Value As Boolean)

            'Syntax: X.ItemConnected = 5
            mvarItemConnected = Value
        End Set
    End Property


    Public Property ItemQuality() As Short
        Get

            'Syntax: Debug.Print X.ItemQuality
            ItemQuality = mvarItemQuality
        End Get
        Set(ByVal Value As Short)

            mvarItemQuality = Value
        End Set
    End Property


    Public Property ItemValue() As Object
        Get

            Dim V As Object
            ReadDevice()
            If Me.NeedConvert Then

                V = (mvarItemValue - Me.Airangedown) / (Me.Airangeup - Me.Airangedown) * (Me.convertedUP - Me.ConvertedDown) +
                    Me.ConvertedDown
            Else
                V = mvarItemValue
            End If
            Select Case Me.ItemDataType
                Case "Boolean"
                    ItemValue = CBool(V)
                Case "Integer"
                    ItemValue = CSng(V)
                Case "Single"
                    ItemValue = CSng(V)
                Case "SingleSwapWord"
                    ItemValue = CSng(V)
                Case "Double"
                    ItemValue = CDbl(V)
                Case "DoubleSwapWord"
                    ItemValue = CDbl(V)
                Case "Long"
                    ItemValue = CDbl(V)
                Case "LongSwapWord"
                    ItemValue = CDbl(V)
                Case "UnInteger"
                    ItemValue = CDbl(V)
            End Select
            If ItemValue > Me.Uplimit Then
                ItemValue = Me.Uplimit
            End If
            If ItemValue < Me.DownLimit Then
                ItemValue = Me.DownLimit
            End If

        End Get
        Set(ByVal Value As Object)
            If IsReference(Value) And Not TypeOf Value Is String Then
                mvarItemValue = Value
            Else

                Dim V As Object
                If Me.NeedConvert Then
                    On Error Resume Next
                    V = (Value - Me.ConvertedDown) / (Me.convertedUP - Me.ConvertedDown) * (Me.Airangeup - Me.Airangedown) +
                        Me.Airangedown
                Else
                    V = Value
                End If
                mvarItemValue = V
            End If
        End Set
    End Property


    Public Property Itemhandle() As Integer
        Get

            'Syntax: Debug.Print X.ItemHandle
            Itemhandle = mvarItemHandle
        End Get
        Set(ByVal Value As Integer)

            'Syntax: X.ItemHandle = 5
            mvarItemHandle = Value
        End Set
    End Property


    Public Property ItemName() As String
        Get

            'Syntax: Debug.Print X.ItemName
            ItemName = mvarItemName
        End Get
        Set(ByVal Value As String)

            'Syntax: X.ItemName = 5
            mvarItemName = Value
        End Set
    End Property


    Private Property IItem_ItemDataType() As String Implements _IItem.ItemDataType
        Get
            IItem_ItemDataType = mvarItemDatatype
        End Get
        Set(ByVal Value As String)
            mvarItemDatatype = Value
        End Set
    End Property


    Private Property IItem_ItemFieldName() As String Implements _IItem.ItemFieldName
        Get
            IItem_ItemFieldName = mvarItemName
        End Get
        Set(ByVal Value As String)
            mvarItemName = Value
        End Set
    End Property


    Private Property IItem_ItemValue() As Object Implements _IItem.ItemValue
        Get
            IItem_ItemValue = Me.ItemValue
        End Get
        Set(ByVal Value As Object)
            If IsReference(Value) And Not TypeOf Value Is String Then
                Me.ItemValue = Value
            Else
                Me.ItemValue = Value

            End If
        End Set
    End Property


    Public Sub WriteDevice(ByRef value As Object)
        'write control data to device

        mvarItemTimeStamp = Now
        mvarItemQuality = 192
        Me.ItemValue = value
        RTUs.WriteToRTU(Me.devad, Me.MBAD, mvarItemValue)
        Select Case Me.ItemDataType
            Case "Boolean"
            Case "Integer"
                Mbs.WritevaluebyAd(devad, MBAD, Datatype.无符号整型, CInt(value))
            Case "Single"
            Case "SingleSwapWord"
            Case CStr(VariantType.String)
                '            If n < 3 Then
                '                mvarItemValue = "http://www.eehoo.net"
                '            ElseIf n < 5 Then
                '                mvarItemValue = "sales@eehoo.net"
                '            ElseIf n < 8 Then
                '                mvarItemValue = "Knight.OPC.Server.VB"
                '            ElseIf n < 10 Then
                '                mvarItemValue = "KOSRDK"
                '            End If
        End Select
    End Sub

    Public Sub ReadDevice()
        'read device data from your hardware
        'we just do some simulation

        'If mvarItemConnected = False Then  ' no client requested this item
        '    mvarItemQuality = 64
        '    Exit Sub
        'End If
        Dim UnCovertV As Double
        Try
            Dim Arrmbad() As String
            Select Case Me.ItemDataType
                Case "Boolean" '布尔有两种情况，一种是直接读1或0打头的寄存器即1x 或 0x ，另一种是读取 4x 或 3x 寄存器的16位中的一个bit.通过判断地址可以区分两种情况
                    If Len(MBAD) > 6 Then

                        Arrmbad = Split(MBAD, ":")


                        UnCovertV = CBool(Mbs.ReadvalueByAd(devad, Arrmbad(0), Datatype.布尔, CInt(Arrmbad(1))))
                    Else
                        UnCovertV = CBool(Mbs.ReadvalueByAd(devad, MBAD, Datatype.布尔))
                    End If
                    '        Case vbInteger
                    '            UnCovertV = CInt(UnCovertV) + 1
                Case "Integer"
                    If Me.IfSwapByte Then
                        UnCovertV = CInt(Mbs.ReadValueSwapByte(devad, MBAD, Datatype.有符号整型))
                    Else
                        UnCovertV = CInt(Mbs.ReadvalueByAd(devad, MBAD, Datatype.有符号整型))
                    End If
                Case "Long"
                    If Me.IfSwapByte Then
                        UnCovertV = CUInt(Mbs.ReadValueSwapByte(devad, MBAD, Datatype.长整型))
                    Else
                        UnCovertV = CUInt(Mbs.ReadvalueByAd(devad, MBAD, Datatype.长整型))
                    End If
                Case "LongSwapWord"
                    If Me.IfSwapByte Then
                        UnCovertV = CUInt(Mbs.ReadValueSwapByte(devad, MBAD, Datatype.长整型高低字交换))
                    Else
                        UnCovertV = CUInt(Mbs.ReadvalueByAd(devad, MBAD, Datatype.长整型高低字交换))
                    End If
                Case "Single"
                    If Me.IfSwapByte Then
                        UnCovertV = CSng(Mbs.ReadValueSwapByte(devad, MBAD, Datatype.浮点数))
                    Else
                        UnCovertV = CSng(Mbs.ReadvalueByAd(devad, MBAD, Datatype.浮点数))
                    End If
                Case "SingleSwapWord"
                    If Me.IfSwapByte Then
                        UnCovertV = CSng(Mbs.ReadValueSwapByte(devad, MBAD, Datatype.浮点数高低字交换))
                    Else
                        UnCovertV = CSng(Mbs.ReadvalueByAd(devad, MBAD, Datatype.浮点数高低字交换))
                    End If
                Case "Double"
                    If Me.IfSwapByte Then
                        UnCovertV = CDbl(Mbs.ReadValueSwapByte(devad, MBAD, Datatype.双精度))
                    Else
                        UnCovertV = CDbl(Mbs.ReadvalueByAd(devad, MBAD, Datatype.双精度))
                    End If

                Case "DoubleSwapWord"
                    If Me.IfSwapByte Then
                        UnCovertV = CDbl(Mbs.ReadValueSwapByte(devad, MBAD, Datatype.双精度高低字交换))
                    Else
                        UnCovertV = CDbl(Mbs.ReadvalueByAd(devad, MBAD, Datatype.双精度高低字交换))
                    End If
                Case "UnInteger"
                    If Me.IfSwapByte Then
                        UnCovertV = CDbl(Mbs.ReadValueSwapByte(devad, MBAD, Datatype.无符号整型))
                    Else
                        UnCovertV = CDbl(Mbs.ReadvalueByAd(devad, MBAD, Datatype.无符号整型))
                    End If
                Case CStr(VariantType.String)
                  
            End Select
            If Me.NeedConvert Then
                If Me.PreValue <> UnCovertV Then
                    Me.PreValue = UnCovertV
                    mvarItemValue = Me.Filt.Filter(UnCovertV, False)
                End If
                If mvarItemValue = 0 Then
                    mvarItemValue = UnCovertV
                End If
                'If mvarItemValue > Me.Airangeup Then
                '    mvarItemValue = Me.Airangeup
                'ElseIf mvarItemValue < Me.Airangedown Then
                '    mvarItemValue = Me.Airangedown

                'End If
            Else
                mvarItemValue = UnCovertV
            End If

            mvarItemTimeStamp = Now
            mvarItemQuality = 192
        Catch ex As Exception
        End Try
    End Sub

    Function GetDTstr() As String
        Select Case Me.ItemDataType
            Case "Boolean"
                GetDTstr = "B"
            Case "Integer", "UnInteger"
                GetDTstr = "F"
            Case "Single"
                GetDTstr = "F"
            Case "SingleSwapWord"

                GetDTstr = "F"
            Case Else
                GetDTstr = "F"
        End Select
    End Function

    Function FloatV() As Single

        FloatV = Me.ItemValue
        If Me.ItemDataType = "Boolean" Then
            FloatV = IIf(Me.ItemValue, 1, 0)
        End If
    End Function

    Public Sub New()
        Filt = New cFilter
        Me.Uplimit = Double.MaxValue
        Me.DownLimit = Double.MinValue
    End Sub
End Class