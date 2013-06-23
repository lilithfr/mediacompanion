Public Class BaseEvent

        'Public Property E        As XbmcController.E
        'Public Property Args     As EventArgs
        'Public Property Priority As PriorityQueue.Priorities 

        'Sub New()
        'End Sub

        'Sub New(ByVal e As XbmcController.E, Optional priority As PriorityQueue.Priorities=PriorityQueue.Priorities.low, Optional ByVal args As EventArgs = Nothing)
        '    Me.E        = e
        '    Me.Priority = priority
        '    Me.Args     = args
        'End Sub


        Public Property E        As XbmcController.E
        Public Property Args     As BaseEventArgs
        Public Property Ts       As DateTime

        Sub New()
        End Sub

        Sub New(ByVal e As XbmcController.E, Optional ByVal args As BaseEventArgs = Nothing)
            Me.E    = e
            Me.Args = args
            Me.Ts   = DateTime.Now
        End Sub

        Sub New(ByVal e As XbmcController.E, Optional ByVal priority As PriorityQueue.Priorities = PriorityQueue.Priorities.low)
            Me.E    = e
            Me.Args = New BaseEventArgs(priority)
            Me.Ts   = DateTime.Now
        End Sub

        Overrides Function ToString As String
            Return  GetTimeStamp + " " + Args.Priority.ToString + " " + E.ToString + " " + Args.ToString
        End Function

        Function GetTimeStamp As String
            Return Format(Ts, "HH:mm:ss.fff").ToString
        End Function

End Class
