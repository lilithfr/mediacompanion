
Public Class XBMC_Controller_Progress 


    Property Qcount       As Integer
    Property BufferQcount As Integer
    Property LastState    As XbmcController.S
    Property Evt          As XbmcController.E
    Property Args         As EventArgs
    Property Action       As String
    Property CurrentState As XbmcController.S
    Property Severity     As String
    Property ErrorMsg     As String
    Property ErrorCount   As Integer=0

    ReadOnly Property TotalQcount As Integer
        Get
            Return Qcount+BufferQcount
        End Get
    End Property

    ReadOnly Property Idle As Boolean
        Get
            Return  Me.Action="Ready & waiting..." and (TotalQcount=0)
      '      Return  Me.CurrentState=XbmcController.S.Ready and (TotalQcount=0)
        End Get
    End Property

    Sub New()
    End Sub

    Sub New ( 
            Qcount       As Integer,
            BufferQcount As Integer,
            LastState    As XbmcController.S,
            Evt          As XbmcController.E,
            Args         As EventArgs,
            Action       As String,
            NextState    As XbmcController.S,
            Severity     As String,
            ErrorMsg     As String
            )

        Me.Qcount       = Qcount      
        Me.BufferQcount = BufferQcount
        Me.LastState    = LastState   
        Me.Evt          = Evt   
        Me.Args         = Args   
        Me.Action       = Action      
        Me.CurrentState    = NextState   
        Me.Severity     = Severity    
        Me.ErrorMsg     = ErrorMsg    

    End Sub

End Class


