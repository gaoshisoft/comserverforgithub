Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("OnlineDtus_NET.OnlineDtus")> Public Class OnlineDtus
	Implements System.Collections.IEnumerable
	'局部变量，保存集合
	Private mCol As Collection
	
	Public Function Add(ByRef PhoneNumber As String, ByRef LoginTime As Date, Optional ByRef sKey As String = "") As onlineDTU
		'创建新对象
		On Error Resume Next
		
		Dim objNewMember As onlineDTU
		objNewMember = New onlineDTU
		
		
		'设置传入方法的属性
		objNewMember.PhoneNumber = PhoneNumber
		objNewMember.LoginTime = LoginTime
		If Len(sKey) = 0 Then
			mCol.Add(objNewMember)
		Else
			mCol.Add(objNewMember, sKey)
		End If
		
		
		'返回已创建的对象
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
		
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As onlineDTU
		Get
			'引用集合中的一个元素时使用。
			'vntIndexKey 包含集合的索引或关键字，
			'这是为什么要声明为 Variant 的原因
			'语法：Set foo = x.Item(xyz) or Set foo = x.Item(5)
			On Error Resume Next
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	
	
	Public ReadOnly Property Count() As Integer
		Get
			'检索集合中的元素数时使用。语法：Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'本属性允许用 For...Each 语法枚举该集合。
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		'GetEnumerator = mCol.GetEnumerator
	End Function
	
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		'删除集合中的元素时使用。
		'vntIndexKey 包含索引或关键字，这是为什么要声明为 Variant 的原因
		'语法：x.Remove(xyz)
		On Error Resume Next
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'创建类后创建集合
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'类终止后破坏集合
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class