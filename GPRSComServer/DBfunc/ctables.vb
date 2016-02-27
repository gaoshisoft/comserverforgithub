Option Strict Off
Option Explicit On
Namespace DBfunc

    Friend Class Ctables
        Implements System.Collections.IEnumerable
        '局部变量，保存集合
        Private mCol As Collection

        Public Function Add(ByRef tablename As String, ByRef ItemCol As Collection, Optional ByRef sKey As String = "") _
            As Ctable
            '创建新对象
            Dim objNewMember As Ctable
            objNewMember = New Ctable


            '设置传入方法的属性
            objNewMember.Tablename = tablename
            'UPGRADE_WARNING: IsObject 有新行为。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"”
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


            '返回已创建的对象
            Add = objNewMember
            'UPGRADE_NOTE: 在对对象 objNewMember 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
            objNewMember = Nothing
        End Function

        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Ctable
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
        End Sub


        'UPGRADE_NOTE: Class_Initialize 已升级到 Class_Initialize_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
        Private Sub Class_Initialize_Renamed()
            '创建类后创建集合
            mCol = New Collection
        End Sub

        Public Sub New()
            MyBase.New()
            Class_Initialize_Renamed()
        End Sub


        'UPGRADE_NOTE: Class_Terminate 已升级到 Class_Terminate_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
        Private Sub Class_Terminate_Renamed()
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