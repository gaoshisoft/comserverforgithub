Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("Devices_NET.Devices")> Public Class Devices
	Implements System.Collections.IEnumerable
	'�ֲ����������漯��
	Private mCol As Collection
	
    Public Function Add(ByVal deviceAddr As Integer, ByVal MBadressQuantity As Integer, ByVal Description As String, Optional ByVal sKey As String = "") As Device
        '�����¶���
        Dim objNewMember As Device
        objNewMember = New Device


        '���ô��뷽��������
        '    objNewMember.MbadByteQty = MbadByteQty
        objNewMember.MBadressQuantity = MBadressQuantity
        objNewMember.deviceAddr = deviceAddr
        objNewMember.devicedescription = Description
        If Len(sKey) = 0 Then
            mCol.Add(objNewMember)
        Else
            mCol.Add(objNewMember, sKey)
        End If


        '�����Ѵ����Ķ���
        Add = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing


    End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Device
		Get
			'���ü����е�һ��Ԫ��ʱʹ�á�
			'vntIndexKey �������ϵ�������ؼ��֣�
			'����ΪʲôҪ����Ϊ Variant ��ԭ��
			'�﷨��Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	
	
	Public ReadOnly Property Count() As Integer
		Get
			'���������е�Ԫ����ʱʹ�á��﷨��Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'������������ For...Each �﷨ö�ٸü��ϡ�
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		'GetEnumerator = mCol.GetEnumerator
	End Function
	
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		'ɾ�������е�Ԫ��ʱʹ�á�
		'vntIndexKey ����������ؼ��֣�����ΪʲôҪ����Ϊ Variant ��ԭ��
		'�﷨��x.Remove(xyz)
		
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'������󴴽�����
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'����ֹ���ƻ�����
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
    Function GetDevicefromAd(ByVal DeviceAD As Integer) As Device
        Dim i As Integer
        For i = 1 To Me.Count
            If Me(i).deviceAddr = DeviceAD Then
                GetDevicefromAd = Me(i)
            End If
        Next i
    End Function
End Class