VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "DSC"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Attribute VB_Ext_KEY = "SavedWithClassBuilder6" ,"Yes"
Attribute VB_Ext_KEY = "Top_Level" ,"Yes"
Attribute VB_Ext_KEY = "Member0" ,"OnlineDtus"
'��������ֵ�ľֲ�����
Public Enum dsctype
  ��� = 1
  ɣ�� = 2
'  ��ʿ�� = 3
 End Enum
 Public Type DtuDataStruct
    PhoneNumber As String                            'Modemģ���ID��
                       
    Databuff(0 To 1024 + 1) As Byte '�洢���յ�������
    Datalen As Integer                        '���յ������ݰ�����
    DataType As Byte                          '���յ������ݰ�����,0x01���û����ݰ�    0x02���Կ�������֡�Ļ�Ӧ
                                                 
End Type
Public Waittime As Long '�ﵽ�ȴ�ʱ����δˢ��ʱ������Ϊ������
Private mvarDSCtype As dsctype  '�ֲ�����
Private mvarOnlineDtus As onlinedtus





Public Property Get onlinedtus() As onlinedtus
    If mvarOnlineDtus Is Nothing Then
        Set mvarOnlineDtus = New onlinedtus
    End If


    Set onlinedtus = mvarOnlineDtus
End Property


Public Property Set onlinedtus(vData As onlinedtus)
    Set mvarOnlineDtus = vData
End Property



Private Sub Class_Terminate()
    Set mvarOnlineDtus = Nothing
    
End Sub




Public Function StopService() As Boolean
Dim Result1 As Long
Dim Result2 As Long
Dim mess(1 To 1024) As Byte

Select Case Me.dsctype

   Case 1
      
       Result1 = hddll.do_close_all_user(mess(1))
       
       
       
         
           Result2 = hddll.stop_gprs_server(mess(1))
         
      
      If Result1 = 0 And Result2 = 0 Then
         StopService = True
         Else
         StopService = False
       End If
         
   Case 2
       Result = SRdll.DSStopService()
       If Result = 1 Then
         StopService = True
         Else
         StopService = False
       End If

   End Select
End Function

Public Function StartService(ServerPort As Long) As Boolean
Dim Result As Long
Dim mess(1 To 1024) As Byte

Select Case Me.dsctype

   Case 1
Result = hddll.start_gprs_server(frmrefreshONlinedtu.hwnd, WM_USER + 103, ServerPort, mess(1))
    If Result = 0 Then
       frmrefreshONlinedtu.tmrReadheartbeat.Enabled = True
       frmrefreshONlinedtu.tmrRefreshonlinedtu.Enabled = True
       StartService = True
    Else
    StartService = False
    End If

   Case 2
       Result = SRdll.DSStartService(ServerPort)
    If Result = 1 Then
       StartService = True
              frmrefreshONlinedtu.tmrRefreshonlinedtu.Enabled = True

    Else
    StartService = False
    End If

   End Select
   
End Function
Public Function GetherData(command() As Byte, ByVal PhoneNumber As String, ByVal Timeout As Long, receiveData() As Byte, rvLength As Long, Usetime As Double) As Boolean
Dim i As Long
Dim L As Long
Dim Result As Long
Dim Rvstr As String
Dim sPhon As String
  Dim mess(0 To 1024) As Byte
L = UBound(command)
Select Case Me.dsctype
    Case ���

   
   For i = 0 To L
       mess(i + 1) = command(i)
   Next i
       Result = hddll.SenddataByPhon(PhoneNumber, L + 1, mess(1))
        
         If Result = 0 Then '���ͳɹ�
          frmrefreshONlinedtu.tmrReadheartbeat.Enabled = False
         End If
     Dim rvData As data_record
  Dim ByteLen As Integer

'      Dim Gprsresult As Long
      '--------------------------�ȴ����ݷ���
                Start = Timer
                Do
                Gprsresult = hddll.DSgetnextdataAndPhonnumber(rvData, sPhon, 0)
'                DoEvents
                Loop While Gprsresult <> 0 And Timer < Start + Timeout
       '---------------------------�Ƿ������ݷ���
       frmrefreshONlinedtu.tmrReadheartbeat.Enabled = True  '�ȴ���ջ�����Ҳ��Ϊ�˶���������timer
   
        If Gprsresult = 0 And Left(sPhon, 11) = Left(Trim(PhoneNumber), 11) Then
             Usetime = Timer - Start
               If rvData.m_data_len > 0 Then
'                     For k = 0 To rvData.m_data_len '��Ϊ���ķ��������±��1��ʼ������������Ų��һ��
'                         Rvstr = Rvstr & Chr(rvData.m_data_buf(k + 1))
'                     Next k
                     receiveData = rvData.m_data_buf
                     rvLength = rvData.m_data_len
               End If
               GetherData = True
        Else
              GetherData = False
        End If
                
      
        Case ɣ��
        
   For i = 0 To L
       mess(i) = command(i)
   Next i
       Result = SRdll.SenddataByPhon(PhoneNumber, L + 1, mess(0))
        
         If Result = 1 Then '���ͳɹ�
          frmrefreshONlinedtu.tmrReadheartbeat.Enabled = False
         End If
     Dim srRvdata As ModemDataStruct

'      Dim Gprsresult As Long
      '--------------------------�ȴ����ݷ���
                Start = Timer
                Do
                Gprsresult = SRdll.DSgetnextdataAndPhonnumber(srRvdata, sPhon, 0)
'                DoEvents
                Loop While Gprsresult <> 1 And Timer < Start + Timeout
       '---------------------------�Ƿ������ݷ���
       frmrefreshONlinedtu.tmrReadheartbeat.Enabled = True '�ȴ���ջ�����Ҳ��Ϊ�˶���������timer
   
        If Gprsresult = 1 And sPhon = PhoneNumber Then
             Usetime = Timer - Start
               If srRvdata.m_data_len > 0 Then
'                     For k = 0 To srRvdata.m_data_len
'                         Rvstr = Rvstr & Chr(srRvdata.m_data_buf(k + 1))
'                     Next k
                     receiveData = srRvdata.m_data_buf
                     rvLength = srRvdata.m_data_len
                     
               End If
               GetherData = True
        Else
        GetherData = False
        End If
        End Select
End Function

Public Function SendbyteData(ByVal PhoneNumber As String, ByVal length As Long, mess As Byte) As Boolean
Select Case Me.dsctype
    Case ���
     
   
        SendbyteData = hddll.SenddataByPhon(PhoneNumber, length, mess)
        Case ɣ��
         
        SendbyteData = SRdll.SenddataByPhon(PhoneNumber, length, mess)
End Select
End Function
Public Function RvbyteData(DtuData As DtuDataStruct) As Boolean
Dim i As Long
Dim sPhon As String
Dim hddata As data_record
Dim Srdata As ModemDataStruct
Dim timeoutseconds As Long
timeoutseconds = 0
Select Case Me.dsctype
   Case ���
   
RvbyteData = hddll.DSgetnextdataAndPhonnumber(hddata, sPhon, timeoutseconds)
    DtuData.Datalen = hddata.m_data_len
    DtuData.PhoneNumber = sPhon
    DtuData.DataType = hddata.m_data_type
    If DtuData.DataType = 9 Then
        For i = 0 To DtuData.Datalen
        DtuData.Databuff(i) = hddata.m_data_buf(i + 1)
        Next i
        RvbyteData = True
       Else
       RvbyteData = False '�����û����ݰ�����Ϊ���յ�����
    End If
       
       
        
    
   Case ɣ��
   
 RvbyteData = SRdll.DSgetnextdataAndPhonnumber(Srdata, sPhon, timeoutseconds)
  DtuData.Datalen = Srdata.m_data_len
    DtuData.PhoneNumber = sPhon
    DtuData.DataType = Srdata.m_data_type
    If DtuData.DataType = 1 Then
        For i = 0 To DtuData.Datalen
        DtuData.Databuff(i) = Srdata.m_data_buf(i)
        Next i
        RvbyteData = True
       Else
       RvbyteData = False '�����û����ݰ�����Ϊ���յ�����
       End If
 End Select

End Function

Public Function SendString(ByVal PhoneNumber As String, ByVal length As Long, Str As String) As Boolean
Dim i As Long
Dim mess(1 To 1024) As Byte
   For i = 1 To length
       mess(i) = Asc(Mid(Str, i, 1))
       Next i
    Select Case Me.dsctype
    Case ���
        hddll.SenddataByPhon PhoneNumber, length, mess(1)
    Case ɣ��
        SRdll.SenddataByPhon PhoneNumber, length, mess(1)
End Select
   
End Function
Function ReceiveString(ByVal PhoneNumber As String, Rvstr As String) As Boolean
Dim i As Long
Dim L As Long
Dim Result As Long
'Dim Rvstr As String
Dim sPhon As String
  Dim mess(0 To 1024) As Byte
L = Len(command)
Select Case Me.dsctype
    Case ���
     Dim rvData As data_record
  Dim ByteLen As Integer

      Dim Gprsresult As Long
      '--------------------------�ȴ����ݷ���
                'Start = Timer
                'Do
                Gprsresult = hddll.DSgetnextdataAndPhonnumber(rvData, sPhon, 0)
                'DoEvents
                'Loop While Gprsresult <> 0 And Timer < Start + Timeout
       '---------------------------�Ƿ������ݷ���
       'frmrefreshONlinedtu.tmrReadheartbeat.Enabled = True  '�ȴ���ջ�����Ҳ��Ϊ�˶���������timer
   
        If Gprsresult = 0 And Left(sPhon, 11) = Left(Trim(PhoneNumber), 11) Then
             Usetime = Timer - Start
               If rvData.m_data_len > 0 Then
                     For k = 0 To rvData.m_data_len '��Ϊ���ķ��������±��1��ʼ������������Ų��һ��
                         Rvstr = Rvstr & Chr(rvData.m_data_buf(k + 1))
                     Next k
                     Data = Rvstr
               End If
               ReceiveString = True
        Else
              ReceiveString = False
        End If
          
        Case ɣ��
         Dim srRvdata As ModemDataStruct

'      Dim Gprsresult As Long
      '--------------------------�ȴ����ݷ���
                'Start = Timer
                'Do
                Gprsresult = SRdll.DSgetnextdataAndPhonnumber(srRvdata, sPhon, 0)
                'DoEvents
                'Loop While Gprsresult <> 1 And Timer < Start + Timeout
       '---------------------------�Ƿ������ݷ���
       'frmrefreshONlinedtu.tmrReadheartbeat.Enabled = True '�ȴ���ջ�����Ҳ��Ϊ�˶���������timer
   
        If Gprsresult = 1 And sPhon = PhoneNumber Then
             Usetime = Timer - Start
               If srRvdata.m_data_len > 0 Then
                     For k = 0 To srRvdata.m_data_len '��Ϊ���ķ��������±��1��ʼ������������Ų��һ��
                         Rvstr = Rvstr & Chr(srRvdata.m_data_buf(k + 1))
                     Next k
                     Data = Rvstr
                     
               End If
              ReceiveString = True
        Else
        ReceiveString = False
        End If
End Select
End Function
Public Function AskForDataBycommand(ByVal command As String, ByVal PhoneNumber As String, ByVal Timeout As Long, Data As String, Usetime As Double) As Boolean
Dim i As Long
Dim L As Long
Dim Result As Long
Dim Rvstr As String
Dim sPhon As String
  Dim mess(0 To 1024) As Byte
L = Len(command)
Select Case Me.dsctype
    Case ���

   
   For i = 1 To L
       mess(i) = Asc(Mid(command, i, 1))
   Next i
       Result = hddll.SenddataByPhon(PhoneNumber, L, mess(1))
        
         If Result = 0 Then '���ͳɹ�
          frmrefreshONlinedtu.tmrReadheartbeat.Enabled = False
         End If
     Dim rvData As data_record
  Dim ByteLen As Integer

      Dim Gprsresult As Long
      '--------------------------�ȴ����ݷ���
                Start = Timer
                Do
                Gprsresult = hddll.DSgetnextdataAndPhonnumber(rvData, sPhon, 0)
                DoEvents
                Loop While Gprsresult <> 0 And Timer < Start + Timeout
       '---------------------------�Ƿ������ݷ���
       frmrefreshONlinedtu.tmrReadheartbeat.Enabled = True  '�ȴ���ջ�����Ҳ��Ϊ�˶���������timer
   
        If Gprsresult = 0 And Left(sPhon, 11) = Left(Trim(PhoneNumber), 11) Then
             Usetime = Timer - Start
               If rvData.m_data_len > 0 Then
                     For k = 0 To rvData.m_data_len '��Ϊ���ķ��������±��1��ʼ������������Ų��һ��
                         Rvstr = Rvstr & Chr(rvData.m_data_buf(k + 1))
                     Next k
                     Data = Rvstr
               End If
               AskForDataBycommand = True
        Else
              AskForDataBycommand = False
        End If
                
        
        
        
        
        
        
        
        
        Case ɣ��
        
   For i = 0 To L - 1
       mess(i) = Asc(Mid(command, i + 1, 1))
   Next i
       Result = SRdll.SenddataByPhon(PhoneNumber, L, mess(0))
        
         If Result = 1 Then '���ͳɹ�
          frmrefreshONlinedtu.tmrReadheartbeat.Enabled = False
         End If
     Dim srRvdata As ModemDataStruct

'      Dim Gprsresult As Long
      '--------------------------�ȴ����ݷ���
                Start = Timer
                Do
                Gprsresult = SRdll.DSgetnextdataAndPhonnumber(srRvdata, sPhon, 0)
                DoEvents
                Loop While Gprsresult <> 1 And Timer < Start + Timeout
       '---------------------------�Ƿ������ݷ���
       frmrefreshONlinedtu.tmrReadheartbeat.Enabled = True '�ȴ���ջ�����Ҳ��Ϊ�˶���������timer
   
        If Gprsresult = 1 And sPhon = PhoneNumber Then
             Usetime = Timer - Start
               If srRvdata.m_data_len > 0 Then
                     For k = 0 To srRvdata.m_data_len '��Ϊ���ķ��������±��1��ʼ������������Ų��һ��
                         Rvstr = Rvstr & Chr(srRvdata.m_data_buf(k + 1))
                     Next k
                     Data = Rvstr
                     
               End If
               AskForDataBycommand = True
        Else
        AskForDataBycommand = False
        End If
                    End Select
    

End Function

Public Property Let dsctype(ByVal vData As dsctype)
'������ָ��ֵʱʹ�ã�λ�ڸ�ֵ������ߡ�
'Syntax: X.DSCtype = 5
    mvarDSCtype = vData
End Property


Public Property Get dsctype() As dsctype
'��������ֵʱʹ�ã�λ�ڸ�ֵ�����ұߡ�
'Syntax: Debug.Print X.DSCtype
    dsctype = mvarDSCtype
End Property
Sub RefreshonlineDTUTable()
Select Case Me.dsctype
Case ���
 Dim tucount As Long
    Dim tmodeminfo As user_info
    Dim B As Date

    tucount = hddll.get_max_user_amount()

    For i = 0 To tucount - 1 '��Զ�Զ�ˢ
        temp = get_user_at(i, tmodeminfo)
            Dim p As String
    p = Left(Trim(StrConv(tmodeminfo.m_userid, vbUnicode)), 11) '����userid��Ϊ���ֻ�����ͬ

    If hddll.IfThisDtuonline(p, Me.Waittime) Then    '�������120����δˢ��ʱ�䣬����Ϊ������
                On Error Resume Next
                Dscs("���").onlinedtus.Add p, StrConv(tmodeminfo.m_logon_date, vbUnicode), p
               
    Else
                Dscs("���").onlinedtus.Remove p
                On Error GoTo 0
    End If
    Next i
Case ɣ��
'      Dim i As Long
'    Dim temp As Long
'    Dim tucount As Long
    Dim Dtuinfo As SRdll.ModemInfoStruct
'    Dim b As Date
'    Me.ListView1.ListItems.Clear
    tucount = SRdll.DSGetModemCount()
    If tucount < 1 Then
        Exit Sub
    End If
    B = #1/1/1970#
    For i = 0 To tucount - 1 '��Զ�Զ�ˢ
        temp = DSGetModemByPosition(i, Dtuinfo)
        
'                   Dim p As String
                   p = Left(Trim(StrConv(Dtuinfo.m_phoneno, vbUnicode)), 11)
          
                   If ((Now - B) * 3600 * 24 - Dtuinfo.m_refresh_time - 28800) < Me.Waittime Then  '�������70����δˢ��ʱ�䣬����Ϊ������
                          On Error Resume Next
                         Dscs("ɣ��").onlinedtus.Add p, DateAdd("s", CLng(Dtuinfo.m_conn_time), "1970-1-1 8:00:00"), p
                          Dscs("ɣ��").onlinedtus(p).DTUid = Dtuinfo.m_modemid
                    Else
                         Dscs("ɣ��").onlinedtus.Remove p
                         On Error GoTo 0
                   End If
    Next i
             
End Select
End Sub

