Option Strict Off
Option Explicit On
Namespace DBfunc
    Friend Class Ctable
        Implements _Itable
        Private _mItemCol As Collection
        Public Tablename As String
        Dim _tableType As String
        Public Property StaName As String
        Property ItemCol() As Collection
            Get
                If _mItemCol Is Nothing Then
                    _mItemCol = New Collection
                End If
                ItemCol = _mItemCol
            End Get
            Set(ByVal Value As Collection)
                _mItemCol = Value
            End Set
        End Property


        Private Property Itable_ItemCol() As Collection Implements _Itable.ItemCol
            Get
                Itable_ItemCol = Me.ItemCol
            End Get
            Set(ByVal Value As Collection)
            End Set
        End Property


        Private Property Itable_tablename() As String Implements _Itable.tablename
            Get
                Itable_tablename = Me.Tablename
            End Get
            Set(ByVal Value As String)
            End Set
        End Property


        Private Property Itable_TableType() As DBconnect.Tbtype Implements _Itable.TableType
            Get
                Itable_TableType = _tableType
            End Get
            Set(ByVal Value As DBconnect.Tbtype)
                _tableType = Value
            End Set
        End Property

        Function GetFieldsString() As String
            Dim k As Object
            Dim fn As String
            fn = ""
            For k = 1 To ItemCol.Count()
                fn = fn & ItemCol.Item(k).ItemName & ","
            Next k
            If fn <> "" Then '保证有字段的才存库
                fn = Left(fn, Len(fn) - 1)
            End If
            GetFieldsString = fn
        End Function

        Function AddField(ByRef Item As CItem) As Boolean
            AddField = True
            Dim o As Object
            On Error GoTo err_Renamed
            For Each o In ItemCol
                If o Is Item Then
                    AddField = False
                    Exit Function
                End If
            Next o
            If AddField = True Then

                ItemCol.Add(Item, Item.ItemName)
            End If

err_Renamed:
        End Function

        Sub Removefield(ByRef i As Object)
            On Error Resume Next
            ItemCol.Remove(i)
        End Sub

        Sub Clear()

            ItemCol.Clear()
        End Sub
    End Class
End Namespace