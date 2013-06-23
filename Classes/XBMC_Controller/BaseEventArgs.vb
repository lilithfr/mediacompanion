﻿Public Class BaseEventArgs : Inherits EventArgs

    Public Property Priority As PriorityQueue.Priorities 

    Sub New(Optional priority As PriorityQueue.Priorities = PriorityQueue.Priorities.low)
        Me.Priority = priority
    End Sub

    Overrides Function ToString As String
        Return ""
    End Function

End Class
