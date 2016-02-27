Option Strict Off
Option Explicit On

Imports System.Runtime.Remoting
Imports VB = Microsoft.VisualBasic

Friend Class ItemDlg
    Inherits System.Windows.Forms.Form

    Public mName As String
    Public mDataType As String
    Public mValue As Object
    Public mok As Boolean
    Public mDev As Integer
    Public mMBAD As String
    Public mNeedconvert As Boolean
    Public mAirangedown As Single
    Public mAirangeup As Single
    Public mConverteddown As Single
    Public mConvertedUP As Single
    Public mIfswapbyte As Boolean
    Public ChineseDis As String
    Public HandleExpresion As String
    Public UnitStr As String
    Public DownLimit As Double
    Public Uplimit As Double

   


    Private Sub CancelButton_Renamed_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
        Handles CancelButton_Renamed.Click
        Me.Close()
    End Sub

    'UPGRADE_WARNING: 初始化窗体时可能激发事件 cboDataType.SelectedIndexChanged。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"”
    Private Sub cboDataType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
        Handles cboDataType.SelectedIndexChanged
        ifVisiblebit()
    End Sub

    'UPGRADE_WARNING: 初始化窗体时可能激发事件 chkneedconvert.CheckStateChanged。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"”
    Private Sub chkneedconvert_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
        Handles chkneedconvert.CheckStateChanged
        'If chkneedconvert.value = 1 Then
        '   Fraconvert.Visible = True
        '   Else
        '   Fraconvert.Visible = False
        '   End If
    End Sub

    'UPGRADE_WARNING: Form 事件 ItemDlg.Activate 具有新的行为。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"”
    Private Sub ItemDlg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
        Handles MyBase.Activated
        ifVisiblebit()
    End Sub

    Private Sub ItemDlg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
        Handles MyBase.Load
        cboDataType.Items.Clear()
        cboDataType.Items.Add("Boolean")
        cboDataType.Items.Add("Integer")
        cboDataType.Items.Add("Single")
        cboDataType.Items.Add("SingleSwapWord")
        cboDataType.Items.Add("Double")
        cboDataType.Items.Add("DoubleSwapWord")
        cboDataType.Items.Add("Long")
        cboDataType.Items.Add("LongSwapWord")
        cboDataType.Items.Add("UnInteger")
        cboDevad.Items.Clear()
        Dim i As Integer
        For i = 1 To Mbs.Devices.Count
            cboDevad.Items.Add(CStr(Mbs.Devices(i).deviceAddr))
        Next i
        cboDevad.SelectedIndex = 0
        cboDataType.SelectedIndex = 0
        mok = False
        txtstaName.Text = mName.Split("_")(0)
        txtparaname.Text = mName.Split("_")(1)
        Dim dp As Integer
        Select Case mDataType
            Case "Boolean"
                dp = 0
            Case "Integer"
                dp = 1
            Case "Single"
                dp = 2
            Case "SingleSwapWord"
                dp = 3
            Case "Double"
                dp = 4
            Case "DoubleSwapWord"
                dp = 5
            Case "Long"
                dp = 6
            Case "LongSwapWord"
                dp = 7
            Case "UnInteger"
                dp = 8
        End Select
        cboDataType.SelectedIndex = dp
        cboDevad.Text = CStr(mDev)
        If mMBAD.Contains(":") Then
            txtMBAD.Text = Split(mMBAD, ":")(0)
            txtBit.Text = Split(mMBAD, ":")(1)
        Else
            txtMBAD.Text = mMBAD
        End If
        chkneedconvert.CheckState = IIf(mNeedconvert = True, 1, 0)
        txtAIrangedown.Text = CStr(mAirangedown)
        txtAIrangeUP.Text = CStr(mAirangeup)
        txtConvertedDown.Text = CStr(mConverteddown)
        txtConvertedUP.Text = CStr(mConvertedUP)
        chkifswapbyte.CheckState = IIf(mIfswapbyte, 1, 0)
        txtChineseDis.Text = ChineseDis
        txtExpresion.Text = HandleExpresion
        cbounitstr.Text = UnitStr
        txtuplimit.Text = Uplimit
        txtDownlimit.Text = DownLimit
    End Sub

    Sub Cleartext()
        'mName = ""
        mMBAD = VB.Left(mMBAD, 4)
    End Sub

    Private Sub OKButton_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
        Handles OKButton.Click
        Dim g_strMsg As String

        If InStr(txtstaName.Text, " ") > 0 Then
            MsgBox("名称中不能有空格！")
            Exit Sub
        End If
        If InStr(txtparaname.Text, " ") > 0 Then
            MsgBox("名称中不能有空格！")
            Exit Sub
        End If
        If Trim(txtstaName.Text) = "" Then
            txtstaName.Text = ""
            g_strMsg = "站名不能为空"
            MsgBox(g_strMsg, MsgBoxStyle.Critical)
            txtstaName.Focus()
            Exit Sub
        End If
        If Trim(txtparaname.Text) = "" Then
            txtstaName.Text = ""
            g_strMsg = "参数名称不能为空"
            MsgBox(g_strMsg, MsgBoxStyle.Critical)
            txtparaname.Focus()
            Exit Sub
        End If
        mName = Trim(txtstaName.Text) & "_" & Trim(txtparaname.Text)
        If txtBit.Text <> "" And cboDataType.Text = "Boolean" Then
            mMBAD = Trim(txtMBAD.Text) & ":" & Trim(txtBit.Text)
        Else
            mMBAD = Trim(txtMBAD.Text)
        End If

        If Trim(txtMBAD.Text) = "" Then
            '    txtName.Text = ""
            g_strMsg = "MBAD is empty"
            MsgBox(g_strMsg, MsgBoxStyle.Critical)
            '        txtName.SetFocus
            Exit Sub
        End If
        Select Case cboDataType.Text
            Case "Boolean"

                mValue = CBool(0)
            Case "Integer"
                mValue = CShort(0)
            Case "Single"
                mValue = CSng(0)
        End Select

        mDataType = cboDataType.Text
        mDev = Val(cboDevad.Text)


        mIfswapbyte = chkifswapbyte.CheckState
        mNeedconvert = chkneedconvert.CheckState
        If chkneedconvert.CheckState = 1 Then

            mAirangedown = Val(txtAIrangedown.Text)
            mAirangeup = Val(txtAIrangeUP.Text)
            mConverteddown = Val(txtConvertedDown.Text)
            mConvertedUP = Val(txtConvertedUP.Text)
            If mAirangedown - mAirangeup = 0 Or mConvertedUP - mConverteddown = 0 Then
                MsgBox("请输入正确的上下限值！", MsgBoxStyle.Critical)
                Exit Sub
            End If
        End If
        ChineseDis = txtChineseDis.Text
        HandleExpresion = txtExpresion.Text
        UnitStr = cbounitstr.Text
        Uplimit = CDbl(txtuplimit.Text)
        DownLimit = CDbl(txtDownlimit.Text)
        mok = True
        Me.Close()
    End Sub

    Private Sub txtBit_Validating(ByVal eventSender As System.Object,
                                  ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBit.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Not IsNumeric(txtBit.Text) Then
            MsgBox("请输入数字", MsgBoxStyle.OKOnly)
            Cancel = True
            GoTo EventExitSub
        End If
        If Val(txtBit.Text) > 15 Then
            MsgBox("请输入0-15之间整数", MsgBoxStyle.OKOnly)
            Cancel = True
            GoTo EventExitSub
        End If
        EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtMBAD_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMBAD.GotFocus
        txtMBAD.SelectionStart = 4
    End Sub

    'UPGRADE_WARNING: 初始化窗体时可能激发事件 txtMBAD.TextChanged。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"”
    Private Sub txtMBAD_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
        Handles txtMBAD.TextChanged
        ifVisiblebit()
    End Sub

    Private Sub txtMBAD_Validating(ByVal eventSender As System.Object,
                                   ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMBAD.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Not Isnumber((txtMBAD.Text)) Then
            MsgBox("请输入数字", MsgBoxStyle.OKOnly)
            Cancel = True
            GoTo EventExitSub
        End If
        If InStr(1, "0,1,3,4", VB.Left(txtMBAD.Text, 1)) = 0 Or Len(txtMBAD.Text) <> 6 Then
            MsgBox("请输入0x,1x,3x或4x的六位数地址！", MsgBoxStyle.OKOnly)
            Cancel = True
            GoTo EventExitSub
        End If
        If _
            Val(VB.Right(txtMBAD.Text, Len(txtMBAD.Text) - 1)) > 3000 Or
            Val(VB.Right(txtMBAD.Text, Len(txtMBAD.Text) - 1)) < 1 Then
            MsgBox("请输入适当的起始地址！")
            Cancel = True
        End If
        EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Sub ifVisiblebit()
        If VB.Left(Trim(txtMBAD.Text), 1) = "3" Or VB.Left(Trim(txtMBAD.Text), 1) = "4" Then
            If cboDataType.Text = "Boolean" Then
                frabit.Visible = True
            Else
                frabit.Visible = False
            End If


        Else
            frabit.Visible = False
        End If
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub
End Class