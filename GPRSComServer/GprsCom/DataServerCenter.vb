Option Strict Off
Option Explicit On

'Imports GPRSCDMADSCserver
Imports System.Net
'Imports ChannelcommDSC.TCPClientDSC.TcpChannel
Imports ChannelcommDSC
Imports System.Xml

Namespace GprsCom


    Friend Class DataServerCenter

        Public HdserverPort As Integer
        Public SrserverPort As Integer
        Public ExinDscPort As Integer
        Public HdWaitTime As Integer
        Public SrWaitTime As Integer
        Public ExinDscWaitTime As Integer
        Public ExinDscAdapter As Integer
        Public Event DataReturn(ByVal phoneNumber As String, ByVal value As Object, ByVal Length As Integer)

        'Dim WithEvents Srdsc As GPRSCDMADSCserver.SHInterface
        Public WithEvents TCPDsc As ExinDSC.cDSC
        'Public WithEvents Udpdsc As UDPDSC.cDSC
        Public WithEvents HDdsc As HDDSC.cDSC
        Private _mvarDscstate As String '局部复制
        Public WithEvents CommDsc As ChannelcommDSC.Channelclient
        Private Shared MyDsc As DataServerCenter
        Public Property Dscstate() As String
            Get


                'mvarDscstate = Srdsc.Dscstate & " " & HDdsc.DscState
                _mvarDscstate = HDdsc.DscState
                Dscstate = Replace(_mvarDscstate, Chr(13) & Chr(10), " ")
            End Get
            Set(ByVal Value As String)

                'Syntax: X.Dscstate = 5
                _mvarDscstate = Value
            End Set
        End Property

        Sub AddTcpClient(ByVal comminfo As String)
            CommDsc.Addnewchannel(comminfo, comminfo, 1, True)
        End Sub



        Public Function SenddataByPhon(ByVal Comminfo As String, ByVal Length As Integer, ByVal Data() As Byte) As Boolean
            Dim D() As Byte
            D = Data
            If Comminfo.Contains(":") Then '说明这是一个modbus tcp RTU x.x.x.x:x
                If CommDsc.GetChannelState(Comminfo) Then


                    CommDsc.SendByteData(Comminfo, Data)
                    Return True
                    Exit Function
                End If
            End If
            'If Srdsc.IfThisChannelUseable(commInfo) Then
            '    SenddataByPhon = Srdsc.SenddataByPhon(commInfo, Length, D(0))
            'End If
            If TCPDsc.IfThisDtuOline(Comminfo) Then
                SenddataByPhon = TCPDsc.SenddataByPhon(Comminfo, Length, D)
            End If
            'If Udpdsc.IfThisDtuOline(Comminfo) Then
            '    SenddataByPhon = Udpdsc.SenddataByPhon(Comminfo, Length, D)
            'End If
            If HDdsc.IfThisDtuOline(Comminfo) Then
                SenddataByPhon = HDdsc.SenddataByPhon(Comminfo, Length, D)
            End If
        End Function




        Function GetOnlineDtus() As HDDSC.ConlineDtus
            Dim Dtus As New HDDSC.ConlineDtus
            Dim Tcpdtus As ExinDSC.ConlineDtus
            Tcpdtus = TCPDsc.OnlineDtus
            'Dim Udpdtus As UDPDSC.ConlineDtus
            'Udpdtus = Udpdsc.ConlineDtus
            'Dtus
            Dim i As Integer
            For i = 1 To HDdsc.ConlineDtus.Count
                Dtus.Add((HDdsc.ConlineDtus(i).Phonenumber), (HDdsc.ConlineDtus(i).LoginTime), HDdsc.ConlineDtus(i).Phonenumber)

            Next
            'For i = 1 To Odtus.count
            '    Dtus.Add((Odtus(i).commInfo), (Odtus(i).LoginTime), Odtus(i).commInfo)
            'Next i
            For i = 1 To Tcpdtus.Count
                Dtus.Add((Tcpdtus(i).SimCardNo), (Tcpdtus(i).LoginTime), Tcpdtus(i).SimCardNo)
            Next i
            'For i = 1 To Udpdtus.Count
            '    Dtus.Add((Udpdtus(i).SimCardNo), (Udpdtus(i).LoginTime), Udpdtus(i).SimCardNo)
            'Next i
            GetOnlineDtus = Dtus
        End Function
        Function IfThisChannelUseable(ByVal comInfo As String) As Boolean
            Dim B2 As Boolean, b3 As Boolean, b4 As Boolean
            If comInfo.Contains(":") Then


                If CommDsc.Channels.CheckKey(comInfo) Then
                    IfThisChannelUseable = CommDsc.Channels(comInfo).ChannelState
                    Exit Function
                End If

            End If
            'B1 = Srdsc.IfThisChannelUseable(commInfo)
            B2 = TCPDsc.IfThisDtuOline(comInfo)
            'b3 = Udpdsc.IfThisDtuOline(comInfo)
            b4 = HDdsc.IfThisDtuOline(comInfo)
            IfThisChannelUseable = B2 Or b4



        End Function


        Private Function DstroyDsc() As Boolean
            'DstroyDsc = Srdsc.DstroyDsc
            'ExinDSC
            For Each r As GPRSRTU In RTUs
                r.Dsc = Nothing
            Next
            TCPDsc = Nothing

            'Udpdsc = Nothing
            HDdsc = Nothing
            CommDsc = Nothing

            'GC.Collect()
            DstroyDsc = True
        End Function



        Private Sub New()
            'Srdsc = New GPRSCDMADSCserver.SHInterface
            'Srdsc.BuildDsc(dsctype.宏电, (RTUs.HdserverPort), RTUs.HdWaitTime)
            'Srdsc.BuildDsc(dsctype.桑荣, (RTUs.SrserverPort), RTUs.SrWaitTime)
            TCPDsc = ExinDSC.cDSC.GetTCPsvrDSC
            'Udpdsc = New UDPDSC.cDSC
            HDdsc = New HDDSC.cDSC
            CommDsc = New Channelclient


        End Sub
        Public Sub Init()
            'Dim Ip As System.Net.IPAddress
            'Ip = ipaddress.any



            Dim Configdoc As New XmlDocument
            Configdoc.Load((My.Application.Info.DirectoryPath & "\RTUconfig.xml"))
            Dim Xmlparent As XmlElement

            Xmlparent = Configdoc.SelectSingleNode("root/rtucomminfo")




            Xmlparent = Configdoc.SelectSingleNode("root/gprscomm")
            HdserverPort = Xmlparent.GetAttribute("hdserverport")
            SrserverPort = Xmlparent.GetAttribute("srserverport")
            ExinDscPort = Xmlparent.GetAttribute("exindscport")
            HdWaitTime = Xmlparent.GetAttribute("hdwaittime")
            SrWaitTime = Xmlparent.GetAttribute("srwaittime")
            ExinDscWaitTime = Xmlparent.GetAttribute("exindscwaittime")
            ExinDscAdapter = Xmlparent.GetAttribute("exindscadapter")



            TCPDsc.Init(ExinDscPort, ExinDscWaitTime)
            'Udpdsc.Init(IPAddress.Any, ExinDscPort, ExinDscWaitTime)
            HDdsc.Init(IPAddress.Any, HdserverPort, HdWaitTime)

        End Sub
        Public Sub Close()
            TCPDsc.Close()
            'Udpdsc.Close()
            HDdsc.Close()
            CommDsc.Sleep()

        End Sub
        Public Shared Function GetMyDsc()
            If MyDsc Is Nothing Then
                MyDsc = New DataServerCenter()
                MyDsc.Init()
            Else
                'MyDsc.Close()
                MyDsc.Init()
            End If
            Return MyDsc
        End Function


        Private Sub NewDSC_DataReturn(ByVal phoneNumber As String, ByVal Value As Object, ByVal length As Integer) Handles TCPDsc.DataReturn
            RaiseEvent DataReturn(phoneNumber, Value, length)
        End Sub

        'Private Sub UDPDSC_DataReturn(ByVal phoneNumber As String, ByVal Value As Object, ByVal length As Integer) Handles Udpdsc.DataReturn
        '    RaiseEvent DataReturn(phoneNumber, Value, length)
        'End Sub

        Private Sub HDdsc_DataReturn(ByVal phoneNumber As String, ByRef Value As Object, ByVal length As Integer) Handles HDdsc.DataReturn
            RaiseEvent DataReturn(phoneNumber, Value, length)
        End Sub

        Private Sub TcpDsc_Dataarrival(ByVal comminfo As String, ByVal data() As Byte) Handles CommDsc.Dataarrival
            RaiseEvent DataReturn(comminfo, data, data.Length)
        End Sub
    End Class
End Namespace