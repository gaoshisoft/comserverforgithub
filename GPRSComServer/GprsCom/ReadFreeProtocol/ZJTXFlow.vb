Imports VB = Microsoft.VisualBasic
Public Class ZJTXFlow
    Implements IGPRSRTU



    Public Const MBAddrQty As Long = 50
    Public Const MBAD As Long = 201
    Private MdtId As Integer
    Private ComState As Boolean
    Private SendTime As Double

    Private WithEvents Tmr As Timer = New Timer
    Private WithEvents Mdsc As DSCinterface
    Private mPhoneNumber As String
    'Public MBAD As Integer
    Private mMBStartAd As Integer
    Private mPollTime As Integer
    Public DtBlocks As System.Collections.Generic.List(Of TXDataBlock)
    Public mPollEnable As Boolean
    Public Enable As Boolean

    Public StationName As String
    Public TimeOut As Integer
    Private mComSuccesstimes As Integer
    Private mComFailedTimes As Integer

    Public Property CurrentDatablockID() As Integer
        Get
            If MdtId < 0 Then
                MdtId = 0
            End If
            If MdtId >= Me.DtBlocks.Count Then
                MdtId = Me.DtBlocks.Count - 1
            End If

            CurrentDatablockID = MdtId
        End Get
        Set(ByVal Value As Integer)
            If Value < 0 Then
                Value = 0
            End If
            If Value >= Me.DtBlocks.Count Then
                Value = 0
                Me.mPollEnable = False '已巡测完最后一个数据块结束此次通讯
            End If

            MdtId = Value

        End Set
    End Property






    Private Sub Tmr_Tick() Handles Tmr.Tick
        Dim Dtid As Integer
        Dim dt As TXDataBlock
        Dim Cmd() As Byte
        Dim SndRst As Integer
        If Me.Enable = True Then
            If Me.DtBlocks.Count > 0 Then
                If Me.mPollEnable = True Then
                    If Mdsc.IfThisDtuOnline((Me.mPhoneNumber)) Then
                        Dtid = Me.CurrentDatablockID

                        dt = Me.DtBlocks(Dtid)

                        If dt.Enable = True Then
                            If Me.ComState = False Then '表此时可发送数据
                                Cmd = dt.GetCommandBytes

                                SndRst = Mdsc.SenddataByPhon(Me.mPhoneNumber, UBound(Cmd) + 1, Cmd)
                                If SndRst = True Then '发送成功
                                    SendTime = DateDiff(Microsoft.VisualBasic.DateInterval.Second, #1/1/1970#, Today) + VB.Timer()
                                    '显示
                                    If Fview.Visible = True Then
                                        If Fview.chkGprscomdisplay.CheckState = 1 Then
                                            Fview.Text1.Text = HextoStr(Cmd, UBound(Cmd) + 1, 0)
                                            Fview.txtRtuname.Text = Me.StationName
                                        End If
                                    End If
                                    Me.ComState = True
                                End If
                            Else '表此时已发送数据还没有返回,这时记时，如果记时到还没有返回则认为超时
                                If DateDiff(Microsoft.VisualBasic.DateInterval.Second, #1/1/1970#, Today) + VB.Timer() - SendTime > Me.TimeOut Then '内部监视已超时,强制此次通讯结束，并将通讯状态复位
                                    '显示给用户
                                    If Fview.Visible = True Then
                                        If Fview.chkGprscomdisplay.CheckState = 1 Then

                                            Fview.Text2.Text = Me.StationName & "块" & Me.CurrentDatablockID & "通讯超时"
                                            Fview.txtRtuname.Text = Me.StationName & "(" & Now & ")"
                                            Fview.txtRvtime.Text = VB6.Format(DateDiff(Microsoft.VisualBasic.DateInterval.Second, #1/1/1970#, Today) + VB.Timer() - SendTime, "0.00")
                                        End If
                                    End If
                                    Me.ComfaileTimes = Me.ComfaileTimes + 1
                                    Me.CurrentDatablockID = Me.CurrentDatablockID + 1
                                    Me.ComState = False
                                End If


                            End If



                        Else
                            Me.CurrentDatablockID = Me.CurrentDatablockID + 1

                        End If


                    End If

                End If
            End If
        End If
    End Sub

    Private Sub Mdsc_DataReturn(ByVal PhoneNumber As String, ByVal value As Object, ByVal Length As Integer) Handles Mdsc.DataReturn
        Dim dt As TXDataBlock
        Dim mValue() As Byte
        Dim find As Boolean
        If Length > 0 Then
            If PhoneNumber = Me.mPhoneNumber Then

                dt = Me.DtBlocks((Me.CurrentDatablockID)) '确定当前数据块
                mValue = value

                If mValue(1) = dt.Addr Then '说明就是当前轮询的返回数据
                    find = True '

                Else '如果功能码不符，说明是控制命令的返回

                    find = False
                End If
                If find = True Then '是轮询数据正常接收
                    'mbresult = Me.GetGPRSByteValue(dt, Length, mValue)
                    dt.GetValueFromRvData(UBound(mValue) + 1, mValue)

                    '显示给用户
                    If Fview.Visible = True Then
                        If Fview.chkGprscomdisplay.CheckState = 1 Then

                            Fview.Text2.Text = HextoStr(mValue, Length, 0) & Me.StationName & "块" & Me.CurrentDatablockID & "通讯成功"
                            Fview.txtRtuname.Text = Me.StationName & "(" & Now & ")"
                            Fview.txtRvtime.Text = VB6.Format(DateDiff(Microsoft.VisualBasic.DateInterval.Second, #1/1/1970#, Today) + VB.Timer() - SendTime, "0.00")
                        End If
                    End If

                    '记数
                    'Me.ReceiveByteQty = Me.ReceiveByteQty + Length
                    '              Me.DataMove.DataMove
                    Me.mComSuccesstimes = Me.mComSuccesstimes + 1
                    Me.ComState = False '表示一次通讯完成,复位通讯状态
                    Me.CurrentDatablockID = Me.CurrentDatablockID + 1
                Else '丢弃
                    'Me.ReceiveCommandReturn(mValue)

                    'If Fview.Visible = True Then
                    '    If Fview.chkGprscomdisplay.CheckState = 1 Then

                    '        Fview.Text2.Text = HextoStr(mValue, Length, 0) & "  " & Me.RtuName & "控制命令已被执行！"
                    '        Fview.txtRtuname.Text = Me.RtuName & "(" & Now & ")"
                    '    End If
                    'End If

                    ''记数
                    'Me.ReceiveByteQty = Me.ReceiveByteQty + Length

                    'Me.ComSuccesstimes = Me.ComSuccesstimes + 1
                End If



            End If
        End If
        'err_Renamed:
    End Sub

    Public Sub New()
        DtBlocks = New System.Collections.Generic.List(Of TXDataBlock)
        Tmr.Interval = 100
        Tmr.Enabled = True
        Mdsc = Mydsc
        'Dim mydele As New GPRSCDMADSCserver.__SHInterface_DataReturnEventHandler(address of mdsc.datareturn)
        Me.TimeOut = 15
    End Sub

    Public Property PhoneNumber() As String Implements IGPRSRTU.PhoneNumber
        Get
            PhoneNumber = mPhoneNumber
        End Get
        Set(ByVal value As String)
            mPhoneNumber = value
        End Set
    End Property

    Public Property ifOnline() As Boolean Implements IGPRSRTU.ifOnline
        Get
            ifOnline = Mdsc.IfThisDtuOnline(PhoneNumber)

        End Get
        Set(ByVal value As Boolean)

        End Set
    End Property

    Public Property PollTime() As Integer Implements IGPRSRTU.PollTime
        Get
            PollTime = mPollTime
        End Get
        Set(ByVal value As Integer)
            mPollTime = value
        End Set
    End Property

    Public Property RtuName() As String Implements IGPRSRTU.RtuName
        Get
            RtuName = StationName
        End Get
        Set(ByVal value As String)
            StationName = value
        End Set
    End Property

    Public Property PollEnable() As Boolean Implements IGPRSRTU.PollEnable
        Get
            PollEnable = mPollEnable
        End Get
        Set(ByVal value As Boolean)
            mPollEnable = value
        End Set
    End Property

    Public Property ComfaileTimes() As Integer Implements IGPRSRTU.ComfaileTimes
        Get
            ComfaileTimes = mComFailedTimes
        End Get
        Set(ByVal value As Integer)
            mComFailedTimes = value
        End Set
    End Property

    Public Property ComSuccesstimes() As Integer Implements IGPRSRTU.ComSuccesstimes
        Get
            ComSuccesstimes = mComSuccesstimes
        End Get
        Set(ByVal value As Integer)
            mComSuccesstimes = value
        End Set
    End Property


    Public Property MBStartAD() As Integer
        Get
            MBStartAD = mMBStartAd
        End Get
        Set(ByVal value As Integer)
            mMBStartAd = value
        End Set
    End Property
End Class
