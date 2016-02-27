Option Strict Off
Option Explicit On
<Runtime.InteropServices.ProgId("DBlocks_NET.DBlocks")> Public Class DBlocks
    Implements System.Collections.IEnumerable
    '局部变量，保存集合
    Private mCol As Collection
    Sub AddBlockobj(ByRef Block As DBlock, Optional ByRef Skey As Object = Nothing)
        If Not IsNothing(Skey) Then
            Skey = GetRightKey(CStr(Skey))

            mCol.Add(Block, Skey)
            Block.Keystr = Skey
        Else
            mCol.Add(Block)
        End If
    End Sub
    Function ChangeName(ByRef Dt As DBlock, ByRef newName As String) As String
        If newName <> Dt.blockName Then
            Dt.blockName = GetRightName(newName)
            changeName = Dt.blockName
        Else
            changeName = newName
        End If
    End Function
    Private Function checkname(ByRef Name As String) As Boolean
        Dim i As Integer

        checkname = True
        For i = 1 To Me.Count
            If Me(i).blockName = Name Then
                checkname = False
            End If
        Next i

    End Function
    Private Function GetRightName(ByRef Name As String) As String

        Dim i As Integer
        Dim tn As String
        tn = Name
        Do While checkname(Name) = False
            i = i + 1

            Name = tn & "_" & i

        Loop
        GetRightName = Name
    End Function


    Private Function GetRightKey(ByRef key As String) As String

        Dim i As Integer
        Dim tn As String
        tn = key
        Do While checkKey(key) = False
            i = i + 1

            key = tn & "_" & i

        Loop
        GetRightKey = key
    End Function
    Private Function checkKey(ByRef key As String) As Boolean
        Dim i As Integer

        checkKey = True
        For i = 1 To Me.Count
            If Me(i).Keystr = key Then
                checkKey = False
            End If
        Next i

    End Function

    Public Function Add(ByRef blockName As String, ByRef ad As Integer, ByRef startad As String, ByRef Length As Integer, ByRef Enable As Boolean, Optional ByRef Skey As Object = Nothing) As DBlock
        '创建新对象
        Dim objNewMember As DBlock
        objNewMember = New DBlock


        '设置传入方法的属性
        objNewMember.blockName = blockName
        objNewMember.ad = ad
        '    objNewMember.FC = FC
        objNewMember.startad = startad
        objNewMember.Enable = Enable

        objNewMember.Length = Length

        If Len(Skey) = 0 Then
            mCol.Add(objNewMember)
        Else
            mCol.Add(objNewMember, Skey)
            'UPGRADE_WARNING: 未能解析对象 Skey 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
            objNewMember.Keystr = Skey
        End If


        '返回已创建的对象
        Add = objNewMember
        'UPGRADE_NOTE: 在对对象 objNewMember 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
        objNewMember = Nothing


    End Function

    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As DBlock
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




    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        'UPGRADE_TODO: 取消注释并更改下列行以返回集合枚举数。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"”
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
        Dim mvarDataBlock As Object

        mCol = New Collection

        mvarDataBlock = New DBlock
    End Sub

    Protected Overrides Sub Finalize()
        Dim mvarDataBlock As Object
        mvarDataBlock = Nothing
        mCol = Nothing
        MyBase.Finalize()
    End Sub
End Class