VERSION 5.00
Begin VB.Form frmrefreshONlinedtu 
   Caption         =   "GPRS/CDMA"
   ClientHeight    =   810
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   3255
   LinkTopic       =   "Form1"
   ScaleHeight     =   810
   ScaleWidth      =   3255
   StartUpPosition =   3  '´°¿ÚÈ±Ê¡
   Begin VB.Timer tmrReadheartbeat 
      Interval        =   100
      Left            =   1935
      Top             =   120
   End
   Begin VB.Timer tmrRefreshonlinedtu 
      Enabled         =   0   'False
      Interval        =   1000
      Left            =   450
      Top             =   120
   End
End
Attribute VB_Name = "frmrefreshONlinedtu"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False

Private Sub Form_Activate()
Me.Visible = False
End Sub

Private Sub tmrReadheartbeat_Timer()
 Dim rvData As data_record
 Dim rv As ModemDataStruct
Dim sPhon As String
      Dim Gprsresult As Long
                Gprsresult = hddll.DSgetnextdataAndPhonnumber(rvData, sPhon, 0)
                If rvData.m_data_type = Val(&H9) Then
                Dim hdrv() As Byte
                ReDim hdrv(UBound(rvData.m_data_buf) - 1)
                For i = 0 To UBound(hdrv)
                   hdrv(i) = rvData.m_data_buf(i + 1)
                   Next i
                Dscs.InformEvent sPhon, hdrv, CLng(rvData.m_data_len)
                
                End If
                Gprsresult = SRdll.DSgetnextdataAndPhonnumber(rv, sPhon, 0)
                If rv.m_data_type = Val(&H1) Then
                Dscs.InformEvent sPhon, rv.m_data_buf, CLng(rv.m_data_len)
                
                End If

End Sub

Private Sub tmrRefreshonlinedtu_Timer()
   
For i = 1 To Dscs.count
   Dscs(i).RefreshonlineDTUTable
   Next i
End Sub
