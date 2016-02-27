Option Strict Off
Option Explicit On
Namespace GprsCom
    Friend Class DataBlocks
        Implements System.Collections.IEnumerable
        '�ֲ����������漯��
        Private mCol As Collection
        Public RtuBaseAD As Integer

        Public Function Add(ByVal blockName As String, ByVal ad As Byte, ByVal startAD As String,
                            ByVal Length As Integer, ByVal Enable As Boolean, Optional ByVal sKey As String = "") _
            As DataBlock
            '�����¶���
            Dim objNewMember As DataBlock
            objNewMember = New DataBlock


            '���ô��뷽��������
            objNewMember.BlockName = blockName
            objNewMember.Addr = ad
            '    objNewMember.FC = FC
            objNewMember.startAD = startAD
            objNewMember.Enable = Enable

            objNewMember.Length = Length

            If Len(sKey) = 0 Then
                mCol.Add(objNewMember)
            Else
                mCol.Add(objNewMember, sKey)
            End If


            '�����Ѵ����Ķ���
            Add = objNewMember
            'UPGRADE_NOTE: �ڶԶ��� objNewMember ������������ǰ�������Խ������١� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"��
            'objNewMember = Nothing
        End Function

        Public Sub Add(ByVal DataBlock As IDataBlock, Optional ByVal Skey As String = "")
            If Len(Skey) = 0 Then
                mCol.Add(DataBlock)
            Else
                mCol.Add(DataBlock, Skey)
            End If
        End Sub

        Sub CalcuDataBlockaddr()
            Dim Start As Integer
            Dim i As Integer
            Dim D As IDataBlock
            For i = 1 To mCol.Count
                If i = 1 Then
                    D = mCol(i)
                    D.SvrMBADStart = RtuBaseAD
                Else
                    D = mCol(i - 1)
                    Start = D.SvrMBADStart + D.SvrAddrLength
                    D = mCol(i)
                    D.SvrMBADStart = Start
                End If
            Next
        End Sub

        Function GetAddrSpace() As Integer
            Dim i As Integer
            If Me.Count > 0 Then

                For i = 1 To Me.Count
                    Dim D As IDataBlock
                    D = mCol.Item(i)
                    GetAddrSpace = GetAddrSpace + D.SvrAddrLength
                Next
            Else
                GetAddrSpace = 0
            End If
        End Function

        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As IDataBlock
            Get
                '���ü����е�һ��Ԫ��ʱʹ�á�
                'vntIndexKey �������ϵ�������ؼ��֣�
                '����ΪʲôҪ����Ϊ Variant ��ԭ��
                '�﷨��Set foo = x.Item(xyz) or Set foo = x.Item(5)
                On Error Resume Next
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
            Me.CalcuDataBlockaddr()
        End Sub


        'UPGRADE_NOTE: Class_Initialize �������� Class_Initialize_Renamed�� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"��
        Private Sub Class_Initialize_Renamed()
            Dim mvarDataBlock As Object
            '������󴴽�����
            mCol = New Collection
            '������ DataBlocks ��ʱ������ mDataBlock ����
            mvarDataBlock = New DataBlock
        End Sub

        Public Sub New()
            MyBase.New()
            Class_Initialize_Renamed()
        End Sub


        'UPGRADE_NOTE: Class_Terminate �������� Class_Terminate_Renamed�� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"��
        Private Sub Class_Terminate_Renamed()
            Dim mvarDataBlock As Object
            'UPGRADE_NOTE: �ڶԶ��� mvarDataBlock ������������ǰ�������Խ������١� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"��
            mvarDataBlock = Nothing
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