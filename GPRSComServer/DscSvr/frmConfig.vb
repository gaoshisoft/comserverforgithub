Option Strict Off
Option Explicit On
Friend Class frmrefreshONlinedtu
	Inherits System.Windows.Forms.Form
	
	'UPGRADE_WARNING: Form event frmrefreshONlinedtu.Activate has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
	Private Sub frmrefreshONlinedtu_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
		Me.Visible = False
	End Sub
	
	'UPGRADE_NOTE: Dscs was upgraded to Dscs_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub tmrReadheartbeat_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles tmrReadheartbeat.Tick
		Dim Dscs_Renamed As Object
		Dim i As Object
		Dim MAX_RECEIVE_BUF As Object
		'UPGRADE_WARNING: Arrays in structure rvData may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
        Dim rvData As New data_record
		'UPGRADE_WARNING: Arrays in structure rv may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		Dim rv As ModemDataStruct
		Dim sPhon As String
		Dim Gprsresult As Integer
		Gprsresult = hddll.DSgetnextdataAndPhonnumber(rvData, sPhon, 0)
		Dim hdrv() As Byte
		If rvData.m_data_type = Val(CStr(&H9)) Then
			ReDim hdrv(UBound(rvData.m_data_buf) - 1)
			For i = 0 To UBound(hdrv)
				'UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				hdrv(i) = rvData.m_data_buf(i + 1)
			Next i
			'UPGRADE_WARNING: Couldn't resolve default property of object Dscs.InformEvent. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            SHDscs.InformEvent(sPhon, hdrv, CInt(rvData.m_data_len))
			
		End If
		Gprsresult = SRdll.DSgetnextdataAndPhonnumber(rv, sPhon, 0)
		If rv.m_data_type = Val(CStr(&H1)) Then
			'UPGRADE_WARNING: Couldn't resolve default property of object Dscs.InformEvent. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            SHDscs.InformEvent(sPhon, rv.m_data_buf, CInt(rv.m_data_len))
			
		End If
		
	End Sub
	
	Private Sub tmrRefreshonlinedtu_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles tmrRefreshonlinedtu.Tick
		Dim i As Object
		
        For i = 1 To SHDscs.count
            SHDscs(i).RefreshonlineDTUTable()
        Next i
	End Sub
End Class