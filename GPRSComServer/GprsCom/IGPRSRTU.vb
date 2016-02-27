Namespace GprsCom
    Public Interface IGPRSRTU
        Property CommInfo() As String

        Property RtuName() As String

        Property PollTime() As Integer

        Property IfOnline() As Boolean
        Property PollEnable() As Boolean

        Property ComSuccesstimes() As Integer

        Property ComfaileTimes() As Integer





    End Interface
End Namespace