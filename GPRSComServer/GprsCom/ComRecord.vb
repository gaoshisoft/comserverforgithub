Namespace GprsCom

    Public Class ComRecord
        Private Sub ComRecord_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Dim R As GPRSRTU
            Dim Tn As TreeNode
            TreeView1.Nodes.Add("GPRS站点", "GPRS站点")
            For Each R In RTUs
                Tn = TreeView1.Nodes("GPRS站点").Nodes.Add(R.RtuName, R.RtuName)
                Tn.EnsureVisible()
                Tn.Expand()
            Next
        End Sub

        Private Sub TreeView1_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) _
            Handles TreeView1.AfterSelect
            Dim N As TreeNode
            N = TreeView1.SelectedNode
            ListView1.Items.Clear()
            If N.Text <> "GPRS站点" Then
                For i As Int32 = 1 To RTUs(N.Text).UpLoadRecordBuff.Count
                    ListView1.Items.Add(BitConverter.ToString(RTUs(N.Text).UpLoadRecordBuff(i)))

                Next i
            End If
        End Sub


        Private Sub 清空ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles 清空ToolStripMenuItem.Click
            Dim N As TreeNode

            N = TreeView1.SelectedNode
            ListView1.Items.Clear()
            If Not TreeView1.SelectedNode Is Nothing Then
                If N.Text <> "GPRS站点" Then
                    RTUs(N.Text).UpLoadRecordBuff.Clear()
                End If
            End If
        End Sub


        Private Sub ListView1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
            Handles ListView1.MouseDoubleClick
            MsgBox(ListView1.SelectedItems(0).Text)
        End Sub

        Private Sub ListView1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
            Handles ListView1.MouseUp
            If e.Button = Windows.Forms.MouseButtons.Right Then
                ContextMenuStrip1.Show(ListView1, New Point(e.Location.X, e.Location.Y))
            End If
        End Sub

        Private Sub 缓冲区大小ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles 缓冲区大小ToolStripMenuItem.Click
            Dim N As TreeNode

            N = TreeView1.SelectedNode
            ListView1.Items.Clear()
            If Not TreeView1.SelectedNode Is Nothing Then
                Try
                    If N.Text <> "GPRS站点" Then
                        RTUs(N.Text).BuffNum = InputBox("输入大小：")
                    End If
                Catch
                End Try

            End If
        End Sub

        Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles ListView1.SelectedIndexChanged
        End Sub
    End Class
End Namespace