Option Strict Off
Option Explicit On
Public Class ConlineDtus
    Implements System.Collections.IEnumerable

    Public Event DataReturn(ByVal PhoneNumber As String, ByVal Value As Object, ByVal length As Integer)
    '�ֲ����������漯��
    Private mCol As Collection



    Sub informDataArrival(ByVal PhoneNumber As String, ByVal Value As Object, ByVal length As Integer)
        RaiseEvent DataReturn(PhoneNumber, Value, length)

    End Sub
    Public Function Add(ByVal SimCardNo As String, ByVal LoginTime As Date, ByVal sKey As String) As ConlineDTU
        '�����¶���
        Dim objNewMember As ConlineDTU
        objNewMember = New ConlineDTU

        objNewMember.HeartBeatTime = Now

        '���ô��뷽��������
        objNewMember.LoginTime = LoginTime
        objNewMember.Phonenumber = SimCardNo

        mCol.Add(objNewMember, sKey)



        '�����Ѵ����Ķ���
        Add = objNewMember
        objNewMember = Nothing


    End Function

    Function ContainsObj(ByVal key As String) As Boolean
        ContainsObj = mCol.Contains(key)
    End Function

    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As ConlineDTU
        Get
            '���ü����е�һ��Ԫ��ʱʹ�á�
            'vntIndexKey �������ϵ�������ؼ��֣�
            '����ΪʲôҪ����Ϊ Variant ��ԭ��
            '�﷨��Set foo = x.Item(xyz) or Set foo = x.Item(5)
            'On Error Resume Next
            Item = mCol.Item(vntIndexKey)
        End Get
    End Property



    Public ReadOnly Property Count() As Integer
        Get
            '���������е�Ԫ����ʱʹ�á��﷨��Debug.Print x.Count
            Count = mCol.Count()
        End Get
    End Property




    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
        GetEnumerator = mCol.GetEnumerator
    End Function


    Public Sub Remove(ByRef vntIndexKey As Object)
        'ɾ�������е�Ԫ��ʱʹ�á�
        'vntIndexKey ����������ؼ��֣�����ΪʲôҪ����Ϊ Variant ��ԭ��
        '�﷨��x.Remove(xyz)


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
End Class