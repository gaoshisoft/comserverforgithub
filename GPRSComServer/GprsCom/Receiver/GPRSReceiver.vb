Public Class GPRSReceiver

    Dim WithEvents DSC As DSCinterface
    Dim PhoneNumberStr As String
    Private RecordBuf As System.Collections.Generic.List(Of Record)


    Public mComSuccesstimes As Double


    Public mComfaileTimes As Double


    Private mvarDeviceAD As Short


    Private mvarRTuName As String


    Public ReceiveByteQty As Double


    Public SendoutByteQty As Double


    Private mvarMBadressQuantity As Integer
    Private WithEvents Tmr As Mytimer




 

    Public Property PhoneNumber() As String
        Get
            PhoneNumber = PhoneNumberStr
        End Get
        Set(ByVal value As String)
            PhoneNumberStr = value
        End Set
    End Property

    Private Sub DSC_DataReturn(ByVal PhoneNumber As String, ByVal value As Object, ByVal Length As Integer) Handles DSC.DataReturn

    End Sub

    Private Sub Tmr_TmrTick() Handles Tmr.TmrTick

    End Sub

    Public Sub SaveToDB(ByVal Record As Record)

    End Sub

    Public Sub GPRSDataToRecordBuf(ByVal B As Byte())

    End Sub

    Public Sub SendTimeRepToRTU()

    End Sub

    Public Sub IfOnline()

    End Sub
End Class
