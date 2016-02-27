Option Strict Off
Option Explicit On

Imports VB = Microsoft.VisualBasic


Friend Class opcItemview
    Inherits System.Windows.Forms.Form
    Public mChoose As String
    Public mok As Boolean

    Private Sub Command1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) _
        Handles Command1.Click
       Dim i As Integer
        mChoose = ""
        For i = 0 To List1.Items.Count - 1
            If List1.GetSelected(i) = True Then
                mChoose = mChoose & List1.Items(i) & ","
            End If
        Next i
        mChoose = VB.Left(mChoose, Len(mChoose) - 1)

        mok = True
        Me.Close()
    End Sub

    Private Sub Command2_Click(ByVal EventSender As System.Object, ByVal EventArgs As System.EventArgs) _
        Handles Command2.Click
        Me.Close()
    End Sub

    Private Sub Command5_Click(ByVal EventSender As System.Object, ByVal EventArgs As System.EventArgs) _
        Handles Command5.Click
        Dim i As Integer
        List1.Items.Clear()

        For i = 1 To GItemCol.Count
            If GItemCol(i).ItemName Like Trim(txtdisplaychoose.Text) Then
                List1.Items.Add(GItemCol(i).ItemName)
            End If
        Next i
    End Sub

    Private Sub opcItemview_Load(ByVal EventSender As System.Object, ByVal EventArgs As System.EventArgs) _
        Handles MyBase.Load
        mok = False
        Dim i As Integer
        List1.Items.Clear()

        For i = 1 To GItemCol.Count

            List1.Items.Add(GItemCol(i).ItemName)
        Next i
    End Sub

    Private Sub List1_DoubleClick(ByVal EventSender As System.Object, ByVal EventArgs As System.EventArgs) _
        Handles List1.DoubleClick
        Command1_Click(Command1, New System.EventArgs())
    End Sub

   
End Class