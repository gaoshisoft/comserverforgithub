Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("DBlock_NET.DBlock")> Public Class DBlock
	Private mvarblockName As String '局部复制
	Private mvarAD As Byte '局部复制
	Private mvarFC As Byte '局部复制
	Private mvarLength As Integer '局部复制
	Private mvarEnable As Boolean '局部复制
	Private mvarStartAD As String '局部复制
	Public Sendtime As Integer
	
	Dim HoldingRegisters() As Byte
	Dim InputRegisters() As Byte
	Dim InputStatus() As Boolean
	Dim CoilStatus() As Boolean
	Public Keystr As String
	

	
	
	Public Property startad() As String
		Get
			'检索属性值时使用，位于赋值语句的右边。
			'Syntax: Debug.Print X.StartAD
			startad = mvarStartAD
		End Get
		Set(ByVal Value As String)
			'向属性指派值时使用，位于赋值语句的左边。
			'Syntax: X.StartAD = 5
			mvarStartAD = Value
		End Set
	End Property
	Public ReadOnly Property LngStartAD() As String
		Get
			'检索属性值时使用，位于赋值语句的右边。
			'Syntax: Debug.Print X.StartAD
			LngStartAD = CStr(Val(Right(mvarStartAD, Len(mvarStartAD) - 1)) - 1)
		End Get
	End Property
	
	Public ReadOnly Property EndAd() As String
		Get
			
			EndAd = Trim(Str(CInt(Me.startad) + Me.Length - 1))
			
		End Get
	End Property
	
	
	
	Public Property Enable() As Boolean
		Get
			'检索属性值时使用，位于赋值语句的右边。
			'Syntax: Debug.Print X.Enable
			Enable = mvarEnable
		End Get
		Set(ByVal Value As Boolean)
			'向属性指派值时使用，位于赋值语句的左边。
			'Syntax: X.Enable = 5
			mvarEnable = Value
		End Set
	End Property
	
	
	
	
	
	Public Property Length() As Integer
		Get
			'检索属性值时使用，位于赋值语句的右边。
			'Syntax: Debug.Print X.Length
			Length = mvarLength
			
		End Get
		Set(ByVal Value As Integer)
			'向属性指派值时使用，位于赋值语句的左边。
			'Syntax: X.Length = 5
			mvarLength = Value
			ReDim HoldingRegisters(Value * 2 - 1)
			ReDim InputRegisters(Value * 2 - 1)
			ReDim InputStatus(Value - 1)
			ReDim CoilStatus(Value - 1)
		End Set
	End Property
	
	
	
	
	
	
	
	
	
	Public ReadOnly Property FC() As Byte
		Get
			'检索属性值时使用，位于赋值语句的右边。
			'Syntax: Debug.Print X.FC
			Select Case Left(Me.startad, 1)
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
	
	
	
	
	
	Public Property ad() As Byte
		Get
			'检索属性值时使用，位于赋值语句的右边。
			'Syntax: Debug.Print X.AD
			ad = mvarAD
		End Get
		Set(ByVal Value As Byte)
			'向属性指派值时使用，位于赋值语句的左边。
			'Syntax: X.AD = 5
			mvarAD = Value
		End Set
	End Property
	
	
	
	
	
	Public Property blockName() As String
		Get
			'检索属性值时使用，位于赋值语句的右边。
			'Syntax: Debug.Print X.blockName
			blockName = mvarblockName
		End Get
		Set(ByVal Value As String)
			'向属性指派值时使用，位于赋值语句的左边。
			'Syntax: X.blockName = 5
			mvarblockName = Value
		End Set
	End Property
	
	Function GetBitValue(ByVal Adr As String, ByVal bitAdr As Integer) As Short 'bitadr为从0开始
		Dim i As Integer
		Dim mName As String
		mName = Trim(Adr)
		Select Case Left(mName, 1)
			Case "4"
				
				i = 256 * HoldingRegisters(Val(Right(mName, Len(mName) - 1)) * 2 - 2) + HoldingRegisters(Val(CStr(CDbl(Right(mName, Len(mName) - 1)) * 2 - 1)))
				GetBitValue = IIf((i And (2 ^ bitAdr)) = 2 ^ bitAdr, 1, 0)
				
			Case "3"
				i = 256 * InputRegisters(Val(CStr(CDbl(Right(mName, Len(mName) - 1)) * 2 - 2))) + InputRegisters(Val(CStr(CDbl(Right(mName, Len(mName) - 1)) * 2 - 1)))
				GetBitValue = IIf((i And (2 ^ bitAdr)) = 2 ^ bitAdr, 1, 0)
			Case "0"
				GetBitValue = IIf((CoilStatus(Val(CStr(CDbl(Right(mName, Len(mName) - 1)) - 1)))), 1, 0)
			Case "1"
				GetBitValue = IIf((InputStatus(Val(CStr(CDbl(Right(mName, Len(mName) - 1)) - 1)))), 1, 0)
		End Select
	End Function
	
	Function GetBitstrByad(ByVal Adr As String) As String
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
	
	Function GetFloatValueByad(ByVal Adr As String, ByVal Swapword As Boolean) As Single
		Dim i As Single
		Dim ad As Integer
		
		
		Dim Tmp(3) As Byte
		Dim result As String
		Dim mName As String
		mName = Trim(Adr)
		Dim byteAD As Integer
		byteAD = Val(CStr(CDbl(Right(mName, Len(mName) - 1)) * 2 - 2))
		If byteAD + 3 < Me.Length * 2 Then
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
		'tmp.
		'i = CSng(Tmp(0))
        'CopyMemory(i, Tmp(0), 4)
		'i = IEEE754Float(Tmp, 0)
        i = BitConverter.ToSingle(Tmp, 0)
		
		GetFloatValueByad = i
	End Function
	Sub WriteHoldingByte(ByVal i As Integer, ByVal v As Byte)
		HoldingRegisters(i) = v
	End Sub
	Sub WriteInputRegisterByte(ByVal i As Integer, ByVal v As Byte)
		InputRegisters(i) = v
	End Sub
	Sub WriteInputStatus(ByVal i As Integer, ByVal v As Boolean)
		InputStatus(i) = v
	End Sub
	Sub WriteCoilStatus(ByVal i As Integer, ByVal v As Boolean)
		CoilStatus(i) = v
	End Sub
	Public Function ReadModbusbyAD(ByVal mbName As String, ByVal Datatype As MBeClient.Datatype, Optional ByRef Bit As Object = Nothing) As Object
        Dim RetValue As Object
        Dim Tmp(3) As Byte
        Dim mName As String
        mName = Trim(mbName)
        If CDbl(mbName) > (CInt(Me.startad) + Me.Length - 1) Or Val(mbName) < Val(Me.startad) Then
            ReadModbusbyAD = 0

        End If
        mName = Left(mbName, 1) & VB6.Format(CInt(mName) - CInt(Me.startad) + 1, "00000")


        Select Case Left(mName, 1)

            Case "4"
                Select Case Datatype
                    Case 0 '整型
                        RetValue = 256 * HoldingRegisters(Val(CStr(CDbl(Right(mName, Len(mName) - 1)) * 2 - 2))) + HoldingRegisters(Val(CStr(CDbl(Right(mName, Len(mName) - 1)) * 2 - 1)))
                        If Not IsNothing(Bit) Then
                            RetValue = Me.GetBitValue(mbName, CInt(Bit))
                        End If
                    Case 1 '二进制形式
                        RetValue = GetBitstrByad(mName)
                    Case 2 '浮点数形式
                        RetValue = GetFloatValueByad(mName, False)
                    Case 3
                        RetValue = GetFloatValueByad(mName, True)

                End Select

            Case "3"
                Select Case Datatype
                    Case 0 '整型
                        RetValue = 256 * InputRegisters(Val(CStr(CDbl(Right(mName, Len(mName) - 1)) * 2 - 2))) + InputRegisters(Val(CStr(CDbl(Right(mName, Len(mName) - 1)) * 2 - 1)))
                        If Not IsNothing(Bit) Then
                            RetValue = Me.GetBitValue(mbName, CInt(Bit))
                        End If
                    Case 1
                        RetValue = GetBitstrByad(mName)
                    Case 2
                        RetValue = GetFloatValueByad(mName, False)
                    Case 3
                        RetValue = GetFloatValueByad(mName, True)
                End Select
            Case "0"
                RetValue = IIf((CoilStatus(Val(CStr(CDbl(Right(mName, Len(mName) - 1)) - 1)))), 1, 0)
            Case "1"
                RetValue = IIf((InputStatus(Val(CStr(CDbl(Right(mName, Len(mName) - 1)) - 1)))), 1, 0)
        End Select
        ReadModbusbyAD = RetValue
	End Function
	Public Function WriteModbusbyAD(ByVal mbName As String, ByVal Mbvalue As Object, ByVal Datatype As MBeClient.Datatype) As String
		Dim i As Integer
		Dim ad As Integer
		Dim v As Single
		Dim Tmp(3) As Byte
		'UPGRADE_WARNING: 未能解析对象 Mbvalue 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
		If Mbvalue > 65535 And Datatype = MBeClient.Datatype.整型 Then
			Exit Function
		End If
		'UPGRADE_WARNING: 未能解析对象 Mbvalue 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
		If Mbvalue < 0 And Datatype = MBeClient.Datatype.整型 Then
			Exit Function
		End If
		
		mbName = Trim(mbName)
		ad = Val(CStr(CDbl(Right(mbName, Len(mbName) - 1)) * 2 - 2))
		ad = ad - CDbl(Me.LngStartAD) + 1
		
		'On Error GoTo Errh
		Select Case Left(mbName, 1)
			Case "4"
				'         i = 256& * HoldingRegisters(Val(Right(mName, Len(mName) - 1) * 2 - 2)) + HoldingRegisters(Val(Right(mName, Len(mName) - 1) * 2 - 1))
				If ad > Me.Length Then
					'WritevaluebyAd = "地址太大！"
					Exit Function
				End If
				If Datatype = MBeClient.Datatype.浮点数 Then
					
					'UPGRADE_WARNING: 未能解析对象 Mbvalue 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					v = Mbvalue
					
                    'CopyMemory(Tmp(0), v, 4)
                    Tmp = BitConverter.GetBytes(v)
					HoldingRegisters(ad) = Tmp(1)
					HoldingRegisters(ad + 1) = Tmp(0)
					HoldingRegisters(ad + 2) = Tmp(3)
					HoldingRegisters(ad + 3) = Tmp(2)
					
				ElseIf Datatype = MBeClient.Datatype.浮点数高低字交换 Then 
					'UPGRADE_WARNING: 未能解析对象 Mbvalue 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					v = Mbvalue
					
                    Tmp = BitConverter.GetBytes(v)
					HoldingRegisters(ad) = Tmp(3)
					HoldingRegisters(ad + 1) = Tmp(2)
					HoldingRegisters(ad + 2) = Tmp(1)
					HoldingRegisters(ad + 3) = Tmp(0)
				Else
					
					'UPGRADE_WARNING: 未能解析对象 Mbvalue 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					HoldingRegisters(ad) = Mbvalue \ 256
					'UPGRADE_WARNING: 未能解析对象 Mbvalue 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					HoldingRegisters(ad + 1) = Mbvalue Mod 256
				End If
			Case "3"
				If ad > Me.Length Then
					'WritevaluebyAd = "地址太大！"
					Exit Function
				End If
				If Datatype = MBeClient.Datatype.浮点数 Then
					
					'UPGRADE_WARNING: 未能解析对象 Mbvalue 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					v = Mbvalue
					
                    Tmp = BitConverter.GetBytes(v)
					'                    Tmp = BitConverter.GetBytes(V)
					InputRegisters(ad) = Tmp(1)
					InputRegisters(ad + 1) = Tmp(0)
					InputRegisters(ad + 2) = Tmp(3)
					InputRegisters(ad + 3) = Tmp(2)
					
				ElseIf Datatype = MBeClient.Datatype.浮点数高低字交换 Then 
					'UPGRADE_WARNING: 未能解析对象 Mbvalue 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					v = Mbvalue
					
                    Tmp = BitConverter.GetBytes(v)
					InputRegisters(ad) = Tmp(3)
					InputRegisters(ad + 1) = Tmp(2)
					InputRegisters(ad + 2) = Tmp(1)
					InputRegisters(ad + 3) = Tmp(0)
				Else
					'UPGRADE_WARNING: 未能解析对象 Mbvalue 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					InputRegisters(ad) = Mbvalue \ 256
					'UPGRADE_WARNING: 未能解析对象 Mbvalue 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					InputRegisters(ad + 1) = Mbvalue Mod 256
				End If
			Case "0"
				If ad > Me.Length Then
					'WritevaluebyAd = "地址太大！"
					Exit Function
				End If
				'         i = IIf((CoilStatus(Val(Right(mName, Len(mName) - 1) - 1))), 1, 0)
				'UPGRADE_WARNING: 未能解析对象 Mbvalue 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				CoilStatus(Val(CStr(CDbl(Right(mbName, Len(mbName) - 1)) - 1))) = IIf(Mbvalue > 0, True, False)
			Case "1"
				If ad > Me.Length Then
					'WritevaluebyAd = "地址太大！"
					Exit Function
				End If
				'UPGRADE_WARNING: 未能解析对象 Mbvalue 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				InputStatus(Val(CStr(CDbl(Right(mbName, Len(mbName) - 1)) - 1))) = IIf(Mbvalue > 0, True, False)
		End Select
		Exit Function
		'Errh:
		'MsgBox("请将RTU的Modbus地址设为足够大！")
		
	End Function
	Function GetWriteCommandByte(ByVal MBAD As String, ByRef Length As Integer, ByVal Value As Object) As Object
		Dim i As Object
		Dim Mbcd() As Byte
		
		Dim Start As Integer
		Dim SlaveAd As Integer
		SlaveAd = Me.ad
		
		
		If Length = 1 Then
			ReDim Mbcd(11)
			Mbcd(0) = 1 \ 256 '标识巡测哪个数据块
			Mbcd(1) = 1 Mod 256
			Mbcd(2) = 0
			Mbcd(3) = 0
			Mbcd(4) = 6 \ 256
			Mbcd(5) = 6 Mod 256
			Select Case Left(MBAD, 1)
				Case CStr(0)
					
					Start = Val(Right(MBAD, 5)) - 1
					Mbcd(6) = SlaveAd
					
					Mbcd(7) = 5
					Mbcd(8) = Start \ 256
					Mbcd(9) = Start Mod 256
					'UPGRADE_WARNING: 未能解析对象 Value() 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					Mbcd(10) = Value(0)
					
					Mbcd(11) = 0
				Case CStr(4)
					Start = Val(Right(MBAD, 5)) - 1
					
					Mbcd(6) = SlaveAd
					
					Mbcd(7) = 6
					Mbcd(8) = Start \ 256
					Mbcd(9) = Start Mod 256
					'UPGRADE_WARNING: 未能解析对象 Value() 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					Mbcd(10) = Value(0)
					
					'UPGRADE_WARNING: 未能解析对象 Value() 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					Mbcd(11) = Value(1)
			End Select
			
		Else
			
			Select Case Left(MBAD, 1)
				Case CStr(0)
					ReDim Mbcd(13 + UBound(Value))
					Mbcd(0) = 1 \ 256 '标识巡测哪个数据块
					Mbcd(1) = 1 Mod 256
					Mbcd(2) = 0
					Mbcd(3) = 0
					Mbcd(4) = 6 \ 256
					Mbcd(5) = 6 Mod 256
					Start = Val(Right(MBAD, 5)) - 1
					
					
					Mbcd(6) = SlaveAd
					
					Mbcd(7) = 15
					Mbcd(8) = Start \ 256
					Mbcd(9) = Start Mod 256
					Mbcd(10) = Length \ 256
					
					Mbcd(11) = Length Mod 256
					Mbcd(12) = UBound(Value) + 1
					For i = 0 To UBound(Value)
						'UPGRADE_WARNING: 未能解析对象 i 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
						'UPGRADE_WARNING: 未能解析对象 Value() 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
						Mbcd(13 + i) = Value(i)
					Next i
				Case CStr(4)
					ReDim Mbcd(13 + UBound(Value))
					Mbcd(0) = 1 \ 256 '标识巡测哪个数据块
					Mbcd(1) = 1 Mod 256
					Mbcd(2) = 0
					Mbcd(3) = 0
					Mbcd(4) = 6 \ 256
					Mbcd(5) = 6 Mod 256
					Start = Val(Right(MBAD, 5)) - 1
					
					
					Mbcd(6) = SlaveAd
					
					Mbcd(7) = 16
					Mbcd(8) = Start \ 256
					Mbcd(9) = Start Mod 256
					Mbcd(10) = Length \ 256
					
					Mbcd(11) = Length Mod 256
					Mbcd(12) = UBound(Value) + 1
					For i = 0 To UBound(Value)
						'UPGRADE_WARNING: 未能解析对象 i 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
						'UPGRADE_WARNING: 未能解析对象 Value() 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
						Mbcd(13 + i) = Value(i)
					Next i
					
					
			End Select
			
			
			
		End If
		'UPGRADE_WARNING: 未能解析对象 GetWriteCommandByte 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
		GetWriteCommandByte = VB6.CopyArray(Mbcd)
		
		
	End Function
	'Function GetWriteCommandByte(ByVal MBAD As String, Length As Long, ByVal Value As Variant) As Variant
	'        Dim Mbcd() As Byte
	'Dim tMbcd() As Byte
	' Dim ByteLength As Long
	' Dim Start As Long
	' Dim SlaveAd As Long
	'    Dim Dt As DataBlock
	'              SlaveAd = Me.ad
	'
	' If Length = 1 Then '长度为1时，功能码分别可为5和6
	' Select Case Left(MBAD, 1)
	'     Case 0
	'
	' Start = Val(Right(MBAD, 5)) - 1
	'
	' ReDim Mbcd(7)
	' ReDim tMbcd(UBound(Mbcd) - 2) As Byte
	'    tMbcd(0) = SlaveAd
	'
	'    tMbcd(1) = 5
	'    tMbcd(2) = Start \ 256
	'    tMbcd(3) = Start Mod 256
	'        tMbcd(4) = Value(0)
	'
	'    tMbcd(5) = 0
	'   For i = 0 To UBound(tMbcd)
	'    Mbcd(i) = tMbcd(i)
	'  Next i
	'  Mbcd(i) = CRC16(tMbcd)(1)
	'  Mbcd(i + 1) = CRC16(tMbcd)(0)
	' Case 4
	'Start = Val(Right(MBAD, 5)) - 1
	'
	' ReDim Mbcd(7)
	' ReDim tMbcd(UBound(Mbcd) - 2) As Byte
	'    tMbcd(0) = SlaveAd
	'
	'    tMbcd(1) = 6
	'    tMbcd(2) = Start \ 256
	'    tMbcd(3) = Start Mod 256
	'        tMbcd(4) = Value(0)
	'
	'    tMbcd(5) = Value(1)
	''    tMbcd(6) = IIf(Length Mod 8 = 0, Length \ 8, Length \ 8 + 1)
	'
	'   For i = 0 To UBound(tMbcd)
	'    Mbcd(i) = tMbcd(i)
	'  Next i
	'  Mbcd(i) = CRC16(tMbcd)(1)
	'  Mbcd(i + 1) = CRC16(tMbcd)(0)
	'  End Select
	'
	' Else ' '-----------------------------------------长度不为1时，则功能码需为15和16
	' Select Case Left(MBAD, 1)
	'     Case 0
	'
	' Start = Val(Right(MBAD, 5)) - 1
	'
	' ReDim Mbcd(7 + UBound(Value) + 2)
	' ReDim tMbcd(UBound(Mbcd) - 2) As Byte
	'    tMbcd(0) = SlaveAd
	'
	'    tMbcd(1) = 15
	'    tMbcd(2) = Start \ 256
	'    tMbcd(3) = Start Mod 256
	'        tMbcd(4) = Length \ 256
	'
	'    tMbcd(5) = Length Mod 256
	'    tMbcd(6) = UBound(Value) + 1
	'    For i = 0 To UBound(Value)
	'    tMbcd(7 + i) = Value(i)
	'  Next i
	'   For i = 0 To UBound(tMbcd)
	'    Mbcd(i) = tMbcd(i)
	'  Next i
	'  Mbcd(i) = CRC16(tMbcd)(1)
	'  Mbcd(i + 1) = CRC16(tMbcd)(0)
	' Case 4
	'Start = Val(Right(MBAD, 5)) - 1
	'
	' ReDim Mbcd(7 + UBound(Value) + 2)
	' ReDim tMbcd(UBound(Mbcd) - 2) As Byte
	'    tMbcd(0) = SlaveAd
	'
	'    tMbcd(1) = 16
	'    tMbcd(2) = Start \ 256
	'    tMbcd(3) = Start Mod 256
	'        tMbcd(4) = Length \ 256
	'
	'    tMbcd(5) = Length Mod 256
	'    tMbcd(6) = UBound(Value) + 1
	'    For i = 0 To UBound(Value)
	'    tMbcd(7 + i) = Value(i)
	'  Next i
	'   For i = 0 To UBound(tMbcd)
	'    Mbcd(i) = tMbcd(i)
	'  Next i
	'  Mbcd(i) = CRC16(tMbcd)(1)
	'  Mbcd(i + 1) = CRC16(tMbcd)(0)
	'  End Select
	'
	'
	'
	' End If
	'  GetWriteCommandByte = Mbcd
	'    End Function
End Class