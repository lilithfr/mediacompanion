
Public Structure str_MovieActors
    Public actorname As String
    Public actorrole As String
    Public actorthumb As String
    Public actorid As String
    Sub New(SetDefaults As Boolean) 'When called with new keyword & boolean constant SetDefault (either T or F), initialises all values to defaults to avoid having some variables left as 'nothing'
        actorname = ""
        actorrole = ""
        actorthumb = ""
        actorid = ""
    End Sub

    Shared Widening Operator CType(ByVal Input As Media_Companion.Actor) As str_MovieActors
        Dim Temp As New str_MovieActors(True)

        Temp.actorid = Input.ActorId.Value
        Temp.actorname = Input.Name.Value
        Temp.actorrole = Input.Role.Value
        Temp.actorthumb = Input.Thumb.Value

        Return Temp
    End Operator

    Shared Widening Operator CType(ByVal Input As str_MovieActors) As Media_Companion.Actor
        Dim Temp As New Media_Companion.Actor

        Temp.ActorId.Value = Input.actorid
        Temp.Name.Value = Input.actorname
        Temp.Role.Value = Input.actorrole
        Temp.Thumb.Value = Input.actorthumb

        Return Temp
    End Operator
    
End Structure
