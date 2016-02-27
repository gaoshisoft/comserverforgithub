Option Strict Off
Option Explicit On
Friend Class frmConnstatequery
    Inherits System.Windows.Forms.Form

    Private Sub Command1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
        Handles Command1.Click
        Me.Close()
    End Sub

    'Private Sub DTPicker1_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles DTPicker1.Change


    'End Sub

    Private Sub frmConnstatequery_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
        Handles MyBase.Load
        Dim Itmx As System.Windows.Forms.ListViewItem
        Dim i As Integer
        Dim Rs As New ADODB.Recordset
        DTPicker1.Value = Today
        Try
            QueryconnstatefromDB(Rs, (DTPicker1.Value))
            ListView1.Items.Clear()
            For i = 1 To Rs.RecordCount
                'UPGRADE_ISSUE: MSComctlLib.ListItems 方法 ListView1.ListItems.Add 未升级。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"”
                Itmx = ListView1.Items.Add("itm1")
                Itmx.Text = Rs.Fields(2).Value
                'UPGRADE_WARNING: 集合 Itmx 的下限已由 1 更改为 0。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"”
                If Itmx.SubItems.Count > 1 Then
                    Itmx.SubItems(1).Text = Rs.Fields(1).Value
                Else
                    Itmx.SubItems.Insert(1,
                                         New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing,
                                                                                               Rs.Fields(1).Value))
                End If
                'UPGRADE_WARNING: 集合 Itmx 的下限已由 1 更改为 0。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"”
                If Itmx.SubItems.Count > 2 Then
                    Itmx.SubItems(2).Text = Rs.Fields(3).Value
                Else
                    Itmx.SubItems.Insert(2,
                                         New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing,
                                                                                               Rs.Fields(3).Value))
                End If
                Rs.MoveNext()
            Next i
        Catch ex As Exception
        Finally
            Rs.Close()
            Rs = Nothing
        End Try
    End Sub


    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles DTPicker1.ValueChanged
        Dim Itmx As System.Windows.Forms.ListViewItem
        Dim i As Integer
        Dim Rs As New ADODB.Recordset
        Try
            QueryconnstatefromDB(Rs, (DTPicker1.Value))
            ListView1.Items.Clear()
            For i = 1 To Rs.RecordCount
                'UPGRADE_ISSUE: MSComctlLib.ListItems 方法 ListView1.ListItems.Add 未升级。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"”
                Itmx = ListView1.Items.Add("itm1")
                Itmx.Text = Rs.Fields(2).Value
                'UPGRADE_WARNING: 集合 Itmx 的下限已由 1 更改为 0。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"”
                If Itmx.SubItems.Count > 1 Then
                    Itmx.SubItems(1).Text = Rs.Fields(1).Value
                Else
                    Itmx.SubItems.Insert(1,
                                         New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing,
                                                                                               Rs.Fields(1).Value))
                End If
                'UPGRADE_WARNING: 集合 Itmx 的下限已由 1 更改为 0。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"”
                If Itmx.SubItems.Count > 2 Then
                    Itmx.SubItems(2).Text = Rs.Fields(3).Value
                Else
                    Itmx.SubItems.Insert(2,
                                         New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing,
                                                                                               Rs.Fields(3).Value))
                End If
                Rs.MoveNext()
            Next i
        Catch ex As Exception
        Finally
            Rs.Close()
            Rs = Nothing
        End Try
    End Sub
End Class