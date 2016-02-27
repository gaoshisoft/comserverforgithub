Option Strict Off
Option Explicit On


Namespace OPCfunc
    Friend Class MyOPCserver
        Dim WithEvents OPC As New OPCSvr.OPCserver

        Sub Init(ByVal SvrName As String)

            OPC.Init(SvrName)
        End Sub

        Sub Uninit()
            OPC.Uninit()
        End Sub

        Function AddItm(ByVal ItmName As String) As Long
            AddItm = OPC.AddItem(ItmName)
        End Function

        Function ReMoveItm(ByVal ItmHandle As Integer) As Integer

            ReMoveItm = OPC.RemoveItem(ItmHandle)
        End Function

        Function UpdateItm(ByVal ItmHandle As Integer, ByVal ItmValue As Object, ByVal itmQuty As Short) As Integer
            UpdateItm = OPC.UpdateItem(ItmHandle, ItmValue, itmQuty)
        End Function

        Public Sub New()
            MyBase.New()
        End Sub

        Private Sub OPC_ClientWrite(ByVal ItemHandle As Integer, ByVal value As Object) Handles OPC.ClientWrite
            Dim citem As CItem

            For Each citem In GItemCol
                If citem.Itemhandle = ItemHandle Then
                    Try
                        citem.WriteDevice(value)
                    Catch EX As Exception
                    End Try

                    Exit For

                End If
            Next
        End Sub

        Function SetHashSize(ByVal size As Integer) As Integer
            SetHashSize = OPC.SetHashSize(size)
        End Function

        Protected Overrides Sub Finalize()
            OPC.Uninit()
            MyBase.Finalize()
        End Sub
    End Class
End Namespace