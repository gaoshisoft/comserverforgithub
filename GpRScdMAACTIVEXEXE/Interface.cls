VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "SHInterface"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Attribute VB_Ext_KEY = "SavedWithClassBuilder6" ,"Yes"
Attribute VB_Ext_KEY = "Top_Level" ,"Yes"

Public Event DataReturn(ByVal PhoneNumber As String, ByVal Value As Variant, ByVal length As Long)
Public WithEvents Mydscs As Dscs
Attribute Mydscs.VB_VarHelpID = -1
Public Dscstate As String


Sub BuildDsc(dsctype As dsctype, ServerPort As Long, Waittime As Long)
On Error Resume Next

Dscs.Add dsctype, IIf(dsctype = 1, "宏电", "桑荣") '每种DSC只能建立一次
Dscs(IIf(dsctype = 1, "宏电", "桑荣")).Waittime = Waittime
If Not Err Then

Dscstate = Dscstate & IIf(dsctype = 1, "宏电DSC", "桑荣DSC") & IIf(Dscs(IIf(dsctype = 1, "宏电", "桑荣")).StartService(ServerPort), "启动成功", "启动失败") & Str(ServerPort) & vbCrLf
End If
End Sub
Function DstroyDsc() As Boolean
Dim tdsc As DSC
  For Each tdsc In Dscs
    DstroyDsc = tdsc.StopService
    Next
     
End Function
Public Function IfThisDtuonline(ByVal Phone As String) As Boolean  '该函数在每个数据中心的onlineDTUs集合中检查是否有这个DTU从而得知这个DTU是否在线
Dim t As Long
Dim sPhon As String
Dim i As Long
Dim Haveonline As Boolean
Dim d As onlineDTU

 Dim tdsc As DSC

Err.Clear
'IfThisDtuonline = False
   For Each tdsc In Dscs
   
   For Each d In tdsc.onlinedtus
     If Left(Trim(d.PhoneNumber), 11) = Phone Then
        Select Case tdsc.dsctype
         Case 宏电
            IfThisDtuonline = hddll.IfThisDtuonline(d.PhoneNumber, tdsc.Waittime)
            Case 桑荣
            IfThisDtuonline = SRdll.IfThisDtuonline(d.PhoneNumber, tdsc.Waittime)
        End Select
     End If
   Next
  Next
           
          
End Function
Public Function SenddataByPhon(ByVal PhoneNumber As String, ByVal length As Long, mess As Byte) As Boolean
Dim d As onlineDTU


 Dim tdsc As DSC

Err.Clear

   For Each tdsc In Dscs
    For Each d In tdsc.onlinedtus
    If d.PhoneNumber = PhoneNumber Then '如果没有错，说明这个DTU存在且就是这个DSC的
   SenddataByPhon = tdsc.SendbyteData(PhoneNumber, length, mess)
   Exit For
   End If
   Next
  Next


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

Function GetOnlineDtus() As onlinedtus
Dim onlinedtus As New onlinedtus
   Dim o As onlineDTU
      Dim tdsc As DSC
         For Each tdsc In Dscs
                 For Each o In tdsc.onlinedtus
             onlinedtus.Add o.PhoneNumber, o.LoginTime, o.PhoneNumber
                 Next
         Next
       Set GetOnlineDtus = onlinedtus
End Function
'Sub StopRead()
'frmrefreshONlinedtu.tmrReadheartbeat.Enabled = False
'End Sub
'Sub StartRead()
'frmrefreshONlinedtu.tmrReadheartbeat.Enabled = True
'
'End Sub

Private Sub Class_Initialize()
Set Mydscs = Dscs
End Sub

Private Sub Mydscs_DataReturn(PhoneNumber As String, Value As Variant, length As Long)
RaiseEvent DataReturn(PhoneNumber, Value, length)
End Sub
