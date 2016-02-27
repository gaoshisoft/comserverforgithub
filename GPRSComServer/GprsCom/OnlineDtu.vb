Option Strict Off
Option Explicit On

Imports GPRSComServer.GprsCom
Imports VB = Microsoft.VisualBasic

Friend Class frmOnlineDTU
    Inherits System.Windows.Forms.Form

    Dim WithEvents Tmr As Timer
    Private Sub Command1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command1.Click
        Me.Hide()
    End Sub

    Private Sub Command2_Click()
        Fview.Show()
    End Sub

    Private Sub frmOnlineDTU_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Label1.Text = Mydsc.Dscstate
        'ListView1.
        Tmr = New Timer
        Tmr.Interval = 5000
        Tmr.Start()
        Tmr_Tick(Me, eventArgs.Empty)
        Me.DoubleBuffered = True
    End Sub



    Function GetListItemFromPhonNumber(ByRef P As String) As System.Windows.Forms.ListViewItem
        'Dim L As System.Windows.Forms.ListViewItem
        Dim Itm As System.Windows.Forms.ListViewItem = Nothing
        Try
            If ListView1.Items.ContainsKey(P) Then
                Itm = ListView1.Items(P)
            End If
        Catch ex As Exception

        Finally
            If Itm Is Nothing Then
                Itm = Me.ListView1.Items.Add("站名")
                Itm.Name = P
                Itm.SubItems(0).Text = "站名"


                Itm.SubItems.Add("登录或下线时间")
                Itm.SubItems.Add("SIM卡号")
                Itm.SubItems.Add("巡测周期")
                Itm.SubItems.Add("连接状态")



            End If
        End Try

        GetListItemFromPhonNumber = Itm
    End Function
    Sub DeleteListItem()
        Dim i As Object
        Dim L As System.Windows.Forms.ListViewItem
        Dim find As Boolean
        Dim c As New Collection
        Dim Onlinedtus As HDDSC.ConlineDtus

        'Dim r As IGPRSRTU
        For Each L In Me.ListView1.Items

            find = False
            If L.Text = "站名" Then '只有未定义过的DTU才需要删

                Onlinedtus = Mydsc.GetOnlineDtus
                If Onlinedtus.ContainsObj(L.SubItems(2).Text) Then
                    find = True
                End If




            Else
                If RTUs.Containsobj(L.Text) Then
                    find = True
                End If
            End If
            If find = False Then
                c.Add(L)

            End If
        Next L
        For i = 1 To c.Count()
            Me.ListView1.Items.RemoveAt(c.Item(i).Index)
        Next i

    End Sub


    Function getRtufromphonnumber(ByRef phon As String) As IGPRSRTU

        Dim i As Integer
        For i = 1 To RTUs.Count
            If RTUs(i).CommInfo.Contains(phon) Or phon.Contains(RTUs(i).CommInfo) Then
                Return RTUs(i)
                Exit Function
            End If
        Next i

    End Function

    Private Sub frmOnlineDTU_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        ListView1.Width = Me.Width
    End Sub

    Private Sub Tmr_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tmr.Tick
        Dim r As GPRSRTU
        Dim i As Integer

        Dim Itmx As System.Windows.Forms.ListViewItem
        Try
            Dim Onlinedtus As HDDSC.ConlineDtus
            Onlinedtus = Mydsc.GetOnlineDtus
            Dim P As String
            For i = 1 To Onlinedtus.Count
                P = Onlinedtus(i).Phonenumber
                r = getRtufromphonnumber(P)


                If r Is Nothing Then '未定义的DTU
                    Itmx = GetListItemFromPhonNumber(P)

                    Itmx.SubItems(2).Text = P
                    Itmx.SubItems(1).Text = CStr(Onlinedtus(i).LoginTime)

                    Itmx.SubItems(4).Text = "在线"

                End If
            Next i

            'Dim find As Boolean
            For i = 1 To RTUs.Count ' 刷新已定义DTU
                r = RTUs(i)

                Dim Rstate As String
                Rstate = IIf(r.IfOnline, "在线", "下线")
                Itmx = GetListItemFromPhonNumber((r.CommInfo))
                Itmx.ForeColor = IIf(r.IfOnline, System.Drawing.Color.Black, System.Drawing.Color.Red)
                If Rstate <> Itmx.SubItems(4).Text Then


                    Itmx.SubItems(0).Text = r.RtuName
                    Dim s As String
                    If r.IfOnline Then
                        If Not r.CommInfo.Contains(":") Then
                            s = Onlinedtus(r.CommInfo).LoginTime.ToString
                        End If
                    Else
                        s = ""
                    End If
                    Itmx.SubItems(1).Text = IIf(r.IfOnline, s, "")
                    Itmx.SubItems(2).Text = r.CommInfo


                    Itmx.SubItems(3).Text = r.polltime & "倍周期"


                    Itmx.SubItems(4).Text = Rstate
                End If





            Next i
            '删除没用的listitem
            DeleteListItem()
        Catch ex As Exception
        End Try
        '-----------------------------------------------------
        'ListView1.EndUpdate()


    End Sub

  

   
   
   

  

    Private Sub 复制上线信息ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 复制上线信息ToolStripMenuItem.Click

        My.Computer.Clipboard.SetText(ListView1.SelectedItems.Item(0).SubItems(2).Text)
        MsgBox("上线信息：" & ListView1.SelectedItems.Item(0).SubItems(2).Text & " 已被复制到剪贴板")
        'Dim text As String
        'If My.Computer.Clipboard.ContainsText Then
        '    text = My.Computer.Clipboard.GetText
        'End If

    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub
End Class
Partial Class DoubleBufferListView
   
    Inherits System.Windows.Forms.ListView

    Private Declare Function LockWindowUpdate Lib "user32.dll" (ByVal handle As IntPtr) As Boolean

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New(ByVal Container As System.ComponentModel.IContainer)
        MyClass.New()

        Me.DoubleBuffered = True

        Container.Add(Me)
    End Sub

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        Me.DoubleBuffered = True
    End Sub

    Public Overloads Sub BeginUpdate()
        LockWindowUpdate(Me.Parent.Handle)
    End Sub

    Public Overloads Sub EndUpdate()
        LockWindowUpdate(0)
    End Sub

End Class

