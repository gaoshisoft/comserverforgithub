Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("OPCserver_NET.OPCserver")> Public Class OPCserver
    Dim Cw As ClientWriteProc
    Dim CSD As ClientShutdownProc
	Public Event ClientWrite(ByVal ItemHandle As Integer, ByVal value As Object)
	Public Event ClientShutdown(ByVal ClientCount As Short)
    Sub InformClnwrt(ByRef ItemHandle As Integer, ByRef value As Object)
        RaiseEvent ClientWrite(ItemHandle, value)


    End Sub
    Sub InformClnShut(ByVal ClientCount As Short)
        RaiseEvent ClientShutdown(ClientCount)

    End Sub
	
    Sub Init(ByRef OpcServerName As String)
        Cw = New ClientWriteProc(AddressOf Me.KOS_ClientWriteProc)
        CSD = New ClientShutdownProc(AddressOf Me.KOS_ClientShutdownProc)


        Dim strName(50) As Byte
        Dim strCode(50) As Byte
        Dim cName(100) As Byte
        Dim Serial(100) As Byte
        Dim mbReturn As Integer


        'initialize GUID, SvrName, SvrDesc & Exe Path

        ' GUID from GUIDGEN
        ' {838D0944-1C9A-401e-9609-A661740B633A}
        'DEFINE_GUID(<<name>>,
        '0x838d0944, 0x1c9a, 0x401e, 0x96, 0x9, 0xa6, 0x61, 0x74, 0xb, 0x63, 0x3a);
        '    {F45AD4F5-D0E2-4c0f-AB8F-C6B66D3B7445}
        GUIDToByte("{F45AD4F5-D0E2-4c0f-AB8F-C6B66D3B7445}", GUID)
        'GUIDToByte("{F638BC81-75E4-46ed-8A7C-721CA9EF9D2E}", GUID)
        '// {F638BC81-75E4-46ed-8A7C-721CA9EF9D2E}
        'static const GUID <<name>> =
        '{ 0xf638bc81, 0x75e4, 0x46ed, { 0x8a, 0x7c, 0x72, 0x1c, 0xa9, 0xef, 0x9d, 0x2e } };

        StringToByte(OpcServerName, svrName)
        StringToByte(OpcServerName, SvrDesc)
        'UPGRADE_WARNING: App property App.EXEName has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
        StringToByte(My.Application.Info.DirectoryPath & "\" & My.Application.Info.AssemblyName & ".exe", ExePath)

        StringToByte("AnShanYiXing", cName)
        StringToByte("E7EA6GWSK0ZW", Serial)
        KOS_Active(cName(0), Serial(0))
        mbReturn = KOS_InitB(GUID(0), 500)
        'UPGRADE_WARNING: Add a delegate for AddressOf KOS_ClientShutdownProc Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="E9E157F7-EF0C-4016-87B7-7D7FBBC6EE08"'
        mbReturn = KOS_SetClientShutdownProc(CSD)
        'UPGRADE_WARNING: Add a delegate for AddressOf KOS_ClientWriteProc Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="E9E157F7-EF0C-4016-87B7-7D7FBBC6EE08"'
        mbReturn = KOS_SetClientWriteProc(Cw)
        '=============================================================
        '  Set Me.g_ItemCol = Xmlcfg.ReadMBToOpcServer
        mbReturn = KOS_RegisterB(GUID(0), svrName(0), SvrDesc(0), ExePath(0))

    End Sub
    Sub KOS_ClientWriteProc(ByVal ItemHandle As Integer, ByRef value As Object)
        InformClnwrt(ItemHandle, value)

    End Sub

    Sub KOS_ClientShutdownProc(ByVal ClientCount As Short)
     
        InformClnShut(ClientCount)

    End Sub
	

	
	Function AddItem(ByRef ItemName As String) As Integer
        Dim v As Object = New Object
		Dim ItemHandle As Integer
		Dim mItemName(50) As Byte
		StringToByte(ItemName, mItemName)
		ItemHandle = KOS_AddItem(mItemName(0), v, 192, True)
		AddItem = ItemHandle
	End Function
	Function RemoveItem(ByRef ItemHandle As Integer) As Integer
		RemoveItem = KOS_RemoveItem(ItemHandle)
		
	End Function
	Function UpdateItem(ByRef ItemHandle As Integer, ByRef ItemValue As Object, ByRef ItemQuality As Short) As Object
		UpdateItem = KOS_UpdateItem(ItemHandle, ItemValue, ItemQuality)
		
	End Function
	Sub Uninit()
		Dim i As Integer
		i = KOS_GetClientCount
		If i > 0 Then
			KOS_ShutdownClients() '�ر�OPC�ͻ���
		End If
		Dim mbReturn As Integer
		
		
		mbReturn = KOS_UnRegisterB(GUID(0), svrName(0))
		mbReturn = KOS_Uninit()
	End Sub
	

	Public Sub New()
		MyBase.New()

	End Sub
	

	Function SetHashSize(ByRef Size As Integer) As Integer
		SetHashSize = KOS_SetHashSize(Size)
	End Function
End Class