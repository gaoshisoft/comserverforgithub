Option Strict Off
Option Explicit On
Namespace GprsCom
    Friend Class DataBlocks
        Implements System.Collections.IEnumerable
        '局部变量，保存集合
        Private mCol As Collection
        Public RtuBaseAD As Integer

        Public Function Add(ByVal blockName As String, ByVal ad As Byte, ByVal startAD As String,
                            ByVal Length As Integer, ByVal Enable As Boolean, Optional ByVal sKey As String = "") _
            As DataBlock
            '创建新对象
            Dim objNewMember As DataBlock
            objNewMember = New DataBlock


            '设置传入方法的属性
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


            '返回已创建的对象
            Add = objNewMember
            'UPGRADE_NOTE: 在对对象 objNewMember 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
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


        'UPGRADE_NOTE: NewEnum 属性已被注释掉。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"”
        'Public ReadOnly Property NewEnum() As stdole.IUnknown
        'Get
        '本属性允许用 For...Each 语法枚举该集合。
        'NewEnum = mCol._NewEnum
        'End Get
        'End Property

        Public Function GetEnumerator() As System.Collections.IEnumerator _
            Implements System.Collections.IEnumerable.GetEnumerator
            'UPGRADE_TODO: 取消注释并更改下列行以返回集合枚举数。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"”
            GetEnumerator = mCol.GetEnumerator
        End Function


        Public Sub Remove(ByRef vntIndexKey As Object)
            '删除集合中的元素时使用。
            'vntIndexKey 包含索引或关键字，这是为什么要声明为 Variant 的原因
            '语法：x.Remove(xyz)


            mCol.Remove(vntIndexKey)
            Me.CalcuDataBlockaddr()
        End Sub


        'UPGRADE_NOTE: Class_Initialize 已升级到 Class_Initialize_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
        Private Sub Class_Initialize_Renamed()
            Dim mvarDataBlock As Object
            '创建类后创建集合
            mCol = New Collection
            '当创建 DataBlocks 类时，创建 mDataBlock 对象
            mvarDataBlock = New DataBlock
        End Sub

        Public Sub New()
            MyBase.New()
            Class_Initialize_Renamed()
        End Sub


        'UPGRADE_NOTE: Class_Terminate 已升级到 Class_Terminate_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
        Private Sub Class_Terminate_Renamed()
            Dim mvarDataBlock As Object
            'UPGRADE_NOTE: 在对对象 mvarDataBlock 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
            mvarDataBlock = Nothing
            '类终止后破坏集合
            'UPGRADE_NOTE: 在对对象 mCol 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
            mCol = Nothing
        End Sub

        Protected Overrides Sub Finalize()
            Class_Terminate_Renamed()
            MyBase.Finalize()
        End Sub
    End Class
End Namespace