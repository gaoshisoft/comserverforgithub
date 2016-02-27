Option Strict Off
Option Explicit On
Module KOS_Module
	'/////////////////////////////////////////////////////////////////////////////
	'//
	'//  Knight OPC Server Rapid Development Toolkit -- KOSRDK
	'//
	'//            Module File for Visual Basic 6.0
	'/////////////////////////////////////////////////////////////////////////////
	'//
	'//          Author: Knight Fox
	'//    Initial Date: 11/18/2001
	'//        Workfile: KOSRDKApi.h
	'//        Revision: 1.8
	'//            Date: Jun 20, 2004 13:25p
	'//   Target System: Microsoft Windows NT 4.0 / 95 /98 / 2000 / XP
	'//     Environment: Visual Basic 6.0 / OPC DataAccess 1.0/2.05
	'//         Remarks:
	'//  Product Update: http://www.eehoo.net
	'//         Contact: opc@eehoo.net
	'//
	'/////////////////////////////////////////////////////////////////////////////
	'//  Copyright (C) 2001-2004, eehoo link Technologies, Inc.
	'/////////////////////////////////////////////////////////////////////////////
	
	'Public g_strMsg As String
	
	Public GUID(16) As Byte
	Public svrName(100) As Byte
	Public SvrDesc(100) As Byte
	Public ExePath(255) As Byte
    'Public Clnt As Clientevent
	
	'------------------------------------------------------------------------------
	'  Initialization and Registration functions
	'------------------------------------------------------------------------------
	'
	'  KOS_InitB()
	'      Initializes DCOM, Security, etc.
	Declare Function KOS_InitB Lib "KOSRDK.dll" (ByRef CLSID_Svr As Byte, ByVal ServerRate As Integer) As Integer
	
	'  KOS_UnInit()
	'      UnInitializes DCOM, Security, etc.
	Declare Function KOS_Uninit Lib "KOSRDK.dll" () As Integer
	
	'  KOS_RegisterB()
	'      Makes the appropriate entries to the Windows Registry to identify the Server.
	Declare Function KOS_RegisterB Lib "KOSRDK.dll" (ByRef CLSID_Svr As Byte, ByRef name As Byte, ByRef Descr As Byte, ByRef ExePath As Byte) As Integer
	
	'  KOS_UnRegisterB()
	'      Remvoe from Windows Registry
	Declare Function KOS_UnRegisterB Lib "KOSRDK.dll" (ByRef CLSID_Svr As Byte, ByRef name As Byte) As Integer
	
	
	'---------------------------------------------------------------------------------
	'  OPC Item Functions
	'---------------------------------------------------------------------------------
	Declare Function KOS_AddItem Lib "KOSRDK.dll" (ByRef name As Byte, ByVal value As Object, ByVal InitialQuality As Short, ByVal IsWritable As Integer) As Integer
	
	Declare Function KOS_UpdateItem Lib "KOSRDK.dll" (ByVal ItemHandle As Integer, ByVal value As Object, ByVal Quality As Short) As Integer
	
	Declare Function KOS_RemoveItem Lib "KOSRDK.dll" (ByVal ItemHandle As Integer) As Integer
	
	'------------------------------------------------------------------------------
	'  Advanced functions
	'------------------------------------------------------------------------------
	'
	'  KOS_GetClientCount()
	'      Allows the Server Application to determine the number of OPC Clients currently connected.
	Declare Function KOS_GetClientCount Lib "KOSRDK.dll" () As Short
	'
	'  KOS_ShutdownClients()
	'      Request all  Clients  disconnect.
	Declare Sub KOS_ShutdownClients Lib "KOSRDK.dll" ()
	
	'------------------------------------------------------------------------------
	'  Registration functions
	'------------------------------------------------------------------------------
	Declare Sub KOS_Active Lib "KOSRDK.dll" (ByRef regName As Byte, ByRef regCode As Byte)
	
	
	'------------------------------------------------------------------------------
	'  Callback functions
    '------------------------------------------------------------------------------
    Delegate Sub ClientWriteProc(ByVal ItemHandle As Integer, ByRef value As Object)
    Declare Function KOS_SetClientWriteProc Lib "KOSRDK.dll" (ByVal Callback As ClientWriteProc) As Integer
    Delegate Sub ClientShutdownProc(ByVal ClientCount As Short)

    Declare Function KOS_SetClientShutdownProc Lib "KOSRDK.dll" (ByVal Callback As ClientShutdownProc) As Integer
	Declare Function KOS_SetHashSize Lib "KOSRDK.dll" (ByVal HashSize As Integer) As Integer
	
	'Declare Function KOS_SetItemConnectProc Lib "KOSRDK.dll" (ByVal Callback As Long) As Long
	
	'Sub ClientWriteProc(ByVal ItemHandle As Long, ByRef var as variant)
	
	'End Sub
	'Sub ClientShutdownProc(ByVal ClientCount As Integer)
	
	'End Sub
	'Sub ItemConnectProc(ByVal ItemHandle As Long, ByVal IsConnect As Long)
	
	'End Sub
	
	'===========================================================================apiº¯ÊýÉùÃ÷Íê±Ï
	
	
	
	'Sub KOS_ItemConnectProc(ByVal ItemHandle As Long, ByVal IsConnect As Long)
	'    For Each CItem In g_ItemCol
	'        If CItem.ItemHandle = ItemHandle Then
	'            CItem.ItemConnected = IsConnect
	'            Exit For
	'        End If
	'    Next
	'End Sub
	
	Sub GUIDToByte(ByRef strGUID As String, ByRef GUID() As Byte)
		GUID(0) = Val("&H" & Mid(strGUID, 8, 2))
		GUID(1) = Val("&H" & Mid(strGUID, 6, 2))
		GUID(2) = Val("&H" & Mid(strGUID, 4, 2))
		GUID(3) = Val("&H" & Mid(strGUID, 2, 2))
		GUID(4) = Val("&H" & Mid(strGUID, 13, 2))
		GUID(5) = Val("&H" & Mid(strGUID, 11, 2))
		GUID(6) = Val("&H" & Mid(strGUID, 18, 2))
		GUID(7) = Val("&H" & Mid(strGUID, 16, 2))
		GUID(8) = Val("&H" & Mid(strGUID, 21, 2))
		GUID(9) = Val("&H" & Mid(strGUID, 23, 2))
		GUID(10) = Val("&H" & Mid(strGUID, 26, 2))
		GUID(11) = Val("&H" & Mid(strGUID, 28, 2))
		GUID(12) = Val("&H" & Mid(strGUID, 30, 2))
		GUID(13) = Val("&H" & Mid(strGUID, 32, 2))
		GUID(14) = Val("&H" & Mid(strGUID, 34, 2))
		GUID(15) = Val("&H" & Mid(strGUID, 36, 2))
	End Sub
	
	Sub StringToByte(ByRef strString As String, ByRef ByteArray() As Byte)
		Dim i As Integer
		Dim bt() As Byte
		'UPGRADE_ISSUE: Constant vbFromUnicode was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
		'UPGRADE_TODO: Code was upgraded to use System.Text.UnicodeEncoding.Unicode.GetBytes() which may not have the same behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="93DD716C-10E3-41BE-A4A8-3BA40157905B"'
        bt = System.Text.ASCIIEncoding.ASCII.GetBytes(strString)
		For i = LBound(bt) To UBound(bt)
			ByteArray(i) = bt(i)
		Next i
		
	End Sub
	
	
	Sub FormatOpcQuality(ByRef nQuality As Short, ByRef strTmp As String)
		Select Case (nQuality)
			Case 64
				strTmp = "OPC_QUALITY_UNCERTAIN"
			Case 0
				strTmp = "OPC_QUALITY_BAD"
			Case 192
				strTmp = "OPC_QUALITY_GOOD"
			Case Else
				strTmp = "Unknow"
		End Select
	End Sub
	
	Sub FormatDataType(ByRef value As Object, ByRef strTmp As String)
		'UPGRADE_WARNING: VarType has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		Select Case (VarType(value))
			Case VariantType.Boolean
				strTmp = "Boolean"
			Case VariantType.Short
				strTmp = "Integer"
			Case VariantType.Integer
				strTmp = "Long"
			Case VariantType.Single
				strTmp = "Single"
			Case VariantType.String
				strTmp = "String"
			Case Else
				strTmp = "Unknow"
		End Select
		
	End Sub
End Module