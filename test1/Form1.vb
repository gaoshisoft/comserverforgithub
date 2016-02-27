Imports SocketUtil

Public Class Form1
    Dim WithEvents serverSocket As ServerSocket = New ServerSocket()
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        serverSocket.setPort(8888)
        serverSocket.setListenCount(100)
        serverSocket.setSessionTimeOut(120000)
        serverSocket.start()

    End Sub

    Private Sub serverSocket_Recieved(ByVal sa As SocketUtil.SocketArgs) Handles serverSocket.Recieved
        TextBox1.AppendText(sa.Datas.ToString)

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        



    End Sub
End Class
