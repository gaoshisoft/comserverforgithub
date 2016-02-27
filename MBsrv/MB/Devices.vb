Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("Devices_NET.Devices")> Public Class Devices
	Implements System.Collections.IEnumerable
	'局部变量，保存集合
	Private mCol As Collection
	
    Public Function Add(ByVal deviceAddr As Integer, ByVal MBadressQuantity As Integer, ByVal Description As String, Optional ByVal sKey As String = "") As Device
        '创建新对象
        Dim objNewMember As Device
        objNewMember = New Device


        '设置传入方法的属性
        '    objNewMember.MbadByteQty = MbadByteQty
        objNewMember.MBadressQuantity = MBadressQuantity
        objNewMember.deviceAddr = deviceAddr
        objNewMember.devicedescription = Description
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
    Function CheckKey(ByVal key As String) As Boolean
        CheckKey = mCol.Contains(key) '查找的一定是key
    End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Device
		Get
			'引用集合中的一个元素时使用。
			'vntIndexKey 包含集合的索引或关键字，
			'这是为什么要声明为 Variant 的原因
			'语法：Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	
	
	Public ReadOnly Property Count() As Integer
		Get
			'检索集合中的元素数时使用。语法：Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
        GetEnumerator = mCol.GetEnumerator
	End Function
	
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		'删除集合中的元素时使用。
		'vntIndexKey 包含索引或关键字，这是为什么要声明为 Variant 的原因
		'语法：x.Remove(xyz)
		
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	

    Public Sub New()
        MyBase.New()
        mCol = New Collection
    End Sub
	

    Protected Overrides Sub Finalize()
        mCol = Nothing
        MyBase.Finalize()
    End Sub
    Function GetDevicefromAd(ByVal DeviceAD As Integer) As Device
        Dim i As Integer
        For i = 1 To Me.Count
            If Me(i).deviceAddr = DeviceAD Then
                GetDevicefromAd = Me(i)
                Exit Function
            End If
        Next i
        'GetDevicefromAd = mCol.Item(DeviceAD)'如果key 也是数字则不要用这种找对象方法，造成系统无法判断。
    End Function
End Class