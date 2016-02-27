VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   6045
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   8685
   LinkTopic       =   "Form1"
   ScaleHeight     =   6045
   ScaleWidth      =   8685
   StartUpPosition =   3  '窗口缺省
   Begin VB.TextBox Text1 
      Height          =   1860
      Left            =   6120
      TabIndex        =   3
      Text            =   "Text1"
      Top             =   1485
      Width           =   2490
   End
   Begin VB.CommandButton Command1 
      Caption         =   "Command1"
      Height          =   465
      Left            =   6480
      TabIndex        =   2
      Top             =   4725
      Width           =   1680
   End
   Begin VB.Timer Timer1 
      Interval        =   1000
      Left            =   6390
      Top             =   3360
   End
   Begin VB.ListBox List1 
      Height          =   4020
      Left            =   1260
      TabIndex        =   0
      Top             =   450
      Width           =   4830
   End
   Begin VB.Label Label1 
      Caption         =   "Label1"
      Height          =   465
      Left            =   1125
      TabIndex        =   1
      Top             =   5130
      Width           =   3570
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Dim Dsc As Dsc

Private Sub Command1_Click()
Dim Data As String
Dim ut As Double

Dsc.AskForDataBycommand "asddfsd", List1.List(0), 10, Data, ut
Text1.Text = Data
End Sub

Private Sub Form_Load()
BuildDsc 宏电
Set Dsc = GetDscs("宏电")
   Label1.Caption = Dsc.StartService(5002)
End Sub

Private Sub Form_Unload(Cancel As Integer)
Dsc.StopService
End Sub

Private Sub Timer1_Timer()
List1.Clear
For i = 1 To Dsc.OnlineDtus.Count
    List1.AddItem Dsc.OnlineDtus(i).PhoneNumber
    Next i
End Sub
