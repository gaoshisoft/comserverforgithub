Option Strict Off
Option Explicit On
Friend Class ItemCollection
    Implements System.Collections.IEnumerable
    '
    Private mCol As Collection

    Public Function Add(ByRef ItemName As String, ByRef chineseDis As String, ByRef HandleExp As String, ByRef Unitstr As String, ByRef ItemTimeStamp As Date, ByRef devad As Integer,
                        ByRef MBAD As String, ByRef datatype As String, ByRef NeedConvert As Boolean,
                        ByRef Airangedown As Single, ByRef Airangeup As Single, ByRef ConvertedDown As Single,
                        ByRef convertedUP As Single, ByRef IfSwapByte As Boolean, ByVal uplimit As Double, ByVal downlimit As Double, Optional ByRef sKey As String = "") _
        As CItem
        Dim ItemValue As Object

        Dim ObjNewMember As CItem

        Dim Itemhandle As Integer
        Dim v As Object
        'StringToByte(ItemName, mItemName)
        If mCol.Contains(ItemName) Then
            Return Nothing

        End If
        ObjNewMember = New CItem
        Select Case datatype
            Case "Boolean"
                v = CBool(0)
            Case "Integer"
                v = CShort(0)
            Case "Single", "SingleSwapWord"
                v = CSng(0)
        End Select
        Itemhandle = OpcSvr.AddItm(ItemName)
        '
        If Itemhandle < 1 Then
            Add = Nothing
            Exit Function
        End If

        '
        ObjNewMember.ItemName = Trim(ItemName)
        ObjNewMember.Itemhandle = Itemhandle
        If IsReference(ItemValue) Then
            ObjNewMember.ItemValue = v
        Else
            ObjNewMember.ItemValue = ItemValue
        End If
        ObjNewMember.ItemQuality = 192
        ObjNewMember.ItemConnected = False
        ObjNewMember.ItemTimeStamp = ItemTimeStamp
        ObjNewMember.devad = devad
        ObjNewMember.MBAD = MBAD
        ObjNewMember.NeedConvert = NeedConvert
        ObjNewMember.Airangedown = Airangedown
        ObjNewMember.Airangeup = Airangeup
        ObjNewMember.ConvertedDown = ConvertedDown
        ObjNewMember.convertedUP = convertedUP
        ObjNewMember.IfSwapByte = IfSwapByte
        ObjNewMember.ChineseDis = chineseDis
        ObjNewMember.HandleExpretion = HandleExp
        ObjNewMember.UnitStr = Unitstr
        ObjNewMember.DownLimit = downlimit
        ObjNewMember.Uplimit = uplimit
        Select Case datatype
            Case "boolean"
                ObjNewMember.ItemValue = CBool(0)
            Case "Integer"
                ObjNewMember.ItemValue = CSng(0)
            Case "Single", "SingleSwapWord"
                ObjNewMember.ItemValue = CSng(0)
        End Select
        ObjNewMember.ItemDataType = datatype
        If Len(sKey) = 0 Then
            mCol.Add(ObjNewMember)
        Else
            mCol.Add(ObjNewMember, sKey)
        End If




        Return ObjNewMember

    End Function

    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As CItem
        Get
            On Error Resume Next
            Item = mCol.Item(vntIndexKey)

        End Get
    End Property


    Public ReadOnly Property Count() As Integer
        Get
            Count = mCol.Count()
        End Get
    End Property


    

    Public Function GetEnumerator() As System.Collections.IEnumerator _
        Implements System.Collections.IEnumerable.GetEnumerator
        GetEnumerator = mCol.GetEnumerator
    End Function


    Public Sub Remove(ByRef vntIndexKey As Object)

        mCol.Remove(vntIndexKey)

        frmOpc.lvListView.Items.RemoveByKey(vntIndexKey)
    End Sub



    Public Sub New()
        MyBase.New()
        mCol = New Collection
    End Sub


   

    Protected Overrides Sub Finalize()
        mCol = Nothing
        MyBase.Finalize()
    End Sub

    Function Exist(ByRef vntIndexKey As Object) As Boolean
       
        Exist = Me.mCol.Contains(vntIndexKey)
       
    End Function
    Sub Clear()
        mCol.Clear()
    End Sub

    Sub ReadDevice()
        Dim citem As CItem
        For Each citem In Me



            OpcSvr.UpdateItm(citem.Itemhandle, citem.ItemValue, citem.ItemQuality) 'itemvalue 属性中包含了readdevice

        Next citem
    End Sub
End Class