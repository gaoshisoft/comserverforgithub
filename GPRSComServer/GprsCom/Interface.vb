Option Strict Off
Option Explicit On
'UPGRADE_WARNING: Class instancing was changed to public. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="ED41034B-3890-49FC-8076-BD6FC2F42A85"'
'UPGRADE_NOTE: Interface was upgraded to Interface_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
<System.Runtime.InteropServices.ProgId("Interface_Renamed_NET.Interface_Renamed")> Public Class SHDscInterface
    Private WithEvents Mydscs As Dscs
    Public Dscstate As String
    Public Event DataReturn(ByRef PhoneNumber As String, ByRef Value As Object, ByRef length As Integer)
    'Function GetDscs() As Dscs
    'Set GetDscs = Dscs
    'End Function

    'UPGRADE_NOTE: Dscs was upgraded to Dscs_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Sub BuildDsc(ByRef dsctype As DSC.dsctype, ByRef ServerPort As Integer, ByRef Waittime As Integer)
        On Error Resume Next

        'UPGRADE_WARNING: Couldn't resolve default property of object Dscs.Add. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        Dim Dscs As New Dscs
        Dscs.Add(dsctype, IIf(dsctype = 1, "宏电", "桑荣")) '每种DSC只能建立一次
        'UPGRADE_WARNING: Couldn't resolve default property of object Dscs().Waittime. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        Dscs(IIf(dsctype = 1, "宏电", "桑荣")).Waittime = Waittime
        If Not Err.Number Then

            'UPGRADE_WARNING: Couldn't resolve default property of object Dscs().StartService. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            Dscstate = Dscstate & IIf(dsctype = 1, "宏电DSC", "桑荣DSC") & IIf(Dscs(IIf(dsctype = 1, "宏电", "桑荣")).StartService(ServerPort), "启动成功", "启动失败") & Str(ServerPort) & vbCrLf
        End If
    End Sub
    Function DstroyDsc() As Boolean
        Dim tdsc As DSC
        For Each tdsc In SHDscs
            DstroyDsc = tdsc.StopService
        Next tdsc

    End Function
    Public Function IfThisDtuonline(ByVal Phone As String) As Boolean '该函数在每个数据中心的onlineDTUs集合中检查是否有这个DTU从而得知这个DTU是否在线
        Dim t As Integer
        Dim sPhon As String
        Dim i As Integer
        Dim Haveonline As Boolean
        Dim d As onlineDTU

        Dim tdsc As DSC

        Err.Clear()
        'IfThisDtuonline = False
        For Each tdsc In SHDscs

            For Each d In tdsc.onlinedtus
                If Left(Trim(d.PhoneNumber), 11) = Phone Then
                    Select Case tdsc.SHDsctype
                        Case DSC.dsctype.宏电
                            IfThisDtuonline = hddll.IfThisDtuonline((d.PhoneNumber), (tdsc.Waittime))
                        Case DSC.dsctype.桑荣
                            IfThisDtuonline = SRdll.IfThisDtuonline(d.PhoneNumber, (tdsc.Waittime))
                    End Select
                End If
            Next d
        Next tdsc


    End Function
    Public Function SenddataByPhon(ByVal PhoneNumber As String, ByVal length As Integer, ByRef mess As Byte) As Boolean
        Dim d As onlineDTU


        Dim tdsc As DSC

        Err.Clear()

        For Each tdsc In SHDscs
            For Each d In tdsc.onlinedtus
                If d.PhoneNumber = PhoneNumber Then '如果没有错，说明这个DTU存在且就是这个DSC的
                    SenddataByPhon = tdsc.SendbyteData(PhoneNumber, length, mess)
                    Exit For
                End If
            Next d
        Next tdsc


    End Function
    'Function GetNextData(Data As DtuDataStruct) As Boolean
    '
    '
    ' Dim tdsc As DSC
    '
    '
    '   For Each tdsc In Dscs
    '   GetNextData = tdsc.RvbyteData(Data)
    '   If GetNextData = True Then
    '   Exit For
    '   End If
    '
    '  Next
    '
    '
    '
    'End Function

    Function GetOnlineDtus() As OnlineDtus
        Dim onlinedtus As New OnlineDtus
        Dim o As onlineDTU
        Dim tdsc As DSC
        For Each tdsc In SHDscs
            For Each o In tdsc.onlinedtus
                onlinedtus.Add((o.PhoneNumber), (o.LoginTime), o.PhoneNumber)
            Next o
        Next tdsc
        GetOnlineDtus = onlinedtus
    End Function
    'Sub StopRead()
    'frmrefreshONlinedtu.tmrReadheartbeat.Enabled = False
    'End Sub
    'Sub StartRead()
    'frmrefreshONlinedtu.tmrReadheartbeat.Enabled = True
    '
    'End Sub

    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        Mydscs = SHDscs
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    Private Sub Mydscs_DataReturn(ByRef PhoneNumber As String, ByRef Value As Object, ByRef length As Integer) Handles Mydscs.DataReturn
        RaiseEvent DataReturn(PhoneNumber, Value, length)
    End Sub
End Class