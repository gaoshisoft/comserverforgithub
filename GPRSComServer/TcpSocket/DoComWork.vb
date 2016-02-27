Option Strict Off
Option Explicit On

Imports VB = Microsoft.VisualBasic
Imports System.Net
Imports System.Net.Sockets
Imports System.Text

Public Class DoComWork
    Dim WithEvents Tmr As Timer
    Dim CmdBuff As New Collection

    Structure WrtCmd
        Dim Pname As String
        Dim V As String
    End Structure


    Function GetResponseFrame(ByVal C As String) As String
        Dim r As String
        'Dim i As Integer
        Dim itm As CItem
        If C = "R" Then
            r = "R{"
            For Each itm In GItemCol
                If r = "R{" Then
                    r = r & itm.ItemName & ":" & itm.GetDTstr & ":" & CStr(itm.FloatV)
                Else
                    r = r & "," & itm.ItemName & ":" & itm.GetDTstr & ":" & CStr(itm.FloatV)
                End If

            Next itm
            r = r & "}"
        End If
        If C = "N" Then
            r = "N{"
            For Each itm In GItemCol
                If r = "N{" Then
                    r = r & itm.ItemName & ":" & itm.GetDTstr & ":" & CStr(itm.FloatV)
                Else
                    r = r & "," & itm.ItemName & ":" & itm.GetDTstr & ":" & CStr(itm.FloatV)
                End If

            Next itm
            r = r & "}"
        End If
        Dim P As String
        Dim V As String
        Dim s As String
        If VB.Left(C, 1) = "W" Then
            s = VB.Right(C, Len(C) - 1) '去"W"
            s = VB.Right(s, Len(s) - 1) '去"{"
            s = VB.Left(s, Len(s) - 1) '去"}"


            P = Split(s, ":")(0)
            V = Split(s, ":")(1)
            GItemCol(P).WriteDevice(V)
            r = C

        End If
        GetResponseFrame = r
    End Function

    Sub SetData(ByVal Pname As String, ByVal V As String)
        Dim W As WrtCmd = New WrtCmd With {.Pname = Pname, .V = V}
        CmdBuff.Add(W)
        If CmdBuff.Count > 0 Then

            Tmr.Start()
        End If
    End Sub

    Public Sub RemWriteValue(ByVal Pname As String, ByVal V As String)
        Dim Citem As CItem

        Citem = GItemCol(Pname)

        Citem.WriteDevice(Convert.ToSingle(V))
    End Sub

    Private Sub Tmr_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tmr.Tick
        If CmdBuff.Count > 0 Then
            Dim W As WrtCmd
            W = CmdBuff(1)

            RemWriteValue(W.Pname, W.V)
            CmdBuff.Remove(1)
        Else
            Tmr.Stop()
        End If
    End Sub

    Public Sub New()
        Tmr = New Timer
        Tmr.Interval = 100
    End Sub
    Sub StopMe()
        Me.Tmr.Enabled = False
    End Sub

    Protected Overrides Sub Finalize()
        Tmr.Enabled = False
        Tmr = Nothing
        MyBase.Finalize()
    End Sub
End Class
