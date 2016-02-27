Imports System.Xml

Public Class frmopcsvrnameset

    Dim Configdoc As New XmlDocument

    Dim Xmlparent As XmlElement
    Private Sub frmopcsvrnameset_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Configdoc.Load((My.Application.Info.DirectoryPath & "\RTUconfig.xml"))
        Xmlparent = Configdoc.SelectSingleNode("root/opcserver")


        TextBox1.Text = Xmlparent.GetAttribute("opcservername")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Xmlparent.SetAttribute("opcservername", TextBox1.Text)
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class