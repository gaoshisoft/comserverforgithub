Option Strict Off
Option Explicit On
Friend Class MyAlarm
	Dim CurrentSystime As Date
	Dim StartSystime As Date
	Public WithEvents tmr As Mytimer
	Public WithEvents tmr2 As Mytimer
	Dim AlmHour As Integer
	'��������ֵ�ľֲ�����
	Private mvarStopDate As Date '�ֲ�����
	
	
	Public Property StopDate() As Date
		Get
			'��������ֵʱʹ�ã�λ�ڸ�ֵ�����ұߡ�
			'Syntax: Debug.Print X.StopDate
			StopDate = mvarStopDate
		End Get
		Set(ByVal Value As Date)
			'������ָ��ֵʱʹ�ã�λ�ڸ�ֵ������ߡ�
			'Syntax: X.StopDate = 5
			mvarStopDate = Value
		End Set
	End Property
	
	
	
	
	
	Private Function shieldpc(ByRef outdata As Object) As Object
		Dim key3, key1, key2, key4 As Object
		Dim step10, step8, step6, step4, step2, count1, dataX2, y22, y1, y2, y11, dataX1, step1, step3, step5, step7, step9, step11 As Object
		
		'replace the follow key with key.txt
		'UPGRADE_WARNING: δ�ܽ������� key1 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		key1 = 54560
		'UPGRADE_WARNING: δ�ܽ������� key2 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		key2 = 61995
		'UPGRADE_WARNING: δ�ܽ������� key3 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		key3 = 57095
		'UPGRADE_WARNING: δ�ܽ������� key4 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		key4 = 6260
		
		
		'UPGRADE_WARNING: δ�ܽ������� dataX2 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		dataX2 = outdata And &HFFFF0000
		For count1 = 0 To 3
			'UPGRADE_WARNING: δ�ܽ������� dataX2 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
			dataX2 = dataX2 / 16
		Next count1
		'UPGRADE_WARNING: δ�ܽ������� dataX2 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		dataX2 = &H1FFFF And dataX2
		
		'UPGRADE_WARNING: δ�ܽ������� dataX2 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		If dataX2 > 65535 Then
			'UPGRADE_WARNING: δ�ܽ������� dataX2 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
			dataX2 = dataX2 - 65536
		End If
		
		'UPGRADE_WARNING: δ�ܽ������� dataX1 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		dataX1 = &H1FFFF And outdata
		
		'UPGRADE_WARNING: δ�ܽ������� dataX1 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		If dataX1 > 65535 Then
			'UPGRADE_WARNING: δ�ܽ������� dataX1 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
			dataX1 = dataX1 - 65536
		End If
		
		'UPGRADE_WARNING: δ�ܽ������� step1 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		step1 = dataX1 Xor key2
		
		'UPGRADE_WARNING: δ�ܽ������� step2 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		step2 = dataX2 Xor key1
		
		'UPGRADE_WARNING: δ�ܽ������� step2 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		'UPGRADE_WARNING: δ�ܽ������� step1 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		'UPGRADE_WARNING: δ�ܽ������� step3 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		step3 = step1 + step2
		
		'UPGRADE_WARNING: δ�ܽ������� step3 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		If step3 > 65535 Then
			'UPGRADE_WARNING: δ�ܽ������� step3 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
			step3 = step3 - 65536
		End If
		
		'UPGRADE_WARNING: δ�ܽ������� step3 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		'UPGRADE_WARNING: δ�ܽ������� step4 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		step4 = step3 * 16 '<< 4
		'UPGRADE_WARNING: δ�ܽ������� step4 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		While step4 > 65535
			'UPGRADE_WARNING: δ�ܽ������� step4 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
			step4 = step4 - 65536
		End While
		
		'UPGRADE_WARNING: δ�ܽ������� key4 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		'UPGRADE_WARNING: δ�ܽ������� step4 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		'UPGRADE_WARNING: Mod ������Ϊ�� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"��
		'UPGRADE_WARNING: δ�ܽ������� step5 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		step5 = step4 Mod key4
		
		
		'UPGRADE_WARNING: δ�ܽ������� key3 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		'UPGRADE_WARNING: δ�ܽ������� step5 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		'UPGRADE_WARNING: δ�ܽ������� step6 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		step6 = step5 * key3
		
		'UPGRADE_WARNING: δ�ܽ������� step6 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		While step6 > 2147483647
			'UPGRADE_WARNING: δ�ܽ������� step6 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
			step6 = step6 - 2147483647 - 1
		End While
		'UPGRADE_WARNING: δ�ܽ������� step6 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		While step6 < -2147483647
			'UPGRADE_WARNING: δ�ܽ������� step6 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
			step6 = step6 + 2147483647 + 1
		End While
		'UPGRADE_WARNING: δ�ܽ������� key1 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		'UPGRADE_WARNING: δ�ܽ������� dataX1 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		'UPGRADE_WARNING: δ�ܽ������� step7 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		step7 = dataX1 + key1
		'UPGRADE_WARNING: δ�ܽ������� step7 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		If step7 > 65535 Then
			'UPGRADE_WARNING: δ�ܽ������� step7 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
			step7 = step7 - 65536
		End If
		'UPGRADE_WARNING: δ�ܽ������� key3 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		'UPGRADE_WARNING: δ�ܽ������� step7 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		'UPGRADE_WARNING: Mod ������Ϊ�� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"��
		'UPGRADE_WARNING: δ�ܽ������� step8 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		step8 = step7 Mod key3
		
		'UPGRADE_WARNING: δ�ܽ������� step9 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		step9 = key4 Xor dataX2
		
		'UPGRADE_WARNING: δ�ܽ������� step9 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		'UPGRADE_WARNING: δ�ܽ������� step8 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		'UPGRADE_WARNING: δ�ܽ������� step10 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		step10 = step8 * step9
		'UPGRADE_WARNING: δ�ܽ������� step10 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		While step10 > 2147483647
			'UPGRADE_WARNING: δ�ܽ������� step10 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
			step10 = step10 - 2147483647 - 1
		End While
		
		'UPGRADE_WARNING: δ�ܽ������� step10 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		While step10 < -2147483647
			'UPGRADE_WARNING: δ�ܽ������� step10 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
			step10 = step10 + 2147483647 + 1
		End While
		
		
		'UPGRADE_WARNING: δ�ܽ������� step11 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		step11 = step10 Xor step6
		
		'UPGRADE_WARNING: δ�ܽ������� step11 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		'UPGRADE_WARNING: δ�ܽ������� shieldpc ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		shieldpc = step11
		
	End Function
	Function GetMyalm() As Boolean '��������
		Dim y1, retVal, y2 As Object

		Dim outdata2, outdata, y7 As Object
		Dim Almbyte As Object
		Dim s As String
		s = "28 74 122 67 189 141 165 141 83 147 189 231 71 100 99 102 103 151 217 73 181 86 179 68 74 125 168 119 134 97 157 114 179 71 0"
		
		'UPGRADE_WARNING: δ�ܽ������� Almbyte ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		Almbyte = Split(s, " ")
		
		
		'Creat Random to retVal From 1 to 2094967295
		
		
		Randomize()
		'UPGRADE_WARNING: δ�ܽ������� retVal ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		retVal = Int((2094967295 * Rnd()) + 1)
		
		'UPGRADE_WARNING: δ�ܽ������� retVal ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		'UPGRADE_WARNING: δ�ܽ������� outdata ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		outdata = retVal
		
		
		'Label8.Caption = "X =  " & Hex(outdata)
		'Call Function ShieldPc of PC
		'UPGRADE_WARNING: δ�ܽ������� shieldpc() ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		'UPGRADE_WARNING: δ�ܽ������� y1 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		y1 = Hex(shieldpc(outdata))
		'Text3.Text = "y1 =   " & y1
		'Call Function Lock32_Function of Lock in cdll.dll
		'UPGRADE_WARNING: δ�ܽ������� outdata ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		'UPGRADE_WARNING: δ�ܽ������� y2 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		y2 = Hex(Lock32_Function(outdata) And &H7FFFFFFF)
		'Text1.Text = "y2 =   " & y2
		
		'UPGRADE_WARNING: δ�ܽ������� y2 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		'UPGRADE_WARNING: δ�ܽ������� y1 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		If y1 = y2 Then
			GetMyalm = True
			
		Else
			GetMyalm = False
			
		End If
		If GetMyalm = False Then
			'MsgBox "����������,ϵͳ����ȫ�������ð�"
			
			'Me.StartTiming (3)
			Me.SetCheckDate(CDate("2009-6-1"))
			
		End If
		
		
	End Function
    'Function GetMyalmReg() As Boolean
    '    Dim y1, retVal, y2 As Object
    '    Dim intx As Short
    '    Dim outdata2, outdata, y7 As Object
    '    Dim Almbyte As Object
    '    Dim s As String
    '    s = "28 74 122 67 189 141 165 141 83 147 189 231 71 100 99 102 103 151 217 73 181 86 179 68 74 125 168 119 134 97 157 114 179 71 0"

    '    'UPGRADE_WARNING: δ�ܽ������� Almbyte ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
    '    Almbyte = Split(s, " ")


    '    'Creat Random to retVal From 1 to 2094967295


    '    Randomize()
    '    'UPGRADE_WARNING: δ�ܽ������� retVal ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
    '    retVal = Int((2094967295 * Rnd()) + 1)

    '    'UPGRADE_WARNING: δ�ܽ������� retVal ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
    '    'UPGRADE_WARNING: δ�ܽ������� outdata ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
    '    outdata = retVal

    '    Dim au As New Authorize.ShouQuan
    '    ''Label8.Caption = "X =  " & Hex(outdata)
    '    ''Call Function ShieldPc of PC
    '    'y1 = Hex(shieldpc(outdata))
    '    ''Text3.Text = "y1 =   " & y1
    '    ''Call Function Lock32_Function of Lock in cdll.dll
    '    'y2 = Hex(Lock32_Function(outdata) And &H7FFFFFFF)
    '    'Text1.Text = "y2 =   " & y2

    '    If au.IfRegistered Then
    '        GetMyalmReg = True

    '    Else
    '        GetMyalmReg = False

    '    End If
    '    If GetMyalmReg = False Then
    '        'MsgBox "ע�������,ϵͳ����5Сʱȫ�������ð�"

    '        'Me.StartTiming (5)
    '        Me.SetCheckDate(CDate("2009-4-1"))

    '    End If


    'End Function
	Sub StartTiming(ByRef h As Integer)
		StartSystime = Now
		Me.tmr = FGetherRtuData.Mytmr
		AlmHour = h
	End Sub
	Sub SetCheckDate(ByRef D As Date)
		Me.tmr2 = FGetherRtuData.Mytmr
		Me.StopDate = D
	End Sub
	
    Private Sub tmr_Timer_Renamed() Handles tmr.TmrTick
        CurrentSystime = Now
        Dim s As String
        Dim Alm As Object
        s = "71 100 99 102 92 125 28 74 122 67 189 141 189"
        'UPGRADE_WARNING: δ�ܽ������� Alm ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
        Alm = Split(s, " ")
        Dim h As Integer
        'UPGRADE_WARNING: DateDiff ��Ϊ���ܲ�ͬ�� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6B38EC3F-686D-4B2E-B5A5-9E8E7A762E32"��
        h = DateDiff(Microsoft.VisualBasic.DateInterval.Hour, StartSystime, CurrentSystime)
        If h >= AlmHour Then
            On Error Resume Next
            MsgBox(OutMyAlm(Alm, 188, 24), MsgBoxStyle.OKOnly)
            usr.Level = 3
            Quit()

        End If

    End Sub
    '����:
    '������: encode
    '������ string as_code
    '���أ�string
    Function encode(ByRef as_code As String) As String
        Dim li_code2 As Object
        Dim gs_key_para As Object
        Dim li_code1 As Object
        Dim ll_i As Object
        Dim ll_j As Object
        Dim ll_len As Object
        Dim ls_code2, ls_rtncode, ls_code1, ls_temp As String
        'UPGRADE_WARNING: δ�ܽ������� ll_len ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
        ll_len = Len(as_code)
        'UPGRADE_WARNING: δ�ܽ������� ll_len ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
        If ll_len <= 0 Then
            encode = ""
        End If
        ls_rtncode = ""
        'UPGRADE_WARNING: δ�ܽ������� ll_j ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
        ll_j = 1
        'UPGRADE_WARNING: δ�ܽ������� ll_len ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
        For ll_i = 1 To ll_len
            'UPGRADE_WARNING: δ�ܽ������� ll_i ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
            'UPGRADE_WARNING: δ�ܽ������� li_code1 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
            li_code1 = Asc(Mid(as_code, ll_i, 1))
            'UPGRADE_WARNING: δ�ܽ������� ll_j ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
            'UPGRADE_WARNING: δ�ܽ������� gs_key_para ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
            'UPGRADE_WARNING: δ�ܽ������� li_code2 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
            li_code2 = Asc(Mid(gs_key_para, ll_j, 1))
            'UPGRADE_WARNING: δ�ܽ������� li_code2 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
            'UPGRADE_WARNING: δ�ܽ������� li_code1 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
            li_code1 = li_code1 + li_code2
            'UPGRADE_WARNING: δ�ܽ������� li_code1 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
            Do While li_code1 > 127
                'UPGRADE_WARNING: δ�ܽ������� li_code1 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
                If li_code1 > 127 Then
                    'UPGRADE_WARNING: δ�ܽ������� li_code1 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
                    li_code1 = li_code1 - 127
                End If
            Loop
            'UPGRADE_WARNING: δ�ܽ������� li_code1 ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
            ls_temp = Chr(li_code1)
            ls_rtncode = ls_rtncode & ls_temp
            'UPGRADE_WARNING: δ�ܽ������� ll_j ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
            ll_j = ll_j + 1
            'UPGRADE_WARNING: δ�ܽ������� ll_j ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
            If ll_j > Len(gs_key_para) Then ll_j = 1
        Next
        encode = ls_rtncode
    End Function



    '����:
    '������: decode
    '������ string as_code
    '���أ�string
    Function decode(ByRef as_code As String) As String
        Dim gs_key_para As Object
        Dim ls_code2, ls_rtncode, ls_code1, ls_temp As String
        Dim ll_i, ll_len, ll_j As Integer
        Dim li_code1, li_code2 As Short
        ll_len = Len(as_code)
        If ll_len <= 0 Then
            decode = ""
        End If
        ls_rtncode = ""
        ll_j = 1
        For ll_i = 1 To ll_len
            li_code1 = Asc(Mid(as_code, ll_i, 1))
            'UPGRADE_WARNING: δ�ܽ������� gs_key_para ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
            li_code2 = Asc(Mid(gs_key_para, ll_j, 1))
            li_code1 = li_code1 - li_code2
            Do While li_code1 < 0
                If li_code1 < 0 Then
                    li_code1 = li_code1 + 127
                End If
            Loop
            ls_temp = Chr(li_code1)
            ls_rtncode = ls_rtncode & ls_temp
            ll_j = ll_j + 1
            If ll_j > Len(gs_key_para) Then ll_j = 1
        Next
        decode = ls_rtncode

    End Function






    Private Function SetMyalm(ByVal strSource As String, ByVal key1 As Byte, ByVal key2 As Short) As Object
        Dim bLowData As Byte
        Dim bHigData As Byte
        Dim i As Short
        Dim strEncrypt As String
        Dim strChar As String
        Dim OutByte() As Byte
        ReDim OutByte(Len(strSource) * 2)

        For i = 1 To Len(strSource)

            '���Ӵ��ӣ��⣩���ַ�����ȡ��һ���ַ�

            strChar = Mid(strSource, i, 1)

            '  ����ȡ�ַ��ĵ��ֽں�Key1�����������

            'UPGRADE_ISSUE: ��֧�� Mid ������ �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"��
            'UPGRADE_ISSUE: ��֧�� ASC ������ �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"��
            bLowData = Asc(Mid(strChar, 1, 1)) Xor key1

            '  ����ȡ�ַ��ĸ��ֽں�K2�����������

            'UPGRADE_ISSUE: ��֧�� Mid ������ �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"��
            'UPGRADE_ISSUE: ��֧�� ASC ������ �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"��
            bHigData = Asc(Mid(strChar, 2, 1)) Xor key2

            '  ���������������ݺϳ��µ��ַ�

            OutByte((i - 1) * 2) = bLowData
            OutByte((i - 1) * 2 + 1) = bHigData

        Next
        'UPGRADE_WARNING: δ�ܽ������� SetMyalm ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
        SetMyalm = VB6.CopyArray(OutByte)
    End Function
    Function OutMyAlm(ByRef v As Object, ByRef key1 As Byte, ByRef key2 As Byte) As String
        Dim bLowData As Byte
        Dim bHigData As Byte
        Dim i As Short
        Dim j As Integer
        Dim strEncrypt As String
        Dim strChar As String
        Dim strByte() As Byte
        i = UBound(v)
        For j = 0 To i / 2 - 1
            'UPGRADE_ISSUE: ��֧�� Chr ������ �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"��
            strChar = strChar & Chr(v(j * 2) Xor key1) & Chr(v(j * 2 + 1) Xor key2)
        Next j
        OutMyAlm = strChar


    End Function


    Private Sub tmr2_Timer_Renamed() Handles tmr2.TmrTick
        '�趨�����˳���
        '	If Today > Me.StopDate Then
        '		If Not Myalm.GetMyalmReg Then
        '			MsgBox("ϵͳ����")
        '			usr.Level = 3
        '			Quit()
        '		End If
        '	End If
    End Sub
End Class