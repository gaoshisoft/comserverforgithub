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

Dscs.Add dsctype, IIf(dsctype = 1, "���", "ɣ��") 'ÿ��DSCֻ�ܽ���һ��
Dscs(IIf(dsctype = 1, "���", "ɣ��")).Waittime = Waittime
If Not Err Then

Dscstate = Dscstate & IIf(dsctype = 1, "���DSC", "ɣ��DSC") & IIf(Dscs(IIf(dsctype = 1, "���", "ɣ��")).StartService(ServerPort), "�����ɹ�", "����ʧ��") & Str(ServerPort) & vbCrLf
End If
End Sub
Function DstroyDsc() As Boolean
Dim tdsc As DSC
  For Each tdsc In Dscs
    DstroyDsc = tdsc.StopService
    Next
     
End Function
Public Function IfThisDtuonline(ByVal Phone As String) As Boolean  '�ú�����ÿ���������ĵ�onlineDTUs�����м���Ƿ������DTU�Ӷ���֪���DTU�Ƿ�����
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
         Case ���
            IfThisDtuonline = hddll.IfThisDtuonline(d.PhoneNumber, tdsc.Waittime)
            Case ɣ��
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
    If d.PhoneNumber = PhoneNumber Then '���û�д���˵�����DTU�����Ҿ������DSC��
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