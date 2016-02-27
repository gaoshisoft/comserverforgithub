Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("Dataserverinterface_NET.Dataserverinterface")> Public Class Dataserverinterface
    Function GetValue(ByRef ItemName As String) As Single
        On Error Resume Next
        GetValue = GItemCol(ItemName).ItemValue

    End Function
    Function GetRangeDown(ByRef ItemName As String) As Single
        On Error Resume Next

        GetRangeDown = GItemCol(ItemName).ConvertedDown

    End Function
    Function GetRangeUP(ByRef ItemName As String) As Single
        On Error Resume Next

        GetRangeUP = GItemCol(ItemName).convertedUP

    End Function
    Sub MinimizedMe()
        MainProg.MdIfmain.WindowState = System.Windows.Forms.FormWindowState.Minimized
    End Sub
    Function GetIfonline(ByRef RtuName As String) As Boolean
        GetIfonline = RTUs(RtuName).ifonline
    End Function
End Class