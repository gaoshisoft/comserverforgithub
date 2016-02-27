Option Strict Off
Option Explicit On
Namespace DBfunc

    Friend Class Ctables
        Implements System.Collections.IEnumerable
        '�ֲ����������漯��
        Private mCol As Collection

        Public Function Add(ByRef tablename As String, ByRef ItemCol As Collection, Optional ByRef sKey As String = "") _
            As Ctable
            '�����¶���
            Dim objNewMember As Ctable
            objNewMember = New Ctable


            '���ô��뷽��������
            objNewMember.Tablename = tablename
            'UPGRADE_WARNING: IsObject ������Ϊ�� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"��
            If IsReference(ItemCol) Then
                objNewMember.ItemCol = ItemCol
            Else
                objNewMember.ItemCol = ItemCol
            End If
            If Len(sKey) = 0 Then
                mCol.Add(objNewMember)
            Else
                mCol.Add(objNewMember, sKey)
            End If


            '�����Ѵ����Ķ���
            Add = objNewMember
            'UPGRADE_NOTE: �ڶԶ��� objNewMember ������������ǰ�������Խ������١� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"��
            objNewMember = Nothing
        End Function

        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Ctable
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


        'UPGRADE_NOTE: NewEnum �����ѱ�ע�͵��� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"��
        'Public ReadOnly Property NewEnum() As stdole.IUnknown
        'Get
        '������������ For...Each �﷨ö�ٸü��ϡ�
        'NewEnum = mCol._NewEnum
        'End Get
        'End Property

        Public Function GetEnumerator() As System.Collections.IEnumerator _
            Implements System.Collections.IEnumerable.GetEnumerator
            'UPGRADE_TODO: ȡ��ע�Ͳ������������Է��ؼ���ö������ �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"��
            GetEnumerator = mCol.GetEnumerator
        End Function


        Public Sub Remove(ByRef vntIndexKey As Object)
            'ɾ�������е�Ԫ��ʱʹ�á�
            'vntIndexKey ����������ؼ��֣�����ΪʲôҪ����Ϊ Variant ��ԭ��
            '�﷨��x.Remove(xyz)


            mCol.Remove(vntIndexKey)
        End Sub


        'UPGRADE_NOTE: Class_Initialize �������� Class_Initialize_Renamed�� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"��
        Private Sub Class_Initialize_Renamed()
            '������󴴽�����
            mCol = New Collection
        End Sub

        Public Sub New()
            MyBase.New()
            Class_Initialize_Renamed()
        End Sub


        'UPGRADE_NOTE: Class_Terminate �������� Class_Terminate_Renamed�� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"��
        Private Sub Class_Terminate_Renamed()
            '����ֹ���ƻ�����
            'UPGRADE_NOTE: �ڶԶ��� mCol ������������ǰ�������Խ������١� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"��
            mCol = Nothing
        End Sub

        Protected Overrides Sub Finalize()
            Class_Terminate_Renamed()
            MyBase.Finalize()
        End Sub
    End Class
End Namespace